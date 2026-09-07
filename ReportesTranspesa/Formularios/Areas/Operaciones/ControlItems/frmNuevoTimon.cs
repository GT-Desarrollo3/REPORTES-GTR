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
    public partial class frmNuevoTimon : Form
    {
        public int Conductor = -1, Inspector = -1, Proveedor = -1, Seguimiento = -1, idTracto = -1;
        public int Opcionlst1, Opcionlst2, Opcion, idTA;
        public byte[] byteArrayImagen = null, byteArrayImagen2 = null;
        public frmListaControlItems frmListaControlItems = new frmListaControlItems();

        public frmNuevoTimon()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void frmNuevoTimon_Load(object sender, EventArgs e) { }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(4, "");
            cbxOperaciones.DataSource = dtOperacion;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }


        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e) { cbxTipo.Focus(); }

        public void cbxTipo_DropDownClosed(object sender, EventArgs e)
        {
            /*
            if (cbxTipo.Text == "COLCHONES")
            {
                label14.Text = "Estado: ";
                cbxRadio.Items.Clear();
                cbxRadio.Items.AddRange(new object[] { "BUENO", "CAMBIO" });
                cbxRadio.Text = "BUENO";
            }
            else
            {
                label14.Text = "Radio: ";
                cbxRadio.Items.Clear();
                cbxRadio.Items.AddRange(new object[] { "OPERATIVA", "NO OPERATIVA", "NO TIENE" });
                cbxRadio.Text = "OPERATIVA";
            }
            */

            cbxTapizado.Focus();
        }

        private void cbxReclinable_DropDownClosed(object sender, EventArgs e) { cbxCorredizo.Focus(); }

        private void cbxCorredizo_DropDownClosed(object sender, EventArgs e) { cbxRadio.Focus(); }

        private void cbxRadio_DropDownClosed(object sender, EventArgs e) { txtLugarInspeccion.Focus(); }

        private void dtpFechaRevision_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtTracto.Focus(); }
        }

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
                txtConductor.Focus();
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
            txtConductor.Focus();
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtConductor.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 278;
            lstEmpleado.Columns[2].Width = 100;
            lstEmpleado.Location = new Point(247, 67);
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
                    cbxOperaciones.Focus();
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
                cbxOperaciones.Focus();
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

        private void txtInspector_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtInspector.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 278;
            lstEmpleado.Columns[2].Width = 100;
            lstEmpleado.Location = new Point(247, 216);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1 || Opcion == 2)
            {
                cbxOperaciones.Text = "LINDLEY";
                cbxTipo.Text = "TIMONES";
                cbxTapizado.Text = "SÍ";
                cbxReclinable.Text = "NO";
                cbxCorredizo.Text = "NO";
                cbxRadio.Text = "OPERATIVA";
                dtpFechaRevision.Value = DateTime.Now;
                txtTracto.Clear(); idTracto = -1;
                txtConductor.Clear(); Conductor = -1;
                txtLugarInspeccion.Clear();
                txtInspector.Clear(); Inspector = -1;
                btnCerrar_Click(sender, e);
            }

            if (Opcion == 3)
            {
                txtProveedor.Clear(); Proveedor = -1;
                txtResponsable.Clear(); Seguimiento = -1;
                dtpFechaReparacion.Value = DateTime.Now;
                btnCerrar2_Click(sender, e);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (Opcion == 1 || Opcion == 2)
            {
                if (txtTracto.Text.Length == 0 || txtConductor.Text.Length == 0 || txtLugarInspeccion.Text.Length == 0 || txtInspector.Text.Length == 0)
                {
                    if (txtTracto.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese el tracto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    dtRevision = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RegistrarEditarAsientos(Opcion, idTA, cbxOperaciones.Text, cbxTipo.Text, dtpFechaRevision.Value, idTracto, Conductor,
                                                                                                 cbxTapizado.Text, cbxReclinable.Text, cbxCorredizo.Text, cbxRadio.Text, txtLugarInspeccion.Text, Inspector, byteArrayImagen,
                                                                                                 Utilitario.Instancia.SesionUsuario.usuario);
                    string Revision = Convert.ToString(dtRevision.Rows[0]["exito"]);
                    string NroRPTA = Revision.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Revision, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListaControlItems.ListarTimonesAsientos();
                        Close();
                    }
                    else { MessageBox.Show(Revision, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            if (Opcion == 3)
            {
                if (txtProveedor.Text.Length == 0 || txtResponsable.Text.Length == 0)
                {
                    if (txtProveedor.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese el nombre del proveedor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtProveedor.Focus();
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
                    dtReparacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_RepararAsientos(idTA, Proveedor, Seguimiento, dtpFechaReparacion.Value,
                                                              byteArrayImagen2, Utilitario.Instancia.SesionUsuario.usuario);
                    string Reparacion = Convert.ToString(dtReparacion.Rows[0]["exito"]);
                    string NroRPTA = Reparacion.Substring(0, 1);

                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Reparacion, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmListaControlItems.ListarTimonesAsientos();
                        Close();
                    }
                    else { MessageBox.Show(Reparacion, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            } 
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstProveedor, clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarNombreRUC(txtProveedor.Text), true, false, false);
            lstProveedor.Columns[0].Width = 0;
            lstProveedor.Columns[1].Width = 0;
            lstProveedor.Columns[2].Width = 278;
            lstProveedor.Location = new Point(13, 69);
            lstProveedor.BringToFront();
            Opcionlst2 = 1;
            lstProveedor.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstProveedor.Visible = false;
                lstProveedor.SendToBack();
                Proveedor = -1;
            }
        }

        private void txtProveedor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstProveedor.Focus(); }
        }

        private void lstProveedor_Enter(object sender, EventArgs e)
        {
            if (!lstProveedor.Items.Count.Equals(0)) { lstProveedor.Items[0].Selected = true; }
        }

        private void lstProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Opcionlst2 == 1)
            {
                if ((e.KeyChar == (char)Keys.Enter) && !lstProveedor.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstProveedor.SelectedItems[0];
                    Proveedor = Int32.Parse(ItemActual.Text);
                    txtProveedor.Text = ItemActual.SubItems[2].Text;

                    lstProveedor.Visible = false;
                    lstProveedor.SendToBack();
                    txtResponsable.Focus();
                }

                if (e.KeyChar == (char)Keys.Back)
                {
                    lstProveedor.Visible = false;
                    lstProveedor.SendToBack();
                    txtProveedor.Focus();
                    Proveedor = -1;
                }
            }

            if (Opcionlst2 == 2)
            {
                if ((e.KeyChar == (char)Keys.Enter) && !lstProveedor.Items.Count.Equals(0))
                {
                    ListViewItem ItemActual;
                    ItemActual = lstProveedor.SelectedItems[0];
                    Seguimiento = Int32.Parse(ItemActual.Text);
                    txtResponsable.Text = ItemActual.SubItems[1].Text;

                    lstProveedor.Visible = false;
                    lstProveedor.SendToBack();
                    dtpFechaReparacion.Focus();
                }

                if (e.KeyChar == (char)Keys.Back)
                {
                    lstProveedor.Visible = false;
                    lstProveedor.SendToBack();
                    txtResponsable.Focus();
                    Seguimiento = -1;
                }
            }
        }

        private void lstProveedor_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Opcionlst2 == 1)
            {
                ListViewItem ItemActual;
                ItemActual = lstProveedor.SelectedItems[0];
                Proveedor = Int32.Parse(ItemActual.Text);
                txtProveedor.Text = ItemActual.SubItems[2].Text;

                lstProveedor.Visible = false;
                lstProveedor.SendToBack();
                txtResponsable.Focus();
            }

            if (Opcionlst2 == 2)
            {
                ListViewItem ItemActual;
                ItemActual = lstProveedor.SelectedItems[0];
                Seguimiento = Int32.Parse(ItemActual.Text);
                txtResponsable.Text = ItemActual.SubItems[1].Text;

                lstProveedor.Visible = false;
                lstProveedor.SendToBack();
                dtpFechaReparacion.Focus();
            }
        }

        private void txtResponsable_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstProveedor, clsConsultaBL.Instancia.GetEmpleado(txtResponsable.Text), true, false, false);
            lstProveedor.Columns[0].Width = 0;
            lstProveedor.Columns[1].Width = 278;
            lstProveedor.Columns[2].Width = 100;
            lstProveedor.Location = new Point(13, 124);
            lstProveedor.BringToFront();
            Opcionlst2 = 2;
            lstProveedor.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstProveedor.Visible = false;
                lstProveedor.SendToBack();
                Seguimiento = -1;
            }
        }

        private void txtResponsable_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstProveedor.Focus(); }
        }

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
