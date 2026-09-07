using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.Data;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Sistemas
{
    public partial class frmModificarAsignacion : MetroFramework.Forms.MetroForm
    {
        public frmModificarAsignacion()
        {
            InitializeComponent();
        }
        public int idempleado;
        public string empleado;
        private void frmModificarAsignacion_Load(object sender, EventArgs e)
        {
            lblIDEmpleado.Text =  idempleado.ToString();
            lblEmpleado.Text = "       " + empleado;
            cargarEquipos();
            cargarLineas();
            cargarRadios();

            if (Utilitario.Instancia.SesionUsuario.usuario == "JROJAS" || Utilitario.Instancia.SesionUsuario.usuario == "GREYES" || Utilitario.Instancia.SesionUsuario.usuario == "SCHAVEZ" || Utilitario.Instancia.SesionUsuario.usuario == "DALLING")
            {

            }
            else
            {
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button5.Visible = false;
                button6.Visible = false;
            }
        }

        private void cargarEquipos()
        {
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.GetAsignacionesCel_Consultas(1, idempleado);
            if (dt.Rows.Count > 0)
            {
                dgvEquipos.DataSource = dt;
                dgvEquipos.Columns["IDEquipo"].Visible = false;
                dgvEquipos.AutoResizeRows();
            }
            else
            {
                //Mensaje m = new Mensaje();
                //m.mensaje = "No hay data para mostrar en Equipos";
                //m.ShowDialog();
                dgvEquipos.DataSource = null;

            }
        
        }

        private void cargarLineas()
        {
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.GetAsignacionesCel_Consultas(2, idempleado);
            if (dt.Rows.Count > 0)
            {
                dgvLineas.DataSource = dt;
                dgvLineas.Columns["IDLinea"].Visible = false;
                dgvLineas.AutoResizeRows();
            }
            else
            {
                dgvLineas.DataSource = null;
                //Mensaje m = new Mensaje();
                //m.mensaje = "No hay data para mostrar en Lineas";
                //m.ShowDialog();
            }

        }

        private void cargarRadios()
        {
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.GetAsignacionesCel_Consultas(7, idempleado);
            if (dt.Rows.Count > 0)
            {
                dgvRadios.DataSource = dt;
                dgvRadios.Columns["IDRadio"].Visible = false;
                dgvRadios.AutoResizeRows();
            }
            else
            {
                dgvRadios.DataSource = null;
                //Mensaje m = new Mensaje();
                //m.mensaje = "No hay data para mostrar en Lineas";
                //m.ShowDialog();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.GetAsignacionesCel_Consultas(3, idempleado);
            if (dt.Rows.Count > 0)
            {
                dgvEquipos.DataSource = dt;
                dgvEquipos.AutoResizeRows();
                btnAceptarEquipo.Visible = true;
                btnCancelarEquipo.Visible = true;
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.GetAsignacionesCel_Consultas(4, idempleado);
            if (dt.Rows.Count > 0)
            {
                dgvLineas.DataSource = dt;
                dgvLineas.AutoResizeRows();
                btnAceptarLinea.Visible = true;
                btnCancelarLinea.Visible = true;
                button3.Enabled = false;
                button4.Enabled = false;
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void btnCancelarEquipo_Click(object sender, EventArgs e)
        {
            btnAceptarEquipo.Visible = false;
            btnCancelarEquipo.Visible = false;
            button1.Enabled = true;
            button2.Enabled = true;
            cargarEquipos();
        }

        private void btnCancelarLinea_Click(object sender, EventArgs e)
        {
            btnAceptarLinea.Visible = false;
            btnCancelarLinea.Visible = false;
            button3.Enabled = true;
            button4.Enabled = true;
            cargarLineas();
        }

        private void button2_Click(object sender, EventArgs e)
        {
 
            string Marca;
            string Descripcion;
            int IDEquipo;

            IDEquipo = Convert.ToInt32(dgvEquipos.CurrentRow.Cells[0].Value.ToString());
            Marca = dgvEquipos.CurrentRow.Cells[1].Value.ToString();
            Descripcion = dgvEquipos.CurrentRow.Cells[2].Value.ToString();

            DialogResult result = MessageBox.Show("¿Esta seguro Que desea desvincular el Equipo "+Marca+" "+Descripcion+ " de:"+lblEmpleado.Text+" ?","Consulta", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string Respuesta;
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsSistemasBL.Instancia.GetAsignacionesCel_Update(2, idempleado, IDEquipo, Utilitario.Instancia.SesionUsuario.usuario);

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                MessageBox.Show(Respuesta);
                cargarEquipos();

            }

        }

        private void btnAceptarEquipo_Click(object sender, EventArgs e)
        {

            string Marca;
            string Modelo;
            string Descripcion;
            string Asignada;
            int IDEquipo;

            IDEquipo = Convert.ToInt32(dgvEquipos.CurrentRow.Cells[0].Value.ToString());
            Marca = dgvEquipos.CurrentRow.Cells[1].Value.ToString();
            Modelo = dgvEquipos.CurrentRow.Cells[2].Value.ToString();
            Descripcion = dgvEquipos.CurrentRow.Cells[3].Value.ToString();
            Asignada = dgvEquipos.CurrentRow.Cells[6].Value.ToString();

            if(Asignada=="ASIGNADA")
            {
             MessageBox.Show("No puede vincular un equipo Asignado. Elija otro o cancele","Aviso");
             return;
            }

            DialogResult result = MessageBox.Show("¿Esta seguro Que desea Vincular el Equipo " + Marca + " " + Descripcion +" "+ Modelo + " con:" + lblEmpleado.Text + " ?", "Consulta", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string Respuesta;
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsSistemasBL.Instancia.GetAsignacionesCel_Update(1, idempleado, IDEquipo, Utilitario.Instancia.SesionUsuario.usuario);

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                MessageBox.Show(Respuesta);
                btnAceptarEquipo.Visible = false;
                btnCancelarEquipo.Visible = false;
                button1.Enabled = true;
                button2.Enabled = true;
                cargarEquipos();


            }


        }

        private void btnAceptarLinea_Click(object sender, EventArgs e)
        {

            string Numero;
            string Tarifa;
            string Imei;
            string Empresa;
            string Asignada;
            int IDLinea;

            IDLinea = Convert.ToInt32(dgvLineas.CurrentRow.Cells[0].Value.ToString());
            Numero = dgvLineas.CurrentRow.Cells[2].Value.ToString();
            Tarifa = dgvLineas.CurrentRow.Cells[3].Value.ToString();
            Imei = dgvLineas.CurrentRow.Cells[4].Value.ToString();
            Empresa = dgvLineas.CurrentRow.Cells[1].Value.ToString();
            Asignada = dgvLineas.CurrentRow.Cells[7].Value.ToString();

            if (Asignada == "ASIGNADA")
            {
                MessageBox.Show("No puede vincular una linea Asignada. Elija otra o cancele", "Aviso");
                return;
            }

            DialogResult result = MessageBox.Show("¿Esta seguro Que desea Vincular la Linea "+Empresa +" "+ Numero + " " + Tarifa + " " + Imei + " con:" + lblEmpleado.Text + " ?", "Consulta", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string Respuesta;
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsSistemasBL.Instancia.GetAsignacionesCel_Update(3, idempleado, IDLinea, Utilitario.Instancia.SesionUsuario.usuario);

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                MessageBox.Show(Respuesta);
                btnAceptarLinea.Visible = false;
                btnCancelarLinea.Visible = false;
                button3.Enabled = true;
                button4.Enabled = true;
                cargarLineas();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string Numero;
            string Tarifa;
            string Tipo;
            string Imei;
            string Empresa;
            int IDLinea;

            IDLinea = Convert.ToInt32(dgvLineas.CurrentRow.Cells[0].Value.ToString());
            Numero = dgvLineas.CurrentRow.Cells[1].Value.ToString();
            Tipo = dgvLineas.CurrentRow.Cells[2].Value.ToString();
            Tarifa = dgvLineas.CurrentRow.Cells[5].Value.ToString();
            Imei = dgvLineas.CurrentRow.Cells[4].Value.ToString();
            Empresa = dgvLineas.CurrentRow.Cells[3].Value.ToString();

            DialogResult result = MessageBox.Show("¿Esta seguro Que desea desvincular la Linea "+ Empresa +" "+ Numero + " " + Tipo + " " + Tarifa + " de:" + lblEmpleado.Text + " ?", "Consulta", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string Respuesta;
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsSistemasBL.Instancia.GetAsignacionesCel_Update(4, idempleado, IDLinea, Utilitario.Instancia.SesionUsuario.usuario);

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                MessageBox.Show(Respuesta);
                cargarLineas();

            }
        }

        private void dgvEquipos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvEquipos_Click(object sender, EventArgs e)
        {
            dgvEquipos.Size = new Size(809, 250);
            dgvLineas.Size = new Size(809, 133);
            dgvRadios.Size = new Size(809, 133);
        }

        private void dgvLineas_Click(object sender, EventArgs e)
        {
            dgvEquipos.Size = new Size(809, 133);
            dgvLineas.Size = new Size(809, 250);
            dgvRadios.Size = new Size(809, 133);
        }

        private void btnCancelarRadio_Click(object sender, EventArgs e)
        {
            btnAceptarRadio.Visible = false;
            btnCancelarRadio.Visible = false;
            button5.Enabled = true;
            button6.Enabled = true;
            cargarLineas();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.GetAsignacionesCel_Consultas(6, idempleado);
            if (dt.Rows.Count > 0)
            {
                dgvRadios.DataSource = dt;
                dgvRadios.AutoResizeRows();
                btnAceptarRadio.Visible = true;
                btnCancelarRadio.Visible = true;
                button6.Enabled = false;
                button5.Enabled = false;
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void btnAceptarRadio_Click(object sender, EventArgs e)
        {
            string Marca;
            string Modelo;
            string Descripcion;
            string Asignada;
            int IDRadio;

            IDRadio = Convert.ToInt32(dgvRadios.CurrentRow.Cells[0].Value.ToString());
            Marca = dgvRadios.CurrentRow.Cells[1].Value.ToString();
            Modelo = dgvRadios.CurrentRow.Cells[2].Value.ToString();
            Descripcion = dgvRadios.CurrentRow.Cells[3].Value.ToString();
            Asignada = dgvRadios.CurrentRow.Cells[6].Value.ToString();

            if (Asignada == "ASIGNADA")
            {
                MessageBox.Show("No puede vincular una Radio Asignada. Elija otro o cancele", "Aviso");
                return;
            }

            DialogResult result = MessageBox.Show("¿Esta seguro Que desea Vincular el Equipo " + Marca + " " + Descripcion + " " + Modelo + " con:" + lblEmpleado.Text + " ?", "Consulta", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string Respuesta;
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsSistemasBL.Instancia.GetAsignacionesCel_Update(5, idempleado, IDRadio, Utilitario.Instancia.SesionUsuario.usuario);

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                MessageBox.Show(Respuesta);
                btnAceptarRadio.Visible = false;
                btnCancelarRadio.Visible = false;
                button6.Enabled = true;
                button5.Enabled = true;
                cargarRadios();


            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Marca;
            string Descripcion;
            int IDRadio;

            IDRadio = Convert.ToInt32(dgvRadios.CurrentRow.Cells[0].Value.ToString());
            Marca = dgvRadios.CurrentRow.Cells[1].Value.ToString();
            Descripcion = dgvRadios.CurrentRow.Cells[2].Value.ToString();

            DialogResult result = MessageBox.Show("¿Esta seguro Que desea desvincular el Equipo " + Marca + " " + Descripcion + " de:" + lblEmpleado.Text + " ?", "Consulta", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                string Respuesta;
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsSistemasBL.Instancia.GetAsignacionesCel_Update(6, idempleado, IDRadio, Utilitario.Instancia.SesionUsuario.usuario);

                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                MessageBox.Show(Respuesta);
                cargarRadios();

            }
        }


    }
}
