using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Linq;
using Negocio;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraEditors.Repository;
using System.Data.OleDb;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using DevExpress.Utils.Menu;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmGuias_ImportarSLV : Form
    {
        string msm;
        string rpta;
        int VerTerceros = 0;
        int posicionCelda;
        int posicionFila;
        public frmGuias_ImportarSLV()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {        
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetOperaciones_GuiasSalaverry_Importar(1, dtpDesde.Text, dtpHasta.Text, VerTerceros);

            dtgvData.DataSource = null;

              if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                label4.Text = "Encontrados: " + dt.Rows.Count.ToString();
                dtgvDataView.Columns["Compania"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["Cliente"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["GuiaRegistrada"].OptionsColumn.AllowEdit = true;
                dtgvDataView.Columns["Serie"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["Numero"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["Fecha"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["Ticket"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["Placa"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["Carreta"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["DNIConductor"].OptionsColumn.AllowEdit = true;
                dtgvDataView.Columns["RucCliente"].OptionsColumn.AllowEdit = false; 
                dtgvDataView.Columns["Conductor"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["DNIAyudante"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["Ayudante"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["PesoPuerto"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["UMBase"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["CantidadBase"].OptionsColumn.AllowEdit = false;
                dtgvDataView.Columns["GuiaRemision"].OptionsColumn.AllowEdit = true;
                //dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();
                 

            }
            else
            {
                MessageBox.Show("No hay datos que mostrar", "Aviso");
            }
            //SqlConnection conexion = new SqlConnection("Data Source=192.168.3.39 ; database=spring26092017 ; Persist Security Info=True ; User ID=sa; Password='sql123456'");
            //SqlCommand cmd = new SqlCommand();
            //conexion.Open();
            //cmd = new SqlCommand("ReportesApp_Almacen_Operaciones_ConsultaTicketsBlz", conexion);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add(new SqlParameter("@Periodo", "202105"));
            
            ////SqlCommand cmd = new SqlCommand("SELECT OD.* FROM OP_GE_OTDetalle OD INNER JOIN OP_TR_Viaje V ON V.IdViaje=OD.IdViaje INNER JOIN OP_TR_Vehiculo VH ON VH.IdVehiculo=V.IdVehiculo WHERE CONVERT(VARCHAR(6),OD.FechaInicio,112)='202105' AND VH.IndTercero='P'", conexion);
            
            //DataTable dt= new DataTable();
            //dt.Load(cmd.ExecuteReader());
            //cmd.CommandTimeout = 0;
            //conexion.Close();

            //if (dt.Rows.Count > 0)
            //{
            //    dtgvData.DataSource = dt;
            //    dtgvDataView.UpdateSummary();
            //    dtgvDataView.BestFitColumns();

            //     string s = new XElement("<r>",
            //     from empList in dt.AsEnumerable()
            //     orderby empList.Field<decimal>("Serie") descending
            //     select new XElement("<d>",
            //          new XAttribute("compania", empList.Field<Int32>("compania")),
            //          new XAttribute("Guia1", empList.Field<decimal>("Guia1")),
            //          new XElement("Serie", empList.Field<string>("Serie")),
            //          new XElement("Numero", empList.Field<string>("Numero"))
            //     )).ToString();

            //    MessageBox.Show(s);
            //}
            //else
            //{
            //    MessageBox.Show("No hay informacion a Mostrar.");
            //}
        }

        private void frmGuias_ImportarSLV_Load(object sender, EventArgs e)
        {
            if ( Utilitario.Instancia.SesionUsuario.usuario == "NMELENDEZ" ||  Utilitario.Instancia.SesionUsuario.usuario == "AGUERRA")
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

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetOperaciones_GuiasSalaverry_Importar(2, dtpDesde.Text, dtpHasta.Text, VerTerceros);

            dtgvData.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                label4.Text = "Encontrados: " + dt.Rows.Count.ToString();
                dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();

                label1.Visible = true;
                label5.Visible = true;
                button4.Visible = true;
                txtTicketCorregir.Visible = true;
                txtSerieNroGuiaCorregir.Visible = true;
                btnGuardarCorrecion.Visible = true;
            }
            else
            {
                MessageBox.Show("No hay datos que mostrar", "Aviso");
            }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Ticket;
            string SerieNumeroMal;

            Ticket = dtgvDataView.GetFocusedRowCellValue("Ticket").ToString().Trim();
            SerieNumeroMal = dtgvDataView.GetFocusedRowCellValue("GuiaRegistrada").ToString().Trim();
            txtTicketCorregir.Text = Ticket;
            txtSerieNroGuiaCorregir.Text = SerieNumeroMal;

        }

        private void btnGuardarCorrecion_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlConnection conexion = new SqlConnection("Data Source=192.168.3.39; database=spring26092017 ; Persist Security Info=True ; User ID=sa; Password='sql123456'");
            SqlCommand cmd = new SqlCommand();
            conexion.Open();
            cmd = new SqlCommand("[dbo].[ReportesApp_Almacen_Operaciones_Guias_Correcion]", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Ticket", txtTicketCorregir.Text));
            cmd.Parameters.Add(new SqlParameter("@SerieNumeroG", txtSerieNroGuiaCorregir.Text));
            dt.Load(cmd.ExecuteReader());
            cmd.CommandTimeout = 0;
            conexion.Close();
            MessageBox.Show("Actualizacion Exitosa");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetOperaciones_GuiasSalaverry_Importar(3, dtpDesde.Text, dtpHasta.Text, VerTerceros);

            dtgvData.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                label4.Text = "Encontrados: " + dt.Rows.Count.ToString();
                dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();

                label1.Visible = false;
                label5.Visible = false;
                button4.Visible = false;
                txtTicketCorregir.Visible = false;
                txtSerieNroGuiaCorregir.Visible = false;
                btnGuardarCorrecion.Visible = false;
            }
            else
            {
                MessageBox.Show("No hay datos que mostrar", "Aviso");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetOperaciones_GuiasSalaverry_Importar(4, dtpDesde.Text, dtpHasta.Text, VerTerceros);

            dtgvData.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                label4.Text = "Encontrados: " + dt.Rows.Count.ToString();
                dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();

                label1.Visible = false;
                label5.Visible = false;
                button4.Visible = false;
                txtTicketCorregir.Visible = false;
                txtSerieNroGuiaCorregir.Visible = false;
                btnGuardarCorrecion.Visible = false;

            }
            else
            {
                MessageBox.Show("No hay datos que mostrar", "Aviso");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            VerAsignados();
        }

        private void VerAsignados (){

            if (checkBox1.Checked == true){VerTerceros = 1;}else {VerTerceros = 0;}            

            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();

             DataTable dt = new DataTable();
             dt = clsOperacionesBL.Instancia.GetOperaciones_GuiasSalaverry_Importar(7, dtpDesde.Text, dtpHasta.Text, VerTerceros);

            dtgvData.DataSource = null;                       

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                label4.Text = "Encontrados: " + dt.Rows.Count.ToString();
                dtgvDataView.Columns["IdViaje"].Visible = false;
                dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();
                btnCrearViaje.Enabled = true;
                txtOT.Enabled = true;
                dtgvDataView.OptionsBehavior.Editable = true;
                btnGuardar.Visible = true;
            }
            else
            {
                btnCrearViaje.Enabled = false;
                txtOT.Enabled = false;
                MessageBox.Show("No hay datos que mostrar", "Aviso");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetOperaciones_GuiasSalaverry_Importar(5, dtpDesde.Text, dtpHasta.Text, VerTerceros);

            dtgvData.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                label4.Text = "Encontrados: " + dt.Rows.Count.ToString();
                dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();
                label1.Visible = false;
                label5.Visible = false;
                button4.Visible = false;
                txtTicketCorregir.Visible = false;
                txtSerieNroGuiaCorregir.Visible = false;
                btnGuardarCorrecion.Visible = false;

            }
            else
            {
                MessageBox.Show("No hay datos que mostrar", "Aviso");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetOperaciones_GuiasSalaverry_Importar(6, dtpDesde.Text, dtpHasta.Text, VerTerceros);

            dtgvData.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                label4.Text = "Encontrados: " + dt.Rows.Count.ToString();
                dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();
            }
            else
            {
                MessageBox.Show("No hay datos que mostrar", "Aviso");
            }
        }

        private void btnCrearViaje_Click(object sender, EventArgs e)
        {
            if (txtOT.Text.Length == 0)
            {
                MessageBox.Show("Debe colocar una OT válida.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtOT.Focus();
                return;
            }

            string ID;
            string IDOT;
            IDOT = txtOT.Text;
            string IDPARTIDA = cboPartida.SelectedValue.ToString();
            string IDLLEGADA = cboLlegada.SelectedValue.ToString();           
            int[] filas = dtgvDataView.GetSelectedRows();           
                           
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    ID = dtgvDataView.GetRowCellValue(filas[i], "ID").ToString();

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsOperacionesBL.Instancia.GetOperaciones_GuiasSalaverry_CreaViaje(ID, IDOT, IDPARTIDA, IDLLEGADA, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    string codviaje =Respuesta.Substring(25, 6);
                    string planilla = Respuesta.Substring(46, 6);
                  
                    if (NroRPTA == "0")
                    {
                        //MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = textBox1.Text + Respuesta + "\r\n";
                        dtgvDataView.SetRowCellValue(filas[i], "VIAJE", codviaje);
                        dtgvDataView.SetRowCellValue(filas[i], "OT", IDOT);
                        dtgvDataView.SetRowCellValue(filas[i], "PLANILLA", planilla);
                        //or
                      //  dtgvDataView.SetRowCellValue(codviaje);
                    }
                    else
                    {
                        //MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = textBox1.Text + Respuesta + "\r\n";
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                MessageBox.Show("No se encontraron Datos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Detalle Guias SLV " + dtpDesde.Value.ToString("dd_MM_yyyy") + " al " + dtpHasta.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmImportarGuias impGuias = new FrmImportarGuias();
            impGuias.Show();

           /* OpenFileDialog openFileDialog = new OpenFileDialog
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
                dtgvData.DataSource = ImportarDatos(openFileDialog.FileName);
                splitContainer3.Panel2Collapsed = true;
                splitContainer3.Panel1Collapsed = false;
                splitContainer4.Panel2Collapsed = true;
                splitContainer4.Panel1Collapsed = false;
            }*/
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {           
                string ID;
                string GuiaRemision;
                string Observacion;
                decimal Peso=12.120000m;
             
                int[] filas = dtgvDataView.GetSelectedRows();
                if (filas.Length != 0)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        ID = dtgvDataView.GetRowCellValue(filas[i], "ID").ToString();
                        GuiaRemision = dtgvDataView.GetRowCellValue(filas[i], "GUIAREMISION").ToString();
                        Observacion = dtgvDataView.GetRowCellValue(filas[i], "OBSERVACION").ToString();

                        if (String.IsNullOrEmpty(dtgvDataView.GetRowCellValue(filas[i], "PESOVIAJE").ToString()))
                        {
                            Peso = Convert.ToDecimal("0.0");
                        }
                        else
                        {
                            Peso = Convert.ToDecimal(dtgvDataView.GetRowCellValue(filas[i], "PESOVIAJE"));
                        }

                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        dtRespuesta = clsOperacionesBL.Instancia.UpdateGuias_Operaciones(ID, GuiaRemision, Observacion,Peso);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        msm = NroRPTA;
                        rpta = Respuesta;
                    }
                    if (msm == "0")
                    {
                        MessageBox.Show(rpta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void txtOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                int otbuscar;
                otbuscar = int.Parse(txtOT.Text);
                DataTable dt = new DataTable();
                DataTable dt1 = new DataTable();               

                dt = clsOperacionesBL.Instancia.GetOperaciones_ListarDireccionesxOts(otbuscar);
                dt1 = clsOperacionesBL.Instancia.GetOperaciones_ListarDireccionesxOts(otbuscar);

                cboPartida.DisplayMember = "Direccion";
                cboPartida.ValueMember = "Secuencia";
                cboPartida.DataSource = dt;

                cboLlegada.DisplayMember = "Direccion";
                cboLlegada.ValueMember = "Secuencia";
                cboLlegada.DataSource = dt1;
               
            }
        }

        private void dtgvDataView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            try
            {
                int[] filass = dtgvDataView.GetSelectedRows();
                string datoseleccionado = dtgvDataView.GetFocusedValue().ToString();

                    for (int i = 0; i < filass.Length; i++)
                    {
                        string guiaeditar = dtgvDataView.GetRowCellValue(filass[i], "GuiaRegistrada").ToString();
                        int idEditar = Convert.ToInt32(dtgvDataView.GetRowCellValue(filass[i], "ID").ToString());
                      
                       
                        e.Menu.Items.Add(new DXMenuItem("Opciones:",
                            new EventHandler((snd, evt) =>
                            {
                               
                            }

                          )));
                        
                        e.Menu.Items.Add(new DXMenuItem("Editar",
                           new EventHandler((snd, evt) =>
                           {
                              

                               frmEditarGuias frmEditar = new frmEditarGuias();
                               frmEditar.guiaregistrada = guiaeditar;
                               frmEditar.idticket = idEditar;
                               frmEditar.OpcionBusca = 0; //cuando selecciona un registro es 0
                               frmEditar.ShowDialog();
                               
                               
                               //DataTable dtAnula = new DataTable();

                               //dtAnula = clsOperacionesBL.Instancia.GetOperaciones_Previajes_AnularViajes(IDVIAJE.ToString(), 0, input, Utilitario.Instancia.SesionUsuario.usuario, "0");
                               //rpta = Convert.ToString(dtAnula.Rows[0]["exito"]);
                               //string NroRPTA = rpta.Substring(0, 1);

                               //if (NroRPTA == "0")
                               //{
                               //    MessageBox.Show(rpta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                               //    VerAsignados();
                               //}
                               //else
                               //{
                               //    MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                               //}
                           }

                         )));
                        int IDVIAJE = Convert.ToInt32(dtgvDataView.GetRowCellValue(filass[i], "IdViaje").ToString());
                        string CODIGOVIAJE = dtgvDataView.GetRowCellValue(filass[i], "VIAJE").ToString();
                        e.Menu.Items.Add(new DXMenuItem("&Anular Viaje",
                            new EventHandler((snd, evt) =>
                            {                               

                                if (MessageBox.Show("Desea ANULAR el viaje: " + CODIGOVIAJE + "...?", "ANULAR VIAJE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {                                  
                                    string input = "";
                                    string rpta;
                                    if (ShowInputDialogBox(ref input, "Agregar un comentario para anular el Viaje: " + CODIGOVIAJE, "ANULAR VIAJE", 300, 200) == DialogResult.OK)
                                    {
                                        DataTable dtAnula = new DataTable();

                                        dtAnula = clsOperacionesBL.Instancia.GetOperaciones_Previajes_AnularViajes(IDVIAJE.ToString(), 0, input, Utilitario.Instancia.SesionUsuario.usuario,"0",1);
                                        rpta = Convert.ToString(dtAnula.Rows[0]["exito"]);
                                        string NroRPTA = rpta.Substring(0, 1);

                                        if (NroRPTA == "0")
                                        {
                                            MessageBox.Show(rpta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            VerAsignados();
                                        }
                                        else
                                        {
                                            MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                    }
                                }
                            }

                          )));                       
                    }
                
            }
            catch (Exception)
            {

            }
        }

        private static DialogResult ShowInputDialogBox(ref string input, string prompt, string title = "Anular Viajes", int width = 100, int height = 200)
        {
            Size size = new Size(width, height);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Height = 150;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.Text = title;

            //Create a new label to hold the prompt
            Label label = new Label();
            label.Text = prompt;
            label.Location = new Point(5, 5);
            label.Width = size.Width - 10;
            label.Margin = new System.Windows.Forms.Padding(3, 25, 2, 35);
            inputBox.Controls.Add(label);

            //Create a textbox to accept the user's input
            TextBox textBox = new TextBox();
            textBox.Size = new Size(260, 23);
            textBox.Location = new Point(20, label.Location.Y + 20);
            textBox.Text = input.ToUpper();
            textBox.Location = new Point(20, 40);
            inputBox.Controls.Add(textBox);

            //Create an OK Button 
            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new Point(size.Width - 80 - 80, 100 - 30);
            inputBox.Controls.Add(okButton);

            //Create a Cancel Button
            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new Point(size.Width - 80, 100 - 30);
            inputBox.Controls.Add(cancelButton);

            //Set the input box's buttons to the created OK and Cancel Buttons respectively so the window appropriately behaves with the button clicks
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            //Show the window dialog box 
            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            string obser = textBox.Text;
            return result;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            dtgvDataView.AddNewRow();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmEditarGuias frmEditar = new frmEditarGuias();
            frmEditar.guiaregistrada = "";
            frmEditar.idticket = 0;
            frmEditar.btnBuscar.Visible = true;
            frmEditar.OpcionBusca = 1;//cuando busca una guia.
            frmEditar.ShowDialog();
        }

        private void dtgvDataView_HiddenEditor(object sender, EventArgs e)
        {
            try
            {
                 //////////////////////////////////////////////// DESACTIVADO TEMPORALMENTE - MODIFICA LOS DATOS DIRECTAMENTE DE ALTRA //////////////////////////////////

                /*
            if (dtgvDataView.Columns[dtgvDataView.FocusedColumn.AbsoluteIndex].FieldName == "GuiaRegistrada" )
            {
                if (dtgvDataView.GetFocusedRowCellValue("Ticket").ToString().Length > 0)
                {
                    string ticket = Convert.ToString(dtgvDataView.GetFocusedRowCellValue("Ticket"));
                    string guiaRegistrada = dtgvDataView.GetFocusedRowCellValue("GuiaRegistrada").ToString();
                    posicionCelda = dtgvDataView.FocusedColumn.AbsoluteIndex;
                    posicionFila = dtgvDataView.FocusedRowHandle;

                    this.Cursor = Cursors.WaitCursor;
                    if (clsOperacionesBL.Instancia.ReportesApp_ActualizarDatosGuiaSalaverry("GuiaRegistrada", ticket, guiaRegistrada))
                    {
                        button1.PerformClick();
                        dtgvDataView.FocusedColumn.ColumnHandle = posicionCelda;
                        dtgvDataView.FocusedRowHandle = posicionFila;
                        dtgvDataView.TopRowIndex = posicionFila;
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button1.PerformClick();
                        dtgvDataView.FocusedColumn.ColumnHandle = posicionCelda;
                        dtgvDataView.FocusedRowHandle = posicionFila;
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox.Show("no tiene ticket, no es posible actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.PerformClick();
                    return;
                }
            }


            if (dtgvDataView.Columns[dtgvDataView.FocusedColumn.AbsoluteIndex].FieldName == "GuiaRemision")
            {
                if (dtgvDataView.GetFocusedRowCellValue("Ticket").ToString().Length > 0)
                {
                    string ticket = Convert.ToString(dtgvDataView.GetFocusedRowCellValue("Ticket"));
                    string guiaRegistrada = dtgvDataView.GetFocusedRowCellValue("GuiaRemision").ToString();
                    posicionCelda = dtgvDataView.FocusedColumn.AbsoluteIndex;
                    posicionFila = dtgvDataView.FocusedRowHandle;
                    this.Cursor = Cursors.WaitCursor;
                    if (clsOperacionesBL.Instancia.ReportesApp_ActualizarDatosGuiaSalaverry("GuiaRemision", ticket, guiaRegistrada))
                    {
                        button1.PerformClick();
                        dtgvDataView.FocusedColumn.ColumnHandle = posicionCelda;
                        dtgvDataView.FocusedRowHandle = posicionFila;
                        dtgvDataView.TopRowIndex = posicionFila;
                        this.Cursor = Cursors.Default;
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button1.PerformClick();
                        dtgvDataView.FocusedColumn.ColumnHandle = posicionCelda;
                        dtgvDataView.FocusedRowHandle = posicionFila;
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox.Show("Codigo de Viaje ya fue generado, no es posible modificar Peso Cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.PerformClick();
                    return;
                }
            }

            /*if (dtgvDataView.Columns[dtgvDataView.FocusedColumn.AbsoluteIndex].FieldName == "Serie")
            {
                if (dtgvDataView.GetFocusedRowCellValue("Ticket").ToString().Length > 0)
                {
                    string ticket = Convert.ToString(dtgvDataView.GetFocusedRowCellValue("Ticket"));
                    string guiaRegistrada = dtgvDataView.GetFocusedRowCellValue("Serie").ToString();
                    posicionCelda = dtgvDataView.FocusedColumn.AbsoluteIndex;
                    posicionFila = dtgvDataView.FocusedRowHandle;

                    if (clsOperacionesBL.Instancia.ReportesApp_ActualizarDatosGuiaSalaverry("Serie", ticket, guiaRegistrada))
                    {
                        button1.PerformClick();
                        dtgvDataView.FocusedColumn.ColumnHandle = posicionCelda;
                        dtgvDataView.FocusedRowHandle = posicionFila;
                        dtgvDataView.TopRowIndex = posicionFila;
                    }
                    else
                    {
                        button1.PerformClick();
                        dtgvDataView.FocusedColumn.ColumnHandle = posicionCelda;
                        dtgvDataView.FocusedRowHandle = posicionFila;
                    }
                }
                else
                {
                    MessageBox.Show("Codigo de Viaje ya fue generado, no es posible modificar Peso Cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.PerformClick();
                    return;
                }
            }


            if (dtgvDataView.Columns[dtgvDataView.FocusedColumn.AbsoluteIndex].FieldName == "Numero")
            {
                if (dtgvDataView.GetFocusedRowCellValue("Ticket").ToString().Length > 0)
                {
                    string ticket = Convert.ToString(dtgvDataView.GetFocusedRowCellValue("Ticket"));
                    string guiaRegistrada = dtgvDataView.GetFocusedRowCellValue("Numero").ToString();
                    posicionCelda = dtgvDataView.FocusedColumn.AbsoluteIndex;
                    posicionFila = dtgvDataView.FocusedRowHandle;

                    if (clsOperacionesBL.Instancia.ReportesApp_ActualizarDatosGuiaSalaverry("Numero", ticket, guiaRegistrada))
                    {
                        button1.PerformClick();
                        dtgvDataView.FocusedColumn.ColumnHandle = posicionCelda;
                        dtgvDataView.FocusedRowHandle = posicionFila;
                        dtgvDataView.TopRowIndex = posicionFila;
                    }
                    else
                    {
                        button1.PerformClick();
                        dtgvDataView.FocusedColumn.ColumnHandle = posicionCelda;
                        dtgvDataView.FocusedRowHandle = posicionFila;
                    }
                }
                else
                {
                    MessageBox.Show("Codigo de Viaje ya fue generado, no es posible modificar Peso Cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.PerformClick();
                    return;
                }
            }*/

/*
            if (dtgvDataView.Columns[dtgvDataView.FocusedColumn.AbsoluteIndex].FieldName == "DNIConductor")
            {
                if (dtgvDataView.GetFocusedRowCellValue("Ticket").ToString().Length > 0)
                {

                    string ticket = Convert.ToString(dtgvDataView.GetFocusedRowCellValue("Ticket"));
                    string guiaRegistrada = dtgvDataView.GetFocusedRowCellValue("DNIConductor").ToString();
                    posicionCelda = dtgvDataView.FocusedColumn.AbsoluteIndex;
                    posicionFila = dtgvDataView.FocusedRowHandle;
                    this.Cursor = Cursors.WaitCursor;
                    if (clsOperacionesBL.Instancia.ReportesApp_ActualizarDatosGuiaSalaverry("DNIConductor", ticket, guiaRegistrada))
                    {
                        this.Cursor = Cursors.Default;
                        button1.PerformClick();
                        dtgvDataView.FocusedColumn.ColumnHandle = posicionCelda;
                        dtgvDataView.FocusedRowHandle = posicionFila;
                        dtgvDataView.TopRowIndex = posicionFila;
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        button1.PerformClick();
                        dtgvDataView.FocusedColumn.ColumnHandle = posicionCelda;
                        dtgvDataView.FocusedRowHandle = posicionFila;
                        this.Cursor = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox.Show("no tiene ticket, no es posible actualizar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    button1.PerformClick();
                    return;
                }
            }


                */

            }
            catch (Exception ex )
            {
                
               MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
