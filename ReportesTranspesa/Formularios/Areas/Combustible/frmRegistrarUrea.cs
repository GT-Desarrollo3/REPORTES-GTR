using Comun;
using Negocio;
using ReportesTranspesa.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class frmRegistrarUrea : Form
    {
        DataTable dtPermisosEspeciales = null;
        public frmRegistrarUrea()
        {
            InitializeComponent();
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstTracto, clsConsultaBL.Instancia.GetUnidades(txtPlaca.Text), true, false, false);

                lstTracto.Columns[0].Width = 0;
                lstTracto.Columns[1].Width = 80;
                lstTracto.Columns[2].Width = 50;
                lstTracto.Columns[3].Width = 120;

                lstTracto.Size = new System.Drawing.Size(260, 103);

                lstTracto.BringToFront();
                lstTracto.Visible = true;
                lstTracto.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstTracto.Visible = false;
                txtPlaca.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTracto.Visible = false;
                txtPlaca.Focus();
                txtPlaca.Tag = null;
            }
        }

        private void lstTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstTracto.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTracto.SelectedItems[0];
                txtPlaca.Tag = Convert.ToInt32(ItemActual.Text);
                txtPlaca.Text = ItemActual.SubItems[1].Text;

                lstTracto.Visible = false;
                txtPlaca.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstTracto.Visible = false;
                txtPlaca.Focus();
            }
        }

        private void lstTracto_Enter(object sender, EventArgs e)
        {
            if (!lstTracto.Items.Count.Equals(0))
            {
                lstTracto.Items[0].Selected = true;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmRegistrarUrea_Load(object sender, EventArgs e)
        {

            anularToolStripMenuItem.Enabled = true;

            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistrarUrea");
            if (dtPermisos.Rows.Count > 0)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                {
                    dtPermisosEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());
                }

                if (dtPermisosEspeciales != null)
                {
                    if (dtPermisosEspeciales.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPermisosEspeciales.Rows.Count; i++)
                        {
                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Anular")
                            {
                                anularToolStripMenuItem.Enabled = true;
                            }
                        }
                    }
                }
            }

            CargarAlmacenes();
            CargarListarUrea();
        }

        private void CargarListarUrea()
        {
            DataTable dtListarUrea = clsCombustibleBL.Instancia.ReportesApp_Combustible_ListarUreaPorAlmacenes(dtpFechaInicio.Text,dtpFechaFin.Text);
            dtgUrea.DataSource = dtListarUrea;
            dgvUreaVista.Columns["idPlaca"].Visible = false;
            dgvUreaVista.Columns["idConductor"].Visible = false;
        }

        private void CargarAlmacenes()
        {
            DataTable dtAlmacenes = clsAlmacenBL.Instancia.ReportesApp_Almacen_ListarAlmacenes();
            cbxSucursalUrea.DataSource = dtAlmacenes;
            cbxSucursalUrea.DisplayMember = "Nombre";
            cbxSucursalUrea.ValueMember = "Codigo";
            cbxSucursalUrea.SelectedIndex = 0;
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                Double cantidad = Convert.ToDouble(txtCantidad.Value);

                if (cantidad < 0.001)
                {
                    MessageBox.Show("La cantidad no puede ser 0.001 o menor", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtPlaca.Tag == null)
                {
                    MessageBox.Show("No a seleccionado una placa", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtPlaca.TextLength < 7)
                {
                    MessageBox.Show("No a seleccionado una placa", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (txtConductor.Tag == null)
                {
                    MessageBox.Show("No a seleccionado un conductor", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (clsCombustibleBL.Instancia.ReportesApp_Combustible_RegistrarUreaPorSucursal(cbxSucursalUrea.SelectedValue.ToString(),Convert.ToInt32(txtPlaca.Tag),txtPlaca.Text,dtpFecha.Text,txtCantidad.Value.ToString(),Convert.ToInt32(txtConductor.Tag)))
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarListarUrea();
                }
                else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CargarListarUrea();
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            //CARGADO DEL CONDUCTOR
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstConductor, clsConsultaBL.Instancia.GetConductores(txtConductor.Text), true, false, false);

                lstConductor.Columns[0].Width = 0;
                lstConductor.Columns[1].Width = 206;
                lstConductor.Columns[2].Width = 0;
                lstConductor.Columns[3].Width = 110;
                
                lstConductor.Size = new System.Drawing.Size(351, 103);
                
                lstConductor.BringToFront();
                lstConductor.Visible = true;
                lstConductor.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstConductor.Visible = false;
                txtConductor.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                txtConductor.Focus();
                txtConductor.Tag = null;
            }
        }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstConductor.SelectedItems[0];
                txtConductor.Tag = Convert.ToInt32(ItemActual.Text);
                txtConductor.Text = ItemActual.SubItems[1].Text;

                lstConductor.Visible = false;
                txtConductor.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstConductor.Visible = false;
                txtConductor.Focus();
            }
        }

        private void lstConductor_Enter(object sender, EventArgs e)
        {
            if (!lstConductor.Items.Count.Equals(0))
            {
                lstConductor.Items[0].Selected = true;
            }
        }

        private void anularToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                 string NotaSalida = dgvUreaVista.GetRowCellValue(dgvUreaVista.FocusedRowHandle, "NotaSalida").ToString();
                 
                if (clsCombustibleBL.Instancia.ReportesApp_Combustible_AnularUrea(NotaSalida))
                 {
                     MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     CargarListarUrea();
                 }
                 else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
