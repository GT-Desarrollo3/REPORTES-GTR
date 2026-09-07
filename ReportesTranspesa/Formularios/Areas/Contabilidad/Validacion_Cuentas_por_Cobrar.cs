using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using Entidades;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class Validacion_Cuentas_por_Cobrar : MetroFramework.Forms.MetroForm
    {
        public Validacion_Cuentas_por_Cobrar()
        {
            InitializeComponent();
        }

        int cliente = -1;

        private void Validacion_Cuentas_por_Cobrar_Load(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;
            if (fecha.Month > 10)
            {
                string mes = fecha.Month.ToString();
                string periodo = fecha.Year + mes;
                txtPeriodo.Text = periodo;
            }
            else
            {
                string mes = '0' + fecha.Month.ToString();
                string periodo = fecha.Year + mes;
                txtPeriodo.Text = periodo;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtCliente.Text.Trim() == "")
            {
                cliente = -1;
            }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsContabilidadBL.Instancia.GetValidacioCuentasxCobrar(txtPeriodo.Text, cliente);
            if (dt.Rows.Count > 0)
            {
                //dt.Columns.Add("Verificacion",Type.GetType("System.Boolean"));
                //dt.Columns.Add("Verificacion", typeof(Boolean));
                //DataColumn newColumn = new DataColumn("Verificación", typeof(System.Boolean));
                //newColumn.DefaultValue = true;
                //dt.Columns.Add(newColumn);
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["MontoTotal"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDataView.Columns["MontoTotal"].DisplayFormat.FormatString = "N2";
                dtgvDataView.Columns["MontoTotal"].DisplayFormat.FormatString = "C2";
                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvCliente, clsConsultaBL.Instancia.GetPersona(txtCliente.Text), true, false, false);

                lvCliente.Columns[0].Width = 0;
                lvCliente.Columns[1].Width = 206;
                lvCliente.Columns[2].Width = 110;

                lvCliente.BringToFront();
                lvCliente.Visible = true;
                lvCliente.Focus();
                splitContainer1.SplitterDistance = lvCliente.Top + lvCliente.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void lvCliente_Enter(object sender, EventArgs e)
        {
            if (!lvCliente.Items.Count.Equals(0))
            {
                lvCliente.Items[0].Selected = true;
            }
        }

        private void lvCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCliente.SelectedItems[0];

                cliente = Int32.Parse(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void lvCliente_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCliente.SelectedItems[0];

                cliente = Int32.Parse(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Validacion de Cuentas por Cobrar del Periodo " + txtPeriodo.Text + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvData.ShowPrintPreview();
            }
        }

        private void dtgvDataView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "Verificación")
            {
                if ((bool)e.Value)
                {
                    dtgvDataView.SetRowCellValue(e.RowHandle, "Verificación", true);
                }
                else
                {
                    dtgvDataView.SetRowCellValue(e.RowHandle, "Verificación", false);
                }
            }  
        }
    }
}
