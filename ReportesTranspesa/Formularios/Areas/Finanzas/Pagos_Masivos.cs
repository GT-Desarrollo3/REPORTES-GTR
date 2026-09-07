using System;
using System.Data;
using System.Windows.Forms;
using Negocio;
using System.Globalization;
using System.Diagnostics;
using ReportesTranspesa.Sistema;
using System.Text;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    public partial class Pagos_Masivos : MetroFramework.Forms.MetroForm
    {
        public Pagos_Masivos()
        {
            InitializeComponent();
        }
        decimal total = 0;
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            DataTable dt = new DataTable();
            total = 0;
            string compañia;
            //if (cboCompañia.SelectedIndex == 0) { compañia = "100000"; } else { compañia = "400000"; }
            if (cboCompañia.SelectedIndex == 0) { compañia = "100000"; } else { if (cboCompañia.SelectedIndex == 1) { compañia = "400000"; } else { compañia = "500000"; } }
            dt = clsFinanzasBL.Instancia.GetPagoMasivo(compañia, dtpFechaIni.Value.ToShortDateString()+" 00:00:00",
                dtpFechaFin.Value.ToShortDateString()+" 23:59:59", "001", cboMoneda.Text, cboCuenta.Text, txtPrepago.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["Espacio"].Visible = false;
                dtgvDataView.Columns["TipoRegistro"].Visible = false;
                dtgvDataView.Columns["Importe_Abonar"].Visible = false;
                dtgvDataView.Columns["Flag_NA"].Visible = false;
                dtgvDataView.Columns["Flag_Delivery"].Visible = false;
                dtgvDataView.Columns["Flag_Valida_RUC"].Visible = false;
                dtgvDataView.Columns["Direccion"].Visible = false;
                dtgvDataView.Columns["Distrito"].Visible = false;
                dtgvDataView.Columns["Provincia"].Visible = false;
                dtgvDataView.Columns["Departamento"].Visible = false;
                dtgvDataView.Columns["Contacto"].Visible = false;
                dtgvDataView.BestFitColumns();
                dtgvDataView.Columns.View.SelectAll();
                CalcularTotal();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
                lblTotal.Text = total.ToString("G");
        }
        private void CalcularTotal() 
        {
            total = 0;
            int[] filas = dtgvDataView.GetSelectedRows();
            for (int i = 0; i < filas.Length ; i++)
            {
                total = total + Convert.ToDecimal(dtgvDataView.GetRowCellValue(filas[i], "Importe_AbonarN"));
            }
            lblTotal.Text = total.ToString("G");
        }
        private bool ValidaCuentasVacias() 
        {
            int[] filas = dtgvDataView.GetSelectedRows();
            for (int i = 0; i < filas.Length; i++)
            {
                if (Convert.ToString(dtgvDataView.GetRowCellValue(filas[i], "NroCuenta")) == "")
                {
                    Mensaje m = new Mensaje();
                    m.Width = 600;
                    m.mensaje = "No se generará el archivo,existen cuentas vacías en las filas seleccionadas.";
                    m.ShowDialog();
                    return false;
                }
            }
            return true;
        }
        private void btnGenerar_Click(object sender, EventArgs e)
        {

            if (ValidaCuentasVacias() == true)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Notepad Documents (*.txt)|*.txt";
                sfd.FileName = "pagomasivo.txt";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToCsV(dtgvDataView, sfd.FileName);
                }
            }
        }
        private void ToCsV(GridView dGV, string filename)
        {
            string stOutput = "",sHeaders = "#1PC", montocabecera;
            int[] filas = dGV.GetSelectedRows();
            Int64 sumacuent = 0;
            //Exportar cabecera:
            montocabecera = total.ToString("F").Replace(".",string.Empty).PadLeft(15, '0');
            for (int i = 0; i < filas.Length; i++)
            {
                Int64 a = Convert.ToInt64((dGV.GetRowCellValue(filas[i], "NroCuenta").ToString().Substring(4, 10)));
                sumacuent = sumacuent + a;
            }
            int opcion;
            opcion = cboCompañia.SelectedIndex;
            switch (opcion)
            {
                case 0: //GT
                    if (cboMoneda.SelectedIndex == 0) // 0 = soles
                    {
                        sHeaders = sHeaders + "57001118106074      S/";
                        sumacuent = sumacuent + 1118106074;
                    }
                    else // 1 = dolares
                    {
                        sHeaders = sHeaders + "57001120598155      US";
                        sumacuent = sumacuent + 1120598155;
                    }
                    break;
                case 1: //BRA
                    if (cboMoneda.SelectedIndex == 0) // 0 = soles
                    {
                        sHeaders = sHeaders + "57002021178013      S/";
                        sumacuent = sumacuent + 2021178013;
                    }
                    else // 1 = dolares
                    {
                        sHeaders = sHeaders + "57002134488168      US";
                        sumacuent = sumacuent + 2134488168;
                    }
                    break;
                //NUEVA CUENTA DE ALAMCENES
                case 2: //ALMACENES
                    if (cboMoneda.SelectedIndex == 0) // 0 = soles
                    {
                        sHeaders = sHeaders + "57002333604031      S/";
                        sumacuent = sumacuent + 2333604031;
                    }
                    else // 1 = dolares
                    {
                        sHeaders = sHeaders + "57002323262177      US";
                        sumacuent = sumacuent + 2323262177;
                    }
                    break;
            }
            sHeaders = sHeaders + montocabecera + DateTime.Now.ToString("ddMMyyyy")+ "PAGOMASIVO          "
            + sumacuent.ToString().PadLeft(15,'0') + dGV.SelectedRowsCount.ToString().PadLeft(6,'0')+"0"+"               "+"0";
            stOutput += sHeaders + "\r\n";           
            
           

            //Exportar data.
            for (int i = 0; i < filas.Length ; i++)
            {
                string stLine = "";
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Espacio"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "TipoRegistro"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "TipoProducto"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "NroCuenta"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Nombre_Razon"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Moneda"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Importe_Abonar"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "DOI"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "DOI_Numero"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Tipo_Doc"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Numero_Doc"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Abono_Agrup"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Referencia"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Flag_NA"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Flag_Delivery"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Flag_Valida_RUC"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Direccion"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Distrito"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Provincia"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Departamento"));
                stLine = stLine.ToString() + Convert.ToString(dGV.GetRowCellValue(filas[i], "Contacto"));
                stOutput += stLine + "\r\n";
            }

            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); 
            bw.Flush();
            bw.Close();
            fs.Close();
        }
        private void Pagos_Masivos_Load(object sender, EventArgs e)
        {
            cboCompañia.SelectedIndex = 0;
            cboBanco.SelectedIndex = 0;
            cboMoneda.SelectedIndex = 0;
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }
        private void cboCompañia_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCuentas();
        }
        private void cboMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCuentas();
        }
        private void CargarCuentas()
        {
            int opcion;
            opcion = cboCompañia.SelectedIndex;
            switch (opcion) 
            {
                case 0: //GT
                        if (cboMoneda.SelectedIndex == 0) // 0 = soles
                        {
                            cboCuenta.Items.Clear();
                            cboCuenta.Items.Add("1118106-0-74");
                            cboCuenta.SelectedIndex = 0;
                        }
                        else // 1 = dolares
                        {
                            cboCuenta.Items.Clear();
                            cboCuenta.Items.Add("1120598-1-55");
                            cboCuenta.SelectedIndex = 0;
                        }
                        break;
                case 1: //BRA
                        if (cboMoneda.SelectedIndex == 0) // 0 = soles
                        {
                            cboCuenta.Items.Clear();
                            cboCuenta.Items.Add("570-2021178-013");
                            cboCuenta.SelectedIndex = 0;
                        }
                        else // 1 = dolares
                        {
                            cboCuenta.Items.Clear();
                            cboCuenta.Items.Add("570-2134488-168");
                            cboCuenta.SelectedIndex = 0;
                        }
                        break;
                case 2: //ALMACENES
                        if (cboMoneda.SelectedIndex == 0) // 0 = soles
                        {
                            cboCuenta.Items.Clear();
                            cboCuenta.Items.Add("570-2333604-031");
                            cboCuenta.SelectedIndex = 0;
                        }
                        else // 1 = dolares
                        {
                            cboCuenta.Items.Clear();
                            cboCuenta.Items.Add("570-2323262-177");
                            cboCuenta.SelectedIndex = 0;
                        }
                        break;
            }
        }
        private void dtgvDataView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            CalcularTotal();
        }
    }
}
