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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{




    public partial class Servicios : MetroFramework.Forms.MetroForm
    {
        string OST;
        string MontoTotal;
        public Servicios()
        {
            InitializeComponent();
        }

        private void Servicios_Load(object sender, EventArgs e)
        {
            cboCompania.SelectedIndex = 0;
        }

        int proveedor = -1;
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Ordenes de Servicio " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvProveedor, clsConsultaBL.Instancia.GetPersona(txtProveedor.Text), true, false, false);

                lvProveedor.Columns[0].Width = 0;
                lvProveedor.Columns[1].Width = 206;
                lvProveedor.Columns[2].Width = 110;

                lvProveedor.BringToFront();
                lvProveedor.Visible = true;
                lvProveedor.Focus();
                splitContainer1.SplitterDistance = lvProveedor.Top + lvProveedor.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 42;
            }
        }

        private void lvProveedor_Enter(object sender, EventArgs e)
        {
            if (!lvProveedor.Items.Count.Equals(0))
            {
                lvProveedor.Items[0].Selected = true;
            }
        }

        private void lvProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvProveedor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvProveedor.SelectedItems[0];

                proveedor = Int32.Parse(ItemActual.Text);
                txtProveedor.Text = ItemActual.SubItems[1].Text;
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 42;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 42;
            }
        }

        private void lvProveedor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvProveedor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvProveedor.SelectedItems[0];

                proveedor = Int32.Parse(ItemActual.Text);
                txtProveedor.Text = ItemActual.SubItems[1].Text;
                lvProveedor.Visible = false;
                txtProveedor.Focus();
                splitContainer1.SplitterDistance = 42;
            }
        }



        private void Buscar()
        {
            if (txtProveedor.Text.Trim() == "") { proveedor = -1; }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            string compania = "";
            switch (cboCompania.SelectedIndex)
            {
                case 0:
                    compania = "";
                    break;
                case 1:
                    compania = "100000";
                    break;
                case 2:
                    compania = "400000";
                    break;
            }

            dt = clsLogisticaBL.Instancia.GetServicios(compania, cboCompania.Text, dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", proveedor);

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                //GridView gridView = dtgvData.FocusedView as GridView;
                //gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                //new GridColumnSortInfo(gridView.Columns["CUENTA CONTABLE"], DevExpress.Data.ColumnSortOrder.Ascending), 
                //}, 1);
                dtgvDataView.Columns["PRECIO UNITARIO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["PRECIO UNITARIO"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["TOTAL"].DisplayFormat.FormatString = "n2";


                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }



        private void button1_Click_1(object sender, EventArgs e)
        {
            string TipoMoneda = "";
            switch (cobTipMoneda.SelectedIndex)
            {
                case 0:
                    TipoMoneda = "LO";
                    break;
                case 1:
                    TipoMoneda = "EX";
                    break;
            }

            if (MessageBox.Show("¿Desea guardar Cambios?", "Crear Cambios", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {

                btnaceptar.Focus();
                Buscar();
                groupBox1.Visible = false;

            }
            else
            {

                return;

            }

            DataTable dtFechaOts = new DataTable();
            string Respuesta;
            decimal IdentificarMontoActual;
            //SI MONTO ACTUAL ES VACIO REEMPLAZA POR 0
            if (TxtMonActual.Text.Length == 0)
            {
                IdentificarMontoActual = 0;
            }
            else
            {
                IdentificarMontoActual = Convert.ToDecimal(TxtMonActual.Text);

            }
            // SI EL MONTO INGRESADO ES VACIO AVISAR CON MENSAJE
            if (txtMonModificar.Text.Length == 0)
            {
                MessageBox.Show("Ingresar un monto a modificar", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonModificar.Focus();
                return;
            }
            dtFechaOts = clsLogisticaBL.Instancia.GetModificarOST(1, OST, IdentificarMontoActual, Convert.ToDecimal(txtMonModificar.Text), Utilitario.Instancia.SesionUsuario.usuario, TipoMoneda);
            Respuesta = Convert.ToString(dtFechaOts.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //refresh automativo
                Buscar();
                groupBox1.Visible = false;
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }


        //MODIFICAR OST SELECCIONANDO EL MONTO TOTAL Y EL OST , MODIFICAND
        private void ModificarOST_Click(object sender, EventArgs e)
        {
            try
            {
                int[] filas = dtgvDataView.GetSelectedRows();
                // string datoseleccionado = dtgvDataView.GetFocusedValue().ToString();
                for (int i = 0; i < filas.Length; i++)
                {
                    OST = dtgvDataView.GetRowCellValue(filas[i], "N° ORDEN").ToString();
                    MontoTotal = dtgvDataView.GetRowCellValue(filas[i], "PRECIO UNITARIO").ToString();
                    string TipoMoneda = dtgvDataView.GetRowCellValue(filas[i], "MONEDA").ToString();

                    if (OST.Substring(0, 4).Equals("1000"))
                    {
                        MessageBox.Show("la ost no puede modificarse", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        if (!OST.Equals(""))
                        {
                            // OST = dtgvDataView.GetRowCellValue(filas[i], "OT").ToString();

                            groupBox1.Visible = true;
                            groupBox1.Text = "Modificar montos de : " + OST;
                            TxtMonActual.Text = MontoTotal;
                            txtOST.Text = OST.ToString();
                            txtMonModificar.Text = "";
                            txtMonModificar.Focus();

                            if (TipoMoneda.Equals("LO"))
                            {
                                cobTipMoneda.Text = "SOLES";
                            }
                            else
                            {
                                cobTipMoneda.Text = "DOLARES";
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }

        private void txtMonModificar_KeyPress(object sender, KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            e.Handled = !(char.IsDigit(e.KeyChar)
                    || e.KeyChar == (char)Keys.Back
                    || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator);
        }
       
    }

}
