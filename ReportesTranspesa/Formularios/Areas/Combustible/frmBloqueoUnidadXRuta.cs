using ReportesTranspesa.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using DevExpress.Utils;
using Comun;
using Negocio;

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class frmBloqueoUnidadXRuta : Form
    {
        DataTable dtPermisos = new DataTable();
        public int idRuta, idTracto;
        public string motivo, FechaRegistro, usuario, esVALE;
        string xmlRuta = "";
        int accion = 0;
        public int xClick = 0, yClick = 0;
        int e1 = 0;

        public frmBloqueoUnidadXRuta()
        {
            InitializeComponent();
        }

        private void frmBloqueoUnidadXRuta_Shown(object sender, EventArgs e) { txtUnidad.Focus(); }

        private void frmBloqueoUnidadXRuta_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmBloqueoUnidadXRuta");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnGuarda.Enabled = true; }
                    else { btnGuarda.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                    {
                        liberarToolStripMenuItem.Enabled = true;
                        desbloquearTodasToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        liberarToolStripMenuItem.Enabled = false;
                        desbloquearTodasToolStripMenuItem.Enabled = false;
                    }
                }
            }

            ListarBloqueos();
            BuscarRutas();
        }


        public void ListarBloqueos()
        {
            dtgvListarUnidadBloqueada.OptionsView.ColumnAutoWidth = false;
            dtgListarUnidadBloqueada.DataSource = null;
            DataTable dtListarUnidadesBloqueadas = new DataTable();

            dtListarUnidadesBloqueadas = clsCombustibleBL.Instancia.getListarUnidadesBloqueadas();

            if (dtgvListarUnidadBloqueada != null) { dtgvListarUnidadBloqueada.ClearSelection(); }

            if (dtListarUnidadesBloqueadas.Rows.Count > 0)
            {
                dtgListarUnidadBloqueada.DataSource = dtListarUnidadesBloqueadas;
                dtgvListarUnidadBloqueada.Columns["FechaBloqueo"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListarUnidadBloqueada.Columns["FechaBloqueo"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dtgvListarUnidadBloqueada.Columns["IdTracto"].Visible = false;
                dtgvListarUnidadBloqueada.BestFitColumns();
            }
        }

        public void BuscarBloqueos()
        {
            dtgListarUnidadBloqueada.DataSource = null;
            dtgvListarUnidadBloqueada.Columns.Clear();

            DataTable dt = new DataTable();
            dt = clsCombustibleBL.Instancia.GetBuscarUnidadesBloqueadas(txtBuscarPlaca.Text);

            if (dt.Rows.Count > 0)
            {
                dtgListarUnidadBloqueada.DataSource = dt;
                dtgvListarUnidadBloqueada.Columns["FechaBloqueo"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListarUnidadBloqueada.Columns["FechaBloqueo"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dtgvListarUnidadBloqueada.Columns["IdTracto"].Visible = false;
                dtgvListarUnidadBloqueada.Columns["idBloqueo"].Visible = false;
                
                dtgvListarUnidadBloqueada.BestFitColumns();
            }
        }

        public void ListarRegistro(int idTracto, string Motivo)
        {
            dtgListaRegistros.DataSource = null;
            dgvListaRegistrosView.Columns.Clear();

            DataTable dt = new DataTable();
            dt = clsCombustibleBL.Instancia.ReportesApp_Combusible_ListarRegistro_UnidadesBloqueadas(idTracto, Motivo);
            
            if (dt.Rows.Count > 0)
            {
                dtgListaRegistros.DataSource = dt;
                dtgvListarUnidadBloqueada.Columns["FechaBloqueo"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListarUnidadBloqueada.Columns["FechaBloqueo"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaRegistrosView.Columns["IdRuta"].Visible = false;
                dgvListaRegistrosView.Columns["IdTracto"].Visible = false;
                dgvListaRegistrosView.Columns["UserBloqueo"].Visible = false;
                
                dgvListaRegistrosView.BestFitColumns();
            }
        }

        public void BuscarRutas()
        {
            dgvRutas.DataSource = null;
            dgvRutasView.Columns.Clear();
            
            DataTable dt = new DataTable();
            dt = clsConsultaBL.Instancia.GetRutasActivas(txtRuta.Text);

            if (dt.Rows.Count > 0)
            {
                dgvRutas.DataSource = dt;
                dgvRutasView.Columns["ID"].Visible = false;
                dgvRutasView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para mostrar.";
                m.ShowDialog();
            }
        }

        public string xmlRutas()
        {
            string Rutas = "";
            try
            {
                int[] filas = dgvRutasView.GetSelectedRows();

                if (filas.Length > 0)
                {
                    XmlDocument doc = new XmlDocument();
                    XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlElement root = doc.DocumentElement;
                    doc.InsertBefore(xmlDeclaration, root);

                    XmlElement r = doc.CreateElement(string.Empty, "r", string.Empty);
                    doc.AppendChild(r);

                    for (int i = 0; i < filas.Length; i++)
                    {
                        XmlElement rutas = doc.CreateElement(string.Empty, "rutas", string.Empty);

                        XmlAttribute attribute_ticket = doc.CreateAttribute("ID");
                        attribute_ticket.Value = dgvRutasView.GetRowCellValue(filas[i], "ID").ToString();
                        rutas.Attributes.Append(attribute_ticket);

                        /*
                        XmlAttribute attribute_placa = doc.CreateAttribute("descripcion");
                        attribute_placa.Value = dgvRutasView.GetRowCellValue(filas[i], "DESCRIPCION").ToString();
                        rutas.Attributes.Append(attribute_placa);
                        */

                        r.AppendChild(rutas);
                    }

                    Rutas = doc.OuterXml;
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message, "Error"); }

            return Rutas;
        }


        private void txtUnidad_Enter(object sender, EventArgs e) { txtUnidad.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTracto, clsConsultaBL.Instancia.GetUnidadesActivas(txtUnidad.Text), true, false, false);
            lstTracto.Columns[0].Width = 0;
            lstTracto.Columns[1].Width = 100;
            lstTracto.Columns[2].Width = 0;
            lstTracto.Columns[3].Width = 100;
            lstTracto.BringToFront();
            lstTracto.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idTracto = -1;
                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }
        }

        private void txtUnidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTracto.Focus(); }
        }

        private void txtUnidad_Leave(object sender, EventArgs e) { txtUnidad.BackColor = Color.White; }

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
                txtUnidad.Text = ItemActual.SubItems[1].Text;

                lstTracto.Visible = false;
                lstTracto.SendToBack();
                txtMotivo.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idTracto = -1;
                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }
        }

        private void lstTracto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTracto.SelectedItems[0];

            idTracto = Int32.Parse(ItemActual.Text);
            txtUnidad.Text = ItemActual.SubItems[1].Text;

            lstTracto.Visible = false;
            lstTracto.SendToBack();
            txtMotivo.Focus();
        }

        private void txtRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { BuscarRutas(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUnidad.Text) || string.IsNullOrWhiteSpace(txtMotivo.Text))
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtUnidad.Text.Length == 0) { txtUnidad.Focus(); }
                else { txtMotivo.Focus(); }
                return;
            }
            else
            {
                DataTable dtRegistrarBloqueoRuta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                xmlRuta = xmlRutas();

                if (xmlRuta != "")
                {
                    dtRegistrarBloqueoRuta = clsCombustibleBL.Instancia.getRegistrarBloqueoUnidadxRuta(1, idTracto, xmlRuta, txtMotivo.Text, Usuario);
                    Respuesta = Convert.ToString(dtRegistrarBloqueoRuta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarBloqueos();
                        txtMotivo.Clear();
                        txtRuta.Clear();
                        txtUnidad.Clear();
                        idTracto = -1;
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("No se ha registrado ninguna ruta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { BuscarBloqueos(); }

        private void txtBuscarPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { BuscarBloqueos(); }
        }

        private void dtgListarUnidadBloqueada_DoubleClick(object sender, EventArgs e)
        {
            try { verDetalleToolStripMenuItem_Click(sender, e); }
            catch { MessageBox.Show("La placa seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void verDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pVerRegistro.Visible = true;
            pVerRegistro.BringToFront();

            lblidPlaca.Text = dtgvListarUnidadBloqueada.GetRowCellValue(dtgvListarUnidadBloqueada.FocusedRowHandle, "NumeroPlaca").ToString();
            int idPlaca = Convert.ToInt32(dtgvListarUnidadBloqueada.GetRowCellValue(dtgvListarUnidadBloqueada.FocusedRowHandle, "IdTracto"));
            string motivo = dtgvListarUnidadBloqueada.GetRowCellValue(dtgvListarUnidadBloqueada.FocusedRowHandle, "MotivoBloqueo").ToString();
            ListarRegistro(idPlaca, motivo);
        }

        private void desbloquearTodasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea desbloquear todas las rutas de esta unidad?", "DESBLOQUEAR RUTAS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dt = new DataTable();
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    string Respuesta;

                    xmlRuta = "";
                    int idTracto = Convert.ToInt32(dtgvListarUnidadBloqueada.GetRowCellValue(dtgvListarUnidadBloqueada.FocusedRowHandle, "IdTracto").ToString());
                    string Motivo = dtgvListarUnidadBloqueada.GetRowCellValue(dtgvListarUnidadBloqueada.FocusedRowHandle, "MotivoBloqueo").ToString();

                    dt = clsCombustibleBL.Instancia.getRegistrarBloqueoUnidadxRuta(2, idTracto, xmlRuta, Motivo, Usuario);
                    Respuesta = Convert.ToString(dt.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BuscarBloqueos();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show("No se pudo eliminar esta ruta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pCerrar_Click(object sender, EventArgs e)
        {
            pVerRegistro.Visible = false;
            pVerRegistro.BringToFront();

            dtgListaRegistros.DataSource = null;
            dgvListaRegistrosView.Columns.Clear();
        }

        private void pVerRegistro_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pVerRegistro.Left = pVerRegistro.Left + (e.X - xClick);
                pVerRegistro.Top = pVerRegistro.Top + (e.Y - yClick);
            }
        }


        private void btnHistorial_Click(object sender, EventArgs e)
        {
            frmHistorialBloqueoRutas frmHistorialBloqueoRutas = new frmHistorialBloqueoRutas();
            frmHistorialBloqueoRutas.ShowDialog();
        }

        private void liberarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea desbloquear esta ruta?", "DESBLOQUEAR RUTA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dt = new DataTable();
                    string Respuesta;

                    int idTracto = Convert.ToInt32(dgvListaRegistrosView.GetRowCellValue(dgvListaRegistrosView.FocusedRowHandle, "IdTracto"));
                    int idRuta = Convert.ToInt32(dgvListaRegistrosView.GetRowCellValue(dgvListaRegistrosView.FocusedRowHandle, "IdRuta"));
                    string Motivo = dgvListaRegistrosView.GetRowCellValue(dgvListaRegistrosView.FocusedRowHandle, "MotivoBloqueo").ToString();

                    dt = clsCombustibleBL.Instancia.ReportesApp_Combusible_EliminarRuta_UnidadesBloqueadas(idRuta, idTracto, Motivo);
                    Respuesta = Convert.ToString(dt.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);

                    if (NroRPTA == "0") { ListarRegistro(idTracto, Motivo); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    
                }
            }
            catch (Exception ex) { MessageBox.Show("No se pudo eliminar esta ruta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}

