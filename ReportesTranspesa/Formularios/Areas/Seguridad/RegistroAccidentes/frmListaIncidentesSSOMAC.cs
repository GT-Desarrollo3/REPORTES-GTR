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
using DevExpress.Utils;
using System.Drawing.Imaging;
using System.IO;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;

namespace ReportesTranspesa.Formularios.Areas.Seguridad.RegistroAccidentes
{
    public partial class frmListaIncidentesSSOMAC : MetroFramework.Forms.MetroForm
    {
        public DataTable dtListaIncidencias = new DataTable();
        DataTable dtPermisos = new DataTable();
        public int xClick = 0, yClick = 0;
        public int posx = 0, posy = 0;

        public frmListaIncidentesSSOMAC()
        {
            InitializeComponent();
            cbxIncidente.SelectedIndexChanged -= cbxTipoDanio_SelectedIndexChanged;
            cbxSede.SelectedIndexChanged -= cbxSede_SelectedIndexChanged;
        }

        private void cbxTipoDanio_SelectedIndexChanged(object sender, EventArgs e) { CargarComboIncidente(); }

        private void cbxSede_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSede(); }

        private void frmListaIncidentesSSOMAC_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaIncidentesSSOMAC");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    btnNuevoIncidente.Enabled = true;
                    tsActualizarReporte.Enabled = true;
                    tsSubirDescargo.Enabled = true;
                }
                else
                {
                    btnNuevoIncidente.Enabled = false;
                    tsActualizarReporte.Enabled = false;
                    tsSubirDescargo.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsIngresarPresupuesto.Enabled = true; }
                else { tsIngresarPresupuesto.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarReporte.Enabled = true; }
                else { tsEliminarReporte.Enabled = false; }
            }

            dtpFechaIni.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            CargarComboIncidente();
            CargarComboSede();
            ListarIncidencias();
        }


        public void CargarComboIncidente()
        {
            DataTable dtIncidente = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ListarAccidentes(2);
            cbxIncidente.DataSource = dtIncidente;
            cbxIncidente.DisplayMember = "Descripcion";
            cbxIncidente.ValueMember = "idAccidente";
        }

        public void CargarComboSede()
        {
            DataTable dtSede = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_BuscarSede(Utilitario.Instancia.SesionUsuario.usuario, 2);
            cbxSede.DataSource = dtSede;
            cbxSede.DisplayMember = "Descripcion";
            cbxSede.ValueMember = "Sucursal";
        }

        public void ListarIncidencias()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaIncidencias = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ListarIncidencias(cbxIncidente.Text, cbxSede.Text, dtpFechaIni.Text, dtpFechaFin.Text);
                dtgIncidentesSeg.DataSource = dtListaIncidencias;
                if (dtListaIncidencias.Rows.Count > 0)
                {
                    dgvIncidentesSegVista.Columns["Incidente"].Visible = false;
                    dgvIncidentesSegVista.Columns["IAdicional"].Visible = false;
                    dgvIncidentesSegVista.Columns["TipoIncidente"].Visible = false;
                    dgvIncidentesSegVista.Columns["idPersona"].Visible = false;
                    dgvIncidentesSegVista.Columns["RutaLocal"].Visible = false;

                    dgvIncidentesSegVista.Columns["FECHA_OCURRENCIA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvIncidentesSegVista.Columns["FECHA_OCURRENCIA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvIncidentesSegVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvIncidentesSegVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvIncidentesSegVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvIncidentesSegVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvIncidentesSegVista.Columns["TIPO_INCIDENTE"].Summary.Clear();
                    dgvIncidentesSegVista.Columns["TIPO_INCIDENTE"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TIPO_INCIDENTE", "Total = {0}");

                    dgvIncidentesSegVista.BestFitColumns();
                }
            }
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

        public void InsertarTablaMarcador(Word.Document doc, string MarcadorNombre, DevExpress.XtraGrid.Views.Grid.GridView gv)
        {
            if (!doc.Bookmarks.Exists(MarcadorNombre)) return;

            var columnasVisibles = gv.VisibleColumns.OrderBy(c => c.VisibleIndex).ToList();
            int colCount = columnasVisibles.Count;
            int rowCount = gv.RowCount;

            Word.Range rangoTabla = doc.Bookmarks[MarcadorNombre].Range;

            // Creamos la tabla en Word: +1 fila para encabezados
            Word.Table tbl = doc.Tables.Add(rangoTabla, rowCount + 1, colCount);
            tbl.Borders.Enable = 1; // bordes simples
            tbl.Rows[1].Range.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray20;
            tbl.Rows[1].Range.Bold = 0;

            for (int c = 0; c < colCount; c++)
            {
                string headerText = columnasVisibles[c].Caption;

                // fallback por si la columna no tiene Caption
                if (string.IsNullOrWhiteSpace(headerText))
                    headerText = columnasVisibles[c].FieldName;

                Word.Range headerCellRange = tbl.Cell(1, c + 1).Range;
                headerCellRange.Text = headerText ?? "";
                headerCellRange.Bold = 0; // aseguramos: sin negrita
                headerCellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                headerCellRange.ParagraphFormat.SpaceAfter = 3;
            }

            // 5. Celdas de datos
            for (int r = 0; r < rowCount; r++)
            {
                for (int c = 0; c < colCount; c++)
                {
                    object valor = gv.GetRowCellValue(r, columnasVisibles[c]);
                    string textoCelda = valor == null ? "" : valor.ToString();

                    Word.Range dataCellRange = tbl.Cell(r + 2, c + 1).Range;
                    dataCellRange.Text = textoCelda;
                    dataCellRange.Bold = 0;

                    // alineación numérica si la columna es decimal / numérica
                    if (gv.Columns[columnasVisibles[c].FieldName].ColumnType == typeof(decimal) ||
                        gv.Columns[columnasVisibles[c].FieldName].ColumnType == typeof(double) ||
                        gv.Columns[columnasVisibles[c].FieldName].ColumnType == typeof(float) ||
                        gv.Columns[columnasVisibles[c].FieldName].ColumnType == typeof(int))
                    { tbl.Cell(r + 2, c + 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight; }
                    else
                    { tbl.Cell(r + 2, c + 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft; }

                    dataCellRange.ParagraphFormat.SpaceAfter = 3;
                }
            }

            object rangeAfterTable = tbl.Range;
            doc.Bookmarks.Add(MarcadorNombre, ref rangeAfterTable);
        }


        private void btnNuevoIncidente_Click(object sender, EventArgs e)
        {
            frmRegistrarIncidenteSSOMAC frmRegistrarIncidenteSSOMAC = new frmRegistrarIncidenteSSOMAC();
            frmRegistrarIncidenteSSOMAC.Opcion = 1;
            frmRegistrarIncidenteSSOMAC.idRegistroInc = 0;
            frmRegistrarIncidenteSSOMAC.CargarComboArea();
            frmRegistrarIncidenteSSOMAC.CargarComboSede();
            frmRegistrarIncidenteSSOMAC.CargarComboOperacion();
            frmRegistrarIncidenteSSOMAC.CargarComboIncidente();
            frmRegistrarIncidenteSSOMAC.rbLeve.Checked = true;
            frmRegistrarIncidenteSSOMAC.rbLeve_Click(sender, e);
            frmRegistrarIncidenteSSOMAC.cbDatosUnidad.Checked = false;
            frmRegistrarIncidenteSSOMAC.cbDatosUnidad_CheckedChanged(sender, e);
            frmRegistrarIncidenteSSOMAC.frmListaIncidentesSSOMAC = this;
            frmRegistrarIncidenteSSOMAC.dtpFechaInicio.Value = DateTime.Now;
            frmRegistrarIncidenteSSOMAC.dtpHoraInicio.Value = DateTime.Now;
            frmRegistrarIncidenteSSOMAC.cbxTipoDanio.Text = "NINGUNO";
            frmRegistrarIncidenteSSOMAC.cbxOperaciones.Text = "LINDLEY";
            frmRegistrarIncidenteSSOMAC.cbxArea.Text = "NINGUNA";
            frmRegistrarIncidenteSSOMAC.cbxSede.Text = "Larrea2";
            frmRegistrarIncidenteSSOMAC.Show(this);
        }

        private void dtgIncidentesSeg_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idIncidente = Convert.ToInt32(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "#"));

                if (idIncidente != 0)
                {
                    tsGenerarReporte.Enabled = true;
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        tsIngresarPresupuesto.Enabled = true;
                        tsSubirDescargo.Enabled = true;
                    }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { tsActualizarReporte.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarReporte.Enabled = true; }
                    tsResumenEstado.Enabled = true; 
                }
                else
                {
                    tsGenerarReporte.Enabled = false;
                    tsIngresarPresupuesto.Enabled = false;
                    tsActualizarReporte.Enabled = false;
                    tsEliminarReporte.Enabled = false;
                    tsResumenEstado.Enabled = false;
                    tsSubirDescargo.Enabled = false;
                }
            }
            catch
            {
                tsGenerarReporte.Enabled = false;
                tsIngresarPresupuesto.Enabled = false;
                tsActualizarReporte.Enabled = false;
                tsEliminarReporte.Enabled = false;
                tsResumenEstado.Enabled = false;
                tsSubirDescargo.Enabled = false;
            }
        }

        private void dtgIncidentesSeg_DoubleClick(object sender, EventArgs e)
        {
            try { tsResumenEstado_Click(sender, e); }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIncidencias(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIncidencias(); }
        }

        private void cbxTipoDanio_DropDownClosed(object sender, EventArgs e) { ListarIncidencias(); }

        private void cbxSede_DropDownClosed(object sender, EventArgs e) { ListarIncidencias(); }

        private void tsActualizarReporte_Click(object sender, EventArgs e)
        {
            frmRegistrarIncidenteSSOMAC frmRegistrarIncidenteSSOMAC = new frmRegistrarIncidenteSSOMAC();
            frmRegistrarIncidenteSSOMAC.Opcion = 2;
            frmRegistrarIncidenteSSOMAC.CargarComboArea();
            frmRegistrarIncidenteSSOMAC.CargarComboSede();
            frmRegistrarIncidenteSSOMAC.CargarComboOperacion();
            frmRegistrarIncidenteSSOMAC.CargarComboIncidente();

            frmRegistrarIncidenteSSOMAC.idRegistroInc = Convert.ToInt32(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "#"));
            frmRegistrarIncidenteSSOMAC.Persona = Convert.ToInt32(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "idPersona"));
            frmRegistrarIncidenteSSOMAC.txtEmpleado.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "NOMBRE_COMPLETO").ToString();
            frmRegistrarIncidenteSSOMAC.cbxArea.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "ÁREA").ToString();
            frmRegistrarIncidenteSSOMAC.cbxSede.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "SEDE").ToString();
            
            if (dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "GRADO").ToString() == "LEVE")
            {
                frmRegistrarIncidenteSSOMAC.rbLeve.Checked = true;
                frmRegistrarIncidenteSSOMAC.rbLeve_Click(sender, e);
            }
            else
            {
                if (dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "GRADO").ToString() == "MODERADA")
                {
                    frmRegistrarIncidenteSSOMAC.rbModerada.Checked = true;
                    frmRegistrarIncidenteSSOMAC.rbModerada_Click(sender, e);
                }
                else
                {
                    frmRegistrarIncidenteSSOMAC.rbGrave.Checked = true;
                    frmRegistrarIncidenteSSOMAC.rbGrave_Click(sender, e);
                }
            }

            frmRegistrarIncidenteSSOMAC.dtpFechaInicio.Value = Convert.ToDateTime(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "FECHA_OCURRENCIA"));
            frmRegistrarIncidenteSSOMAC.dtpHoraInicio.Value = Convert.ToDateTime(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "FECHA_OCURRENCIA"));
            frmRegistrarIncidenteSSOMAC.cbxTipoIncidente.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TIPO_INCIDENTE").ToString();
            frmRegistrarIncidenteSSOMAC.cbxTipoDanio.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TIPO_DAÑO").ToString();
            frmRegistrarIncidenteSSOMAC.txtDIncidente.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "DESCRIPCIÓN").ToString();
            frmRegistrarIncidenteSSOMAC.txtObservacion.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "OBSERVACIÓN").ToString();

            if (dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TRACTO").ToString() == "")
            {
                frmRegistrarIncidenteSSOMAC.cbDatosUnidad.Checked = false;
                frmRegistrarIncidenteSSOMAC.cbDatosUnidad_CheckedChanged(sender, e);
            }
            else
            {
                frmRegistrarIncidenteSSOMAC.cbDatosUnidad.Checked = true;
                frmRegistrarIncidenteSSOMAC.cbDatosUnidad_CheckedChanged(sender, e);
                frmRegistrarIncidenteSSOMAC.txtTracto.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TRACTO").ToString();
                frmRegistrarIncidenteSSOMAC.txtCarreta.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "CARRETA").ToString();
                frmRegistrarIncidenteSSOMAC.cbxOperaciones.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "OPERACIÓN").ToString();
            }

            if (dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "Incidente").ToString() != "")
            {
                frmRegistrarIncidenteSSOMAC.byteArrayImagen = (Byte[])dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "Incidente");
                
                Byte[] byteBLOBData;
                byteBLOBData = (Byte[])dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "Incidente");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                frmRegistrarIncidenteSSOMAC.pbIncidente.Image = x;
                frmRegistrarIncidenteSSOMAC.pbIncidente.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            if (dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "IAdicional").ToString() != "")
            {
                frmRegistrarIncidenteSSOMAC.byteArrayImagen2 = (Byte[])dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "IAdicional");
                
                Byte[] byteBLOBFalla;
                byteBLOBFalla = (Byte[])dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "IAdicional");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBFalla));

                frmRegistrarIncidenteSSOMAC.pbIncidente2.Image = x;
                frmRegistrarIncidenteSSOMAC.pbIncidente2.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            frmRegistrarIncidenteSSOMAC.frmListaIncidentesSSOMAC = this;
            frmRegistrarIncidenteSSOMAC.Show(this);
        }

        private void tsEliminarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este incidente?", "ELIMINAR INCIDENTE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idIncidente = Convert.ToInt32(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "#"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_InsertarModificarIncidencias(3, idIncidente, 0, "", "", "", "", "", "", 0,
                                                                                                     "", "", "", "", "", "", null, null, Utilitario.Instancia.SesionUsuario.usuario);
                    string respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = respta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarIncidencias(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("El incidente seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarIncidencias(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgIncidentesSeg.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE INCIDENTES DE SEGURIDAD - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgIncidentesSeg.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void tsGenerarReporte_Click(object sender, EventArgs e)
        {
            if (dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "Incidente").ToString() != "")
            {
                Byte[] byteBLOBData;
                byteBLOBData = (Byte[])dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "Incidente");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                pbIncidenteS.Image = x;
                pbIncidenteS.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            if (dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "IAdicional").ToString() != "")
            {
                Byte[] byteBLOBFalla;
                byteBLOBFalla = (Byte[])dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "IAdicional");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBFalla));

                pbIncidenteS2.Visible = true;
                pbIncidenteS2.Image = x;
                pbIncidenteS2.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else { pbIncidenteS2.Visible = false; }

            txtEmpleado.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "NOMBRE_COMPLETO").ToString();
            txtArea.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "ÁREA").ToString();
            txtSede.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "SEDE").ToString();
            txtGrado.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "GRADO").ToString();
            txtFechaOcurrencia.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "FECHA_OCURRENCIA").ToString();
            txtTipoIncidente.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TIPO_INCIDENTE").ToString();
            txtTipoDanio.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TIPO_DAÑO").ToString();
            txtTracto.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TRACTO").ToString();
            txtCarreta.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "CARRETA").ToString();
            txtOperacion.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "OPERACIÓN").ToString();
            txtDescripcion.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "DESCRIPCIÓN").ToString();
            txtObservacion.Text = dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "OBSERVACIÓN").ToString();

            pbIncidenteS.SendToBack();
            pbImagen.SendToBack();
            pFlashReport.Visible = true;
            pFlashReport.BringToFront();
            pbIncidenteS2.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pFlashReport.Visible = false;
            pFlashReport.SendToBack();
            pbIncidenteS.Image = null;
            pbIncidenteS2.Image = null;

            txtEmpleado.Clear();
            txtArea.Clear();
            txtSede.Clear();
            txtGrado.Clear();
            txtFechaOcurrencia.Clear();
            txtTipoIncidente.Clear();
            txtTipoDanio.Clear();
            txtTracto.Clear();
            txtCarreta.Clear();
            txtOperacion.Clear();
            txtDescripcion.Clear();
            txtObservacion.Clear();
        }

        private void pFlashReport_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { posx = e.X; posy = e.Y; }
            else
            {
                pFlashReport.Left = pFlashReport.Left + (e.X - posx);
                pFlashReport.Top = pFlashReport.Top + (e.Y - posy);
            }
        }

        private void btnGuardarImagen_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Png Image (.png)|*.png|JPG Image (.jpg)|*.jpg|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (var bmp = new Bitmap(pReporteImagen.Width, pReporteImagen.Height))
                {
                    pbIncidenteS.BringToFront();
                    pbImagen.BringToFront();
                    pReporteImagen.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    btnCerrar2_Click(sender, e);
                }
            }
        }

        private void btnImportarWord_Click(object sender, EventArgs e)
        {
            string plantilla = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "INFORME DE INCIDENCIAS.docx");

            Word.Application wordApp = new Word.Application();
            wordApp.Visible = false;
            Word.Document doc = null;

            try
            {
                // 1. Abrir la plantilla
                doc = wordApp.Documents.Open(plantilla);

                // 2. Función local para setear texto en un marcador
                InsertarMarcador(doc, "Nombre", txtEmpleado.Text);
                InsertarMarcador(doc, "Area", txtArea.Text);
                InsertarMarcador(doc, "Sede", txtSede.Text);
                InsertarMarcador(doc, "Grado", txtGrado.Text);
                InsertarMarcador(doc, "FechaI", txtFechaOcurrencia.Text);
                InsertarMarcador(doc, "TipoI", txtTipoIncidente.Text);
                InsertarMarcador(doc, "TipoD", txtTipoDanio.Text);
                InsertarMarcador(doc, "Tracto", txtTracto.Text);
                InsertarMarcador(doc, "Carreta", txtCarreta.Text);
                InsertarMarcador(doc, "Operacion", txtOperacion.Text);
                InsertarMarcador(doc, "Descripcion", txtDescripcion.Text);
                InsertarMarcador(doc, "Observacion", txtObservacion.Text);

                // 3. Insertar imágenes en los marcadores
                InsertarImagenMarcador(doc, "Imagen1", pbIncidenteS);

                if (pbIncidenteS2.Visible && pbIncidenteS2.Image != null)
                { InsertarImagenMarcador(doc, "Imagen2", pbIncidenteS2); }

                int idIncidencia = Convert.ToInt32(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "#"));
                decimal IGV, MontoTotal;
                DataTable dtListaPresupuestos = new DataTable();

                dtListaPresupuestos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(5, idIncidencia);
                dtgListaPresupuesto.DataSource = dtListaPresupuestos;
                if (dtListaPresupuestos.Rows.Count > 0)
                {
                    dgvListaPresupuestoView.Columns["idRegistroInc"].Visible = false;
                    dgvListaPresupuestoView.BestFitColumns();

                    dgvListaPresupuestoView.Columns["IMPORTE_TOTAL"].Summary.Clear();
                    dgvListaPresupuestoView.Columns["IMPORTE_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "IMPORTE_TOTAL", "{0:N2}");

                    txtSubtotal.Text = Convert.ToString(dgvListaPresupuestoView.Columns["IMPORTE_TOTAL"].SummaryText);
                    IGV = Math.Round((Convert.ToDecimal(txtSubtotal.Text) * 0.18M), 2);
                    txtIGV.Text = Convert.ToString(IGV);
                    MontoTotal = IGV + Convert.ToDecimal(txtSubtotal.Text);
                    txtMontoTotal.Text = Convert.ToString(MontoTotal);

                    InsertarTablaMarcador(doc, "Presupuesto", dgvListaPresupuestoView);
                }
                else
                {
                    txtSubtotal.Text = "0.00";
                    txtIGV.Text = "0.00";
                    txtMontoTotal.Text = "0.00";
                }

                InsertarMarcador(doc, "SubTotal", txtSubtotal.Text);
                InsertarMarcador(doc, "IGVTotal", txtIGV.Text);
                InsertarMarcador(doc, "TotalFinal", txtMontoTotal.Text);

                // 4. Guardar como nuevo archivo
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string destino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "ReporteIncidente_" + txtTracto.Text + "_" + DateTime.Now.ToString("T", dtfi) + ".docx");
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

        private void pbIncidenteS2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { posx = e.X; posy = e.Y; }
            else
            {
                pbIncidenteS2.Left = pbIncidenteS2.Left + (e.X - posx);
                pbIncidenteS2.Top = pbIncidenteS2.Top + (e.Y - posy);
            }
        }

        private void tsIngresarPresupuesto_Click(object sender, EventArgs e)
        {
            frmGenerarPresupuesto frmGenerarPresupuesto = new frmGenerarPresupuesto();
            frmGenerarPresupuesto.formulario3 = this;
            frmGenerarPresupuesto.Opcion = 2;
            frmGenerarPresupuesto.Elimino = 1;
            frmGenerarPresupuesto.idFalla = 0;
            frmGenerarPresupuesto._idIncidencia = Convert.ToInt32(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "#"));
            frmGenerarPresupuesto.txtOperacion2.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "OPERACIÓN"));
            frmGenerarPresupuesto.dtpFechaIncidente.Value = Convert.ToDateTime(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "FECHA_OCURRENCIA"));
            frmGenerarPresupuesto.txtTracto2.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TRACTO"));
            frmGenerarPresupuesto.txtCarreta2.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "CARRETA"));
            frmGenerarPresupuesto.txtConductor2.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "NOMBRE_COMPLETO"));
            frmGenerarPresupuesto.txtIncidente.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "DESCRIPCIÓN"));
            frmGenerarPresupuesto.txtRutaLocal2.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "RutaLocal"));
            frmGenerarPresupuesto.ShowDialog();
        }

        private void dgvIncidentesSegVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "PENDIENTE") { e.Appearance.BackColor = Color.Yellow; }

                if (e.CellValue.ToString() == "PRESUPUESTADO") { e.Appearance.BackColor = Color.Yellow; }

                if (e.CellValue.ToString() == "APROBADO") { e.Appearance.BackColor = Color.Yellow; }

                if (e.CellValue.ToString() == "RECHAZADO") { e.Appearance.BackColor = Color.FromArgb(255, 192, 192); }

                if (e.CellValue.ToString() == "REVISADO") { e.Appearance.BackColor = Color.Yellow; }

                if (e.CellValue.ToString() == "CERRADO") { e.Appearance.BackColor = Color.Lime; }
            }
        }

        private void tsResumenEstado_Click(object sender, EventArgs e)
        {
            frmResumenEstado frmResumenEstado = new frmResumenEstado();
            frmResumenEstado.frmListaIncidentesSSOMAC = this;
            frmResumenEstado.idRegistroInc = Convert.ToInt32(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "#"));
            frmResumenEstado.txtFechaOcurrencia.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "FECHA_OCURRENCIA"));
            frmResumenEstado.txtTipoIncidente.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TIPO_INCIDENTE"));
            frmResumenEstado.txtEmpleado.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "NOMBRE_COMPLETO"));
            frmResumenEstado.txtArea.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "ÁREA"));
            frmResumenEstado.txtSede.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "SEDE"));
            frmResumenEstado.txtTracto.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TRACTO"));
            frmResumenEstado.txtCarreta.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "CARRETA"));

            string Costo = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "PRECIO_TOTAL"));
            if (Costo == "0.00")
            {
                frmResumenEstado.txtEstadoMtto.Text = "PENDIENTE";
                frmResumenEstado.txtEstadoMtto.BackColor = Color.FromArgb(192, 255, 255);
            }
            else
            {
                frmResumenEstado.txtEstadoMtto.Text = "PRESUPUESTADO";
                frmResumenEstado.txtEstadoMtto.BackColor = Color.FromArgb(128, 255, 128);
            }

            frmResumenEstado.btnBuscar.Enabled = false;
            frmResumenEstado.btnCerrarLocal.Enabled = false;
            frmResumenEstado.btnDescargo.Enabled = false;
            frmResumenEstado.ListarEstado();
            frmResumenEstado.ShowDialog();
        }

        private void tsSubirDescargo_Click(object sender, EventArgs e)
        {
            try
            {
                frmResumenEstado frmResumenEstado = new frmResumenEstado();
                frmResumenEstado.frmListaIncidentesSSOMAC = this;
                frmResumenEstado.idRegistroInc = Convert.ToInt32(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "#"));
                frmResumenEstado.txtFechaOcurrencia.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "FECHA_OCURRENCIA"));
                frmResumenEstado.txtTipoIncidente.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TIPO_INCIDENTE"));
                frmResumenEstado.txtEmpleado.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "NOMBRE_COMPLETO"));
                frmResumenEstado.txtArea.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "ÁREA"));
                frmResumenEstado.txtSede.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "SEDE"));
                frmResumenEstado.txtTracto.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "TRACTO"));
                frmResumenEstado.txtCarreta.Text = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "CARRETA"));

                string Costo = Convert.ToString(dgvIncidentesSegVista.GetRowCellValue(dgvIncidentesSegVista.FocusedRowHandle, "PRECIO_TOTAL"));
                if (Costo == "0.00")
                {
                    frmResumenEstado.txtEstadoMtto.Text = "PENDIENTE";
                    frmResumenEstado.txtEstadoMtto.BackColor = Color.FromArgb(192, 255, 255);
                }
                else
                {
                    frmResumenEstado.txtEstadoMtto.Text = "PRESUPUESTADO";
                    frmResumenEstado.txtEstadoMtto.BackColor = Color.FromArgb(128, 255, 128);
                }

                frmResumenEstado.ListarEstado();
                frmResumenEstado.ShowDialog();
            }
            catch { MessageBox.Show("El registro seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
