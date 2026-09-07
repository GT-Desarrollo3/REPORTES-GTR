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
using System.IO;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    public partial class frmRevisionConosTacos : Form
    {
        public int Responsable = -1, idTracto = -1;
        public int Opcion, idCT;
        public byte[] byteArrayImagen = null, byteArrayImagen2 = null;
        public frmListaControlItems frmListaControlItems = new frmListaControlItems();
        
        public frmRevisionConosTacos()
        {
            InitializeComponent();
        }

        public void frmRevisionConosTacos_Load(object sender, EventArgs e) { }


        public void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTracto, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtTracto.Text), true, false, false);
            lstTracto.Columns[0].Width = 0;
            lstTracto.Columns[1].Width = 80;
            lstTracto.Columns[2].Width = 0;
            lstTracto.Columns[3].Width = 0;
            lstTracto.Columns[4].Width = 0;
            lstTracto.Columns[5].Width = 0;

            lstTracto.BringToFront();
            lstTracto.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTracto.Visible = false;
                lstTracto.SendToBack();
                txtTracto.Focus();
                txtOperacion.Clear();
                idTracto = -1;
            }
        }

        public void txtTracto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTracto.Focus(); }
        }

        public void lstTracto_Enter(object sender, EventArgs e)
        {
            if (!lstTracto.Items.Count.Equals(0)) { lstTracto.Items[0].Selected = true; }
        }

        public void lstTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTracto.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTracto.SelectedItems[0];

                idTracto = Int32.Parse(ItemActual.SubItems[0].Text);
                txtTracto.Text = ItemActual.SubItems[1].Text;
                txtOperacion.Text = ItemActual.SubItems[4].Text;

                lstTracto.Visible = false;
                lstTracto.SendToBack();
                cbxTipo.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTracto.Visible = false;
                lstTracto.SendToBack();
                txtTracto.Focus();
                txtOperacion.Clear();
                idTracto = -1;
            }
        }

        public void lstTracto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTracto.SelectedItems[0];

            idTracto = Int32.Parse(ItemActual.SubItems[0].Text);
            txtTracto.Text = ItemActual.SubItems[1].Text;
            txtOperacion.Text = ItemActual.SubItems[4].Text;

            lstTracto.Visible = false;
            lstTracto.SendToBack();
            cbxTipo.Focus();
        }

        public void dtpFechaRevision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtResponsable.Focus(); }
        }

        public void cbxTipo_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxTipo.Text == "CONOS" || cbxTipo.Text == "CINTAS LUMINISCENTES" || cbxTipo.Text == "PARLANTES") { txtCantidad.Text = "2"; }
            
            if (cbxTipo.Text == "TACOS" || cbxTipo.Text == "CORTINAS" || cbxTipo.Text == "CINTURONES") { txtCantidad.Text = "4"; }

            if (cbxTipo.Text == "DUPLICADO" || cbxTipo.Text == "RADIO" || cbxTipo.Text == "BOTIQUÍN" || cbxTipo.Text == "STICKER TANQUE COMB.")
            { txtCantidad.Text = "1"; }

            txtCantidad.Focus();
        }

        public void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxEstado.Focus(); }
        }

        public void cbxEstado_DropDownClosed(object sender, EventArgs e) { txtLugarRevision.Focus(); }

        public void txtLugarRevision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxTipo.Focus(); }
        }

        public void txtResponsable_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtResponsable.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 278;
            lstEmpleado.Columns[2].Width = 100;
            lstEmpleado.Location = new Point(21, 204);
            lstEmpleado.BringToFront();
            lstEmpleado.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                Responsable = -1;
            }
        }

        public void txtResponsable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstEmpleado.Focus(); }
        }

        public void lstEmpleado_Enter(object sender, EventArgs e)
        {
            if (!lstEmpleado.Items.Count.Equals(0)) { lstEmpleado.Items[0].Selected = true; }
        }

        public void lstEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstEmpleado.SelectedItems[0];
                Responsable = Int32.Parse(ItemActual.Text);
                txtResponsable.Text = ItemActual.SubItems[1].Text;

                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                btnBuscarHallazgo.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                txtResponsable.Focus();
                Responsable = -1;
            }
        }

        public void lstEmpleado_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstEmpleado.SelectedItems[0];
            Responsable = Int32.Parse(ItemActual.Text);
            txtResponsable.Text = ItemActual.SubItems[1].Text;

            lstEmpleado.Visible = false;
            lstEmpleado.SendToBack();
            btnBuscarHallazgo.Focus();
        }

        public void dtpFechaReparacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarReparacion.Focus(); }
        }

        private void btnBuscarHallazgo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK)
                {
                    byteArrayImagen = File.ReadAllBytes(getImage.FileName);

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayImagen));
                    pbHallazgo.Image = x;
                    pbHallazgo.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            byteArrayImagen = null;
            pbHallazgo.Image = null;
            pbHallazgo.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnBuscarReparacion_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK)
                {
                    byteArrayImagen2 = File.ReadAllBytes(getImage.FileName);

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayImagen2));
                    pbReparacion.Image = x;
                    pbReparacion.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            byteArrayImagen2 = null;
            pbReparacion.Image = null;
            pbReparacion.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1 || Opcion == 2)
            {
                txtTracto.Clear(); idTracto = -1;
                txtOperacion.Clear();
                dtpFechaRevision.Value = DateTime.Now;
                cbxTipo.Text = "CONOS";
                cbxTipo_DropDownClosed(sender, e);
                txtCantidad.Clear();
                cbxEstado.Text = "OK";
                txtLugarRevision.Clear();
                txtResponsable.Clear(); Responsable = -1;
                btnCerrar_Click(sender, e);
            }

            if (Opcion == 3)
            {
                dtpFechaReparacion.Value = DateTime.Now;
                btnCerrar2_Click(sender, e);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1 || Opcion == 2)
            {
                if (txtTracto.Text.Length == 0 || txtCantidad.Text.Length == 0 || txtResponsable.Text.Length == 0)
                {
                    if (txtTracto.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese la unidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtTracto.Focus();
                    }
                    else
                    {
                        if (txtCantidad.Text.Length == 0)
                        {
                            MessageBox.Show("Por favor, ingrese la cantidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtCantidad.Focus();
                        }
                        else
                        {
                            MessageBox.Show("Por favor, ingrese al responsable de la revisión.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtResponsable.Focus();
                        }
                    }

                    return;
                }
                else
                {
                    DataTable dtRevision = new DataTable();
                    dtRevision = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarEditarCT(Opcion, idCT, idTracto, dtpFechaRevision.Value,
                                                            cbxTipo.Text, Convert.ToInt32(txtCantidad.Text), cbxEstado.Text, txtLugarRevision.Text, Responsable,
                                                            byteArrayImagen, Utilitario.Instancia.SesionUsuario.usuario);
                    string Revision = Convert.ToString(dtRevision.Rows[0]["exito"]);
                    string NroRPTA = Revision.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Revision, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListaControlItems.ListarConosTacos();
                        Close();
                    }
                    else { MessageBox.Show(Revision, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            if (Opcion == 3)
            {
                DataTable dtReparacion = new DataTable();
                dtReparacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RepararCT(idCT, dtpFechaReparacion.Value, byteArrayImagen2, Utilitario.Instancia.SesionUsuario.usuario);
                string Reparacion = Convert.ToString(dtReparacion.Rows[0]["exito"]);
                string NroRPTA = Reparacion.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    MessageBox.Show(Reparacion, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmListaControlItems.ListarConosTacos();
                    Close();
                }
                else { MessageBox.Show(Reparacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
