using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class ProductosxRotacion : MetroFramework.Forms.MetroForm
    {
        public ProductosxRotacion()
        {
            InitializeComponent();
        }

        private void ProductosxRotacion_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechaini = "";
            string fechafin = "";
            fechaini = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechafin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
            #region Condicion
            //if (Convert.ToInt32(dtpFechaIni.Value.Month.ToString()) < 10 && Convert.ToInt32(dtpFechaFin.Value.Month.ToString()) < 10)
            //{
            //    //var fechaini = dtpFechaIni.Value.Year.ToString() + "0" + dtpFechaIni.Value.Month.ToString();
            //    //var fechafin = dtpFechaFin.Value.Year.ToString() + "0" + dtpFechaFin.Value.Month.ToString();
            //    System.Data.DataTable dt = new System.Data.DataTable();
            //    dt = clsLogisticaBL.Instancia.GetProductosxRotacion(fechaini, fechafin);
            //    if (dt.Rows.Count > 0)
            //    {
            //        dtgvItem.DataSource = dt;
            //        dtgvViewItem.BestFitColumns();
            //    }
            //    else
            //    {
            //        Mensaje m = new Mensaje();
            //        m.mensaje = "No hay data para mostrar";
            //        m.ShowDialog();
            //    }
            //}
            //else
            //{
            //   //var fechaini = dtpFechaIni.Value.Year.ToString() + dtpFechaIni.Value.Month.ToString();
            //   //var fechafin = dtpFechaFin.Value.Year.ToString() + dtpFechaFin.Value.Month.ToString();
            //   System.Data.DataTable dt = new System.Data.DataTable();
            //   dt = clsLogisticaBL.Instancia.GetProductosxRotacion(fechaini, fechafin);
            //   if (dt.Rows.Count > 0)
            //   {
            //       dtgvItem.DataSource = dt;
            //       dtgvViewItem.BestFitColumns();
            //   }
            //   else
            //   {
            //       Mensaje m = new Mensaje();
            //       m.mensaje = "No hay data para mostrar";
            //       m.ShowDialog();
            //   }
            //}
            #endregion
            //fechaini = dtpFechaIni.Value.Year.ToString() + dtpFechaIni.Value.Month.ToString();
            //fechafin = dtpFechaFin.Value.Year.ToString() + dtpFechaFin.Value.Month.ToString();
            //dtgvItem.DataSource = null;
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsLogisticaBL.Instancia.GetProductosxRotacion(fechaini, fechafin);
            if (dt.Rows.Count > 0)
            {
                dtgvItem.DataSource = dt;
                dtgvViewItem.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void btnExcelDetalle_Click(object sender, EventArgs e)
        {
            if (dtgvItem.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Productos por Rotación del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvItem.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvItem.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvItem.ShowPrintPreview();
            }
        }
    }
}
