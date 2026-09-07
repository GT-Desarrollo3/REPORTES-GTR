using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmNuevoTercero : Form
    {
        public int Opcion, Persona = -1;
        public string Titulo, Extension, Area;
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        public frmIngresoTerceros frmIngresoTerceros;
        
        public frmNuevoTercero()
        {
            InitializeComponent();
            cbxArea2.SelectedIndexChanged -= cbxArea2_SelectedIndexChanged;
        }

        private void cbxArea2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea2(); }

        private void frmNuevoTercero_Load(object sender, EventArgs e)
        {
            dgvPersonal.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPersonal.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPersonal.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            /*
            DataTable dtAreaUsuario = new DataTable();
            dtAreaUsuario = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios(Utilitario.Instancia.SesionUsuario.usuario);

            if (dtAreaUsuario.Rows.Count > 0)
            {
                Area = dtAreaUsuario.Rows[0]["AREA"].ToString();
                cbxArea2.Text = Area;
                //cbxArea2.Enabled = false;
            }
            */
        }


        public void CargarComboArea2()
        {
            DataTable dtArea2 = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarAreas(2);
            cbxArea2.DataSource = dtArea2;
            cbxArea2.DisplayMember = "AREA";
            cbxArea2.ValueMember = "CODIGO";
        }

        public void ListarExternos()
        {
            DataTable dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_PersonalExterno_ListarNombres();

            dgvPersonal.DataSource = null;
            dgvPersonal.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                dgvPersonal.DataSource = dt;
                dgvPersonal.AutoResizeColumns();

                dgvPersonal.Columns["NRO"].Frozen = true;
                dgvPersonal.Columns["NRO"].ReadOnly = true;
                dgvPersonal.Columns["APELLIDOS"].Frozen = true;
                dgvPersonal.Columns["APELLIDOS"].ReadOnly = true;
                dgvPersonal.Columns["NOMBRES"].Frozen = true;
                dgvPersonal.Columns["NOMBRES"].ReadOnly = true;

                dgvPersonal.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FAB34B");
                dgvPersonal.EnableHeadersVisualStyles = false;

                dgvPersonal.Columns["NRO"].Visible = false;
            }
        }


        private void txtEmpExt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { txtApellido.Focus(); }
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { txtNombres.Focus(); }
        }

        private void txtNombres_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { txtDNI.Focus(); }
        }

        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Enter) { btnNuevo.Focus(); }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (txtApellido.Text.Length == 0 || txtNombres.Text.Length == 0 || txtDNI.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtApellido.Text.Length == 0) { txtApellido.Focus(); }
                else
                {
                    if (txtNombres.Text.Length == 0) { txtNombres.Focus(); }
                    else { txtDNI.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta = "";

                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_PersonalExterno_InsertarEliminarNombres(1, 0, txtApellido.Text, txtNombres.Text, txtDNI.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    //MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtApellido.Clear();
                    txtNombres.Clear();
                    txtDNI.Clear();
                    ListarExternos();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsQuitarPersonal_Click(object sender, EventArgs e)
        {
            try
            {
                int Nro = Convert.ToInt32(dgvPersonal.CurrentRow.Cells["NRO"].Value.ToString());

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_PersonalExterno_InsertarEliminarNombres(2, Nro, "", "", "");
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarExternos(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { }
        }

        private void txtEmpleado_Enter(object sender, EventArgs e) { txtEmpleado.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtEmpleado.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 300;
            lstEmpleado.Columns[2].Width = 0;
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
                cbxArea2.Focus();
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
            cbxArea2.Focus();
        }

        private void dgvPersonal_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvPersonal.RowCount > 0) { dgvPersonal.ContextMenuStrip = contextMenuStrip1; }
                else { dgvPersonal.ContextMenuStrip = null; }
            }
            catch (Exception ex) { }
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(op.FileName))
                {
                    txtRutaLocal.Text = op.FileName;
                    Titulo = Path.GetFileNameWithoutExtension(op.FileName);
                    Extension = Path.GetExtension(op.FileName);
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarLocal_Click(object sender, EventArgs e)
        {
            txtRutaLocal.Clear();
            Titulo = null;
            Extension = null;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtEmpExt.Clear();
            txtApellido.Clear();
            txtNombres.Clear();
            txtDNI.Clear();
            btnCerrarLocal_Click(sender, e);
            txtEmpleado.Clear();
            lstEmpleado.Visible = false;
            lstEmpleado.SendToBack();
            Persona = -1;
            Titulo = null; Extension = null;
            txtMotivo.Clear();
            dtpFechaIngreso.Value = DateTime.Now;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtRutaLocal.Text.Length == 0)
            {
                MessageBox.Show("Por favor, seleccione un documento a registrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtEmpExt.Text.Length == 0 || dgvPersonal.RowCount == 0 || txtEmpleado.Text.Length == 0 || txtMotivo.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtEmpExt.Text.Length == 0) { txtEmpExt.Focus(); }
                else
                {
                    if (txtEmpleado.Text.Length == 0) { txtEmpleado.Focus(); }
                    else { txtMotivo.Focus(); }
                }
                
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta = "";
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                string ruta = txtRutaLocal.Text;

                if (!File.Exists(ruta))
                {
                    MessageBox.Show("La ruta no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                byte[] archivoBytes = File.ReadAllBytes(ruta);

                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_PersonalExterno_RegistrarExterno(Opcion, txtEmpExt.Text, archivoBytes, Titulo, Extension,
                                                       Persona, cbxArea2.Text, txtMotivo.Text, dtpFechaIngreso.Value, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmIngresoTerceros.ListarExternos();
                    Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
