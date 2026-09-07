using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors;
﻿using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using ReportesTranspesa.Properties;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    public partial class frmComponenteDesgaste : Form
    {
        public int idUnidad = -1;
        public int OpcionC = 0;
        public DataSet dsTabla, dsTabla2;
        public DataSet dsHistorialT, dsHistorialC;

        public frmComponenteDesgaste()
        {
            InitializeComponent();
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones(); }

        private void frmComponenteDesgaste_Load(object sender, EventArgs e)
        {
            CargarComboOperaciones();
            cbxOperacion.Text = "TODO";
            cbxOperacion_DropDownClosed(sender, e);
        }


        public void CargarComboOperaciones()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperacion.DataSource = dtOperaciones;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void ListarDesgastes()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dsTabla = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_CompDesgaste_ListarComponentes(1, txtBuscarUnidad.Text, cbxOperacion.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgReporteCarreta.DataSource = dsTabla.Tables[0];
                dtgReporteTracto.DataSource = dsTabla.Tables[1];

                if (dsTabla.Tables[0].Rows.Count > 0)
                {
                    dgvReporteCarretaVista.Columns["idCompDesgasteC"].Visible = false;
                    dgvReporteCarretaVista.Columns["idPlaca"].Visible = false;
                    dgvReporteCarretaVista.Columns["idComponente"].Visible = false;

                    dgvReporteCarretaVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReporteCarretaVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvReporteCarretaVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReporteCarretaVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvReporteCarretaVista.Columns["PLACA"].Summary.Clear();
                    dgvReporteCarretaVista.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");

                    dgvReporteCarretaVista.BestFitColumns();
                    dgvReporteCarretaVista.OptionsBehavior.Editable = false;
                }

                if (dsTabla.Tables[1].Rows.Count > 0)
                {
                    dgvReporteTractoVista.Columns["idCompDesgasteT"].Visible = false;
                    dgvReporteTractoVista.Columns["idPlaca"].Visible = false;
                    dgvReporteTractoVista.Columns["idComponente"].Visible = false;

                    dgvReporteTractoVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReporteTractoVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvReporteTractoVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReporteTractoVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvReporteTractoVista.Columns["PLACA"].Summary.Clear();
                    dgvReporteTractoVista.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");

                    dgvReporteTractoVista.BestFitColumns();
                    dgvReporteTractoVista.OptionsBehavior.Editable = false;
                }
            }
        }

        public void ListarHistorialDesgaste()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dsTabla = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_CompDesgaste_ListarComponentes(2, txtBuscarUnidad.Text, cbxOperacion.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgReporteCarreta.DataSource = dsTabla.Tables[0];
                dtgReporteTracto.DataSource = dsTabla.Tables[1];

                if (dsTabla.Tables[0].Rows.Count > 0)
                {
                    dgvReporteCarretaVista.Columns["idCompDesgasteC"].Visible = false;
                    dgvReporteCarretaVista.Columns["idPlaca"].Visible = false;
                    dgvReporteCarretaVista.Columns["idComponente"].Visible = false;

                    dgvReporteCarretaVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReporteCarretaVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvReporteCarretaVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReporteCarretaVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvReporteCarretaVista.Columns["PLACA"].Summary.Clear();
                    dgvReporteCarretaVista.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");

                    dgvReporteCarretaVista.BestFitColumns();
                    dgvReporteCarretaVista.OptionsBehavior.Editable = false;
                }

                if (dsTabla.Tables[1].Rows.Count > 0)
                {
                    dgvReporteTractoVista.Columns["idCompDesgasteT"].Visible = false;
                    dgvReporteTractoVista.Columns["idPlaca"].Visible = false;
                    dgvReporteTractoVista.Columns["idComponente"].Visible = false;

                    dgvReporteTractoVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReporteTractoVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvReporteTractoVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvReporteTractoVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvReporteTractoVista.Columns["PLACA"].Summary.Clear();
                    dgvReporteTractoVista.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");

                    dgvReporteTractoVista.BestFitColumns();
                    dgvReporteTractoVista.OptionsBehavior.Editable = false;
                }
            }
        }

        public void ConfigurarColumnas()
        {
            dgvReporteCarretaVista.OptionsBehavior.Editable = true;

            var repoDateTime = new RepositoryItemDateEdit();

            repoDateTime.CalendarView = CalendarView.Vista;
            repoDateTime.VistaDisplayMode = DefaultBoolean.True;

            repoDateTime.Mask.UseMaskAsDisplayFormat = true;
            repoDateTime.Mask.EditMask = "dd/MM/yyyy";

            repoDateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repoDateTime.DisplayFormat.FormatString = "dd/MM/yyyy";
            repoDateTime.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repoDateTime.EditFormat.FormatString = "dd/MM/yyyy";

            dtgReporteCarreta.RepositoryItems.Add(repoDateTime);

            dgvReporteCarretaVista.Columns["PLACA"].OptionsColumn.AllowEdit = false;
            dgvReporteCarretaVista.Columns["TIPO"].OptionsColumn.AllowEdit = false;
            dgvReporteCarretaVista.Columns["OPERACION"].OptionsColumn.AllowEdit = false;
            dgvReporteCarretaVista.Columns["COMPONENTE"].OptionsColumn.AllowEdit = false;
            dgvReporteCarretaVista.Columns["STATUS"].OptionsColumn.AllowEdit = false;
            dgvReporteCarretaVista.Columns["FECHA"].OptionsColumn.AllowEdit = false;
            dgvReporteCarretaVista.Columns["USUARIO"].OptionsColumn.AllowEdit = false;

            dgvReporteCarretaVista.Columns["FECHA_ANTERIOR"].ColumnEdit = repoDateTime;
        }

        public void ConfigurarColumnasT()
        {
            dgvReporteTractoVista.OptionsBehavior.Editable = true;

            var repoDateTime = new RepositoryItemDateEdit();

            repoDateTime.CalendarView = CalendarView.Vista;
            repoDateTime.VistaDisplayMode = DefaultBoolean.True;

            repoDateTime.Mask.UseMaskAsDisplayFormat = true;
            repoDateTime.Mask.EditMask = "dd/MM/yyyy";

            repoDateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repoDateTime.DisplayFormat.FormatString = "dd/MM/yyyy";
            repoDateTime.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            repoDateTime.EditFormat.FormatString = "dd/MM/yyyy";

            dtgReporteTracto.RepositoryItems.Add(repoDateTime);

            dgvReporteTractoVista.Columns["PLACA"].OptionsColumn.AllowEdit = false;
            dgvReporteTractoVista.Columns["TIPO"].OptionsColumn.AllowEdit = false;
            dgvReporteTractoVista.Columns["OPERACION"].OptionsColumn.AllowEdit = false;
            dgvReporteTractoVista.Columns["COMPONENTE"].OptionsColumn.AllowEdit = false;
            dgvReporteTractoVista.Columns["NRO"].OptionsColumn.AllowEdit = false;
            dgvReporteTractoVista.Columns["PROGRAMACION"].OptionsColumn.AllowEdit = false;
            dgvReporteTractoVista.Columns["FECHA"].OptionsColumn.AllowEdit = false;
            dgvReporteTractoVista.Columns["USUARIO"].OptionsColumn.AllowEdit = false;

            dgvReporteTractoVista.Columns["FECHA_ANTERIOR"].ColumnEdit = repoDateTime;


            var repoComboBox = new RepositoryItemComboBox();
            repoComboBox.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            repoComboBox.Items.Add("REGULACION");
            repoComboBox.Items.Add("MTTO");

            dgvReporteTractoVista.Columns["ACTIVIDAD"].ColumnEdit = repoComboBox;
        }


        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtPlaca.Text), true, false, false);
            lstPlaca.Columns[0].Width = 0;
            lstPlaca.Columns[1].Width = 60;
            lstPlaca.Columns[2].Width = 100;
            lstPlaca.Columns[3].Width = 0;
            lstPlaca.Columns[4].Width = 0;
            lstPlaca.Columns[5].Width = 0;
            lstPlaca.BringToFront();
            lstPlaca.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtTipoUnidad.Clear();
                idUnidad = -1;
            }
        }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
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

                idUnidad = Int32.Parse(ItemActual.Text);
                txtPlaca.Text = ItemActual.SubItems[1].Text;
                txtTipoUnidad.Text = ItemActual.SubItems[2].Text;

                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                btnGuardarComp.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPlaca.Visible = false;
                lstPlaca.SendToBack();
                txtTipoUnidad.Clear();
                idUnidad = -1;
            }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca.SelectedItems[0];

            idUnidad = Int32.Parse(ItemActual.Text);
            txtPlaca.Text = ItemActual.SubItems[1].Text;
            txtTipoUnidad.Text = ItemActual.SubItems[2].Text;

            lstPlaca.Visible = false;
            lstPlaca.SendToBack();
            btnGuardarComp.Focus();
        }

        private void btnGuardarComp_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text.Length == 0 || txtTipoUnidad.Text.Length == 0)
            {
                MessageBox.Show("Los datos de la unidad no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPlaca.Focus(); 
                return;
            }
            else
            {
                int idComponente = 0;
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                if (txtTipoUnidad.Text == "TRACTO") { idComponente = 2; }
                else { idComponente = 1; }

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_CompDesgaste_CrearComponente(idUnidad, idComponente, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPlaca.Clear();
                    txtTipoUnidad.Clear();
                    idUnidad = -1;
                    ListarDesgastes();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPlaca.Focus();
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (OpcionC == 0) { ListarDesgastes(); }
            else { ListarHistorialDesgaste(); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                dtgReporteTracto.ForceInitialize();
                dtgReporteCarreta.ForceInitialize();

                compositeLink1.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "REPORTE DE COMPONENTES DESGASTE - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink1.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtBuscarUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (OpcionC == 0) { ListarDesgastes(); }
                else { ListarHistorialDesgaste(); }
            }
        }      

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e)
        {
            if (OpcionC == 0) { ListarDesgastes(); }
            else { ListarHistorialDesgaste(); }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (OpcionC == 0) { ListarDesgastes(); }
                else { ListarHistorialDesgaste(); }
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (OpcionC == 0) { ListarDesgastes(); }
                else { ListarHistorialDesgaste(); }
            }
        }

        private void tsActualizarC_Click(object sender, EventArgs e) { ConfigurarColumnas(); }

        private void dtgReporteCarreta_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dgvReporteCarretaVista.GetRowCellValue(dgvReporteCarretaVista.FocusedRowHandle, "idCompDesgasteC").ToString();

                if (Vacio != "") { tsActualizarC.Enabled = true; }
            }
            catch { tsActualizarC.Enabled = false; }
        }

        private void dgvReporteCarretaVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "STATUS")
            {
                if (Convert.ToString(e.CellValue) == "NORMAL") { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "PRECAUCIÓN") { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "CRÍTICO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void dgvReporteCarretaVista_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (OpcionC == 0 && dgvReporteCarretaVista.OptionsBehavior.Editable == true)
                {
                    var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                    int idPlaca, idComponente;
                    decimal Milimetro;
                    DateTime FechaAnterior;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    if (e.KeyCode == Keys.Escape)
                    {
                        view.CloseEditor();
                        view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                        ListarDesgastes();
                    }

                    if (e.KeyCode == Keys.Enter)
                    {
                        if (MessageBox.Show("¿Desea actualizar el desgaste de esta unidad?", "ACTUALIZAR DESGASTE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            view.CloseEditor();
                            idPlaca = Convert.ToInt32(dgvReporteCarretaVista.GetRowCellValue(dgvReporteCarretaVista.FocusedRowHandle, "idPlaca"));
                            FechaAnterior = Convert.ToDateTime(dgvReporteCarretaVista.GetRowCellValue(dgvReporteCarretaVista.FocusedRowHandle, "FECHA_ANTERIOR"));
                            Milimetro = Convert.ToDecimal(dgvReporteCarretaVista.GetRowCellValue(dgvReporteCarretaVista.FocusedRowHandle, "MM"));
                            idComponente = Convert.ToInt32(dgvReporteCarretaVista.GetRowCellValue(dgvReporteCarretaVista.FocusedRowHandle, "idComponente"));

                            DataTable dtRespuesta = new DataTable();
                            string Respuesta;

                            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteC(idPlaca, idComponente, FechaAnterior, Milimetro, Usuario);
                            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                            string NroRPTA = Respuesta.Substring(0, 1);

                            if (NroRPTA != "0")
                            {
                                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                view.PostEditor();
                                view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                                view.UpdateCurrentRow();
                            }
                            else
                            {
                                //MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                view.CloseEditor();
                                view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                                view.UpdateCurrentRow();
                                ListarDesgastes();
                            }
                        }
                    }
                }
            }
            catch { MessageBox.Show("No se pudo actualizar el reporte. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsActualizarT_Click(object sender, EventArgs e) { ConfigurarColumnasT(); }

        private void dtgReporteTracto_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Vacio = dgvReporteTractoVista.GetRowCellValue(dgvReporteTractoVista.FocusedRowHandle, "idCompDesgasteT").ToString();

                if (Vacio != "") { tsActualizarT.Enabled = true; }
            }
            catch { tsActualizarT.Enabled = false; }
        }

        private void dgvReporteTractoVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PROGRAMACION")
            {
                if (Convert.ToString(e.CellValue) == "NORMAL") { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "PRECAUCIÓN") { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "CRÍTICO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void dgvReporteTractoVista_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (OpcionC == 0 && dgvReporteTractoVista.OptionsBehavior.Editable == true)
                {
                    var view = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
                    int idPlaca, idComponente;
                    string Actividad;
                    DateTime FechaAnterior;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    if (e.KeyCode == Keys.Escape)
                    {
                        view.CloseEditor();
                        view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                        ListarDesgastes();
                    }

                    if (e.KeyCode == Keys.Enter)
                    {
                        if (MessageBox.Show("¿Desea actualizar el desgaste de esta unidad?", "ACTUALIZAR DESGASTE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            view.CloseEditor();
                            idPlaca = Convert.ToInt32(dgvReporteTractoVista.GetRowCellValue(dgvReporteTractoVista.FocusedRowHandle, "idPlaca"));
                            FechaAnterior = Convert.ToDateTime(dgvReporteTractoVista.GetRowCellValue(dgvReporteTractoVista.FocusedRowHandle, "FECHA_ANTERIOR"));
                            Actividad = Convert.ToString(dgvReporteTractoVista.GetRowCellValue(dgvReporteTractoVista.FocusedRowHandle, "ACTIVIDAD"));
                            idComponente = Convert.ToInt32(dgvReporteTractoVista.GetRowCellValue(dgvReporteTractoVista.FocusedRowHandle, "idComponente"));

                            DataTable dtRespuesta = new DataTable();
                            string Respuesta;

                            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_CompDesgaste_ActualizarComponenteT(idPlaca, idComponente, FechaAnterior, Actividad, Usuario);
                            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                            string NroRPTA = Respuesta.Substring(0, 1);

                            if (NroRPTA != "0")
                            {
                                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                view.PostEditor();
                                view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                                view.UpdateCurrentRow();
                            }
                            else
                            {
                                view.CloseEditor();
                                view.SetRowCellValue(view.FocusedRowHandle, view.FocusedColumn, DBNull.Value);
                                view.UpdateCurrentRow();
                                ListarDesgastes();
                            }
                        }
                    }
                }
            }
            catch { MessageBox.Show("No se pudo actualizar el reporte. Intente de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsHistReportes_Click(object sender, EventArgs e)
        {
            dtgReporteTracto.DataSource = null;
            dgvReporteTractoVista.Columns.Clear();
            dtgReporteCarreta.DataSource = null;
            dgvReporteCarretaVista.Columns.Clear();
            
            if (OpcionC == 0)
            {
                OpcionC = 1;
                dtgReporteCarreta.ContextMenuStrip = null;
                dtgReporteTracto.ContextMenuStrip = null;

                tsHistReportes.Text = "Reporte de Desgastes";
                labelKP.Text = "HISTORIAL DE KINPIN";
                labelT.Text = "HISTORIAL DE TORNAMESA";

                ListarHistorialDesgaste();
            }
            else
            {
                OpcionC = 0;
                dtgReporteCarreta.ContextMenuStrip = contextMenuStrip1;
                dtgReporteTracto.ContextMenuStrip = contextMenuStrip2;

                tsHistReportes.Text = "Historial de Desgastes";
                labelKP.Text = "KINPIN";
                labelT.Text = "TORNAMESA";

                ListarDesgastes();
            }
        }
    }
}
