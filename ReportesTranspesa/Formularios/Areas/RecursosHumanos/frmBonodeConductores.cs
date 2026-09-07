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
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System.Runtime.CompilerServices;
using DevExpress.XtraGrid.Columns;
using DevExpress.Data;
using ReportesTranspesa.Sistema;
using System.Diagnostics;
using DevExpress.Utils;
using DevExpress.Utils.Menu;
using System.Globalization;
using DevExpress.XtraEditors.Helpers;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using System.IO;
using System.Reflection;
using ReportesTranspesa.Properties;
using DevExpress.XtraGrid.Views.Grid;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmBonodeConductores : Form
    {         
         string vfechaCierre;
         string vMesApertura;
         int AccesoModificar = 1;//VARIABLE PARA ACCESOS A MOFICAR OPERACIONES
         string _Usuario;
         string valor;
         string Periodo;
         string periodoExcepciones;
         int PeriodoCerrado = 0;
         DataTable dtBonos = new DataTable();
        public frmBonodeConductores()
        {
            InitializeComponent();
        }

        private void frmBonodeConductores_Load(object sender, EventArgs e)
        {
          // Buscardatos(); 
            splitContainer1.Panel1Collapsed = true;

            decimal tiempo = 12.000000m; 
          
            DateTime fecha = Convert.ToDateTime(dtpFechaCierre.Text);
            DateTime mesmodificar = Convert.ToDateTime(dtpFechaCierre.Text);
            int dia = Convert.ToInt32(fecha.Day.ToString());
            int mes = Convert.ToInt32(fecha.Month.ToString());
            if(mes == 1)
            {
                mes = -1;
            }
            else
            {
                mes = - 1;
            }
           
            if (dia>23)
            {
               int dias = dia - 23;
               tiempo = dias;

               decimal diaNuevo = Math.Ceiling(tiempo);
               decimal negDia = -Math.Abs(diaNuevo);
               

               diaNuevo = diaNuevo + 1;
               decimal negDia2 = -Math.Abs(diaNuevo);
               DateTime mesmenos = mesmodificar.AddMonths(mes);
               DateTime FechaApertutar = mesmenos.AddDays(Convert.ToDouble(negDia));
               DateTime fecha2 = fecha.AddDays(Convert.ToDouble(negDia2));
               vfechaCierre = Convert.ToString(fecha2);
               vMesApertura = Convert.ToString(FechaApertutar);
            }

            if (dia < 23)
            {
               int dia1 =  23 - dia  ;
                tiempo = dia1;

                decimal diaNuevo = Math.Ceiling(tiempo);
                DateTime fecha2 = fecha.AddDays(Convert.ToDouble(diaNuevo - 1));

                DateTime mesmenos = mesmodificar.AddMonths(mes);
                DateTime FechaApertutar = mesmenos.AddDays(Convert.ToDouble(diaNuevo));
                vfechaCierre = Convert.ToString(fecha2);
                vMesApertura = Convert.ToString(FechaApertutar);
            }

            if (dia == 23)
            {
               int dia2 = 23;
                tiempo = dia2;
                decimal diaNuevo = Math.Ceiling(tiempo);
                DateTime fecha2 = fecha.AddDays(Convert.ToDouble(diaNuevo - 1));

                DateTime mesmenos = mesmodificar.AddMonths(mes);
                DateTime FechaApertutar = mesmenos.AddDays(Convert.ToDouble(diaNuevo));
                vfechaCierre = Convert.ToString(fecha2);
                vMesApertura = Convert.ToString(FechaApertutar);
            }           

            //this.Text = "BONO DE"
             label1.Text = "BONO DE CONDUCTORES PERIODO: " + vfechaCierre.Substring(6, 4) + "" + vfechaCierre.Substring(3, 2); 
            dtpFechaCierre.Text = vfechaCierre;
            dtpFechaCierre.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpFechaApertura.Text = vMesApertura;
            dtpFechaApertura.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            //listaBonos();
        }       

        private void Buscardatos() 
        {
            // Switch to the Advanced Banded Grid View. 
            AdvBandedGridView view = new AdvBandedGridView();
            view.OptionsBehavior.AutoPopulateColumns = false;
            grvBonoConductores.MainView = view;

            // Create top-level bands.  
            GridBand bandMain = new GridBand() { Caption = "Main" };
            GridBand bandPerformanceAttributes = new GridBand() { Caption = "Performance Attributes" };
            GridBand bandNotes = new GridBand() { Caption = "Notes" };
            view.Bands.AddRange(new GridBand[] { bandMain, bandPerformanceAttributes, bandNotes });

            // Create nested bands.
            GridBand bandModel = new GridBand { Caption = "Model" };
            GridBand bandPrice = new GridBand { Caption = "Price" };
            bandMain.Children.AddRange(new GridBand[] { bandModel, bandPrice });

            // Create banded grid columns and make them visible.
            BandedGridColumn colTrademark = new BandedGridColumn() { FieldName = "Trademark", Visible = true };
            BandedGridColumn colModel = new BandedGridColumn() { FieldName = "Model", Visible = true };
            BandedGridColumn colCategory = new BandedGridColumn() { FieldName = "Category", Visible = true };
            BandedGridColumn colPrice = new BandedGridColumn() { FieldName = "Price", Visible = true };
            BandedGridColumn colHP = new BandedGridColumn() { FieldName = "HP", Visible = true };
            BandedGridColumn colLiter = new BandedGridColumn() { FieldName = "Liter", Visible = true };
            BandedGridColumn colCyl = new BandedGridColumn() { FieldName = "Cyl", Visible = true };
            BandedGridColumn colDescription = new BandedGridColumn() { FieldName = "Description", Visible = true };
            BandedGridColumn colPicture = new BandedGridColumn() { FieldName = "Picture", Visible = true };
            view.Columns.AddRange(new BandedGridColumn[] { colTrademark, colModel, colCategory, colPrice, colHP, colLiter, colCyl, colDescription, colPicture });

            // Format the Price column values as currency.
            colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            colPrice.DisplayFormat.FormatString = "c2";

            // Assign columns to bands.  
           colTrademark.OwnerBand = bandModel;
            colModel.OwnerBand = bandModel;
            colCategory.OwnerBand = bandModel;
            colPrice.OwnerBand = bandPrice;
            colHP.OwnerBand = bandPerformanceAttributes;
            colLiter.OwnerBand = bandPerformanceAttributes;
            colCyl.OwnerBand = bandPerformanceAttributes;
            colDescription.OwnerBand = bandNotes;
            colPicture.OwnerBand = bandNotes;

            // Set the vertical position of column headers.
            colCategory.RowIndex = 1;
            colLiter.RowIndex = 1;

            // Stretch columns to fit empty spaces below them.
            colPrice.AutoFillDown = true;
            colDescription.AutoFillDown = true;
            colPicture.AutoFillDown = true;       
        }

        private void listaBonos() 
        {
           // InitGrid();
            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = dtpFechaApertura.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaCierre.Value.ToShortDateString() + " 23:59:59";

            if (dtpFechaApertura.Value > dtpFechaCierre.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaApertura.Focus();

                return;
            }

            grvBonoConductores.DataSource = null;
            gridView2.Columns.Clear();
                        
            dtBonos = clsRecursosHumanosBL.Instancia.GetListarBonosPeriodo(dtpFechaCierre.Text.Substring(6, 4) + "" + dtpFechaCierre.Text.Substring(3, 2), dtpFechaApertura.Value.ToShortDateString(), dtpFechaCierre.Value.ToShortDateString());

            if (dtBonos.Rows.Count > 0)
            {
                grvBonoConductores.DataSource = dtBonos;
                gridView2.OptionsBehavior.Editable = false;

                gridView2.Columns["IDCONDUCTOR"].Visible = false;
                gridView2.Columns["IDOPERACION"].Visible = false;
                gridView2.Columns["IDUNIDAD"].Visible = false;
                gridView2.Columns["CONDUCTOR"].Width = 250;
                gridView2.Columns["CONDUCTOR"].SortOrder = ColumnSortOrder.Ascending;
                gridView2.Columns["ICO"].Width = 10;
                gridView2.Columns["ICS"].Width = 10;
                gridView2.Columns["MONTOCOMB"].DisplayFormat.FormatType = FormatType.Numeric;
                gridView2.Columns["MONTOCOMB"].DisplayFormat.FormatString = "c2";

                gridView2.Columns["MONTOSEG"].DisplayFormat.FormatType = FormatType.Numeric;
                gridView2.Columns["MONTOSEG"].DisplayFormat.FormatString = "c2";
                gridView2.Columns["SUB_TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                gridView2.Columns["SUB_TOTAL"].DisplayFormat.FormatString = "c2";
                gridView2.Columns["BONO_TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                gridView2.Columns["BONO_TOTAL"].DisplayFormat.FormatString = "c2";
                gridView2.Columns["EXCEPCION"].DisplayFormat.FormatType = FormatType.Numeric;
                gridView2.Columns["EXCEPCION"].DisplayFormat.FormatString = "c2";

                gridView2.Columns["% DIF."].DisplayFormat.FormatType = FormatType.Numeric;
                gridView2.Columns["% DIF."].DisplayFormat.FormatString = "{0:N2} %";

                gridView2.Columns["CODIGO"].Fixed = FixedStyle.Left;
                gridView2.Columns["CONDUCTOR"].Fixed = FixedStyle.Left;
                gridView2.Columns["OPERACION"].Fixed = FixedStyle.Left;               

                GridFormatRule gridFormatRule = new GridFormatRule();
                FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                GridFormatRule gridFormatRuleB = new GridFormatRule();
                FormatConditionRuleIconSet formatConditionRuleIconSetB = new FormatConditionRuleIconSet();
                FormatConditionIconSet iconSetB = formatConditionRuleIconSetB.IconSet = new FormatConditionIconSet();
                FormatConditionIconSetIcon icon1B = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon2B = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon3B = new FormatConditionIconSetIcon();             

                //Choose predefined icons.
                icon1.PredefinedName = "TrafficLights3_1.png";
                icon2.PredefinedName = "TrafficLights3_3.png";
                icon3.PredefinedName = "TrafficLights3_3.png";

                icon1B.PredefinedName = "TrafficLights3_1.png";
                icon2B.PredefinedName = "TrafficLights3_3.png";
                icon3B.PredefinedName = "TrafficLights3_3.png";
              
                //Specify the type of threshold values.
                //iconSet.ValueType = FormatConditionValueType.Percent;
                iconSet.ValueType = FormatConditionValueType.Number;
                iconSetB.ValueType = FormatConditionValueType.Number;

                //Define ranges to which icons are applied by setting threshold values.
                icon1.Value = 3; // target range: >=75
                icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon2.Value = 2; // NO APLICA
                icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon3.Value = 1; // target range: >75
                icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                icon1B.Value = 3; // target range: >=75
                icon1B.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon2B.Value = 2; // NO APLICA
                icon2B.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon3B.Value = 1; // target range: >75
                icon3B.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                //Add icons to the icon set.
                iconSet.Icons.Add(icon1);
                iconSet.Icons.Add(icon3);
                iconSet.Icons.Add(icon3);

                iconSetB.Icons.Add(icon1B);
                iconSetB.Icons.Add(icon3B);
                iconSetB.Icons.Add(icon3B);

                //Specify the rule type.
                gridFormatRule.Rule = formatConditionRuleIconSet;
                //Specify the column to which formatting is applied.
                gridFormatRule.Column = gridView2.Columns["ICS"];               
                //Add the formatting rule to the GridView.
                gridView2.FormatRules.Add(gridFormatRule);
                gridFormatRuleB.Rule = formatConditionRuleIconSetB;
                //Specify the column to which formatting is applied.
                gridFormatRuleB.Column = gridView2.Columns["ICO"];
                //Add the formatting rule to the GridView.
                gridView2.FormatRules.Add(gridFormatRuleB);                          
              //  gridView1.BestFitColumns();
                gridView2.Columns["BONO_SEG"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "BONO_SEG", "TOTALES:");
                gridView2.Columns["BONO_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "BONO_TOTAL", "{0:C2}");
                gridView2.Columns["SUB_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SUB_TOTAL", "{0:C2}");
                gridView2.Columns["EXCEPCION"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "EXCEPCION", "{0:C2}");
                gridView2.Columns["MONTOSEG"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTOSEG", "{0:C2}");
                gridView2.Columns["MONTOCOMB"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTOCOMB", "{0:C2}");
                gridView2.Columns["ASISTENCIAS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ASISTENCIAS", "{0}");
                gridView2.Columns["FALTAS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FALTAS", "{0}");
                gridView2.Columns["PERMISO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PERMISO", "{0}");
                gridView2.Columns["DESMEDICO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DESMEDICO", "{0}");
                gridView2.Columns["VACACIONES"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "VACACIONES", "{0}");
                gridView2.Columns["LICENCIA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "LICENCIA", "{0}");
                gridView2.Columns["SUSPENSION"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SUSPENSION", "{0}");
             
              lblTotal.Text = "Total Conductores: "+ Convert.ToString(dtBonos.Rows.Count);

              gridView2.BestFitColumns();
             
            }
            else 
            {
                grvBonoConductores.DataSource = null;
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (gridView2.DataSource == null)
            {
                MessageBox.Show("AVISO", "No hay data para exportar");
            }
            else
            {
                if (checkBox1.Checked == false)
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Bono de Conductores del " + dtpFechaApertura.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaCierre.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    gridView2.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Bono de Conductores del Periodo Cerrado - " + Periodo.Substring(0, 6) + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    gridView2.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
        }  
            

        private void Filtros()
        {
            try
            {
                string colFiltrar = "CONDUCTOR";
                ((DataTable)grvBonoConductores.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);             

            }
            catch (Exception)
            {
            
            }
        }

        private void gridView2_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            string FechaInicioDetalle;
            string FechaFinDetalle;
            try
            {
            decimal PuntajeCombustible;
                if (AccesoModificar == 1)
                {
                    int[] filass = gridView2.GetSelectedRows();
                    string datoseleccionado = gridView2.GetFocusedValue().ToString();

                    for (int i = 0; i < filass.Length; i++)
                    {
                        int IdConductorDetalle = Convert.ToInt32(gridView2.GetRowCellValue(filass[i], "IDCONDUCTOR").ToString());
                        string ConductorDetalle = gridView2.GetRowCellValue(filass[i], "CONDUCTOR").ToString();
                        string Operacion = gridView2.GetRowCellValue(filass[i], "OPERACION").ToString();
                        decimal PuntajeSeguridad = Convert.ToDecimal(gridView2.GetRowCellValue(filass[i], "PUNTAJE_SEGURIDAD").ToString());
                        int operacion = Convert.ToInt32(gridView2.GetRowCellValue(filass[i], "IDOPERACION").ToString());
                        if (operacion == 1)
                        { PuntajeCombustible = Convert.ToDecimal(gridView2.GetRowCellValue(filass[i], "DIFERENCIA").ToString()); }
                        else
                        { PuntajeCombustible = Convert.ToDecimal(gridView2.GetRowCellValue(filass[i], "% DIF.").ToString()); }

                        if (PeriodoCerrado == 0)
                        {
                             FechaInicioDetalle = dtpFechaApertura.Value.ToShortDateString();
                             FechaFinDetalle = dtpFechaCierre.Value.ToShortDateString();
                        }
                        else
                        {
                            string MesInicioCerrado = Periodo.Substring(4, 2);
                            int AnioInicioCerrado = Convert.ToInt32(Periodo.Substring(0, 4));
                            int _mes,_anio;
                            if (MesInicioCerrado.Equals("01")) { _mes = 12; _anio = AnioInicioCerrado - 1; } else { _mes = Convert.ToInt32(MesInicioCerrado) - 1; _anio = AnioInicioCerrado; }

                            if (_mes <10){MesInicioCerrado = "0"+_mes.ToString();}else {MesInicioCerrado = _mes.ToString();}

                            FechaInicioDetalle = "23/" + MesInicioCerrado + "/" + _anio.ToString();
                            FechaFinDetalle = "22/" + Periodo.Substring(4, 2) + "/" + Periodo.Substring(0, 4);
                        }

                        /* if (gridView1.FocusedColumn.FieldName == "PUNTAJE_COMBUSTIBLE")
                         {  */
                        e.Menu.Items.Add(new DXMenuItem("&VER DETALLE COMBUSTIBLE",
                            new EventHandler((snd, evt) =>
                            {
                                frmBonoConductoresDetalle frmDetalle = new frmBonoConductoresDetalle();
                                frmDetalle.EnviarDatosDetalle(1, IdConductorDetalle, ConductorDetalle, FechaInicioDetalle, FechaFinDetalle,PuntajeCombustible,operacion);
                                frmDetalle.Text = "Detalle de Bono Combustible";
                                frmDetalle.Show();
                                
                            }

                          ),Resources.abasteciop));
                        /* }
                         if (gridView1.FocusedColumn.FieldName == "PUNTAJE_SEGURIDAD")
                         {*/
                        e.Menu.Items.Add(new DXMenuItem("&VER DETALLE SEGURIDAD",
                            new EventHandler((snd, evt) =>
                            {
                                frmBonoConductoresDetalle frmDetalle = new frmBonoConductoresDetalle();
                                frmDetalle.EnviarDatosDetalle(2, IdConductorDetalle, ConductorDetalle, FechaInicioDetalle, FechaFinDetalle,PuntajeSeguridad,operacion);
                                frmDetalle.Text = "Detalle de Bono Seguridad";
                                frmDetalle.Show();
                            }

                          ),Resources.bonoseguridadicono));
                        /* }
                         if (gridView1.FocusedColumn.FieldName == "MERMA")
                         {*/
                        e.Menu.Items.Add(new DXMenuItem("&VER DETALLE MERMAS",
                            new EventHandler((snd, evt) =>
                            {
                                frmBonoConductoresDetalle frmDetalle = new frmBonoConductoresDetalle();
                                frmDetalle.EnviarDatosDetalle(3, IdConductorDetalle, ConductorDetalle, FechaInicioDetalle, FechaFinDetalle,0,0);
                                frmDetalle.Text = "Detalle de Mermas Por Conductor";
                                frmDetalle.Show();
                            }

                          ),Resources.mermaicono));

                       e.Menu.Items.Add(new DXMenuItem("&AGREGAR EXCEPCION",
                            new EventHandler((snd, evt) =>
                            {
                                
                                DataTable DTPermisos = new DataTable();
                                DTPermisos = clsOperacionesBL.Instancia.GetDataPermisoRegitroExcepciones(Utilitario.Instancia.SesionUsuario.usuario);
                                if (checkBox1.Checked == false)
                                {
                                    if (DTPermisos.Rows.Count > 0)
                                    {
                                        if (checkBox1.Checked == false)
                                        {
                                             periodoExcepciones = dtpFechaCierre.Text.Substring(6, 4) + "" + dtpFechaCierre.Text.Substring(3, 2);
                                        }
                                        else 
                                        {
                                            periodoExcepciones = Periodo;
                                        }
                                        frmBonoConductoresExcepciones frmDetalle = new frmBonoConductoresExcepciones();
                                        frmDetalle.idConductor = IdConductorDetalle;
                                        frmDetalle.conductor = ConductorDetalle;
                                        frmDetalle.periodo = periodoExcepciones;
                                        frmDetalle.Operacion = Operacion;
                                        frmDetalle.StartPosition = FormStartPosition.CenterParent;
                                        frmDetalle.Show();
                                    }
                                    else
                                    {
                                        MessageBox.Show("No tienes acceso a registrar Excepciones", "ALERTA");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Periodo cerrado, no puedes registrar excepciones", "ALERTA");
                                }
                            }
                          ),Resources.excepcionesicono));
                    }                    
                }
            }
            catch (Exception)
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Al cerrar el periodo no se permiten cambios a futuro. ¿Desea cerrar el periodo?", "CERRAR PERIODO BONOS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                gridView2.SelectAll();                
                string cadena = "";
                System.Console.WriteLine(cadena);              

                if (gridView2.RowCount > 0)
                {                    
                    int columnscount = 25;                    
                    cadena += "<r>";

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        cadena += "<d";
                        int j = 0;
                        for (j= 0; j < columnscount; j++)
                        {
                            string cabecera = gridView2.Columns[j].Name;
                            if (j == 0) { valor = gridView2.GetRowCellValue(i, "CODIGO").ToString(); cabecera = "CODIGO"; }
                            if (j == 1) { valor = gridView2.GetRowCellValue(i, "IDOPERACION").ToString(); cabecera = "IDOPERACION"; }
                            if (j == 2) { valor = gridView2.GetRowCellValue(i, "FECHAINGRESO").ToString(); cabecera = "FECHAINGRESO"; }
                            if (j == 3) { valor = gridView2.GetRowCellValue(i, "SUELDO").ToString(); cabecera = "SUELDO"; }
                            if (j == 4) { valor = gridView2.GetRowCellValue(i, "KM_RECORRIDO").ToString(); cabecera = "KM_RECORRIDO"; }
                            if (j == 5) { valor = gridView2.GetRowCellValue(i, "CANTIDADVIAJES").ToString(); cabecera = "CANTIDADVIAJES"; }
                            if (j == 6) { valor = gridView2.GetRowCellValue(i, "MERMA").ToString(); cabecera = "MERMA"; }
                            if (j == 7) { valor = gridView2.GetRowCellValue(i, "BONO_SEG").ToString(); cabecera = "BONO_SEG"; }
                            if (j == 8) { valor = gridView2.GetRowCellValue(i, "PUNTAJE_SEGURIDAD").ToString(); cabecera = "PUNTAJE_SEGURIDAD"; }
                            if (j == 9) { valor = gridView2.GetRowCellValue(i, "MONTOSEG").ToString(); cabecera = "MONTOSEG"; }
                            if (j == 10) { valor = gridView2.GetRowCellValue(i, "BONO_COMB").ToString(); cabecera = "BONO_COMB"; }
                            if (j == 11) { valor = gridView2.GetRowCellValue(i, "DIFERENCIA").ToString(); cabecera = "DIFERENCIA"; }
                            if (j == 12) { valor = gridView2.GetRowCellValue(i, "% DIF.").ToString(); cabecera = "PORCENTAJE"; }
                            if (j == 13) { valor = gridView2.GetRowCellValue(i, "MONTOCOMB").ToString(); cabecera = "MONTOCOMB"; }
                            if (j == 14) { valor = gridView2.GetRowCellValue(i, "SUB_TOTAL").ToString(); cabecera = "SUB_TOTAL"; }
                            if (j == 15) { valor = gridView2.GetRowCellValue(i, "EXCEPCION").ToString(); cabecera = "EXCEPCION"; }
                            if (j == 16) { valor = gridView2.GetRowCellValue(i, "BONO_TOTAL").ToString(); cabecera = "BONO_TOTAL"; }
                            if (j == 17) { valor = gridView2.GetRowCellValue(i, "ASISTENCIAS").ToString(); cabecera = "ASISTENCIAS"; }
                            if (j == 18) { valor = gridView2.GetRowCellValue(i, "FALTAS").ToString(); cabecera = "FALTAS"; }
                            if (j == 19) { valor = gridView2.GetRowCellValue(i, "PERMISO").ToString(); cabecera = "PERMISO"; }
                            if (j == 20) { valor = gridView2.GetRowCellValue(i, "DESMEDICO").ToString(); cabecera = "DESMEDICO"; }
                            if (j == 21) { valor = gridView2.GetRowCellValue(i, "VACACIONES").ToString(); cabecera = "VACACIONES"; }
                            if (j == 22) { valor = gridView2.GetRowCellValue(i, "LICENCIA").ToString(); cabecera = "LICENCIA"; }
                            if (j == 23) { valor = gridView2.GetRowCellValue(i, "SUSPENSION").ToString(); cabecera = "SUSPENSION"; }
                            if (j == 24) { valor = gridView2.GetRowCellValue(i, "MOTIVOEXCEPCION").ToString(); cabecera = "MOTIVOEXCEPCION"; }
                            
                            cadena += " " + cabecera;
                            cadena += "=\"" + valor.ToString();
                            cadena += "\"";
                        }                        

                        cadena += " />";
                    }

                    cadena += "</r>";


                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataGenerarCierre("10000000", dtpFechaCierre.Text.Substring(6, 4) + "" + dtpFechaCierre.Text.Substring(3, 2)
                                                                                        , cadena, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       // this.Close();
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }  
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //CHECKBOX PARA HABILITAR Y DESABILITAR EL NAVEGADOR WEB
            if (checkBox1.Checked == true)
            {
                splitContainer1.Panel1Collapsed = false;
                groupBox1.Enabled = false;
                button2.Enabled = false;

                CargaPeriodosCerrados();
                PeriodoCerrado = 1;
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
                groupBox1.Enabled = true;
                grvBonoConductores.DataSource = null;
                button2.Enabled = true;
                PeriodoCerrado = 0;
            }
        }

        private void CargaPeriodosCerrados()
        {
            gridControl1.DataSource = null;
            grvBonoConductores.DataSource = null;
            gridView1.Columns.Clear();
            gridView1.GroupSummary.Clear();
            gridView1.OptionsBehavior.Editable = false;
            DataTable dt = new DataTable();
            dt = clsRecursosHumanosBL.Instancia.GetDataBonoPeriodosCerrados();
            gridControl1.DataSource = dt;            
            
            GridView gridView = gridControl1.FocusedView as GridView;            
            gridView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                new GridColumnSortInfo(gridView.Columns["Anio"], DevExpress.Data.ColumnSortOrder.Descending), 
                }, 1);
           
            gridView1.BestFitColumns();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            label1.Text = "BONO DE CONDUCTORES PERIODO: " + dtpFechaCierre.Text.Substring(6, 4) + "" + dtpFechaCierre.Text.Substring(3, 2);
            listaBonos();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Filtros();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {            
                if (AccesoModificar == 1)
                {
                    int[] filass = gridView1.GetSelectedRows();

                    for (int i = 0; i < filass.Length; i++)
                    {
                        Periodo = gridView1.GetRowCellValue(filass[i], "Periodo").ToString();
                        label1.Text = "BONO DE CONDUCTORES PERIODO: " + Periodo.Substring(0, 6);
                    }

                    grvBonoConductores.DataSource = null;
                    gridView2.Columns.Clear();

                    DataTable DTPeriodosCerrados = new DataTable();

                    DTPeriodosCerrados = clsRecursosHumanosBL.Instancia.GetDataBonoPeriodosCerradosDetalle(Periodo.Substring(0, 6));

                    if (DTPeriodosCerrados.Rows.Count > 0)
                    {
                        grvBonoConductores.DataSource = DTPeriodosCerrados;
                        gridView2.OptionsBehavior.Editable = false;

                        gridView2.Columns["IDCONDUCTOR"].Visible = false;
                        gridView2.Columns["IDOPERACION"].Visible = false;
                        gridView2.Columns["IDUNIDAD"].Visible = false;
                        //gridView1.Columns["ICS"].Visible = false;
                        //gridView1.Columns["ICONO_COMB"].Visible = false;
                        gridView2.Columns["CONDUCTOR"].Width = 250;
                        gridView2.Columns["CONDUCTOR"].SortOrder = ColumnSortOrder.Ascending;
                        //gridView2.Columns["CONDUCTOR"].Image = Image.FromFile("");
                        gridView2.Columns["ICO"].Width = 10;
                        gridView2.Columns["ICS"].Width = 10;
                        gridView2.Columns["MONTOCOMB"].DisplayFormat.FormatType = FormatType.Numeric;
                        gridView2.Columns["MONTOCOMB"].DisplayFormat.FormatString = "c2";

                        gridView2.Columns["MONTOSEG"].DisplayFormat.FormatType = FormatType.Numeric;
                        gridView2.Columns["MONTOSEG"].DisplayFormat.FormatString = "c2";
                        gridView2.Columns["SUB_TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                        gridView2.Columns["SUB_TOTAL"].DisplayFormat.FormatString = "c2";
                        gridView2.Columns["BONO_TOTAL"].DisplayFormat.FormatType = FormatType.Numeric;
                        gridView2.Columns["BONO_TOTAL"].DisplayFormat.FormatString = "c2";
                        gridView2.Columns["EXCEPCION"].DisplayFormat.FormatType = FormatType.Numeric;
                        gridView2.Columns["EXCEPCION"].DisplayFormat.FormatString = "c2";

                        gridView2.Columns["% DIF."].DisplayFormat.FormatType = FormatType.Numeric;
                        gridView2.Columns["% DIF."].DisplayFormat.FormatString = "{0:N2} %";

                        gridView2.Columns["CODIGO"].Fixed = FixedStyle.Left;
                        gridView2.Columns["CONDUCTOR"].Fixed = FixedStyle.Left;
                        gridView2.Columns["OPERACION"].Fixed = FixedStyle.Left;

                        GridFormatRule gridFormatRule = new GridFormatRule();
                        FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                        FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                        FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                        FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                        FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                        GridFormatRule gridFormatRuleB = new GridFormatRule();
                        FormatConditionRuleIconSet formatConditionRuleIconSetB = new FormatConditionRuleIconSet();
                        FormatConditionIconSet iconSetB = formatConditionRuleIconSetB.IconSet = new FormatConditionIconSet();
                        FormatConditionIconSetIcon icon1B = new FormatConditionIconSetIcon();
                        FormatConditionIconSetIcon icon2B = new FormatConditionIconSetIcon();
                        FormatConditionIconSetIcon icon3B = new FormatConditionIconSetIcon();

                        //Choose predefined icons.
                        icon1.PredefinedName = "TrafficLights3_1.png";
                        icon2.PredefinedName = "TrafficLights3_3.png";
                        icon3.PredefinedName = "TrafficLights3_3.png";

                        icon1B.PredefinedName = "TrafficLights3_1.png";
                        icon2B.PredefinedName = "TrafficLights3_3.png";
                        icon3B.PredefinedName = "TrafficLights3_3.png";

                        //Specify the type of threshold values.
                        //iconSet.ValueType = FormatConditionValueType.Percent;
                        iconSet.ValueType = FormatConditionValueType.Number;
                        iconSetB.ValueType = FormatConditionValueType.Number;

                        //Define ranges to which icons are applied by setting threshold values.
                        icon1.Value = 3; // target range: >=75
                        icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                        icon2.Value = 2; // NO APLICA
                        icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                        icon3.Value = 1; // target range: >75
                        icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                        icon1B.Value = 3; // target range: >=75
                        icon1B.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                        icon2B.Value = 2; // NO APLICA
                        icon2B.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                        icon3B.Value = 1; // target range: >75
                        icon3B.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                        //Add icons to the icon set.
                        iconSet.Icons.Add(icon1);
                        iconSet.Icons.Add(icon3);
                        iconSet.Icons.Add(icon3);

                        iconSetB.Icons.Add(icon1B);
                        iconSetB.Icons.Add(icon3B);
                        iconSetB.Icons.Add(icon3B);

                        //Specify the rule type.
                        gridFormatRule.Rule = formatConditionRuleIconSet;
                        //Specify the column to which formatting is applied.
                        gridFormatRule.Column = gridView2.Columns["ICS"];
                        //Add the formatting rule to the GridView.
                        gridView2.FormatRules.Add(gridFormatRule);
                        gridFormatRuleB.Rule = formatConditionRuleIconSetB;
                        //Specify the column to which formatting is applied.
                        gridFormatRuleB.Column = gridView2.Columns["ICO"];
                        //Add the formatting rule to the GridView.
                        gridView2.FormatRules.Add(gridFormatRuleB);
                        //  gridView1.BestFitColumns();
                        gridView2.Columns["BONO_SEG"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "BONO_SEG", "TOTALES:");
                        gridView2.Columns["BONO_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "BONO_TOTAL", "{0:C2}");
                        gridView2.Columns["SUB_TOTAL"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SUB_TOTAL", "{0:C2}");
                        gridView2.Columns["EXCEPCION"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "EXCEPCION", "{0:C2}");
                        gridView2.Columns["MONTOSEG"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTOSEG", "{0:C2}");
                        gridView2.Columns["MONTOCOMB"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "MONTOCOMB", "{0:C2}");
                        gridView2.Columns["ASISTENCIAS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "ASISTENCIAS", "{0}");
                        gridView2.Columns["FALTAS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "FALTAS", "{0}");
                        gridView2.Columns["PERMISO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PERMISO", "{0}");
                        gridView2.Columns["DESMEDICO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "DESMEDICO", "{0}");
                        gridView2.Columns["VACACIONES"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "VACACIONES", "{0}");
                        gridView2.Columns["LICENCIA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "LICENCIA", "{0}");
                        gridView2.Columns["SUSPENSION"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "SUSPENSION", "{0}");

                        lblTotal.Text = "Total Conductores: " + Convert.ToString(DTPeriodosCerrados.Rows.Count);

                        gridView2.BestFitColumns();
                    }
                    else
                    {
                        grvBonoConductores.DataSource = null;
                    }
                }
            }
            catch (Exception)
            {                
               // throw;
            }
                    
        }

        private void grvBonoConductores_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (AccesoModificar == 1)
                {
                    int[] filass = gridView2.GetSelectedRows();
                    string datoseleccionado = gridView2.GetFocusedValue().ToString();

                    for (int i = 0; i < filass.Length; i++)
                    {
                        int IdPersonaDetalle = Convert.ToInt32(gridView2.GetRowCellValue(filass[i], "IDCONDUCTOR").ToString());
                        string ConductorDetalle = gridView2.GetRowCellValue(filass[i], "CONDUCTOR").ToString();
                        string Observacion = gridView2.GetRowCellValue(filass[i], "MOTIVOEXCEPCION").ToString();
                        string Operacion = gridView2.GetRowCellValue(filass[i], "OPERACION").ToString();
                        string FechaInicioDetalle = dtpFechaApertura.Value.ToShortDateString();
                        string FechaFinDetalle = dtpFechaCierre.Value.ToShortDateString();
                        decimal montoexce = Convert.ToDecimal(gridView2.GetRowCellValue(filass[i], "EXCEPCION").ToString());

                        string periodo = dtpFechaCierre.Text.Substring(6, 4) + "" + dtpFechaCierre.Text.Substring(3, 2);

                        if (montoexce > 0 || montoexce < 0)
                        {
                            frmBonoConductoresExcepciones frmDetalle = new frmBonoConductoresExcepciones();
                            frmDetalle.idConductor = IdPersonaDetalle;
                            frmDetalle.conductor = ConductorDetalle;
                            frmDetalle.periodo = periodo;
                            frmDetalle.Opcion = 0;
                            frmDetalle.Operacion = Operacion;
                            frmDetalle.radioButton1.Enabled = false;
                            frmDetalle.radioButton2.Enabled = false;
                            frmDetalle.txtMonto.Enabled = false;
                            frmDetalle.txtMotivo.Enabled = false;
                            frmDetalle.btnEliminar.Enabled = false;
                            frmDetalle.button1.Enabled = false;
                            frmDetalle.Show();
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpFechaApertura.Value.Day.ToString() != "23")
                {
                    MessageBox.Show("Fecha de Apertura incorrecta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (dtpFechaCierre.Value.Day.ToString() != "22")
                {
                    MessageBox.Show("Fecha de Cierre Incorrecta", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

             
                if (MessageBox.Show("Al reabir el periodo de bonos, se eliminará los registros subidos de bono seguridad y se reprocesarán las asistencias , combustible. ¿Desea abrir el periodo?", "ARBRI PERIODO BONOS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    if (clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_BonoConductores_ReabrirAsistencias(dtpFechaApertura.Text,dtpFechaCierre.Text))
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex )
            {
                
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
