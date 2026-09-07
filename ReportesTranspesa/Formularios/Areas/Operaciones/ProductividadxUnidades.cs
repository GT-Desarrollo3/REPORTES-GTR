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
using DevExpress.Data;
using System.Globalization;
using System.Diagnostics;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmUnidadesProductivas : MetroFramework.Forms.MetroForm
    {
        public frmUnidadesProductivas()
        {
            InitializeComponent();
        }

        private void frmUnidadesProductivas_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //dtgvUnidadesView.Columns.Clear();
            //dtgvUnidades.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = null;
            gridView3.Columns.Clear();
            gridControl3.DataSource = null;
            gridView7.Columns.Clear();
            gridControl4.DataSource = null;
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            System.Data.DataTable dt3 = new System.Data.DataTable();
            System.Data.DataTable dt4 = new System.Data.DataTable();
            //dt = clsOperacionesBL.Instancia.GetLista_Tractos(dtpFechaIni.Value.ToShortDateString());
            dt1 = clsOperacionesBL.Instancia.GetLista_Consulta_Tracto_Mantenimiento(dtpFechaIni.Value.ToShortDateString());
            dt2 = clsOperacionesBL.Instancia.GetLista_Consulta_Tractos(dtpFechaIni.Value.ToShortDateString());
            dt3 = clsOperacionesBL.Instancia.GetLista_Consultar_Tractos_No_Programados();
            dt4 = clsOperacionesBL.Instancia.GetLista_Consultar_Tractos_Tiempo(dtpFechaIni.Value.ToShortDateString());
            //if (dt.Rows.Count > 0 || dt1.Rows.Count > 0 || dt2.Rows.Count > 0)
            if (dt1.Rows.Count > 0)
            {

                //dtgvUnidades.DataSource = dt;
                gridControl1.DataSource = dt1;
                gridControl2.DataSource = dt2;
                gridControl3.DataSource = dt3;
                gridControl4.DataSource = dt4;
                //gridView1.Columns["HORA"].DisplayFormat.FormatType = FormatType.DateTime;
                //gridView1.Columns["HORA"].DisplayFormat.FormatString = "hh:mm tt";
                //dtgvUnidadesView.BestFitColumns();
                gridView1.BestFitColumns();
                gridView2.BestFitColumns();
                gridView5.BestFitColumns();
                gridView7.BestFitColumns();
                //lblTotal.Text = dt.Rows.Count.ToString();
                lblTotalC.Text = dt1.Rows.Count.ToString();
                lblTotalP.Text = dt2.Rows.Count.ToString();
                lblTotalNP.Text = dt3.Rows.Count.ToString();

                if (dt1.Rows == dt2.Rows)
                {
                    System.Data.DataTable dt6 = new System.Data.DataTable();
                    dt6.Clear();
                    gridControl6.DataSource = dt6;
                    gridView11.BestFitColumns();

                }

                else 
                {
 
                }
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
                lblTotal.Text = "0";
                lblTotalC.Text = "0";
                lblTotalP.Text = "0";
                lblTotalNP.Text = "0";
            }

        }

        private void btnTransito_Click(object sender, EventArgs e)
        {
            Detalle_UnidadesTransito u = new Detalle_UnidadesTransito();
            u.mensaje = dtpFechaIni.Value.ToShortDateString();
            u.Show();
            
        }
    }

}