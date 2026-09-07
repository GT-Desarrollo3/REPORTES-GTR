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
using System.Xml;
using System.IO;
using ReportesTranspesa.Sistema;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using System.Diagnostics;
using ReportesTranspesa.Properties;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.HojaRecorrido
{
    public partial class frmListaHojaRecorrido : Form
    {
        string compania,FechaInicio,FechaFin;
        int idPersona = 0, idProceso = 0, idGrupo = 0;

        DataTable DtCompania;
        DataTable DtProcesos;
        DataTable DtGrupos;
        int AccesoRegistrar, AccesoVer, AccesoFirmar, AccesoImprimir;
        public frmListaHojaRecorrido()
        {
            InitializeComponent();
        }

        private void frmListaHojaRecorrido_Load(object sender, EventArgs e)
        {
            DataTable dtAccesos = new DataTable();
            dtAccesos = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Accesos(Utilitario.Instancia.SesionUsuario.usuario);
            if (dtAccesos.Rows.Count > 0)
            {
                for (int i = 0; i < dtAccesos.Rows.Count; i++)
                {
                    AccesoRegistrar = Convert.ToInt32(dtAccesos.Rows[i]["Registrar"].ToString());
                    AccesoVer = Convert.ToInt32(dtAccesos.Rows[i]["Ver"].ToString());
                    AccesoFirmar = Convert.ToInt32(dtAccesos.Rows[i]["Firmar"].ToString());
                    AccesoImprimir = Convert.ToInt32(dtAccesos.Rows[i]["Imprimir"].ToString());
                }
            }
            else 
            {
                MessageBox.Show("Usted no tiene acesos...");
                this.Close();
            }


            DataTable dtControl = new DataTable();

            dtControl.Clear();

            dtControl = clsRecursosHumanosBL.Instancia.GetListarDatoFiltros();

            if (dtControl == null)
            {
                MessageBox.Show("No se cargaron controles", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (dtControl.Rows.Count > 0)
                {
                    DtCompania = ConvertirXmlToDataTable(dtControl.Rows[0][0].ToString());
                    DtProcesos = ConvertirXmlToDataTable(dtControl.Rows[0][1].ToString());
                    DtGrupos = ConvertirXmlToDataTable(dtControl.Rows[0][2].ToString());                                   

                    //Llenado de Empresas
                    cbxCompania.DataSource = DtCompania;
                    cbxCompania.DisplayMember = "Compania";
                    cbxCompania.ValueMember = "Codigo";
                    cbxCompania.SelectedIndex = 0;

                    //Llenado de TiposDocumentos 
                    cbxProcesos.DataSource = DtProcesos;
                    cbxProcesos.DisplayMember = "Proceso";
                    cbxProcesos.ValueMember = "IdProceso";
                    cbxProcesos.SelectedIndex = 0;

                    //Llenado de TiposDocumentos 
                    cbxGrupos.DataSource = DtGrupos;
                    cbxGrupos.DisplayMember = "Grupo";
                    cbxGrupos.ValueMember = "IdGrupo";
                    cbxGrupos.SelectedIndex = 0;
                }
            }
        }
        private DataTable ConvertirXmlToDataTable(string pXml)
        {
            string xml = pXml;

            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(xml));

            DataTable Dt = new DataTable(Name);
            try
            {
                XmlNode NodoEstructura = doc.FirstChild.FirstChild;
                //  Table structure (columns definition) 
                foreach (XmlNode columna in NodoEstructura.ChildNodes)
                {
                    Dt.Columns.Add(columna.Name, typeof(String));
                }

                XmlNode Filas = doc.FirstChild;
                //  Data Rows 
                foreach (XmlNode Fila in Filas.ChildNodes)
                {
                    List<string> Valores = new List<string>();
                    foreach (XmlNode Columna in Fila.ChildNodes)
                    {
                        Valores.Add(Columna.InnerText);
                    }
                    Dt.Rows.Add(Valores.ToArray());
                }
            }
            catch (Exception)
            {

            }
            return Dt;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtPersona.Text.Length == 0)
            {
                idPersona = 0;
            }

            compania = cbxCompania.SelectedValue.ToString();
            FechaInicio = dtpFechaInicio.Text;
            FechaFin = dtpFechaFin.Text;
            idProceso = Convert.ToInt32(cbxProcesos.SelectedValue.ToString());
            idGrupo = Convert.ToInt32(cbxGrupos.SelectedValue.ToString());
            
            DataTable dtListarHojasRecorrdido = new DataTable();
            dtListarHojasRecorrdido = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_ListarRegistros(compania, FechaInicio, FechaFin, idPersona, idProceso, idGrupo);

            if (dtListarHojasRecorrdido.Rows.Count > 0)
            {
                dgvHojasRecorrido.DataSource = dtListarHojasRecorrdido;
                dgvHojasRecorridoView.Columns["Anio"].Visible = false;
                dgvHojasRecorridoView.Columns["CodigoHoja"].Visible = false;
                dgvHojasRecorridoView.Columns["IdProceso"].Visible = false;
                dgvHojasRecorridoView.Columns["IdGrupo"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona1"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona2"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona3"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona4"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona5"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona6"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona7"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona8"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona9"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona10"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona11"].Visible = false;
                dgvHojasRecorridoView.Columns["IdPersona12"].Visible = false;
                dgvHojasRecorridoView.Columns["IDETSADO"].Visible = false;

                dgvHojasRecorridoView.Columns["Compania"].Fixed = FixedStyle.Left;
                dgvHojasRecorridoView.Columns["Codigo"].Fixed = FixedStyle.Left;
                dgvHojasRecorridoView.Columns["Empleado"].Fixed = FixedStyle.Left;
                dgvHojasRecorridoView.Columns["Proceso"].Fixed = FixedStyle.Left;
                dgvHojasRecorridoView.Columns["Grupo"].Fixed = FixedStyle.Left;

              /*  GridFormatRule gridFormatRule = new GridFormatRule();
                FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                icon1.PredefinedName = "TrafficLights3_1.png";
                icon2.PredefinedName = "TrafficLights3_2.png";
                icon3.PredefinedName = "TrafficLights3_3.png";
                //Specify the type of threshold values.
                //iconSet.ValueType = FormatConditionValueType.Percent;
                iconSet.ValueType = FormatConditionValueType.Number;
                //Define ranges to which icons are applied by setting threshold values.
                icon1.Value = 1; // target range: >=75
                icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon2.Value = 2; // NO APLICA
                icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon3.Value = 3; // target range: >75
                icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
              
                //Add icons to the icon set.
                iconSet.Icons.Add(icon1);
                iconSet.Icons.Add(icon3);
                iconSet.Icons.Add(icon3);                
               // dgvHojasRecorridoView.Columns["Estado"].AppearanceCell.BackColor = Color.LightSalmon;

                //Specify the rule type.
                gridFormatRule.Rule = formatConditionRuleIconSet;
                //Specify the column to which formatting is applied.
                gridFormatRule.Column = dgvHojasRecorridoView.Columns["Estado"];

                //Add the formatting rule to the GridView.
                dgvHojasRecorridoView.FormatRules.Add(gridFormatRule);  */            
                dgvHojasRecorridoView.BestFitColumns();

            }
            else
            {
                dgvHojasRecorrido.DataSource = null;
            }
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            if (AccesoRegistrar == 1)
            {
                frmHojaRecorrido frmHR = new frmHojaRecorrido();
                frmHR.chbFirma1.Visible = false;
                frmHR.cbhFirma2.Visible = false;
                frmHR.cbhFirma3.Visible = false;
                frmHR.cbhFirma4.Visible = false;
                frmHR.cbhFirma5.Visible = false;
                frmHR.cbhFirma6.Visible = false;
                frmHR.cbhFirma7.Visible = false;
                frmHR.cbhFirma8.Visible = false;
                frmHR.cbhFirma9.Visible = false;
                frmHR.cbhFirma10.Visible = false;
                frmHR.cbhFirma11.Visible = false;
                frmHR.cbhFirma12.Visible = false;
                frmHR.AccesoImprimir = AccesoImprimir;
                frmHR.AccesoFirmar = AccesoFirmar;
                frmHR.TipoRegistro = 1;
                frmHR.txtObs1.Visible = true;
                frmHR.DatosEDITAR(cbxCompania.SelectedValue.ToString() + "00", "0", cbxProcesos.SelectedValue.ToString(), cbxProcesos.Text, cbxGrupos.SelectedValue.ToString(), cbxGrupos.Text, "0", "", "0");
                frmHR.ShowDialog();
            }

        }

        private void txtPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {             
                    clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtPersona.Text), true, false, false);

                    lstPersona.Columns[0].Width = 0;
                    lstPersona.Columns[1].Width = 206;
                    lstPersona.Columns[2].Width = 110;

                    lstPersona.Size = new System.Drawing.Size(350, 111);

                    lstPersona.BringToFront();
                    lstPersona.Visible = true;
                    lstPersona.Focus();                                          
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtPersona.Focus();
              
            }
        }       

        private void lstPersona_Enter_1(object sender, EventArgs e)
        {
            if (!lstPersona.Items.Count.Equals(0))
            {
                lstPersona.Items[0].Selected = true;
            }
        }

        private void lstPersona_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lstPersona.SelectedItems[0];

                idPersona = Convert.ToInt32(ItemActual.Text);
                txtPersona.Text = ItemActual.SubItems[1].Text;
                lstPersona.Visible = false;
                btnBuscar.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtPersona.Focus();
            }
        }

        private void lstPersona_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;

            ItemActual = lstPersona.SelectedItems[0];

            idPersona = Convert.ToInt32(ItemActual.Text);
            txtPersona.Text = ItemActual.SubItems[1].Text;
            lstPersona.Visible = false;
            btnBuscar.Focus();
        }

        private void Formatos_Click(object sender, EventArgs e)
        {
            if (AccesoRegistrar == 1)
            {
                frmHojaRecorrido frmHRecorrido = new frmHojaRecorrido();
                frmHRecorrido.TipoRegistro = 0;
                frmHRecorrido.button1.Visible = false;
                frmHRecorrido.button8.Visible = false;
                frmHRecorrido.button3.Visible = false;
                frmHRecorrido.btnGuardarFormato.Visible = true;
                frmHRecorrido.txtObs1.Visible = true;
                frmHRecorrido.txtObs2.Visible = true;
                frmHRecorrido.txtObs3.Visible = true;
                frmHRecorrido.txtObs4.Visible = true;
                frmHRecorrido.txtObs5.Visible = true;
                frmHRecorrido.txtObs6.Visible = true;
                frmHRecorrido.txtObs7.Visible = true;
                frmHRecorrido.txtObs8.Visible = true;
                frmHRecorrido.txtObs9.Visible = true;
                frmHRecorrido.txtObs10.Visible = true;
                frmHRecorrido.txtObs11.Visible = true;
                frmHRecorrido.txtObs12.Visible = true;
                frmHRecorrido.txtPersona.Visible = false;
                frmHRecorrido.dtpFechaIngreso.Visible = false;
                frmHRecorrido.dtpFechaSalida.Visible = false;
                frmHRecorrido.lblFechaIngreso.Visible = false;
                frmHRecorrido.label13.Visible = false;
                frmHRecorrido.label15.Visible = false;
                frmHRecorrido.tscboAreas1.Visible = true;
                frmHRecorrido.tscboAreas2.Visible = true;
                frmHRecorrido.tscboAreas3.Visible = true;
                frmHRecorrido.tscboAreas4.Visible = true;
                frmHRecorrido.tscboAreas5.Visible = true;
                frmHRecorrido.tscboAreas6.Visible = true;
                frmHRecorrido.tscboAreas7.Visible = true;
                frmHRecorrido.tscboAreas8.Visible = true;
                frmHRecorrido.tscboAreas9.Visible = true;
                frmHRecorrido.tscboAreas10.Visible = true;
                frmHRecorrido.tscboAreas11.Visible = true;
                frmHRecorrido.tscboAreas12.Visible = true;
                frmHRecorrido.chbFirma1.Visible = false;
                frmHRecorrido.cbhFirma2.Visible = false;
                frmHRecorrido.cbhFirma3.Visible = false;
                frmHRecorrido.cbhFirma4.Visible = false;
                frmHRecorrido.cbhFirma5.Visible = false;
                frmHRecorrido.cbhFirma6.Visible = false;
                frmHRecorrido.cbhFirma7.Visible = false;
                frmHRecorrido.cbhFirma8.Visible = false;
                frmHRecorrido.cbhFirma9.Visible = false;
                frmHRecorrido.cbhFirma10.Visible = false;
                frmHRecorrido.cbhFirma11.Visible = false;
                frmHRecorrido.cbhFirma12.Visible = false;
                frmHRecorrido.txtCodigo.Visible = false;
                frmHRecorrido.ShowDialog();
            }
        }

        private void dgvHojasRecorrido_DoubleClick(object sender, EventArgs e)
        {           
            try
            {
                int[] filass = dgvHojasRecorridoView.GetSelectedRows();

                for (int i = 0; i < filass.Length; i++)
                {                  
                    string Compania = dgvHojasRecorridoView.GetRowCellValue(filass[i], "Compania").ToString();
                    string CodigoHoja = dgvHojasRecorridoView.GetRowCellValue(filass[i], "CodigoHoja").ToString();
                    string IdProceso = dgvHojasRecorridoView.GetRowCellValue(filass[i], "IdProceso").ToString();
                    string IdGrupo = dgvHojasRecorridoView.GetRowCellValue(filass[i], "IdGrupo").ToString();
                    string Procesos = dgvHojasRecorridoView.GetRowCellValue(filass[i], "IdProceso").ToString();
                    string Grupos = dgvHojasRecorridoView.GetRowCellValue(filass[i], "IdGrupo").ToString();
                    string Anio = dgvHojasRecorridoView.GetRowCellValue(filass[i], "Anio").ToString();
                    string PersonaNombre = dgvHojasRecorridoView.GetRowCellValue(filass[i], "Empleado").ToString();
                    string IdPersona = dgvHojasRecorridoView.GetRowCellValue(filass[i], "IdPersona").ToString();

                    // OTselec = gvrUnidades.GetRowCellValue(filas[i], "ID").ToString();
                    frmHojaRecorrido frmHojaRecorridoEditar =  new frmHojaRecorrido();
                    frmHojaRecorridoEditar.Text = "Edición de Hoja Recorrido";
                    frmHojaRecorridoEditar.txtObs1.Visible = true;                    
                    frmHojaRecorridoEditar.txtObs2.Visible = true;
                    frmHojaRecorridoEditar.txtObs3.Visible = true;
                    frmHojaRecorridoEditar.txtObs4.Visible = true;
                    frmHojaRecorridoEditar.txtObs5.Visible = true;
                    frmHojaRecorridoEditar.txtObs6.Visible = true;
                    frmHojaRecorridoEditar.txtObs7.Visible = true;
                    frmHojaRecorridoEditar.txtObs8.Visible = true;
                    frmHojaRecorridoEditar.txtObs9.Visible = true;
                    frmHojaRecorridoEditar.txtObs10.Visible = true;
                    frmHojaRecorridoEditar.txtObs11.Visible = true;
                    frmHojaRecorridoEditar.txtObs12.Visible = true;
                    frmHojaRecorridoEditar.txtObs1.ReadOnly = false;
                    frmHojaRecorridoEditar.txtObs2.ReadOnly = true;
                    frmHojaRecorridoEditar.txtObs3.ReadOnly = true;
                    frmHojaRecorridoEditar.txtObs4.ReadOnly = true;
                    frmHojaRecorridoEditar.txtObs5.ReadOnly = true;
                    frmHojaRecorridoEditar.txtObs6.ReadOnly = true;
                    frmHojaRecorridoEditar.txtObs7.ReadOnly = true;
                    frmHojaRecorridoEditar.txtObs8.ReadOnly = true;
                    frmHojaRecorridoEditar.txtObs9.ReadOnly = true;
                    frmHojaRecorridoEditar.txtObs10.ReadOnly = true;
                    frmHojaRecorridoEditar.txtObs11.ReadOnly = true;
                    frmHojaRecorridoEditar.txtObs12.ReadOnly = true;
                    frmHojaRecorridoEditar.chbFirma1.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma2.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma3.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma4.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma5.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma6.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma7.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma8.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma9.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma10.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma11.Enabled = false;
                    frmHojaRecorridoEditar.cbhFirma12.Enabled = false;
                    frmHojaRecorridoEditar.cboGrupo.Enabled = false;
                    frmHojaRecorridoEditar.cboProcesos.Enabled = false;
                    frmHojaRecorridoEditar.AccesoImprimir = AccesoImprimir;
                    frmHojaRecorridoEditar.AccesoFirmar = AccesoFirmar;
                    frmHojaRecorridoEditar.TipoRegistro = 2;
                    frmHojaRecorridoEditar.button3.Visible = false;
                    frmHojaRecorridoEditar.DatosEDITAR(Compania, CodigoHoja, IdProceso, Procesos, IdGrupo, Grupos, Anio, PersonaNombre,IdPersona);
                    frmHojaRecorridoEditar.ShowDialog();                   
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgvHojasRecorridoView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
             GridView View = sender as GridView;
             if (e.RowHandle >= 0)
             {
                 string ValorEstado = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Estado"]);
                 if (ValorEstado == "NO VISADO")
                 {
                     /*e.Appearance.BackColor = Color.FromArgb(100, Color.Red);
                     e.Appearance.BackColor2 = Color.White;*/
                     dgvHojasRecorridoView.Columns["Estado"].AppearanceCell.BackColor = Color.FromArgb(100, Color.Red);// Color.LightSalmon;
                     dgvHojasRecorridoView.Columns["Estado"].AppearanceCell.BackColor2 = Color.White;// Color.LightSalmon;
                 }
                 if (ValorEstado == "VISADO PARCIAL")
                 {
                    /* e.RowHandle["Estado"].Appearance.BackColor = Color.FromArgb(150, Color.Yellow);
                     e.Appearance.BackColor2 = Color.White;*/
                     dgvHojasRecorridoView.Columns["Estado"].AppearanceCell.BackColor = Color.FromArgb(150, Color.Yellow);
                    dgvHojasRecorridoView.Columns["Estado"].AppearanceCell.BackColor2 = Color.White;
                 }
                 if (ValorEstado == "VISADO")
                 {
                     /*e.Appearance.BackColor = Color.YellowGreen;
                     e.Appearance.BackColor2 = Color.YellowGreen;*/
                     dgvHojasRecorridoView.Columns["Estado"].AppearanceCell.BackColor = Color.YellowGreen;
                    dgvHojasRecorridoView.Columns["Estado"].AppearanceCell.BackColor2 = Color.YellowGreen;
                 }
             }
        }

        private void dgvHojasRecorridoView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
          /*  GridView view = sender as GridView;
            if (e.RowHandle == view.FocusedRowHandle) return;
            if (e.Column.FieldName != "UnitPrice") return;

            // Fill a cell's background if its value is greater than 30.
            if (Convert.ToInt32(e.CellValue) > 30)
                e.Appearance.BackColor = Color.FromArgb(60, Color.Salmon);         */

           /* if (e.RowHandle != GridControl.NewItemRowHandle && e.Column.FieldName == "Estado"
         && e.CellValue.ToString() == "VISADO PARCIAL")
            {
               // e.RepositoryItem = repositoryItemTextEdit2;
                dgvHojasRecorridoView.Columns["Estado"].AppearanceCell.Image = Resources.liberar;
            }*/
        }

        private void reistrarFirmaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AccesoFirmar == 1)
            {
                frmMaestroFirmas frmHojaRecorridoEditar = new frmMaestroFirmas();
                frmHojaRecorridoEditar.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvHojasRecorridoView.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte Hojas de Recorrido " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dgvHojasRecorridoView.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
