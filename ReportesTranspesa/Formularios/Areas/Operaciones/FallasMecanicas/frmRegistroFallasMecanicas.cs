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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    public partial class frmRegistroFallasMecanicas : Form
    {
        int _NroProgramacion;
        string _Usuario, TipoFalla;
        public int xClick = 0, yClick = 0;
        byte[] byteArrayImagen = null, byteArrayFalla = null;

        public int Persona, idRuta, idTracto, idCarreta, idConductor;
        public int BloqueoC, BloqueoU;


        public frmRegistroFallasMecanicas()
        {
            InitializeComponent();
            cbxTipoAuxilio.SelectedIndexChanged -= cbxTipoAuxilio_SelectedIndexChanged;
        }

        private void cbxTipoAuxilio_SelectedIndexChanged(object sender, EventArgs e) { CargarComboAuxilio(); }

        private void frmRegistroFallasMecanicas_Shown(object sender, EventArgs e) { txtMotivo.Focus(); }

        private void frmRegistroFallasMecanicas_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now;
            dtpHoraInicio.Value = DateTime.Now;
            dtpFechaViaje.Value = DateTime.Now;
            CargarComboAuxilio();

            if (groupBox1.Enabled == true) { ListarRegistro(); }
            
            rbFallaParada.Checked = true;
            rbFallaParada_Click(sender, e);
        }


        public void EnviarDatos(int NroProgramacion, string Usuario)
        {
            _NroProgramacion = NroProgramacion;
            _Usuario = Usuario;
        }

        private void CargarComboAuxilio()
        {
            DataTable dtTipoAuxilio = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarAuxilio();
            cbxTipoAuxilio.DataSource = dtTipoAuxilio;
            cbxTipoAuxilio.DisplayMember = "Descripcion";
            cbxTipoAuxilio.ValueMember = "idTipoAuxilio";
        }

        private void ListarRegistro()
        {
            DataTable dtListaRegistros = new DataTable();
            dtListaRegistros = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarRegistro(_NroProgramacion);

            if (dtListaRegistros.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaRegistros.Rows.Count; i++)
                {
                    txtTracto.Text = dtListaRegistros.Rows[i]["TRACTO"].ToString();
                    txtProgramacion.Text = dtListaRegistros.Rows[i]["PROGRAMACION"].ToString();
                    txtFechaViaje.Text = dtListaRegistros.Rows[i]["FECHA_VIAJE"].ToString();
                    txtSemirremolque.Text = dtListaRegistros.Rows[i]["SEMIRREMOLQUE"].ToString();
                    txtConductor.Text = dtListaRegistros.Rows[i]["CONDUCTOR"].ToString();
                    txtRuta.Text = dtListaRegistros.Rows[i]["RUTA"].ToString();
                    txtCliente.Text = dtListaRegistros.Rows[i]["CLIENTE"].ToString();
                }
            }
        }

        private void InsertarFalla()
        {
            if (txtMotivo.Text.Length == 0 || txtUbicacion.Text.Length == 0 || txtDIncidente.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtMotivo.Text.Length == 0) { txtMotivo.Focus(); }
                else
                {
                    if (txtUbicacion.Text.Length == 0) { txtUbicacion.Focus(); }
                    else { txtDIncidente.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_Insertar(_NroProgramacion, Convert.ToInt32(cbxTipoAuxilio.SelectedValue), TipoFalla, txtMotivo.Text,
                              Convert.ToDateTime(dtpFechaInicio.Value), Convert.ToDateTime(dtpHoraInicio.Value), txtUbicacion.Text, _Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataTable dtIncidente = new DataTable();
                    string RIncidente;
                    int idFalla;
                    DataTable dtBloqueoUnidad = new DataTable();
                    string BloqueoUnidad;
                    DataTable dtBloqueoConductor = new DataTable();
                    string BloqueoConductor;

                    idFalla = Convert.ToInt32(Respuesta.Substring(32));
                    dtIncidente = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_InsertarIncidencias(idFalla, txtEstadoUnidad.Text, txtDIncidente.Text, cbxTipoDanio.Text, txtDanio.Text,
                                  txtMotivo.Text, txtGPS.Text, txtRecursos.Text, txtObservacion.Text, byteArrayImagen, byteArrayFalla, _Usuario);
                    RIncidente = Convert.ToString(dtIncidente.Rows[0]["exito"]);
                    string NroRPTA2 = RIncidente.Substring(0, 1);

                    if (NroRPTA2 == "0" && cbxTipoAuxilio.Text == "INCIDENTE")
                    { MessageBox.Show(RIncidente, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                    if (BloqueoU == 1)
                    {
                        dtBloqueoUnidad = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(1, 0, 0, 0, idTracto, "FALLA MECÁNICA", "SEGURIDAD", txtMotivo.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                                                                                        0, dtpFechaInicio.Text, dtpFechaInicio.Text);

                        dtBloqueoUnidad = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(1, 0, 0, 0, idCarreta, "FALLA MECÁNICA", "SEGURIDAD", txtMotivo.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                                                                                        0, dtpFechaInicio.Text, dtpFechaInicio.Text);

                        BloqueoUnidad = Convert.ToString(dtBloqueoUnidad.Rows[0]["exito"]);
                        string NroRPTA3 = BloqueoUnidad.Substring(0, 1);

                        if (NroRPTA3 == "0") { MessageBox.Show(BloqueoUnidad, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        else { MessageBox.Show(BloqueoUnidad, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    if (BloqueoC == 1)
                    {
                        dtBloqueoConductor = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_BloquearConductorMtto(idConductor, txtMotivo.Text, Utilitario.Instancia.SesionUsuario.usuario);
                        BloqueoConductor = Convert.ToString(dtBloqueoConductor.Rows[0]["exito"]);
                        string NroRPTA4 = BloqueoConductor.Substring(0, 1);

                        if (NroRPTA4 == "0") { MessageBox.Show(BloqueoConductor, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        else { MessageBox.Show(BloqueoConductor, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    Close();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMotivo.Focus();
                }
            }
        }


        private void cbxTipoAuxilio_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxTipoAuxilio.Text == "NEUMÁTICO")
            {
                pKitNeumatico.Location = new System.Drawing.Point(628, 130);
                pKitNeumatico.Visible = true;
                pKitNeumatico.BringToFront();

                DataTable dtKitNeumatico = new DataTable();
                string FechaAsignacion;
                dtKitNeumatico = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RevisarKitNeumatico(idTracto);
                FechaAsignacion = Convert.ToString(dtKitNeumatico.Rows[0]["Fecha"]);
                txtFechaKN.Text = FechaAsignacion;

                if (FechaAsignacion == "NO ASIGNADA")
                {
                    lblMensaje.Text = "La unidad no tiene ningún kit de\r\nneumático asignado.";
                    lblMensaje.ForeColor = Color.Red;
                }
                else
                {
                    lblMensaje.Text = "La unidad sí tiene asignada un\r\nkit de neumático.";
                    lblMensaje.ForeColor = Color.ForestGreen;
                }
            }
        }

        private void pKitNeumatico_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pKitNeumatico.Left = pKitNeumatico.Left + (e.X - xClick);
                pKitNeumatico.Top = pKitNeumatico.Top + (e.Y - yClick);
            }
        }

        private void btnCerrarKN_Click(object sender, EventArgs e)
        {
            pKitNeumatico.Visible = false;
            pKitNeumatico.SendToBack();

            txtPlacaKN.Clear();
            txtFechaKN.Clear();
            lblMensaje.Text = "";
        }

        private void rbFallaParada_Click(object sender, EventArgs e) { TipoFalla = "PARADA"; }

        private void rbLeve_Click(object sender, EventArgs e) { TipoFalla = "LEVE"; }

        private void txtEstadoUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaInicio.Focus(); }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraInicio.Focus(); }
        }

        private void dtpHoraInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtUbicacion.Focus(); }
        }

        private void txtUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtGPS.Focus(); }
        }

        private void txtGPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtMotivo.Focus(); }
        }

        private void txtMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtDIncidente.Focus(); }
        }

        private void txtRecursos_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxTipoDanio.Focus(); }
        }

        private void cbxTipoDanio_DropDownClosed(object sender, EventArgs e)
        {
            /*
            if (cbxTipoDanio.Text == "NINGUNO") { txtDanio.Text = "NINGUNO"; }
            else { txtDanio.Clear(); }
            */ 
        }

        private void btnBuscarImagen_Click(object sender, EventArgs e)
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
                    pbImagen.Image = x;
                    pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            byteArrayImagen = null;
            pbImagen.Image = null;
            pbImagen.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnBuscarFalla_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK)
                {
                    byteArrayFalla = File.ReadAllBytes(getImage.FileName);

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayFalla));
                    pbFalla.Image = x;
                    pbFalla.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            byteArrayFalla = null;
            pbFalla.Image = null;
            pbFalla.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now;
            dtpHoraInicio.Value = DateTime.Now;
            txtMotivo.Clear();
            txtUbicacion.Clear();
            txtMotivo.Focus();
            rbFallaParada.Checked = true;
            rbFallaParada_Click(sender, e);
            cbxTipoAuxilio.Text = "MECÁNICO";

            txtDIncidente.Clear();
            cbxTipoDanio.Text = "NINGUNO";
            txtObservacion.Clear();
            byteArrayImagen = null;
            pbImagen.Image = null;
            byteArrayFalla = null;
            pbFalla.Image = null;

            cbBloqueoC.Checked = false;
            cbBloqueoC_CheckedChanged(sender, e);
            cbBloqueoU.Checked = false;
            cbBloqueoU_CheckedChanged(sender, e);
        }

        private void btnAgregarFalla_Click(object sender, EventArgs e) { InsertarFalla(); }

        private void txtTracto2_Enter(object sender, EventArgs e) { txtTracto2.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtTracto2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTracto, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte2(txtTracto2.Text), true, false, false);
            lstTracto.Columns[0].Width = 0;
            lstTracto.Columns[1].Width = 80;
            lstTracto.BringToFront();
            lstTracto.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idTracto = -1;
                lstTracto.Visible = false;
                lstTracto.SendToBack();
                txtTracto2.Focus();
            }
        }

        private void txtTracto2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTracto.Focus(); }
        }

        private void txtTracto2_Leave(object sender, EventArgs e) { txtTracto2.BackColor = Color.White; }

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

                idTracto = Int32.Parse(ItemActual.Text);
                txtTracto2.Text = ItemActual.SubItems[1].Text;
                txtPlacaKN.Text = ItemActual.SubItems[1].Text;

                lstTracto.Visible = false;
                lstTracto.SendToBack();
                txtCarreta2.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idTracto = -1;
                lstTracto.Visible = false;
                lstTracto.SendToBack();
                txtTracto2.Focus();
            }
        }

        private void lstTracto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTracto.SelectedItems[0];

            idTracto = Int32.Parse(ItemActual.Text);
            txtTracto2.Text = ItemActual.SubItems[1].Text;
            txtPlacaKN.Text = ItemActual.SubItems[1].Text;

            lstTracto.Visible = false;
            lstTracto.SendToBack();
            txtCarreta2.Focus();
        }

        private void txtCarreta2_Enter(object sender, EventArgs e) { txtCarreta2.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtCarreta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstCarreta, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte2(txtCarreta2.Text), true, false, false);
            lstCarreta.Columns[0].Width = 0;
            lstCarreta.Columns[1].Width = 80;
            lstCarreta.BringToFront();
            lstCarreta.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idCarreta = -1;
                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                txtCarreta2.Focus();
            }
        }

        private void txtCarreta2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstCarreta.Focus(); }
        }

        private void txtCarreta2_Leave(object sender, EventArgs e) { txtCarreta2.BackColor = Color.White; }

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

                idCarreta = Int32.Parse(ItemActual.Text);
                txtCarreta2.Text = ItemActual.SubItems[1].Text;

                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                dtpFechaViaje.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idCarreta = -1;
                lstCarreta.Visible = false;
                lstCarreta.SendToBack();
                txtCarreta2.Focus();
            }
        }

        private void lstCarreta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstCarreta.SelectedItems[0];

            idCarreta = Int32.Parse(ItemActual.Text);
            txtCarreta2.Text = ItemActual.SubItems[1].Text;

            lstCarreta.Visible = false;
            lstCarreta.SendToBack();
            dtpFechaViaje.Focus();
        }

        private void txtConductor2_Enter(object sender, EventArgs e) { txtConductor2.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtConductor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstConductor, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte(txtConductor2.Text), true, false, false);
            lstConductor.Columns[0].Width = 0;
            lstConductor.Columns[1].Width = 350;
            lstConductor.Columns[2].Width = 0;
            lstConductor.Columns[3].Width = 0;
            lstConductor.BringToFront();
            lstConductor.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                txtConductor2.Focus();
            }
        }

        private void txtConductor2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstConductor.Focus(); }
        }

        private void txtConductor2_Leave(object sender, EventArgs e) { txtConductor2.BackColor = Color.White; }

        private void lstConductor_Enter(object sender, EventArgs e)
        {
            if (!lstConductor.Items.Count.Equals(0)) { lstConductor.Items[0].Selected = true; }
        }

        private void lstConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstConductor.SelectedItems[0];

                Persona = Int32.Parse(ItemActual.Text);
                txtConductor2.Text = ItemActual.SubItems[1].Text;

                lstConductor.Visible = false;
                lstConductor.SendToBack();
                txtRuta2.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstConductor.Visible = false;
                lstConductor.SendToBack();
                txtConductor2.Focus();
                Persona = -1;
            }
        }

        private void lstConductor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstConductor.SelectedItems[0];

            Persona = Int32.Parse(ItemActual.Text);
            txtConductor2.Text = ItemActual.SubItems[1].Text;

            lstConductor.Visible = false;
            lstConductor.SendToBack();
            txtRuta2.Focus();
        }

        private void txtRuta2_Enter(object sender, EventArgs e) { txtRuta2.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtRuta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstRuta, clsConsultaBL.Instancia.GetRutasActivas(txtRuta2.Text), true, false, false);
            lstRuta.Columns[0].Width = 0;
            lstRuta.Columns[1].Width = 400;
            lstRuta.BringToFront();
            lstRuta.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstRuta.Visible = false;
                lstRuta.SendToBack();
                txtRuta2.Focus();
            }
        }

        private void txtRuta2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstRuta.Focus(); }
        }

        private void txtRuta2_Leave(object sender, EventArgs e) { txtRuta2.BackColor = Color.White; }

        private void lstRuta_Enter(object sender, EventArgs e)
        {
            if (!lstRuta.Items.Count.Equals(0)) { lstRuta.Items[0].Selected = true; }
        }

        private void lstRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstRuta.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstRuta.SelectedItems[0];

                idRuta = Int32.Parse(ItemActual.Text);
                txtRuta2.Text = ItemActual.SubItems[1].Text;

                lstRuta.Visible = false;
                lstRuta.SendToBack();
                cbxTipoAuxilio.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstRuta.Visible = false;
                lstRuta.SendToBack();
                txtRuta2.Focus();
                idRuta = -1;
            }
        }

        private void lstRuta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstRuta.SelectedItems[0];

            idRuta = Int32.Parse(ItemActual.Text);
            txtRuta2.Text = ItemActual.SubItems[1].Text;

            lstRuta.Visible = false;
            lstRuta.SendToBack();
            cbxTipoAuxilio.Focus();
        }

        public void cbBloqueoC_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBloqueoC.Checked == true) { BloqueoC = 1; }

            if (cbBloqueoC.Checked == false) { BloqueoC = 0; }
        }

        public void cbBloqueoU_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBloqueoU.Checked == true) { BloqueoU = 1; }

            if (cbBloqueoU.Checked == false) { BloqueoU = 0; }
        }

        private void btnCancelarT_Click(object sender, EventArgs e)
        {
            Persona = -1; idRuta = -1;
            idTracto = -1; idCarreta = -1;
            txtTracto2.Clear();
            txtCarreta2.Clear();
            dtpFechaViaje.Value = DateTime.Now;
            txtConductor2.Clear();
            txtRuta2.Clear();
            
            dtpFechaInicio.Value = DateTime.Now;
            dtpHoraInicio.Value = DateTime.Now;
            txtMotivo.Clear();
            txtUbicacion.Clear();
            txtMotivo.Focus();
            rbFallaParada.Checked = true;
            rbFallaParada_Click(sender, e);
            cbxTipoAuxilio.Text = "MECÁNICO";

            txtDIncidente.Clear();
            cbxTipoDanio.Text = "NINGUNO";
            txtObservacion.Clear();
            byteArrayImagen = null;
            pbImagen.Image = null;
            byteArrayFalla = null;
            pbFalla.Image = null;
        }

        private void btnRegistrarT_Click(object sender, EventArgs e)
        {
            if (txtTracto2.Text.Length == 0 || txtCarreta2.Text.Length == 0 || txtConductor2.Text.Length == 0 || txtRuta2.Text.Length == 0 ||
                txtMotivo.Text.Length == 0 || txtUbicacion.Text.Length == 0 || txtDIncidente.Text.Length == 0 )
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtTracto2.Text.Length == 0) { txtTracto2.Focus(); }
                else
                {
                    if (txtCarreta2.Text.Length == 0) { txtCarreta2.Focus(); }
                    else
                    {
                        if (txtConductor2.Text.Length == 0) { txtConductor2.Focus(); }
                        else
                        {
                            if (txtRuta2.Text.Length == 0) { txtRuta2.Focus(); }
                            else
                            {
                                if (txtMotivo.Text.Length == 0) { txtMotivo.Focus(); }
                                else
                                {
                                    if (txtUbicacion.Text.Length == 0) { txtUbicacion.Focus(); }
                                    else { txtDIncidente.Focus(); }
                                }
                            }
                        }
                    }
                }

                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                DataTable dtBloqueoUnidad = new DataTable();
                string BloqueoUnidad;
                DataTable dtBloqueoConductor = new DataTable();
                string BloqueoConductor;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarTolvas(txtTracto2.Text, txtCarreta2.Text, dtpFechaViaje.Value, Persona, idRuta,
                                                           Convert.ToInt32(cbxTipoAuxilio.SelectedValue), TipoFalla, txtMotivo.Text, Convert.ToDateTime(dtpFechaInicio.Value), Convert.ToDateTime(dtpHoraInicio.Value),
                                                           txtUbicacion.Text, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DataTable dtIncidente = new DataTable();
                    string RIncidente;
                    int idFalla = Convert.ToInt32(Respuesta.Substring(32));

                    dtIncidente = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_InsertarIncidencias(idFalla, txtEstadoUnidad.Text, txtDIncidente.Text, cbxTipoDanio.Text, txtDanio.Text,
                                  txtMotivo.Text, txtGPS.Text, txtRecursos.Text, txtObservacion.Text, byteArrayImagen, byteArrayFalla, Utilitario.Instancia.SesionUsuario.usuario);
                    RIncidente = Convert.ToString(dtIncidente.Rows[0]["exito"]);
                    string NroRPTA2 = RIncidente.Substring(0, 1);

                    if (NroRPTA2 == "0" && cbxTipoAuxilio.Text == "INCIDENTE")
                    { MessageBox.Show(RIncidente, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                    if (BloqueoU == 1)
                    {
                        dtBloqueoUnidad = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(1, 0, 0, 0, idTracto, "FALLA MECÁNICA", "SEGURIDAD", txtMotivo.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                                                                                        0, dtpFechaInicio.Text, dtpFechaInicio.Text);

                        dtBloqueoUnidad = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(1, 0, 0, 0, idCarreta, "FALLA MECÁNICA", "SEGURIDAD", txtMotivo.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                                                                                        0, dtpFechaInicio.Text, dtpFechaInicio.Text);

                        BloqueoUnidad = Convert.ToString(dtBloqueoUnidad.Rows[0]["exito"]);
                        string NroRPTA3 = BloqueoUnidad.Substring(0, 1);

                        if (NroRPTA3 == "0") { MessageBox.Show(BloqueoUnidad, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        else { MessageBox.Show(BloqueoUnidad, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    if (BloqueoC == 1)
                    {
                        dtBloqueoConductor = clsOperacionesBL.Instancia.GetViajes_Conductor_BloqueaDesbloquea(1, 0, Persona, txtMotivo.Text, Utilitario.Instancia.SesionUsuario.usuario, 1, 1);
                        BloqueoConductor = Convert.ToString(dtBloqueoConductor.Rows[0]["exito"]);
                        string NroRPTA4 = BloqueoConductor.Substring(0, 1);

                        if (NroRPTA4 == "0") { MessageBox.Show(BloqueoConductor, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        else { MessageBox.Show(BloqueoConductor, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    Close();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMotivo.Focus();
                }
            }
        }
    }
}
