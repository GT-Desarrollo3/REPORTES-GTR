using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    public partial class frmListaMantenimiento : Form
    {
        DataTable dtListaMantenimiento = new DataTable();
        DataTable dtListaMantenimientoMaquinas = new DataTable();
        DataTable dtListaMantenimientoEquipos = new DataTable();

        private DataTable dtCumplimientoBase = new DataTable();
        private ContextMenuStrip menuFiltroEstadoCumplimiento;
        private string filtroEstadoCumplimiento = "TODOS";

        DataTable dtListaRecursos = new DataTable();
        DataTable dtListaManoObra = new DataTable();
        DataTable dtActividades = new DataTable();
        DataTable dtInspecciones = new DataTable();
        DataTable dtMttoCorrectivo = new DataTable();
        DataTable dtMttoPredictivo = new DataTable();
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public int xClick3 = 0, yClick3 = 0;
        public int xClick4 = 0, yClick4 = 0;
        public int xClick5 = 0, yClick5 = 0;
        public int xClick6 = 0, yClick6 = 0;
        public int xClick7 = 0, yClick7 = 0;
        public int xClick8 = 0, yClick8 = 0;
        int saveRow = 0, saveCol = 0;
        int saveRow2 = 0, saveCol2 = 0;
        int saveRow3 = 0, saveCol3 = 0;
        int saveRow4 = 0, saveCol4 = 0;
        int saveRow5 = 0, saveCol5 = 0;
        int idRegistro, idVehiculo, OpcionF, idUnidad2, idRuta, idMttoC;
        DataTable dtPermisos = new DataTable();
        int TotalEjecutados = 0, TotalPendientes = 0, TotalMttoProg = 0, TotalMttoEje = 0, TotalInsProg = 0, TotalInsEje = 0, TotalActProg = 0, TotalActEje = 0;
        int FiltroTiempo, OpcionMC, TipoInspeccion = 0;
        string Fecha, Estado;
        public int OpcionC = 0, OpcionC2 = 0, OpcionC3 = 0, OpcionC4 = 0, OpcionFI = 0, OpcionFO = 0, OpcionRecursos = 0, OpcionAct = 0;
        string Area;
        int Tecnica;
        RepositoryItemHyperLinkEdit DirectorioPDF = new RepositoryItemHyperLinkEdit();

        private static readonly Dictionary<string, int> HorasPorM = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            { "M1", 4 },
            { "M2", 8 },
            { "M3", 24 },
            { "M4", 24 },
            { "M5", 48 }
        };

        public frmListaMantenimiento()
        {
            InitializeComponent();
            cbxTipoUnidad.SelectedIndexChanged -= cbxTipoUnidad_SelectedIndexChanged;
            cbxTipoMaquina.SelectedIndexChanged -= cbxTipoMaquina_SelectedIndexChanged;
            cbxTipoEquipo.SelectedIndexChanged -= cbxTipoEquipo_SelectedIndexChanged;
            cbxPlanProg.SelectedIndexChanged -= cbxPlanProg_SelectedIndexChanged;
            cbxPlanTipo.SelectedIndexChanged -= cbxPlanTipo_SelectedIndexChanged;
            cbxTipoR.SelectedIndexChanged -= cbxTipoR_SelectedIndexChanged;
            cbxTipoR2.SelectedIndexChanged -= cbxTipoR2_SelectedIndexChanged;
            cbxTipoA.SelectedIndexChanged -= cbxTipoA_SelectedIndexChanged;
            cbxTipoI.SelectedIndexChanged -= cbxTipoI_SelectedIndexChanged;
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
            cbxEspecialidad.SelectedIndexChanged -= cbxEspecialidad_SelectedIndexChanged;
            cbxMPTipo.SelectedIndexChanged -= cbxMPTipo_SelectedIndexChanged;
            cbxMPTecnica.SelectedIndexChanged -= cbxMPTecnica_SelectedIndexChanged;
            cbxMPSistema.SelectedIndexChanged -= cbxMPSistema_SelectedIndexChanged;
            cbxOperaciones3.SelectedIndexChanged -= cbxOperaciones3_SelectedIndexChanged;
            cbxOperaciones4.SelectedIndexChanged -= cbxOperaciones4_SelectedIndexChanged;
            cbxOperaciones5.SelectedIndexChanged -= cbxOperaciones5_SelectedIndexChanged;
            cbxOperaciones6.SelectedIndexChanged -= cbxOperaciones6_SelectedIndexChanged;
        }

        private void cbxTipoUnidad_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUnidad(); }

        private void cbxTipoMaquina_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMaquinas(); }

        private void cbxTipoEquipo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboEquipo(); }

        private void cbxPlanProg_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones(); }

        private void cbxPlanTipo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMaquinas2(); }

        private void cbxTipoR_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMaquinas3(); }

        private void cbxTipoR2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMaquinas6(); }

        private void cbxTipoA_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMaquinas4(); }

        private void cbxTipoI_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMaquinas5(); }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones2(); }

        private void cbxEspecialidad_SelectedIndexChanged(object sender, EventArgs e) { CargarComboEspecialidades(); }

        private void cbxMPTipo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUnidad2(); }

        private void cbxMPTecnica_SelectedIndexChanged(object sender, EventArgs e) { CargarComboTecnica(); }

        private void cbxMPSistema_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSistema(); }

        private void cbxOperaciones3_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion3(); }

        private void cbxOperaciones4_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion4(); }

        private void cbxOperaciones5_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion5(); }

        private void cbxOperaciones6_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion6(); }

        private void frmListaMantenimiento_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaMantenimiento");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    btnNuevoVehiculo.Enabled = true;
                    btnNuevaMaquina.Enabled = true;
                    btnNuevoEquipo.Enabled = true;
                    btnNuevoVehiculo2.Enabled = true;
                }
                else
                {
                    btnNuevoVehiculo.Enabled = false;
                    btnNuevaMaquina.Enabled = false;
                    btnNuevoEquipo.Enabled = false;
                    btnNuevoVehiculo2.Enabled = true;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    modificarIntervaloToolStripMenuItem.Enabled = true;
                    modificarIntervalo2ToolStripMenuItem.Enabled = true;
                    tsNuevaInspeccion.Enabled = true;
                    btnDesbloquearUnidad.Enabled = true;
                    tsActualizarEstado.Enabled = true;
                    tsEliminarMtto.Enabled = true;
                    btnProgramarCumplimiento.Enabled = true;
                    btnProgramarCumplimiento2.Enabled = true;
                    btnProgramarCumplimiento3.Enabled = true;
                    btnProgramarCumplimiento4.Enabled = true;
                    tsRegistrarCump.Enabled = true;
                    tsEliminarCump.Enabled = true;
                    tsRegistrarCump2.Enabled = true;
                    tsEliminarCump2.Enabled = true;
                    tsRegistrarCump3.Enabled = true;
                    tsEliminarCump3.Enabled = true;
                    tsQuitarMO.Enabled = true;
                }
                else
                {
                    modificarIntervaloToolStripMenuItem.Enabled = false;
                    modificarIntervalo2ToolStripMenuItem.Enabled = false;
                    tsNuevaInspeccion.Enabled = false;
                    btnDesbloquearUnidad.Enabled = false;
                    tsActualizarEstado.Enabled = false;
                    tsEliminarMtto.Enabled = false;
                    btnProgramarCumplimiento.Enabled = false;
                    btnProgramarCumplimiento2.Enabled = false;
                    btnProgramarCumplimiento3.Enabled = false;
                    btnProgramarCumplimiento4.Enabled = false;
                    tsRegistrarCump.Enabled = false;
                    tsEliminarCump.Enabled = false;
                    tsRegistrarCump2.Enabled = false;
                    tsEliminarCump2.Enabled = false;
                    tsRegistrarCump3.Enabled = true;
                    tsEliminarCump3.Enabled = true;
                    tsQuitarMO.Enabled = false;
                }
            }

            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = new DateTime(dtpFechaFin.Value.Year, dtpFechaFin.Value.Month, 1).AddMonths(1);
            dtpInicioMaquina.Value = new DateTime(dtpInicioMaquina.Value.Year, dtpInicioMaquina.Value.Month, 1);
            dtpFinMaquina.Value = new DateTime(dtpFinMaquina.Value.Year, dtpFinMaquina.Value.Month, 1).AddMonths(1);
            dtpFechaInicioEquipo.Value = new DateTime(dtpFechaInicioEquipo.Value.Year, dtpFechaInicioEquipo.Value.Month, 1);
            dtpFechaFinEquipo.Value = new DateTime(dtpFechaFinEquipo.Value.Year, dtpFechaFinEquipo.Value.Month, 1).AddMonths(1);
            dtpFechaInicioR.Value = new DateTime(dtpFechaInicioR.Value.Year, dtpFechaInicioR.Value.Month, 1);
            dtpFechaFinR.Value = new DateTime(dtpFechaFinR.Value.Year, dtpFechaFinR.Value.Month, 1).AddMonths(1);
            dtpFechaInicioR2.Value = new DateTime(dtpFechaInicioR2.Value.Year, dtpFechaInicioR2.Value.Month, 1);
            dtpFechaFinR2.Value = new DateTime(dtpFechaFinR2.Value.Year, dtpFechaFinR2.Value.Month, 1).AddMonths(1);
            dtpFechaInicioA.Value = new DateTime(dtpFechaInicioA.Value.Year, dtpFechaInicioA.Value.Month, 1);
            dtpFechaFinA.Value = new DateTime(dtpFechaFinA.Value.Year, dtpFechaFinA.Value.Month, 1).AddMonths(1);
            dtpFechaInicioI.Value = new DateTime(dtpFechaInicioI.Value.Year, dtpFechaInicioI.Value.Month, 1);
            dtpFechaFinI.Value = new DateTime(dtpFechaFinI.Value.Year, dtpFechaFinI.Value.Month, 1).AddMonths(1);
            dtpFechaCompromiso.Value = DateTime.Now;
            dtpFechaInicio3.Value = new DateTime(dtpFechaInicio3.Value.Year, dtpFechaInicio3.Value.Month, 1);
            dtpFechaFin3.Value = DateTime.Now;
            dtpFechaInicio4.Value = new DateTime(dtpFechaInicio4.Value.Year, dtpFechaInicio4.Value.Month, 1);
            dtpFechaFin4.Value = DateTime.Now;
            dtpAnio.Value = DateTime.Now;
            dtpAnio2.Value = DateTime.Now;
            dtpAnio3.Value = DateTime.Now;
            dtpAnio4.Value = DateTime.Now;
            dtpPeriodo2.Value = DateTime.Now;
            txtNroSemana.Text = "0";
            txtNroSemana2.Text = "0";
            txtNroSemana3.Text = "0";
            txtNroSemana4.Text = "0";

            DateTime date = DateTime.Now;
            dtpPeriodo.Value = new DateTime(date.Year, date.Month, 1);

            CargarComboUnidad();
            CargarComboMaquinas();
            CargarComboEquipo();
            CargarComboOperaciones();
            CargarComboOperaciones2();
            CargarComboMaquinas2();
            CargarComboMaquinas3();
            CargarComboMaquinas4();
            CargarComboMaquinas5();
            CargarComboMaquinas6();
            CargarComboEspecialidades();
            CargarComboUnidad2();
            CargarComboOperacion3();
            CargarComboOperacion4();
            CargarComboOperacion5();
            CargarComboOperacion6();

            cbxPlanProg.Text = "TODO";
            cbxPlanTipo.Text = "TODOS";
            cbxTipoMaquina.Text = "TODOS";
            cbxTipoEquipo.Text = "UNIDAD ESTACIONARIA";
            cbxTipoR.Text = "TODOS";
            cbxTipoR2.Text = "TODOS";
            cbxOrigen.Text = "TODOS";
            cbxOperacion.Text = "TODO";
            cbxMPTipo.Text = "TODOS";
            cbxOperaciones3.Text = "TODO";
            cbxOperaciones4.Text = "TODO";
            cbxOperaciones5.Text = "TODO";
            cbxOperaciones6.Text = "TODO";

            CargarComboTecnica();
            Tecnica = 1;
            cbxMPTecnica_DropDownClosed(sender, e);

            string UsuarioAcceso = Utilitario.Instancia.SesionUsuario.usuario;

            if (UsuarioAcceso == "PNAVARRO" || UsuarioAcceso == "LQUEZADA" || UsuarioAcceso == "MADELEINEC") { cbSucursal.Text = "LIMA"; }
            else { cbSucursal.Text = "TRUJILLO"; }
            
            DataTable dtAreaUsuario = new DataTable();
            dtAreaUsuario = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios(UsuarioAcceso);

            if (dtAreaUsuario.Rows.Count > 0) { Area = dtAreaUsuario.Rows[0]["AREA"].ToString(); }

            if (Area == "MANTENIMIENTO" || Area == "SISTEMAS")
            {
                ListarMantenimientos();
                ListarMantenimientoMaquinas();
                ListarMantenimientoEquipos();
                ListarRecursosAsignados();
                ListarRecursosManoObra();
                ListarActividades();
                ListarInspecciones();
                ListarMttoCorrectivo();
                ListarCalendario();
                ListarMttoPredictivo();
            }
            else
            {
                panel3.Enabled = false;
                toolStrip1.Enabled = false;
                dtgMantenimiento.ContextMenuStrip = null;
                panel1.Enabled = false;
                toolStrip2.Enabled = false;
                dtgMaquinaria.ContextMenuStrip = null;
                panel4.Enabled = false;
                toolStrip3.Enabled = false;
                dtgInspecciones.ContextMenuStrip = null;
                panel6.Enabled = false;
                panel5.Enabled = false;
                toolStrip5.Enabled = false;
                toolStrip6.Enabled = false;
                panel12.Enabled = false;
                panel13.Enabled = false;
                toolStrip9.Enabled = false;
                dtgEquipos.ContextMenuStrip = null;
                toolStrip10.Enabled = false;
                panel14.Enabled = false;
                dtgMttoPredictivo.ContextMenuStrip = null;

                tabInfo.SelectedTab = tabMttoCorrectivo;
            }

            rbPeriodos.Checked = true;
            rbPeriodos_Click(sender, e);
            rbProximaI.Checked = true;
            rbProximaI_Click(sender, e);
            rbPendiente.Checked = true;
            rbPendiente_Click(sender, e);

            dgvPlan.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPlan.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPlan.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvCumplimiento.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCumplimiento.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCumplimiento.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCumplimiento.DataBindingComplete += Dgv_DataBindingComplete_Totales;

            dgvPorcentaje.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPorcentaje.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPorcentaje.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvOperaciones.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperaciones.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperaciones.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvCalendario.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCalendario.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCalendario.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvCumplimiento2.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCumplimiento2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCumplimiento2.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvPorcentaje2.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPorcentaje2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPorcentaje2.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvOperaciones2.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperaciones2.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperaciones2.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvCumplimiento3.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCumplimiento3.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCumplimiento3.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvPorcentaje3.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPorcentaje3.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPorcentaje3.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvOperaciones3.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperaciones3.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperaciones3.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvDesviacion.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDesviacion.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDesviacion.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            CrearFiltroEstadoCumplimiento();

            dgvCumplimiento.ColumnHeaderMouseClick -= dgvCumplimiento_ColumnHeaderMouseClick;
            dgvCumplimiento.ColumnHeaderMouseClick += dgvCumplimiento_ColumnHeaderMouseClick;
        }


        public void CargarComboUnidad()
        {
            DataTable dtTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(5, 1);
            cbxTipoUnidad.DataSource = dtTipo;
            cbxTipoUnidad.DisplayMember = "Descripcion";
            cbxTipoUnidad.ValueMember = "idTipoVehiculo";
        }

        public void CargarComboMaquinas()
        {
            DataTable dtTipo2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(2);
            cbxTipoMaquina.DataSource = dtTipo2;
            cbxTipoMaquina.DisplayMember = "DescripcionLocal";
            cbxTipoMaquina.ValueMember = "TipoMaquina";
        }

        public void CargarComboEquipo()
        {
            DataTable dtTipo3 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(4);
            cbxTipoEquipo.DataSource = dtTipo3;
            cbxTipoEquipo.DisplayMember = "EquipoNombre";
            cbxTipoEquipo.ValueMember = "Equipo";
        }

        public void CargarComboOperaciones()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxPlanProg.DataSource = dtOperaciones;
            cbxPlanProg.DisplayMember = "Descripcion";
            cbxPlanProg.ValueMember = "IdOperacion";
        }

        public void CargarComboOperaciones2()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperacion.DataSource = dtOperaciones;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void CargarComboMaquinas2()
        {
            DataTable dtTipo3 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(3);
            cbxPlanTipo.DataSource = dtTipo3;
            cbxPlanTipo.DisplayMember = "DescripcionLocal";
            cbxPlanTipo.ValueMember = "TipoMaquina";
        }

        public void CargarComboMaquinas3()
        {
            DataTable dtTipo4 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(3);
            cbxTipoR.DataSource = dtTipo4;
            cbxTipoR.DisplayMember = "DescripcionLocal";
            cbxTipoR.ValueMember = "TipoMaquina";
        }

        public void CargarComboMaquinas4()
        {
            DataTable dtTipo5 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(3);
            cbxTipoA.DataSource = dtTipo5;
            cbxTipoA.DisplayMember = "DescripcionLocal";
            cbxTipoA.ValueMember = "TipoMaquina";
        }

        public void CargarComboMaquinas5()
        {
            DataTable dtTipo6 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(3);
            cbxTipoI.DataSource = dtTipo6;
            cbxTipoI.DisplayMember = "DescripcionLocal";
            cbxTipoI.ValueMember = "TipoMaquina";
        }

        public void CargarComboMaquinas6()
        {
            DataTable dtTipo7 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(3);
            cbxTipoR2.DataSource = dtTipo7;
            cbxTipoR2.DisplayMember = "DescripcionLocal";
            cbxTipoR2.ValueMember = "TipoMaquina";
        }

        public void CargarComboEspecialidades()
        {
            DataTable dtTipo8 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMtto("C-2");
            cbxEspecialidad.DataSource = dtTipo8;
            cbxEspecialidad.DisplayMember = "Descripcion";
            cbxEspecialidad.ValueMember = "idEspecialidad";
        }

        public void CargarComboUnidad2()
        {
            DataTable dtTipo9 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(5, 1);
            cbxMPTipo.DataSource = dtTipo9;
            cbxMPTipo.DisplayMember = "Descripcion";
            cbxMPTipo.ValueMember = "idTipoVehiculo";
        }

        public void CargarComboTecnica()
        {
            DataTable dtTipo10 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_ListarTecnicaSistema(1, Tecnica);
            cbxMPTecnica.DataSource = dtTipo10;
            cbxMPTecnica.DisplayMember = "Descripcion";
            cbxMPTecnica.ValueMember = "idTecnica";
        }

        public void CargarComboSistema()
        {
            DataTable dtTipo11 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_ListarTecnicaSistema(2, Tecnica);
            cbxMPSistema.DataSource = dtTipo11;
            cbxMPSistema.DisplayMember = "Descripcion";
            cbxMPSistema.ValueMember = "idSistema";
        }

        public void CargarComboOperacion3()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones3.DataSource = dtOperacion;
            cbxOperaciones3.DisplayMember = "Descripcion";
            cbxOperaciones3.ValueMember = "IdOperacion";
        }

        public void CargarComboOperacion4()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones4.DataSource = dtOperacion;
            cbxOperaciones4.DisplayMember = "Descripcion";
            cbxOperaciones4.ValueMember = "IdOperacion";
        }

        public void CargarComboOperacion5()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones5.DataSource = dtOperacion;
            cbxOperaciones5.DisplayMember = "Descripcion";
            cbxOperaciones5.ValueMember = "IdOperacion";
        }

        public void CargarComboOperacion6()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones6.DataSource = dtOperacion;
            cbxOperaciones6.DisplayMember = "Descripcion";
            cbxOperaciones6.ValueMember = "IdOperacion";
        }

        public void ListarMantenimientos()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaMantenimiento = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMttos(txtPlaca.Text, Convert.ToInt32(cbxTipoUnidad.SelectedValue), dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgMantenimiento.DataSource = dtListaMantenimiento;
                if (dtListaMantenimiento.Rows.Count > 0)
                {
                    dgvMantenimientoVista.Columns["idVehiculo"].Visible = false;
                    dgvMantenimientoVista.Columns["idMantenimientoOP"].Visible = false;

                    dgvMantenimientoVista.Columns["FECHA_UM"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMantenimientoVista.Columns["FECHA_UM"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvMantenimientoVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMantenimientoVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvMantenimientoVista.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMantenimientoVista.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvMantenimientoVista.Columns["ESTADO"].Summary.Clear();
                    dgvMantenimientoVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvMantenimientoVista.BestFitColumns();
                }
            }
        }

        public void ListarMantenimientoMaquinas()
        {
            if (dtpInicioMaquina.Value > dtpFinMaquina.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpInicioMaquina.Focus();
                return;
            }
            else
            {
                dtListaMantenimientoMaquinas = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosMaquinas(txtMaquina.Text, Convert.ToString(cbxTipoMaquina.SelectedValue), dtpInicioMaquina.Text, dtpFinMaquina.Text);
                dtgMaquinaria.DataSource = dtListaMantenimientoMaquinas;
                if (dtListaMantenimientoMaquinas.Rows.Count > 0)
                {
                    dgvMaquinariaVista.Columns["idMantenimientoOP"].Visible = false;

                    dgvMaquinariaVista.Columns["FECHA_UM"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMaquinariaVista.Columns["FECHA_UM"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvMaquinariaVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMaquinariaVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvMaquinariaVista.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMaquinariaVista.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvMaquinariaVista.Columns["ESTADO"].Summary.Clear();
                    dgvMaquinariaVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvMaquinariaVista.BestFitColumns();
                }
            }
        }

        public void ListarMantenimientoEquipos()
        {
            if (dtpFechaInicioEquipo.Value > dtpFechaFinEquipo.Value)
            {
                MessageBox.Show("La Fecha de Inicio debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicioEquipo.Focus();
                return;
            }
            else
            {
                dtListaMantenimientoEquipos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMttosEquipos(txtEquipo.Text, cbxTipoEquipo.Text, dtpFechaInicioEquipo.Text, dtpFechaFinEquipo.Text);
                dtgEquipos.DataSource = dtListaMantenimientoEquipos;
                if (dtListaMantenimientoEquipos.Rows.Count > 0)
                {
                    dgvEquiposVista.Columns["idRegistro"].Visible = false;
                    dgvEquiposVista.Columns["DESCRIPCION"].Visible = false;

                    dgvEquiposVista.Columns["FECHA_ULTIMA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvEquiposVista.Columns["FECHA_ULTIMA"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvEquiposVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvEquiposVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvEquiposVista.Columns["FechaRegistro"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvEquiposVista.Columns["FechaRegistro"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvEquiposVista.Columns["MAQUINARIA"].Summary.Clear();
                    dgvEquiposVista.Columns["MAQUINARIA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvEquiposVista.BestFitColumns();
                }
            }
        }

        public void CargarTablaPlacas()
        {
            TotalEjecutados = 0; TotalPendientes = 0;

            DataTable Tabla, dt, dtContador;
            if (FiltroTiempo == 1)
            {
                Tabla = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMtto(dtpPeriodo.Text, txtPlaca.Text, cbxPlanProg.Text, cbxPlanTipo.Text);
                dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarPlanMtto(dtpPeriodo.Text);
                dtContador = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMtto(dtpPeriodo.Text);
            }
            else
            {
                Tabla = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarPlanMttoFecha(dtpFechaInicio3.Text, dtpFechaFin3.Text, txtPlaca.Text, cbxPlanProg.Text, cbxPlanTipo.Text);
                dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarPlanMtto(dtpFechaFin3.Text.Substring(3, 2) + dtpFechaFin3.Text.Substring(6, 4));
                dtContador = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ContarPlanMttoFechas(dtpFechaInicio3.Text, dtpFechaFin3.Text);
            }

            dgvPlan.DataSource = null;
            dgvPlan.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                dgvPlan.DataSource = dt;
                dgvPlan.AutoResizeColumns();
                dgvPlan.Columns["Nro"].Frozen = true;
                dgvPlan.Columns["Nro"].ReadOnly = true;
                dgvPlan.Columns["PLACA"].Frozen = true;
                dgvPlan.Columns["PLACA"].ReadOnly = true;
                dgvPlan.Columns["OPERACION"].Frozen = true;
                dgvPlan.Columns["OPERACION"].ReadOnly = true;
                dgvPlan.Columns["TIPO_UNIDAD"].Frozen = true;
                dgvPlan.Columns["TIPO_UNIDAD"].ReadOnly = true;
                dgvPlan.Columns["KM_DIFERENCIA"].Frozen = true;
                dgvPlan.Columns["KM_DIFERENCIA"].ReadOnly = true;

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvPlan.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FF992B");
                dgvPlan.EnableHeadersVisualStyles = false;

                dgvPlan.Columns["Nro"].Visible = false;
                dgvPlan.Columns["ULTIMA_FECHA"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow != 0 && saveRow < dgvPlan.Rows.Count)
                    {
                        dgvPlan.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvPlan.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }

            if (dtContador.Rows.Count > 0)
            {
                TotalPendientes = Convert.ToInt32(dtContador.Rows[0]["PENDIENTES"]);
                TotalEjecutados = Convert.ToInt32(dtContador.Rows[0]["EJECUTADOS"]);
            }
            else { TotalPendientes = 0; TotalEjecutados = 0; }

            lblMttoPendiente.Text = "MTTOS. PENDIENTES: " + TotalPendientes + " Unidades";
            lblMttoEjecutado.Text = "MTTOS. REALIZADOS: " + TotalEjecutados + " Unidades";
        }

        public void ListarRecursosAsignados()
        {
            if (dtpFechaInicioR.Value > dtpFechaFinR.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicioR.Focus();
                return;
            }
            else
            {
                dtListaRecursos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarRecursos(dtpFechaInicioR.Text, dtpFechaFinR.Text, txtUnidadR.Text, cbxTipoR.Text, txtActividad.Text);
                dtgRecursos.DataSource = dtListaRecursos;
                if (dtListaRecursos.Rows.Count > 0)
                {
                    dgvRecursosR.Columns["idProcesoMtto"].Visible = false;
                    dgvRecursosR.Columns["idVehiculo"].Visible = false;
                    dgvRecursosR.Columns["idRecursoMtto"].Visible = false;

                    dgvRecursosR.Columns["FECHA_CAMBIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRecursosR.Columns["FECHA_CAMBIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvRecursosR.Columns["FECHA_ACTUAL"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRecursosR.Columns["FECHA_ACTUAL"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvRecursosR.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRecursosR.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvRecursosR.Columns["PLACA"].Summary.Clear();
                    dgvRecursosR.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                    dgvRecursosR.BestFitColumns();
                }
            }
        }

        public void ListarRecursosManoObra()
        {
            if (dtpFechaInicioR2.Value > dtpFechaFinR2.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicioR2.Focus();
                return;
            }
            else
            {
                dtListaManoObra = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ManoObra_ListarManoObra(dtpFechaInicioR2.Text, dtpFechaFinR2.Text, txtUnidadR2.Text, cbxTipoR2.Text, cbxEspecialidad.Text);
                dtgManoObra.DataSource = dtListaManoObra;
                if (dtListaManoObra.Rows.Count > 0)
                {
                    dgvManoObra.Columns["idProcesoMtto"].Visible = false;
                    dgvManoObra.Columns["idVehiculo"].Visible = false;
                    dgvManoObra.Columns["idManoObra"].Visible = false;

                    dgvManoObra.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvManoObra.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvManoObra.Columns["PLACA"].Summary.Clear();
                    dgvManoObra.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                    dgvManoObra.BestFitColumns();
                }
            }
        }

        public void ListarActividades()
        {
            if (dtpFechaInicioA.Value > dtpFechaFinA.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicioA.Focus();
                return;
            }
            else
            {
                dtActividades = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarActividades(dtpFechaInicioA.Text, dtpFechaFinA.Text, txtUnidadA.Text, cbxTipoA.Text, txtActividadA.Text);
                dtgActividades.DataSource = dtActividades;
                if (dtActividades.Rows.Count > 0)
                {
                    dgvActividadesVista.Columns["idProcesoMtto"].Visible = false;
                    dgvActividadesVista.Columns["idVehiculo"].Visible = false;

                    dgvActividadesVista.Columns["FECHA_CAMBIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvActividadesVista.Columns["FECHA_CAMBIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvActividadesVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvActividadesVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvActividadesVista.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvActividadesVista.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvActividadesVista.Columns["PLACA"].Summary.Clear();
                    dgvActividadesVista.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                    dgvActividadesVista.BestFitColumns();
                }
            }
        }

        public void ListarInspecciones()
        {
            dtgInspecciones.DataSource = null;
            dgvInspeccionesVista.Columns.Clear();

            if (dtpFechaInicioI.Value > dtpFechaFinI.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicioI.Focus();
                return;
            }
            else
            {
                dtInspecciones = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspecciones(1, Fecha, dtpFechaInicioI.Text, dtpFechaFinI.Text, txtUnidadI.Text, cbxTipoI.Text, cbSucursal.Text);
                dtgInspecciones.DataSource = dtInspecciones;
                if (dtInspecciones.Rows.Count > 0)
                {
                    dgvInspeccionesVista.Columns["idVehiculo"].Visible = false;
                    dgvInspeccionesVista.Columns["idInspeccionC"].Visible = false;

                    dgvInspeccionesVista.Columns["Mecanico1"].Visible = false;
                    dgvInspeccionesVista.Columns["Electrico2"].Visible = false;
                    dgvInspeccionesVista.Columns["Neumatico3"].Visible = false;
                    dgvInspeccionesVista.Columns["Soldador4"].Visible = false;

                    dgvInspeccionesVista.Columns["PROX_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvInspeccionesVista.Columns["PROX_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvInspeccionesVista.Columns["PROX_MTTO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvInspeccionesVista.Columns["PROX_MTTO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvInspeccionesVista.Columns["PLACA"].Summary.Clear();
                    dgvInspeccionesVista.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total = {0}");

                    dgvInspeccionesVista.BestFitColumns();
                }
            }
        }

        public void ListarDesbloqueo()
        {
            DataTable dtListaDesbloqueo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarDesbloqueoUnidades(txtUnidad.Text, txtRuta.Text);
            dtgListaDesbloqueo.DataSource = dtListaDesbloqueo;
            if (dtListaDesbloqueo.Rows.Count > 0)
            {
                dtgvListaDesbloqueo.Columns["idDesbloqueo"].Visible = false;

                dtgvListaDesbloqueo.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListaDesbloqueo.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dtgvListaDesbloqueo.BestFitColumns();
            }
        }

        public void ListarMttoCorrectivo()
        {
            if (dtpFechaInicio4.Value > dtpFechaFin4.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio4.Focus();
                return;
            }
            else
            {
                dtMttoCorrectivo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarRegistros(txtPlaca4.Text, txtDescripcion.Text, dtpFechaInicio4.Text, dtpFechaFin4.Text, cbxOrigen.Text, Estado);
                dtgMttoCorrectivo.DataSource = dtMttoCorrectivo;

                if (dtMttoCorrectivo.Rows.Count > 0)
                {
                    dgvMttoCorrectivoVista.Columns["FECHA_REPORTADA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMttoCorrectivoVista.Columns["FECHA_REPORTADA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvMttoCorrectivoVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMttoCorrectivoVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvMttoCorrectivoVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvMttoCorrectivoVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvMttoCorrectivoVista.Columns["PLACA"].Summary.Clear();
                    dgvMttoCorrectivoVista.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvMttoCorrectivoVista.BestFitColumns();
                }
            }
        }

        public void ListarMttoPredictivo()
        {
            dtMttoPredictivo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPredictivo_ListarRegistro(txtMPUnidad.Text, Convert.ToInt32(cbxMPTipo.SelectedValue), Convert.ToInt32(cbxMPTecnica.SelectedValue), Convert.ToInt32(cbxMPSistema.SelectedValue));
            dtgMttoPredictivo.DataSource = dtMttoPredictivo;
            if (dtMttoPredictivo.Rows.Count > 0)
            {
                dgvMttoPredictivoVista.Columns["idTecnica"].Visible = false;
                dgvMttoPredictivoVista.Columns["idSistema"].Visible = false;
                
                dgvMttoPredictivoVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvMttoPredictivoVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvMttoPredictivoVista.Columns["UltimaFecha"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvMttoPredictivoVista.Columns["UltimaFecha"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvMttoPredictivoVista.Columns["ESTADO"].Summary.Clear();
                dgvMttoPredictivoVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                dgvMttoPredictivoVista.Columns["INFORME"].ColumnEdit = DirectorioPDF;

                dgvMttoPredictivoVista.BestFitColumns();

                dgvMttoPredictivoVista.Columns["INFORME"].Width = 150;
            }
        }

        public void ListarCumplimientos()
        {
            try
            {
                if (txtNroSemana.Text.Length != 0)
                {
                    DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarCumplimiento(Convert.ToInt32(dtpAnio.Text), Convert.ToInt32(txtNroSemana.Text), cbxOperaciones3.Text);
                    DataTable dtContador = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ContarMttosProgramados(Convert.ToInt32(dtpAnio.Text), Convert.ToInt32(txtNroSemana.Text));
                    DataTable dtPorcentaje = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgruparPorcentaje(1, Convert.ToInt32(dtpAnio.Text), Convert.ToInt32(txtNroSemana.Text));
                    DataTable dtOperacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgruparOperacion(1, Convert.ToInt32(dtpAnio.Text), Convert.ToInt32(txtNroSemana.Text));

                    dgvCumplimiento.DataSource = null;
                    dgvCumplimiento.Columns.Clear();

                    if (dt.Rows.Count > 0)
                    {
                        // Guardamos los datos SIN la fila TOTAL HORAS.
                        // Esta será nuestra fuente original para los filtros.
                        dtCumplimientoBase = dt.Copy();

                        // Cada vez que hacemos una nueva búsqueda,
                        // inicialmente mostramos todos.
                        filtroEstadoCumplimiento = "TODOS";

                        AplicarFiltroEstadoCumplimiento();

                        try
                        {
                            if (saveRow2 != 0 && saveRow2 < dgvCumplimiento.Rows.Count)
                            {
                                dgvCumplimiento.FirstDisplayedScrollingColumnIndex = saveCol2;
                                dgvCumplimiento.FirstDisplayedScrollingRowIndex = saveRow2;
                            }
                        }
                        catch (Exception ex) { MessageBox.Show(ex.Message); }
                    }

                    if (dtContador.Rows.Count > 0)
                    {
                        TotalMttoProg = Convert.ToInt32(dtContador.Rows[0]["TOTAL_UNIDADES"]);
                        TotalMttoEje = Convert.ToInt32(dtContador.Rows[0]["TOTAL_EJECUTADOS"]);
                    }
                    else { TotalMttoProg = 0; TotalMttoEje = 0; }

                    tsTotalUnidades.Text = "TOTAL: " + TotalMttoProg + " Unidades";
                    tsTotalEjecutadas.Text = "EJECUTADAS: " + TotalMttoEje + " Unidades";

                    Double Porcentaje = (Convert.ToDouble(TotalMttoEje) * 100) / Convert.ToDouble(TotalMttoProg);
                    lblporcentaje.Text = Convert.ToDouble(Porcentaje).ToString("F2") + " %";

                    dgvPorcentaje.DataSource = null;
                    dgvPorcentaje.Columns.Clear();
                    dgvPorcentaje.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7AF8FA");
                    dgvPorcentaje.EnableHeadersVisualStyles = false;

                    if (dtPorcentaje.Rows.Count > 0)
                    {
                        dgvPorcentaje.DataSource = dtPorcentaje;
                        dgvPorcentaje.AutoResizeColumns();
                    }

                    // OPERACIONES
                    dgvOperaciones.DataSource = null;
                    dgvOperaciones.Columns.Clear();
                    dgvOperaciones.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7AF8FA");
                    dgvOperaciones.EnableHeadersVisualStyles = false;

                    if (dtOperacion.Rows.Count > 0)
                    {
                        dgvOperaciones.DataSource = dtOperacion;
                        dgvOperaciones.AutoResizeColumns();
                    }
                }
                else { MessageBox.Show("Por favor, ingrese el número de semana.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { }
        }

        private int CalcularHorasCelda(object value)
        {
            if (value == null) return 0;

            var s = value.ToString();
            if (string.IsNullOrWhiteSpace(s)) return 0;

            s = s.Trim().ToUpperInvariant();

            // Toma SOLO el "M#" del inicio (M1..M5), sirve para "M1-2", "M3-1", etc.
            var match = System.Text.RegularExpressions.Regex.Match(s, @"^(M[1-5])");
            if (!match.Success) return 0;

            string key = match.Groups[1].Value; // "M1".."M5"
            int horas;
            return HorasPorM.TryGetValue(key, out horas) ? horas : 0;
        }

        private bool EsColumnaDia(DataColumn col)
        {
            if (col == null) return false;

            string n = col.ColumnName;
            if (string.IsNullOrEmpty(n)) return false;

            n = n.Trim().ToUpperInvariant();

            if (!System.Text.RegularExpressions.Regex.IsMatch(n, @"^[A-ZÑÁÉÍÓÚÜ]{3}\d+$"))
                return false;

            string dia = n.Substring(0, 3);
            return (dia == "SAB" || dia == "SÁB" || dia == "DOM" || dia == "LUN" || dia == "MAR" ||
                    dia == "MIE" || dia == "MIÉ" || dia == "JUE" || dia == "VIE");
        }

        private void AgregarFilaTotalesHoras (DataTable dt, DataGridView dgv)
        {
            if (dt == null || dt.Rows.Count == 0) return;

            DataRow rowTotal = dt.NewRow();
            rowTotal["PLACA"] = "TOTAL HORAS: ";

            foreach (DataColumn col in dt.Columns)
            {
                if (!EsColumnaDia(col)) continue;

                int suma = 0;
                foreach (DataRow r in dt.Rows)
                    suma += CalcularHorasCelda(r[col]);

                rowTotal[col] = suma;
            }

            dt.Rows.Add(rowTotal);
        }

        private void Dgv_DataBindingComplete_Totales(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var dgv = (DataGridView)sender;
            if (dgv.Rows.Count == 0) return;

            // buscar la fila "TOTAL HORAS"
            foreach (DataGridViewRow row in dgv.Rows)
            {
                object v = null;

                if (row.Cells["PLACA"] != null)
                    v = row.Cells["PLACA"].Value;

                string oper = (v == null) ? "" : v.ToString();

                if (oper == "TOTAL HORAS: ")
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                    row.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    row.ReadOnly = true;
                    break;
                }
            }

            // Opcional: mostrar "X h" en columnas día (si son números)
            foreach (DataGridViewColumn c in dgv.Columns)
            {
                if (c.DataPropertyName == null) continue;
                if (EsColumnaDia(((DataTable)dgv.DataSource).Columns[c.DataPropertyName]))
                {
                    c.DefaultCellStyle.Format = "0' h'";
                    c.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        public void ListarCumplimientosI()
        {
            try
            {
                if (txtNroSemana2.Text.Length != 0)
                {
                    DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpInspeccion(Convert.ToInt32(dtpAnio2.Text), Convert.ToInt32(txtNroSemana2.Text), cbxOperaciones4.Text);
                    DataTable dtContador = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpInspecciones(Convert.ToInt32(dtpAnio2.Text), Convert.ToInt32(txtNroSemana2.Text));
                    DataTable dtPorcentaje = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgruparPorcentaje(2, Convert.ToInt32(dtpAnio2.Text), Convert.ToInt32(txtNroSemana2.Text));
                    DataTable dtOperacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgruparOperacion(2, Convert.ToInt32(dtpAnio2.Text), Convert.ToInt32(txtNroSemana2.Text));

                    dgvCumplimiento2.DataSource = null;
                    dgvCumplimiento2.Columns.Clear();

                    if (dt.Rows.Count > 0)
                    {
                        dgvCumplimiento2.DataSource = dt;
                        dgvCumplimiento2.AutoResizeColumns();
                        dgvCumplimiento2.Columns["Nro"].Frozen = true;
                        dgvCumplimiento2.Columns["Nro"].ReadOnly = true;
                        dgvCumplimiento2.Columns["OPERACION"].Frozen = true;
                        dgvCumplimiento2.Columns["OPERACION"].ReadOnly = true;
                        dgvCumplimiento2.Columns["PLACA"].Frozen = true;
                        dgvCumplimiento2.Columns["PLACA"].ReadOnly = true;
                        dgvCumplimiento2.Columns["TIPO_UNIDAD"].Frozen = true;
                        dgvCumplimiento2.Columns["TIPO_UNIDAD"].ReadOnly = true;
                        dgvCumplimiento2.Columns["MARCA"].Frozen = true;
                        dgvCumplimiento2.Columns["MARCA"].ReadOnly = true;
                        dgvCumplimiento2.Columns["PROX_INSPECCION"].Frozen = true;
                        dgvCumplimiento2.Columns["PROX_INSPECCION"].ReadOnly = true;
                        dgvCumplimiento2.Columns["SEMANA"].Frozen = true;
                        dgvCumplimiento2.Columns["SEMANA"].ReadOnly = true;
                        dgvCumplimiento2.Columns["ESTADO"].Frozen = true;
                        dgvCumplimiento2.Columns["ESTADO"].ReadOnly = true;
                        dgvCumplimiento2.Columns["FECHA_INICIO"].Frozen = true;
                        dgvCumplimiento2.Columns["FECHA_INICIO"].ReadOnly = true;
                        dgvCumplimiento2.Columns["FECHA_FIN"].Frozen = true;
                        dgvCumplimiento2.Columns["FECHA_FIN"].ReadOnly = true;
                        dgvCumplimiento2.Columns["FECHA_INGRESO"].Frozen = true;
                        dgvCumplimiento2.Columns["FECHA_INGRESO"].ReadOnly = true;

                        int numeroCol = 0;
                        numeroCol = dt.Columns.Count;
                        dgvCumplimiento2.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FA7A7A");
                        dgvCumplimiento2.EnableHeadersVisualStyles = false;

                        dgvCumplimiento2.Columns["Nro"].Visible = false;
                        dgvCumplimiento2.Columns["FECHA_INICIO"].Visible = false;
                        dgvCumplimiento2.Columns["FECHA_FIN"].Visible = false;

                        try
                        {
                            if (saveRow4 != 0 && saveRow4 < dgvCumplimiento2.Rows.Count)
                            {
                                dgvCumplimiento2.FirstDisplayedScrollingColumnIndex = saveCol4;
                                dgvCumplimiento2.FirstDisplayedScrollingRowIndex = saveRow4;
                            }
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                            MessageBox.Show(error);
                        }
                    }

                    if (dtContador.Rows.Count > 0)
                    {
                        TotalInsProg = Convert.ToInt32(dtContador.Rows[0]["TOTAL_UNIDADES"]);
                        TotalInsEje = Convert.ToInt32(dtContador.Rows[0]["TOTAL_EJECUTADOS"]);
                    }
                    else { TotalInsProg = 0; TotalInsEje = 0; }

                    tsTotalUnidades2.Text = "TOTAL: " + TotalInsProg + " Unidades";
                    tsTotalEjecutadas2.Text = "EJECUTADAS: " + TotalInsEje + " Unidades";

                    Double Porcentaje = (Convert.ToDouble(TotalInsEje) * 100) / Convert.ToDouble(TotalInsProg);
                    lblPorcentaje2.Text = Convert.ToDouble(Porcentaje).ToString("F2") + " %";

                    dgvPorcentaje2.DataSource = null;
                    dgvPorcentaje2.Columns.Clear();
                    dgvPorcentaje2.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FA7A7A");
                    dgvPorcentaje2.EnableHeadersVisualStyles = false;

                    if (dtPorcentaje.Rows.Count > 0)
                    {
                        dgvPorcentaje2.DataSource = dtPorcentaje;
                        dgvPorcentaje2.AutoResizeColumns();
                    }

                    // OPERACIONES
                    dgvOperaciones2.DataSource = null;
                    dgvOperaciones2.Columns.Clear();
                    dgvOperaciones2.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FA7A7A");
                    dgvOperaciones2.EnableHeadersVisualStyles = false;

                    if (dtOperacion.Rows.Count > 0)
                    {
                        dgvOperaciones2.DataSource = dtOperacion;
                        dgvOperaciones2.AutoResizeColumns();
                    }
                }
                else { MessageBox.Show("Por favor, ingrese el número de semana.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { }
        }

        public void ListarCumplimientosA()
        {
            try
            {
                if (txtNroSemana3.Text.Length != 0)
                {
                    DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarCumpActividad(Convert.ToInt32(dtpAnio3.Text), Convert.ToInt32(txtNroSemana3.Text), cbxOperaciones5.Text);
                    DataTable dtContador = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ContarCumpActividades(Convert.ToInt32(dtpAnio3.Text), Convert.ToInt32(txtNroSemana3.Text));
                    DataTable dtPorcentaje = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgruparPorcentaje(3, Convert.ToInt32(dtpAnio3.Text), Convert.ToInt32(txtNroSemana3.Text));
                    DataTable dtOperacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgruparOperacion(3, Convert.ToInt32(dtpAnio3.Text), Convert.ToInt32(txtNroSemana3.Text));
                    
                    dgvCumplimiento3.DataSource = null;
                    dgvCumplimiento3.Columns.Clear();

                    if (dt.Rows.Count > 0)
                    {
                        dgvCumplimiento3.DataSource = dt;
                        dgvCumplimiento3.AutoResizeColumns();
                        dgvCumplimiento3.Columns["Nro"].Frozen = true;
                        dgvCumplimiento3.Columns["Nro"].ReadOnly = true;
                        dgvCumplimiento3.Columns["OPERACION"].Frozen = true;
                        dgvCumplimiento3.Columns["OPERACION"].ReadOnly = true;
                        dgvCumplimiento3.Columns["PLACA"].Frozen = true;
                        dgvCumplimiento3.Columns["PLACA"].ReadOnly = true;
                        dgvCumplimiento3.Columns["TIPO_UNIDAD"].Frozen = true;
                        dgvCumplimiento3.Columns["TIPO_UNIDAD"].ReadOnly = true;
                        dgvCumplimiento3.Columns["ACTIVIDAD"].Frozen = true;
                        dgvCumplimiento3.Columns["ACTIVIDAD"].ReadOnly = true;
                        dgvCumplimiento3.Columns["FECHA_PROYECTADA"].Frozen = true;
                        dgvCumplimiento3.Columns["FECHA_PROYECTADA"].ReadOnly = true;
                        dgvCumplimiento3.Columns["SEMANA"].Frozen = true;
                        dgvCumplimiento3.Columns["SEMANA"].ReadOnly = true;
                        dgvCumplimiento3.Columns["ESTADO"].Frozen = true;
                        dgvCumplimiento3.Columns["ESTADO"].ReadOnly = true;
                        dgvCumplimiento3.Columns["FECHA_INICIO"].Frozen = true;
                        dgvCumplimiento3.Columns["FECHA_INICIO"].ReadOnly = true;
                        dgvCumplimiento3.Columns["FECHA_FIN"].Frozen = true;
                        dgvCumplimiento3.Columns["FECHA_FIN"].ReadOnly = true;
                        dgvCumplimiento3.Columns["FECHA_INGRESO"].Frozen = true;
                        dgvCumplimiento3.Columns["FECHA_INGRESO"].ReadOnly = true;

                        int numeroCol = 0;
                        numeroCol = dt.Columns.Count;
                        dgvCumplimiento3.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FAF17A");
                        dgvCumplimiento3.EnableHeadersVisualStyles = false;

                        dgvCumplimiento3.Columns["Nro"].Visible = false;
                        dgvCumplimiento3.Columns["FECHA_INICIO"].Visible = false;
                        dgvCumplimiento3.Columns["FECHA_FIN"].Visible = false;

                        try
                        {
                            if (saveRow5 != 0 && saveRow5 < dgvCumplimiento3.Rows.Count)
                            {
                                dgvCumplimiento3.FirstDisplayedScrollingColumnIndex = saveCol5;
                                dgvCumplimiento3.FirstDisplayedScrollingRowIndex = saveRow5;
                            }
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                            MessageBox.Show(error);
                        }
                    }

                    if (dtContador.Rows.Count > 0)
                    {
                        TotalActProg = Convert.ToInt32(dtContador.Rows[0]["TOTAL_UNIDADES"]);
                        TotalActEje = Convert.ToInt32(dtContador.Rows[0]["TOTAL_EJECUTADOS"]);
                    }
                    else { TotalActProg = 0; TotalActEje = 0; }

                    tsTotalUnidades3.Text = "TOTAL: " + TotalActProg + " Unidades";
                    tsTotalEjecutadas3.Text = "EJECUTADAS: " + TotalActEje + " Unidades";

                    Double Porcentaje = (Convert.ToDouble(TotalActEje) * 100) / Convert.ToDouble(TotalActProg);
                    lblPorcentaje3.Text = Convert.ToDouble(Porcentaje).ToString("F2") + " %";

                    dgvPorcentaje3.DataSource = null;
                    dgvPorcentaje3.Columns.Clear();
                    dgvPorcentaje3.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FAF17A");
                    dgvPorcentaje3.EnableHeadersVisualStyles = false;

                    if (dtPorcentaje.Rows.Count > 0)
                    {
                        dgvPorcentaje3.DataSource = dtPorcentaje;
                        dgvPorcentaje3.AutoResizeColumns();
                    }

                    // OPERACIONES
                    dgvOperaciones3.DataSource = null;
                    dgvOperaciones3.Columns.Clear();
                    dgvOperaciones3.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FAF17A");
                    dgvOperaciones3.EnableHeadersVisualStyles = false;

                    if (dtPorcentaje.Rows.Count > 0)
                    {
                        dgvOperaciones3.DataSource = dtOperacion;
                        dgvOperaciones3.AutoResizeColumns();
                    }
                }
                else { MessageBox.Show("Por favor, ingrese el número de semana.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { }
        }

        public void ListarCalendario()
        {
            DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarCalendarioMtto(dtpPeriodo2.Text, txtPlaca6.Text, cbxOperacion.Text);

            dgvCalendario.DataSource = null;
            dgvCalendario.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                dgvCalendario.DataSource = dt;
                dgvCalendario.AutoResizeColumns();

                dgvCalendario.Columns["OPERACION"].Frozen = true;
                dgvCalendario.Columns["OPERACION"].ReadOnly = true;
                dgvCalendario.Columns["PLACA"].Frozen = true;
                dgvCalendario.Columns["PLACA"].ReadOnly = true;
                dgvCalendario.Columns["TIPO_UNIDAD"].Frozen = true;
                dgvCalendario.Columns["TIPO_UNIDAD"].ReadOnly = true;

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvCalendario.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#DDA0DD");
                dgvCalendario.EnableHeadersVisualStyles = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow3 != 0 && saveRow3 < dgvCalendario.Rows.Count)
                    {
                        dgvCalendario.FirstDisplayedScrollingColumnIndex = saveCol3;
                        dgvCalendario.FirstDisplayedScrollingRowIndex = saveRow3;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
        }

        public static string CalcularDia(int col)
        {
            string respuesta;

            if (col < 10) { respuesta = "0" + col.ToString(); }
            else { respuesta = col.ToString(); }

            return respuesta;
        }

        private void CrearFiltroEstadoCumplimiento()
        {
            menuFiltroEstadoCumplimiento = new ContextMenuStrip();

            ToolStripMenuItem itemTodos = new ToolStripMenuItem("Todos");
            ToolStripMenuItem itemProgramados = new ToolStripMenuItem("Programados");
            ToolStripMenuItem itemEjecutados = new ToolStripMenuItem("Ejecutados");

            itemTodos.Click += (s, e) =>
            {
                filtroEstadoCumplimiento = "TODOS";
                AplicarFiltroEstadoCumplimiento();
            };

            itemProgramados.Click += (s, e) =>
            {
                filtroEstadoCumplimiento = "PROGRAMADOS";
                AplicarFiltroEstadoCumplimiento();
            };

            itemEjecutados.Click += (s, e) =>
            {
                filtroEstadoCumplimiento = "EJECUTADOS";
                AplicarFiltroEstadoCumplimiento();
            };

            menuFiltroEstadoCumplimiento.Items.Add(itemTodos);
            menuFiltroEstadoCumplimiento.Items.Add(new ToolStripSeparator());
            menuFiltroEstadoCumplimiento.Items.Add(itemProgramados);
            menuFiltroEstadoCumplimiento.Items.Add(itemEjecutados);
        }

        private void AplicarFiltroEstadoCumplimiento()
        {
            try
            {
                if (dtCumplimientoBase == null || dtCumplimientoBase.Columns.Count == 0) return;

                DataTable dtFiltrado;

                switch (filtroEstadoCumplimiento)
                {
                    case "PROGRAMADOS":
                        DataRow[] filasProgramadas = dtCumplimientoBase.Select("ESTADO = 'PROGRAMADO' OR ESTADO = 'REPROGRAMADO'");
                        dtFiltrado = dtCumplimientoBase.Clone();
                        foreach (DataRow row in filasProgramadas)
                            dtFiltrado.ImportRow(row);
                    break;

                    case "EJECUTADOS":
                        DataRow[] filasEjecutadas = dtCumplimientoBase.Select("ESTADO = 'EJECUTADO'");
                        dtFiltrado = dtCumplimientoBase.Clone();
                        foreach (DataRow row in filasEjecutadas)
                            dtFiltrado.ImportRow(row);
                    break;
                    default:
                        dtFiltrado = dtCumplimientoBase.Copy();
                    break;
                }

                if (dtFiltrado.Rows.Count > 0) AgregarFilaTotalesHoras(dtFiltrado, dgvCumplimiento);

                dgvCumplimiento.DataSource = null;
                dgvCumplimiento.DataSource = dtFiltrado;

                ConfigurarDgvCumplimiento();
                ActualizarTituloFiltroEstado();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ConfigurarDgvCumplimiento()
        {
            if (dgvCumplimiento.Columns.Count == 0) return;

            dgvCumplimiento.AutoResizeColumns();

            dgvCumplimiento.Columns["Nro"].Frozen = true;
            dgvCumplimiento.Columns["Nro"].ReadOnly = true;
            dgvCumplimiento.Columns["OPERACION"].Frozen = true;
            dgvCumplimiento.Columns["OPERACION"].ReadOnly = true;
            dgvCumplimiento.Columns["PLACA"].Frozen = true;
            dgvCumplimiento.Columns["PLACA"].ReadOnly = true;
            dgvCumplimiento.Columns["TIPO_UNIDAD"].Frozen = true;
            dgvCumplimiento.Columns["TIPO_UNIDAD"].ReadOnly = true;
            dgvCumplimiento.Columns["MARCA"].Frozen = true;
            dgvCumplimiento.Columns["MARCA"].ReadOnly = true;
            dgvCumplimiento.Columns["UBIGEO"].Frozen = true;
            dgvCumplimiento.Columns["UBIGEO"].ReadOnly = true;
            dgvCumplimiento.Columns["MTTO_PREVENTIVO"].Frozen = true;
            dgvCumplimiento.Columns["MTTO_PREVENTIVO"].ReadOnly = true;
            dgvCumplimiento.Columns["FECHA_PROG"].Frozen = true;
            dgvCumplimiento.Columns["FECHA_PROG"].ReadOnly = true;
            dgvCumplimiento.Columns["SEMANA"].Frozen = true;
            dgvCumplimiento.Columns["SEMANA"].ReadOnly = true;
            dgvCumplimiento.Columns["ESTADO"].Frozen = true;
            dgvCumplimiento.Columns["ESTADO"].ReadOnly = true;
            dgvCumplimiento.Columns["FECHA_INICIO"].Frozen = true;
            dgvCumplimiento.Columns["FECHA_INICIO"].ReadOnly = true;
            dgvCumplimiento.Columns["FECHA_FIN"].Frozen = true;
            dgvCumplimiento.Columns["FECHA_FIN"].ReadOnly = true;
            dgvCumplimiento.Columns["FECHA_INGRESO"].Frozen = true;
            dgvCumplimiento.Columns["FECHA_INGRESO"].ReadOnly = true;
            dgvCumplimiento.Columns["ALINEAMIENTO"].Frozen = true;
            dgvCumplimiento.Columns["ALINEAMIENTO"].ReadOnly = true;

            dgvCumplimiento.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7AF8FA");
            dgvCumplimiento.EnableHeadersVisualStyles = false;

            dgvCumplimiento.Columns["Nro"].Visible = false;
            dgvCumplimiento.Columns["FECHA_INICIO"].Visible = false;
            dgvCumplimiento.Columns["FECHA_FIN"].Visible = false;
        }

        private void ActualizarTituloFiltroEstado()
        {
            if (!dgvCumplimiento.Columns.Contains("ESTADO")) return;

            switch (filtroEstadoCumplimiento)
            {
                case "PROGRAMADOS":
                    dgvCumplimiento.Columns["ESTADO"].HeaderText = "ESTADO ▼ (PROG)";
                break;

                case "EJECUTADOS":
                    dgvCumplimiento.Columns["ESTADO"].HeaderText = "ESTADO ▼ (EJEC)";
                break;

                default:
                    dgvCumplimiento.Columns["ESTADO"].HeaderText = "ESTADO ▼";
                break;
            }
        }

        public void ListarDesviacion()
        {
            try
            {
                if (txtNroSemana4.Text.Length != 0)
                {
                    DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarDesviacion(Convert.ToInt32(dtpAnio4.Text), Convert.ToInt32(txtNroSemana4.Text), cbxOperaciones6.Text, txtTipoMtto.Text);

                    dgvDesviacion.DataSource = null;
                    dgvDesviacion.Columns.Clear();

                    if (dt.Rows.Count > 0)
                    {
                        AgregarFilaTotales(dt);
                        dgvDesviacion.DataSource = dt;
                        dgvDesviacion.AutoResizeColumns();

                        dgvDesviacion.Columns["Nro"].Frozen = true;
                        dgvDesviacion.Columns["Nro"].ReadOnly = true;
                        dgvDesviacion.Columns["PLACA"].Frozen = true;
                        dgvDesviacion.Columns["PLACA"].ReadOnly = true;
                        dgvDesviacion.Columns["OPERACION"].Frozen = true;
                        dgvDesviacion.Columns["OPERACION"].ReadOnly = true;
                        dgvDesviacion.Columns["TIPO_UNIDAD"].Frozen = true;
                        dgvDesviacion.Columns["TIPO_UNIDAD"].ReadOnly = true;
                        dgvDesviacion.Columns["MARCA"].Frozen = true;
                        dgvDesviacion.Columns["MARCA"].ReadOnly = true;
                        dgvDesviacion.Columns["UBIGEO"].Frozen = true;
                        dgvDesviacion.Columns["UBIGEO"].ReadOnly = true;
                        dgvDesviacion.Columns["MTTO_PREVENTIVO"].Frozen = true;
                        dgvDesviacion.Columns["MTTO_PREVENTIVO"].ReadOnly = true;
                        dgvDesviacion.Columns["TIEMPO_PROM"].Frozen = true;
                        dgvDesviacion.Columns["TIEMPO_PROM"].ReadOnly = true;
                        dgvDesviacion.Columns["FECHA_PROG"].Frozen = true;
                        dgvDesviacion.Columns["FECHA_PROG"].ReadOnly = true;
                        dgvDesviacion.Columns["SEMANA"].Frozen = true;
                        dgvDesviacion.Columns["SEMANA"].ReadOnly = true;
                        dgvDesviacion.Columns["ESTADO"].Frozen = true;
                        dgvDesviacion.Columns["ESTADO"].ReadOnly = true;

                        dgvDesviacion.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#00FBFF");
                        dgvDesviacion.EnableHeadersVisualStyles = false;

                        dgvDesviacion.Columns["Nro"].Visible = false;
                        dgvDesviacion.Columns["F_INICIO"].Visible = false;
                        dgvDesviacion.Columns["F_FIN"].Visible = false;
                        dgvDesviacion.Columns["ESTADO"].Visible = false;

                        FormatearFilaTotales();
                    }
                }
                else { MessageBox.Show("Por favor, ingrese el número de semana.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { }
        }

        private void AgregarFilaTotales(DataTable dt)
        {
            TimeSpan totalTiempoProm = TimeSpan.Zero;
            TimeSpan totalTiempoReal = TimeSpan.Zero;

            foreach (DataRow row in dt.Rows)
            {
                totalTiempoProm += ConvertirATiempo(row["TIEMPO_PROM"]);    // TIEMPO PROGRAMADO / PROMEDIO
                totalTiempoReal += ConvertirATiempo(row["TIEMPO_TOTAL"]);   // TIEMPO REAL EJECUTADO
            }

            double desviacion = 0;

            if (totalTiempoProm.TotalSeconds > 0)
            { desviacion = ((totalTiempoReal.TotalSeconds - totalTiempoProm.TotalSeconds) / totalTiempoProm.TotalSeconds) * 100; }

            DataRow filaTotal = dt.NewRow();

            filaTotal["PLACA"] = "TOTALES";

            AsignarTiempo(filaTotal, "TIEMPO_PROM", totalTiempoProm);
            AsignarTiempo(filaTotal, "TIEMPO_TOTAL", totalTiempoReal);

            // Colocar desviación
            if (dt.Columns["DESVIACION"].DataType == typeof(string)) { filaTotal["DESVIACION"] = desviacion.ToString("0") + "%"; }
            else { filaTotal["DESVIACION"] = desviacion; }

            dt.Rows.Add(filaTotal);
        }

        private TimeSpan ConvertirATiempo(object valor)
        {
            if (valor == null || valor == DBNull.Value) return TimeSpan.Zero;

            if (valor is TimeSpan) return (TimeSpan)valor;

            string texto = valor.ToString().Trim();

            if (string.IsNullOrEmpty(texto)) return TimeSpan.Zero;

            string[] partes = texto.Split(':');

            int horas = 0;
            int minutos = 0;
            int segundos = 0;

            if (partes.Length >= 1) int.TryParse(partes[0], out horas);
            if (partes.Length >= 2) int.TryParse(partes[1], out minutos);
            if (partes.Length >= 3) int.TryParse(partes[2], out segundos);

            return TimeSpan.FromHours(horas) + TimeSpan.FromMinutes(minutos) + TimeSpan.FromSeconds(segundos);
        }

        private string FormatearTiempo(TimeSpan tiempo)
        {
            int horas = (int)tiempo.TotalHours;

            return string.Format("{0:00}:{1:00}:{2:00}", horas, tiempo.Minutes, tiempo.Seconds);
        }

        private void AsignarTiempo(DataRow fila, string columna, TimeSpan tiempo)
        {
            Type tipo = fila.Table.Columns[columna].DataType;

            if (tipo == typeof(TimeSpan)) { fila[columna] = tiempo; }
            else { fila[columna] = FormatearTiempo(tiempo); }
        }

        private void FormatearFilaTotales()
        {
            if (dgvDesviacion.Rows.Count == 0) return;

            DataGridViewRow fila = dgvDesviacion.Rows[dgvDesviacion.Rows.Count - 1];
            fila.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#00FBFF");

            fila.DefaultCellStyle.Font = new Font(dgvDesviacion.Font, FontStyle.Bold);

            fila.Cells["DESVIACION"].Style.BackColor = Color.Red;
            fila.Cells["DESVIACION"].Style.ForeColor = Color.White;

            fila.Cells["TIEMPO_PROM"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            fila.Cells["TIEMPO_TOTAL"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            fila.Cells["DESVIACION"].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (dgvDesviacion.Columns["DESVIACION"].ValueType != typeof(string))
            {
                dgvDesviacion.Columns["DESVIACION"].DefaultCellStyle.Format = "0'%'";

                fila.Cells["DESVIACION"].Style.Format = "0'%'";
            }
        }


        private void dgvMantenimientoVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE (%)")
            {
                if (Convert.ToDecimal(e.CellValue) < 60)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) >= 60 && Convert.ToDecimal(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) >= 100 || Convert.ToDecimal(e.CellValue) < 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CONFORME")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "POR VENCER")
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "VENCIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void btnNuevoVehiculo_Click(object sender, EventArgs e)
        {
            frmProgramarMtto frmProgramarMtto = new frmProgramarMtto();
            frmProgramarMtto.Opcion = 1;
            frmProgramarMtto.CargarComboUnidad();
            frmProgramarMtto.cbxTipoUnidad_DropDownClosed(sender, e);
            frmProgramarMtto.label1.Text = "MTTO. DE VEHÍCULOS";
            frmProgramarMtto.cbxAceite.Text = "MINERAL";
            frmProgramarMtto.CargarComboOperacion();
            frmProgramarMtto.cbxAceite_DropDownClosed(sender, e);
            frmProgramarMtto.groupBox2.Enabled = false;
            frmProgramarMtto.formulario = this;
            frmProgramarMtto.txtPlaca.ReadOnly = false;
            frmProgramarMtto.txtFrecuencia.ReadOnly = false;
            frmProgramarMtto.dtpFechaMtto.Value = DateTime.Now;
            frmProgramarMtto.ShowDialog();
        }

        private void btnNuevaMaquina_Click(object sender, EventArgs e)
        {
            frmProgramarMaquina frmProgramarMaquina = new frmProgramarMaquina();
            frmProgramarMaquina.Opcion = 1;
            frmProgramarMaquina.CargarComboUnidad();
            frmProgramarMaquina.cbxTipoUnidad_DropDownClosed(sender, e);
            frmProgramarMaquina.label1.Text = "MTTO. DE MAQUINARIAS";
            frmProgramarMaquina.cbxAceite.Text = "MINERAL";
            frmProgramarMaquina.CargarComboOperacion();
            frmProgramarMaquina.cbxAceite_DropDownClosed(sender, e);
            frmProgramarMaquina.groupBox2.Enabled = false;
            frmProgramarMaquina.formulario = this;
            frmProgramarMaquina.txtPlaca.ReadOnly = false;
            frmProgramarMaquina.txtFrecuencia.ReadOnly = false;
            frmProgramarMaquina.dtpFechaMtto.Value = DateTime.Now;
            frmProgramarMaquina.ShowDialog();
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMantenimientos(); }
        }

        private void cbxTipoUnidad_DropDownClosed(object sender, EventArgs e) { ListarMantenimientos(); }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMantenimientos(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMantenimientos(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarMantenimientos(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgMantenimiento.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE MANTENIMIENTOS DE VEHÍCULOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgMantenimiento.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgMantenimiento_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string Vacio = dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "PLACA").ToString();

                if (Vacio != "")
                {
                    idRegistro = Convert.ToInt32(dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "NRO"));

                    frmProgramarMtto frmProgramarMtto = new frmProgramarMtto();
                    frmProgramarMtto.Opcion = 2;
                    frmProgramarMtto.CargarComboUnidad();
                    frmProgramarMtto.cbxTipoUnidad.Enabled = false;
                    frmProgramarMtto.cbxTipoUnidad.Text = Convert.ToString(dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "TIPO_UNIDAD"));
                    frmProgramarMtto.label1.Text = "ACTUALIZAR MTTO. DE VEHÍCULOS";
                    frmProgramarMtto.formulario = this;
                    frmProgramarMtto.txtPlaca.ReadOnly = true;
                    frmProgramarMtto.FiltrarMantenimiento(idRegistro, sender, e);

                    if (Convert.ToString(dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "ACEITE")) == "")
                    {
                        frmProgramarMtto.label5.Visible = false;
                        frmProgramarMtto.cbxAceite.Visible = false;
                    }

                    frmProgramarMtto.CargarComboOperacion();
                    frmProgramarMtto.cbxOperacion.SelectedValue = Convert.ToInt32(dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "idMantenimientoOP"));
                    frmProgramarMtto.CargarComboMtto(Convert.ToInt32(frmProgramarMtto.cbxOperacion.SelectedValue));
                    frmProgramarMtto.cbxTipoMtto.SelectedValue = Convert.ToInt32(Convert.ToInt32(dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "PS")) + 1);
                    frmProgramarMtto.ShowDialog();
                }
            }
            catch { }
        }

        private void dtgMaquinaria_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string Vacio = dgvMantenimientoVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "PLACA").ToString();

                if (Vacio != "")
                {
                    idRegistro = Convert.ToInt32(dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "NRO"));

                    frmProgramarMaquina frmProgramarMaquina = new frmProgramarMaquina();
                    frmProgramarMaquina.Opcion = 2;
                    frmProgramarMaquina.CargarComboUnidad();
                    frmProgramarMaquina.cbxTipoUnidad.Enabled = false;
                    frmProgramarMaquina.cbxTipoUnidad.Text = Convert.ToString(dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "GRUPO"));
                    frmProgramarMaquina.label1.Text = "ACTUALIZAR MTTO. DE MAQUINARIAS";
                    frmProgramarMaquina.formulario = this;
                    frmProgramarMaquina.txtPlaca.ReadOnly = true;
                    frmProgramarMaquina.FiltrarMantenimiento(idRegistro, sender, e);

                    if (Convert.ToString(dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "ACEITE")) == "")
                    {
                        frmProgramarMaquina.label5.Visible = false;
                        frmProgramarMaquina.cbxAceite.Visible = false;
                    }

                    frmProgramarMaquina.CargarComboOperacion();
                    frmProgramarMaquina.cbxOperacion.SelectedValue = Convert.ToInt32(dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "idMantenimientoOP"));
                    frmProgramarMaquina.CargarComboMtto(Convert.ToInt32(frmProgramarMaquina.cbxOperacion.SelectedValue));
                    frmProgramarMaquina.cbxTipoMtto.SelectedValue = Convert.ToInt32(Convert.ToInt32(dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "PS")) + 1);
                    frmProgramarMaquina.ShowDialog();
                }
            }
            catch { }
        }

        private void modificarIntervaloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionF = 1;
                pModificarFrec.Visible = true;
                pModificarFrec.BringToFront();
                txtPlacaFrec.Text = Convert.ToString(dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "PLACA"));
                txtNuevaFrec.Text = Convert.ToString(dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "FREC/KM"));
                idVehiculo = Convert.ToInt32(dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "idVehiculo"));
            }
            catch { }
        }

        private void modificarIntervalo2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionF = 2;
                pModificarFrec.Visible = true;
                pModificarFrec.BringToFront();
                txtPlacaFrec.Text = Convert.ToString(dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "PLACA"));
                txtNuevaFrec.Text = Convert.ToString(dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "FREC/KM"));
                idVehiculo = 0;
            }
            catch { }
        }

        private void pModificarFrec_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pModificarFrec.Left = pModificarFrec.Left + (e.X - xClick);
                pModificarFrec.Top = pModificarFrec.Top + (e.Y - yClick);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pModificarFrec.Visible = false;
            pModificarFrec.SendToBack();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtNuevaFrec.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el intervalo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNuevaFrec.Focus();
                return;
            }
            else
            {
                if (MessageBox.Show("¿Desea actualizar la frecuencia?", "MODIFICAR FRECUENCIA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtModificar = new DataTable();
                    string respta;

                    dtModificar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ModificarFrecuencia(OpcionF, idVehiculo, txtPlacaFrec.Text, Convert.ToInt32(txtNuevaFrec.Text));
                    respta = Convert.ToString(dtModificar.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        if (OpcionF == 1) { ListarMantenimientos(); }
                        else { ListarMantenimientoMaquinas(); }
                        
                        pModificarFrec.Visible = false;
                        pModificarFrec.SendToBack();
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void txtNuevaFrec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnModificar_Click(sender, e); }
        }

        private void historialDeMantenimientosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmHistorialMtto frmHistorialMtto = new frmHistorialMtto();
                frmHistorialMtto.Opcion = 1;
                frmHistorialMtto.cbxTipoUnidad.Visible = true;
                frmHistorialMtto.cbxTipoMaquina.Visible = false;
                frmHistorialMtto.cbxTipoEquipo.Visible = false;
                frmHistorialMtto.cbxTipoUnidad.BringToFront();
                frmHistorialMtto.CargarComboUnidad();
                frmHistorialMtto.txtPlaca.Text = dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "PLACA").ToString();
                frmHistorialMtto.cbxTipoUnidad.Text = dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "TIPO_UNIDAD").ToString();
                frmHistorialMtto.dtpFechaInicio.Value = dtpFechaInicio.Value;
                frmHistorialMtto.dtpFechaFin.Value = dtpFechaFin.Value;
                frmHistorialMtto.ShowDialog();
            }
            catch { }
        }

        private void historialDeMantenimientos2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmHistorialMtto frmHistorialMtto = new frmHistorialMtto();
                frmHistorialMtto.Opcion = 2;
                frmHistorialMtto.cbxTipoMaquina.Visible = true;
                frmHistorialMtto.cbxTipoUnidad.Visible = false;
                frmHistorialMtto.cbxTipoEquipo.Visible = false;
                frmHistorialMtto.cbxTipoMaquina.BringToFront();
                frmHistorialMtto.CargarComboMaquinas();
                frmHistorialMtto.txtPlaca.Text = dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "PLACA").ToString();
                frmHistorialMtto.cbxTipoMaquina.Text = dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "MAQUINA").ToString();
                frmHistorialMtto.dtpFechaInicio.Value = dtpInicioMaquina.Value;
                frmHistorialMtto.dtpFechaFin.Value = dtpFinMaquina.Value;
                frmHistorialMtto.ShowDialog();
            }
            catch { }
        }

        private void btnHistorialMtto_Click(object sender, EventArgs e)
        {
            frmHistorialMtto frmHistorialMtto = new frmHistorialMtto();
            frmHistorialMtto.Opcion = 1;
            frmHistorialMtto.cbxTipoUnidad.Visible = true;
            frmHistorialMtto.cbxTipoMaquina.Visible = false;
            frmHistorialMtto.cbxTipoEquipo.Visible = false;
            frmHistorialMtto.cbxTipoUnidad.BringToFront();
            frmHistorialMtto.CargarComboUnidad();
            frmHistorialMtto.cbxTipoUnidad.Text = cbxTipoUnidad.Text;
            frmHistorialMtto.dtpFechaInicio.Value = dtpFechaInicio.Value;
            frmHistorialMtto.dtpFechaFin.Value = dtpFechaFin.Value;
            frmHistorialMtto.ShowDialog();
        }

        private void btnHistorialMtto2_Click(object sender, EventArgs e)
        {
            frmHistorialMtto frmHistorialMtto = new frmHistorialMtto();
            frmHistorialMtto.Opcion = 2;
            frmHistorialMtto.cbxTipoMaquina.Visible = true;
            frmHistorialMtto.cbxTipoUnidad.Visible = false;
            frmHistorialMtto.cbxTipoEquipo.Visible = false;
            frmHistorialMtto.cbxTipoMaquina.BringToFront();
            frmHistorialMtto.CargarComboMaquinas();
            frmHistorialMtto.cbxTipoUnidad.Text = cbxTipoMaquina.Text;
            frmHistorialMtto.dtpFechaInicio.Value = dtpInicioMaquina.Value;
            frmHistorialMtto.dtpFechaFin.Value = dtpFinMaquina.Value;
            frmHistorialMtto.ShowDialog();
        }

        private void btnRegistroKM_Click(object sender, EventArgs e)
        {
            frmRegistroKM frmRegistroKM = new frmRegistroKM();
            frmRegistroKM.frmListaMantenimiento = this;
            frmRegistroKM.ShowDialog();
        }

        private void btnRegistroKM2_Click(object sender, EventArgs e)
        {
            frmRegistroKMMaquinas frmRegistroKMMaquinas = new frmRegistroKMMaquinas();
            frmRegistroKMMaquinas.frmListaMantenimiento = this;
            frmRegistroKMMaquinas.ShowDialog();
        }

        private void listaProcesosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmControlMtto frmControlMtto = new frmControlMtto();
                frmControlMtto.idVehiculo = Convert.ToInt32(dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "idVehiculo"));
                frmControlMtto.lblPlaca.Text = dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "PLACA").ToString();
                frmControlMtto.lblOperacion.Text = dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "OPERACION").ToString();
                frmControlMtto.KMActual = Convert.ToDecimal(dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "KM_ACTUAL"));
                frmControlMtto.lblMarca.Text = dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "MARCA").ToString();
                frmControlMtto.lblModelo.Text = dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "MODELO").ToString();
                frmControlMtto.ShowDialog();
            }
            catch { }
        }

        private void listaProcesos2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmControlMttoMaquina frmControlMttoMaquina = new frmControlMttoMaquina();
                frmControlMttoMaquina.CodigoMaquina = dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "PLACA").ToString();
                frmControlMttoMaquina.lblPlaca.Text = dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "PLACA").ToString();
                frmControlMttoMaquina.KMActual = Convert.ToDecimal(dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "KM_ACTUAL"));
                frmControlMttoMaquina.lblMarca.Text = dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "MARCA").ToString();
                frmControlMttoMaquina.lblModelo.Text = dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "MODELO").ToString();
                frmControlMttoMaquina.ShowDialog();
            }
            catch { }
        }

        private void dtgMantenimiento_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dgvMantenimientoVista.GetRowCellValue(dgvMantenimientoVista.FocusedRowHandle, "PLACA").ToString();

                if (Vacio != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { modificarIntervaloToolStripMenuItem.Enabled = true; }
                    historialDeMantenimientosToolStripMenuItem.Enabled = true;
                    listaProcesosToolStripMenuItem.Enabled = true;
                }
            }
            catch
            {
                modificarIntervaloToolStripMenuItem.Enabled = false;
                historialDeMantenimientosToolStripMenuItem.Enabled = false;
                listaProcesosToolStripMenuItem.Enabled = false;
            }
        }

        private void dtgMaquinaria_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dgvMaquinariaVista.GetRowCellValue(dgvMaquinariaVista.FocusedRowHandle, "PLACA").ToString();

                if (Vacio != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { modificarIntervalo2ToolStripMenuItem.Enabled = true; }
                    historialDeMantenimientos2ToolStripMenuItem.Enabled = true;
                    listaProcesos2ToolStripMenuItem.Enabled = true;
                }
            }
            catch
            {
                modificarIntervalo2ToolStripMenuItem.Enabled = false;
                historialDeMantenimientos2ToolStripMenuItem.Enabled = false;
                listaProcesos2ToolStripMenuItem.Enabled = false;
            }
        }

        private void txtMaquina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMantenimientoMaquinas(); }
        }

        private void cbxTipoMaquina_DropDownClosed(object sender, EventArgs e) { ListarMantenimientoMaquinas(); }

        private void dtpInicioMaquina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMantenimientoMaquinas(); }
        }

        private void dtpFinMaquina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMantenimientoMaquinas(); }
        }

        private void btnBuscarMaquina_Click(object sender, EventArgs e) { ListarMantenimientoMaquinas(); }

        private void btnExcelMaquina_Click(object sender, EventArgs e)
        {
            if (dtgMaquinaria.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE MANTENIMIENTOS DE MAQUINARIAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgMaquinaria.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvMaquinariaVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE (%)")
            {
                if (Convert.ToDecimal(e.CellValue) < 60)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) >= 60 && Convert.ToDecimal(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) >= 100 || Convert.ToDecimal(e.CellValue) < 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CONFORME")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "POR VENCER")
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "VENCIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void btnBuscarPlan_Click(object sender, EventArgs e) { CargarTablaPlacas(); }

        private void btnPlanExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPlan.DataSource == null) { MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    DataTable dtExcel = new DataTable();
                    gcExcelPlacas.DataSource = null;
                    gvExcelPlacas.Columns.Clear();
                    dtExcel = Utilitario.Instancia.GetContentAsDataTable(dgvPlan, true);
                    gcExcelPlacas.DataSource = dtExcel;

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Plan de Mtto Preventivo - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    gcExcelPlacas.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtPlanPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { CargarTablaPlacas(); }
        }

        private void dtpPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { CargarTablaPlacas(); }
        }

        private void cbxPlanProg_DropDownClosed(object sender, EventArgs e) { CargarTablaPlacas(); }

        private void cbxPlanTipo_DropDownClosed(object sender, EventArgs e) { CargarTablaPlacas(); }

        private void dgvPlan_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvPlan.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 6)
                        {
                            dgvPlan.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvPlan.ContextMenuStrip = null;
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dgvPlan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvPlan.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvPlan.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("(E)")) { e.CellStyle.BackColor = Color.LimeGreen; }
                            else { e.CellStyle.BackColor = Color.LightSkyBlue; }
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dtpFechaInicioR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRecursosAsignados(); }
        }

        private void dtpFechaFinR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRecursosAsignados(); }
        }

        private void txtUnidadR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRecursosAsignados(); }
        }

        private void txtActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRecursosAsignados(); }
        }

        private void cbxTipoR_DropDownClosed(object sender, EventArgs e) { ListarRecursosAsignados(); }

        private void dgvRecursosR_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "%")
            {
                if (Convert.ToDecimal(e.CellValue) < 60)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) >= 60 && Convert.ToDecimal(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) >= 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
            }
        }

        private void btnBuscarR_Click(object sender, EventArgs e) { ListarRecursosAsignados(); }

        private void btnExcelR_Click(object sender, EventArgs e)
        {
            if (dtgRecursos.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "INSUMO DE RECURSOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgRecursos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void tsResumen_Click(object sender, EventArgs e)
        {
            frmResumenRecursos frmResumenRecursos = new frmResumenRecursos();
            frmResumenRecursos.dtpFechaInicio.Value = dtpFechaInicioR.Value;
            frmResumenRecursos.dtpFechaFin.Value = dtpFechaFinR.Value;
            frmResumenRecursos.dtpFechaInicio2.Value = dtpFechaInicioR2.Value;
            frmResumenRecursos.dtpFechaFin2.Value = dtpFechaFinR2.Value;
            frmResumenRecursos.ShowDialog();
        }

        private void tsEliminarItems_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpcionAct == 0)
                {
                    tsEliminarItems.Text = "Eliminar Recursos";
                    dgvRecursosR.OptionsSelection.MultiSelect = true;
                    dgvRecursosR.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    OpcionAct = 1;
                }
                else
                {
                    int[] filas = dgvRecursosR.GetSelectedRows();

                    if (filas.Length != 0)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                        string respta;
                        int Correcto = 0;

                        if (MessageBox.Show("¿Desea eliminar los items de las actividades?", "ELIMINAR ÍTEMS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            for (int i = 0; i < filas.Length; i++)
                            {
                                int idProcesoMtto = Convert.ToInt32(dgvRecursosR.GetRowCellValue(filas[i], "idProcesoMtto"));
                                int idRecursoMtto = Convert.ToInt32(dgvRecursosR.GetRowCellValue(filas[i], "idRecursoMtto"));
                                int idVehiculo = Convert.ToInt32(dgvRecursosR.GetRowCellValue(filas[i], "idVehiculo"));
                                string Placa = Convert.ToString(dgvRecursosR.GetRowCellValue(filas[i], "PLACA"));
                                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_RegistrarEliminarRecursos(2, idRecursoMtto, idProcesoMtto,
                                                                 idVehiculo, Placa, "", "", 0, Usuario);
                                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                string NroRspta = respta.Substring(0, 1);
                                if (NroRspta == "0") { Correcto = Correcto + 1; }
                                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            }

                            if (Correcto == filas.Length) { MessageBox.Show("0 = El ítem ha sido asignado a las actividades.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                            tsEliminarItems.Text = "Seleccionar Recursos";
                            dgvRecursosR.OptionsSelection.MultiSelect = false;
                            dgvRecursosR.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                            OpcionAct = 0;
                            ListarRecursosAsignados();
                        }
                    }
                    else
                    {
                        tsEliminarItems.Text = "Asignar Recursos";
                        dgvRecursosR.OptionsSelection.MultiSelect = false;
                        dgvRecursosR.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        OpcionAct = 0;
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar los ítems.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pDesbloquearUnidad_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pDesbloquearUnidad.Left = pDesbloquearUnidad.Left + (e.X - xClick2);
                pDesbloquearUnidad.Top = pDesbloquearUnidad.Top + (e.Y - yClick2);
            }
        }

        private void btnDesbloquearUnidad_Click(object sender, EventArgs e)
        {
            pDesbloquearUnidad.Visible = true;
            pDesbloquearUnidad.BringToFront();
            ListarDesbloqueo();
        }

        private void pCerrar2_Click(object sender, EventArgs e)
        {
            pDesbloquearUnidad.Visible = false;
            pDesbloquearUnidad.SendToBack();
            txtUnidad.Clear();
            txtRuta.Clear();
        }

        private void txtUnidad_Enter(object sender, EventArgs e) { txtUnidad.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtUnidad_Leave(object sender, EventArgs e) { txtUnidad.BackColor = Color.White; }

        private void txtUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarTractos(txtUnidad.Text, 1), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 80;
            lstPlaca.Columns[2].Width = 0;
            lstPlaca.Columns[3].Width = 116;
            lstPlaca.Columns[4].Width = 0;
            lstPlaca.Columns[5].Width = 0;
            lstPlaca.Columns[6].Width = 0;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                idUnidad2 = 0;
            }
        }

        private void txtUnidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca.Focus(); }
        }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            if (!lstPlaca.Items.Count.Equals(0)) { lstPlaca.Items[0].Selected = true; }
        }

        private void lstPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlaca.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlaca.SelectedItems[0];
                idUnidad2 = Int32.Parse(ItemActual.Text);
                txtUnidad.Text = ItemActual.SubItems[1].Text;

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                ListarDesbloqueo();
                txtRuta.Focus();
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];
            idUnidad2 = Int32.Parse(ItemActual.Text);
            txtUnidad.Text = ItemActual.SubItems[1].Text;

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            ListarDesbloqueo();
            txtRuta.Focus();
        }

        private void txtRuta_Enter(object sender, EventArgs e) { txtRuta.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtRuta_Leave(object sender, EventArgs e) { txtRuta.BackColor = Color.White; }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lvRuta, clsConsultaBL.Instancia.GetRutasActivas(txtRuta.Text), true, false, false);
            lvRuta.Columns[0].Width = 0;
            lvRuta.Columns[1].Width = 350;
            lvRuta.BringToFront();
            lvRuta.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRuta.Visible = false;
                lvRuta.SendToBack();
                idRuta = 0;
            }
        }

        private void txtRuta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lvRuta.Focus(); }
        }

        private void lvRuta_Enter(object sender, EventArgs e)
        {
            if (!lvRuta.Items.Count.Equals(0)) { lvRuta.Items[0].Selected = true; }
        }

        private void lvRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lvRuta.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lvRuta.SelectedItems[0];
                idRuta = Int32.Parse(ItemActual.Text);
                txtRuta.Text = ItemActual.SubItems[1].Text;

                lvRuta.Visible = false;
                lvRuta.SendToBack();
                ListarDesbloqueo();
                dtpFechaCompromiso.Focus();
            }
        }

        private void lvRuta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lvRuta.SelectedItems[0];
            idRuta = Int32.Parse(ItemActual.Text);
            txtRuta.Text = ItemActual.SubItems[1].Text;

            lvRuta.Visible = false;
            lvRuta.SendToBack();
            ListarDesbloqueo();
            dtpFechaCompromiso.Focus();
        }

        private void dtgListaDesbloqueo_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dtgvListaDesbloqueo.GetRowCellValue(dtgvListaDesbloqueo.FocusedRowHandle, "PLACA").ToString();
                if (Vacio != "") { tsQuitarDesbloqueo.Enabled = true; }
            }
            catch { tsQuitarDesbloqueo.Enabled = false; }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtUnidad.Text.Length == 0 || txtRuta.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtUnidad.Text.Length == 0) { txtUnidad.Focus(); }
                else { txtRuta.Focus(); }
                return;
            }
            else
            {
                DataTable dtDesbloquear = new DataTable();
                string respta;

                dtDesbloquear = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_IngresarEliminarDesbloqueo(1, 0, idUnidad2, idRuta, dtpFechaCompromiso.Value, Utilitario.Instancia.SesionUsuario.usuario);
                respta = Convert.ToString(dtDesbloquear.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarDesbloqueo();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnBuscarDes_Click(object sender, EventArgs e) { ListarDesbloqueo(); }

        private void tsQuitarDesbloqueo_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea quitar el desbloqueo de esta unidad?", "DESBLOQUEO DE UNIDADES", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idDesbloqueo = Convert.ToInt32(dtgvListaDesbloqueo.GetRowCellValue(dtgvListaDesbloqueo.FocusedRowHandle, "idDesbloqueo"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_IngresarEliminarDesbloqueo(2, idDesbloqueo, 0, 0, DateTime.Now, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarDesbloqueo(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }

        private void rbPeriodos_Click(object sender, EventArgs e)
        {
            if (rbPeriodos.Checked == true)
            {
                FiltroTiempo = 1;
                dtpPeriodo.Enabled = true;
                dtpFechaInicio3.Enabled = false;
                dtpFechaFin3.Enabled = false;
            }
        }

        private void rbRangoFechas_Click(object sender, EventArgs e)
        {
            if (rbRangoFechas.Checked == true)
            {
                FiltroTiempo = 2;
                dtpPeriodo.Enabled = false;
                dtpFechaInicio3.Enabled = true;
                dtpFechaFin3.Enabled = true;
            }
        }

        private void dtpFechaInicioA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarActividades(); }
        }

        private void dtpFechaFinA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarActividades(); }
        }

        private void txtUnidadA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarActividades(); }
        }

        private void txtActividadA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarActividades(); }
        }

        private void cbxTipoA_DropDownClosed(object sender, EventArgs e) { ListarActividades(); }

        private void dgvActividadesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "%")
            {
                if (Convert.ToDecimal(e.CellValue) < 60)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) >= 60 && Convert.ToDecimal(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) >= 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
            }
        }

        private void btnBuscarA_Click(object sender, EventArgs e) { ListarActividades(); }

        private void btnExcelA_Click(object sender, EventArgs e)
        {
            if (dtgActividades.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE ACTIVIDADES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgActividades.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void tsInspeccionarUnidad_Click(object sender, EventArgs e)
        {
            
        }

        private void dgvInspeccionesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CONFORME")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "OBSERVADO")
                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
            }

            /*
            if (e.Column.FieldName == "ESTADO_PEDIDO")
            {
                if (Convert.ToString(e.CellValue) == "PEDIDO")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "SIN PEDIR")
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }
            }
            */
        }

        private void dtpFechaInicioI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarInspecciones(); }
        }

        private void dtpFechaFinI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarInspecciones(); }
        }

        private void txtUnidadI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarInspecciones(); }
        }

        private void cbxTipoI_DropDownClosed(object sender, EventArgs e) { ListarInspecciones(); }

        private void cbSucursal_DropDownClosed(object sender, EventArgs e) { ListarInspecciones(); }

        private void dtgInspecciones_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvInspeccionesVista.RowCount > 0) { dtgInspecciones.ContextMenuStrip = contextMenuStrip6; }
                else { dtgInspecciones.ContextMenuStrip = null; }
            }
            catch (Exception ex) { }
        }

        private void tsNuevaInspeccion_Click(object sender, EventArgs e)
        {
            try
            {
                string Placa, Tipo, SubTipo, Operacion, Marca, Modelo;
                DateTime FechaProyectada;
                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                Placa = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "PLACA").ToString();
                Tipo = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "TIPO_UNIDAD").ToString();
                SubTipo = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "SUBTIPO_UNIDAD").ToString();
                Operacion = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "OPERACION").ToString();
                Marca = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "MARCA").ToString();
                Modelo = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "MODELO").ToString();
                FechaProyectada = Convert.ToDateTime(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "PROX_MTTO"));

                if (SubTipo == "TRACTO") { TipoInspeccion = 1; }
                else { TipoInspeccion = 2; }

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_CrearInspeccion(Placa, Tipo, SubTipo, Operacion, Marca, Modelo, FechaProyectada, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    frmInspeccionUnidades frmInspeccionUnidades = new frmInspeccionUnidades();
                    frmInspeccionUnidades.frmListaMantenimiento = this;
                    frmInspeccionUnidades.lblPlaca.Text = Placa;
                    frmInspeccionUnidades.lblOperacion.Text = Operacion;
                    frmInspeccionUnidades.lblMarca.Text = Marca;
                    frmInspeccionUnidades.lblModelo.Text = Modelo;
                    frmInspeccionUnidades.dtpNuevaFechaI.Value = DateTime.Now;
                    frmInspeccionUnidades.dtpNuevaFechaF.Value = DateTime.Now;
                    frmInspeccionUnidades.cbxTurno.Text = "DÍA";

                    if (Usuario == "PNAVARRO" || Usuario == "LQUEZADA" || Usuario == "MADELEINEC")
                    { frmInspeccionUnidades.cbxSucursal.Text = "LIMA"; }
                    else { frmInspeccionUnidades.cbxSucursal.Text = "TRUJILLO"; }

                    frmInspeccionUnidades.Opcion = 1;
                    frmInspeccionUnidades.EliminarPedido = 1;
                    frmInspeccionUnidades.TipoInspeccion = TipoInspeccion;
                    frmInspeccionUnidades.ListarInspeccion();
                    frmInspeccionUnidades.Show();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { }
        }

        private void dtgInspecciones_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string Fecha = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "INICIO_INSPECCION").ToString();

                if (Fecha != "")
                {
                    frmInspeccionUnidades frmInspeccionUnidades = new frmInspeccionUnidades();
                    frmInspeccionUnidades.frmListaMantenimiento = this;
                    frmInspeccionUnidades.idInspeccionC = Convert.ToInt32(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "idInspeccionC"));
                    frmInspeccionUnidades.lblPlaca.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "PLACA").ToString();
                    frmInspeccionUnidades.lblOperacion.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "OPERACION").ToString();
                    frmInspeccionUnidades.lblMarca.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "MARCA").ToString();
                    frmInspeccionUnidades.lblModelo.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "MODELO").ToString();
                    frmInspeccionUnidades.EliminarPedido = 1;

                    frmInspeccionUnidades.Persona1 = Convert.ToInt32(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "Mecanico1"));
                    frmInspeccionUnidades.txtMecanico.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "MECÁNICO").ToString();
                    frmInspeccionUnidades.Persona2 = Convert.ToInt32(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "Electrico2"));
                    frmInspeccionUnidades.txtElectrico.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "ELÉCTRICO").ToString();
                    frmInspeccionUnidades.Persona3 = Convert.ToInt32(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "Neumatico3"));
                    frmInspeccionUnidades.txtNeumatico.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "NEUMÁTICO").ToString();
                    frmInspeccionUnidades.Persona4 = Convert.ToInt32(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "Soldador4"));
                    frmInspeccionUnidades.txtSoldador.Text = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "SOLDADOR").ToString();
                    frmInspeccionUnidades.dtpNuevaFechaI.Value = Convert.ToDateTime(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "INICIO_INSPECCION"));
                    frmInspeccionUnidades.dtpNuevaFechaF.Value = Convert.ToDateTime(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "FIN_INSPECCION"));
                    frmInspeccionUnidades.cbxTurno.Text = Convert.ToString(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "TURNO"));
                    frmInspeccionUnidades.cbxSucursal.Text = Convert.ToString(dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "SUCURSAL"));

                    string Subtipo = dgvInspeccionesVista.GetRowCellValue(dgvInspeccionesVista.FocusedRowHandle, "SUBTIPO_UNIDAD").ToString();
                    if (Subtipo == "TRACTO") { frmInspeccionUnidades.TipoInspeccion = 1; }
                    else { frmInspeccionUnidades.TipoInspeccion = 2; }

                    frmInspeccionUnidades.Opcion = 2;
                    frmInspeccionUnidades.BuscarInspeccion();
                    frmInspeccionUnidades.Show();
                }
            }
            catch { }
        }

        private void btnBuscarI_Click(object sender, EventArgs e) { ListarInspecciones(); }

        private void btnExcelI_Click(object sender, EventArgs e)
        {
            if (dtgInspecciones.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE INSPECCIONES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgInspecciones.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnCompDesgaste_Click(object sender, EventArgs e)
        {
            frmComponenteDesgaste frmComponenteDesgaste = new frmComponenteDesgaste();
            frmComponenteDesgaste.dtpFechaInicio.Value = dtpFechaInicioI.Value;
            frmComponenteDesgaste.dtpFechaFin.Value = dtpFechaFinI.Value;
            frmComponenteDesgaste.ShowDialog();
        }

        private void tsHistInspeccion_Click(object sender, EventArgs e)
        {
            frmHistorialInspecciones frmHistorialInspecciones = new frmHistorialInspecciones();
            frmHistorialInspecciones.dtpFechaInicio.Value = dtpFechaInicioI.Value;
            frmHistorialInspecciones.dtpFechaFin.Value = dtpFechaFinI.Value;
            frmHistorialInspecciones.cbxSucursal.Text = "TRUJILLO";
            frmHistorialInspecciones.Opcion = 1;
            frmHistorialInspecciones.label1.Text = "HISTORIAL DE INSPECCIONES REALIZADAS";
            frmHistorialInspecciones.ShowDialog();
        }

        private void tsRegistroPedidos_Click(object sender, EventArgs e)
        {
            frmHistorialInspecciones frmHistorialInspecciones = new frmHistorialInspecciones();
            frmHistorialInspecciones.dtpFechaInicio.Value = dtpFechaInicioI.Value;
            frmHistorialInspecciones.dtpFechaFin.Value = dtpFechaFinI.Value;
            frmHistorialInspecciones.cbxSucursal.Text = "TRUJILLO";
            frmHistorialInspecciones.Opcion = 2;
            frmHistorialInspecciones.label1.Text = "REGISTRO DE OBSERVACIONES";
            frmHistorialInspecciones.ShowDialog();
        }

        private void rbProximaI_Click(object sender, EventArgs e)
        {
            Fecha = "FP";
            ListarInspecciones();
        }

        private void rbRealizaciónI_Click(object sender, EventArgs e)
        {
            Fecha = "FR";
            ListarInspecciones();
        }

        private void dgvMttoCorrectivoVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (e.CellValue.ToString() == "TERMINADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void dtgMttoCorrectivo_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Estado = Convert.ToString(dgvMttoCorrectivoVista.GetRowCellValue(dgvMttoCorrectivoVista.FocusedRowHandle, "ESTADO"));

                if (Estado == "PENDIENTE")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        tsActualizarEstado.Enabled = true;
                        tsEliminarMtto.Enabled = true;
                    }
                }
                else
                {
                    tsActualizarEstado.Enabled = false;
                    tsEliminarMtto.Enabled = false;
                }
            }
            catch
            {
                tsActualizarEstado.Enabled = false;
                tsEliminarMtto.Enabled = false;
            }
        }

        private void txtPlaca4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMttoCorrectivo(); }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMttoCorrectivo(); }
        }

        private void dtpFechaInicio4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMttoCorrectivo(); }
        }

        private void dtpFechaFin4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMttoCorrectivo(); }
        }

        private void cbxOrigen_DropDownClosed(object sender, EventArgs e) { ListarMttoCorrectivo(); }

        private void btnBuscar4_Click(object sender, EventArgs e) { ListarMttoCorrectivo(); }

        private void btnExcel4_Click(object sender, EventArgs e)
        {
            if (dtgMttoCorrectivo.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE MANTENIMIENTOS CORRECTIVOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgMttoCorrectivo.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void rbPendiente_Click(object sender, EventArgs e)
        {
            Estado = "PENDIENTE";
            ListarMttoCorrectivo();
        }

        private void rbTerminado_Click(object sender, EventArgs e)
        {
            Estado = "TERMINADO";
            ListarMttoCorrectivo();
        }

        private void tsProgramarFecha_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionMC = 1;
                idMttoC = Convert.ToInt32(dgvMttoCorrectivoVista.GetRowCellValue(dgvMttoCorrectivoVista.FocusedRowHandle, "NRO"));

                txtMCPlaca.Text = dgvMttoCorrectivoVista.GetRowCellValue(dgvMttoCorrectivoVista.FocusedRowHandle, "PLACA").ToString();
                txtMCOrigen.Text = dgvMttoCorrectivoVista.GetRowCellValue(dgvMttoCorrectivoVista.FocusedRowHandle, "ORIGEN").ToString();
                txtMCObservacion.Text = dgvMttoCorrectivoVista.GetRowCellValue(dgvMttoCorrectivoVista.FocusedRowHandle, "OBSERVACION").ToString();
                dtpMCFecha.CustomFormat = "dd/MM/yyyy HH:mm:ss";
                dtpMCFecha.Value = DateTime.Now;

                panel8.Visible = true;
                panel8.BringToFront();
                pAgregarProgramacion.Visible = true;
                pAgregarProgramacion.BringToFront();
            }
            catch { }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pAgregarProgramacion.Visible = false;
            pAgregarProgramacion.SendToBack();

            idMttoC = -1;
            txtMCPlaca.Clear();
            txtMCOrigen.Clear();
            txtMCObservacion.Clear();
            txtMCOT.Clear();
        }

        private void pAgregarProgramacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick3 = e.X; yClick3 = e.Y; }
            else
            {
                pAgregarProgramacion.Left = pAgregarProgramacion.Left + (e.X - xClick3);
                pAgregarProgramacion.Top = pAgregarProgramacion.Top + (e.Y - yClick3);
            }
        }

        private void btnMCGuardar_Click(object sender, EventArgs e)
        {
            DataTable dtpProgramar = new DataTable();
            string respta;

            if (OpcionMC == 1)      // AGREGAR PROGRAMACION
            {
                dtpProgramar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ModificarMtto(1, idMttoC, dtpMCFecha.Value, "", Utilitario.Instancia.SesionUsuario.usuario);
                respta = Convert.ToString(dtpProgramar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar2_Click(sender, e);
                    ListarMttoCorrectivo();
                    ListarCalendario();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (OpcionMC == 2)      // ACTUALIZAR OT
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ModificarMtto(2, idMttoC, dtpMCFecha.Value, txtMCOT.Text, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    idMttoC = -1;
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar2_Click(sender, e);
                    rbTerminado.Checked = true;
                    rbTerminado_Click(sender, e);
                    ListarMttoCorrectivo();
                    ListarCalendario();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void txtMCOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstOT, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarOTProgramadas(txtMCPlaca.Text, txtMCOT.Text), true, false, false);
            lstOT.Columns[0].Width = 100;
            lstOT.Columns[1].Width = 100;
            lstOT.Columns[2].Width = 200;
            lstOT.BringToFront();
            lstOT.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstOT.Visible = false;
                lstOT.SendToBack();
            }
        }

        private void txtMCOT_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstOT.Focus(); }
        }

        private void lstOT_Enter(object sender, EventArgs e)
        {
            if (!lstOT.Items.Count.Equals(0)) { lstOT.Items[0].Selected = true; }
        }

        private void lstOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstOT.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstOT.SelectedItems[0];
                txtMCOT.Text = ItemActual.SubItems[0].Text;

                lstOT.Visible = false;
                lstOT.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstOT.Visible = false;
                lstOT.SendToBack();
            }
        }

        private void lstOT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstOT.SelectedItems[0];
            txtMCOT.Text = ItemActual.SubItems[0].Text;

            lstOT.Visible = false;
            lstOT.SendToBack();
        }

        private void tsActualizarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionMC = 2;
                idMttoC = Convert.ToInt32(dgvMttoCorrectivoVista.GetRowCellValue(dgvMttoCorrectivoVista.FocusedRowHandle, "NRO"));

                txtMCPlaca.Text = dgvMttoCorrectivoVista.GetRowCellValue(dgvMttoCorrectivoVista.FocusedRowHandle, "PLACA").ToString();
                txtMCOrigen.Text = dgvMttoCorrectivoVista.GetRowCellValue(dgvMttoCorrectivoVista.FocusedRowHandle, "ORIGEN").ToString();
                txtMCObservacion.Text = dgvMttoCorrectivoVista.GetRowCellValue(dgvMttoCorrectivoVista.FocusedRowHandle, "OBSERVACION").ToString();

                panel8.Visible = false;
                panel8.SendToBack();
                pAgregarProgramacion.Visible = true;
                pAgregarProgramacion.BringToFront();
            }
            catch { }
        }

        private void tsEliminarMtto_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este mantenimiento?", "ELIMINAR MANTENIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    idMttoC = Convert.ToInt32(dgvMttoCorrectivoVista.GetRowCellValue(dgvMttoCorrectivoVista.FocusedRowHandle, "NRO"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ModificarMtto(3, idMttoC, dtpMCFecha.Value, "", Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        idMttoC = -1;
                        ListarMttoCorrectivo();
                        ListarCalendario();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }

        private void btnProgramarCumplimiento_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpcionC == 0)
                {
                    btnProgramarCumplimiento.Text = "Generar Cumplimiento";
                    dtpFCInicio.Value = DateTime.Now;
                    dtpFCFin.Value = DateTime.Now.AddDays(6);
                    groupBox7.Visible = true;
                    dgvMantenimientoVista.OptionsSelection.MultiSelect = true;
                    dgvMantenimientoVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    OpcionC = 1;
                }
                else
                {
                    if (dtpFCFin.Value != dtpFCInicio.Value.AddDays(6))
                    {
                        MessageBox.Show("No puede ingresar un rango mayor o menor a 6 días.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnProgramarCumplimiento.Text = "Programar Cumplimiento";
                        groupBox7.Visible = false;
                        dgvMantenimientoVista.OptionsSelection.MultiSelect = false;
                        dgvMantenimientoVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        OpcionC = 0;
                        return;
                    }
                    else
                    {
                        int[] filas = dgvMantenimientoVista.GetSelectedRows();
                        if (filas.Length != 0)
                        {
                            DataTable dtRespuesta = new DataTable();
                            string respta;
                            int Correcto = 0;

                            if (MessageBox.Show("¿Desea generar un cumplimiento para estos mantenimientos?", "GENERAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                for (int i = 0; i < filas.Length; i++)
                                {
                                    string Placa = Convert.ToString(dgvMantenimientoVista.GetRowCellValue(filas[i], "PLACA"));
                                    string Operacion = Convert.ToString(dgvMantenimientoVista.GetRowCellValue(filas[i], "OPERACION"));
                                    string TipoUnidad = Convert.ToString(dgvMantenimientoVista.GetRowCellValue(filas[i], "SUBTIPO_UNIDAD"));
                                    string Marca = Convert.ToString(dgvMantenimientoVista.GetRowCellValue(filas[i], "MARCA"));
                                    string Ubigeo = Convert.ToString(dgvMantenimientoVista.GetRowCellValue(filas[i], "UBIGEO"));
                                    string MttoPreventivo = Convert.ToString(dgvMantenimientoVista.GetRowCellValue(filas[i], "PROXIMO_MTTO"));
                                    DateTime FechaProgramada = Convert.ToDateTime(dgvMantenimientoVista.GetRowCellValue(filas[i], "FECHA_PROYECTADA"));

                                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumplimiento(Placa, Operacion, TipoUnidad, Marca, Ubigeo,
                                                  MttoPreventivo, FechaProgramada, dtpFCInicio.Value, dtpFCFin.Value);
                                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0") { Correcto = Correcto + 1; }
                                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }

                                if (Correcto == filas.Length) { MessageBox.Show("0 = El cumplimiento semanal ha sido generado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                btnProgramarCumplimiento.Text = "Programar Cumplimiento";
                                groupBox7.Visible = false;
                                dgvMantenimientoVista.OptionsSelection.MultiSelect = false;
                                dgvMantenimientoVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                                OpcionC = 0;

                                ListarCumplimientos();
                            }
                        }
                        else
                        {
                            btnProgramarCumplimiento.Text = "Programar Cumplimiento";
                            groupBox7.Visible = false;
                            dgvMantenimientoVista.OptionsSelection.MultiSelect = false;
                            dgvMantenimientoVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                            OpcionC = 0;
                        }
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el reporte de cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtNroSemana_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCumplimientos(); }
        }

        private void dtpAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCumplimientos(); }
        }

        private void cbxOperaciones3_DropDownClosed(object sender, EventArgs e) { ListarCumplimientos(); }

        private void btnBuscar5_Click(object sender, EventArgs e) { ListarCumplimientos(); }

        private void btnExcel5_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCumplimiento.DataSource == null) { MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    DataTable dtExcel = new DataTable();
                    gcExcelCump.DataSource = null;
                    gvExcelCump.Columns.Clear();
                    dtExcel = Utilitario.Instancia.GetContentAsDataTable(dgvCumplimiento, true);
                    gcExcelCump.DataSource = dtExcel;
                    gvExcelCump.BestFitColumns();

                    DataTable dtExcelTotal = new DataTable();
                    gcExcelCump2.DataSource = null;
                    gvExcelCump2.Columns.Clear();
                    dtExcelTotal = Utilitario.Instancia.GetContentAsDataTable(dgvPorcentaje, true);
                    gcExcelCump2.DataSource = dtExcelTotal;
                    gvExcelCump2.BestFitColumns();

                    DataTable dtExcelOperaciones = new DataTable();
                    gcExcelCump3.DataSource = null;
                    gvExcelCump3.Columns.Clear();
                    dtExcelOperaciones = Utilitario.Instancia.GetContentAsDataTable(dgvOperaciones, true);
                    gcExcelCump3.DataSource = dtExcelOperaciones;
                    gvExcelCump3.BestFitColumns();

                    gcExcelCump.ForceInitialize();
                    gcExcelCump2.ForceInitialize();
                    gcExcelCump3.ForceInitialize();

                    compositeLink1.CreatePageForEachLink();

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                    XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                    options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                    string nombre = System.IO.Path.Combine(desktop, "Registro de Cumplimiento Semanal - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    compositeLink1.ExportToXlsx(nombre, options);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvCumplimiento_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvCumplimiento.RowCount > 0 && dgvCumplimiento.CurrentRow != null)
                {
                    if (e.ColumnIndex < 11 || e.ColumnIndex > 17)
                    {
                        dgvCumplimiento.ContextMenuStrip = contextMenuStrip5;
                        tsRegistrarCump.Enabled = false;
                    }
                    else
                    {
                        dgvCumplimiento.ContextMenuStrip = contextMenuStrip5;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsRegistrarCump.Enabled = true; }
                    }

                    if (dgvCumplimiento.CurrentRow.Cells["ESTADO"].Value.ToString() == "REPROGRAMADO") { tsObservacion.Enabled = true; }
                    else { tsObservacion.Enabled = false; }
                }
                else { dgvCumplimiento.ContextMenuStrip = null; }
            }
            catch (Exception) { }
        }

        private void dgvCumplimiento_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvCumplimiento.RowCount > 0)
                {
                    int lastRowIndex = dgvCumplimiento.RowCount - 1;

                    if (dgvCumplimiento.CurrentRow.Index == lastRowIndex)
                    {
                        dgvCumplimiento.ContextMenuStrip = null;
                        return;
                    }
                    else { dgvCumplimiento.ContextMenuStrip = contextMenuStrip5; }
                }
                else { dgvCumplimiento.ContextMenuStrip = null; }
            }
            catch (Exception ex) { }
        }

        private void dgvCumplimiento_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvCumplimiento.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("ESTADO"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("REPROGRAMADO"))
                            {
                                e.CellStyle.BackColor = Color.Pink;
                                e.CellStyle.ForeColor = Color.Red;
                            }
                            else if (Convert.ToString(e.Value).Contains("EJECUTADO"))
                            {
                                e.CellStyle.BackColor = Color.PaleGreen;
                                e.CellStyle.ForeColor = Color.Green;
                            }
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("ALINEAMIENTO"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("PENDIENTE")) { e.CellStyle.BackColor = Color.Aqua; }
                            else if (Convert.ToString(e.Value).Contains("EJECUTADO")) { e.CellStyle.BackColor = Color.Lime; }
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("LUN"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("M1")) { e.CellStyle.BackColor = Color.LightGray; }
                            else if (Convert.ToString(e.Value).Contains("M2")) { e.CellStyle.BackColor = Color.LightGreen; }
                            else if (Convert.ToString(e.Value).Contains("M3")) { e.CellStyle.BackColor = Color.LightSalmon; }
                            else if (Convert.ToString(e.Value).Contains("M4")) { e.CellStyle.BackColor = Color.Yellow; }
                            else if (Convert.ToString(e.Value).Contains("M5")) { e.CellStyle.BackColor = Color.LightSkyBlue; }

                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("MAR"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("M1"))
                            {
                                e.CellStyle.BackColor = Color.LightGray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("M2"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("M3"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("M4"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("M5"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("MIÉ"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("M1")) { e.CellStyle.BackColor = Color.LightGray; }
                            else if (Convert.ToString(e.Value).Contains("M2")) { e.CellStyle.BackColor = Color.LightGreen; }
                            else if (Convert.ToString(e.Value).Contains("M3")) { e.CellStyle.BackColor = Color.LightSalmon; }
                            else if (Convert.ToString(e.Value).Contains("M4")) { e.CellStyle.BackColor = Color.Yellow; }
                            else if (Convert.ToString(e.Value).Contains("M5")) { e.CellStyle.BackColor = Color.LightSkyBlue; }

                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("JUE"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("M1")) { e.CellStyle.BackColor = Color.LightGray; }
                            else if (Convert.ToString(e.Value).Contains("M2")) { e.CellStyle.BackColor = Color.LightGreen; }
                            else if (Convert.ToString(e.Value).Contains("M3")) { e.CellStyle.BackColor = Color.LightSalmon; }
                            else if (Convert.ToString(e.Value).Contains("M4")) { e.CellStyle.BackColor = Color.Yellow; }
                            else if (Convert.ToString(e.Value).Contains("M5")) { e.CellStyle.BackColor = Color.LightSkyBlue; }

                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("VIE"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("M1")) { e.CellStyle.BackColor = Color.LightGray; }
                            else if (Convert.ToString(e.Value).Contains("M2")) { e.CellStyle.BackColor = Color.LightGreen; }
                            else if (Convert.ToString(e.Value).Contains("M3")) { e.CellStyle.BackColor = Color.LightSalmon; }
                            else if (Convert.ToString(e.Value).Contains("M4")) { e.CellStyle.BackColor = Color.Yellow; }
                            else if (Convert.ToString(e.Value).Contains("M5")) { e.CellStyle.BackColor = Color.LightSkyBlue; }

                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("SÁB"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("M1")) { e.CellStyle.BackColor = Color.LightGray; }
                            else if (Convert.ToString(e.Value).Contains("M2")) { e.CellStyle.BackColor = Color.LightGreen; }
                            else if (Convert.ToString(e.Value).Contains("M3")) { e.CellStyle.BackColor = Color.LightSalmon; }
                            else if (Convert.ToString(e.Value).Contains("M4")) { e.CellStyle.BackColor = Color.Yellow; }
                            else if (Convert.ToString(e.Value).Contains("M5")) { e.CellStyle.BackColor = Color.LightSkyBlue; }

                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("DOM"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("M1")) { e.CellStyle.BackColor = Color.LightGray; }
                            else if (Convert.ToString(e.Value).Contains("M2")) { e.CellStyle.BackColor = Color.LightGreen; }
                            else if (Convert.ToString(e.Value).Contains("M3")) { e.CellStyle.BackColor = Color.LightSalmon; }
                            else if (Convert.ToString(e.Value).Contains("M4")) { e.CellStyle.BackColor = Color.Yellow; }
                            else if (Convert.ToString(e.Value).Contains("M5")) { e.CellStyle.BackColor = Color.LightSkyBlue; }

                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvCumplimiento_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0) return;

                DataGridViewColumn columna = dgvCumplimiento.Columns[e.ColumnIndex];

                if (columna.Name == "ESTADO")
                {
                    Rectangle rect = dgvCumplimiento.GetCellDisplayRectangle(e.ColumnIndex, -1, true);

                    menuFiltroEstadoCumplimiento.Show(dgvCumplimiento, new Point(rect.Left, rect.Bottom));
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvPorcentaje_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try { dgvPorcentaje.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable; }
            catch (Exception) { throw; }
        }

        private void dgvOperaciones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try { dgvOperaciones.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable; }
            catch (Exception) { throw; }
        }

        private void tsEliminarCump_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este registro de cumplimiento?", "ELIMINAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int Nro = Convert.ToInt32(dgvCumplimiento.CurrentRow.Cells["Nro"].Value.ToString());

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarCumplimiento(1, Nro);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarCumplimientos(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }

        private void tsRegistrarCump_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionFI = 1;
                lblPlaca.Text = dgvCumplimiento.CurrentRow.Cells["PLACA"].Value.ToString();
                label61.Visible = true;
                lblTipoMtto.Visible = true;
                lblTipoMtto.Text = dgvCumplimiento.CurrentRow.Cells["MTTO_PREVENTIVO"].Value.ToString();
                dtpFechaProgramada.Value = Convert.ToDateTime(dgvCumplimiento.CurrentRow.Cells["FECHA_PROG"].Value.ToString());
                cbxEstado.Text = dgvCumplimiento.CurrentRow.Cells["ESTADO"].Value.ToString();
                txtObservacion2.Text = dgvCumplimiento.CurrentRow.Cells["OBSERVACION"].Value.ToString();
                pRegistrarCump.Visible = true;
                pRegistrarCump.BringToFront();
            }
            catch { }
        }

        private void pRegistrarCump_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick4 = e.X; yClick4 = e.Y; }
            else
            {
                pRegistrarCump.Left = pRegistrarCump.Left + (e.X - xClick4);
                pRegistrarCump.Top = pRegistrarCump.Top + (e.Y - yClick4);
            }
        }

        private void btnCerrar6_Click(object sender, EventArgs e)
        {
            pRegistrarCump.Visible = false;
            pRegistrarCump.SendToBack();
            dtpFechaProgramada.Value = DateTime.Now;
            cbxEstado.Text = "PROGRAMADO";
            txtObservacion2.Clear();
            OpcionFI = 0;
        }

        private void btnGuardarCump_Click(object sender, EventArgs e)
        {
            if (OpcionFI == 1)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int Nro = Convert.ToInt32(dgvCumplimiento.CurrentRow.Cells["Nro"].Value.ToString());

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumplimiento(1, Nro, lblTipoMtto.Text, dtpFechaProgramada.Value, cbxEstado.Text, txtObservacion2.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar6_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCumplimientos();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (OpcionFI == 2)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int Nro = Convert.ToInt32(dgvCumplimiento2.CurrentRow.Cells["Nro"].Value.ToString());

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpInspeccion(1, Nro, dtpFechaProgramada.Value, cbxEstado.Text, txtObservacion2.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar6_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCumplimientosI();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (OpcionFI == 3)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int Nro = Convert.ToInt32(dgvCumplimiento3.CurrentRow.Cells["Nro"].Value.ToString());

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpActividades(1, Nro, dtpFechaProgramada.Value, cbxEstado.Text, txtObservacion2.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar6_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCumplimientosA();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsFechaIngreso_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionFI = 1;
                lblPlaca2.Text = dgvCumplimiento.CurrentRow.Cells["PLACA"].Value.ToString();
                label75.Visible = true;
                lblTipoMtto2.Visible = true;
                lblTipoMtto2.Text = dgvCumplimiento.CurrentRow.Cells["MTTO_PREVENTIVO"].Value.ToString();
                dtpFechaIngreso.Value = DateTime.Now;
                pFechaIngreso.Location = new Point(723, 208);
                pFechaIngreso.Visible = true;
                pFechaIngreso.BringToFront();
            }
            catch { }
        }

        private void pFechaIngreso_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick6 = e.X; yClick6 = e.Y; }
            else
            {
                pFechaIngreso.Left = pFechaIngreso.Left + (e.X - xClick6);
                pFechaIngreso.Top = pFechaIngreso.Top + (e.Y - yClick6);
            }
        }

        private void btnGuardarIngreso_Click(object sender, EventArgs e)
        {
            if (OpcionFI == 1)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int Nro = Convert.ToInt32(dgvCumplimiento.CurrentRow.Cells["Nro"].Value.ToString());

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumplimiento(2, Nro, lblTipoMtto2.Text, dtpFechaIngreso.Value, " ", " ");
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar7_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCumplimientos();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (OpcionFI == 2)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int Nro = Convert.ToInt32(dgvCumplimiento2.CurrentRow.Cells["Nro"].Value.ToString());

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpInspeccion(2, Nro, dtpFechaIngreso.Value, " ", " ", Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar7_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCumplimientosI();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (OpcionFI == 3)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int Nro = Convert.ToInt32(dgvCumplimiento3.CurrentRow.Cells["Nro"].Value.ToString());

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumpActividades(2, Nro, dtpFechaIngreso.Value, " ", " ", Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar7_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCumplimientosA();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnCerrar7_Click(object sender, EventArgs e)
        {
            pFechaIngreso.Visible = false;
            pFechaIngreso.SendToBack();
            dtpFechaIngreso.Value = DateTime.Now;
            OpcionFI = 0;
        }

        private void dtpFechaIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardarIngreso_Click(sender, e); }
        }

        private void tsObservacion_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionFO = 1;
                lblPlaca3.Text = dgvCumplimiento.CurrentRow.Cells["PLACA"].Value.ToString();
                label83.Visible = true;
                lblTipoMtto3.Visible = true;
                lblTipoMtto3.Text = dgvCumplimiento.CurrentRow.Cells["MTTO_PREVENTIVO"].Value.ToString();
                txtObservacionOP.Clear();
                pObservacionOP.Location = new Point(723, 164);
                pObservacionOP.Visible = true;
                pObservacionOP.BringToFront();
            }
            catch { }
        }

        private void pObservacionOP_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick7 = e.X; yClick7 = e.Y; }
            else
            {
                pObservacionOP.Left = pObservacionOP.Left + (e.X - xClick7);
                pObservacionOP.Top = pObservacionOP.Top + (e.Y - yClick7);
            }
        }

        private void btnCerrar8_Click(object sender, EventArgs e)
        {
            pObservacionOP.Visible = false;
            pObservacionOP.SendToBack();
            txtObservacionOP.Clear();
            OpcionFO = 0;
        }

        private void btnGuardarOP_Click(object sender, EventArgs e)
        {
            if (OpcionFO == 1)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int Nro = Convert.ToInt32(dgvCumplimiento.CurrentRow.Cells["Nro"].Value.ToString());

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumplimiento(3, Nro, " ", DateTime.Now, " ", txtObservacionOP.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar8_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCumplimientos();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (OpcionFO == 2)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int Nro = Convert.ToInt32(dgvCumplimiento2.CurrentRow.Cells["Nro"].Value.ToString());

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumplimiento(4, Nro, " ", DateTime.Now, " ", txtObservacionOP.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar8_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCumplimientosI();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (OpcionFO == 3)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int Nro = Convert.ToInt32(dgvCumplimiento3.CurrentRow.Cells["Nro"].Value.ToString());

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ProgramarCumplimiento(5, Nro, " ", DateTime.Now, " ", txtObservacionOP.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar8_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCumplimientosA();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnProgramarCumplimiento2_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpcionC2 == 0)
                {
                    btnProgramarCumplimiento2.Text = "Generar Cumplimiento";
                    dtpFCInicio2.Value = DateTime.Now;
                    dtpFCFin2.Value = DateTime.Now.AddDays(6);
                    groupBox11.Visible = true;
                    dgvMaquinariaVista.OptionsSelection.MultiSelect = true;
                    dgvMaquinariaVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    OpcionC2 = 1;
                }
                else
                {
                    if (dtpFCFin2.Value != dtpFCInicio2.Value.AddDays(6))
                    {
                        MessageBox.Show("No puede ingresar un rango mayor o menor a 6 días.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnProgramarCumplimiento2.Text = "Programar Cumplimiento";
                        groupBox11.Visible = false;
                        dgvMaquinariaVista.OptionsSelection.MultiSelect = false;
                        dgvMaquinariaVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        OpcionC2 = 0;
                        return;
                    }
                    else
                    {
                        int[] filas = dgvMaquinariaVista.GetSelectedRows();
                        if (filas.Length != 0)
                        {
                            DataTable dtRespuesta = new DataTable();
                            string respta;
                            int Correcto = 0;

                            if (MessageBox.Show("¿Desea generar un cumplimiento para estos mantenimientos?", "GENERAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                for (int i = 0; i < filas.Length; i++)
                                {
                                    string Placa = Convert.ToString(dgvMaquinariaVista.GetRowCellValue(filas[i], "PLACA"));
                                    string Operacion = "";
                                    string TipoUnidad = Convert.ToString(dgvMaquinariaVista.GetRowCellValue(filas[i], "MAQUINA"));
                                    string Marca = Convert.ToString(dgvMaquinariaVista.GetRowCellValue(filas[i], "MARCA"));
                                    string MttoPreventivo = Convert.ToString(dgvMaquinariaVista.GetRowCellValue(filas[i], "PROXIMO_MTTO"));
                                    string Ubigeo = Convert.ToString(dgvMaquinariaVista.GetRowCellValue(filas[i], "UBICACION"));
                                    DateTime FechaProgramada = Convert.ToDateTime(dgvMaquinariaVista.GetRowCellValue(filas[i], "FECHA_PROYECTADA"));

                                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumplimiento(Placa, Operacion, TipoUnidad, Marca, Ubigeo,
                                                  MttoPreventivo, FechaProgramada, dtpFCInicio2.Value, dtpFCFin2.Value);
                                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0") { Correcto = Correcto + 1; }
                                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }

                                if (Correcto == filas.Length) { MessageBox.Show("0 = El cumplimiento semanal ha sido generado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                btnProgramarCumplimiento2.Text = "Programar Cumplimiento";
                                groupBox11.Visible = false;
                                dgvMaquinariaVista.OptionsSelection.MultiSelect = false;
                                dgvMaquinariaVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                                OpcionC2 = 0;

                                ListarCumplimientos();
                            }
                        }
                        else
                        {
                            btnProgramarCumplimiento2.Text = "Programar Cumplimiento";
                            groupBox11.Visible = false;
                            dgvMaquinariaVista.OptionsSelection.MultiSelect = false;
                            dgvMaquinariaVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                            OpcionC2 = 0;
                        }
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el reporte de cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtpPeriodo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCalendario(); }
        }

        private void txtPlaca6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCalendario(); }
        }

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e) { ListarCalendario(); }

        private void btnBuscar6_Click(object sender, EventArgs e) { ListarCalendario(); }

        private void btnExcel6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCalendario.DataSource == null) { MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    DataTable dtExcel = new DataTable();
                    gcExcelCalendario.DataSource = null;
                    gvExcelCalendario.Columns.Clear();
                    dtExcel = Utilitario.Instancia.GetContentAsDataTable(dgvCalendario, true);
                    gcExcelCalendario.DataSource = dtExcel;

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Calendario de Mttos. Correctivos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    gcExcelCalendario.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvCalendario_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvCalendario.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 3) { dgvCalendario.CurrentRow.Cells[e.ColumnIndex].Selected = false; }
                        else
                        {
                            string Placa = dgvCalendario.CurrentRow.Cells["PLACA"].Value.ToString();
                            int dia = Convert.ToInt32(e.ColumnIndex - 2);
                            string FechaOP = CalcularDia(dia);
                            FechaOP = FechaOP + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");

                            DataTable dtListaMttos = new DataTable();
                            dtListaMttos.Clear();

                            dtListaMttos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoCorrectivo_ListarMttoProgramado(Placa, Convert.ToDateTime(FechaOP));
                            if (dtListaMttos.Rows.Count > 0)
                            {
                                lblUnidad.Text = Placa;
                                lblFecha2.Text = Convert.ToString(FechaOP);
                                dtgListaMttos.DataSource = dtListaMttos;

                                dgvListaMttos.Columns["idMttoC"].Visible = false;
                                dgvListaMttos.Columns["Placa"].Visible = false;

                                dgvListaMttos.Columns["OBSERVACION"].Summary.Clear();
                                dgvListaMttos.Columns["OBSERVACION"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "OBSERVACION", "Total: {0}");
                                dgvListaMttos.BestFitColumns();

                                pListarMttos.Visible = true;
                                pListarMttos.BringToFront();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dgvCalendario_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvCalendario.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCalendario.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightSkyBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            pListarMttos.Visible = false;
            pListarMttos.SendToBack();
            lblUnidad.Text = "";
            lblFecha2.Text = "";
        }

        private void pListarMttos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick5 = e.X; yClick5 = e.Y; }
            else
            {
                pListarMttos.Left = pListarMttos.Left + (e.X - xClick5);
                pListarMttos.Top = pListarMttos.Top + (e.Y - yClick5);
            }
        }

        private void tsProgramarCumplimiento3_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpcionC3 == 0)
                {
                    btnProgramarCumplimiento3.Text = "Generar Cumplimiento";
                    dtpFCInicio3.Value = DateTime.Now;
                    dtpFCFin3.Value = DateTime.Now.AddDays(6);
                    groupBox13.Visible = true;
                    dgvInspeccionesVista.OptionsSelection.MultiSelect = true;
                    dgvInspeccionesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    OpcionC3 = 1;
                }
                else
                {
                    if (dtpFCFin3.Value != dtpFCInicio3.Value.AddDays(6))
                    {
                        MessageBox.Show("No puede ingresar un rango mayor o menor a 6 días.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnProgramarCumplimiento3.Text = "Programar Cumplimiento";
                        groupBox13.Visible = false;
                        dgvInspeccionesVista.OptionsSelection.MultiSelect = false;
                        dgvInspeccionesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        OpcionC3 = 0;
                        return;
                    }
                    else
                    {
                        int[] filas = dgvInspeccionesVista.GetSelectedRows();
                        if (filas.Length != 0)
                        {
                            DataTable dtRespuesta = new DataTable();
                            string respta;
                            int Correcto = 0;

                            if (MessageBox.Show("¿Desea generar un cumplimiento para estas inspecciones?", "GENERAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                for (int i = 0; i < filas.Length; i++)
                                {
                                    string Placa = Convert.ToString(dgvInspeccionesVista.GetRowCellValue(filas[i], "PLACA"));
                                    string Operacion = Convert.ToString(dgvInspeccionesVista.GetRowCellValue(filas[i], "OPERACION"));
                                    string TipoUnidad = Convert.ToString(dgvInspeccionesVista.GetRowCellValue(filas[i], "SUBTIPO_UNIDAD"));
                                    string Marca = Convert.ToString(dgvInspeccionesVista.GetRowCellValue(filas[i], "MARCA"));
                                    DateTime ProxInspeccion = Convert.ToDateTime(dgvInspeccionesVista.GetRowCellValue(filas[i], "PROX_INSPECCION"));

                                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpInspeccion(Placa, Operacion, TipoUnidad, Marca, ProxInspeccion, dtpFCInicio3.Value, dtpFCFin3.Value);
                                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0") { Correcto = Correcto + 1; }
                                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }

                                if (Correcto == filas.Length) { MessageBox.Show("0 = El cumplimiento semanal ha sido generado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                btnProgramarCumplimiento3.Text = "Programar Cumplimiento";
                                groupBox13.Visible = false;
                                dgvInspeccionesVista.OptionsSelection.MultiSelect = false;
                                dgvInspeccionesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                                OpcionC3 = 0;

                                ListarCumplimientosI();
                            }
                        }
                        else
                        {
                            btnProgramarCumplimiento3.Text = "Programar Cumplimiento";
                            groupBox13.Visible = false;
                            dgvInspeccionesVista.OptionsSelection.MultiSelect = false;
                            dgvInspeccionesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                            OpcionC3 = 0;
                        }
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el reporte de cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnIndicadorInspeccion_Click(object sender, EventArgs e)
        {
            frmIndicadorInspecciones frmIndicadorInspecciones = new frmIndicadorInspecciones();
            frmIndicadorInspecciones.ShowDialog();
        }

        private void txtNroSemana2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCumplimientosI(); }
        }

        private void dtpAnio2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCumplimientosI(); }
        }

        private void cbxOperaciones4_DropDownClosed(object sender, EventArgs e) { ListarCumplimientosI(); }

        private void btnBuscar7_Click(object sender, EventArgs e) { ListarCumplimientosI(); }

        private void btnExcel7_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCumplimiento2.DataSource == null) { MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    DataTable dtExcel = new DataTable();
                    gcExcelCumpI.DataSource = null;
                    gvExcelCumpI.Columns.Clear();
                    dtExcel = Utilitario.Instancia.GetContentAsDataTable(dgvCumplimiento2, true);
                    gcExcelCumpI.DataSource = dtExcel;

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Cumplimiento Semanal de Inspecciones - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    gcExcelCumpI.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvCumplimiento2_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvCumplimiento2.RowCount > 0) { dgvCumplimiento2.ContextMenuStrip = contextMenuStrip7; }
                else { dgvCumplimiento2.ContextMenuStrip = null; }
            }
            catch (Exception ex) { }
        }

        private void dgvCumplimiento2_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvCumplimiento2.RowCount > 0)
                {
                    if (e.ColumnIndex < 9 || e.ColumnIndex > 15)
                    {
                        dgvCumplimiento2.ContextMenuStrip = contextMenuStrip7;
                        tsRegistrarCump2.Enabled = false;
                    }
                    else
                    {
                        dgvCumplimiento2.ContextMenuStrip = contextMenuStrip7;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsRegistrarCump2.Enabled = true; }
                    }

                    if (dgvCumplimiento2.CurrentRow.Cells["ESTADO"].Value.ToString() == "REPROGRAMADO") { tsAgregarObservacion2.Enabled = true; }
                    else { tsAgregarObservacion2.Enabled = false; }
                }
                else { dgvCumplimiento2.ContextMenuStrip = null; }
            }
            catch (Exception ex) { }
        }

        private void dgvCumplimiento2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvCumplimiento2.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvCumplimiento2.Columns[e.ColumnIndex].Name.Contains("ESTADO"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("REPROGRAMADO"))
                            {
                                e.CellStyle.BackColor = Color.Pink;
                                e.CellStyle.ForeColor = Color.Red;
                            }
                            else if (Convert.ToString(e.Value).Contains("EJECUTADO"))
                            {
                                e.CellStyle.BackColor = Color.PaleGreen;
                                e.CellStyle.ForeColor = Color.Green;
                            }
                        }
                    }
                }

                if (this.dgvCumplimiento2.Columns[e.ColumnIndex].Name.Contains("LUN"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento2.Columns[e.ColumnIndex].Name.Contains("MAR"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("INS"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCumplimiento2.Columns[e.ColumnIndex].Name.Contains("MIÉ"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento2.Columns[e.ColumnIndex].Name.Contains("JUE"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento2.Columns[e.ColumnIndex].Name.Contains("VIE"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento2.Columns[e.ColumnIndex].Name.Contains("SÁB"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento2.Columns[e.ColumnIndex].Name.Contains("DOM"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvPorcentaje2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try { dgvPorcentaje2.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable; }
            catch (Exception) { throw; }
        }

        private void dgvOperaciones2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try { dgvOperaciones2.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable; }
            catch (Exception) { throw; }
        }

        private void tsFechaIngreso2_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionFI = 2;
                lblPlaca2.Text = dgvCumplimiento2.CurrentRow.Cells["PLACA"].Value.ToString();
                label75.Visible = false;
                lblTipoMtto2.Visible = false;
                lblTipoMtto2.Text = "";
                dtpFechaIngreso.Value = DateTime.Now;
                pFechaIngreso.Location = new Point(723, 208);
                pFechaIngreso.Visible = true;
                pFechaIngreso.BringToFront();
            }
            catch { }
        }

        private void tsAgregarObservacion2_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionFO = 2;
                lblPlaca3.Text = dgvCumplimiento2.CurrentRow.Cells["PLACA"].Value.ToString();
                label83.Visible = false;
                lblTipoMtto3.Visible = false;
                lblTipoMtto3.Text = "";
                txtObservacionOP.Clear();
                pObservacionOP.Location = new Point(723, 164);
                pObservacionOP.Visible = true;
                pObservacionOP.BringToFront();
            }
            catch { }
        }

        private void tsEliminarCump2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este registro de cumplimiento?", "ELIMINAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int Nro = Convert.ToInt32(dgvCumplimiento2.CurrentRow.Cells["Nro"].Value.ToString());

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarCumplimiento(2, Nro);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarCumplimientosI(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }

        private void tsRegistrarCump2_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionFI = 2;
                lblPlaca.Text = dgvCumplimiento2.CurrentRow.Cells["PLACA"].Value.ToString();
                label61.Visible = false;
                lblTipoMtto.Visible = false;
                lblTipoMtto.Text = "";
                dtpFechaProgramada.Value = Convert.ToDateTime(dgvCumplimiento2.CurrentRow.Cells["PROX_INSPECCION"].Value.ToString());
                cbxEstado.Text = dgvCumplimiento2.CurrentRow.Cells["ESTADO"].Value.ToString();
                txtObservacion2.Text = dgvCumplimiento2.CurrentRow.Cells["OBSERVACION"].Value.ToString();
                pRegistrarCump.Visible = true;
                pRegistrarCump.BringToFront();
            }
            catch { }
        }

        private void dtpFechaInicioR2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRecursosManoObra(); }
        }

        private void dtpFechaFinR2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRecursosManoObra(); }
        }

        private void txtUnidadR2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarRecursosManoObra(); }
        }

        private void cbxTipoR2_DropDownClosed(object sender, EventArgs e) { ListarRecursosManoObra(); }

        private void cbxEspecialidad_DropDownClosed(object sender, EventArgs e) { ListarRecursosManoObra(); }

        private void tsQuitarMO_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea quitar este insumo de mano de obra?", "QUITAR MANO DE OBRA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idManoObra = Convert.ToInt32(dgvManoObra.GetRowCellValue(dgvManoObra.FocusedRowHandle, "idManoObra"));
                    int idProcesoMtto = Convert.ToInt32(dgvManoObra.GetRowCellValue(dgvManoObra.FocusedRowHandle, "idProcesoMtto"));

                    string TipoUnidad = dgvManoObra.GetRowCellValue(dgvManoObra.FocusedRowHandle, "TIPO_UNIDAD").ToString();

                    if (TipoUnidad == "TRACTO" || TipoUnidad == "SEMIRREMOLQUE")
                    {
                        int idVehiculo = Convert.ToInt32(dgvManoObra.GetRowCellValue(dgvManoObra.FocusedRowHandle, "idVehiculo"));

                        DataTable dtRespuesta = new DataTable();
                        string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                        dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMO(2, idManoObra, idProcesoMtto, idVehiculo, "", "", Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0") { ListarRecursosManoObra(); }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else
                    {
                        string CodigoMaquina = dgvManoObra.GetRowCellValue(dgvManoObra.FocusedRowHandle, "PLACA").ToString();

                        DataTable dtRespuesta = new DataTable();
                        string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                        dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ManoObra_IngresarEliminarMOMaquinas(2, idManoObra, idProcesoMtto, CodigoMaquina, "", "", Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0") { ListarRecursosManoObra(); }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
            catch { }
        }

        private void dgvManoObra_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "%")
            {
                if (Convert.ToDecimal(e.CellValue) < 60)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) >= 60 && Convert.ToDecimal(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) >= 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 128, 128); }
            }
        }

        private void btnBuscarR2_Click(object sender, EventArgs e) { ListarRecursosManoObra(); }

        private void btnExcelR2_Click(object sender, EventArgs e)
        {
            if (dtgManoObra.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "INSUMOS DE MANO DE OBRA - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgManoObra.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void txtEquipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMantenimientoEquipos(); }
        }

        private void cbxTipoEquipo_DropDownClosed(object sender, EventArgs e) { ListarMantenimientoEquipos(); }

        private void dtpFechaInicioEquipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMantenimientoEquipos(); }
        }

        private void dtpFechaFinEquipo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMantenimientoEquipos(); }
        }

        private void btnBuscarEquipo_Click(object sender, EventArgs e) { ListarMantenimientoEquipos(); }

        private void btnExcelEquipo_Click(object sender, EventArgs e)
        {
            if (dtgEquipos.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE MANTENIMIENTOS DE EQUIPOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgEquipos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnNuevoEquipo_Click(object sender, EventArgs e)
        {
            frmProgramarEquipo frmProgramarEquipo = new frmProgramarEquipo();
            frmProgramarEquipo.Opcion = 1;
            frmProgramarEquipo.idRegistroE = 0;
            frmProgramarEquipo.CargarComboUnidad();
            frmProgramarEquipo.cbxTipoUnidad.Text = "UNIDAD ESTACIONARIA";
            frmProgramarEquipo.label1.Text = "MANTENIMIENTO DE EQUIPOS";
            frmProgramarEquipo.formulario = this;
            frmProgramarEquipo.dtpUltFecha.Value = DateTime.Now;
            frmProgramarEquipo.btnActualizar.Visible = false;
            frmProgramarEquipo.txtPlaca.Focus();
            frmProgramarEquipo.ShowDialog();
        }

        private void btnHistorialMtto3_Click(object sender, EventArgs e)
        {
            frmHistorialMtto frmHistorialMtto = new frmHistorialMtto();
            frmHistorialMtto.Opcion = 3;
            frmHistorialMtto.cbxTipoEquipo.Visible = true;
            frmHistorialMtto.cbxTipoMaquina.Visible = false;
            frmHistorialMtto.cbxTipoUnidad.Visible = false;
            frmHistorialMtto.cbxTipoEquipo.BringToFront();
            frmHistorialMtto.CargarComboEquipo();
            frmHistorialMtto.cbxTipoEquipo.Text = cbxTipoEquipo.Text;
            frmHistorialMtto.dtpFechaInicio.Value = dtpFechaInicioEquipo.Value;
            frmHistorialMtto.dtpFechaFin.Value = dtpFechaFinEquipo.Value;
            frmHistorialMtto.ShowDialog();
        }

        private void dtgEquipos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmProgramarEquipo frmProgramarEquipo = new frmProgramarEquipo();
                frmProgramarEquipo.Opcion = 2;
                frmProgramarEquipo.idRegistroE = Convert.ToInt32(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "idRegistro"));
                frmProgramarEquipo.CargarComboUnidad();
                frmProgramarEquipo.label1.Text = "MANTENIMIENTO DE EQUIPOS";
                frmProgramarEquipo.formulario = this;
                frmProgramarEquipo.cbxTipoUnidad.Text = Convert.ToString(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "GRUPO"));
                frmProgramarEquipo.txtPlaca.Text = Convert.ToString(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "PLACA"));
                frmProgramarEquipo.txtPeriodo.Text = Convert.ToString(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "PERIODO"));
                frmProgramarEquipo.txtEquipo.Text = Convert.ToString(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "MAQUINARIA"));
                frmProgramarEquipo.txtDescripcion.Text = Convert.ToString(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "DESCRIPCION"));
                frmProgramarEquipo.txtUbicacion.Text = Convert.ToString(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "UBICACION"));
                frmProgramarEquipo.txtMarca.Text = Convert.ToString(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "MARCA"));
                frmProgramarEquipo.txtModelo.Text = Convert.ToString(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "MODELO"));
                frmProgramarEquipo.dtpUltFecha.Value = Convert.ToDateTime(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "FECHA_ULTIMA"));
                frmProgramarEquipo.txtPlaca.Focus();
                frmProgramarEquipo.btnActualizar.Visible = true;
                frmProgramarEquipo.ShowDialog();
            }
            catch { }
        }

        private void dtgEquipos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "PLACA").ToString();

                if (Vacio != "")
                {
                    tsHistorialMtto.Enabled = true;
                    tsControlMtto.Enabled = true;
                }
            }
            catch
            {
                tsHistorialMtto.Enabled = false;
                tsControlMtto.Enabled = false;
            }
        }

        private void tsHistorialMtto_Click(object sender, EventArgs e)
        {
            try
            {
                frmHistorialMtto frmHistorialMtto = new frmHistorialMtto();
                frmHistorialMtto.Opcion = 3;
                frmHistorialMtto.cbxTipoEquipo.Visible = true;
                frmHistorialMtto.cbxTipoMaquina.Visible = false;
                frmHistorialMtto.cbxTipoUnidad.Visible = false;
                frmHistorialMtto.cbxTipoEquipo.BringToFront();
                frmHistorialMtto.CargarComboEquipo();
                frmHistorialMtto.cbxTipoEquipo.Text = dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "MAQUINARIA").ToString();
                frmHistorialMtto.txtPlaca.Text = dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "PLACA").ToString();
                frmHistorialMtto.dtpFechaInicio.Value = dtpFechaInicioEquipo.Value;
                frmHistorialMtto.dtpFechaFin.Value = dtpFechaFinEquipo.Value;
                frmHistorialMtto.ShowDialog();
            }
            catch { }
        }

        private void tsControlMtto_Click(object sender, EventArgs e)
        {
            try
            {
                frmControlMttoEquipos frmControlMttoEquipos = new frmControlMttoEquipos();
                frmControlMttoEquipos.idRegistroE = Convert.ToInt32(dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "idRegistro"));
                frmControlMttoEquipos.lblPlaca.Text = dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "PLACA").ToString();
                frmControlMttoEquipos.lblMarca.Text = dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "MARCA").ToString();
                frmControlMttoEquipos.lblModelo.Text = dgvEquiposVista.GetRowCellValue(dgvEquiposVista.FocusedRowHandle, "MODELO").ToString();
                frmControlMttoEquipos.ShowDialog();
            }
            catch { }
        }

        private void dgvEquiposVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE (%)")
            {
                if (Convert.ToDecimal(e.CellValue) < 60)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) >= 60 && Convert.ToDecimal(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) >= 100 || Convert.ToDecimal(e.CellValue) < 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CONFORME")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "POR VENCER")
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "VENCIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void cbxMPTecnica_DropDownClosed(object sender, EventArgs e)
        {
            Tecnica = Convert.ToInt32(cbxMPTecnica.SelectedValue);
            CargarComboSistema();
        }

        private void btnNuevoVehiculo2_Click(object sender, EventArgs e)
        {
            frmNuevoMttoPredictivo frmNuevoMttoPredictivo = new frmNuevoMttoPredictivo();
            frmNuevoMttoPredictivo.lblTitulo.Text = "REGISTRAR MTTO. PREDICTIVO";
            frmNuevoMttoPredictivo.Opcion = 1;
            frmNuevoMttoPredictivo.CargarComboUnidad();
            frmNuevoMttoPredictivo.cbxTipoUnidad_DropDownClosed(sender, e);
            frmNuevoMttoPredictivo.Tecnica = Convert.ToInt32(cbxMPTecnica.SelectedValue);
            frmNuevoMttoPredictivo.lblTecnica.Text = cbxMPTecnica.Text;
            frmNuevoMttoPredictivo.Sistema = Convert.ToInt32(cbxMPSistema.SelectedValue);
            frmNuevoMttoPredictivo.lblSistema.Text = cbxMPSistema.Text;
            frmNuevoMttoPredictivo.dtpFecha.Value = DateTime.Now;
            frmNuevoMttoPredictivo.cbxEstado.Text = "NORMAL";
            frmNuevoMttoPredictivo.formulario = this;
            frmNuevoMttoPredictivo.ShowDialog();
        }

        private void txtMPUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMttoPredictivo(); }
        }

        private void cbxMPTipo_DropDownClosed(object sender, EventArgs e) { ListarMttoPredictivo(); }

        private void cbxMPSistema_DropDownClosed(object sender, EventArgs e) { ListarMttoPredictivo(); }

        private void btnMPBuscar_Click(object sender, EventArgs e) { ListarMttoPredictivo(); }

        private void btnMPExcel_Click(object sender, EventArgs e)
        {
            if (dtgMttoPredictivo.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE MANTENIMIENTOS PREDICTIVOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgMttoPredictivo.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvMttoPredictivoVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "NORMAL") { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "PRECAUCIÓN") { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "ALERTA")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void dtgMttoPredictivo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var hi = dgvMttoPredictivoVista.CalcHitInfo(dtgMttoPredictivo.PointToClient(MousePosition));

                if (hi.InRowCell)
                {
                    if (dgvMttoPredictivoVista.Columns[hi.Column.FieldName].ColumnEdit is RepositoryItemHyperLinkEdit)
                    {
                        string url = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "INFORME").ToString();
                        
                        try { System.Diagnostics.Process.Start(url); }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        frmNuevoMttoPredictivo frmNuevoMttoPredictivo = new frmNuevoMttoPredictivo();
                        frmNuevoMttoPredictivo.lblTitulo.Text = "ACTUALIZAR MTTO. PREDICTIVO";
                        frmNuevoMttoPredictivo.Opcion = 2;

                        frmNuevoMttoPredictivo.Tecnica = Convert.ToInt32(dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "idTecnica"));
                        frmNuevoMttoPredictivo.lblTecnica.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "TECNICA").ToString();
                        frmNuevoMttoPredictivo.Sistema = Convert.ToInt32(dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "idSistema"));
                        frmNuevoMttoPredictivo.lblSistema.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "SISTEMA").ToString();

                        frmNuevoMttoPredictivo.CargarComboUnidad();
                        frmNuevoMttoPredictivo.cbxTipoUnidad.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "TIPO_UNIDAD").ToString();
                        frmNuevoMttoPredictivo.txtPlaca.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "PLACA").ToString();
                        frmNuevoMttoPredictivo.cbxTipoUnidad.Enabled = false;
                        frmNuevoMttoPredictivo.txtPlaca.Enabled = false;
                        frmNuevoMttoPredictivo.txtOperacion.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "OPERACION").ToString();
                        frmNuevoMttoPredictivo.txtMarca.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "MARCA").ToString();
                        frmNuevoMttoPredictivo.txtModelo.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "MODELO").ToString();
                        frmNuevoMttoPredictivo.txtLubricante.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "TIPO_LUBRICANTE").ToString();
                        frmNuevoMttoPredictivo.dtpFecha.Value = Convert.ToDateTime(dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "FECHA"));
                        frmNuevoMttoPredictivo.cbxEstado.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "ESTADO").ToString();
                        frmNuevoMttoPredictivo.txtRecomendacion.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "RECOMENDACION").ToString();
                        frmNuevoMttoPredictivo.txtRutaLocal.Text = dgvMttoPredictivoVista.GetRowCellValue(dgvMttoPredictivoVista.FocusedRowHandle, "INFORME").ToString();

                        frmNuevoMttoPredictivo.btnCancelar.Enabled = false;
                        frmNuevoMttoPredictivo.formulario = this;
                        frmNuevoMttoPredictivo.ShowDialog();
                    }
                }
            }
            catch { }
        }

        private void btnHistorialMtto4_Click(object sender, EventArgs e)
        {
            frmHistorialMttoPredictivo frmHistorialMttoPredictivo = new frmHistorialMttoPredictivo();
            frmHistorialMttoPredictivo.dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            frmHistorialMttoPredictivo.dtpFechaFin.Value = DateTime.Now;
            frmHistorialMttoPredictivo.ShowDialog();
        }

        private void btnProgramarCumplimiento4_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpcionC4 == 0)
                {
                    btnProgramarCumplimiento4.Text = "Generar Cumplimiento";
                    dtpFCInicio4.Value = DateTime.Now;
                    dtpFCFin4.Value = DateTime.Now.AddDays(6);
                    pRecursos.Visible = false;
                    pRecursos.SendToBack();
                    groupBox21.Visible = true;
                    groupBox21.BringToFront();
                    dgvActividadesVista.OptionsSelection.MultiSelect = true;
                    dgvActividadesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    OpcionC4 = 1;
                }
                else
                {
                    if (dtpFCFin4.Value != dtpFCInicio4.Value.AddDays(6))
                    {
                        MessageBox.Show("No puede ingresar un rango mayor o menor a 6 días.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnProgramarCumplimiento4.Text = "Programar Cumplimiento";
                        pRecursos.Visible = false;
                        pRecursos.SendToBack();
                        groupBox21.Visible = false;
                        groupBox21.SendToBack();
                        dgvActividadesVista.OptionsSelection.MultiSelect = false;
                        dgvActividadesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        OpcionC4 = 0;
                        return;
                    }
                    else
                    {
                        int[] filas = dgvActividadesVista.GetSelectedRows();
                        if (filas.Length != 0)
                        {
                            DataTable dtRespuesta = new DataTable();
                            string respta;
                            int Correcto = 0;

                            if (MessageBox.Show("¿Desea generar un cumplimiento para estas actividades?", "GENERAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                for (int i = 0; i < filas.Length; i++)
                                {
                                    int idProcesoMtto = Convert.ToInt32(dgvActividadesVista.GetRowCellValue(filas[i], "idProcesoMtto"));
                                    string Placa = Convert.ToString(dgvActividadesVista.GetRowCellValue(filas[i], "PLACA"));
                                    string Operacion = Convert.ToString(dgvActividadesVista.GetRowCellValue(filas[i], "OPERACION"));
                                    string TipoUnidad = Convert.ToString(dgvActividadesVista.GetRowCellValue(filas[i], "SUBTIPO_UNIDAD"));
                                    string Actividad = Convert.ToString(dgvActividadesVista.GetRowCellValue(filas[i], "ACTIVIDAD"));
                                    DateTime ProxFecha = Convert.ToDateTime(dgvActividadesVista.GetRowCellValue(filas[i], "FECHA_PROYECTADA"));

                                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_GenerarCumpActividad(Placa, Operacion, TipoUnidad, Actividad, ProxFecha, dtpFCInicio4.Value, dtpFCFin4.Value);
                                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0") { Correcto = Correcto + 1; }
                                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }

                                if (Correcto == filas.Length) { MessageBox.Show("0 = El cumplimiento semanal ha sido generado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                btnProgramarCumplimiento4.Text = "Programar Cumplimiento";
                                pRecursos.Visible = false;
                                pRecursos.SendToBack();
                                groupBox21.Visible = false;
                                groupBox21.SendToBack();
                                dgvActividadesVista.OptionsSelection.MultiSelect = false;
                                dgvActividadesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                                OpcionC4 = 0;
                                ListarCumplimientosA();
                            }
                        }
                        else
                        {
                            btnProgramarCumplimiento4.Text = "Programar Cumplimiento";
                            pRecursos.Visible = false;
                            pRecursos.SendToBack();
                            groupBox21.Visible = false;
                            groupBox21.SendToBack();
                            dgvActividadesVista.OptionsSelection.MultiSelect = false;
                            dgvActividadesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                            OpcionC4 = 0;
                        }
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el reporte de cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        
        private void tsAsignarRecursos_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpcionRecursos == 0)
                {
                    tsAsignarRecursos.Text = "Generar Asignación";
                    pRecursos.Visible = true;
                    pRecursos.BringToFront();
                    txtCodigoItem.Clear();
                    txtCantidad.Clear();
                    txtDescripcionItem.Clear();
                    groupBox21.Visible = false;
                    groupBox21.SendToBack();
                    dgvActividadesVista.OptionsSelection.MultiSelect = true;
                    dgvActividadesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    OpcionRecursos = 1;
                }
                else
                {
                    if (txtDescripcionItem.Text.Length == 0 || txtCantidad.Text.Length == 0 || txtCantidad.Text == "0")
                    {
                        MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        tsAsignarRecursos.Text = "Asignar Recursos";
                        pRecursos.Visible = false;
                        pRecursos.SendToBack();
                        groupBox21.Visible = false;
                        groupBox21.SendToBack();
                        dgvActividadesVista.OptionsSelection.MultiSelect = false;
                        dgvActividadesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        OpcionRecursos = 0;
                        return;
                    }
                    else
                    {
                        int[] filas = dgvActividadesVista.GetSelectedRows();

                        if (filas.Length != 0)
                        {
                            DataTable dtRespuesta = new DataTable();
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                            string respta;
                            int Correcto = 0;

                            if (MessageBox.Show("¿Desea asignar un ítem a estas actividades?", "ASIGNAR ÍTEMS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                for (int i = 0; i < filas.Length; i++)
                                {
                                    int idProcesoMtto = Convert.ToInt32(dgvActividadesVista.GetRowCellValue(filas[i], "idProcesoMtto"));
                                    int idVehiculo = Convert.ToInt32(dgvActividadesVista.GetRowCellValue(filas[i], "idVehiculo"));
                                    string Placa = Convert.ToString(dgvActividadesVista.GetRowCellValue(filas[i], "PLACA"));
                                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_RegistrarEliminarRecursos(1, 0, idProcesoMtto, idVehiculo,
                                                                     Placa, txtCodigoItem.Text, txtDescripcionItem.Text, Convert.ToDecimal(txtCantidad.Text), Usuario);
                                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0") { Correcto = Correcto + 1; }
                                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }

                                if (Correcto == filas.Length) { MessageBox.Show("0 = El ítem ha sido asignado a las actividades.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                tsAsignarRecursos.Text = "Asignar Recursos";
                                pRecursos.Visible = false;
                                pRecursos.SendToBack();
                                groupBox21.Visible = false;
                                groupBox21.SendToBack();
                                dgvActividadesVista.OptionsSelection.MultiSelect = false;
                                dgvActividadesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                                OpcionRecursos = 0;
                                ListarActividades();
                                ListarRecursosAsignados();
                            }
                        }
                        else
                        {
                            tsAsignarRecursos.Text = "Asignar Recursos";
                            pRecursos.Visible = false;
                            pRecursos.SendToBack();
                            groupBox21.Visible = false;
                            groupBox21.SendToBack();
                            dgvActividadesVista.OptionsSelection.MultiSelect = false;
                            dgvActividadesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                            OpcionRecursos = 0;
                        }
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al asignar ítems a las actividades.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtCodigoItem_Enter(object sender, EventArgs e) { txtCodigoItem.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtCodigoItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstItemsAlmacen, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMaestroItems(txtCodigoItem.Text), true, false, false);
            lstItemsAlmacen.Columns[0].Width = 80;
            lstItemsAlmacen.Columns[1].Width = 400;
            lstItemsAlmacen.BringToFront();
            lstItemsAlmacen.Visible = true;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                txtDescripcionItem.Clear();
            }
        }

        private void txtCodigoItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstItemsAlmacen.Focus(); }
        }

        private void txtCodigoItem_Leave(object sender, EventArgs e) { txtCodigoItem.BackColor = Color.White; }

        private void lstItemsAlmacen_Enter(object sender, EventArgs e)
        {
            if (!lstItemsAlmacen.Items.Count.Equals(0)) { lstItemsAlmacen.Items[0].Selected = true; }
        }

        private void lstItemsAlmacen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstItemsAlmacen.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstItemsAlmacen.SelectedItems[0];

                txtCodigoItem.Text = ItemActual.SubItems[0].Text;
                txtDescripcionItem.Text = ItemActual.SubItems[1].Text;

                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                txtCantidad.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                txtCodigoItem.Focus();
            }
        }

        private void lstItemsAlmacen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstItemsAlmacen.SelectedItems[0];

            txtCodigoItem.Text = ItemActual.SubItems[0].Text;
            txtDescripcionItem.Text = ItemActual.SubItems[1].Text;

            lstItemsAlmacen.Visible = false;
            lstItemsAlmacen.SendToBack();
            txtCantidad.Focus();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { tsAsignarRecursos_Click(sender, e); }
        }

        private void dtpAnio3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCumplimientosA(); }
        }

        private void txtNroSemana3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCumplimientosA(); }
        }

        private void cbxOperaciones5_DropDownClosed(object sender, EventArgs e) { ListarCumplimientosA(); }

        private void btnBuscar8_Click(object sender, EventArgs e) { ListarCumplimientosA(); }

        private void btnExcel8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCumplimiento3.DataSource == null) { MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    DataTable dtExcel = new DataTable();
                    gcExcelCumpA.DataSource = null;
                    gvExcelCumpA.Columns.Clear();
                    dtExcel = Utilitario.Instancia.GetContentAsDataTable(dgvCumplimiento3, true);
                    gcExcelCumpA.DataSource = dtExcel;

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Cumplimiento Semanal de Actividades - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    gcExcelCumpA.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvCumplimiento3_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvCumplimiento3.RowCount > 0) { dgvCumplimiento3.ContextMenuStrip = contextMenuStrip10; }
                else { dgvCumplimiento3.ContextMenuStrip = null; }
            }
            catch (Exception ex) { }
        }

        private void dgvCumplimiento3_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvCumplimiento3.RowCount > 0)
                {
                    if (e.ColumnIndex < 9 || e.ColumnIndex > 15)
                    {
                        dgvCumplimiento3.ContextMenuStrip = contextMenuStrip10;
                        tsRegistrarCump3.Enabled = false;
                    }
                    else
                    {
                        dgvCumplimiento3.ContextMenuStrip = contextMenuStrip10;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsRegistrarCump3.Enabled = true; }
                    }

                    if (dgvCumplimiento3.CurrentRow.Cells["ESTADO"].Value.ToString() == "REPROGRAMADO") { tsAgregarObservacion3.Enabled = true; }
                    else { tsAgregarObservacion3.Enabled = false; }
                }
                else { dgvCumplimiento3.ContextMenuStrip = null; }
            }
            catch (Exception ex) { }
        }

        private void dgvCumplimiento3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvCumplimiento3.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvCumplimiento3.Columns[e.ColumnIndex].Name.Contains("ESTADO"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("REPROGRAMADO"))
                            {
                                e.CellStyle.BackColor = Color.Pink;
                                e.CellStyle.ForeColor = Color.Red;
                            }
                            else if (Convert.ToString(e.Value).Contains("EJECUTADO"))
                            {
                                e.CellStyle.BackColor = Color.PaleGreen;
                                e.CellStyle.ForeColor = Color.Green;
                            }
                        }
                    }
                }

                if (this.dgvCumplimiento3.Columns[e.ColumnIndex].Name.Contains("LUN"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento3.Columns[e.ColumnIndex].Name.Contains("MAR"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento3.Columns[e.ColumnIndex].Name.Contains("MIÉ"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento3.Columns[e.ColumnIndex].Name.Contains("JUE"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento3.Columns[e.ColumnIndex].Name.Contains("VIE"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento3.Columns[e.ColumnIndex].Name.Contains("SÁB"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento3.Columns[e.ColumnIndex].Name.Contains("DOM"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvPorcentaje3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try { dgvPorcentaje3.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable; }
            catch (Exception) { throw; }
        }

        private void dgvOperaciones3_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try { dgvOperaciones3.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable; }
            catch (Exception) { throw; }
        }

        private void tsFechaIngreso3_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionFI = 3;
                lblPlaca2.Text = dgvCumplimiento3.CurrentRow.Cells["PLACA"].Value.ToString();
                label75.Visible = false;
                lblTipoMtto2.Visible = false;
                lblTipoMtto2.Text = "";
                dtpFechaIngreso.Value = DateTime.Now;
                pFechaIngreso.Location = new Point(723, 208);
                pFechaIngreso.Visible = true;
                pFechaIngreso.BringToFront();
            }
            catch { }
        }

        private void tsAgregarObservacion3_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionFO = 3;
                lblPlaca3.Text = dgvCumplimiento3.CurrentRow.Cells["PLACA"].Value.ToString();
                label83.Visible = false;
                lblTipoMtto3.Visible = false;
                lblTipoMtto3.Text = "";
                txtObservacionOP.Clear();
                pObservacionOP.Location = new Point(723, 164);
                pObservacionOP.Visible = true;
                pObservacionOP.BringToFront();
            }
            catch { }
        }

        private void tsRegistrarCump3_Click(object sender, EventArgs e)
        {
            try
            {
                OpcionFI = 3;
                lblPlaca.Text = dgvCumplimiento3.CurrentRow.Cells["PLACA"].Value.ToString();
                label61.Visible = false;
                lblTipoMtto.Visible = false;
                lblTipoMtto.Text = "";
                dtpFechaProgramada.Value = Convert.ToDateTime(dgvCumplimiento3.CurrentRow.Cells["FECHA_PROYECTADA"].Value.ToString());
                cbxEstado.Text = dgvCumplimiento3.CurrentRow.Cells["ESTADO"].Value.ToString();
                txtObservacion2.Text = dgvCumplimiento3.CurrentRow.Cells["OBSERVACION"].Value.ToString();
                pRegistrarCump.Visible = true;
                pRegistrarCump.BringToFront();
            }
            catch { }
        }

        private void tsEliminarCump3_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este registro de cumplimiento?", "ELIMINAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int Nro = Convert.ToInt32(dgvCumplimiento3.CurrentRow.Cells["Nro"].Value.ToString());

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarCumplimiento(3, Nro);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarCumplimientosA(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }

        private void dtpAnio4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDesviacion(); }
        }

        private void txtNroSemana4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDesviacion(); }
        }

        private void cbxOperaciones6_DropDownClosed(object sender, EventArgs e) { ListarDesviacion(); }

        private void txtTipoMtto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDesviacion(); }
        }

        private void btnBuscar9_Click(object sender, EventArgs e) { ListarDesviacion(); }

        private void btnExcel9_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDesviacion.DataSource == null) { MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    DataTable dtExcel = new DataTable();
                    gcExcelDesv.DataSource = null;
                    gvExcelDesv.Columns.Clear();
                    dtExcel = Utilitario.Instancia.GetContentAsDataTable(dgvDesviacion, true);
                    gcExcelDesv.DataSource = dtExcel;

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Registro de Tiempos de Desviacion - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    gcExcelDesv.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvDesviacion_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvDesviacion.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex >= 10)
                        {
                            lblPlacaDesv.Text = dgvDesviacion.CurrentRow.Cells["PLACA"].Value.ToString();
                            lblTipoMttoDesv.Text = dgvDesviacion.CurrentRow.Cells["MTTO_PREVENTIVO"].Value.ToString();
                            lblNro.Text = dgvDesviacion.CurrentRow.Cells["Nro"].Value.ToString();

                            DataTable dtListaSolicitud = new DataTable();
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                            dtgVincularSolicitud.DataSource = null;
                            dgvVincularSolicitudVista.Columns.Clear();
                            dtListaSolicitud = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarSolicitudes(lblPlacaDesv.Text);

                            if (dtListaSolicitud.Rows.Count > 0)
                            {
                                dtgVincularSolicitud.DataSource = dtListaSolicitud;

                                dgvVincularSolicitudVista.Columns["idSolicitud"].Visible = false;
                                dgvVincularSolicitudVista.Columns["TRACTO"].Visible = false;
                                dgvVincularSolicitudVista.BestFitColumns();

                                pVincularSolicitud.Location = new Point(562, 158);
                                pVincularSolicitud.Visible = true;
                                pVincularSolicitud.BringToFront();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarDesv_Click(object sender, EventArgs e)
        {
            lblPlacaDesv.Text = "";
            lblTipoMttoDesv.Text = "";
            lblNro.Text = "";

            pVincularSolicitud.Visible = false;
            pVincularSolicitud.SendToBack();
        }

        private void pVincularSolicitud_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick8 = e.X; yClick8 = e.Y; }
            else
            {
                pVincularSolicitud.Left = pVincularSolicitud.Left + (e.X - xClick8);
                pVincularSolicitud.Top = pVincularSolicitud.Top + (e.Y - yClick8);
            }
        }

        private void dtgVincularSolicitud_DoubleClick(object sender, EventArgs e)
        {
            int idSolicitud = Convert.ToInt32(dgvVincularSolicitudVista.GetRowCellValue(dgvVincularSolicitudVista.FocusedRowHandle, "idSolicitud"));
            
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_VincularSolicitudes(1, Convert.ToInt32(lblNro.Text), idSolicitud);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            
            if (NroRPTA == "0")
            {
                btnCerrarDesv_Click(sender, e);
                ListarDesviacion();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            int idSolicitud = Convert.ToInt32(dgvVincularSolicitudVista.GetRowCellValue(dgvVincularSolicitudVista.FocusedRowHandle, "idSolicitud"));

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_VincularSolicitudes(2, Convert.ToInt32(lblNro.Text), idSolicitud);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0")
            {
                btnCerrarDesv_Click(sender, e);
                ListarDesviacion();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvDesviacion_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvDesviacion.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvDesviacion.Columns[e.ColumnIndex].Name.Contains("DESVIACION"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToDecimal(e.Value) <= 0)
                            {
                                e.CellStyle.BackColor = Color.PaleGreen;
                                e.CellStyle.ForeColor = Color.Green;  
                            }
                            else
                            {
                                e.CellStyle.BackColor = Color.Pink;
                                e.CellStyle.ForeColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }
    }
}