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
using System.Globalization;
using System.Diagnostics;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    public partial class Facturacion_Contado : MetroFramework.Forms.MetroForm
    {
        public Facturacion_Contado()
        {
            InitializeComponent();
        }

        char sucursal = 'T';
        //char sucursal1 = 'T';
        //char sucursal2 = 'L';
        //char sucursal3 = 'S';

        private void Facturacion_Contado_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            cboAlmacen.SelectedIndex = 0;
        }

        private void rbLima_CheckedChanged(object sender, EventArgs e)
        {/*
            if (rbLima.Checked == true)
            {
                sucursal2 = 'L';
            }
            else
            {
                if (rbTrujillo.Checked == true)
                {
                    sucursal1 = 'T';
                }
                else
                {
                    sucursal3 = 'S';
                }
            }*/
        }

        private void rbTrujillo_CheckedChanged(object sender, EventArgs e)
        {/*
            if (rbTrujillo.Checked == true)
            {
                sucursal1 = 'T';
            }
            else
            {
                if (rbLima.Checked == true)
                {
                    sucursal2 = 'L';
                }
                else
                {
                    sucursal3 = 'S';
                }
            }*/
        }

        private void rbSalaverry_CheckedChanged(object sender, EventArgs e)
        {/*
            if (rbSalaverry.Checked == true)
            {
                sucursal3 = 'S';
            }
            else
            {
                if (rbLima.Checked == true)
                {
                    sucursal2 = 'L';
                }
                else
                {
                    sucursal1 = 'T';
                }
            }*/
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            switch (cboAlmacen.SelectedIndex)
            {
                case 0: //Almacenes Trujillo
                    sucursal = 'T';
                    break;
                case 1: //Almacenes Lima
                    sucursal = 'L';
                    break;
                case 2: //Almacenes Salaverry
                    sucursal = 'S';
                    break;
            }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsFinanzasBL.Instancia.GetFacturacionContadoActualizado(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", sucursal);
            //dt = clsFinanzasBL.Instancia.GetFacturacionContado(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            //    dtpFechaFin.Value.ToShortDateString() + " 23:59:59", sucursal);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["TN"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["TN"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["V.VENTA"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["V.VENTA"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["IGV"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["IGV"].DisplayFormat.FormatString = "n2";
                dtgvDataView.Columns["TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["TOTAL"].DisplayFormat.FormatString = "n2";

                GridColumnSummaryItem item = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TOTAL", "Total:={0:n2}");
                dtgvDataView.Columns["TOTAL"].Summary.Add(item);
                dtgvDataView.BestFitColumns();
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
                string nombre = System.IO.Path.Combine(desktop, "Facturación Contado del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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

        private void dtgvDataView_PrintInitialize(object sender, DevExpress.XtraGrid.Views.Base.PrintInitializeEventArgs e)
        {
            DevExpress.XtraPrinting.PrintingSystemBase pb = (DevExpress.XtraPrinting.PrintingSystemBase)(e.PrintingSystem);
            pb.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            pb.PageSettings.LeftMargin = 50;
            //pb.PageSettings.MarginsF.Left = 13;
            pb.PageSettings.RightMargin = 50;
            //pb.PageSettings.MarginsF.Right = 13;
            pb.PageSettings.TopMargin = 50;
            pb.PageSettings.BottomMargin = 50;
        }


    }
}
