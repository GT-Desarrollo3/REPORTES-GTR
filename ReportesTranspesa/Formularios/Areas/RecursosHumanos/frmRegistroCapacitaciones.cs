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

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmRegistroCapacitaciones : MetroFramework.Forms.MetroForm
    {
        DataTable dtListaDocumentos = new DataTable();
        DataTable dtPermisos = new DataTable();
        int Nro, ListaP = 0, Opcion = 0, Persona = -1, Proveedor = -1;
        public int xClick = 0, yClick = 0;

        public frmRegistroCapacitaciones()
        {
            InitializeComponent();
        }

        private void frmRegistroCapacitaciones_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistroCapacitaciones");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevoRegistro.Enabled = true; }
                else { btnNuevoRegistro.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsModificarVersion.Enabled = true; }
                else { tsModificarVersion.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsAnularVersion.Enabled = true; }
                else { tsAnularVersion.Enabled = false; }
            }

            dtpFechaIni.Value = new DateTime(2025, 1, 1);
            dtpFechaFin.Value = DateTime.Now;
            cbxEstado.Text = "TODOS";

            ListarCapacitaciones();
        }


        public void ListarCapacitaciones()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaDocumentos = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Capacitaciones_ListarCapacitaciones(txtEmpleadoC.Text, dtpFechaIni.Text, dtpFechaFin.Text, cbxEstado.Text);
                dtgListaCapacitacion.DataSource = null;
                dgvListaCapacitacionVista.Columns.Clear();
                dtgListaCapacitacion.DataSource = dtListaDocumentos;
                if (dtListaDocumentos.Rows.Count > 0)
                {
                    dgvListaCapacitacionVista.Columns["EXTENSION"].Visible = false;
                    dgvListaCapacitacionVista.Columns["Persona"].Visible = false;
                    dgvListaCapacitacionVista.Columns["Proveedor"].Visible = false;

                    dgvListaCapacitacionVista.Columns["INICIO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaCapacitacionVista.Columns["INICIO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaCapacitacionVista.Columns["FIN"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaCapacitacionVista.Columns["FIN"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaCapacitacionVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaCapacitacionVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvListaCapacitacionVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaCapacitacionVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvListaCapacitacionVista.Columns["VISTA PREVIA"].Summary.Clear();
                    dgvListaCapacitacionVista.Columns["VISTA PREVIA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "VISTA PREVIA", "Total = {0}");

                    dgvListaCapacitacionVista.BestFitColumns();
                }
            }
        }

        private void txtEmpleadoC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCapacitaciones(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCapacitaciones(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCapacitaciones(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarCapacitaciones(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaCapacitacion.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Convenios de Capacitación - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaCapacitacion.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            Nro = 0;
            Opcion = 1;
            pImportarDocumentos.Location = new System.Drawing.Point(573, 263);
            pImportarDocumentos.Visible = true;
            pImportarDocumentos.BringToFront();

            btnNuevoRegistro.Enabled = false;
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
            Nro = 0;
            Persona = -1;
            Proveedor = -1;

            txtRutaLocal.Clear();
            txtTitulo.Clear();
            txtExtension.Clear();
            txtEmpleado.Clear();
            txtProveedor.Clear();
            txtDuracion.Clear();
            txtMonto.Clear();

            pImportarDocumentos.Visible = false;
            pImportarDocumentos.SendToBack();

            btnNuevoRegistro.Enabled = true;
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(op.FileName))
                {
                    txtRutaLocal.Text = op.FileName;
                    txtTitulo.Text = Path.GetFileNameWithoutExtension(op.FileName);
                    txtExtension.Text = Path.GetExtension(op.FileName);

                    txtDuracion.Focus();
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarLocal_Click(object sender, EventArgs e)
        {
            txtRutaLocal.Clear();
            txtExtension.Clear();
        }

        private void txtEmpleado_Enter(object sender, EventArgs e)
        {
            ListaP = 1;
            lstPersona.Location = new System.Drawing.Point(88, 70);
        }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersona, clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Capacitaciones_ListarEmpleados(1, txtEmpleado.Text), true, false, false);
            lstPersona.Columns[0].Width = 0;
            lstPersona.Columns[1].Width = 250;
            lstPersona.Columns[2].Width = 80;
            lstPersona.BringToFront();
            lstPersona.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                lstPersona.Visible = false;
                lstPersona.SendToBack();
            }
        }

        private void txtEmpleado_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersona.Focus(); }
        }

        private void txtProveedor_Enter(object sender, EventArgs e)
        {
            ListaP = 2;
            lstPersona.Location = new System.Drawing.Point(88, 105);
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersona, clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Capacitaciones_ListarEmpleados(2, txtProveedor.Text), true, false, false);
            lstPersona.Columns[0].Width = 0;
            lstPersona.Columns[1].Width = 250;
            lstPersona.Columns[2].Width = 80;
            lstPersona.BringToFront();
            lstPersona.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Proveedor = -1;
                lstPersona.Visible = false;
                lstPersona.SendToBack();
            }
        }

        private void txtProveedor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersona.Focus(); }
        }

        private void lstPersona_Enter(object sender, EventArgs e)
        {
            if (!lstPersona.Items.Count.Equals(0)) { lstPersona.Items[0].Selected = true; }
        }

        private void lstPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersona.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersona.SelectedItems[0];

                if (ListaP == 1)
                {
                    Persona = Int32.Parse(ItemActual.Text);
                    txtEmpleado.Text = ItemActual.SubItems[1].Text;
                }

                if (ListaP == 2)
                {
                    Proveedor = Int32.Parse(ItemActual.Text);
                    txtProveedor.Text = ItemActual.SubItems[1].Text;
                    btnBuscarArchivo.Focus();
                }

                lstPersona.Visible = false;
                lstPersona.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                if (ListaP == 1) { Persona = -1; }
                if (ListaP == 2) { Proveedor = -1; }

                lstPersona.Visible = false;
                lstPersona.SendToBack();
            }
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersona.SelectedItems[0];

            if (ListaP == 1)
            {
                Persona = Int32.Parse(ItemActual.Text);
                txtEmpleado.Text = ItemActual.SubItems[1].Text;
            }

            if (ListaP == 2)
            {
                Proveedor = Int32.Parse(ItemActual.Text);
                txtProveedor.Text = ItemActual.SubItems[1].Text;
                btnBuscarArchivo.Focus();
            }

            lstPersona.Visible = false;
            lstPersona.SendToBack();
        }

        private void txtDuracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpInicio.Focus(); }
        }

        private void dtpInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtMonto.Focus(); }
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('-'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar.Focus(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtRutaLocal.Text.Length == 0)
            {
                MessageBox.Show("Por favor, seleccione un documento a registrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtEmpleado.Text.Length == 0 || txtProveedor.Text.Length == 0 || txtDuracion.Text.Length == 0 || txtMonto.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtEmpleado.Text.Length == 0) { txtEmpleado.Focus(); }
                else
                {
                    if (txtProveedor.Text.Length == 0) { txtProveedor.Focus(); }
                    else
                    {
                        if (txtDuracion.Text.Length == 0) { txtDuracion.Focus(); }
                        else { txtMonto.Focus(); }
                    }
                }

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
                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Capacitaciones_RegistrarEditarEliminarCapacitacion(Opcion, Nro, Persona, Proveedor,
                                                            archivoBytes, txtTitulo.Text, txtExtension.Text, Convert.ToInt32(txtDuracion.Text), dtpInicio.Value,
                                                            Convert.ToDecimal(txtMonto.Text), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    ListarCapacitaciones();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgListaCapacitacion_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Nro = dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "NRO").ToString();

                if (Nro != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsModificarVersion.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsAnularVersion.Enabled = true; }
                }
                else
                {
                    tsModificarVersion.Enabled = false;
                    tsAnularVersion.Enabled = false;
                }
            }
            catch
            {
                tsModificarVersion.Enabled = false;
                tsAnularVersion.Enabled = false;
            }
        }

        private void tsModificarVersion_Click(object sender, EventArgs e)
        {
            Opcion = 2;
            btnNuevoRegistro.Enabled = false;
            pImportarDocumentos.Location = new System.Drawing.Point(573, 263);

            Nro = Convert.ToInt32(dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "NRO"));
            Persona = Convert.ToInt32(dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "Persona"));
            txtEmpleado.Text = dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "EMPLEADO").ToString();
            Proveedor = Convert.ToInt32(dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "Proveedor"));
            txtProveedor.Text = dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "NOMBRE_PROVEEDOR").ToString();
            txtTitulo.Text = dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "DOCUMENTO").ToString();
            txtExtension.Text = dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "EXTENSION").ToString();
            txtDuracion.Text = dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "DURACION").ToString();
            dtpInicio.Value = Convert.ToDateTime(dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "INICIO"));
            txtMonto.Text = dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "MONTO").ToString();

            pImportarDocumentos.Visible = true;
            pImportarDocumentos.BringToFront();
        }

        private void tsAnularVersion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este registro?", "ELIMINAR CONVENIO CAPACITACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int Nro2 = Convert.ToInt32(dgvListaCapacitacionVista.GetRowCellValue(dgvListaCapacitacionVista.FocusedRowHandle, "NRO"));
                byte[] archivoBytes = null;

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Capacitaciones_RegistrarEditarEliminarCapacitacion(3, Nro2, 0, 0,
                                                            archivoBytes, "", "", 0, DateTime.Now, 0.00m, "");
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0") { ListarCapacitaciones(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgvListaCapacitacionVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "DESCARGAR")
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

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (Convert.ToString(e.CellValue) == "EN CURSO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "CONCLUIDO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void dgvListaCapacitacionVista_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName == "VISTA PREVIA")
            {
                var view = (GridView)sender;
                var row = view.GetDataRow(e.RowHandle);
                if (row == null) return;

                if (!string.Equals(Convert.ToString(row["DESCARGAR"]), "Descargar", StringComparison.OrdinalIgnoreCase)) return;

                int idNro3 = Convert.ToInt32(row["NRO"]);

                DataTable dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_BuscarDocumentos(3, idNro3);

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

                string titulo = Convert.ToString(row["DOCUMENTO"]);
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

            if (e.Column.FieldName == "DESCARGAR")
            {
                var view = (GridView)sender;
                var row = view.GetDataRow(e.RowHandle);
                if (row == null) return;

                if (!string.Equals(Convert.ToString(row["DESCARGAR"]), "Descargar", StringComparison.OrdinalIgnoreCase)) return;

                int idNro3 = Convert.ToInt32(row["NRO"]);

                DataTable dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_BuscarDocumentos(3, idNro3);

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

                string titulo = Convert.ToString(row["DOCUMENTO"]);
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
    }
}
