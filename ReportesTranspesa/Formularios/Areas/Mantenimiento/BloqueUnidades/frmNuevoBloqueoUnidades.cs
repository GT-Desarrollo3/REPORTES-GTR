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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.BloqueUnidades
{
    public partial class frmNuevoBloqueoUnidades : Form
    {
        string _area;
        int _idunidad,_accion,_IdBloqueo;
        string _placa;
        int predeterminado;
        string _motivo;

        public frmNuevoBloqueoUnidades()
        {
            InitializeComponent();
        }

        public void setearvariable(int Accion,int IdBloqueo,string Area, int IdUnidad,string Placa,string Motivo)
        {
            _accion = Accion;
            _area = Area;
            _idunidad = IdUnidad;
            _placa = Placa;
            _IdBloqueo = IdBloqueo;
            _motivo = Motivo;
        }
        private void frmNuevoBloqueoUnidades_Load(object sender, EventArgs e)
        {
            label1.Text = "BLOQUEAR UNIDAD";
            txtArea.Text = _area;
            txtUnidad.Text = _placa;
            btGuardar.Text = "Bloquear";
            predeterminado = 0;
            label5.Text = "Usuario Bloquea:";
            label6.Text = "Fecha Bloquea:";

            dtpFechaInicio.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpFechaInicio.Format = DateTimePickerFormat.Custom;
            dtpFechaFin.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dtpFechaFin.Format = DateTimePickerFormat.Custom;  
                                  

            txtUsuario.Text = Utilitario.Instancia.SesionUsuario.usuario;
            txtFecha.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            
            //LISTADO DE AREA ASIGNADA POR USUSARIO
           DataTable dtArea= new DataTable();

           dtArea = clsOperacionesBL.Instancia.GetOperaciones_ListarMotivoBloqueo(Utilitario.Instancia.SesionUsuario.usuario);

            if (dtArea.Rows.Count > 0)
            {
                for (int i = 0; i < dtArea.Rows.Count; i++)
                {
                    txtArea.Text = dtArea.Rows[0]["AREAS"].ToString();
                }
            }

            //LISTADO DE MOTIVOS ASIGNADO POR USUARIO
            DataTable DTMOTIVOS = new DataTable();

            DTMOTIVOS = clsOperacionesBL.Instancia.GetOperaciones_ListarMotivoBloqueo(Utilitario.Instancia.SesionUsuario.usuario);

            if (DTMOTIVOS.Rows.Count>0)
            {
                comboBox1.DisplayMember = "Descripcion";
                comboBox1.ValueMember = "Item";
               // comboBox1.DataSource = dtTipoProg;
                comboBox1.DataSource= DTMOTIVOS;
            }

            if (_accion == 2)
            {
                comboBox1.Text = _motivo;
                comboBox1.Enabled = false;
                label1.Text = "DESBLOQUEAR UNIDAD";
                dtpFechaInicio.Visible = false;
                dtpFechaFin.Visible = false;
                chbTiempo.Visible = false;
                label8.Visible = false;
                label9.Visible = false;
                btGuardar.Text = "Desbloquear";
                label5.Text = "Usuario Desbloquea:";
                label6.Text = "Fecha Desbloquea:";
            }
        }

        private void btGuardar_Click(object sender, EventArgs e)
        {  
            if (txtUnidad.Text.Equals(""))
            {
                MessageBox.Show("Debe seleccionar Unidad.");
            }
            else 
            {
                string RPTA;
                DataTable dtRegistroBloqueo = new DataTable();
                dtRegistroBloqueo = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(_accion, _IdBloqueo, 0, 0, _idunidad, comboBox1.Text, txtArea.Text,
                                                                                                    txtDescripcion.Text, Utilitario.Instancia.SesionUsuario.usuario,predeterminado,
                                                                                                     dtpFechaInicio.Text, dtpFechaFin.Text);

                RPTA =Convert.ToString(dtRegistroBloqueo.Rows[0]["Exito"]);
                string Respuesta = RPTA.Substring(0, 1);

                if (Respuesta == "0")
                {
                    MessageBox.Show(RPTA, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else 
                {
                    MessageBox.Show(RPTA, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chbTiempo_CheckedChanged(object sender, EventArgs e)
        {
            if (chbTiempo.Checked == true)
            {
                dtpFechaFin.Enabled = true;
                dtpFechaInicio.Enabled = true;
                label8.Enabled = true;
                label9.Enabled = true;
                predeterminado = 1;
            }
            else             
            {
                dtpFechaFin.Enabled = false;
                dtpFechaInicio.Enabled = false;
                label8.Enabled = false;
                label9.Enabled = false;
                predeterminado = 0;
            }
        }
    }
}
