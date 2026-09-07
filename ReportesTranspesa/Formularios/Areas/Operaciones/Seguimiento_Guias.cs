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
using DevExpress.XtraPivotGrid;
using System.Globalization;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraEditors.Repository;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class Seguimiento_Guias : MetroFramework.Forms.MetroForm
    {
        public Seguimiento_Guias()
        {
            InitializeComponent();
        }



        private void chkEstado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEstado.Checked == true)
            {
                cboEstado.Enabled = true;
                cboEstado.SelectedIndex = 0;
                //dtpFechaIni.Enabled = true;
                //dtpFechaFin.Enabled = true;
                txtParametro.Enabled = true;
                cboParametro.Enabled = true;
                dtpFechaIni.Enabled = false;
                dtpFechaFin.Enabled = false;
            }
            else
            {
                cboEstado.Enabled = false;
                cboEstado.SelectedIndex = -1;
                //dtpFechaIni.Enabled = false;
                //dtpFechaFin.Enabled = false;
                txtParametro.Text = "";
                txtParametro.Enabled = false;
                cboParametro.Enabled = false;
                dtpFechaIni.Enabled = true;
                dtpFechaFin.Enabled = true;
            }
        }

        private void chkSerie_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSerie.Checked == true)
            {
                txtSerie.Enabled = true;
            }
            else
            {
                txtSerie.Text = "";
                txtSerie.Enabled = false;
            }
        }

        private void chkNumero_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNumero.Checked == true)
            {
                txtNumero.Enabled = true;
            }
            else
            {
                txtNumero.Text = "";
                txtNumero.Enabled = false;
            }
        }

        private void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEstado.SelectedIndex != 1)
            {
                cboParametro.SelectedIndex = 1;
            }
            else
            {
                cboParametro.SelectedIndex = 0;
            }
        }

        private void cboParametro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboParametro.Text == "Conductor" && cboEstado.SelectedIndex != 1)
            {
                cboParametro.SelectedIndex = 1;
            }
        }



        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            dtgvSeguimientoGuias.DataSource = null;
            dtgvDataView.Columns.Clear();

          string fechin = "01/01/1980";
          string fechfin = "31/12/2030";
          string ser = "";
          string num = "";
          string est = "TODOS";
          string param = "";
          fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
          fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
          if (chkSerie.Checked == true) { ser = txtSerie.Text; }
          if (chkNumero.Checked == true) { num = txtNumero.Text; }
          if (chkEstado.Checked == true)
          {
              est = cboEstado.Text;
              param = txtParametro.Text;
          }
            System.Data.DataTable dt = new System.Data.DataTable();
          dt = clsOperacionesBL.Instancia.GetDataSeguimientoGuias(fechin, fechfin, ser, num, est, cboParametro.SelectedIndex, param);
            if (dt.Rows.Count > 0)
            {
                dtgvSeguimientoGuias.DataSource = dt;
                dtgvDataView.BestFitColumns();
                //FormatearMontosTotales(dt);
                //lblEncontrados.Text = dt.Rows.Count.ToString();
                //CalcularTotalGrid();
               // foreach (DataGridViewColumn dt in dtgvDataView.Columns)
                //{
                  //  dt.ReadOnly = true;
                //}
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void btnExcel_Click_1(object sender, EventArgs e)
        {
            if (dtgvSeguimientoGuias.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Facturas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvSeguimientoGuias.ExportToXlsx(nombre);
                Process.Start(nombre);
            }

        }

        private void Operaciones_Seguimiento_Guias_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvSeguimientoGuias.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvSeguimientoGuias.ShowPrintPreview();
            }
        }

    }
}
