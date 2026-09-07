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
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    public partial class frmAnalisisFallas : Form
    {
        public string EstadoAnalisis;
        public int idAnalisisFalla = -1;
        public int xClick = 0, yClick = 0;
        
        public frmAnalisisFallas()
        {
            InitializeComponent();
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void frmAnalisisFallas_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            
            CargarComboOperacion();
            cbxOperacion.Text = "TODO";

            rbListaTodas.Checked = true;
            rbListaTodas_Click(sender, e);
        }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(5, "");
            cbxOperacion.DataSource = dtOperacion;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void ListarAnalisis()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                DataTable dtListaAnalisis = new DataTable();
                dtListaAnalisis = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarAnalisisFallas(txtBuscarPlaca.Text, cbxOperacion.Text,
                                                               dtpFechaIni.Text, dtpFechaFin.Text, EstadoAnalisis);
                dtgAnalisisFallas.DataSource = dtListaAnalisis;

                if (dtListaAnalisis.Rows.Count > 0)
                {
                    dgvAnalisisFallasView.Columns["idFalla"].Visible = false;

                    dgvAnalisisFallasView.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAnalisisFallasView.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvAnalisisFallasView.Columns["ULTIMA_FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAnalisisFallasView.Columns["ULTIMA_FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvAnalisisFallasView.BestFitColumns();
                }
            }
        }


        private void dgvAnalisisFallasView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "PENDIENTE") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (e.CellValue.ToString() == "COMPLETADA") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void dtgAnalisisFallas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                idAnalisisFalla = Convert.ToInt32(dgvAnalisisFallasView.GetRowCellValue(dgvAnalisisFallasView.FocusedRowHandle, "NRO"));
                lblTracto.Text = dgvAnalisisFallasView.GetRowCellValue(dgvAnalisisFallasView.FocusedRowHandle, "TRACTO").ToString();
                lblSR.Text = dgvAnalisisFallasView.GetRowCellValue(dgvAnalisisFallasView.FocusedRowHandle, "CARRETA").ToString();
                lblMotivo.Text = dgvAnalisisFallasView.GetRowCellValue(dgvAnalisisFallasView.FocusedRowHandle, "FALLA").ToString();

                txtRaizFalla.Clear();
                pRaizFalla.Visible = true;
                pRaizFalla.BringToFront();
            }
            catch { }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtRaizFalla.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese la causa raíz.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRaizFalla.Focus();
                return;
            }
            else
            {
                DataTable dtRegistrar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRegistrar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarEliminarRaizFalla(1, idAnalisisFalla, txtRaizFalla.Text, Usuario);
                respta = Convert.ToString(dtRegistrar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    ListarAnalisis();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsBorrarRaizFalla_Click(object sender, EventArgs e)
        {
            try
            {
                idAnalisisFalla = Convert.ToInt32(dgvAnalisisFallasView.GetRowCellValue(dgvAnalisisFallasView.FocusedRowHandle, "NRO"));
                string Estado = dgvAnalisisFallasView.GetRowCellValue(dgvAnalisisFallasView.FocusedRowHandle, "CAUSA_RAIZ").ToString();
                
                if (MessageBox.Show("¿Desea borrar la causa raíz de esta falla?", "BORRAR CAUSA RAÍZ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarEliminarRaizFalla(2, idAnalisisFalla, "", Usuario);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        ListarAnalisis();
                        idAnalisisFalla = -1;
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al borrar la causa raíz.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgAnalisisFallas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string CausaRaiz = dgvAnalisisFallasView.GetRowCellValue(dgvAnalisisFallasView.FocusedRowHandle, "CAUSA_RAIZ").ToString();

                if (CausaRaiz != "") { tsBorrarRaizFalla.Enabled = true; }
                else { tsBorrarRaizFalla.Enabled = false; }
            }
            catch { tsBorrarRaizFalla.Enabled = false; }
        }

        private void pRaizFalla_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pRaizFalla.Left = pRaizFalla.Left + (e.X - xClick);
                pRaizFalla.Top = pRaizFalla.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            idAnalisisFalla = -1;
            txtRaizFalla.Clear();
            pRaizFalla.Visible = false;
            pRaizFalla.SendToBack();
        }

        private void txtBuscarPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarAnalisis(); }
        }

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e) { ListarAnalisis(); }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarAnalisis(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarAnalisis(); }
        }

        private void rbListaTodas_Click(object sender, EventArgs e)
        {
            EstadoAnalisis = "TODAS";
            ListarAnalisis();
        }

        private void rbListaPendientes_Click(object sender, EventArgs e)
        {
            EstadoAnalisis = "PENDIENTE";
            ListarAnalisis();
        }

        private void rbListaSolucionadas_Click(object sender, EventArgs e)
        {
            EstadoAnalisis = "COMPLETADA";
            ListarAnalisis();
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarAnalisis(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgAnalisisFallas.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "ANÁLISIS DE FALLAS DE UNIDADES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgAnalisisFallas.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
