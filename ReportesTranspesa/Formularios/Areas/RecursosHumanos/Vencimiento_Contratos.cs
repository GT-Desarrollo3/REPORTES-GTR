using System;
using System.Drawing;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Diagnostics;
using System.Globalization;
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.Utils;
using DevExpress.XtraPrintingLinks;
using Word = Microsoft.Office.Interop.Word;
using Comun;
namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class Vencimiento_Contratos : MetroFramework.Forms.MetroForm
    {
        public Vencimiento_Contratos()
        {
            InitializeComponent();
        }

        private void Vencimiento_Contratos_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = new DateTime(dtpFechaFin.Value.Year, dtpFechaFin.Value.Month, DateTime.DaysInMonth(dtpFechaFin.Value.Year, dtpFechaFin.Value.Month));
            cboArea.SelectedIndex = 0;
            cboEmpresa.SelectedIndex = 0;
        }
        bool logobra;
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            string transpesa="";
            string bra = "";
            string altra = "";
            string amt = "";
            string aduanas = "";
            //if (chkTranspesa.Checked == true)
            //{
            //    transpesa = "10000000";
            //}
            //if(chkBra.Checked == true)
            //{
            //    bra = "40000000";
            //}
            if (transpesa == "" && bra != "")
            {
                logobra = true;
            }
            else
            {
                logobra = false;
            }

            switch (cboEmpresa.SelectedIndex)
            {
                case 0: //transpesa
                    transpesa = "10000000";
                    break;
                case 1: //bra
                    bra = "40000000";
                    break;
                case 2: //altra
                    altra = "50000000";
                    break;
                case 3: //amt
                    amt = "60000000";
                    break;
                case 4: //aduanas
                    aduanas = "70000000";
                    break;
            }

            if (cboArea.SelectedIndex == 0)
            {
                dt = clsRecursosHumanosBL.Instancia.GetVencimientoContratosTodos(dtpFechaIni.Text, dtpFechaFin.Text, bra, transpesa, altra, amt, aduanas);
                if (dt.Rows.Count > 0)
                {
                    dtgvData.DataSource = dt;
                    dtgvDataView.Columns["SUELDO"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDataView.Columns["SUELDO"].DisplayFormat.FormatString = "c0";
                    dtgvDataView.BestFitColumns();
                }
                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No se encontraron resultados.";
                    m.ShowDialog();
                }
            }
            else
            {
                dt = clsRecursosHumanosBL.Instancia.GetDataVencimientoContratos(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                    dtpFechaFin.Value.ToShortDateString() + " 23:59:59", cboArea.Text.Trim(), transpesa, bra, altra, amt, aduanas);
                if (dt.Rows.Count > 0)
                {
                    dtgvData.DataSource = dt;
                    dtgvDataView.Columns["SUELDO"].DisplayFormat.FormatType = FormatType.Numeric;
                    dtgvDataView.Columns["SUELDO"].DisplayFormat.FormatString = "c0";
                    dtgvDataView.BestFitColumns();
                }
                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No se encontraron resultados.";
                    m.ShowDialog();
                }
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
                string fechaini = dtpFechaIni.Value.ToString("dd_MM_yyyy"); 
                string fechafin = dtpFechaFin.Value.ToString("dd_MM_yyyy");

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Vencimiento de contratos del " + fechaini + " al "+ fechafin + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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
                CompositeLink composLink = new CompositeLink(new PrintingSystem());
                composLink.CreateMarginalHeaderArea += new CreateAreaEventHandler(composLink_CreateMarginalHeaderArea);
                PrintableComponentLink pcLink1 = new PrintableComponentLink();
                Link linkGrid1Report = new Link();
                linkGrid1Report.CreateDetailArea += new CreateAreaEventHandler(linkGrid1Report_CreateDetailArea);
                pcLink1.Component = this.dtgvData;
                composLink.Links.Add(linkGrid1Report);
                composLink.Links.Add(pcLink1);
                composLink.ShowPreviewDialog();
            }
        }

        public void composLink_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            Image imagen;
            if (logobra == true)
            {
                imagen = Image.FromFile(@"C:\Transpesa\Logos\bra.jpg");
            }
            else
            {
                imagen = Image.FromFile(@"C:\Transpesa\Logos\transpesa.jpg");
            }
            e.Graph.DrawImage(imagen, new RectangleF(0, 0, 150, 40));
            e.Graph.DrawString(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString(), new RectangleF(800,0, 150, 20));
            e.Graph.DrawString(Utilitario.Instancia.SesionUsuario.usuario, new RectangleF(680, 20, 300, 20));
            
        }

        public void linkGrid1Report_CreateDetailArea(object sender, CreateAreaEventArgs e)
        {
            string area;
            if (cboArea.Text.Trim() == "OPERADORES DE FLOTA") 
            {
                area = "OPER. DE FLOTA";
            }
            else
            {
                area = cboArea.Text.Trim();
            }
            TextBrick tb = new TextBrick();
            tb.Text = "REPORTE DE VENCIMIENTO DE CONTRATOS AL " + dtpFechaFin.Value.ToShortDateString() + " (ÁREA:"+area+")";
            tb.Font = new Font("Arial", 15);
            tb.Rect = new RectangleF(0, 0, 900, 25);
            tb.BorderWidth = 0;
            tb.BackColor = Color.Transparent;
            tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            e.Graph.DrawBrick(tb);
        }
    }
}
