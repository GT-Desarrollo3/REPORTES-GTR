using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmDocumentosCapacitacion : MetroFramework.Forms.MetroForm
    {
        DataTable dtListaDocumentos = new DataTable();
        DataTable dtPermisos = new DataTable();
        int Opcion = 0, idConductor = -1, idCapacitacion = 0, Teoria, Revision, Circuito;
        public int xClick = 0, yClick = 0;

        public frmDocumentosCapacitacion()
        {
            InitializeComponent();
        }

        private void frmDocumentosCapacitacion_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmDocumentosCapacitacion");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevaCapacitacion.Enabled = true; }
                else { btnNuevaCapacitacion.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarDocumento.Enabled = true; }
                else { tsEliminarDocumento.Enabled = false; }
            }

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;

            ListarDocumentosC();
        }


        public void ListarDocumentosC()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaDocumentos = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Capacitaciones_ListarDocumentos(txtBCapacitacion.Text, txtBConductor.Text, dtpFechaIni.Text, dtpFechaFin.Text);
                dtgDocumentos.DataSource = null;
                dgvDocumentosVista.Columns.Clear();
                dtgDocumentos.DataSource = dtListaDocumentos;
                if (dtListaDocumentos.Rows.Count > 0)
                {
                    dgvDocumentosVista.Columns["TITULO"].Visible = false;
                    dgvDocumentosVista.Columns["EXTENSION"].Visible = false;

                    dgvDocumentosVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvDocumentosVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvDocumentosVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvDocumentosVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";


                    dgvDocumentosVista.Columns["CONDUCTOR"].Summary.Clear();
                    dgvDocumentosVista.Columns["CONDUCTOR"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "VISTA PREVIA", "Total = {0}");

                    // AGRUPAR TABLAS
                    dgvDocumentosVista.OptionsView.AllowCellMerge = true;
                    dgvDocumentosVista.OptionsView.ShowGroupPanel = false;

                    dgvDocumentosVista.OptionsBehavior.Editable = false;
                    dgvDocumentosVista.OptionsSelection.EnableAppearanceFocusedCell = false;

                    string[] columnasMerge = { "NRO", "FECHA", "CAPACITACION", "BASE", "INSTRUCTOR", "VISTA PREVIA", "DOCUMENTO", "UsuarioCreacion", "FechaCreacion" };

                    foreach (DevExpress.XtraGrid.Columns.GridColumn col in dgvDocumentosVista.Columns)
                    {
                        col.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;

                        col.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        col.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    }

                    foreach (string nombreColumna in columnasMerge)
                    {
                        if (dgvDocumentosVista.Columns[nombreColumna] != null)
                        { dgvDocumentosVista.Columns[nombreColumna].OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.True; }
                    }

                    dgvDocumentosVista.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    dgvDocumentosVista.Appearance.HeaderPanel.Font = new Font(dgvDocumentosVista.Appearance.HeaderPanel.Font, FontStyle.Bold);

                    dgvDocumentosVista.CellMerge -= dgvDocumentosVista_CellMerge;
                    dgvDocumentosVista.CellMerge += dgvDocumentosVista_CellMerge;

                    dgvDocumentosVista.BestFitColumns();
                }
            }
        }

        public void ListarAsistentes(int idCapacitacion)
        {
            DataTable dtListaAsistencias = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Capacitaciones_ListarAsistentes(idCapacitacion);
            dtgAsistentes.DataSource = null;
            dgvAsistentesVista.Columns.Clear();
            dtgAsistentes.DataSource = dtListaAsistencias;

            if (dtListaAsistencias.Rows.Count > 0)
            {
                dgvAsistentesVista.Columns["idCapacitacion"].Visible = false;
                dgvAsistentesVista.Columns["idCapacitacionD"].Visible = false;

                dgvAsistentesVista.Columns["CONDUCTOR"].Summary.Clear();
                dgvAsistentesVista.Columns["CONDUCTOR"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CONDUCTOR", "Total = {0}");

                dgvAsistentesVista.BestFitColumns();
            }
        }


        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDocumentosC(); }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDocumentosC(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDocumentosC(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDocumentosC(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarDocumentosC(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgDocumentos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Capacitaciones en Operaciones - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgDocumentos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnNuevaCapacitacion_Click(object sender, EventArgs e)
        {
            Opcion = 1;
            idCapacitacion = 0;
            txtCapacitacion.Focus();
            dtpFecha.Value = DateTime.Now;
            cbxBase.Text = "TRUJILLO";
            cbxEstado.Text = " ";
            chBloque.Checked = true;
            chBloque_CheckedChanged(sender, e);
            cbRevision.Checked = true;
            cbRevision_CheckedChanged(sender, e);
            cbCircuito.Checked = true;
            cbCircuito_CheckedChanged(sender, e);

            pImportarDocumentos.Location = new System.Drawing.Point(479, 333);
            pImportarDocumentos.Visible = true;
            pImportarDocumentos.BringToFront();
            btnNuevaCapacitacion.Enabled = false;

            ListarAsistentes(idCapacitacion);
        }

        private void pImportarDocumentos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pImportarDocumentos.Left = pImportarDocumentos.Left + (e.X - xClick);
                pImportarDocumentos.Top = pImportarDocumentos.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Opcion = 0;
            idConductor = -1;

            txtTituloD.Clear();
            txtExtension.Clear();

            txtCapacitacion.Clear();
            txtInstructor.Clear();
            btnCerrarLocal_Click(sender, e);
            txtConductor.Clear();
            txtNotaExamen.Clear();
            cbxEstado.Text = " ";

            chBloque.Checked = false;
            chBloque_CheckedChanged(sender, e);
            cbRevision.Checked = false;
            cbRevision_CheckedChanged(sender, e);
            cbCircuito.Checked = false;
            cbCircuito_CheckedChanged(sender, e);

            pImportarDocumentos.Visible = false;
            pImportarDocumentos.SendToBack();
            btnNuevaCapacitacion.Enabled = true;

            ListarDocumentosC();
        }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxBase.Focus(); }
        }

        private void cbxBase_DropDownClosed(object sender, EventArgs e) { txtInstructor.Focus(); }

        private void txtInstructor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarArchivo.Focus(); }
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(op.FileName))
                {
                    txtRutaLocal.Text = op.FileName;
                    txtTituloD.Text = Path.GetFileNameWithoutExtension(op.FileName);
                    txtExtension.Text = Path.GetExtension(op.FileName);
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarLocal_Click(object sender, EventArgs e)
        {
            txtRutaLocal.Clear();
            txtTituloD.Clear();
            txtExtension.Clear();
        }

        private void txtConductor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstConductor, clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ListarMotivos(1, txtConductor.Text), true, false, false);
            lstConductor.Columns[0].Width = 0;
            lstConductor.Columns[1].Width = 320;
            lstConductor.BringToFront();
            lstConductor.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                idConductor = -1;
            }
        }

        private void txtConductor2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstConductor.Focus(); }
        }

        private void lstConductor_Enter(object sender, EventArgs e)
        {
            if (!lstConductor.Items.Count.Equals(0)) { lstConductor.Items[0].Selected = true; }
        }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstConductor.SelectedItems[0];

                idConductor = Int32.Parse(ItemActual.Text);
                txtConductor.Text = ItemActual.SubItems[1].Text;

                lstConductor.Visible = false;
                lstConductor.SendToBack();
                txtNotaExamen.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                idConductor = -1;
            }
        }

        private void lstConductor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstConductor.SelectedItems[0];

            idConductor = Int32.Parse(ItemActual.Text);
            txtConductor.Text = ItemActual.SubItems[1].Text;

            lstConductor.Visible = false;
            lstConductor.SendToBack();
            txtNotaExamen.Focus();
        }

        private void txtNotaExamen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxEstado.Focus(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { btnAgregar.Focus(); }

        private void chBloque_CheckedChanged(object sender, EventArgs e)
        {
            if (chBloque.Checked == true) { Teoria = 1; }

            if (chBloque.Checked == false) { Teoria = 0; }
        }

        private void cbRevision_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRevision.Checked == true) { Revision = 1; }

            if (cbRevision.Checked == false) { Revision = 0; }
        }

        private void cbCircuito_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCircuito.Checked == true) { Circuito = 1; }

            if (cbCircuito.Checked == false) { Circuito = 0; }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtConductor.Text.Length == 0 || txtNotaExamen.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtConductor.Text.Length == 0) { txtConductor.Focus(); }
                else { txtNotaExamen.Focus(); }
                return;
            }

            if (Convert.ToInt32(txtNotaExamen.Text) < 0 || Convert.ToInt32(txtNotaExamen.Text) > 20)
            {
                MessageBox.Show("Ingrese valores entre 0 y 20.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtConductor.Text.Length == 0) { txtConductor.Focus(); }
                else { txtNotaExamen.Focus(); }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta = "";
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Capacitaciones_RegistrarAsistentes(1, idCapacitacion, 0, idConductor, Convert.ToInt32(txtNotaExamen.Text),
                                                         cbxEstado.Text, Teoria, Revision, Circuito);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    idConductor = -1;
                    txtConductor.Clear();
                    txtNotaExamen.Clear();
                    cbxEstado.Text = " ";

                    ListarAsistentes(idCapacitacion);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgAsistentes_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Nro = dgvAsistentesVista.GetRowCellValue(dgvAsistentesVista.FocusedRowHandle, "idCapacitacionD").ToString();

                if (Nro != "") { tsQuitarAsistente.Enabled = true; }
                else { tsQuitarAsistente.Enabled = false; }
            }
            catch { tsQuitarAsistente.Enabled = false; }
        }

        private void tsQuitarAsistente_Click(object sender, EventArgs e)
        {
            int idAsis = Convert.ToInt32(dgvAsistentesVista.GetRowCellValue(dgvAsistentesVista.FocusedRowHandle, "idCapacitacion"));
            int idAsisD = Convert.ToInt32(dgvAsistentesVista.GetRowCellValue(dgvAsistentesVista.FocusedRowHandle, "idCapacitacionD"));

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Capacitaciones_RegistrarAsistentes(2, idAsis, idAsisD, 0, 0, "", 0, 0, 0);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0") { ListarAsistentes(idAsis); }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtRutaLocal.Text.Length == 0)
            {
                MessageBox.Show("Por favor, seleccione un documento a registrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtCapacitacion.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el nombre de la capacitación.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCapacitacion.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta = "";
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                string ruta = txtRutaLocal.Text;

                if (!File.Exists(ruta))
                {
                    MessageBox.Show("La ruta no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                byte[] archivoBytes = File.ReadAllBytes(ruta);

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Capacitaciones_RegistrarEditarDocumentos(Opcion, idCapacitacion, txtCapacitacion.Text,
                                                         dtpFecha.Value, cbxBase.Text, txtInstructor.Text, archivoBytes, txtTituloD.Text, txtExtension.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    ListarDocumentosC();
                }

                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgDocumentos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Nro = dgvDocumentosVista.GetRowCellValue(dgvDocumentosVista.FocusedRowHandle, "NRO").ToString();

                if (Nro != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarDocumento.Enabled = true; }
                }
                else { tsEliminarDocumento.Enabled = false; }
            }
            catch { tsEliminarDocumento.Enabled = false; }
        }

        private void dtgDocumentos_DoubleClick(object sender, EventArgs e)
        {
            Opcion = 2;
            idCapacitacion = Convert.ToInt32(dgvDocumentosVista.GetRowCellValue(dgvDocumentosVista.FocusedRowHandle, "NRO"));
            txtCapacitacion.Text = Convert.ToString(dgvDocumentosVista.GetRowCellValue(dgvDocumentosVista.FocusedRowHandle, "CAPACITACION"));
            dtpFecha.Value = Convert.ToDateTime(dgvDocumentosVista.GetRowCellValue(dgvDocumentosVista.FocusedRowHandle, "FECHA"));
            cbxBase.Text = Convert.ToString(dgvDocumentosVista.GetRowCellValue(dgvDocumentosVista.FocusedRowHandle, "BASE"));
            txtInstructor.Text = Convert.ToString(dgvDocumentosVista.GetRowCellValue(dgvDocumentosVista.FocusedRowHandle, "INSTRUCTOR"));
            txtTituloD.Text = dgvDocumentosVista.GetRowCellValue(dgvDocumentosVista.FocusedRowHandle, "TITULO").ToString();
            txtExtension.Text = dgvDocumentosVista.GetRowCellValue(dgvDocumentosVista.FocusedRowHandle, "EXTENSION").ToString();
            cbxEstado.Text = " ";
            chBloque.Checked = true;
            chBloque_CheckedChanged(sender, e);
            cbRevision.Checked = true;
            cbRevision_CheckedChanged(sender, e);
            cbCircuito.Checked = true;
            cbCircuito_CheckedChanged(sender, e);

            pImportarDocumentos.Location = new System.Drawing.Point(479, 333);
            pImportarDocumentos.Visible = true;
            pImportarDocumentos.BringToFront();
            btnNuevaCapacitacion.Enabled = false;

            ListarAsistentes(idCapacitacion);
        }

        private void tsEliminarDocumento_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar esta capacitación?", "ELIMINAR CAPACITACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idCapacitacion = Convert.ToInt32(dgvDocumentosVista.GetRowCellValue(dgvDocumentosVista.FocusedRowHandle, "NRO"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Capacitaciones_RegistrarEditarDocumentos(3, idCapacitacion, "", dtpFecha.Value, "",
                                                         "", null, "", "", "");
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0") { ListarDocumentosC(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgvDocumentosVista_CellMerge(object sender, CellMergeEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;

            string[] columnasMerge = { "NRO", "FECHA", "CAPACITACION", "BASE", "INSTRUCTOR", "VISTA PREVIA", "DOCUMENTO", "UsuarioCreacion", "FechaCreacion" };

            if (!columnasMerge.Contains(e.Column.FieldName))
            {
                e.Merge = false;
                e.Handled = true;
                return;
            }

            object nro1 = view.GetRowCellValue(e.RowHandle1, "NRO");
            object nro2 = view.GetRowCellValue(e.RowHandle2, "NRO");

            object capacitacion1 = view.GetRowCellValue(e.RowHandle1, "CAPACITACION");
            object capacitacion2 = view.GetRowCellValue(e.RowHandle2, "CAPACITACION");

            bool mismoGrupo = Equals(nro1, nro2) && Equals(capacitacion1, capacitacion2);

            e.Merge = mismoGrupo && Equals(e.CellValue1, e.CellValue2);
            e.Handled = true;
        }

        private void dgvDocumentosVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "DOCUMENTO")
            {
                if (Convert.ToString(e.CellValue) == "Descargar")
                {
                    e.Appearance.ForeColor = Color.DodgerBlue;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }

            if (e.Column.FieldName == "VISTA PREVIA")
            {
                if (Convert.ToString(e.CellValue) == "Vista Previa")
                {
                    e.Appearance.ForeColor = Color.DodgerBlue;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }

            if (e.Column.FieldName == "NOTA")
            {
                if (Convert.ToInt32(e.CellValue) >= 0 || Convert.ToInt32(e.CellValue) < 12)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }

                if (Convert.ToDecimal(e.CellValue) >= 12)
                {
                    e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }

        private void dgvDocumentosVista_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "VISTA PREVIA")
            {
                var view = (GridView)sender;
                var row = view.GetDataRow(e.RowHandle);
                if (row == null) return;

                if (!string.Equals(Convert.ToString(row["DOCUMENTO"]), "Descargar", StringComparison.OrdinalIgnoreCase)) return;

                int idNro3 = Convert.ToInt32(row["NRO"]);

                DataTable dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_BuscarDocumentos(2, idNro3);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el archivo.");
                    return;
                }

                byte[] bytes = dt.Rows[0]["ARCHIVO"] as byte[];

                if (bytes == null || bytes.Length == 0)
                {
                    MessageBox.Show("El archivo está vacío.");
                    return;
                }

                string titulo = Convert.ToString(row["TITULO"]);
                string extension = Convert.ToString(row["EXTENSION"]);

                if (!string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("El archivo no es un PDF.");
                    return;
                }

                try
                {
                    string fileName = Guid.NewGuid().ToString() + ".pdf";
                    string tempPath = Path.Combine(Path.GetTempPath(), fileName);
                    File.WriteAllBytes(tempPath, bytes);

                    Process.Start(new ProcessStartInfo(tempPath)
                    {
                        UseShellExecute = true
                    });
                }
                catch (Exception ex) { MessageBox.Show("Error al abrir el archivo."); }
            }

            if (e.Column.FieldName == "DOCUMENTO")
            {
                var view = (GridView)sender;
                var row = view.GetDataRow(e.RowHandle);
                if (row == null) return;

                if (!string.Equals(Convert.ToString(row["DOCUMENTO"]), "Descargar", StringComparison.OrdinalIgnoreCase)) return;

                int idNro3 = Convert.ToInt32(row["NRO"]);

                DataTable dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_BuscarDocumentos(2, idNro3);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el archivo.");
                    return;
                }

                byte[] bytes = dt.Rows[0]["ARCHIVO"] as byte[];

                if (bytes == null || bytes.Length == 0)
                {
                    MessageBox.Show("El archivo está vacío.");
                    return;
                }

                string titulo = Convert.ToString(row["TITULO"]);
                string extension = Convert.ToString(row["EXTENSION"]);

                using (var sfd = new SaveFileDialog())
                {
                    sfd.FileName = titulo + extension;
                    sfd.Filter = "ARCHIVO (*" + extension + ")|*" + extension + "|Todos los archivos (*.*)|*.*";
                    sfd.AddExtension = true;
                    sfd.OverwritePrompt = true;

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllBytes(sfd.FileName, bytes);

                        try
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = sfd.FileName,
                                UseShellExecute = true
                            });
                        }
                        catch { }
                    }
                }
            }
        }

        private void dgvAsistentesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.FieldName == "NOTA")
            {
                if (Convert.ToInt32(e.CellValue) >= 0 || Convert.ToInt32(e.CellValue) < 12)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }

                if (Convert.ToDecimal(e.CellValue) >= 12)
                {
                    e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
                    e.Appearance.ForeColor = Color.Black;
                }
            }
        }
    }
}
