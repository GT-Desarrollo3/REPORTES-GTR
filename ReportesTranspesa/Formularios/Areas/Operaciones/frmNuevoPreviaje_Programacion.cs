using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Negocio;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReportesTranspesa.Sistema;
using System.Globalization;
using Comun;
using ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes;
using ReportesTranspesa.Formularios.Areas.Combustible;
using Entidades;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmNuevoPreviaje_Programacion : Form
    {
        //Variable PARA RECIBIR LOS VALORES DEL COMBO
        private int R_IdProgramacionPrincipal;
        private int idProgramacion;
        public DataTable MaestroGR = new DataTable();
        private string R_PROGRAMACION;
        public int _IdProg;
        public int _TipoProg;
        public string _DescTipo, _FechaProg, _DescTracto, _DesRemolque, _Sucursal;
        public string _FechaInicio, _ConducInicio, _PrimCambio, _SegCambio, _TerCambio;
        public string _FechaFin;
        public int _Tracto, TipoProgramacion;
        public int _Remolque, _Accesos = 0;
        public int _Conductor;
        public int _Apoyo;
        public int _Cliente;
        public int _Ruta, _dia;
        public string _Destino, _nomConductor, _nomApoyo, _nomCliente, _Zona, _Turno, _MontoPlanila;
        public int _Producto, _Ot, idUnidadApoyo;
        public int _Estado;
        public string _HoraSailda, _nomProducto, _nomRuta, _OrdenServicio, _tipounidad;
        public string _HoraLlegada;
        public string _FechaDescarga;
        public decimal _PesoAlmacen = 12.120000m;
        public decimal _PesoCliente = 12.120000m;
        public decimal _Viaticos = 12.120000m;
        public decimal _Merma = 12.120000m;
        public decimal _montoOrdenServicio = 12.120000m;
        public string _Viaje, idViaje, ost,_FechaRegistro;
        public string _Planilla, _EsInterna;
        public string _Observacion,_PlacaMaquinaria;
        public string _Serie, _guiaEntrega, _userModifica, _fechaMod,_usuarioCrea;
        public string _Numero, _UnidadApoyo, _tipoSemirre;
        public string _Remitente,_anio,_CodigoTolvaz;
        public int crea1_modifi2_anula3 = 0;
        decimal montoOrdenServicio;
        int idProducto = -1, _UbigeoPartida ,_UbigeoLlegada;
        int idConductor = -1;
        int idtracto = -1;
        int idremolque = -1;
        int idapoyo = -1;
        int idCliente = -1;
        int idRuta = -1;
        decimal tiempo = 12.120000m;
        decimal Merma;
        string EstadoP, EstadoR, UnidadFallaP, UnidadFallaR;
        string lineaConsolidado = "";
        string IDPARTIDA="0";
        string IDLLEGADA="0";
        string NombrePartida = "";
        string NombreDestino = "";
        public int idRutaDia = -1;
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public int xClick3 = 0, yClick3 = 0;
        public int idRelacion = -1;
        public int idUnidadMtto;
        public int xClick4 = 0, yClick4 = 0;

        DataTable dtotsdis = new DataTable();

        public DataTable dtControlesPreviajes;

        public frmNuevoPreviaje_Programacion()
        {
            InitializeComponent();
        }

        //Recepcion y cargado de Valores del Form "frmOperacionPreviajes"
        public void setearvariable(int accion, int IDPROGRAMA, string PROGRAMACION, int var_IdProg, string var_Sucursal, int var_TipoProg, string var_DesTipo, string var_FechaProg, string var_FechaInicio, string var_FechaFin,
                                    int var_Tracto, string var_DesTracto, int var_Remolque, string var_DesRemolque, int var_Conductor, int var_Apoyo, int var_Cliente, string var_nomConductor, string var_nomApoyo,
                                    string var_nomCliente, string var_nomProducto, string var_nomRuta, int var_Ruta, string var_Destino, int var_Producto, int var_Estado,
                                    string var_HoraSailda, string var_HoraLlegada, string var_FechaDescarga, decimal var_PesoAlmacen, decimal var_PesoCliente, decimal var_Merma,
                                    string var_Viaje,string var_Idviaje, string var_Planilla, string var_Observacion, string var_Serie, string var_Numero, string var_Remitente, string var_zona,
                                    string var_turno, string var_ConducInicio, string var_PrimCambio, string var_SegCambio, string var_TerCambio, int var_Accesos, int Ot, decimal Viaticos, string OrdenServicio,
                                    string var_TipoUnidad, string var_UnidadApoyo, string var_guiaEntrega, decimal var_MontoOrden, string tipoSemirremolque, int var_dia, string var_Usermodifica, string var_fechaMod,string var_usuarioCrea, string var_Interna,
                                    int var_UbigeoPartida, int var_UbigeoLlegada, string var_Anio, string PlacaMaquinaria, string CodigoTolvaz, string fechaRegistro, string var_MontoPlanila, string var_progOrigen,string linea)
        {
            R_IdProgramacionPrincipal = IDPROGRAMA;
            idProgramacion = IDPROGRAMA;
            R_PROGRAMACION = PROGRAMACION;
            _IdProg = var_IdProg;
            _TipoProg = var_TipoProg;
            _DescTipo = var_DesTipo;
            _FechaProg = var_FechaProg;
            _FechaInicio = var_FechaInicio;
            _FechaFin = var_FechaFin;
            _Tracto = var_Tracto;
            _DescTracto = var_DesTracto;
            _Remolque = var_Remolque;
            _DesRemolque = var_DesRemolque;
            _Conductor = var_Conductor;
            _Apoyo = var_Apoyo;
            _Cliente = var_Cliente;
            _nomConductor = var_nomConductor;
            _nomApoyo = var_nomApoyo;
            _nomCliente = var_nomCliente;
            _nomProducto = var_nomProducto;
            _nomRuta = var_nomRuta;
            _Ruta = var_Ruta;
            _Destino = var_Destino;
            _Producto = var_Producto;
            _Estado = var_Estado;
            _HoraSailda = var_HoraSailda;
            _HoraLlegada = var_HoraLlegada;
            _FechaDescarga = var_FechaDescarga;
            _PesoAlmacen = var_PesoAlmacen;
            _PesoCliente = var_PesoCliente;
            _Merma = var_Merma;
            _tipoSemirre = tipoSemirremolque;
            _dia = var_dia;
            

            if (var_Viaje.Equals(""))
            {
                _Viaje = var_Viaje;
            }
            else
            {
                _Viaje = var_Viaje.Substring(0, 6);
            }
            _Planilla = var_Planilla;
            _Observacion = var_Observacion;
            _Serie = var_Serie;
            _Numero = var_Numero;
            _Remitente = var_Remitente;
            _Zona = var_zona;
            _Turno = var_turno;
            _Sucursal = var_Sucursal;
            _ConducInicio = var_ConducInicio;
            _PrimCambio = var_PrimCambio;
            _SegCambio = var_SegCambio;
            _TerCambio = var_TerCambio;
            _Accesos = var_Accesos;
            _Ot = Ot;
            _Viaticos = Viaticos;
            _OrdenServicio = OrdenServicio;
            _tipounidad = var_TipoUnidad;
            _UnidadApoyo = var_UnidadApoyo;
            _guiaEntrega = var_guiaEntrega;
            _montoOrdenServicio = var_MontoOrden;
            _userModifica = var_Usermodifica;
            _fechaMod = var_fechaMod;
            _usuarioCrea = var_usuarioCrea;
            _EsInterna = var_Interna;
            crea1_modifi2_anula3 = accion;
            _UbigeoPartida = var_UbigeoPartida;
            _UbigeoLlegada = var_UbigeoLlegada;
            _anio = var_Anio;
            _PlacaMaquinaria = PlacaMaquinaria;
            _CodigoTolvaz = CodigoTolvaz;
            _FechaRegistro = fechaRegistro;
            _MontoPlanila = var_MontoPlanila;
            idViaje = var_Idviaje;
            txtProgramacionOrigen.Text = var_progOrigen;
            lineaConsolidado = linea;
        }

        private void frmNuevoPreviaje_Programacion_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("Operaciones_Programacion_Previajes");
                DataTable dtEspeciales = null;

                if (dtPermisos != null)
                {
                    if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                    { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

                    if (dtEspeciales != null)
                    {
                        for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                        {
                            if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Anular Consolidado")
                            {
                                tsQuitarOT.Enabled = true;
                                i = 999;
                            }
                            else { tsQuitarOT.Enabled = false; }
                        }
                    }
                }
                
                CargarControlesPreviajes();
                //Formato de las fechas
                dtFechaProgramada.CustomFormat = "dd/MM/yyyy HH:mm:ss";
                dtFechaProgramada.Format = DateTimePickerFormat.Custom;
                dtpFechaDescarga.CustomFormat = "dd/MM/yyyy HH:mm:ss";
                dtpFechaDescarga.Format = DateTimePickerFormat.Custom;
                dtFechaInicio.CustomFormat = "dd/MM/yyyy HH:mm:ss";
                dtFechaInicio.Format = DateTimePickerFormat.Custom;
                dtFechaFin.CustomFormat = "dd/MM/yyyy HH:mm:ss";
                dtFechaFin.Format = DateTimePickerFormat.Custom;
                dtLlegada.CustomFormat = "HH:mm:ss";
                dtLlegada.Format = DateTimePickerFormat.Custom;
                dtSalida.CustomFormat = "HH:mm:ss";
                dtSalida.Format = DateTimePickerFormat.Custom;

                //Llenado del combo TipoPreviajes
               /* DataTable dt = new DataTable();
                dt = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeOperaciones(Utilitario.Instancia.SesionUsuario.usuario);

                cboTipoProgramacion.DisplayMember = "Descripcion";
                cboTipoProgramacion.ValueMember = "IdOperacion";
                cboTipoProgramacion.DataSource = dt;*/

                //CARGO LOS VALORES EN Label CAJA De TEXTO
                if (!string.IsNullOrEmpty(R_PROGRAMACION))
                    textBox1.Text = R_PROGRAMACION;
                txtSucursal.Text = _Sucursal;
                txtTracto.Focus();
                if (R_PROGRAMACION.Equals("LINDLEY"))
                {
                    txtPesoClientes.Visible = false;
                    lblPesoCliente.Visible = false;
                    txtMerma.Visible = false;
                    lblMerma.Visible = false;
                    tmePrimerCambio.Visible = false;
                    tmeSegCambio.Visible = false;
                    tmeTerCambio.Visible = false;
                    txtConductorInicio.Visible = false;
                    label28.Visible = false;
                    label29.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    label16.Visible = false;
                    label18.Visible = false;
                    label17.Visible = false;
                    dtSalida.Visible = false;
                    dtLlegada.Visible = false;
                    dtpFechaDescarga.Visible = false;
                    txtUnidadApoyo.Visible = true;
                    lblUnidadApoyo.Visible = true;
                    //label32.Visible = false;
                    //label33.Visible = false;
                    //cboPartida.Visible = false;
                    //cboLlegada.Visible = false;
                    txtMaquinaria.Visible = false;
                    lblCodProgram.Visible = false;
                    txtCodTolvas.Visible = false;
                }
                if (R_PROGRAMACION.Equals("GENERAL"))
                {
                    txtPesoClientes.Visible = false;
                    lblPesoCliente.Visible = false;
                    txtMerma.Visible = false;
                    lblMerma.Visible = false;
                    tmePrimerCambio.Visible = false;
                    tmeSegCambio.Visible = false;
                    tmeTerCambio.Visible = false;
                    txtConductorInicio.Visible = false;
                    label28.Visible = false;
                    label29.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    txtPesoClientes.Visible = false;
                    lblPesoCliente.Visible = false;
                    txtMerma.Visible = false;
                    lblMerma.Visible = false;
                    //label32.Visible = false;
                    //label33.Visible = false;
                    //cboPartida.Visible = false;
                    //cboLlegada.Visible = false;
                    txtMaquinaria.Visible = false;
                    txtUnidadApoyo.Visible = true;
                    lblUnidadApoyo.Visible = true;
                    lblCodProgram.Visible = false;
                    txtCodTolvas.Visible = false;
                    txtSerie.Visible = true;
                    txtNumero.Visible = true;
                    txtGuiaRemitente.Visible = true;
                }
                if (R_PROGRAMACION.Equals("LIMAGAS"))
                {
                    lblTurno.Visible = false;
                    txtTurno.Visible = false;
                    txtSerie.Visible = true;
                    txtNumero.Visible = true;
                    txtGuiaRemitente.Visible = true;
                    label19.Visible = true;
                    label20.Visible = true;
                    lblGuiaEntrega.Visible = true;
                    txtGuiaEntrega.Visible = true;
                    //label32.Visible = false;
                    //label33.Visible = false;
                    //cboPartida.Visible = false;
                    //cboLlegada.Visible = false;
                    txtMaquinaria.Visible = false;
                    txtUnidadApoyo.Visible = true;
                    lblUnidadApoyo.Visible = true;
                    lblCodProgram.Visible = false;
                    txtCodTolvas.Visible = false;
                }
                if (R_PROGRAMACION.Equals("TOLVAS"))
                {
                    lblTurno.Visible = false;
                    txtTurno.Visible = false;
                    txtSerie.Visible = true;
                    txtNumero.Visible = true;
                    txtGuiaRemitente.Visible = true;
                    label19.Visible = true;
                    label20.Visible = true;
                    lblGuiaEntrega.Visible = true;
                    txtGuiaEntrega.Visible = true;
                    label32.Visible = true;
                    label33.Visible = true;
                    cboPartida.Visible = true;
                    cboLlegada.Visible = true;
                    txtMaquinaria.Visible = false;
                    txtUnidadApoyo.Visible = true;
                    lblUnidadApoyo.Visible = true;
                    lblCodProgram.Visible = true;
                    txtCodTolvas.Visible = true;
                }
                if (R_PROGRAMACION.Equals("MAQ/CAMIONETAS"))
                {
                    groupBox3.Enabled = false;
                    checkBox1.Visible = false;
                    txtConductorInicio.Visible = false;
                    tmePrimerCambio.Visible = false;
                    tmeSegCambio.Visible = false;
                    tmeTerCambio.Visible = false;
                    txtMaquinaria.Visible = true;
                    txtAyudante.Enabled = false;
                    txtUnidadApoyo.Visible = false;
                    txtRemolque.Enabled = false;
                    label28.Visible = false;
                    label29.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    label5.Text = "Unidad:";
                    txtObservacion.Location = new Point(142, 440);
                    label21.Location = new Point(58, 450);
                    lblCodProgram.Visible = false;
                    txtCodTolvas.Visible = false;
                }

                if (R_PROGRAMACION.Equals("MTO"))
                {
                    groupBox3.Enabled = false;
                    checkBox1.Visible = false;
                    txtConductorInicio.Visible = false;
                    tmePrimerCambio.Visible = false;
                    tmeSegCambio.Visible = false;
                    tmeTerCambio.Visible = false;
                    txtMaquinaria.Visible = true;
                    txtAyudante.Enabled = false;
                    txtUnidadApoyo.Visible = false;
                    txtRemolque.Enabled = false;
                    label28.Visible = false;
                    label29.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    label5.Text = "Unidad:";
                    txtObservacion.Location = new Point(122, 440);
                    label21.Location = new Point(24, 450);
                    lblCodProgram.Visible = false;
                    txtCodTolvas.Visible = false;
                }

                if (R_PROGRAMACION.Equals("COMBUSTIBLE"))
                {
                    groupBox3.Enabled = false;
                    checkBox1.Visible = false;
                    txtConductorInicio.Visible = false;
                    tmePrimerCambio.Visible = false;
                    tmeSegCambio.Visible = false;
                    tmeTerCambio.Visible = false;
                    txtMaquinaria.Visible = true;
                    txtAyudante.Enabled = false;
                    txtUnidadApoyo.Visible = false;
                    txtRemolque.Enabled = false;
                    label28.Visible = false;
                    label29.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    label5.Text = "Unidad:";
                    txtObservacion.Location = new Point(142, 440);
                    label21.Location = new Point(24, 450);
                    lblCodProgram.Visible = false;
                    txtCodTolvas.Visible = false;
                }

                if (R_PROGRAMACION.Equals("LOCAL"))
                {
                   //groupBox3.Enabled = false;
                    checkBox1.Visible = false;
                    txtConductorInicio.Visible = false;
                    tmePrimerCambio.Visible = false;
                    tmeSegCambio.Visible = false;
                    tmeTerCambio.Visible = false;
                    txtMaquinaria.Visible = true;
                    txtAyudante.Enabled = false;
                    txtUnidadApoyo.Visible = false;
                    txtRemolque.Enabled = true;
                    label28.Visible = false;
                    label29.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    label5.Text = "Unidad:";
                    //txtObservacion.Location = new Point(142, 440);
                    //label21.Location = new Point(24, 450);
                    lblCodProgram.Visible = false;
                    txtCodTolvas.Visible = false;
                }
                if (R_PROGRAMACION.Equals("VOLCAN"))
                {
                    txtPesoClientes.Visible = true;
                    lblPesoCliente.Visible = true;
                    txtMerma.Visible = true;
                    lblMerma.Visible = true;
                    tmePrimerCambio.Visible = false;
                    tmeSegCambio.Visible = false;
                    tmeTerCambio.Visible = false;
                    txtConductorInicio.Visible = false;
                    label28.Visible = false;
                    label29.Visible = false;
                    label30.Visible = false;
                    label31.Visible = false;
                    label16.Visible = false;
                    label18.Visible = false;
                    label17.Visible = false;
                    dtSalida.Visible = false;
                    dtLlegada.Visible = false;
                    dtpFechaDescarga.Visible = false;
                    txtUnidadApoyo.Visible = true;
                    lblUnidadApoyo.Visible = true;
                    //label32.Visible = false;
                    //label33.Visible = false;
                    //cboPartida.Visible = false;
                    //cboLlegada.Visible = false;
                    txtMaquinaria.Visible = false;
                    lblCodProgram.Visible = false;
                    txtCodTolvas.Visible = false;
                }

                //Carga de consolidados
                if (!string.IsNullOrEmpty(_Viaje))
                {
                    CargarConsolidado();
                }
                comboBox1.SelectedIndex = 0;

                //Cargado de los Valores al Formulario al para la opcion Modificar
                if (crea1_modifi2_anula3 == 2)
                {
                    label34.Text = "CODIGO:   " + _anio.Remove(0, 2) + "" + _IdProg;
                    label34.Visible = true;
                    //  cboTipoProgramacion.SelectedIndex = _TipoProg - 1;
                    if (!string.IsNullOrEmpty(_OrdenServicio))
                    {
                        txtCostoOrdenServicio.Visible = true;
                        txtOrdServicio.Visible = true;
                    }
                    if (_montoOrdenServicio > 0)
                    {
                        txtCostoOrdenServicio.Visible = true;
                        txtOrdServicio.Visible = true;
                    }
                    if (_EsInterna.Equals("1"))
                    {
                        chbPrograInterna.Checked = true;
                    }

                    textBox1.Text = _DescTipo;
                    comboBox1.SelectedIndex = _Estado - 1;
                    txtSucursal.Text = _Sucursal;
                    dtFechaProgramada.Text = _FechaProg;
                    dtFechaInicio.Text = _FechaInicio;
                    dtFechaFin.Text = _FechaFin;
                    txtDestino.Text = _Destino;
                    dtSalida.Text = _HoraSailda;
                    dtLlegada.Text = _HoraLlegada;
                    txtTracto.Text = _DescTracto;
                    txtMaquinaria.Text = _DescTracto;
                    txtRemolque.Text = _DesRemolque;
                    dtpFechaDescarga.Text = _FechaDescarga;
                    txtPesoAlmacen.Text = Convert.ToString(_PesoAlmacen);
                    txtPesoClientes.Text = Convert.ToString(_PesoCliente);
                    txtMerma.Text = Convert.ToString(_Merma);
                    txtCodigoViaje.Text = _Viaje;
                    txtPlanilla.Text = _Planilla;
                    txtObservacion.Text = _Observacion;
                    txtSerie.Text = _Serie;
                    txtNumero.Text = _Numero;
                    txtGuiaRemitente.Text = _Remitente;
                    txtZona.Text = _Zona;
                    txtTurno.Text = Convert.ToString(_Turno);
                    lblTiposemirremolque.Text = _tipoSemirre;
                    lblDias.Text = Convert.ToString(_dia);
                    txtUserMod.Text = _userModifica;
                    txtFechaMod.Text = _fechaMod;
                    txtUsuarioCrea.Text = _usuarioCrea;
                    txtMontoPlanilla.Text = _MontoPlanila;

                    //Cargado los texbox editabls
                    txtConductor.Text = _nomConductor;
                    txtAyudante.Text = _nomApoyo;
                    txtCliente.Text = _nomCliente;
                    txtProducto.Text = _nomProducto;
                    txtRuta.Text = _nomRuta;
                    txtConductorInicio.Text = _ConducInicio;
                    tmePrimerCambio.Text = _PrimCambio;
                    tmeSegCambio.Text = _SegCambio;
                    tmeTerCambio.Text = _TerCambio;
                    txtOT.Text = Convert.ToString(_Ot);
                    txtMontoViaticos.Text = Convert.ToString(_Viaticos);
                    txtOrdServicio.Text = _OrdenServicio;
                    txtUnidadApoyo.Text = _UnidadApoyo;
                    txtGuiaEntrega.Text = _guiaEntrega;
                    txtCostoOrdenServicio.Text = Convert.ToString(_montoOrdenServicio);
                    txtMaquinaria.Text = _PlacaMaquinaria;
                    txtCodTolvas.Text = _CodigoTolvaz;
                    txtFecharegistro.Text = _FechaRegistro;

                    if (_Accesos == 1 || _Estado == 10 || _Estado == 9)
                    {
                        button1.Enabled = false;
                        textBox1.ReadOnly = true;
                        comboBox1.Enabled = false;
                        txtSucursal.ReadOnly = true;
                        dtFechaProgramada.Enabled = false;
                        dtFechaInicio.Enabled = false;
                        dtFechaFin.Enabled = false;
                        txtDestino.ReadOnly = true;
                        // dtSalida.ReadOnly = false;
                        // dtLlegada.ReadOnly = false;
                        txtTracto.ReadOnly = true;
                        txtMaquinaria.ReadOnly = true;
                        txtRemolque.ReadOnly = true;
                        //  dtpFechaDescarga.Enabled = false;
                        txtPesoAlmacen.ReadOnly = true;
                        txtPesoClientes.ReadOnly = true;
                        txtMerma.ReadOnly = true;
                        txtCodigoViaje.ReadOnly = true;
                        txtPlanilla.ReadOnly = true;
                        txtObservacion.ReadOnly = true;
                        txtSerie.ReadOnly = true;
                        txtNumero.ReadOnly = true;
                        txtGuiaRemitente.ReadOnly = true;
                        txtZona.ReadOnly = true;
                        txtTurno.ReadOnly = true;
                        //  lblTiposemirremolque.Enabled = false;
                        //  lblDias.Enabled = false;
                        txtUserMod.ReadOnly = true;
                        txtFechaMod.ReadOnly = true;
                        txtMontoPlanilla.ReadOnly = true;

                        //Cargado los texbox editabls
                        txtConductor.ReadOnly = true;
                        txtAyudante.ReadOnly = true;
                        txtCliente.ReadOnly = true;
                        txtProducto.ReadOnly = true;
                        txtRuta.ReadOnly = true;
                        txtConductorInicio.ReadOnly = true;
                        //tmePrimerCambio.Enabled = false;
                        // tmeSegCambio.Enabled = false;
                        //tmeTerCambio.Enabled = false;
                        txtOT.ReadOnly = true;
                        txtMontoViaticos.ReadOnly = true;
                        txtOrdServicio.ReadOnly = true;
                        txtUnidadApoyo.ReadOnly = true;
                        txtGuiaEntrega.ReadOnly = true;
                        txtCostoOrdenServicio.ReadOnly = true;
                        txtMaquinaria.ReadOnly = true;
                        txtCodTolvas.ReadOnly = true;
                        txtFecharegistro.ReadOnly = true;
                        btnVerOt.Enabled = false;
                        btnMantDestino.Enabled = false;
                    }

                    if (_Accesos == 1 || _Estado == 10)
                    {
                        checkBox1.Enabled = false;
                        btnAgregar.Enabled = false;
                    }
                    if (_tipounidad.Equals("T"))
                    {
                        lblCostoOrdServicio.Visible = true;
                        txtCostoOrdenServicio.Visible = true;
                        txtOrdServicio.Visible = true;
                        lblTipoUnidad.Text = "Unidad: Tercera";
                    }
                    else if (_tipounidad.Equals("P") || _tipounidad.Equals("C"))
                    {
                        lblTipoUnidad.Text = "Unidad: Propia";
                    }

                    if (cboPartida.Visible == true && _Ot > 0)
                    {


                        if (R_PROGRAMACION.Equals("GENERAL") || R_PROGRAMACION.Equals("LINDLEY") || R_PROGRAMACION.Equals("LIMAGAS") || R_PROGRAMACION.Equals("TOLVAS"))
                        {
                            DataTable direccionesPartida = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_Transportista_GuiaEelectronica(_Cliente, _Ruta, 0, 0);
                            DataTable direccionesDestino = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_Transportista_GuiaEelectronica(_Cliente, _Ruta, 0, 0);

                            cboPartida.DisplayMember = "Direccion";
                            cboPartida.ValueMember = "Secuencia";
                            cboPartida.DataSource = direccionesPartida;

                            cboLlegada.DisplayMember = "Direccion";
                            cboLlegada.ValueMember = "Secuencia";
                            cboLlegada.DataSource = direccionesDestino;
                        }
                        else
                        {
                            DataTable dtup1 = new DataTable();
                            DataTable dtup2 = new DataTable();

                            dtup1 = clsOperacionesBL.Instancia.GetOperaciones_ListarDireccionesxOts(_Ot);
                            dtup2 = clsOperacionesBL.Instancia.GetOperaciones_ListarDireccionesxOts(_Ot);

                            cboPartida.DisplayMember = "Direccion";
                            cboPartida.ValueMember = "Secuencia";
                            cboPartida.DataSource = dtup1;

                            cboLlegada.DisplayMember = "Direccion";
                            cboLlegada.ValueMember = "Secuencia";
                            cboLlegada.DataSource = dtup2;
                        }

                        

                    }

                    if (_UbigeoPartida > 0)
                    {
                        cboPartida.SelectedValue= _UbigeoPartida ;
                    }
                    if (_UbigeoLlegada > 0)
                    {
                        cboLlegada.SelectedValue = _UbigeoLlegada ;
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        public void CargarComboTiempo()
        {
            DataTable dtTiempo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarTiempos();
            cbxTiempo.DataSource = dtTiempo;
            cbxTiempo.DisplayMember = "Descripcion";
            cbxTiempo.ValueMember = "idTiempo";
        }

        private void CargarControlesPreviajes()
        {

            DataTable dtEstado = clsOperacionesBL.Instancia.Llenar_ControlesPreViajes();

            if (dtEstado != null || dtEstado.Rows.Count > 0)
            {
                comboBox1.DataSource = dtEstado;
                comboBox1.DisplayMember = "Descripcion";
                comboBox1.ValueMember = "Descripcion";

                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("No se puedo cargar controles");
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
            f1.Val_Respuesta = "0";
            this.Close();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            //CARGADO DEL CONDUCTOR
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                if (txtMaquinaria.Visible == true)
                {
                    clsVisuales.Instancia.LlenarLw(lisview, clsConsultaBL.Instancia.GetConductores(txtConductor.Text), true, false, false);

                    lisview.Columns[0].Width = 0;
                    lisview.Columns[1].Width = 206;
                    lisview.Columns[2].Width = 0;
                    lisview.Columns[3].Width = 110;

                    lisview.Size = new System.Drawing.Size(351, 103);

                    lisview.BringToFront();
                    lisview.Visible = true;
                    lisview.Focus();
                }
                else
                {
                    clsVisuales.Instancia.LlenarLw(lisview, clsConsultaBL.Instancia.GetConductores(txtConductor.Text), true, false, false);

                    lisview.Columns[0].Width = 0;
                    lisview.Columns[1].Width = 206;
                    lisview.Columns[2].Width = 0;
                    lisview.Columns[3].Width = 110;

                    lisview.Size = new System.Drawing.Size(351, 103);

                    lisview.BringToFront();
                    lisview.Visible = true;
                    lisview.Focus();
                }
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lisview.Visible = false;
                txtConductor.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lisview.Visible = false;
                txtConductor.Focus();
                idConductor = -1;
            }
        }


        private void CargarConsolidado()
        {
            
            dgvConsolidado.Size = new System.Drawing.Size(529, 126);
            DataTable dtCargarConsolidados = new DataTable();
            dtCargarConsolidados = clsOperacionesBL.Instancia.GetOperaciones_ListarConsolidados(Convert.ToInt32(_Viaje));
            dgvConsolidado.DataSource = null;
            if (dtCargarConsolidados.Rows.Count > 0)
            {
                dgvConsolidado.Visible = true;
                dgvConsolidado.DataSource = dtCargarConsolidados;
                dgvConsolidado.RowHeadersVisible = false;
                checkBox1.Checked = true;
                dgvConsolidado.Columns["Persona"].Visible = false;
                dgvConsolidado.Columns["IdRuta"].Visible = false;
            }
        }

        private void lisview_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lisview.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    // int idConductor;
                    ItemActual = lisview.SelectedItems[0];

                    idConductor = Convert.ToInt32(ItemActual.Text);
                    
                    DataTable dtContador = new DataTable();
                    string Respuesta;
                    dtContador = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ContadorPlanillasPendientes(idConductor, idProgramacion);
                    Respuesta = Convert.ToString(dtContador.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        //MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // IdConductor.Text = Convert.ToString(idConductor);
                        txtConductor.Text = ItemActual.SubItems[1].Text;
                        txtConductorInicio.Text = ItemActual.SubItems[1].Text;

                        lisview.Visible = false;
                        if (txtMaquinaria.Visible == true) { txtObservacion.Focus(); }
                        else { txtAyudante.Focus(); }

                        lblRelacion.Text = txtConductor.Text;

                        DataTable ListaDocumentos = new DataTable();
                        ListaDocumentos = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarDocumentos(2, idConductor, idProgramacion);

                        if (ListaDocumentos.Rows.Count > 0)
                        {
                            dgvVencimientos.DataSource = ListaDocumentos;
                            dgvVencimientosVista.BestFitColumns();
                        }

                        pDocumentosRegistrados.Visible = true;
                        pDocumentosRegistrados.BringToFront();

                        //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
                        DataTable ValidaUnidadVencida = new DataTable();
                        ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idConductor, "CONDUCTOR");

                        if (ValidaUnidadVencida.Rows.Count > 0)
                        {
                            grbControlDoc.Visible = true;
                            grbControlDoc.Size = new System.Drawing.Size(420, 150);
                            lsvControlDoc.Visible = true;
                            btnCerrarControlDoc.Visible = true;
                            btnGenerarReq.Visible = false;

                            clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                            lsvControlDoc.Columns[0].Width = 0;
                            lsvControlDoc.Columns[1].Width = 0;
                            lsvControlDoc.Columns[2].Width = 0;
                            lsvControlDoc.Columns[3].Width = 190;
                            lsvControlDoc.Columns[4].Width = 60;
                            lsvControlDoc.Columns[5].Width = 80;
                            lsvControlDoc.Columns[6].Width = 85;
                            lsvControlDoc.Columns[7].Width = 0;
                            lsvControlDoc.Columns[8].Width = 0;
                            lsvControlDoc.Columns[9].Width = 0;

                            lsvControlDoc.Size = new System.Drawing.Size(415, 95);

                            lsvControlDoc.BringToFront();
                            lsvControlDoc.Visible = true;
                            //  lsvControlDoc.Focus();
                        }

                        // CONSULTA PARA VALIDAR SI EL TRACTO ESTÁ ASIGNADO AL CONDUCTOR
                        if ((idtracto > 0 || _Tracto > 0) && idConductor > 0)
                        {
                            try
                            {
                                DataTable ValidarAsigna = new DataTable();
                                if (idtracto > 0) { ValidarAsigna = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Programacion_RevisarAsignaciones(idtracto, idConductor, idProgramacion); }
                                else { ValidarAsigna = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Programacion_RevisarAsignaciones(_Tracto, idConductor, idProgramacion); }

                                if (ValidarAsigna.Rows.Count > 0)
                                {
                                    string respta = Convert.ToString(ValidarAsigna.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0") { lisview.Visible = false; }
                                    else
                                    {
                                        MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        lisview.Visible = false;
                                        idConductor = -1;
                                        txtConductor.Text = "";
                                        txtConductor.Focus();
                                    }
                                }
                                else { lisview.Visible = false; }
                            }
                            catch
                            {
                                lisview.Visible = false;
                                idConductor = -1;
                                txtConductor.Text = "";
                                txtConductor.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        idConductor = -1;
                        txtConductor.Clear();
                        lisview.Visible = false;
                        txtConductor.Focus();
                    }
                }

                if (e.KeyChar == (char)Keys.Enter)
                {
                    if ((idtracto > 0 || _Tracto > 0) && (idConductor > 0) && (idRuta > 0 || _Ruta > 0))
                    {
                        DataTable RendCombustible = new DataTable();
                        if (idtracto > 0) { RendCombustible = clsOperacionesBL.Instancia.ReportesApp_Combustible_Rendimientos_BuscarUltimoRendimiento(idtracto, idConductor, idRuta, idProgramacion); }
                        else { RendCombustible = clsOperacionesBL.Instancia.ReportesApp_Combustible_Rendimientos_BuscarUltimoRendimiento(_Tracto, idConductor, _Ruta, idProgramacion); }

                        if (RendCombustible.Rows.Count > 0)
                        {
                            if (Convert.ToDecimal(RendCombustible.Rows[0]["REND KM/GL"]) > Convert.ToDecimal(RendCombustible.Rows[0]["REND.PROM"]))
                            {
                                txtUltViaje.Text = RendCombustible.Rows[0]["CodViaje"].ToString();
                                txtRendProm.Text = RendCombustible.Rows[0]["REND.PROM"].ToString();
                                txtRendKM.Text = RendCombustible.Rows[0]["REND KM/GL"].ToString();

                                lblBConductor.Text = RendCombustible.Rows[0]["Nombre"].ToString();
                                lblBPlaca.Text = RendCombustible.Rows[0]["NumeroPlaca"].ToString();

                                lblMensaje.ForeColor = Color.Red;
                                lblMensaje.Text = "El rendimiento del último viaje del\r\nconductor ha sido mayor al promedio.";
                                btnHistorial.Enabled = true;

                                pRendComb.Visible = true;
                                pRendComb.BringToFront();
                            }
                        }
                    }
                }

                if (e.KeyChar == (char)Keys.Escape)
                {
                    lisview.Visible = false;
                    txtConductor.Focus();
                }

                if (e.KeyChar == (char)Keys.Back)
                {
                    lisview.Visible = false;
                    txtConductor.Focus();
                    idConductor = -1;
                }
            }
            catch (Exception)
            {

            }
        }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstTracto, clsConsultaBL.Instancia.GetUnidades(txtTracto.Text), true, false, false);

                lstTracto.Columns[0].Width = 0;
                lstTracto.Columns[1].Width = 80;
                lstTracto.Columns[2].Width = 50;
                lstTracto.Columns[3].Width = 120;

                lstTracto.Size = new System.Drawing.Size(260, 103);

                lstTracto.BringToFront();
                lstTracto.Visible = true;
                lstTracto.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstTracto.Visible = false;
                txtTracto.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTracto.Visible = false;
                txtTracto.Focus();
                idtracto = -1;
            }
        }

        private void txtRemolque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstRemolque, clsConsultaBL.Instancia.GetUnidades(txtRemolque.Text), true, false, false);

                lstRemolque.Columns[0].Width = 0;
                lstRemolque.Columns[1].Width = 80;
                lstRemolque.Columns[2].Width = 50;
                lstRemolque.Columns[3].Width = 120;

                lstRemolque.Size = new System.Drawing.Size(280, 103);

                lstRemolque.BringToFront();
                lstRemolque.Visible = true;
                lstRemolque.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstRemolque.Visible = false;
                txtRemolque.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstRemolque.Visible = false;
                txtRemolque.Focus();
                idremolque = -1;
            }
        }

        private void txtAyudante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstApoyo, clsConsultaBL.Instancia.GetConductores(txtAyudante.Text), true, false, false);

                lstApoyo.Columns[0].Width = 0;
                lstApoyo.Columns[1].Width = 206;
                lstApoyo.Columns[2].Width = 0;
                lstApoyo.Columns[3].Width = 110;

                lstApoyo.Size = new System.Drawing.Size(280, 103);

                lstApoyo.BringToFront();
                lstApoyo.Visible = true;
                lstApoyo.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstApoyo.Visible = false;
                txtAyudante.Focus();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstCliente, clsConsultaBL.Instancia.GetPersona(txtCliente.Text), true, false, false);

                lstCliente.Columns[0].Width = 0;
                lstCliente.Columns[1].Width = 206;
                lstCliente.Columns[2].Width = 110;

                lstCliente.Size = new System.Drawing.Size(351, 103);

                lstCliente.BringToFront();
                lstCliente.Visible = true;
                lstCliente.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstCliente.Visible = false;
                txtCliente.Focus();
            }
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                
                clsVisuales.Instancia.LlenarLw(lstProducto, clsConsultaBL.Instancia.GetProductos(txtProducto.Text), true, false, false);

                lstProducto.Columns[0].Width = 0;
                lstProducto.Columns[1].Width = 60;
                lstProducto.Columns[2].Width = 250;

                lstProducto.Size = new System.Drawing.Size(351, 103);

                lstProducto.BringToFront();
                lstProducto.Visible = true;
                lstProducto.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstProducto.Visible = false;
                txtProducto.Focus();
            }
        }

        private void lstTracto_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lstTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstTracto.Items.Count.Equals(0))
            {
                if (txtMaquinaria.Visible == true)
                {
                    ListViewItem ItemActual;
                    // int idtracto;
                    ItemActual = lstTracto.SelectedItems[0];

                    idtracto = Convert.ToInt32(ItemActual.Text);
                    // IdConductor.Text = Convert.ToString(idtracto);
                    txtMaquinaria.Text = ItemActual.SubItems[1].Text;
                    string tipo = ItemActual.SubItems[2].Text;
                    lstTracto.Visible = false;
                    txtRemolque.Focus();

                    //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
                    DataTable ValidaUnidadVencida = new DataTable();
                    ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idtracto, "UNIDAD");

                    if (ValidaUnidadVencida.Rows.Count > 0)
                    {
                        idRelacion = idtracto;

                        grbControlDoc.Visible = true;
                        grbControlDoc.Size = new System.Drawing.Size(285, 150);
                        lsvControlDoc.Visible = true;
                        btnCerrarControlDoc.Visible = true;
                        btnGenerarReq.Visible = true;

                        clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                        lsvControlDoc.Columns[0].Width = 0;
                        lsvControlDoc.Columns[1].Width = 60;
                        lsvControlDoc.Columns[2].Width = 250;
                        lsvControlDoc.Columns[7].Width = 0;
                        lsvControlDoc.Columns[8].Width = 0;
                        lsvControlDoc.Columns[9].Width = 0;

                        lsvControlDoc.Size = new System.Drawing.Size(275, 103);

                        lsvControlDoc.BringToFront();
                        lsvControlDoc.Visible = true;
                    }
                }
                else
                {
                    ListViewItem ItemActual;
                    // int idtracto;
                    ItemActual = lstTracto.SelectedItems[0];

                    idtracto = Convert.ToInt32(ItemActual.Text);
                    // IdConductor.Text = Convert.ToString(idtracto);
                    txtTracto.Text = ItemActual.SubItems[1].Text;
                    string tipo = ItemActual.SubItems[2].Text;
                    if (tipo.Equals("P") || tipo.Equals("C"))
                    {
                        lblTipoUnidad.Text = "Unidad: Propia";
                        txtOrdServicio.Visible = false;
                        lblOrdenServicio.Visible = false;
                        lblCostoOrdServicio.Visible = false;
                        txtCostoOrdenServicio.Visible = false;
                        txtCostoOrdenServicio.Text = "";
                        label24.Text = "";
                    }
                    else
                    {
                        lblTipoUnidad.Text = "Unidad: Tercera";
                        txtOrdServicio.Visible = true;
                        lblOrdenServicio.Visible = true;
                        lblCostoOrdServicio.Visible = true;
                        txtCostoOrdenServicio.Visible = true;
                    }

                    //Consulta para validar si la unidad contiene un programacion...
                    DataTable ValidaUnidad = new DataTable();
                    ValidaUnidad = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_ValidarUnidad(idtracto, dtFechaInicio.Text);

                    if (ValidaUnidad.Rows.Count > 0)
                    {
                        for (int i = 0; i < ValidaUnidad.Rows.Count; i++)
                        {
                            string fechavalida = ValidaUnidad.Rows[i]["FechaFin"].ToString();
                            string programacion = ValidaUnidad.Rows[i]["Programacion"].ToString();
                            if (fechavalida.Equals("0"))
                            {
                                lstTracto.Visible = false;
                                txtRemolque.Focus();
                            }
                            else
                            {
                                MessageBox.Show("La unidad " + txtTracto.Text + ", contiene una programación " + programacion + " hasta el: " + fechavalida, "ADVERTENCIA");
                                lstTracto.Visible = false;
                                idtracto = -1;
                                txtTracto.Text = "";
                                txtTracto.Focus();
                            }
                        }
                    }
                    else
                    {
                        lstTracto.Visible = false;
                        txtRemolque.Focus();
                    }

                    lblRelacion.Text = txtTracto.Text;

                    DataTable ListaDocumentos = new DataTable();
                    ListaDocumentos = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarDocumentos(1, idtracto, idProgramacion);

                    if (ListaDocumentos.Rows.Count > 0)
                    {
                        dgvVencimientos.DataSource = ListaDocumentos;
                        dgvVencimientosVista.BestFitColumns();

                        pDocumentosRegistrados.Visible = true;
                        pDocumentosRegistrados.BringToFront();
                    }

                    lblPlaca2.Text = txtTracto.Text;
                    idUnidadMtto = idtracto;

                    DataTable ListaMttoC = new DataTable();
                    ListaMttoC = clsOperacionesBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarPendientes(idUnidadMtto);

                    if (ListaMttoC.Rows.Count > 0)
                    {
                        dtgMttoCorrectivo.DataSource = ListaMttoC;

                        dgvMttoCorrectivoView.Columns["idMttoC"].Visible = false;
                        dgvMttoCorrectivoView.Columns["IdVehiculo"].Visible = false;
                        dgvMttoCorrectivoView.Columns["Placa"].Visible = false;
                        dgvMttoCorrectivoView.Columns["ESTADO"].Visible = false;

                        dgvMttoCorrectivoView.Columns["FECHA_PROGRAMACION"].DisplayFormat.FormatType = FormatType.DateTime;
                        dgvMttoCorrectivoView.Columns["FECHA_PROGRAMACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                        dgvMttoCorrectivoView.BestFitColumns();

                        pMttoCorrectivo.Visible = true;
                        pMttoCorrectivo.BringToFront();
                    }

                    // CONSULTA PARA VALIDAR SI EL TRACTO ESTÁ ASIGNADO AL CONDUCTOR
                    if (idtracto > 0 && (idConductor > 0 || _Conductor > 0))
                    {
                        try
                        {
                            DataTable ValidarAsigna = new DataTable();
                            if (idConductor > 0) { ValidarAsigna = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Programacion_RevisarAsignaciones(idtracto, idConductor, idProgramacion); }
                            else { ValidarAsigna = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Programacion_RevisarAsignaciones(idtracto, _Conductor, idProgramacion); }

                            if (ValidarAsigna.Rows.Count > 0)
                            {
                                string respta = Convert.ToString(ValidarAsigna.Rows[0]["exito"]);
                                string NroRspta = respta.Substring(0, 1);
                                if (NroRspta == "0")
                                {
                                    lstTracto.Visible = false;
                                    txtRemolque.Focus();
                                }
                                else
                                {
                                    MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    lstTracto.Visible = false;
                                    idtracto = -1;
                                    txtTracto.Text = "";
                                    txtTracto.Focus();
                                }
                            }
                            else
                            {
                                lstTracto.Visible = false;
                                txtRemolque.Focus();
                            }
                        }
                        catch
                        {
                            lstTracto.Visible = false;
                            idtracto = -1;
                            txtTracto.Text = "";
                            txtTracto.Focus();
                        }
                    }

                    //CONSULTA PARA VALIDAR SI EL ÚLTIMO VIAJE ATENDIDO TIENE FECHA DE TERMINO
                    try
                    {
                        DataTable UltimoAtendido = new DataTable();
                        UltimoAtendido = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Programacion_UltimoViajeCompletado(idtracto, idProgramacion);

                        if (UltimoAtendido.Rows.Count > 0)
                        {
                            string respta = Convert.ToString(UltimoAtendido.Rows[0]["exito"]);
                            string NroRspta = respta.Substring(0, 1);
                            if (NroRspta == "0")
                            {
                                lstTracto.Visible = false;
                                txtRemolque.Focus();
                            }
                            else
                            {
                                MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                lstTracto.Visible = false;
                                idtracto = -1;
                                txtTracto.Text = "";
                                txtTracto.Focus();
                            }
                        }
                        else
                        {
                            lstTracto.Visible = false;
                            txtRemolque.Focus();
                        }
                    }
                    catch
                    {
                        lstTracto.Visible = false;
                        idtracto = -1;
                        txtTracto.Text = "";
                        txtTracto.Focus();
                    }

                    //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
                    DataTable ValidaUnidadVencida = new DataTable();
                    ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idtracto, "UNIDAD");

                    if (ValidaUnidadVencida.Rows.Count > 0)
                    {
                        idRelacion = idtracto;

                        grbControlDoc.Visible = true;
                        grbControlDoc.Size = new System.Drawing.Size(420, 150);
                        lsvControlDoc.Visible = true;
                        btnCerrarControlDoc.Visible = true;
                        btnGenerarReq.Visible = true;

                        clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                        lsvControlDoc.Columns[0].Width = 0;
                        lsvControlDoc.Columns[1].Width = 0;
                        lsvControlDoc.Columns[2].Width = 0;
                        lsvControlDoc.Columns[3].Width = 190;
                        lsvControlDoc.Columns[4].Width = 60;
                        lsvControlDoc.Columns[5].Width = 80;
                        lsvControlDoc.Columns[6].Width = 85;
                        lsvControlDoc.Columns[7].Width = 0;
                        lsvControlDoc.Columns[8].Width = 0;
                        lsvControlDoc.Columns[9].Width = 0;

                        lsvControlDoc.Size = new System.Drawing.Size(415, 95);

                        lsvControlDoc.BringToFront();
                        lsvControlDoc.Visible = true;
                        //  lsvControlDoc.Focus();
                    }
                }             
            }

            if (e.KeyChar == (char)Keys.Enter)
            {
                if ((idtracto > 0) && (idConductor > 0 || _Conductor > 0) && (idRuta > 0 || _Ruta > 0))
                {
                    DataTable RendCombustible = new DataTable();
                    if (idConductor > 0) { RendCombustible = clsOperacionesBL.Instancia.ReportesApp_Combustible_Rendimientos_BuscarUltimoRendimiento(idtracto, idConductor, idRuta, idProgramacion); }
                    else { RendCombustible = clsOperacionesBL.Instancia.ReportesApp_Combustible_Rendimientos_BuscarUltimoRendimiento(idtracto, _Conductor, _Ruta, idProgramacion); }

                    if (RendCombustible.Rows.Count > 0)
                    {
                        if (Convert.ToDecimal(RendCombustible.Rows[0]["REND KM/GL"]) > Convert.ToDecimal(RendCombustible.Rows[0]["REND.PROM"]))
                        {
                            txtUltViaje.Text = RendCombustible.Rows[0]["CodViaje"].ToString();
                            txtRendProm.Text = RendCombustible.Rows[0]["REND.PROM"].ToString();
                            txtRendKM.Text = RendCombustible.Rows[0]["REND KM/GL"].ToString();

                            lblBConductor.Text = RendCombustible.Rows[0]["Nombre"].ToString();
                            lblBPlaca.Text = RendCombustible.Rows[0]["NumeroPlaca"].ToString();

                            lblMensaje.ForeColor = Color.Red;
                            lblMensaje.Text = "El rendimiento del último viaje del\r\nconductor ha sido mayor al promedio.";
                            btnHistorial.Enabled = true;

                            pRendComb.Visible = true;
                            pRendComb.BringToFront();
                        }
                    }
                }

                if (idtracto > 0 && (idRuta > 0 || _Ruta > 0) && lblTipoUnidad.Text.Contains("Propia"))
                {
                    DataTable KMUnidades = new DataTable();
                    if (idRuta > 0) { KMUnidades = clsOperacionesBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades(idtracto, idRuta, txtSucursal.Text); }
                    else { KMUnidades = clsOperacionesBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades(idtracto, _Ruta, txtSucursal.Text); }

                    if (KMUnidades.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(KMUnidades.Rows[0]["ESTADO"]) == 0)
                        {
                            lblPlaca.Text = txtTracto.Text;
                            lblProxMtto.Text = KMUnidades.Rows[0]["PROXIMO"].ToString();

                            if (Convert.ToDecimal(KMUnidades.Rows[0]["TRACTO"]) > 0) { txtKMRestante.Text = KMUnidades.Rows[0]["TRACTO"].ToString(); }
                            else { txtKMRestante.Text = "0.00"; }

                            txtKMRuta.Text = KMUnidades.Rows[0]["RUTA"].ToString();

                            pKMUnidades.Visible = true;
                            pKMUnidades.BringToFront();
                            button1.Enabled = false;
                        }
                        else
                        {
                            button1.Enabled = true;

                            // CONSULTA PARA DETERMINAR SI UNA UNIDAD TIENE ASIGNADO UN KIT DE NEUMÁTICOS
                            try
                            {
                                DataTable KitAsignado = new DataTable();
                                if (idRuta > 0) { KitAsignado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico(idtracto, idRuta, textBox1.Text); }
                                else { KitAsignado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico(idtracto, _Ruta, textBox1.Text); }

                                if (KitAsignado.Rows.Count > 0)
                                {
                                    string respta = Convert.ToString(KitAsignado.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0")
                                    {
                                        lstTracto.Visible = false;
                                        txtRemolque.Focus();
                                    }
                                    else
                                    {
                                        MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        lstTracto.Visible = false;
                                        idtracto = -1;
                                        txtTracto.Clear();
                                        txtTracto.Focus();
                                    }
                                }
                                else
                                {
                                    lstTracto.Visible = false;
                                    txtRemolque.Focus();
                                }
                            }
                            catch
                            {
                                lstTracto.Visible = false;
                                idtracto = -1;
                                txtTracto.Clear();
                                txtTracto.Focus();
                            }
                        }
                    }
                }
                else { button1.Enabled = true; }

                if (idtracto > 0 && textBox1.Text != "COMBUSTIBLE")
                {
                    DataTable UnidadBloq = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(idtracto);
                    string Rpta;

                    if (UnidadBloq.Rows.Count > 0)
                    {
                        Rpta = Convert.ToString(UnidadBloq.Rows[0]["exito"]);
                        MessageBox.Show(Rpta, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        lblTipoUnidad.Text = "";
                        txtTracto.Clear();
                        idtracto = 0;
                        txtTracto.Focus();
                        return;
                    }

                    // Consulta para verificar si las Inspecciones Técnicas están por vencer
                    if (textBox1.Text == "LINDLEY" || textBox1.Text == "LIMAGAS" || textBox1.Text == "GENERAL" || textBox1.Text == "TOLVAS")
                    {
                        DataTable UnidadVencida2 = new DataTable();
                        UnidadVencida2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarInspeccionesVencidas(idtracto, "UNIDAD");

                        if (UnidadVencida2.Rows.Count > 0)
                        {
                            MessageBox.Show("No puede seleccionar la unidad " + txtTracto.Text + " porque tiene documentos por vencer o ya están vencidos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            lblTipoUnidad.Text = "";
                            txtTracto.Clear();
                            idtracto = 0;
                            txtTracto.Focus();
                            return;
                        }
                    }
                }
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstTracto.Visible = false;
                txtTracto.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTracto.Visible = false;
                txtTracto.Focus();
                idtracto = -1;
            }
        }

        private void lstRemolque_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstRemolque.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                // int idremolque;
                ItemActual = lstRemolque.SelectedItems[0];

                idremolque = Convert.ToInt32(ItemActual.Text);
                // IdConductor.Text = Convert.ToString(idtracto);
                txtRemolque.Text = ItemActual.SubItems[1].Text;

                lstRemolque.Visible = false;
                txtConductor.Focus();

                lblRelacion.Text = txtRemolque.Text;

                DataTable ListaDocumentos = new DataTable();
                ListaDocumentos = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarDocumentos(1, idremolque, idProgramacion);

                if (ListaDocumentos.Rows.Count > 0)
                {
                    dgvVencimientos.DataSource = ListaDocumentos;
                    dgvVencimientosVista.BestFitColumns();

                    pDocumentosRegistrados.Visible = true;
                    pDocumentosRegistrados.BringToFront();
                }

                lblPlaca2.Text = txtRemolque.Text;
                idUnidadMtto = idremolque;

                DataTable ListaMttoC = new DataTable();
                ListaMttoC = clsOperacionesBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarPendientes(idUnidadMtto);

                if (ListaMttoC.Rows.Count > 0)
                {
                    dtgMttoCorrectivo.DataSource = ListaMttoC;

                    dgvMttoCorrectivoView.Columns["idMttoC"].Visible = false;
                    dgvMttoCorrectivoView.Columns["IdVehiculo"].Visible = false;
                    dgvMttoCorrectivoView.Columns["Placa"].Visible = false;
                    dgvMttoCorrectivoView.Columns["ESTADO"].Visible = false;

                    dgvMttoCorrectivoView.Columns["FECHA_PROGRAMACION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMttoCorrectivoView.Columns["FECHA_PROGRAMACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvMttoCorrectivoView.BestFitColumns();

                    pMttoCorrectivo.Visible = true;
                    pMttoCorrectivo.BringToFront();
                }

                //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
                DataTable ValidaUnidadVencida = new DataTable();
                ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idremolque, "UNIDAD");

                if (ValidaUnidadVencida.Rows.Count > 0)
                {
                    idRelacion = idremolque;

                    grbControlDoc.Visible = true;
                    grbControlDoc.Size = new System.Drawing.Size(420, 150);
                    lsvControlDoc.Visible = true;
                    btnCerrarControlDoc.Visible = true;
                    btnGenerarReq.Visible = true;

                    clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                    lsvControlDoc.Columns[0].Width = 0;
                    lsvControlDoc.Columns[1].Width = 0;
                    lsvControlDoc.Columns[2].Width = 0;
                    lsvControlDoc.Columns[3].Width = 190;
                    lsvControlDoc.Columns[4].Width = 60;
                    lsvControlDoc.Columns[5].Width = 80;
                    lsvControlDoc.Columns[6].Width = 85;
                    lsvControlDoc.Columns[7].Width = 0;
                    lsvControlDoc.Columns[8].Width = 0;
                    lsvControlDoc.Columns[9].Width = 0;

                    lsvControlDoc.Size = new System.Drawing.Size(415, 95);

                    lsvControlDoc.BringToFront();
                    lsvControlDoc.Visible = true;
                    //  lsvControlDoc.Focus();
                }
            }

            if (idremolque > 0 && textBox1.Text != "COMBUSTIBLE")
            {
                DataTable UnidadBloq = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarUnidadBloqueada(idremolque);
                string Rpta;

                if (UnidadBloq.Rows.Count > 0)
                {
                    Rpta = Convert.ToString(UnidadBloq.Rows[0]["exito"]);
                    MessageBox.Show(Rpta, "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtRemolque.Clear();
                    idremolque = 0;
                    txtRemolque.Focus();
                    return;
                }

                // Consulta para verificar si las Inspecciones Técnicas están por vencer
                DataTable UnidadVencida2 = new DataTable();
                UnidadVencida2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_BuscarInspeccionesVencidas(idremolque, "UNIDAD");

                if (UnidadVencida2.Rows.Count > 0)
                {
                    MessageBox.Show("No puede seleccionar la unidad " + txtRemolque.Text + " porque tiene documentos por vencer o ya están vencidos.", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtRemolque.Clear();
                    idremolque = 0;
                    txtRemolque.Focus();
                    return;
                }
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstRemolque.Visible = false;
                txtRemolque.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstRemolque.Visible = false;
                txtRemolque.Focus();
                idremolque = -1;
            }
        }

        private void lstApoyo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstApoyo.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                // int idapoyo;
                ItemActual = lstApoyo.SelectedItems[0];

                idapoyo = Convert.ToInt32(ItemActual.Text);
                // IdConductor.Text = Convert.ToString(idtracto);
                txtAyudante.Text = ItemActual.SubItems[1].Text;

                lstApoyo.Visible = false;
                txtConductorInicio.Focus();

                //Consulta para validar si la unidad contiene un DOCUMETOS VNCIDOS...
                DataTable ValidaUnidadVencida = new DataTable();
                ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idapoyo, "CONDUCTOR");

                if (ValidaUnidadVencida.Rows.Count > 0)
                {
                    grbControlDoc.Visible = true;
                    grbControlDoc.Size = new System.Drawing.Size(420, 150);
                    lsvControlDoc.Visible = true;
                    btnCerrarControlDoc.Visible = true;
                    btnGenerarReq.Visible = false;

                    clsVisuales.Instancia.LlenarLw(lsvControlDoc, ValidaUnidadVencida, true, false, false);

                    lsvControlDoc.Columns[0].Width = 0;
                    lsvControlDoc.Columns[1].Width = 0;
                    lsvControlDoc.Columns[2].Width = 0;
                    lsvControlDoc.Columns[3].Width = 190;
                    lsvControlDoc.Columns[4].Width = 60;
                    lsvControlDoc.Columns[5].Width = 80;
                    lsvControlDoc.Columns[6].Width = 85;
                    lsvControlDoc.Columns[7].Width = 0;
                    lsvControlDoc.Columns[8].Width = 0;
                    lsvControlDoc.Columns[9].Width = 0;

                    lsvControlDoc.Size = new System.Drawing.Size(415, 95);

                    lsvControlDoc.BringToFront();
                    lsvControlDoc.Visible = true;
                    //  lsvControlDoc.Focus();
                }
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstApoyo.Visible = false;
                txtAyudante.Focus();
            }
        }

        private void lstCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstCliente.SelectedItems[0];
                idCliente = Convert.ToInt32(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;

                lstCliente.Visible = false;
                txtProducto.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstCliente.Visible = false;
                txtCliente.Focus();
            }
        }

        private void lstProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstProducto.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstProducto.SelectedItems[0];
                idProducto = Convert.ToInt32(ItemActual.Text);
                txtProducto.Text = ItemActual.SubItems[2].Text;

                lstProducto.Visible = false;
                txtPesoAlmacen.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstProducto.Visible = false;
                txtProducto.Focus();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (_Accesos == 2)
            {
                //Datos a validar al crear un nuevo previaje
                if (crea1_modifi2_anula3 == 1)
                {
                    TipoProgramacion = R_IdProgramacionPrincipal;
                }

                if (lblDias.Text.Equals(""))
                {
                    lblDias.Text = "0";
                }
                _EsInterna = "0";
                _PlacaMaquinaria = txtMaquinaria.Text;

                //Datos a validar al Modificar un previaje
                if (crea1_modifi2_anula3 == 2)
                {
                    R_IdProgramacionPrincipal = _IdProg;
                    TipoProgramacion = _TipoProg;

                    if (txtTracto.Text.Equals(_DescTracto)) {idtracto = _Tracto;}
                    if ( txtRemolque.Text.Equals(_DesRemolque)) { idremolque = _Remolque;}
                    if (txtConductor.Text.Equals(_nomConductor))
                    {
                        idConductor = _Conductor;
                    }
                    if (txtAyudante.Text.Equals(_nomApoyo))
                    {
                        idapoyo = _Apoyo;
                    }
                    if (txtCliente.Text.Equals(_nomCliente))
                    {
                        idCliente = _Cliente;
                    }
                    if ( txtRuta.Text.Equals(_nomRuta))
                    {
                        idRuta = _Ruta;
                    }
                    if ( txtProducto.Text.Equals(_nomProducto))
                    {
                        idProducto = _Producto;
                    }
                }
                decimal PesoAlmacen;
                decimal PesoCliente;
                decimal Viatico;
                int EstadoProgramacionRegistrar = 0;
                if (txtPesoAlmacen.Text.Equals(""))
                {
                    PesoAlmacen = 0;
                }
                else
                {
                    PesoAlmacen = Convert.ToDecimal(txtPesoAlmacen.Text);
                }
                if (txtPesoClientes.Text.Equals(""))
                {
                    PesoCliente = 0;
                }
                else
                {
                    PesoCliente = Convert.ToDecimal(txtPesoClientes.Text);
                }

                if (txtCostoOrdenServicio.Text.Equals(""))
                {
                    montoOrdenServicio = 0;
                }
                else
                {
                    montoOrdenServicio = Convert.ToDecimal(txtCostoOrdenServicio.Text);
                }

                if (PesoCliente > 0)
                {
                    Merma = PesoCliente - PesoAlmacen;
                }
                else
                {
                    Merma = 0;
                }
                if (txtMontoViaticos.Text.Equals(""))
                {
                    Viatico = 0;
                }
                else
                {
                    Viatico = Convert.ToDecimal(txtMontoViaticos.Text);
                }
                int valOt;
                if (txtOT.Text.Equals(""))
                {
                    valOt = 0;
                }
                else
                {
                    if (lblDias.Text.Length == 0 || txtMontoViaticos.Text.Length == 0)
                    {
                        MessageBox.Show("Es obligatorio ingresar el gasto de esta ruta en el sistema.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else { valOt = Convert.ToInt32(txtOT.Text); }
                }

                /*
                DataTable dtSolicitud = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ObtenerEstado(txtTracto.Text);
                DataTable dtSolicitud2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ObtenerEstado(txtRemolque.Text);

                if (dtSolicitud.Rows.Count > 0)
                {
                    EstadoP = dtSolicitud.Rows[0]["ESTADO"].ToString();
                    UnidadFallaP = dtSolicitud.Rows[0]["FALLA"].ToString();
                }
                else
                {
                    EstadoP = "COMPLETADO";
                    UnidadFallaP = "";
                }

                if (dtSolicitud2.Rows.Count > 0)
                {
                    EstadoR = dtSolicitud2.Rows[0]["ESTADO"].ToString();
                    UnidadFallaR = dtSolicitud2.Rows[0]["FALLA"].ToString();
                }
                else
                {
                    EstadoR = "COMPLETADO";
                    UnidadFallaR = "";
                }

                if (UnidadFallaP == "TRACTO" || UnidadFallaP == "AMBOS")
                {
                    if (EstadoP != "COMPLETADO")
                    {
                        MessageBox.Show("No puede seleccionar la unidad " + txtTracto.Text + " porque tiene una solicitud de mantenimiento abierta. Consultar con el área de Mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (UnidadFallaR == "SEMIRREMOLQUE" || UnidadFallaP == "AMBOS")
                {
                    if (EstadoR != "COMPLETADO")
                    {
                        MessageBox.Show("No puede seleccionar la unidad " + txtRemolque.Text + " porque tiene una solicitud de mantenimiento abierta. Consultar con el área de Mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                */

                _PlacaMaquinaria = txtMaquinaria.Text;

                if (comboBox1.Text.Equals("ANULADO") || textBox1.Text.Equals("COMBUSTIBLE"))
                {
                    if (txtObservacion.Text.Equals(""))
                    {
                        MessageBox.Show("Obligatorio ingresar obrservación", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtObservacion.Focus();
                        return;
                    }
                }

                //CUANDO EL ESTADO DE LA PROGRAMACION ESTA EN ATENDIDO Y LA OT ASIGNADA SE CREA EL VIAJE AUTOMATICAMENTE
                if (comboBox1.Text.Equals("ATENDIDO") /*&& (Convert.ToInt32(txtOT.Text) > 0) */&& txtCodigoViaje.Text.Equals(""))
                {
                    if (PesoAlmacen < 1 && groupBox3.Enabled == true)
                    {
                        MessageBox.Show("La cantidad ingresada es Menor o IGUAL  a 0 ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPesoAlmacen.Focus();
                        return;
                    }

                    if (MessageBox.Show("Programación en estado 'ATENDIDO', desea generar el viaje...?", "CREAR VIAJE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        EstadoProgramacionRegistrar = (comboBox1.SelectedIndex) + 1;
                        int ID = R_IdProgramacionPrincipal;

                        string IDOT  = txtOT.Text;
                        DataTable datosOT  = clsOperacionesBL.Instancia.GetOperaciones_ListarDatosOts(Convert.ToInt32(IDOT));
                        if (datosOT.Rows.Count == 0)
                        {
                            MessageBox.Show("La OT ingresada no existe, favor de verificar si se digitó corretamente. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (groupBox3.Enabled == true)
                        {
                            DataTable dtRptaViaje = new DataTable();
                            string Rpta;
                            dtRptaViaje = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_CreaViaje(ID, IDOT, IDPARTIDA, IDLLEGADA, Utilitario.Instancia.SesionUsuario.usuario, montoOrdenServicio, _anio, TipoProgramacion);
                            Rpta = Convert.ToString(dtRptaViaje.Rows[0]["exito"]);
                            string NrRPTA = Rpta.Substring(0, 1);
                            string codviaje;
                            if (NrRPTA == "0")
                            {
                                codviaje = Rpta.Substring(9, 6);
                                idViaje = Rpta.Substring(25, 6);

                                txtCodigoViaje.Text = codviaje;
                                if (Rpta.Substring(40, 1).Equals("O"))
                                {
                                    ost = Rpta.Substring(40, 9);
                                    txtOrdServicio.Text = ost;
                                }
                            }
                            else
                            {
                                MessageBox.Show(Rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        EstadoProgramacionRegistrar = 8;
                    }
                }

                if (EstadoProgramacionRegistrar != 9 && EstadoProgramacionRegistrar != 8)
                {
                    EstadoProgramacionRegistrar = (comboBox1.SelectedIndex) + 1;
                }
               
                //CREACION Y MODIFICACION DE LAS PROGRAMACIONES
                if (!cboPartida.Text.Equals(""))
                {
                    IDPARTIDA = cboPartida.SelectedValue.ToString();
                    IDLLEGADA = cboLlegada.SelectedValue.ToString();
                    NombrePartida = cboPartida.Text;
                    NombreDestino = cboLlegada.Text;
                }

                dtSalida.CustomFormat = "HH:mm:ss";
              
                string fechasalidavar = dtSalida.Text;

               

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsOperacionesBL.Instancia.GetOperaciones_Registro_Previajes(crea1_modifi2_anula3, R_IdProgramacionPrincipal, txtSucursal.Text.ToUpper(), TipoProgramacion, 
                                                                                            dtFechaProgramada.Text, dtFechaInicio.Text, dtFechaFin.Text,
                                                                                            idtracto, idremolque, idConductor, idapoyo, idCliente, idRuta, txtDestino.Text,
                                                                                            idProducto, EstadoProgramacionRegistrar, dtSalida.Text, dtLlegada.Text, dtpFechaDescarga.Text, 
                                                                                            PesoAlmacen,PesoCliente, Merma, txtCodigoViaje.Text, txtPlanilla.Text, 
                                                                                            txtObservacion.Text.ToUpper(),Utilitario.Instancia.SesionUsuario.usuario, txtSerie.Text, 
                                                                                            txtNumero.Text, txtGuiaRemitente.Text,txtZona.Text.ToUpper(), txtTurno.Text.TrimStart(), 
                                                                                            txtConductorInicio.Text, tmePrimerCambio.Text,tmeSegCambio.Text, tmeTerCambio.Text, 
                                                                                            valOt, Viatico, txtOrdServicio.Text, txtUnidadApoyo.Text, txtGuiaEntrega.Text,
                                                                                            montoOrdenServicio, Convert.ToInt32(lblDias.Text), Convert.ToInt32(_EsInterna),
                                                                                            Convert.ToInt32(IDPARTIDA), Convert.ToInt32(IDLLEGADA), _anio, _PlacaMaquinaria,txtCodTolvas.Text,txtProgramacionOrigen.Text,
                                                                                            lineaConsolidado,NombrePartida,NombreDestino);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (txtProgramacionOrigen.Text.Length == 0)
                    {
                        frmOperacion_Previajes f1 = (frmOperacion_Previajes)this.Owner;
                        f1.Val_Respuesta = "1";
                    }

                    if (EstadoProgramacionRegistrar == 9 && (TipoProgramacion == 1 || TipoProgramacion == 2 || TipoProgramacion == 3 || TipoProgramacion == 4 ||
                        TipoProgramacion == 10 || TipoProgramacion == 11 || TipoProgramacion == 13))
                    {
                        DataTable dtOperatividad = new DataTable();
                        string Operativo;
                        dtOperatividad = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ActualizarViajeEstado(R_IdProgramacionPrincipal, _anio, idtracto, EstadoProgramacionRegistrar,
                                                                                               TipoProgramacion, dtFechaInicio.Value, dtFechaFin.Value, Utilitario.Instancia.SesionUsuario.usuario);
                        Operativo = Convert.ToString(dtOperatividad.Rows[0]["exito"]);
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Usted tiene acceso solo Visualizar...!", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void lstProducto_Enter(object sender, EventArgs e)
        {
            if (!lstProducto.Items.Count.Equals(0))
            {
                lstProducto.Items[0].Selected = true;
            }
        }

        private void lstCliente_Enter(object sender, EventArgs e)
        {
            if (!lstCliente.Items.Count.Equals(0))
            {
                lstCliente.Items[0].Selected = true;
            }
        }

        private void lstApoyo_Enter(object sender, EventArgs e)
        {
            if (!lstApoyo.Items.Count.Equals(0))
            {
                lstApoyo.Items[0].Selected = true;
            }
        }

        private void lstRemolque_Enter(object sender, EventArgs e)
        {
            if (!lstRemolque.Items.Count.Equals(0))
            {
                lstRemolque.Items[0].Selected = true;
            }
        }

        private void lstTracto_Enter(object sender, EventArgs e)
        {
            if (!lstTracto.Items.Count.Equals(0))
            {
                lstTracto.Items[0].Selected = true;
            }
        }

        private void txtRuta_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvRuta, clsConsultaBL.Instancia.GetRutas(txtRuta.Text), true, false, false);

                lvRuta.Columns[0].Width = 0;
                lvRuta.Columns[1].Width = 350;

                lvRuta.Size = new System.Drawing.Size(351, 103);

                lvRuta.BringToFront();
                lvRuta.Visible = true;
                lvRuta.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvRuta.Visible = false;
                txtRuta.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRuta.Visible = false;
                txtRuta.Focus();
                idRuta = -1;
            }
        }

        private void lvRuta_Enter_1(object sender, EventArgs e)
        {
            if (!lvRuta.Items.Count.Equals(0))
            {
                lvRuta.Items[0].Selected = true;
            }
        }

        private void cbxTiempo_DropDownClosed(object sender, EventArgs e)
        {
            MaestroGR = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(idRutaDia, idProgramacion, Convert.ToInt32(cbxTiempo.SelectedValue));
            if (MaestroGR.Rows.Count > 0)
            {
                for (int j = 0; j < MaestroGR.Rows.Count; j++)
                {
                    lblDias.Text = MaestroGR.Rows[0]["TotalDias"].ToString();
                    decimal viaticosSuma = Convert.ToDecimal(MaestroGR.Rows[0]["GastoTotal"]);
                    txtMontoViaticos.Text = viaticosSuma.ToString();
                }
            }
            else
            {
                lblDias.Text = "";
                txtMontoViaticos.Clear();
            }
        }

        private void lvRuta_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvRuta.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                //  int idProducto;
                ItemActual = lvRuta.SelectedItems[0];

                idRuta = Convert.ToInt32(ItemActual.Text);
                // IdConductor.Text = Convert.ToString(idtracto);
                txtRuta.Text = ItemActual.SubItems[1].Text;

                lvRuta.Visible = false;

                // GERARDO - 26/11
                MaestroGR = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(idRuta, idProgramacion, Convert.ToInt32(cbxTiempo.SelectedValue));
                if (MaestroGR.Rows.Count > 0)
                {
                    for (int j = 0; j < MaestroGR.Rows.Count; j++)
                    {
                        lblDias.Text = MaestroGR.Rows[0]["TotalDias"].ToString();
                        decimal viaticosSuma = Convert.ToDecimal(MaestroGR.Rows[0]["GastoTotal"]);
                        txtMontoViaticos.Text = viaticosSuma.ToString();
                    }
                }
                else
                {
                    lblDias.Text = "";
                    txtMontoViaticos.Clear();
                }

                if ((idtracto > 0 || _Tracto > 0) && idRuta > 0 && lblTipoUnidad.Text.Contains("Propia"))
                {
                    DataTable KMUnidades = new DataTable();
                    if (idtracto > 0) { KMUnidades = clsOperacionesBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades(idtracto, idRuta, txtSucursal.Text); }
                    else { KMUnidades = clsOperacionesBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades(_Tracto, idRuta, txtSucursal.Text); }

                    if (KMUnidades.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(KMUnidades.Rows[0]["ESTADO"]) == 0)
                        {
                            lblPlaca.Text = txtTracto.Text;
                            lblProxMtto.Text = KMUnidades.Rows[0]["PROXIMO"].ToString();

                            if (Convert.ToDecimal(KMUnidades.Rows[0]["TRACTO"]) > 0) { txtKMRestante.Text = KMUnidades.Rows[0]["TRACTO"].ToString(); }
                            else { txtKMRestante.Text = "0.00"; }

                            txtKMRuta.Text = KMUnidades.Rows[0]["RUTA"].ToString();

                            pKMUnidades.Visible = true;
                            pKMUnidades.BringToFront();
                            button1.Enabled = false;
                        }
                        else
                        {
                            button1.Enabled = true;

                            // CONSULTA PARA DETERMINAR SI UNA UNIDAD TIENE ASIGNADO UN KIT DE NEUMÁTICOS
                            try
                            {
                                DataTable KitAsignado = new DataTable();
                                if (idtracto > 0) { KitAsignado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico(idtracto, idRuta, textBox1.Text); }
                                else { KitAsignado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico(_Tracto, idRuta, textBox1.Text); }

                                if (KitAsignado.Rows.Count > 0)
                                {
                                    string respta = Convert.ToString(KitAsignado.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0") { lvRuta.Visible = false; }
                                    else
                                    {
                                        MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        lvRuta.Visible = false;
                                        idRuta = -1;
                                        txtRuta.Clear();
                                        txtMontoViaticos.Clear();
                                        lblDias.Text = "";
                                        txtRuta.Focus();
                                    }
                                }
                                else
                                {
                                    lvRuta.Visible = false;
                                    txtRuta.Focus();
                                }
                            }
                            catch
                            {
                                lvRuta.Visible = false;
                                idRuta = -1;
                                txtRuta.Clear();
                                txtRuta.Focus();
                            }
                        }
                    }
                }
                else { button1.Enabled = true; }

                txtDestino.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvRuta.Visible = false;
                txtRuta.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRuta.Visible = false;
                txtRuta.Focus();
                idRuta = -1;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (_Viaje.Equals(""))
                {
                    MessageBox.Show("La programación no contiene Viaje para generar el consolidado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dgvConsolidado.Visible = true;
                    dgvConsolidado.Size = new System.Drawing.Size(529, 126);
                    btnAgregar.Visible = true;
                }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            //LLAMA AL FORM CREAR CONSOLIDADO
            int IdProgramacionConso = _IdProg;
            int viaje = Convert.ToInt32(_Viaje);
            string anio = _anio;

            ProgramacionViajes.frmNuevoViaje frm = new ProgramacionViajes.frmNuevoViaje();
            frm.envioviaje(viaje, IdProgramacionConso,anio);
            frm.ShowDialog();
            CargarConsolidado();
        }

        private void txtOT_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtOT.Text.Equals(""))
                {
                    return;
                }
                if (Char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }

                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
                {
                    int otbuscar;
                    otbuscar = int.Parse(txtOT.Text);

                    DataTable datosOT = new DataTable();

                    datosOT = clsOperacionesBL.Instancia.GetOperaciones_ListarDatosOts(otbuscar);

                    if (datosOT.Rows.Count > 0)
                    {
                        for (int i = 0; i < datosOT.Rows.Count; i++)
                        {
                            txtCliente.Text = datosOT.Rows[i]["BUSQUEDA"].ToString();
                            idCliente = Convert.ToInt32(datosOT.Rows[i]["IdClienteFacturacion"].ToString());
                            txtRuta.Text = datosOT.Rows[i]["DESCRIPCION"].ToString();
                            idRuta = Convert.ToInt32(datosOT.Rows[i]["IdRuta"].ToString());
                            txtProducto.Text = datosOT.Rows[i]["Nombre"].ToString();
                            idProducto = Convert.ToInt32(datosOT.Rows[i]["Producto"].ToString());
                            lblUniMedida.Text = datosOT.Rows[i]["UMUso"].ToString();
                            tiempo = Convert.ToDecimal(datosOT.Rows[i]["tiempo"].ToString());
                            txtZona.Text = datosOT.Rows[i]["ZONA"].ToString();
                            txtDestino.Text = datosOT.Rows[i]["DESTINO"].ToString();
                            lblDias.Text = datosOT.Rows[i]["DIA"].ToString();
                            if (datosOT.Rows[i]["BUSQUEDA"].ToString().Equals("AC LOGISTICA DEL PERU S.A.C") || datosOT.Rows[i]["UMUso"].ToString().Equals("VI"))
                            {
                                txtPesoAlmacen.Text = "1";
                            }

                            // GERARDO - 26/11
                            idRutaDia = Convert.ToInt32(datosOT.Rows[i]["IdRuta"].ToString());
                            CargarComboTiempo();
                            MaestroGR = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarGastoCabecera(idRutaDia, idProgramacion, Convert.ToInt32(cbxTiempo.SelectedValue));
                            if (MaestroGR.Rows.Count > 0)
                            {
                                for (int j = 0; j < MaestroGR.Rows.Count; j++)
                                {
                                    lblDias.Text = MaestroGR.Rows[0]["TotalDias"].ToString();
                                    decimal viaticosSuma = Convert.ToDecimal(MaestroGR.Rows[0]["GastoTotal"]);
                                    txtMontoViaticos.Text = viaticosSuma.ToString();
                                }
                            }
                            else
                            {
                                lblDias.Text = "";
                                txtMontoViaticos.Clear();
                            }
                            // GERARDO - 26/11

                            decimal price = Math.Ceiling(tiempo);
                            DateTime fecha = Convert.ToDateTime(dtFechaProgramada.Text);
                            DateTime fecha2 = fecha.AddHours(Convert.ToDouble(price));

                            string vfehapro = Convert.ToString(fecha2);
                            dtFechaFin.Text = vfehapro;
                            dtFechaFin.CustomFormat = "dd/MM/yyyy HH:mm:ss";
                            dtLlegada.Text = dtFechaFin.Text;
                            dtLlegada.CustomFormat = "HH:mm:ss";
                        }

                        if (txtOrdServicio.Visible == true)
                        {
                            if (idtracto == -1)
                            {
                                idtracto = _Tracto;
                            }
                            DataTable dtPrecios = new DataTable();
                            dtPrecios = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarPrecios(idtracto, idRuta, idProducto, txtSucursal.Text.ToUpper());
                            if (dtPrecios.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtPrecios.Rows.Count; i++)
                                {
                                    label24.Text = "El Costo de la Ord. de Servicio es: " + dtPrecios.Rows[i]["Precio"].ToString();
                                    txtCostoOrdenServicio.Text = dtPrecios.Rows[i]["Precio"].ToString();
                                }
                            }
                            else
                            {
                                label24.Text = "El Costo de la Ord. de Servicio es: 0";
                                txtCostoOrdenServicio.Text = "0";
                            }
                        }
                        if (cboPartida.Visible == true)
                        {
                            if (R_PROGRAMACION.Equals("GENERAL") || R_PROGRAMACION.Equals("LINDLEY") || R_PROGRAMACION.Equals("LIMAGAS") || R_PROGRAMACION.Equals("VOLCAN"))
                            {
                                DataTable direccionesPartida = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_Transportista_GuiaEelectronica(idCliente, idRuta, 0, 0);
                                DataTable direccionesDestino = clsOperacionesBL.Instancia.ReportesApp_ListarDireccionesEmpresa_Transportista_GuiaEelectronica(idCliente, idRuta, 0, 0);

                                cboPartida.DisplayMember = "Direccion";
                                cboPartida.ValueMember = "Secuencia";
                                cboPartida.DataSource = direccionesPartida;

                                cboLlegada.DisplayMember = "Direccion";
                                cboLlegada.ValueMember = "Secuencia";
                                cboLlegada.DataSource = direccionesDestino;
                            }
                            else
                            {
                                otbuscar = int.Parse(txtOT.Text);
                                DataTable dt = new DataTable();
                                DataTable dt1 = new DataTable();

                                dt = clsOperacionesBL.Instancia.GetOperaciones_ListarDireccionesxOts(otbuscar);
                                dt1 = clsOperacionesBL.Instancia.GetOperaciones_ListarDireccionesxOts(otbuscar);

                                cboPartida.DisplayMember = "Direccion";
                                cboPartida.ValueMember = "Secuencia";
                                cboPartida.DataSource = dt;

                                cboLlegada.DisplayMember = "Direccion";
                                cboLlegada.ValueMember = "Secuencia";
                                cboLlegada.DataSource = dt1;
                            }
                        }

                        if (e.KeyChar == (char)Keys.Enter)
                        {
                            if ((idtracto > 0 || _Tracto > 0) && (idConductor > 0 || _Conductor > 0) && (idRuta > 0 || _Ruta > 0))
                            {
                                DataTable RendCombustible = new DataTable();
                                if (idtracto > 0) { RendCombustible = clsOperacionesBL.Instancia.ReportesApp_Combustible_Rendimientos_BuscarUltimoRendimiento(idtracto, idConductor, idRuta, idProgramacion); }
                                else { RendCombustible = clsOperacionesBL.Instancia.ReportesApp_Combustible_Rendimientos_BuscarUltimoRendimiento(_Tracto, _Conductor, _Ruta, idProgramacion); }

                                if (RendCombustible.Rows.Count > 0)
                                {
                                    if (Convert.ToDecimal(RendCombustible.Rows[0]["REND KM/GL"]) > Convert.ToDecimal(RendCombustible.Rows[0]["REND.PROM"]))
                                    {
                                        txtUltViaje.Text = RendCombustible.Rows[0]["CodViaje"].ToString();
                                        txtRendProm.Text = RendCombustible.Rows[0]["REND.PROM"].ToString();
                                        txtRendKM.Text = RendCombustible.Rows[0]["REND KM/GL"].ToString();

                                        lblBConductor.Text = RendCombustible.Rows[0]["Nombre"].ToString();
                                        lblBPlaca.Text = RendCombustible.Rows[0]["NumeroPlaca"].ToString();

                                        lblMensaje.ForeColor = Color.Red;
                                        lblMensaje.Text = "El rendimiento del último viaje del\r\nconductor ha sido mayor al promedio.";
                                        btnHistorial.Enabled = true;

                                        pRendComb.Visible = true;
                                        pRendComb.BringToFront();
                                    }
                                }
                            }

                            if ((idtracto > 0 || _Tracto > 0) && (idRuta > 0 || _Ruta > 0) && lblTipoUnidad.Text.Contains("Propia"))
                            {
                                DataTable KMUnidades = new DataTable();
                                if (idtracto > 0) { KMUnidades = clsOperacionesBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades(idtracto, idRuta, txtSucursal.Text); }
                                else { KMUnidades = clsOperacionesBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AlertarKMUnidades(_Tracto, _Ruta, txtSucursal.Text); }

                                if (KMUnidades.Rows.Count > 0)
                                {
                                    if (Convert.ToInt32(KMUnidades.Rows[0]["ESTADO"]) == 0)
                                    {
                                        lblPlaca.Text = txtTracto.Text;
                                        lblProxMtto.Text = KMUnidades.Rows[0]["PROXIMO"].ToString();

                                        if (Convert.ToDecimal(KMUnidades.Rows[0]["TRACTO"]) > 0) { txtKMRestante.Text = KMUnidades.Rows[0]["TRACTO"].ToString(); }
                                        else { txtKMRestante.Text = "0.00"; }

                                        txtKMRuta.Text = KMUnidades.Rows[0]["RUTA"].ToString();

                                        pKMUnidades.Visible = true;
                                        pKMUnidades.BringToFront();
                                        button1.Enabled = false;
                                    }
                                    else
                                    {
                                        button1.Enabled = true;

                                        // CONSULTA PARA DETERMINAR SI UNA UNIDAD TIENE ASIGNADO UN KIT DE NEUMÁTICOS
                                        DataTable KitAsignado = new DataTable();
                                        if (idtracto > 0) { KitAsignado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico(idtracto, idRuta, textBox1.Text); }
                                        else { KitAsignado = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_AlertarKitNeumatico(_Tracto, _Ruta, textBox1.Text); }

                                        if (KitAsignado.Rows.Count > 0)
                                        {
                                            string respta = Convert.ToString(KitAsignado.Rows[0]["exito"]);
                                            string NroRspta = respta.Substring(0, 1);
                                            if (NroRspta == "0")
                                            {
                                                button1.Focus();
                                                button1.Enabled = true;
                                            }
                                            else
                                            {
                                                MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                button1.Enabled = false;
                                            }
                                        }
                                    }
                                }                            
                            }
                            else { button1.Enabled = true; }
                        }
                    }
                    else
                    {
                        txtCliente.Text = "";
                        idCliente = 0;
                        txtRuta.Text = "";
                        idRuta = 0;
                        txtProducto.Text = "";
                        idProducto = 0;
                        lblUniMedida.Text = "";
                        tiempo = 0;
                        txtZona.Text = "";
                        txtDestino.Text = "";
                        lblDias.Text = "";
                        MessageBox.Show("La OT ingresada no existe...!");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtPesoClientes_KeyPress(object sender, KeyPressEventArgs e)
        {           

            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                if (!txtPesoAlmacen.Text.Equals("0"))
                {
                    decimal cb = Convert.ToDecimal(txtPesoAlmacen.Text) - Convert.ToDecimal(txtPesoClientes.Text);
                    txtMerma.Text = Convert.ToString(cb);
                }
                else
                {
                    MessageBox.Show("La Cantidad debe ser mayor a cero...!");
                }
            }
        }

        private void dtFechaProgramada_KeyPress(object sender, KeyPressEventArgs e)
        {
            dtFechaInicio.Text = dtFechaProgramada.Text;
            dtSalida.Text = dtFechaProgramada.Text;
            dtSalida.CustomFormat = "HH:mm:ss";
            if (!string.IsNullOrEmpty(txtOT.Text))
            {
                if (Convert.ToInt32(txtOT.Text) > 0)
                {
                    DataTable datosOT = new DataTable();

                    datosOT = clsOperacionesBL.Instancia.GetOperaciones_ListarDatosOts(Convert.ToInt32(txtOT.Text));

                    if (datosOT.Rows.Count > 0)
                    {
                        for (int i = 0; i < datosOT.Rows.Count; i++)
                        {
                            txtCliente.Text = datosOT.Rows[i]["BUSQUEDA"].ToString();
                            idCliente = Convert.ToInt32(datosOT.Rows[i]["IdClienteFacturacion"].ToString());
                            txtRuta.Text = datosOT.Rows[i]["DESCRIPCION"].ToString();
                            idRuta = Convert.ToInt32(datosOT.Rows[i]["IdRuta"].ToString());
                            txtProducto.Text = datosOT.Rows[i]["Nombre"].ToString();
                            idProducto = Convert.ToInt32(datosOT.Rows[i]["Producto"].ToString());
                            lblUniMedida.Text = datosOT.Rows[i]["UMUso"].ToString();
                            tiempo = Convert.ToDecimal(datosOT.Rows[i]["tiempo"].ToString());

                            decimal price = Math.Ceiling(tiempo);
                            DateTime fecha = Convert.ToDateTime(dtFechaProgramada.Text);
                            DateTime fecha2 = fecha.AddHours(Convert.ToDouble(price));

                            string vfehapro = Convert.ToString(fecha2);
                            dtFechaFin.Text = vfehapro;
                            dtFechaFin.CustomFormat = "dd/MM/yyyy HH:mm:ss";
                            dtLlegada.Text = dtFechaFin.Text;
                            dtLlegada.CustomFormat = "HH:mm:ss";
                        }
                    }
                }
            }
        }

        private void btnVerOt_Click(object sender, EventArgs e)
        {
            dtotsdis = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeOtsDisponibles();

            if (dtotsdis.Rows.Count > 0)
            {
                txtObservacion.Visible = false; //LA CAJA DE OBSERVACION SE MUEVE POR LAS PROGRAMACIONES
                label21.Visible = false;
                panel1.Visible = true;
                panel1.Size = new System.Drawing.Size(828, 441);
                lblOtsDisponilbes.Visible = true;
                dtgvData.Visible = true;
                dtgvData.DataSource = dtotsdis;
                dgvDataView.Columns["TARIFA"].Visible = false;
                dtgvData.Focus();
            }
        }

        private void frmNuevoPreviaje_Programacion_Click(object sender, EventArgs e)
        {
            if (panel1.Visible == true)
            {
                panel1.Visible = false;
                lblOtsDisponilbes.Visible = false;
                dtgvData.Visible = false;
                txtOT.Focus();
                txtObservacion.Visible = true;
                label21.Visible = true;
            }
        }

        private void dtgvData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int[] filas = dgvDataView.GetSelectedRows();
                string datoseleccionado = dgvDataView.GetFocusedValue().ToString();

                for (int i = 0; i < filas.Length; i++)
                {
                    string OTselec = dgvDataView.GetRowCellValue(filas[i], "OT").ToString();

                    if (Convert.ToInt32(OTselec) > 0)
                    {
                        OTselec = dgvDataView.GetRowCellValue(filas[i], "OT").ToString();
                        txtOT.Text = OTselec;
                        panel1.Visible = false;
                        lblOtsDisponilbes.Visible = false;
                        dtgvData.Visible = false;
                        txtOT.Focus();
                        CargarComboTiempo();
                        txtOT_KeyPress_1(this, new KeyPressEventArgs((char)(Keys.Enter)));
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void dtgvData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                panel1.Visible = false;
                lblOtsDisponilbes.Visible = false;
                dtgvData.Visible = false;
                txtOT.Focus();
            }
        }

        private void btnMantDestino_Click(object sender, EventArgs e)
        {
            if (txtOT.Text.Equals(""))
            {
                MessageBox.Show("Ingresar OT");
            }
            else
            {
                ProgramacionViajes.frmMantDestinos frmMaesDestino = new ProgramacionViajes.frmMantDestinos();
                frmMaesDestino.envioOTdes(Convert.ToInt32(txtOT.Text));
                frmMaesDestino.idTipoProgramacion = R_IdProgramacionPrincipal;
                frmMaesDestino.idruta =idRuta;
                frmMaesDestino.ShowDialog();
                txtOT_KeyPress_1(this, new KeyPressEventArgs((char)(Keys.Enter)));
            }
        }

        private void txtMaquinaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstTracto, clsConsultaBL.Instancia.GetUnidadesMaquinarias(txtMaquinaria.Text), true, false, false);

                lstTracto.Columns[0].Width = 0;
                lstTracto.Columns[1].Width = 100;
                lstTracto.Columns[2].Width = 50;

                lstTracto.Size = new System.Drawing.Size(200, 103);

                lstTracto.BringToFront();
                lstTracto.Visible = true;
                lstTracto.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstTracto.Visible = false;
                txtMaquinaria.Focus();
                idtracto = -1;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTracto.Visible = false;
                txtMaquinaria.Focus();
                idtracto = -1;
            }
        }

        private void txtPlanilla_KeyPress(object sender, KeyPressEventArgs e)
        {
          /*if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }*/            
            decimales(sender as TextBox, e);
        }

        public void decimales(object sender, KeyPressEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (char.IsControl(e.KeyChar) == false)
            {
                if (char.IsDigit(e.KeyChar) || e.KeyChar == '.')
                {
                    if (textBox.Text.Contains("."))
                    {
                        e.Handled = true;
                        if (textBox.Text.Split('.').Length < 2)
                        {
                            if (char.IsDigit(e.KeyChar) == false)
                                e.Handled = true;
                        }
                        else
                            e.Handled = true;
                    }
                }
                else
                    e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCerrarControlDoc_Click(object sender, EventArgs e)
        {
            grbControlDoc.Visible = false;
            lsvControlDoc.Visible = false;
            btnCerrarControlDoc.Visible = false;
        }

        private void txtOT_TextChanged(object sender, EventArgs e)
        {

        }
        private DataTable cargarDatosPorDefectoSegunCliente()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CompletarDestinatario_Direcciones(Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["Persona"].Value), Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["idRuta"].Value));

            return dt;
        }
        private void generarGuiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvConsolidado.CurrentCell.Selected)
                {
                    if (lblTipoUnidad.Text == "Unidad: Propia")
                    {


                        OperacionPreViajes.FrmGuiaElectronicaTransportista openGenerarGuia = new OperacionPreViajes.FrmGuiaElectronicaTransportista();
                        openGenerarGuia.TipoOperacion = Utilitario.TipoOperacion.Registrar;
                        openGenerarGuia.entGuiaTransportista.TipoOperacion = Utilitario.TipoOperacion.Registrar;

                        openGenerarGuia.entGuiaTransportista.idproveedor = Convert.ToInt32(lblTipoUnidad.Text.ToString().Equals("Unidad: Propia") ? "1553" : "-777");
                        openGenerarGuia.entGuiaTransportista.idcliente = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["Persona"].Value);
                        openGenerarGuia.entGuiaTransportista.Cliente = Convert.ToString(dgvConsolidado.CurrentRow.Cells["Busqueda"].Value).TrimEnd();
                        openGenerarGuia.txtCliente.Text = Convert.ToString(dgvConsolidado.CurrentRow.Cells["Busqueda"].Value).TrimEnd();
                        openGenerarGuia.txtCliente.Tag = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["Persona"].Value);
                        openGenerarGuia.entGuiaTransportista.entGRT_Remitente_RazonSocial_M = Convert.ToString(dgvConsolidado.CurrentRow.Cells["Busqueda"].Value).TrimEnd();
                        openGenerarGuia.entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = Convert.ToString(dgvConsolidado.CurrentRow.Cells["Busqueda"].Value).TrimEnd();
                        openGenerarGuia.entGuiaTransportista.AnioProgramacion = Convert.ToInt32(_anio);

                        DataTable dtDatos = cargarDatosPorDefectoSegunCliente();
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

                        if (dgvConsolidado.CurrentRow.Cells["Ot"].Value.ToString().Length > 0)
                        {
                            openGenerarGuia.entGuiaTransportista.idOT = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["Ot"].Value);
                        }
                        else
                        {
                            MessageBox.Show("Programacion no tiene OT asignada, no es posible generar Guia Electronica", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (idViaje == "")
                        {
                            openGenerarGuia.entGuiaTransportista.idviaje = 0;
                        }
                        else
                        {
                            openGenerarGuia.entGuiaTransportista.idviaje = Convert.ToInt32(idViaje);
                        }

                        openGenerarGuia.entGuiaTransportista.viaje = dgvConsolidado.CurrentRow.Cells["IDVIAJE"].Value.ToString() == "" ? "" : dgvConsolidado.CurrentRow.Cells["IDVIAJE"].Value.ToString(); // En la tabla consolidado el codigo de viaje esta como nobre de columna IDViaje
                        openGenerarGuia.entGuiaTransportista.estadoviaje = comboBox1.Text.TrimEnd();
                        openGenerarGuia.entGuiaTransportista.idRuta = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["idRuta"].Value);
                        openGenerarGuia.entGuiaTransportista.ruta = Convert.ToString(dgvConsolidado.CurrentRow.Cells["Descripcion"].Value).TrimEnd();
                        openGenerarGuia.entGuiaTransportista.conductor = txtConductor.Text; // nombre y apellidos completos concatenados del conductor
                        openGenerarGuia.entGuiaTransportista.idconductor = _Conductor;
                        openGenerarGuia.entGuiaTransportista.tracto = Convert.ToString(txtTracto.Text).Replace("-", "").Replace(".", "").TrimEnd(); // placa
                        openGenerarGuia.entGuiaTransportista.idtracto = Convert.ToString(_Tracto); // id placa
                        openGenerarGuia.entGuiaTransportista.idCarreta = Convert.ToString(_Remolque);
                        openGenerarGuia.entGuiaTransportista.carreta = Convert.ToString(txtRemolque.Text).Replace("-", "").Replace(".", "").TrimEnd();
                        openGenerarGuia.entGuiaTransportista.destino = "";
                        openGenerarGuia.entGuiaTransportista.FechaProgramacion = dtFechaProgramada.Text;
                        openGenerarGuia.TipoProgramacion = textBox1.Text.ToString(); //<<<<-------- ENVIO EL TIPO DE PROGRAMACION
                        openGenerarGuia.entGuiaTransportista.CodigoProgramacion = _anio.Remove(0, 2) + "" + _IdProg;
                        openGenerarGuia.entGuiaTransportista.idProgramacion = _IdProg;
                        openGenerarGuia.entGuiaTransportista.TipoGuia = "T"; //Transportista
                        openGenerarGuia.entGuiaTransportista.idTipoProgramacion = Convert.ToInt32(_TipoProg);
                        openGenerarGuia.entGuiaTransportista.idEstadoProgramacion = Convert.ToInt32(comboBox1.SelectedIndex);
                        openGenerarGuia.PesoTotal = Convert.ToString(dgvConsolidado.CurrentRow.Cells["Cantidad"].Value).TrimEnd();
                        openGenerarGuia.entGuiaTransportista.idOT = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["Ot"].Value);
                        openGenerarGuia.entGuiaTransportista.lineaConsolidado = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["Linea"].Value);
                                      
                        openGenerarGuia.esConsolidado = true;

                        if (dgvConsolidado.CurrentRow.Cells["Busqueda"].Value.ToString() == "LIMA GAS S A")
                        {
                            openGenerarGuia.entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = "LIMA GAS S A";
                        }

                        if (dgvConsolidado.CurrentRow.Cells["Busqueda"].Value.ToString() == "VOLCAN COMPANIA MINERA S.A.A.")
                        {
                            openGenerarGuia.entGuiaTransportista.entGRT_Destinatario_RazonSocial_M = "VOLCAN COMPANIA MINERA S.A.A.";
                        }


                        if (openGenerarGuia.ShowDialog() == DialogResult.OK)
                        {
                            CargarConsolidado();
                        }
                    }
                    else
                    {
                        MessageBox.Show("No es posible generar guia cuando el Proveedor es un Tercero", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void anexarGuiasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvConsolidado.SelectedCells.Count > 0)
                {
                    p_guiasfisicas.Visible = true;
                    p_guiasfisicas.Height = 123;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            p_guiasfisicas.Visible = false;
            txtSerieTran.Clear();
            txtSerieRem.Clear();
            txtNumeroTran.Clear();
            txtNumeroRem.Clear();
        }

        private void btnGuardarGuiaFisica_Click(object sender, EventArgs e)
        {
            try
            {

               clsGRT guiaTransportista = new clsGRT();

                if (dgvConsolidado.CurrentRow.Cells["Ot"].Value.ToString().Length == 0)
                {
                    MessageBox.Show("Programacion no tiene OT asignada, no es posible generar Guia Electronica", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtSerieTran.Text.Length < 3)
                {
                    MessageBox.Show("La longitud de la Serie de la guia de Transportista es incorrecta (mayor a 2 digitos)", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtSerieRem.Text.Length < 3)
                {
                    MessageBox.Show("La longitud de la Serie de la guia de Remitente es incorrecta (mayor a 2 digitos)", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtNumeroTran.Text.Length == 0)
                {
                    MessageBox.Show("No ha ingreso ningun valor en el Numero de la guia de Transportista", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtNumeroRem.Text.Length == 0)
                {
                    MessageBox.Show("No ha ingreso ningun valor en el Numero de la guia de Remitente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["LineaOT"].Value) == 0)
                {
                    MessageBox.Show("La ot seleccionada es la principal no es posible generar guia fisica por este medio, vaya a previaje y click en guias", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (txtSerieTran.Text[0].ToString() == "V")
                {
                    MessageBox.Show("La guia ingresada no puede ser electronica, opcion solo para guias fisicas", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                guiaTransportista.idOT = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["Ot"].Value);
                guiaTransportista.lineaConsolidado = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["Linea"].Value);
                guiaTransportista.viaje = dgvConsolidado.CurrentRow.Cells["IDVIAJE"].Value.ToString() == "" ? "" : dgvConsolidado.CurrentRow.Cells["IDVIAJE"].Value.ToString(); // En la tabla consolidado el codigo de viaje esta como nobre de columna IDViaje
                guiaTransportista.idRuta = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["idRuta"].Value);
                guiaTransportista.CodigoProgramacion = _anio.Remove(0, 2) + "" + _IdProg;
                guiaTransportista.idProgramacion = _IdProg;
                guiaTransportista.AnioProgramacion = Convert.ToInt32(_anio);
                guiaTransportista.idconductor = _Conductor;
                guiaTransportista.LineaOT = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["LineaOT"].Value);


                if (clsOperacionesBL.Instancia.ReportesApp_Operacones_VincularGuiasFisicas_Consolidado(guiaTransportista, txtSerieTran.Text, txtNumeroTran.Text,txtSerieRem.Text, txtNumeroRem.Text))
                {
                    MessageBox.Show( Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    p_guiasfisicas.Visible = false;
                    CargarConsolidado();
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void crearViajeLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "GENERAL")
                {
                    if (dgvConsolidado.CurrentRow.Cells["ProgLocal"].Value.ToString().Length > 0)
                    {
                        MessageBox.Show("Item de Consolidado seleccionado ya tiene previaje Local creado, no es posible adicionar, favor primero anular previaje", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    frmNuevoPreviaje_Programacion previaje = new frmNuevoPreviaje_Programacion();
       
                    //previaje.txtProgramacionOrigen.Text = _anio.Remove(0, 2) + "" + _IdProg;
                    previaje.lblProgramacionOriginal.Visible = true;
                    previaje.txtProgramacionOrigen.Visible = true;
                    //int IDPROGRAMA = Convert.ToInt32(comboBox1.SelectedValue.ToString());

                    string var_nomConductor = "";
                    string var_nomApoyo = "";
                    string var_nomCliente = "";
                    string var_nomProdcto = "";
                    string var_nomRuta = "";
                    decimal var_PesoAlmacen = 0;
                    decimal var_PesoCliente = 0;
                    decimal var_Merma = 0;
                    string var_Serie = "";
                    string var_Numero = "";
                    string var_Remitente = "";
                    int var_Accesos = 2;
                    decimal var_Viaticos = 0;
                    decimal var_MontoOrden = 0;
                    lineaConsolidado = dgvConsolidado.CurrentRow.Cells["Linea"].Value.ToString();

                    crea1_modifi2_anula3 = 1;
                    previaje.setearvariable(crea1_modifi2_anula3, 9, "LOCAL", 1, txtSucursal.Text, 1, "", "", "", "", 0, "", 1, "", 1, 1, 1, var_nomConductor, var_nomApoyo, var_nomCliente, var_nomProdcto,
                                   var_nomRuta, 1, "", 1, 1, "", "", "", var_PesoAlmacen, var_PesoCliente, var_Merma, "", "", "", "", var_Serie, var_Numero, var_Remitente, "", "", "", "", "", "",
                                   var_Accesos, 0, var_Viaticos, "", "", "", "", var_MontoOrden, "", 0, "", "", "", "0", 0, 0, "", "", "", "", "", _anio.Remove(0, 2) + "" + _IdProg, lineaConsolidado);


                    previaje.ShowDialog();
                }
                else
                {
                    MessageBox.Show("El viaje Inicial tiene que ser GENERAL", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pRendComb.Visible = false;
            pRendComb.SendToBack();

            txtUltViaje.Clear();
            txtRendProm.Clear();
            txtRendKM.Clear();
        }

        private void pRendComb_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pRendComb.Left = pRendComb.Left + (e.X - xClick);
                pRendComb.Top = pRendComb.Top + (e.Y - yClick);
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            CargaCombustible CargaCombustible = new CargaCombustible();
            CargaCombustible.txtTracto.Text = lblBPlaca.Text;
            CargaCombustible.txtConductor.Text = lblBConductor.Text;
            CargaCombustible.dtpFechaFin.Value = DateTime.Now;
            CargaCombustible.Show();
            CargaCombustible.btnBuscar_Click(sender, e);
        }

        private void pKMUnidades_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pKMUnidades.Left = pKMUnidades.Left + (e.X - xClick2);
                pKMUnidades.Top = pKMUnidades.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            pKMUnidades.Visible = false;
            pKMUnidades.SendToBack();
            pKMUnidades.Location = new Point(683, 462);
            //button1.Enabled = true;

            lblPlaca.Text = "";
            lblProxMtto.Text = "";
            txtKMRestante.Clear();
            txtKMRuta.Clear();
        }

        private void btnGenerarReq_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable ValidaUnidadVencida = new DataTable();
                ValidaUnidadVencida = clsOperacionesBL.Instancia.GetOperaciones_Programaciones_DcunetosVencidos(idRelacion, "UNIDAD");

                if (MessageBox.Show("¿Desea generar un requerimiento para estos documentos?", "GENERAR REQUERIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    for (int i = 0; i < ValidaUnidadVencida.Rows.Count; i++)
                    {
                        int idDocumento = Convert.ToInt32(ValidaUnidadVencida.Rows[i]["IDDOCUMENTO"]);
                        int idTipoDocumento = Convert.ToInt32(ValidaUnidadVencida.Rows[i]["ID_TIPO_DOCUMENTO"]);
                        string TipoDocumento = Convert.ToString(ValidaUnidadVencida.Rows[i]["TIPO_DOCUMENTO"]);
                        int idUnidad = idRelacion;
                        string Placa = Convert.ToString(ValidaUnidadVencida.Rows[i]["RELACION_NOMBRE"]);
                        string CentroCosto = Convert.ToString(ValidaUnidadVencida.Rows[i]["CENTRO_COSTO"]);
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        if (idTipoDocumento != 17 && idTipoDocumento != 3)
                        { MessageBox.Show("Solo puede generar requerimientos para inspecciones técnicas.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        else
                        {
                            DataTable dtRespuesta = new DataTable();
                            string respta;

                            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlDocumentos_GenerarRequerimiento(idDocumento, TipoDocumento, idUnidad, Placa, CentroCosto, Usuario);
                            respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                            string NroRspta = respta.Substring(0, 1);
                            if (NroRspta == "0")
                            {
                                MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                    }

                    idRelacion = -1;
                    btnCerrarControlDoc_Click(sender, e);
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el requerimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrar4_Click(object sender, EventArgs e)
        {
            dgvVencimientos.DataSource = null;
            dgvVencimientosVista.Columns.Clear();

            pDocumentosRegistrados.Visible = false;
            pDocumentosRegistrados.SendToBack();
        }

        private void dgvVencimientosVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "VIGENTE") { e.Appearance.BackColor = Color.Lime; }

                if (e.CellValue.ToString() == "POR VENCER") { e.Appearance.BackColor = Color.Yellow; }

                if (e.CellValue.ToString() == "NO VIGENTE") { e.Appearance.BackColor = Color.Salmon; }
            }
        }

        private void pDocumentosRegistrados_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick3 = e.X; yClick3 = e.Y; }
            else
            {
                pDocumentosRegistrados.Left = pDocumentosRegistrados.Left + (e.X - xClick3);
                pDocumentosRegistrados.Top = pDocumentosRegistrados.Top + (e.Y - yClick3);
            }
        }

        private void btnCerrar5_Click(object sender, EventArgs e)
        {
            idUnidadMtto = -1;
            dtgMttoCorrectivo.DataSource = null;
            dgvMttoCorrectivoView.Columns.Clear();

            pMttoCorrectivo.Visible = false;
            pMttoCorrectivo.SendToBack();
        }

        private void pMttoCorrectivo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick4 = e.X; yClick4 = e.Y; }
            else
            {
                pMttoCorrectivo.Left = pMttoCorrectivo.Left + (e.X - xClick4);
                pMttoCorrectivo.Top = pMttoCorrectivo.Top + (e.Y - yClick4);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            int[] filas = dgvMttoCorrectivoView.GetSelectedRows();
            string respta = "";
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    if (dgvMttoCorrectivoView.GetRowCellValue(filas[i], "FECHA_PROGRAMACION").ToString() == "")
                    {
                        MessageBox.Show("La fecha no puede estar vacía.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        int idMttoC = Convert.ToInt32(dgvMttoCorrectivoView.GetRowCellValue(filas[i], "idMttoC").ToString());
                        DateTime fechaProgramacion = Convert.ToDateTime(dgvMttoCorrectivoView.GetRowCellValue(filas[i], "FECHA_PROGRAMACION"));

                        DataTable dtModificar = new DataTable();
                        dtModificar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ModificarMtto(1, idMttoC, fechaProgramacion, "", Utilitario.Instancia.SesionUsuario.usuario);
                        respta = Convert.ToString(dtModificar.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0") { mensaje = "S"; }
                        else
                        {
                            MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }

                if (mensaje == "S")
                {
                    MessageBox.Show(respta, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataTable ListaMttoC = new DataTable();
                    ListaMttoC = clsOperacionesBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarPendientes(idUnidadMtto);

                    if (ListaMttoC.Rows.Count > 0)
                    {
                        dtgMttoCorrectivo.DataSource = ListaMttoC;

                        dgvMttoCorrectivoView.Columns["idMttoC"].Visible = false;
                        dgvMttoCorrectivoView.Columns["IdVehiculo"].Visible = false;
                        dgvMttoCorrectivoView.Columns["Placa"].Visible = false;
                        dgvMttoCorrectivoView.Columns["ESTADO"].Visible = false;

                        dgvMttoCorrectivoView.Columns["FECHA_PROGRAMACION"].DisplayFormat.FormatType = FormatType.DateTime;
                        dgvMttoCorrectivoView.Columns["FECHA_PROGRAMACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                        dgvMttoCorrectivoView.BestFitColumns();
                    }
                }
            }
            else { MessageBox.Show("No ha seleccionado ningún mantenimiento para programar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvConsolidado_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvConsolidado.RowCount > 0) { dgvConsolidado.ContextMenuStrip = contextMenuStrip1; }
                else { dgvConsolidado.ContextMenuStrip = null; }
            }
            catch (Exception ex) { }
        }

        private void tsQuitarOT_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea quitar el consolidado de este viaje?", "QUITAR CONSOLIDADO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string OT = dgvConsolidado.CurrentRow.Cells["Ot"].Value.ToString();
                    int CodViaje = Convert.ToInt32(dgvConsolidado.CurrentRow.Cells["IDViaje"].Value.ToString());

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Programacion_EliminarConsolidados(OT, CodViaje);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { CargarConsolidado(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }
    }
}
