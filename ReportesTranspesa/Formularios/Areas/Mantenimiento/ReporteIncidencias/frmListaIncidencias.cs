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
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using System.Drawing.Imaging;
using System.IO;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;
using Word = Microsoft.Office.Interop.Word;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias
{
    public partial class frmListaIncidencias : Form
    {
        public DataTable dtListaIncidencias = new DataTable();
        public DataTable dtIncidencia = new DataTable();
        DataTable dtPermisos = new DataTable();
        public int xClick = 0, yClick = 0;
        public int posx = 0, posy = 0;
        
        public frmListaIncidencias()
        {
            InitializeComponent();
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void frmListaIncidencias_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaIncidencias");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    btnNuevo.Enabled = true;
                    imprimirReporteToolStripMenuItem.Enabled = true;
                    tsEditarReporte.Enabled = true;
                }
                else
                {
                    btnNuevo.Enabled = false;
                    imprimirReporteToolStripMenuItem.Enabled = false;
                    tsEditarReporte.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { ingresarPresupuestoToolStripMenuItem.Enabled = true; }
                else { ingresarPresupuestoToolStripMenuItem.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarReporteToolStripMenuItem.Enabled = true; }
                else { eliminarReporteToolStripMenuItem.Enabled = false; }
            }

            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            CargarComboOperacion();
            cbxOperacion.SelectedValue = 5;
            ListarIncidencias();
        }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperacion.DataSource = dtOperacion;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void ListarIncidencias()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaIncidencias = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ListarIncidencias(txtPlaca.Text, Convert.ToInt32(cbxOperacion.SelectedValue), dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgListaIncidencias.DataSource = dtListaIncidencias;
                if (dtListaIncidencias.Rows.Count > 0)
                {
                    dgvListaIncidenciasVista.Columns["idFalla"].Visible = false;
                    dgvListaIncidenciasVista.Columns["Descripcion"].Visible = false;
                    dgvListaIncidenciasVista.Columns["TipoDanio"].Visible = false;
                    dgvListaIncidenciasVista.Columns["DescripcionDanio"].Visible = false;
                    dgvListaIncidenciasVista.Columns["Danio"].Visible = false;
                    dgvListaIncidenciasVista.Columns["Observacion"].Visible = false;
                    dgvListaIncidenciasVista.Columns["Telefono"].Visible = false;
                    dgvListaIncidenciasVista.Columns["Documento"].Visible = false;
                    dgvListaIncidenciasVista.Columns["RUTA"].Visible = false;
                    dgvListaIncidenciasVista.Columns["Recursos"].Visible = false;
                    dgvListaIncidenciasVista.Columns["EstadoUnidad"].Visible = false;
                    dgvListaIncidenciasVista.Columns["CLIENTE"].Visible = false;
                    dgvListaIncidenciasVista.Columns["Imagen"].Visible = false;
                    dgvListaIncidenciasVista.Columns["TipoFalla"].Visible = false;
                    dgvListaIncidenciasVista.Columns["Falla"].Visible = false;
                    dgvListaIncidenciasVista.Columns["RutaLocal"].Visible = false;

                    dgvListaIncidenciasVista.Columns["INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaIncidenciasVista.Columns["INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaIncidenciasVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaIncidenciasVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    dgvListaIncidenciasVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaIncidenciasVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    
                    dgvListaIncidenciasVista.Columns["PRECIO_TOTAL"].Summary.Clear();
                    dgvListaIncidenciasVista.Columns["PRECIO_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PRECIO_TOTAL", "Total = {0:N2}");

                    dgvListaIncidenciasVista.BestFitColumns();
                    
                    //RepositoryItemHyperLinkEdit InformeSeguridad = new RepositoryItemHyperLinkEdit();
                    //dgvListaIncidenciasVista.Columns["INFORME_SEGURIDAD"].ColumnEdit = InformeSeguridad;
                    //dgvListaIncidenciasVista.Columns["INFORME_SEGURIDAD"].Width = 200;
                }
            }
        }

        public void cargarImagen()
        {
            if (dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "Imagen").ToString() != "")
            {
                Byte[] byteBLOBData;
                byteBLOBData = (Byte[])dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "Imagen");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                pbImagenRuta.Image = x;
                pbImagenRuta.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            if (dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "Falla").ToString() != "")
            {
                Byte[] byteBLOBFalla;
                byteBLOBFalla = (Byte[])dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "Falla");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBFalla));

                pbFalla.Visible = true;
                pbFalla.Image = x;
                pbFalla.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else { pbFalla.Visible = false; }
        }

        public void InsertarMarcador(Word.Document doc, string MarcadorNombre, string Valor)
        {
            if (doc.Bookmarks.Exists(MarcadorNombre))
            {
                Word.Range r = doc.Bookmarks[MarcadorNombre].Range;
                r.Text = Valor ?? "";

                doc.Bookmarks.Add(MarcadorNombre, r);
            }
        }

        public void InsertarImagenMarcador(Word.Document doc, string MarcadorNombre, PictureBox pb)
        {
            if (pb.Image == null) return;
            if (!doc.Bookmarks.Exists(MarcadorNombre)) return;

            string tempPath = Path.Combine(Path.GetTempPath(), MarcadorNombre + ".jpg");
            pb.Image.Save(tempPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            Word.Range r = doc.Bookmarks[MarcadorNombre].Range;
            Word.InlineShape pic = r.InlineShapes.AddPicture(tempPath);
            pic.Width = 250;
            pic.Height = 150;

            Word.Range newRange = pic.Range;
            doc.Bookmarks.Add(MarcadorNombre, newRange);
        }


        private void btnNuevo_Click(object sender, EventArgs e)
        {
            frmNuevaIncidencia frmNuevaIncidencia = new frmNuevaIncidencia();
            frmNuevaIncidencia.formulario = this;
            frmNuevaIncidencia.ShowDialog();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaIncidencias.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE INCIDENCIAS Y SINIESTROS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaIncidencias.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarIncidencias(); }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIncidencias(); }
        }

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e) { ListarIncidencias(); }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIncidencias(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIncidencias(); }
        }

        private void dtgListaIncidencias_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idIncidenteC = Convert.ToInt32(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "NRO"));

                if (idIncidenteC != 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                    {
                        tsEditarReporte.Enabled = true;
                        imprimirReporteToolStripMenuItem.Enabled = true;
                    }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { ingresarPresupuestoToolStripMenuItem.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarReporteToolStripMenuItem.Enabled = true; }
                }
                else
                {
                    imprimirReporteToolStripMenuItem.Enabled = false;
                    tsEditarReporte.Enabled = false;
                    ingresarPresupuestoToolStripMenuItem.Enabled = false;
                    eliminarReporteToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                imprimirReporteToolStripMenuItem.Enabled = false;
                tsEditarReporte.Enabled = false;
                ingresarPresupuestoToolStripMenuItem.Enabled = false;
                eliminarReporteToolStripMenuItem.Enabled = false;
            }
        }

        private void dtgListaIncidencias_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int idIncidencia = Convert.ToInt32(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "NRO"));

                if (idIncidencia != 0)
                {
                    frmGenerarPresupuesto frmGenerarPresupuesto = new frmGenerarPresupuesto();
                    frmGenerarPresupuesto.formulario = this;
                    frmGenerarPresupuesto.groupBox2.Enabled = false;
                    frmGenerarPresupuesto.btnGuardar.Enabled = false;
                    frmGenerarPresupuesto.btnBuscar.Enabled = false;
                    frmGenerarPresupuesto.btnCerrarLocal.Enabled = false;
                    frmGenerarPresupuesto.Elimino = 0;
                    frmGenerarPresupuesto.idFalla = Convert.ToInt32(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "idFalla"));
                    frmGenerarPresupuesto._idIncidencia = Convert.ToInt32(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "NRO"));
                    frmGenerarPresupuesto.txtOperacion2.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "OPERACION"));
                    frmGenerarPresupuesto.dtpFechaIncidente.Value = Convert.ToDateTime(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "FECHA_INICIO"));
                    frmGenerarPresupuesto.txtTracto2.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "TRACTO"));
                    frmGenerarPresupuesto.txtCarreta2.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "SEMIRREMOLQUE"));
                    frmGenerarPresupuesto.txtConductor2.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "CONDUCTOR"));
                    frmGenerarPresupuesto.txtIncidente.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "MOTIVO"));
                    frmGenerarPresupuesto.txtRutaLocal2.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "RutaLocal"));
                    frmGenerarPresupuesto.ShowDialog();
                }
                else { MessageBox.Show("No se ha asignado ningún presupuesto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch { MessageBox.Show("No se ha asignado ningún presupuesto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void imprimirReporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            cargarImagen();
            txtTipoInc.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "AUXILIO"));
            txtConductor.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "CONDUCTOR"));
            txtCelular.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "Telefono"));
            txtRPlaca.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "TRACTO"));
            txtRCarreta.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "SEMIRREMOLQUE"));
            txtEstadoUnidad.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "EstadoUnidad"));
            txtCliente.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "CLIENTE"));
            txtRuta.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "RUTA"));
            txtLugar.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "UBICACIÓN"));
            txtGPS.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "GPS"));
            txtFecha.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "FECHA_INICIO"));
            txtFechaRep.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "FechaCreacion"));
            txtIncidente.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "Descripcion"));
            txtDanio.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "TipoDanio"));
            txtDescripcionDanio.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "DescripcionDanio"));
            txtFechaMtto.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "INSPECCION"));
            txtObservacion.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "Observacion"));

            pbImagenRuta.SendToBack();
            pbImagen.SendToBack();
            pFlashReport.Visible = true;
            pFlashReport.BringToFront();
            pbFalla.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pFlashReport.Visible = false;
            pFlashReport.SendToBack();
            pbImagenRuta.Image = null;
            pbFalla.Image = null;

            txtTipoInc.Clear();
            txtConductor.Clear();
            txtCelular.Clear();
            txtRPlaca.Clear();
            txtRCarreta.Clear();
            txtEstadoUnidad.Clear();
            txtCliente.Clear();
            txtRuta.Clear();
            txtLugar.Clear();
            txtGPS.Clear();
            txtFecha.Clear();
            txtFechaRep.Clear();
            txtIncidente.Clear();
            txtDanio.Clear();
            txtFechaMtto.Clear();
            txtDescripcionDanio.Clear();
            txtObservacion.Clear();
        }

        private void ingresarPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGenerarPresupuesto frmGenerarPresupuesto = new frmGenerarPresupuesto();
            frmGenerarPresupuesto.formulario = this;
            frmGenerarPresupuesto.Opcion = 1;
            frmGenerarPresupuesto.Elimino = 1;
            frmGenerarPresupuesto.idFalla = Convert.ToInt32(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "idFalla"));
            frmGenerarPresupuesto._idIncidencia = Convert.ToInt32(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "NRO"));
            frmGenerarPresupuesto.txtOperacion2.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "OPERACION"));
            frmGenerarPresupuesto.dtpFechaIncidente.Value = Convert.ToDateTime(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "FECHA_INICIO"));
            frmGenerarPresupuesto.txtTracto2.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "TRACTO"));
            frmGenerarPresupuesto.txtCarreta2.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "SEMIRREMOLQUE"));
            frmGenerarPresupuesto.txtConductor2.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "CONDUCTOR"));
            frmGenerarPresupuesto.txtIncidente.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "MOTIVO"));
            frmGenerarPresupuesto.txtRutaLocal2.Text = Convert.ToString(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "RutaLocal"));
            frmGenerarPresupuesto.ShowDialog();
        }

        private void eliminarReporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este reporte?", "ELIMINAR REPORTE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idIncidencia = Convert.ToInt32(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "NRO"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(2, idIncidencia);
                    string respta = Convert.ToString(dtRespuesta.Rows[0]["Mensaje"]);
                    string NroRPTA = respta.Substring(0, 1);
                    if (NroRPTA == "0")
                    { MessageBox.Show("Esta incidencia está siendo tratada en el área de Mtto. No puede ser eliminada.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    ListarIncidencias();
                }
            }
            catch { MessageBox.Show("El archivo seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pFlashReport_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pFlashReport.Left = pFlashReport.Left + (e.X - xClick);
                pFlashReport.Top = pFlashReport.Top + (e.Y - yClick);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Png Image (.png)|*.png|JPG Image (.jpg)|*.jpg|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (var bmp = new Bitmap(pReporteImagen.Width, pReporteImagen.Height))
                {
                    pbImagenRuta.BringToFront();
                    pbImagen.BringToFront();
                    pReporteImagen.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    btnCerrar2_Click(sender, e);
                }
            }
        }

        private void btnImportarWord_Click(object sender, EventArgs e)
        {
            string plantilla = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REPORTE DE INCIDENCIAS.docx");

            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            Word.Document doc = null;

            try
            {
                doc = wordApp.Documents.Open(plantilla);
                InsertarMarcador(doc, "TipoEvento", txtTipoInc.Text);
                InsertarMarcador(doc, "Conductor", txtConductor.Text);
                InsertarMarcador(doc, "Celular", txtCelular.Text);
                InsertarMarcador(doc, "Tracto", txtRPlaca.Text);
                InsertarMarcador(doc, "Carreta", txtRCarreta.Text);
                InsertarMarcador(doc, "EstadoUnidad", txtEstadoUnidad.Text);
                InsertarMarcador(doc, "Cliente", txtCliente.Text);
                InsertarMarcador(doc, "Ruta", txtRuta.Text);
                InsertarMarcador(doc, "Ubicacion", txtLugar.Text);
                InsertarMarcador(doc, "GPS", txtGPS.Text);
                InsertarMarcador(doc, "FechaEvento", txtFecha.Text);
                InsertarMarcador(doc, "FechaReporte", txtFechaRep.Text);
                InsertarMarcador(doc, "Descripcion", txtIncidente.Text);
                InsertarMarcador(doc, "DescripcionDanio", txtDescripcionDanio.Text);
                InsertarMarcador(doc, "Reporte", txtObservacion.Text);
                cargarImagen();
                InsertarImagenMarcador(doc, "ImagenRuta", pbImagenRuta);
                InsertarImagenMarcador(doc, "ImagenFalla", pbFalla);

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string destino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "FlashReport_" + Utilitario.Instancia.SesionUsuario.usuario + "_" + DateTime.Now.ToString("T", dtfi) + ".docx");
                doc.SaveAs2(destino);
                doc.Close();
                wordApp.Quit();
                MessageBox.Show("Documento generado:\n" + destino, "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start(destino);
            }
            catch (Exception ex)
            {
                if (doc != null) { doc.Close(false); }
                wordApp.Quit(false);
                MessageBox.Show("Error exportando a Word: " + ex.Message);
            }
        }

        private void pbFalla_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { posx = e.X; posy = e.Y; }
            else
            {
                pbFalla.Left = pbFalla.Left + (e.X - posx);
                pbFalla.Top = pbFalla.Top + (e.Y - posy);
            }
        }

        private void dgvListaIncidenciasVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO_AUXILIO")
            {
                if (e.CellValue.ToString() == "EN COORDINACIÓN") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (e.CellValue.ToString() == "EN RUTA") { e.Appearance.BackColor = Color.FromArgb(255, 128, 0); }

                if (e.CellValue.ToString() == "EN ATENCIÓN") { e.Appearance.BackColor = Color.FromArgb(255, 255, 0); }

                if (e.CellValue.ToString() == "ATENDIDO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void tsEditarReporte_Click(object sender, EventArgs e)
        {
            int idIncidenteC = Convert.ToInt32(dgvListaIncidenciasVista.GetRowCellValue(dgvListaIncidenciasVista.FocusedRowHandle, "NRO"));
            dtIncidencia = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_BuscarIncidencias(idIncidenteC);

            if (dtIncidencia.Rows.Count > 0)
            {
                frmNuevaIncidencia frmNuevaIncidencia = new frmNuevaIncidencia();

                frmNuevaIncidencia.formulario = this;
                frmNuevaIncidencia.CargarComboAuxilio();
                frmNuevaIncidencia.idIncidenteC = Convert.ToInt32(dtIncidencia.Rows[0]["idIncidenteC"]);
                frmNuevaIncidencia.cbxTipoAuxilio.Text = dtIncidencia.Rows[0]["Auxilio"].ToString();
                frmNuevaIncidencia.txtEstadoUnidad.Text = dtIncidencia.Rows[0]["EstadoUnidad"].ToString();
                if (dtIncidencia.Rows[0]["TipoFalla"].ToString() == "PARADA")
                {
                    frmNuevaIncidencia.rbFallaParada.Checked = true;
                    frmNuevaIncidencia.rbFallaParada_Click(sender, e);
                }
                else
                {
                    frmNuevaIncidencia.rbLeve.Checked = true;
                    frmNuevaIncidencia.rbLeve_Click(sender, e);
                }
                frmNuevaIncidencia.dtpFechaInicio.Value = Convert.ToDateTime(dtIncidencia.Rows[0]["FechaInicio"]);
                frmNuevaIncidencia.dtpHoraInicio.Text = Convert.ToString(dtIncidencia.Rows[0]["HoraInicio"]);
                frmNuevaIncidencia.txtUbicacion.Text = dtIncidencia.Rows[0]["Ubicacion"].ToString();
                frmNuevaIncidencia.txtMotivo.Text = dtIncidencia.Rows[0]["Motivo"].ToString();
                frmNuevaIncidencia.txtDIncidente.Text = dtIncidencia.Rows[0]["Descripcion"].ToString();
                frmNuevaIncidencia.cbxTipoDanio.Text = dtIncidencia.Rows[0]["TipoDanio"].ToString();
                frmNuevaIncidencia.txtDanio.Text = dtIncidencia.Rows[0]["Danio"].ToString();
                frmNuevaIncidencia.txtGPS.Text = dtIncidencia.Rows[0]["GPS"].ToString();
                frmNuevaIncidencia.txtRecursos.Text = dtIncidencia.Rows[0]["Recursos"].ToString();
                frmNuevaIncidencia.txtObservacion.Text = dtIncidencia.Rows[0]["Observacion"].ToString();

                if (dtIncidencia.Rows[0]["Imagen"].ToString() != "")
                {
                    frmNuevaIncidencia.byteArrayImagen = (Byte[])dtIncidencia.Rows[0]["Imagen"];

                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dtIncidencia.Rows[0]["Imagen"];
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    frmNuevaIncidencia.pbImagen.Image = x;
                    frmNuevaIncidencia.pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                if (dtIncidencia.Rows[0]["Falla"].ToString() != "")
                {
                    frmNuevaIncidencia.byteArrayFalla = (Byte[])dtIncidencia.Rows[0]["Falla"];

                    Byte[] byteBLOBFalla;
                    byteBLOBFalla = (Byte[])dtIncidencia.Rows[0]["Falla"];
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBFalla));
                    frmNuevaIncidencia.pbFalla.Image = x;
                    frmNuevaIncidencia.pbFalla.SizeMode = PictureBoxSizeMode.StretchImage;
                }

                frmNuevaIncidencia.Show();
            }
        }

        private void btnReporteIncidentes_Click(object sender, EventArgs e)
        {
            frmReporteIncidencias frmReporteIncidencias = new frmReporteIncidencias();
            frmReporteIncidencias.Show(this);
        }
    }
}
