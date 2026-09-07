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
﻿using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Logistica.CuadroComparativo
{
    public partial class frmNuevaOferta : Form
    {
        public frmListaOferta frmListaOferta = new frmListaOferta();
        public int idEvaluacionC = -1;
        public int posx = 0, posy = 0;
        public int idEvaluacionC2 = -1, idEvaluacionD2 = -1;
        public DataTable dtListaOfertas = new DataTable();
        DataTable dtPermisos = new DataTable();
        public DataSet dsTabla;

        public frmNuevaOferta()
        {
            InitializeComponent();
            cbxUND.SelectedIndexChanged -= cbxUND_SelectedIndexChanged;
        }

        private void cbxUND_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUnidades(); }

        private void frmNuevaOferta_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaOferta");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { tsEliminarOferta.Enabled = true; }
                else { tsEliminarOferta.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    tsAsignarMinimo.Enabled = true;
                    tsPuntajeTecnico.Enabled = true;
                }
                else
                {
                    tsAsignarMinimo.Enabled = false;
                    tsPuntajeTecnico.Enabled = true;
                }
            }
        }


        public void CargarComboUnidades()
        {
            DataTable dtUnidad = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_FiltrarEliminarIncidencias(4, 0);
            cbxUND.DataSource = dtUnidad;
            cbxUND.DisplayMember = "CODIGO";
            cbxUND.ValueMember = "UNIDAD";
        }

        public void ListarEvaluacionD()
        {
            if (lblEvaluacion.Text == "CT-01-") { dtListaOfertas = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_DetalleListar(0); }
            else { dtListaOfertas = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_DetalleListar(idEvaluacionC); }

            dtgListaEvaluacion.DataSource = dtListaOfertas;
            if (dtListaOfertas.Rows.Count > 0)
            {
                dgvListaEvaluacionView.Columns["idEvaluacionC"].Visible = false;
                dgvListaEvaluacionView.BestFitColumns();
            }
        }


        private void dtgListaEvaluacion_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idOferta = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "NRO"));

                if (idOferta != 0)
                {
                    
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { tsEliminarOferta.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        tsPuntajeTecnico.Enabled = true;
                        tsAsignarMinimo.Enabled = true;
                    }
                }
                else
                {
                    tsPuntajeTecnico.Enabled = false;
                    tsEliminarOferta.Enabled = false;
                    tsAsignarMinimo.Enabled = false;
                }
            }
            catch
            {
                tsPuntajeTecnico.Enabled = false;
                tsEliminarOferta.Enabled = false;
                tsAsignarMinimo.Enabled = false;
            }
        }

        private void dgvListaEvaluacionView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "VALOR_MINIMO") { e.Appearance.ForeColor = Color.DarkRed; }
        }

        private void txtPrecioUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
                { e.Handled = true; }
                else { e.Handled = false; }

                if (e.KeyChar == (char)Keys.Enter)
                {
                    double Precio;
                    Precio = Convert.ToDouble(txtPrecioUnitario.Text == "" ? 0 : Convert.ToDouble(txtPrecioUnitario.Text));
                    txtPrecioUnitario.Text = Precio.ToString();
                    txtCantidad.Focus();

                    txtPrecioTotal.Text = Convert.ToString(Math.Round(Precio * Convert.ToDouble(txtCantidad.Text), 2));
                }
            }
            catch { MessageBox.Show("Error calculando el monto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
                { e.Handled = true; }
                else { e.Handled = false; }

                if (e.KeyChar == (char)Keys.Enter)
                {
                    double Cantidad;
                    Cantidad = Convert.ToDouble(txtCantidad.Text == "" ? 1 : Convert.ToDouble(txtCantidad.Text));
                    txtCantidad.Text = Cantidad.ToString();
                    txtPrecioUnitario.Focus();

                    txtPrecioTotal.Text = Convert.ToString(Math.Round(Convert.ToDouble(txtPrecioUnitario.Text) * Cantidad, 2));
                    txtPTotal.Text = Convert.ToString(Math.Round(Convert.ToDouble(txtPUnitario.Text) * Cantidad, 2));
                    txtCostoFinanciero.Text = Convert.ToString(Math.Round((Math.Pow(1.07, (Convert.ToDouble(txtFormaPago.Text) / 360)) - 1) * Convert.ToDouble(txtPTotal.Text), 2));
                    txtPrecioEquivalente.Text = Convert.ToString(Math.Round(Convert.ToDouble(txtPTotal.Text) - Convert.ToDouble(txtCostoFinanciero.Text), 2));
                }
            }
            catch { MessageBox.Show("Error calculando el monto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { txtPUnitario.Focus(); }
        }

        private void txtPUnitario_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
                { e.Handled = true; }
                else { e.Handled = false; }

                if (e.KeyChar == (char)Keys.Enter)
                {
                    double Precio;
                    Precio = Convert.ToDouble(txtPUnitario.Text == "" ? 0 : Convert.ToDouble(txtPUnitario.Text));
                    txtPUnitario.Text = Precio.ToString();
                    txtFormaPago.Focus();

                    txtPTotal.Text = Convert.ToString(Math.Round(Precio * Convert.ToDouble(txtCantidad.Text), 2));
                    txtCostoFinanciero.Text = Convert.ToString(Math.Round((Math.Pow(1.07, (Convert.ToDouble(txtFormaPago.Text) / 360)) - 1) * Convert.ToDouble(txtPTotal.Text), 2));
                    txtPrecioEquivalente.Text = Convert.ToString(Math.Round(Convert.ToDouble(txtPTotal.Text) - Convert.ToDouble(txtCostoFinanciero.Text), 2));
                }
            }
            catch { MessageBox.Show("Error calculando el monto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtFormaPago_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
                { e.Handled = true; }
                else { e.Handled = false; }

                if (e.KeyChar == (char)Keys.Enter)
                {
                    double FormaPago;
                    FormaPago = Convert.ToDouble(txtFormaPago.Text == "" ? 1 : Convert.ToDouble(txtFormaPago.Text));
                    txtFormaPago.Text = FormaPago.ToString();
                    txtPUnitario.Focus();

                    txtCostoFinanciero.Text = Convert.ToString(Math.Round((Math.Pow(1.07, (FormaPago / 360)) - 1) * Convert.ToDouble(txtPTotal.Text), 2));
                    txtPrecioEquivalente.Text = Convert.ToString(Math.Round(Convert.ToDouble(txtPTotal.Text) - Convert.ToDouble(txtCostoFinanciero.Text), 2));
                }
            }
            catch { MessageBox.Show("Error calculando el monto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtProveedor.Clear();
            txtPUnitario.Text = "0";
            txtFormaPago.Text = "0";
            txtPTotal.Text = "0";
            txtCostoFinanciero.Text = "0";
            txtPrecioEquivalente.Text = "0";
        }

        private void btnAniadir_Click(object sender, EventArgs e)
        {
            if (txtProveedor.Text.Length == 0 || txtPUnitario.Text.Length == 0 || txtFormaPago.Text.Length == 0 || txtPrecioTotal.Text == "0.00")
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
                if (txtProveedor.Text.Length == 0) { txtProveedor.Focus(); }
                else
                {
                    if (txtPUnitario.Text.Length == 0) { txtPUnitario.Focus(); }
                    else { txtFormaPago.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                if (lblEvaluacion.Text == "CT-01-")
                {
                    dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_DetalleInsertar(0, txtProveedor.Text, Convert.ToDecimal(txtPUnitario.Text), Convert.ToDecimal(txtFormaPago.Text),
                                                           Convert.ToDecimal(txtPTotal.Text), Convert.ToDecimal(txtCostoFinanciero.Text), Convert.ToDecimal(txtPrecioEquivalente.Text));
                }
                else
                {
                    dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_DetalleInsertar(idEvaluacionC, txtProveedor.Text, Convert.ToDecimal(txtPUnitario.Text), Convert.ToDecimal(txtFormaPago.Text),
                                                           Convert.ToDecimal(txtPTotal.Text), Convert.ToDecimal(txtCostoFinanciero.Text), Convert.ToDecimal(txtPrecioEquivalente.Text));
                }

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                
                if (NroRPTA == "0")
                {
                    if (lblEvaluacion.Text == "CT-01-") { ListarEvaluacionD(); }
                    else { ListarEvaluacionD(); }
                    btnCancelar_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtDescripcion.Focus();
                }
            }
        }

        private void tsAsignarMinimo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                int idEvaluacionC = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "idEvaluacionC"));
                int idEvaluacionD = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "NRO"));

                dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_DetalleEditar(1, idEvaluacionC, idEvaluacionD, 0.00M);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0") { ListarEvaluacionD(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("La oferta seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsPuntajeTecnico_Click(object sender, EventArgs e)
        {
            idEvaluacionC2 = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "idEvaluacionC"));
            idEvaluacionD2 = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "NRO"));
            
            pEvaluacionTecnica.BringToFront();
            pEvaluacionTecnica.Visible = true;
            txtPuntajeTecnico.Focus();
        }

        private void pEvaluacionTecnica_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { posx = e.X; posy = e.Y; }
            else
            {
                pEvaluacionTecnica.Left = pEvaluacionTecnica.Left + (e.X - posx);
                pEvaluacionTecnica.Top = pEvaluacionTecnica.Top + (e.Y - posy);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pEvaluacionTecnica.Visible = false;
            pEvaluacionTecnica.SendToBack();
            pEvaluacionTecnica.Location = new System.Drawing.Point(273, 355);
            idEvaluacionC2 = -1; idEvaluacionD2 = -1;
            txtPuntajeTecnico.Clear();
        }

        private void txtPuntajeTecnico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }
            
            if (e.KeyChar == (char)Keys.Enter) { btnGuardar2_Click(sender, e); }
        }

        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            if (txtPuntajeTecnico.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el puntaje técnico.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPuntajeTecnico.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_DetalleEditar(2, idEvaluacionC2, idEvaluacionD2, Convert.ToDecimal(txtPuntajeTecnico.Text));
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    btnCerrar2_Click(sender, e);
                    ListarEvaluacionD();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminarOferta_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Quiere eliminar esta oferta de la evaluación?", "ELIMINAR OFERTA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;

                    int idEvaluacionC = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "idEvaluacionC"));
                    int idEvaluacionD = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "NRO"));

                    dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_DetalleEditar(3, idEvaluacionC, idEvaluacionD, 0.00M);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);

                    if (NroRPTA == "0") { ListarEvaluacionD(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("La oferta seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length == 0 || txtCantidad.Text.Length == 0 || txtPrecioUnitario.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtDescripcion.Text.Length == 0) { txtDescripcion.Focus(); }
                else
                {
                    if (txtCantidad.Text.Length == 0) { txtCantidad.Focus(); }
                    else { txtPrecioUnitario.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                if (lblEvaluacion.Text == "CT-01-")
                {
                    dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_InsertarEvaluacion(1, 0, txtDescripcion.Text, cbxUND.Text, Convert.ToDecimal(txtCantidad.Text),
                                                           Convert.ToDecimal(txtPrecioUnitario.Text), Convert.ToDecimal(txtPrecioTotal.Text), lblUsuarioEV.Text);
                }
                else
                {
                    dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_InsertarEvaluacion(2, idEvaluacionC, txtDescripcion.Text, cbxUND.Text, Convert.ToDecimal(txtCantidad.Text),
                                                           Convert.ToDecimal(txtPrecioUnitario.Text), Convert.ToDecimal(txtPrecioTotal.Text), lblUsuarioEV.Text);
                }

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListaOferta.btnBuscar_Click(sender, e);
                    this.Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); } 
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaEvaluacion.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                dsTabla = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_ExportarExcel(idEvaluacionC);
                dtgExportarC.DataSource = null;
                dgvExportarC.Columns.Clear();
                dtgExportarC.DataSource = dsTabla.Tables[0];
                dgvExportarC.Columns["NRO"].Visible = false;
                dgvExportarC.BestFitColumns();

                dtgExportarD.DataSource = null;
                dgvExportarD.Columns.Clear();
                dtgExportarD.DataSource = dsTabla.Tables[1];
                dgvExportarD.BestFitColumns();

                dtgExportarC.ForceInitialize();
                dtgExportarD.ForceInitialize();

                compositeLink1.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "CUADRO COMPARATIVO DE OFERTAS - " + lblEvaluacion.Text + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink1.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
        }
    }
}
