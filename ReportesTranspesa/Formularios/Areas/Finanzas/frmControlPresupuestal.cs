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
using System.Xml;
using System.IO;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    public partial class frmControlPresupuestal : MetroFramework.Forms.MetroForm
    {
        public int Documento;
        public DataSet dsTabla;
        public DataSet dsTabla2;
        public DataSet dsTabla3;
        public DataSet dsTabla4;
        DataTable dtListaPR;
        public int Opcion;
        String xmlPresupuesto;
        String CarpetaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"Downloads");
        int CantidadTotal, Cantidad, CantidadTotal2, Cantidad2, xClick = 0, yClick = 0, xClick2 = 0, yClick2 = 0;

        public frmControlPresupuestal()
        {
            InitializeComponent();
            cbxCentroCosto.SelectedIndexChanged -= cbxCentroCosto_SelectedIndexChanged;
        }

        private void cbxCentroCosto_SelectedIndexChanged(object sender, EventArgs e) { CargarCentroCosto(); }

        private void frmControlPresupuestal_Load(object sender, EventArgs e)
        {
            dtpPeriodo.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpResumen1.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpResumen2.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpResumen3.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            CargarCentroCosto();
            rbOrdenServicio.Checked = true;
            rbOrdenServicio_Click(sender, e);
            cbCentroCosto.Checked = false;
            cbCentroCosto_CheckedChanged(sender, e);
            //ListarPresupuesto();
        }


        public void CargarCentroCosto()
        {
            DataTable dtCentroCosto = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarCentroCosto(Utilitario.Instancia.SesionUsuario.usuario);
            cbxCentroCosto.DataSource = dtCentroCosto;
            cbxCentroCosto.DisplayMember = "LocalName";
            cbxCentroCosto.ValueMember = "CostCenter";
        }

        public void IngresarPagina()
        {
            if (Documento == 1)
            {
                if (txtNroPagina.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese un número.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNroPagina.Focus();
                    return;
                }
                else
                {
                    int Pagina = Convert.ToInt32(txtNroPagina.Text);
                    int PagInicio = (Pagina - 1) * 60 + 1;
                    int PagFinal = Pagina * 60;
                    ListarPresupuesto(PagInicio, PagFinal);
                }
            }
            
            if (Documento == 2)
            {
                if (txtNroPagina2.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese un número.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNroPagina2.Focus();
                    return;
                }
                else
                {
                    int Pagina = Convert.ToInt32(txtNroPagina2.Text);
                    int PagInicio = (Pagina - 1) * 60 + 1;
                    int PagFinal = Pagina * 60;
                    ListarPresupuesto(PagInicio, PagFinal);
                }
            }
        }

        public void ListarPresupuesto(int PagInicio, int PagFinal)
        {
            if (Documento == 1)
            {
                btnAvanzar.Enabled = true;
                btnRetroceder.Enabled = true;

                dsTabla = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_Listar(Opcion, dtpPeriodo.Text, Convert.ToString(cbxCentroCosto.SelectedValue), Documento, PagInicio, PagFinal);

                CantidadTotal = Convert.ToInt32(dsTabla.Tables[0].Rows[0][0]);

                dtgOrdenServicio.DataSource = dsTabla.Tables[1];
                dgvOrdenServicioVista.BestFitColumns();

                Cantidad = CantidadTotal / 60;

                if (CantidadTotal % 60 > 0) { Cantidad += 1; }

                label4.Text = "de " + Convert.ToString(Cantidad);
            }

            if (Documento == 2)
            {
                btnAvanzar2.Enabled = true;
                btnRetroceder2.Enabled = true;

                dsTabla2 = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_Listar(Opcion, dtpPeriodo.Text, Convert.ToString(cbxCentroCosto.SelectedValue), Documento, PagInicio, PagFinal);

                CantidadTotal2 = Convert.ToInt32(dsTabla2.Tables[0].Rows[0][0]);

                dtgNotaSalida.DataSource = dsTabla2.Tables[1];
                dgvNotaSalidaVista.BestFitColumns();

                Cantidad2 = CantidadTotal2 / 60;

                if (CantidadTotal2 % 60 > 0) { Cantidad2 += 1; }

                label6.Text = "de " + Convert.ToString(Cantidad2);
            }
        }

        public void ListarCuadroComparativo()
        {
            DataTable dtCuadroComparativo = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarCuadroComparativo(Opcion, dtpPeriodo.Text, Convert.ToString(cbxCentroCosto.SelectedValue));
            dtgCuadroComparativo1.DataSource = dtCuadroComparativo;
            if (dtCuadroComparativo.Rows.Count > 0)
            {
                dgvCuadroComparativo1View.Columns["Group01"].Visible = false;
                dgvCuadroComparativo1View.Columns["Group02"].Visible = false;
                dgvCuadroComparativo1View.Columns["Group03"].Visible = false;

                dgvCuadroComparativo1View.Columns["MONTO"].Summary.Clear();
                dgvCuadroComparativo1View.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                var Convertir = dgvCuadroComparativo1View.Columns["MONTO"].SummaryItem.SummaryValue;
                Decimal Negativo = Convert.ToDecimal(Convertir) * -1;

                dgvCuadroComparativo1View.Columns["MONTO"].Summary.Clear();
                dgvCuadroComparativo1View.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo.ToString("#,##0.00"));

                dgvCuadroComparativo1View.BestFitColumns();
            }

            dtpPeriodo.Value = dtpPeriodo.Value.AddYears(-1);

            DataTable dtCuadroComparativo2 = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarCuadroComparativo(Opcion, dtpPeriodo.Text, Convert.ToString(cbxCentroCosto.SelectedValue));
            dtgCuadroComparativo2.DataSource = dtCuadroComparativo2;
            if (dtCuadroComparativo2.Rows.Count > 0)
            {
                dgvCuadroComparativo2View.Columns["Group01"].Visible = false;
                dgvCuadroComparativo2View.Columns["Group02"].Visible = false;
                dgvCuadroComparativo2View.Columns["Group03"].Visible = false;

                dgvCuadroComparativo2View.Columns["MONTO"].Summary.Clear();
                dgvCuadroComparativo2View.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                var Convertir = dgvCuadroComparativo2View.Columns["MONTO"].SummaryItem.SummaryValue;
                Decimal Negativo = Convert.ToDecimal(Convertir) * -1;

                dgvCuadroComparativo2View.Columns["MONTO"].Summary.Clear();
                dgvCuadroComparativo2View.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo.ToString("#,##0.00"));

                dgvCuadroComparativo2View.BestFitColumns();
            }

            dtpPeriodo.Value = dtpPeriodo.Value.AddYears(1);
        }

        public void CargarArchivo()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.InitialDirectory = CarpetaDestino;
                op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                op.Title = "Archivo.xlsx";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        txtDirectorio.Text = op.FileName;
                        btnGenerar.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        public void ListarPresupuesto()
        {
            DataTable dtListaPR = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarPresupuestos(Opcion, dtpPeriodo.Text, Convert.ToString(cbxCentroCosto.SelectedValue));
            dtgImportarP.DataSource = dtListaPR;
            if (dtListaPR.Rows.Count > 0)
            {
                dgvImportarP.Columns["idPresupuesto"].Visible = false;

                dgvImportarP.Columns["FechaRegistra"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvImportarP.Columns["FechaRegistra"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvImportarP.Columns["IMPORTE"].Summary.Clear();
                dgvImportarP.Columns["IMPORTE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "IMPORTE", "Total: {0:N2}");

                dgvImportarP.BestFitColumns();
            }
        }

        public void CargarComparativo()
        {
            dsTabla3 = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarPresupuestosCC(Opcion, dtpPeriodo.Text, Convert.ToString(cbxCentroCosto.SelectedValue));
            dtgComparativo2.DataSource = dsTabla3.Tables[0];
            if (dsTabla3.Tables[0].Rows.Count > 0)
            {
                dgvComparativo2.Columns["MONTO"].Summary.Clear();
                dgvComparativo2.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                var Convertir = dgvComparativo2.Columns["MONTO"].SummaryItem.SummaryValue;
                Decimal Negativo = Convert.ToDecimal(Convertir) * -1;

                dgvComparativo2.Columns["MONTO"].Summary.Clear();
                dgvComparativo2.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo.ToString("#,##0.00"));

                dgvComparativo2.BestFitColumns();
            }

            dtpPeriodo.Value = dtpPeriodo.Value.AddYears(-1);

            dsTabla4 = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarPresupuestosCC(Opcion, dtpPeriodo.Text, Convert.ToString(cbxCentroCosto.SelectedValue));
            dtgComparativo1.DataSource = dsTabla4.Tables[0];
            if (dsTabla4.Tables[0].Rows.Count > 0)
            {
                dgvComparativo1.Columns["MONTO"].Summary.Clear();
                dgvComparativo1.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                var Convertir2 = dgvComparativo1.Columns["MONTO"].SummaryItem.SummaryValue;
                Decimal Negativo2 = Convert.ToDecimal(Convertir2) * -1;

                dgvComparativo1.Columns["MONTO"].Summary.Clear();
                dgvComparativo1.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo2.ToString("#,##0.00"));

                dgvComparativo1.BestFitColumns();
            }

            dtgComparativo3.DataSource = dsTabla4.Tables[1];
            if (dsTabla4.Tables[1].Rows.Count > 0)
            {
                dgvComparativo3.Columns["MONTO"].Summary.Clear();
                dgvComparativo3.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                var Convertir3 = dgvComparativo3.Columns["MONTO"].SummaryItem.SummaryValue;
                Decimal Negativo3 = Convert.ToDecimal(Convertir3) * 1;

                dgvComparativo3.Columns["MONTO"].Summary.Clear();
                dgvComparativo3.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo3.ToString("#,##0.00"));

                dgvComparativo3.BestFitColumns();
            }

            dtpPeriodo.Value = dtpPeriodo.Value.AddYears(1);
        }
        

        private void dtpPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (Documento == 1)
                {
                    txtNroPagina.Text = "1";
                    txtNroPagina_KeyPress(sender, e);
                }

                if (Documento == 2)
                {
                    txtNroPagina2.Text = "1";
                    txtNroPagina2_KeyPress(sender, e);
                }
            }
        }

        private void cbxCentroCosto_DropDownClosed(object sender, EventArgs e)
        {
            txtNroPagina.Text = "1";
            IngresarPagina();
        }

        private void cbCentroCosto_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCentroCosto.Checked == true)
            {
                Opcion = 1;
                txtNroPagina.Text = "1";
                cbxCentroCosto.Enabled = true;
            }

            if (cbCentroCosto.Checked == false)
            {
                Opcion = 0;
                txtNroPagina2.Text = "1";
                cbxCentroCosto.Enabled = false;
            }
        }

        private void rbOrdenServicio_Click(object sender, EventArgs e)
        {
            Documento = 1;
            txtNroPagina.Text = "1";
            tabControlPresupuesto.SelectedTab = tabOrdenServicio;
        }

        private void rbNotaSalida_Click(object sender, EventArgs e)
        {
            Documento = 2;
            txtNroPagina.Text = "1";
            tabControlPresupuesto.SelectedTab = tabNotaSalida;
        }

        private void txtNroPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            rbOrdenServicio.Checked = true;
            Documento = 1;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
            
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { IngresarPagina(); }
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            rbOrdenServicio.Checked = true;
            Documento = 1;

            if (txtNroPagina.Text == "1") { txtNroPagina.Text = "1"; }
            else
            {
                txtNroPagina.Text = Convert.ToString(Convert.ToInt32(txtNroPagina.Text) - 1);
                IngresarPagina();
            }
        }

        private void btnAvanzar_Click(object sender, EventArgs e)
        {
            rbOrdenServicio.Checked = true;
            Documento = 1;

            if (txtNroPagina.Text == Convert.ToString(Cantidad)) { txtNroPagina.Text = Convert.ToString(Cantidad); }
            else
            {
                txtNroPagina.Text = Convert.ToString(Convert.ToInt32(txtNroPagina.Text) + 1);
                IngresarPagina();
            }
        }

        private void txtNroPagina2_KeyPress(object sender, KeyPressEventArgs e)
        {
            rbNotaSalida.Checked = true;
            Documento = 2;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { IngresarPagina(); }
        }

        private void btnRetroceder2_Click(object sender, EventArgs e)
        {
            rbNotaSalida.Checked = true;
            Documento = 2;

            if (txtNroPagina2.Text == "1") { txtNroPagina2.Text = "1"; }
            else
            {
                txtNroPagina2.Text = Convert.ToString(Convert.ToInt32(txtNroPagina2.Text) - 1);
                IngresarPagina();
            }
        }

        private void btnAvanzar2_Click(object sender, EventArgs e)
        {
            rbNotaSalida.Checked = true;
            Documento = 2;

            if (txtNroPagina2.Text == Convert.ToString(Cantidad2)) { txtNroPagina2.Text = Convert.ToString(Cantidad2); }
            else
            {
                txtNroPagina2.Text = Convert.ToString(Convert.ToInt32(txtNroPagina2.Text) + 1);
                IngresarPagina();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Documento == 1) { txtNroPagina.Text = "1"; }
            else { txtNroPagina2.Text = "1"; }

            IngresarPagina();
        }

        private void btnCuadroComparativo_Click(object sender, EventArgs e) { ListarCuadroComparativo(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (Documento == 1)
            {
                if (dtgOrdenServicio.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "REPORTE DE CONTROL DE PRESUPUESTOS - OS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgOrdenServicio.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }

            if (Documento == 2)
            {
                if (dtgNotaSalida.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "REPORTE DE CONTROL DE PRESUPUESTOS - NS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgNotaSalida.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
        }

        private void dtgOrdenServicio_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string NroDocumento = dgvOrdenServicioVista.GetRowCellValue(dgvOrdenServicioVista.FocusedRowHandle, "ORDEN_SERVICIO").ToString();
            string Voucher = dgvOrdenServicioVista.GetRowCellValue(dgvOrdenServicioVista.FocusedRowHandle, "VOUCHER").ToString();

            if (NroDocumento != "")
            {
                lblDocumento.Text = NroDocumento;

                string TipoDocumento = NroDocumento.Substring(0,2);
                string NumeroDocumento = NroDocumento.Substring(3);

                DataTable dtDocumentos = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarDocumentos(1, dtpPeriodo.Text, TipoDocumento, NumeroDocumento, Voucher);
                dtgDocumentos.DataSource = dtDocumentos;
                if (dtDocumentos.Rows.Count > 0)
                {
                    dgvDocumentosVista.Columns["ORDEN_SERVICIO"].Visible = false;

                    dgvDocumentosVista.Columns["CANTIDAD"].Summary.Clear();
                    dgvDocumentosVista.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Total: {0:N2}");
                    dgvDocumentosVista.Columns["MONTO_TOTAL"].Summary.Clear();
                    dgvDocumentosVista.Columns["MONTO_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO_TOTAL", "Total: {0:N2}");

                    dgvDocumentosVista.BestFitColumns();

                    pDetalleDocumento.Visible = true;
                    pDetalleDocumento.BringToFront();
                }
            }
        }

        private void dtgNotaSalida_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string NotaSalida = dgvNotaSalidaVista.GetRowCellValue(dgvNotaSalidaVista.FocusedRowHandle, "NOTA_SALIDA").ToString();
            string Voucher = dgvNotaSalidaVista.GetRowCellValue(dgvNotaSalidaVista.FocusedRowHandle, "VOUCHER").ToString();

            if (NotaSalida != "")
            {
                lblDocumento.Text = NotaSalida;

                string TipoDocumento = NotaSalida.Substring(0,2);
                string NumeroDocumento = NotaSalida.Substring(3);

                DataTable dtDocumentos = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarDocumentos(2, dtpPeriodo.Text, TipoDocumento, NumeroDocumento, Voucher);
                dtgDocumentos.DataSource = dtDocumentos;
                if (dtDocumentos.Rows.Count > 0)
                {
                    dgvDocumentosVista.Columns["NOTA_SALIDA"].Visible = false;

                    dgvDocumentosVista.Columns["CANTIDAD"].Summary.Clear();
                    dgvDocumentosVista.Columns["CANTIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD", "Total: {0:N2}");
                    dgvDocumentosVista.Columns["MONTO_TOTAL"].Summary.Clear();
                    dgvDocumentosVista.Columns["MONTO_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO_TOTAL", "Total: {0:N2}");

                    dgvDocumentosVista.BestFitColumns();

                    pDetalleDocumento.Visible = true;
                    pDetalleDocumento.BringToFront();
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            lblDocumento.Text = "";
            dtgDocumentos.DataSource = null;
            dgvDocumentosVista.Columns.Clear();

            pDetalleDocumento.Visible = false;
            pDetalleDocumento.SendToBack();
        }

        private void pDetalleDocumento_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pDetalleDocumento.Left = pDetalleDocumento.Left + (e.X - xClick);
                pDetalleDocumento.Top = pDetalleDocumento.Top + (e.Y - yClick);
            }
        }

        private void btnExcel2_Click(object sender, EventArgs e)
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
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE ÍTEMS DE DOCUMENTO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgDocumentos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnExcelCuadro_Click(object sender, EventArgs e)
        {
            try
            {
                dtgCuadroComparativo1.ForceInitialize();
                dtgCuadroComparativo2.ForceInitialize();

                compositeLink1.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "COMPARACIÓN DE PRESUPUESTOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink1.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            pNuevoPR.Visible = true;
            pNuevoPR.BringToFront();
            btnGenerar.Enabled = false;
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pNuevoPR.Visible = false;
            pNuevoPR.SendToBack();
            txtDirectorio.Clear();
            dgvPresupuesto.DataSource = null;
            btnGenerar.Enabled = false;
        }

        private void pNuevoPR_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pNuevoPR.Left = pNuevoPR.Left + (e.X - xClick2);
                pNuevoPR.Top = pNuevoPR.Top + (e.Y - yClick2);
            }
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                CargarArchivo();

                if (System.IO.File.Exists(txtDirectorio.Text))
                {
                    string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtDirectorio.Text);
                    OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                    string queryDetalle = String.Format("select * from [{0}$]", "Presupuesto");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtListaPR = dataSetDetalle.Tables[0];
                }

                if (dtListaPR.Rows.Count > 0)
                {
                    xmlPresupuesto = "";
                    dgvPresupuesto.DataSource = dtListaPR;
                    xmlPresupuesto = Comun.Utilitario.Instancia.DatatableToXml(dtListaPR);
                }
                else { dgvPresupuesto.DataSource = null; }
            }
            catch (Exception ex)
            {
                btnGenerar.Enabled = false;
                dgvPresupuesto.DataSource = null;
                MessageBox.Show("El archivo seleccionado no es el correcto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta = "";

            try
            {
                string xmlDetalleSinTildes = Utilitario.Instancia.QuitarTildes(xmlPresupuesto);
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_GenerarPresupuesto(xmlDetalleSinTildes, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    txtDirectorio.Clear();
                    dgvPresupuesto.DataSource = null;
                    btnGenerar.Enabled = false;
                    btnCerrar2_Click(sender, e);
                    ListarPresupuesto();
                    CargarComparativo();
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnListarPR_Click(object sender, EventArgs e) { ListarPresupuesto(); }

        private void btnExcel3_Click(object sender, EventArgs e)
        {
            if (dtgImportarP.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REPORTE DE PRESUPUESTOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgImportarP.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea quitar los presupuestos de la lista?", "ELIMINAR SOLICITUD", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idPresupuesto;
                int[] filas = dgvImportarP.GetSelectedRows();

                if (filas.Length != 0)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        idPresupuesto = Convert.ToInt32(dgvImportarP.GetRowCellValue(filas[i], "idPresupuesto"));

                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;

                        dtRespuesta = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_EliminarPresupuestos(idPresupuesto);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA != "0")
                        {
                            MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }

                    ListarPresupuesto();
                    CargarComparativo();
                }
                else { MessageBox.Show("No ha seleccionado ningún presupuesto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnBuscarPR_Click(object sender, EventArgs e) { CargarComparativo(); }

        private void btnExcelPR_Click(object sender, EventArgs e)
        {
            try
            {
                dtgComparativo1.ForceInitialize();
                dtgComparativo2.ForceInitialize();
                dtgComparativo3.ForceInitialize();

                compositeLink3.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "CUADRO DE PRESUPUESTOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink3.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void dtpResumen1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                DataTable dtListaR = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarResumenPresupuestos(1, dtpResumen1.Text);
                dtgResumen1.DataSource = dtListaR;
                if (dtListaR.Rows.Count > 0)
                {
                    dgvResumen1.Columns["MONTO"].Summary.Clear();
                    dgvResumen1.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                    var Convertir = dgvResumen1.Columns["MONTO"].SummaryItem.SummaryValue;
                    Decimal Negativo = Convert.ToDecimal(Convertir) * 1;

                    dgvResumen1.Columns["MONTO"].Summary.Clear();
                    dgvResumen1.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo.ToString("#,##0.00"));

                    dgvResumen1.BestFitColumns();
                }
            }
        }

        public void dtpResumen2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                DataTable dtListaR = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarResumenPresupuestos(1, dtpResumen2.Text);
                dtgResumen2.DataSource = dtListaR;
                if (dtListaR.Rows.Count > 0)
                {
                    dgvResumen2.Columns["MONTO"].Summary.Clear();
                    dgvResumen2.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                    var Convertir = dgvResumen2.Columns["MONTO"].SummaryItem.SummaryValue;
                    Decimal Negativo = Convert.ToDecimal(Convertir) * 1;

                    dgvResumen2.Columns["MONTO"].Summary.Clear();
                    dgvResumen2.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo.ToString("#,##0.00"));

                    dgvResumen2.BestFitColumns();
                }
            }
        }

        private void dtpResumen3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                DataTable dtListaR = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarResumenPresupuestos(2, dtpResumen3.Text);
                dtgResumen3.DataSource = dtListaR;
                if (dtListaR.Rows.Count > 0)
                {
                    dgvResumen3.Columns["MONTO"].Summary.Clear();
                    dgvResumen3.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                    var Convertir = dgvResumen3.Columns["MONTO"].SummaryItem.SummaryValue;
                    Decimal Negativo = Convert.ToDecimal(Convertir) * 1;

                    dgvResumen3.Columns["MONTO"].Summary.Clear();
                    dgvResumen3.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo.ToString("#,##0.00"));

                    dgvResumen3.BestFitColumns();
                }
            }
        }

        private void btnBuscarResumen_Click(object sender, EventArgs e)
        {
            DataTable dtListaR = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarResumenPresupuestos(1, dtpResumen1.Text);
            dtgResumen1.DataSource = dtListaR;
            if (dtListaR.Rows.Count > 0)
            {
                dgvResumen1.Columns["MONTO"].Summary.Clear();
                dgvResumen1.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                var Convertir = dgvResumen1.Columns["MONTO"].SummaryItem.SummaryValue;
                Decimal Negativo = Convert.ToDecimal(Convertir) * 1;

                dgvResumen1.Columns["MONTO"].Summary.Clear();
                dgvResumen1.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo.ToString("#,##0.00"));

                dgvResumen1.BestFitColumns();
            }

            DataTable dtListaR2 = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarResumenPresupuestos(1, dtpResumen2.Text);
            dtgResumen2.DataSource = dtListaR2;
            if (dtListaR2.Rows.Count > 0)
            {
                dgvResumen2.Columns["MONTO"].Summary.Clear();
                dgvResumen2.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                var Convertir = dgvResumen2.Columns["MONTO"].SummaryItem.SummaryValue;
                Decimal Negativo = Convert.ToDecimal(Convertir) * 1;

                dgvResumen2.Columns["MONTO"].Summary.Clear();
                dgvResumen2.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo.ToString("#,##0.00"));

                dgvResumen2.BestFitColumns();
            }

            DataTable dtListaR3 = clsFinanzasBL.Instancia.ReportesApp_Costos_ControlPresupuestal_ListarResumenPresupuestos(2, dtpResumen3.Text);
            dtgResumen3.DataSource = dtListaR3;
            if (dtListaR3.Rows.Count > 0)
            {
                dgvResumen3.Columns["MONTO"].Summary.Clear();
                dgvResumen3.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: {0:N2}");

                var Convertir = dgvResumen3.Columns["MONTO"].SummaryItem.SummaryValue;
                Decimal Negativo = Convert.ToDecimal(Convertir) * 1;

                dgvResumen3.Columns["MONTO"].Summary.Clear();
                dgvResumen3.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total: S/. " + Negativo.ToString("#,##0.00"));

                dgvResumen3.BestFitColumns();
            }
        }

        private void btnExcelResumen_Click(object sender, EventArgs e)
        {
            try
            {
                dtgResumen1.ForceInitialize();
                dtgResumen2.ForceInitialize();
                dtgResumen3.ForceInitialize();

                compositeLink4.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "RESUMEN DE PRESUPUESTOS POR GRUPOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink4.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
