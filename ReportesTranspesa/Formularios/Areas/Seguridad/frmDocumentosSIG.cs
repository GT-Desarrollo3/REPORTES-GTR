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

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmDocumentosSIG : MetroFramework.Forms.MetroForm
    {
        DataTable dtListaDocumentos = new DataTable();
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        int Nro;
        string Area;
        int Opcion = 0;
        public int xClick = 0, yClick = 0;
        
        public frmDocumentosSIG()
        {
            InitializeComponent();
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
            cbxArea2.SelectedIndexChanged -= cbxArea2_SelectedIndexChanged;
            cbxProceso.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
            cbxProceso2.SelectedIndexChanged -= cbxOperacion2_SelectedIndexChanged;
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void cbxArea2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea2(); }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones(); }

        private void cbxOperacion2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones2(); }

        private void frmDocumentosSIG_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmDocumentosSIG");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnSubirDocumento.Enabled = true; }
                else { btnSubirDocumento.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsNuevaVersion.Enabled = true; }
                else { tsNuevaVersion.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarDocumento.Enabled = true; }
                else { tsEliminarDocumento.Enabled = false; }
            }

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            CargarComboArea();
            CargarComboOperaciones2();
            cbxProceso2.Text = "TODO";

            DataTable dtAreaUsuario = new DataTable();
            dtAreaUsuario = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_BuscarUsuarios(Utilitario.Instancia.SesionUsuario.usuario);

            if (dtAreaUsuario.Rows.Count > 0)
            {
                Area = dtAreaUsuario.Rows[0]["AREA"].ToString();
                cbxArea.Text = Area;
                cbxArea.Enabled = false;
            }

            if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
            { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

            if (dtEspeciales != null)
            {
                if (dtEspeciales.Rows.Count > 0)
                {
                    cbxArea.Text = "TODAS";
                    cbxArea.Enabled = true;
                }
            }

            ListarDocumentosSIG();
        }


        private void CargarComboArea()
        {
            DataTable dtArea = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarAreas(1);
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "AREA";
            cbxArea.ValueMember = "CODIGO";
        }

        private void CargarComboArea2()
        {
            DataTable dtArea2 = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarAreas(2);
            cbxArea2.DataSource = dtArea2;
            cbxArea2.DisplayMember = "AREA";
            cbxArea2.ValueMember = "CODIGO";
        }

        private void CargarComboOperaciones()
        {
            DataTable dtOperaciones = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarOperaciones(2);
            cbxProceso.DataSource = dtOperaciones;
            cbxProceso.DisplayMember = "DESCRIPCION";
            cbxProceso.ValueMember = "ID";
        }

        private void CargarComboOperaciones2()
        {
            DataTable dtOperaciones = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarOperaciones(1);
            cbxProceso2.DataSource = dtOperaciones;
            cbxProceso2.DisplayMember = "DESCRIPCION";
            cbxProceso2.ValueMember = "ID";
        }

        public void ListarDocumentosSIG()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaDocumentos = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarDocumentos(txtTitulo.Text, cbxArea.Text, cbxProceso2.Text, "01/01/2020", "31/12/3030");
                dtgDocumentosSIG.DataSource = null;
                dgvDocumentosSIGVista.Columns.Clear();
                dtgDocumentosSIG.DataSource = dtListaDocumentos;
                if (dtListaDocumentos.Rows.Count > 0)
                {
                    dgvDocumentosSIGVista.Columns["EXTENSION"].Visible = false;
                    
                    dgvDocumentosSIGVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvDocumentosSIGVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvDocumentosSIGVista.Columns["DOCUMENTO"].Visible = false;

                    dgvDocumentosSIGVista.Columns["VISTA PREVIA"].Summary.Clear();
                    dgvDocumentosSIGVista.Columns["VISTA PREVIA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ESTADO", "Total = {0}");

                    dgvDocumentosSIGVista.BestFitColumns();
                }
            }
        }


        private void txtTitulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDocumentosSIG(); }
        }

        private void cbxArea_DropDownClosed(object sender, EventArgs e) { ListarDocumentosSIG(); }

        private void cbxProceso2_DropDownClosed(object sender, EventArgs e) { ListarDocumentosSIG(); }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDocumentosSIG(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDocumentosSIG(); }
        }

        private void txtVersion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDocumentosSIG(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarDocumentosSIG(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgDocumentosSIG.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Registro de Documentos SIG - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgDocumentosSIG.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            Nro = 0;
            Opcion = 1;
            CargarComboArea2();
            CargarComboOperaciones();
            pImportarDocumentos.Location = new System.Drawing.Point(181, 263);
            pImportarDocumentos.Visible = true;
            pImportarDocumentos.BringToFront();
            txtVersion2.Text = "1";
            cbxProceso.Text = "LINDLEY";

            btnSubirDocumento.Enabled = false;
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
            txtRutaLocal.Clear();
            txtTitulo2.Clear();
            txtVersion2.Clear();
            txtExtension.Clear();

            pImportarDocumentos.Visible = false;
            pImportarDocumentos.SendToBack();

            btnSubirDocumento.Enabled = true;
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(op.FileName))
                {
                    txtRutaLocal.Text = op.FileName;
                    txtTitulo2.Text = Path.GetFileNameWithoutExtension(op.FileName);
                    txtExtension.Text = Path.GetExtension(op.FileName);

                    txtTitulo2.Focus();
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarLocal_Click(object sender, EventArgs e)
        {
            txtRutaLocal.Clear();
            txtTitulo2.Clear();
            txtExtension.Clear();
        }

        private void txtVersion2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtRutaLocal.Text.Length == 0)
            {
                MessageBox.Show("Por favor, seleccione un documento a registrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            if (txtTitulo2.Text.Length == 0 || txtVersion2.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtTitulo2.Text.Length == 0) { txtTitulo2.Focus(); }
                else { txtVersion2.Focus(); }
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

                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_RegistrarEditarDocumentos(Opcion, Nro, archivoBytes, txtTitulo2.Text,
                                                       cbxArea2.Text, cbxProceso.Text, Convert.ToInt32(txtVersion2.Text), txtExtension.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    ListarDocumentosSIG();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgDocumentosSIG_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Nro = dgvDocumentosSIGVista.GetRowCellValue(dgvDocumentosSIGVista.FocusedRowHandle, "NRO").ToString();

                if (Nro != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsNuevaVersion.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarDocumento.Enabled = true; }
                }
                else
                {
                    tsNuevaVersion.Enabled = false;
                    tsEliminarDocumento.Enabled = false;
                }
            }
            catch
            {
                tsNuevaVersion.Enabled = false;
                tsEliminarDocumento.Enabled = false;
            }
        }

        private void tsNuevaVersion_Click(object sender, EventArgs e)
        {
            Opcion = 2;
            btnSubirDocumento.Enabled = false;
            CargarComboArea2();
            CargarComboOperaciones();
            pImportarDocumentos.Location = new System.Drawing.Point(181, 263);

            Nro = Convert.ToInt32(dgvDocumentosSIGVista.GetRowCellValue(dgvDocumentosSIGVista.FocusedRowHandle, "NRO"));
            txtTitulo2.Text = dgvDocumentosSIGVista.GetRowCellValue(dgvDocumentosSIGVista.FocusedRowHandle, "TITULO").ToString();
            cbxArea2.Text = dgvDocumentosSIGVista.GetRowCellValue(dgvDocumentosSIGVista.FocusedRowHandle, "AREA").ToString();
            int Version = Convert.ToInt32(dgvDocumentosSIGVista.GetRowCellValue(dgvDocumentosSIGVista.FocusedRowHandle, "VERSION")) + 1;
            txtVersion2.Text = Convert.ToString(Version);
            txtExtension.Text = dgvDocumentosSIGVista.GetRowCellValue(dgvDocumentosSIGVista.FocusedRowHandle, "EXTENSION").ToString();
            cbxProceso.Text = dgvDocumentosSIGVista.GetRowCellValue(dgvDocumentosSIGVista.FocusedRowHandle, "PROCESO").ToString();

            pImportarDocumentos.Visible = true;
            pImportarDocumentos.BringToFront();
        }

        private void tsEliminarDocumento_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este registro?", "ELIMINAR DOCUMENTO SIG", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int Nro2 = Convert.ToInt32(dgvDocumentosSIGVista.GetRowCellValue(dgvDocumentosSIGVista.FocusedRowHandle, "NRO"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_EliminarDocumentos(1, Nro2);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0") { ListarDocumentosSIG(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgvDocumentosSIGVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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
        }

        private void dgvDocumentosSIGVista_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            /*if (e.Column.FieldName == "DOCUMENTO")
            {
                var view = (GridView)sender;
                var row = view.GetDataRow(e.RowHandle);
                if (row == null) return;

                if (!string.Equals(Convert.ToString(row["DOCUMENTO"]), "Descargar", StringComparison.OrdinalIgnoreCase)) return;

                int idNro3 = Convert.ToInt32(row["NRO"]);

                //Traer el registro con el varbinary
                DataTable dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_BuscarDocumentos(idNro3);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el archivo.");
                    return;
                }

                // Ajusta los nombres de columnas según tu SP:
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
            }*/

            if (e.Column.FieldName == "VISTA PREVIA")
            {
                var view = (GridView)sender;
                var row = view.GetDataRow(e.RowHandle);
                if (row == null) return;

                if (!string.Equals(Convert.ToString(row["DOCUMENTO"]), "Descargar", StringComparison.OrdinalIgnoreCase)) return;

                int idNro3 = Convert.ToInt32(row["NRO"]);

                //Traer el registro con el varbinary
                DataTable dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_BuscarDocumentos(1, idNro3);

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el archivo.");
                    return;
                }

                // Ajusta los nombres de columnas según tu SP:
                byte[] bytes = dt.Rows[0]["ARCHIVO"] as byte[];

                if (bytes == null || bytes.Length == 0)
                {
                    MessageBox.Show("El archivo está vacío.");
                    return;
                }

                string titulo = Convert.ToString(row["TITULO"]);
                string extension = Convert.ToString(row["EXTENSION"]);

                // Verificar que la extensión sea PDF (opcional)
                if (!string.Equals(extension, ".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("El archivo no es un PDF.");
                    return;
                }

                // Crear archivo temporal y abrir con visor predeterminado
                try
                {
                    string fileName = Guid.NewGuid().ToString() + ".pdf";
                    string tempPath = Path.Combine(Path.GetTempPath(), fileName);
                    File.WriteAllBytes(tempPath, bytes);

                    // Abrir con la app predeterminada del sistema
                    Process.Start(new ProcessStartInfo(tempPath)
                    {
                        UseShellExecute = true
                    });
                }
                catch (Exception ex) { MessageBox.Show("Error al abrir el archivo."); }
            }
        }
    }
}
