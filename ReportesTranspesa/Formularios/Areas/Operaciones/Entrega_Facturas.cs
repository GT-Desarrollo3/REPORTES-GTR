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
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class Entrega_Facturas : MetroFramework.Forms.MetroForm
    {
        public Entrega_Facturas()
        {
            InitializeComponent();
        }

        private void Entrega_Facturas_Load(object sender, EventArgs e)
        {
            //dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtgvDataView.OptionsBehavior.Editable = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string serie = txtSerie.Text.Trim();
            string tipofecha = "P";
            if (rbEmision.Checked == true) 
            {
                tipofecha = "E";
            }
            if (serie == "") 
            {
                Mensaje m = new Mensaje();
                m.mensaje = "Ingresa una serie";
                m.ShowDialog();
            }
            else 
            {
                dtgvData.DataSource = null;
                dtgvDataView.Columns.Clear();
                dtgvDataView.GroupSummary.Clear();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsOperacionesBL.Instancia.GetFacturas(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                    dtpFechaFin.Value.ToShortDateString() + " 23:59:59",serie,tipofecha);
                if (dt.Rows.Count > 0)
                {
                    dtgvData.DataSource = dt;
                    if (Utilitario.Instancia.SesionUsuario.usuario == "LSEGURA")
                    {
                        foreach (System.Data.DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == "FINANZAS")
                            {
                                dc.ReadOnly = false;
                            }
                        }
                    }

                    if (Utilitario.Instancia.SesionUsuario.usuario == "ITERRONES")
                    {
                        foreach (System.Data.DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == "FINANZAS")
                            {
                                dc.ReadOnly = false;
                            }
                        }
                    }

                    if (Utilitario.Instancia.SesionUsuario.usuario == "JBOBADILLA")
                    {
                        foreach (System.Data.DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == "FINANZAS")
                            {
                                dc.ReadOnly = false;
                            }
                            else 
                            {
                                dc.ReadOnly = true;
                            }
                        }
                    }
                    if (Utilitario.Instancia.SesionUsuario.usuario == "MLEZCANO")
                    {
                        foreach (System.Data.DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == "FINANZAS")
                            {
                                dc.ReadOnly = false;
                            }
                        }
                    }
                    if (Utilitario.Instancia.SesionUsuario.usuario == "BGUZMAN" || Utilitario.Instancia.SesionUsuario.usuario == "JBOBADILLA")
                    {
                        foreach (System.Data.DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == "CONTABILIDAD")
                            {
                                dc.ReadOnly = false;
                            }
                            else
                            {
                                dc.ReadOnly = true;
                            }
                        }
                    }
                    if (Utilitario.Instancia.SesionUsuario.usuario == "LVALDERRAMA" || Utilitario.Instancia.SesionUsuario.usuario == "RALAYO" || Utilitario.Instancia.SesionUsuario.usuario == "JBOBADILLA")
                    {
                        foreach (System.Data.DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName == "OP/FINANZAS" || dc.ColumnName == "OP/CONTABILIDAD")
                            {
                                dc.ReadOnly = false;
                            }
                            else
                            {
                                dc.ReadOnly = true;
                            }
                        }
                    }
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
             if (dtgvData.DataSource != null) 
            {
                for (int i = 0; i < dtgvDataView.DataRowCount; i++) 
                {
                    bool resultado;
                    resultado =clsOperacionesBL.Instancia.UpdateFactura(
                    dtgvDataView.GetRowCellValue(i, "N° DOC.").ToString(),
                    Convert.ToInt32(dtgvDataView.GetRowCellValue(i, "FINANZAS")),
                    Convert.ToInt32(dtgvDataView.GetRowCellValue(i, "OP/FINANZAS")),
                    Convert.ToInt32(dtgvDataView.GetRowCellValue(i, "CONTABILIDAD")),
                    Convert.ToInt32(dtgvDataView.GetRowCellValue(i, "OP/CONTABILIDAD"))
                    );
                    if (resultado != true)
                    {
                        Mensaje m = new Mensaje();
                        m.Width = 500;
                        m.mensaje = "No existe el documento " + dtgvDataView.GetRowCellValue(i, "N° DOC.").ToString();
                        m.ShowDialog();
                        break;
                    }
                }
                Mensaje m2 = new Mensaje();
                m2.mensaje = "Se actualizaron las facturas";
                m2.ShowDialog();
            }
        }
        private Microsoft.Office.Interop.Excel.Application app;
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
                string nombre = System.IO.Path.Combine(desktop, "Entrega Facturas ("+ txtSerie.Text+") del "
                    + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + 
                    Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));
            }
        }
    }
}
