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
using ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Neumaticos
{
    public partial class frmKilometrajeUnidades : MetroFramework.Forms.MetroForm
    {
        DataTable dtListaKM = new DataTable();
        int filtro;

        public frmKilometrajeUnidades()
        {
            InitializeComponent();
        }

        private void frmKilometrajeUnidades_Shown(object sender, EventArgs e)
        {
            dtpFechaIni.Focus();
        }

        private void frmKilometrajeUnidades_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.AddDays(1);
        }


        private void ListarKMUnidades()
        {
            if (rbTracto.Checked == false && rbCarreta.Checked == false)
            {
                MessageBox.Show("Seleccione un filtro.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (rbTracto.Checked == true)
                {
                    dtgvKilometrajes.Visible = true;

                    dtListaKM = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_Consultar_KMVehiculos(dtpFechaIni.Text, dtpFechaFin.Text, txtTracto.Text, filtro);
                    dtgvKilometrajes.DataSource = dtListaKM;
                    if (dtListaKM.Rows.Count > 0)
                    {
                        dtgvKilometrajesView.Columns["FECHAPROG_VIAJE"].DisplayFormat.FormatType = FormatType.DateTime;
                        dtgvKilometrajesView.Columns["FECHAPROG_VIAJE"].DisplayFormat.FormatString = "dd/MM/yyyy";
                        dtgvKilometrajesView.Columns["KM"].DisplayFormat.FormatType = FormatType.Numeric;
                        dtgvKilometrajesView.Columns["KM"].DisplayFormat.FormatString = "N2";

                        dtgvKilometrajesView.Columns["KM"].Summary.Remove(dtgvKilometrajesView.Columns["KM"].SummaryItem);
                        dtgvKilometrajesView.Columns["KM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "KM", "Total = {0:N2}");

                        dtgvKilometrajesView.Columns["RecorridoTotal"].Summary.Remove(dtgvKilometrajesView.Columns["RecorridoTotal"].SummaryItem);
                        dtgvKilometrajesView.Columns["RecorridoTotal"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "RecorridoTotal", "Total = {0:N2}");

                        dtgvKilometrajesView.BestFitColumns();
                        dtgvKilometrajesView.ExpandAllGroups();
                    }
                }

                if(rbCarreta.Checked == true)
                {
                    dtgvKilometrajes.Visible = false;

                    dtListaKM = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_Consultar_KMVehiculos(dtpFechaIni.Text, dtpFechaFin.Text, txtTracto.Text, filtro);
                    dtgvDespachos.DataSource = dtListaKM;
                    if (dtListaKM.Rows.Count > 0)
                    {
                        dtgvDespachosView.Columns["DESPACHO"].DisplayFormat.FormatType = FormatType.DateTime;
                        dtgvDespachosView.Columns["DESPACHO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                        dtgvDespachosView.Columns["FECHAPROG_VIAJE"].DisplayFormat.FormatType = FormatType.DateTime;
                        dtgvDespachosView.Columns["FECHAPROG_VIAJE"].DisplayFormat.FormatString = "dd/MM/yyyy";
                        dtgvDespachosView.Columns["KM"].DisplayFormat.FormatType = FormatType.Numeric;
                        dtgvDespachosView.Columns["KM"].DisplayFormat.FormatString = "N2";

                        dtgvDespachosView.Columns["KM"].Summary.Remove(dtgvDespachosView.Columns["KM"].SummaryItem);
                        dtgvDespachosView.Columns["KM"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "KM", "Total = {0:N2}");

                        dtgvDespachosView.BestFitColumns();
                        dtgvDespachosView.ExpandAllGroups();
                    }
                }
            }
        }


        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                dtpFechaFin.Focus();
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarKMUnidades();
            }
        }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarKMUnidades();
            }
        }

        private void rbTracto_MouseUp(object sender, MouseEventArgs e)
        {
            filtro = 0;
        }

        private void rbCarreta_MouseUp(object sender, MouseEventArgs e)
        {
            filtro = 1;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarKMUnidades();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvKilometrajes.DataSource == null && dtgvDespachos.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Kilometraje de Unidades en Viajes - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss",dtfi) + ".xlsx");
                if (rbTracto.Checked == true)
                {
                    dtgvKilometrajes.ExportToXlsx(nombre);
                }

                if (rbCarreta.Checked == true)
                {
                    dtgvDespachos.ExportToXlsx(nombre);
                }
                Process.Start(nombre);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvKilometrajes.DataSource == null && dtgvDespachos.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para imprimir.";
                m.ShowDialog();
            }
            else
            {
                if (rbTracto.Checked == true)
                {
                    dtgvKilometrajes.ShowPrintPreview();
                }

                if (rbCarreta.Checked == true)
                {
                    dtgvDespachos.ShowPrintPreview();
                }
            }
        }

        private void btnKilometraje_Click(object sender, EventArgs e)
        {
            frmRegistroKMUnidades frmRegistroKMUnidades = new frmRegistroKMUnidades();
            frmRegistroKMUnidades.ShowDialog();
        }
    }
}

