using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using System.IO;
using Microsoft.Office;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmInventarioSillas : Form
    {
        public int Opcion;
        public int idPersonal = -1;
        public int xClick = 0, yClick = 0;
        public byte[] byteArrayImagen = null;

        public frmInventarioSillas()
        {
            InitializeComponent();
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void frmInventarioSillas_Load(object sender, EventArgs e)
        {
            cbxEstado.Text = "TODOS";
            cbxSede.Text = "TODAS";
            ListarInventarioS();
            cbxSede2.Text = "LARREA";
            CargarComboArea();
        }


        public void CargarComboArea()
        {
            DataTable dtArea = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlInventario_ListarSedeArea(2, cbxSede2.Text);
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "Area";
            cbxArea.ValueMember = "idArea";
        }

        public void ListarInventarioS()
        {
            DataTable dtListaIS = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlInventario_ListarSillas(txtTrabajador.Text, cbxSede.Text, cbxEstado.Text);
            dtgInventarioSillas.DataSource = dtListaIS;
            if (dtListaIS.Rows.Count > 0)
            {
                dgvInventarioSillas.Columns["idPersona"].Visible = false;
                dgvInventarioSillas.Columns["CodigoBarras"].Visible = false;
                dgvInventarioSillas.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvInventarioSillas.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvInventarioSillas.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvInventarioSillas.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvInventarioSillas.BestFitColumns();
            }
        }


        private void dtgInventarioSillas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idTicket = dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "TICKET").ToString();

                if (idTicket != "") { tsEliminarSillas.Enabled = true; }
                else { tsEliminarSillas.Enabled = false; }
            }
            catch { tsEliminarSillas.Enabled = false; }
        }

        private void txtTrabajador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarInventarioS(); }
        }

        private void cbxSede_DropDownClosed(object sender, EventArgs e) { ListarInventarioS(); }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarInventarioS(); }

        private void dgvInventarioSillas_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "OCUPADO") { e.Appearance.BackColor = Color.Aqua; }

                if (e.CellValue.ToString() == "LIBRE") { e.Appearance.BackColor = Color.Lime; }

                if (e.CellValue.ToString() == "INACTIVO")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarInventarioS(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgInventarioSillas.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "CONTROL DE INVENTARIO DE GUIAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgInventarioSillas.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Opcion = 1;
            lblActivo.Text = "SILLA";
            cbxSede2.Text = "LARREA";
            CargarComboArea();
            cbxEstado2.Text = "LIBRE";
            txtTrabajador2.Text = "LIBRE";
            idPersonal = -1;
            txtTicket.ReadOnly = false;
            btnCerrar2_Click(sender, e);

            pRegistrarActivo.Visible = true;
            pRegistrarActivo.BringToFront();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pRegistrarActivo.Visible = false;
            pRegistrarActivo.SendToBack();
            pRegistrarActivo.Location = new System.Drawing.Point(507, 315);

            idPersonal = -1;
            btnCerrar2_Click(sender, e);
            txtTicket.Clear();
            txtObservacion.Clear();
            txtTrabajador2.Clear();
        }

        private void pRegistrarActivo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pRegistrarActivo.Left = pRegistrarActivo.Left + (e.X - xClick);
                pRegistrarActivo.Top = pRegistrarActivo.Top + (e.Y - yClick);
            }
        }

        private void cbxSede2_DropDownClosed(object sender, EventArgs e) { CargarComboArea(); }

        private void txtTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Enter) { cbxSede2.Focus(); }
        }

        private void cbxEstado2_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxEstado2.Text == "LIBRE")
            {
                idPersonal = -1;
                txtTrabajador2.Text = "LIBRE";
            }

            if (cbxEstado2.Text == "INACTIVO")
            {
                idPersonal = -2;
                txtTrabajador2.Text = "INACTIVO";
            }

            if (cbxEstado2.Text == "OCUPADO") { txtTrabajador2.Clear(); }
        }

        private void txtTrabajador2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersona, clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroEMO_ListarPersonal(txtTrabajador2.Text), true, false, false);
            lstPersona.Columns[0].Width = 0;
            lstPersona.Columns[1].Width = 400;
            lstPersona.Columns[2].Width = 0;
            lstPersona.Columns[3].Width = 0;
            lstPersona.BringToFront();
            lstPersona.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPersona.Visible = false;
                idPersonal = 0;
            }

            if (e.KeyChar == (char)Keys.Enter) { lstPersona.Visible = false; }
        }

        private void txtTrabajador2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersona.Focus(); }
        }

        private void lstPersona_Enter(object sender, EventArgs e)
        {
            if (!lstPersona.Items.Count.Equals(0)) { lstPersona.Items[0].Selected = true; }
        }

        private void lstPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersona.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersona.SelectedItems[0];
                idPersonal = Int32.Parse(ItemActual.Text);
                txtTrabajador2.Text = ItemActual.SubItems[1].Text;
                lstPersona.Visible = false;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPersona.Visible = false;
                txtTrabajador2.Focus();
            }
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersona.SelectedItems[0];
            idPersonal = Int32.Parse(ItemActual.Text);
            txtTrabajador2.Text = ItemActual.SubItems[1].Text;
            lstPersona.Visible = false;
        }

        private void dtgInventarioSillas_DoubleClick(object sender, EventArgs e)
        {
            Opcion = 2;
            lblActivo.Text = "SILLA";

            txtTicket.ReadOnly = true;
            txtTicket.Text = dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "TICKET").ToString();
            cbxSede2.Text = dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "SEDE").ToString();
            CargarComboArea();
            cbxArea.Text = dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "AREA").ToString();
            txtObservacion.Text = dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "OBSERVACION").ToString();
            cbxEstado2.Text = dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "ESTADO").ToString();
            idPersonal = Convert.ToInt32(dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "idPersona"));
            txtTrabajador2.Text = dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "TRABAJADOR").ToString();

            if (dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "CodigoBarras").ToString() != "")
            {
                byteArrayImagen = (Byte[])dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "CodigoBarras");

                Byte[] byteBLOBData;
                byteBLOBData = (Byte[])dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "CodigoBarras");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                pbCodigoBarras.Image = x;
                pbCodigoBarras.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            pRegistrarActivo.Visible = true;
            pRegistrarActivo.BringToFront();
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
                    pbCodigoBarras.Image = x;
                    pbCodigoBarras.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            byteArrayImagen = null;
            pbCodigoBarras.Image = null;
            pbCodigoBarras.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtTicket.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese un número de ticket.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTicket.Focus();

                return;
            }
            else
            {
                DataTable dtSillas = new DataTable();
                string Inventario;

                dtSillas = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlInventario_InsertarModificar(Opcion, Convert.ToInt32(txtTicket.Text), lblActivo.Text,
                                                        cbxSede2.Text, cbxArea.Text, idPersonal, txtTrabajador2.Text, txtObservacion.Text, byteArrayImagen, Utilitario.Instancia.SesionUsuario.usuario);
                Inventario = Convert.ToString(dtSillas.Rows[0]["exito"]);
                string NroRPTA = Inventario.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Inventario, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarInventarioS();
                    btnCerrar_Click(sender, e);
                }
                else { MessageBox.Show(Inventario, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminarSillas_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea quitar este ítem del inventario?", "QUITAR SILLA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idTicket = Convert.ToInt32(dgvInventarioSillas.GetRowCellValue(dgvInventarioSillas.FocusedRowHandle, "TICKET"));

                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlInventario_InsertarModificar(3, idTicket, "","","",0,"","",
                                                               null,Utilitario.Instancia.SesionUsuario.usuario);
                    
                    string respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = respta.Substring(0, 1);

                    if (NroRPTA == "0") { ListarInventarioS(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("El ítem seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
