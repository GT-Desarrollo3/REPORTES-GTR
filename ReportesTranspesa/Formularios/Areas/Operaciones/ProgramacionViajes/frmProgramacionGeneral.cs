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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    
    public partial class frmProgramacionGeneral : Form
    {
        int datopro;
        int datoSursal;
        int datoAcceso;
        public frmProgramacionGeneral()
        {
            InitializeComponent();
        }

        private void frmProgramacionGeneral_Load(object sender, EventArgs e)
        {
            CargarAccesos();
            DataTable dtProgramaciones = new DataTable();

            dtProgramaciones = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeOperacionesAccesos();          

            comboBox1.DisplayMember = "Descripcion";
            comboBox1.ValueMember = "IdOperacion";
            comboBox1.DataSource = dtProgramaciones;            

        }

        private void CargarAccesos() {
            DataTable DtAccesos = new DataTable();
            DtAccesos = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarAccesos();
            if (DtAccesos.Rows.Count > 0)
            {
                dataGridView1.DataSource = DtAccesos;

                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    textBox1_TextChanged_1(this, new KeyPressEventArgs((char)(Keys.Enter)));
                }

            }
            else
            {
                MessageBox.Show("No hay Accesos");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            string usuario = textBox1.Text;
            string sucursal = comboBox2.Text; 
            int programacion = 0;
            int acceso = 0;

            if (usuario.Equals("")) 
            {
                MessageBox.Show("Ingresar nombre de Usuario....!");
                textBox1.Focus();
                return;
            }

            switch (comboBox1.Text)
            {
                case "TOLVAS": programacion = 1;

                    break;

                case "LINDLEY": programacion = 2;

                    break;

                case "LIMAGAS": programacion = 3;

                    break;

                case "GENERAL": programacion = 4;

                    break;

                case "TODO": programacion = 5;

                    break;

                case "MAQ/CAMIONETAS": programacion = 6;

                    break;

                case "MTO": programacion = 7;

                    break;
                case "COMBUSTIBLE": programacion = 8;

                    break;
                case "LOCAL": programacion = 9;

                    break;
            }

            switch (comboBox3.Text)
            {
                case "VER":
                    acceso = 1;

                    break;

                case "CREAR": acceso = 2;

                    break;
                
            }

            string Respuesta;
            DataTable DtAccesos = new DataTable();
            DtAccesos = clsOperacionesBL.Instancia.GetOperaciones_PreviajesAgregaAcceso(usuario, programacion, sucursal, acceso);
            Respuesta = Convert.ToString(DtAccesos.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarAccesos();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Filtros()
        {
            try
            {               
               string colFiltrar = "USUARIO";
               ((DataTable)dataGridView1.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", colFiltrar, textBox1.Text);               

            }
            catch (Exception)
            {

            }
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            Filtros();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string Usuario = dataGridView1.CurrentRow.Cells["USUARIO"].Value.ToString();
            string sucursal = dataGridView1.CurrentRow.Cells["SUCURSAL"].Value.ToString();
            string progrmacion = dataGridView1.CurrentRow.Cells["OPERACION"].Value.ToString();
            string acceso = dataGridView1.CurrentRow.Cells["ACCESOS"].Value.ToString();


            textBox1.Text = Usuario;

            
             switch (progrmacion)
            {
                case "TOLVAS":
                    datopro = 1;

                    break;

                case "LINDLEY": datopro = 2;

                    break;

                case "LIMAGAS": datopro = 3;

                    break;

                case "GENERAL": datopro = 4;

                    break;

                case "TODO": datopro = 5;

                    break;
            }

             switch (sucursal)
             {
                 case "TRUJILLO":
                     datoSursal = 1;

                     break;

                 case "LIMA": datoSursal = 2;

                     break;

                 case "TODO": datoSursal = 3;

                     break;                
             }
             switch (acceso)
             {
                 case "VER":
                     datoAcceso = 1;

                     break;

                 case "CREAR": datoAcceso = 2;

                     break;

                
             }

            comboBox1.SelectedIndex = datopro - 1 ;
            comboBox2.SelectedIndex = datoSursal -1;
            comboBox3.SelectedIndex = datoAcceso -1;

        }
    }
}
