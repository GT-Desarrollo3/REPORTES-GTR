using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class ReporteKardexCombustible : MetroFramework.Forms.MetroForm
    {
        public ReporteKardexCombustible()
        {
            InitializeComponent();
        }

        private void CargaCombustible_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 76;
            dtpPeriodo.Value = new DateTime(dtpPeriodo.Value.Year, dtpPeriodo.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            dtgvDespachosDiarios.DataSource = null;
            dtgvDespachosDiariosView.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();

            int filtro = 0;

            dt.Clear();

            dt = clsCombustibleBL.Instancia.ObtenerReporteKardex_Surtidor_Otros(dtpPeriodo.Text, filtro);
            
            
            if (dt.Rows.Count > 0)
            {
                
                dtgvDespachosDiarios.DataSource = dt;
              
                ////Add icons to the icon set.
                //iconSet.Icons.Add(icon1);
                //iconSet.Icons.Add(icon2);
                //iconSet.Icons.Add(icon3);

                ////Specify the rule type.
                //gridFormatRule.Rule = formatConditionRuleIconSet;
                ////Specify the column to which formatting is applied.
                //////gridFormatRule.Column = dtgvDespachosDiariosView.Columns["DIF"];
                ////Add the formatting rule to the GridView.
                //dtgvDespachosDiariosView.FormatRules.Add(gridFormatRule);
                dtgvDespachosDiariosView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

                if (dtgvDespachosDiarios.DataSource == null)
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
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Kardex " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvDespachosDiarios.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }        

        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvDespachosDiarios.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvDespachosDiarios.ShowPrintPreview();
            }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray(0, 1)[0];
        }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = e.KeyChar.ToString().ToUpper().ToCharArray(0, 1)[0];
        }


        private void verInformacionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInformacionDespacho frm = new FrmInformacionDespacho();

            //dtgvDespachosDiariosView.GetSelectedRows;

            
            string Placa = "";
            DateTime FechaProgramacion = DateTime.Now;

            foreach(var i in dtgvDespachosDiariosView.GetSelectedRows())
            {
                Placa = dtgvDespachosDiariosView.GetDataRow(i)["PLACA"].ToString();
                FechaProgramacion = Convert.ToDateTime(dtgvDespachosDiariosView.GetDataRow(i)["FECHAPROG_VIAJE"].ToString());
            }

            frm.Placa = Placa;
            frm.FechaProgramacion = FechaProgramacion;
            frm.ShowDialog();

        }
    }
}
