using System;
using System.IO;
using System.Data;
using System.Windows.Forms;
using Negocio;
using System.Globalization;
using System.Diagnostics;
using ReportesTranspesa.Sistema;
using System.Text;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.Data;
using System.Data.OleDb;

namespace ReportesTranspesa.Formularios.Areas.Finanzas
{
    public partial class Movimientos_Diarios : MetroFramework.Forms.MetroForm
    {
        public Movimientos_Diarios()
        {
            InitializeComponent();
        }
        
        private void Movimientos_Diarios_Load(object sender, EventArgs e)
        {
            #region llenacombos
            DataTable dt = new DataTable();
            dt.Columns.Add("IDCOMPANIA");
            dt.Columns.Add("COMPANIADESC");
            DataRow fila = dt.NewRow();
            fila["IDCOMPANIA"] = "10000000";
            fila["COMPANIADESC"] = "GRUPO TRANSPESA SAC";
            dt.Rows.Add(fila);
            DataRow fila2 = dt.NewRow();
            fila2["IDCOMPANIA"] = "40000000";
            fila2["COMPANIADESC"] = "FABRICACIONES BRA";
            dt.Rows.Add(fila2);
            cboCompania.DataSource = dt;
            cboCompania.DisplayMember = "COMPANIADESC";
            cboCompania.ValueMember = "IDCOMPANIA";
            cboCompania.SelectedIndex = 0;

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("IDCUENTA");
            dt2.Columns.Add("CUENTADESC");
            DataRow fila3 = dt2.NewRow();
            fila3["IDCUENTA"] = "1041001";
            fila3["CUENTADESC"] = "BCP MN";
            dt2.Rows.Add(fila3);
            DataRow fila4 = dt2.NewRow();
            fila4["IDCUENTA"] = "1041002";
            fila4["CUENTADESC"] = "BBVA MN";
            dt2.Rows.Add(fila4);
            DataRow fila5 = dt2.NewRow();
            fila5["IDCUENTA"] = "1041004";
            fila5["CUENTADESC"] = "BANBIF MN";
            dt2.Rows.Add(fila5);
            DataRow fila6 = dt2.NewRow();
            fila6["IDCUENTA"] = "1041005";
            fila6["CUENTADESC"] = "GNB";
            dt2.Rows.Add(fila6);
            DataRow fila7 = dt2.NewRow();
            fila7["IDCUENTA"] = "1041006";
            fila7["CUENTADESC"] = "INTERBANK MN";
            dt2.Rows.Add(fila7);
            DataRow fila8 = dt2.NewRow();
            fila8["IDCUENTA"] = "1041101";
            fila8["CUENTADESC"] = "BCP ME";
            dt2.Rows.Add(fila8);
            DataRow fila9 = dt2.NewRow();
            fila9["IDCUENTA"] = "1041102";
            fila9["CUENTADESC"] = "BBVA ME";
            dt2.Rows.Add(fila9);
            cboCuenta.DataSource = dt2;
            cboCuenta.DisplayMember = "CUENTADESC";
            cboCuenta.ValueMember = "IDCUENTA";
            cboCuenta.SelectedIndex = 0;
            #endregion
        }
        decimal real = 0;
        decimal pendiente = 0;
        private void cboCompania_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCompania.SelectedIndex == 1)//BRA
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IDCUENTA");
                dt.Columns.Add("CUENTADESC");
                DataRow fila = dt.NewRow();
                fila["IDCUENTA"] = "1041001";
                fila["CUENTADESC"] = "BCP MN";
                dt.Rows.Add(fila);
                DataRow fila2 = dt.NewRow();
                fila2["IDCUENTA"] = "1041002";
                fila2["CUENTADESC"] = "BBVA MN";
                dt.Rows.Add(fila2);
                DataRow fila3 = dt.NewRow();
                fila3["IDCUENTA"] = "1041003";
                fila3["CUENTADESC"] = "SCOTIABANK MN";
                dt.Rows.Add(fila3);
                DataRow fila4 = dt.NewRow();
                fila4["IDCUENTA"] = "1041109";
                fila4["CUENTADESC"] = "BCP ME";
                dt.Rows.Add(fila4);
                DataRow fila5 = dt.NewRow();
                fila5["IDCUENTA"] = "1041102";
                fila5["CUENTADESC"] = "BBVA ME";
                dt.Rows.Add(fila5);
                cboCuenta.DataSource = dt;
                cboCuenta.DisplayMember = "CUENTADESC";
                cboCuenta.ValueMember = "IDCUENTA";
                cboCuenta.SelectedIndex = 0;
            }
            else 
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IDCUENTA");
                dt.Columns.Add("CUENTADESC");
                DataRow fila = dt.NewRow();
                fila["IDCUENTA"] = "1041001";
                fila["CUENTADESC"] = "BCP MN";
                dt.Rows.Add(fila);
                DataRow fila2 = dt.NewRow();
                fila2["IDCUENTA"] = "1041002";
                fila2["CUENTADESC"] = "BBVA MN";
                dt.Rows.Add(fila2);
                DataRow fila3 = dt.NewRow();
                fila3["IDCUENTA"] = "1041004";
                fila3["CUENTADESC"] = "BANBIF MN";
                dt.Rows.Add(fila3);
                DataRow fila4 = dt.NewRow();
                fila4["IDCUENTA"] = "1041005";
                fila4["CUENTADESC"] = "GNB";
                dt.Rows.Add(fila4);
                DataRow fila5 = dt.NewRow();
                fila5["IDCUENTA"] = "1041006";
                fila5["CUENTADESC"] = "INTERBANK MN";
                dt.Rows.Add(fila5);
                DataRow fila6 = dt.NewRow();
                fila6["IDCUENTA"] = "1041101";
                fila6["CUENTADESC"] = "BCP ME";
                dt.Rows.Add(fila6);
                DataRow fila7 = dt.NewRow();
                fila7["IDCUENTA"] = "1041102";
                fila7["CUENTADESC"] = "BBVA ME";
                dt.Rows.Add(fila7);
                cboCuenta.DataSource = dt;
                cboCuenta.DisplayMember = "CUENTADESC";
                cboCuenta.ValueMember = "IDCUENTA";
                cboCuenta.SelectedIndex = 0;
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            DataTable data = new DataTable();
            data = clsFinanzasBL.Instancia.GetMovimientos(cboCompania.SelectedValue.ToString(), cboCuenta.SelectedValue.ToString(),
                dtpFecha.Value.ToShortDateString() + " 00:00:00", dtpFecha.Value.ToShortDateString() + " 23:59:59");
            if (data.Rows.Count > 0)
            {
                dtgvData.DataSource = data;
                //dtgvDataView.Columns.View.SelectAll();
                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["MONTO"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "MONTO", "REAL={0:n2}");
                dtgvDataView.Columns["MONTO"].SummaryItem.Tag = 1;
                dtgvDataView.Columns["PERSONA"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "MONTO", "PENDIENTE={0:n2}");
                dtgvDataView.Columns["PERSONA"].SummaryItem.Tag = 2;
                dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }
        private void dtgvDataView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            // ID = TAG 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
            GridView View = sender as GridView;
            
            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                real = 0;
                pendiente = 0;
            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch (summaryID)
                {
                    case 1:
                        if (View.IsRowSelected(e.RowHandle))
                        {
                            real += Convert.ToDecimal(View.GetRowCellValue(e.RowHandle, "MONTO"));
                        }
                        break;
                    case 2:
                        if (View.IsRowSelected(e.RowHandle) == false)
                        {
                            pendiente += Convert.ToDecimal(View.GetRowCellValue(e.RowHandle, "MONTO"));
                        }
                        break;
                }
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                switch (summaryID)
                {
                    case 1:
                        e.TotalValue = real;
                        break;
                    case 2:
                        e.TotalValue = pendiente;
                        break;
                }
            }
        }
        private void dtgvDataView_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e) 
        {
            dtgvDataView.UpdateSummary();
            dtgvDataView.BestFitColumns();
        }
        private void btnLeerArchivo_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult ();
            
            OpenFileDialog openfiledialog1 = new OpenFileDialog();
            openfiledialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            dr = openfiledialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                string filename = openfiledialog1.FileName;
                DataTable dt = ExcelToDataTable(filename, "");///HOJA111111111
                FormateaDataTable(dt,cboCuenta.Text);
                dtgvData2.DataSource = null;
                dtgvDataView2.Columns.Clear();
                dtgvData2.DataSource = dt;
                if (cboCuenta.Text != "INTERBANK MN") 
                { 
                    dtgvDataView2.Columns["MONTO"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDataView2.Columns["MONTO"].DisplayFormat.FormatString = "n2";
                    dtgvDataView2.Columns["MONTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "TOTAL={0:n2}");
                }
                dtgvDataView2.BestFitColumns();
            }
        }
        public DataTable ExcelToDataTable(string pathName, string sheetName)
        {
            DataTable tbContainer = new DataTable();
            string strConn = string.Empty;
            if (string.IsNullOrEmpty(sheetName)) { sheetName = "Hoja1"; }
            FileInfo file = new FileInfo(pathName);
            if (!file.Exists) { throw new Exception("Error, archivo no existe!"); }
            string extension = file.Extension;
            switch (extension)
            {
                case ".xls":
                    strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'", pathName);
                    //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                    break;
                case ".xlsx":
                    strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'", pathName);
                    //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
                    break;
                default:
                    strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'", pathName);
                    //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                    break;
            }
            OleDbConnection cnnxls = new OleDbConnection(strConn);
            OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [{0}$]", sheetName), cnnxls);
            DataSet ds = new DataSet();
            oda.Fill(tbContainer);
            return tbContainer;
        }
        public void FormateaDataTable(DataTable dt,string banco) 
        {
            switch (banco) 
            {
                case "BCP MN": 
                    dt.Rows[0].Delete();
                    dt.Rows[1].Delete();
                    dt.Rows[2].Delete();
                    dt.Rows[3].Delete();
                    dt.Columns.RemoveAt(1);
                    dt.Columns.RemoveAt(3);
                    dt.Columns.RemoveAt(3);
                    dt.Columns.RemoveAt(4);
                    dt.Columns.RemoveAt(4);
                    dt.Columns.RemoveAt(4);
                    dt.Columns.RemoveAt(4);
                    dt.Columns[3].SetOrdinal(1);
                    dt.Columns[0].ColumnName = "FECHA";
                    dt.Columns[1].ColumnName = "N° OPERACION";
                    dt.Columns[2].ColumnName = "DESCRIPCION";
                    dt.Columns[3].ColumnName = "MONTO";
                    break;
                case "BCP ME":
                    dt.Rows[0].Delete();
                    dt.Rows[1].Delete();
                    dt.Rows[2].Delete();
                    dt.Rows[3].Delete();
                    dt.Columns.RemoveAt(1);
                    dt.Columns.RemoveAt(3);
                    dt.Columns.RemoveAt(3);
                    dt.Columns.RemoveAt(4);
                    dt.Columns.RemoveAt(4);
                    dt.Columns.RemoveAt(4);
                    dt.Columns.RemoveAt(4);
                    dt.Columns[3].SetOrdinal(1);
                    dt.Columns[0].ColumnName = "FECHA";
                    dt.Columns[1].ColumnName = "N° OPERACION";
                    dt.Columns[2].ColumnName = "DESCRIPCION";
                    dt.Columns[3].ColumnName = "MONTO";
                    break;
                case "BBVA MN":
                    dt.Rows[0].Delete();
                    dt.Rows[1].Delete();
                    dt.Rows[2].Delete();
                    dt.Rows[3].Delete();
                    dt.Rows[4].Delete();
                    dt.Rows[5].Delete();
                    dt.Rows[6].Delete();
                    dt.Rows[7].Delete();
                    dt.Rows[8].Delete();
                    dt.Rows[9].Delete();
                    dt.Rows[10].Delete();
                    dt.Rows[11].Delete();
                    dt.Columns.RemoveAt(3);
                    dt.Columns[3].SetOrdinal(1);
                    dt.Columns[0].ColumnName = "FECHA";
                    dt.Columns[1].ColumnName = "N° OPERACION";
                    dt.Columns[2].ColumnName = "DESCRIPCION";
                    dt.Columns[3].ColumnName = "MONTO";
                    break;
                case "BBVA ME":
                    dt.Rows[0].Delete();
                    dt.Rows[1].Delete();
                    dt.Rows[2].Delete();
                    dt.Rows[3].Delete();
                    dt.Rows[4].Delete();
                    dt.Rows[5].Delete();
                    dt.Rows[6].Delete();
                    dt.Rows[7].Delete();
                    dt.Rows[8].Delete();
                    dt.Rows[9].Delete();
                    dt.Rows[10].Delete();
                    dt.Rows[11].Delete();
                    dt.Columns.RemoveAt(3);
                    dt.Columns[3].SetOrdinal(1);
                    dt.Columns[0].ColumnName = "FECHA";
                    dt.Columns[1].ColumnName = "N° OPERACION";
                    dt.Columns[2].ColumnName = "DESCRIPCION";
                    dt.Columns[3].ColumnName = "MONTO";
                    break;

                case "INTERBANK MN":
                    dt.Rows[0].Delete();
                    dt.Rows[1].Delete();
                    dt.Rows[2].Delete();
                    dt.Rows[3].Delete();
                    dt.Rows[4].Delete();
                    dt.Rows[5].Delete();
                    dt.Rows[6].Delete();
                    dt.Rows[7].Delete();
                    dt.Rows[8].Delete();
                    dt.Rows[9].Delete();
                    dt.Rows[10].Delete();
                    dt.Rows[11].Delete();
                    dt.Rows[12].Delete();
                    dt.Rows[13].Delete();
                    dt.Rows[14].Delete();
                    dt.Rows[15].Delete();
                    dt.Rows[16].Delete();
                    dt.Columns.RemoveAt(1);
                    dt.Columns.RemoveAt(5);
                    dt.Columns.RemoveAt(5);
                    dt.Columns.RemoveAt(5);
                    dt.Columns[2].SetOrdinal(1);
                    dt.Columns[0].ColumnName = "FECHA";
                    dt.Columns[1].ColumnName = "N° OPERACION";
                    dt.Columns[2].ColumnName = "DESCRIPCION";
                    dt.Columns[3].ColumnName = "CARGO";
                    dt.Columns[4].ColumnName = "ABONO";
                    break;
            }
        }
    }
}

