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
using Comun;
using System.Reflection.Emit;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class AnexarGuiasRetorno : Form
    {
        int estadoViaje = 4;

        public AnexarGuiasRetorno()
        {
            InitializeComponent();
        }

        private void AnexarGuias_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // AUTO COMPLETADO DE ruta
        #region auto compeltado de ruta
        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtRuta, ref lvRuta, clsConsultaBL.Instancia.GetRutas);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
           
        }

        private void lvRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool PresionoEnter = Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtRuta, ref lvRuta, clsConsultaBL.Instancia.GetRutas);
                if (PresionoEnter)
                {
                    DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text == null ? null : txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                    cargarDatosDevExpress(dt);


                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }


        private void lvRuta_Enter(object sender, EventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtRuta, ref  lvRuta, clsConsultaBL.Instancia.GetRutas);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }


        private void lvRuta_KeyUp(object sender, KeyEventArgs e)
        {

            Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtRuta, ref  lvRuta, clsConsultaBL.Instancia.GetRutas);

        }

        private void txtRuta_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtRuta, ref lvRuta, clsConsultaBL.Instancia.GetRutas);
                if (txtRuta.Text.Length == 0)
                {
                    DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                    cargarDatosDevExpress(dt);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

        }

        #endregion

        // AUTO COMPLETADO DE CONDUCTOR
        private void txtNombreConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtNombreConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);


        }
        private void lstConductor_KeyUp(object sender, KeyEventArgs e)
        {
            Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtNombreConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);
        }

        private void txtNombreConductor_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtNombreConductor, ref lstConductor, clsConsultaBL.Instancia.GetConductores);
                if (txtNombreConductor.Text.Length == 0)
                {
                    DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                    cargarDatosDevExpress(dt);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }

        }


        private void lstConductor_Enter(object sender, EventArgs e)
        {
            Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtNombreConductor, ref  lstConductor, clsConsultaBL.Instancia.GetConductores);
        }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                bool PresionoEnter = Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtNombreConductor, ref  lstConductor, clsConsultaBL.Instancia.GetConductores);
                if (PresionoEnter)
                {
                    DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                    cargarDatosDevExpress(dt);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void cargarDatosDevExpress(DataTable dt)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                        dtgViajesData.DataSource = dt;
                        //dgvViajeExpressVista.Columns["idRuta"].Visible = false;
                        dgvViajeExpressVista.Columns["idConductor"].Visible = false;
                        dgvViajeExpressVista.Columns["IdVehiculo"].Visible = false;
                        dgvViajeExpressVista.Columns["idEstado"].Visible = false;
                        dgvViajeExpressVista.Columns["IdViaje"].Visible = false;
                        dgvViajeExpressVista.Columns["IdRuta"].Visible = false;
                        //dgvViajeExpressVista.Columns["idCliente"].Visible = false;
                        //dgvViajeExpressVista.Columns["idGuia"].Visible = false;

                        dgvViajeExpressVista.Columns["FECHA"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        dgvViajeExpressVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";
                        /*GridView gridView = dtgViajesData.FocusedView as GridView;
                        gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                        new GridColumnSortInfo(gridView.Columns["CONDUCTOR"], DevExpress.Data.ColumnSortOrder.Ascending), 
                        }, 1);*/
                        
                        //GridGroupSummaryItem item = new GridGroupSummaryItem();
                        //item.FieldName = "idGuia";
                        //item.SummaryType = DevExpress.Data.SummaryItemType.Count;
                        //gridView.GroupSummary.Add(item);
                        //dgvViajeExpressVista.ExpandAllGroups();
                        dgvViajeExpressVista.BestFitColumns();
                }
                else
                {
                    dtgViajesData.DataSource = null;

                }

            }

        }

        private void cargarDatosGrillaComun(DataTable dt)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    if (dgvViajes.Rows.Count > 0)
                    {
                        dgvViajes.Rows.Clear();
                    }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dgvViajes.Rows.Add(dt.Rows[i]["CODIGOVIAJE"],
                               dt.Rows[i]["FECHA"],
                               dt.Rows[i]["RUTA"],
                               dt.Rows[i]["idRuta"],
                               dt.Rows[i]["KM"],
                               dt.Rows[i]["CONDUCTOR"],
                               dt.Rows[i]["idConductor"],
                               dt.Rows[i]["CLIENTE"],
                               dt.Rows[i]["idCliente"],
                               dt.Rows[i]["GUIA/TRANSP"],
                               dt.Rows[i]["GUIA/REM"],
                               dt.Rows[i]["IdGuia"],
                               //dt.Rows[i]["PESO"],
                               dt.Rows[i]["TRACTO"],
                               dt.Rows[i]["PROYECTO"],
                               dt.Rows[i]["CARRETA"],
                               dt.Rows[i]["ESTADO"]
                               //dt.Rows[i]["FACTURADO"],
                               //dt.Rows[i]["DOCUMENTO"],
                               //dt.Rows[i]["MONTO"],
                               //dt.Rows[i]["P.CLIENTE"],
                               //dt.Rows[i]["SUCURSAL"],
                               //dt.Rows[i]["TIPO"],
                               //dt.Rows[i]["TRANSPORTE"],
                               //dt.Rows[i]["PROVEEDOR"]
                               );

                    }



                }
                else
                {
                    dgvViajes.DataSource = null;
                    
                }

            }
            else
            {
                dgvViajes.DataSource = null;
            }
        }


        private void chkEstadoViaje_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkEstadoViaje.Checked)
                {
                    if (rbtProgramado.Checked) estadoViaje = 2;
                    if (rbtEjecucion.Checked) estadoViaje = 3;
                    if (rbtCompletado.Checked) estadoViaje = 4;
                    grEstadoGrupo.Enabled = true;
                    rbtCompletado.Checked = true;
                    if (chkEstadoViaje.Checked)
                    {
                        DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                        cargarDatosDevExpress(dt);
                    }
                }
                else
                {
                    estadoViaje = 4;
                    grEstadoGrupo.Enabled = false;
                    rbtCompletado.Checked = false;
                    rbtEjecucion.Checked = false;
                    rbtProgramado.Checked = false;
                }
            }
            catch (Exception ex)
            {
                
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);;
            }

        }

        private void dtpFechaInicio_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rbtCompletado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEstadoViaje.Checked)
            {
                DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                cargarDatosDevExpress(dt);
            }

        }

        private void rbtProgramado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEstadoViaje.Checked)
            {
                DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                cargarDatosDevExpress(dt);
            }
        }

        private void rbtEjecucion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEstadoViaje.Checked)
            {
                DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                cargarDatosDevExpress(dt);
            }
        }

        private void txtCodViaje_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                cargarDatosDevExpress(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            
        }

        private void dgvViajeExpressVista_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {

                if (e.KeyChar == (char)Keys.Enter)
                {

                    if (dgvViajeExpressVista.SelectedRowsCount > 0)
                    {


                        RegistrarGuiasViajeRetorno openRegistrarGuiasViaje = new RegistrarGuiasViajeRetorno();
                        openRegistrarGuiasViaje.codigoViajeGuia = dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "CODIGOVIAJE").ToString();
                        openRegistrarGuiasViaje.idviajeGuia = Convert.ToInt32(dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "IdViaje"));
                        openRegistrarGuiasViaje.fecha = dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "FECHA").ToString();
                        openRegistrarGuiasViaje.conductorGuia = dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "CONDUCTOR").ToString();
                        openRegistrarGuiasViaje.idConductorGuia = Convert.ToInt32(dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "idConductor"));
                        openRegistrarGuiasViaje.tracto = dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "TRACTO").ToString();
                        openRegistrarGuiasViaje.idVehiculo = Convert.ToInt32(dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "idVehiculo"));
                        openRegistrarGuiasViaje.idEstado = Convert.ToInt32(dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "idEstado"));
                        openRegistrarGuiasViaje.idRuta = Convert.ToInt32(dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "idRuta"));
                        openRegistrarGuiasViaje.ruta = dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "Ruta").ToString();

                        openRegistrarGuiasViaje.ShowDialog();
                        

                        
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }

        }

        private void dgvViajeExpressVista_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                    if (dgvViajeExpressVista.SelectedRowsCount > 0)
                    {


                        RegistrarGuiasViajeRetorno openRegistrarGuiasViaje = new RegistrarGuiasViajeRetorno();
                        openRegistrarGuiasViaje.codigoViajeGuia = dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "CODIGOVIAJE").ToString();
                        openRegistrarGuiasViaje.idviajeGuia = Convert.ToInt32(dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "IdViaje"));
                        openRegistrarGuiasViaje.fecha = dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "FECHA").ToString();
                        openRegistrarGuiasViaje.conductorGuia = dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "CONDUCTOR").ToString();
                        openRegistrarGuiasViaje.idConductorGuia = Convert.ToInt32(dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "idConductor"));
                        openRegistrarGuiasViaje.tracto = dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "TRACTO").ToString();
                        openRegistrarGuiasViaje.idVehiculo = Convert.ToInt32(dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "idVehiculo"));
                        openRegistrarGuiasViaje.idEstado = Convert.ToInt32(dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "idEstado"));
                        openRegistrarGuiasViaje.idRuta = Convert.ToInt32(dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "idRuta"));
                        openRegistrarGuiasViaje.ruta = dgvViajeExpressVista.GetRowCellValue(dgvViajeExpressVista.FocusedRowHandle, "Ruta").ToString();

                        if (openRegistrarGuiasViaje.ShowDialog() == DialogResult.OK)
                        {

                        }
                    
                    }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }

        private void lstConductor_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListViewItem ItemActual;
                ItemActual = lstConductor.SelectedItems[0];
                txtNombreConductor.Tag = Convert.ToInt32(ItemActual.Text);
                txtNombreConductor.Text = ItemActual.SubItems[1].Text;
                lstConductor.Visible = false;
                DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                cargarDatosDevExpress(dt);
                //PresionoEnter = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lvRuta_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                ListViewItem ItemActual;
                ItemActual = lvRuta.SelectedItems[0];
                txtRuta.Tag = Convert.ToInt32(ItemActual.Text);
                txtRuta.Text = ItemActual.SubItems[1].Text;
                lvRuta.Visible = false;
                DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text == null ? null : txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
                cargarDatosDevExpress(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
               dgv.DataSource = null;
               DataTable dt = clsOperacionesBL.Instancia.Reportesapp_Operaciones_GenerarReporteGuiasRetorno(dtpFechaInicio.Text, dtpFechaFin.Text);
               if (dt.Rows.Count > 0)
               {
                   dgv.DataSource = dt;

                   CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                   DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                   dtfi.TimeSeparator = ".";
                   SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                   saveFileDialog1.Filter = @"Excel (*.xls)|*.xls";
                   saveFileDialog1.Title = "Guardar Excel";
                   saveFileDialog1.ShowDialog();
                   dgvExportar.ExportToXls(saveFileDialog1.FileName);
             
                   //ExportToExcel(dt, "x");
               }
               else
               {
                   dgv.DataSource = null;
               }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable dt = clsContabilidadBL.Instancia.ReportesApp_ListarViajes_AnexarGuias(dtpFechaInicio.Text, dtpFechaFin.Text, txtCodViaje.Text, txtNombreConductor.Tag == null ? null : txtNombreConductor.Tag.ToString(), txtRuta.Tag == null ? null : txtRuta.Tag.ToString(), estadoViaje);
            cargarDatosDevExpress(dt);
        }




         /*   public  void ExportToExcel(this DataTable tbl, string excelFilePath = null)
            {
                try
                {
                    if (tbl == null || tbl.Columns.Count == 0)
                    {
                        throw new Exception("ExportToExcel: Null or empty input table!\n");
                    }

                    // load excel, and create a new workbook
                    var excelApp = new Excel.Application();
                    excelApp.Workbooks.Add();

                    // single worksheet
                    Excel._Worksheet workSheet = excelApp.ActiveSheet;

                    // column headings
                    for (var i = 0; i < tbl.Columns.Count; i++)
                    {
                        workSheet.Cells[1, i + 1] = tbl.Columns[i].ColumnName;
                    }

                    // rows
                    for (var i = 0; i < tbl.Rows.Count; i++)
                    {
                        // to do: format datetime values before printing
                        for (var j = 0; j < tbl.Columns.Count; j++)
                        {
                            workSheet.Cells[i + 2, j + 1] = tbl.Rows[i][j];
                        }
                    }

                    // check file path
                    if (!string.IsNullOrEmpty(excelFilePath))
                    {
                        try
                        {
                            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                            saveFileDialog1.Filter = @"Excel (*.xls)|*.xls";
                            saveFileDialog1.Title = "Guardar Excel";
                            saveFileDialog1.ShowDialog();


                            workSheet.SaveAs(saveFileDialog1.FileName);
                            excelApp.Quit();
                            MessageBox.Show("Excel file saved!");
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("ExportToExcel: Excel file could not be saved! Check filepath.\n"
                                                + ex.Message);
                        }
                    }
                    else
                    { // no file path is given
                        excelApp.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("ExportToExcel: \n" + ex.Message);
                }

            
        }*/

            


        //fin
    }
}
