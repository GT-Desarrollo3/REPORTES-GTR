using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using Negocio;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using System.Drawing.Drawing2D;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using System.Net;
using ReportesTranspesa.Sistema;
using System.Diagnostics;
using ReportesTranspesa.Properties;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;
using Comun;
using ReportesTranspesa.Formularios.Areas.Operaciones;
using ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes;
using ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas;
using ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto;
using ReportesTranspesa.Formularios.Areas.Operaciones.LavadoUnidades;
using ReportesTranspesa.Formularios.Areas.Combustible;
using ReportesTranspesa.Formularios.Areas.Operaciones.EntregaUnidad;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias;
using ReportesTranspesa.Formularios.Areas.Operaciones.ItinerarioViajes;
using ReportesTranspesa.Formularios.Areas.Operaciones.TiemposViaje;
using ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems;


namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmOperacion_Previajes : Form
    {
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public string var_Programa;
        string var_DESPrograma;
        public int Prog = 0;
        public int Prog1 = 0;
        public int Prog2 = 0;
        public int Prog3 = 0;
        public int Prog4 = 0;
        public string Respuesta;
        public string NroRPTA;
        private int crea1_modifi2_anula3 = 0;
        DataTable dtListaProgramaciones = new DataTable(); //datatable para listar las programaciones
        DataTable dtTipoProg = new DataTable();//datatable para listar los tipos de programacion
        DataTable dtColumnasSeleccionadas = new DataTable();//datatable para listar las columnas seleccionadas por usuario
        DataTable dtColumnasSeleccionadasExcel = new DataTable();//datatable para listar las columnas seleccionadas por usuario

        DataTable dtPermisos3 = new DataTable(), dtEspeciales2 = new DataTable();
        DataTable dtPermisos5 = new DataTable(), dtPermisos6 = new DataTable(), dtPermisos7 = new DataTable();
       
        public int idproganula;
        public string var_Desctipo, var_DesTracto, var_Desremolque;
        int contador = 0;
        public string Val_Respuesta = "0";
        public string Val_frmSeleccionados = "0";
        private Boolean flag;
        string imagen;
        string imagencaja;
        int conteoColumnas = 0;
        string NOMBRECOLUMNA;
        string NOMBRECOLUMNAexcel;
        int conChocope = 0;
        int conChocopeAtendidos = 0;
        int valtotalProgramachoco = 0;
        int valtotalAtendidoChocope = 0;
        float resultSinChocope;
        int Tiporder = 1;

        Bitmap abasteciosi = Properties.Resources.abasteciop;
        Bitmap abasteciono = Properties.Resources.estacionno;
        Bitmap gastosi = Properties.Resources.gastosentregados;
        Bitmap gastono = Properties.Resources.gastospendientes;

        string NombreImpresora;
        string _codigo, _sucursal, _tipoprogramacion, _fechaprogramacion, _placa, _ruta, _cliente, _conductor, _producto, _programador, _dni;
        public DataTable dtControlEstado = new DataTable();

        public frmOperacion_Previajes()
        {
            InitializeComponent();
        }
        
        private void frmOperacion_Previajes_Load(object sender, EventArgs e)
        {
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaFallasMecanicas");
            if (dtPermisos != null)
            { 
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true) { registroDeFallasMecánicasToolStripMenuItem.Enabled = true; }
                    else { registroDeFallasMecánicasToolStripMenuItem.Enabled = false; }
                }
            }

            DataTable dtPermisos2 = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistroFaltantesMercaderia");
            if (dtPermisos2 != null)
            {
                if (dtPermisos2.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos2.Rows[0]["Leer"]) == true) { mostrarFaltantesDeViajeToolStripMenuItem.Enabled = true; }
                    else { mostrarFaltantesDeViajeToolStripMenuItem.Enabled = false; }
                }
            }

            dtPermisos3 = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListarPlanillas");

            if (dtPermisos3 != null)
            {
                if (dtPermisos3.Rows[0]["PermisosEspeciales"].ToString() != "")
                {
                    dtEspeciales2 = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos3.Rows[0]["PermisosEspeciales"].ToString());
                }
            }

            if (dtEspeciales2.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales2.Rows.Count; i++)
                {
                    if (dtEspeciales2.Rows[i]["NombrePermiso"].ToString() == "Viatico sin Viaje")
                    {
                        asignarViaticoAdicionalToolStripMenuItem.Enabled = true;
                        i = 999;
                    }
                    else { asignarViaticoAdicionalToolStripMenuItem.Enabled = false; }
                }
            }
            else { asignarViaticoAdicionalToolStripMenuItem.Enabled = false; }

            try
            {
                DataTable dtPermisos4 = Utilitario.Instancia.ObtenerPermisosPorFormulario("Operaciones_Programacion_Previajes");
                DataTable dtEspeciales4 = null;
                if (dtPermisos4 != null)
                {
                    if (dtPermisos4.Rows[0]["PermisosEspeciales"].ToString() != "")
                    { dtEspeciales4 = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos4.Rows[0]["PermisosEspeciales"].ToString()); }
                }
                if (dtEspeciales4 != null)
                {
                    for (int i = 0; i < dtEspeciales4.Rows.Count; i++)
                    {
                        if (dtEspeciales4.Rows[i]["NombrePermiso"].ToString() == "Maestro Gasto X Ruta")
                        {
                            maestroDeZonasDeRutaToolStripMenuItem.Enabled = true;
                            i = 999;
                        }
                        else { maestroDeZonasDeRutaToolStripMenuItem.Enabled = false; }
                    }

                    for (int i = 0; i < dtEspeciales4.Rows.Count; i++)
                    {
                        if (dtEspeciales4.Rows[i]["NombrePermiso"].ToString() == "Ver Planillas Pendientes")
                        {
                            verPlanillasPendientesToolStripMenuItem.Enabled = true;
                            i = 999;
                        }
                        else { verPlanillasPendientesToolStripMenuItem.Enabled = false; }
                    }

                   /* for (int i = 0; i < dtEspeciales4.Rows.Count; i++)
                    {
                        if (dtEspeciales4.Rows[i]["NombrePermiso"].ToString() == "Crear Viaje")
                        {
                            verPlanillasPendientesToolStripMenuItem.Enabled = true;
                            i = 999;
                        }
                        else { verPlanillasPendientesToolStripMenuItem.Enabled = false; }
                    }*/
                }
                else
                {
                    maestroDeZonasDeRutaToolStripMenuItem.Enabled = false;
                    verPlanillasPendientesToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                maestroDeZonasDeRutaToolStripMenuItem.Enabled = false;
                verPlanillasPendientesToolStripMenuItem.Enabled = false;
            }

            try
            {
                DataTable dtPermisos6 = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmItinerarioViajes");
                if (dtPermisos6 != null)
                {
                    if (dtPermisos6.Rows.Count > 0)
                    {
                        if (Convert.ToBoolean(dtPermisos6.Rows[0]["Leer"]) == true) { tsItinerarioViajes.Enabled = true; }
                        else { tsItinerarioViajes.Enabled = false; }
                    }
                }
            }
            catch { tsItinerarioViajes.Enabled = false; }

            dtPermisos7 = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaTiemposViaje");

            try
            {
                if (dtPermisos7 != null)
                {
                    if (Convert.ToBoolean(dtPermisos7.Rows[0]["Nuevo"]) == true) { tsRegistroTiempos.Enabled = true; }
                    else { tsRegistroTiempos.Enabled = false; }
                }
                else { tsRegistroTiempos.Enabled = false; }
            }
            catch { tsRegistroTiempos.Enabled = false; }

            radioButton1.Enabled = false;
            radioButton2.Enabled = false;

            contador = 1;
            splitContainer2.Panel2Collapsed = true;
            DataTable dtSucursal = new DataTable();

            dtTipoProg = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeOperaciones(Utilitario.Instancia.SesionUsuario.usuario);
            dtSucursal = clsOperacionesBL.Instancia.GetOperaciones_ListarSucursalPreviajes(Utilitario.Instancia.SesionUsuario.usuario);

            if (dtTipoProg.Rows.Count == 0)
            {
                MessageBox.Show("Usted no tiene permisos de sucursal o de tipo de programacion favor solicitarlos, SP: [ReportesApp_Operaciones_ListarSucursalPreviaje] !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            comboBox1.DisplayMember = "Descripcion";
            comboBox1.ValueMember = "IdOperacion";
            comboBox1.DataSource = dtTipoProg;

            cboSucursal.DisplayMember = "Sucursal";
            cboSucursal.DataSource = dtSucursal;

            for (int i = 0; i < dtTipoProg.Rows.Count; i++)
            {
                var_Programa += dtTipoProg.Rows[i]["IdOperacion"].ToString() + ",";
                var_DESPrograma += dtTipoProg.Rows[i]["Descripcion"].ToString() + ",";
            }

            CargarControlEstados();
            CargarDatos();

        }

        private void CargarControlEstados()
        {
            dtControlEstado = clsOperacionesBL.Instancia.Llenar_ControlesPreViajes();
        }


        // string MostrarProgramacion;
        private void button1_Click(object sender, EventArgs e)
        {
            gcExcel.DataSource = null;
            gvexcel.Columns.Clear();
            for (int i = 0; i < dtTipoProg.Rows.Count; i++)
            {
                if (comboBox1.Text.ToString() == dtTipoProg.Rows[i]["Descripcion"].ToString())
                    var_Accesos = Convert.ToInt32(dtTipoProg.Rows[i]["ACCESOS"].ToString());
            }
            if (cboSucursal.Text.Equals("TODO"))
            {
                MessageBox.Show("Selecciona tu Sucursal Para crear Programaciones...!", "Aviso");
                return;
            }
            if (comboBox1.Text.Equals("TODO"))
            {
                MessageBox.Show("Selecciona un Tipo de Programación...!", "Aviso");
                return;
            }

            if (var_Accesos == 2)
            {
                int IDPROGRAMA = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                string PROGRAMACION = comboBox1.Text.ToString();
                string Sucursal = cboSucursal.Text.ToString();
                crea1_modifi2_anula3 = 1;
                frmNuevoPreviaje_Programacion frm2 = new frmNuevoPreviaje_Programacion();
                frm2.setearvariable(crea1_modifi2_anula3, IDPROGRAMA, PROGRAMACION, 1, Sucursal, 1, "", "", "", "", 0, "", 0, "", 0, 0, 0, var_nomConductor, var_nomApoyo, var_nomCliente, var_nomProducto,
                                    var_nomRuta, 0, "", 1, 1, "", "", "", var_PesoAlmacen, var_PesoCliente, var_Merma, "", "", "", "", var_Serie, var_Numero, var_Remitente, "", "", "", "", "", "",
                                    var_Accesos, 0, var_Viaticos, "", "", "", "", var_MontoOrden, "", 0, "", "","", "0", 0, 0, "", "", "", "", "","","");

                frm2.dtControlesPreviajes = dtControlEstado;
                frm2.ShowDialog(this);
                if (Val_Respuesta.Equals("1"))
                {
                    CargarDatos();
                }
            }
            else
            {
                MessageBox.Show("Usted no tiene acceso para crear Programaciones...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void cargarTransportista(FrmGuiaElectronicaTransportista transportista)
        {
            transportista.FormClosed -= transportista.FrmGuiaElectronicaTransportista_FormClosed;
            transportista.Close();
            CargarDatos();
        }


        public void CargarDatos()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                //LISTADO DE LAS COLUMNAS DEFINIDAS POR EL USUARIO
                dtColumnasSeleccionadas = clsOperacionesBL.Instancia.GetOperaciones_Previaje_ListarColumnasSeleccionadasxUsuario(Utilitario.Instancia.SesionUsuario.usuario);

                //LISTADO DE LAS PROGRAMACIONES
                dtListaProgramaciones = clsOperacionesBL.Instancia.GetOperaciones_ListarProgramacionesPorProgramacion(cboSucursal.Text, dtpFechaProInicio.Text, dtpFechProFin.Text, Convert.ToInt32(comboBox1.SelectedValue.ToString()));

                string Valorturno;
                string valorEstado;
                int turPrimero = 0;
                int turSegundo = 0;
                int turTercero = 0;
                int turAdelanto = 0;
                int valTotalProgramados = 0, valAtendidos = 0, valAnulados = 0;
                conChocope = 0;
                conChocopeAtendidos = 0;
                valtotalProgramachoco = 0;
                valtotalAtendidoChocope = 0;

                //SI LA OPCION SELECCIONADA ES TODO - TODO OCULTAMOS LAS IMAGENES PARA ACELERAR EL CARGADO DE LAS IMAGENES 
                if (!cboSucursal.Text.Equals("TODO") && !comboBox1.Text.ToString().Equals("TODO"))
                {
                    dtListaProgramaciones.Columns.Add("ABASTECIDO", typeof(String));
                    dtListaProgramaciones.Columns.Add("CAJA", typeof(String));
                    /*
                    dtListaProgramaciones.Columns.Add("ABASTECIDO", Type.GetType("System.Byte[]"));
                    dtListaProgramaciones.Columns.Add("CAJA", Type.GetType("System.Byte[]"));
                    */
                }

                if (dtListaProgramaciones.Rows.Count > 0)
                {
                    for (int i = 0; i < dtListaProgramaciones.Rows.Count; i++)
                    {
                        // string PRUEBAS = dtListaProgramaciones.Rows[i]["CONDUCTOR"].ToString();
                        Valorturno = dtListaProgramaciones.Rows[i]["TURNO"].ToString();
                        valorEstado = dtListaProgramaciones.Rows[i]["ESTADO"].ToString();
                        imagen = dtListaProgramaciones.Rows[i]["ABASTECIO"].ToString();
                        imagencaja = dtListaProgramaciones.Rows[i]["ESTADOCAJA"].ToString();

                        //CONTADORES SEGUN EL REQUERIMIENTO
                        if (Valorturno.Equals("1"))
                        {
                            turPrimero = turPrimero + 1;
                        }
                        if (Valorturno.Equals("2"))
                        {
                            turSegundo = turSegundo + 1;
                        }
                        if (Valorturno.Equals("3"))
                        {
                            turTercero = turTercero + 1;
                        }
                        if (Valorturno.Equals("A"))
                        {
                            turAdelanto = turAdelanto + 1;
                        }
                        if (valorEstado.Trim().Equals("PROGRAMADO"))
                        {
                            valTotalProgramados = valTotalProgramados + 1;
                        }
                        if (valorEstado.Trim().Equals("ATENDIDO"))
                        {
                            valAtendidos = valAtendidos + 1;
                        }
                        if (valorEstado.Trim().Equals("ANULADO"))
                        {
                            valAnulados = valAnulados + 1;
                        }

                        if (dtListaProgramaciones.Rows[i]["DESTINO"].ToString().Equals("CHOCOPE"))
                        {
                            conChocope = conChocope + 1;
                        }

                        if (dtListaProgramaciones.Rows[i]["IDRUTA"].ToString().Equals("149") /*&& dtListaProgramaciones.Rows[i]["ESTADO"].ToString().Equals("ATENDIDO")*/)
                        {
                            conChocopeAtendidos = conChocopeAtendidos + 1;
                        }


                        //CARGADO DE DE LAS COLUMNAS CON IMAGENES

                        if (!cboSucursal.Text.Equals("TODO") && !comboBox1.Text.ToString().Equals("TODO"))
                        {
                            foreach (DataRow drow in dtListaProgramaciones.Rows)
                            {
                                try
                                {
                                    if (imagen.Equals("SI"))
                                    {
                                        dtListaProgramaciones.Rows[i]["ABASTECIDO"] = "AB";
                                        //dtListaProgramaciones.Rows[i]["ABASTECIDO"] = imageToByteArray(abasteciosi);
                                        // imageToByteArray(abasteciono); //= File.ReadAllBytes("R:\\imagen\\abasteciop.jpg");
                                        //dtListaProgramaciones.Rows[i]["ABASTECIDO"] = imageToByteArray(abasteciosi);
                                        //dtListaProgramaciones.Rows.Add("ABASTECIDO",Properties.Resources.abasteciop);
                                    }
                                    else
                                    {
                                        dtListaProgramaciones.Rows[i]["ABASTECIDO"] = " ";
                                        //dtListaProgramaciones.Rows[i]["ABASTECIDO"] = imageToByteArray(abasteciono);
                                                                                      // File.ReadAllBytes("R:\\imagen\\estacionno.jpg");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }

                            foreach (DataRow drow in dtListaProgramaciones.Rows)
                            {
                                try
                                {
                                    if (imagencaja.Equals("PAGADO"))
                                    {
                                        dtListaProgramaciones.Rows[i]["CAJA"] = "PA";
                                        //dtListaProgramaciones.Rows[i]["CAJA"] = imageToByteArray(gastosi); //File.ReadAllBytes("R:\\imagen\\gastosentregados.jpg");
                                    }
                                    else
                                    {
                                        dtListaProgramaciones.Rows[i]["CAJA"] = " ";
                                        //dtListaProgramaciones.Rows[i]["CAJA"] = imageToByteArray(gastono); //File.ReadAllBytes("R:\\imagen\\gastospendientes.jpg");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                    }

                    if (comboBox1.Text.ToString().Equals("LINDLEY") || comboBox1.Text.ToString().Equals("VOLCAN"))
                    {
                        lblPrimTurno.Text = "1° Turno: " + turPrimero;
                        lblSegTurno.Text = "2° Turno: " + turSegundo;
                        lblTercTurno.Text = "3° Turno: " + turTercero;
                        lblAdelanto.Text = "Adelantos: " + turAdelanto;
                        btnPorcentaje.Visible = true;
                        checkBox1.Visible = true;
                        button6.Visible = false;
                    }

                    int totalprograma = Convert.ToInt32(dtListaProgramaciones.Rows.Count.ToString());
                    tslTotal.Text = "TOTAL PROGRAMACIONES: " + dtListaProgramaciones.Rows.Count.ToString();
                    tslProgramados.Text = "TOTAL PROGRAMADO: " + valTotalProgramados;
                    tslAtendidos.Text = "TOTAL ATENDIDO: " + valAtendidos;
                    tslAnulados.Text = "TOTAL ANULADO: " + valAnulados;

                    valtotalProgramachoco = totalprograma;
                    valtotalAtendidoChocope = valAtendidos;

                    //Calculando el porcentaje total
                    int entero = (totalprograma - valAnulados);
                    float result = ((valAtendidos * 100) / entero);
                    btnPorcentaje.Text = result.ToString() + "%";
                    if (result < 30)
                    {
                        btnPorcentaje.BackColor = Color.Red;
                    }
                    else if (result < 60 && result >= 30)
                    {
                        btnPorcentaje.BackColor = Color.Yellow;
                    }
                    else if (result > 60)
                    {
                        btnPorcentaje.BackColor = Color.Lime;
                    }

                    //Calculando el porcentaje cuando es sin chcocope
                    int calcular = valAtendidos - conChocopeAtendidos;
                    if (calcular > 0)
                    {
                        int enterosinchocope = (entero - conChocope);
                        resultSinChocope = (((valAtendidos - conChocopeAtendidos) * 100) / enterosinchocope);
                        btnSinchocope.Text = resultSinChocope + "%";
                    }
                    else
                    {
                        btnSinchocope.Text = "0%";
                    }

                    //ORDENA LOS DESTINOS POR LA ORDEN ASIGNADA
                    if (chbOrdenDestino.Checked == true)
                    {
                        DataView dtV = dtListaProgramaciones.DefaultView;
                        dtV.Sort = "ORDENDESTINO ASC";
                        dtListaProgramaciones = dtV.ToTable();
                    }
                   
                    dgvPreviajes.DataSource = dtListaProgramaciones;
                    gcExcel.DataSource = dtListaProgramaciones;

                    /*************TEMPORAL****************************************************************/
                    gvexcel.Columns["IDPROGRAMACION"].Visible = false;
                    gvexcel.Columns["IDTIPO"].Visible = false;
                    gvexcel.Columns["IDTRACTO"].Visible = false;
                    gvexcel.Columns["IDREMOLQUE"].Visible = false;
                    gvexcel.Columns["IDAPOYO"].Visible = false;
                    gvexcel.Columns["IDCLIENTE"].Visible = false;
                    //gvexcel.Columns["FECHA_INICIO"].Visible = false;
                    //gvexcel.Columns["FECHA_FIN"].Visible = false;
                    gvexcel.Columns["IDCONDUCTOR"].Visible = false;
                    gvexcel.Columns["IDRUTA"].Visible = false;
                    gvexcel.Columns["IDPRODUCTO"].Visible = false;
                    gvexcel.Columns["IDESTADO"].Visible = false;
                    gvexcel.Columns["GUIAENTREGA"].Visible = false;
                    gvexcel.Columns["VIATICOS"].Visible = false;
                    gvexcel.Columns["1° CAMBIO"].Visible = false;
                    gvexcel.Columns["2° CAMBIO"].Visible = false;
                    gvexcel.Columns["3° CAMBIO"].Visible = false;
                    gvexcel.Columns["CONDUCTOR_INICIO"].Visible = false;
                    gvexcel.Columns["TIPOUNIDAD"].Visible = false;
                    gvexcel.Columns["IDVIAJE"].Visible = false;
                    gvexcel.Columns["SERIE"].Visible = false;
                    gvexcel.Columns["GUIA"].Visible = false;
                    gvexcel.Columns["SALIDA"].Visible = false;
                    gvexcel.Columns["LLEGADA"].Visible = false;
                    gvexcel.Columns["CANTIDADDIA"].Visible = false;
                    gvexcel.Columns["USUARIOMOD"].Visible = false;
                    gvexcel.Columns["FECHAMOD"].Visible = false;
                    gvexcel.Columns["INTERNA"].Visible = false;
                    gvexcel.Columns["UBICACION"].Visible = false;
                    gvexcel.Columns["UBIGEOPARTIDA"].Visible = false;
                    gvexcel.Columns["UBIGEOLLEGADA"].Visible = false;
                    gvexcel.Columns["DNI"].Visible = false;
                    gvexcel.Columns["ANIO"].Visible = false;
                    gvexcel.Columns["MAQUINARIA"].Visible = false;
                    gvexcel.Columns["ENLACE"].Visible = false;
                    gvexcel.Columns["ESTADOCAJA"].Visible = false;
                    gvexcel.Columns["ENTREGADO"].Visible = false;
                    /**************************************************************************************/
                    dgvPreviajes.Columns["IDPROGRAMACION"].Visible = false;
                    dgvPreviajes.Columns["IDTIPO"].Visible = false;
                    dgvPreviajes.Columns["IDTRACTO"].Visible = false;
                    dgvPreviajes.Columns["IDREMOLQUE"].Visible = false;
                    dgvPreviajes.Columns["IDAPOYO"].Visible = false;
                    dgvPreviajes.Columns["IDCLIENTE"].Visible = false;
                    dgvPreviajes.Columns["FECHA_INICIO"].Visible = false;
                    dgvPreviajes.Columns["FECHA_FIN"].Visible = false;
                    dgvPreviajes.Columns["IDCONDUCTOR"].Visible = false;
                    dgvPreviajes.Columns["IDRUTA"].Visible = false;
                    dgvPreviajes.Columns["IDPRODUCTO"].Visible = false;
                    dgvPreviajes.Columns["IDESTADO"].Visible = false;
                    dgvPreviajes.Columns["GUIAENTREGA"].Visible = false;
                    dgvPreviajes.Columns["VIATICOS"].Visible = false;
                    dgvPreviajes.Columns["1° CAMBIO"].Visible = false;
                    dgvPreviajes.Columns["2° CAMBIO"].Visible = false;
                    dgvPreviajes.Columns["3° CAMBIO"].Visible = false;
                    dgvPreviajes.Columns["CONDUCTOR_INICIO"].Visible = false;
                    dgvPreviajes.Columns["TIPOUNIDAD"].Visible = false;
                    dgvPreviajes.Columns["IDVIAJE"].Visible = false;
                    dgvPreviajes.Columns["SERIE"].Visible = false;
                    dgvPreviajes.Columns["GUIA"].Visible = false;
                    dgvPreviajes.Columns["SALIDA"].Visible = false;
                    dgvPreviajes.Columns["LLEGADA"].Visible = false;
                    dgvPreviajes.Columns["MONTORDEN"].Visible = false;
                    dgvPreviajes.Columns["CANTIDADDIA"].Visible = false;
                    dgvPreviajes.Columns["USUARIOMOD"].Visible = false;
                    dgvPreviajes.Columns["FECHAMOD"].Visible = false;
                    dgvPreviajes.Columns["INTERNA"].Visible = false;
                    dgvPreviajes.Columns["UBICACION"].Visible = false;
                    dgvPreviajes.Columns["UBIGEOPARTIDA"].Visible = false;
                    dgvPreviajes.Columns["UBIGEOLLEGADA"].Visible = false;
                    dgvPreviajes.Columns["DNI"].Visible = false;
                    dgvPreviajes.Columns["ANIO"].Visible = false;
                    dgvPreviajes.Columns["MAQUINARIA"].Visible = false;
                    dgvPreviajes.Columns["ENLACE"].Visible = false;
                    dgvPreviajes.Columns["ESTADOCAJA"].Visible = false;
                    dgvPreviajes.Columns["ENTREGADO"].Visible = false;
                    dgvPreviajes.Columns["ORDENDESTINO"].Visible = false;
                    //dgvPreviajes.Columns["IMPRESO"].Visible = false;
                    dgvPreviajes.RowHeadersVisible = false;
                    dgvPreviajes.Columns["ITEM"].Visible = false;
                    dgvPreviajes.Columns["CODIGO"].Visible = false;
                    dgvPreviajes.Columns["ABASTECIO"].Visible = false;
                    dgvPreviajes.Columns["ZONA"].Visible = false;
                    dgvPreviajes.Columns["FECHAREGISTRO"].Visible = false;
                    dgvPreviajes.Columns["FECHAPROGR"].Visible = false;
                    dgvPreviajes.Columns["FECHA_INICIO"].Visible = false;
                    dgvPreviajes.Columns["FECHA_FIN"].Visible = false;
                    dgvPreviajes.Columns["TRACTO"].Visible = false;
                    dgvPreviajes.Columns["TIPOUNIDAD"].Visible = false;
                    dgvPreviajes.Columns["UNIDAD"].Visible = false;
                    dgvPreviajes.Columns["PROVEEDOR"].Visible = false;
                    dgvPreviajes.Columns["SEMIRREMOLQUE"].Visible = false;
                    dgvPreviajes.Columns["TIPOSEMIR"].Visible = false;
                    dgvPreviajes.Columns["CONDUCTOR"].Visible = false;
                    dgvPreviajes.Columns["UNIDADAPOYO"].Visible = false;
                    dgvPreviajes.Columns["CONDUCTOR_APOYO"].Visible = false;
                    dgvPreviajes.Columns["IDCLIENTE"].Visible = false;
                    dgvPreviajes.Columns["RUTA"].Visible = false;
                    dgvPreviajes.Columns["DESTINO"].Visible = false;
                    dgvPreviajes.Columns["PRODUCTO"].Visible = false;
                    dgvPreviajes.Columns["ESTADO"].Visible = false;
                    dgvPreviajes.Columns["CLIENTE"].Visible = false;
                    dgvPreviajes.Columns["DESCARGA"].Visible = false;
                    dgvPreviajes.Columns["VIAJE"].Visible = false;
                    dgvPreviajes.Columns["ESTADOVIAJE"].Visible = false;
                    dgvPreviajes.Columns["PESO"].Visible = false;
                    dgvPreviajes.Columns["PESOCLIENTE"].Visible = false;
                    dgvPreviajes.Columns["MERMA"].Visible = false;
                    dgvPreviajes.Columns["PLANILLA"].Visible = false;
                    dgvPreviajes.Columns["TURNO"].Visible = false;
                    dgvPreviajes.Columns["OT"].Visible = false;
                    dgvPreviajes.Columns["ORDENSERVICIO"].Visible = false;
                    dgvPreviajes.Columns["SUCURSAL"].Visible = false;
                    dgvPreviajes.Columns["TIPO"].Visible = false;
                    dgvPreviajes.Columns["GUIAT."].Visible = false;
                    dgvPreviajes.Columns["G/R"].Visible = false;
                    dgvPreviajes.Columns["OBSERVACION"].Visible = false;
                    dgvPreviajes.Columns["COMENTARIO"].Visible = false;
                    dgvPreviajes.Columns["STATUSPLANTA"].Visible = false;
                    dgvPreviajes.Columns["LineaOT"].Visible = false;
                    dgvPreviajes.Columns["FAC"].Visible = false;

                    //VALIDAMOS LAS COLUMNAS DEL USUARIO CON LAS COLUMNAS DE LA CONSULTA Y MOSTRAMOS SOLO LAS QUE COINCIDEN
                    for (int i = 0; i < dtColumnasSeleccionadas.Rows.Count; i++)
                    {
                        string NOMBRECOLUMNA = dtColumnasSeleccionadas.Rows[i]["DESCRIPCION"].ToString();
                        dgvPreviajes.Columns[NOMBRECOLUMNA].Visible = true;
                    }

                    //APLICAMOS LOS FILTROS REQUERIDOS
                    if (!string.IsNullOrEmpty(textBox1.Text))
                    {
                        textBox1_TextChanged(this, new KeyPressEventArgs((char)(Keys.Enter)));
                    }

                    if (chbFiltroHabilita.Checked == true && cboFiltrosVarios.Text.Equals("CONSOLIDADOS"))
                    {
                        string colFiltrar = "VIAJE";
                        ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, "C");
                    }
                    if (radioButton1.Checked == true && radioButton2.Checked == false && cboEstados.Enabled == true)
                    {
                        string colFiltrar = "ESTADO";
                        string dato = cboEstados.Text.ToString();
                        ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}'", colFiltrar, dato);

                    }
                    if (radioButton2.Checked == true && radioButton1.Checked == false && cboEstados.Enabled == true)
                    {
                        string colFiltrar = "ESTADOVIAJE";
                        string dato = cboEstados.Text.ToString();
                        ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}'", colFiltrar, dato);
                    }
                }
                else
                {
                    dgvPreviajes.DataSource = null;
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    if (comboBox1.Text.ToString().Equals("LINDLEY"))
                    {
                        lblPrimTurno.Text = "1° Turno: 0";
                        lblSegTurno.Text = "2° Turno:0 ";
                        lblTercTurno.Text = "3° Turno:0 ";
                        lblAdelanto.Text = "Adelantos: 0";
                        btnPorcentaje.Visible = true;

                        btnPorcentaje.Text = "0%";
                        btnPorcentaje.BackColor = Color.White;
                    }

                    if (comboBox1.Text.ToString().Equals("VOLCAN"))
                    {
                        lblPrimTurno.Text = "1° Turno: 0";
                        lblSegTurno.Text = "2° Turno:0 ";
                        lblTercTurno.Text = "3° Turno:0 ";
                        lblAdelanto.Text = "Adelantos: 0";
                        btnPorcentaje.Visible = true;

                        btnPorcentaje.Text = "0%";
                        btnPorcentaje.BackColor = Color.White;
                    }
                }
                dgvPreviajes.Focus();

                //vERIFICA CUANTAS PROGRAMACIONES PENDIENTES TIENE EL PROGRAMADOR
                DataTable DTProgramaPendientes = new DataTable();
                DTProgramaPendientes = clsOperacionesBL.Instancia.GetOperaciones_Previajes_PrograPendientes(Utilitario.Instancia.SesionUsuario.usuario);
                if (DTProgramaPendientes.Rows.Count > 0)
                {
                    for (int i = 0; i < DTProgramaPendientes.Rows.Count; i++)
                    {
                        string valConteo = DTProgramaPendientes.Rows[i]["DATOS"].ToString();
                        if (!valConteo.Equals("0"))
                        {
                            timer1.Start();
                            label2.Visible = true;
                            label2.Text = "ADVERTENCIA: Tienes " + DTProgramaPendientes.Rows[i]["DATOS"].ToString() + " programaciones por ATENDER con fechas pasadas - Regularizar antes de las 9:30 A.M";
                        }
                        else
                        {
                            timer1.Stop();
                            label2.Visible = false;
                        }
                    }
                }

                this.Cursor = Cursors.Default;

            }
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
        private void dgvPreviajes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                for (int i = 0; i < dtTipoProg.Rows.Count; i++)
                {
                    if (comboBox1.Text.ToString() == dtTipoProg.Rows[i]["Descripcion"].ToString())
                        var_Accesos = Convert.ToInt32(dtTipoProg.Rows[i]["ACCESOS"].ToString());
                }

                if (dgvPreviajes.Rows[e.RowIndex].Cells["ESTADO"].Value.ToString().Equals("ANULADO") /*|| dgvPreviajes.Rows[e.RowIndex].Cells["ESTADO"].Value.ToString().Equals("ATENDIDO")*/)
                {
                    MessageBox.Show("Programación " + dgvPreviajes.Rows[e.RowIndex].Cells["ESTADO"].Value.ToString() + ", No se peude modificar...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var_IdProg = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDPROGRAMACION"].Value.ToString());
                    var_TipoProg = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value.ToString());
                    var_Sucursal = dgvPreviajes.Rows[e.RowIndex].Cells["SUCURSAL"].Value.ToString();
                    var_Desctipo = dgvPreviajes.Rows[e.RowIndex].Cells["TIPO"].Value.ToString();
                    var_FechaProg = dgvPreviajes.Rows[e.RowIndex].Cells["FECHAPROGR"].Value.ToString();
                    var_FechaInicio = dgvPreviajes.Rows[e.RowIndex].Cells["FECHA_INICIO"].Value.ToString();
                    var_FechaFin = dgvPreviajes.Rows[e.RowIndex].Cells["FECHA_FIN"].Value.ToString();
                    var_Tracto = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDTRACTO"].Value.ToString());
                    var_DesTracto = dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString();
                    var_Remolque = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDREMOLQUE"].Value.ToString());
                    var_Desremolque = dgvPreviajes.Rows[e.RowIndex].Cells["SEMIRREMOLQUE"].Value.ToString();
                    var_Conductor = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDCONDUCTOR"].Value.ToString());
                    var_nomConductor = dgvPreviajes.Rows[e.RowIndex].Cells["CONDUCTOR"].Value.ToString();
                    var_Apoyo = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDAPOYO"].Value.ToString());
                    var_nomApoyo = dgvPreviajes.Rows[e.RowIndex].Cells["CONDUCTOR_APOYO"].Value.ToString();
                    var_Cliente = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDCLIENTE"].Value.ToString());
                    var_nomCliente = dgvPreviajes.Rows[e.RowIndex].Cells["CLIENTE"].Value.ToString();
                    var_nomRuta = dgvPreviajes.Rows[e.RowIndex].Cells["RUTA"].Value.ToString();
                    var_nomProducto = dgvPreviajes.Rows[e.RowIndex].Cells["PRODUCTO"].Value.ToString();
                    var_Ruta = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDRUTA"].Value.ToString());
                    var_Destino = dgvPreviajes.Rows[e.RowIndex].Cells["DESTINO"].Value.ToString();
                    var_Producto = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDPRODUCTO"].Value.ToString());
                    var_Estado = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDESTADO"].Value.ToString());
                    var_HoraSailda = dgvPreviajes.Rows[e.RowIndex].Cells["SALIDA"].Value.ToString();
                    var_HoraLlegada = dgvPreviajes.Rows[e.RowIndex].Cells["LLEGADA"].Value.ToString();
                    var_FechaDescarga = dgvPreviajes.Rows[e.RowIndex].Cells["DESCARGA"].Value.ToString();
                    var_PesoAlmacen = Convert.ToDecimal(dgvPreviajes.Rows[e.RowIndex].Cells["PESO"].Value.ToString());
                    var_PesoCliente = Convert.ToDecimal(dgvPreviajes.Rows[e.RowIndex].Cells["PESOCLIENTE"].Value.ToString());
                    var_Merma = Convert.ToDecimal(dgvPreviajes.Rows[e.RowIndex].Cells["MERMA"].Value.ToString());
                    var_Viaje = dgvPreviajes.Rows[e.RowIndex].Cells["VIAJE"].Value.ToString();
                    var_idViaje = dgvPreviajes.Rows[e.RowIndex].Cells["IDVIAJE"].Value.ToString();
                    var_Planilla = dgvPreviajes.Rows[e.RowIndex].Cells["PLANILLA"].Value.ToString();
                    var_Observacion = dgvPreviajes.Rows[e.RowIndex].Cells["OBSERVACION"].Value.ToString();
                    var_Serie = dgvPreviajes.Rows[e.RowIndex].Cells["SERIE"].Value.ToString();
                    var_Numero = dgvPreviajes.Rows[e.RowIndex].Cells["GUIA"].Value.ToString();
                    var_Remitente = dgvPreviajes.Rows[e.RowIndex].Cells["G/R"].Value.ToString();
                    var_Zona = dgvPreviajes.Rows[e.RowIndex].Cells["ZONA"].Value.ToString();
                    var_turno = dgvPreviajes.Rows[e.RowIndex].Cells["TURNO"].Value.ToString();
                    var_ConducInicio = dgvPreviajes.Rows[e.RowIndex].Cells["CONDUCTOR_INICIO"].Value.ToString();
                    var_PrimCambio = dgvPreviajes.Rows[e.RowIndex].Cells["1° CAMBIO"].Value.ToString();
                    var_SegCambio = dgvPreviajes.Rows[e.RowIndex].Cells["2° CAMBIO"].Value.ToString();
                    var_TerCambio = dgvPreviajes.Rows[e.RowIndex].Cells["3° CAMBIO"].Value.ToString();
                    var_OT = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["OT"].Value.ToString());
                    var_Viaticos = Convert.ToDecimal(dgvPreviajes.Rows[e.RowIndex].Cells["VIATICOS"].Value.ToString());
                    var_OrdenServicio = dgvPreviajes.Rows[e.RowIndex].Cells["ORDENSERVICIO"].Value.ToString();
                    var_tipounidad = dgvPreviajes.Rows[e.RowIndex].Cells["TIPOUNIDAD"].Value.ToString();
                    var_guiaEntrega = dgvPreviajes.Rows[e.RowIndex].Cells["GUIAENTREGA"].Value.ToString();
                    var_UnidadApoyo = dgvPreviajes.Rows[e.RowIndex].Cells["UNIDADAPOYO"].Value.ToString();
                    var_MontoOrden = Convert.ToDecimal(dgvPreviajes.Rows[e.RowIndex].Cells["MONTORDEN"].Value.ToString());
                    var_TopoSemir = dgvPreviajes.Rows[e.RowIndex].Cells["TIPOSEMIR"].Value.ToString();
                    var_Dia = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["CANTIDADDIA"].Value.ToString());
                    int IDPROGRAMAEDIT = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                    userModifica = dgvPreviajes.Rows[e.RowIndex].Cells["USUARIOMOD"].Value.ToString();
                    fechaModifica = dgvPreviajes.Rows[e.RowIndex].Cells["FECHAMOD"].Value.ToString();
                    var_Interna = dgvPreviajes.Rows[e.RowIndex].Cells["INTERNA"].Value.ToString();
                    string PROGRAMACIONEDIT = comboBox1.Text.ToString();
                    var_UbigeoPartida = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["UBIGEOPARTIDA"].Value.ToString());
                    var_UbigeoLlegada = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["UBIGEOLLEGADA"].Value.ToString());
                    var_Maquinaria = dgvPreviajes.CurrentRow.Cells["MAQUINARIA"].Value.ToString();
                    string correlativoprogramacion = dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString();
                    VAR_anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                    var_CodiTolvas = dgvPreviajes.CurrentRow.Cells["ENLACE"].Value.ToString();
                    var_FechaRegistro = dgvPreviajes.CurrentRow.Cells["FECHAREGISTRO"].Value.ToString();
                    var_usuarioCrea = dgvPreviajes.CurrentRow.Cells["UsuarioCrea"].Value.ToString();
                    var_MontoPlanila = dgvPreviajes.CurrentRow.Cells["ENTREGADO"].Value.ToString();
                    
                    crea1_modifi2_anula3 = 2;
                    string var_progOrigen = dgvPreviajes.CurrentRow.Cells["ProgOrigen"].Value.ToString();

                    frmNuevoPreviaje_Programacion frm2 = new frmNuevoPreviaje_Programacion();

                    if (var_progOrigen != "")
                    {
                        frm2.lblProgramacionOriginal.Visible = true;
                        frm2.txtProgramacionOrigen.Visible = true;
                    }
                    frm2.setearvariable(crea1_modifi2_anula3, IDPROGRAMAEDIT, PROGRAMACIONEDIT, var_IdProg, var_Sucursal, var_TipoProg, var_Desctipo, var_FechaProg, var_FechaInicio, var_FechaFin,
                                        var_Tracto, var_DesTracto, var_Remolque, var_Desremolque, var_Conductor, var_Apoyo, var_Cliente, var_nomConductor, var_nomApoyo, var_nomCliente,
                                        var_nomProducto, var_nomRuta, var_Ruta, var_Destino, var_Producto, var_Estado, var_HoraSailda, var_HoraLlegada, var_FechaDescarga, var_PesoAlmacen, var_PesoCliente,
                                        var_Merma, var_Viaje,var_idViaje, var_Planilla, var_Observacion, var_Serie, var_Numero, var_Remitente, var_Zona, var_turno, var_ConducInicio, var_PrimCambio,
                                        var_SegCambio, var_TerCambio, var_Accesos, var_OT, var_Viaticos, var_OrdenServicio, var_tipounidad, var_UnidadApoyo, var_guiaEntrega, var_MontoOrden,
                                        var_TopoSemir, var_Dia, userModifica, fechaModifica, var_usuarioCrea, var_Interna, var_UbigeoPartida, var_UbigeoLlegada, VAR_anio, var_Maquinaria, var_CodiTolvas, var_FechaRegistro
                                        , var_MontoPlanila, var_progOrigen,"");
                    frm2.Text = "Editar Previaje - Programación: " + correlativoprogramacion;
                    frm2.CargarComboTiempo();
                    frm2.idRutaDia = var_Ruta;
                    frm2.ShowDialog(this);

                    if (Val_Respuesta.Equals("1"))
                    {
                        //CargarDatos();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error al seleccionar...!");
            }
        }

        int var_IdProg;
        int var_TipoProg;
        string var_FechaProg, var_Sucursal, var_ConducInicio, var_PrimCambio, var_SegCambio, var_TerCambio;
        string var_FechaInicio;
        string var_FechaFin, idViaje;
        int var_Tracto, var_OT;
        int var_Remolque;
        int var_Conductor;
        int var_Apoyo;
        int var_Cliente;
        int var_Ruta;
        string var_Destino, var_nomConductor, var_nomApoyo, var_nomCliente, var_Zona, var_turno;
        int var_Producto, var_Accesos = 0;
        int var_Estado, var_Dia;
        string var_HoraSailda, var_nomProducto, var_nomRuta, var_tipounidad;
        string var_HoraLlegada, var_FechaRegistro, var_MontoPlanila;
        string var_FechaDescarga;
        decimal var_PesoAlmacen = 12.120000m;
        decimal var_PesoCliente = 12.120000m;
        decimal var_Merma = 12.120000m;
        decimal var_Viaticos = 12.120000m;
        decimal var_MontoOrden;
        string var_Viaje, var_idViaje,var_UnidadApoyo, var_Interna;
        string var_Planilla, userModifica, fechaModifica;
        string var_Observacion, var_OrdenServicio;
        string var_Serie, var_guiaEntrega, var_Maquinaria;
        string var_Numero, VAR_anio, var_CodiTolvas;
        string var_Remitente, var_TopoSemir;
        int var_UbigeoPartida, var_UbigeoLlegada;
        string var_usuarioCrea;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtros();
        }

        private void Filtros()
        {
            try
            {
                if (cboFiltrosVarios.Text.Equals("CLIENTE"))
                {
                    string colFiltrar = "CLIENTE";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);
                }
                if (cboFiltrosVarios.Text.Equals("CONDUCTOR"))
                {
                    string colFiltrar = "CONDUCTOR";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);
                }
                if (cboFiltrosVarios.Text.Equals("RUTA"))
                {
                    string colFiltrar = "RUTA";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);
                }
                if (cboFiltrosVarios.Text.Equals("CODIGO VIAJE"))
                {
                    string colFiltrar = "VIAJE";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", colFiltrar, textBox1.Text);
                }
                if (cboFiltrosVarios.Text.Equals("PRODUCTO"))
                {
                    string colFiltrar = "PRODUCTO";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);
                }
                if (cboFiltrosVarios.Text.Equals("GUIA TRANSPORTE"))
                {
                    string colFiltrar = "GUIAT.";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);
                }
                if (cboFiltrosVarios.Text.Equals("GUIA REMITENTE"))
                {
                    string colFiltrar = "G/R";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);
                }
                if (cboFiltrosVarios.Text.Equals("PROVEEDOR"))
                {
                    string colFiltrar = "PROVEEDOR";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);
                }
                if (cboFiltrosVarios.Text.Equals("TIPO UNIDAD"))
                {
                    string colFiltrar = "UNIDAD";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", colFiltrar, textBox1.Text);
                }
                if (cboFiltrosVarios.Text.Equals("TRACTO"))
                {
                    string colFiltrar = "TRACTO";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);
                }
                if (cboFiltrosVarios.Text.Equals("CODIGO"))
                {
                    string colFiltrar = "CODIGO";
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);
                }
            }
            catch (Exception)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void dgvPreviajes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                //FORMATO DE COLOR AL DATAGRIDVIEW
                if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "ESTADO")
                {
                    if (e.Value != null)
                    {
                        string stringValue = (string)e.Value;
                        if (stringValue.Equals("ATENDIDO"))
                        {
                            e.CellStyle.BackColor = Color.PaleGreen;
                        }
                        if (stringValue.Equals("ANULADO"))
                        {
                            e.CellStyle.BackColor = Color.LightSalmon;
                            e.CellStyle.ForeColor = Color.White;
                        }
                        if (stringValue.Equals("PROGRAMADO"))
                        {
                            e.CellStyle.BackColor = Color.Yellow;

                        }
                        if (stringValue.Equals("COLA IN"))
                        {
                            e.CellStyle.BackColor = Color.Orange;

                        }
                        if (stringValue.Equals("COLA OUT"))
                        {
                            e.CellStyle.BackColor = Color.Red;

                        }
                        if (stringValue.Equals("CARGANDO"))
                        {
                            e.CellStyle.BackColor = Color.MediumPurple;
                        }
                    }
                }

                if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "REND KM/GL")
                {
                    if (e.Value != null)
                    {
                        decimal rendProm = Convert.ToDecimal(dgvPreviajes["REND.PROM", e.RowIndex].Value);
                        decimal rendKM = Convert.ToDecimal(dgvPreviajes["REND KM/GL", e.RowIndex].Value);

                        if (Convert.ToInt32(rendProm) != 0 && Convert.ToInt32(rendKM) != 0 && rendProm > rendKM)
                        { e.CellStyle.BackColor = Color.PaleGreen; }

                        if (Convert.ToInt32(rendProm) != 0 && Convert.ToInt32(rendKM) != 0 && rendProm < rendKM)
                        {
                            e.CellStyle.BackColor = Color.LightSalmon;
                            e.CellStyle.ForeColor = Color.White;
                        }
                    }
                }

                if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "TIPOPROG")
                {
                    if (e.Value != null)
                    {
                        string stringValue = (string)e.Value;
                        if (stringValue.Equals("LINDLEY"))
                        {
                            dgvPreviajes.Columns["CONDUCTOR_INICIO"].Visible = false;
                            dgvPreviajes.Columns["1° CAMBIO"].Visible = false;
                            dgvPreviajes.Columns["2° CAMBIO"].Visible = false;
                            dgvPreviajes.Columns["3° CAMBIO"].Visible = false;
                            dgvPreviajes.Columns["PESOCLIENTE"].Visible = false;
                            dgvPreviajes.Columns["MERMA"].Visible = false;
                            button6.Visible = false;
                        }
                        if (stringValue.Equals("VOLCAN"))
                        {
                            dgvPreviajes.Columns["CONDUCTOR_INICIO"].Visible = false;
                            dgvPreviajes.Columns["1° CAMBIO"].Visible = false;
                            dgvPreviajes.Columns["2° CAMBIO"].Visible = false;
                            dgvPreviajes.Columns["3° CAMBIO"].Visible = false;
                            dgvPreviajes.Columns["PESOCLIENTE"].Visible = false;
                            dgvPreviajes.Columns["MERMA"].Visible = false;
                            button6.Visible = false;
                        }
                        if (stringValue.Equals("LIMAGAS"))
                        {
                            dgvPreviajes.Columns["TURNO"].Visible = false;
                            button6.Visible = false;
                        }
                        if (stringValue.Equals("SOLGAS") || stringValue.Equals("SOLGAS GNL"))
                        {
                            dgvPreviajes.Columns["TURNO"].Visible = false;
                            button6.Visible = false;
                        }
                        if (stringValue.Equals("GENERAL"))
                        {
                            dgvPreviajes.Columns["TURNO"].Visible = false;
                            dgvPreviajes.Columns["CONDUCTOR_INICIO"].Visible = false;
                            dgvPreviajes.Columns["1° CAMBIO"].Visible = false;
                            dgvPreviajes.Columns["2° CAMBIO"].Visible = false;
                            dgvPreviajes.Columns["3° CAMBIO"].Visible = false;
                            button6.Visible = false;
                        }
                        if (stringValue.Equals("TOLVAS"))
                        {
                            dgvPreviajes.Columns["TURNO"].Visible = false;
                            dgvPreviajes.Columns["CONDUCTOR_INICIO"].Visible = false;
                            dgvPreviajes.Columns["1° CAMBIO"].Visible = false;
                            dgvPreviajes.Columns["2° CAMBIO"].Visible = false;
                            dgvPreviajes.Columns["3° CAMBIO"].Visible = false;
                            button6.Visible = true;
                        }
                    }
                }

                /* if (!cboSucursal.Text.Equals("TODO") && !comboBox1.Text.ToString().Equals("TODO"))
                 {
                     foreach (DataRow drow in dtListaProgramaciones.Rows)
                     {
                         try
                         {
                             if (imagen.Equals("SI"))
                             {
                                 if (dtListaProgramaciones.Columns[e.ColumnIndex].ColumnName == "ABASTECIDO")
                                 {
                                    // dtListaProgramaciones.Rows[e.ColumnIndex]["ABASTECIDO"] = imageToByteArray(abasteciosi);
                                     e.Value = Image.FromFile(Properties.Resources.abasteciop);
                                 }
                                 //dtListaProgramaciones.Rows.Add("ABASTECIDO", Properties.Resources.abasteciop);
                                 e.Value = Image.FromFile("Foto.jpg");
                             }//File.ReadAllBytes("R:\\imagen\\abastecio.jpg");                                    }
                             else
                             {
                                 dtListaProgramaciones.Rows[e.ColumnIndex]["ABASTECIDO"] = imageToByteArray(abasteciono); // = File.ReadAllBytes("R:\\imagen\\estacionno.jpg");
                             }
                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show(ex.Message);
                         }
                     }

                     foreach (DataRow drow in dtListaProgramaciones.Rows)
                     {
                         try
                         {
                             if (imagencaja.Equals("PAGADO"))
                             {
                                 dtListaProgramaciones.Rows[e.ColumnIndex]["CAJA"] = imageToByteArray(gastosi);//File.ReadAllBytes("R:\\imagen\\gastosentregados.jpg");
                             }
                             else
                             {
                                 dtListaProgramaciones.Rows[e.ColumnIndex]["CAJA"] = imageToByteArray(gastono);//File.ReadAllBytes("R:\\imagen\\gastospendientes.jpg");
                             }
                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show(ex.Message);
                         }
                     }
                }   */
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (gvexcel.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Ubicacion" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                gvexcel.ExportToXlsx(nombre);
                Process.Start(nombre);
            }

            // Exportar datos a Excel;
            // Exportar2 exp = new Exportar2();
            // ExportarDataGridViewExcel(dgvPreviajes);
            /*  char letter = 'A';
              char lettersiguiente = 'A';
              int conut = 0;
           for (int D = 0 ; D<50;D++) 
            
           {                        
               if (conut > 25)
               {
                   MessageBox.Show("A" + lettersiguiente.ToString());
                   lettersiguiente++;
               }
               else 
               {
                   MessageBox.Show(letter.ToString());
                   letter++;
               }
               conut=conut+1;
              
           }*/

            /*  dtColumnasSeleccionadasExcel = clsOperacionesBL.Instancia.GetOperaciones_Previaje_ListarColumnasSeleccionadasxUsuario(Utilitario.Instancia.SesionUsuario.usuario);
              DataTable dtexcel = new DataTable();
              for (int i = 0; i < dtColumnasSeleccionadasExcel.Rows.Count; i++)

              {

                  NOMBRECOLUMNAexcel = dtColumnasSeleccionadas.Rows[i]["DESCRIPCION"].ToString();
                  if (dgvPreviajes.Columns[NOMBRECOLUMNAexcel].Visible == true)
                  {
                   
                      dtexcel.Columns.Add(NOMBRECOLUMNAexcel, typeof(System.String));

                      foreach (DataGridViewRow rowGrid in dgvPreviajes.Rows)
                      {
                          DataRow row = dtexcel.NewRow();
                          dtexcel.Rows.Add(row);
                         // row[NOMBRECOLUMNAexcel] = rowGrid.Cells[i].Value;
                          dtexcel.Rows[i][NOMBRECOLUMNAexcel] = rowGrid.Cells[i].Value; ;                        
                      }                   
                  }
                 //  NOMBRECOLUMNAexcel = dtColumnasSeleccionadas.Rows[i]["DESCRIPCION"].ToString();
                 // conteoColumnas = dtP.Rows.Count;                          
              }  */

            // gcExcel.DataSource = dtexcel;
        }

        public void ExportarDataGridViewExcel(DataGridView grd)
        {
            try
            {
                SaveFileDialog archivo = new SaveFileDialog();
                archivo.Filter = "Excel (*.xls)|*.xls";
                archivo.FileName = "Reporte de programacion " + DateTime.Now.Date.ToShortDateString().Replace('/', '-');
                if (archivo.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application aplicacion;
                    Microsoft.Office.Interop.Excel.Workbook libroDeTrabajo;
                    Microsoft.Office.Interop.Excel.Worksheet hojaDeTrabajo;
                    aplicacion = new Microsoft.Office.Interop.Excel.Application();
                    libroDeTrabajo = aplicacion.Workbooks.Add();
                    hojaDeTrabajo = (Microsoft.Office.Interop.Excel.Worksheet)libroDeTrabajo.Worksheets.get_Item(1);

                    hojaDeTrabajo.Cells[1, "A"] = grd.Columns["ITEM"].HeaderText;
                    hojaDeTrabajo.Cells[1, "B"] = grd.Columns["CODIGO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "C"] = grd.Columns["ZONA"].HeaderText;
                    hojaDeTrabajo.Cells[1, "D"] = grd.Columns["FECHAREGISTRO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "E"] = grd.Columns["FECHAPROGR"].HeaderText;
                    hojaDeTrabajo.Cells[1, "F"] = grd.Columns["FECHA_INICIO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "G"] = grd.Columns["FECHA_FIN"].HeaderText;
                    hojaDeTrabajo.Cells[1, "H"] = grd.Columns["FECHA_TERMINO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "I"] = grd.Columns["TRACTO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "J"] = grd.Columns["UNIDAD"].HeaderText;
                    hojaDeTrabajo.Cells[1, "K"] = grd.Columns["PROVEEDOR"].HeaderText;
                    hojaDeTrabajo.Cells[1, "L"] = grd.Columns["SEMIRREMOLQUE"].HeaderText;
                    hojaDeTrabajo.Cells[1, "M"] = grd.Columns["TIPOSEMIR"].HeaderText;
                    hojaDeTrabajo.Cells[1, "N"] = grd.Columns["CONDUCTOR"].HeaderText;
                    hojaDeTrabajo.Cells[1, "O"] = grd.Columns["UNIDADAPOYO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "P"] = grd.Columns["CONDUCTOR_APOYO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "Q"] = grd.Columns["RUTA"].HeaderText;
                    hojaDeTrabajo.Cells[1, "R"] = grd.Columns["DESTINO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "S"] = grd.Columns["PRODUCTO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "T"] = grd.Columns["ESTADO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "U"] = grd.Columns["CLIENTE"].HeaderText;
                    hojaDeTrabajo.Cells[1, "V"] = grd.Columns["DESCARGA"].HeaderText;
                    hojaDeTrabajo.Cells[1, "W"] = grd.Columns["VIAJE"].HeaderText;
                    hojaDeTrabajo.Cells[1, "X"] = grd.Columns["ESTADOVIAJE"].HeaderText;
                    hojaDeTrabajo.Cells[1, "Y"] = grd.Columns["PESO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "Z"] = grd.Columns["PESOCLIENTE"].HeaderText;
                    hojaDeTrabajo.Cells[1, "AA"] = grd.Columns["MERMA"].HeaderText;
                    hojaDeTrabajo.Cells[1, "AB"] = grd.Columns["PLANILLA"].HeaderText;
                    hojaDeTrabajo.Cells[1, "AC"] = grd.Columns["TURNO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "AD"] = grd.Columns["OT"].HeaderText;
                    hojaDeTrabajo.Cells[1, "AE"] = grd.Columns["ORDENSERVICIO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "AF"] = grd.Columns["SUCURSAL"].HeaderText;
                    hojaDeTrabajo.Cells[1, "AG"] = grd.Columns["TIPO"].HeaderText;
                    hojaDeTrabajo.Cells[1, "AH"] = grd.Columns["GUIAT."].HeaderText;
                    hojaDeTrabajo.Cells[1, "AI"] = grd.Columns["G/R"].HeaderText;
                    hojaDeTrabajo.Cells[1, "AJ"] = grd.Columns["OBSERVACION"].HeaderText;
                    hojaDeTrabajo.Cells[1, "AK"] = grd.Columns["COMENTARIO"].HeaderText;

                    hojaDeTrabajo.Columns[1].AutoFit();
                    hojaDeTrabajo.Columns[2].AutoFit();
                    hojaDeTrabajo.Columns[3].AutoFit();
                    hojaDeTrabajo.Columns[4].AutoFit();
                    hojaDeTrabajo.Columns[5].AutoFit();
                    hojaDeTrabajo.Columns[6].AutoFit();
                    hojaDeTrabajo.Columns[7].AutoFit();
                    hojaDeTrabajo.Columns[8].AutoFit();
                    hojaDeTrabajo.Columns[9].AutoFit();
                    hojaDeTrabajo.Columns[10].AutoFit();
                    hojaDeTrabajo.Columns[11].AutoFit();
                    hojaDeTrabajo.Columns[12].AutoFit();
                    hojaDeTrabajo.Columns[13].AutoFit();
                    hojaDeTrabajo.Columns[14].AutoFit();
                    hojaDeTrabajo.Columns[15].AutoFit();
                    hojaDeTrabajo.Columns[16].AutoFit();
                    hojaDeTrabajo.Columns[17].AutoFit();
                    hojaDeTrabajo.Columns[18].AutoFit();
                    hojaDeTrabajo.Columns[19].AutoFit();
                    hojaDeTrabajo.Columns[20].AutoFit();
                    hojaDeTrabajo.Columns[21].AutoFit();
                    hojaDeTrabajo.Columns[22].AutoFit();
                    hojaDeTrabajo.Columns[23].AutoFit();
                    hojaDeTrabajo.Columns[24].AutoFit();
                    hojaDeTrabajo.Columns[25].AutoFit();
                    hojaDeTrabajo.Columns[26].AutoFit();
                    hojaDeTrabajo.Columns[27].AutoFit();
                    hojaDeTrabajo.Columns[28].AutoFit();
                    hojaDeTrabajo.Columns[29].AutoFit();
                    hojaDeTrabajo.Columns[30].AutoFit();
                    hojaDeTrabajo.Columns[31].AutoFit();
                    hojaDeTrabajo.Columns[32].AutoFit();
                    hojaDeTrabajo.Columns[33].AutoFit();
                    hojaDeTrabajo.Columns[34].AutoFit();
                    hojaDeTrabajo.Columns[35].AutoFit();
                    hojaDeTrabajo.Columns[36].AutoFit();
                    hojaDeTrabajo.Columns[37].AutoFit();

                    hojaDeTrabajo.Name = "Programaciones";
                    //Recorremos el DataGridView rellenando la hoja de trabajo
                    for (int i = 0; i < grd.Rows.Count; i++)
                    {
                        int p = 0;
                        for (int j = 0; j < grd.Columns.Count; j++)
                        {
                            if (p == 0)
                            {
                                p = j;
                            }

                            if (grd.Columns[j].Visible == true)
                            {
                                string nombrecolumna = grd.Columns[j].HeaderText;
                                if (j >= 0)
                                {
                                    //* if (nombrecolumna != "ITEM")
                                    // {

                                    if (grd.Rows[i].Cells[j].Value != null)
                                    {
                                        hojaDeTrabajo.Cells[i + 2, p + 1] = grd.Rows[i].Cells[j].Value.ToString();
                                    }

                                }
                                p = p + 1;
                            }

                        }
                    }
                    libroDeTrabajo.SaveAs(archivo.FileName,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    libroDeTrabajo.Close(true);
                    aplicacion.Quit();
                    MessageBox.Show("Reporte de Programación Exportado a Excel", "TRANSPESA");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar la información debido a: " + ex.ToString());
            }
        }

        string inputviaje;
        string programacionmostrar;

        private void dgvPreviajes_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (!dgvPreviajes.Rows[e.RowIndex].Cells["ESTADO"].Value.ToString().Equals("ANULADO"))
                    {
                        //this.barItem1.Image = new ImageExt(System.Drawing.Image.FromFile(@"..\..\..\File.png"));


                        dgvPreviajes.CurrentCell = dgvPreviajes.Rows[e.RowIndex].Cells[e.ColumnIndex];

                        ContextMenuStrip menu = new ContextMenuStrip();

                        menu.Items.Add("Seleccionar Opción:").Enabled = false;
                        //OPCION PARA DUPLICAR FILAS EN EL LISTADO DE PREVIAJES
                        /*menu.Items.Add("Duplicar Fila", Resources.duplica, (snd, evt) =>
                        {
                            if (dgvPreviajes.Rows[e.RowIndex].Cells["IDESTADO"].Value.ToString().Equals("9"))
                            {
                                MessageBox.Show("No puedes Duplicar esta programacion porque esta ATENDIDA.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }

                            if (dgvPreviajes.Rows[e.RowIndex].Cells["IDESTADO"].Value.ToString().Equals("10"))
                            {
                                MessageBox.Show("No puedes Duplicar esta programación porque esta  ANULADA.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (var_Accesos == 1)
                            {
                                MessageBox.Show("Usted no tiene permiso para duplicar filas.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                var_IdProg = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDPROGRAMACION"].Value.ToString());
                                var_TipoProg = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value.ToString());
                                var_Sucursal = dgvPreviajes.Rows[e.RowIndex].Cells["SUCURSAL"].Value.ToString();
                                var_Desctipo = dgvPreviajes.Rows[e.RowIndex].Cells["TIPO"].Value.ToString();
                                var_FechaProg = dgvPreviajes.Rows[e.RowIndex].Cells["FECHAPROGR"].Value.ToString();
                                var_FechaInicio = dgvPreviajes.Rows[e.RowIndex].Cells["FECHA_INICIO"].Value.ToString();
                                var_FechaFin = dgvPreviajes.Rows[e.RowIndex].Cells["FECHA_FIN"].Value.ToString();
                                var_Tracto = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDTRACTO"].Value.ToString());
                                var_DesTracto = dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString();
                                var_Remolque = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDREMOLQUE"].Value.ToString());
                                var_Desremolque = dgvPreviajes.Rows[e.RowIndex].Cells["SEMIRREMOLQUE"].Value.ToString();
                                var_Conductor = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDCONDUCTOR"].Value.ToString());
                                var_nomConductor = dgvPreviajes.Rows[e.RowIndex].Cells["CONDUCTOR"].Value.ToString();
                                var_Apoyo = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDAPOYO"].Value.ToString());
                                var_nomApoyo = dgvPreviajes.Rows[e.RowIndex].Cells["CONDUCTOR_APOYO"].Value.ToString();
                                var_Cliente = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDCLIENTE"].Value.ToString());
                                var_nomCliente = dgvPreviajes.Rows[e.RowIndex].Cells["CLIENTE"].Value.ToString();
                                var_nomRuta = dgvPreviajes.Rows[e.RowIndex].Cells["RUTA"].Value.ToString();
                                var_nomProducto = dgvPreviajes.Rows[e.RowIndex].Cells["PRODUCTO"].Value.ToString();
                                var_Ruta = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDRUTA"].Value.ToString());
                                var_Destino = dgvPreviajes.Rows[e.RowIndex].Cells["DESTINO"].Value.ToString();
                                var_Producto = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDPRODUCTO"].Value.ToString());
                                var_Estado = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDESTADO"].Value.ToString());
                                var_HoraSailda = dgvPreviajes.Rows[e.RowIndex].Cells["SALIDA"].Value.ToString();
                                var_HoraLlegada = dgvPreviajes.Rows[e.RowIndex].Cells["LLEGADA"].Value.ToString();
                                var_FechaDescarga = dgvPreviajes.Rows[e.RowIndex].Cells["DESCARGA"].Value.ToString();
                                var_PesoAlmacen = Convert.ToDecimal(dgvPreviajes.Rows[e.RowIndex].Cells["PESO"].Value.ToString());
                                var_PesoCliente = Convert.ToDecimal(dgvPreviajes.Rows[e.RowIndex].Cells["PESOCLIENTE"].Value.ToString());
                                var_Merma = Convert.ToDecimal(dgvPreviajes.Rows[e.RowIndex].Cells["MERMA"].Value.ToString());
                                var_Viaje = dgvPreviajes.Rows[e.RowIndex].Cells["VIAJE"].Value.ToString();
                                var_Planilla = dgvPreviajes.Rows[e.RowIndex].Cells["PLANILLA"].Value.ToString();
                                var_Observacion = dgvPreviajes.Rows[e.RowIndex].Cells["OBSERVACION"].Value.ToString();
                                var_Serie = dgvPreviajes.Rows[e.RowIndex].Cells["SERIE"].Value.ToString();
                                var_Numero = dgvPreviajes.Rows[e.RowIndex].Cells["GUIA"].Value.ToString();
                                var_Remitente = dgvPreviajes.Rows[e.RowIndex].Cells["G/R"].Value.ToString();
                                var_Zona = dgvPreviajes.Rows[e.RowIndex].Cells["ZONA"].Value.ToString();
                                var_turno = dgvPreviajes.Rows[e.RowIndex].Cells["TURNO"].Value.ToString();
                                var_ConducInicio = dgvPreviajes.Rows[e.RowIndex].Cells["CONDUCTOR_INICIO"].Value.ToString();
                                var_PrimCambio = dgvPreviajes.Rows[e.RowIndex].Cells["1° CAMBIO"].Value.ToString();
                                var_SegCambio = dgvPreviajes.Rows[e.RowIndex].Cells["2° CAMBIO"].Value.ToString();
                                var_TerCambio = dgvPreviajes.Rows[e.RowIndex].Cells["3° CAMBIO"].Value.ToString();
                                var_OT = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["OT"].Value.ToString());
                                var_Viaticos = Convert.ToDecimal(dgvPreviajes.Rows[e.RowIndex].Cells["VIATICOS"].Value.ToString());
                                var_OrdenServicio = dgvPreviajes.Rows[e.RowIndex].Cells["ORDENSERVICIO"].Value.ToString();
                                var_tipounidad = dgvPreviajes.Rows[e.RowIndex].Cells["TIPOUNIDAD"].Value.ToString();
                                var_guiaEntrega = dgvPreviajes.Rows[e.RowIndex].Cells["GUIAENTREGA"].Value.ToString();
                                var_UnidadApoyo = dgvPreviajes.Rows[e.RowIndex].Cells["UNIDADAPOYO"].Value.ToString();
                                var_MontoOrden = Convert.ToDecimal(dgvPreviajes.Rows[e.RowIndex].Cells["MONTORDEN"].Value.ToString());
                                var_TopoSemir = dgvPreviajes.Rows[e.RowIndex].Cells["TIPOSEMIR"].Value.ToString();
                                var_Dia = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["CANTIDADDIA"].Value.ToString());
                                int IDPROGRAMAEDIT = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                                userModifica = dgvPreviajes.Rows[e.RowIndex].Cells["USUARIOMOD"].Value.ToString();
                                fechaModifica = dgvPreviajes.Rows[e.RowIndex].Cells["FECHAMOD"].Value.ToString();
                                var_Interna = dgvPreviajes.Rows[e.RowIndex].Cells["INTERNA"].Value.ToString();
                                var_UbigeoPartida = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["UBIGEOPARTIDA"].Value.ToString());
                                var_UbigeoLlegada = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["UBIGEOLLEGADA"].Value.ToString());
                                VAR_anio = dgvPreviajes.CurrentRow.Cells["UBIGEOLLEGADA"].Value.ToString();
                                var_Maquinaria = dgvPreviajes.Rows[e.RowIndex].Cells["MAQUINARIA"].Value.ToString();
                                string PROGRAMACIONEDIT = comboBox1.Text.ToString();
                                var_CodiTolvas = dgvPreviajes.CurrentRow.Cells["ENLACE"].Value.ToString();
                                DataTable dtRespuesta = new DataTable();
                                string Respuesta;
                                dtRespuesta = clsOperacionesBL.Instancia.GetOperaciones_Registro_Previajes(4, var_IdProg, var_Sucursal.ToUpper(), var_TipoProg, var_FechaProg, var_FechaInicio, var_FechaFin,
                                                                                                            var_Tracto, var_Remolque, var_Conductor, var_Apoyo, var_Cliente, var_Ruta, var_Destino,
                                                                                                            var_Producto, var_Estado, var_HoraSailda, var_HoraLlegada, var_FechaDescarga, var_PesoAlmacen,
                                                                                                            var_PesoCliente, var_Merma, var_Viaje, var_Planilla, var_Observacion.ToUpper(),
                                                                                                            Utilitario.Instancia.SesionUsuario.usuario, var_Serie, var_Numero, var_Remitente,
                                                                                                           var_Zona.ToUpper(), var_turno, var_ConducInicio, var_PrimCambio,
                                                                                                            var_SegCambio, var_TerCambio, var_OT, var_Viaticos, var_OrdenServicio, var_UnidadApoyo,
                                                                                                            var_guiaEntrega, var_MontoOrden, var_Dia, Convert.ToInt32(var_Interna),
                                                                                                            var_UbigeoPartida, var_UbigeoLlegada, VAR_anio, var_Maquinaria, var_CodiTolvas);
                                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                string NroRPTA = Respuesta.Substring(0, 1);

                                if (NroRPTA == "0")
                                {
                                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    CargarDatos();
                                }
                                else
                                {
                                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }); */


                        #region Guia Electronica Generar

                        // por aquí pasó sem

                        menu.Items.Add("Guia Electronica Transportista", Resources.doc_guia);
                        DataTable dt = Utilitario.Instancia.ObtenerPermisosPorFormulario("Operaciones_Programacion_Previajes");
                        DataTable dtEspeciales = null;
                        if (dt != null)
                        {
                            if (dt.Rows[0]["PermisosEspeciales"].ToString() != "")
                            {
                                dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dt.Rows[0]["PermisosEspeciales"].ToString());
                            }
                            
                        }



                        (menu.Items[1] as ToolStripMenuItem).DropDownItems.Add("Generar Guia", Resources.nuevo_button, (snd, evt) =>
                        {
                            if (dgvPreviajes.Rows[e.RowIndex].Cells["GUIAT."].Value.ToString() != "-" && dgvPreviajes.Rows[e.RowIndex].Cells["GUIAT."].Value.ToString() != "" && dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value.ToString() == "3" && Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Cliente"].Value) != "LIMAGAS NATURAL PERU SA" )
                            {
                                MessageBox.Show("La guia ya fue generada, no es posible generar, favor adicionarla como consolidado", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            if (dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value.ToString() != "2" && dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value.ToString() != "3" && dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value.ToString() != "4"
                                && dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value.ToString() != "9" && dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value.ToString() != "10" && dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value.ToString() != "11") //Local
                            {
                                MessageBox.Show("Tipo de Operacion " + comboBox1.Text.ToString() + " no está habilitada para generar guias electronicas", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                              
                                OperacionPreViajes.FrmGuiaElectronicaTransportista openGenerarGuia = new OperacionPreViajes.FrmGuiaElectronicaTransportista();
                                openGenerarGuia.TipoOperacion = Utilitario.TipoOperacion.Registrar;
                                openGenerarGuia.entGuiaTransportista.TipoOperacion = Utilitario.TipoOperacion.Registrar;
                                openGenerarGuia.entGuiaTransportista.item = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["Item"].Value);
                                openGenerarGuia.entGuiaTransportista.idproveedor = Convert.ToInt32((Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Proveedor"].Value).Equals("GRUPO TRANSPESA S.A.C")) ? "1553" : "-777");

                                    //Datos del cliente (Sistema)
                                openGenerarGuia.txtCliente.Tag = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["idCliente"].Value);
                                openGenerarGuia.txtCliente.Text = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Cliente"].Value);
                                openGenerarGuia.entGuiaTransportista.idcliente = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["idCliente"].Value);
                                openGenerarGuia.entGuiaTransportista.Cliente = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Cliente"].Value);
                                openGenerarGuia.entGuiaTransportista.AnioProgramacion = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["ANIO"].Value);
                                openGenerarGuia.entGuiaTransportista.idviaje = dgvPreviajes.Rows[e.RowIndex].Cells["IdViaje"].Value.ToString() == "" ? 0 : Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IdViaje"].Value);
                                openGenerarGuia.entGuiaTransportista.viaje = dgvPreviajes.Rows[e.RowIndex].Cells["Viaje"].Value.ToString() == "" ? "" : dgvPreviajes.Rows[e.RowIndex].Cells["Viaje"].Value.ToString();
                                openGenerarGuia.entGuiaTransportista.estadoviaje = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["IDESTADO"].Value);
                                openGenerarGuia.entGuiaTransportista.idRuta = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["idRuta"].Value);
                                openGenerarGuia.entGuiaTransportista.ruta = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Ruta"].Value);
                                openGenerarGuia.entGuiaTransportista.conductor = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Conductor"].Value); // nombre y apellidos completos concatenados del conductor
                                openGenerarGuia.entGuiaTransportista.idconductor = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["idConductor"].Value);
                                openGenerarGuia.entGuiaTransportista.tracto = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Tracto"].Value).Replace("-", "").Replace(".", "").TrimEnd(); // placa
                                openGenerarGuia.entGuiaTransportista.idtracto = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["idTracto"].Value); // id placa
                                openGenerarGuia.entGuiaTransportista.idCarreta = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["idRemolque"].Value);
                                openGenerarGuia.entGuiaTransportista.carreta = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["SemiRremolque"].Value).Replace("-", "").Replace(".", "").TrimEnd();
                                openGenerarGuia.entGuiaTransportista.destino = (Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Destino"].Value)) == string.Empty ? "" : Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Destino"].Value);
                                openGenerarGuia.entGuiaTransportista.FechaProgramacion = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["FECHAPROGR"].Value);
                                openGenerarGuia.TipoProgramacion = comboBox1.Text.ToString(); //<<<<-------- ENVIO EL TIPO DE PROGRAMACION
                                openGenerarGuia.entGuiaTransportista.CodigoProgramacion = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Codigo"].Value);
                                openGenerarGuia.entGuiaTransportista.idProgramacion = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDPROGRAMACION"].Value);
                                openGenerarGuia.entGuiaTransportista.TipoGuia = "T"; //Transportista
                                openGenerarGuia.entGuiaTransportista.idTipoProgramacion = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value);
                                openGenerarGuia.entGuiaTransportista.idEstadoProgramacion = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDESTADO"].Value);
                                openGenerarGuia.PesoTotal = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Peso"].Value);
                            
                                if (Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["LineaOT"].Value) > 0)
                                { openGenerarGuia.entGuiaTransportista.LineaOT = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["LineaOT"].Value); }
                                
                                if (dgvPreviajes.Rows[e.RowIndex].Cells["Viaje"].Value.ToString().Contains("(C)"))
                                {
                                     openGenerarGuia.idotSeleccionado = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["OT"].Value);
                                     openGenerarGuia.esConsolidado = true;
                                     openGenerarGuia.entGuiaTransportista.lineaConsolidado = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["OT"].Value); ;
                                }
                                else { openGenerarGuia.esConsolidado = false; }

                                if (Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Cliente"].Value) == "LIMA GAS S A")
                                {
                                    openGenerarGuia.entGuiaTransportista.entGRT_Remitente_RazonSocial_M = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Cliente"].Value);
                                    openGenerarGuia.entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = "LIMA GAS S A";
                                    openGenerarGuia.entGuiaTransportista.idDestinatario = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["idCliente"].Value);
                                    openGenerarGuia.entGuiaTransportista.idRemitente = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["idCliente"].Value);
                                }

                                if (openGenerarGuia.TipoProgramacion == "LINDLEY" || openGenerarGuia.TipoProgramacion == "VOLCAN COMPANIA MINERA S.A.A.")
                                { openGenerarGuia.PesoTotal = "1.00"; }

                                if (openGenerarGuia.TipoProgramacion == "LOCAL")
                                {
                                    openGenerarGuia.enProductoTolvas.entGTR_ProductoBienes_Descripcion = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["PRODUCTO"].Value);
                                    openGenerarGuia.enProductoTolvas.entGTR_ProductoBienes_Codigo = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["IDPRODUCTO"].Value);
                                    openGenerarGuia.PesoTotal = "1.00";
                                }

                                DataTable dtDatos =  cargarDatosPorDefectoSegunCliente();
                                if (dtDatos.Rows.Count > 0)
                                {
                                    openGenerarGuia.entGuiaTransportista.entGRT_Remitente_RazonSocial_M = dtDatos.Rows[0]["Remitente"].ToString();
                                    openGenerarGuia.entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = dtDatos.Rows[0]["Destinatario"].ToString();
                                    openGenerarGuia.entGuiaTransportista.entGRT_PuntoPartida_Direccion_Secuencia = Convert.ToInt32(dtDatos.Rows[0]["SecuenciaPartida"]);
                                    openGenerarGuia.entGuiaTransportista.entGRT_PuntoDestino_Direccion_Secuencia = Convert.ToInt32(dtDatos.Rows[0]["SecuenciaDestino"]);
                                    openGenerarGuia.entGuiaTransportista.entGRT_PuntoPartida_DireccionCompleta_M = dtDatos.Rows[0]["DireccionPartida"].ToString();
                                    openGenerarGuia.entGuiaTransportista.entGRT_PuntoDestino_DireccionCompleta_M = dtDatos.Rows[0]["DireccionDestino"].ToString();
                                    openGenerarGuia.txtDireccionPartida.Text = dtDatos.Rows[0]["DireccionPartida"].ToString();
                                    openGenerarGuia.txtDireccionDestino.Text = dtDatos.Rows[0]["DireccionDestino"].ToString();
                                    //openGenerarGuia.entGuiaTransportista.idRuta = Convert.ToInt16(dgvPreviajes.Rows[e.RowIndex].Cells["idRuta"].Value);
                                }

                                openGenerarGuia.CargarLista += new FrmGuiaElectronicaTransportista.CargarListaEventHandler(cargarTransportista);
                                openGenerarGuia.Show();
                        });

                        // habilitar evento de abrir guia segun la accion 
                          (menu.Items[1] as ToolStripMenuItem).DropDownItems.Add("Ver Guias", Resources.history, (snd, evt) =>
                          {
                              OperacionPreViajes.FrmListaGuiasElectronicas openGenerarGuia = new OperacionPreViajes.FrmListaGuiasElectronicas();
                              openGenerarGuia.TipoOperacion = Utilitario.TipoOperacion.Lectura;
                              openGenerarGuia.dtpFechaInicio.Value = Convert.ToDateTime(dgvPreviajes.CurrentRow.Cells["FECHAPROGR"].Value);
                              openGenerarGuia.dtpFechaFin.Value = DateTime.Now;
                              string serie = dgvPreviajes.CurrentRow.Cells["GUIAT."].Value.ToString().TrimEnd() == "-" ? "" : dgvPreviajes.CurrentRow.Cells["GUIAT."].Value.ToString().Substring(0,4);
                              openGenerarGuia.nroSerie = serie;
                              openGenerarGuia.txtviaje.Text = dgvPreviajes.Rows[e.RowIndex].Cells["Viaje"].Value.ToString() == "" ? "" : dgvPreviajes.Rows[e.RowIndex].Cells["Viaje"].Value.ToString();
                              openGenerarGuia.Show();
                          });

                          (menu.Items[1] as ToolStripMenuItem).DropDownItems.Add("Configurar Autompletado", Resources.editar, (snd, evt) =>
                          {
                              OperacionPreViajes.FrmMaestroRutaClienteTransportista openGenerarGuia = new OperacionPreViajes.FrmMaestroRutaClienteTransportista();
                              openGenerarGuia.remitente = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Cliente"].Value);
                              openGenerarGuia.idCliente = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IdCliente"].Value);
                              openGenerarGuia.txtCliente.Text = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Cliente"].Value);
                              openGenerarGuia.txtCliente.Tag = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["IdCliente"].Value);
                              openGenerarGuia.ruta = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Ruta"].Value);
                              openGenerarGuia.idRuta = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IdRuta"].Value);
                              openGenerarGuia.txtRuta.Text = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["Ruta"].Value);
                              openGenerarGuia.txtRuta.Tag = Convert.ToString(dgvPreviajes.Rows[e.RowIndex].Cells["IdRuta"].Value);
                              openGenerarGuia.Show();
                          });


                        if (dtEspeciales != null)
                        {
                            // desactivar opciones de Guia Electronica segun permisos

                            if (dtEspeciales.Rows[0]["NombrePermiso"].ToString() != "Guia Transportista")
                            {
                                (menu.Items[1] as ToolStripMenuItem).DropDownItems[0].Enabled = false;
                            }



                        }
                        else { (menu.Items[1] as ToolStripMenuItem).DropDownItems[0].Enabled = false; }



                        #endregion


                        //OPCION PARA LIBERAR UN VIAJE DE LA PROGRAMACION Y ANULAR EL VIAJE EN EL SPRING
                        menu.Items.Add("Anular Viaje", Resources.cerrar, (snd, evt) =>
                        {
                            for (int i = 0; i < dtTipoProg.Rows.Count; i++)
                            {
                                if (comboBox1.Text.ToString() == dtTipoProg.Rows[i]["Descripcion"].ToString())
                                    var_Accesos = Convert.ToInt32(dtTipoProg.Rows[i]["ACCESOS"].ToString());
                                inputviaje = dgvPreviajes.Rows[e.RowIndex].Cells["VIAJE"].Value.ToString();
                                programacionmostrar = dgvPreviajes.Rows[e.RowIndex].Cells["TIPO"].Value.ToString();
                            }

                            if (dgvPreviajes.Rows[e.RowIndex].Cells["ESTADO"].Value.ToString().Equals("ANULADO"))
                            {
                                MessageBox.Show("La programacion esta Anulada.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else if (dgvPreviajes.Rows[e.RowIndex].Cells["VIAJE"].Value.ToString().Equals(""))
                            {
                                MessageBox.Show("La programación no contiene Viaje.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            if (var_Accesos == 1)
                            {
                                MessageBox.Show("Usted no tiene permiso para Anular Viajes.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                            else
                            {
                                if (MessageBox.Show("Desea ANULAR el viaje: " + inputviaje + " de la Programación: " + programacionmostrar + "...?", "ANULAR VIAJE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    idViaje = dgvPreviajes.Rows[e.RowIndex].Cells["IDVIAJE"].Value.ToString();
                                    var_IdProg = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDPROGRAMACION"].Value.ToString());
                                    var_TipoProg = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDTIPO"].Value.ToString());
                                    string anio = dgvPreviajes.Rows[e.RowIndex].Cells["ANIO"].Value.ToString();
                                    string input = "";
                                    string rpta;
                                    if (ShowInputDialogBox(ref input, "Agregar un comentario para anular el Viaje: " + inputviaje, "ANULAR VIAJE", 300, 200) == DialogResult.OK)
                                    {
                                        DataTable dtAnula = new DataTable();

                                        dtAnula = clsOperacionesBL.Instancia.GetOperaciones_Previajes_AnularViajes(idViaje, var_IdProg, input, Utilitario.Instancia.SesionUsuario.usuario, anio, var_TipoProg);
                                        rpta = Convert.ToString(dtAnula.Rows[0]["exito"]);
                                        string NroRPTA = rpta.Substring(0, 1);

                                        if (NroRPTA == "0")
                                        {
                                            MessageBox.Show(rpta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //CargarDatos();
                                        }
                                        else { MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                    }
                                }
                            }
                        });

                        //OPCION PARA IMPRIMIR UN TICKET DEL PREVIAJE
                        menu.Items.Add("Impri. Ticket Combustible.", Resources.impresora, (snd, evt) =>
                        {
                            string codigo = dgvPreviajes.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString();
                            string sucursal = dgvPreviajes.Rows[e.RowIndex].Cells["SUCURSAL"].Value.ToString();
                            string tipoprogramacion = dgvPreviajes.Rows[e.RowIndex].Cells["TIPO"].Value.ToString();
                            string fechaprogramacion = dgvPreviajes.Rows[e.RowIndex].Cells["FECHAPROGR"].Value.ToString();
                            string placa = dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString();
                            string conductor = dgvPreviajes.Rows[e.RowIndex].Cells["CONDUCTOR"].Value.ToString();
                            string cliente = dgvPreviajes.Rows[e.RowIndex].Cells["CLIENTE"].Value.ToString();
                            string ruta = dgvPreviajes.Rows[e.RowIndex].Cells["RUTA"].Value.ToString();
                            string producto = dgvPreviajes.Rows[e.RowIndex].Cells["PRODUCTO"].Value.ToString();
                            string programador = dgvPreviajes.Rows[e.RowIndex].Cells["USUARIOMOD"].Value.ToString();
                            string dni = dgvPreviajes.Rows[e.RowIndex].Cells["DNI"].Value.ToString();
                            string observacion = dgvPreviajes.Rows[e.RowIndex].Cells["OBSERVACION"].Value.ToString();
                            string EsConsolidado = "1";

                            ProgramacionViajes.frmTicket frmTick = new ProgramacionViajes.frmTicket();
                            frmTick.Enviardatosticket(codigo, sucursal, tipoprogramacion, fechaprogramacion, placa, ruta, cliente, conductor, producto, programador, dni, EsConsolidado, observacion);
                            frmTick.ShowDialog(this);

                        });

                        if (comboBox1.Text.Equals("TOLVAS"))
                        {
                            //OPCION PARA VER VIAJES ENLAZADOS AL PREVIAJE SOLO PARA PROGRAMACIO "TOLVAS"
                            menu.Items.Add("Ver Viajes", Resources.binocular, (snd, evt) =>
                            {
                                int IDTRACTO = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDTRACTO"].Value.ToString());
                                int IDCONDUCTOR = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDCONDUCTOR"].Value.ToString());
                                string CODIGO = dgvPreviajes.Rows[e.RowIndex].Cells["TIPO"].Value.ToString();
                                string CODPROG = dgvPreviajes.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString(); ;
                                string PLACA = dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString();
                                string CONDUCTOR = dgvPreviajes.Rows[e.RowIndex].Cells["CONDUCTOR"].Value.ToString();
                                string CODIGOENLACE = dgvPreviajes.Rows[e.RowIndex].Cells["ENLACE"].Value.ToString();
                                DataTable dtViajesAltra = new DataTable();
                                dtViajesAltra = clsOperacionesBL.Instancia.GetOperaciones_ListarViajesConTolvaz(IDTRACTO, IDCONDUCTOR, CODIGOENLACE);

                                if (dtViajesAltra.Rows.Count > 0)
                                {
                                    groupBox3.Visible = true;
                                    groupBox3.Size = new Size(1000, 450);
                                    dgvViajesAltra.Size = new Size(985, 425);
                                    groupBox3.Text = "VIAJES DE LA PROGRAMACIÓN: " + CODPROG + "- PLACA: " + PLACA + "- CONDUCTOR: " + CONDUCTOR;
                                    dgvViajesAltra.Visible = true;
                                    button4.Visible = true;
                                    dgvViajesAltra.DataSource = dtViajesAltra;
                                }
                            });
                        }

                        //OPCION PARA VER VIAJES ENLAZADOS AL PREVIAJE SOLO PARA PROGRAMACIO "TOLVAS"
                        menu.Items.Add("Liberar Ticket", Resources.liberar, (snd, evt) =>
                            {
                                int var_IdProg = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDPROGRAMACION"].Value.ToString());
                                string anio = dgvPreviajes.Rows[e.RowIndex].Cells["ANIO"].Value.ToString();

                                DataTable dtPeso = new DataTable();
                                dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(11, var_IdProg, 0, Utilitario.Instancia.SesionUsuario.usuario, anio);
                                string rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                                string NroRPTA = rspta.Substring(0, 1);

                                if (NroRPTA == "0")
                                {
                                    MessageBox.Show(rspta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    //CargarDatos();
                                }
                                else
                                {
                                    MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            });

                        menu.Items.Add("Ver abastesimiento", default(Image), (snd, evt) =>
                        {


                            string abastesimiento = dgvPreviajes.Rows[e.RowIndex].Cells["ABASTECIO"].Value.ToString();
                            string codigo = dgvPreviajes.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString();
                            string sucursal = dgvPreviajes.Rows[e.RowIndex].Cells["SUCURSAL"].Value.ToString();
                            string PLACA = dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString();
                            string conductor = dgvPreviajes.Rows[e.RowIndex].Cells["CONDUCTOR"].Value.ToString();

                            if (abastesimiento.Equals("SI"))
                            {
                                ProgramacionViajes.frmVerTicketDeAbastesimiento frmTicket = new ProgramacionViajes.frmVerTicketDeAbastesimiento();
                                frmTicket.Enviardatostickets(codigo, sucursal, PLACA, conductor);
                                frmTicket.ShowDialog(this);
                            }
                            else
                            {
                                MessageBox.Show("PROGRAMACION SIN ABASTECIMIENTO", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }


                        });

                        /* menu.Items.Add("Ver Despacho", default(Image), (snd, evt) =>
                         {

                         });*/
                        menu.Items.Add("Seguimiento", default(Image), (snd, evt) =>
                        {
                            if (dgvPreviajes.DataSource == null)
                            {
                                MessageBox.Show("Debe Seleccionar una Programación", "ALERTA");
                                return;
                            }
                            if (Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["OT"].Value.ToString()) < 1)
                            {
                                MessageBox.Show("La programacion seleccionada no contine OT...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                int pos = Convert.ToInt32(dgvPreviajes.CurrentRow.Index.ToString());
                                string codigo = dgvPreviajes.Rows[pos].Cells["CODIGO"].Value.ToString();
                                var_IdProg = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                                string anio = dgvPreviajes.Rows[e.RowIndex].Cells["ANIO"].Value.ToString();
                                string ruta = dgvPreviajes.Rows[e.RowIndex].Cells["RUTA"].Value.ToString();
                                ProgramacionViajes.frmVerTracking frmtracking = new ProgramacionViajes.frmVerTracking();
                                frmtracking.setearvariable(var_IdProg, codigo, anio, ruta);
                                frmtracking.ShowDialog(this);
                            }
                        });

                        //OPCION PARA MOVER LA PROGRAMACION DE UNA OPERACION A OTRA
                        menu.Items.Add("Mover de programación", Resources.desplazamiento, (snd, evt) =>
                        {
                            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            int tipoProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTIPO"].Value.ToString());
                            //string planilla = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["Planilla"].Value.ToString());
                            string Programacion = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                         

                            ProgramacionViajes.frmMoverProgramaciones frmtracking = new ProgramacionViajes.frmMoverProgramaciones();
                            frmtracking.enviarDatos(NroProgramacion, tipoProgramacion, Programacion);
                            frmtracking.ShowDialog(this);

                        });
                        //Obtienes las coordenadas de la celda seleccionada. 
                        Rectangle coordenada = dgvPreviajes.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                        int anchoCelda = coordenada.Location.X; //Ancho de la localizacion de la celda
                        int altoCelda = coordenada.Location.Y;  //Alto de la localizacion de la celda
                        //Y para mostrar el menú lo haces de esta forma:  
                        int X = anchoCelda + dgvPreviajes.Location.X;
                        int Y = altoCelda + dgvPreviajes.Location.Y + 15;
                        menu.Show(dgvPreviajes, new Point(X, Y));

                        menu.Items.Add("Registrar Faltantes de Viaje", Resources.history, (snd, evt) =>
                        {
                            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                            Operaciones.frmRegistroFaltantesMercaderia frmFaltantes = new Operaciones.frmRegistroFaltantesMercaderia();
                            frmFaltantes.EnviarDatos(NroProgramacion, Usuario);
                            frmFaltantes.ShowDialog(this);
                        });

                        menu.Items.Add("Registrar Falla Mecánica", Resources.menos, (snd, evt) =>
                        {
                            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                            FallasMecanicas.frmRegistroFallasMecanicas frmFallas = new FallasMecanicas.frmRegistroFallasMecanicas();
                            frmFallas.pManual.Enabled = false;
                            frmFallas.pManual.SendToBack();
                            frmFallas.groupBoxT.Enabled = false;
                            frmFallas.groupBoxT.SendToBack();
                            frmFallas.groupBox1.Enabled = true;
                            frmFallas.groupBox1.BringToFront();
                            frmFallas.idTracto = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDTRACTO"].Value.ToString());
                            frmFallas.idCarreta = dgvPreviajes.CurrentRow.Cells["IDREMOLQUE"].Value.ToString() == "" ? -1 : Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDREMOLQUE"].Value.ToString());
                            frmFallas.idConductor = Convert.ToInt32(dgvPreviajes.Rows[e.RowIndex].Cells["IDCONDUCTOR"].Value.ToString());
                            frmFallas.cbBloqueoC.Checked = false;
                            frmFallas.cbBloqueoC_CheckedChanged(sender, e);
                            frmFallas.cbBloqueoU.Checked = false;
                            frmFallas.cbBloqueoU_CheckedChanged(sender, e);
                            frmFallas.cbxTipoDanio.Text = "MATERIAL";
                            frmFallas.txtPlacaKN.Text = dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString();
                            frmFallas.EnviarDatos(NroProgramacion, Usuario);
                            frmFallas.ShowDialog(this);
                        });

                        menu.Items.Add("Generar Planilla de Gasto", Resources.gastosentregados, (snd, evt) =>
                        {
                            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                        });

                        (menu.Items[10] as ToolStripMenuItem).DropDownItems.Add("Generar Planilla", Resources.nuevo_button, (snd, evt) =>
                        {
                            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                            int idOperacion = Convert.ToInt32(comboBox1.SelectedValue);
                            int IdRuta = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IdRuta"].Value.ToString());
                            decimal Gasto = 0.00M;
                            //decimal Gasto = Convert.ToDecimal(dgvPreviajes.CurrentRow.Cells["VIATICOS"].Value.ToString());

                            if (IdRuta == -1)
                            {
                                MessageBox.Show("No puede generar planilla en un viaje sin ruta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                string ValorViatico = dgvPreviajes.CurrentRow.Cells["VIATICOS"].Value.ToString();

                                if (ValorViatico.Length != 0)
                                { Gasto = Convert.ToDecimal(dgvPreviajes.CurrentRow.Cells["VIATICOS"].Value.ToString()); }
                                else
                                {
                                    DataTable dtGastoRuta = new DataTable();
                                    dtGastoRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(4, IdRuta, idOperacion, -1);
                                    if (dtGastoRuta.Rows.Count > 0) { Gasto = Convert.ToDecimal(dtGastoRuta.Rows[0]["GASTO"]); }
                                }

                                TicketsGasto.frmGenerarTicketGasto frmTicketGasto = new TicketsGasto.frmGenerarTicketGasto();
                                frmTicketGasto.EnviarDatos(NroProgramacion, Usuario, Gasto);
                                frmTicketGasto.ShowDialog(this);
                            }
                        });

                        (menu.Items[10] as ToolStripMenuItem).DropDownItems.Add("Gastos Adicionales", Resources.persona_logo_icon_169946, (snd, evt) =>
                        {
                            string Planilla = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();
                            int idConductor = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCONDUCTOR"].Value.ToString());
                            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            int idOperacion = Convert.ToInt32(comboBox1.SelectedValue);
                            int IdRuta = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IdRuta"].Value.ToString());

                            TicketsGasto.frmGastosAdicionalesTolvas frmGastosAdicionalesTolvas = new TicketsGasto.frmGastosAdicionalesTolvas();
                            frmGastosAdicionalesTolvas.idConductor = idConductor;
                            frmGastosAdicionalesTolvas.lblConductor.Text = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();
                            frmGastosAdicionalesTolvas.txtPlanilla2.Text = Planilla;
                            frmGastosAdicionalesTolvas.NroProgramacion = NroProgramacion;
                            frmGastosAdicionalesTolvas.idOperacion = idOperacion;
                            frmGastosAdicionalesTolvas.idRuta = IdRuta;
                            frmGastosAdicionalesTolvas.frmOperacion_Previajes = this;
                            frmGastosAdicionalesTolvas.frmLPT = 0;
                            frmGastosAdicionalesTolvas.ShowDialog(this);
                        });

                        (menu.Items[10] as ToolStripMenuItem).DropDownItems.Add("Agregar Cambio de Ruta", Resources.desplazamiento, (snd, evt) =>
                        {
                            string Planilla = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();
                            int IdRuta = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IdRuta"].Value.ToString());
                            int idProg = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTIPO"].Value.ToString());
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                            if (Planilla == "")
                            {
                                MessageBox.Show("No puede generar un gasto adicional a este viaje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                DataTable dtGastoRuta = new DataTable();
                                dtGastoRuta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastosAdicionales(6, -1, Convert.ToInt32(Planilla), -1);
                                if (dtGastoRuta.Rows.Count > 0)
                                {
                                    TicketsGasto.frmViaticoCambioRuta frmViaticoCambioRuta = new TicketsGasto.frmViaticoCambioRuta();
                                    frmViaticoCambioRuta.frmLPT = 0;
                                    frmViaticoCambioRuta.frmOperacion_Previajes = this;
                                    frmViaticoCambioRuta.lblPlanilla.Text = Planilla;
                                    frmViaticoCambioRuta.txtRutaOriginal.Text = Convert.ToString(dtGastoRuta.Rows[0]["RUTA"]);
                                    frmViaticoCambioRuta.txtGastoOriginal.Text = Convert.ToString(dtGastoRuta.Rows[0]["GASTO"]);
                                    frmViaticoCambioRuta.idRutaNueva = IdRuta;
                                    frmViaticoCambioRuta.txtNuevaRuta.Text = dgvPreviajes.CurrentRow.Cells["RUTA"].Value.ToString();
                                    frmViaticoCambioRuta.idProg = idProg;
                                    frmViaticoCambioRuta.cbxRuta.Visible = false;
                                    frmViaticoCambioRuta.cbxRuta.SendToBack();
                                    frmViaticoCambioRuta.ShowDialog(this);
                                }
                                else
                                {
                                    MessageBox.Show("La ruta asignada no tiene un gasto definido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }

                            /*
                            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            string Planilla = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                            int idConductor = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCONDUCTOR"].Value.ToString());
                            string Estado = dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString();

                            if (Planilla != "")
                            {
                                MessageBox.Show("Este viaje ya tiene una planilla adjunta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                TicketsGasto.frmViaticosAdicionales frmViaticosAdicionales = new TicketsGasto.frmViaticosAdicionales();
                                frmViaticosAdicionales.NroTicket = NroProgramacion;
                                frmViaticosAdicionales.Usuario = Usuario;
                                frmViaticosAdicionales.idConductor = idConductor;
                                frmViaticosAdicionales.lblConductor.Text = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();
                                frmViaticosAdicionales.Planilla = Planilla;

                                frmViaticosAdicionales.panel4.Visible = false;
                                frmViaticosAdicionales.panel1.Visible = false;
                                frmViaticosAdicionales.dtpFechaViatico.Enabled = false;
                                frmViaticosAdicionales.cbxTipoViatico.Enabled = false;
                                frmViaticosAdicionales.txtMonto.Enabled = false;
                                frmViaticosAdicionales.btnImprimirViatico.Enabled = false;
                                frmViaticosAdicionales.btnGuardar.Enabled = false;
                                frmViaticosAdicionales.eliminarToolStripMenuItem.Enabled = false;

                                frmViaticosAdicionales.ShowDialog(this);
                            }
                            */
                        });

                        (menu.Items[10] as ToolStripMenuItem).DropDownItems.Add("Quitar Planilla", Resources.cancel, (snd, evt) =>
                        {
                            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            string Planilla = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                            if (Planilla == "")
                            {
                                MessageBox.Show("Este viaje no tiene planilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                if (MessageBox.Show("¿Desea desvincular esta planilla?", "DESVINCULAR PLANILLA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    DataTable dtRespuesta = new DataTable();
                                    string Respuesta;
                                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_EliminarPlanilla(NroProgramacion, Planilla, Usuario);
                                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                    string NroRPTA = Respuesta.Substring(0, 1);
                                    if (NroRPTA == "0")
                                    {
                                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //CargarDatos();
                                    }
                                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }
                            }
                        });

                        (menu.Items[10] as ToolStripMenuItem).DropDownItems.Add("Asignar Planilla", Resources.history, (snd, evt) =>
                        {
                            try
                            {
                                dtPlanillas.DataSource = null;
                                dgvPlanillas.Columns.Clear();

                                txtBuscaConductor.Text = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();

                                System.Data.DataTable dt2 = new System.Data.DataTable();
                                dt2.Clear();
                                dt2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPlanillasSinViaje(txtBuscaConductor.Text, txtBuscarRuta.Text);
                                if (dt2.Rows.Count > 0)
                                {
                                    dtPlanillas.DataSource = dt2;
                                    dgvPlanillas.Columns["idGastoXRutaC"].Visible = false;
                                    dgvPlanillas.BestFitColumns();
                                }

                                pPlanillas.Visible = true;
                                pPlanillas.BringToFront();

                                /*
                                string Planilla = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();

                                if (Planilla != "")
                                {
                                    MessageBox.Show("Este viaje ya tiene una planilla asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                else
                                {
                                    dtPlanillas.DataSource = null;
                                    dgvPlanillas.Columns.Clear();

                                    System.Data.DataTable dt2 = new System.Data.DataTable();
                                    dt2.Clear();
                                    dt2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPlanillasSinViaje();
                                    if (dt.Rows.Count > 0)
                                    {
                                        dtPlanillas.DataSource = dt2;

                                        dgvPlanillas.Columns["idGastoXRutaC"].Visible = false;

                                        dgvPlanillas.BestFitColumns();
                                    }
                                }*/
                            }
                            catch
                            { MessageBox.Show("No se pudo asignar la planilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        });

                        (menu.Items[10] as ToolStripMenuItem).DropDownItems.Add("Generar Planilla de Evento", Resources.mover, (snd, evt) =>
                        {
                            string Planilla = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();
                            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            int idConductor = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCONDUCTOR"].Value.ToString());
                            int IdRuta = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IdRuta"].Value.ToString());

                            frmGenerarPLE frmGenerarPLE = new frmGenerarPLE();
                            frmGenerarPLE.txtPlanilla.Text = Planilla;
                            frmGenerarPLE.NroTicket = NroProgramacion;
                            frmGenerarPLE.idConductorN = idConductor;
                            frmGenerarPLE.txtConductorN.Text = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();
                            frmGenerarPLE.idRuta = IdRuta;
                            frmGenerarPLE.txtRuta.Text = dgvPreviajes.CurrentRow.Cells["RUTA"].Value.ToString();
                            frmGenerarPLE.ShowDialog(this);
                        });

                        int idProg2 = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTIPO"].Value.ToString());

                        if (idProg2 == 3 || idProg2 == 10)
                        {
                            (menu.Items[10] as ToolStripMenuItem).DropDownItems.Add("Generar P. Apoyo", Resources.team, (snd, evt) =>
                            {
                                frmGenerarPlanillaRE frmGenerarPlanillaRE = new TicketsGasto.frmGenerarPlanillaRE();
                                frmGenerarPlanillaRE.idOperacion = idProg2;
                                frmGenerarPlanillaRE.lblTitulo.Text = "GENERAR PLANILLA DE APOYO";
                                frmGenerarPlanillaRE.txtPreviaje.Text = Convert.ToString(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                                frmGenerarPlanillaRE.txtPlanilla.Text = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();
                                frmGenerarPlanillaRE.idConductorP = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCONDUCTOR"].Value.ToString());
                                frmGenerarPlanillaRE.txtConductorP.Text = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();
                                frmGenerarPlanillaRE.idRuta = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IdRuta"].Value.ToString());
                                frmGenerarPlanillaRE.txtRuta.Text = dgvPreviajes.CurrentRow.Cells["RUTA"].Value.ToString();
                                frmGenerarPlanillaRE.cbRedondear.Checked = false;
                                frmGenerarPlanillaRE.cbRedondear_CheckedChanged(sender, e);
                                frmGenerarPlanillaRE.label5.Text = "Gasto Ruta Asignado: ";
                                frmGenerarPlanillaRE.ShowDialog(this);
                            });
                        }
                        else
                        {
                            (menu.Items[10] as ToolStripMenuItem).DropDownItems.Add("Generar P. Reconocimiento", Resources.team, (snd, evt) =>
                            {
                                frmGenerarPlanillaRE frmGenerarPlanillaRE = new TicketsGasto.frmGenerarPlanillaRE();
                                frmGenerarPlanillaRE.idOperacion = idProg2;
                                frmGenerarPlanillaRE.lblTitulo.Text = "GENERAR PLANILLA DE RECONOCIMIENTO";
                                frmGenerarPlanillaRE.txtPreviaje.Text = Convert.ToString(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                                frmGenerarPlanillaRE.txtPlanilla.Text = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();
                                frmGenerarPlanillaRE.idConductorP = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCONDUCTOR"].Value.ToString());
                                frmGenerarPlanillaRE.txtConductorP.Text = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();
                                frmGenerarPlanillaRE.idRuta = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IdRuta"].Value.ToString());
                                frmGenerarPlanillaRE.txtRuta.Text = dgvPreviajes.CurrentRow.Cells["RUTA"].Value.ToString();
                                frmGenerarPlanillaRE.cbRedondear.Enabled = false;
                                frmGenerarPlanillaRE.label5.Text = "Gasto Reconocimiento: ";
                                frmGenerarPlanillaRE.ShowDialog(this);
                            });
                        }

                        menu.Items.Add("Ticket de Despacho de Unidad", Resources.text, (snd, evt) =>
                        {
                            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            DateTime FechaRegistro = DateTime.Now; //Convert.ToDateTime(dgvPreviajes.CurrentRow.Cells["FECHAREGISTRO"].Value.ToString());

                            frmTicketDespacho frmTicket = new frmTicketDespacho();
                            frmTicket.EnviarDatos(NroProgramacion);
                            frmTicket.dtpFecha.Value = FechaRegistro;
                            frmTicket.ShowDialog(this);
                        });

                        try
                        {
                            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaFallasMecanicas");
                            if (dtPermisos != null)
                            {
                                if (dtPermisos.Rows.Count > 0)
                                {
                                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                                    {
                                        menu.Items[9].Enabled = true;
                                    }
                                    else { menu.Items[9].Enabled = false; }
                                }
                            }

                        }
                        catch
                        {
                            menu.Items[9].Enabled = false;
                        }

                        try
                        {
                            DataTable dtPermisos2 = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistroFaltantesMercaderia");
                            if (dtPermisos2 != null)
                            {
                                if (dtPermisos2.Rows.Count > 0)
                                {
                                    if (Convert.ToBoolean(dtPermisos2.Rows[0]["Nuevo"]) == true)
                                    {
                                        menu.Items[8].Enabled = true;
                                    }
                                    else { menu.Items[8].Enabled = false; }
                                }
                            }

                        }
                        catch { menu.Items[8].Enabled = false; }

                        try
                        {
                            if (dtPermisos3 != null)
                            {
                                if (dtPermisos3.Rows.Count > 0)
                                {
                                    if (Convert.ToBoolean(dtPermisos3.Rows[0]["Nuevo"]) == true)
                                    {
                                        menu.Items[10].Enabled = true;

                                        string Planilla3 = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();

                                        if (Planilla3 == "")
                                        {
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[0].Enabled = true;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[1].Enabled = false;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[2].Enabled = false;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[3].Enabled = false;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[4].Enabled = true;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[5].Enabled = false;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[6].Enabled = false;
                                        }
                                        else
                                        {
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[0].Enabled = true;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[1].Enabled = true;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[2].Enabled = true;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[3].Enabled = true;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[4].Enabled = true;
                                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[5].Enabled = true;

                                            int idProg = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTIPO"].Value.ToString());

                                            if (idProg == 2 || idProg == 3 || idProg == 9 || idProg == 10 || idProg == 11 || idProg == 13)
                                            { (menu.Items[10] as ToolStripMenuItem).DropDownItems[6].Enabled = true; }
                                            else { (menu.Items[10] as ToolStripMenuItem).DropDownItems[6].Enabled = false; }
                                        }
                                    }
                                    else { menu.Items[10].Enabled = false; }
                                }
                            }
                        }
                        catch
                        {
                            menu.Items[10].Enabled = false;
                            (menu.Items[10] as ToolStripMenuItem).DropDownItems[1].Enabled = false;
                        }

                        menu.Items.Add("Historial Abastecimiento", Resources.impresora, (snd, evt) =>
                        {
                           /* string codigo = dgvPreviajes.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString();
                            string sucursal = dgvPreviajes.Rows[e.RowIndex].Cells["SUCURSAL"].Value.ToString();
                            string tipoprogramacion = dgvPreviajes.Rows[e.RowIndex].Cells["TIPO"].Value.ToString();
                            string fechaprogramacion = dgvPreviajes.Rows[e.RowIndex].Cells["FECHAPROGR"].Value.ToString();
                            string placa = dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString();
                            string conductor = dgvPreviajes.Rows[e.RowIndex].Cells["CONDUCTOR"].Value.ToString();
                           

                            ProgramacionViajes.frmTicket frmTick = new ProgramacionViajes.frmTicket();
                            frmTick.Enviardatosticket(codigo, sucursal, tipoprogramacion, fechaprogramacion, placa, ruta, cliente, conductor, producto, programador, dni, EsConsolidado, observacion);
                            frmTick.ShowDialog(this);*/
                        });

                        menu.Items.Add("Generar Ticket de Lavado", Resources.liberardoc, (snd, evt) =>
                        {
                            txtTracto.Text = dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString();
                            txtCarreta.Text = dgvPreviajes.Rows[e.RowIndex].Cells["SEMIRREMOLQUE"].Value.ToString();
                            txtOperacion.Text = dgvPreviajes.Rows[e.RowIndex].Cells["TIPO"].Value.ToString();
                            cbxTipoLavado.Text = "AMBOS";

                            pNuevoLavado.Visible = true;
                            pNuevoLavado.BringToFront();
                        });

                        menu.Items.Add("Ver Rendimiento Combustible", Resources.abasteciop, (snd, evt) =>
                        {
                            CargaCombustible CargaCombustible = new CargaCombustible();
                            CargaCombustible.txtTracto.Text = dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString();
                            CargaCombustible.dtpFechaFin.Value = DateTime.Now;
                            CargaCombustible.Show();
                            CargaCombustible.btnBuscar_Click(sender, e);
                        });

                        /*
                        menu.Items.Add("Constancia de Entrega de Unidad", Resources.settingscol, (snd, evt) =>
                        {
                            frmGenerarConstancia frmGenerarConstancia = new frmGenerarConstancia();
                            frmGenerarConstancia.idConductorViaje = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCONDUCTOR"].Value.ToString());
                            frmGenerarConstancia.idTracto = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTRACTO"].Value.ToString());
                            frmGenerarConstancia.idCarreta = dgvPreviajes.CurrentRow.Cells["IDREMOLQUE"].Value.ToString() == "" ? -1 : Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDREMOLQUE"].Value.ToString());
                            frmGenerarConstancia.txtConductorViaje.Text = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();
                            frmGenerarConstancia.txtPlacaViaje.Text = dgvPreviajes.CurrentRow.Cells["TRACTO"].Value.ToString();
                            frmGenerarConstancia.txtCarreta.Text = dgvPreviajes.CurrentRow.Cells["SEMIRREMOLQUE"].Value.ToString();
                            frmGenerarConstancia.Show();
                        });

                        dtPermisos5 = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmConstanciaUnidades");

                        try
                        {
                            if (dtPermisos5 != null)
                            {
                                if (dgvPreviajes.Rows[e.RowIndex].Cells["ESTADO"].Value.ToString().Equals("ATENDIDO") && Convert.ToBoolean(dtPermisos5.Rows[0]["Nuevo"]) == true)
                                { (menu.Items[15] as ToolStripMenuItem).Enabled = true; }
                                else
                                { (menu.Items[15] as ToolStripMenuItem).Enabled = false; }
                            }
                        }
                        catch { menu.Items[15].Enabled = false; }
                        */

                        menu.Items.Add("Generar Itinerario de Viaje", Resources.history, (snd, evt) =>
                        {
                            frmRegistrarItinerario frmRegistrarItinerario = new frmRegistrarItinerario();
                            frmRegistrarItinerario.NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
                            frmRegistrarItinerario.FechaProgramacion = Convert.ToDateTime(dgvPreviajes.CurrentRow.Cells["FECHAPROGR"].Value.ToString());
                            frmRegistrarItinerario.idConductorViaje = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCONDUCTOR"].Value.ToString());
                            frmRegistrarItinerario.idTracto = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTRACTO"].Value.ToString());
                            frmRegistrarItinerario.idCarreta = dgvPreviajes.CurrentRow.Cells["IDREMOLQUE"].Value.ToString() == "" ? -1 : Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDREMOLQUE"].Value.ToString());
                            frmRegistrarItinerario.IdRuta = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IdRuta"].Value.ToString());

                            frmRegistrarItinerario.lblCodigo.Text = dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString();
                            frmRegistrarItinerario.txtFechaProg.Text = dgvPreviajes.CurrentRow.Cells["FECHAPROGR"].Value.ToString();
                            frmRegistrarItinerario.txtConductor.Text = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();
                            frmRegistrarItinerario.txtTracto.Text = dgvPreviajes.CurrentRow.Cells["TRACTO"].Value.ToString();
                            frmRegistrarItinerario.txtCarreta.Text = dgvPreviajes.CurrentRow.Cells["SEMIRREMOLQUE"].Value.ToString();
                            frmRegistrarItinerario.txtRuta.Text = dgvPreviajes.CurrentRow.Cells["RUTA"].Value.ToString();
                            frmRegistrarItinerario.ShowDialog();
                        });

                        dtPermisos6 = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmItinerarioViajes");

                        try
                        {
                            if (dtPermisos6 != null)
                            {
                                if (Convert.ToBoolean(dtPermisos6.Rows[0]["Nuevo"]) == true)
                                { (menu.Items[15] as ToolStripMenuItem).Enabled = true; }
                                else
                                { (menu.Items[15] as ToolStripMenuItem).Enabled = false; }
                            }
                        }
                        catch { menu.Items[15].Enabled = false; }

                        menu.Items.Add("Tiempos de Viaje", Resources.binocular, (snd, evt) => { });

                        (menu.Items[16] as ToolStripMenuItem).DropDownItems.Add("Agregar Tiempos de Viaje", Resources.nuevo_button, (snd, evt) =>
                        {
                            string Operacion = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();

                            if (Operacion == "LINDLEY" || Operacion == "LIMAGAS" || Operacion == "GENERAL" || Operacion == "VOLCAN")
                            {
                                frmRegistrarTiemposViaje frmRegistrarTiemposViaje = new frmRegistrarTiemposViaje();

                                frmRegistrarTiemposViaje.txtPreviaje.Text = dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString();
                                frmRegistrarTiemposViaje.dtpFechaProg.Text = dgvPreviajes.CurrentRow.Cells["FECHAPROGR"].Value.ToString();
                                frmRegistrarTiemposViaje.txtOperacion.Text = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                                frmRegistrarTiemposViaje.txtConductor.Text = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();
                                frmRegistrarTiemposViaje.txtRuta.Text = dgvPreviajes.CurrentRow.Cells["RUTA"].Value.ToString();
                                frmRegistrarTiemposViaje.txtTracto.Text = dgvPreviajes.CurrentRow.Cells["TRACTO"].Value.ToString();
                                frmRegistrarTiemposViaje.txtSR.Text = dgvPreviajes.CurrentRow.Cells["SEMIRREMOLQUE"].Value.ToString();

                                frmRegistrarTiemposViaje.Opcion = 1;
                                frmRegistrarTiemposViaje.idRuta = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IdRuta"].Value.ToString());
                                frmRegistrarTiemposViaje.CargarComboUbicacion();
                                frmRegistrarTiemposViaje.ShowDialog(this);
                            }
                            else { MessageBox.Show("No puede asignar tiempos para viajes de esta operación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        });

                        (menu.Items[16] as ToolStripMenuItem).DropDownItems.Add("Agregar Tiempos de Pernocte", Resources.dormido, (snd, evt) =>
                        {
                            string Operacion = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();

                            if (Operacion == "LINDLEY" || Operacion == "LIMAGAS" || Operacion == "GENERAL" || Operacion == "VOLCAN")
                            {
                                frmRegistrarTiempoPernocte frmRegistrarTiempoPernocte = new frmRegistrarTiempoPernocte();

                                frmRegistrarTiempoPernocte.txtPreviaje.Text = dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString();
                                frmRegistrarTiempoPernocte.dtpFechaProg.Text = dgvPreviajes.CurrentRow.Cells["FECHAPROGR"].Value.ToString();
                                frmRegistrarTiempoPernocte.txtOperacion.Text = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                                frmRegistrarTiempoPernocte.txtConductor.Text = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();
                                frmRegistrarTiempoPernocte.txtRuta.Text = dgvPreviajes.CurrentRow.Cells["RUTA"].Value.ToString();
                                frmRegistrarTiempoPernocte.txtTracto.Text = dgvPreviajes.CurrentRow.Cells["TRACTO"].Value.ToString();
                                frmRegistrarTiempoPernocte.txtSR.Text = dgvPreviajes.CurrentRow.Cells["SEMIRREMOLQUE"].Value.ToString();
                                frmRegistrarTiempoPernocte.ShowDialog(this);
                            }
                            else { MessageBox.Show("No puede asignar tiempos de pernocte a este viaje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        });

                        dtPermisos7 = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaTiemposViaje");

                        try
                        {
                            if (dtPermisos7 != null)
                            {
                                if (Convert.ToBoolean(dtPermisos7.Rows[0]["Nuevo"]) == true) { (menu.Items[16] as ToolStripMenuItem).Enabled = true; }
                                else { (menu.Items[16] as ToolStripMenuItem).Enabled = false; }
                            }
                            else { menu.Items[16].Enabled = false; }
                        }
                        catch { menu.Items[16].Enabled = false; }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private DataTable cargarDatosPorDefectoSegunCliente()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CompletarDestinatario_Direcciones(Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IdCliente"].Value), Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["idRuta"].Value));

            return dt;
        }

        //INPUTBOX PARA DIFERENTES OPCIONES: ANULAR VIAJES,AGREGAR COMENTARIO, INGRESAR PESO
        private static DialogResult ShowInputDialogBox(ref string input, string prompt, string title = "Anular Viajes", int width = 100, int height = 200)
        {
            Size size = new Size(width, height);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Height = 150;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.Text = title;

            //Create a new label to hold the prompt
            Label label = new Label();
            label.Text = prompt;
            label.Location = new Point(5, 5);
            label.Width = size.Width - 10;
            label.Margin = new System.Windows.Forms.Padding(3, 25, 2, 35);
            inputBox.Controls.Add(label);

            //Create a textbox to accept the user's input
            TextBox textBox = new TextBox();
            textBox.Size = new Size(260, 23);
            textBox.Location = new Point(20, label.Location.Y + 20);
            textBox.Text = input.ToUpper();
            textBox.Location = new Point(20, 40);
            inputBox.Controls.Add(textBox);

            //Create an OK Button 
            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new Point(size.Width - 80 - 80, 100 - 30);
            inputBox.Controls.Add(okButton);

            //Create a Cancel Button
            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new Point(size.Width - 80, 100 - 30);
            inputBox.Controls.Add(cancelButton);

            //Set the input box's buttons to the created OK and Cancel Buttons respectively so the window appropriately behaves with the button clicks
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            //Show the window dialog box 
            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            string obser = textBox.Text;
            return result;
        }

        //INPUTBOX CON COMBOBOX INCLUIDO CARGADO CON ESTADOS DESDE SQL PARA PREVIAJES
        private static DialogResult ShowInputDialogCombo(ref string input, string prompt, string title = "Estado Viajes", int width = 100, int height = 200)
        {
            Size size = new Size(width, height);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Height = 150;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.Text = title;

            //Create a new label to hold the prompt
            Label label = new Label();
            label.Text = prompt;
            label.Location = new Point(5, 5);
            label.Width = size.Width - 10;
            label.Margin = new System.Windows.Forms.Padding(3, 25, 2, 35);
            inputBox.Controls.Add(label);

            //Create a textbox to accept the user's input
            System.Windows.Forms.ComboBox cbo = new System.Windows.Forms.ComboBox();
            cbo.Size = new Size(260, 23);
            cbo.Location = new Point(20, label.Location.Y + 20);
            cbo.Text = "Seleccionar...";
            cbo.DropDownStyle = ComboBoxStyle.DropDownList;
            // cbo.DrawItem += new DrawItemEventHandler(ListBox1_DrawItem);
            cbo.Location = new Point(20, 40);
            // inputBox.Controls.Add(cbo);
            DataTable dtEstado = clsOperacionesBL.Instancia.Llenar_ControlesPreViajes();

            if (dtEstado != null || dtEstado.Rows.Count > 0)
            {
                cbo.DataSource = dtEstado;
                cbo.DisplayMember = "Descripcion";
                cbo.ValueMember = "Descripcion";
            }
            else
            {
                MessageBox.Show("No hay estados");
            }

            inputBox.Controls.Add(cbo);

            //Create an OK Button 
            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new Point(size.Width - 80 - 80, 100 - 30);
            inputBox.Controls.Add(okButton);

            //Create a Cancel Button
            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new Point(size.Width - 80, 100 - 30);
            inputBox.Controls.Add(cancelButton);

            //Set the input box's buttons to the created OK and Cancel Buttons respectively so the window appropriately behaves with the button clicks
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            //Show the window dialog box 
            DialogResult result = inputBox.ShowDialog();
            int datoestado = cbo.SelectedIndex + 1;
            input = datoestado.ToString();
            string obser = cbo.Text;

            return result;
        }

        private void chbUbicacion_CheckedChanged(object sender, EventArgs e)
        {
            //CHECKBOX PARA HABILITAR Y DESABILITAR EL NAVEGADOR WEB
            if (chbUbicacion.Checked == true)
            {
                splitContainer2.Panel2Collapsed = false;
                webView1.Navigate("about:blank");
            }
            else
            {
                splitContainer2.Panel2Collapsed = true;
                webView1.Navigate("about:blank");
            }
        }

        string UBIDireccion;
        string UBIGps;
        private void dgvPreviajes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                groupBox3.Visible = false;
                dgvViajesAltra.Visible = false;

                for (int i = 0; i < dtTipoProg.Rows.Count; i++)
                {
                    if (comboBox1.Text.ToString() == dtTipoProg.Rows[i]["Descripcion"].ToString())
                        var_Accesos = Convert.ToInt32(dtTipoProg.Rows[i]["ACCESOS"].ToString());
                }                

                if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "ABASTECIDO" && this.dgvPreviajes.CurrentRow.Index == 0)
                {
                    if (Tiporder == 1)
                    {
                        dgvPreviajes.Sort(dgvPreviajes.Columns[5], ListSortDirection.Descending);
                        Tiporder = 2;
                    }
                    //Filtra primero los abastecimientos de vacios a llenos
                    else
                    {
                        dgvPreviajes.Sort(dgvPreviajes.Columns[5], ListSortDirection.Ascending);
                        Tiporder = 1;
                    }
                }

                if (var_Accesos == 2)
                {

                    if ((comboBox1.Text == "LINDLEY") && dgvPreviajes.Rows[e.RowIndex].Cells["TIPO"].Value.ToString() == "LINDLEY")
                    {
                        int idProgramacionActual = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());

                        //OPCION PARA CAMBIAR LA POSICION DE UN PREVIAJE EN EL LISTADO
                        if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "ITEM")
                        {

                            int itemInicial = Convert.ToInt32(dgvPreviajes.Rows[0].Cells["ITEM"].Value.ToString());
                            int itemFinal = Convert.ToInt32(dgvPreviajes.Rows[dgvPreviajes.Rows.Count - 1].Cells["ITEM"].Value.ToString());

                            int item = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["ITEM"].Value.ToString());
                            string programacionmostrar = dgvPreviajes.Rows[e.RowIndex].Cells["TIPO"].Value.ToString();
                            string anio = dgvPreviajes.Rows[e.RowIndex].Cells["ANIO"].Value.ToString();

                            int idProgramacionNuevo = 0;
                            int itemNuevo = 0;

                            if (MessageBox.Show("Desea cambiar posición del Item: " + item.ToString() + " de la programacion: " + programacionmostrar + "?", "CAMBIAR POSICIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                FrmCambiarPosicionPreViaje frm = new FrmCambiarPosicionPreViaje();

                                frm.ItemInicial = itemInicial;
                                frm.ItemFinal = itemFinal;
                                frm.ItemActual = item;

                                frm.ShowDialog(this);

                                if (frm.Resp == 1)
                                {
                                    itemNuevo = frm.ItemNuevo;

                                    int esMenorMayorPosicion;

                                    if (itemNuevo < item) { esMenorMayorPosicion = 0; }
                                    else { esMenorMayorPosicion = 1; }

                                    idProgramacionNuevo = Convert.ToInt32(dgvPreviajes.Rows[itemNuevo - 1].Cells["IDPROGRAMACION"].Value.ToString());
                                    DataTable dtResp = clsOperacionesBL.Instancia.Cambiar_PosicionPreviaje(idProgramacionActual, idProgramacionNuevo, esMenorMayorPosicion, Utilitario.Instancia.SesionUsuario.usuario, anio);
                                    
                                    string Rpta = Convert.ToString(dtResp.Rows[0]["exito"]);
                                    string NrRPTA = Rpta.Substring(0, 1);

                                    if (NrRPTA == "0") { MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                    else
                                    {
                                        MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    //OPCION PARA AGREGAR O MOFICIAR LA OBSERVACION DE UNA PROGRAMACION
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "OBSERVACION")
                    {
                        string input = "";
                        string rspta;
                        int IdProgramacionPeso = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        string programacionPeso = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                        string ANIO = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                        input = dgvPreviajes.CurrentRow.Cells["OBSERVACION"].Value.ToString();
                        if (ShowInputDialogBox(ref input, "Escribir Observación: " + programacionPeso, "INGRESAR OBSERVACION", 300, 200) == DialogResult.OK)
                        {
                            DataTable dtPeso = new DataTable();
                            dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(3, IdProgramacionPeso, 0, input, ANIO);
                            rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                            string NroRPTA = rspta.Substring(0, 1);

                            if (NroRPTA == "0") { MessageBox.Show(rspta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            else { MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }

                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "PESO" && (comboBox1.Text.Equals("LIMAGAS") || comboBox1.Text.Equals("LINDLEY") || comboBox1.Text.Equals("VOLCAN") || comboBox1.Text.Equals("SOLGAS") || comboBox1.Text.Equals("SOLGAS GNL") || comboBox1.Text.Equals("GENERAL")) &&
                        dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString() == "ATENDIDO" && (dgvPreviajes.CurrentRow.Cells["GUIAT."].Value.ToString() != "-" && dgvPreviajes.CurrentRow.Cells["GUIAT."].Value.ToString().Substring(0, 1) != "V"))
                    {
                        string input = "";
                        string rspta;
                        int IdProgramacionPeso = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        string programacionPeso = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                        string ANIO = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                       
                        input = dgvPreviajes.CurrentRow.Cells["PESO"].Value.ToString();
                        if (ShowInputDialogBox(ref input, "Escribir el Nuevo Peso: " + programacionPeso, "INGRESAR PESO", 300, 200) == DialogResult.OK)
                        {
                            decimal result;
                            DataTable dtPeso = new DataTable();
                            Boolean esNumero = decimal.TryParse(input, out result);

                            if (esNumero)
                            {
                                dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(13, IdProgramacionPeso, Convert.ToDecimal(input), "", ANIO); // Ingresar Peso Carga
                                rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                                string NroRPTA = rspta.Substring(0, 1);

                                if (NroRPTA == "0") { MessageBox.Show(rspta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                                else { MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            }
                            else
                            {
                                MessageBox.Show("El valor ingersado no es Numero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }

                    //OPCION PARA CAMBIAR EL ESTADO DE UNA PROGRAMACION DESDE EL LSITADO - CUALQUIER PROGRAMACION MENOS "TOLVAS"
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "ESTADO" && (!comboBox1.Text.Equals("TOLVAS")))
                    {
                        string ESTADOPOSICION = dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString();
                        if (!ESTADOPOSICION.Equals("ATENDIDO") && !ESTADOPOSICION.Equals("ANULADO"))
                        {
                            string input = "";

                            int pos = Convert.ToInt32(dgvPreviajes.CurrentRow.Index.ToString());

                            int IdProgramacionObser = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                            string OTVALOR = dgvPreviajes.CurrentRow.Cells["OT"].Value.ToString();
                            string ANIO = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                            int TipoProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTIPO"].Value.ToString());

                            input = dgvPreviajes.CurrentRow.Cells["IDESTADO"].Value.ToString();
                            int idtractos = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTRACTO"].Value.ToString());
                            int idcarretas = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDREMOLQUE"].Value.ToString());
                            int idconductores = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCONDUCTOR"].Value.ToString());
                            int idots = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["OT"].Value.ToString());
                            decimal valpeso = Convert.ToDecimal(dgvPreviajes.CurrentRow.Cells["PESO"].Value.ToString());
                            decimal valprecioost = Convert.ToDecimal(dgvPreviajes.CurrentRow.Cells["MONTORDEN"].Value.ToString());

                            if (ShowInputDialogCombo(ref input, "Seleccionar Estado: ", "CAMBIAR ESTADO", 300, 200) == DialogResult.OK)
                            {
                                if (input.Equals("9") && dgvPreviajes.CurrentRow.Cells["INTERNA"].Value.ToString().Equals("0"))
                                {
                                    if (idtractos > 0 && idcarretas > 0 && idcarretas > 0 && idots > 0 && valpeso > 0)
                                    {
                                        DataTable dtRptaViaje = new DataTable();
                                        string Rpta;
                                        dtRptaViaje = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_CreaViaje(IdProgramacionObser, OTVALOR, "1", "1", Utilitario.Instancia.SesionUsuario.usuario, valprecioost, ANIO, TipoProgramacion);
                                        Rpta = Convert.ToString(dtRptaViaje.Rows[0]["exito"]);
                                        string NrRPTA = Rpta.Substring(0, 1);
                                        string codviaje;
                                        if (NrRPTA == "0")
                                        {
                                            codviaje = Rpta.Substring(9, 6);
                                            idViaje = Rpta.Substring(25, 6);

                                            DataTable dtRptaIsertarViaje = new DataTable();
                                            string RptaViaje;
                                            dtRptaIsertarViaje = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_GuardaViajeEstado(IdProgramacionObser, Convert.ToInt32(input), codviaje, "", Utilitario.Instancia.SesionUsuario.usuario, ANIO);
                                            RptaViaje = Convert.ToString(dtRptaIsertarViaje.Rows[0]["exito"]);
                                            string NrRPTAVIAJE = RptaViaje.Substring(0, 1);

                                            if (NrRPTAVIAJE == "0")
                                            {
                                                MessageBox.Show(RptaViaje, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                CargarDatos();
                                            }
                                            else
                                            {
                                                MessageBox.Show(RptaViaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(Rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Para cambiar a estado ATENDIDO, debe ingresar todos los datos de la Programación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                }
                                else
                                {
                                    string Estadocambiar = input;

                                    if (Estadocambiar.Equals("10"))
                                    {
                                        if (ShowInputDialogBox(ref input, "Agregar Comentario para anular", "AGREGAR COMENTARIO", 300, 200) == DialogResult.OK)
                                        {
                                            DataTable dtRptaIsertarViaje = new DataTable();
                                            string RptaViaje;
                                            dtRptaIsertarViaje = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_GuardaViajeEstado(IdProgramacionObser, Convert.ToInt32(Estadocambiar), "", input, Utilitario.Instancia.SesionUsuario.usuario, ANIO);
                                            RptaViaje = Convert.ToString(dtRptaIsertarViaje.Rows[0]["exito"]);
                                            string NrRPTAVIAJE = RptaViaje.Substring(0, 1);

                                            if (NrRPTAVIAJE == "0")
                                            {
                                                MessageBox.Show(RptaViaje, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                CargarDatos();
                                            }
                                            else
                                            {
                                                MessageBox.Show(RptaViaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        DataTable dtRptaIsertarViajes = new DataTable();
                                        string RptaViajes;
                                        dtRptaIsertarViajes = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_GuardaViajeEstado(IdProgramacionObser, Convert.ToInt32(Estadocambiar), "", input, Utilitario.Instancia.SesionUsuario.usuario, ANIO);
                                        RptaViajes = Convert.ToString(dtRptaIsertarViajes.Rows[0]["exito"]);
                                        string NrRPTAVIAJE = RptaViajes.Substring(0, 1);

                                        if (NrRPTAVIAJE == "0")
                                        {
                                            MessageBox.Show(RptaViajes, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            CargarDatos();
                                        }

                                        else
                                        {
                                            MessageBox.Show(RptaViajes, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //OPCION PARA AGREGAR EL PESO DEL CLIENTE SOLO PARA PROGRAMACION LIMAGAS
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "PESOCLIENTE" && ((comboBox1.Text.Equals("LIMAGAS") || (comboBox1.Text.Equals("VOLCAN")) || comboBox1.Text.Equals("SOLGAS") || comboBox1.Text.Equals("SOLGAS GNL") ||
                        this.dgvPreviajes.CurrentRow.Cells["Cliente"].Value.ToString() == "LIMA GAS S A") || this.dgvPreviajes.CurrentRow.Cells["Cliente"].Value.ToString() == "VOLCAN COMPANIA MINERA S.A.A." ||
                        this.dgvPreviajes.CurrentRow.Cells["Cliente"].Value.ToString() == "SOLGAS S.A."))
                    {
                        string input = "";
                        string rspta;
                        int IdProgramacionPeso = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        string programacionPeso = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();

                        if (ShowInputDialogBox(ref input, "Ingresar Peso a la programacion: " + programacionPeso, "INGRESAR PESO CLIENTE", 300, 200) == DialogResult.OK)
                        {
                            DataTable dtPeso = new DataTable();
                            dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(1, IdProgramacionPeso, Convert.ToDecimal(input), "", anio);
                            rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                            string NroRPTA = rspta.Substring(0, 1);

                            if (NroRPTA == "0")
                            {
                                MessageBox.Show(rspta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarDatos();
                            }
                            else { MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }

                    //OPCION PARA AGREGAR UN COMENTARIO A LA PROGRMACION
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "COMENTARIO")
                    {
                        string input = "";
                        string rspta;
                        int IdProgramacionObser = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        string programacionObser = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                        input = dgvPreviajes.CurrentRow.Cells["COMENTARIO"].Value.ToString();
                        
                        if (ShowInputDialogBox(ref input, "Ingresar Comentario a la Programación: " + programacionObser, "AGREGAR COMENTARIO", 300, 200) == DialogResult.OK)
                        {
                            DataTable dtPeso = new DataTable();
                            dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(2, IdProgramacionObser, 0, input, anio);
                            rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                            string NroRPTA = rspta.Substring(0, 1);

                            if (NroRPTA == "0") { MessageBox.Show(rspta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            else { MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }

                    //OPCION PARA AGREGAR ESTADO DE PLANATA SOLO "LINDELY"
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "STATUSPLANTA" && comboBox1.Text.Equals("LINDLEY") && !dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ATENDIDO"))
                    {
                        string input = "";
                        string rspta;
                        int IdProgramacionObser = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        string programacionObser = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                        input = dgvPreviajes.CurrentRow.Cells["STATUSPLANTA"].Value.ToString();
                        
                        if (ShowInputDialogBox(ref input, "Ingresar STATUS a la Programación: " + programacionObser, "AGREGAR STATUS", 300, 200) == DialogResult.OK)
                        {
                            DataTable dtPeso = new DataTable();
                            dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(4, IdProgramacionObser, 0, input, anio);
                            rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                            string NroRPTA = rspta.Substring(0, 1);

                            if (NroRPTA == "0")
                            {
                                MessageBox.Show(rspta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarDatos();
                            }
                            else { MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }

                    //OPCION PARA AGERGAR TRACTO A LA PROGRAMACION DESDE EL LISTADO
                   /* if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "TRACTO" && !dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ATENDIDO"))
                    {
                        string input = "";
                        string rspta;
                        int IdProgramTracto = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        input = dgvPreviajes.CurrentRow.Cells["TRACTO"].Value.ToString();
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                        if (ShowInputDialogBox(ref input, "Ingresar Placa Tracto", "INGRESAR TRACTO", 300, 200) == DialogResult.OK)
                        {

                            DataTable dtPeso = new DataTable();
                            dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(5, IdProgramTracto, 0, input, anio);
                            rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                            string NroRPTA = rspta.Substring(0, 1);

                            if (NroRPTA == "0")
                            {
                                MessageBox.Show(rspta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarDatos();
                            }
                            else
                            {
                                MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }*/

                    //OPCION PARA AGREGAR PLANILLA DEL CONDUCTOR DESDE EL LISTADO DE PROGRMACIONES
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "PLANILLA" && !dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ATENDIDO"))
                    {
                        string input = "";
                        string rspta;
                        int IdProgramTracto = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        input = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                        if (ShowInputDialogBox(ref input, "Ingresar codigo de Planilla", "INGRESAR PLANILLA", 300, 200) == DialogResult.OK)
                        {
                            DataTable dtPeso = new DataTable();
                            dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(10, IdProgramTracto, 0, input, anio);
                            rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                            string NroRPTA = rspta.Substring(0, 1);

                            if (NroRPTA == "0")
                            {
                                MessageBox.Show(rspta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarDatos();
                            }
                            else { MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }

                    //OPCION PARA AGREGAR SEMIRREMOLUE A LA PROGRAMACION DESDE EL LISTADO   
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "SEMIRREMOLQUE" && !dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ATENDIDO"))
                    {
                        string input = "";
                        string rspta;
                        int IdProgramCarreta = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        input = dgvPreviajes.CurrentRow.Cells["SEMIRREMOLQUE"].Value.ToString();
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();

                        if (ShowInputDialogBox(ref input, "Ingresar Placa Semirremolque", "INGRESAR SEMIRREMOLQUE", 300, 200) == DialogResult.OK)
                        {
                            DataTable dtPeso = new DataTable();
                            dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(9, IdProgramCarreta, 0, input, anio);
                            rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                            string NroRPTA = rspta.Substring(0, 1);

                            if (NroRPTA == "0")
                            {
                                MessageBox.Show(rspta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarDatos();
                            }
                            else { MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }

                    //OPCION PARA AGREGAR TURNO SOLO PARA PROGRAMACIONES "LINDLEY" DESDE EL LISTADO
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "TURNO" && !dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ATENDIDO"))
                    {
                        string input = "";
                        string rspta;
                        int IdProgramTracto = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        input = dgvPreviajes.CurrentRow.Cells["TURNO"].Value.ToString();
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                        if (ShowInputDialogBox(ref input, "Ingresar Turno", "INGRESAR OBSERVACION", 300, 200) == DialogResult.OK)
                        {
                            DataTable dtPeso = new DataTable();
                            dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(6, IdProgramTracto, 0, input.ToUpper(), anio);
                            rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                            string NroRPTA = rspta.Substring(0, 1);

                            if (NroRPTA == "0") { MessageBox.Show(rspta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            else { MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }

                    //OPCION PARA AGREGAR VIATICOS DEL CONDUCTOR A LA PROGRAMACION DE SDE EL LISTADO 
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "VIATICOS" && !dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ATENDIDO"))
                    {
                        string input = "";
                        string rspta;
                        int IdProgramTracto = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        input = dgvPreviajes.CurrentRow.Cells["VIATICOS"].Value.ToString();
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                        if (ShowInputDialogBox(ref input, "Ingresar Gastos de Viaje", "INGRESAR OBSERVACION", 300, 200) == DialogResult.OK)
                        {
                            DataTable dtPeso = new DataTable();
                            dtPeso = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaPeso(7, IdProgramTracto, 0, input, anio);
                            rspta = Convert.ToString(dtPeso.Rows[0]["exito"]);
                            string NroRPTA = rspta.Substring(0, 1);

                            if (NroRPTA == "0")
                            {
                                MessageBox.Show(rspta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //CargarDatos();
                            }
                            else { MessageBox.Show(rspta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }

                    //oPCION PARA AGREGAR LAS GUIAS DE LA PROGRAMACION DESDE EL LISTADO
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "GUIAT." || this.dgvPreviajes.Columns[e.ColumnIndex].Name == "G/R")
                    {
                        //if (dgvPreviajes.CurrentRow.Cells["GUIAT."].Value.ToString().Length <= 1) // SE VALIDA SI YA TIENE GUIA INGRESADA NO PERMITA AGREGAR MAS
                        //{
                            int IdProgramacionGuia = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                            string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                            ProgramacionViajes.frmAgregarGuias frm = new ProgramacionViajes.frmAgregarGuias();
                            frm.envioviaje(IdProgramacionGuia, 1, anio);
                            frm.idCliente = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IdCliente"].Value.ToString());
                            frm.tipoprogramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTIPO"].Value.ToString());
                            frm.ot = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["OT"].Value.ToString());

                            DataTable dtDatos = cargarDatosPorDefectoSegunCliente();
                            if (dtDatos.Rows.Count > 0)
                            {
                                frm.DireccionPartida = dtDatos.Rows[0]["DireccionPartida"].ToString();
                                frm.DireccionDestino = dtDatos.Rows[0]["DireccionDestino"].ToString();
                            }

                            frm.ShowDialog(this);
                        //}
                    }

                    //OPCION PARA AGREGAR CONDUTOR DE APOYO A LA PROGRAMACION DE SDE LE LISTADO
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "CONDUCTOR_APOYO" && !dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ATENDIDO"))
                    {
                        int IdProgramacionGuia = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                        ProgramacionViajes.frmAgregarGuias frm = new ProgramacionViajes.frmAgregarGuias();
                        frm.envioviaje(IdProgramacionGuia, 2, anio);
                        frm.Text = "Asignar Conductor";
                        frm.ShowDialog(this);

                        // if (Val_Respuesta.Equals("1")) { CargarDatos(); }
                    }

                    //OPCION PARA AGREGAR LA FECHA DE DESCARGA
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "DESCARGA" && !dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ANULADO"))
                    {
                        int IdProgramTracto = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                        ProgramacionViajes.frmAgregarDescarga frm = new ProgramacionViajes.frmAgregarDescarga();
                        frm.envioviaje(IdProgramTracto, 2, anio);
                        frm.Text = "Asignar Conductor";
                        frm.ShowDialog(this);

                        if (Val_Respuesta.Equals("1"))
                        {
                            //CargarDatos();
                        }
                    }

                    //OPCION PARA AGREGAR LA FECHA DE DESCARGA
                    if (this.dgvPreviajes.Columns[e.ColumnIndex].Name == "FECHA_TERMINO" && !dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ANULADO"))
                    {

                        int IdProgramTracto = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                        string anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                        ProgramacionViajes.frmAgregarDescarga frm = new ProgramacionViajes.frmAgregarDescarga();
                        frm.dateTimePicker1.Text = dgvPreviajes.CurrentRow.Cells["FECHA_FIN"].Value.ToString();
                        frm.esFechaTermino = true;
                        frm.envioviaje(IdProgramTracto, 2, anio);
                        frm.Text = "Asignar Conductor";
                        frm.ShowDialog(this);

                        if (Val_Respuesta.Equals("1"))
                        {
                            //CargarDatos();
                        }
                    }
                                      
                }

                //SI EL CEHCKBOX DEL VISUALIZADOR WEB ESTA ACTIVO SE MUESTRA LA UBICAION DE LA UNIDAD EN TIEMPO REAL
                string dato = dgvPreviajes.CurrentCell.Value.ToString();
                if (chbUbicacion.Checked == true)
                {
                    string placa = dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString();
                    if (!placa.Equals(""))
                    {
                        DataTable DGPS = new DataTable();
                        DGPS = clsOperacionesBL.Instancia.GetOperaciones_ListarUbicacionGps(placa);
                        for (int i = 0; i < DGPS.Rows.Count; i++)
                        {
                            UBIDireccion = DGPS.Rows[i]["DIRECCION"].ToString();
                            UBIGps = DGPS.Rows[i]["GPS"].ToString();
                            textBox2.Text = UBIDireccion;
                            webView1.Navigate(UBIGps);
                        }
                    }
                    if (dgvPreviajes.Rows[e.RowIndex].Cells["TRACTO"].Value.ToString().Equals("") && !dgvPreviajes.Rows[e.RowIndex].Cells["UNIDADAPOYO"].Value.ToString().Equals(""))
                    {
                        DataTable DGPS = new DataTable();
                        DGPS = clsOperacionesBL.Instancia.GetOperaciones_ListarUbicacionGps(placa);
                        for (int i = 0; i < DGPS.Rows.Count; i++)
                        {
                            UBIDireccion = DGPS.Rows[i]["DIRECCION"].ToString();
                            UBIGps = DGPS.Rows[i]["GPS"].ToString();
                            textBox2.Text = UBIDireccion;
                            webView1.Navigate(UBIGps);
                        }
                    }
                }
                else
                {
                    webView1.Navigate("about:blank");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webView1.Navigate(textUrl.Text);
        }

        private void btnUnidades_Click(object sender, EventArgs e)
        {
            ProgramacionViajes.frmUnidadesBloqueadas frmUnidadesBlobueadas = new ProgramacionViajes.frmUnidadesBloqueadas();
            frmUnidadesBlobueadas.ShowDialog();           
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //LLENADO DEL COMBOBOX PARA LOS FILTROS DE LA PROGRAMACION 
            if (radioButton1.Checked == true)
            {
                DataTable dtEstadosPro = new DataTable();
                dtEstadosPro.Columns.Add("ID", typeof(int));
                dtEstadosPro.Columns.Add("ESTADO", typeof(string));
                dtEstadosPro.Rows.Add(1, "PROGRAMADO");
                dtEstadosPro.Rows.Add(2, "ATENDIDO");
                dtEstadosPro.Rows.Add(3, "ANULADO");
                dtEstadosPro.Rows.Add(4, "PEND. EN LLEGAR");
                dtEstadosPro.Rows.Add(5, "EN ESPERA");
                dtEstadosPro.Rows.Add(6, "CARGA EN BASE");
                dtEstadosPro.Rows.Add(7, "COLA IN");
                dtEstadosPro.Rows.Add(8, "COLA OUT");
                dtEstadosPro.Rows.Add(9, "CARGADO");
                dtEstadosPro.Rows.Add(10, "FUERA DE PLANTA");
                cboEstados.DisplayMember = "ESTADO";
                cboEstados.ValueMember = "ID";
                cboEstados.DataSource = dtEstadosPro;

                radioButton2.Checked = false;
                chbFiltroHabilita.Checked = false;
            }
            cboEstados.Enabled = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //LLENADO DEL COMBOBOX PARA LOS FILTROS DE LA PROGRAMACION 
            if (radioButton2.Checked == true)
            {
                DataTable dtEstadosVia = new DataTable();
                dtEstadosVia.Columns.Add("ID", typeof(int));
                dtEstadosVia.Columns.Add("ESTADO", typeof(string));
                dtEstadosVia.Rows.Add(1, "PROGRAMADO");
                dtEstadosVia.Rows.Add(2, "EJECUCION");
                dtEstadosVia.Rows.Add(3, "COMPLETADO");
                dtEstadosVia.Rows.Add(4, "CANCELADO");
                dtEstadosVia.Rows.Add(5, "ANULADO");

                cboEstados.DisplayMember = "ESTADO";
                cboEstados.ValueMember = "ID";
                cboEstados.DataSource = dtEstadosVia;

                radioButton1.Checked = false;
                chbFiltroHabilita.Checked = false;
            }
            cboEstados.Enabled = true;
        }       

        private void cboEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FILTRO DE ESTADOS PARA LOS CODIGO DE VIAJES Y PROGRAMACIONES
            try
            {
                if (radioButton1.Checked == true)
                {
                    string colFiltrar = "ESTADO";
                    string dato = cboEstados.Text.ToString();
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}'", colFiltrar, dato);
                }
                if (radioButton2.Checked == true)
                {
                    string colFiltrar = "ESTADOVIAJE";
                    string dato = cboEstados.Text.ToString();
                    ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}'", colFiltrar, dato);
                }
            }
            catch (Exception)
            {

            }
        }


        private void cboFiltrosVarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dgvPreviajes.DataSource == null)
                return;

            //Filtra los viajes consolidados
            if (cboFiltrosVarios.Text.Equals("CONSOLIDADOS"))
            {
                string colFiltrar = "VIAJE";
                ((DataTable)dgvPreviajes.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, "C");
            }

            ////Filtra primero los abastecimientos de llenos a vacios
            //if (cboFiltrosVarios.Text.Equals("↑↓ABASTACIO") && Tiporder == 1)
            //{                 
            //    dgvPreviajes.Sort(dgvPreviajes.Columns[5], ListSortDirection.Descending);
            //    Tiporder = 2;
            //}
            ////Filtra primero los abastecimientos de vacios a llenos
            //else
            //{
            //    dgvPreviajes.Sort(dgvPreviajes.Columns[5], ListSortDirection.Ascending);
            //    Tiporder = 1;
            //}
        }

        private void chbFiltroHabilita_CheckedChanged(object sender, EventArgs e)
        {
            if (chbFiltroHabilita.Checked == true)
            {
                cboFiltrosVarios.Enabled = true;
                textBox1.Enabled = true;
                radioButton1.Checked = false;
                radioButton1.Checked = false;
                chbFiltroEstados.Checked = false;
            }
            if (chbFiltroHabilita.Checked == false)
            {
                cboFiltrosVarios.Enabled = false;
                textBox1.Enabled = false;
                textBox1.Text = "";
            }
        }

        private void chbFiltroEstados_CheckedChanged(object sender, EventArgs e)
        {
            if (chbFiltroEstados.Checked == false)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                cboEstados.Enabled = false;
            }
            if (chbFiltroEstados.Checked == true)
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                chbFiltroHabilita.Checked = false;
            }
        }

        private void BtnConductoresBloqueados_Click(object sender, EventArgs e)
        {
            FrmConductoresBloqueados frmConductoresBloqueados = new FrmConductoresBloqueados();
            frmConductoresBloqueados.ShowDialog(this);
        }

        private void dgvPreviajes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter /*|| e.KeyChar == (char)Keys.Return)*/))
            {
                for (int i = 0; i < dtTipoProg.Rows.Count; i++)
                {
                    if (comboBox1.Text.ToString() == dtTipoProg.Rows[i]["Descripcion"].ToString())
                        var_Accesos = Convert.ToInt32(dtTipoProg.Rows[i]["ACCESOS"].ToString());
                }

                if (dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ANULADO") /*|| dgvPreviajes.Rows[e.RowIndex].Cells["ESTADO"].Value.ToString().Equals("ATENDIDO")*/)
                {
                    MessageBox.Show("Programación " + dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString() + ", No se puede modificar...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int idProgramacionActual = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                    var_IdProg = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                    var_TipoProg = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTIPO"].Value.ToString());
                    var_Sucursal = dgvPreviajes.CurrentRow.Cells["SUCURSAL"].Value.ToString();
                    var_Desctipo = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                    var_FechaProg = dgvPreviajes.CurrentRow.Cells["FECHAPROGR"].Value.ToString();
                    var_FechaInicio = dgvPreviajes.CurrentRow.Cells["FECHA_INICIO"].Value.ToString();
                    var_FechaFin = dgvPreviajes.CurrentRow.Cells["FECHA_FIN"].Value.ToString();
                    var_Tracto = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTRACTO"].Value.ToString());
                    var_DesTracto = dgvPreviajes.CurrentRow.Cells["TRACTO"].Value.ToString();
                    var_Remolque = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDREMOLQUE"].Value.ToString());
                    var_Desremolque = dgvPreviajes.CurrentRow.Cells["SEMIRREMOLQUE"].Value.ToString();
                    var_Conductor = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCONDUCTOR"].Value.ToString());
                    var_nomConductor = dgvPreviajes.CurrentRow.Cells["CONDUCTOR"].Value.ToString();
                    var_Apoyo = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDAPOYO"].Value.ToString());
                    var_nomApoyo = dgvPreviajes.CurrentRow.Cells["CONDUCTOR_APOYO"].Value.ToString();
                    var_Cliente = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCLIENTE"].Value.ToString());
                    var_nomCliente = dgvPreviajes.CurrentRow.Cells["CLIENTE"].Value.ToString();
                    var_nomRuta = dgvPreviajes.CurrentRow.Cells["RUTA"].Value.ToString();
                    var_nomProducto = dgvPreviajes.CurrentRow.Cells["PRODUCTO"].Value.ToString();
                    var_Ruta = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDRUTA"].Value.ToString());
                    var_Destino = dgvPreviajes.CurrentRow.Cells["DESTINO"].Value.ToString();
                    var_Producto = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPRODUCTO"].Value.ToString());
                    var_Estado = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDESTADO"].Value.ToString());
                    var_HoraSailda = dgvPreviajes.CurrentRow.Cells["SALIDA"].Value.ToString();
                    var_HoraLlegada = dgvPreviajes.CurrentRow.Cells["LLEGADA"].Value.ToString();
                    var_FechaDescarga = dgvPreviajes.CurrentRow.Cells["DESCARGA"].Value.ToString();
                    var_PesoAlmacen = Convert.ToDecimal(dgvPreviajes.CurrentRow.Cells["PESO"].Value.ToString());
                    var_PesoCliente = Convert.ToDecimal(dgvPreviajes.CurrentRow.Cells["PESOCLIENTE"].Value.ToString());
                    var_Merma = Convert.ToDecimal(dgvPreviajes.CurrentRow.Cells["MERMA"].Value.ToString());
                    var_Viaje = dgvPreviajes.CurrentRow.Cells["VIAJE"].Value.ToString();
                    var_idViaje = dgvPreviajes.CurrentRow.Cells["IDVIAJE"].Value.ToString();
                    var_Planilla = dgvPreviajes.CurrentRow.Cells["PLANILLA"].Value.ToString();
                    var_Observacion = dgvPreviajes.CurrentRow.Cells["OBSERVACION"].Value.ToString();
                    var_Serie = dgvPreviajes.CurrentRow.Cells["SERIE"].Value.ToString();
                    var_Numero = dgvPreviajes.CurrentRow.Cells["GUIA"].Value.ToString();
                    var_Remitente = dgvPreviajes.CurrentRow.Cells["G/R"].Value.ToString();
                    var_Zona = dgvPreviajes.CurrentRow.Cells["ZONA"].Value.ToString();
                    var_turno = dgvPreviajes.CurrentRow.Cells["TURNO"].Value.ToString();
                    var_ConducInicio = dgvPreviajes.CurrentRow.Cells["CONDUCTOR_INICIO"].Value.ToString();
                    var_PrimCambio = dgvPreviajes.CurrentRow.Cells["1° CAMBIO"].Value.ToString();
                    var_SegCambio = dgvPreviajes.CurrentRow.Cells["2° CAMBIO"].Value.ToString();
                    var_TerCambio = dgvPreviajes.CurrentRow.Cells["3° CAMBIO"].Value.ToString();
                    var_OT = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["OT"].Value.ToString());
                    var_Viaticos = Convert.ToDecimal(dgvPreviajes.CurrentRow.Cells["VIATICOS"].Value.ToString());
                    var_OrdenServicio = dgvPreviajes.CurrentRow.Cells["ORDENSERVICIO"].Value.ToString();
                    var_tipounidad = dgvPreviajes.CurrentRow.Cells["TIPOUNIDAD"].Value.ToString();
                    var_guiaEntrega = dgvPreviajes.CurrentRow.Cells["GUIAENTREGA"].Value.ToString();
                    var_UnidadApoyo = dgvPreviajes.CurrentRow.Cells["UNIDADAPOYO"].Value.ToString();
                    var_MontoOrden = Convert.ToDecimal(dgvPreviajes.CurrentRow.Cells["MONTORDEN"].Value.ToString());
                    var_TopoSemir = dgvPreviajes.CurrentRow.Cells["TIPOSEMIR"].Value.ToString();
                    var_Dia = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CANTIDADDIA"].Value.ToString());
                    int IDPROGRAMAEDIT = Convert.ToInt32(comboBox1.SelectedValue.ToString());
                    userModifica = dgvPreviajes.CurrentRow.Cells["USUARIOMOD"].Value.ToString();
                    fechaModifica = dgvPreviajes.CurrentRow.Cells["FECHAMOD"].Value.ToString();
                    var_usuarioCrea = dgvPreviajes.CurrentRow.Cells["UsuarioCrea"].Value.ToString();
                    var_UbigeoPartida = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["INTERNA"].Value.ToString());
                    var_UbigeoLlegada = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["UBIGEOPARTIDA"].Value.ToString());
                    var_Interna = dgvPreviajes.CurrentRow.Cells["UBIGEOLLEGADA"].Value.ToString();
                    VAR_anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                    var_Maquinaria = dgvPreviajes.CurrentRow.Cells["MAQUINARIA"].Value.ToString();
                    string PROGRAMACIONEDIT = comboBox1.Text.ToString();
                    var_CodiTolvas = dgvPreviajes.CurrentRow.Cells["ENLACE"].Value.ToString();
                    var_FechaRegistro = dgvPreviajes.CurrentRow.Cells["FECHAREGISTRO"].Value.ToString();
                    var_MontoPlanila = dgvPreviajes.CurrentRow.Cells["ENTREGADO"].Value.ToString();
                    crea1_modifi2_anula3 = 2;
                    frmNuevoPreviaje_Programacion frm2 = new frmNuevoPreviaje_Programacion();
                    frm2.setearvariable(crea1_modifi2_anula3, IDPROGRAMAEDIT, PROGRAMACIONEDIT, var_IdProg, var_Sucursal, var_TipoProg, var_Desctipo, var_FechaProg, var_FechaInicio, var_FechaFin,
                                        var_Tracto, var_DesTracto, var_Remolque, var_Desremolque, var_Conductor, var_Apoyo, var_Cliente, var_nomConductor, var_nomApoyo, var_nomCliente,
                                        var_nomProducto, var_nomRuta, var_Ruta, var_Destino, var_Producto, var_Estado, var_HoraSailda, var_HoraLlegada, var_FechaDescarga, var_PesoAlmacen, var_PesoCliente,
                                        var_Merma, var_Viaje,var_idViaje, var_Planilla, var_Observacion, var_Serie, var_Numero, var_Remitente, var_Zona, var_turno, var_ConducInicio, var_PrimCambio,
                                        var_SegCambio, var_TerCambio, var_Accesos, var_OT, var_Viaticos, var_OrdenServicio, var_tipounidad, var_UnidadApoyo, var_guiaEntrega,
                                        var_MontoOrden, var_TopoSemir, var_Dia, userModifica, fechaModifica,var_usuarioCrea, var_Interna, var_UbigeoPartida, var_UbigeoLlegada, VAR_anio,
                                        var_Maquinaria, var_CodiTolvas, var_FechaRegistro, var_MontoPlanila,"","");
                    frm2.ShowDialog(this);

                    if (Val_Respuesta.Equals("1"))
                    {
                        //CargarDatos();
                    }
                }
            }
        }

        int cnt;
        private void timer1_Tick(object sender, EventArgs e)
        {
            //PARPADEO DE LA NOTIFICACION DE ALERTAS PARA PROGRAMACIONES POR DAR ATENDIDO
            if (cnt == 0)
            {
                label2.Visible = true;
            }

            cnt += 1;
            if (cnt == 4)
            {
                label2.Visible = false;
                cnt = 0;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (checkBox1.Checked == true)
                {
                    btnPorcentaje.Visible = false;
                    btnSinchocope.Visible = true;

                    if (resultSinChocope < 30)
                    {
                        btnSinchocope.BackColor = Color.Red;
                    }
                    else if (resultSinChocope < 60 && resultSinChocope >= 30)
                    {
                        btnSinchocope.BackColor = Color.Yellow;
                    }
                    else if (resultSinChocope > 60)
                    {
                        btnSinchocope.BackColor = Color.Lime;
                    }
                }
                else
                {
                    btnPorcentaje.Visible = true;
                    btnSinchocope.Visible = false;
                }
            }
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frmProgramacionGeneral frmConductoresBloqueados = new frmProgramacionGeneral();
            frmConductoresBloqueados.ShowDialog(this);
        }

        private void button6_Click(object sender, EventArgs e)
        {            
            ProgramacionViajes.frmImportarProgramaciones frmImporGuias = new ProgramacionViajes.frmImportarProgramaciones();

            frmImporGuias.ShowDialog(this);
            if (Val_Respuesta.Equals("1"))
            {
                CargarDatos();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //SELECCIONAR LAS COLUMNAS A MOSTRAR POR USUARIO
            ProgramacionViajes.frmSeleccionarColumnasProgrmacion frmColpro = new ProgramacionViajes.frmSeleccionarColumnasProgrmacion();
            frmColpro.envioVariables(Utilitario.Instancia.SesionUsuario.usuario);
            frmColpro.ShowDialog(this);

            if (Val_frmSeleccionados.Equals("1"))
            {
                CargarDatos();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtRendProm = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_CalcularRendProm(comboBox1.Text);

            if (comboBox1.Text.ToString().Equals("TOLVAS"))
            {
                lblPrimTurno.Text = " ";
                lblSegTurno.Text = "";
                lblTercTurno.Text = "";
                lblAdelanto.Text = "";
                btnPorcentaje.Visible = false;
                btnSinchocope.Visible = false;
                checkBox1.Visible = false;
                button6.Visible = false;
                dgvPreviajes.DataSource = null;
                btnImpresMasiva.Visible = false;
                opcionTolvasToolStripMenuItem.Visible = true;
                confOrdenDestinosToolStripMenuItem.Visible = false;
                chbOrdenDestino.Visible = false;
            }
            if (comboBox1.Text.ToString().Equals("LIMAGAS"))
            {
                lblPrimTurno.Text = " ";
                lblSegTurno.Text = "";
                lblTercTurno.Text = "";
                lblAdelanto.Text = "";
                btnPorcentaje.Visible = false;
                btnSinchocope.Visible = false;
                checkBox1.Visible = false;
                button6.Visible = false;
                dgvPreviajes.DataSource = null;
                btnImpresMasiva.Visible = false;
                opcionTolvasToolStripMenuItem.Visible = false;
                confOrdenDestinosToolStripMenuItem.Visible = false;
                chbOrdenDestino.Visible = false;
            }
            if (comboBox1.Text.ToString().Equals("LINDLEY"))
            {               
                btnPorcentaje.Visible = true;
                checkBox1.Visible = true;
                button6.Visible = false;
                dgvPreviajes.DataSource = null;
                btnImpresMasiva.Visible = false;
                opcionTolvasToolStripMenuItem.Visible = false;
                confOrdenDestinosToolStripMenuItem.Visible = true;
                chbOrdenDestino.Visible = true;
            }

            if (comboBox1.Text.ToString().Equals("VOLCAN"))
            {
                btnPorcentaje.Visible = true;
                checkBox1.Visible = true;
                button6.Visible = false;
                dgvPreviajes.DataSource = null;
                btnImpresMasiva.Visible = false;
                opcionTolvasToolStripMenuItem.Visible = false;
                confOrdenDestinosToolStripMenuItem.Visible = true;
                chbOrdenDestino.Visible = true;
            }

            if (comboBox1.Text.ToString().Equals("TODO"))
            {
                lblPrimTurno.Text = " ";
                lblSegTurno.Text = "";
                lblTercTurno.Text = "";
                lblAdelanto.Text = "";
                btnPorcentaje.Visible = false;
                btnSinchocope.Visible = false;
                checkBox1.Visible = false;
                button6.Visible = false;
                dgvPreviajes.DataSource = null;
                btnImpresMasiva.Visible = false;
                opcionTolvasToolStripMenuItem.Visible = false;
                confOrdenDestinosToolStripMenuItem.Visible = false;
                chbOrdenDestino.Visible = false;
            }
            if (comboBox1.Text.ToString().Equals("GENERAL"))
            {
                lblPrimTurno.Text = " ";
                lblSegTurno.Text = "";
                lblTercTurno.Text = "";
                lblAdelanto.Text = "";
                btnPorcentaje.Visible = false;
                btnSinchocope.Visible = false;
                checkBox1.Visible = false;
                button6.Visible = false;
                dgvPreviajes.DataSource = null;
                btnImpresMasiva.Visible = false;
                opcionTolvasToolStripMenuItem.Visible = false;
                confOrdenDestinosToolStripMenuItem.Visible = false;
                chbOrdenDestino.Visible = false;
            }
            if (comboBox1.Text.ToString().Equals("MTO"))
            {
                lblPrimTurno.Text = " ";
                lblSegTurno.Text = "";
                lblTercTurno.Text = "";
                lblAdelanto.Text = "";
                btnPorcentaje.Visible = false;
                btnSinchocope.Visible = false;
                checkBox1.Visible = false;
                button6.Visible = false;
                dgvPreviajes.DataSource = null;
                btnImpresMasiva.Visible = false;
                opcionTolvasToolStripMenuItem.Visible = false;
                confOrdenDestinosToolStripMenuItem.Visible = false;
                chbOrdenDestino.Visible = false;
            }
            if (comboBox1.Text.ToString().Equals("COMBUSTIBLE"))
            {
                lblPrimTurno.Text = " ";
                lblSegTurno.Text = "";
                lblTercTurno.Text = "";
                lblAdelanto.Text = "";
                btnPorcentaje.Visible = false;
                btnSinchocope.Visible = false;
                checkBox1.Visible = false;
                button6.Visible = false;
                dgvPreviajes.DataSource = null;
                btnImpresMasiva.Visible = false;
                opcionTolvasToolStripMenuItem.Visible = false;
                confOrdenDestinosToolStripMenuItem.Visible = false;
                chbOrdenDestino.Visible = false;
            }
            if (comboBox1.Text.ToString().Equals("LOCAL"))
            {
                lblPrimTurno.Text = " ";
                lblSegTurno.Text = "";
                lblTercTurno.Text = "";
                lblAdelanto.Text = "";
                btnPorcentaje.Visible = false;
                btnSinchocope.Visible = false;
                checkBox1.Visible = false;
                button6.Visible = false;
                dgvPreviajes.DataSource = null;
                btnImpresMasiva.Visible = false;
                opcionTolvasToolStripMenuItem.Visible = false;
                confOrdenDestinosToolStripMenuItem.Visible = false;
                chbOrdenDestino.Visible = false;
            }
            if (comboBox1.Text.ToString().Equals("MAQ/CAMIONETAS"))
            {
                lblPrimTurno.Text = " ";
                lblSegTurno.Text = "";
                lblTercTurno.Text = "";
                lblAdelanto.Text = "";
                btnPorcentaje.Visible = false;
                btnSinchocope.Visible = false;
                checkBox1.Visible = false;
                button6.Visible = false;
                dgvPreviajes.DataSource = null;
                btnImpresMasiva.Visible = false;
                opcionTolvasToolStripMenuItem.Visible = false;
                confOrdenDestinosToolStripMenuItem.Visible = false;
                chbOrdenDestino.Visible = false;
            }

            txtOperacionR.Text = comboBox1.Text;
            if (dtRendProm.Rows.Count > 0) { txtRendProm.Text = dtRendProm.Rows[0]["REND KM/GL"].ToString(); }
            else { txtRendProm.Text = "-"; }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            groupBox3.Visible = false;
            dgvViajesAltra.Visible = false;
            button4.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //actualizacion para impresiones masivas programacion TOLVAS
            int cont = 0;

            //Revisamos si el usuario tiene asignado una ticketera
            DataTable dtConsultarImpresora = new DataTable();
            dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);
            NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

            if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
            {
                MessageBox.Show("No tiene Impresora asignada");
                return;
            }

            //RECORREMOS EL DATATABLE PARA REVISAR CUANTAS PROGRAMACIONES TIENE
            if (dtListaProgramaciones.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaProgramaciones.Rows.Count; i++)
                {
                    _codigo = dtListaProgramaciones.Rows[i]["CODIGO"].ToString();
                    _sucursal = dtListaProgramaciones.Rows[i]["SUCURSAL"].ToString();
                    _tipoprogramacion = dtListaProgramaciones.Rows[i]["TIPO"].ToString();
                    _fechaprogramacion = dtListaProgramaciones.Rows[i]["FECHAPROGR"].ToString();
                    _placa = dtListaProgramaciones.Rows[i]["TRACTO"].ToString();
                    _ruta = dtListaProgramaciones.Rows[i]["RUTA"].ToString();
                    _cliente = dtListaProgramaciones.Rows[i]["CLIENTE"].ToString();
                    _conductor = dtListaProgramaciones.Rows[i]["CONDUCTOR"].ToString();
                    _producto = dtListaProgramaciones.Rows[i]["PRODUCTO"].ToString();
                    _programador = Utilitario.Instancia.SesionUsuario.usuario;
                    _dni = dtListaProgramaciones.Rows[i]["DNI"].ToString();

                    DataTable dtImpresoS = new DataTable();
                    dtImpresoS = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeImpreso(_codigo);

                    if (dtImpresoS.Rows.Count > 0)
                    {
                        // MessageBox.Show("La programación: " +_codigo+" ya fue impresa, Para reimprimir comunicarse con su Supervisor");
                        // return;
                        string rspta = Convert.ToString(dtImpresoS.Rows[0]["IMPRESO"]);

                        if (rspta == "1")
                        {
                            MessageBox.Show("La programación ya fue impresa, Para reimprimir comunicarse con su Supervisor");
                            return;
                        }
                        else
                        {
                            cont = cont + 1;

                            Imprimir();

                            string RPTA;
                            DataTable dtImpreso = new DataTable();
                            dtImpreso = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ActualizarImpresion(_codigo);

                            RPTA = Convert.ToString(dtImpreso.Rows[0]["exito"]);
                            string val = RPTA.Substring(0, 1);
                        }
                    }
                }

                MessageBox.Show(cont.ToString() + " Ticket(s) Impreso(s)....!");
            }

        }

        private void Imprimir()
        {
            //AGREGANDO DATOS AL TICKET
            Ticket ticket = new Ticket();
            ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
            ticket.AddSubHeaderLine2("CODIGO: " + _codigo + "                         ");
            ticket.AddSubHeaderLine("SEDE:" + _sucursal + "  PLACA: " + _placa);
            ticket.AddSubHeaderLine("FECHAPROG: " + _fechaprogramacion);
            ticket.AddSubHeaderLine("DNI: " + _dni + "                         ");
            ticket.AddSubHeaderLine("CHOFER: " + _conductor);
            ticket.AddSubHeaderLine("RUTA: " + _ruta);
            ticket.AddSubHeaderLine("PROGRAMADOR: " + _programador + "                   ");
            ticket.AddSubHeaderLine("PROGRAMACION: " + _tipoprogramacion);
            ticket.AddSubHeaderLine("Fecha Ticket:" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            ticket.AddFooterLine("    ** VIAJA CON CUIDADO **");
            ticket.PrintTicket(NombreImpresora);
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (dgvPreviajes.DataSource == null)
            {
                MessageBox.Show("Debe Seleccionar una Programacion", "ALERTA");
                return;

            }
            if (dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ANULADO"))
            {

                MessageBox.Show("Programación " + dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString() + ", No se peude modificar...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {

                int pos = Convert.ToInt32(dgvPreviajes.CurrentRow.Index.ToString());
                if (dgvPreviajes.CurrentRow.Selected == true)
                {
                    string codigo = dgvPreviajes.Rows[pos].Cells["CODIGO"].Value.ToString();
                    string conductor = dgvPreviajes.Rows[pos].Cells["CONDUCTOR"].Value.ToString();
                    string placa = dgvPreviajes.Rows[pos].Cells["TRACTO"].Value.ToString();
                    string var_TopoSemir = dgvPreviajes.Rows[pos].Cells["TIPOSEMIR"].Value.ToString();
                    string ruta = dgvPreviajes.Rows[pos].Cells["RUTA"].Value.ToString();
                    string programacion = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                    string anio = VAR_anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                    int idTipoProg = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTIPO"].Value.ToString());
                    var_nomProducto = dgvPreviajes.CurrentRow.Cells["PRODUCTO"].Value.ToString();
                    string idproducto = dgvPreviajes.CurrentRow.Cells["IDPRODUCTO"].Value.ToString();
                    var_IdProg = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                    string OT = dgvPreviajes.CurrentRow.Cells["OT"].Value.ToString();
                    int idcliente = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCLIENTE"].Value.ToString());
                    string cliente = dgvPreviajes.CurrentRow.Cells["CLIENTE"].Value.ToString();


                    ////////////CONDICION PARA QUE NO REGISTRE CUANDO LA OT ESTE EN 0 O NULL///////////////////////////

                    if (Convert.ToInt32(OT) > 0)
                    {
                        ProgramacionViajes.frmRegistrarTracking frm = new ProgramacionViajes.frmRegistrarTracking();
                        frm.RegistrarTicket(1, codigo, conductor, placa, var_TopoSemir, ruta, programacion, anio, idTipoProg, var_nomProducto, idproducto, var_IdProg, OT, idcliente);
                        frm.ShowDialog();
                    }

                    else
                    {

                        MessageBox.Show("el codigo previaje: " + codigo + "  seleccionado no contiene OT", "ALERTA");

                    }

                }
            }
        }

        private void trackingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void registrarTrackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
           /* if (dgvPreviajes.DataSource == null)
            {
                MessageBox.Show("Debe Seleccionar una Programacion", "ALERTA");
                return;
            }
            if (dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString().Equals("ANULADO"))
            {

                MessageBox.Show("Programación " + dgvPreviajes.CurrentRow.Cells["ESTADO"].Value.ToString() + ", No se peude modificar...!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                int pos = Convert.ToInt32(dgvPreviajes.CurrentRow.Index.ToString());
                if (dgvPreviajes.CurrentRow.Selected == true)
                {
                    string codigo = dgvPreviajes.Rows[pos].Cells["CODIGO"].Value.ToString();
                    string conductor = dgvPreviajes.Rows[pos].Cells["CONDUCTOR"].Value.ToString();
                    string placa = dgvPreviajes.Rows[pos].Cells["TRACTO"].Value.ToString();
                    string var_TopoSemir = dgvPreviajes.Rows[pos].Cells["TIPOSEMIR"].Value.ToString();
                    string ruta = dgvPreviajes.Rows[pos].Cells["RUTA"].Value.ToString();
                    string programacion = dgvPreviajes.CurrentRow.Cells["TIPO"].Value.ToString();
                    string anio = VAR_anio = dgvPreviajes.CurrentRow.Cells["ANIO"].Value.ToString();
                    int idTipoProg = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDTIPO"].Value.ToString());
                    var_nomProducto = dgvPreviajes.CurrentRow.Cells["PRODUCTO"].Value.ToString();
                    string idproducto = dgvPreviajes.CurrentRow.Cells["IDPRODUCTO"].Value.ToString();
                    var_IdProg = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDPROGRAMACION"].Value.ToString());
                    string OT = dgvPreviajes.CurrentRow.Cells["OT"].Value.ToString();
                    int idcliente = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCLIENTE"].Value.ToString());
                    string cliente = dgvPreviajes.CurrentRow.Cells["CLIENTE"].Value.ToString();

                    ////////////CONDICION PARA QUE NO REGISTRE CUANDO LA OT ESTE EN 0 O NULL///////////////////////////
                    if (Convert.ToInt32(OT) > 0)
                    {
                        ProgramacionViajes.frmRegistrarTracking frm = new ProgramacionViajes.frmRegistrarTracking();
                        frm.RegistrarTicket(1, codigo, conductor, placa, var_TopoSemir, ruta, programacion, anio, idTipoProg, var_nomProducto, idproducto, var_IdProg, OT, idcliente);
                        frm.ShowDialog();
                    }

                    else
                    {
                        MessageBox.Show("el codigo previaje: " + codigo + "  seleccionado no contiene OT", "ALERTA");
                    }
                }
            }*/
        }

        private void importarDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramacionViajes.frmImportarProgramaciones frmImporGuias = new ProgramacionViajes.frmImportarProgramaciones();

            frmImporGuias.ShowDialog(this);
            if (Val_Respuesta.Equals("1"))
            {
                //CargarDatos();
            }
        }

        private void impresionMasivaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int cont = 0;

            //Revisamos si el usuario tiene asignado una ticketera
            DataTable dtConsultarImpresora = new DataTable();
            dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);
            NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

            if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
            {
                MessageBox.Show("No tiene Impresora asignada");
                return;
            }

            //RECORREMOS EL DATATABLE PARA REVISAR CUANTAS PROGRAMACIONES TIENE
            if (dtListaProgramaciones.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaProgramaciones.Rows.Count; i++)
                {
                    _codigo = dtListaProgramaciones.Rows[i]["CODIGO"].ToString();
                    _sucursal = dtListaProgramaciones.Rows[i]["SUCURSAL"].ToString();
                    _tipoprogramacion = dtListaProgramaciones.Rows[i]["TIPO"].ToString();
                    _fechaprogramacion = dtListaProgramaciones.Rows[i]["FECHAPROGR"].ToString();
                    _placa = dtListaProgramaciones.Rows[i]["TRACTO"].ToString();
                    _ruta = dtListaProgramaciones.Rows[i]["RUTA"].ToString();
                    _cliente = dtListaProgramaciones.Rows[i]["CLIENTE"].ToString();
                    _conductor = dtListaProgramaciones.Rows[i]["CONDUCTOR"].ToString();
                    _producto = dtListaProgramaciones.Rows[i]["PRODUCTO"].ToString();
                    _programador = Utilitario.Instancia.SesionUsuario.usuario;
                    _dni = dtListaProgramaciones.Rows[i]["DNI"].ToString();

                    DataTable dtImpresoS = new DataTable();
                    dtImpresoS = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeImpreso(_codigo);

                    if (dtImpresoS.Rows.Count > 0)
                    {
                        // MessageBox.Show("La programación: " +_codigo+" ya fue impresa, Para reimprimir comunicarse con su Supervisor");
                        // return;
                        string rspta = Convert.ToString(dtImpresoS.Rows[0]["IMPRESO"]);

                        if (rspta == "1")
                        {
                            //MessageBox.Show("La programación ya fue impresa, Para reimprimir comunicarse con su Supervisor");
                            //return;
                        }
                        else
                        {
                            cont = cont + 1;

                            Imprimir();

                            string RPTA;
                            DataTable dtImpreso = new DataTable();
                            dtImpreso = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ActualizarImpresion(_codigo);

                            RPTA = Convert.ToString(dtImpreso.Rows[0]["exito"]);
                            string val = RPTA.Substring(0, 1);
                        }
                    }
                }

                MessageBox.Show(cont.ToString() + " Ticket(s) Impreso(s)....!");
            }
        }

        private void configurarColumnasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SELECCIONAR LAS COLUMNAS A MOSTRAR POR USUARIO
            ProgramacionViajes.frmSeleccionarColumnasProgrmacion frmColpro = new ProgramacionViajes.frmSeleccionarColumnasProgrmacion();
            frmColpro.envioVariables(Utilitario.Instancia.SesionUsuario.usuario);
            frmColpro.ShowDialog(this);

            if (Val_frmSeleccionados.Equals("1"))
            {
                CargarDatos();
            }
        }

        private void unidadesDisponiblesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProgramacionViajes.frmUnidadesBloqueadas frmUnidadesBlobueadas = new ProgramacionViajes.frmUnidadesBloqueadas();
            frmUnidadesBlobueadas.ShowDialog();
        }

        private void verPlanillasPendientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TicketsGasto.frmVerPlanillasPendientes frmVerPlanillasPendientes = new TicketsGasto.frmVerPlanillasPendientes();
            frmVerPlanillasPendientes.ShowDialog();
        }

        private void conductoresBloqueadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConductoresBloqueados frmConductoresBloqueados = new FrmConductoresBloqueados();
            frmConductoresBloqueados.ShowDialog(this);
        }

        private void exportarTrackingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int IdProgramacionReporte = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            gcExcel.DataSource = null;
            gvexcel.Columns.Clear();
            DataTable dtReporte = new DataTable();
            dtReporte = clsOperacionesBL.Instancia.GetOperaciones_ListarReporteTracking(IdProgramacionReporte, dtpFechaProInicio.Text + " 00:00:00.000", dtpFechProFin.Text + " 23:59:59.999");

            if (dtReporte.Rows.Count > 0)
            {                
                // Creamos un objeto Excel.
                Microsoft.Office.Interop.Excel.Application Mi_Excel = default(Microsoft.Office.Interop.Excel.Application);
                // Creamos un objeto WorkBook. Para crear el documento Excel.           
                Microsoft.Office.Interop.Excel.Workbook LibroExcel = default(Microsoft.Office.Interop.Excel.Workbook);
                // Creamos un objeto WorkSheet. Para crear la hoja del documento.
                Microsoft.Office.Interop.Excel.Worksheet HojaExcel = default(Microsoft.Office.Interop.Excel.Worksheet);
                // Iniciamos una instancia a Excel, y Hacemos visibles para ver como se va creando el reporte, 
                // podemos hacerlo visible al final si se desea.
                Mi_Excel = new Microsoft.Office.Interop.Excel.Application();
                Mi_Excel.Visible = true;
                /* Ahora creamos un nuevo documento y seleccionamos la primera hoja del 
                 * documento en la cual crearemos nuestro informe.*/                  
                // Creamos una instancia del Workbooks de excel.            
                LibroExcel = Mi_Excel.Workbooks.Add();
                // Creamos una instancia de la primera hoja de trabajo de excel            
                HojaExcel = LibroExcel.Worksheets[1];
                HojaExcel.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible;
                // Hacemos esta hoja la visible en pantalla 
                // (como seleccionamos la primera esto no es necesario
                // si seleccionamos una diferente a la primera si lo
                // necesitariamos).
                HojaExcel.Activate();
                // La primera línea une las celdas y las convierte un en una sola.            
                HojaExcel.Range["A1:C1"].Merge();
                // La segunda línea Asigna el nombre del encabezado.
                HojaExcel.Range["A1:C1"].Value = "GRUPO TRANSPESA";
                HojaExcel.Range["A1:C1"].Font.Italic = true;
                HojaExcel.Range["A1:C1"].Font.Size = 20;
                HojaExcel.Range["A1:C1"].Font.Bold = true;
                HojaExcel.Range["A1:C1"].Font.Color = Color.White;
                HojaExcel.Range["A1:C1"].Interior.Color = Color.Red;
                HojaExcel.Range["D1:F1"].Merge();
                HojaExcel.Range["D1:F1"].Value = "PROGRAMACION: " + comboBox1.Text.ToString();
                HojaExcel.Range["D1:F1"].Font.Italic = true;
                HojaExcel.Range["D1:F1"].Font.Size = 20;
                HojaExcel.Range["D1:F1"].Font.Bold = true;
                HojaExcel.Range["D1:F1"].Font.Color = Color.White;
                HojaExcel.Range["D1:F1"].Interior.Color = Color.Red;                
                HojaExcel.Range["G1:V1"].Merge();
                HojaExcel.Range["G1:V1"].Interior.Color = Color.White;
                // Crear el subencabezado de nuestro informe
                HojaExcel.Range["A2:V2"].Merge();
                HojaExcel.Range["A2:V2"].Interior.Color = Color.White;
               // HojaExcel.Range["D3:D500"].FormatConditions.AddDatabar();
                Microsoft.Office.Interop.Excel.Range objCelda = HojaExcel.Range["A3", Type.Missing];
                objCelda.Value = "Fecha";
                objCelda.Font.Color = Color.Orange;
                objCelda.Borders.Color = Color.Black;
                //objCelda.Interior.Color = Color.Black;
                objCelda.Font.Bold = true;
                objCelda.VerticalAlignment = VertAlignment.Center;
                objCelda.HorizontalAlignment = HorizontalAlignment.Center;

                objCelda = HojaExcel.Range["B3", Type.Missing];
                objCelda.Value = "Ruta";
                objCelda.Font.Color = Color.Orange;
                objCelda.Borders.Color = Color.Black;
                objCelda.Font.Bold = true;
                objCelda = HojaExcel.Range["C3", Type.Missing];
                objCelda.Value = "Tracto";
                objCelda.Font.Color = Color.Orange;
                objCelda.Borders.Color = Color.Black;
                objCelda.Font.Bold = true;
                objCelda = HojaExcel.Range["D3", Type.Missing];
                objCelda.Value = "Conductor";
                objCelda.Font.Color = Color.Orange;
                objCelda.Borders.Color = Color.Black;
                objCelda.Font.Bold = true;
                objCelda = HojaExcel.Range["E3", Type.Missing];
                objCelda.Value = "Carreta";
                objCelda.Font.Color = Color.Orange;
                objCelda.Borders.Color = Color.Black;
                objCelda.Font.Bold = true;
                objCelda = HojaExcel.Range["F3", Type.Missing];
                objCelda.Value = "Producto";
                objCelda.Font.Color = Color.Orange;
                objCelda.Borders.Color = Color.Black;
                objCelda.Font.Bold = true;

                string datos = "G3H3I3J3K3L3M3N3O3P3Q3R3S3T3U3V3W3X3Y3";
                int i = 4;
                //definir la operacion----
                if (comboBox1.Text.Equals("LINDLEY") || comboBox1.Text.Equals("VOLCAN"))
                {                    
                    if (dtReporte.Rows.Count > 0)
                    {
                        //string VBLA = dtReporte.Columns[6].ColumnName.ToString();
                        objCelda = HojaExcel.Range[datos.Substring(0, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[6].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(2, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[7].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(4, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[8].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(6, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[9].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(8, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[10].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(10, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[11].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Interior.Color = Color.FromArgb(65, 125, 193);
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(12, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[12].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(14, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[13].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.FromArgb(87, 166, 57);
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(16, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[14].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.FromArgb(87, 166, 57);
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(18, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[15].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.FromArgb(87, 166, 57);
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(20, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[16].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(22, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[17].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.Red;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(24, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[18].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.Red;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(26, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[19].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.Red;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(28, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[20].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.Red;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(30, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[21].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.Red;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                    }
                    
                    for (int j = 0; j < dtReporte.Rows.Count; j++)
                    {
                        // Asignar los valores de los registros a las celdas
                        HojaExcel.Cells[i, "A"] = dtReporte.Rows[j]["Fecha"].ToString();
                        HojaExcel.Cells[i, "A"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "B"] = dtReporte.Rows[j]["Ruta"].ToString();
                        HojaExcel.Cells[i, "B"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "C"] = dtReporte.Rows[j]["Tracto"].ToString();
                        HojaExcel.Cells[i, "C"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "D"] = dtReporte.Rows[j]["Conductor"].ToString();
                        HojaExcel.Cells[i, "D"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "E"] = dtReporte.Rows[j]["Carreta"].ToString();
                        HojaExcel.Cells[i, "E"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "F"] = dtReporte.Rows[j]["Producto"].ToString();
                        HojaExcel.Cells[i, "F"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "G"] = dtReporte.Rows[j]["Llegada a planta"].ToString();
                        HojaExcel.Cells[i, "G"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "H"] = dtReporte.Rows[j]["Ingreso a Planta"].ToString();
                        HojaExcel.Cells[i, "H"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "I"] = dtReporte.Rows[j]["Inicio de atencion"].ToString();
                        HojaExcel.Cells[i, "I"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "J"] = dtReporte.Rows[j]["Fin de atencion"].ToString();
                        HojaExcel.Cells[i, "J"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "K"] = dtReporte.Rows[j]["Entrega de guias"].ToString();
                        HojaExcel.Cells[i, "K"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "L"] = dtReporte.Rows[j]["Salida de planta"].ToString();
                        HojaExcel.Cells[i, "L"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "M"] = dtReporte.Rows[j]["Salida a  ruta"].ToString();
                        HojaExcel.Cells[i, "M"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "N"] = dtReporte.Rows[j]["Llegaba al CDA"].ToString();
                        HojaExcel.Cells[i, "N"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "O"] = dtReporte.Rows[j]["Inicio Descarga"].ToString();
                        HojaExcel.Cells[i, "O"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "P"] = dtReporte.Rows[j]["Fin Descarga"].ToString();
                        HojaExcel.Cells[i, "P"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "Q"] = dtReporte.Rows[j]["Llegada a Base"].ToString();
                        HojaExcel.Cells[i, "Q"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "R"] = dtReporte.Rows[j]["Ubicacion"].ToString();
                        HojaExcel.Cells[i, "R"].Font.Color = Color.Blue;
                        HojaExcel.Cells[i, "R"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "S"] = dtReporte.Rows[j]["%Transito"].ToString();
                        HojaExcel.Cells[i, "S"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "S"].FormatConditions.AddDatabar();
                        HojaExcel.Cells[i, "T"] = dtReporte.Rows[j]["Estado"].ToString();
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Cargado Base"))
                        {
                            HojaExcel.Cells[i, "T"].Font.Color = Color.Red;
                            objCelda.Interior.Color = Color.Red;
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Cargado"))
                        {
                            HojaExcel.Cells[i, "T"].Font.Color = Color.Red;
                            objCelda.Interior.Color = Color.Red;
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Descargando"))
                        {
                            HojaExcel.Cells[i, "T"].Font.Color = Color.Red;
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("En transito"))
                        {
                            HojaExcel.Cells[i, "T"].Font.Color = Color.FromArgb(87, 166, 57);
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Espera descarga"))
                        {
                            HojaExcel.Cells[i, "T"].Font.Color = Color.Lime;
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Finalizado"))
                        {
                            HojaExcel.Cells[i, "T"].Font.Color = Color.FromArgb(65, 125, 193);
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Retorno"))
                        {
                            HojaExcel.Cells[i, "T"].Font.Color = Color.Blue;
                        }
                        // Opciones
                        HojaExcel.Cells[i, "U"] = dtReporte.Rows[j]["Observaciones"].ToString();
                        HojaExcel.Cells[i, "U"].Borders.Color = Color.Black;
                        // Valor de la Respuesta
                        HojaExcel.Cells[i, "V"] = dtReporte.Rows[j]["Tracto_Apoyo"].ToString();
                        HojaExcel.Cells[i, "V"].Borders.Color = Color.Black;
                        i++;
                    }
                }
                else if (comboBox1.Text.Equals("LIMAGAS") || comboBox1.Text.Equals("SOLGAS") || comboBox1.Text.Equals("SOLGAS GNL"))
                {
                    if (dtReporte.Rows.Count > 0)
                    {                        
                        objCelda = HojaExcel.Range[datos.Substring(0, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[6].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(2, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[7].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(4, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[8].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(6, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[9].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;                       
                        objCelda = HojaExcel.Range[datos.Substring(8, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[10].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.Red; //UBICACION
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(10, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[11].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.Red; //STATUS
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(12, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[12].ColumnName.ToString();
                        objCelda.Font.Color = Color.FromArgb(87, 166, 57);
                        objCelda.Interior.Color = Color.LightYellow; //PLANTA
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(14, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[13].ColumnName.ToString();
                        objCelda.Font.Color = Color.FromArgb(87, 166, 57);
                        objCelda.Interior.Color = Color.LightYellow;    //PUNTODESCARGA
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(16, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[14].ColumnName.ToString();
                        objCelda.Font.Color = Color.Black;
                       // objCelda.Interior.Color = Color.Red;    //TRACTOAPOYO
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                    }

                    for (int j = 0; j < dtReporte.Rows.Count; j++)
                    {
                        // Asignar los valores de los registros a las celdas
                        HojaExcel.Cells[i, "A"] = dtReporte.Rows[j]["Fecha"].ToString();
                        HojaExcel.Cells[i, "A"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "B"] = dtReporte.Rows[j]["Ruta"].ToString();
                        HojaExcel.Cells[i, "B"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "C"] = dtReporte.Rows[j]["Tracto"].ToString();
                        HojaExcel.Cells[i, "C"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "D"] = dtReporte.Rows[j]["Conductor"].ToString();
                        HojaExcel.Cells[i, "D"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "E"] = dtReporte.Rows[j]["Carreta"].ToString();
                        HojaExcel.Cells[i, "E"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "F"] = dtReporte.Rows[j]["Producto"].ToString();
                        HojaExcel.Cells[i, "F"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "G"] = dtReporte.Rows[j]["Salida Planta"].ToString();
                        HojaExcel.Cells[i, "G"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "H"] = dtReporte.Rows[j]["KMCarga"].ToString();
                        HojaExcel.Cells[i, "H"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "I"] = dtReporte.Rows[j]["Salida Punto Descarga"].ToString();
                        HojaExcel.Cells[i, "I"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "J"] = dtReporte.Rows[j]["KMDescarga"].ToString();
                        HojaExcel.Cells[i, "J"].Borders.Color = Color.Black;                        
                        HojaExcel.Cells[i, "K"] = dtReporte.Rows[j]["Ubicacion"].ToString();
                        HojaExcel.Cells[i, "K"].Font.Color = Color.Blue;
                        HojaExcel.Cells[i, "K"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "L"] = dtReporte.Rows[j]["Estado"].ToString();
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Detenido Ret"))
                        {
                            HojaExcel.Cells[i, "L"].Font.Color = Color.Red;
                            HojaExcel.Cells[i, "L"].Borders.Color = Color.Black;
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("En transito"))
                        {
                            HojaExcel.Cells[i, "L"].Font.Color = Color.FromArgb(65, 125, 193);
                            HojaExcel.Cells[i, "L"].Borders.Color = Color.Black;
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Espera Carga"))
                        {
                            HojaExcel.Cells[i, "L"].Font.Color = Color.DeepSkyBlue;
                            HojaExcel.Cells[i, "L"].Borders.Color = Color.Black;
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Finalizado"))
                        {
                            HojaExcel.Cells[i, "L"].Font.Color = Color.FromArgb(87, 166, 57);
                            HojaExcel.Cells[i, "L"].Borders.Color = Color.Black;
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Retorno"))
                        {
                            HojaExcel.Cells[i, "L"].Font.Color = Color.SlateGray;
                            HojaExcel.Cells[i, "L"].Borders.Color = Color.Black;
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Stand By"))
                        {
                            HojaExcel.Cells[i, "L"].Font.Color = Color.Purple;
                            HojaExcel.Cells[i, "L"].Interior.Color = Color.Goldenrod;
                            HojaExcel.Cells[i, "L"].Borders.Color = Color.Black;
                        }
                       
                        // Opciones
                        HojaExcel.Cells[i, "M"] = dtReporte.Rows[j]["Planta"].ToString();
                        HojaExcel.Cells[i, "M"].Borders.Color = Color.Black;
                        // Valor de la Respuesta
                        HojaExcel.Cells[i, "N"] = dtReporte.Rows[j]["PuntoDescarga"].ToString();
                        HojaExcel.Cells[i, "N"].Borders.Color = Color.Black;

                        HojaExcel.Cells[i, "O"] = dtReporte.Rows[j]["Tracto_Apoyo"].ToString();
                        HojaExcel.Cells[i, "O"].Borders.Color = Color.Black;
                        i++;
                    }
                }
                if (comboBox1.Text.Equals("GENERAL"))
                {
                    if (dtReporte.Rows.Count > 0)
                    {
                        //string VBLA = dtReporte.Columns[6].ColumnName.ToString();
                        objCelda = HojaExcel.Range[datos.Substring(0, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[6].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.FromArgb(65, 125, 193);
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(2, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[7].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.FromArgb(65, 125, 193);
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(4, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[8].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.FromArgb(65, 125, 193);
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(6, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[9].ColumnName.ToString();
                        objCelda.Font.Color = Color.Orange;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(8, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[10].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.FromArgb(87, 166, 57);
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(10, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[11].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Interior.Color = Color.FromArgb(87, 166, 57);
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(12, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[12].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.FromArgb(87, 166, 57);
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;

                        objCelda = HojaExcel.Range[datos.Substring(14, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[13].ColumnName.ToString();
                        objCelda.Font.Color = Color.Green;
                        objCelda.Interior.Color = Color.PaleGreen; //UBICACION
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(16, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[14].ColumnName.ToString();
                        objCelda.Font.Color = Color.Green;
                        objCelda.Interior.Color = Color.PaleGreen; //STATUS
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(18, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[15].ColumnName.ToString();
                        objCelda.Font.Color = Color.White;
                        objCelda.Interior.Color = Color.Red; //observacion
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(20, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[16].ColumnName.ToString();
                        objCelda.Font.Color = Color.Green;
                        objCelda.Interior.Color = Color.PaleGreen; //TRACTO APOYO
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(22, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[17].ColumnName.ToString();
                        objCelda.Font.Color = Color.Green;
                        objCelda.Interior.Color = Color.PaleGreen;    //CONDUCTOR APOYO
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                        objCelda = HojaExcel.Range[datos.Substring(24, 2), Type.Missing];
                        objCelda.Value = dtReporte.Columns[18].ColumnName.ToString();
                        objCelda.Font.Color = Color.Green;
                        objCelda.Interior.Color = Color.PaleGreen;    //CARGA Y DESCARGA
                        objCelda.Borders.Color = Color.Black;
                        objCelda.Font.Bold = true;
                    }

                    for (int j = 0; j < dtReporte.Rows.Count; j++)
                    {
                        // Asignar los valores de los registros a las celdas
                        HojaExcel.Cells[i, "A"] = dtReporte.Rows[j]["Fecha"].ToString();
                        HojaExcel.Cells[i, "A"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "B"] = dtReporte.Rows[j]["Ruta"].ToString();
                        HojaExcel.Cells[i, "B"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "C"] = dtReporte.Rows[j]["Tracto"].ToString();
                        HojaExcel.Cells[i, "C"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "D"] = dtReporte.Rows[j]["Conductor"].ToString();
                        HojaExcel.Cells[i, "D"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "E"] = dtReporte.Rows[j]["Carreta"].ToString();
                        HojaExcel.Cells[i, "E"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "F"] = dtReporte.Rows[j]["Producto"].ToString();
                        HojaExcel.Cells[i, "F"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "G"] = dtReporte.Rows[j]["Hora de Llegada Carga"].ToString();
                        HojaExcel.Cells[i, "G"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "H"] = dtReporte.Rows[j]["Hora de Carga"].ToString();
                        HojaExcel.Cells[i, "H"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "I"] = dtReporte.Rows[j]["Salida a Ruta"].ToString();
                        HojaExcel.Cells[i, "I"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "J"] = dtReporte.Rows[j]["Hora Salida Carga"].ToString();
                        HojaExcel.Cells[i, "J"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "K"] = dtReporte.Rows[j]["llegada"].ToString();
                        HojaExcel.Cells[i, "K"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "L"] = dtReporte.Rows[j]["Inicio Descarga"].ToString();
                        HojaExcel.Cells[i, "L"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "M"] = dtReporte.Rows[j]["Fin Descarga"].ToString();
                        HojaExcel.Cells[i, "M"].Borders.Color = Color.Black;
                        /*HojaExcel.Cells[i, "N"] = dtReporte.Rows[j]["Ubicacion"].ToString();
                        HojaExcel.Cells[i, "N"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "O"] = dtReporte.Rows[j]["Estado"].ToString();
                        HojaExcel.Cells[i, "O"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "P"] = dtReporte.Rows[j]["Tracto_Apoyo"].ToString();
                        HojaExcel.Cells[i, "P"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "Q"] = dtReporte.Rows[j]["Llegada a Base"].ToString();
                        HojaExcel.Cells[i, "Q"].Borders.Color = Color.Black;*/
                        HojaExcel.Cells[i, "N"] = dtReporte.Rows[j]["Ubicacion"].ToString();
                        HojaExcel.Cells[i, "N"].Font.Color = Color.Blue;
                        HojaExcel.Cells[i, "N"].Borders.Color = Color.Black;
                        /*HojaExcel.Cells[i, "S"] = dtReporte.Rows[j]["%Transito"].ToString();
                        HojaExcel.Cells[i, "S"].Borders.Color = Color.Black;
                        HojaExcel.Cells[i, "S"].FormatConditions.AddDatabar();*/
                        HojaExcel.Cells[i, "O"] = dtReporte.Rows[j]["Estado"].ToString();
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Cargado Base"))
                        {
                            HojaExcel.Cells[i, "O"].Font.Color = Color.Red;
                            HojaExcel.Cells[i, "O"].Interior.Color = Color.Yellow;
                        }                        
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Detenido"))
                        {
                            HojaExcel.Cells[i, "O"].Font.Color = Color.Red;
                        }
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("En Transito"))
                        {
                            HojaExcel.Cells[i, "O"].Font.Color = Color.FromArgb(65, 125, 193);
                        }                       
                        if (dtReporte.Rows[j]["Estado"].ToString().Equals("Finalizado"))
                        {
                            HojaExcel.Cells[i, "O"].Font.Color = Color.FromArgb(87, 166, 57);
                        }
                      
                        // Opciones
                        HojaExcel.Cells[i, "P"] = dtReporte.Rows[j]["Observaciones"].ToString();
                        HojaExcel.Cells[i, "P"].Borders.Color = Color.Black;
                        // Valor de la Respuesta
                        HojaExcel.Cells[i, "Q"] = dtReporte.Rows[j]["Tracto_Apoyo"].ToString();
                        HojaExcel.Cells[i, "Q"].Borders.Color = Color.Black;

                        HojaExcel.Cells[i, "R"] = dtReporte.Rows[j]["Conductor_Apoyo"].ToString();
                        HojaExcel.Cells[i, "R"].Borders.Color = Color.Black;
                        // Valor de la Respuesta
                        HojaExcel.Cells[i, "S"] = dtReporte.Rows[j]["Carga y Descarga"].ToString();
                        HojaExcel.Cells[i, "S"].Borders.Color = Color.Black;
                        i++;
                    }
                }
                // Seleccionar todo el bloque desde A1 hasta D #de filas.
                Microsoft.Office.Interop.Excel.Range Rango = HojaExcel.Range["A3:E" + (i - 1).ToString()];
                // Selecionado todo el rango especificado
                Rango.Select();
                // Ajustamos el ancho de las columnas al ancho máximo del
                // contenido de sus celdas
                Rango.Columns.AutoFit();
                // Asignar filtro por columna
                Rango.AutoFilter(1);
                // Crear un total general
                // LibroExcel.PrintPreview();
            }
            else
            {
                MessageBox.Show("Las programaciones de la fecha seleccionada no contienen seguimiento...!", "Alerta");
            }
        }
              

        private void dgvPreviajes_Click(object sender, EventArgs e)
        {
          /*  try
            {

                if (dgvPreviajes.DataSource == null)
                    return;

                if (this.dgvPreviajes.Columns[5].Name == "ABASTECIO" && this.dgvPreviajes.CurrentRow.Index == 0)
                {
                    if (Tiporder == 1)
                    {
                        dgvPreviajes.Sort(dgvPreviajes.Columns[5], ListSortDirection.Descending);
                        Tiporder = 2;
                    }
                    //Filtra primero los abastecimientos de vacios a llenos
                    else
                    {
                        dgvPreviajes.Sort(dgvPreviajes.Columns[5], ListSortDirection.Ascending);
                        Tiporder = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void prograPorAtenderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProgramacionesxAtender frmPrograAtender = new frmProgramacionesxAtender();
            frmPrograAtender.ShowDialog();
        }

        private void confOrdenDestinosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrdenarDestinos frmDestinos = new frmOrdenarDestinos();
            frmDestinos.Operaciones = comboBox1.Text.ToString();
            frmDestinos.idOperacion = Convert.ToInt32(comboBox1.SelectedValue.ToString());
            frmDestinos.ShowDialog();
        }

        private void registrarNochesToolStripMenuItem_Click(object sender, EventArgs e)
        {       
            frmAsistenciaRegistrarNoche frmAsistenciaRegistrarNoche = new frmAsistenciaRegistrarNoche();
            frmAsistenciaRegistrarNoche.ShowDialog();        

        }

        private void mostrarFaltantesDeViajeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListaFaltantesMercaderia frmListaFaltantesMercaderia = new frmListaFaltantesMercaderia();
            frmListaFaltantesMercaderia.ShowDialog();  
        }

        private void registroDeFallasMecánicasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListaFallasMecanicas frmListaFallasMecanicas = new frmListaFallasMecanicas();
            frmListaFallasMecanicas.ShowDialog();
        }

        private void registroDeIncidentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListaIncidencias frmListaIncidencias = new frmListaIncidencias();
            frmListaIncidencias.ShowDialog();
        }

        private void asignarGastoDeRutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListarGastosRuta frmListarGastosRuta = new frmListarGastosRuta();
            frmListarGastosRuta.ShowDialog();
        }

        private void asignarViaticoAdicionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TicketsGasto.frmViaticosAdicionales frmViaticosAdicionales = new TicketsGasto.frmViaticosAdicionales();
            frmViaticosAdicionales.adjuntarToolStripMenuItem.Enabled = false;
            frmViaticosAdicionales.Opcion = 1;
            frmViaticosAdicionales.CargarComboViatico();
            frmViaticosAdicionales.cbxTipoViatico_DropDownClosed(sender, e);
            frmViaticosAdicionales.ShowDialog(this);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pPlanillas.Visible = false;
            pPlanillas.SendToBack();
            txtBuscaConductor.Clear();
            txtBuscarRuta.Clear();
        }

        private void pPlanillas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pPlanillas.Left = pPlanillas.Left + (e.X - xClick);
                pPlanillas.Top = pPlanillas.Top + (e.Y - yClick);
            }
        }

        private void dtPlanillas_DoubleClick(object sender, EventArgs e)
        {
            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
            string Planilla = dgvPlanillas.GetRowCellValue(dgvPlanillas.FocusedRowHandle, "CODIGO").ToString();
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dtListaTicket2 = new DataTable();
            dtListaTicket2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ActualizarPlanilla(NroProgramacion, Planilla, Usuario);

            string Respuesta = Convert.ToString(dtListaTicket2.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pPlanillas.Visible = false;
                pPlanillas.SendToBack();
                //CargarDatos();
            }

            /*
            int NroProgramacion = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["CODIGO"].Value.ToString());
            string Planilla = dgvPlanillas.GetRowCellValue(dgvPlanillas.FocusedRowHandle, "CODIGO").ToString();
            
            int idConductor = Convert.ToInt32(dgvPreviajes.CurrentRow.Cells["IDCONDUCTOR"].Value.ToString());
            decimal Gasto = Convert.ToDecimal(dgvPlanillas.GetRowCellValue(dgvPlanillas.FocusedRowHandle, "MONTO").ToString());
             
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket2 = new DataTable();
                dtListaTicket2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ActualizarPlanilla(NroProgramacion, Planilla, Usuario, idConductor, Gasto);

                string Respuesta = Convert.ToString(dtListaTicket2.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pPlanillas.Visible = false;
                    pPlanillas.SendToBack();
                    CargarDatos();

                    string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

                    if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
                    {
                        MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        DataTable dtListaTicket = new DataTable();
                        dtListaTicket = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarRegistro(NroProgramacion);

                        if (dtListaTicket.Rows.Count > 0)
                        {
                            Ticket ticket = new Ticket();

                            decimal MontoTotal = Convert.ToDecimal(dtListaTicket.Rows[0]["GASTO_TOTAL"]) + Convert.ToDecimal(dtListaTicket.Rows[0]["GASTO_DIFERENCIAL"]);
                            
                            ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                            ticket.AddSubHeaderLine2("PL - " + dtListaTicket.Rows[0]["CodGasto"].ToString() + "                         ");
                            ticket.AddSubHeaderLine("Chofer: " + dtListaTicket.Rows[0]["CONDUCTOR"].ToString());
                            ticket.AddSubHeaderLine("Tracto: " + dtListaTicket.Rows[0]["TRACTO"].ToString() + "   SR: " + dtListaTicket.Rows[0]["SEMIRREMOLQUE"].ToString());
                            ticket.AddSubHeaderLine("Ruta: " + dtListaTicket.Rows[0]["RUTA"].ToString());
                            ticket.AddSubHeaderLine("Cliente: " + dtListaTicket.Rows[0]["CLIENTE"].ToString());
                            ticket.AddSubHeaderLine("F.Viaje: " + dtListaTicket.Rows[0]["FECHA_VIAJE"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("TOTAL EFECTIVO: S/. " + MontoTotal.ToString());
                            ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket.AddSubHeaderLine("FIRMA: ");
                            ticket.HeaderImage = Resources.TABLA;
                            ticket.PrintTicket(NombreImpresora);
                        }
                    }
                }
                else
                { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("No se pudo asignar la planilla.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            */
        }



        private void btnBuscarP_Click(object sender, EventArgs e)
        {
            dtPlanillas.DataSource = null;
            dgvPlanillas.Columns.Clear();

            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt2.Clear();
            dt2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPlanillasSinViaje(txtBuscaConductor.Text, txtBuscarRuta.Text);
            if (dt2.Rows.Count > 0)
            {
                dtPlanillas.DataSource = dt2;
                dgvPlanillas.Columns["idGastoXRutaC"].Visible = false;
                dgvPlanillas.BestFitColumns();
            }

            pPlanillas.Visible = true;
            pPlanillas.BringToFront();
        }

        private void txtBuscaConductor_KeyPress(object sender, KeyPressEventArgs e)
        { if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarP_Click(sender, e); } }

        private void txtBuscarRuta_KeyPress(object sender, KeyPressEventArgs e)
        { if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarP_Click(sender, e); } }

        private void maestroDeZonasDeRutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmZonaDeRuta frmZonaDeRuta = new frmZonaDeRuta();
            frmZonaDeRuta.ShowDialog();
        }

        private void registrarLavadoDeUnidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListaTicketsLavadero frmListaTicketsLavadero = new frmListaTicketsLavadero();
            frmListaTicketsLavadero.ShowDialog();
        }

        private void constanciasDeEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConstanciaUnidades frmConstanciaUnidades = new frmConstanciaUnidades();
            frmConstanciaUnidades.ShowDialog();
        }

        private void planDeMttoPreventivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmListaMantenimiento frmListaMantenimiento = new frmListaMantenimiento();
            frmListaMantenimiento.ShowDialog();
        }

        private void tsRegistrarAuxilio_Click(object sender, EventArgs e)
        {
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            FallasMecanicas.frmRegistroFallasMecanicas frmFallas = new FallasMecanicas.frmRegistroFallasMecanicas();
            frmFallas.pManual.Enabled = true;
            frmFallas.pManual.BringToFront();
            frmFallas.groupBox1.Enabled = false;
            frmFallas.groupBox1.SendToBack();
            frmFallas.groupBoxT.Enabled = true;
            frmFallas.groupBoxT.BringToFront();
            frmFallas.cbBloqueoC.Checked = false;
            frmFallas.cbBloqueoC_CheckedChanged(sender, e);
            frmFallas.cbBloqueoU.Checked = false;
            frmFallas.cbBloqueoU_CheckedChanged(sender, e);
            frmFallas.EnviarDatos(-1, Usuario);
            frmFallas.cbxTipoDanio.Text = "MATERIAL";
            frmFallas.ShowDialog(this);
        }

        private void tsDesbloquearPlanilla_Click(object sender, EventArgs e)
        {
            frmDesbloqueoPlanilla frmDesbloqueoPlanilla = new frmDesbloqueoPlanilla();
            frmDesbloqueoPlanilla.ShowDialog();
        }

        private void tsKitNeumatico_Click(object sender, EventArgs e)
        {
            frmDesbloqueoKit frmDesbloqueoKit = new frmDesbloqueoKit();
            frmDesbloqueoKit.ShowDialog();
        }

        private void tsItinerarioViajes_Click(object sender, EventArgs e)
        {
            frmItinerarioViajes frmItinerarioViajes = new frmItinerarioViajes();
            frmItinerarioViajes.ShowDialog();
        }

        private void tsRegistroTiempos_Click(object sender, EventArgs e)
        {
            frmListaTiemposViaje frmListaTiemposViaje = new frmListaTiemposViaje();
            frmListaTiemposViaje.ShowDialog();
        }

        private void tsUnidadesDisponibles_Click(object sender, EventArgs e)
        {
            frmUnidadesDisponibles frmUnidadesDisponibles = new frmUnidadesDisponibles();
            frmUnidadesDisponibles.ShowDialog();
        }

        private void pNuevoLavado_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pNuevoLavado.Left = pNuevoLavado.Left + (e.X - xClick2);
                pNuevoLavado.Top = pNuevoLavado.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            txtTracto.Clear();
            txtCarreta.Clear();
            txtOperacion.Clear();
            cbxTipoLavado.Text = "AMBOS";

            pNuevoLavado.Visible = false;
            pNuevoLavado.SendToBack();
        }

        private void btnImprimirLavado_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea generar un ticket de lavado para esta unidad?", "GENERAR TICKET DE LAVADO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DateTime FechaProg = DateTime.Now;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    if (cbxTipoLavado.Text == "TRACTO")
                    {
                        DataTable dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_GenerarTicket(txtTracto.Text, FechaProg, txtOperacion.Text, cbxTipoLavado.Text, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        MessageBox.Show("Ticket de Lavado Generado. N° " + Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (cbxTipoLavado.Text == "SEMIRREMOLQUE")
                    {
                        DataTable dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_GenerarTicket(txtCarreta.Text, FechaProg, txtOperacion.Text, cbxTipoLavado.Text, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        MessageBox.Show("Ticket de Lavado Generado. N° " + Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    if (cbxTipoLavado.Text == "AMBOS")
                    {
                        DataTable dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_GenerarTicket(txtTracto.Text + " || " + txtCarreta.Text, FechaProg, txtOperacion.Text, cbxTipoLavado.Text, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        MessageBox.Show("Ticket de Lavado Generado. N° " + Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch { MessageBox.Show("Error generando el ticket de lavado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                try
                {
                    DataTable dtConsultarImpresora = new DataTable();
                    dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                    DataTable dtListaTicket = new DataTable();
                    dtListaTicket = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_TicketsLavadero_ListarTicket(Respuesta);

                    string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

                    if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
                    {
                        MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnCerrar_Click(sender, e);
                        return;
                    }
                    else
                    {
                        if (dtListaTicket.Rows.Count > 0)
                        {
                            Ticket ticket = new Ticket();

                            ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                            ticket.AddSubHeaderLine2("TICKET DE LAVADO");
                            ticket.AddSubHeaderLine2("DE UNIDAD");
                            ticket.AddSubHeaderLine2("TL - " + dtListaTicket.Rows[0]["CodLavado"].ToString() + "                         ");
                            ticket.AddSubHeaderLine("Unidad(es): " + dtListaTicket.Rows[0]["PLACA"].ToString());
                            ticket.AddSubHeaderLine("Lavado: " + dtListaTicket.Rows[0]["LAVADO"].ToString());
                            ticket.AddSubHeaderLine("Operación: " + dtListaTicket.Rows[0]["OPERACION"].ToString());
                            ticket.AddSubHeaderLine("F.Programada: " + dtListaTicket.Rows[0]["FECHA"].ToString());
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket.PrintTicket(NombreImpresora);
                            btnCerrar_Click(sender, e);
                        }
                    }
                }
                catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}