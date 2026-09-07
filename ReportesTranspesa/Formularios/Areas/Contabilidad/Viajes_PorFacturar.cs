using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
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

    public partial class Viajes_PorFacturar : MetroFramework.Forms.MetroForm
    {

        public Viajes_PorFacturar()
        {
            InitializeComponent();
            
        }

        int cliente = -1;
        DateTime FechaGuia;
        string Viaje;

       

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            if (rbResumen.Checked)
            {
                splitContainer2.Panel2Collapsed = false;
                splitContainer2.Panel1Collapsed = true;
                BuscarResumen();
            }else
            {
                splitContainer2.Panel2Collapsed = true;
                splitContainer2.Panel1Collapsed = false;
                BuscarDetallado();
            }
            
           
        }
        void BuscarResumen()
        {
            dgvResumen.DataSource = null;
             System.Data.DataTable dt = new System.Data.DataTable();
             dt = clsContabilidadBL.Instancia.GetViajesPorFacturar_Resumen(dtpPeriodo.Value.ToShortDateString() + " 00:00:00");
             if (dt.Rows.Count > 0)
             {
                 dgvResumen.DataSource = dt;
                 dgvResumen.Rows[0].DefaultCellStyle.BackColor = Color.GreenYellow;
                 dgvResumen.AutoResizeColumns();
                 dgvResumen.Columns["CLIENTE"].Frozen = true;

             }
             else
             {
                 Mensaje m = new Mensaje();
                 m.mensaje = "No hay data para mostrar";
                 m.ShowDialog();
             }

        }

        public bool isDecimal(String num)
        {
            try
            {
                decimal.Parse(num);
                return true;
            }
            catch
            {
                return false;
            }
        }

        void BuscarDetallado()
        {
            if (txtCliente.Text.Trim() == "")
            {
                cliente = -1;
                if (chkOtros.Checked == true) { cliente = -2; }
            }
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt0 = new System.Data.DataTable();
            //dt0 = clsContabilidadBL.Instancia.GetDataSumarizadoImporte(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            //dtpFechaFin.Value.ToShortDateString() + " 23:59:59");
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsContabilidadBL.Instancia.GetViajesPorFacturar(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", cliente);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;

                DataTable dt2 = new DataTable();
                dt2.Clear();
                dt2.Columns.Add("Situacion");
                //dt2.Columns.Add("Importe");
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
                dtgvDataView.Columns["SITUACION"].ColumnEdit = riLookup;
                //dtgvDataView.Columns["IMPORTE"].Width = 120;
                //dtgvDataView.Columns["MONTO"].Visible = true;
                //dtgvDataView.Columns["IMPORTE"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDataView.Columns["IMPORTE"].DisplayFormat.FormatString = "c2";
                //dtgvDataView.Columns["IMPORTE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTO", "Total={0:c2}");
                dtgvDataView.BestFitColumns();
                dtgvDataView.Columns["GT"].Width = 180;
                dtgvDataView.Columns["GR"].Width = 180;
                // dtgvDataView.Columns["ESTADO"].Width = 120;
                dtgvDataView.Columns["SITUACION"].Width = 120;
                //dtgvDataView.Columns["DETALLE"].Width = 300;
                dtgvDataView.Columns["OBSERVACIONES"].Width = 300;
                //GridColumnSummaryItem item = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "IMPORTE", "Importe:={0:n2}");
                //dtgvDataView.Columns["IMPORTE"].Summary.Add(item);



            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void Viajes_PorFacturar_Load(object sender, EventArgs e)
        {
            //dtgvDataView.OptionsBehavior.Editable = true;
            //btnGuardar.Visible = true;
            btnResumen.Visible = false;
            splitContainer2.Panel2Collapsed = true;
            //ESTE CODIGO ES PARA RESTRINGUIR A LOS USUARIOS CON OPCION A MODIFICAR.
            if (Utilitario.Instancia.SesionUsuario.usuario == "DLIZA" | Utilitario.Instancia.SesionUsuario.usuario == "MALCANTARA" || Utilitario.Instancia.SesionUsuario.usuario == "MALCANTAR" || Utilitario.Instancia.SesionUsuario.usuario == "JMARQUINA" || Utilitario.Instancia.SesionUsuario.usuario == "FRUIZ" || Utilitario.Instancia.SesionUsuario.usuario == "FARENAS" || Utilitario.Instancia.SesionUsuario.usuario == "EFERNANDEZ" || Utilitario.Instancia.SesionUsuario.usuario == "SFERNANDEZ" || Utilitario.Instancia.SesionUsuario.usuario == "MLOPEZ")
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
            if (!lvCliente.Items.Count.Equals(0))
            {
                lvCliente.Items[0].Selected = true;
            }
        }

        private void lvCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCliente.SelectedItems[0];

                cliente = Int32.Parse(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void lvCliente_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCliente.SelectedItems[0];

                cliente = Int32.Parse(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource != null)
            {
                for (int i = 0; i < dtgvDataView.DataRowCount; i++)
                {
                    bool resultado;
                    resultado = clsContabilidadBL.Instancia.UpdateViajePF(
                    dtgvDataView.GetRowCellValue(i, "CODIGO VIAJE").ToString(),
                    dtgvDataView.GetRowCellValue(i, "SITUACION").ToString(),
                    dtgvDataView.GetRowCellValue(i, "OBSERVACIONES").ToString()
                    //Convert.ToDateTime(dtgvDataView.GetRowCellValue(i, "FECHA ENVIO GUIAS").ToString(), CultureInfo.InvariantCulture)
                    //dtgvDataView.GetRowCellValue(i, "FECHA ENVIO GUIAS").ToString()
                    //dtgvDataView.GetRowCellValue(i, "DETALLE").ToString()
                    );
                    if (resultado != true)
                    {
                        Mensaje m = new Mensaje();
                        m.Width = 500;
                        m.mensaje = "No existe el viaje " + dtgvDataView.GetRowCellValue(i, "CODIGO VIAJE").ToString();
                        m.ShowDialog();
                        break;
                    }
                }
                Mensaje m2 = new Mensaje();
                m2.mensaje = "Se actualizaron los viajes";
                m2.ShowDialog();
                GuardaFechaEnvioGuia();
            }
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
            if(rbResumen.Checked)
            {
                ExportarResumen(dgvResumen);
            }
        }
        void ExportarDetallado()
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
        void ExportarResumen(DataGridView datalistado)
        {
            if (dgvResumen.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                try
                {
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application(); // Instancia a la libreria de Microsoft Office
                    excel.Application.Workbooks.Add(true); //Con esto añadimos una hoja en el Excel para exportar los archivos
                    int IndiceColumna = 0;
                    foreach (DataGridViewColumn columna in datalistado.Columns) //Aquí empezamos a leer las columnas del listado a exportar
                    {
                        IndiceColumna++;
                        excel.Cells[1, IndiceColumna] = columna.Name;
                    }
                    int IndiceFila = 0;
                    foreach (DataGridViewRow fila in datalistado.Rows) //Aquí leemos las filas de las columnas leídas
                    {
                        IndiceFila++;
                        IndiceColumna = 0;
                        foreach (DataGridViewColumn columna in datalistado.Columns)
                        {
                            IndiceColumna++;
                            //excel.Cells[IndiceFila + 1, IndiceColumna] = fila.Cells[columna.Name].Value; // Con el +1 sale cabecera
                            excel.Cells[IndiceFila , IndiceColumna] = fila.Cells[columna.Name].Value; // Sin el +1 No sale cabecera
                        }
                    }
                    excel.Visible = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("No hay Registros a Exportar.");
                }
            }
        }


        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

      
        

        private void btnResumen_Click(object sender, EventArgs e)
        {
            Resumen_Viajes_por_Facturar res = new Resumen_Viajes_por_Facturar();
            res.Show();
        }

        public void GuardaFechaEnvioGuia()
        {
            int renglonesSeleccionados = dtgvDataView.SelectedRowsCount;
            if (renglonesSeleccionados == 0)
            {
                MessageBox.Show("Tienes que seleccionar por lo menos una fila.");
            }
            else
            {
                DataTable tabla = new DataTable();
                tabla.Columns.Add("FECHA ENVIO GUIAS", typeof(DateTime));
                //tabla.Columns.Add("DOCUMENTO", typeof(string));  
                foreach (int indice in dtgvDataView.GetSelectedRows())
                {
                    try
                    {
                        DataRow fila = tabla.NewRow();
                        fila["FECHA ENVIO GUIAS"] = dtgvDataView.GetRowCellValue(indice, "FECHA ENVIO GUIAS").ToString();
                        //fila["DOCUMENTO"] = dtgvDataView.GetRowCellValue(indice, "DOCUMENTO").ToString();
                        //DataRow row = dtgvDataView.GetDataRow(dtgvDataView.GetSelectedRows()[0]);
                        //var documento = row["DOCUMENTO"].ToString();
                        var viaje = dtgvDataView.GetRowCellValue(indice, "CODIGO VIAJE").ToString();
                        Viaje = viaje.ToString();
                        tabla.Rows.Add(fila);

                        if (fila["FECHA ENVIO GUIAS"].ToString() != null)
                        {
                            FechaGuia = Convert.ToDateTime(fila["FECHA ENVIO GUIAS"].ToString());
                            clsContabilidadBL.Instancia.UpdateFechaGuia(FechaGuia, Viaje);
                            Mensaje m = new Mensaje();
                            //m.mensaje = "Se Actualizo Fecha Recepción";
                            //m.mensaje = "Se Actualizo Fecha Envio Guia" + Environment.NewLine + " del Viaje: " + Viaje;
                            //m.ShowDialog();
                        }
                        else
                        {
                            Mensaje m = new Mensaje();
                            m.mensaje = "No Se Actualizo Fecha Recepción";
                            m.ShowDialog();
                        }
                    }
                    catch
                    {
                        Mensaje m = new Mensaje();
                        m.mensaje = "Seleccionar bien el Viaje";
                        m.ShowDialog();
                    }
                }
            }
        }

        private void rbResumen_CheckedChanged(object sender, EventArgs e)
        {
            txtCliente.Visible = false;
            metroLabel1.Visible = false;
            metroLabel10.Visible = false;
            dtpFechaIni.Visible = false;
            dtpFechaFin.Visible = false;
            chkOtros.Visible = false;
            dtpPeriodo.Visible = true;
            metroLabel2.Text = "Periodo (Mes-Año): ";
        }

        private void rbDetalle_CheckedChanged(object sender, EventArgs e)
        {
            txtCliente.Visible = true;
            metroLabel1.Visible = true;
            metroLabel10.Visible = true;
            dtpFechaIni.Visible = true;
            dtpFechaFin.Visible = true;
            chkOtros.Visible = true;
            dtpPeriodo.Visible = false;
            metroLabel2.Text = "Fecha Inicio: ";
        }

        private void dgvResumen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvResumen_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
                   
        }

       
    }
}
