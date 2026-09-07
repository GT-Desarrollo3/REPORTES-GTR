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
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using ReportesTranspesa.Sistema;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Base;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class PersonalNoGrato : MetroFramework.Forms.MetroForm
    {
        public PersonalNoGrato()
        {
            InitializeComponent();
        }

        string fechaini;
        string fechafin;

        private void PersonalNoGrato_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            Periodos();
            cboPeriodo.SelectedIndex = 0;
            dtgvPNG.DataSource = null;
            dtgvPNGView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            DataTable dtRespuesta = new DataTable();

            txtPersona.Text = "";

            string Respuesta;
            string user;
            user = Utilitario.Instancia.SesionUsuario.usuario;
                       
            dtRespuesta = clsRecursosHumanosBL.Instancia.GetPersonaNoGrata_Permisos(user);
            if (dtRespuesta.Rows.Count > 0)
            {
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                if (Respuesta == "1")
                {
                    buttonnew.Visible = true;
                    buttonmod.Visible = true;
                    buttondel.Visible = true;
                }
                else
                {
                    if (Respuesta == "2")
                    {
                        buttonnew.Visible = true;
                        buttonmod.Visible = true;

                    }

                }
            }
               
            //dt = clsRecursosHumanosBL.Instancia.GetPersonalNoGrato();
            dt = clsRecursosHumanosBL.Instancia.GetPersonaNoGrata();

            this.WindowState = FormWindowState.Maximized;
            splitContainer1.SplitterDistance = 76;


            if (dt.Rows.Count > 0)
            {
                dtgvPNG.DataSource = dt;
                dtgvPNGView.BestFitColumns();
            }
        }

        private void PersonalNoGrato_LoadS()
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            Periodos();
            cboPeriodo.SelectedIndex = 0;
            dtgvPNG.DataSource = null;
            dtgvPNGView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();

            //dt = clsRecursosHumanosBL.Instancia.GetPersonalNoGrato();
            dt = clsRecursosHumanosBL.Instancia.GetPersonaNoGrata();

            if (dt.Rows.Count > 0)
            {
                dtgvPNG.DataSource = dt;
                dtgvPNGView.BestFitColumns();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvPNG.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Personas No Gratas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvPNG.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        public void Periodos()
        {
            cboPeriodo.Items.Add("Seleccione:");
            string[] meses = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
            string[] años = { "2019", "2020" };
            //string[] años = {"2015","2016","2017","2018","2019","2020","2021","2022","2023","2025"};
            foreach (var año in años)
            {
                foreach (var mes in meses)
                {
                    var fecha = año + mes;
                    cboPeriodo.Items.Add(fecha);
                }
            }
            ////////////////////////////////////////////
            #region tablasperiodo
            //DataTable dsmeses = new DataTable("meses");
            ////Agregamos las Columnas codigo y desripcion
            //DataColumn colInt = new DataColumn("Codigo");
            //colInt.DataType = System.Type.GetType("System.Int32");
            //dsmeses.Columns.Add(colInt);
            //DataColumn colString = new DataColumn("Descripcion");
            //colString.DataType = System.Type.GetType("System.String");
            //dsmeses.Columns.Add(colString);
            //dsmeses.Columns.Add("Codigo", typeof(Int16));
            //dsmeses.Columns.Add("Codigo", typeof(String));
            //dsmeses.Columns.Add("Descripcion", typeof(String));

            //Agregamos las filas
            //DataRow myNewRow; 
            //myNewRow = dsmeses.NewRow();
            //myNewRow["Codigo"] = 01;
            //myNewRow["Descripcion"] = "Enero";
            //myNewRow["Codigo"] = 02;
            //myNewRow["Descripcion"] = "Febrero";
            //myNewRow["Codigo"] = 03;
            //myNewRow["Descripcion"] = "Marzo";
            //myNewRow["Codigo"] = 04;
            //myNewRow["Descripcion"] = "Abril";
            //myNewRow["Codigo"] = 05;
            //myNewRow["Descripcion"] = "Mayo";
            //myNewRow["Codigo"] = 06;
            //myNewRow["Descripcion"] = "Junio";
            //myNewRow["Codigo"] = 07;
            //myNewRow["Descripcion"] = "Julio";
            //myNewRow["Codigo"] = 08;
            //myNewRow["Descripcion"] = "Agosto";
            //myNewRow["Codigo"] = 09;
            //myNewRow["Descripcion"] = "Setimebre";
            //myNewRow["Codigo"] = 10;
            //myNewRow["Descripcion"] = "Octubre";
            //myNewRow["Codigo"] = 11;
            //myNewRow["Descripcion"] = "Noviembre";
            //myNewRow["Codigo"] = 12;
            //myNewRow["Descripcion"] = "Diciembre";
            //dsmeses.Rows.Add(myNewRow);
            //dsmeses.Rows.Add(new object[] { "01", "Enero" });
            //dsmeses.Rows.Add(new object[] { "02", "Febrero" });
            //dsmeses.Rows.Add(new object[] { "03", "Marzo" });
            //dsmeses.Rows.Add(new object[] { "04", "Abril" });
            //dsmeses.Rows.Add(new object[] { "05", "Mayo" });
            //dsmeses.Rows.Add(new object[] { "06", "Junio" });
            //dsmeses.Rows.Add(new object[] { "07", "Julio" });
            //dsmeses.Rows.Add(new object[] { "08", "Agosto" });
            //dsmeses.Rows.Add(new object[] { "09", "Setiembre" });
            //dsmeses.Rows.Add(new object[] { "10", "Octubre" });
            //dsmeses.Rows.Add(new object[] { "11", "Noviembre" });
            //dsmeses.Rows.Add(new object[] { "12", "Dicimebre" });
            //dsmeses.Rows.Add(new object[] { 01, "Enero" });
            //dsmeses.Rows.Add(new object[] { 02, "Febrero" });
            //dsmeses.Rows.Add(new object[] { 03, "Marzo" });
            //dsmeses.Rows.Add(new object[] { 04, "Abril" });
            //dsmeses.Rows.Add(new object[] { 05, "Mayo" });
            //dsmeses.Rows.Add(new object[] { 06, "Junio" });
            //dsmeses.Rows.Add(new object[] { 07, "Julio" });
            //dsmeses.Rows.Add(new object[] { 08, "Agosto" });
            //dsmeses.Rows.Add(new object[] { 09, "Setiembre" });
            //dsmeses.Rows.Add(new object[] { 10, "Octubre" });
            //dsmeses.Rows.Add(new object[] { 11, "Noviembre" });
            //dsmeses.Rows.Add(new object[] { 12, "Dicimebre" });
            //dsmeses.AcceptChanges();

            //Establecemos la Tabla con fuente de datos de nuestro comboBox
            //cboPeriodo.DataSource = dsmeses;
            //cboPeriodo.ValueMember = "Codigo";
            //cboPeriodo.DisplayMember = "Codigo";
            //cboPeriodo.DisplayMember = "Descripcion";

            //Seleccionamos en mes actual
            //cboPeriodo.SelectedValue = DateTime.Now.Month;
            //var mes = DateTime.Now.Month;
            //DateTime mes = new DateTime();
            //string.Format("MM",mes);
            //string.Format("{0:MM}", mes.Month);
            //cboPeriodo.Items.Add(mes);
            //cboPeriodo.Items.Add(DateTime.Now.Month);
            #endregion
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (chkFecha.Checked == true)
            {
                dtgvPNG.DataSource = null;
                dtgvPNGView.Columns.Clear();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsRecursosHumanosBL.Instancia.GetPersonaNoGrataPeriodo(dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString(), txtDescripcion.Text);
                //dt = clsRecursosHumanosBL.Instancia.GetPersonaNoGrataPeriodo(fechaini, fechafin);

                if (dt.Rows.Count > 0)
                {
                    dtgvPNG.DataSource = dt;
                    dtgvPNGView.BestFitColumns();
                }

                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hubo resultados";
                    m.ShowDialog();
                }

            }
            else
            {
                string periodo = "";
                periodo = cboPeriodo.SelectedItem.ToString();
                dtgvPNG.DataSource = null;
                dtgvPNGView.Columns.Clear();
                System.Data.DataTable dt1 = new System.Data.DataTable();
                dt1 = clsRecursosHumanosBL.Instancia.GetPersonaNoGrataxPeriodo(periodo, txtDescripcion.Text);

                if (dt1.Rows.Count > 0)
                {
                    dtgvPNG.DataSource = dt1;
                    dtgvPNGView.BestFitColumns();
                }

                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hubo resultados";
                    m.ShowDialog();
                }
            }
        }

        private void chkFecha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFecha.Checked == true)
            {
                dtpFechaIni.Enabled = true;
                dtpFechaFin.Enabled = true;
                fechaini = dtpFechaIni.Value.ToShortDateString();
                fechafin = dtpFechaFin.Value.ToShortDateString();
                cboPeriodo.Enabled = false;
            }
            else
            {
                dtpFechaIni.Enabled = false;
                dtpFechaFin.Enabled = false;
                fechaini = "01/01/1950";
                fechafin = "30/12/2050";
                cboPeriodo.Enabled = true;
            }
        }

        private void txtEmpleado_TextChanged(object sender, EventArgs e)
        {
            int length = txtEmpleado.Text.Length;
            if (length == 0)
            {
                txtCodigo.Text = "";
            }
        }
     

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvEmpleado, clsConsultaBL.Instancia.GetPersona(txtEmpleado.Text), true, false, false);

                lvEmpleado.Size = new System.Drawing.Size(412, 119);

                lvEmpleado.Columns[0].Width = 0;
                lvEmpleado.Columns[1].Width = 210;
                lvEmpleado.Columns[2].Width = 110;

                lvEmpleado.BringToFront();
                lvEmpleado.Visible = true;
                lvEmpleado.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
            }
        }

        private void lvEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvEmpleado.SelectedItems[0];

                txtCodigo.Text = ItemActual.Text;
                txtEmpleado.Text = ItemActual.SubItems[1].Text;
                lvEmpleado.Visible = false;

                DatosPersona(txtCodigo.Text);

                txtEmpleado.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            txtEmpleado.ReadOnly = false;
            txtEmpleado.Focus();
        }
        void DatosPersona(string IDPersona)
        {
            string ApePaterno;
            string ApeMaterno;
            string Nombres;
            string TipoDoc;
            string Documento;
            string Motivo;
            string Codigo;

            DataTable dtPersona = new DataTable();
            dtPersona = clsConsultaBL.Instancia.GetPersona3(IDPersona);

            if(dtPersona.Rows.Count==0)
            {
                MessageBox.Show("Debe seleccionar un trabajador, no una Empresa. Revisar.");
                txtCodigo.Text = "";
                txtApePaterno.Text = "";
                txtApeMaterno.Text = "";
                txtApeNombres.Text = "";
                txtDni.Text = "";
                txtCodigo.Text = "";
                txtEmpleado.Focus();
            }
            else
            {
                ApePaterno = Convert.ToString(dtPersona.Rows[0]["apellidopaterno"]);
                ApeMaterno = Convert.ToString(dtPersona.Rows[0]["apellidomaterno"]);
                Nombres = Convert.ToString(dtPersona.Rows[0]["Nombres"]);
                TipoDoc = Convert.ToString(dtPersona.Rows[0]["tipodocumento"]);
                Documento = Convert.ToString(dtPersona.Rows[0]["documento"]);
                Motivo = Convert.ToString(dtPersona.Rows[0]["Motivo"]);
                Codigo = Convert.ToString(dtPersona.Rows[0]["Persona"]);
                txtApePaterno.Text = ApePaterno;
                txtApeMaterno.Text = ApeMaterno;
                txtApeNombres.Text = Nombres;
                txtTipoDoc.Text = TipoDoc;
                txtDni.Text = Documento;
                txtMotivo.Text = Motivo;
                txtCodigo.Text = Codigo;
            }
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            txtEmpleado.Text = "";
            txtCodigo.Text = "";
            txtApePaterno.Text = "";
            txtApeMaterno.Text = "";
            txtApeNombres.Text = "";
            txtDni.Text = "";
            txtMotivo.Text = "";
            groupBox2.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string ApePaterno;
            string ApeMaterno;
            string Nombres;
            string IDPersona;
            string DNI;
            string Fecha;
            string Motivo;
            string User;

            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            dtfi.TimeSeparator = "/";
            Fecha = DateTime.Now.ToString("d-M-yyyy", dtfi);
            ApePaterno = txtApePaterno.Text;
            ApeMaterno = txtApeMaterno.Text;
            IDPersona = txtCodigo.Text;
            Nombres = txtApeNombres.Text;
            DNI = txtDni.Text;
            Motivo = txtMotivo.Text;
            User =Utilitario.Instancia.SesionUsuario.usuario;



            DialogResult result = MessageBox.Show("¿Esta seguro que deseas realizar esta acción?", "Consulta", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string Respuesta;
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsRecursosHumanosBL.Instancia.GetPersonaNoGrata_Registro(IDPersona, ApePaterno, ApeMaterno, Nombres, DNI, Fecha, Motivo, User);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //txtDescripcion.Text = "";
                    //txtCodigo.Text = "";
                    //textBox1.Text = "";
                    PersonalNoGrato_LoadS();
                    groupBox2.Visible = false;
                }
                else
                {
                    MessageBox.Show(Respuesta  , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                              

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string DNI;

            if (txtPersona.Text!="")
            {
                string id;
                DNI = txtPersona.Text;
                groupBox2.Visible = true;
                txtEmpleado.ReadOnly = true;
                DataTable dtPersona = new DataTable();
                dtPersona = clsConsultaBL.Instancia.GetPersona4(txtPersona.Text);
                id = Convert.ToString(dtPersona.Rows[0]["ID"]);
                DatosPersona(id);
                
                
            }
            else
            {
                MessageBox.Show("Debe seleccionar a un empleado de la lista");
            }
        }

        private void txtMotivo_TextChanged(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            string DNI;

            if (txtPersona.Text != "")
            {
                DialogResult result = MessageBox.Show("¿Esta seguro que desea eliminar el siguiente registro?", "Consulta", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    string id;
                    string Respuesta;
                    DNI = txtPersona.Text;
                    DataTable dtPersona = new DataTable();
                    dtPersona = clsConsultaBL.Instancia.GetPersona4(txtPersona.Text);
                    id = Convert.ToString(dtPersona.Rows[0]["ID"]);
                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsRecursosHumanosBL.Instancia.GetPersonaNoGrata_Eliminar(id);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        PersonalNoGrato_LoadS();
                        groupBox2.Visible = false;
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar a un empleado de la lista");
            }
        }

        private void dtgvPNG_Click(object sender, EventArgs e)
        {
            
            txtPersona.Text = (dtgvPNGView.GetFocusedRowCellValue("DNI").ToString()).Trim();

        }       

    }
}
