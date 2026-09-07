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
using DevExpress.DashboardWin.Native;

namespace ReportesTranspesa.Sistema
{
    public partial class ControlPeriodos : MetroFramework.Forms.MetroForm
    {
        public ControlPeriodos()
        {
            InitializeComponent();
        }

        public void Periodos()
        {
            DateTime Fechaini;
            DateTime Fechafin;
            DateTime fecha = DateTime.Now;
            Fechaini = new DateTime(fecha.Year, fecha.Month, 1);
            txtFechaIni.Text = Fechaini.ToShortDateString();
            Fechafin = new DateTime(fecha.Year, fecha.Month, 1).AddMonths(1).AddDays(-1);
            txtFechaFin.Text = Fechafin.ToShortDateString();
        }

        private void ControlPeriodos_Load(object sender, EventArgs e)
        {
            Periodos();
            cboPeriodo.SelectedIndex = 0;
            cboReporte.SelectedIndex = 0;
            FechaPeriodos(); ;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (cboPeriodo.SelectedIndex == 0)
            {
                dtgvData.DataSource = null;
                string reporte = "";
                reporte = cboReporte.Text.Trim();
                DataTable dt = new DataTable();
                dt = clsReporteBL.Instancia.ListaPeriodos(reporte);
                if (dt.Rows.Count > 0)
                {
                    dtgvData.DataSource = dt;
                    dtgvDataView.Columns["Id"].Visible = false;
                    dtgvDataView.BestFitColumns();
                }
            }
            else
            {
                dtgvData.DataSource = null;
                string reporte = "";
                switch (cboReporte.SelectedIndex)
                {
                    case 0: reporte = "";
                        break;

                    case 1: reporte = cboReporte.Text.Trim();
                        break;

                    case 2: reporte = cboReporte.Text.Trim();
                        break;
                }
                //reporte = cboReporte.Text.Trim();
                string periodo = "";
                periodo = cboPeriodo.Text.Trim();
                DataTable dt = new DataTable();
                dt = clsReporteBL.Instancia.ComboPorPeriodos(reporte,periodo);
                if (dt.Rows.Count > 0)
                {
                    dtgvData.DataSource = dt;
                    dtgvDataView.Columns["Id"].Visible = false;
                    dtgvDataView.BestFitColumns();
                }
                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hay data para mostrar";
                    m.ShowDialog();
                }
            }
        }

        public void FechaPeriodos()
        {
            string formulario = "";
            formulario = cboReporte.Text.Trim();
            DataTable dt = new DataTable();
            dt = clsReporteBL.Instancia.ComboPeriodos(formulario);
            if(dt.Rows.Count > 0)
            {
                DataRow dr = dt.NewRow();
                dr[0] = "TODOS";
                dt.Rows.InsertAt(dr, 0);
                cboPeriodo.DataSource = dt;
                cboPeriodo.ValueMember = "Periodo";
                cboPeriodo.DisplayMember = "Periodo";
            }
        }

        private void cboPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FechaPeriodos();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string formulario = "";
            formulario = cboReporte.Text.Trim();
            string fechaini = txtFechaIni.Text;
            string fechafin = txtFechaFin.Text;
            clsReporteBL.Instancia.Registrar_Nuevo_Periodo(formulario, fechaini, fechafin);
            string Peridodo="";
            Peridodo = txtFechaIni.Text.Substring(6,4)+"-"+txtFechaFin.Text.Substring(3,2);
            //Peridodo = DateTime.Now.Year.ToString()+"-"+DateTime.Now.Month.ToString();
            Mensaje m = new Mensaje();
            m.mensaje = "Se Agrego Nuevo Periodo: " + Peridodo;
            m.ShowDialog();
            ListaPeriodos();
        }

        public void ListaPeriodos()
        {
            FechaPeriodos();
            dtgvData.DataSource = null;
            string reporte = "";
            reporte = cboReporte.Text.Trim();
            DataTable dt = new DataTable();
            dt = clsReporteBL.Instancia.ListaPeriodos(reporte);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["Id"].Visible = false;
                dtgvDataView.BestFitColumns();
            }
        }
    }
}

