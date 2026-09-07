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
using DevExpress.Data;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class frmAperturarPeriodos : MetroFramework.Forms.MetroForm
    {
        public DataTable dtListaPeriodo = new DataTable();

        public frmAperturarPeriodos()
        {
            InitializeComponent();
            cbxCompania.SelectedIndexChanged -= cbxCompania_SelectedIndexChanged;
        }

        private void cbxCompania_SelectedIndexChanged(object sender, EventArgs e) { CargarComboCompania(); }

        private void frmAperturarPeriodos_Load(object sender, EventArgs e)
        {
            dtpFecha.Value = DateTime.Now;
            CargarComboCompania();
            ListarPeriodos();
        }


        public void CargarComboCompania()
        {
            DataTable dtCompania = clsFinanzasBL.Instancia.GetOperaciones_ListarCompania("");
            cbxCompania.DataSource = dtCompania;
            cbxCompania.DisplayMember = "DescripcionCorta";
            cbxCompania.ValueMember = "CompaniaCodigo";
        }

        public void ListarPeriodos()
        {
            dtListaPeriodo = clsContabilidadBL.Instancia.ReportesApp_Contabilidad_ListarPeriodosAbiertos(cbxCompania.Text, dtpFecha.Text);
            dtgPeriodos.DataSource = dtListaPeriodo;

            if (dtListaPeriodo.Rows.Count > 0)
            {
                dgvPeriodosView.Columns["CompaniaSocio"].Visible = false;

                dgvPeriodosView.Columns["ULTIMA_FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvPeriodosView.Columns["ULTIMA_FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                dgvPeriodosView.BestFitColumns();
            }
        }


        private void cbxCompania_DropDownClosed(object sender, EventArgs e) { ListarPeriodos(); }

        private void dtpFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarPeriodos(); }
        }

        private void btnNuevoPeriodo_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea aperturar este periodo?", "ABRIR PERIODO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtAgregar = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtAgregar = clsContabilidadBL.Instancia.ReportesApp_Contabilidad_AbrirCerrarPeriodos(1, Convert.ToString(cbxCompania.SelectedValue),
                                                            dtpFecha.Text, Usuario);
                    respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);

                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarPeriodos();
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }

        private void btnCerrarPeriodo_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea cerrar este periodo para todas las compañías?", "CERRAR PERIODO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtAgregar = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtAgregar = clsContabilidadBL.Instancia.ReportesApp_Contabilidad_AbrirCerrarPeriodos(2, Convert.ToString(cbxCompania.SelectedValue),
                                                            dtpFecha.Text, Usuario);
                    respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);

                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarPeriodos();
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarPeriodos(); }

        private void dgvPeriodosView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "ABIERTO")
                {
                    e.Appearance.BackColor = Color.FromArgb(192, 255, 192);
                    e.Appearance.ForeColor = Color.Black;
                }

                if (Convert.ToString(e.CellValue) == "CERRADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgPeriodos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "CONTROL DE PERIODOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgPeriodos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgPeriodos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Estado = dgvPeriodosView.GetRowCellValue(dgvPeriodosView.FocusedRowHandle, "ESTADO").ToString();

                if (Estado == "") { tsCerrarAbrirPeriodo.Enabled = false; }
                else
                {
                    if (Estado == "ABIERTO") { tsCerrarAbrirPeriodo.Text = "Cerrar periodo"; }
                    else { tsCerrarAbrirPeriodo.Text = "Abrir periodo"; }

                    tsCerrarAbrirPeriodo.Enabled = true;
                }
            }
            catch { tsCerrarAbrirPeriodo.Enabled = false; }
        }

        private void tsCerrarAbrirPeriodo_Click(object sender, EventArgs e)
        {
            try
            {
                string Compania = dgvPeriodosView.GetRowCellValue(dgvPeriodosView.FocusedRowHandle, "CompaniaSocio").ToString();
                string Modulo = dgvPeriodosView.GetRowCellValue(dgvPeriodosView.FocusedRowHandle, "MODULO").ToString();
                string Estado = dgvPeriodosView.GetRowCellValue(dgvPeriodosView.FocusedRowHandle, "ESTADO").ToString();
                string Periodo = dgvPeriodosView.GetRowCellValue(dgvPeriodosView.FocusedRowHandle, "PERIODO").ToString();
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (MessageBox.Show("¿Desea modificar el periodo de este registro?", "MODIFICAR PERIODO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsContabilidadBL.Instancia.ReportesApp_Contabilidad_AbrirCerrarPeriodo(Compania, Modulo, Estado, Periodo, Usuario);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);

                    if (NroRspta == "0") { btnBuscar_Click(sender, e); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al actualizar el periodo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
