using System;
using System.ComponentModel;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;
using System.IO;
using System.Threading;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class Generacion_Libros : MetroFramework.Forms.MetroForm
    {
        public Generacion_Libros()
        {
            InitializeComponent();
        }

        System.Data.DataTable dt = new System.Data.DataTable();
        string compañia = "10000";
        int tipo = 0;
        string periodofecha;
        private void Registro_Ventas_Load(object sender, EventArgs e)
        {
            cboCompania.SelectedIndex = 0;
            cboTipo.SelectedIndex = 0;
            CheckForIllegalCrossThreadCalls = false;
            pgbProgreso.Properties.Step = 1;
            pgbProgreso.Properties.PercentView = true;
            pgbProgreso.Properties.Maximum = 100;
            pgbProgreso.Properties.Minimum = 0;

        }

        private void BuscaData() 
        {
            dt = clsContabilidadBL.Instancia.GetDataLibro(tipo, periodofecha, compañia);
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
               dc.ReadOnly = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            chkIndice.Enabled = false;
            pgbProgreso.Visible = true;
            switch (cboCompania.SelectedIndex)
            {
                case 0: //transpesa
                    compañia = "10000";
                    break;
                case 1: //bra
                    compañia = "40000";
                    break;
                case 2: //altra
                    compañia = "50000";
                    break;
                case 3: //amt
                    compañia = "60000";
                    break;
                case 4: //agencia aduanas
                    compañia = "70000";
                    break;
            }
            tipo = cboTipo.SelectedIndex;
            periodofecha = txtPeriodo.Text.Trim();
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            ThreadStart delegado = new ThreadStart(BuscaData);
            Thread hilo = new Thread(delegado);
            hilo.Start();
            while(hilo.IsAlive){
                pgbProgreso.PerformStep();
                pgbProgreso.Update();
                if (Convert.ToInt32(pgbProgreso.EditValue) == 100)
                {
                    pgbProgreso.EditValue = 0;
                }
            }
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();
                chkIndice.Enabled = true;
                chkIndice.Checked = false;
            }
            else
            {
                Mensaje n = new Mensaje();
                n.mensaje = "No hay data para mostrar";
                n.ShowDialog();
                chkIndice.Enabled = false;
                chkIndice.Checked = false;
            }
            pgbProgreso.Refresh();
            pgbProgreso.EditValue = 0;
            pgbProgreso.Visible = false;
            
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "Notepad Documents (*.txt)|*.txt";

           switch (cboCompania.SelectedIndex)
           {
               case 0: //transpesa
                   //compañia = "10000";
                   sfd.FileName = "LE20439331918";
                   break;
               case 1: //bra
                   //compañia = "40000";
                   sfd.FileName = "LE20477313214";
                   break;
               case 2: //altra
                   //compañia = "50000";
                   sfd.FileName = "LE20601544262";
                   break;
               case 3: //amt
                   //compañia = "60000";
                   sfd.FileName = "LE20565650271";
                   break;
               case 4: //aduana
                   //compañia = "70000";
                   sfd.FileName = "LE20559882853";
                   break;
           }

            switch (tipo) 
            {
                case 0:
                    sfd.FileName = sfd.FileName + txtPeriodo.Text.Trim() + "00080100001111.txt";
                    break;
                case 1:
                    sfd.FileName = sfd.FileName + periodofecha + "00140100001111.txt";
                    break;
                case 2:
                    sfd.FileName = sfd.FileName + periodofecha + "00060100001111.txt";
                    break;
                case 3:
                    sfd.FileName = sfd.FileName + periodofecha + "00050100001111.txt";
                    break;
                case 4:
                    sfd.FileName = sfd.FileName + periodofecha + "00050300001111.txt";
                    break;
            }

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                ToCsV(dtgvDataView, sfd.FileName);
                if(tipo == 0)
                {
                    int l;
                    l = sfd.FileName.ToString().Length;
                    var aStringBuilder = new StringBuilder(sfd.FileName.ToString());
                    aStringBuilder.Remove(l-13, 1);
                    aStringBuilder.Insert(l-13, "2");
                    aStringBuilder.Remove(l-7, 1);
                    aStringBuilder.Insert(l-7, "0");
                    File.Create(aStringBuilder.ToString());
                }
            }
        }

        private void ToCsV(GridView dGV, string filename)
        {
            string stOutput = "";

            pgbProgreso.Visible = true;
            pgbProgreso.Properties.Maximum = dGV.RowCount;

            //Export data.
            for (int i = 0; i <= dGV.RowCount - 1; i++)
            {
                string stLine = "";
                int rowHandle = dGV.GetVisibleRowHandle(i);

                for (int j = 0; j < dGV.VisibleColumns.Count; j++)
                {
                    stLine = stLine.ToString() + dGV.GetRowCellValue(rowHandle, dGV.VisibleColumns[j]).ToString() + "|";
                    pgbProgreso.PerformStep();
                    pgbProgreso.Update();
                }
                stOutput += stLine + "\r\n";
            }

            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
            pgbProgreso.EditValue = 0;
            pgbProgreso.Visible = false;
            Mensaje m = new Mensaje();
            m.mensaje = "Archivo generado.";
            m.ShowDialog();
        }

        private void dtgvDataView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
                e.Value = e.ListSourceRowIndex + 1;
        }

        private void chkIndice_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndice.Checked == true) 
            {
                var col = dtgvDataView.Columns.Add();
                col.VisibleIndex = 0;
                col.FieldName = "#";
                col.Visible = true;
                col.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                dtgvDataView.CustomUnboundColumnData += dtgvDataView_CustomUnboundColumnData;
            }
            else
            {
                dtgvDataView.Columns["#"].Visible = false;
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
                //string nombre = System.IO.Path.Combine(desktop, cboTipo.Text + " del Periodo: " + txtPeriodo.Text + ".xlsx");
                //string nombre = System.IO.Path.Combine(desktop, "Libros Electronicos " + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                //string nombre = System.IO.Path.Combine(desktop, cboTipo.Text + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                string nombre = System.IO.Path.Combine(desktop, cboTipo.Text + " " + txtPeriodo.Text + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void cboCompania_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
