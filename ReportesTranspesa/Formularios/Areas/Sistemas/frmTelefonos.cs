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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Areas.Sistemas
{
    public partial class frmTelefonos : MetroFramework.Forms.MetroForm
    {
        public frmTelefonos()
        {
            InitializeComponent();
        }
        public delegate void CargarTelefonoEventHandler(Boolean EsCorrecto);
        public string imeitransf;
        public char origen;
        private void frmTelefonos_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.GetCelulares(5,0);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
         
        }

        private void btnGrabar_Click(object sender, EventArgs e)
        {
            string marca;
            string modelo;
            string descripcion;
            string accesorios;
            string propietario;
            string fechaAdquisicion;

            marca = txtMarca.Text;
            modelo = txtModelo.Text;
            descripcion = txtDescripcion.Text;
            accesorios = txtAccesorios.Text;
            propietario = cboPropietario.Text;
            fechaAdquisicion = txtFechaAdquisicion.Text;

            DialogResult result = MessageBox.Show("¿Esta seguro de Guardar " + marca + " " + modelo + " " + descripcion + " " + accesorios+" ?", "Consulta", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                //string Respuesta;
                //DataTable dtRespuesta = new DataTable();
                //dtRespuesta = clsSistemasBL.Instancia.GetAsignacionesCel_Update(3, idempleado, IDLinea, Utilitario.Instancia.SesionUsuario.usuario);

                //Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                //MessageBox.Show(Respuesta);
                //btnAceptarLinea.Visible = false;
                //btnCancelarLinea.Visible = false;
                //button3.Enabled = true;
                //button4.Enabled = true;
                //cargarLineas();
            }

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

        }

    }
}
