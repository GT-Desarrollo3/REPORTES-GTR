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
using ReportesTranspesa.Sistema;
using ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas;
using ReportesTranspesa.Formularios.Areas.Seguridad.RegistroAccidentes;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Logistica.CuadroComparativo
{
    public partial class frmListaOferta : Form
    {
        public DataTable dtListaOfertas = new DataTable();
        DataTable dtPermisos = new DataTable();
        public int idEvaluacionC = -1;
        public int posx = 0, posy = 0;

        public frmListaOferta()
        {
            InitializeComponent();
        }

        private void frmListaOferta_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaOferta");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnAgregar.Enabled = true; }
                else { btnAgregar.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsModificarEvaluacion.Enabled = true; }
                else { tsModificarEvaluacion.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarEvaluacion.Enabled = true; }
                else { tsEliminarEvaluacion.Enabled = false; }
            }
            
            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            ListarEvaluaciones();
        }


        public void ListarEvaluaciones()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaOfertas = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_ListarEvaluaciones(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodigo.Text);
                dtgListaEvaluacion.DataSource = dtListaOfertas;
                if (dtListaOfertas.Rows.Count > 0)
                {
                    dgvListaEvaluacionView.Columns["idEvaluacionC"].Visible = false;

                    dgvListaEvaluacionView.Columns["FECHA_EVALUACION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaEvaluacionView.Columns["FECHA_EVALUACION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    /*
                    dgvIncidentesSegVista.Columns["TIPO_INCIDENTE"].Summary.Clear();
                    dgvIncidentesSegVista.Columns["TIPO_INCIDENTE"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TIPO_INCIDENTE", "Total = {0}");
                    */

                    dgvListaEvaluacionView.BestFitColumns();
                }
            }
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmNuevaOferta frmNuevaOferta = new frmNuevaOferta();
            frmNuevaOferta.frmListaOferta = this;
            frmNuevaOferta.lblFechaEV.Text = DateTime.Now.ToShortDateString();
            frmNuevaOferta.lblUsuarioEV.Text = Utilitario.Instancia.SesionUsuario.usuario;
            frmNuevaOferta.btnExcel.Enabled = false;

            frmNuevaOferta.CargarComboUnidades();
            frmNuevaOferta.cbxUND.Text = "UNI";
            frmNuevaOferta.txtCantidad.Text = "0";
            frmNuevaOferta.txtPrecioUnitario.Text = "0.00";
            frmNuevaOferta.txtPrecioTotal.Text = "0.00";

            frmNuevaOferta.txtPUnitario.Text = "0.00";
            frmNuevaOferta.txtFormaPago.Text = "0";
            frmNuevaOferta.txtPTotal.Text = "0.00";
            frmNuevaOferta.txtCostoFinanciero.Text = "0.00";
            frmNuevaOferta.txtPrecioEquivalente.Text = "0.00";

            frmNuevaOferta.ListarEvaluacionD();
            frmNuevaOferta.Show(this);
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarEvaluaciones(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarEvaluaciones(); }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarEvaluaciones(); }
        }

        private void dtgListaEvaluacion_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idEvaluacionC = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "idEvaluacionC"));

                if (idEvaluacionC != 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsModificarEvaluacion.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarEvaluacion.Enabled = true; }
                }
                else
                {
                    tsModificarEvaluacion.Enabled = false;
                    tsEliminarEvaluacion.Enabled = false;
                }
            }
            catch
            {
                tsModificarEvaluacion.Enabled = false;
                tsEliminarEvaluacion.Enabled = false;
            }
        }

        public void btnBuscar_Click(object sender, EventArgs e) { ListarEvaluaciones(); }

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
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE EVALUACIONES DE OFERTAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaEvaluacion.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgListaEvaluacion_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmNuevaOferta frmNuevaOferta = new frmNuevaOferta();
                frmNuevaOferta.idEvaluacionC = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "idEvaluacionC"));
                frmNuevaOferta.lblEvaluacion.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "CODIGO"));
                frmNuevaOferta.lblFechaEV.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "FECHA_EVALUACION")).Substring(0, 10);
                frmNuevaOferta.lblUsuarioEV.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "USUARIO_EVALUACION"));
                frmNuevaOferta.dtgListaEvaluacion.ContextMenuStrip = null;

                frmNuevaOferta.CargarComboUnidades();
                frmNuevaOferta.groupBox1.Enabled = false;
                frmNuevaOferta.txtDescripcion.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "SERVICIO"));
                frmNuevaOferta.cbxUND.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "UNIDAD"));
                frmNuevaOferta.txtCantidad.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "CANTIDAD"));
                frmNuevaOferta.txtPrecioUnitario.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "PRECIO_UNITARIO"));
                frmNuevaOferta.txtPrecioTotal.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "PRECIO_TOTAL"));
                frmNuevaOferta.groupBox2.Enabled = false;
                frmNuevaOferta.btnGuardar.Enabled = false;
                frmNuevaOferta.btnExcel.Enabled = true;

                frmNuevaOferta.ListarEvaluacionD();
                frmNuevaOferta.Show(this);
            }
            catch { MessageBox.Show("La fecha seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsModificarEvaluacion_Click(object sender, EventArgs e)
        {
            frmNuevaOferta frmNuevaOferta = new frmNuevaOferta();
            frmNuevaOferta.idEvaluacionC = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "idEvaluacionC"));
            frmNuevaOferta.lblEvaluacion.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "CODIGO"));
            frmNuevaOferta.lblFechaEV.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "FECHA_EVALUACION")).Substring(0, 10);
            frmNuevaOferta.lblUsuarioEV.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "USUARIO_EVALUACION"));

            frmNuevaOferta.CargarComboUnidades();
            frmNuevaOferta.groupBox1.Enabled = false;
            frmNuevaOferta.btnExcel.Enabled = true;
            frmNuevaOferta.txtDescripcion.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "SERVICIO"));
            frmNuevaOferta.cbxUND.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "UNIDAD"));
            frmNuevaOferta.txtCantidad.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "CANTIDAD"));
            frmNuevaOferta.txtPrecioUnitario.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "PRECIO_UNITARIO"));
            frmNuevaOferta.txtPrecioTotal.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "PRECIO_TOTAL"));

            frmNuevaOferta.txtPUnitario.Text = "0.00";
            frmNuevaOferta.txtFormaPago.Text = "0";
            frmNuevaOferta.txtPTotal.Text = "0.00";
            frmNuevaOferta.txtCostoFinanciero.Text = "0.00";
            frmNuevaOferta.txtPrecioEquivalente.Text = "0.00";

            frmNuevaOferta.ListarEvaluacionD();
            frmNuevaOferta.Show(this);
        }

        private void tsAnadirAdicional_Click(object sender, EventArgs e)
        {
            idEvaluacionC = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "idEvaluacionC"));
            lblCodigo.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "CODIGO"));
            txtDescuento.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "DESCUENTO"));
            txtDescripcion.Text = Convert.ToString(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "CONCLUSIONES"));

            pDatosAdicionales.BringToFront();
            pDatosAdicionales.Visible = true;
            txtDescuento.Focus();
        }

        private void pDatosAdicionales_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { posx = e.X; posy = e.Y; }
            else
            {
                pDatosAdicionales.Left = pDatosAdicionales.Left + (e.X - posx);
                pDatosAdicionales.Top = pDatosAdicionales.Top + (e.Y - posy);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pDatosAdicionales.Visible = false;
            pDatosAdicionales.SendToBack();
            pDatosAdicionales.Location = new System.Drawing.Point(273, 155);
            idEvaluacionC = -1;
            lblCodigo.Text = "";
            txtDescuento.Clear();
            txtDescripcion.Clear();
        }

        private void txtDescuento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Enter) { txtDescripcion.Focus(); }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { btnGuardar.Focus(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtDescuento.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el porcentaje.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDescuento.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_AgregarAdicionales(2, idEvaluacionC, Convert.ToDecimal(txtDescuento.Text),
                                                                                                                  txtDescripcion.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    btnCerrar2_Click(sender, e);
                    ListarEvaluaciones();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminarEvaluacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Quiere eliminar esta evaluación?", "ELIMINAR EVALUACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;

                    int idEvaluacionC2 = Convert.ToInt32(dgvListaEvaluacionView.GetRowCellValue(dgvListaEvaluacionView.FocusedRowHandle, "idEvaluacionC"));

                    dtRespuesta = clsLogisticaBL.Instancia.ReportesApp_Logistica_EvaluacionOfertas_AgregarAdicionales(1, idEvaluacionC2, 0.00M, "");
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);

                    if (NroRPTA == "0") { ListarEvaluaciones(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("La oferta seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
