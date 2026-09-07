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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmBonoConductoresExcepciones : Form
    {
        public int idConductor,resultado =0;
        public string conductor,periodo;
        public int Opcion = 1;//0 = Ver, 1= Nuevo, 2=Modificar, 3 = Borrar
        public string Operacion;
        int TipoExcepcion = 0;

        public frmBonoConductoresExcepciones()
        {
            InitializeComponent();
            cbxMotivo.SelectedIndexChanged -= cbxMotivo_SelectedIndexChanged;
        }

        private void cbxMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboMotivo();
        }

        private void frmBonoConductoresExcepciones_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            CargarComboMotivo();
            if (Opcion == 0)
            {
                DataTable dtListaExcepcions = new DataTable();
                dtListaExcepcions = clsRecursosHumanosBL.Instancia.GetListarExcepciones(idConductor, periodo);
                if (dtListaExcepcions.Rows.Count > 0)
                {                   
                   // btnEliminar.Visible = true;
                    for (int i = 0; i < dtListaExcepcions.Rows.Count; i++)
                    {
                        txtMonto.Text = dtListaExcepcions.Rows[0]["Monto"].ToString();
                        txtMotivo.Text = dtListaExcepcions.Rows[0]["Motivo"].ToString();
                        comboBox1.Text = dtListaExcepcions.Rows[0]["TipoBono"].ToString();
                        txtUsuario.Text = dtListaExcepcions.Rows[0]["UsuarioRegistra"].ToString();
                        txtFechaRegistro.Text = dtListaExcepcions.Rows[0]["FechaRegistro"].ToString();

                        if (Convert.ToDecimal(dtListaExcepcions.Rows[0]["Monto"].ToString()) < 0)
                        {
                            radioButton2.Checked = true;
                        }
                        else
                        {
                            radioButton1.Checked = true;
                        }
                    }
                }
            }
            else
            {
                DataTable dtListaExcepcions = new DataTable();
                dtListaExcepcions = clsRecursosHumanosBL.Instancia.GetListarExcepciones(idConductor, periodo);
                if (dtListaExcepcions.Rows.Count > 0)
                {
                    Opcion = 2;
                    btnEliminar.Visible = true;
                    for (int i = 0; i < dtListaExcepcions.Rows.Count; i++)
                    {
                        txtMonto.Text = dtListaExcepcions.Rows[0]["Monto"].ToString();
                        txtMotivo.Text = dtListaExcepcions.Rows[0]["Motivo"].ToString();
                        comboBox1.Text = dtListaExcepcions.Rows[0]["TipoBono"].ToString();
                        txtUsuario.Text = dtListaExcepcions.Rows[0]["UsuarioRegistra"].ToString();
                        txtFechaRegistro.Text = dtListaExcepcions.Rows[0]["FechaRegistro"].ToString();

                        if (Convert.ToDecimal(dtListaExcepcions.Rows[0]["Monto"].ToString()) < 0)
                        {
                            radioButton2.Checked = true;
                        }
                        else
                        {
                            radioButton1.Checked = true;
                        }
                    }
                }  
            }          

                label1.Text = "Conductor: " + conductor;
                lblOperacion.Text = "Operacion: " + Operacion;
        }

        //GERARDO - 01/08
        private void CargarComboMotivo()
        {
            DataTable dtMotivo = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_BonoConductores_ListarMotivos(comboBox1.Text);
            cbxMotivo.DataSource = dtMotivo;
            cbxMotivo.DisplayMember = "Descripcion";
            cbxMotivo.ValueMember = "idMotivoBono";
        }
        //GERARDO - 01/08

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMonto.Text.Length == 0)
            {
                MessageBox.Show("Ingresar Monto...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMonto.Focus();
                return;
            }
            if (txtMotivo.Text.Length == 0)
            {
                MessageBox.Show("Ingresar Motivo...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMotivo.Focus();
                return;
            }
            
            decimal MontoNegativo = Convert.ToDecimal(txtMonto.Text);
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Seleccionar una Opción...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (radioButton2.Checked == true)
            {
                MontoNegativo = -Math.Abs(MontoNegativo);
            }

            string rpta;
            DataTable dtExcepciones = new DataTable();
            dtExcepciones = clsRecursosHumanosBL.Instancia.GetDataBonoRegistrarExcepciones(Opcion,idConductor,comboBox1.Text, Convert.ToInt32(cbxMotivo.SelectedValue), periodo, MontoNegativo, txtMotivo.Text, Utilitario.Instancia.SesionUsuario.usuario);

            rpta = Convert.ToString(dtExcepciones.Rows[0]["exito"]);
            string NrRPTA = rpta.Substring(0, 1);
            if (NrRPTA == "0")
            {
                MessageBox.Show(rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            decimal MontoNegativo = 0;
            string rpta;
            DataTable dtExcepciones = new DataTable();
            dtExcepciones = clsRecursosHumanosBL.Instancia.GetDataBonoRegistrarExcepciones(3, idConductor,"", Convert.ToInt32(cbxMotivo.SelectedValue), periodo, MontoNegativo, "", Utilitario.Instancia.SesionUsuario.usuario);

            rpta = Convert.ToString(dtExcepciones.Rows[0]["exito"]);
            string NrRPTA = rpta.Substring(0, 1);
            if (NrRPTA == "0")
            {
                MessageBox.Show(rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            CargarComboMotivo();
        }
    }
}
