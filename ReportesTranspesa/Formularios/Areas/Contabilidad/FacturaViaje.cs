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
using DevExpress.XtraEditors.Repository;
using Entidades;
using Comun;


namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class FacturarViaje : MetroFramework.Forms.MetroForm
    {
        public FacturarViaje()
        {
            InitializeComponent();
        }

        int cliente = -1;
        private void FacturarViaje_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            //ESTE CODIGO ES PARA RESTRINGUIR A LOS USUARIOS CON OPCION A MODIFICAR.
            if (Utilitario.Instancia.SesionUsuario.usuario == "DLIZA" || Utilitario.Instancia.SesionUsuario.usuario == "ASANCHEZ" || Utilitario.Instancia.SesionUsuario.usuario == "JMARQUINA" || Utilitario.Instancia.SesionUsuario.usuario == "RALAYO")
            {
                dtgvDataView.OptionsBehavior.Editable = true;
                btnGuardar.Visible = true;
            }
            else
            {
                dtgvDataView.OptionsBehavior.Editable = false;
                btnGuardar.Visible = false;
            }
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtCliente.Text.Trim() == "")
            {
                cliente = -1;
                if (chkOtros.Checked == true) { cliente = -2; }
            }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsContabilidadBL.Instancia.GetViajesPorFacturar(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", cliente);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;


                DataTable dt2 = new DataTable();
                dt2.Clear();
                dt2.Columns.Add("Situacion");
                DataRow fila = dt2.NewRow();
                fila["Situacion"] = "PENDIENTE DE O/C";
                dt2.Rows.Add(fila);
                DataRow fila2 = dt2.NewRow();
                fila2["Situacion"] = "CON O/C";
                dt2.Rows.Add(fila2);
                DataRow fila3 = dt2.NewRow();
                fila3["Situacion"] = "OBSERVADO";
                dt2.Rows.Add(fila3);
                DataRow fila4 = dt2.NewRow();
                fila4["Situacion"] = "POR FACTURAR";
                dt2.Rows.Add(fila4);
                DataRow fila5 = dt2.NewRow();
                fila5["Situacion"] = "DOC EXTRAVIADO";
                dt2.Rows.Add(fila5);
                DataRow fila6 = dt2.NewRow();
                fila6["Situacion"] = "NO RECONOCE EL CLIENTE";
                dt2.Rows.Add(fila6);

                // Create an in-place LookupEdit control.
                RepositoryItemLookUpEdit riLookup = new RepositoryItemLookUpEdit();
                riLookup.DataSource = dt2;
                riLookup.ValueMember = "Situacion";
                riLookup.DisplayMember = "Situacion";

                // Enable the "best-fit" functionality mode in which columns have proportional widths and the popup window is resized to fit all the columns.
                riLookup.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
                // Specify the dropdown height.
                riLookup.DropDownRows = dt2.Rows.Count;

                // Enable the automatic completion feature. In this mode, when the dropdown is closed, 
                // the text in the edit box is automatically completed if it matches a DisplayMember field value of one of dropdown rows. 
                riLookup.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                // Specify the column against which an incremental search is performed in SearchMode.AutoComplete and SearchMode.OnlyInPopup modes
                riLookup.AutoSearchColumnIndex = 1;

                // Optionally hide the Description column in the dropdown.
                // riLookup.PopulateColumns();
                // riLookup.Columns["Description"].Visible = false;

                // Assign the in-place LookupEdit control to the grid's CategoryID column.
                // Note that the data types of the "ID" and "CategoryID" fields match.
                gridView1.Columns["SITUACION"].ColumnEdit = riLookup;


                gridView1.BestFitColumns();
                gridView1.Columns["GT"].Width = 180;
                gridView1.Columns["GR"].Width = 180;
                // dtgvDataView.Columns["ESTADO"].Width = 120;
                gridView1.Columns["SITUACION"].Width = 120;
                gridView1.Columns["DETALLE"].Width = 300;
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvCliente, clsConsultaBL.Instancia.GetPersona(txtCliente.Text), true, false, false);

                lvCliente.Columns[0].Width = 0;
                lvCliente.Columns[1].Width = 206;
                lvCliente.Columns[2].Width = 110;

                lvCliente.BringToFront();
                lvCliente.Visible = true;
                lvCliente.Focus();
                splitContainer1.SplitterDistance = lvCliente.Top + lvCliente.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void lvCliente_Enter(object sender, EventArgs e)
        {

        }

        private void lvCliente_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void lvCliente_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            //if (dtgvData.DataSource != null)
            //{
            //    for (int i = 0; i < dtgvDataView.DataRowCount; i++)
            //    {
            //        bool resultado;
            //        resultado = clsContabilidadBL.Instancia.UpdateViajePF(
            //        dtgvDataView.GetRowCellValue(i, "CODIGO VIAJE").ToString(),
            //        dtgvDataView.GetRowCellValue(i, "SITUACION").ToString(),
            //        dtgvDataView.GetRowCellValue(i, "DETALLE").ToString()
            //        );
            //        if (resultado != true)
            //        {
            //            Mensaje m = new Mensaje();
            //            m.Width = 500;
            //            m.mensaje = "No existe el viaje " + dtgvDataView.GetRowCellValue(i, "CODIGO VIAJE").ToString();
            //            m.ShowDialog();
            //            break;
            //        }
            //    }
            //    Mensaje m2 = new Mensaje();
            //    m2.mensaje = "Se actualizaron los viajes";
            //    m2.ShowDialog();
            //}
        }

        private void chkOtros_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOtros.Checked == true)
            {
                txtCliente.Text = "";
                txtCliente.Enabled = false;
                lvCliente.Visible = false;
            }
            else
            {
                txtCliente.Text = "";
                txtCliente.Enabled = true;
                lvCliente.Visible = false;
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
                string nombre = System.IO.Path.Combine(desktop, "Viajes por Facturar del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
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
    }
}
