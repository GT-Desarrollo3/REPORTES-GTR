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
    public partial class frmNuevaRevision : Form
    {
        public int Conductor = -1, Inspector = -1, Tecnico = -1, Seguimiento = -1;
        public int idTracto = -1, idCarreta = -1;
        public int Opcionlst1, Opcionlst2, Opcion, idTC;
        public byte[] byteArrayImagen = null, byteArrayImagen2 = null, byteArrayImagen3 = null;
        public frmListaControlItems frmListaControlItems = new frmListaControlItems();

        public frmNuevaRevision()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
            cbxObservacion.SelectedIndexChanged -= cbxObservacion_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void cbxObservacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboObservacion(); }

        private void frmNuevaRevision_Load(object sender, EventArgs e) { }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(4, "");
            cbxOperaciones.DataSource = dtOperacion;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        public void CargarComboObservacion()
        {
            DataTable dtObservacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarObservacionesI(1);
            cbxObservacion.DataSource = dtObservacion;
            cbxObservacion.DisplayMember = "Descripcion";
            cbxObservacion.ValueMember = "idObservacion";
        }


        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e) { dtpFechaRevision.Focus(); }

        private void dtpFechaRevision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxObservacion.Focus(); }
        }

        private void cbxObservacion_DropDownClosed(object sender, EventArgs e) { txtTracto.Focus(); }

        private void txtTracto_Enter(object sender, EventArgs e) { txtTracto.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
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
                idTracto = -1;
            }
        }

        private void txtTracto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTracto.Focus(); }
        }

        private void txtTracto_Leave(object sender, EventArgs e) { txtTracto.BackColor = Color.White; }

        private void lstTracto_Enter(object sender, EventArgs e)
        {
            if (!lstTracto.Items.Count.Equals(0)) { lstTracto.Items[0].Selected = true; }
        }

        private void lstTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTracto.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTracto.SelectedItems[0];

                idTracto = Int32.Parse(ItemActual.SubItems[0].Text);
                txtTracto.Text = ItemActual.SubItems[1].Text;

                lstTracto.Visible = false;
                lstTracto.SendToBack();
                txtCarreta.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTracto.Visible = false;
                lstTracto.SendToBack();
                txtTracto.Focus();
                idTracto = -1;
            }
        }

        private void lstTracto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTracto.SelectedItems[0];

            idTracto = Int32.Parse(ItemActual.SubItems[0].Text);
            txtTracto.Text = ItemActual.SubItems[1].Text;

            lstTracto.Visible = false;
            lstTracto.SendToBack();
            txtCarreta.Focus();
        }

        private void txtCarreta_Enter(object sender, EventArgs e) { txtCarreta.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstCarreta, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtCarreta.Text), true, false, false);
            lstCarreta.Columns[0].Width = 0;
            lstCarreta.Columns[1].Width = 80;
            lstCarreta.Columns[2].Width = 0;
            lstCarreta.Columns[3].Width = 0;
            lstCarreta.Columns[4].Width = 0;
            lstCarreta.Columns[5].Width = 0;
            lstCarreta.BringToFront();
            lstCarreta.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                txtCarreta.Focus();
                idCarreta = -1;
            }
        }

        private void txtCarreta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstCarreta.Focus(); }
        }

        private void txtCarreta_Leave(object sender, EventArgs e) { txtCarreta.BackColor = Color.White; }

        private void lstCarreta_Enter(object sender, EventArgs e)
        {
            if (!lstCarreta.Items.Count.Equals(0)) { lstCarreta.Items[0].Selected = true; }
        }

        private void lstCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstCarreta.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstCarreta.SelectedItems[0];

                idCarreta = Int32.Parse(ItemActual.SubItems[0].Text);
                txtCarreta.Text = ItemActual.SubItems[1].Text;

                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                txtConductor.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                txtCarreta.Focus();
                idCarreta = -1;
            }
        }

        private void lstCarreta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstCarreta.SelectedItems[0];

            idCarreta = Int32.Parse(ItemActual.SubItems[0].Text);
            txtCarreta.Text = ItemActual.SubItems[1].Text;

            lstCarreta.Visible = false;
            lstCarreta.SendToBack();
            txtConductor.Focus();
        }

        private void txtConductor_Enter(object sender, EventArgs e) { txtConductor.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtConductor.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 300;
            lstEmpleado.Columns[2].Width = 110;
            lstEmpleado.Location = new Point(296,184);
            lstEmpleado.BringToFront();
            Opcionlst1 = 1;
            lstEmpleado.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                Conductor = -1;
            }
        }

        private void txtConductor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstEmpleado.Focus(); }
        }

        private void txtConductor_Leave(object sender, EventArgs e) { txtConductor.BackColor = Color.White; }

        private void lstEmpleado_Enter(object sender, EventArgs e)
        {
            if (!lstEmpleado.Items.Count.Equals(0)) { lstEmpleado.Items[0].Selected = true; }
        }

        private void lstEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Opcionlst1 == 1)
            {
                if ((e.KeyChar == (char)Keys.Enter) && !lstEmpleado.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstEmpleado.SelectedItems[0];
                    Conductor = Int32.Parse(ItemActual.Text);
                    txtConductor.Text = ItemActual.SubItems[1].Text;

                    lstEmpleado.Visible = false;
                    lstEmpleado.SendToBack();
                    txtLugarInspeccion.Focus();
                }

                if (e.KeyChar == (char)Keys.Back)
                {
                    lstEmpleado.Visible = false;
                    lstEmpleado.SendToBack();
                    txtConductor.Focus();
                    Conductor = -1;
                }
            }
            
            if (Opcionlst1 == 2)
            {
                if ((e.KeyChar == (char)Keys.Enter) && !lstEmpleado.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstEmpleado.SelectedItems[0];
                    Inspector = Int32.Parse(ItemActual.Text);
                    txtInspector.Text = ItemActual.SubItems[1].Text;

                    lstEmpleado.Visible = false;
                    lstEmpleado.SendToBack();
                    btnBuscarHallazgo.Focus();
                }

                if (e.KeyChar == (char)Keys.Back)
                {
                    lstEmpleado.Visible = false;
                    lstEmpleado.SendToBack();
                    txtInspector.Focus();
                    Inspector = -1;
                }
            }
        }

        private void lstEmpleado_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Opcionlst1 == 1)
            {
                ListViewItem ItemActual;
                ItemActual = lstEmpleado.SelectedItems[0];
                Conductor = Int32.Parse(ItemActual.Text);
                txtConductor.Text = ItemActual.SubItems[1].Text;

                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                txtLugarInspeccion.Focus();
            }

            if (Opcionlst1 == 2)
            {
                ListViewItem ItemActual;
                ItemActual = lstEmpleado.SelectedItems[0];
                Inspector = Int32.Parse(ItemActual.Text);
                txtInspector.Text = ItemActual.SubItems[1].Text;

                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                btnBuscarHallazgo.Focus();
            }
        }

        private void txtLugarInspeccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtInspector.Focus(); }
        }

        private void txtInspector_Enter(object sender, EventArgs e)
        {
            txtInspector.BackColor = Color.FromArgb(192, 255, 192);
        }

        private void txtInspector_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtInspector.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 300;
            lstEmpleado.Columns[2].Width = 110;
            lstEmpleado.Location = new Point(296,249);
            lstEmpleado.BringToFront();
            Opcionlst1 = 2;
            lstEmpleado.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                Inspector = -1;
            }
        }

        private void txtInspector_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstEmpleado.Focus(); }
        }

        private void txtInspector_Leave(object sender, EventArgs e)
        {
            txtInspector.BackColor = Color.White;
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

        private void btnBuscarHallazgo2_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK)
                {
                    byteArrayImagen3 = File.ReadAllBytes(getImage.FileName);

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayImagen3));
                    pbHallazgo2.Image = x;
                    pbHallazgo2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            byteArrayImagen3 = null;
            pbHallazgo2.Image = null;
            pbHallazgo2.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1 || Opcion == 2)
            {
                cbxOperaciones.Text = "LINDLEY";
                cbxObservacion.Text = "OK";
                dtpFechaRevision.Value = DateTime.Now;
                txtTracto.Clear(); idTracto = -1;
                txtCarreta.Clear(); idCarreta = -1;
                txtConductor.Clear(); Conductor = -1;
                txtLugarInspeccion.Clear();
                txtInspector.Clear(); Inspector = -1;
                btnCerrar_Click(sender, e);
                btnCerrar3_Click(sender, e);
            }

            if (Opcion == 3)
            {
                txtTecnico.Clear(); Tecnico = -1;
                txtResponsable.Clear(); Seguimiento = -1;
                dtpFechaReparacion.Value = DateTime.Now;
                btnCerrar2_Click(sender, e);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1 || Opcion == 2)
            {
                if ((txtTracto.Text.Length == 0 && txtCarreta.Text.Length == 0) || txtConductor.Text.Length == 0 || txtLugarInspeccion.Text.Length == 0 || txtInspector.Text.Length == 0)
                {
                    if (txtTracto.Text.Length == 0 && txtCarreta.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese el tracto y/o el semirremolque.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtTracto.Focus();
                    }
                    else
                    {
                        if (txtConductor.Text.Length == 0)
                        {
                            MessageBox.Show("Por favor, ingrese un conductor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtConductor.Focus();
                        }
                        else
                        {
                            if (txtLugarInspeccion.Text.Length == 0)
                            {
                                MessageBox.Show("Por favor, ingrese el lugar de inspección.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtLugarInspeccion.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Por favor, ingrese el lugar de inspección.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                txtLugarInspeccion.Focus();
                            }
                        }
                    }

                    return;
                }
                else
                {
                    DataTable dtRevision = new DataTable();
                    dtRevision = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarEditarTanques(Opcion, idTC, cbxOperaciones.Text, dtpFechaRevision.Value, idTracto, idCarreta, Conductor,
                                                                                                 txtLugarInspeccion.Text, Inspector, cbxObservacion.Text, byteArrayImagen, byteArrayImagen3, Utilitario.Instancia.SesionUsuario.usuario);
                    string Revision = Convert.ToString(dtRevision.Rows[0]["exito"]);
                    string NroRPTA = Revision.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Revision, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListaControlItems.ListarTanquesCombustible();
                        Close();
                    }
                    else { MessageBox.Show(Revision, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            if (Opcion == 3)
            {
                if (txtTecnico.Text.Length == 0 || txtResponsable.Text.Length == 0)
                {
                    if (txtTecnico.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese el nombre del técnico.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtTecnico.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese el nombre del responsable del seguimiento.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtResponsable.Focus();
                    }
                }
                else
                {
                    DataTable dtReparacion = new DataTable();
                    dtReparacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarReparacion(idTC, Tecnico, Seguimiento, dtpFechaReparacion.Value,
                                                              byteArrayImagen2, Utilitario.Instancia.SesionUsuario.usuario);
                    string Reparacion = Convert.ToString(dtReparacion.Rows[0]["exito"]);
                    string NroRPTA = Reparacion.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Reparacion, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListaControlItems.ListarTanquesCombustible();
                        Close();
                    }
                    else { MessageBox.Show(Reparacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void txtTecnico_Enter(object sender, EventArgs e) { txtTecnico.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtTecnico_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTecnico, clsConsultaBL.Instancia.GetEmpleado(txtTecnico.Text), true, false, false);
            lstTecnico.Columns[0].Width = 0;
            lstTecnico.Columns[1].Width = 300;
            lstTecnico.Columns[2].Width = 110;
            lstTecnico.Location = new Point(644,122);
            lstTecnico.BringToFront();
            Opcionlst2 = 1;
            lstTecnico.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTecnico.Visible = false;
                lstTecnico.SendToBack();
                Tecnico = -1;
            }
        }

        private void txtTecnico_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTecnico.Focus(); }
        }

        private void txtTecnico_Leave(object sender, EventArgs e) { txtTecnico.BackColor = Color.White; }

        private void lstTecnico_Enter(object sender, EventArgs e)
        {
            if (!lstTecnico.Items.Count.Equals(0)) { lstTecnico.Items[0].Selected = true; }
        }

        private void lstTecnico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Opcionlst2 == 1)
            {
                if ((e.KeyChar == (char)Keys.Enter) && !lstTecnico.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstTecnico.SelectedItems[0];
                    Tecnico = Int32.Parse(ItemActual.Text);
                    txtTecnico.Text = ItemActual.SubItems[1].Text;

                    lstTecnico.Visible = false;
                    lstTecnico.SendToBack();
                    txtResponsable.Focus();
                }

                if (e.KeyChar == (char)Keys.Back)
                {
                    lstTecnico.Visible = false;
                    lstTecnico.SendToBack();
                    txtTecnico.Focus();
                    Tecnico = -1;
                }
            }

            if (Opcionlst2 == 2)
            {
                if ((e.KeyChar == (char)Keys.Enter) && !lstTecnico.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstTecnico.SelectedItems[0];
                    Seguimiento = Int32.Parse(ItemActual.Text);
                    txtResponsable.Text = ItemActual.SubItems[1].Text;

                    lstTecnico.Visible = false;
                    lstTecnico.SendToBack();
                    dtpFechaReparacion.Focus();
                }

                if (e.KeyChar == (char)Keys.Back)
                {
                    lstTecnico.Visible = false;
                    lstTecnico.SendToBack();
                    txtResponsable.Focus();
                    Seguimiento = -1;
                }
            }
        }

        private void lstTecnico_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Opcionlst2 == 1)
            {
                ListViewItem ItemActual;
                ItemActual = lstTecnico.SelectedItems[0];
                Tecnico = Int32.Parse(ItemActual.Text);
                txtTecnico.Text = ItemActual.SubItems[1].Text;

                lstTecnico.Visible = false;
                lstTecnico.SendToBack();
                txtResponsable.Focus();
            }

            if (Opcionlst2 == 2)
            {
                ListViewItem ItemActual;
                ItemActual = lstTecnico.SelectedItems[0];
                Seguimiento = Int32.Parse(ItemActual.Text);
                txtResponsable.Text = ItemActual.SubItems[1].Text;

                lstTecnico.Visible = false;
                lstTecnico.SendToBack();
                dtpFechaReparacion.Focus();
            }
        }

        private void txtResponsable_Enter(object sender, EventArgs e) { txtResponsable.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtResponsable_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTecnico, clsConsultaBL.Instancia.GetEmpleado(txtResponsable.Text), true, false, false);
            lstTecnico.Columns[0].Width = 0;
            lstTecnico.Columns[1].Width = 300;
            lstTecnico.Columns[2].Width = 110;
            lstTecnico.Location = new Point(644,184);
            lstTecnico.BringToFront();
            Opcionlst2 = 2;
            lstTecnico.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTecnico.Visible = false;
                lstTecnico.SendToBack();
                Seguimiento = -1;
            }
        }

        private void txtResponsable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTecnico.Focus(); }
        }

        private void txtResponsable_Leave(object sender, EventArgs e) { txtResponsable.BackColor = Color.White; }

        private void dtpFechaReparacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscarReparacion.Focus(); }
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
    }
}
