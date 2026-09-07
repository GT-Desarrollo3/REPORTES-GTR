using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Entidades;
using ReportesTranspesa.Formularios.Areas.Operaciones;
using ReportesTranspesa.Formularios.Areas.Contabilidad;
using ReportesTranspesa.Formularios.Areas.Facturacion;
using ReportesTranspesa.Formularios.Areas.RecursosHumanos;
using ReportesTranspesa.Formularios.Areas.Finanzas;
using ReportesTranspesa.Formularios.Areas.Mantenimiento;
using ReportesTranspesa.Formularios.Areas.Almacen;
using ReportesTranspesa.Formularios.Areas.Logistica;
using ReportesTranspesa.Formularios.Areas.Sistemas;
using ReportesTranspesa.Formularios.Areas.Combustible;
using ReportesTranspesa.Formularios.Areas.Neumaticos;
using ReportesTranspesa.Formularios.Areas.Seguridad;
using ReportesTranspesa.Formularios.Administrador;
using ReportesTranspesa.Sistema;
using System.Net;
using System.Net.Sockets;
using ReportesTranspesa.Formularios.Areas.Operaciones.ClientesProveedores;
using Comun;
using ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes;
using ReportesTranspesa.Formularios.Areas.RecursosHumanos.SolicitudesPersonal;    
using ReportesTranspesa.Formularios.Areas.RecursosHumanos.AsignacionUniformes;     
using ReportesTranspesa.Formularios.Areas.RecursosHumanos.ConstanciaNoDeudo;
using ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas;
using ReportesTranspesa.Formularios.Areas.Seguridad.ControlCapacitaciones;      
using ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroBaterias;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias;
using ReportesTranspesa.Formularios.Areas.Seguridad.GestionSeguridad;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.AsignacionOT;
using ReportesTranspesa.Formularios.Areas.Operaciones.OperatividadFlota;
using ReportesTranspesa.Formularios.Areas.Operaciones.LavadoUnidades;
using ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems;
using ReportesTranspesa.Formularios.Areas.Operaciones.EntregaUnidad;
using ReportesTranspesa.Formularios.Areas.Seguridad.RegistroAccidentes;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroCanaletas;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;
using ReportesTranspesa.Formularios.Areas.Seguridad.MedicoOcupacional;
using ReportesTranspesa.Formularios.Areas.Operaciones.CumplimientoViajes;
using ReportesTranspesa.Formularios.Areas.Operaciones.ItinerarioViajes;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.MovimientoComponentes;
using ReportesTranspesa.Formularios.Areas.Operaciones.TiemposViaje;
using ReportesTranspesa.Formularios.Areas.Logistica.CuadroComparativo;
using ReportesTranspesa.Formularios.Areas.Finanzas.ControlCotizacion;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroNeumaticos;

namespace ReportesTranspesa
{
    public partial class Menu : MetroFramework.Forms.MetroForm
    {
        //private Timer Tiempo;

        public Menu()
        {
            InitializeComponent();
            //this.BringToFront();
            //this.ShowInTaskbar = true;
            //Tiempo = new Timer();
            //Tiempo.Tick += new EventHandler(EventoTimer);
            //Tiempo.Enabled = true;

        }

        public clsUsuario objUsuario = null;
        string usuario = "";
        public List<clsDetalleUsuarioReporte> listaReporte = null;

        public static String ListSTreeViewArea;
        //string selectedNodeText;

        public delegate void pasar(string dato);
        //public event pasar lista;

        Label hora = new Label();

        private void EventoTimer(object sender, EventArgs e)
        {
            lblHora.Text = "Fecha y Hora: " + DateTime.Now.ToString();
            hora.Text = "Fecha y Hora: " + DateTime.Now.ToString();
            lblHora2.Text = "Fecha y Hora: " + DateTime.Now.ToString();
            //lblHora.Text = DateTime.Now.ToString();
            //lblHora.Text = DateTime.Now.ToString("t");
            //lblHora.Text = "Hora: "+DateTime.Now.ToString("hh:mm:ss tt");
            //lblHora.Text = "La Hora: " + DateTime.Now.ToShortTimeString();
            //lblHora.Text = "Hora: " + DateTime.Now.ToLongTimeString();
        }

        public void MostrarHora()
        {
            hora.Size = new System.Drawing.Size(270, 22);
            hora.Location = new System.Drawing.Point(1050, 10);
            hora.ForeColor = Color.Black;
            hora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            hora.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            splitContainer1.Panel1.Controls.Add(hora);
        }
        
        public void CreateMyLabel()
        {
            #region Creacion de Label
            // Create an instance of a Label.
            Label label1 = new Label();

            // Set the border to a three-dimensional border.
            //label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            // Set the ImageList to use for displaying an image.
            label1.ImageIndex = 1;
            // Align the image to the top left corner.
            label1.ImageAlign = ContentAlignment.TopLeft;

            // Specify that the text can display mnemonic characters.
            label1.UseMnemonic = true;
            // Set the text of the control and specify a mnemonic character.
            DataTable dt = clsUsuarioBL.Instancia.GestUsuariosDB(Utilitario.Instancia.SesionUsuario.usuario);
            if (dt.Rows.Count > 0)
            {
                //label1.Size = new System.Drawing.Size(423, 19);
                //label1.Location = new System.Drawing.Point(123, 10);
                label1.Size = new System.Drawing.Size(35, 13);
                label1.Location = new System.Drawing.Point(238, 14);
                label1.AutoSize = true;
                label1.TextAlign = ContentAlignment.MiddleCenter;
                label1.ForeColor = Color.White;
                label1.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                label1.Text = "NOMBRE: " + dt.Rows[0]["Nombre"].ToString();
                splitContainer1.Panel1.Controls.Add(label1);
                
            }
            //else
            //{
            //    Mensaje m = new Mensaje();
            //    m.mensaje = "No hubo resultados";
            //    m.ShowDialog();
            //}
            #endregion
            #region Consultas
            //label1.Text = "First &Name:";
            //if (Environment.UserName == "JMARQUINA")
            //{
            //    label1.Size = new System.Drawing.Size(423, 19);
            //    label1.Location = new System.Drawing.Point(123, 10);
            //    label1.AutoSize = true;
            //    label1.TextAlign = ContentAlignment.MiddleCenter;
            //    splitContainer1.Panel1.Controls.Add(label1);
            //    label1.BackColor = Color.White;
            //    label1.ForeColor = Color.Black;
            //    label1.ForeColor = Color.White;
            //    label1.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            //}
            //else
            //{
            //    label1.Size = new System.Drawing.Size(423, 19);
            //    label1.Location = new System.Drawing.Point(114, 32);
            //    label1.AutoSize = true;
            //    label1.TextAlign = ContentAlignment.MiddleCenter;
            //    splitContainer1.Panel1.Controls.Add(label1);
            //    label1.BackColor = Color.White;
            //    label1.ForeColor = Color.Black;
            //    label1.ForeColor = Color.White;
            //    label1.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            //}

            /* Set the size of the control based on the PreferredHeight and PreferredWidth values. */
            //label1.Size = new Size(label1.PreferredWidth, label1.PreferredHeight);
            //label1.Size = new System.Drawing.Size(423, 19);
            //label1.Location = new System.Drawing.Point(123, 10);
            //label1.AutoSize = true;
            //label1.TextAlign = ContentAlignment.MiddleCenter;
            //splitContainer1.Panel1.Controls.Add(label1);
            ////label1.BackColor = Color.White;
            ////label1.ForeColor = Color.Black;
            //label1.ForeColor = Color.White;
            //label1.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            //label1.Font = new Font("Arial", 10, FontStyle.Regular);
            //Label lbl = new Label();
            //lbl.Left = 10;
            //lbl.Top = 20;
            //lbl.Text = "Hola";
            //splitContainer1.Panel1.Controls.Add(lbl);
            #endregion
        }

        public void LabelPersonal()
        {
            #region CreacionLabelPersonal
            if (Utilitario.Instancia.SesionUsuario.usuario != "JMARQUINA")
            {
                Label Personal = new Label();
                Personal.ImageIndex = 1;
                Personal.ImageAlign = ContentAlignment.TopLeft;
                Personal.UseMnemonic = true;
                Personal.Size = new System.Drawing.Size(123, 19);
                Personal.Location = new System.Drawing.Point(3, 19);
                //Personal.Size = new System.Drawing.Size(423, 19);
                //Personal.Location = new System.Drawing.Point(123, 19);
                Personal.AutoSize = true;
                Personal.ForeColor = Color.White;
                Personal.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                Personal.TextAlign = ContentAlignment.MiddleCenter;
                DataTable dt = clsUsuarioBL.Instancia.GestUsuariosDB(Utilitario.Instancia.SesionUsuario.usuario);
                //DataTable dt = clsUsuarioBL.Instancia.GestUsuariosDB("MLEZCANO");
                if (dt.Rows.Count > 0)
                {
                    Personal.Text = "NOMBRE: " + dt.Rows[0]["Nombre"].ToString();
                    splitContainer1.Panel1.Controls.Add(Personal);
                }
                //else
                //{
                //    Mensaje m = new Mensaje();
                //    m.mensaje = "No hubo resultados";
                //    m.ShowDialog();
                //}
            }
            else
            {
                Label Personal = new Label();
                Personal.ImageIndex = 1;
                Personal.ImageAlign = ContentAlignment.TopLeft;
                Personal.UseMnemonic = true;
                Personal.Size = new System.Drawing.Size(123, 19);
                Personal.Location = new System.Drawing.Point(3, 19);
                Personal.AutoSize = true;
                Personal.ForeColor = Color.White;
                Personal.Font = new Font("Segoe UI", 9, FontStyle.Regular);
                Personal.TextAlign = ContentAlignment.MiddleCenter;
                DataTable dt = clsUsuarioBL.Instancia.GestUsuariosDB(Utilitario.Instancia.SesionUsuario.usuario);
                //DataTable dt = clsUsuarioBL.Instancia.GestUsuariosDB("MLEZCANO");
                if (dt.Rows.Count > 0)
                {
                    Personal.Text = "NOMBRE: " + dt.Rows[0]["Nombre"].ToString();
                    splitContainer1.Panel1.Controls.Add(Personal);
                }
                //else
                //{
                //    Mensaje m = new Mensaje();
                //    m.mensaje = "No hubo resultados";
                //    m.ShowDialog();
                //}
            }
            #endregion
        }

        #region Formularios
        private MayorDetallado frmMayorDetallado;
        private Sumarizado frmSumarizado;
        private Facturacion_Diaria frmFacturacionDiaria;
        private Consolidado_Viajes frmConsolidadoViajes;
        private Produccion_Diaria frmProduccionDiaria;
        private Bonos_Conductores frmBonosConductores;
        private Ficha_Empleados frmFichaEmpleados;
        private Vencimiento_Contratos frmVencimientoContratos;
        private Pagos_Masivos frmPagoMasivo;
        private Consumo_Mantenimiento frmConsumoMantenimiento;
        //Reporte para Visualizar el Costo del Mantenimiento de las Unidades 
        private Costo_por_Unidades frmCosto_por_Unidades;
        //Lista de la Ubicacion de los Conductores
        //private Lista_de_Condutores frmLista_de_Conductores;
        private Saldos_Proveedores frmSaldosProveedores;
        private Adelantos_Planillas frmAdelantosPlanillas;
        private Guias_por_entregar frmGuiasxEntregar;
        private Ordenes_retiro frmOrdenesRetiro;
        private Ingresos frmIngresos;
        private Despachos frmDespachos;
        private Operaciones frmOperacionesAlmacen;
        private CajaChica_ReporteGastos frmCajaChicaRptGastos;
        private Generacion_Libros frmGeneracionLibros;
        private Permisos frmPermisos;
        private ListarReportes frmListaReporte;
        private Servicios frmServicios;
        private Compras frmCompras;
        private Saldos_Almacen frmSaldosAlmacen;
        private Estado_Cuenta frmEstadoCuenta;
        private Ficha_Conductores frmFichaConductores;
        private Movimientos_Diarios frmMovimientosDiarios;
        private Entrega_Facturas frmEntregasFacturas;
        private Utilizacion_Vacaciones frmUtilizacionVacaciones;
        private Viajes_PorFacturar frmViajesPorFacturar;
        private Facturacion_Almacen frmFacturacionAlmacen;
        private Facturas_Lindley frmFacturasLindley;
        private Facturacion_Contado frmFacturacionContado;
        private Detalle_Pagos frmDetallePagos;
        private Viajes_Terceros frmViajesTerceros;
        private Buscar_Facturas frmBuscarFacturas;
        private Faltas_Suspensiones frmFaltasySuspensiones;
        private Aportaciones_Retenciones frmAportacionesRetenciones;
        private frmAsignacionesTelefono frmAsignacionesTelefono;
        private Asistencia frmAsistencia;
        private Seguimiento_Guias Operaciones_Seguimiento_Guias;
        private GuiasxEstado frmGuias_por_Estado;
        //private FacturarViaje frmViajeFacturado;
        private Resumen_Viajes_por_Facturar frmResumenViajesporFacturar;
        private frmUnidadesProductivas frmUnidadesProductivas;
        private Adelantos_Aplicados frmAdelantosAplicados;
        private ListaProveedores frmProveedores;
        private PersonalNoGrato frmPersonalNoGrato;
        //private CargaCombustible frmCargaCombustible;
        private RendimientoCombustible frmRendimientoCombustible;
        private Prefacturas frmPrefacturasLindley;
        private ConsumoNeumaticos frmConsumoNeumaticos;
        private ListaViajes frmListaViajes;
        private Resumen_Tipo_Transporte frmTipoServicio;
        private FaltasConductores frmFataConductaConductores;
        private ListaFaltaConducta frmListaConductaConductores;
        private DetalleCobranzas frmCobranzasDetalle;
        private RendimientoUnidades frmRendimientoUnidades;
        private ReporteDiario frmRendimientoDiario;
        private CargaCombustible frmDespachosCombustible;
        private ProductosxRotacion frmProductosporRotacion;
        private MaestroParametrosTractos frmMaestroParametrosTractos;
        private Mantenimiento_Tractos frmMantenimientoTractos;
        private CantidadViajes frmCantidadViajes;
        private AnalisisFondoGV frmAnalisisFGV;
        //private Lista_de_Condutores frmLista_de_Condutores;
        private Reporte_Empleados frmReporte_Empleados;
        private Adelanto_Contabilidad_vs_CuentasporPagar frmAdelanto_Contabilidad_vs_CuentasporPagar;
        private Validacion_Cuentas_por_Cobrar frmValidacion_Cuentas_por_Cobrar;
        private ComercialvsContabilidad frmComercialvsContabilidad;
        private Proyeccion_Cobranzas frmProyeccion_Cobranzas;
        private frmLineasRPC frmLineasCelulares;
        private AprobacionPlanillasMultiple frmAprobacionPlanillas;
        private PlanillasTrabajadores frmPlanillasTrabajadores;
        private TrabajadoresCesados frmTrabajadoresCesados;
        private ControldeFacturas frmControlFacturas;
        private frmAccesosSpring frmListaAccesos;
        private frmGuias_ImpresionDesemparque frmGuias_ImpresionDesemparque;
        private frmGuias_ImportarSLV frmImportarGuiasAltraSLV;
        private Peajes_Verificar frmPeajesValidar;
        private frmKpiTransportes frmKPITransporte;
        private frmInventarioValorizadoPeriodoCerrado_043 frmLogistica_ReporteInveValorizadoPeriodoCerrado_043;
        private frmControlHerramientas frmControlHerramientas;
        private frmInventarioEquipos frmInventarioEquipos;
        private AgendarReuniones frmAgendarReuniones;
        private AnexarGuiasRetorno frmAnexarGuiasRetorno;
        private frmListarEPPSxPersonal frmListarEPPSxPersonal;
        private frmBloqueoUnidadXRuta frmBloqueoUnidadXRuta;
        private FrmListaGuiasElectronicas frmListarGuiasElectronicas;
        private frmGastosReten frmGastosReten;
        private frmConstanciaNoDeudo frmConstanciaNoDeudo;
        private frmProgramacionAlertasStock frmaltertastock;
        private frmListaFallasMecanicas frmListaFallasMecanicas;
        private frmKilometrajeUnidades frmKilometrajeUnidades;
        private frmObligacionesMenu frmObligacionesMenu;
        private frmReporteIngresoSubsidios frmReporteIngresoSubsidios;
        private frmListaSolicitudesPersonal frmListaSolicitudesPersonal;
        private frmListaUniformes frmListaUniformes;
        private frmListaContratos frmListaContratos;
        private frmActivosDeSegundoUso frmActivosdeSegunoUso;
        private frmSire_GenerarTXT frmSire;
        private frmListarCapacitaciones frmListarCapacitaciones;
        private frmListarPlanillas frmListarPlanillas;
        private frmListaBaterias frmListaBaterias;
        private frmControlAlcoholTest frmControlAlcoholTest;
        private frmListaMantenimiento frmListaMantenimiento;
        private frmAgregarGuiasFisicas frmAgregarGuiasFisicas;
        private frmListaIncidencias frmListaIncidencias;
        private frmRequerimientosCompras frmRequerimientosCompras;
        private frmGestionSeguridad frmGestionSeguridad;
        private frmPendientesDiarios frmPendientesDiarios;
        private frmRegistroOT frmRegistroOT;
        private frmSegundoUsoNeumaticos frmSegundoUsoNeimaticos;
        private frmRegistroOperatividad frmRegistroOperatividad;
        private frmListaTicketsLavadero frmListaTicketsLavadero;
        private frmListaControlItems frmListaControlItems;
        private frmConstanciaUnidades frmConstanciaUnidades;
        private frmSistemaDeColasAlmacen frmSistemaDeColasAlmacen;
        private frmEstadoUnidades frmEstadoUnidades;
        private ListaAsistenciaExterna listaAsistenciaExterna;
        private frmListarGuiasViaje frmListarGuiasViaje;
        private frmListarReclamosClientes frmListarReclamosClientes;
        private frmListaIncidentesSSOMAC frmListaIncidentesSSOMAC;
        private frmListaTarifasOT frmListaTarifasOT;
        private frmReporteReqServicios frmReporteReqServicios;
        private frmListaCanaletas frmListaCanaletas;
        private FrmActualizarTarifaViajes FrmActualizarTarifaViajes;
        private frmListaEMO frmListaEMO;
        private frmRegistroCumplimiento frmRegistroCumplimiento;
        private frmReporte_Req_CentroCostos frmReporte_Req_CentroCostos;
        private frmItinerarioViajes frmItinerarioViajes;
        private frmRegistroMovimientos frmRegistroMovimientos;
        private frmImprimirTransacciones frmImprimirTransacciones;
        private frmControlPresupuestal frmControlPresupuestal;
        private frmListaTiemposViaje frmListaTiemposViaje;
        private frmControlOperativos frmControlOperativos;
        private frmItemsPendientes frmItemsPendientes;
        private frmDisponibilidad frmDisponibilidad;
        private frmListaOferta frmListaOferta;
        private frmControlCotizacion frmControlCotizacion;
        private frmMovTransformacion frmMovTransformacion;
        private frmPlanVacaComp frmPlanVacaComp;
        private frmKardexAlmacenes frmKardexAlmacenes;
        private frmControlNeumaticos frmControlNeumaticos;
        private frmDocumentosSIG frmDocumentosSIG;
        private frmIngresoTerceros frmIngresoTerceros;
        private frmAlmacenSalaverryMovimientos frmAlmacenSalaverryMovimientos;
        private frmListaCodNeumaticos frmListaCodNeumaticos;
        private frmInspeccionTanque frmInspeccionTanque;
        private frmVacacionesPendientes frmVacacionesPendientes;
        private frmDocumentosCapacitacion frmDocumentosCapacitacion;
        private frmRegistroCapacitaciones frmRegistroCapacitaciones;
        private frmAperturarPeriodos frmAperturarPeriodos;
        private frmListarGuiasFisicas frmListarGuiasFisicas;
        #endregion

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }

        private void Menu_Load(object sender, EventArgs e)
        {

            this.WindowState = FormWindowState.Maximized;
            this.Activate();
            //CreateMyLabel();
            //LabelPersonal();

            #region TextColor
            if (GetLocalIPAddress() == "192.168.4.60")
            {
                //Base Lima
                toolStripDropDownButton1.ForeColor = Color.Black;
                toolStripDropDownButton2.ForeColor = Color.Black;
                toolStripDropDownButton3.ForeColor = Color.Black;
                toolStripDropDownButton4.ForeColor = Color.Black;
                lblUsuario.ForeColor = Color.Black;
            }
            else
            {
                //Base Trujillo
                toolStripDropDownButton1.ForeColor = Color.White;
                toolStripDropDownButton2.ForeColor = Color.White;
                toolStripDropDownButton3.ForeColor = Color.White;
                toolStripDropDownButton4.ForeColor = Color.White;
            }
            #endregion

            List<clsArea> listaArea = clsAreaBL.Instancia.consulta_Area_Activas();

            foreach (clsArea obj in listaArea)
            {
                TreeNode node = new TreeNode(obj.descripcion);

                treeViewArea.Nodes.Add(node);
            }
            
            usuario = Utilitario.Instancia.SesionUsuario.usuario;
            UsuariosEspeciales();
            lblUsuario.Text = "Bienvenido:  " + usuario;


            /* if (usuario == "EMENDOZA" || usuario == "SOPORTETI" || usuario == "SOPORTE")
             {
                 listaReporte = clsDetalleReporteUsuarioBL.Instancia.consulta_Reporte_Todos();
                 btnPermisos.Visible = true;
                 btnListaReporte.Visible = true;
                 splitContainer3.Panel1Collapsed = true;
                 statusStrip1.Visible = true;
                 //splitContainer1.Panel1Collapsed = true;
                 CreateMyLabel();
                 LabelPersonal();
                 splitContainer1.Panel1.Controls.Add(lblHora2);
                 lblHora2.Visible = false;
                 //MostrarHora();
             }
             else
             {*/


            //if (usuario == "JROJAS" || usuario == "AGUERRA" || usuario == "REDES2" || usuario == "ADMINISTRADOR" || usuario == "JTORIBIO" || usuario == "SCHAVEZ")
            //{
            //    listaReporte = clsDetalleReporteUsuarioBL.Instancia.consulta_Reporte_Todos();
            //    btnPermisos.Visible = true;
            //    btnListaReporte.Visible = true;

            //    if (Utilitario.Instancia.SesionUsuario.usuario == "JROJAS" || Utilitario.Instancia.SesionUsuario.usuario == "AGUERRA" || Utilitario.Instancia.SesionUsuario.usuario == "JTORIBIO" || usuario == "SCHAVEZ")
            //    {
            //        cAMBIARBDtoolStripMenuItem.Visible = true;
            //    }
            //    else
            //    {
            //        cAMBIARBDtoolStripMenuItem.Visible = false;
            //    }
            //    //cAMBIARBDtoolStripMenuItem.Visible = false;
            //    //splitContainer3.Panel1Collapsed = true;
            //    statusStrip1.Visible = true;
            //    CreateMyLabel();
            //    lblHora.Visible = false;
            //    lblHora2.Visible = false;
            //    //MostrarHora();
            //    if (usuario == "AGUERRA" || usuario == "SCHAVEZ")
            //    {
            //        lblHora2.Visible = false;
            //        MostrarHora();

            //    }
                //}
                //else
                //{ 
                listaReporte = clsDetalleReporteUsuarioBL.Instancia.consulta_DetalleReporte_Por_Usuario(usuario);
                clsDetalleReporteUsuarioBL.Instancia.consulta_DetalleReporte_Por_Usuario_PermisosFormlario(usuario);

                splitContainer3.Panel1Collapsed = true;
                splitContainer1.Panel1Collapsed = false;
                LabelPersonal();
                lblHora2.Visible = false;
                //MostrarHora();

                if (Utilitario.Instancia.SesionUsuario.usuario == "KORBEGOSO" || Utilitario.Instancia.SesionUsuario.usuario == "AGUERRA" || Utilitario.Instancia.SesionUsuario.usuario == "JTORIBIO" || usuario == "SCHAVEZ" || usuario == "KGARCIAF" || usuario == "GREYES")
                {
                    panel1.Size = new Size(980, 18);
                    lblServidor.Visible = true;
                    lblServidor.Text = Utilitario.Instancia.TextoMenuServidor;

                    ColorLabelServidor();
                    btnPermisos.Visible = true;
                    btnListaReporte.Visible = true;
                    splitContainer3.Panel1Collapsed = false;
                    statusStrip1.Visible = true;
                    splitContainer1.Panel1Collapsed = false;
                    CreateMyLabel();
                    LabelPersonal();
                    lblHora.Visible = false;
                }
                else
                {
                    cAMBIARBDtoolStripMenuItem.Visible = false;
                }
                //}


                //}

                if (listaReporte != null)
                {
                    foreach (clsDetalleUsuarioReporte objDetalleReporte in listaReporte)
                        for (int i = 0; i < treeViewArea.Nodes.Count; i++)
                            if (treeViewArea.Nodes[i].Text.CompareTo(objDetalleReporte.objReporte.objArea.descripcion) == 0)

                                treeViewArea.Nodes[i].Nodes.Add(objDetalleReporte.objReporte.nombre);

                }
                else
                {
                    if (MessageBox.Show("No tiene ningún permiso asignado", "Mensaje", MessageBoxButtons.OK) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
            //}
        } 

        public void ColorLabelServidor()
        {

            if (Utilitario.Instancia.ipServidor == "172.16.0.11")
            {
                lblServidor.BackColor = Color.Beige;
                lblServidor.ForeColor = Color.DarkBlue;
            }
            else
            {
                lblServidor.BackColor = Color.Beige;
                lblServidor.ForeColor = Color.DarkRed;
            }
        }
        // fin
        public void UsuariosEspeciales()
        {
            if (usuario == "LVALDERRAMA")
            {
                usuario = "LVALDERRAM";
            }
            if (usuario == "BALANZA1" || usuario == "BALANZA2" || usuario == "BALANZA02")
            {
                usuario = "JLLORCA";
            }
            if (usuario == "FACTURACIONBALANZA")
            {
                usuario = "RJOAQUIN";
            }
            if (usuario == "COMBUSTIBLE2")
            {
                usuario = "EANGULO";
            }
            if (usuario == "BALANZASALAVERRY")
            {
                usuario = "CALVARADO";
            }
            if (usuario == "FRUIZ2")
            {
                usuario = "FRUIZ";
            }
            if (usuario == "FARENAS2")
            {
                usuario = "FARENAS";
            }
            if (usuario == "ALEZCANO")
            {
                usuario = "MLEZCANO";
            }
            if (usuario == "JARTEAGAB")
            {
                usuario = "JARTEAGA";
            }
            if (usuario == "EPN")
            {
                usuario = "EPESANTES";
            }
            if (usuario == "MSANCHEZ")
            {
                usuario = "FSANCHEZ";
            }
            if (usuario == "EFERNANDEZ")
            {
                usuario = "SFERNANDEZ";
            }
            if (usuario == "GPSTRUJILLO")
            {
                usuario = "KIMBERLYC";
            }
            if (usuario == "JVARAS")
            {
                usuario = "PVARGAS";
            }
            if (usuario == "RJOAQUIN")
            {
                usuario = "JLLORCA";
            }
            if (usuario == "MGARCIA")
            {
                usuario = "MGARCIAC";
            }
        }

        private void treeViewArea_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 1)
                foreach (clsDetalleUsuarioReporte obj in listaReporte)
                    if (obj.objReporte.nombre.CompareTo(e.Node.Text) == 0)
                    {
                        switch (obj.objReporte.objFormulario.nombre)
                        {

                            #region GPS

                            //case "GPS_Reporte_Seguimiento_Viaje":

                            //    GPS_Reporte_Seguimiento_Viaje frmGPS_Reporte_Seguimiento_Viaje = new GPS_Reporte_Seguimiento_Viaje();
                            //    frmGPS_Reporte_Seguimiento_Viaje.Show();

                            //    break;

                            #endregion

                            #region Almacen

                            case "Almacen_Reporte_OrdenesRetiro":
                                if (frmOrdenesRetiro == null || frmOrdenesRetiro.IsDisposed)
                                {
                                    frmOrdenesRetiro = new Ordenes_retiro();
                                    frmOrdenesRetiro.Show();
                                }
                                else
                                {
                                    frmOrdenesRetiro.Activate();
                                    frmOrdenesRetiro.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Almacen_Reporte_Ingresos":
                                if (frmIngresos == null || frmIngresos.IsDisposed)
                                {
                                    frmIngresos = new Ingresos();
                                    frmIngresos.Show();
                                }
                                else
                                {
                                    frmIngresos.Activate();
                                    frmIngresos.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Almacen_Reporte_Despachos":
                                if (frmDespachos == null || frmDespachos.IsDisposed)
                                {
                                    frmDespachos = new Despachos();
                                    frmDespachos.Show();
                                }
                                else
                                {
                                    frmDespachos.Activate();
                                    frmDespachos.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "Almacen_Reporte_Saldos":
                                if (frmSaldosAlmacen == null || frmDespachos.IsDisposed)
                                {
                                    frmSaldosAlmacen = new Saldos_Almacen();
                                    frmSaldosAlmacen.Show();
                                }
                                else
                                {
                                    frmSaldosAlmacen.Activate();
                                    frmSaldosAlmacen.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "Almacen_Operaciones":
                                if (frmOperacionesAlmacen == null || frmOperacionesAlmacen.IsDisposed)
                                {
                                    frmOperacionesAlmacen = new Operaciones();
                                    frmOperacionesAlmacen.Show();
                                }
                                else
                                {
                                    frmOperacionesAlmacen.Activate();
                                    frmOperacionesAlmacen.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "MISCELANEOS":
                                frmMicelaneos MICELANEOS = new frmMicelaneos();
                                MICELANEOS.Show();
                                break;

                            case "frmSistemaDeColasAlmacen":
                                if (frmSistemaDeColasAlmacen == null || frmSistemaDeColasAlmacen.IsDisposed)
                                {
                                    frmSistemaDeColasAlmacen = new frmSistemaDeColasAlmacen();
                                    frmSistemaDeColasAlmacen.Show();
                                }
                                else
                                {
                                    frmSistemaDeColasAlmacen.Activate();
                                    frmSistemaDeColasAlmacen.WindowState = FormWindowState.Normal;
                                }
                                break;

                            case "frmMovTransformacion":
                                frmMovTransformacion = new frmMovTransformacion();
                                frmMovTransformacion.Show();
                            break;

                            case "frmKardexAlmacenes":
                                frmKardexAlmacenes = new frmKardexAlmacenes();
                                frmKardexAlmacenes.Show();
                            break;

                            case "frmAlmacenSalaverryMovimientos":
                            if (frmAlmacenSalaverryMovimientos == null || frmAlmacenSalaverryMovimientos.IsDisposed)
                            {
                                frmAlmacenSalaverryMovimientos = new frmAlmacenSalaverryMovimientos();
                                frmAlmacenSalaverryMovimientos.Show();
                            }
                            else
                            {
                                frmAlmacenSalaverryMovimientos.Activate();
                                frmAlmacenSalaverryMovimientos.WindowState = FormWindowState.Normal;
                            }
                            break;
                            //#region Logistica

                            //case "Logistica_Indicador_RotacionInventario":

                            //    Logistica_Indicador_RotacionInventario Logistica_Indicador_RotacionInventario = new Logistica_Indicador_RotacionInventario();
                            //    Logistica_Indicador_RotacionInventario.definicion = obj.objReporte.descripcion;
                            //    Logistica_Indicador_RotacionInventario.formula = obj.objReporte.formula;
                            //    Logistica_Indicador_RotacionInventario.Show();

                            //    break;

                            #endregion

                            #region Facturacion

                            case "Facturacion_Diaria":

                                if (frmFacturacionDiaria == null || frmFacturacionDiaria.IsDisposed)
                                {
                                    frmFacturacionDiaria = new Facturacion_Diaria();
                                    frmFacturacionDiaria.usuario = usuario;
                                    frmFacturacionDiaria.Show();
                                }
                                else
                                {
                                    frmFacturacionDiaria.Activate();
                                    frmFacturacionDiaria.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Facturacion_Almacen":
                                if (frmFacturacionAlmacen == null || frmFacturacionAlmacen.IsDisposed)
                                {
                                    frmFacturacionAlmacen = new Facturacion_Almacen();
                                    frmFacturacionAlmacen.Show();
                                }
                                else
                                {
                                    frmFacturacionAlmacen.Activate();
                                    frmFacturacionAlmacen.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Facturacion_Contado":
                                if (frmFacturacionContado == null || frmFacturacionContado.IsDisposed)
                                {
                                    frmFacturacionContado = new Facturacion_Contado();
                                    frmFacturacionContado.Show();
                                }
                                else
                                {
                                    frmFacturacionContado.Activate();
                                    frmFacturacionContado.WindowState = FormWindowState.Maximized;
                                }
                                break;


                            case "Facturacion_Lista":

                                frmListadoDeFacturas frmlistadoDeFacturas = new frmListadoDeFacturas();
                                frmlistadoDeFacturas.Show();

                                break;

                            #endregion

                            #region Finanzas

                            case "Finanzas_Pago_Masivo":

                                if (frmPagoMasivo == null || frmPagoMasivo.IsDisposed)
                                {
                                    frmPagoMasivo = new Pagos_Masivos();
                                    frmPagoMasivo.Show();
                                }
                                else
                                {
                                    frmPagoMasivo.Activate();
                                    frmPagoMasivo.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Finanzas_Saldos_Proveedores":

                                if (frmSaldosProveedores == null || frmSaldosProveedores.IsDisposed)
                                {
                                    frmSaldosProveedores = new Saldos_Proveedores();
                                    frmSaldosProveedores.Show();
                                }
                                else
                                {
                                    frmSaldosProveedores.Activate();
                                    frmSaldosProveedores.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Finanzas_Movimientos_Diarios":

                                if (frmMovimientosDiarios == null || frmMovimientosDiarios.IsDisposed)
                                {
                                    frmMovimientosDiarios = new Movimientos_Diarios();
                                    frmMovimientosDiarios.Show();
                                }
                                else
                                {
                                    frmMovimientosDiarios.Activate();
                                    frmMovimientosDiarios.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Finanzas_Estado_Cuenta":

                                if (frmEstadoCuenta == null || frmEstadoCuenta.IsDisposed)
                                {
                                    frmEstadoCuenta = new Estado_Cuenta();
                                    frmEstadoCuenta.Show();
                                }
                                else
                                {
                                    frmEstadoCuenta.Activate();
                                    frmEstadoCuenta.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Finanzas_Detalle_Pagos":

                                if (frmDetallePagos == null || frmDetallePagos.IsDisposed)
                                {
                                    frmDetallePagos = new Detalle_Pagos();
                                    frmDetallePagos.Show();
                                }
                                else
                                {
                                    frmDetallePagos.Activate();
                                    frmDetallePagos.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Finanzas_Lista_Proveedores":

                                if (frmProveedores == null || frmProveedores.IsDisposed)
                                {
                                    frmProveedores = new ListaProveedores();
                                    frmProveedores.Show();
                                }
                                else
                                {
                                    frmProveedores.Activate();
                                    frmProveedores.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Finanzas_Prefacturas":

                                if (frmPrefacturasLindley == null || frmPrefacturasLindley.IsDisposed)
                                {
                                    frmPrefacturasLindley = new Prefacturas();
                                    frmPrefacturasLindley.Show();
                                }
                                else
                                {
                                    frmPrefacturasLindley.Activate();
                                    frmPrefacturasLindley.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Finanzas_CobranzasDetalle":

                                if (frmCobranzasDetalle == null || frmCobranzasDetalle.IsDisposed)
                                {
                                    frmCobranzasDetalle = new DetalleCobranzas();
                                    frmCobranzasDetalle.Show();
                                }
                                else
                                {
                                    frmCobranzasDetalle.Activate();
                                    frmCobranzasDetalle.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Finanzas_Proyeccion_Cobranzas":

                                if (frmProyeccion_Cobranzas == null || frmProyeccion_Cobranzas.IsDisposed)
                                {
                                    frmProyeccion_Cobranzas = new Proyeccion_Cobranzas();
                                    frmProyeccion_Cobranzas.Show();
                                }
                                else
                                {
                                    frmProyeccion_Cobranzas.Activate();
                                    frmProyeccion_Cobranzas.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "frmControlPresupuestal":
                                frmControlPresupuestal = new frmControlPresupuestal();
                                frmControlPresupuestal.Show();
                            break;

                            case "frmControlCotizacion":
                                frmControlCotizacion = new frmControlCotizacion();
                                frmControlCotizacion.Show();
                            break;
                            //case "Finanzas_Indicador_PlazoPago":

                            //    Finanzas_Indicador_PlazoPago Finanzas_Indicador_PlazoPago = new Finanzas_Indicador_PlazoPago();
                            //    Finanzas_Indicador_PlazoPago.definicion = obj.objReporte.descripcion;
                            //    Finanzas_Indicador_PlazoPago.formula = obj.objReporte.formula;
                            //    Finanzas_Indicador_PlazoPago.Show();
                            //    break;

                            //case "Finanzas_Indicador_PeriodoCobro":

                            //    Finanzas_Indicador_PeriodoCobro Finanzas_Indicador_PeriodoCobro = new Finanzas_Indicador_PeriodoCobro();
                            //    Finanzas_Indicador_PeriodoCobro.definicion = obj.objReporte.descripcion;
                            //    Finanzas_Indicador_PeriodoCobro.formula = obj.objReporte.formula;
                            //    Finanzas_Indicador_PeriodoCobro.Show();
                            //    break;
                            #endregion

                            #region Mantenimiento
                                
                            case "frmRegistrarActivoSegundoUso":

                                frmActivosdeSegunoUso = new frmActivosDeSegundoUso();
                                frmActivosdeSegunoUso.Show();
                                break;


                            ////case "Mantenimiento_Indicador_CostoMttoMaquinariaPesada":

                            ////    Mantenimiento_Indicador_CostoMttoMaquinariaPesada Mantenimiento_Indicador_CostoMttoMaquinariaPesada = new Mantenimiento_Indicador_CostoMttoMaquinariaPesada();
                            ////    Mantenimiento_Indicador_CostoMttoMaquinariaPesada.definicion = obj.objReporte.descripcion;
                            ////    Mantenimiento_Indicador_CostoMttoMaquinariaPesada.formula = obj.objReporte.formula;
                            ////    Mantenimiento_Indicador_CostoMttoMaquinariaPesada.Show();
                            ////    break;

                            //case "Mantenimiento_Indicador_CostoMttoTotal_Kilometraje":

                            //    Mantenimiento_Indicador_CostoMttoTotal_Kilometraje Mantenimiento_Indicador_CostoMttoTotal_Kilometraje = new Mantenimiento_Indicador_CostoMttoTotal_Kilometraje();
                            //    Mantenimiento_Indicador_CostoMttoTotal_Kilometraje.definicion = obj.objReporte.descripcion;
                            //    Mantenimiento_Indicador_CostoMttoTotal_Kilometraje.formula = obj.objReporte.formula;
                            //    Mantenimiento_Indicador_CostoMttoTotal_Kilometraje.Show();
                            //    break;

                            //case "Mantenimiento_Indicador_CostoMttoSiniestro":

                            //    Mantenimiento_Indicador_CostoMttoSiniestro Mantenimiento_Indicador_CostoMttoSiniestro = new Mantenimiento_Indicador_CostoMttoSiniestro();
                            //    Mantenimiento_Indicador_CostoMttoSiniestro.definicion = obj.objReporte.descripcion;
                            //    Mantenimiento_Indicador_CostoMttoSiniestro.formula = obj.objReporte.formula;
                            //    Mantenimiento_Indicador_CostoMttoSiniestro.Show();
                            //    break;

                            ////case "Mantenimiento_Indicador_CostoMttoPreventivo":

                            ////    Mantenimiento_Indicador_CostoMttoPreventivo Mantenimiento_Indicador_CostoMttoPreventivo = new Mantenimiento_Indicador_CostoMttoPreventivo();
                            ////    Mantenimiento_Indicador_CostoMttoPreventivo.definicion = obj.objReporte.descripcion;
                            ////    Mantenimiento_Indicador_CostoMttoPreventivo.formula = obj.objReporte.formula;
                            ////    Mantenimiento_Indicador_CostoMttoPreventivo.Show();
                            ////    break;

                            //case "Mantenimiento_Indicador_CostoMttoTotal_CostoServicio":

                            //    Mantenimiento_Indicador_CostoMttoTotal_CostoServicio Mantenimiento_Indicador_CostoMttoTotal_CostoServicio = new Mantenimiento_Indicador_CostoMttoTotal_CostoServicio();
                            //    Mantenimiento_Indicador_CostoMttoTotal_CostoServicio.definicion = obj.objReporte.descripcion;
                            //    Mantenimiento_Indicador_CostoMttoTotal_CostoServicio.formula = obj.objReporte.formula;
                            //    Mantenimiento_Indicador_CostoMttoTotal_CostoServicio.Show();
                            //    break;


                            #endregion
  
                            #region RRHH

                            case "Operaciones_Reporte_Bonificacion":

                                frmBonodeConductores frmBono = new frmBonodeConductores(); 
                                frmBono.Show();
                                break;

                            case "RRHH_Ficha_Empleados":

                                if (frmFichaEmpleados == null || frmFichaEmpleados.IsDisposed)
                                {
                                    frmFichaEmpleados = new Ficha_Empleados();
                                    frmFichaEmpleados.Show();
                                }
                                else
                                {
                                    frmFichaEmpleados.Activate();
                                    frmFichaEmpleados.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "RRHH_Vencimiento_Contratos":

                                if (frmVencimientoContratos == null || frmVencimientoContratos.IsDisposed)
                                {
                                    frmVencimientoContratos = new Vencimiento_Contratos();
                                    frmVencimientoContratos.Show();
                                }
                                else
                                {
                                    frmVencimientoContratos.Activate();
                                    frmVencimientoContratos.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "RRHH_Utilizacion_Vacaciones":

                                if (frmUtilizacionVacaciones == null || frmUtilizacionVacaciones.IsDisposed)
                                {
                                    frmUtilizacionVacaciones = new Utilizacion_Vacaciones();
                                    frmUtilizacionVacaciones.Show();
                                }
                                else
                                {
                                    frmUtilizacionVacaciones.Activate();
                                    frmUtilizacionVacaciones.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "RRHH_Faltas_Suspensiones":

                                if (frmFaltasySuspensiones == null || frmFaltasySuspensiones.IsDisposed)
                                {
                                    frmFaltasySuspensiones = new Faltas_Suspensiones();
                                    frmFaltasySuspensiones.Show();
                                }
                                else
                                {
                                    frmFaltasySuspensiones.Activate();
                                    frmFaltasySuspensiones.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "RRHH_Aportaciones_Retenciones":

                                if (frmAportacionesRetenciones == null || frmAportacionesRetenciones.IsDisposed)
                                {
                                    frmAportacionesRetenciones = new Aportaciones_Retenciones();
                                    frmAportacionesRetenciones.Show();
                                }
                                else
                                {
                                    frmAportacionesRetenciones.Activate();
                                    frmAportacionesRetenciones.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "RRHH_Asistencia":

                                if (frmAsistencia == null || frmAsistencia.IsDisposed)
                                {
                                    frmAsistencia = new Asistencia();
                                    frmAsistencia.Show();
                                }
                                else
                                {
                                    frmAsistencia.Activate();
                                    frmAsistencia.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "RRHH_AsistenciaCondutores":
                                frmAsistenciaMapeo frmAsistenciaConductor = new frmAsistenciaMapeo();
                                frmAsistenciaConductor.Show();
                                break;
                               
                                
                            case "RRHH_Personal_No_Grato":

                                if (frmPersonalNoGrato == null || frmPersonalNoGrato.IsDisposed)
                                {
                                    frmPersonalNoGrato = new PersonalNoGrato();
                                    frmPersonalNoGrato.Show();
                                }
                                else
                                {
                                    frmPersonalNoGrato.Activate();
                                    frmPersonalNoGrato.WindowState = FormWindowState.Maximized;
                                }
                                break;

                         /*  case "RRHH_Planillas_Trabajadores":

                                if (frmPlanillasTrabajadores == null || frmPlanillasTrabajadores.IsDisposed)
                                {
                                    frmPlanillasTrabajadores = new PlanillasTrabajadores();
                                    frmPlanillasTrabajadores.Show();
                                }
                                else
                                {
                                    frmPlanillasTrabajadores.Activate();
                                    frmPlanillasTrabajadores.WindowState = FormWindowState.Maximized;
                                }
                                break;*/

                            case "RRHH_Lista_Cesados":

                                if (frmTrabajadoresCesados == null || frmTrabajadoresCesados.IsDisposed)
                                {
                                    frmTrabajadoresCesados = new TrabajadoresCesados();
                                    frmTrabajadoresCesados.Show();
                                }
                                else
                                {
                                    frmTrabajadoresCesados.Activate();
                                    frmTrabajadoresCesados.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "Compromisos_Memorandums":
                                frmListarCompromisosMemorandums frmMemos = new frmListarCompromisosMemorandums();
                                frmMemos.Show();
                                break;

                            case "Planilla_Oficial":
                                frmPlanillaOficinalMensual frmPlanilas= new frmPlanillaOficinalMensual();
                                frmPlanilas.Show();
                                break;

                            case "frmConstanciaNoDeudo":
                                frmConstanciaNoDeudo = new frmConstanciaNoDeudo();
                                frmConstanciaNoDeudo.Show();
                                break;

                            case "frmObligacionesMenu":
                                frmObligacionesMenu frmObligacionesMenu = new frmObligacionesMenu();
                                frmObligacionesMenu.Show();
                                break;

                            case "frmReporteIngresoSubsidios":
                                frmReporteIngresoSubsidios frmReporteIngresoSubsidios = new frmReporteIngresoSubsidios();
                                frmReporteIngresoSubsidios.Show();
                            break;

                            case "frmListaSolicitudesPersonal":
                                frmListaSolicitudesPersonal frmListaSolicitudesPersonal = new frmListaSolicitudesPersonal();
                                frmListaSolicitudesPersonal.Show();
                            break;

                            case "frmListaUniformes":
                                frmListaUniformes frmListaUniformes = new frmListaUniformes();
                                frmListaUniformes.Show();
                            break;
                               
                            //case "RRHH_Falta_Conducta_Conductores":

                            //    if (frmFataConductaConductores == null || frmFataConductaConductores.IsDisposed)
                            //    {
                            //        frmFataConductaConductores = new FaltasConductores();
                            //        frmFataConductaConductores.Show();
                            //    }
                            //    else
                            //    {
                            //        frmFataConductaConductores.Activate();
                            //        frmFataConductaConductores.WindowState = FormWindowState.Maximized;
                            //    }
                            //    break;

                            //case "GTH_Indicador_AusentismoPersonal":

                            //    GTH_Indicador_AusentismoPersonal frmGTH_Indicador_AusentismoPersonal = new GTH_Indicador_AusentismoPersonal();
                            //    frmGTH_Indicador_AusentismoPersonal.definicion = obj.objReporte.descripcion;
                            //    frmGTH_Indicador_AusentismoPersonal.formula = obj.objReporte.formula;
                            //    frmGTH_Indicador_AusentismoPersonal.Show();

                            //    break;

                            //case "GTH_Indicador_RotacionPersonal":

                            //    GTH_Indicador_RotacionPersonal frmGTH_Indicador_RotacionPersonal = new GTH_Indicador_RotacionPersonal();
                            //    frmGTH_Indicador_RotacionPersonal.definicion = obj.objReporte.descripcion;
                            //    frmGTH_Indicador_RotacionPersonal.formula = obj.objReporte.formula;
                            //    frmGTH_Indicador_RotacionPersonal.Show();
                            //    break;
                            case "ListaAsistenciaExterna":  
                                ListaAsistenciaExterna ListaAsistenciaExterna = new ListaAsistenciaExterna();
                                ListaAsistenciaExterna.Show();
                            break;

                            case "frmVacacionesPendientes":
                                frmVacacionesPendientes frmVacacionesPendientes = new frmVacacionesPendientes();
                                frmVacacionesPendientes.Show();
                            break;

                            case "frmRegistroCapacitaciones":
                                frmRegistroCapacitaciones frmRegistroCapacitaciones = new frmRegistroCapacitaciones();
                                frmRegistroCapacitaciones.Show();
                            break;
                            #endregion

                            #region Contabilidad

                            //case "Contabilidad_ReporteMovilidad":

                            //    Contabilidad_ReporteMovilidad Contabilidad_ReporteMovilidad = new Contabilidad_ReporteMovilidad();
                            //    Contabilidad_ReporteMovilidad.definicion = obj.objReporte.descripcion;
                            //    Contabilidad_ReporteMovilidad.formula = obj.objReporte.formula;
                            //    Contabilidad_ReporteMovilidad.Show();
                            //    break;

                            //case "Contabilidad_ReporteDetalleLibroDiario":

                            //    Contabilidad_ReporteDetalleLibroDiario Contabilidad_ReporteDetalleLibroDiario = new Contabilidad_ReporteDetalleLibroDiario();
                            //    Contabilidad_ReporteDetalleLibroDiario.definicion = obj.objReporte.descripcion;
                            //    Contabilidad_ReporteDetalleLibroDiario.formula = obj.objReporte.formula;
                            //    Contabilidad_ReporteDetalleLibroDiario.Show();
                            //    break;

                            //case "Contabilidad_ReporteLibroMayor":

                            //    Contabilidad_ReporteLibroMayor Contabilidad_ReporteLibroMayor = new Contabilidad_ReporteLibroMayor();
                            //    Contabilidad_ReporteLibroMayor.definicion = obj.objReporte.descripcion;
                            //    Contabilidad_ReporteLibroMayor.formula = obj.objReporte.formula;
                            //    Contabilidad_ReporteLibroMayor.Show();
                            //    break;

                            //case "Contabilidad_ReporteRegistroCompras":

                            //    Contabilidad_ReporteRegistroCompras Contabilidad_ReporteRegistroCompras = new Contabilidad_ReporteRegistroCompras();
                            //    Contabilidad_ReporteRegistroCompras.definicion = obj.objReporte.descripcion;
                            //    Contabilidad_ReporteRegistroCompras.formula = obj.objReporte.formula;
                            //    Contabilidad_ReporteRegistroCompras.Show();
                            //    break;

                            //case "Contabilidad_ReporteRegistroVentas":

                            //    if (frmRegistroVentas == null || frmRegistroVentas.IsDisposed)
                            //    {
                            //        frmRegistroVentas = new Generacion_Libros();
                            //        frmRegistroVentas.Show();
                            //    }
                            //    else
                            //    {
                            //        frmRegistroVentas.Activate();
                            //        frmRegistroVentas.WindowState = FormWindowState.Maximized;
                            //    }
                            //    break;

                            case "Contabilidad_Generacion_Libros":

                                if (frmGeneracionLibros == null || frmGeneracionLibros.IsDisposed)
                                {
                                    frmGeneracionLibros = new Generacion_Libros();
                                    frmGeneracionLibros.Show();
                                }
                                else
                                {
                                    frmGeneracionLibros.Activate();
                                    frmGeneracionLibros.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            //case "Contabilidad_ReporteLibroDiario":

                            //    Contabilidad_ReporteLibroDiario Contabilidad_ReporteLibroDiario = new Contabilidad_ReporteLibroDiario();
                            //    Contabilidad_ReporteLibroDiario.definicion = obj.objReporte.descripcion;
                            //    Contabilidad_ReporteLibroDiario.formula = obj.objReporte.formula;
                            //    Contabilidad_ReporteLibroDiario.Show();
                            //    break;

                            //case "Contabilidad_Reporte_Sumarizado":

                            //    Contabilidad_Reporte_Sumarizado Contabilidad_ReporteSumarizado = new Contabilidad_Reporte_Sumarizado();
                            //    Contabilidad_ReporteSumarizado.Show();
                            //    break;

                            case "Contabilidad_MayorDetallado":

                                if (frmMayorDetallado == null || frmMayorDetallado.IsDisposed)
                                {
                                    frmMayorDetallado = new MayorDetallado();
                                    frmMayorDetallado.Show();
                                }
                                else
                                {
                                    frmMayorDetallado.Activate();
                                    frmMayorDetallado.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_Reporte_Sumarizado":

                                if (frmSumarizado == null || frmSumarizado.IsDisposed)
                                {
                                    frmSumarizado = new Sumarizado();
                                    frmSumarizado.Show();
                                }
                                else
                                {
                                    frmSumarizado.Activate();
                                    frmSumarizado.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_CajaChica_ReporteGastos":

                                if (frmCajaChicaRptGastos == null || frmCajaChicaRptGastos.IsDisposed)
                                {
                                    frmCajaChicaRptGastos = new CajaChica_ReporteGastos();
                                    frmCajaChicaRptGastos.Show();
                                }
                                else
                                {
                                    frmCajaChicaRptGastos.Activate();
                                    frmCajaChicaRptGastos.WindowState = FormWindowState.Maximized;
                                }
                                break;


                            case "Contabilidad_Viajes_PorFacturar":

                                if (frmViajesPorFacturar == null || frmViajesPorFacturar.IsDisposed)
                                {
                                    frmViajesPorFacturar = new Viajes_PorFacturar();
                                    frmViajesPorFacturar.Show();
                                }
                                else
                                {
                                    frmViajesPorFacturar.Activate();
                                    frmViajesPorFacturar.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Resumen_Viajes_por_Facturar":
                                if (frmResumenViajesporFacturar == null || frmResumenViajesporFacturar.IsDisposed)
                                {
                                    frmResumenViajesporFacturar = new Resumen_Viajes_por_Facturar();
                                    frmResumenViajesporFacturar.Show();
                                }
                                else
                                {
                                    frmResumenViajesporFacturar.Activate();
                                    frmResumenViajesporFacturar.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_Buscar_Facturas":

                                if (frmBuscarFacturas == null || frmBuscarFacturas.IsDisposed)
                                {
                                    frmBuscarFacturas = new Buscar_Facturas();
                                    frmBuscarFacturas.Show();
                                }
                                else
                                {
                                    frmBuscarFacturas.Activate();
                                    frmBuscarFacturas.WindowState = FormWindowState.Maximized;
                                }
                                break;


                            case "Contabilidad_Adelantos_Aplicados":

                                if (frmAdelantosAplicados == null || frmAdelantosAplicados.IsDisposed)
                                {
                                    frmAdelantosAplicados = new Adelantos_Aplicados();
                                    frmAdelantosAplicados.Show();
                                }
                                else
                                {
                                    frmAdelantosAplicados.Activate();
                                    frmAdelantosAplicados.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_Tipos_Servicio":

                                if (frmTipoServicio == null || frmTipoServicio.IsDisposed)
                                {
                                    frmTipoServicio = new Resumen_Tipo_Transporte();
                                    frmTipoServicio.Show();
                                }
                                else
                                {
                                    frmTipoServicio.Activate();
                                    frmTipoServicio.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_Analisis_Fondo_GV":

                                if (frmAnalisisFGV == null || frmAnalisisFGV.IsDisposed)
                                {
                                    frmAnalisisFGV = new AnalisisFondoGV();
                                    frmAnalisisFGV.Show();
                                }
                                else
                                {
                                    frmAnalisisFGV.Activate();
                                    frmAnalisisFGV.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_CxP_vs_Conta":

                                if (frmAdelanto_Contabilidad_vs_CuentasporPagar == null || frmAdelanto_Contabilidad_vs_CuentasporPagar.IsDisposed)
                                {
                                    frmAdelanto_Contabilidad_vs_CuentasporPagar = new Adelanto_Contabilidad_vs_CuentasporPagar();
                                    frmAdelanto_Contabilidad_vs_CuentasporPagar.Show();
                                }
                                else
                                {
                                    frmAdelanto_Contabilidad_vs_CuentasporPagar.Activate();
                                    frmAdelanto_Contabilidad_vs_CuentasporPagar.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_Validacion_CuentasxCobrar":

                                if (frmValidacion_Cuentas_por_Cobrar == null || frmValidacion_Cuentas_por_Cobrar.IsDisposed)
                                {
                                    frmValidacion_Cuentas_por_Cobrar = new Validacion_Cuentas_por_Cobrar();
                                    frmValidacion_Cuentas_por_Cobrar.Show();
                                }
                                else
                                {
                                    frmValidacion_Cuentas_por_Cobrar.Activate();
                                    frmValidacion_Cuentas_por_Cobrar.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_ComercialvsContabilidad":

                                if (frmComercialvsContabilidad == null || frmComercialvsContabilidad.IsDisposed)
                                {
                                    frmComercialvsContabilidad = new ComercialvsContabilidad();
                                    frmComercialvsContabilidad.Show();
                                }
                                else
                                {
                                    frmComercialvsContabilidad.Activate();
                                    frmComercialvsContabilidad.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_Aprobacion_Planillas":

                                if (frmAprobacionPlanillas == null || frmAprobacionPlanillas.IsDisposed)
                                {
                                    frmAprobacionPlanillas = new AprobacionPlanillasMultiple();
                                    frmAprobacionPlanillas.Show();
                                }
                                else
                                {
                                    frmAprobacionPlanillas.Activate();
                                    frmAprobacionPlanillas.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_Control_Facturas":

                                if (frmControlFacturas == null || frmControlFacturas.IsDisposed)
                                {
                                    frmControlFacturas = new ControldeFacturas();
                                    frmControlFacturas.Show();
                                }
                                else
                                {
                                    frmControlFacturas.Activate();
                                    frmControlFacturas.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "Contabilidad_Peajes_Validar":

                                if (frmPeajesValidar == null || frmPeajesValidar.IsDisposed)
                                {
                                    frmPeajesValidar = new Peajes_Verificar();
                                    frmPeajesValidar.Show();
                                }
                                else
                                {
                                    frmPeajesValidar.Activate();
                                    frmPeajesValidar.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Contabilidad_kpi_Transporte":

                                if (frmKPITransporte == null || frmKPITransporte.IsDisposed)
                                {
                                    frmKPITransporte = new frmKpiTransportes();
                                    frmKPITransporte.Show();
                                }
                                else
                                {
                                    frmKPITransporte.Activate();
                                    frmKPITransporte.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "frmSire_GenerarTXT":

                                if (frmSire == null || frmSire.IsDisposed)
                                {
                                    frmSire = new frmSire_GenerarTXT();
                                    frmSire.Show();
                                }
                                else
                                {
                                    frmSire.Activate();
                                    frmSire.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "frmAperturarPeriodos":
                                frmAperturarPeriodos frmAperturarPeriodos = new frmAperturarPeriodos();
                                frmAperturarPeriodos.Show();
                            break;
                            #endregion

                            #region Combustible
                            //case "Combustible_Reporte_Despachos":

                            //    if (frmCargaCombustible == null || frmCargaCombustible.IsDisposed)
                            //    {
                            //        frmCargaCombustible = new CargaCombustible();
                            //        frmCargaCombustible.Show();
                            //    }
                            //    else
                            //    {
                            //        frmCargaCombustible.Activate();
                            //        frmCargaCombustible.WindowState = FormWindowState.Maximized;
                            //    }
                            //    break;

                            case "Combustible_Reporte_Detallados":

                                if (frmRendimientoCombustible == null || frmRendimientoCombustible.IsDisposed)
                                {
                                    frmRendimientoCombustible = new RendimientoCombustible();
                                    frmRendimientoCombustible.Show();
                                }
                                else
                                {
                                    frmRendimientoCombustible.Activate();
                                    frmRendimientoCombustible.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Combustible_Rendimiento":

                                if (frmRendimientoUnidades == null || frmRendimientoUnidades.IsDisposed)
                                {
                                    frmRendimientoUnidades = new RendimientoUnidades();
                                    frmRendimientoUnidades.Show();
                                }
                                else
                                {
                                    frmRendimientoUnidades.Activate();
                                    frmRendimientoUnidades.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Combustible_Reporte_Diario":

                                if (frmRendimientoDiario == null || frmRendimientoDiario.IsDisposed)
                                {
                                    frmRendimientoDiario = new ReporteDiario();
                                    frmRendimientoDiario.Show();
                                }
                                else
                                {
                                    frmRendimientoDiario.Activate();
                                    frmRendimientoDiario.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Combustible_Despachos":

                                if (frmDespachosCombustible == null || frmDespachosCombustible.IsDisposed)
                                {
                                    frmDespachosCombustible = new CargaCombustible();
                                    frmDespachosCombustible.Show();
                                }
                                else
                                {
                                    frmDespachosCombustible.Activate();
                                    frmDespachosCombustible.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "Combustible_Precio_Diario":

                                Combustible_Precio_Diario frmPrecios = new Combustible_Precio_Diario();
                                frmPrecios.Show();
                                break;

                            case "Surtidor_Anexo_Kardex":

                                ReporteTicketSurtidorAnexadoKardex frm = new ReporteTicketSurtidorAnexadoKardex();
                                frm.Show();
                                break;

                            case "Reporte_Kardex_Combustible":

                                ReporteKardexCombustible frmKardex = new ReporteKardexCombustible();
                                frmKardex.Show();
                                break;

                            case "frmBloqueoUnidadXRuta":

                                frmBloqueoUnidadXRuta frmBloqueoUnidadXRuta = new frmBloqueoUnidadXRuta();
                                frmBloqueoUnidadXRuta.Show();
                                break;


                            case "frmImportargasboy":

                                frmImportargasboy frmImportargasboy = new frmImportargasboy();
                                frmImportargasboy.Show();
                                break;

                            case "frmRegistrarUrea":

                                frmRegistrarUrea frmRegistrarUrea = new frmRegistrarUrea();
                                frmRegistrarUrea.Show();
                                break;

                            case "frmInspeccionTanque":
                                frmInspeccionTanque frmInspeccionTanque = new frmInspeccionTanque();
                                frmInspeccionTanque.Show();
                            break;

                            #endregion

                            #region Neumatico

                            //case "Neumatico_CompraNeumatico_PorAnio":

                            //    Neumatico_Indicador_CompraNeumatico Neumatico_CompraNeumatico_PorAnio = new Neumatico_Indicador_CompraNeumatico();
                            //    Neumatico_CompraNeumatico_PorAnio.definicion = obj.objReporte.descripcion;
                            //    Neumatico_CompraNeumatico_PorAnio.formula = obj.objReporte.formula;
                            //    Neumatico_CompraNeumatico_PorAnio.Show();
                            //    break;

                            //case "Neumatico_ReporteScrab_PorAnio":

                            //    Neumatico_ReporteScrab_PorAnio Neumatico_ReporteScrab_PorAnio = new Neumatico_ReporteScrab_PorAnio();
                            //    Neumatico_ReporteScrab_PorAnio.definicion = obj.objReporte.descripcion;
                            //    Neumatico_ReporteScrab_PorAnio.formula = obj.objReporte.formula;
                            //    Neumatico_ReporteScrab_PorAnio.Show();
                            //    break;

                            //case "Neumatico_ServicioReencauche_PorAnio":

                            //    Neumatico_Indicador_ServicioReencauche Neumatico_ServicioReencauche_PorAnio = new Neumatico_Indicador_ServicioReencauche();
                            //    Neumatico_ServicioReencauche_PorAnio.definicion = obj.objReporte.descripcion;
                            //    Neumatico_ServicioReencauche_PorAnio.formula = obj.objReporte.formula;
                            //    Neumatico_ServicioReencauche_PorAnio.Show();
                            //    break;

                            case "Neumatico_Consumo":

                                if (frmConsumoNeumaticos == null || frmConsumoNeumaticos.IsDisposed)
                                {
                                    frmConsumoNeumaticos = new ConsumoNeumaticos();
                                    frmConsumoNeumaticos.Show();
                                }
                                else
                                {
                                    frmConsumoNeumaticos.Activate();
                                    frmConsumoNeumaticos.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "frmKilometrajeUnidades":
                                frmKilometrajeUnidades frmKilometrajeUnidades = new frmKilometrajeUnidades();
                                frmKilometrajeUnidades.Show();
                            break;

                            case "frmIngresarMetasRendimiento":
                                frmIngresarMetasRendimiento frmIngresarMetasRendimiento = new frmIngresarMetasRendimiento();
                                frmIngresarMetasRendimiento.Show();
                            break;

                            case "frmControlNeumaticos":
                                frmControlNeumaticos frmControlNeumaticos = new frmControlNeumaticos();
                                frmControlNeumaticos.Show();
                            break;

                            case "frmListaCodNeumaticos":
                                frmListaCodNeumaticos frmListaCodNeumaticos = new frmListaCodNeumaticos();
                                frmListaCodNeumaticos.Show();
                            break;
                            #endregion

                            #region Operaciones
                          
                            case "Operaciones_Guias_Imp_Desembarque":

                                if (frmGuias_ImpresionDesemparque == null || frmGuias_ImpresionDesemparque.IsDisposed)
                                {
                                    frmGuias_ImpresionDesemparque = new frmGuias_ImpresionDesemparque();
                                    frmGuias_ImpresionDesemparque.Show();
                                }
                                else
                                {
                                    frmGuias_ImpresionDesemparque.Activate();
                                    frmGuias_ImpresionDesemparque.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_ImportacionGuiasSLV":

                                if (frmImportarGuiasAltraSLV == null || frmImportarGuiasAltraSLV.IsDisposed)
                                {
                                    frmImportarGuiasAltraSLV = new frmGuias_ImportarSLV();
                                    frmImportarGuiasAltraSLV.Show();
                                }
                                else
                                {
                                    frmGuias_ImpresionDesemparque.Activate();
                                    frmGuias_ImpresionDesemparque.WindowState = FormWindowState.Maximized;
                                }
                                break;


                            case "Operaciones_Reporte_Consolidado_Fechas":

                                if (frmConsolidadoViajes == null || frmConsolidadoViajes.IsDisposed)
                                {
                                    frmConsolidadoViajes = new Consolidado_Viajes();
                                    frmConsolidadoViajes.Show();
                                }
                                else
                                {
                                    frmConsolidadoViajes.Activate();
                                    frmConsolidadoViajes.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_Reporte_Produccion_Diaria":

                                if (frmProduccionDiaria == null || frmProduccionDiaria.IsDisposed)
                                {
                                    frmProduccionDiaria = new Produccion_Diaria();
                                    frmProduccionDiaria.Show();
                                }
                                else
                                {
                                    frmProduccionDiaria.Activate();
                                    frmProduccionDiaria.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_Reporte_Adelantos_Planillas":

                                if (frmAdelantosPlanillas == null || frmAdelantosPlanillas.IsDisposed)
                                {
                                    frmAdelantosPlanillas = new Adelantos_Planillas();
                                    frmAdelantosPlanillas.Show();
                                }
                                else
                                {
                                    frmAdelantosPlanillas.Activate();
                                    frmAdelantosPlanillas.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_Reporte_GuiasxEntregar":

                                if (frmGuiasxEntregar == null || frmGuiasxEntregar.IsDisposed)
                                {
                                    frmGuiasxEntregar = new Guias_por_entregar();
                                    frmGuiasxEntregar.Show();
                                }
                                else
                                {
                                    frmGuiasxEntregar.Activate();
                                    frmGuiasxEntregar.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_Lista_Conductores":

                                if (frmFichaConductores == null || frmFichaConductores.IsDisposed)
                                {
                                    frmFichaConductores = new Ficha_Conductores();
                                    frmFichaConductores.Show();
                                }
                                else
                                {
                                    frmFichaConductores.Activate();
                                    frmFichaConductores.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "ClientesProveedores":
                                frmClientesProveedores frmClienteProv = new frmClientesProveedores();
                                frmClienteProv.Show();
                            break;

                            case "frmListaContratos":
                            frmListaContratos frmListaContratos = new frmListaContratos();
                            frmListaContratos.Show();
                            break;

                            case "Operaciones_Facturas_Entregadas":

                                if (frmEntregasFacturas == null || frmEntregasFacturas.IsDisposed)
                                {
                                    frmEntregasFacturas = new Entrega_Facturas();
                                    frmEntregasFacturas.Show();
                                }
                                else
                                {
                                    frmEntregasFacturas.Activate();
                                    frmEntregasFacturas.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_Facturas_Lindley":

                                if (frmFacturasLindley == null || frmFacturasLindley.IsDisposed)
                                {
                                    frmFacturasLindley = new Facturas_Lindley();
                                    frmFacturasLindley.Show();
                                }
                                else
                                {
                                    frmFacturasLindley.Activate();
                                    frmFacturasLindley.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_Viajes_Terceros":

                                if (frmViajesTerceros == null || frmViajesTerceros.IsDisposed)
                                {
                                    frmViajesTerceros = new Viajes_Terceros();
                                    frmViajesTerceros.Show();
                                }
                                else
                                {
                                    frmViajesTerceros.Activate();
                                    frmViajesTerceros.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_Unidades_Productivas":

                                if (frmUnidadesProductivas == null || frmUnidadesProductivas.IsDisposed)
                                {
                                    frmUnidadesProductivas = new frmUnidadesProductivas();
                                    frmUnidadesProductivas.Show();
                                }
                                else
                                {
                                    frmUnidadesProductivas.Activate();
                                    frmUnidadesProductivas.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_Lista_Viajes":

                                if (frmListaViajes == null || frmListaViajes.IsDisposed)
                                {
                                    frmListaViajes = new ListaViajes();
                                    frmListaViajes.Show();
                                }
                                else
                                {
                                    frmListaViajes.Activate();
                                    frmListaViajes.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_Cantidad_Viajes":

                                if (frmCantidadViajes == null || frmCantidadViajes.IsDisposed)
                                {
                                    frmCantidadViajes = new CantidadViajes();
                                    frmCantidadViajes.Show();
                                }
                                else
                                {
                                    frmCantidadViajes.Activate();
                                    frmCantidadViajes.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "frmSegundoUsoNeumaticos":

                                if (frmSegundoUsoNeimaticos == null || frmSegundoUsoNeimaticos.IsDisposed)
                                {
                                    frmSegundoUsoNeimaticos = new frmSegundoUsoNeumaticos();
                                    frmSegundoUsoNeimaticos.Show();
                                }
                          
                                break;

                            //case "Operaciones_Lista_de_Condutores":

                            //    if (frmLista_de_Condutores == null || frmLista_de_Condutores.IsDisposed)
                            //    {
                            //        frmLista_de_Condutores = new Lista_de_Condutores();
                            //        frmLista_de_Condutores.Show();
                            //    }
                            //    else
                            //    {
                            //        frmLista_de_Condutores.Activate();
                            //        frmLista_de_Condutores.WindowState = FormWindowState.Maximized;
                            //    }
                            //    break;

                            case "Operaciones_Seguimiento_guias":
                                if (Operaciones_Seguimiento_Guias == null || Operaciones_Seguimiento_Guias.IsDisposed)
                                {
                                    Operaciones_Seguimiento_Guias = new Seguimiento_Guias();
                                    Operaciones_Seguimiento_Guias.Show();
                                }
                                else
                                {
                                    Operaciones_Seguimiento_Guias.Activate();
                                    Operaciones_Seguimiento_Guias.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            //case "Operaciones_GuiasxEstado":
                            //    if (frmGuias_por_Estado == null || frmGuias_por_Estado.IsDisposed)
                            //    {
                            //        frmGuias_por_Estado = new GuiasxEstado();
                            //        frmGuias_por_Estado.Show();
                            //    }
                            //    else
                            //    {
                            //        frmGuias_por_Estado.Activate();
                            //        frmGuias_por_Estado.WindowState = FormWindowState.Maximized;
                            //    }
                            //    break;

                            case "AnexarGuiasRetorno":
                                if (frmAnexarGuiasRetorno == null || frmAnexarGuiasRetorno.IsDisposed)
                                {
                                    frmAnexarGuiasRetorno = new AnexarGuiasRetorno();
                                    frmAnexarGuiasRetorno.Show();
                                }
                                else
                                {
                                    frmGuias_por_Estado.Activate();
                                    frmGuias_por_Estado.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Operaciones_Reporte_GuiasxEstado":
                                GuiasxEstado gpe = new GuiasxEstado();
                                gpe.Show();
                                break;

                            case "Operaciones_Reporte_StatusUnidadesGEOTAB":
                                frmStatusUnidadesGEOTAB frmStatusUnidadesGEOTAB = new frmStatusUnidadesGEOTAB();
                                frmStatusUnidadesGEOTAB.Show();
                                break;

                            case "Operaciones_Programacion_Previajes":
                                frmOperacion_Previajes frmPreviajes = new frmOperacion_Previajes();
                                frmPreviajes.Show();
                                break;

                            case "GT_Liquidacion_Planilla":
                                FrmLiquidacionPlanilla frmLiquidaPlanilla = new FrmLiquidacionPlanilla();
                                frmLiquidaPlanilla.Show();
                                break;

                            case "Ordenes_Trabajo_ots":
                                frmOdenesdeTrabajo frmOts = new frmOdenesdeTrabajo();
                                frmOts.Show();
                                break;

                            case "Reporte_Programaciones_Anulados":
                                frmRptProgramacionesAnulados frmRptPrgAnulado = new frmRptProgramacionesAnulados();
                                frmRptPrgAnulado.Show();
                                break;

                            case "Control_Documentos":
                                frmControlDocumentos frmCtrlDocumentos = new frmControlDocumentos();
                                frmCtrlDocumentos.Show();
                                break;                           

                            case "Despacho_Tickets_Terceros":
                                frmDespachosTerceros frmDespachoTercero = new frmDespachosTerceros();
                                frmDespachoTercero.Show();
                                break;

                            case "Maestro_Unidades_Conductor":
                                frmMaestroConductorUnidades frmMaestroUnidaesCond = new frmMaestroConductorUnidades();
                                frmMaestroUnidaesCond.Show();
                                break;

                            case "FrmListaGuiasElectronicas":
                                FrmListaGuiasElectronicas frmListarGuiasElectronicas = new FrmListaGuiasElectronicas();
                                frmListarGuiasElectronicas.Show();
                                break;

                            case "frmListarPlanillas":
                                frmListarPlanillas frmListarPlanillas = new frmListarPlanillas();
                                frmListarPlanillas.Show();
                            break;

                            case "frmPendientesDiarios":
                                frmPendientesDiarios frmPendientesDiarios = new frmPendientesDiarios();
                                frmPendientesDiarios.Show();
                            break;

                            case "frmRegistroOperatividad":
                                frmRegistroOperatividad frmRegistroOperatividad = new frmRegistroOperatividad();
                                frmRegistroOperatividad.Show();
                            break;

                            case "frmListaTicketsLavadero":
                                frmListaTicketsLavadero frmListaTicketsLavadero = new frmListaTicketsLavadero();
                                frmListaTicketsLavadero.Show();
                            break;

                            case "frmListaControlItems":
                                frmListaControlItems frmListaControlItems = new frmListaControlItems();
                                frmListaControlItems.Show();
                            break;

                            case "frmConstanciaUnidades":
                                frmConstanciaUnidades frmConstanciaUnidades = new frmConstanciaUnidades();
                                frmConstanciaUnidades.Show();
                            break;

                            case "frmListarGuiasViaje":
                                frmListarGuiasViaje frmListarGuiasViaje = new frmListarGuiasViaje();
                                frmListarGuiasViaje.Show();
                            break;

                            case "frmListarGuiasFisicas":
                                frmListarGuiasFisicas frmListarGuiasFisicas = new frmListarGuiasFisicas();
                                frmListarGuiasFisicas.Show();
                            break;

                            case "frmListaTarifasOT":
                                frmListaTarifasOT frmListaTarifasOT = new frmListaTarifasOT();
                                frmListaTarifasOT.Show();
                            break;

                            case "FrmActualizarTarifaViajes":
                            FrmActualizarTarifaViajes frmActualizarTarifas = new FrmActualizarTarifaViajes();
                            frmActualizarTarifas.Show();
                            break;

                            case "frmRegistroCumplimiento":
                                frmRegistroCumplimiento frmRegistroCumplimiento = new frmRegistroCumplimiento();
                                frmRegistroCumplimiento.Show();
                            break;

                            case "frmItinerarioViajes":
                                frmItinerarioViajes frmItinerarioViajes = new frmItinerarioViajes();
                                frmItinerarioViajes.Show();
                            break;

                            case "frmListaTiemposViaje":
                                frmListaTiemposViaje frmListaTiemposViaje = new frmListaTiemposViaje();
                                frmListaTiemposViaje.Show();
                            break;


                            case "frmConsolidado_pesos_operaciones":
                            frmConsolidado_pesos_operaciones frmconsolidadoviajes = new frmConsolidado_pesos_operaciones();
                                frmconsolidadoviajes.Show();
                            break;

                            case "frmPlanVacaComp":
                                frmPlanVacaComp frmPlanVacaComp = new frmPlanVacaComp();
                                frmPlanVacaComp.Show();
                            break;

                            case "frmDocumentosCapacitacion":
                                frmDocumentosCapacitacion frmDocumentosCapacitacion = new frmDocumentosCapacitacion();
                                frmDocumentosCapacitacion.Show();
                            break;
                            //#endregion

                            //#region Gerencia

                            //case "Gerencia_Reporte_Retrasos":

                            //    Gerencia_Retrasos_Viajes retrasos = new Gerencia_Retrasos_Viajes();
                            //    retrasos.Show();
                            //    break;

                            #endregion

                            #region Mantenimiento

                            case "Mante_Consumo_Mantenimiento":

                                if (frmConsumoMantenimiento == null || frmConsumoMantenimiento.IsDisposed)
                                {
                                    frmConsumoMantenimiento = new Consumo_Mantenimiento();
                                    frmConsumoMantenimiento.Show();
                                }
                                else
                                {
                                    frmConsumoMantenimiento.Activate();
                                    frmConsumoMantenimiento.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Mante_Costo_por_Unidades":

                                if (frmCosto_por_Unidades == null || frmCosto_por_Unidades.IsDisposed)
                                {
                                    frmCosto_por_Unidades = new Costo_por_Unidades();
                                    frmCosto_por_Unidades.Show();
                                }
                                else
                                {
                                    frmCosto_por_Unidades.Activate();
                                    frmCosto_por_Unidades.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Mante_Plan_Tractos":

                                if (frmMaestroParametrosTractos == null || frmMaestroParametrosTractos.IsDisposed)
                                {
                                    frmMaestroParametrosTractos = new MaestroParametrosTractos();
                                    frmMaestroParametrosTractos.Show();
                                }
                                else
                                {
                                    frmMaestroParametrosTractos.Activate();
                                    frmMaestroParametrosTractos.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Mante_Mantenimiento_Tractos":

                                if (frmMantenimientoTractos == null || frmMantenimientoTractos.IsDisposed)
                                {
                                    frmMantenimientoTractos = new Mantenimiento_Tractos();
                                    frmMantenimientoTractos.Show();
                                }
                                else
                                {
                                    frmMantenimientoTractos.Activate();
                                    frmMantenimientoTractos.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "Mantenimiento_ControlHerramientas":

                                if (frmControlHerramientas == null || frmControlHerramientas.IsDisposed)
                                {
                                    frmControlHerramientas = new frmControlHerramientas();
                                    frmControlHerramientas.Show();
                                }
                                else
                                {
                                    frmMantenimientoTractos.Activate();
                                    frmMantenimientoTractos.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Bloqueo_Unidades":

                              frmListadoUnidades frmListUnidades  = new frmListadoUnidades();
                              frmListUnidades.Show();
                                
                                break;

                            case "frmListaBaterias":
                                frmListaBaterias frmListaBaterias = new frmListaBaterias();
                                frmListaBaterias.Show();
                            break;

                            case "frmListaMantenimiento":
                            frmListaMantenimiento frmListaMantenimiento = new frmListaMantenimiento();
                            frmListaMantenimiento.Show();
                            break;

                            case "frmListaIncidencias":
                                frmListaIncidencias frmListaIncidencias = new frmListaIncidencias();
                                frmListaIncidencias.Show();
                            break;

                            case "frmRegistroOT":
                                frmRegistroOT frmRegistroOT = new frmRegistroOT();
                                frmRegistroOT.Show();
                            break;

                            case "frmReporteReqServicios":
                                frmReporteReqServicios frmReporteReqServicios = new frmReporteReqServicios();
                                frmReporteReqServicios.Show();
                            break;

                            case "frmListaCanaletas":
                                frmListaCanaletas frmListaCanaletas = new frmListaCanaletas();
                                frmListaCanaletas.Show();
                            break;

                            case "frmRegistroMovimientos":
                                frmRegistroMovimientos frmRegistroMovimientos = new frmRegistroMovimientos();
                                frmRegistroMovimientos.Show();
                            break;

                            case "frmControlOperativos":
                                frmControlOperativos frmControlOperativos = new frmControlOperativos();
                                frmControlOperativos.Show();
                            break;
                            #endregion

                            #region Logistica
                            case "Logistica_Servicios":

                                if (frmServicios == null || frmServicios.IsDisposed)
                                {
                                    frmServicios = new Servicios();
                                    frmServicios.Show();
                                }
                                else
                                {
                                    frmServicios.Activate();
                                    frmServicios.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "Logistica_Compras":

                                if (frmCompras == null || frmCompras.IsDisposed)
                                {
                                    frmCompras = new Compras();
                                    frmCompras.Show();
                                }
                                else
                                {
                                    frmCompras.Activate();
                                    frmCompras.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "Logistica_Productos_por_Rotacion":

                                if (frmProductosporRotacion == null || frmProductosporRotacion.IsDisposed)
                                {
                                    frmProductosporRotacion = new ProductosxRotacion();
                                    frmProductosporRotacion.Show();
                                }
                                else
                                {
                                    frmProductosporRotacion.Activate();
                                    frmProductosporRotacion.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "Logistica_Reporte_InventarioValorizadoPeriodoCerrado_043":

                                if (frmLogistica_ReporteInveValorizadoPeriodoCerrado_043 == null || frmLogistica_ReporteInveValorizadoPeriodoCerrado_043.IsDisposed)
                                {
                                    frmLogistica_ReporteInveValorizadoPeriodoCerrado_043 = new frmInventarioValorizadoPeriodoCerrado_043();
                                    frmLogistica_ReporteInveValorizadoPeriodoCerrado_043.Show();
                                }
                                else
                                {
                                    frmLogistica_ReporteInveValorizadoPeriodoCerrado_043.Activate();
                                    frmLogistica_ReporteInveValorizadoPeriodoCerrado_043.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "Proveedores_CombustibleTercero":
                                frmPrecioCombustibleRuta frmMaestroPrecio = new frmPrecioCombustibleRuta();
                                frmMaestroPrecio.Show();
                                break;

                            case "ImporteRequerimientosMasivos":
                                frmImportRequerimientosMasivos frmRequerimientoMasivo = new frmImportRequerimientosMasivos();
                                frmRequerimientoMasivo.Show();
                                break;
                            case "frmProgramacionAlertasStock":
                                frmProgramacionAlertasStock frmaltertastock = new frmProgramacionAlertasStock();
                                frmaltertastock.Show();
                                break;

                            case "frmRequerimientosCompras":
                                frmRequerimientosCompras frmRequerimientosCompras = new frmRequerimientosCompras();
                                frmRequerimientosCompras.Show();
                            break;

                            case "frmListaFallasMecanicas":
                                frmListaFallasMecanicas frmListaFallasMecanicas = new frmListaFallasMecanicas();
                                frmListaFallasMecanicas.Show();
                                break;

                            case "frmEstadoUnidades":
                                frmEstadoUnidades frmEstadoUnidades = new frmEstadoUnidades();
                                frmEstadoUnidades.Show();
                            break;

                            case "frmListarReclamosClientes":
                            frmListarReclamosClientes frmListarReclamosClientes = new frmListarReclamosClientes();
                            frmListarReclamosClientes.Show();
                            break;

                            case "frmReporte_Req_CentroCostos":
                                frmReporte_Req_CentroCostos open = new frmReporte_Req_CentroCostos();
                                open.Show();
                            break;

                            case "frmImprimirTransacciones":
                                frmImprimirTransacciones frmImprimirTransacciones = new frmImprimirTransacciones();
                                frmImprimirTransacciones.Show();
                            break;

                            case "frmItemsPendientes":
                                frmItemsPendientes frmItemsPendientes = new frmItemsPendientes();
                                frmItemsPendientes.Show();
                            break;

                            case "frmDisponibilidad":
                                frmDisponibilidad frmDisponibilidad = new frmDisponibilidad();
                                frmDisponibilidad.Show();
                            break;

                            case "frmListaOferta":
                                frmListaOferta frmListaOferta = new frmListaOferta();
                                frmListaOferta.Show();
                            break;

                            case "Consumos":
                            Consumos frmConsumos = new Consumos();
                            frmConsumos.Show();
                            break;
                            #endregion

                            #region Sistemas

                            case "SIST_Asignacion_Celulares":

                                if (frmAsignacionesTelefono == null || frmAsignacionesTelefono.IsDisposed)
                                {
                                    frmAsignacionesTelefono = new frmAsignacionesTelefono();
                                    frmAsignacionesTelefono.Show();
                                }
                                else
                                {
                                    frmAsignacionesTelefono.Activate();
                                    frmAsignacionesTelefono.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "SIST_Lista_Celulares":

                                break;

                            case "SIST_Lineas_RPC":

                                if (frmLineasCelulares == null || frmLineasCelulares.IsDisposed)
                                {
                                    frmLineasCelulares = new frmLineasRPC();
                                    frmLineasCelulares.Show();
                                }
                                else
                                {
                                    frmLineasCelulares.Activate();
                                    frmLineasCelulares.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "SIST_Lista_Accesos_Spring":

                                if (frmListaAccesos == null || frmListaAccesos.IsDisposed)
                                {
                                    frmListaAccesos = new frmAccesosSpring();
                                    frmListaAccesos.Show();
                                }
                                else
                                {
                                    frmListaAccesos.Activate();
                                    frmListaAccesos.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "Sistemas_InventarioEquipos":

                                if (frmInventarioEquipos == null || frmInventarioEquipos.IsDisposed)
                                {
                                    frmInventarioEquipos = new frmInventarioEquipos();
                                    frmInventarioEquipos.Show();
                                }
                                else
                                {
                                    frmInventarioEquipos.Activate();
                                    frmInventarioEquipos.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            case "AgendarReuniones":

                                if (frmAgendarReuniones == null || frmAgendarReuniones.IsDisposed)
                                {
                                    frmAgendarReuniones = new AgendarReuniones();
                                    frmAgendarReuniones.Show();
                                }
                                else
                                {
                                    frmAgendarReuniones.Activate();
                                    frmAgendarReuniones.WindowState = FormWindowState.Maximized;
                                }
                                break;
                            #endregion

                            #region Seguridad
                            case "SEG_Falta_Conducta_Conductores":

                                if (frmFataConductaConductores == null || frmFataConductaConductores.IsDisposed)
                                {
                                    frmFataConductaConductores = new FaltasConductores();
                                    frmFataConductaConductores.Show();
                                }
                                else
                                {
                                    frmFataConductaConductores.Activate();
                                    frmFataConductaConductores.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "SEG_Lista_Falta_Conducta_Conductores":

                                if (frmListaConductaConductores == null || frmListaConductaConductores.IsDisposed)
                                {
                                    frmListaConductaConductores = new ListaFaltaConducta();
                                    frmListaConductaConductores.Show();
                                }
                                else
                                {
                                    frmListaConductaConductores.Activate();
                                    frmListaConductaConductores.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "SEG_Reporte_Empleados":

                                if (frmReporte_Empleados == null || frmReporte_Empleados.IsDisposed)
                                {
                                    frmReporte_Empleados = new Reporte_Empleados();
                                    frmReporte_Empleados.Show();
                                }
                                else
                                {
                                    frmReporte_Empleados.Activate();
                                    frmReporte_Empleados.WindowState = FormWindowState.Maximized;
                                }
                                break;

                            case "SEG_Calificativo_Conductores":
                                frmBonoConductores frmbono = new frmBonoConductores();
                                frmbono.Show();
                               
                                break;

                            case "frmListarEPPSxPersonal":
                                frmListarEPPSxPersonal frmEPPS = new frmListarEPPSxPersonal();
                                frmEPPS.Show();
                            break;

                            case "frmGastosReten":
                                frmGastosReten = new frmGastosReten();
                                frmGastosReten.Show();
                            break;

                            case "frmListarCapacitaciones":
                                frmListarCapacitaciones = new frmListarCapacitaciones();
                                frmListarCapacitaciones.Show();
                            break;

                            case "frmControlAlcoholTest":
                                frmControlAlcoholTest = new frmControlAlcoholTest();
                                frmControlAlcoholTest.Show();
                            break;

                            case "frmAgregarGuiasFisicas":
                                frmAgregarGuiasFisicas = new frmAgregarGuiasFisicas();
                                frmAgregarGuiasFisicas.Show();
                            break;

                            case "frmGestionSeguridad":
                                frmGestionSeguridad = new frmGestionSeguridad();
                                frmGestionSeguridad.Show();
                            break;

                            case "frmListaIncidentesSSOMAC":
                                frmListaIncidentesSSOMAC = new frmListaIncidentesSSOMAC();
                                frmListaIncidentesSSOMAC.Show();
                            break;

                            case "frmListaEMO":
                                frmListaEMO = new frmListaEMO();
                                frmListaEMO.Show();
                            break;

                            case "frmDocumentosSIG":
                                frmDocumentosSIG = new frmDocumentosSIG();
                                frmDocumentosSIG.Show();
                            break;

                            case "frmIngresoTerceros":
                                frmIngresoTerceros = new frmIngresoTerceros();
                                frmIngresoTerceros.Show();
                            break;
                            #endregion

                            #region Flota
                            //case "Flota_Vencimiento_Documentos":

                            //    if (frmVencimientoDocumentos == null || frmVencimientoDocumentos.IsDisposed)
                            //    {
                            //        frmVencimientoDocumentos = new Vencimiento_Documentos();
                            //        frmVencimientoDocumentos.Show();
                            //    }
                            //    else
                            //    {
                            //        frmVencimientoDocumentos.Activate();
                            //        frmVencimientoDocumentos.WindowState = FormWindowState.Maximized;
                            //    }
                            //    break;
                            #endregion
                        }
                    }
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnPermisos_Click(object sender, EventArgs e)
        {
            if (frmPermisos == null || frmPermisos.IsDisposed)
            {
                frmPermisos = new Permisos();
                frmPermisos.Show();
            }
            else
            {
                frmPermisos.Activate();
                frmPermisos.WindowState = FormWindowState.Maximized;
            }
        }

        private void gESTIONARREPORTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Administrador_MantenedorReporte frm = new Administrador_MantenedorReporte();
            frm.Show();
        }

        private void registrosDeReportesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //RegistroReportes frm = new RegistroReportes();
            //frm.Show();
        }

        private void treeViewArea_AfterSelect(object sender, TreeViewEventArgs e)
        {
            e.Node.Text.ToString();
            //MessageBox.Show(e.Node.Text);
        }

        public void Insert()
        {
                for (int i = 0; i < Application.OpenForms.Count; i++)
                {
                    Form f = Application.OpenForms[i];
                    if(f != null && usuario != null)
                    {
                        string formulario = "";
                        usuario = Utilitario.Instancia.SesionUsuario.usuario;
                        UsuariosEspeciales();
                        formulario = f.Name;
                        clsReporteBL.Instancia.GuardaRegistrosForms(usuario, formulario);
                        //MessageBox.Show(formulario + " " + usuario,"Informacion");
                    }
                    else
                    {
                        MessageBox.Show("No hay formularios Abiertos", "Informacion");
                    }
                }
        }

        private void treeViewArea_DoubleClick(object sender, EventArgs e)
        {
            Insert();
        }

        private void nUEVOSREPORTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NuevosReportes frm = new NuevosReportes();
            frm.Show();
        }

        private void bASEDEDATOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformacionDB frm = new InformacionDB();
            frm.Show();
        }

        private void cAMBIARBDtoolStripMenuItem_Click(object sender, EventArgs e)
        {
            CambiarBD frm = new CambiarBD();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                lblServidor.Text = Utilitario.Instancia.TextoMenuServidor;
                ColorLabelServidor();
                //Application.Restart();
            }
        }

        private void rEGISTROSDEPERIODOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ControlPeriodos frm = new ControlPeriodos();
            frm.Show();
        }

        private void btnListaReporte_Click(object sender, EventArgs e)
        {
            if (frmListaReporte == null || frmListaReporte.IsDisposed)
            {
                frmListaReporte = new ListarReportes();
                frmListaReporte.Show();
            }
            else
            {
                frmListaReporte.Activate();
                frmListaReporte.WindowState = FormWindowState.Maximized;
            }
        }

        private void gESTIONARPERMISOSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aSIGNARTIKETERASToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lblHora_Click(object sender, EventArgs e)
        {

        }        
    }
}




