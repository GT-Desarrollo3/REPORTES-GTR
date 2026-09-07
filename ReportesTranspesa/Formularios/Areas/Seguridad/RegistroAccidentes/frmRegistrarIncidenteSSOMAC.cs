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

namespace ReportesTranspesa.Formularios.Areas.Seguridad.RegistroAccidentes
{
    public partial class frmRegistrarIncidenteSSOMAC : Form
    {
        public string Grado;
        public int DatosUnidad, Persona = -1, Opcion, idRegistroInc;
        public int idTracto, idCarreta;
        public int xClick = 0, yClick = 0;
        public byte[] byteArrayImagen = null;
        public byte[] byteArrayImagen2 = null;
        public frmListaIncidentesSSOMAC frmListaIncidentesSSOMAC = new frmListaIncidentesSSOMAC();

        public frmRegistrarIncidenteSSOMAC()
        {
            InitializeComponent();
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
            cbxSede.SelectedIndexChanged -= cbxSede_SelectedIndexChanged;
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
            cbxTipoIncidente.SelectedIndexChanged -= cbxTipoIncidente_SelectedIndexChanged;
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void cbxSede_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSede(); }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void cbxTipoIncidente_SelectedIndexChanged(object sender, EventArgs e) { CargarComboIncidente(); }

        private void frmRegistrarIncidenteSSOMAC_Load(object sender, EventArgs e) { }


        public void CargarComboArea()
        {
            DataTable dtArea = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(4, "");
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "description";
            cbxArea.ValueMember = "department";
        }

        public void CargarComboSede()
        {
            DataTable dtSede = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_BuscarSede(Utilitario.Instancia.SesionUsuario.usuario, 3);
            cbxSede.DataSource = dtSede;
            cbxSede.DisplayMember = "Descripcion";
            cbxSede.ValueMember = "Sucursal";
        }

        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperacion;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        public void CargarComboIncidente()
        {
            DataTable dtIncidente = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ListarAccidentes(1);
            cbxTipoIncidente.DataSource = dtIncidente;
            cbxTipoIncidente.DisplayMember = "Descripcion";
            cbxTipoIncidente.ValueMember = "idAccidente";
        }


        private void txtEmpleado_Enter(object sender, EventArgs e) { txtEmpleado.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtEmpleado.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 300;
            lstEmpleado.Columns[2].Width = 110;
            lstEmpleado.BringToFront();
            lstEmpleado.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                Persona = -1;
            }
        }

        private void txtEmpleado_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstEmpleado.Focus(); }
        }

        private void txtEmpleado_Leave(object sender, EventArgs e) { txtEmpleado.BackColor = Color.White; }

        private void lstEmpleado_Enter(object sender, EventArgs e)
        {
            if (!lstEmpleado.Items.Count.Equals(0)) { lstEmpleado.Items[0].Selected = true; }
        }

        private void lstEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstEmpleado.SelectedItems[0];
                Persona = Int32.Parse(ItemActual.Text);
                txtEmpleado.Text = ItemActual.SubItems[1].Text;

                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                cbxArea.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                txtEmpleado.Focus();
                Persona = -1;
            }
        }

        private void lstEmpleado_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstEmpleado.SelectedItems[0];
            Persona = Int32.Parse(ItemActual.Text);
            txtEmpleado.Text = ItemActual.SubItems[1].Text;

            lstEmpleado.Visible = false;
            lstEmpleado.SendToBack();
            cbxArea.Focus();
        }

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
                cbxOperaciones.Focus();
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
            cbxOperaciones.Focus();
        }

        public void rbLeve_Click(object sender, EventArgs e) { Grado = "LEVE"; }

        public void rbModerada_Click(object sender, EventArgs e) { Grado = "MODERADA"; }

        public void rbGrave_Click(object sender, EventArgs e) { Grado = "GRAVE"; }

        public void cbDatosUnidad_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDatosUnidad.Checked == true)
            {
                DatosUnidad = 1;
                groupBox2.Enabled = true;
            }
            
            if (cbDatosUnidad.Checked == false)
            {
                DatosUnidad = 0;
                groupBox2.Enabled = false;
                txtTracto.Clear();
                txtCarreta.Clear();
                idTracto = -1; idCarreta = -1;
                cbxOperaciones.Text = "LINDLEY";
            }
        }

        private void btnBuscarAccidente_Click(object sender, EventArgs e)
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
                    pbIncidente.Image = x;
                    pbIncidente.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            byteArrayImagen = null;
            pbIncidente.Image = null;
            pbIncidente.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnBuscarImagen2_Click(object sender, EventArgs e)
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
                    pbIncidente2.Image = x;
                    pbIncidente2.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            byteArrayImagen2 = null;
            pbIncidente2.Image = null;
            pbIncidente2.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Persona = -1;
            txtEmpleado.Clear();
            cbxArea.Text = "NINGUNA";
            cbxSede.Text = "Larrea2";

            rbLeve.Checked = true;
            rbLeve_Click(sender, e);
            dtpFechaInicio.Value = DateTime.Now;
            dtpHoraInicio.Value = DateTime.Now;
            cbDatosUnidad.Checked = false;
            cbDatosUnidad_CheckedChanged(sender, e);

            txtDIncidente.Clear();
            cbxTipoDanio.Text = "NINGUNO";
            txtObservacion.Clear();
            byteArrayImagen = null;
            pbIncidente.Image = null;
            byteArrayImagen2 = null;
            pbIncidente2.Image = null;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtDIncidente.Text.Length == 0 || txtObservacion.Text.Length == 0)
            {
                if (txtDIncidente.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, describa el incidente a registrar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDIncidente.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese la observación.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDIncidente.Focus();
                }

                return;
            }
            else
            {
                DataTable dtIncidente = new DataTable();
                string RIncidente;
                DataTable dtBloqueoUnidad = new DataTable();
                string BloqueoUnidad;

                if (DatosUnidad == 1)
                {
                    dtIncidente = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_InsertarModificarIncidencias(Opcion, idRegistroInc, Persona, txtEmpleado.Text, cbxArea.Text,
                                                                                                 cbxSede.Text, Grado, dtpFechaInicio.Text, dtpHoraInicio.Text, Convert.ToInt32(cbxTipoIncidente.SelectedValue),
                                                                                                 cbxTipoDanio.Text, txtTracto.Text, txtCarreta.Text, cbxOperaciones.Text, txtDIncidente.Text, txtObservacion.Text,
                                                                                                 byteArrayImagen, byteArrayImagen2, Utilitario.Instancia.SesionUsuario.usuario);

                    if (txtTracto.Text.Length > 0)
                    {
                        dtBloqueoUnidad = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(1, 0, 0, 0, idTracto, "INCIDENTE", "SEGURIDAD",  txtDIncidente.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                                                                                        0, dtpFechaInicio.Text, dtpFechaInicio.Text);
                    }

                    if (txtCarreta.Text.Length > 0)
                    {
                        dtBloqueoUnidad = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(1, 0, 0, 0, idCarreta, "INCIDENTE", "SEGURIDAD", txtDIncidente.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                                                                                        0, dtpFechaInicio.Text, dtpFechaInicio.Text);
                    }
                }
                else
                {
                    dtIncidente = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_InsertarModificarIncidencias(Opcion, idRegistroInc, Persona, txtEmpleado.Text, cbxArea.Text,
                                                                                                 cbxSede.Text, Grado, dtpFechaInicio.Text, dtpHoraInicio.Text, Convert.ToInt32(cbxTipoIncidente.SelectedValue),
                                                                                                 cbxTipoDanio.Text, txtTracto.Text, txtCarreta.Text, " ", txtDIncidente.Text, txtObservacion.Text,
                                                                                                 byteArrayImagen, byteArrayImagen2, Utilitario.Instancia.SesionUsuario.usuario);
                }
                RIncidente = Convert.ToString(dtIncidente.Rows[0]["exito"]);
                string NroRPTA = RIncidente.Substring(0, 1);

                if (DatosUnidad == 1)
                {
                    BloqueoUnidad = Convert.ToString(dtBloqueoUnidad.Rows[0]["exito"]);
                    string NroRPTA2 = BloqueoUnidad.Substring(0, 1);

                    if (NroRPTA2 == "0")
                    { MessageBox.Show(BloqueoUnidad, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else { MessageBox.Show(BloqueoUnidad, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                
                if (NroRPTA == "0") { MessageBox.Show(RIncidente, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                else { MessageBox.Show(RIncidente, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                
                frmListaIncidentesSSOMAC.ListarIncidencias();
                Close();
            }
        }

        private void pNuevo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevo.Left = pNuevo.Left + (e.X - xClick);
                pNuevo.Top = pNuevo.Top + (e.Y - yClick);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            pNuevo.Visible = true;
            pNuevo.BringToFront();
        }

        private void btnCerrarNuevo_Click(object sender, EventArgs e)
        {
            pNuevo.Visible = false;
            txtIncidente.Clear();
            pNuevo.SendToBack();
        }

        private void btnNuevoIncidente_Click(object sender, EventArgs e)
        {
            if (txtIncidente.Text.Length == 0)
            {
                MessageBox.Show("Ingrese un tipo de incidente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtIncidente.Focus();
                return;
            }
            else
            {
                DataTable dtNuevo = new DataTable();
                string respta;

                dtNuevo = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_InsertarIncidente(txtIncidente.Text);
                respta = Convert.ToString(dtNuevo.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtIncidente.Clear();
                    CargarComboIncidente();
                    btnCerrarNuevo_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
