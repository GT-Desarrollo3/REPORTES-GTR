using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.Data;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using System.IO;
using Comun;
using Negocio;
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    public partial class frmListaControlItems : Form
    {
        DataTable dtListaItems = new DataTable();
        DataTable dtListaKN = new DataTable();
        DataTable dtListaExtintor = new DataTable();
        DataTable dtListaKAD = new DataTable();
        DataTable dtListaTC = new DataTable();
        DataTable dtListaAT = new DataTable();
        DataTable dtListaCT = new DataTable();
        DataTable dtListaKV = new DataTable();
        DataTable dtListaKL = new DataTable();
        DataTable dtListaCortinera = new DataTable();

        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();

        int idVehiculo, idVehiculo2, idVehiculo3, idVehiculoC, Opcion, idExtintor;
        int Opcion2, idKAD;
        int OpcionC, idCortinera;
        string UnidadPeso, TieneKAD, TieneC, TipoAT, OpcionH, OpcionCS, TipoBotiquin = "";
        int xClick = 0, yClick = 0;
        int xClick2 = 0, yClick2 = 0;
        int xClick3 = 0, yClick3 = 0;
        int xClick4 = 0, yClick4 = 0;
        int xClick5 = 0, yClick5 = 0;
        int xClick6 = 0, yClick6 = 0;
        int e1 = 0, e2 = 0, e3 = 0, e4 = 0, e5 = 0, e6 = 0, e7 = 0, e8 = 0, e9 = 0, e10 = 0, e11 = 0, e12 = 0, e13 = 0, e14 = 0;
        int KNPersona, KNNuevaPersona, KNTracto, KNPersona2, KNTracto2;
        int idKitVolcan = -1, idKitLimagas = -1, idTractoV = -1, idCarretaV = -1, PersonaV = -1, TipoVH;
        public byte[] byteArrayImagen = null;
        public byte[] byteArrayImagen2 = null;
        public byte[] byteArrayImagen3 = null;
        public byte[] byteArrayImagenC = null;

        public frmListaControlItems()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
            cbxOperaciones2.SelectedIndexChanged -= cbxOperaciones2_SelectedIndexChanged;
            cbxOperaciones3.SelectedIndexChanged -= cbxOperaciones3_SelectedIndexChanged;
            cbxOperaciones4.SelectedIndexChanged -= cbxOperaciones4_SelectedIndexChanged;
            cbxOperaciones5.SelectedIndexChanged -= cbxOperaciones5_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void cbxOperaciones2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion2(); }

        private void cbxOperaciones3_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion3(); }

        private void cbxOperaciones4_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion4(); }

        private void cbxOperaciones5_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion5(); }

        private void frmListaControlItems_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaControlItems");
            if (dtPermisos.Rows.Count > 0) { }

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Botiquines")
                    {
                        btnAgregar.Enabled = true;
                        groupBox3.Enabled = true;
                        tsModificar.Enabled = true;
                        tsGenerarRequerimiento.Enabled = true;
                        tsDesvincular.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else
                    {
                        btnAgregar.Enabled = false;
                        groupBox3.Enabled = false;
                        tsModificar.Enabled = false;
                        tsGenerarRequerimiento.Enabled = false;
                        tsDesvincular.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Kit Neumaticos")
                    {
                        btnAsignarKit.Enabled = true;
                        btnDevolverKit.Enabled = true;
                        i = 999; e2 = 1;
                    }
                    else
                    {
                        btnAsignarKit.Enabled = false;
                        btnDevolverKit.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Extintores")
                    {
                        btnNuevoExtintor.Enabled = true;
                        tsActualizarFecha.Enabled = true;
                        tsEliminarExtintor.Enabled = true;
                        tsGenerarRequerimiento2.Enabled = true;
                        i = 999; e3 = 1;
                    }
                    else
                    {
                        btnNuevoExtintor.Enabled = false;
                        tsActualizarFecha.Enabled = false;
                        tsEliminarExtintor.Enabled = false;
                        tsGenerarRequerimiento2.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Kit Anti-derrame")
                    {
                        btnNuevoKAD.Enabled = true;
                        tsModificarEstado.Enabled = true;
                        tsEliminarKAD.Enabled = true;
                        i = 999; e4 = 1;
                    }
                    else
                    {
                        btnNuevoKAD.Enabled = false;
                        tsModificarEstado.Enabled = false;
                        tsEliminarKAD.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Tanques Combustible")
                    {
                        btnNuevoTanque.Enabled = true;
                        tsEditarRevision.Enabled = true;
                        tsAnadirReparacion.Enabled = true;
                        tsEliminarRevision.Enabled = true;
                        i = 999; e5 = 1;
                    }
                    else
                    {
                        btnNuevoTanque.Enabled = false;
                        tsEditarRevision.Enabled = false;
                        tsAnadirReparacion.Enabled = false;
                        tsEliminarRevision.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Tanques Combustible - Revision")
                    {
                        btnNuevoTanque.Enabled = true;
                        tsEditarRevision.Enabled = false;
                        tsAnadirReparacion.Enabled = false;
                        tsEliminarRevision.Enabled = false;
                        i = 999; e6 = 1;
                    }
                    else
                    {
                        btnNuevoTanque.Enabled = false;
                        tsEditarRevision.Enabled = false;
                        tsAnadirReparacion.Enabled = false;
                        tsEliminarRevision.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Tanques Combustible - Reparacion")
                    {
                        btnNuevoTanque.Enabled = false;
                        tsEditarRevision.Enabled = false;
                        tsAnadirReparacion.Enabled = true;
                        tsEliminarRevision.Enabled = false;
                        i = 999; e7 = 1;
                    }
                    else
                    {
                        btnNuevoTanque.Enabled = false;
                        tsEditarRevision.Enabled = false;
                        tsAnadirReparacion.Enabled = false;
                        tsEliminarRevision.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Timones")
                    {
                        btnNuevoAsiento.Enabled = true;
                        tsEditarAsiento.Enabled = true;
                        tsRepararAsiento.Enabled = true;
                        tsEliminarAsiento.Enabled = true;
                        i = 999; e8 = 1;
                    }
                    else
                    {
                        btnNuevoAsiento.Enabled = false;
                        tsEditarAsiento.Enabled = false;
                        tsRepararAsiento.Enabled = false;
                        tsEliminarAsiento.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Timones - Revision")
                    {
                        btnNuevoAsiento.Enabled = true;
                        tsEditarAsiento.Enabled = false;
                        tsRepararAsiento.Enabled = false;
                        tsEliminarAsiento.Enabled = false;
                        i = 999; e9 = 1;
                    }
                    else
                    {
                        btnNuevoAsiento.Enabled = false;
                        tsEditarAsiento.Enabled = false;
                        tsRepararAsiento.Enabled = false;
                        tsEliminarAsiento.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Timones - Reparacion")
                    {
                        btnNuevoAsiento.Enabled = false;
                        tsEditarAsiento.Enabled = false;
                        tsRepararAsiento.Enabled = true;
                        tsEliminarAsiento.Enabled = false;
                        i = 999; e10 = 1;
                    }
                    else
                    {
                        btnNuevoAsiento.Enabled = false;
                        tsEditarAsiento.Enabled = false;
                        tsRepararAsiento.Enabled = false;
                        tsEliminarAsiento.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Operación Volcan")
                    {
                        btnAsignarVolcan.Enabled = true;
                        btnDevolverVolcan.Enabled = true;
                        tsEditarAsignacion.Enabled = true;
                        i = 999; e11 = 1;
                    }
                    else
                    {
                        btnAsignarVolcan.Enabled = false;
                        btnDevolverVolcan.Enabled = false;
                        tsEditarAsignacion.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Conos y Tacos")
                    {
                        btnNuevoCT.Enabled = true;
                        tsEditarRevisionCT.Enabled = true;
                        tsAniadirReparacion.Enabled = true;
                        tsQuitarRevision.Enabled = true;
                        i = 999; e12 = 1;
                    }
                    else
                    {
                        btnNuevoCT.Enabled = false;
                        tsEditarRevisionCT.Enabled = false;
                        tsAniadirReparacion.Enabled = false;
                        tsQuitarRevision.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Operación Limagas")
                    {
                        btnAsignarLimagas.Enabled = true;
                        btnDevolverLimagas.Enabled = true;
                        i = 999; e13 = 1;
                    }
                    else
                    {
                        btnAsignarLimagas.Enabled = false;
                        btnDevolverLimagas.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Cortineras")
                    {
                        btnRegistrarCortinera.Enabled = true;
                        tsModificarC.Enabled = true;
                        tsDesvincularC.Enabled = true;
                        i = 999; e14 = 1;
                    }
                    else
                    {
                        btnRegistrarCortinera.Enabled = false;
                        tsModificarC.Enabled = false;
                        tsDesvincularC.Enabled = false;
                    }
                }
            }
            else
            {
                btnAgregar.Enabled = false;
                groupBox3.Enabled = false;
                tsModificar.Enabled = false;
                tsGenerarRequerimiento.Enabled = false;
                tsDesvincular.Enabled = false;
                btnAsignarKit.Enabled = false;
                btnDevolverKit.Enabled = false;
                btnNuevoExtintor.Enabled = false;
                tsActualizarFecha.Enabled = false;
                tsEliminarExtintor.Enabled = false;
                tsGenerarRequerimiento2.Enabled = false;
                btnNuevoKAD.Enabled = false;
                tsModificarEstado.Enabled = false;
                tsEliminarKAD.Enabled = false;
                btnNuevoTanque.Enabled = false;
                tsEditarRevision.Enabled = false;
                tsAnadirReparacion.Enabled = false;
                tsEliminarRevision.Enabled = false;
                btnNuevoAsiento.Enabled = false;
                tsEditarAsiento.Enabled = false;
                tsRepararAsiento.Enabled = false;
                tsEliminarAsiento.Enabled = false;
                btnAsignarVolcan.Enabled = false;
                btnDevolverVolcan.Enabled = false;
                btnNuevoCT.Enabled = false;
                tsEditarRevisionCT.Enabled = false;
                tsAniadirReparacion.Enabled = false;
                tsQuitarRevision.Enabled = false;
                btnAsignarLimagas.Enabled = false;
                btnDevolverLimagas.Enabled = false;
                tsEditarAsignacion2.Enabled = false;
                btnRegistrarCortinera.Enabled = false;
                tsModificarC.Enabled = false;
                tsDesvincularC.Enabled = false;
            }

            if (e5 == 1 || e6 == 1) { btnNuevoTanque.Enabled = true; }
            if (e8 == 1 || e9 == 1) { btnNuevoAsiento.Enabled = true; }

            CargarComboOperacion();
            CargarComboOperacion2();
            CargarComboOperacion3();
            CargarComboOperacion4();
            CargarComboOperacion5();

            dtpFechaFin.Value = DateTime.Now;

            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            dtpFechaInicio2.Value = new DateTime(dtpFechaInicio2.Value.Year, dtpFechaInicio2.Value.Month, 1);
            dtpFechaFin2.Value = DateTime.Now;
            dtpFechaInicioCT.Value = new DateTime(dtpFechaInicioCT.Value.Year, dtpFechaInicioCT.Value.Month, 1);
            dtpFechaFinCT.Value = DateTime.Now;

            cbxOperaciones.Text = "LINDLEY";
            cbxOperaciones2.Text = "TODO";
            cbxOperaciones3.Text = "TODO";
            cbxEstado.Text = "TODOS";
            cbxKitAsignado.Text = "TODOS";
            cbxOperaciones4.Text = "TODO";
            cbxOperaciones5.Text = "TODO";
            cbxEstadoTC.Text = "TODOS";
            cbxEstadoTA.Text = "TODOS";
            cbxInventario.Text = "TODOS";
            cbxTipoItemCT.Text = "TODOS";
            cbxEstadoCT.Text = "TODOS";
            cbxTodoItem.Text = "TODOS";
            cbxTipoBotiquin.Text = "MTC";

            rbMTC.Checked = true;
            rbMTC_CheckedChanged(sender, e);
            rbTecles.Checked = true;
            rbTecles_Click(sender, e);

            //ListarBotiquinPlacas();
            ListarKitNeumaticos();
            ListarExtintores();
            ListarKitAntiderrame();
            ListarKitVolcan();
            ListarKitLimagas();
            ListarTimonesAsientos();
            ListarConosTacos();
            ListarCortineras();
        }

        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperacion;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        public void CargarComboOperacion2()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones2.DataSource = dtOperacion;
            cbxOperaciones2.DisplayMember = "Descripcion";
            cbxOperaciones2.ValueMember = "IdOperacion";
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

        public void ListarBotiquinPlacas()
        {
            dtListaItems = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarBotiquines(txtPlaca.Text, cbxOperaciones.Text, TipoBotiquin);
            dtgControlBotiquines.DataSource = dtListaItems;
            if (dtListaItems.Rows.Count > 0)
            {
                dgvControlBotiquinesVista.Columns["idBotiquinUnidadC"].Visible = false;
                dgvControlBotiquinesVista.Columns["idBotiquinUnidadD"].Visible = false;
                dgvControlBotiquinesVista.Columns["idItemBotiquin"].Visible = false;
                
                dgvControlBotiquinesVista.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvControlBotiquinesVista.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvControlBotiquinesVista.Columns["FechaModifica"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvControlBotiquinesVista.Columns["FechaModifica"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                // AGRUPAR TABLAS
                dgvControlBotiquinesVista.OptionsView.AllowCellMerge = true;
                dgvControlBotiquinesVista.OptionsView.ShowGroupPanel = false;

                dgvControlBotiquinesVista.OptionsBehavior.Editable = false;
                dgvControlBotiquinesVista.OptionsSelection.EnableAppearanceFocusedCell = false;

                string[] columnasMerge = { "UNIDAD", "OPERACION", "TIPO", "UsuarioCrea", "FechaCrea", "UsuarioModifica", "FechaModifica" };

                foreach (DevExpress.XtraGrid.Columns.GridColumn col in dgvControlBotiquinesVista.Columns)
                {
                    col.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

                    col.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                    col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                }

                foreach (string nombreColumna in columnasMerge)
                {
                    if (dgvControlBotiquinesVista.Columns[nombreColumna] != null)
                    { dgvControlBotiquinesVista.Columns[nombreColumna].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True; }
                }

                dgvControlBotiquinesVista.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                dgvControlBotiquinesVista.Appearance.HeaderPanel.Font = new Font(dgvControlBotiquinesVista.Appearance.HeaderPanel.Font, FontStyle.Bold);

                dgvControlBotiquinesVista.CellMerge -= dgvControlBotiquinesVista_CellMerge;
                dgvControlBotiquinesVista.CellMerge += dgvControlBotiquinesVista_CellMerge;

                dgvControlBotiquinesVista.BestFitColumns();
            }
        }

        public void ListarKitNeumaticos()
        {
            dtListaKN = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarKitNeumatico(2, txtEmpleado2.Text, txtHerramienta.Text, txtTracto.Text);
            dtgKitNeumaticos.DataSource = dtListaKN;
            if (dtListaKN.Rows.Count > 0)
            {
                dgvKitNeumaticosVista.Columns["Persona"].Visible = false;
                dgvKitNeumaticosVista.Columns["idTracto"].Visible = false;
                dgvKitNeumaticosVista.Columns["idHerramienta"].Visible = false;
                dgvKitNeumaticosVista.Columns["Imagen1"].Visible = false;
                dgvKitNeumaticosVista.Columns["Imagen2"].Visible = false;

                dgvKitNeumaticosVista.Columns["FECHA_ENTREGA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvKitNeumaticosVista.Columns["FECHA_ENTREGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvKitNeumaticosVista.BestFitColumns();
                txtHttasE.Text = "Herramientas Entregadas: " + dtListaKN.Rows.Count.ToString();
            }
            else
            {
                dtgKitNeumaticos.DataSource = null;
                txtHttasE.Text = "Herramientas disponibles: ";
            }
        }

        public void ListarExtintores()
        {
            dtListaExtintor = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarExtintores(txtPlaca2.Text, cbxEstado.Text, cbxOperaciones2.Text);
            dtgControlExtintores.DataSource = dtListaExtintor;
            if (dtListaExtintor.Rows.Count > 0)
            {
                dgvControlExtintoresVista.Columns["IdTracto"].Visible = false;

                dgvControlExtintoresVista.Columns["FECHA_VENCIMIENTO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvControlExtintoresVista.Columns["FECHA_VENCIMIENTO"].DisplayFormat.FormatString = "MM/yyyy";
                dgvControlExtintoresVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvControlExtintoresVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvControlExtintoresVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvControlExtintoresVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvControlExtintoresVista.Columns["ESTADO"].Summary.Clear();
                dgvControlExtintoresVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ESTADO", "Total: {0}");

                dgvControlExtintoresVista.BestFitColumns();
            }
            else { dtgControlExtintores.DataSource = null; }
        }

        public void ListarKitAntiderrame()
        {
            dtListaKAD = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarKAD(txtPlaca3.Text, cbxKitAsignado.Text, cbxOperaciones3.Text);
            dtgAntiDerrame.DataSource = dtListaKAD;
            if (dtListaKAD.Rows.Count > 0)
            {
                dgvAntiDerrameVista.Columns["IdTracto"].Visible = false;
                dgvAntiDerrameVista.Columns["Imagen"].Visible = false;

                dgvAntiDerrameVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvAntiDerrameVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvAntiDerrameVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvAntiDerrameVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvAntiDerrameVista.Columns["ESTADO"].Summary.Clear();
                dgvAntiDerrameVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ESTADO", "Total: {0}");

                dgvAntiDerrameVista.BestFitColumns();
            }
            else { dtgAntiDerrame.DataSource = null; }
        }

        public void ListarTanquesCombustible()
        {
            dtListaTC = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarTanquesCombustible(txtPlaca4.Text, cbxOperaciones4.Text, txtConductor.Text,
                                                   cbxEstadoTC.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
            dtgTanqueC.DataSource = dtListaTC;
            if (dtListaTC.Rows.Count > 0)
            {
                dgvTanqueC.Columns["idTracto"].Visible = false;
                dgvTanqueC.Columns["idCarreta"].Visible = false;
                dgvTanqueC.Columns["PersonaConductor"].Visible = false;
                dgvTanqueC.Columns["PersonaInspector"].Visible = false;
                dgvTanqueC.Columns["ImagenHallazgo"].Visible = false;
                dgvTanqueC.Columns["ImagenHallazgo2"].Visible = false;
                dgvTanqueC.Columns["PersonaTecnico"].Visible = false;
                dgvTanqueC.Columns["ImagenReparacion"].Visible = false;
                dgvTanqueC.Columns["PersonaSeguimiento"].Visible = false;

                dgvTanqueC.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvTanqueC.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvTanqueC.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvTanqueC.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvTanqueC.Columns["ESTADO"].Summary.Clear();
                dgvTanqueC.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ESTADO", "Total: {0}");

                dgvTanqueC.BestFitColumns();
            }
            else { dtgTanqueC.DataSource = null; }
        }

        public void ListarTimonesAsientos()
        {
            int focusedRowHandle = dgvTimonAsiento.FocusedRowHandle;
            
            dtgTimonAsiento.DataSource = null;
            dgvTimonAsiento.Columns.Clear();
            
            dtListaAT = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarAsientosTimones(cbxInventario.Text, txtPlaca5.Text,
                                                   cbxOperaciones5.Text, txtConductor2.Text, cbxEstadoTA.Text, dtpFechaInicio2.Text, dtpFechaFin2.Text);
            dtgTimonAsiento.DataSource = dtListaAT;
            if (dtListaAT.Rows.Count > 0)
            {
                dgvTimonAsiento.Columns["idTracto"].Visible = false;
                dgvTimonAsiento.Columns["PersonaConductor"].Visible = false;
                dgvTimonAsiento.Columns["PersonaInspector"].Visible = false;
                dgvTimonAsiento.Columns["PersonaProveedor"].Visible = false;
                dgvTimonAsiento.Columns["PersonaSeguimiento"].Visible = false;

                if (cbxInventario.Text == "TIMONES" || cbxInventario.Text == "ASIENTOS")
                { dgvTimonAsiento.Columns["TAPIZADO"].Visible = true; }
                else { dgvTimonAsiento.Columns["TAPIZADO"].Visible = false; }

                dgvTimonAsiento.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvTimonAsiento.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvTimonAsiento.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvTimonAsiento.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvTimonAsiento.Columns["ESTADO"].Summary.Clear();
                dgvTimonAsiento.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ESTADO", "Total: {0}");

                dgvTimonAsiento.BestFitColumns();

                var colImg = dgvTimonAsiento.Columns["ImagenHallazgo"];
                var colImg2 = dgvTimonAsiento.Columns["ImagenReparacion"];

                if (colImg != null)
                {
                    var repImg = new RepositoryItemPictureEdit();
                    repImg.SizeMode = PictureSizeMode.Zoom;
                    repImg.CustomHeight = 80;
                    repImg.NullText = "";

                    dtgTimonAsiento.RepositoryItems.Add(repImg);
                    colImg.ColumnEdit = repImg;

                    colImg.Width = 180;
                    dgvTimonAsiento.RowHeight = 90;

                    colImg.OptionsColumn.FixedWidth = true;
                }

                if (colImg2 != null)
                {
                    var repImg = new RepositoryItemPictureEdit();
                    repImg.SizeMode = PictureSizeMode.Zoom;
                    repImg.CustomHeight = 80;
                    repImg.NullText = "";

                    dtgTimonAsiento.RepositoryItems.Add(repImg);
                    colImg2.ColumnEdit = repImg;

                    colImg2.Width = 180;
                    dgvTimonAsiento.RowHeight = 90;

                    colImg2.OptionsColumn.FixedWidth = true;
                }
            }
            else { dtgTimonAsiento.DataSource = null; }

            if (focusedRowHandle >= 0 && focusedRowHandle < dgvTimonAsiento.RowCount) { dgvTimonAsiento.FocusedRowHandle = focusedRowHandle; }
        }

        public void ListarKitVolcan()
        {
            dtListaKV = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_ListarRegistros(txtPlacaV.Text, txtCarretaV.Text, txtEmpleado.Text, txtItemV.Text);
            dtgOperacionVolcan.DataSource = dtListaKV;

            if (dtListaKV.Rows.Count > 0)
            {
                dgvOperacionVolcanVista.Columns["idKitVolcan"].Visible = false;
                dgvOperacionVolcanVista.Columns["idItemV"].Visible = false;
                dgvOperacionVolcanVista.Columns["Persona"].Visible = false;
                dgvOperacionVolcanVista.Columns["idTracto"].Visible = false;
                //dgvOperacionVolcanVista.Columns["idCarreta"].Visible = false;

                dgvOperacionVolcanVista.Columns["FECHA_ENTREGA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvOperacionVolcanVista.Columns["FECHA_ENTREGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvOperacionVolcanVista.BestFitColumns();
                txtImplE.Text = "Implementos Entregados: " + dtListaKV.Rows.Count.ToString();
            }
            else
            {
                dtgOperacionVolcan.DataSource = null;
                txtImplE.Text = "Implementos Entregados: 0";
            }
        }

        public void ListarKitLimagas()
        {
            dtListaKL = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_ListarRegistros(txtPlacaL.Text, txtCarretaL.Text, txtEmpleadoL.Text, txtItemL.Text);
            dtgOperacionLimagas.DataSource = dtListaKL;

            if (dtListaKL.Rows.Count > 0)
            {
                dgvOperacionLimagasVista.Columns["idKitLimagas"].Visible = false;
                dgvOperacionLimagasVista.Columns["idItemL"].Visible = false;
                dgvOperacionLimagasVista.Columns["Persona"].Visible = false;
                dgvOperacionLimagasVista.Columns["idTracto"].Visible = false;
                //dgvOperacionLimagasVista.Columns["idCarreta"].Visible = false;

                dgvOperacionLimagasVista.Columns["FECHA_ENTREGA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvOperacionLimagasVista.Columns["FECHA_ENTREGA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvOperacionLimagasVista.BestFitColumns();
                txtImplE2.Text = "Implementos Entregados: " + dtListaKL.Rows.Count.ToString();
            }
            else
            {
                dtgOperacionVolcan.DataSource = null;
                txtImplE2.Text = "Implementos Entregados: 0";
            }
        }

        public void ListarConosTacos()
        {
            int focusedRowHandle = dgvConosTacosView.FocusedRowHandle;
            
            dtgConosTacos.DataSource = null;
            dgvConosTacosView.Columns.Clear();

            dtListaCT = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarConosTacos(dtpFechaInicioCT.Text, dtpFechaFinCT.Text, txtPlacaCT.Text, cbxTipoItemCT.Text, cbxEstadoCT.Text);
            dtgConosTacos.DataSource = dtListaCT;

            if (dtListaCT.Rows.Count > 0)
            {
                dgvConosTacosView.Columns["idTracto"].Visible = false;
                dgvConosTacosView.Columns["PersonaR"].Visible = false;

                dgvConosTacosView.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvConosTacosView.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvConosTacosView.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvConosTacosView.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvConosTacosView.Columns["ESTADO"].Summary.Clear();
                dgvConosTacosView.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ESTADO", "Total: {0}");

                dgvConosTacosView.BestFitColumns();

                var colImg = dgvConosTacosView.Columns["ImagenHallazgo"];
                var colImg2 = dgvConosTacosView.Columns["ImagenReparacion"];

                if (colImg != null)
                {
                    var repImg = new RepositoryItemPictureEdit();
                    repImg.SizeMode = PictureSizeMode.Zoom;
                    repImg.CustomHeight = 80;
                    repImg.NullText = "";

                    dtgConosTacos.RepositoryItems.Add(repImg);
                    colImg.ColumnEdit = repImg;

                    colImg.Width = 180;
                    dgvConosTacosView.RowHeight = 90;

                    colImg.OptionsColumn.FixedWidth = true;
                }

                if (colImg2 != null)
                {
                    var repImg = new RepositoryItemPictureEdit();
                    repImg.SizeMode = PictureSizeMode.Zoom;
                    repImg.CustomHeight = 80;
                    repImg.NullText = "";

                    dtgConosTacos.RepositoryItems.Add(repImg);
                    colImg2.ColumnEdit = repImg;

                    colImg2.Width = 180;
                    dgvConosTacosView.RowHeight = 90;

                    colImg2.OptionsColumn.FixedWidth = true;
                }
            }
            else { dtgConosTacos.DataSource = null; }

            if (focusedRowHandle >= 0 && focusedRowHandle < dgvConosTacosView.RowCount) { dgvConosTacosView.FocusedRowHandle = focusedRowHandle; }
        }

        public void ListarCortineras()
        {
            int focusedRowHandle = dgvCortinerasVista.FocusedRowHandle;
            
            dtgCortineras.DataSource = null;
            dgvCortinerasVista.Columns.Clear();

            dtListaCortinera = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarCortinera(OpcionCS, txtPlacaC.Text, cbxTodoItem.Text);
            dtgCortineras.DataSource = dtListaCortinera;
            
            if (dtListaCortinera.Rows.Count > 0)
            {
                dgvCortinerasVista.Columns["IdTracto"].Visible = false;

                dgvCortinerasVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvCortinerasVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvCortinerasVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvCortinerasVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvCortinerasVista.Columns["CANTIDAD"].Summary.Clear();
                dgvCortinerasVista.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Total: {0}");
                
                dgvCortinerasVista.BestFitColumns();

                var colImg = dgvCortinerasVista.Columns["Imagen"];
                if (colImg != null)
                {
                    var repImg = new RepositoryItemPictureEdit();
                    repImg.SizeMode = PictureSizeMode.Zoom;
                    repImg.CustomHeight = 80;
                    repImg.NullText = "";

                    dtgCortineras.RepositoryItems.Add(repImg);
                    colImg.ColumnEdit = repImg;

                    colImg.Width = 180;
                    dgvCortinerasVista.RowHeight = 90;

                    colImg.OptionsColumn.FixedWidth = true;
                }
            }
            else { dtgCortineras.DataSource = null; }

            if (focusedRowHandle >= 0 && focusedRowHandle < dgvCortinerasVista.RowCount) { dgvCortinerasVista.FocusedRowHandle = focusedRowHandle; }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarItemBotiquin frmAgregarItemBotiquin = new frmAgregarItemBotiquin();
            frmAgregarItemBotiquin.Show(this);
        }

        private void txtNuevaPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlacas, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtNuevaPlaca.Text), true, false, false);
            lstPlacas.Columns[0].Width = 0;
            lstPlacas.Columns[1].Width = 80;
            lstPlacas.Columns[2].Width = 100;
            lstPlacas.Columns[3].Width = 0;
            lstPlacas.Columns[4].Width = 130;
            lstPlacas.Columns[5].Width = 0;
            lstPlacas.BringToFront();
            lstPlacas.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo = -1;
                txtOperacion.Clear();
                lstPlacas.Visible = false;
                lstPlacas.SendToBack();
            }
        }

        private void txtNuevaPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlacas.Focus(); }
        }

        private void lstPlacas_Enter(object sender, EventArgs e)
        {
            if (!lstPlacas.Items.Count.Equals(0)) { lstPlacas.Items[0].Selected = true; }
        }

        private void lstPlacas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlacas.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlacas.SelectedItems[0];

                idVehiculo = Int32.Parse(ItemActual.Text);
                txtNuevaPlaca.Text = ItemActual.SubItems[1].Text;
                txtOperacion.Text = ItemActual.SubItems[4].Text;

                lstPlacas.Visible = false;
                lstPlacas.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo = -1;
                txtOperacion.Clear();
                lstPlacas.Visible = false;
                lstPlacas.SendToBack();
            }
        }

        private void lstPlacas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlacas.SelectedItems[0];

            idVehiculo = Int32.Parse(ItemActual.Text);
            txtNuevaPlaca.Text = ItemActual.SubItems[1].Text;
            txtOperacion.Text = ItemActual.SubItems[4].Text;

            lstPlacas.Visible = false;
            lstPlacas.SendToBack();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtOperacion.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese correctamente la placa.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNuevaPlaca.Focus();
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_AsignarBotiquin(idVehiculo, cbxTipoBotiquin.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtNuevaPlaca.Clear();
                        txtOperacion.Clear();
                        cbxTipoBotiquin.Text = "MTC";
                        idVehiculo = -1;
                        ListarBotiquinPlacas();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch { MessageBox.Show("No se pudo registrar el ítem.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void rbMTC_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMTC.Checked == true) { TipoBotiquin = "MTC"; }
        }

        private void rbQuemadura_CheckedChanged(object sender, EventArgs e)
        {
            if (rbQuemadura.Checked == true) { TipoBotiquin = "QUEMADURA"; }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarBotiquinPlacas(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarBotiquinPlacas(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgControlBotiquines.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Botiquines de Unidades - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgControlBotiquines.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvControlBotiquinesVista_CellMerge(object sender, CellMergeEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            string[] columnasMerge = { "UNIDAD", "OPERACION", "TIPO", "UsuarioCrea", "FechaCrea", "UsuarioModifica", "FechaModifica" };

            if (!columnasMerge.Contains(e.Column.FieldName))
            {
                e.Merge = false;
                e.Handled = true;
                return;
            }

            object Unidad1 = view.GetRowCellValue(e.RowHandle1, "UNIDAD");
            object Unidad2 = view.GetRowCellValue(e.RowHandle2, "UNIDAD");

            object Operacion1 = view.GetRowCellValue(e.RowHandle1, "OPERACION");
            object Operacion2 = view.GetRowCellValue(e.RowHandle2, "OPERACION");

            object Tipo1 = view.GetRowCellValue(e.RowHandle1, "TIPO");
            object Tipo2 = view.GetRowCellValue(e.RowHandle2, "TIPO");

            bool mismoGrupo = Equals(Unidad1, Unidad2) && Equals(Operacion1, Operacion2) && Equals(Tipo1, Tipo2);

            e.Merge = mismoGrupo && Equals(e.CellValue1, e.CellValue2);
            e.Handled = true;
        }

        private void dgvControlBotiquinesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "PENDIENTE")
                { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }
                
                if (Convert.ToString(e.CellValue) == "POR VENCER")
                { e.Appearance.BackColor = Color.Yellow; }

                if (Convert.ToString(e.CellValue) == "CONFORME")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "VENCIDO")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }

            if (e.Column.FieldName == "CANTIDAD")
            {
                if (Convert.ToInt32(e.CellValue) == 0)
                {
                    e.Appearance.BackColor = Color.MistyRose;
                    e.Appearance.ForeColor = Color.FromArgb(255, 0, 0);
                }
            }
        }

        private void dtgControlBotiquines_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idBotiquinUnidadC = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "idBotiquinUnidadC"));
                string Estado = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "ESTADO"));

                if (idBotiquinUnidadC != "")
                {
                    if (e1 == 1)
                    {
                        tsModificar.Enabled = true;
                        if (Estado == "PENDIENTE" || Estado == "VENCIDO" || Estado == "POR VENCER") { tsGenerarRequerimiento.Enabled = true; }
                        else { tsGenerarRequerimiento.Enabled = false; }
                        tsDesvincular.Enabled = true;
                    }
                }
                else
                {
                    tsModificar.Enabled = false;
                    tsDesvincular.Enabled = false;
                    tsGenerarRequerimiento.Enabled = false;
                }
            }
            catch
            {
                tsModificar.Enabled = false;
                tsDesvincular.Enabled = false;
                tsGenerarRequerimiento.Enabled = false;
            }
        }

        private void dtgControlBotiquines_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e1 == 1)
                {
                    frmAsignarBotiquin frmAsignarBotiquin = new frmAsignarBotiquin();
                    frmAsignarBotiquin.idBotiquinUnidadC = Convert.ToInt32(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "idBotiquinUnidadC"));
                    frmAsignarBotiquin.lblPlaca.Text = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "UNIDAD"));
                    frmAsignarBotiquin.TipoBotiquin = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "TIPO"));
                    frmAsignarBotiquin.lblProgramacion.Text = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "OPERACION"));
                    frmAsignarBotiquin.CargarComboItems();
                    frmAsignarBotiquin.cbxItem.Text = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "ITEM"));
                    frmAsignarBotiquin.dtpFVencimiento.Value = Convert.ToDateTime(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "FECHA_VENCIMIENTO"));
                    frmAsignarBotiquin.txtCantidad.Text = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "CANTIDAD"));
                    frmAsignarBotiquin.txtObservacion.Text = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "OBSERVACION"));
                    frmAsignarBotiquin.frmListaControlItems = this;
                    frmAsignarBotiquin.Show();
                }
            }
            catch { MessageBox.Show("El ítem seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsModificar_Click(object sender, EventArgs e)
        {
            frmAsignarBotiquin frmAsignarBotiquin = new frmAsignarBotiquin();
            frmAsignarBotiquin.idBotiquinUnidadC = Convert.ToInt32(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "idBotiquinUnidadC"));
            frmAsignarBotiquin.lblPlaca.Text = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "UNIDAD"));
            frmAsignarBotiquin.TipoBotiquin = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "TIPO"));
            frmAsignarBotiquin.lblProgramacion.Text = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "OPERACION"));
            frmAsignarBotiquin.CargarComboItems();
            frmAsignarBotiquin.cbxItem.Text = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "ITEM"));
            frmAsignarBotiquin.dtpFVencimiento.Value = Convert.ToDateTime(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "FECHA_VENCIMIENTO"));
            frmAsignarBotiquin.txtCantidad.Text = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "CANTIDAD"));
            frmAsignarBotiquin.txtObservacion.Text = Convert.ToString(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "OBSERVACION"));
            frmAsignarBotiquin.frmListaControlItems = this;
            frmAsignarBotiquin.Show();
        }

        private void tsDesvincular_Click(object sender, EventArgs e)
        {
            try
            {
                int idBotiquinUnidadC = Convert.ToInt32(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "idBotiquinUnidadC"));

                if (MessageBox.Show("¿Desea desvincular el botiquín de esta unidad?", "DESVINCULAR BOTIQUIN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_EliminarItemBotiquin(3, idBotiquinUnidadC, -1);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarBotiquinPlacas(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el ítem.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsGenerarRequerimiento_Click(object sender, EventArgs e)
        {
            try
            {
                int idBotiquinUnidadC = Convert.ToInt32(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "idBotiquinUnidadC"));
                int idBotiquinUnidadD = Convert.ToInt32(dgvControlBotiquinesVista.GetRowCellValue(dgvControlBotiquinesVista.FocusedRowHandle, "idBotiquinUnidadD"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (MessageBox.Show("¿Desea generar un requerimiento para este ítem?", "GENERAR REQUERIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_GenerarRequerimiento(idBotiquinUnidadC, idBotiquinUnidadD, Usuario);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarBotiquinPlacas();
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el requerimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsignarKit_Click(object sender, EventArgs e)
        {
            frmAsignarKitNeumaticos frmAsignarKitNeumaticos = new frmAsignarKitNeumaticos();
            frmAsignarKitNeumaticos.frmListaControlItems = this;
            frmAsignarKitNeumaticos.Show(this);
        }

        private void btnDevolverKit_Click(object sender, EventArgs e)
        {
            string CodHta, Persona;
            int idTracto;

            int[] filas = dgvKitNeumaticosVista.GetSelectedRows();
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    CodHta = dgvKitNeumaticosVista.GetRowCellValue(filas[i], "idHerramienta").ToString();
                    Persona = dgvKitNeumaticosVista.GetRowCellValue(filas[i], "Persona").ToString();
                    idTracto = Convert.ToInt32(dgvKitNeumaticosVista.GetRowCellValue(filas[i], "idTracto"));

                    DataTable dtRespuesta = new DataTable();
                    DataTable dtRespuesta2 = new DataTable();
                    string Respuesta, Respuesta2;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_AsignarKitNeumatico(2, Convert.ToInt32(CodHta), Convert.ToInt32(Persona), idTracto, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA != "0")
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    else
                    {
                        dtRespuesta2 = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_HerramPersona_Vincula(Convert.ToInt32(CodHta), Convert.ToInt32(Persona), Utilitario.Instancia.SesionUsuario.usuario, 2);
                        Respuesta2 = Convert.ToString(dtRespuesta2.Rows[0]["exito"]);
                        string NroRPTA2 = Respuesta2.Substring(0, 1);
                        if (NroRPTA2 != "0")
                        {
                            MessageBox.Show(Respuesta2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }

                ListarKitNeumaticos();
            }
            else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pNuevoPersonal_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick3 = e.X; yClick3 = e.Y; }
            else
            {
                pNuevoPersonal.Left = pNuevoPersonal.Left + (e.X - xClick3);
                pNuevoPersonal.Top = pNuevoPersonal.Top + (e.Y - yClick3);
            }
        }

        private void dtgKitNeumaticos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Persona = Convert.ToString(dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "Persona"));

                if (Persona != "") { tsCambiarPersonal.Enabled = true; }
                else { tsCambiarPersonal.Enabled = false; }
            }
            catch { tsCambiarPersonal.Enabled = false; }
        }

        private void tsCambiarPersonal_Click(object sender, EventArgs e)
        {
            KNTracto = Convert.ToInt32(dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "idTracto"));
            KNPersona = Convert.ToInt32(dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "Persona"));

            txtKNPlaca.Text = Convert.ToString(dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "TRACTO"));
            txtKNPersonal.Text = Convert.ToString(dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "EMPLEADO"));

            pImagenKN.Location = new System.Drawing.Point(699, 233); 
            pNuevoPersonal.Visible = true;
            pNuevoPersonal.BringToFront();
        }

        private void btnKNCerrar_Click(object sender, EventArgs e)
        {
            KNTracto = -1; KNPersona = -1; KNNuevaPersona = -1;

            txtKNPlaca.Clear();
            txtKNPersonal.Clear();
            txtKNNuevoPersonal.Clear();
            lstKNPersonal.Visible = false;
            lstKNPersonal.SendToBack();

            pNuevoPersonal.Visible = false;
            pNuevoPersonal.SendToBack();
        }

        private void txtKNNuevoPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstKNPersonal, clsConsultaBL.Instancia.GetEmpleado(txtKNNuevoPersonal.Text), true, false, false);
            lstKNPersonal.Columns[0].Width = 0;
            lstKNPersonal.Columns[1].Width = 300;
            lstKNPersonal.Columns[2].Width = 110;
            lstKNPersonal.BringToFront();
            lstKNPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstKNPersonal.Visible = false;
                lstKNPersonal.SendToBack();
                KNNuevaPersona = -1;
            }
        }

        private void txtKNNuevoPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstKNPersonal.Focus(); }
        }

        private void lstKNPersonal_Enter(object sender, EventArgs e)
        {
            if (!lstKNPersonal.Items.Count.Equals(0)) { lstKNPersonal.Items[0].Selected = true; }
        }

        private void lstKNPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstKNPersonal.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstKNPersonal.SelectedItems[0];
                KNNuevaPersona = Int32.Parse(ItemActual.Text);
                txtKNNuevoPersonal.Text = ItemActual.SubItems[1].Text;

                lstKNPersonal.Visible = false;
                lstKNPersonal.SendToBack();
                btnKNGuardar.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstKNPersonal.Visible = false;
                lstKNPersonal.SendToBack();
                txtKNNuevoPersonal.Focus();
                KNNuevaPersona = -1;
            }
        }

        private void lstKNPersonal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstKNPersonal.SelectedItems[0];
            KNNuevaPersona = Int32.Parse(ItemActual.Text);
            txtKNNuevoPersonal.Text = ItemActual.SubItems[1].Text;

            lstKNPersonal.Visible = false;
            lstKNPersonal.SendToBack();
            btnKNGuardar.Focus();
        }

        private void btnKNGuardar_Click(object sender, EventArgs e)
        {
            if (txtKNNuevoPersonal.Text.Length == 0 || txtKNNuevoPersonal.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese al nuevo personal.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtKNNuevoPersonal.Focus();
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ModificarKitNeumatico(KNPersona, KNNuevaPersona, KNTracto, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnKNCerrar_Click(sender, e);
                        ListarKitNeumaticos();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch { MessageBox.Show("No se pudo asignar el conductor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitNeumaticos(); }
        }

        private void txtEmpleado2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitNeumaticos(); }
        }

        private void txtHerramienta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitNeumaticos(); }
        }

        private void btnBuscar2_Click(object sender, EventArgs e) { ListarKitNeumaticos(); }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            if (dtgKitNeumaticos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Kit de Neumáticos Entregados - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgKitNeumaticos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgKitNeumaticos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                KNTracto2 = Convert.ToInt32(dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "idTracto"));
                KNPersona2 = Convert.ToInt32(dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "Persona"));

                txtKNPlaca2.Text = Convert.ToString(dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "TRACTO"));
                txtKNPersonal2.Text = Convert.ToString(dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "EMPLEADO"));

                if (dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "Imagen1").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "Imagen1");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    byteArrayImagen2 = (Byte[])dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "Imagen1");
                    pbHallazgo2.Image = x;
                    pbHallazgo2.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "Imagen2").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "Imagen2");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    byteArrayImagen3 = (Byte[])dgvKitNeumaticosVista.GetRowCellValue(dgvKitNeumaticosVista.FocusedRowHandle, "Imagen2");
                    pbHallazgo3.Image = x;
                    pbHallazgo3.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                pImagenKN.Location = new System.Drawing.Point(654, 233); 
                pImagenKN.Visible = true;
                pImagenKN.BringToFront();
            }
            catch { }
        }

        private void pImagenKN_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick5 = e.X; yClick5 = e.Y; }
            else
            {
                pImagenKN.Left = pImagenKN.Left + (e.X - xClick5);
                pImagenKN.Top = pImagenKN.Top + (e.Y - yClick5);
            }
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            KNTracto2 = -1;
            KNPersona2 = -1;
            btnCerrarIN_Click(sender, e);
            btnCerrarIN2_Click(sender, e);
            txtKNPlaca2.Clear();
            txtKNPersonal2.Clear();

            pImagenKN.Visible = false;
            pImagenKN.SendToBack();
        }

        private void btnBuscarIN_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK)
                {
                    byteArrayImagen2 = File.ReadAllBytes(getImage.FileName);

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayImagen2));
                    pbHallazgo2.Image = x;
                    pbHallazgo2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrarIN_Click(object sender, EventArgs e)
        {
            byteArrayImagen2 = null;
            pbHallazgo2.Image = null;
            pbHallazgo2.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnBuscarIN2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK)
                {
                    byteArrayImagen3 = File.ReadAllBytes(getImage.FileName);

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayImagen3));
                    pbHallazgo3.Image = x;
                    pbHallazgo3.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrarIN2_Click(object sender, EventArgs e)
        {
            byteArrayImagen3 = null;
            pbHallazgo3.Image = null;
            pbHallazgo3.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnGuardarKN_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_EditarKitNeumatico(KNTracto2, KNPersona2, byteArrayImagen2, byteArrayImagen3, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar3_Click(sender, e);
                    ListarKitNeumaticos();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("No se pudieron actualizar las imágenes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnNuevoExtintor_Click(object sender, EventArgs e)
        {
            Opcion = 1;
            idExtintor = 0;
            btnCancelar2.Enabled = true;
            txtNuevaPlaca2.ReadOnly = false;
            dtpFechaVenc.Value = new DateTime(dtpFechaVenc.Value.Year, dtpFechaVenc.Value.Month, 1);
            rbKilos.Checked = true;
            rbKilos_Click(sender, e);

            pNuevoExtintor.Visible = true;
            pNuevoExtintor.BringToFront();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            btnCancelar2_Click(sender, e);
            txtNuevaPlaca2.ReadOnly = false;
            pNuevoExtintor.Visible = false;
            pNuevoExtintor.SendToBack();
        }

        private void pNuevoExtintor_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevoExtintor.Left = pNuevoExtintor.Left + (e.X - xClick);
                pNuevoExtintor.Top = pNuevoExtintor.Top + (e.Y - yClick);
            }
        }

        private void txtNuevaPlaca2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlacas2, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtNuevaPlaca2.Text), true, false, false);
            lstPlacas2.Columns[0].Width = 0;
            lstPlacas2.Columns[1].Width = 80;
            lstPlacas2.Columns[2].Width = 100;
            lstPlacas2.Columns[3].Width = 0;
            lstPlacas2.Columns[4].Width = 130;
            lstPlacas2.Columns[5].Width = 0;
            lstPlacas2.BringToFront();
            lstPlacas2.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo2 = -1;
                txtOperacion2.Clear();
                lstPlacas2.Visible = false;
                lstPlacas2.SendToBack();
            }
        }

        private void txtNuevaPlaca2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlacas2.Focus(); }
        }

        private void lstPlacas2_Enter(object sender, EventArgs e)
        {
            if (!lstPlacas2.Items.Count.Equals(0)) { lstPlacas2.Items[0].Selected = true; }
        }

        private void lstPlacas2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlacas2.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlacas2.SelectedItems[0];

                idVehiculo2 = Int32.Parse(ItemActual.Text);
                txtNuevaPlaca2.Text = ItemActual.SubItems[1].Text;
                txtOperacion2.Text = ItemActual.SubItems[4].Text;
                txtCodigo.Focus();

                lstPlacas2.Visible = false;
                lstPlacas2.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo2 = -1;
                txtOperacion2.Clear();
                lstPlacas2.Visible = false;
                lstPlacas2.SendToBack();
            }
        }

        private void lstPlacas2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlacas2.SelectedItems[0];

            idVehiculo2 = Int32.Parse(ItemActual.Text);
            txtNuevaPlaca2.Text = ItemActual.SubItems[1].Text;
            txtOperacion2.Text = ItemActual.SubItems[4].Text;
            txtCodigo.Focus();

            lstPlacas2.Visible = false;
            lstPlacas2.SendToBack();
        }

        private void txtPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void rbKilos_Click(object sender, EventArgs e)
        {
            if (rbKilos.Checked == true) { UnidadPeso = "K"; }
        }

        private void rbLibras_Click(object sender, EventArgs e)
        {
            if (rbLibras.Checked == true) { UnidadPeso = "LB"; }
        }

        private void btnCancelar2_Click(object sender, EventArgs e)
        {
            txtNuevaPlaca2.Clear();
            idVehiculo2 = -1;
            txtOperacion2.Clear();
            txtCodigo.Clear();
            dtpFechaVenc.Value = DateTime.Now;
            txtPeso.Clear();
            rbKilos.Checked = true;
            rbKilos_Click(sender, e);
        }

        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            if (txtNuevaPlaca2.Text.Length == 0 || txtOperacion2.Text.Length == 0 || txtPeso.Text.Length == 0)
            {
                if (txtOperacion2.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese correctamente la placa.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNuevaPlaca2.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese correctamente el peso.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPeso.Focus();
                }
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarExtintor(Opcion, idExtintor, idVehiculo2, txtCodigo.Text,
                                                             dtpFechaVenc.Value, Convert.ToDecimal(txtPeso.Text), UnidadPeso, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCancelar2_Click(sender, e);
                        btnCerrar_Click(sender, e);
                        ListarExtintores();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch { MessageBox.Show("No se pudo asignar el extintor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void txtPlaca2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarExtintores(); }
        }

        private void cbxOperaciones2_DropDownClosed(object sender, EventArgs e) { ListarExtintores(); }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarExtintores(); }

        private void btnBuscar3_Click(object sender, EventArgs e) { ListarExtintores(); }

        private void btnExcel3_Click(object sender, EventArgs e)
        {
            if (dtgControlExtintores.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Extintores de Unidades - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgControlExtintores.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvControlExtintoresVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "VIGENTE")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "POR VENCER")
                { e.Appearance.BackColor = Color.Yellow; }

                if (Convert.ToString(e.CellValue) == "VENCIDO")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void dtgControlExtintores_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idExtintor = Convert.ToString(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "NRO"));
                string Estado = Convert.ToString(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "ESTADO"));

                if (idExtintor != "")
                {
                    if (e3 == 1)
                    {
                        tsActualizarFecha.Enabled = true;
                        if (Estado == "VENCIDO" || Estado == "POR VENCER") { tsGenerarRequerimiento2.Enabled = true; }
                        else { tsGenerarRequerimiento2.Enabled = false; }
                        tsEliminarExtintor.Enabled = true;
                    }
                }
                else
                {
                    tsActualizarFecha.Enabled = false;
                    tsEliminarExtintor.Enabled = false;
                    tsGenerarRequerimiento2.Enabled = false;
                }
            }
            catch
            {
                tsActualizarFecha.Enabled = false;
                tsEliminarExtintor.Enabled = false;
                tsGenerarRequerimiento2.Enabled = false;
            }
        }

        private void tsActualizarFecha_Click(object sender, EventArgs e)
        {
            Opcion = 2;
            dtpFechaVenc.Value = DateTime.Now;
            dtpFechaVenc.Value = new DateTime(dtpFechaVenc.Value.Year, dtpFechaVenc.Value.Month, 1);
            btnCancelar2.Enabled = false;

            idVehiculo2 = Convert.ToInt32(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "IdTracto"));
            idExtintor = Convert.ToInt32(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "NRO"));
            txtNuevaPlaca2.ReadOnly = true;
            txtNuevaPlaca2.Text = Convert.ToString(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "UNIDAD"));
            txtOperacion2.Text = Convert.ToString(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "OPERACION"));
            txtCodigo.Text = Convert.ToString(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "CODIGO"));
            txtPeso.Text = Convert.ToString(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "PESO"));
            dtpFechaVenc.Text = Convert.ToString(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "FECHA_VENCIMIENTO"));
            string UnidadPeso = Convert.ToString(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "UNIDAD_PESO"));
            if (UnidadPeso == "K")
            {
                rbKilos.Checked = true;
                rbKilos_Click(sender, e);
            }
            else
            {
                rbLibras.Checked = true;
                rbLibras_Click(sender, e);
            }

            pNuevoExtintor.Visible = true;
            pNuevoExtintor.BringToFront();
        }

        private void tsEliminarExtintor_Click(object sender, EventArgs e)
        {
            try
            {
                int idTracto = Convert.ToInt32(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "IdTracto"));
                int idExtintor = Convert.ToInt32(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "NRO"));

                if (MessageBox.Show("¿Desea desvincular este extintor de la unidad?", "DESVINCULAR EXTINTOR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarExtintor(3, idExtintor, idTracto, "", DateTime.Now, 0, "", "");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarExtintores(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el extintor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsGenerarRequerimiento2_Click(object sender, EventArgs e)
        {
            /*
            try
            {
                int idTracto = Convert.ToInt32(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "IdTracto"));
                int idExtintor = Convert.ToInt32(dgvControlExtintoresVista.GetRowCellValue(dgvControlExtintoresVista.FocusedRowHandle, "NRO"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (MessageBox.Show("¿Desea generar un requerimiento para esta unidad?", "GENERAR REQUERIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_GenerarRequerimiento(idBotiquinUnidadC, idBotiquinUnidadD, Usuario);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarBotiquinPlacas();
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el requerimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            */
        }

        private void btnNuevoKAD_Click(object sender, EventArgs e)
        {
            Opcion2 = 1;
            idKAD = 0;
            btnCancelar3.Enabled = true;
            txtNuevaPlaca3.ReadOnly = false;
            rbSiKAD.Checked = true;
            rbSiKAD_Click(sender, e);
            pNuevoKAD.Location = new System.Drawing.Point(654, 202); 

            pNuevoKAD.Visible = true;
            pNuevoKAD.BringToFront();
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            btnCancelar3_Click(sender, e);
            txtNuevaPlaca3.ReadOnly = false;
            pNuevoKAD.Visible = false;
            pNuevoKAD.SendToBack();
        }

        private void pNuevoKAD_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pNuevoKAD.Left = pNuevoKAD.Left + (e.X - xClick2);
                pNuevoKAD.Top = pNuevoKAD.Top + (e.Y - yClick2);
            }
        }

        private void txtNuevaPlaca3_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlacas3, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtNuevaPlaca3.Text), true, false, false);
            lstPlacas3.Columns[0].Width = 0;
            lstPlacas3.Columns[1].Width = 80;
            lstPlacas3.Columns[2].Width = 100;
            lstPlacas3.Columns[3].Width = 0;
            lstPlacas3.Columns[4].Width = 130;
            lstPlacas3.Columns[5].Width = 0;
            lstPlacas3.BringToFront();
            lstPlacas3.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo3 = -1;
                txtOperacion3.Clear();
                lstPlacas3.Visible = false;
                lstPlacas3.SendToBack();
            }
        }

        private void txtNuevaPlaca3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlacas3.Focus(); }
        }

        private void lstPlacas3_Enter(object sender, EventArgs e)
        {
            if (!lstPlacas3.Items.Count.Equals(0)) { lstPlacas3.Items[0].Selected = true; }
        }

        private void lstPlacas3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlacas3.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlacas3.SelectedItems[0];

                idVehiculo3 = Int32.Parse(ItemActual.Text);
                txtNuevaPlaca3.Text = ItemActual.SubItems[1].Text;
                txtOperacion3.Text = ItemActual.SubItems[4].Text;
                txtObservacion.Focus();

                lstPlacas3.Visible = false;
                lstPlacas3.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo3 = -1;
                txtOperacion3.Clear();
                lstPlacas3.Visible = false;
                lstPlacas3.SendToBack();
            }
        }

        private void lstPlacas3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlacas3.SelectedItems[0];

            idVehiculo3 = Int32.Parse(ItemActual.Text);
            txtNuevaPlaca3.Text = ItemActual.SubItems[1].Text;
            txtOperacion3.Text = ItemActual.SubItems[4].Text;
            txtObservacion.Focus();

            lstPlacas3.Visible = false;
            lstPlacas3.SendToBack();
        }

        private void rbSiKAD_Click(object sender, EventArgs e)
        {
            if (rbSiKAD.Checked == true) { TieneKAD = "SÍ"; }
        }

        private void rbNoKAD_Click(object sender, EventArgs e)
        {
            if (rbNoKAD.Checked == true) { TieneKAD = "NO"; }
        }

        private void btnBuscarKN_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK)
                {
                    byteArrayImagen = File.ReadAllBytes(getImage.FileName);

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayImagen));
                    pbHallazgo.Image = x;
                    pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrarIK_Click(object sender, EventArgs e)
        {
            byteArrayImagen = null;
            pbHallazgo.Image = null;
            pbHallazgo.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnCancelar3_Click(object sender, EventArgs e)
        {
            txtNuevaPlaca3.Clear();
            idVehiculo3 = -1;
            txtOperacion3.Clear();
            txtObservacion.Clear();
            rbSiKAD.Checked = true;
            rbSiKAD_Click(sender, e);
            btnCerrarIK_Click(sender, e);
        }

        private void btnGuardar3_Click(object sender, EventArgs e)
        {
            if (txtNuevaPlaca3.Text.Length == 0 || txtOperacion3.Text.Length == 0)
            {
                if (txtNuevaPlaca3.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese correctamente la placa.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNuevaPlaca3.Focus();
                }
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarKAD(Opcion2, idKAD, idVehiculo3, TieneKAD, txtObservacion.Text, byteArrayImagen, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCerrar2_Click(sender, e);
                        ListarKitAntiderrame();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch { MessageBox.Show("No se pudo asignar el kit antiderrame.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void txtPlaca3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitAntiderrame(); }
        }

        private void cbxOperaciones3_DropDownClosed(object sender, EventArgs e) { ListarKitAntiderrame(); }

        private void cbxKitAsignado_DropDownClosed(object sender, EventArgs e) { ListarKitAntiderrame(); }

        private void btnBuscar4_Click(object sender, EventArgs e) { ListarKitAntiderrame(); }

        private void btnExcel4_Click(object sender, EventArgs e)
        {
            if (dtgAntiDerrame.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Kits Anti-derrame de Unidades - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgAntiDerrame.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvAntiDerrameVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "SÍ") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "NO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void dtgAntiDerrame_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idKAD = Convert.ToString(dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "NRO"));

                if (idKAD != "")
                {
                    if (e4 == 1)
                    {
                        tsModificarEstado.Enabled = true;
                        tsEliminarKAD.Enabled = true;
                    }
                }
                else
                {
                    tsModificarEstado.Enabled = false;
                    tsEliminarKAD.Enabled = false;
                }
            }
            catch
            {
                tsModificarEstado.Enabled = false;
                tsEliminarKAD.Enabled = false;
            }
        }

        private void tsModificarEstado_Click(object sender, EventArgs e)
        {
            Opcion2 = 2;
            btnCancelar3.Enabled = false;

            idVehiculo3 = Convert.ToInt32(dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "IdTracto"));
            idKAD = Convert.ToInt32(dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "NRO"));
            txtNuevaPlaca3.ReadOnly = true;
            txtNuevaPlaca3.Text = Convert.ToString(dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "UNIDAD"));
            txtOperacion3.Text = Convert.ToString(dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "OPERACION"));
            txtObservacion.Text = Convert.ToString(dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "OBSERVACION"));

            if (dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "Imagen").ToString() != "")
            {
                Byte[] byteBLOBData;
                byteBLOBData = (Byte[])dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "Imagen");
                byteArrayImagen = (Byte[])dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "Imagen");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                pbHallazgo.Image = x;
                pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            string EstadoKAD = Convert.ToString(dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "ESTADO"));
            if (EstadoKAD == "SÍ")
            {
                rbSiKAD.Checked = true;
                rbSiKAD_Click(sender, e);
            }
            else
            {
                rbNoKAD.Checked = true;
                rbNoKAD_Click(sender, e);
            }

            pNuevoKAD.Location = new System.Drawing.Point(654, 202); 
            pNuevoKAD.Visible = true;
            pNuevoKAD.BringToFront();
        }

        private void tsEliminarKAD_Click(object sender, EventArgs e)
        {
            try
            {
                int idTracto = Convert.ToInt32(dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "IdTracto"));
                int idKAD = Convert.ToInt32(dgvAntiDerrameVista.GetRowCellValue(dgvAntiDerrameVista.FocusedRowHandle, "NRO"));

                if (MessageBox.Show("¿Desea desvincular este kit antiderrame de la unidad?", "DESVINCULAR KIT ANTIDERRAME", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarKAD(3, idKAD, idTracto, "", "", byteArrayImagen, "");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarKitAntiderrame(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el kit antiderrame.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnNuevoTanque_Click(object sender, EventArgs e)
        {
            frmNuevaRevision frmNuevaRevision = new frmNuevaRevision();
            frmNuevaRevision.frmListaControlItems = this;
            frmNuevaRevision.dtpFechaRevision.Value = DateTime.Now;
            frmNuevaRevision.dtpFechaReparacion.Value = DateTime.Now;
            frmNuevaRevision.CargarComboOperacion();
            frmNuevaRevision.CargarComboObservacion();
            frmNuevaRevision.cbxOperaciones.Text = "LINDLEY";
            frmNuevaRevision.cbxObservacion.Text = "OK";
            frmNuevaRevision.groupBox1.Enabled = false;
            frmNuevaRevision.Opcion = 1;
            frmNuevaRevision.idTC = 0;
            frmNuevaRevision.Show(this);
        }

        private void txtPlaca4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTanquesCombustible(); }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTanquesCombustible(); }
        }

        private void cbxOperaciones4_DropDownClosed(object sender, EventArgs e) { ListarTanquesCombustible(); }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTanquesCombustible(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTanquesCombustible(); }
        }

        private void cbxEstadoTC_DropDownClosed(object sender, EventArgs e) { ListarTanquesCombustible(); }

        private void btnBuscar5_Click(object sender, EventArgs e) { ListarTanquesCombustible(); }

        private void btnExcel5_Click(object sender, EventArgs e)
        {
            if (dtgTanqueC.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Tanques de Combustible - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgTanqueC.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvTanqueC_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "REPARADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "REVISADO") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }
            }
        }

        private void dtgTanqueC_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmNuevaRevision frmNuevaRevision = new frmNuevaRevision();
                frmNuevaRevision.groupBox6.Enabled = false;
                frmNuevaRevision.groupBox1.Enabled = false;
                frmNuevaRevision.btnCancelar.Enabled = false;
                frmNuevaRevision.btnAgregar.Enabled = false;

                /*
                frmNuevaRevision.dtpFechaRevision.Enabled = false;
                frmNuevaRevision.cbxOperaciones.Enabled = false;
                frmNuevaRevision.txtTracto.ReadOnly = true;
                frmNuevaRevision.txtCarreta.ReadOnly = true;
                frmNuevaRevision.txtConductor.ReadOnly = true;
                frmNuevaRevision.txtLugarInspeccion.ReadOnly = true;
                frmNuevaRevision.txtInspector.ReadOnly = true;
                frmNuevaRevision.btnBuscarHallazgo.Enabled = false;
                frmNuevaRevision.btnCerrar.Enabled = false;
                frmNuevaRevision.txtTecnico.ReadOnly = true;
                frmNuevaRevision.txtResponsable.ReadOnly = true;
                frmNuevaRevision.dtpFechaReparacion.Enabled = false;
                frmNuevaRevision.btnBuscarReparacion.Enabled = false;
                frmNuevaRevision.btnCerrar2.Enabled = false;
                */

                frmNuevaRevision.CargarComboOperacion();
                frmNuevaRevision.CargarComboObservacion();
                frmNuevaRevision.cbxOperaciones.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "OPERACION"));
                frmNuevaRevision.dtpFechaRevision.Value = Convert.ToDateTime(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "FECHA_REVISION"));
                frmNuevaRevision.txtTracto.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "TRACTO"));
                frmNuevaRevision.txtCarreta.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "CARRETA"));
                frmNuevaRevision.txtConductor.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "CONDUCTOR"));
                frmNuevaRevision.txtLugarInspeccion.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "LUGAR_INSPECCION"));
                frmNuevaRevision.txtInspector.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "INSPECTOR"));
                frmNuevaRevision.cbxObservacion.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "OBSERVACION"));

                if (dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmNuevaRevision.pbHallazgo.Image = x;
                    frmNuevaRevision.pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo2").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo2");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmNuevaRevision.pbHallazgo2.Image = x;
                    frmNuevaRevision.pbHallazgo2.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                frmNuevaRevision.txtTecnico.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "TECNICO"));
                frmNuevaRevision.txtResponsable.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "SEGUIMIENTO"));
                if (Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "FECHA_REPARACION")) != "")
                { frmNuevaRevision.dtpFechaReparacion.Value = Convert.ToDateTime(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "FECHA_REPARACION")); }
                else { frmNuevaRevision.dtpFechaReparacion.Value = DateTime.Now; }

                if (dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenReparacion").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenReparacion");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmNuevaRevision.pbReparacion.Image = x;
                    frmNuevaRevision.pbReparacion.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                frmNuevaRevision.Show(this);
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void dtgTanqueC_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idTC = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ITEM"));
                string Estado = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ESTADO"));

                if (idTC != "")
                {
                    if (Estado == "REVISADO")
                    {
                        if (e5 == 1) { tsEditarRevision.Enabled = true; }
                        if (e5 == 1 || e7 == 1) { tsAnadirReparacion.Enabled = true; }
                        if (e5 == 1) { tsEliminarRevision.Enabled = true; }
                    }

                    if (Estado == "REPARADO")
                    {
                        tsEditarRevision.Enabled = false;
                        if (e5 == 1 || e7 == 1) { tsAnadirReparacion.Enabled = true; }
                        tsEliminarRevision.Enabled = false;
                    }
                }
                else
                {
                    tsEditarRevision.Enabled = false;
                    tsAnadirReparacion.Enabled = false;
                    tsEliminarRevision.Enabled = false;
                }
            }
            catch
            {
                tsEditarRevision.Enabled = false;
                tsAnadirReparacion.Enabled = false;
                tsEliminarRevision.Enabled = false;
            }
        }

        private void tsEditarRevision_Click(object sender, EventArgs e)
        {
            frmNuevaRevision frmNuevaRevision = new frmNuevaRevision();
            frmNuevaRevision.frmListaControlItems = this;

            frmNuevaRevision.groupBox1.Enabled = false;
            frmNuevaRevision.dtpFechaReparacion.Value = DateTime.Now;
            frmNuevaRevision.btnCancelar.Enabled = false;

            frmNuevaRevision.CargarComboOperacion();
            frmNuevaRevision.CargarComboObservacion();
            frmNuevaRevision.idTC = Convert.ToInt32(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ITEM"));
            frmNuevaRevision.cbxOperaciones.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "OPERACION"));
            frmNuevaRevision.dtpFechaRevision.Value = Convert.ToDateTime(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "FECHA_REVISION"));
            frmNuevaRevision.idTracto = Convert.ToInt32(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "idTracto"));
            frmNuevaRevision.txtTracto.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "TRACTO"));
            frmNuevaRevision.idCarreta = Convert.ToInt32(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "idCarreta"));
            frmNuevaRevision.txtCarreta.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "CARRETA"));
            frmNuevaRevision.Conductor = Convert.ToInt32(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "PersonaConductor"));
            frmNuevaRevision.txtConductor.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "CONDUCTOR"));
            frmNuevaRevision.txtLugarInspeccion.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "LUGAR_INSPECCION"));
            frmNuevaRevision.Inspector = Convert.ToInt32(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "PersonaInspector"));
            frmNuevaRevision.txtInspector.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "INSPECTOR"));
            frmNuevaRevision.cbxObservacion.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "OBSERVACION"));

            if (dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo").ToString() != "")
            {
                Byte[] byteBLOBData;
                byteBLOBData = (Byte[])dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo");
                frmNuevaRevision.byteArrayImagen = (Byte[])dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                frmNuevaRevision.pbHallazgo.Image = x;
                frmNuevaRevision.pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            if (dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo2").ToString() != "")
            {
                Byte[] byteBLOBData;
                byteBLOBData = (Byte[])dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo2");
                frmNuevaRevision.byteArrayImagen3 = (Byte[])dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo2");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                frmNuevaRevision.pbHallazgo2.Image = x;
                frmNuevaRevision.pbHallazgo2.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            frmNuevaRevision.Opcion = 2;
            frmNuevaRevision.Show(this);
        }

        private void tsAnadirReparacion_Click(object sender, EventArgs e)
        {
            try
            {
                frmNuevaRevision frmNuevaRevision = new frmNuevaRevision();
                frmNuevaRevision.frmListaControlItems = this;
                frmNuevaRevision.Opcion = 3;
                frmNuevaRevision.groupBox6.Enabled = false;

                /*
                frmNuevaRevision.dtpFechaRevision.Enabled = false;
                frmNuevaRevision.cbxOperaciones.Enabled = false;
                frmNuevaRevision.txtTracto.ReadOnly = true;
                frmNuevaRevision.txtCarreta.ReadOnly = true;
                frmNuevaRevision.txtConductor.ReadOnly = true;
                frmNuevaRevision.txtLugarInspeccion.ReadOnly = true;
                frmNuevaRevision.txtInspector.ReadOnly = true;
                frmNuevaRevision.btnBuscarHallazgo.Enabled = false;
                frmNuevaRevision.btnCerrar.Enabled = false;
                */

                frmNuevaRevision.CargarComboOperacion();
                frmNuevaRevision.CargarComboObservacion();
                frmNuevaRevision.idTC = Convert.ToInt32(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ITEM"));
                frmNuevaRevision.cbxOperaciones.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "OPERACION"));
                frmNuevaRevision.dtpFechaRevision.Value = Convert.ToDateTime(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "FECHA_REVISION"));
                frmNuevaRevision.txtTracto.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "TRACTO"));
                frmNuevaRevision.txtCarreta.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "CARRETA"));
                frmNuevaRevision.txtConductor.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "CONDUCTOR"));
                frmNuevaRevision.txtLugarInspeccion.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "LUGAR_INSPECCION"));
                frmNuevaRevision.txtInspector.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "INSPECTOR"));
                frmNuevaRevision.cbxObservacion.Text = Convert.ToString(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "OBSERVACION"));

                if (dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmNuevaRevision.pbHallazgo.Image = x;
                    frmNuevaRevision.pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo2").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ImagenHallazgo2");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmNuevaRevision.pbHallazgo2.Image = x;
                    frmNuevaRevision.pbHallazgo2.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                frmNuevaRevision.dtpFechaReparacion.Value = DateTime.Now;
                frmNuevaRevision.Show(this);
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void tsEliminarRevision_Click(object sender, EventArgs e)
        {
            try
            {
                int idTC = Convert.ToInt32(dgvTanqueC.GetRowCellValue(dgvTanqueC.FocusedRowHandle, "ITEM"));

                if (MessageBox.Show("¿Desea eliminar esta revisión?", "ELIMINAR REVISIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarEditarTanques(3, idTC, "", DateTime.Now, 0, 0, 0, "", 0, "", null, null, Utilitario.Instancia.SesionUsuario.usuario);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarTanquesCombustible(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el tanque de combustible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnNuevoAsiento_Click(object sender, EventArgs e)
        {
            frmNuevoTimon frmNuevoTimon = new frmNuevoTimon();
            frmNuevoTimon.frmListaControlItems = this;
            frmNuevoTimon.dtpFechaRevision.Value = DateTime.Now;
            frmNuevoTimon.dtpFechaReparacion.Value = DateTime.Now;
            frmNuevoTimon.CargarComboOperacion();
            frmNuevoTimon.cbxTipo.Text = "TIMONES";
            frmNuevoTimon.cbxTipo_DropDownClosed(sender, e);
            frmNuevoTimon.cbxOperaciones.Text = "LINDLEY";
            frmNuevoTimon.cbxTapizado.Text = "SÍ";
            frmNuevoTimon.cbxReclinable.Text = "NO";
            frmNuevoTimon.cbxCorredizo.Text = "NO";
            frmNuevoTimon.cbxRadio.Text = "OPERATIVA";
            frmNuevoTimon.groupBox1.Enabled = false;
            frmNuevoTimon.Opcion = 1;
            frmNuevoTimon.idTA = 0;
            frmNuevoTimon.Show(this);
        }

        private void rbTodos_Click(object sender, EventArgs e)
        {
            TipoAT = "TODOS";
            rbTodos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            rbTimones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbAsientos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbColchones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            ListarTimonesAsientos();
        }

        private void rbTimones_Click(object sender, EventArgs e)
        {
            TipoAT = "TIMONES";
            rbTodos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbTimones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            rbAsientos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbColchones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            ListarTimonesAsientos();
        }

        private void rbAsientos_Click(object sender, EventArgs e)
        {
            TipoAT = "ASIENTOS";
            rbTodos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbTimones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbAsientos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            rbColchones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            ListarTimonesAsientos();
        }

        private void rbColchones_Click(object sender, EventArgs e)
        {
            TipoAT = "COLCHONES";
            rbTodos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbTimones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbAsientos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbColchones.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            ListarTimonesAsientos();
        }

        private void txtPlaca5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTimonesAsientos(); }
        }

        private void txtConductor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTimonesAsientos(); }
        }

        private void cbxOperaciones5_DropDownClosed(object sender, EventArgs e) { ListarTimonesAsientos(); }

        private void dtpFechaInicio2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTimonesAsientos(); }
        }

        private void dtpFechaFin2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTimonesAsientos(); }
        }

        private void cbxEstadoTA_DropDownClosed(object sender, EventArgs e) { ListarTimonesAsientos(); }

        private void cbxInventario_DropDownClosed(object sender, EventArgs e) { ListarTimonesAsientos(); }

        private void btnBuscar6_Click(object sender, EventArgs e) { ListarTimonesAsientos(); }

        private void btnExcel6_Click(object sender, EventArgs e)
        {
            if (dtgTimonAsiento.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Timones y Asientos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgTimonAsiento.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvTimonAsiento_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "REPARADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "REVISADO") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }
            }

            if (cbxInventario.Text == "TIMONES" || cbxInventario.Text == "ASIENTOS")
            {
                if (e.Column.FieldName == "TAPIZADO")
                {
                    if (Convert.ToString(e.CellValue) == "SÍ") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                    if (Convert.ToString(e.CellValue) == "NO")
                    {
                        e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                        e.Appearance.ForeColor = Color.White;
                    }
                }
            }
        }

        private void dtgTimonAsiento_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmNuevoTimon frmNuevoTimon = new frmNuevoTimon();
                frmNuevoTimon.dtpFechaRevision.Enabled = false;
                frmNuevoTimon.cbxOperaciones.Enabled = false;
                frmNuevoTimon.cbxTapizado.Enabled = false;
                frmNuevoTimon.cbxReclinable.Enabled = false;
                frmNuevoTimon.cbxCorredizo.Enabled = false;
                frmNuevoTimon.cbxRadio.Enabled = false;
                frmNuevoTimon.cbxTipo.Enabled = false;
                frmNuevoTimon.txtTracto.ReadOnly = true;
                frmNuevoTimon.txtConductor.ReadOnly = true;
                frmNuevoTimon.txtLugarInspeccion.ReadOnly = true;
                frmNuevoTimon.txtInspector.ReadOnly = true;
                frmNuevoTimon.btnBuscarHallazgo.Enabled = false;
                frmNuevoTimon.btnCerrar.Enabled = false;
                frmNuevoTimon.txtProveedor.ReadOnly = true;
                frmNuevoTimon.txtResponsable.ReadOnly = true;
                frmNuevoTimon.dtpFechaReparacion.Enabled = false;
                frmNuevoTimon.btnBuscarReparacion.Enabled = false;
                frmNuevoTimon.btnCerrar2.Enabled = false;
                frmNuevoTimon.btnCancelar.Enabled = false;
                frmNuevoTimon.btnAgregar.Enabled = false;

                frmNuevoTimon.CargarComboOperacion();
                frmNuevoTimon.cbxTipo.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "TIPO"));
                frmNuevoTimon.cbxTipo_DropDownClosed(sender, e);

                frmNuevoTimon.cbxOperaciones.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "OPERACION"));
                frmNuevoTimon.cbxTapizado.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "TAPIZADO"));
                frmNuevoTimon.cbxReclinable.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "RECLINABLE"));
                frmNuevoTimon.cbxCorredizo.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "CORREDIZO"));
                frmNuevoTimon.cbxRadio.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ESTADO_ITEM")); 
                frmNuevoTimon.dtpFechaRevision.Value = Convert.ToDateTime(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "FECHA_REVISION"));
                frmNuevoTimon.txtTracto.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "TRACTO"));
                frmNuevoTimon.txtConductor.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "CONDUCTOR"));
                frmNuevoTimon.txtLugarInspeccion.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "LUGAR_INSPECCION"));
                frmNuevoTimon.txtInspector.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "INSPECTOR"));

                if (dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ImagenHallazgo").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ImagenHallazgo");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmNuevoTimon.pbHallazgo.Image = x;
                    frmNuevoTimon.pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                frmNuevoTimon.txtProveedor.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "PROVEEDOR"));
                frmNuevoTimon.txtResponsable.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "SEGUIMIENTO"));
                if (Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "FECHA_REPARACION")) != "")
                { frmNuevoTimon.dtpFechaReparacion.Value = Convert.ToDateTime(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "FECHA_REPARACION")); }
                else { frmNuevoTimon.dtpFechaReparacion.Value = DateTime.Now; }

                if (dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ImagenReparacion").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ImagenReparacion");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmNuevoTimon.pbReparacion.Image = x;
                    frmNuevoTimon.pbReparacion.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                frmNuevoTimon.Show(this);
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void dtgTimonAsiento_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idTA = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ITEM"));
                string Estado = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ESTADO"));

                if (idTA != "")
                {
                    if (Estado == "REVISADO")
                    {
                        if (e8 == 1) { tsEditarAsiento.Enabled = true; }
                        if (e8 == 1 || e10 == 1) { tsRepararAsiento.Enabled = true; }
                        if (e8 == 1) { tsEliminarAsiento.Enabled = true; }
                    }

                    if (Estado == "REPARADO")
                    {
                        tsEditarAsiento.Enabled = false;
                        if (e8 == 1 || e10 == 1) { tsRepararAsiento.Enabled = true; }
                        tsEliminarAsiento.Enabled = false;
                    }
                }
                else
                {
                    tsEditarAsiento.Enabled = false;
                    tsRepararAsiento.Enabled = false;
                    tsEliminarAsiento.Enabled = false;
                }
            }
            catch
            {
                tsEditarAsiento.Enabled = false;
                tsRepararAsiento.Enabled = false;
                tsEliminarAsiento.Enabled = false;
            }
        }

        private void tsEditarAsiento_Click(object sender, EventArgs e)
        {
            frmNuevoTimon frmNuevoTimon = new frmNuevoTimon();
            frmNuevoTimon.frmListaControlItems = this;

            frmNuevoTimon.groupBox1.Enabled = false;
            frmNuevoTimon.dtpFechaReparacion.Value = DateTime.Now;

            frmNuevoTimon.CargarComboOperacion();
            frmNuevoTimon.idTA = Convert.ToInt32(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ITEM"));
            frmNuevoTimon.cbxTipo.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "TIPO"));
            frmNuevoTimon.cbxTipo_DropDownClosed(sender, e);

            frmNuevoTimon.cbxOperaciones.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "OPERACION"));
            frmNuevoTimon.cbxTapizado.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "TAPIZADO"));
            frmNuevoTimon.cbxReclinable.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "RECLINABLE"));
            frmNuevoTimon.cbxCorredizo.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "CORREDIZO"));
            frmNuevoTimon.cbxRadio.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ESTADO_ITEM")); 
            frmNuevoTimon.dtpFechaRevision.Value = Convert.ToDateTime(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "FECHA_REVISION"));
            frmNuevoTimon.idTracto = Convert.ToInt32(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "idTracto"));
            frmNuevoTimon.txtTracto.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "TRACTO"));
            frmNuevoTimon.Conductor = Convert.ToInt32(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "PersonaConductor"));
            frmNuevoTimon.txtConductor.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "CONDUCTOR"));
            frmNuevoTimon.txtLugarInspeccion.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "LUGAR_INSPECCION"));
            frmNuevoTimon.Inspector = Convert.ToInt32(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "PersonaInspector"));
            frmNuevoTimon.txtInspector.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "INSPECTOR"));

            if (dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ImagenHallazgo").ToString() != "")
            {
                Byte[] byteBLOBData;
                byteBLOBData = (Byte[])dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ImagenHallazgo");
                frmNuevoTimon.byteArrayImagen = (Byte[])dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ImagenHallazgo");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                frmNuevoTimon.pbHallazgo.Image = x;
                frmNuevoTimon.pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            frmNuevoTimon.Opcion = 2;
            frmNuevoTimon.Show(this);
        }

        private void tsRepararAsiento_Click(object sender, EventArgs e)
        {
            try
            {
                frmNuevoTimon frmNuevoTimon = new frmNuevoTimon();
                frmNuevoTimon.frmListaControlItems = this;
                frmNuevoTimon.Opcion = 3;
                frmNuevoTimon.dtpFechaRevision.Enabled = false;
                frmNuevoTimon.cbxOperaciones.Enabled = false;
                frmNuevoTimon.cbxTapizado.Enabled = false;
                frmNuevoTimon.cbxReclinable.Enabled = false; 
                frmNuevoTimon.cbxCorredizo.Enabled = false;
                frmNuevoTimon.cbxRadio.Enabled = false;
                frmNuevoTimon.cbxTipo.Enabled = false;
                frmNuevoTimon.txtTracto.ReadOnly = true;
                frmNuevoTimon.txtConductor.ReadOnly = true;
                frmNuevoTimon.txtLugarInspeccion.ReadOnly = true;
                frmNuevoTimon.txtInspector.ReadOnly = true;
                frmNuevoTimon.btnBuscarHallazgo.Enabled = false;
                frmNuevoTimon.btnCerrar.Enabled = false;

                frmNuevoTimon.CargarComboOperacion();
                frmNuevoTimon.idTA = Convert.ToInt32(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ITEM"));
                frmNuevoTimon.cbxTipo.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "TIPO"));
                frmNuevoTimon.cbxTipo_DropDownClosed(sender, e);

                frmNuevoTimon.cbxOperaciones.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "OPERACION"));
                frmNuevoTimon.cbxTapizado.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "TAPIZADO"));
                frmNuevoTimon.cbxReclinable.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "RECLINABLE"));
                frmNuevoTimon.cbxCorredizo.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "CORREDIZO"));
                frmNuevoTimon.cbxRadio.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ESTADO_ITEM")); 
                frmNuevoTimon.dtpFechaRevision.Value = Convert.ToDateTime(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "FECHA_REVISION"));
                frmNuevoTimon.txtTracto.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "TRACTO"));
                frmNuevoTimon.txtConductor.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "CONDUCTOR"));
                frmNuevoTimon.txtLugarInspeccion.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "LUGAR_INSPECCION"));
                frmNuevoTimon.txtInspector.Text = Convert.ToString(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "INSPECTOR"));

                if (dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ImagenHallazgo").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ImagenHallazgo");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmNuevoTimon.pbHallazgo.Image = x;
                    frmNuevoTimon.pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                frmNuevoTimon.dtpFechaReparacion.Value = DateTime.Now;
                frmNuevoTimon.Show(this);
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void tsEliminarAsiento_Click(object sender, EventArgs e)
        {
            try
            {
                int idTA = Convert.ToInt32(dgvTimonAsiento.GetRowCellValue(dgvTimonAsiento.FocusedRowHandle, "ITEM"));

                if (MessageBox.Show("¿Desea eliminar esta revisión?", "ELIMINAR REVISIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarEditarAsientos(3, idTA, "", "", DateTime.Now, 0, 0, "", "", "", "","", 0, null, Utilitario.Instancia.SesionUsuario.usuario);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarTimonesAsientos(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar la revisión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsignarVolcan_Click(object sender, EventArgs e)
        {
            frmAsignarKitVolcan frmAsignarKitVolcan = new frmAsignarKitVolcan();
            frmAsignarKitVolcan.frmListaControlItems = this;

            frmAsignarKitVolcan.Opcion = 1;
            frmAsignarKitVolcan.label2.Text = "ASIGNACIÓN DE HERRAMIENTAS - VOLCAN";
            frmAsignarKitVolcan.label2.BackColor = Color.FromArgb(192,255,192);
            frmAsignarKitVolcan.Show(this);
        }

        private void btnDevolverVolcan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea devolver estos implementos?", "DEVOLVER IMPLEMENTOS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int CodTracto, CodCarreta, CodItem, Persona;
                int[] filas = dgvOperacionVolcanVista.GetSelectedRows();

                if (filas.Length != 0)
                {
                    int Correcto = 0;
                    string Respuesta = "0 = Implementos eliminados correctamente.";

                    for (int i = 0; i < filas.Length; i++)
                    {
                        CodTracto = Convert.ToInt32(dgvOperacionVolcanVista.GetRowCellValue(filas[i], "idTracto"));
                        CodCarreta = -1;
                        CodItem = Convert.ToInt32(dgvOperacionVolcanVista.GetRowCellValue(filas[i], "idItemV"));
                        Persona = Convert.ToInt32(dgvOperacionVolcanVista.GetRowCellValue(filas[i], "Persona"));

                        DataTable dtRespuesta = new DataTable();
                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarEliminarItems(2, CodItem, Persona, CodTracto, CodCarreta, Utilitario.Instancia.SesionUsuario.usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA == "0") { Correcto = Correcto + 1; }
                    }

                    if (Correcto == filas.Length) { ListarKitVolcan(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("No ha seleccionado ningún implemento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void txtPlacaV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitVolcan(); }
        }

        private void txtCarretaV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitVolcan(); }
        }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitVolcan(); }
        }

        private void txtItemV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitVolcan(); }
        }

        private void btnBuscarV_Click(object sender, EventArgs e) { ListarKitVolcan(); }

        private void btnExcelV_Click(object sender, EventArgs e)
        {
            if (dtgOperacionVolcan.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Kit de Implementos - Volcan - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgOperacionVolcan.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgOperacionVolcan_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idKitVolcan = Convert.ToString(dgvOperacionVolcanVista.GetRowCellValue(dgvOperacionVolcanVista.FocusedRowHandle, "idKitVolcan"));

                if (idKitVolcan != "")
                {
                    if (e11 == 1) { tsEditarAsignacion.Enabled = true; }
                }
                else { tsEditarAsignacion.Enabled = false; }
            }
            catch { tsEditarAsignacion.Enabled = false; }
        }

        private void tsEditarAsignacion_Click(object sender, EventArgs e)
        {
            OpcionH = "V";
            idKitVolcan = Convert.ToInt32(dgvOperacionVolcanVista.GetRowCellValue(dgvOperacionVolcanVista.FocusedRowHandle, "idKitVolcan"));
            idTractoV = Convert.ToInt32(dgvOperacionVolcanVista.GetRowCellValue(dgvOperacionVolcanVista.FocusedRowHandle, "idTracto"));
            //idCarretaV = Convert.ToInt32(dgvOperacionVolcanVista.GetRowCellValue(dgvOperacionVolcanVista.FocusedRowHandle, "idCarreta"));
            PersonaV = Convert.ToInt32(dgvOperacionVolcanVista.GetRowCellValue(dgvOperacionVolcanVista.FocusedRowHandle, "Persona"));

            txtCodigoInterno.Text = Convert.ToString(dgvOperacionVolcanVista.GetRowCellValue(dgvOperacionVolcanVista.FocusedRowHandle, "COD_INTERNO"));
            txtImplemento.Text = Convert.ToString(dgvOperacionVolcanVista.GetRowCellValue(dgvOperacionVolcanVista.FocusedRowHandle, "IMPLEMENTO"));
            txtEmpleadoVolcan.Text = Convert.ToString(dgvOperacionVolcanVista.GetRowCellValue(dgvOperacionVolcanVista.FocusedRowHandle, "EMPLEADO"));
            txtTractoVolcan.Text = Convert.ToString(dgvOperacionVolcanVista.GetRowCellValue(dgvOperacionVolcanVista.FocusedRowHandle, "TRACTO"));
            //txtCarretaVolcan.Text = Convert.ToString(dgvOperacionVolcanVista.GetRowCellValue(dgvOperacionVolcanVista.FocusedRowHandle, "CARRETA"));

            pEditarAsignacion.Visible = true;
            pEditarAsignacion.BringToFront();
        }

        private void pEditarAsignacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick4 = e.X; yClick4 = e.Y; }
            else
            {
                pEditarAsignacion.Left = pEditarAsignacion.Left + (e.X - xClick4);
                pEditarAsignacion.Top = pEditarAsignacion.Top + (e.Y - yClick4);
            }
        }

        private void btnCerrarV_Click(object sender, EventArgs e)
        {
            idKitVolcan = -1; idKitLimagas = -1; idTractoV = -1; idCarretaV = -1; PersonaV = -1;
            pEditarAsignacion.Location = new System.Drawing.Point(701, 319);

            txtCodigoInterno.Clear();
            txtImplemento.Clear();
            txtEmpleadoVolcan.Clear();
            txtTractoVolcan.Clear();
            txtCarretaVolcan.Clear();

            pEditarAsignacion.Visible = false;
            pEditarAsignacion.SendToBack();
        }

        private void txtEmpleadoVolcan_Enter(object sender, EventArgs e) { txtEmpleadoVolcan.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtEmpleadoVolcan_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleadoV, clsConsultaBL.Instancia.GetEmpleado(txtEmpleadoVolcan.Text), true, false, false);
            lstEmpleadoV.Columns[0].Width = 0;
            lstEmpleadoV.Columns[1].Width = 320;
            lstEmpleadoV.Columns[2].Width = 0;
            lstEmpleadoV.BringToFront();
            lstEmpleadoV.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleadoV.Visible = false;
                lstEmpleadoV.SendToBack();
                PersonaV = -1;
            }
        }

        private void txtEmpleadoVolcan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstEmpleadoV.Focus(); }
        }

        private void txtEmpleadoVolcan_Leave(object sender, EventArgs e) { txtEmpleadoVolcan.BackColor = Color.White; }

        private void lstEmpleadoV_Enter(object sender, EventArgs e)
        {
            if (!lstEmpleadoV.Items.Count.Equals(0)) { lstEmpleadoV.Items[0].Selected = true; }
        }

        private void lstEmpleadoV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstEmpleadoV.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstEmpleadoV.SelectedItems[0];
                PersonaV = Int32.Parse(ItemActual.Text);
                txtEmpleadoVolcan.Text = ItemActual.SubItems[1].Text;

                lstEmpleadoV.Visible = false;
                lstEmpleadoV.SendToBack();
                txtTractoVolcan.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleadoV.Visible = false;
                lstEmpleadoV.SendToBack();
                txtEmpleadoVolcan.Focus();
                PersonaV = -1;
            }
        }

        private void lstEmpleadoV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstEmpleadoV.SelectedItems[0];
            PersonaV = Int32.Parse(ItemActual.Text);
            txtEmpleadoVolcan.Text = ItemActual.SubItems[1].Text;

            lstEmpleadoV.Visible = false;
            lstEmpleadoV.SendToBack();
            txtTractoVolcan.Focus();
        }

        private void txtTractoVolcan_Enter(object sender, EventArgs e)
        {
            TipoVH = 1;
            txtTractoVolcan.BackColor = Color.FromArgb(192, 255, 192);
            lstTractoV.Location = new System.Drawing.Point(85, 153);
        }

        private void txtTractoVolcan_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTractoV, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtTractoVolcan.Text), true, false, false);
            lstTractoV.Columns[0].Width = 0;
            lstTractoV.Columns[1].Width = 80;
            lstTractoV.Columns[2].Width = 0;
            lstTractoV.Columns[3].Width = 0;
            lstTractoV.Columns[4].Width = 0;
            lstTractoV.Columns[5].Width = 0;
            lstTractoV.BringToFront();
            lstTractoV.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idTractoV = -1;
                lstTractoV.Visible = false;
                lstTractoV.SendToBack();
            }
        }

        private void txtTractoVolcan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTractoV.Focus(); }
        }

        private void txtTractoVolcan_Leave(object sender, EventArgs e) { txtTractoVolcan.BackColor = Color.White; }

        private void txtCarretaVolcan_Enter(object sender, EventArgs e)
        {
            TipoVH = 2;
            txtCarretaVolcan.BackColor = Color.FromArgb(192, 255, 192);
            lstTractoV.Location = new System.Drawing.Point(301, 153);
        }

        private void txtCarretaVolcan_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTractoV, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtCarretaVolcan.Text), true, false, false);
            lstTractoV.Columns[0].Width = 0;
            lstTractoV.Columns[1].Width = 80;
            lstTractoV.Columns[2].Width = 0;
            lstTractoV.Columns[3].Width = 0;
            lstTractoV.Columns[4].Width = 0;
            lstTractoV.Columns[5].Width = 0;
            lstTractoV.BringToFront();
            lstTractoV.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idCarretaV = -1;
                lstTractoV.Visible = false;
                lstTractoV.SendToBack();
            }
        }

        private void txtCarretaVolcan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTractoV.Focus(); }
        }

        private void txtCarretaVolcan_Leave(object sender, EventArgs e) { txtCarretaVolcan.BackColor = Color.White; }

        private void lstTractoV_Enter(object sender, EventArgs e)
        {
            if (!lstTractoV.Items.Count.Equals(0)) { lstTractoV.Items[0].Selected = true; }
        }

        private void lstTractoV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTractoV.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTractoV.SelectedItems[0];

                if (TipoVH == 1)
                {
                    idTractoV = Int32.Parse(ItemActual.Text);
                    txtTractoVolcan.Text = ItemActual.SubItems[1].Text;
                }

                if (TipoVH == 2)
                {
                    idCarretaV = Int32.Parse(ItemActual.Text);
                    txtCarretaVolcan.Text = ItemActual.SubItems[1].Text;
                }

                lstTractoV.Visible = false;
                lstTractoV.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                if (TipoVH == 1) { idTractoV = -1; }
                if (TipoVH == 2) { idCarretaV = -1; }

                lstTractoV.Visible = false;
                lstTractoV.SendToBack();
            }
        }

        private void lstTractoV_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTractoV.SelectedItems[0];

            if (TipoVH == 1)
            {
                idTractoV = Int32.Parse(ItemActual.Text);
                txtTractoVolcan.Text = ItemActual.SubItems[1].Text;
            }

            if (TipoVH == 2)
            {
                idCarretaV = Int32.Parse(ItemActual.Text);
                txtCarretaVolcan.Text = ItemActual.SubItems[1].Text;
            }

            lstTractoV.Visible = false;
            lstTractoV.SendToBack();
        }

        private void btnGuardarVolcan_Click(object sender, EventArgs e)
        {
            if (txtEmpleadoVolcan.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese un empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmpleado.Focus();

                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (OpcionH == "V")
                { dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarEliminarItems(3, idKitVolcan, PersonaV, idTractoV, idCarretaV, Utilitario.Instancia.SesionUsuario.usuario); }
                if (OpcionH == "L")
                { dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarEliminarItems(3, idKitLimagas, PersonaV, idTractoV, idCarretaV, Utilitario.Instancia.SesionUsuario.usuario); }

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (OpcionH == "V") { ListarKitVolcan(); }
                    if (OpcionH == "L") { ListarKitLimagas(); }
                    
                    btnCerrarV_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnNuevoCT_Click(object sender, EventArgs e)
        {
            frmRevisionConosTacos frmRevisionConosTacos = new frmRevisionConosTacos();
            frmRevisionConosTacos.frmListaControlItems = this;
            frmRevisionConosTacos.dtpFechaRevision.Value = DateTime.Now;
            frmRevisionConosTacos.dtpFechaReparacion.Value = DateTime.Now;
            frmRevisionConosTacos.cbxTipo.Text = "CONOS";
            frmRevisionConosTacos.cbxTipo_DropDownClosed(sender, e);
            frmRevisionConosTacos.cbxEstado.Text = "OK";
            frmRevisionConosTacos.Opcion = 1;
            frmRevisionConosTacos.idCT = 0;

            frmRevisionConosTacos.dtpFechaReparacion.Enabled = false;
            frmRevisionConosTacos.btnBuscarReparacion.Enabled = false;
            frmRevisionConosTacos.btnCerrar2.Enabled = false;

            frmRevisionConosTacos.Show(this);
        }

        private void dtpFechaInicioCT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConosTacos(); }
        }

        private void dtpFechaFinCT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConosTacos(); }
        }

        private void txtConductorCT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConosTacos(); }
        }

        private void txtPlacaCT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConosTacos(); }
        }

        private void cbxTipoItemCT_DropDownClosed(object sender, EventArgs e) { ListarConosTacos(); }

        private void cbxEstadoCT_DropDownClosed(object sender, EventArgs e) { ListarConosTacos(); }

        private void btnBuscarCT_Click(object sender, EventArgs e) { ListarConosTacos(); }

        private void btnExcelCT_Click(object sender, EventArgs e)
        {
            if (dtgConosTacos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Conos y Tacos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgConosTacos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgConosTacos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idCT = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "NRO"));
                string Estado = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ESTADO"));

                if (idCT != "")
                {
                    if (Estado == "PENDIENTE")
                    {
                        if (e12 == 1)
                        {
                            tsEditarRevisionCT.Enabled = true;
                            tsAniadirReparacion.Enabled = true;
                            tsQuitarRevision.Enabled = true;
                        }
                    }

                    if (Estado == "REVISADO")
                    {
                        tsEditarRevisionCT.Enabled = false;
                        tsAniadirReparacion.Enabled = false;
                        tsQuitarRevision.Enabled = false;
                    }
                }
                else
                {
                    tsEditarRevisionCT.Enabled = false;
                    tsAniadirReparacion.Enabled = false;
                    tsQuitarRevision.Enabled = false;
                }
            }
            catch
            {
                tsEditarRevisionCT.Enabled = false;
                tsAniadirReparacion.Enabled = false;
                tsQuitarRevision.Enabled = false;
            }
        }

        private void dgvConosTacosView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "REVISADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }
            }
        }

        private void dtgConosTacos_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmRevisionConosTacos frmRevisionConosTacos = new frmRevisionConosTacos();
                frmRevisionConosTacos.txtTracto.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "TRACTO"));
                frmRevisionConosTacos.txtOperacion.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "OPERACION"));
                frmRevisionConosTacos.txtLugarRevision.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "LUGAR_REVISION"));
                frmRevisionConosTacos.cbxTipo.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "TIPO"));
                frmRevisionConosTacos.txtCantidad.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "CANTIDAD"));
                frmRevisionConosTacos.cbxEstado.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ESTADO_ITEMS"));
                frmRevisionConosTacos.dtpFechaRevision.Value = Convert.ToDateTime(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "FECHA_REVISION"));
                frmRevisionConosTacos.txtResponsable.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "RESPONSABLE"));
                if (Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "FECHA_REPARACION")) != "")
                { frmRevisionConosTacos.dtpFechaReparacion.Value = Convert.ToDateTime(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "FECHA_REPARACION")); }
                else { frmRevisionConosTacos.dtpFechaReparacion.Value = DateTime.Now; }

                if (dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenHallazgo").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenHallazgo");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmRevisionConosTacos.byteArrayImagen = (Byte[])dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenHallazgo");
                    frmRevisionConosTacos.pbHallazgo.Image = x;
                    frmRevisionConosTacos.pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenReparacion").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenReparacion");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmRevisionConosTacos.byteArrayImagen2 = (Byte[])dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenReparacion");
                    frmRevisionConosTacos.pbReparacion.Image = x;
                    frmRevisionConosTacos.pbReparacion.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                frmRevisionConosTacos.txtTracto.ReadOnly = true;
                frmRevisionConosTacos.txtLugarRevision.ReadOnly = true;
                frmRevisionConosTacos.cbxTipo.Enabled = false;
                frmRevisionConosTacos.txtCantidad.ReadOnly = true;
                frmRevisionConosTacos.cbxEstado.Enabled = false;
                frmRevisionConosTacos.dtpFechaRevision.Enabled = false;
                frmRevisionConosTacos.txtResponsable.ReadOnly = true;
                frmRevisionConosTacos.dtpFechaReparacion.Enabled = false;
                frmRevisionConosTacos.btnBuscarHallazgo.Enabled = false;
                frmRevisionConosTacos.btnBuscarReparacion.Enabled = false;
                frmRevisionConosTacos.btnCerrar.Enabled = false;
                frmRevisionConosTacos.btnCerrar2.Enabled = false;
                frmRevisionConosTacos.btnCancelar.Enabled = false;
                frmRevisionConosTacos.btnAgregar.Enabled = false;
                frmRevisionConosTacos.Show(this);
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void tsEditarRevisionCT_Click(object sender, EventArgs e)
        {
            try
            {
                frmRevisionConosTacos frmRevisionConosTacos = new frmRevisionConosTacos();
                frmRevisionConosTacos.frmListaControlItems = this;
                frmRevisionConosTacos.Opcion = 2;
                frmRevisionConosTacos.idCT = Convert.ToInt32(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "NRO"));
                frmRevisionConosTacos.idTracto = Convert.ToInt32(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "idTracto"));
                frmRevisionConosTacos.txtTracto.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "TRACTO"));
                frmRevisionConosTacos.txtOperacion.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "OPERACION"));
                frmRevisionConosTacos.txtLugarRevision.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "LUGAR_REVISION"));
                frmRevisionConosTacos.cbxTipo.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "TIPO"));
                frmRevisionConosTacos.txtCantidad.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "CANTIDAD"));
                frmRevisionConosTacos.cbxEstado.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ESTADO_ITEMS"));
                frmRevisionConosTacos.dtpFechaRevision.Value = Convert.ToDateTime(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "FECHA_REVISION"));
                frmRevisionConosTacos.Responsable = Convert.ToInt32(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "PersonaR"));
                frmRevisionConosTacos.txtResponsable.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "RESPONSABLE"));
                frmRevisionConosTacos.dtpFechaReparacion.Value = DateTime.Now;

                if (dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenHallazgo").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenHallazgo");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmRevisionConosTacos.byteArrayImagen = (Byte[])dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenHallazgo");
                    frmRevisionConosTacos.pbHallazgo.Image = x;
                    frmRevisionConosTacos.pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                frmRevisionConosTacos.dtpFechaReparacion.Enabled = false;
                frmRevisionConosTacos.btnBuscarReparacion.Enabled = false;
                frmRevisionConosTacos.btnCerrar2.Enabled = false;
                frmRevisionConosTacos.Show(this);
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void tsAniadirReparacion_Click(object sender, EventArgs e)
        {
            try
            {
                frmRevisionConosTacos frmRevisionConosTacos = new frmRevisionConosTacos();
                frmRevisionConosTacos.frmListaControlItems = this;
                frmRevisionConosTacos.Opcion = 3;
                frmRevisionConosTacos.idCT = Convert.ToInt32(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "NRO"));
                frmRevisionConosTacos.txtTracto.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "TRACTO"));
                frmRevisionConosTacos.txtOperacion.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "OPERACION"));
                frmRevisionConosTacos.txtLugarRevision.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "LUGAR_REVISION"));
                frmRevisionConosTacos.cbxTipo.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "TIPO"));
                frmRevisionConosTacos.txtCantidad.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "CANTIDAD"));
                frmRevisionConosTacos.cbxEstado.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ESTADO_ITEMS"));
                frmRevisionConosTacos.dtpFechaRevision.Value = Convert.ToDateTime(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "FECHA_REVISION"));
                frmRevisionConosTacos.txtResponsable.Text = Convert.ToString(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "RESPONSABLE"));
                frmRevisionConosTacos.dtpFechaReparacion.Value = DateTime.Now;

                if (dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenHallazgo").ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenHallazgo");
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmRevisionConosTacos.byteArrayImagen = (Byte[])dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "ImagenHallazgo");
                    frmRevisionConosTacos.pbHallazgo.Image = x;
                    frmRevisionConosTacos.pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                frmRevisionConosTacos.txtTracto.ReadOnly = true;
                frmRevisionConosTacos.txtLugarRevision.ReadOnly = true;
                frmRevisionConosTacos.cbxTipo.Enabled = false;
                frmRevisionConosTacos.txtCantidad.ReadOnly = true;
                frmRevisionConosTacos.cbxEstado.Enabled = false;
                frmRevisionConosTacos.dtpFechaRevision.Enabled = false;
                frmRevisionConosTacos.txtResponsable.ReadOnly = true;
                frmRevisionConosTacos.btnBuscarHallazgo.Enabled = false;
                frmRevisionConosTacos.btnCerrar.Enabled = false;
                frmRevisionConosTacos.btnCancelar.Enabled = false;
                frmRevisionConosTacos.Show(this);
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void tsQuitarRevision_Click(object sender, EventArgs e)
        {
            try
            {
                int idCT = Convert.ToInt32(dgvConosTacosView.GetRowCellValue(dgvConosTacosView.FocusedRowHandle, "NRO"));

                if (MessageBox.Show("¿Desea quitar esta revisión?", "QUITAR REVISIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarEditarCT(3, idCT, 0, DateTime.Now,"", 0, "", "", 0, null,"");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarConosTacos(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar la revisión.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsignarLimagas_Click(object sender, EventArgs e)
        {
            frmAsignarKitVolcan frmAsignarKitVolcan = new frmAsignarKitVolcan();
            frmAsignarKitVolcan.frmListaControlItems = this;

            frmAsignarKitVolcan.Opcion = 2;
            frmAsignarKitVolcan.label2.Text = "ASIGNACIÓN DE HERRAMIENTAS";
            frmAsignarKitVolcan.label2.BackColor = Color.FromArgb(255,255,192); 
            frmAsignarKitVolcan.Show(this);
        }

        private void btnDevolverLimagas_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea devolver estos implementos?", "DEVOLVER IMPLEMENTOS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int CodTracto, CodCarreta, CodItem, Persona;
                int[] filas = dgvOperacionLimagasVista.GetSelectedRows();

                if (filas.Length != 0)
                {
                    int Correcto = 0;
                    string Respuesta = "0 = Implementos eliminados correctamente.";

                    for (int i = 0; i < filas.Length; i++)
                    {
                        CodTracto = Convert.ToInt32(dgvOperacionLimagasVista.GetRowCellValue(filas[i], "idTracto"));
                        CodCarreta = -1;
                        CodItem = Convert.ToInt32(dgvOperacionLimagasVista.GetRowCellValue(filas[i], "idItemL"));
                        Persona = Convert.ToInt32(dgvOperacionLimagasVista.GetRowCellValue(filas[i], "Persona"));

                        DataTable dtRespuesta = new DataTable();
                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarEliminarItems(2, CodItem, Persona, CodTracto, CodCarreta, Utilitario.Instancia.SesionUsuario.usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA == "0") { Correcto = Correcto + 1; }
                    }

                    if (Correcto == filas.Length) { ListarKitLimagas(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("No ha seleccionado ningún implemento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void txtPlacaL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitLimagas(); }
        }

        private void txtCarretaL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitLimagas(); }
        }

        private void txtEmpleadoL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitLimagas(); }
        }

        private void txtItemL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarKitLimagas(); }
        }

        private void btnBuscarL_Click(object sender, EventArgs e) { ListarKitLimagas(); }

        private void btnExcelL_Click(object sender, EventArgs e)
        {
            if (dtgOperacionLimagas.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Kit de Implementos - Limagas - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgOperacionLimagas.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgOperacionLimagas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idKitLimagas = Convert.ToString(dgvOperacionLimagasVista.GetRowCellValue(dgvOperacionLimagasVista.FocusedRowHandle, "idKitLimagas"));

                if (idKitLimagas != "") { tsEditarAsignacion2.Enabled = true; }
                else { tsEditarAsignacion2.Enabled = false; }
            }
            catch { tsEditarAsignacion2.Enabled = false; }
        }

        private void tsEditarAsignacion2_Click(object sender, EventArgs e)
        {
            OpcionH = "L";
            idKitLimagas = Convert.ToInt32(dgvOperacionLimagasVista.GetRowCellValue(dgvOperacionLimagasVista.FocusedRowHandle, "idKitLimagas"));
            idTractoV = Convert.ToInt32(dgvOperacionLimagasVista.GetRowCellValue(dgvOperacionLimagasVista.FocusedRowHandle, "idTracto"));
            //idCarretaV = Convert.ToInt32(dgvOperacionLimagasVista.GetRowCellValue(dgvOperacionLimagasVista.FocusedRowHandle, "idCarreta"));
            PersonaV = Convert.ToInt32(dgvOperacionLimagasVista.GetRowCellValue(dgvOperacionLimagasVista.FocusedRowHandle, "Persona"));

            txtCodigoInterno.Text = Convert.ToString(dgvOperacionLimagasVista.GetRowCellValue(dgvOperacionLimagasVista.FocusedRowHandle, "COD_INTERNO"));
            txtImplemento.Text = Convert.ToString(dgvOperacionLimagasVista.GetRowCellValue(dgvOperacionLimagasVista.FocusedRowHandle, "IMPLEMENTO"));
            txtEmpleadoVolcan.Text = Convert.ToString(dgvOperacionLimagasVista.GetRowCellValue(dgvOperacionLimagasVista.FocusedRowHandle, "EMPLEADO"));
            txtTractoVolcan.Text = Convert.ToString(dgvOperacionLimagasVista.GetRowCellValue(dgvOperacionLimagasVista.FocusedRowHandle, "TRACTO"));
            //txtCarretaVolcan.Text = Convert.ToString(dgvOperacionLimagasVista.GetRowCellValue(dgvOperacionLimagasVista.FocusedRowHandle, "CARRETA"));

            pEditarAsignacion.Visible = true;
            pEditarAsignacion.BringToFront();
        }

        private void btnRegistrarCortinera_Click(object sender, EventArgs e)
        {
            OpcionC = 1;
            idCortinera = 0;

            btnCancelarC.Enabled = true;
            txtNuevaPlacaC.Enabled = true;

            if (OpcionCS == "TECLES")
            {
                rbNuevoSi.Checked = true;
                rbNuevoSi_Click(sender, e);
                panel12.Visible = true;
                panel12.BringToFront();
            }

            if (OpcionCS == "SOGAS" || OpcionCS == "CÁMARAS")
            {
                panel12.SendToBack();
                panel12.Visible = false;
                if (OpcionCS == "SOGAS") label79.Text = "Soga:"; else label79.Text = "Cámara:";
                cbxSogas.Text = "OPERATIVO";
            }

            pNuevaCortinera.Location = new System.Drawing.Point(654, 202);
            pNuevaCortinera.Visible = true;
            pNuevaCortinera.BringToFront();
        }

        private void btnCerrarC2_Click(object sender, EventArgs e)
        {
            btnCancelarC_Click(sender, e);
            txtNuevaPlacaC.Enabled = true;
            pNuevaCortinera.Visible = false;
            pNuevaCortinera.SendToBack();
        }

        private void pNuevaCortinera_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick6 = e.X; yClick6 = e.Y; }
            else
            {
                pNuevaCortinera.Left = pNuevaCortinera.Left + (e.X - xClick6);
                pNuevaCortinera.Top = pNuevaCortinera.Top + (e.Y - yClick6);
            }
        }

        private void txtNuevaPlacaC_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlacaC, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtNuevaPlacaC.Text), true, false, false);
            lstPlacaC.Columns[0].Width = 0;
            lstPlacaC.Columns[1].Width = 80;
            lstPlacaC.Columns[2].Width = 100;
            lstPlacaC.Columns[3].Width = 0;
            lstPlacaC.Columns[4].Width = 130;
            lstPlacaC.Columns[5].Width = 0;
            lstPlacaC.BringToFront();
            lstPlacaC.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculoC = -1;
                lstPlacaC.Visible = false;
                lstPlacaC.SendToBack();
            }
        }

        private void txtNuevaPlacaC_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlacaC.Focus(); }
        }

        private void lstPlacaC_Enter(object sender, EventArgs e)
        {
            if (!lstPlacaC.Items.Count.Equals(0)) { lstPlacaC.Items[0].Selected = true; }
        }

        private void lstPlacaC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlacaC.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlacaC.SelectedItems[0];

                idVehiculoC = Int32.Parse(ItemActual.Text);
                txtNuevaPlacaC.Text = ItemActual.SubItems[1].Text;

                if (OpcionCS == "TECLES") { txtCantidadC.Focus(); }
                if (OpcionCS == "SOGAS" || OpcionCS == "CÁMARAS") { txtCantidadS.Focus(); }

                lstPlacaC.Visible = false;
                lstPlacaC.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculoC = -1;
                lstPlacaC.Visible = false;
                lstPlacaC.SendToBack();
            }
        }

        private void lstPlacaC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlacaC.SelectedItems[0];

            idVehiculoC = Int32.Parse(ItemActual.Text);
            txtNuevaPlacaC.Text = ItemActual.SubItems[1].Text;

            if (OpcionCS == "TECLES") { txtCantidadC.Focus(); }
            if (OpcionCS == "SOGAS" || OpcionCS == "CÁMARAS") { txtCantidadS.Focus(); }

            lstPlacaC.Visible = false;
            lstPlacaC.SendToBack();
        }

        private void rbNuevoSi_Click(object sender, EventArgs e)
        {
            if (rbNuevoSi.Checked == true) { TieneC = "SÍ"; }
        }

        private void rbNuevoNo_Click(object sender, EventArgs e)
        {
            if (rbNuevoNo.Checked == true) { TieneC = "NO"; }
        }

        private void txtCantidadC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtCapacidadC.Focus(); }
        }

        private void txtCapacidadC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarNuevoC.Focus(); }
        }

        private void cbxSogas_DropDownClosed(object sender, EventArgs e) { txtCantidadS.Focus(); }

        private void txtCantidadS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardarC.Focus(); }
        }

        private void btnBuscarNuevoC_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK)
                {
                    byteArrayImagenC = File.ReadAllBytes(getImage.FileName);

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayImagenC));
                    pbCortinera.Image = x;
                    pbCortinera.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrarC_Click(object sender, EventArgs e)
        {
            byteArrayImagenC = null;
            pbCortinera.Image = null;
            pbCortinera.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnCancelarC_Click(object sender, EventArgs e)
        {
            txtNuevaPlacaC.Clear();
            idVehiculoC = -1;
            btnCerrarC_Click(sender, e);

            if (OpcionCS == "TECLES")
            {
                rbNuevoSi.Checked = true;
                rbNuevoSi_Click(sender, e);
                txtCantidadC.Clear();
                txtCapacidadC.Clear();
            }

            if (OpcionCS == "SOGAS" || OpcionCS == "CÁMARAS")
            {
                cbxSogas.Text = "OPERATIVO";
                txtCantidadS.Clear();
            }
        }

        private void btnGuardarC_Click(object sender, EventArgs e)
        {
            if (OpcionCS == "TECLES")
            {
                if (txtNuevaPlacaC.Text.Length == 0 || txtCantidadC.Text.Length == 0 || txtCapacidadC.Text.Length == 0)
                {
                    MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    if (txtNuevaPlacaC.Text.Length == 0) { txtNuevaPlacaC.Focus(); }
                    else
                    {
                        if (txtCantidadC.Text.Length == 0) { txtCantidadC.Focus(); }
                        else { txtCapacidadC.Focus(); }
                    }
                    return;
                }
                else
                {
                    try
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCortinera(OpcionC, idCortinera, idVehiculoC, TieneC,
                                                                 Convert.ToDecimal(txtCantidadC.Text), Convert.ToDecimal(txtCapacidadC.Text), byteArrayImagenC, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        
                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCerrarC2_Click(sender, e);
                            ListarCortineras();
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    catch { MessageBox.Show("No se pudo asignar el tecle de la unidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            
            if (OpcionCS == "SOGAS")
            {
                if (txtNuevaPlacaC.Text.Length == 0 || txtCantidadS.Text.Length == 0)
                {
                    MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    if (txtNuevaPlacaC.Text.Length == 0) { txtNuevaPlacaC.Focus(); }
                    else { txtCantidadS.Focus(); }
                    return;
                }
                else
                {
                    try
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarSogas(OpcionC, idCortinera, idVehiculoC,
                                                                 cbxSogas.Text, Convert.ToInt32(txtCantidadS.Text), byteArrayImagenC, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        
                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCerrarC2_Click(sender, e);
                            ListarCortineras();
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    catch { MessageBox.Show("No se pudo asignar la soga a la unidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            if (OpcionCS == "CÁMARAS")
            {
                if (txtNuevaPlacaC.Text.Length == 0 || txtCantidadS.Text.Length == 0)
                {
                    MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    if (txtNuevaPlacaC.Text.Length == 0) { txtNuevaPlacaC.Focus(); }
                    else { txtCantidadS.Focus(); }
                    return;
                }
                else
                {
                    try
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCamaras(OpcionC, idCortinera, idVehiculoC,
                                                                 cbxSogas.Text, Convert.ToInt32(txtCantidadS.Text), byteArrayImagenC, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        
                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btnCerrarC2_Click(sender, e);
                            ListarCortineras();
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    catch { MessageBox.Show("No se pudo asignar la cámara a la unidad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void txtPlacaC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCortineras(); }
        }

        private void cbxTodoItem_DropDownClosed(object sender, EventArgs e) { ListarCortineras(); }

        private void btnBuscarC_Click(object sender, EventArgs e) { ListarCortineras(); }

        private void btnExcelC_Click(object sender, EventArgs e)
        {
            if (dtgCortineras.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Cortineras - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dtgCortineras.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvCortinerasVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "TECLE")
            {
                if (Convert.ToString(e.CellValue) == "SÍ") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "NO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "OPERATIVO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "INOPERATIVO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void dtgCortineras_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idCortinera = Convert.ToString(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "NRO"));

                if (idCortinera != "")
                {
                    if (e14 == 1)
                    {
                        tsModificarC.Enabled = true;
                        tsDesvincularC.Enabled = true;
                    }
                }
                else
                {
                    tsModificarC.Enabled = false;
                    tsDesvincularC.Enabled = false;
                }
            }
            catch
            {
                tsModificarC.Enabled = false;
                tsDesvincularC.Enabled = false;
            }
        }

        private void tsModificarC_Click(object sender, EventArgs e)
        {
            OpcionC = 2;
            btnCancelarC.Enabled = false;

            idVehiculoC = Convert.ToInt32(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "IdTracto"));
            idCortinera = Convert.ToInt32(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "NRO"));
            txtNuevaPlacaC.Enabled = false;
            txtNuevaPlacaC.Text = Convert.ToString(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "UNIDAD"));
            
            if (OpcionCS == "TECLES")
            {
                panel12.Visible = true;
                panel12.BringToFront();

                string EstadoTecle = Convert.ToString(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "TECLE"));
                if (EstadoTecle == "SÍ")
                {
                    rbNuevoSi.Checked = true;
                    rbNuevoSi_Click(sender, e);
                }
                else
                {
                    rbNuevoNo.Checked = true;
                    rbNuevoNo_Click(sender, e);
                }

                txtCantidadC.Text = Convert.ToString(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "CANTIDAD"));
                txtCapacidadC.Text = Convert.ToString(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "CAPACIDAD"));
            }

            if (OpcionCS == "SOGAS" || OpcionCS == "CÁMARAS")
            {
                panel12.SendToBack();
                panel12.Visible = false;

                cbxSogas.Text = Convert.ToString(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "ESTADO"));
                txtCantidadS.Text = Convert.ToString(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "CANTIDAD"));
            }

            if (dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "Imagen").ToString() != "")
            {
                Byte[] byteBLOBData;
                byteBLOBData = (Byte[])dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "Imagen");
                byteArrayImagenC = (Byte[])dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "Imagen");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                pbCortinera.Image = x;
                pbCortinera.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            pNuevaCortinera.Location = new System.Drawing.Point(654, 202);
            pNuevaCortinera.Visible = true;
            pNuevaCortinera.BringToFront();
        }

        private void tsDesvincularC_Click(object sender, EventArgs e)
        {
            try
            {
                int idTracto = Convert.ToInt32(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "IdTracto"));
                int idCortinera = Convert.ToInt32(dgvCortinerasVista.GetRowCellValue(dgvCortinerasVista.FocusedRowHandle, "NRO"));

                if (OpcionCS == "TECLES")
                {
                    if (MessageBox.Show("¿Desea desvincular este tecle de la unidad?", "DESVINCULAR CORTINERA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string respta;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCortinera(3, idCortinera, idTracto, "", 0.00M, 0.00M, byteArrayImagenC, "");
                        respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0") { ListarCortineras(); }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
                
                if (OpcionCS == "SOGAS")
                {
                    if (MessageBox.Show("¿Desea desvincular esta soga de la unidad?", "DESVINCULAR CORTINERA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string respta;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarSogas(3, idCortinera, idTracto, "", 0, byteArrayImagenC, "");
                        respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0") { ListarCortineras(); }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }

                if (OpcionCS == "CÁMARAS")
                {
                    if (MessageBox.Show("¿Desea desvincular esta cámara de la unidad?", "DESVINCULAR CORTINERA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string respta;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_IngresarModificarEliminarCamaras(3, idCortinera, idTracto, "", 0, byteArrayImagenC, "");
                        respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0") { ListarCortineras(); }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void rbTecles_Click(object sender, EventArgs e)
        {
            if (rbTecles.Checked == true)
            {
                OpcionCS = "TECLES";
                label75.Text = "Tecle:";
                cbxTodoItem.Items.Clear();
                cbxTodoItem.Items.AddRange(new object[] { "TODOS", "SÍ", "NO" });
                cbxTodoItem.Text = "TODOS";
                btnCerrarC2_Click(sender, e);
                ListarCortineras();
            }
        }

        private void rbSogas_Click(object sender, EventArgs e)
        {
            if (rbSogas.Checked == true)
            {
                OpcionCS = "SOGAS";
                label75.Text = "Estado:";
                cbxTodoItem.Items.Clear();
                cbxTodoItem.Items.AddRange(new object[] { "TODOS", "OPERATIVO", "INOPERATIVO" });
                cbxTodoItem.Text = "TODOS";
                btnCerrarC2_Click(sender, e);
                ListarCortineras();
            }
        }

        private void rbCamara_Click(object sender, EventArgs e)
        {
            if (rbCamara.Checked == true)
            {
                OpcionCS = "CÁMARAS";
                label75.Text = "Estado:";
                cbxTodoItem.Items.Clear();
                cbxTodoItem.Items.AddRange(new object[] { "TODOS", "OPERATIVO", "INOPERATIVO" });
                cbxTodoItem.Text = "TODOS";
                btnCerrarC2_Click(sender, e);
                ListarCortineras();
            }
        }
    }
}
