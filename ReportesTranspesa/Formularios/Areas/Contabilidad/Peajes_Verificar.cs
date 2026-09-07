using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Negocio;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Windows.Forms;
using System.IO;
using Comun;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{

    public partial class Peajes_Verificar : Form
    {
        string FechaBusqueda;
        string UsuarioBusqueda;
        int saveRow = 0;
        int saveRow2 = 0;
        DataTable DtDetalle;
        string PlacaMantenerRefresh;
        DataTable dtRespuesta;
        DataTable dtRespuesta1;
        DataTable dtRespuesta2;
        DataTable dtRespuesta4;
        DataTable df;
        int estadoTabla = 0; // 0 es primera carga ,  1 es principal , 2 es filtro placa
        int estadoTablaViaje = 0;
        List<int> seleccionarFilas = new List<int>();
        List<int> seleccionarPeaje = new List<int>();
        public Peajes_Verificar()
        {
            InitializeComponent();
        }


        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                //DE ESTA MANERA FILTRAMOS TODOS LOS ARCHIVOS EXCEL EN EL NAVEGADOR DE ARCHIVOS
                Filter = "Excel | *.xls;*.xlsx;",

                //AQUÍ INDICAMOS QUE NOMBRE TENDRÁ EL NAVEGADOR DE ARCHIVOS COMO TITULO
                Title = "Seleccionar Archivo"
            };

            //EN CASO DE SELECCIONAR EL ARCHIVO, ENTONCES PROCEDEMOS A ABRIR EL ARCHIVO CORRESPONDIENTE
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtArchivo.Text = openFileDialog.FileName.ToString();
                dataGridView1.DataSource = ImportarDatos(openFileDialog.FileName);
                splitContainer3.Panel2Collapsed = true;
                splitContainer3.Panel1Collapsed = false;
                splitContainer4.Panel2Collapsed = true;
                splitContainer4.Panel1Collapsed = false;
            }

        }

        DataView ImportarDatos(string nombrearchivo) //COMO PARAMETROS OBTENEMOS EL NOMBRE DEL ARCHIVO A IMPORTAR
        {
            //UTILIZAMOS 12.0 DEPENDIENDO DE LA VERSION DEL EXCEL, EN CASO DE QUE LA VERSIÓN QUE TIENES ES INFERIOR AL DEL 2013, CAMBIAR A EXCEL 8.0 Y EN VEZ DE
            //ACE.OLEDB.12.0 UTILIZAR LO SIGUIENTE (Jet.Oledb.4.0)
            string conexion = string.Format("Provider = Microsoft.ACE.OLEDB.12.0; Data Source = {0}; Extended Properties = 'Excel 12.0;'", nombrearchivo);

            OleDbConnection conector = new OleDbConnection(conexion);

            conector.Open();

            //DEPENDIENDO DEL NOMBRE QUE TIENE LA PESTAÑA EN TU ARCHIVO EXCEL COLOCAR DENTRO DE LOS []
            OleDbCommand consulta = new OleDbCommand("select * from [Hoja1$]", conector);

            OleDbDataAdapter adaptador = new OleDbDataAdapter
            {
                SelectCommand = consulta
            };

            DataSet ds = new DataSet();

            adaptador.Fill(ds);

            conector.Close();

            return ds.Tables[0].DefaultView;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //PASA TODO EL DETALLE DE UNA TABLA A XML //////////////////////////////////  JOEL ROJAS
            //--------------------------------------------------------------------------------------
            if (txtArchivo.Text.Length == 0)
            {
                MessageBox.Show("No ha seleccionado ningun archivo a importar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string cadena = "";
            System.Console.WriteLine(cadena);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (dataGridView1.Rows.Count > 0)
            {

                int rowscount = dataGridView1.Rows.Count;
                int columnscount = dataGridView1.Columns.Count;

                cadena += "<r>";

                for (int i = 0; i < rowscount; i++)
                {
                    cadena += "<d";

                    for (int j = 0; j < columnscount; j++)
                    {
                        string cabecera = dataGridView1.Columns[j].HeaderText;
                        string valor = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        cadena += " " + cabecera.ToString();
                        cadena += "=\"" + valor.ToString();
                        cadena += "\"";
                    }

                    cadena += " />";
                }

                cadena += "</r>";

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsContabilidadBL.Instancia.GetPeajes_ImportarDataExcel01(cadena, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    buscarImportados();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                //PASA TODO EL DETALLE DE UNA TABLA A XML  ////////////////////////////////// JOEL ROJAS
                //--------------------------------------------------------------------------------------

                //System.Text.StringBuilder sb = new System.Text.StringBuilder();
                //sb.Append("<r>");

                //for (int fila = 0; fila < dataGridView1.Rows.Count - 1; fila++)
                //{
                //    sb.Append("<d");

                //    for (int col = 0; col < dataGridView1.Rows[fila].Cells.Count; col++)
                //    {
                //        string cabecera = dataGridView1.Rows[0].Cells[col].Value.ToString();
                //        string valor = dataGridView1.Rows[fila].Cells[col].Value.ToString();

                //        sb.Append(" " + cabecera.ToString());
                //        sb.Append("=\"" + valor.ToString());
                //        sb.Append("\"");
                //    }

                //    sb.Append(" />");
                //}

                //sb.Append("</r>");

                //MessageBox.Show(sb.ToString(), "Selected Cells");

            }
            else
            {
                return;
            }


        }

        private void Peajes_Verificar_Load(object sender, EventArgs e)
        {
            splitContainer3.Panel2Collapsed = true;
            splitContainer3.Panel1Collapsed = false;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            buscarImportados();
        }

        private void buscarImportados()
        {
            while (dataGridView1.RowCount > 1)
            {

                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);

            }

            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsContabilidadBL.Instancia.GetPeajes_Ver(1, "", "", "");
            dataGridView1.DataSource = dtRespuesta;
            splitContainer5.Panel1Collapsed = false;
            splitContainer5.Panel2Collapsed = true;
            splitContainer3.Panel1Collapsed = false;
            splitContainer3.Panel2Collapsed = true;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns["Usuario"] != null)
            {
                if (e.RowIndex != -1)
                {
                    FechaBusqueda = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    UsuarioBusqueda = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

                    dtRespuesta1 = new DataTable();
                    dtRespuesta1 = clsContabilidadBL.Instancia.GetPeajes_Ver(2, FechaBusqueda, UsuarioBusqueda, "");
                    if (dtRespuesta1.Rows.Count > 0)
                    {
                        txtFechaIni.Text = dtRespuesta1.Rows[0]["FechaIni"].ToString();
                        txtFechaFin.Text = dtRespuesta1.Rows[0]["FechaFin"].ToString();
                    }

                    dtRespuesta2 = new DataTable();
                    dtRespuesta2 = clsContabilidadBL.Instancia.GetPeajes_Ver(3, FechaBusqueda, UsuarioBusqueda, "");
                    foreach (DataColumn col in dtRespuesta2.Columns)
                    {
                        col.ReadOnly = false;

                    }
                    if (dtRespuesta2.Rows.Count > 0)
                    {
                        //No visibles
                        estadoTabla = 1;
                        seleccionarPeaje.Clear();
                        dataGridView3.DataSource = dtRespuesta2;
                        dataGridView3.Columns["IdPeaje"].Visible = false;
                        dataGridView3.Columns["Categoria"].Visible = false;
                        dataGridView3.Columns["TipoDoc"].Visible = false;
                        dataGridView3.Columns["Cuenta"].Visible = false;
                        dataGridView3.Columns["Usuario"].Visible = false;
                        dataGridView3.Columns["FechaHoraImporta"].Visible = false;
                        //visibles
                        dataGridView3.Columns["FechaHora"].Width = 100;
                        dataGridView3.Columns["Peaje"].Width = 70;
                        dataGridView3.Columns["Placa"].Width = 50;
                        dataGridView3.Columns["Via"].Width = 80;
                        dataGridView3.Columns["Importe"].Width = 50;
                        dataGridView3.Columns["SerieDoc"].Width = 40;
                        dataGridView3.Columns["NumeroDoc"].Width = 70;
                        dataGridView3.Columns["VIAJE"].Width = 60;
                        dataGridView3.Columns["RUTA"].Width = 200;

                        splitContainer5.Panel2Collapsed = false;
                        DtDetalle = dtRespuesta2;
                        foreach (DataColumn col in DtDetalle.Columns)
                        {
                            col.ReadOnly = false;

                        }
                        textBox1.Text = "";
                    }
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            splitContainer5.Panel1Collapsed = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            splitContainer5.Panel1Collapsed = false;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (estadoTabla == 1)
            {
                if (Convert.ToBoolean(dtRespuesta2.Rows[e.RowIndex]["Check"]) == true)
                {
                    dtRespuesta2.Rows[e.RowIndex]["Check"] = false;
                    seleccionarPeaje.Remove(e.RowIndex);
                    dataGridView3.Rows[e.RowIndex].Selected = false;
                }
                else
                {
                    dtRespuesta2.Rows[e.RowIndex]["Check"] = true;
                    seleccionarPeaje.Add(e.RowIndex);
                    dataGridView3.Rows[e.RowIndex].Selected = true;
                }
                dtRespuesta2.AcceptChanges();
            }



            if (estadoTabla == 2)
            {
                if (Convert.ToBoolean(DtDetalle.Rows[e.RowIndex]["Check"]) == true)
                {
                    DtDetalle.Rows[e.RowIndex]["Check"] = false;
                    seleccionarPeaje.Remove(e.RowIndex);
                    dataGridView3.Rows[e.RowIndex].Selected = false;
                }
                else
                {
                    DtDetalle.Rows[e.RowIndex]["Check"] = true;
                    seleccionarPeaje.Add(e.RowIndex);
                    dataGridView3.Rows[e.RowIndex].Selected = true;
                }
                DtDetalle.AcceptChanges();
            }


            if (estadoTabla == 3)
            {
                if (Convert.ToBoolean(df.Rows[e.RowIndex]["Check"]) == true)
                {
                    df.Rows[e.RowIndex]["Check"] = false;
                    seleccionarPeaje.Remove(e.RowIndex);
                    dataGridView3.Rows[e.RowIndex].Selected = false;
                }
                else
                {
                    df.Rows[e.RowIndex]["Check"] = true;
                    seleccionarPeaje.Add(e.RowIndex);
                    dataGridView3.Rows[e.RowIndex].Selected = true;
                }
                df.AcceptChanges();
            }

            /*
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                dataGridView3.Rows[i].Selected = false;
            }
            foreach (int i in seleccionarPeaje)
            {
                dataGridView3.Rows[i].Selected = true;
            }

            dataGridView3.Update();
            */
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.Columns["Placa"] != null)
            {
                if (e.RowIndex != -1)
                {

                    string IdPeaje = dataGridView3.Rows[e.RowIndex].Cells["IDPeaje"].Value.ToString();
                    string Placa = dataGridView3.Rows[e.RowIndex].Cells["Placa"].Value.ToString();
                    string Peaje = dataGridView3.Rows[e.RowIndex].Cells["Peaje"].Value.ToString();

                    toolStripStatusLabel1.Text = "Seleccionado: ID " + IdPeaje + " Placa: " + Placa + " Peaje: " + Peaje;

                    txtIdPeaje.Text = IdPeaje;

                    //Captura de la Posicion en DataGridView                  
                    if (dataGridView3.Rows.Count > 0 && dataGridView3.FirstDisplayedCell != null)
                        saveRow = dataGridView3.FirstDisplayedCell.RowIndex;
                    //Captura de la Posicion en DataGridView

                    if (txtPlaca.Text != Placa)
                    {
                        txtPlaca.Text = Placa;

                        dtRespuesta = new DataTable();
                        dtRespuesta = clsContabilidadBL.Instancia.GetPeajes_Ver(4, Placa, txtFechaIni.Text, txtFechaFin.Text);
                    }
                    

                    foreach (DataColumn col in dtRespuesta.Columns)
                    {
                        col.ReadOnly = false;

                    }
                    dataGridView2.DataSource = null;
                    if (dtRespuesta.Rows.Count > 0)
                    {
                        seleccionarFilas.Clear();
                        estadoTablaViaje = 1;
                        dataGridView2.DataSource = dtRespuesta;
                        dataGridView2.Columns["codigo"].Width = 60;
                        dataGridView2.Columns["ruta"].Width = 200;
                        dataGridView2.Columns["Placa"].Width = 60;

                        if (PlacaMantenerRefresh == Placa)
                        {
                            if (saveRow2 != 0 && saveRow2 < dataGridView2.Rows.Count)
                                dataGridView2.FirstDisplayedScrollingRowIndex = saveRow2;
                        }

                    }
                }

            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (estadoTablaViaje == 1)
            {


                if (Convert.ToBoolean(dtRespuesta.Rows[e.RowIndex]["Check"]) == true)
                {
                    dtRespuesta.Rows[e.RowIndex]["Check"] = false;
                    seleccionarFilas.Remove(e.RowIndex);
                    dataGridView2.Rows[e.RowIndex].Selected = false;

                }
                else
                {
                    dtRespuesta.Rows[e.RowIndex]["Check"] = true;
                    seleccionarFilas.Add(e.RowIndex);

                }


                dtRespuesta.AcceptChanges();
            }

            if (estadoTablaViaje == 2)
            {
                if (Convert.ToBoolean(dtRespuesta4.Rows[e.RowIndex]["Check"]) == true)
                {
                    dtRespuesta4.Rows[e.RowIndex]["Check"] = false;
                    seleccionarFilas.Remove(e.RowIndex);
                    dataGridView2.Rows[e.RowIndex].Selected = false;

                }
                else
                {
                    dtRespuesta4.Rows[e.RowIndex]["Check"] = true;
                    seleccionarFilas.Add(e.RowIndex);
                }
                dtRespuesta4.AcceptChanges();
            }

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                dataGridView2.Rows[i].Selected = false;
            }
            foreach (int i in seleccionarFilas)
            {
                dataGridView2.Rows[i].Selected = true;
            }

            dataGridView2.Update();

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.Columns["Codigo"] != null)
            {
                if (e.RowIndex != -1)
                {
                    string CodigoViaje = dataGridView2.Rows[e.RowIndex].Cells["CODIGO"].Value.ToString();
                    string RutaViaje = dataGridView2.Rows[e.RowIndex].Cells["RUTA"].Value.ToString();

                    toolStripStatusLabel2.Text = "Seleccionado: Codigo_Viaje " + CodigoViaje + " Ruta: " + RutaViaje;

                    txtCodigoViaje.Text = CodigoViaje;
                    txtRutaViaje.Text = RutaViaje;

                    //Captura de la Posicion en DataGridView                  
                    if (dataGridView2.Rows.Count > 0 && dataGridView2.FirstDisplayedCell != null)
                        saveRow2 = dataGridView2.FirstDisplayedCell.RowIndex;
                    //Captura de la Posicion en DataGridView

                }
            }
        }

        private void btnVincular_Click(object sender, EventArgs e)
        {

            if (txtCodigoViaje.Text.Length == 0)
            {
                MessageBox.Show("Primero, debe seleccionar uno de los viajes. No puede seguir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtResultado = new DataTable();
            DataTable dtSeleccionadosPeaje = new DataTable();
            DataTable dtSeleccionadosViaje = new DataTable();
            string xmlPeaje= string.Empty;
            string xmlViaje = string.Empty;
            string xmlPeajeSintilde = string.Empty;
            string xmlViajeSintilde = string.Empty;
            string Respuesta;

            if (estadoTabla == 1)
            {
                dtSeleccionadosPeaje = (from item in dtRespuesta2.Rows.Cast<DataRow>()
                                        where item.Field<bool>("Check") == true
                                        select item).CopyToDataTable();
            }

            if (estadoTabla == 2)
            {
                dtSeleccionadosPeaje = (from item in DtDetalle.Rows.Cast<DataRow>()
                                        where item.Field<bool>("Check") == true
                                        select item).CopyToDataTable();
            }
            if (estadoTabla == 3)
            {
                dtSeleccionadosPeaje = (from item in df.Rows.Cast<DataRow>()
                                        where item.Field<bool>("Check") == true
                                        select item).CopyToDataTable();
            }


            if (estadoTablaViaje == 1)
            {
                dtSeleccionadosViaje = (from item in dtRespuesta.Rows.Cast<DataRow>()
                                        where item.Field<bool>("Check") == true
                                        select item).CopyToDataTable();
            }

            if (estadoTablaViaje == 2)
            {
                dtSeleccionadosViaje = (from item in dtRespuesta4.Rows.Cast<DataRow>()
                                        where item.Field<bool>("Check") == true
                                        select item).CopyToDataTable();
            }

            if (dtSeleccionadosPeaje.Rows.Count > 0)
            {

                xmlPeaje = Utilitario.Instancia.DatatableToXml(dtSeleccionadosPeaje);
                xmlPeajeSintilde = Regex.Replace(xmlPeaje.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9""=<>?'/:.Ññ ]+", "");
            }
            if (dtSeleccionadosViaje.Rows.Count > 0)
            {
                xmlViaje = Utilitario.Instancia.DatatableToXml(dtSeleccionadosViaje);
                xmlViajeSintilde = Regex.Replace(xmlViaje.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9""=<>?'/:.Ññ ]+", "");
            }

            dtResultado = clsContabilidadBL.Instancia.GetPeajes_Vincular(xmlPeajeSintilde, xmlViajeSintilde, Convert.ToInt32(txtIdPeaje.Text), txtCodigoViaje.Text, txtRutaViaje.Text, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtResultado.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                seleccionarPeaje.Clear();
                PlacaMantenerRefresh = txtPlaca.Text;
                MessageBox.Show(Respuesta, "Vinculación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BuscarPeajes();
                DataGridViewCellEventArgs Argumentos = new DataGridViewCellEventArgs(0, 0);
                if (dataGridView3.Rows.Count > 0) { dataGridView3_CellClick(dataGridView3, Argumentos); }
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Necesitará tener permisos para esta acción", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (txtIdPeaje.Text.Length == 0)
            {
                MessageBox.Show("Primero, debe seleccionar uno de los Peajes. No puede seguir", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsContabilidadBL.Instancia.GetPeajes_Desvincular(Convert.ToInt32(txtIdPeaje.Text), Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Desvinculación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                BuscarPeajes();

                if (saveRow != 0 && saveRow < dataGridView3.Rows.Count)
                    dataGridView3.FirstDisplayedScrollingRowIndex = saveRow;
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void BuscarPeajes()
        {
            DataTable dtRespuesta2 = new DataTable();
            dtRespuesta2 = clsContabilidadBL.Instancia.GetPeajes_Ver(3, FechaBusqueda, UsuarioBusqueda, "");

            if (dtRespuesta2.Rows.Count > 0)
            {
                //No visibles
                dataGridView3.DataSource = dtRespuesta2;
                dataGridView3.Columns["IdPeaje"].Visible = false;
                dataGridView3.Columns["Categoria"].Visible = false;
                dataGridView3.Columns["TipoDoc"].Visible = false;
                dataGridView3.Columns["Cuenta"].Visible = false;
                dataGridView3.Columns["Usuario"].Visible = false;
                dataGridView3.Columns["FechaHoraImporta"].Visible = false;
                //visibles
                dataGridView3.Columns["FechaHora"].Width = 100;
                dataGridView3.Columns["Peaje"].Width = 80;
                dataGridView3.Columns["Placa"].Width = 60;
                dataGridView3.Columns["Via"].Width = 90;
                dataGridView3.Columns["Importe"].Width = 50;
                dataGridView3.Columns["SerieDoc"].Width = 40;
                dataGridView3.Columns["NumeroDoc"].Width = 70;

                DtDetalle = dtRespuesta2; //Clonamos para Filtros por Placa.
                foreach (DataColumn col in DtDetalle.Columns) { col.ReadOnly = false; }

                splitContainer5.Panel2Collapsed = false;

                if (textBox1.Text.Length > 0) { filtroPorPlaca(); }
                else
                {
                    if (saveRow != 0 && saveRow < dataGridView3.Rows.Count)
                        dataGridView3.FirstDisplayedScrollingRowIndex = saveRow;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        { verReporte(1, dtpDesde.Text.ToString(), dtpHasta.Text.ToString()); }

        private void verReporte(int op, string Dato1, string Dato2)
        {
            CrystalReportViewer rv = new CrystalReportViewer();

            string FolderDegug = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FolderForm = FolderDegug + @"\Formularios\Areas\Contabilidad\Reportes\";
            string rpt = "crvPeajes_Reporte.rpt";

            string reportPath = Path.Combine(FolderForm, rpt);

            ReportDocument r = new ReportDocument();

            r.Load(reportPath);

            String user, pass, host, catalog;
            user = "sa"; //clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Usuario();
            pass = "Ma5@iñb0t$"; //clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Clave();
            host = "190.116.64.132,29692"; //clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_Servidor();
            catalog = "spring"; //clsConexionCrystalReportBL.Instancia.clsConexionCrystalReportBL_BaseDatos();
            r.DataSourceConnections[0].SetConnection(host, catalog, user, pass);
            r.SetParameterValue("@Opcion", op);
            r.SetParameterValue("@DatoFiltro1", Dato1);
            r.SetParameterValue("@DatoFiltro2", Dato2);
            crvReporte.ReportSource = r;

            splitContainer3.Panel2Collapsed = false;
            splitContainer3.Panel1Collapsed = true;

            this.WindowState = FormWindowState.Maximized;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            verReporte(2, txtSerieDoc.Text, txtNroDoc.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {

                estadoTabla = 2;
                seleccionarPeaje.Clear();
                dataGridView3.DataSource = DtDetalle;
                return;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                filtroPorPlaca();
            }
        }

        void filtroPorPlaca()
        {
            // Si no hay filtro, restauramos el grid original y salimos
            if (textBox1.Text == "")
            {
                estadoTabla = 2;
                dataGridView3.DataSource = DtDetalle;
                return;
            }

            string busqueda = textBox1.Text.Replace("-", "").Replace(".", "").ToUpper();

            try
            {
                var filas = DtDetalle.Rows.Cast<DataRow>().Where(item =>
                {
                    string codigo = Convert.ToString(item["Placa"]);

                    if (codigo == null) codigo = "";
                    
                    codigo = codigo.Replace("-", "").Replace(".", "").ToUpper();
                    
                    return codigo.Contains(busqueda);
                })

                .OrderBy(item =>
                {
                    object valorFecha = item["FechaHora"];

                    if (valorFecha == null || valorFecha == DBNull.Value) return DateTime.MaxValue;

                    if (valorFecha is DateTime) return (DateTime)valorFecha;

                    string textoFecha = Convert.ToString(valorFecha).Trim();
                    
                    DateTime fecha;
                    
                    bool fechaValida = DateTime.TryParseExact(textoFecha, new string[]
                    {
                        "dd/MM/yyyy HH:mm",
                        "d/MM/yyyy HH:mm",
                        "dd/MM/yyyy H:mm",
                        "d/MM/yyyy H:mm",
                        "dd/MM/yyyy HH:mm:ss",
                        "d/MM/yyyy HH:mm:ss"
                    },
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out fecha);
                    
                    if (fechaValida) return fecha;
                    
                    return DateTime.MaxValue;
                }).ToList();

                if (filas.Count > 0)
                {
                    df = filas.CopyToDataTable();

                    // No visibles
                    seleccionarFilas.Clear();
                    seleccionarPeaje.Clear();

                    estadoTabla = 3;
                    dataGridView3.DataSource = df;

                    dataGridView3.Columns["IdPeaje"].Visible = false;
                    dataGridView3.Columns["Categoria"].Visible = false;
                    dataGridView3.Columns["TipoDoc"].Visible = false;
                    dataGridView3.Columns["Cuenta"].Visible = false;
                    dataGridView3.Columns["Usuario"].Visible = false;
                    dataGridView3.Columns["FechaHoraImporta"].Visible = false;

                    // Visibles
                    dataGridView3.Columns["FechaHora"].Width = 100;
                    dataGridView3.Columns["Peaje"].Width = 70;
                    dataGridView3.Columns["Placa"].Width = 50;
                    dataGridView3.Columns["Via"].Width = 80;
                    dataGridView3.Columns["Importe"].Width = 50;
                    dataGridView3.Columns["SerieDoc"].Width = 40;
                    dataGridView3.Columns["NumeroDoc"].Width = 70;
                    dataGridView3.Columns["VIAJE"].Width = 60;
                    dataGridView3.Columns["RUTA"].Width = 200;

                    if (saveRow != 0 && saveRow < dataGridView3.Rows.Count)
                        dataGridView3.FirstDisplayedScrollingRowIndex = saveRow;
                }
                else
                {
                    estadoTabla = -1;
                    dataGridView3.DataSource = null;
                }
            }
            catch (Exception ex) { Console.WriteLine("No hay datos: " + ex.Message); }
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.Columns["Placa"] != null)
            {
                if (e.RowIndex != -1)
                {

                    string Placa = dataGridView3.Rows[e.RowIndex].Cells["Placa"].Value.ToString();
                    textBox1.Text = Placa;
                    textBox1.Focus();

                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                dtRespuesta4 = new DataTable();
                dtRespuesta4 = clsContabilidadBL.Instancia.GetPeajes_Ver(5, txtPlaca.Text, textBox2.Text, "");
                dataGridView2.DataSource = null;
                if (dtRespuesta4.Rows.Count > 0)
                {
                    dataGridView2.DataSource = dtRespuesta4;
                    estadoTablaViaje = 2;
                    seleccionarFilas.Clear();
                    foreach (DataColumn col in dtRespuesta4.Columns)
                    {
                        col.ReadOnly = false;

                    }
                    dataGridView2.Columns["codigo"].Width = 60;
                    dataGridView2.Columns["ruta"].Width = 200;
                    dataGridView2.Columns["Placa"].Width = 60;

                    if (PlacaMantenerRefresh == txtPlaca.Text)
                    {
                        if (saveRow2 != 0 && saveRow2 < dataGridView2.Rows.Count)
                            dataGridView2.FirstDisplayedScrollingRowIndex = saveRow2;
                    }

                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_Validating(object sender, CancelEventArgs e)
        {

        }

        private void dataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellValidated(object sender, DataGridViewCellEventArgs e)
        {


        }



    }
}
