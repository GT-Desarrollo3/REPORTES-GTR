using System;
using System.Text;
using Negocio;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using ReportesTranspesa.Sistema;
using System.Globalization;
using Word = Microsoft.Office.Interop.Word;
using FMBUtilitario;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using ReportesTranspesa.Formularios.Areas.RecursosHumanos.HojaRecorrido;
using Comun;
using System.IO;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class Ficha_Empleados : MetroFramework.Forms.MetroForm
    {
        private Word.Application objWord;
        private Object oMissing = System.Reflection.Missing.Value;
        
        public Ficha_Empleados()
        {
            InitializeComponent();
        }
        private Memos_Compromisos frmMemos_Compromisos;
        public static String Formulario;        
       
        private void Ficha_Empleados_Load(object sender, EventArgs e)
        {
            string formname = this.GetType().Name;
            Formulario = formname;
            dtgvDataView.OptionsBehavior.Editable = true;
            //if (Utilitario.Instancia.SesionUsuario.usuario == "SRUIZG" || Utilitario.Instancia.SesionUsuario.usuario == "JMARQUINA")
            if (Utilitario.Instancia.SesionUsuario.usuario == "SRUIZG" || Utilitario.Instancia.SesionUsuario.usuario == "FRUIZ2")
            {
                btnContrato.Visible = false;
                btnSCTR.Visible = false;
                btnMemorandum.Visible = false;
                dtgvDataView.OptionsSelection.MultiSelect = false;
            }
          //if (Utilitario.Instancia.SesionUsuario.usuario == "SESCOBEDO" || Utilitario.Instancia.SesionUsuario.usuario == "DLIZA" || Utilitario.Instancia.SesionUsuario.usuario == "JBOBADILLA" || Utilitario.Instancia.SesionUsuario.usuario == "JARTEAGA")
            //{
                //btnMemorandum.Visible = true;
                //btnMemorandum.Enabled = true;
            //}
            //else
            //{
                //btnMemorandum.Visible = false;
                //btnMemorandum.Enabled = false;
            //}
        }

        private void chkCompania_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompania.Checked) 
            {
                cboCompañia.Enabled = true;
                cboCompañia.SelectedIndex = 0;
            }
            else 
            {
                cboCompañia.Enabled = false;
                cboCompañia.SelectedIndex = -1;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            string transpesa = "10000000";
            string bra = "40000000";
            string altra = "50000000";
            string amt = "60000000";
            string aduanas = "70000000";
            string inomac = "90000000";
            char estado = 'A';
            if (chkCompania.Checked == true)
            {
                switch (cboCompañia.SelectedIndex)
                {
                    case 0: transpesa = "10000000";
                        bra = "";
                        altra = "";
                        amt = "";
                        aduanas = "";
                        inomac = "";
                        break;

                    case 1: bra = "40000000";
                        transpesa = "";
                        altra = "";
                        amt = "";
                        aduanas = "";
                        inomac = "";
                        break;

                    case 2: altra = "50000000";
                        transpesa = "";
                        bra = "";
                        amt = "";
                        aduanas = "";
                        break;

                    case 3: amt = "60000000";
                        transpesa = "";
                        bra = "";
                        altra = "";
                        aduanas = "";
                        inomac = "";
                        break;

                    case 4: aduanas = "70000000";
                        transpesa = "";
                        bra = "";
                        altra = "";
                        amt = "";
                        inomac = "";
                        break;

                    case 5: inomac = "90000000";
                        aduanas = "";
                        transpesa = "";
                        bra = "";
                        altra = "";
                        amt = "";
                        break;
                }
                #region compañia
                //if (cboCompañia.SelectedIndex == 0)
                //{
                //    transpesa = "10000000";
                //    bra = "";
                //    altra = "";
                //}
                //else
                //{
                //    if (cboCompañia.SelectedIndex == 1)
                //    {
                //        bra = "40000000";
                //        transpesa = "";
                //        altra = "";
                //    }
                //    else
                //    {
                //        altra = "50000000";
                //        bra = "";
                //        transpesa = "";
                //    }
                //}
                /////////////////////////////////////
                //if (cboCompañia.SelectedIndex == 0)
                //{
                //    transpesa = "10000000";
                //    bra = "";
                //}
                //else
                //{
                //    bra = "40000000";
                //    transpesa = "";
                //}
                #endregion
            }
            if (cboCesados.Checked == true)
            {
                estado = 'I';
            }
            dt = clsRecursosHumanosBL.Instancia.GetDataFichaEmpleados(transpesa, bra, altra, amt, aduanas, inomac, estado);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                ProtegeColumnas();

                if (Utilitario.Instancia.SesionUsuario.usuario == "SRUIZG" || Utilitario.Instancia.SesionUsuario.usuario == "FRUIZ2")
                {
                    dtgvDataView.Columns["DNI"].Visible = false;
                    dtgvDataView.Columns["DIRECCION"].Visible = false;
                    dtgvDataView.Columns["TELEFONOS"].Visible = false;
                    dtgvDataView.Columns["F. NACIMIENTO"].Visible = false;
                    dtgvDataView.Columns["F. INGRESO"].Visible = false;
                    dtgvDataView.Columns["F. CESE"].Visible = false;
                    dtgvDataView.Columns["T.S. AÑOS"].Visible = false;
                    dtgvDataView.Columns["T.S. MESES"].Visible = false;
                    dtgvDataView.Columns["REMUNERACION"].Visible = false;
                    dtgvDataView.Columns["ESTABLE"].Visible = false;
                    dtgvDataView.Columns["INICIO CONTRATO"].Visible = false;
                    dtgvDataView.Columns["FIN CONTRATO"].Visible = false;
                    dtgvDataView.Columns["BANCO"].Visible = false;
                    dtgvDataView.Columns["CUENTA"].Visible = false;
                    dtgvDataView.Columns["PENSION"].Visible = false;
                    dtgvDataView.Columns["SCTR"].Visible = false;
                }

                GridView gridView = dtgvData.FocusedView as GridView;
                gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["TIPOTRABAJADOR"], DevExpress.Data.ColumnSortOrder.Ascending), 
                }, 1);
                dtgvDataView.ExpandAllGroups();
                dtgvDataView.Columns["REMUNERACION"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataView.Columns["REMUNERACION"].DisplayFormat.FormatString = "c0";
                dtgvDataView.Columns["ID"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "ID", "Total ={0}");
                dtgvDataView.BestFitColumns();

                if (rbEmpleados.Checked)
                {
                    dtgvDataView.Columns["TIPOTRABAJADOR"].FilterInfo = new ColumnFilterInfo("[TIPOTRABAJADOR] = 'EMPLEADOS'");
                    OcultaColumnas();
                    return;
                }
                if (rbObreros.Checked)
                {
                    dtgvDataView.Columns["TIPOTRABAJADOR"].FilterInfo = new ColumnFilterInfo("[TIPOTRABAJADOR] = 'OBREROS' AND [CARGO] != 'CONDUCTOR'");
                    OcultaColumnas();
                    return;
                }
                if (rbChoferes.Checked)
                {
                    dtgvDataView.Columns["TIPOTRABAJADOR"].FilterInfo = new ColumnFilterInfo("[TIPOTRABAJADOR] = 'OBREROS' AND [CARGO] = 'CONDUCTOR'");
                    OcultaColumnas();
                    return;
                }

                OcultaColumnas();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void ProtegeColumnas() 
        {
            dtgvDataView.Columns["COMPAÑIA"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["TIPOTRABAJADOR"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["ID"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["NOMBRE"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["DNI"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["DIRECCION"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["TELEFONOS"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["F. NACIMIENTO"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["F. INGRESO"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["F. CESE"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["T.S. AÑOS"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["T.S. MESES"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["CARGO"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["AREA"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["REMUNERACION"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["ASIG_FAMILIAR"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["TIPO_CONTRATO"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["ESTABLE"].OptionsColumn.ReadOnly = true;
            //dtgvDataView.Columns["INICIO CONTRATO"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["INICIO_CONTRATO_LETRAS"].OptionsColumn.ReadOnly = true;
            //dtgvDataView.Columns["FIN CONTRATO"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["FIN_CONTRATO_LETRAS"].OptionsColumn.ReadOnly = true;
            //dtgvDataView.Columns["INICIO_PRUEBA_CONTRATO"].OptionsColumn.ReadOnly = true;
            //dtgvDataView.Columns["FIN_PRUEBA_CONTRATO"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["TIEMPO_DIAS"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["TIEMPO_MESES"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["DIA_SUSC"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["MES_SUSC"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["AÑO_SUSC"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["BANCO"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["CUENTA"].OptionsColumn.ReadOnly = true;
            dtgvDataView.Columns["SCTR"].OptionsColumn.ReadOnly = false;
        }
        
        private void rbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (dtgvData.DataSource != null) 
            {
                dtgvDataView.Columns["TIPOTRABAJADOR"].ClearFilter();
            }
        }

        private void rbEmpleados_CheckedChanged(object sender, EventArgs e)
        {
            if (dtgvData.DataSource != null)
            {
                dtgvDataView.Columns["TIPOTRABAJADOR"].FilterInfo = new ColumnFilterInfo("[TIPOTRABAJADOR] = 'EMPLEADOS'");
            }
        }

        private void rbObreros_CheckedChanged(object sender, EventArgs e)
        {
            if (dtgvData.DataSource != null)
            {
                dtgvDataView.Columns["TIPOTRABAJADOR"].FilterInfo = new ColumnFilterInfo("[TIPOTRABAJADOR] = 'OBREROS' AND [CARGO] != 'CONDUCTOR'");
            }
        }

        private void rbChoferes_CheckedChanged(object sender, EventArgs e)
        {
            if (dtgvData.DataSource != null)
            {
                dtgvDataView.Columns["TIPOTRABAJADOR"].FilterInfo = new ColumnFilterInfo("[TIPOTRABAJADOR] = 'OBREROS' AND [CARGO] = 'CONDUCTOR'");
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
                string tipo = "";
                if (rbTodos.Checked)
                {
                    tipo = "Todos";
                }
                else 
                { 
                    if (rbEmpleados.Checked) 
                    {
                        tipo = "Empleados";
                    }
                    else 
                    {
                        if (rbObreros.Checked) 
                        {
                            tipo = "Obreros";
                        }
                        else
                        {
                            tipo = "Choferes";
                        }
                    }
                }
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Listado general de trabajadores ("+ tipo + ") "+ Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));
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

        public string enletras(string num)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }

            entero = Convert.ToInt64(Math.Truncate(nro));
            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                dec = " con " + decimales.ToString() + "/100";
            }

            res = toText(Convert.ToDouble(entero)) + dec;
            return res;
        }

        private string toText(double value)
        {
            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "Cero";
            else if (value == 1) Num2Text = "Uno";
            else if (value == 2) Num2Text = "Dos";
            else if (value == 3) Num2Text = "Tres";
            else if (value == 4) Num2Text = "Cuatro";
            else if (value == 5) Num2Text = "Cinco";
            else if (value == 6) Num2Text = "Seis";
            else if (value == 7) Num2Text = "Siete";
            else if (value == 8) Num2Text = "Ocho";
            else if (value == 9) Num2Text = "Nueve";
            else if (value == 10) Num2Text = "Diez";
            else if (value == 11) Num2Text = "Once";
            else if (value == 12) Num2Text = "Doce";
            else if (value == 13) Num2Text = "Trece";
            else if (value == 14) Num2Text = "Catorce";
            else if (value == 15) Num2Text = "Quince";
            else if (value < 20) Num2Text = "Dieci" + toText(value - 10);
            else if (value == 20) Num2Text = "Viente";
            else if (value < 30) Num2Text = "Veinti" + toText(value - 20);
            else if (value == 30) Num2Text = "Treinta";
            else if (value == 40) Num2Text = "Cuarenta";
            else if (value == 50) Num2Text = "Cincuenta";
            else if (value == 60) Num2Text = "Sesenta";
            else if (value == 70) Num2Text = "Setenta";
            else if (value == 80) Num2Text = "Ochenta";
            else if (value == 90) Num2Text = "Noventa";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " y " + toText(value % 10);
            else if (value == 100) Num2Text = "Cien";
            else if (value < 200) Num2Text = "Ciento " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "Cientos";
            else if (value == 500) Num2Text = "Quinientos";
            else if (value == 700) Num2Text = "Setecientos";
            else if (value == 900) Num2Text = "Novecientos";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "Mil";
            else if (value < 2000) Num2Text = "Mil " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " Mil";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "Un Millon";
            else if (value < 2000000) Num2Text = "Un Millon " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " Millones ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "Un Billon";
            else if (value < 2000000000000) Num2Text = "Un Billon " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " Billones";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;

        }

        private void OcultaColumnas() 
        {
            if (dtgvData.DataSource != null)
            {
                dtgvDataView.Columns["COMPAÑIA"].Visible = false;
                //dtgvDataView.Columns["SEXO"].Visible = false;
                dtgvDataView.Columns["ASIG_FAMILIAR"].Visible = false;
                dtgvDataView.Columns["TIPO_CONTRATO"].Visible = false;
                dtgvDataView.Columns["INICIO_CONTRATO_LETRAS"].Visible = false;
                dtgvDataView.Columns["FIN_CONTRATO_LETRAS"].Visible = false;
                //dtgvDataView.Columns["INICIO_PRUEBA_CONTRATO"].Visible = false;
                //dtgvDataView.Columns["FIN_PRUEBA_CONTRATO"].Visible = false;
                dtgvDataView.Columns["TIEMPO_DIAS"].Visible = false;
                dtgvDataView.Columns["TIEMPO_MESES"].Visible = false;
                dtgvDataView.Columns["DIA_SUSC"].Visible = false;
                dtgvDataView.Columns["MES_SUSC"].Visible = false;
                dtgvDataView.Columns["AÑO_SUSC"].Visible = false;
                //dtgvDataView.Columns["INICIO CONTRATO"].Visible = false;
                //dtgvDataView.Columns["FIN CONTRATO"].Visible = true;
            }
        }

        private bool ValidaEstables(int [] filas)
        {
            for (int i = 0; i < filas.Length; i++)
            {
                if (Convert.ToString(dtgvDataView.GetRowCellValue(filas[i], "TIPO_CONTRATO")) == "ES")
                {
                    Mensaje m = new Mensaje();
                    m.Width = 600;
                    m.mensaje = "No se generarán contratos, existen empleados estables seleccionados.";
                    m.ShowDialog();
                    return false;
                }
            }
            return true;
        }

        static bool noesCabecera(int n)
        {
            return n < 0;
        }

        private void btnContrato_Click(object sender, EventArgs e)
        {
            int[] filas = dtgvDataView.GetSelectedRows();
            if (filas.Length != 0)
            {
                List<string> listacontratos = new List<string>();

                if (ValidaEstables(filas) == true)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        //if (dtgvDataView.GetRowCellValue(filas[i], "ASIG_FAMILIAR").ToString().Trim() != "" && dtgvDataView.GetRowCellValue(filas[i], "INICIO_PRUEBA_CONTRATO").ToString().Trim() != "" && dtgvDataView.GetRowCellValue(filas[i], "FIN_PRUEBA_CONTRATO").ToString().Trim() != "")
                        if (dtgvDataView.GetRowCellValue(filas[i], "ASIG_FAMILIAR").ToString().Trim() != "")
                        {
                            listacontratos.Add(generaPDFContrato("D:\\Contratos\\" + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString() + ".pdf",
                            dtgvDataView.GetRowCellValue(filas[i], "COMPAÑIA").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString(), dtgvDataView.GetRowCellValue(filas[i], "DNI").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "CARGO").ToString(), dtgvDataView.GetRowCellValue(filas[i], "DIRECCION").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "TIPO_CONTRATO").ToString(), dtgvDataView.GetRowCellValue(filas[i], "INICIO_CONTRATO_LETRAS").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "FIN_CONTRATO_LETRAS").ToString(), dtgvDataView.GetRowCellValue(filas[i], "TIEMPO_DIAS").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "TIEMPO_MESES").ToString(), dtgvDataView.GetRowCellValue(filas[i], "DIA_SUSC").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "MES_SUSC").ToString(), dtgvDataView.GetRowCellValue(filas[i], "AÑO_SUSC").ToString(),
                            (Convert.ToDouble(dtgvDataView.GetRowCellValue(filas[i], "REMUNERACION")) - 93).ToString(),
                            enletras((Convert.ToDouble(dtgvDataView.GetRowCellValue(filas[i], "REMUNERACION")) - 93).ToString()),
                            dtgvDataView.GetRowCellValue(filas[i], "ASIG_FAMILIAR").ToString()));
                            //dtgvDataView.GetRowCellValue(filas[i], "ASIG_FAMILIAR").ToString(),dtgvDataView.GetRowCellValue(filas[i], "INICIO_PRUEBA_CONTRATO").ToString(),
                            //dtgvDataView.GetRowCellValue(filas[i], "FIN_PRUEBA_CONTRATO").ToString()));
                        }
                        else
                        {
                            listacontratos.Add(generaPDFContrato("D:\\Contratos\\" + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString() + ".pdf",
                            dtgvDataView.GetRowCellValue(filas[i], "COMPAÑIA").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString(), dtgvDataView.GetRowCellValue(filas[i], "DNI").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "CARGO").ToString(), dtgvDataView.GetRowCellValue(filas[i], "DIRECCION").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "TIPO_CONTRATO").ToString(), dtgvDataView.GetRowCellValue(filas[i], "INICIO_CONTRATO_LETRAS").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "FIN_CONTRATO_LETRAS").ToString(), dtgvDataView.GetRowCellValue(filas[i], "TIEMPO_DIAS").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "TIEMPO_MESES").ToString(), dtgvDataView.GetRowCellValue(filas[i], "DIA_SUSC").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "MES_SUSC").ToString(), dtgvDataView.GetRowCellValue(filas[i], "AÑO_SUSC").ToString(),
                            dtgvDataView.GetRowCellValue(filas[i], "REMUNERACION").ToString(), enletras(dtgvDataView.GetRowCellValue(filas[i], "REMUNERACION").ToString()),
                            dtgvDataView.GetRowCellValue(filas[i], "ASIG_FAMILIAR").ToString()));
                            //dtgvDataView.GetRowCellValue(filas[i], "ASIG_FAMILIAR").ToString(),dtgvDataView.GetRowCellValue(filas[i], "INICIO_PRUEBA_CONTRATO").ToString(),
                            //dtgvDataView.GetRowCellValue(filas[i], "FIN_PRUEBA_CONTRATO").ToString()));
                        }
                    }

                    DialogResult dialogResult = MessageBox.Show("Se generaron " + filas.Length + " contratos satisfactoriamente. ¿Desea imprimirlos?", "Confirmación", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        for (int i = 0; i < listacontratos.Count; i++)
                        {
                            ImprimirPDF(listacontratos[i]);
                        }
                    }
                    dtgvDataView.ClearSelection();
                }
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay empleados seleccionados";
                m.Width = 300;
                m.ShowDialog();
            }
        }

        private string generaPDFContrato(string filename,string compañia,string nombre,string dni,string cargo,string direccion,string tipocontrato,string fini,string ffin,
            string tdias,string tmeses,string diasusc,string messusc,string añosusc,string sueldo,string sueldoletras,string asigfam)
            //string tdias,string tmeses,string diasusc,string messusc,string añosusc,string sueldo,string sueldoletras,string asigfam,string iniprueb,string finprueb)
        {
            try
            {
                foreach (Process proceso in Process.GetProcesses())
                    if (proceso.ProcessName.ToLower().CompareTo("winword") == 0)
                        proceso.Kill();

                string plantilla;
                string Reporte = "C:\\Transpesa\\Plantillas\\reporte.docx";

                //Seleccion de la plantilla
                if (tipocontrato.Trim() == "N") //nuevo
                {
                    if (cargo == "VIGILANTE" || cargo == "SUPERVISOR DE SEGURIDAD")
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\vigi_nuevo.docx";
                    }
                    else
                    {
                        if (cargo == "CONDUCTOR" && compañia == "10000000")
                        {
                            //plantilla = "C:\\Transpesa\\Plantillas\\nuevo_conductor.docx";
                            plantilla = "C:\\Transpesa\\Plantillas\\opera_nuevo.docx";
                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_nuevo_2018.docx";
                        }
                        else
                        {
                            if (cargo == "ASISTENTE ADMINISTRATIVO" && compañia == "10000000")
                            {
                                plantilla = "C:\\Transpesa\\Plantillas\\adm_recepcion.docx";
                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                            }
                            else
                            {
                                if (cargo == "ASISTENTE ADMINISTRATIVO" && compañia == "10000000")
                                {
                                    plantilla = "C:\\Transpesa\\Plantillas\\adm_opera.docx";
                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                }
                                else
                                {
                                    if (cargo == "ASISTENTE DE ALMACEN" && compañia == "10000000")
                                    {
                                        plantilla = "C:\\Transpesa\\Plantillas\\adm_almacen.docx";
                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                    }
                                    else 
                                    {
                                        if (cargo == "ASISTENTE DE CONTABILIDAD" && compañia == "10000000")
                                        {
                                            plantilla = "C:\\Transpesa\\Plantillas\\adm_contabilidad.docx";
                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                        }
                                        else
                                        {
                                            if (cargo == "ASISTENTE DE CONTROL DE FLOTA" && compañia == "10000000")
                                            {
                                                plantilla = "C:\\Transpesa\\Plantillas\\adm_flota.docx";
                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                            }
                                            else
                                            {
                                                if (cargo == "ASISTENTE DE CONTROL GPS" && compañia == "10000000")
                                                {
                                                    plantilla = "C:\\Transpesa\\Plantillas\\adm_gps.docx";
                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                }
                                                else
                                                {
                                                    if (cargo == "ASISTENTE DE FINANZAS" && compañia == "10000000")
                                                    {
                                                        plantilla = "C:\\Transpesa\\Plantillas\\adm_finanzas.docx";
                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                    }
                                                    else
                                                    {
                                                        if (cargo == "ASISTENTE DE GESTIÓN DEL TALENTO HUMANO" && compañia == "10000000")
                                                        {
                                                            plantilla = "C:\\Transpesa\\Plantillas\\adm_rrhh.docx";
                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                        }   
                                                        else
                                                        {
                                                            if (cargo == "ASISTENTE DE MANTENIMIENTO" && compañia == "10000000")
                                                            {
                                                                plantilla = "C:\\Transpesa\\Plantillas\\adm_mantenimiento.docx";
                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                            }
                                                            else
                                                            {
                                                                if (cargo == "ASISTENTE DE OPERACIONES" && compañia == "10000000")
                                                                {
                                                                    plantilla = "C:\\Transpesa\\Plantillas\\adm_operaciones.docx";
                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                }
                                                                else
                                                                {
                                                                    if (cargo == "ASISTENTE DE SISTEMAS INTEGRADOS" && compañia == "10000000")
                                                                    {
                                                                        plantilla = "C:\\Transpesa\\Plantillas\\adm_seguridad.docx";
                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                    }
                                                                    else
                                                                    {
                                                                        if (cargo == "ASISTENTE DE ALMACEN" && compañia == "10000000")
                                                                        {
                                                                            plantilla = "C:\\Transpesa\\Plantillas\\adm_almacen.docx";
                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                        }
                                                                        else
                                                                        {
                                                                            if (cargo == "ABASTECEDOR DE COMBUSTIBLE" && compañia == "10000000")
                                                                            {
                                                                                plantilla = "C:\\Transpesa\\Plantillas\\aba_comb_nuevo.docx";
                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                            }

                                                                            else
                                                                            {
                                                                                if (cargo == "AYUDANTE DE PATIO" && compañia == "10000000")
                                                                                {
                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\ayud_patio.docx";
                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (cargo == "AUXILIAR DE LIMPIEZA" && compañia == "10000000")
                                                                                    {
                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\aux_limpieza.docx";
                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (cargo == "COORDINADOR DE OPERACIONES" && compañia == "10000000")
                                                                                        {
                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\coordinador_opera.docx";
                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (cargo == "ELECTRICISTA" && compañia == "10000000") 
                                                                                            {
                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\electricista.docx";
                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (cargo == "JEFE DE SEGURIDAD Y SISTEMAS INTEGRADOS DE GESTION" && compañia == "10000000")
                                                                                                {
                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\jefe_seguridad.docx";
                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (cargo == "MECANICO" && compañia == "10000000")
                                                                                                    {
                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\mecanico.docx";
                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (cargo == "PINTOR" && compañia == "10000000")
                                                                                                        {
                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\pintor.docx";
                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (cargo == "SUPERVISOR DE GESTION DEL TALENTO HUMANO" && compañia == "10000000")
                                                                                                            {
                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\supervisor_rrhh.docx";
                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (cargo == "SUPERVISOR DE LOGISTICA" && compañia == "10000000")
                                                                                                                {
                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\supervisor_logistica.docx";
                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (cargo == "SUPERVISOR DE MANTENIMIENTO" && compañia == "10000000")
                                                                                                                    {
                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\supervisor_mantenimiento.docx";
                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (cargo == "SUPERVISOR DE COMBUSTIBLE" && compañia == "10000000")
                                                                                                                        {
                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\supervisor_combustible.docx";
                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (cargo == "ASISTENTA SOCIAL" && compañia == "10000000")
                                                                                                                            {
                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\asistenta_social.docx";
                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (cargo == "ASISTENTE DE AUDITORIA" && compañia == "10000000")
                                                                                                                                {
                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\asistente_auditoria.docx";
                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (cargo == "ASISTENTE DE COMBUSTIBLE" && compañia == "10000000")
                                                                                                                                    {
                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\asistente_combustible.docx";
                                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if (cargo == "ASISTENTE DE CONTROL DOCUMENTARIO" && compañia == "10000000")
                                                                                                                                        {
                                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\asistente_control_documentario.docx";
                                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if (cargo == "ASISTENTE DE LOGISTICA" && compañia == "10000000")
                                                                                                                                            {
                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\asistente_logistica.docx";
                                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                if (cargo == "ASISTENTE DE PATIO" && compañia == "10000000")
                                                                                                                                                {
                                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\asistente_patio.docx";
                                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {

                                                                                                                                                    if (cargo == "ASISTENTE DE TECNOLOGÍA DE LA INFORMACIÓN" && compañia == "10000000")
                                                                                                                                                    {
                                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\asistente_ti.docx";
                                                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if (cargo == "AUXILIAR DE CONTABILIDAD" && compañia == "10000000")
                                                                                                                                                        {
                                                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\aux_contabilidad.docx";
                                                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            if (cargo == "AUXILIAR DE OFICINA" && compañia == "10000000")
                                                                                                                                                            {
                                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\aux_oficina.docx";
                                                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                if (cargo == "AUXILIAR DE OFICINA" && compañia == "10000000")
                                                                                                                                                                {
                                                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\aux_oficina.docx";
                                                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    if (cargo == "AUXILIAR DE OPERACIONES" && compañia == "10000000")
                                                                                                                                                                    {
                                                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\aux_operaciones.docx";
                                                                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        if (cargo == "AYUDANTE DE ALMACEN" && compañia == "10000000")
                                                                                                                                                                        {
                                                                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\ayudante_almacen.docx";
                                                                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            if (cargo == "AYUDANTE MECANICO" && compañia == "10000000")
                                                                                                                                                                            {
                                                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\ayudante_mecanico.docx";
                                                                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                if (cargo == "CHOFER OPERATIVO" && compañia == "10000000")
                                                                                                                                                                                {
                                                                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\chofer_operativo.docx";
                                                                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    if (cargo == "CONSERJE" && compañia == "10000000")
                                                                                                                                                                                    {
                                                                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\conserje.docx";
                                                                                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        if (cargo == "ASISTENTE DE CONTROL DE INVENTARIO" && compañia == "10000000")
                                                                                                                                                                                        {
                                                                                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\asistente_inventario.docx";
                                                                                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            if (cargo == "CONTADOR GENERAL" && compañia == "10000000")
                                                                                                                                                                                            {
                                                                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\contador_general.docx";
                                                                                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                if (cargo == "JEFE DE FINANZAS" && compañia == "10000000")
                                                                                                                                                                                                {
                                                                                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\jefe_finanzas.docx";
                                                                                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (cargo == "TORNERO" && compañia == "10000000")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\tornero.docx";
                                                                                                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (cargo == "OPERADOR DE MAQUINARIA PESADA" && compañia == "10000000")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\oper_maquina.docx";
                                                                                                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (cargo == "SOLDADOR" && compañia == "10000000")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\soldador.docx";
                                                                                                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                if (cargo == "COORDINADOR DE OPERACIONES LIMA" && compañia == "10000000")
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\coordinador_opera_lima.docx";
                                                                                                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                                                }
                                                                                                                                                                                                                else
                                                                                                                                                                                                                {
                                                                                                                                                                                                                    if (compañia == "10000000")
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\adm_nuevo.docx";
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                    else
                                                                                                                                                                                                                    {
                                                                                                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\bra_renov.docx";
                                                                                                                                                                                                                    }
                                                                                                                                                                                                                }
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                    }
                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                    
                                                                                                                }
                                                                                                                
                                                                                                            }
                                                                                                            
                                                                                                        }
                                                                                                        
                                                                                                    }
                                                                                                    
                                                                                                }

                                                                                            }
                                                                                            
                                                                                        }
                                                                                        
                                                                                    }

                                                                                }

                                                                            }   

                                                                        }
  
                                                                    }
                                                                    
                                                                }
                                                                
                                                            }
                                                            
                                                        }
                                                        
                                                    }
                                                    
                                                }
                                                
                                            }
                                            
                                        }
                                        
                                    }
                                    
                                }
                                                                
                            }

                        }
                    }
                }
                else //renovacion////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                {
                    if (cargo == "VIGILANTE" || cargo == "SUPERVISOR DE SEGURIDAD")
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\vigi_renov.docx";
                    }
                    else
                    {
                        if (cargo == "CONDUCTOR" && compañia == "10000000")
                        {
                            //plantilla = "C:\\Transpesa\\Plantillas\\renov_condutor.docx";
                            plantilla = "C:\\Transpesa\\Plantillas\\opera_renov.docx";
                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                        }
                        else
                        {
                            if (cargo == "ASISTENTE ADMINISTRATIVO" && compañia == "10000000")
                            {
                                plantilla = "C:\\Transpesa\\Plantillas\\adm_recepcion.docx";
                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018adm_opera.docx";
                            }
                            else
                            {
                                if (cargo == "ASISTENTE ADMINISTRATIVO" && compañia == "10000000")
                                {
                                    plantilla = "C:\\Transpesa\\Plantillas\\adm_opera.docx";
                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                }
                                else 
                                {
                                    if (cargo == "ASISTENTE DE CONTABILIDAD" && compañia == "10000000")
                                    {
                                        plantilla = "C:\\Transpesa\\Plantillas\\adm_contabilidad.docx";
                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                    }
                                    else
                                    {
                                        if (cargo == "ASISTENTE DE CONTROL DE FLOTA" && compañia == "10000000")
                                        {
                                            plantilla = "C:\\Transpesa\\Plantillas\\adm_flota.docx";
                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                        }
                                        else
                                        {
                                            if (cargo == "ASISTENTE DE CONTROL GPS" && compañia == "10000000")
                                            {
                                                plantilla = "C:\\Transpesa\\Plantillas\\adm_gps.docx";
                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                            }
                                            else
                                            {
                                                if (cargo == "ASISTENTE DE FINANZAS" && compañia == "10000000")
                                                {
                                                    plantilla = "C:\\Transpesa\\Plantillas\\adm_finanzas.docx";
                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                }
                                                else
                                                {
                                                    if (cargo == "ASISTENTE DE GESTIÓN DEL TALENTO HUMANO" && compañia == "10000000")
                                                    {
                                                        plantilla = "C:\\Transpesa\\Plantillas\\adm_rrhh.docx";
                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                    }
                                                    else
                                                    {
                                                        if (cargo == "ASISTENTE DE MANTENIMIENTO" && compañia == "10000000")
                                                        {
                                                            plantilla = "C:\\Transpesa\\Plantillas\\adm_mantenimiento.docx";
                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                        }
                                                        else
                                                        {
                                                            if (cargo == "ASISTENTE DE OPERACIONES" && compañia == "10000000")
                                                            {
                                                                plantilla = "C:\\Transpesa\\Plantillas\\adm_operaciones.docx";
                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                            }
                                                            else
                                                            {
                                                                if (cargo == "ASISTENTE DE SISTEMAS INTEGRADOS" && compañia == "10000000")
                                                                {
                                                                    plantilla = "C:\\Transpesa\\Plantillas\\adm_seguridad.docx";
                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                }
                                                                else
                                                                {
                                                                    if (cargo == "ASISTENTE DE ALMACEN" && compañia == "10000000")
                                                                    {
                                                                        plantilla = "C:\\Transpesa\\Plantillas\\adm_almacen.docx";
                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                    }
                                                                    else
                                                                    {
                                                                        if (cargo == "AYUDANTE DE PATIO" && compañia == "10000000")
                                                                        {
                                                                            plantilla = "C:\\Transpesa\\Plantillas\\ayud_patio.docx";
                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                        }
                                                                        else
                                                                        {
                                                                            if (cargo == "ABASTECEDOR DE COMBUSTIBLE" && compañia == "10000000")
                                                                            {
                                                                                plantilla = "C:\\Transpesa\\Plantillas\\aba_comb_nuevo.docx";
                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                            }
                                                                            else
                                                                            {
                                                                                if (cargo == "AUXILIAR DE LIMPIEZA" && compañia == "10000000")
                                                                                {
                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\aux_limpieza.docx";
                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (cargo == "COORDINADOR DE OPERACIONES" && compañia == "10000000")
                                                                                    {
                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\coordinador_opera.docx";
                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        if (cargo == "ELECTRICISTA" && compañia == "10000000")
                                                                                        {
                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\electricista.docx";
                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                        }
                                                                                        else
                                                                                        {
                                                                                            if (cargo == "JEFE DE SEGURIDAD Y SISTEMAS INTEGRADOS DE GESTION" && compañia == "10000000")
                                                                                            {
                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\jefe_seguridad.docx";
                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                            }
                                                                                            else
                                                                                            {
                                                                                                if (cargo == "MECANICO" && compañia == "10000000")
                                                                                                {
                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\mecanico.docx";
                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    if (cargo == "PINTOR" && compañia == "10000000")
                                                                                                    {
                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\pintor.docx";
                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        if (cargo == "SUPERVISOR DE GESTION DEL TALENTO HUMANO" && compañia == "10000000")
                                                                                                        {
                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\supervisor_rrhh.docx";
                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                        }
                                                                                                        else
                                                                                                        {
                                                                                                            if (cargo == "SUPERVISOR DE LOGISTICA" && compañia == "10000000")
                                                                                                            {
                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\supervisor_logistica.docx";
                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                            }
                                                                                                            else
                                                                                                            {
                                                                                                                if (cargo == "SUPERVISOR DE MANTENIMIENTO" && compañia == "10000000")
                                                                                                                {
                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\supervisor_mantenimiento.docx";
                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                }
                                                                                                                else
                                                                                                                {
                                                                                                                    if (cargo == "SUPERVISOR DE COMBUSTIBLE" && compañia == "10000000")
                                                                                                                    {
                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\supervisor_combustible.docx";
                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                    }
                                                                                                                    else
                                                                                                                    {
                                                                                                                        if (cargo == "ASISTENTA SOCIAL" && compañia == "10000000")
                                                                                                                        {
                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\asistenta_social.docx";
                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            if (cargo == "ASISTENTE DE AUDITORIA" && compañia == "10000000")
                                                                                                                            {
                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\asistente_auditoria.docx";
                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                            }
                                                                                                                            else
                                                                                                                            {
                                                                                                                                if (cargo == "ASISTENTE DE COMBUSTIBLE" && compañia == "10000000")
                                                                                                                                {
                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\asistente_combustible.docx";
                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                }
                                                                                                                                else
                                                                                                                                {
                                                                                                                                    if (cargo == "ASISTENTE DE CONTROL DOCUMENTARIO" && compañia == "10000000")
                                                                                                                                    {
                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\asistente_control_documentario.docx";
                                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                    }
                                                                                                                                    else
                                                                                                                                    {
                                                                                                                                        if (cargo == "ASISTENTE DE LOGISTICA" && compañia == "10000000")
                                                                                                                                        {
                                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\asistente_logistica.docx";
                                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                        }
                                                                                                                                        else
                                                                                                                                        {
                                                                                                                                            if (cargo == "ASISTENTE DE PATIO" && compañia == "10000000")
                                                                                                                                            {
                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\asistente_patio.docx";
                                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                            }
                                                                                                                                            else
                                                                                                                                            {
                                                                                                                                                if (cargo == "ASISTENTE DE TECNOLOGÍA DE LA INFORMACIÓN" && compañia == "10000000")
                                                                                                                                                {
                                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\asistente_ti.docx";
                                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                }
                                                                                                                                                else
                                                                                                                                                {
                                                                                                                                                    if (cargo == "AUXILIAR DE CONTABILIDAD" && compañia == "10000000")
                                                                                                                                                    {
                                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\aux_contabilidad.docx";
                                                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                    }
                                                                                                                                                    else
                                                                                                                                                    {
                                                                                                                                                        if (cargo == "AUXILIAR DE OFICINA" && compañia == "10000000")
                                                                                                                                                        {
                                                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\aux_oficina.docx";
                                                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                        }
                                                                                                                                                        else
                                                                                                                                                        {
                                                                                                                                                            if (cargo == "AUXILIAR DE OPERACIONES" && compañia == "10000000")
                                                                                                                                                            {
                                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\aux_operaciones.docx";
                                                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                            }
                                                                                                                                                            else
                                                                                                                                                            {
                                                                                                                                                                if (cargo == "AYUDANTE DE ALMACEN" && compañia == "10000000")
                                                                                                                                                                {
                                                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\ayudante_almacen.docx";
                                                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                }
                                                                                                                                                                else
                                                                                                                                                                {
                                                                                                                                                                    if (cargo == "AYUDANTE MECANICO" && compañia == "10000000")
                                                                                                                                                                    {
                                                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\ayudante_mecanico.docx";
                                                                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                    }
                                                                                                                                                                    else
                                                                                                                                                                    {
                                                                                                                                                                        if (cargo == "CHOFER OPERATIVO" && compañia == "10000000")
                                                                                                                                                                        {
                                                                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\chofer_operativo.docx";
                                                                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                        }
                                                                                                                                                                        else
                                                                                                                                                                        {
                                                                                                                                                                            if (cargo == "CONSERJE" && compañia == "10000000")
                                                                                                                                                                            {
                                                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\conserje.docx";
                                                                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                            }
                                                                                                                                                                            else
                                                                                                                                                                            {
                                                                                                                                                                                if (cargo == "ASISTENTE DE CONTROL DE INVENTARIO" && compañia == "10000000")
                                                                                                                                                                                {
                                                                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\asistente_inventario.docx";
                                                                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                }
                                                                                                                                                                                else
                                                                                                                                                                                {
                                                                                                                                                                                    if (cargo == "CONTADOR GENERAL" && compañia == "10000000")
                                                                                                                                                                                    {
                                                                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\contador_general.docx";
                                                                                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                    }
                                                                                                                                                                                    else
                                                                                                                                                                                    {
                                                                                                                                                                                        if (cargo == "JEFE DE FINANZAS" && compañia == "10000000")
                                                                                                                                                                                        {
                                                                                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\jefe_finanzas.docx";
                                                                                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                        }
                                                                                                                                                                                        else
                                                                                                                                                                                        {
                                                                                                                                                                                            if (cargo == "TORNERO" && compañia == "10000000")
                                                                                                                                                                                            {
                                                                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\tornero.docx";
                                                                                                                                                                                                //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                            }
                                                                                                                                                                                            else
                                                                                                                                                                                            {
                                                                                                                                                                                                if (cargo == "OPERADOR DE MAQUINARIA PESADA" && compañia == "10000000")
                                                                                                                                                                                                {
                                                                                                                                                                                                    plantilla = "C:\\Transpesa\\Plantillas\\oper_maquina.docx";
                                                                                                                                                                                                    //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                                }
                                                                                                                                                                                                else
                                                                                                                                                                                                {
                                                                                                                                                                                                    if (cargo == "SOLDADOR" && compañia == "10000000")
                                                                                                                                                                                                    {
                                                                                                                                                                                                        plantilla = "C:\\Transpesa\\Plantillas\\soldador.docx";
                                                                                                                                                                                                        //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                                    }
                                                                                                                                                                                                    else
                                                                                                                                                                                                    {
                                                                                                                                                                                                        if (cargo == "COORDINADOR DE OPERACIONES LIMA" && compañia == "10000000")
                                                                                                                                                                                                        {
                                                                                                                                                                                                            plantilla = "C:\\Transpesa\\Plantillas\\coordinador_opera_lima.docx";
                                                                                                                                                                                                            //plantilla = "C:\\Transpesa\\Plantillas\\opera_renov_2018.docx";
                                                                                                                                                                                                        }
                                                                                                                                                                                                        else
                                                                                                                                                                                                        {
                                                                                                                                                                                                            if (compañia == "10000000")
                                                                                                                                                                                                            {
                                                                                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\adm_renov.docx";
                                                                                                                                                                                                            }
                                                                                                                                                                                                            else
                                                                                                                                                                                                            {
                                                                                                                                                                                                                plantilla = "C:\\Transpesa\\Plantillas\\bra_renov.docx";
                                                                                                                                                                                                            }
                                                                                                                                                                                                        }
                                                                                                                                                                                                    }
                                                                                                                                                                                                }
                                                                                                                                                                                            }
                                                                                                                                                                                        }
                                                                                                                                                                                    }

                                                                                                                                                                                }
                                                                                                                                                                            }
                                                                                                                                                                        }
                                                                                                                                                                    }
                                                                                                                                                                }
                                                                                                                                                            }
                                                                                                                                                        }
                                                                                                                                                    }
                                                                                                                                                }
                                                                                                                                            }
                                                                                                                                        }
                                                                                                                                    }
                                                                                                                                }
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                                
                                                                                                            }
                                                                                                            
                                                                                                        }
                                                                                                        
                                                                                                    }
                                                                                                    
                                                                                                }
                                                                                                
                                                                                            }
                                                                                            
                                                                                        }
                                                                                        
                                                                                    }
                                                                                    
                                                                                }

                                                                            }

                                                                        }
                                                                        
                                                                    }

                                                                }
                                                                
                                                            }
                                                            
                                                        }
                                                        
                                                    }
                                                    
                                                }
                                                
                                            }
                                            
                                        }
                               
                                    }
                                   
                                }
                                
                            }
                        }
                    }
                }
               ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                System.IO.File.Copy(plantilla, Reporte, true);

                objWord = new Word.Application();
                objWord.Documents.Open(Reporte, oMissing, oMissing);

                #region Markers

                object Nombre = "Nombre";
                object Nombre2 = "Nombre2";
                object DNI = "DNI";
                object DNI2 = "DNI2";
                object Cargo = "Cargo";
                object Cargo2 = "Cargo2";
                object Direccion = "Direccion";
                object Fecha_inicio = "Fecha_inicio";
                object Fecha_fin = "Fecha_fin";
                object Tiempo_dias = "Tiempo_dias";   
                object Tiempo_meses = "Tiempo_meses";
                object Dia_susc = "Dia_susc";
                object Mes_susc = "Mes_susc";
                object Año_susc = "Año_susc";
                object Sueldo = "Sueldo";
                object Sueldo_letras = "Sueldo_letras";
                object Asig_familiar = "Asig_familiar";
                //object Ini_Prueba = "Ini_Prueba";
                //object Fin_Prueba = "Fin_Prueba";

                if (objWord.ActiveDocument.Bookmarks.Count > 0)
                {
                    if(compañia == "10000000")//transpesa
                    {
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Nombre).Range.Text = nombre;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Nombre2).Range.Text = nombre;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref DNI).Range.Text = dni;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref DNI2).Range.Text = dni;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Cargo).Range.Text = cargo;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Cargo2).Range.Text = cargo;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Direccion).Range.Text = direccion;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Fecha_inicio).Range.Text = fini;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Fecha_fin).Range.Text = ffin;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Tiempo_dias).Range.Text = tdias;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Tiempo_meses).Range.Text = tmeses;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Dia_susc).Range.Text = diasusc;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Mes_susc).Range.Text = messusc;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Año_susc).Range.Text = añosusc;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Sueldo).Range.Text = string.Format("{0:0.00}", Convert.ToDouble(sueldo));
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Sueldo_letras).Range.Text = sueldoletras;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Asig_familiar).Range.Text = asigfam;
                        //objWord.ActiveDocument.Bookmarks.get_Item(ref Ini_Prueba).Range.Text = iniprueb;
                        //objWord.ActiveDocument.Bookmarks.get_Item(ref Fin_Prueba).Range.Text = finprueb;
                    }
                    else //bra
                    {
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Nombre).Range.Text = nombre;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Nombre2).Range.Text = nombre;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref DNI).Range.Text = dni;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Cargo).Range.Text = cargo;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Direccion).Range.Text = direccion;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Fecha_inicio).Range.Text = fini;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Fecha_fin).Range.Text = ffin;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Tiempo_dias).Range.Text = tdias;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Tiempo_meses).Range.Text = tmeses;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Dia_susc).Range.Text = diasusc;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Mes_susc).Range.Text = messusc;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Año_susc).Range.Text = añosusc;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Sueldo).Range.Text = string.Format("{0:0.00}", Convert.ToDouble(sueldo));
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Sueldo_letras).Range.Text = sueldoletras;
                        objWord.ActiveDocument.Bookmarks.get_Item(ref Asig_familiar).Range.Text = asigfam;
                        //objWord.ActiveDocument.Bookmarks.get_Item(ref Ini_Prueba).Range.Text = iniprueb;
                        //objWord.ActiveDocument.Bookmarks.get_Item(ref Fin_Prueba).Range.Text = finprueb;
                    }
                }

                #endregion

                objWord.ActiveDocument.Save();

                Export.ToPDF(objWord, filename, false, true);

                //Process.Start(filename);

                //foreach (Process proceso in Process.GetProcesses())
                //    if (proceso.ProcessName.ToLower().CompareTo("winword") == 0)
                //        proceso.Kill();

                return filename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }

            try
            {
                objWord.Documents.Close();
                objWord.Quit();
            }
            catch (Exception x)
            {

            }
        }
       
        private void ImprimirPDF(string ruta)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = Path.GetFullPath(ruta); ;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            //if (false == p.CloseMainWindow())
            //    p.Kill();
        }

        private void dtgvDataView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = sender as GridView;

            bool resultado;
            resultado = clsRecursosHumanosBL.Instancia.UpdateSCTR(Convert.ToInt32(e.Value),
                Convert.ToInt32(view.GetRowCellValue(e.RowHandle, dtgvDataView.Columns["ID"])));
            if (resultado != true)
            {
                Mensaje m = new Mensaje();
                m.Width = 500;
                m.mensaje = "Error";
                m.ShowDialog();
            }
            else
            {
                Mensaje m2 = new Mensaje();
                m2.Width = 320;
                m2.mensaje = "Se actualizó el SCTR del empleado";
                m2.ShowDialog();
            }

        }

        private void btnSCTR_Click(object sender, EventArgs e)
        {
            string transpesa = "10000000";
            string bra = "40000000";
            //char estado = 'A';
            if (chkCompania.Checked == true)
            {
                if (cboCompañia.SelectedIndex == 0)
                {
                    transpesa = "10000000";
                    bra = "";
                }
                else
                {
                    bra = "40000000";
                    transpesa = "";
                }
            }
            dtgvSCTR.DataSource = null;
            dtgvSCTRView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsRecursosHumanosBL.Instancia.GetSCTR(transpesa, bra);
            if (dt.Rows.Count > 0)
            {
                dtgvSCTR.DataSource = dt;
                string periodo = (DateTime.Now).AddMonths(1).ToString("MM-yyyy", CultureInfo.InvariantCulture);
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "SCTR " + cboCompañia.SelectedItem + " " + periodo + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvSCTR.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "ERROR";
                m.ShowDialog();
            }
        }

        private void btnMemorandum_Click(object sender, EventArgs e)
        {
            AgregarDocumento();
        }

        public void AgregarDocumento() 
        {
            int[] filas = dtgvDataView.GetSelectedRows();
            if (filas.Length != 0)
            {
                if (frmMemos_Compromisos == null || frmMemos_Compromisos.IsDisposed)
                {
                    frmMemos_Compromisos = new Memos_Compromisos();
                    frmMemos_Compromisos.CargarDoc += new Memos_Compromisos.CargarDocEventHandler(PrepararDoc);
                    //frmMemos_Compromisos.MdiParent = this.ParentForm;
                    //frmMemos_Compromisos.Tipodoc = Tipodoc;
                    //frmMemos_Compromisos.Asuntodoc = Asuntodoc;
                    //frmMemos_Compromisos.Fechadoc = Fechadoc;
                    //frmMemos_Compromisos.Cuerpodoc = Cuerpodoc;
                    frmMemos_Compromisos.Show();
                }
                else
                {
                    frmMemos_Compromisos.Activate();
                }
            }
        }

        public void PrepararDoc(Boolean EsCorrecto) 
        {
            if (EsCorrecto)
            {
                List<int> filas = new List<int>(dtgvDataView.GetSelectedRows());
                //int[] filas = dtgvDataView.GetSelectedRows();
                List<string> listadocumentos = new List<string>();
                string filename = "";
                int correlativo;
                //int contador;
                string empleado="";
                string saludo="";
                bool resultado;
                if (frmMemos_Compromisos.Tipodoc == "C")
                {
                    correlativo = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("CX") + 1;
                    //contador = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("CX") + 1;
                    //filename = "D:\\Compromisos\\Comunicado Interno";
                    filename = "T:\\MEMORAMDUM\\" + Utilitario.Instancia.SesionUsuario.usuario + "\\Comunicado Interno ";
                    //filename = "D:\\sescobedo\\MEMORANDUMS\\AÑO 2017\\GERENCIA GENERAL\\COMPROMISOS\\Comunicado Interno ";
                }
                else
                {
                    correlativo = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("MX") + 1;
                    
                    //contador = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("MX") + 1;
                    filename = "T:\\MEMORAMDUM\\" + Utilitario.Instancia.SesionUsuario.usuario + "\\Memorandum ";
                    //filename = "D:\\Memorandums\\Memorandum";
                    //filename = "D:\\sescobedo\\MEMORANDUMS\\AÑO 2017\\GERENCIA GENERAL\\MEMORANDUMS\\Memorandum ";
                }
                if (filas[0] == -1) 
                {
                    for (int i = 0; i < filas.Count - 1; i++) 
                    {
                        filas[i] = filas[i + 1];
                    }
                    filas.RemoveAt(filas.Count-1);
                }
                for (int i = 0; i < filas.Count; i++)
                {
                    //if (dtgvDataView.GetRowCellValue(filas[i], "SEXO").ToString() == "M")
                    //{
                    //    empleado = "Sr. " + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString();
                    //    saludo = "Estimado " + empleado; 
                    //}
                    //else 
                    //{
                    //    empleado = "Sra./Srta. " + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString();
                    //    saludo = "Estimada " + empleado; 
                    //}
               
                    empleado = "Sr(a). " + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString();
                    saludo = "Estimado(a) " + empleado; 

                    listadocumentos.Add(generaPDFDocumento(
                        filename + (correlativo + i).ToString() + " " + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString() + ".pdf",
                        (correlativo + i).ToString(),
                        frmMemos_Compromisos.Tipodoc,
                        empleado,
                        dtgvDataView.GetRowCellValue(filas[i], "CARGO").ToString(),
                        frmMemos_Compromisos.Asuntodoc,
                        frmMemos_Compromisos.Fechadoc,
                        saludo,
                        frmMemos_Compromisos.Cuerpodoc,
                        frmMemos_Compromisos.Firma));
                        resultado = clsRecursosHumanosBL.Instancia.InsertDocumento(correlativo + i, 
                        Convert.ToInt32(dtgvDataView.GetRowCellValue(filas[i], "ID")),
                        frmMemos_Compromisos.Tipodoc, frmMemos_Compromisos.Asuntodoc, 
                        frmMemos_Compromisos.Fechadoc,frmMemos_Compromisos.Cuerpodoc,Utilitario.Instancia.SesionUsuario.usuario);
                    if (resultado != true)
                    {
                        MessageBox.Show("Error al guardar", "Mensaje");
                        break;
                    }
                }
                DialogResult dialogResult = MessageBox.Show("Se generaron " + filas.Count + " documentos satisfactoriamente. ¿Desea imprimirlos?", "Confirmación", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    for (int i = 0; i < listadocumentos.Count; i++)
                    {
                        ImprimirPDF(listadocumentos[i]);
                    }
                }
                dtgvDataView.ClearSelection();
            }
            else
            {
                dtgvDataView.ClearSelection();
            }
        }

        private string generaPDFDocumento(string filename, string correlativo,string tipodoc, string empleado, string cargo,
            string asunto, string fecha, string saludo, string cuerpo, bool firma)
        {
            try
            {
                foreach (Process proceso in Process.GetProcesses())
                    if (proceso.ProcessName.ToLower().CompareTo("winword") == 0)
                        proceso.Kill();

                string plantilla;
                string Reporte = "C:\\Transpesa\\Plantillas\\reporte.docx";

                //////Eleccion de la plantilla
                if (tipodoc == "C") //compromiso
                {

                    if (firma == true)
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\Comun_interno_firma.docx";
                    }
                    else
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\Comun_interno.docx";
                    }
                }
                else //memo
                {
                    if (firma == true)
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\Memorandum_firma.docx";
                    }
                    else
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\Memorandum.docx";
                    }
                }
                ///////////////////////////////////////////

                System.IO.File.Copy(plantilla, Reporte, true);

                objWord = new Word.Application();
                objWord.Visible = true;

           

                objWord.Documents.Open(Reporte, oMissing, oMissing);

                #region Markers

                object Correlativo = "Correlativo";
                object Empleado = "Empleado";
                object Cargo = "Cargo";
                object Asunto = "Asunto";
                object Fecha = "Fecha";
                object Saludo = "Saludo";
                object Cuerpo = "Cuerpo";

                if (objWord.ActiveDocument.Bookmarks.Count > 0)
                {
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Correlativo).Range.Text = correlativo;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Empleado).Range.Text = empleado;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Cargo).Range.Text = cargo;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Asunto).Range.Text = asunto;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Fecha).Range.Text = fecha;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Saludo).Range.Text = saludo;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Cuerpo).Range.Text = cuerpo;
                }

                #endregion
                
                objWord.ActiveDocument.Save();
                
                Export.ToPDF(objWord, filename, false, true);

                //Process.Start(filename);

                //foreach (Process proceso in Process.GetProcesses())
                //    if (proceso.ProcessName.ToLower().CompareTo("winword") == 0)
                //        proceso.Kill();

                return filename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }

            finally
            {
                //objWord.Documents.Close(Word.WdSaveOptions.wdDoNotSaveChanges);
                //objWord.Quit(Word.WdSaveOptions.wdDoNotSaveChanges);
            }

        }

        private void cboCompañia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmListaHojaRecorrido frmhr = new frmListaHojaRecorrido();
            frmhr.ShowDialog();

        }
        
    }
}
