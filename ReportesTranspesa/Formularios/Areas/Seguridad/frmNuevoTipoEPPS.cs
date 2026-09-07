using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmNuevoTipoEPPS : Form
    {
        byte Estado = 0;
        int TipoEPPS;

        public frmNuevoTipoEPPS()
        {
            InitializeComponent();
        }

        private void frmNuevoTipoEPPS_Shown(object sender, EventArgs e)
        {
            txtNombreEPPS.Focus();
        }

        private void frmNuevoTipoEPPS_Load(object sender, EventArgs e)
        {
            refresh();
        }


        void refresh()
        {
            DataTable DtEPPPS = new DataTable();
            dtgTipoEPPS.DataSource = null;
            DtEPPPS = clsSeguridadBL.Instancia.GetListarTipoEPPS();

            if (dgvTipoEPPS != null)
            {
                dgvTipoEPPS.ClearSelection();
            }

            if (DtEPPPS.Rows.Count > 0)
            {
                dtgTipoEPPS.DataSource = DtEPPPS;
                dgvTipoEPPS.Columns["TipoEPPS"].Visible = false;
                dgvTipoEPPS.BestFitColumns();
            }
        }


        private void button1_Click(object sender, EventArgs e) //REGISTRAR
        {
            Estado = 1;
            DataTable dt = new DataTable();
            string Respuesta;

            if (txtNombreEPPS.Text.Length == 0)
            {
                MessageBox.Show("Ingrese el nombre del EPP.", "Mensaje");
                txtNombreEPPS.Focus();
                return;
            }

            dt = clsSeguridadBL.Instancia.GetRegistrarTipoEPPS(4, TipoEPPS, txtNombreEPPS.Text, Estado);
            Respuesta = Convert.ToString(dt.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresh();
                txtNombreEPPS.Clear();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)    //ANULAR
        {
            DataTable dt = new DataTable();
            string Respuesta;
            int[] filass = dgvTipoEPPS.GetSelectedRows();
            string datoseleccionado = dgvTipoEPPS.GetFocusedValue().ToString();

            for (int i = 0; i < filass.Length; i++)
            {
                TipoEPPS = Convert.ToInt32(dgvTipoEPPS.GetRowCellValue(filass[i], "TipoEPPS").ToString());
            }

            dt = clsSeguridadBL.Instancia.GetRegistrarTipoEPPS(2, TipoEPPS, txtNombreEPPS.Text, Estado);
            Respuesta = Convert.ToString(dt.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresh();
                txtNombreEPPS.Clear();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTipoEPPS_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            string NombreEPPS = txtNombreEPPS.Text;
            int[] filass = dgvTipoEPPS.GetSelectedRows();
            string datoseleccionado = dgvTipoEPPS.GetFocusedValue().ToString();

            for (int i = 0; i < filass.Length; i++)
            {
                txtNombreEPPS.Text = dgvTipoEPPS.GetRowCellValue(filass[i], "Nombre").ToString();
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)  // EDITAR
        {
            DataTable dt = new DataTable();
            string Respuesta;
            string NombreEPPS = txtNombreEPPS.Text;
            int[] filass = dgvTipoEPPS.GetSelectedRows();
            string datoseleccionado = dgvTipoEPPS.GetFocusedValue().ToString();

            for (int i = 0; i < filass.Length; i++)
            {
                TipoEPPS = Convert.ToInt32(dgvTipoEPPS.GetRowCellValue(filass[i], "TipoEPPS").ToString());
                NombreEPPS = dgvTipoEPPS.GetRowCellValue(filass[i], "Nombre").ToString();
            }

            dt = clsSeguridadBL.Instancia.GetRegistrarTipoEPPS(1, TipoEPPS, txtNombreEPPS.Text, Estado);
            Respuesta = Convert.ToString(dt.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresh();
                txtNombreEPPS.Clear();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void activarEstadoToolStripMenuItem_Click_1(object sender, EventArgs e)   //CAMBIAR DE ESTADO 
        {
            DataTable dt = new DataTable();
            string Respuesta;
            string NombreEPPS = txtNombreEPPS.Text;
            int[] filass = dgvTipoEPPS.GetSelectedRows();
            string datoseleccionado = dgvTipoEPPS.GetFocusedValue().ToString();

            for (int i = 0; i < filass.Length; i++)
            {
                TipoEPPS = Convert.ToInt32(dgvTipoEPPS.GetRowCellValue(filass[i], "TipoEPPS").ToString());
                NombreEPPS = dgvTipoEPPS.GetRowCellValue(filass[i], "Nombre").ToString();
            }

            dt = clsSeguridadBL.Instancia.GetRegistrarTipoEPPS(3, TipoEPPS, NombreEPPS, Estado);
            Respuesta = Convert.ToString(dt.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                refresh();
                txtNombreEPPS.Clear();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtNombreEPPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Estado = 1;
                DataTable dt = new DataTable();
                string Respuesta;

                if (txtNombreEPPS.Text.Length == 0)
                {
                    MessageBox.Show("Ingresar el nombre del EPP.", "Mensaje");
                    txtNombreEPPS.Focus();
                    return;
                }

                dt = clsSeguridadBL.Instancia.GetRegistrarTipoEPPS(4, TipoEPPS, txtNombreEPPS.Text, Estado);
                Respuesta = Convert.ToString(dt.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refresh();
                    txtNombreEPPS.Clear();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
