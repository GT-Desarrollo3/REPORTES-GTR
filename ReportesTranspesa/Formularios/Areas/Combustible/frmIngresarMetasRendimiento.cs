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
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using System.Xml;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Combustible
{
    public partial class frmIngresarMetasRendimiento : Form
    {
        public int xClick = 0, yClick = 0;
        int Opcion;
        public string Marca;
        string xmlRuta = "";
        DataTable dtPermisos = new DataTable();

        public frmIngresarMetasRendimiento()
        {
            InitializeComponent();
            cbxOperacionB.SelectedIndexChanged -= cbxOperacionB_SelectedIndexChanged;
            cbxMarcaB.SelectedIndexChanged -= cbxMarcaB_SelectedIndexChanged;
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
            cbxRegion.SelectedIndexChanged -= cbxRegion_SelectedIndexChanged;
            cbxMarca.SelectedIndexChanged -= cbxMarca_SelectedIndexChanged;
            cbxModelo.SelectedIndexChanged -= cbxModelo_SelectedIndexChanged;
        }

        private void cbxOperacionB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboOperacion();
        }

        private void cbxMarcaB_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboMarca();
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboOperacionA();
        }

        private void cbxRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboRegion();
        }

        private void cbxMarca_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboMarcaA();
        }

        private void cbxModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboModelo();
        }


        private void frmIngresarMetasRendimiento_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmIngresarMetasRendimiento");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    btnAgregarMeta.Enabled = true;
                }
                else { btnAgregarMeta.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    editarMetaToolStripMenuItem.Enabled = true;
                }
                else { editarMetaToolStripMenuItem.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                {
                    eliminarToolStripMenuItem.Visible = true;
                }
                else { eliminarToolStripMenuItem.Visible = false; }
            }
            
            CargarComboOperacion();
            CargarComboMarca();

            CargarComboOperacionA();
            CargarComboRegion();
            CargarComboMarcaA();
            cbxMarca_DropDownClosed(sender, e);

            BuscarRutas();
        }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarCombo(1, "a");
            cbxOperacionB.DataSource = dtOperacion;
            cbxOperacionB.DisplayMember = "Descripcion";
            cbxOperacionB.ValueMember = "IdOperacion";
        }

        public void CargarComboOperacionA()
        {
            DataTable dtOperacion2 = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarCombo(2, "a");
            cbxOperacion.DataSource = dtOperacion2;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void CargarComboRegion()
        {
            DataTable dtRegion = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarCombo(3, "a");
            cbxRegion.DataSource = dtRegion;
            cbxRegion.DisplayMember = "Descripcion";
            cbxRegion.ValueMember = "idRegion";
        }

        public void CargarComboMarca()
        {
            DataTable dtMarca2 = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarCombo(4, "a");
            cbxMarcaB.DataSource = dtMarca2;
            cbxMarcaB.DisplayMember = "Descripcion";
            cbxMarcaB.ValueMember = "Marca";
        }

        public void CargarComboMarcaA()
        {
            DataTable dtMarca = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarCombo(5, "a");
            cbxMarca.DataSource = dtMarca;
            cbxMarca.DisplayMember = "Descripcion";
            cbxMarca.ValueMember = "Marca";
        }

        public void CargarComboModelo()
        {
            DataTable dtModelo = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarCombo(6, Marca);
            cbxModelo.DataSource = dtModelo;
            cbxModelo.DisplayMember = "Modelo";
            cbxModelo.ValueMember = "idModelo";
        }

        public void ListarMetas()
        {
            dtgMetasRendimiento.DataSource = null;
            dgvMetasRendimientoVista.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarMetas(Convert.ToInt32(cbxOperacionB.SelectedValue), txtTituloRuta.Text, Convert.ToString(cbxMarcaB.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                dtgMetasRendimiento.DataSource = dt;
                dgvMetasRendimientoVista.Columns["idMetaRendimiento"].Visible = false;
                dgvMetasRendimientoVista.Columns["Anio"].Visible = false;
                dgvMetasRendimientoVista.Columns["IdOperacion"].Visible = false;
                dgvMetasRendimientoVista.Columns["idRutas"].Visible = false;
                dgvMetasRendimientoVista.Columns["idMarca"].Visible = false;

                dgvMetasRendimientoVista.Columns["Fecha"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvMetasRendimientoVista.Columns["Fecha"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvMetasRendimientoVista.BestFitColumns();
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
                        attribute_ticket.Value = dgvRutasView.GetRowCellValue(filas[i], "IdRuta").ToString();
                        rutas.Attributes.Append(attribute_ticket);

                        XmlAttribute attribute_placa = doc.CreateAttribute("descripcion");
                        attribute_placa.Value = dgvRutasView.GetRowCellValue(filas[i], "RUTA").ToString();
                        rutas.Attributes.Append(attribute_placa);

                        XmlAttribute attribute_rendProm = doc.CreateAttribute("rendProm");
                        attribute_rendProm.Value = dgvRutasView.GetRowCellValue(filas[i], "REND_PROM").ToString();
                        rutas.Attributes.Append(attribute_rendProm);

                        r.AppendChild(rutas);
                    }

                    Rutas = doc.OuterXml;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }

            return Rutas;
        }


        private void btnAgregarMeta_Click(object sender, EventArgs e)
        {
            Opcion = 1;
            BuscarRutas();

            dtgRutasEnlazadas.DataSource = null;
            gRutas.Enabled = true;
            btnGuardar.Enabled = true;
            btnActualizar.Visible = false;

            CargarComboOperacionA();
            CargarComboRegion();
            CargarComboMarcaA();
            cbxMarca_DropDownClosed(sender, e);

            txtTitulo.ReadOnly = false;
            txtOP.Visible = false;
            txtRE.Visible = false;
            txtMA.Visible = false;
            txtMO.Visible = false;
            label9.Visible = false;
            txtRendimiento.Visible = false;
            txtMeta.ReadOnly = false;
            
            txtTitulo.Clear();
            txtRendimiento.Clear();
            txtMeta.Clear();
            txtRuta.Clear();

            txtRendimiento.Text = "0.00";
            pIngresarMetas.Visible = true;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pIngresarMetas.Visible = false;
        }

        private void pIngresarMetas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                xClick = e.X; yClick = e.Y;
            }
            else
            {
                pIngresarMetas.Left = pIngresarMetas.Left + (e.X - xClick);
                pIngresarMetas.Top = pIngresarMetas.Top + (e.Y - yClick);
            }
        }

        private void txtTituloRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarMetas();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarMetas();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgMetasRendimiento.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Metas de Rendimiento por Ruta - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgMetasRendimiento.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void cbxMarca_DropDownClosed(object sender, EventArgs e)
        {
            Marca = Convert.ToString(cbxMarca.SelectedValue);
            CargarComboModelo();
        }

        private void txtRendimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtMeta.Focus();
            }
        }

        private void txtMeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                txtRuta.Focus();
            }
        }

        public void BuscarRutas()
        {
            dgvRutas.DataSource = null;
            dgvRutasView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_ListarRutasRendimiento(txtRuta.Text);
            if (dt.Rows.Count > 0)
            {
                dgvRutas.DataSource = dt;
                dgvRutasView.Columns["IdRuta"].Visible = false;
                dgvRutasView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para mostrar.";
                m.ShowDialog();
            }
        }

        private void txtRuta_KeyUp(object sender, KeyEventArgs e)
        {
            BuscarRutas();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtMeta.Text))
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtTitulo.Text.Length == 0) { txtTitulo.Focus(); }
                else { txtMeta.Focus(); }
                return;
            }
            else
            {
                DataTable dtAgregarMetas = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                xmlRuta = xmlRutas();
                if (xmlRuta != "")
                {
                    dtAgregarMetas = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_Registrar(Convert.ToInt32(cbxOperacion.SelectedValue), Convert.ToInt32(cbxRegion.SelectedValue), txtTitulo.Text,
                                     Convert.ToString(cbxMarca.SelectedValue), Convert.ToInt32(cbxModelo.SelectedValue), Convert.ToDecimal(txtMeta.Text), xmlRuta, Usuario);
                    Respuesta = Convert.ToString(dtAgregarMetas.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        pIngresarMetas.Visible = false;
                        txtTitulo.Clear();
                        txtRendimiento.Clear();
                        txtMeta.Clear();
                        txtRuta.Clear();
                        ListarMetas();
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("No se ha registrado ninguna ruta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMeta.Text))
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtMeta.Focus();
                return;
            }
            else
            {
                DataTable dtModificarMetas = new DataTable();
                string Respuesta;
                int idMetaRendimiento = Convert.ToInt32(dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "idMetaRendimiento"));
                int Anio = Convert.ToInt32(dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "Anio"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtModificarMetas = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_ActualizarMetas(idMetaRendimiento, Anio, Convert.ToDecimal(txtMeta.Text), Convert.ToDecimal(txtRendimiento.Text), Usuario);
                Respuesta = Convert.ToString(dtModificarMetas.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pIngresarMetas.Visible = false;
                    btnActualizar.Visible = false;
                    txtTitulo.Clear();
                    txtRendimiento.Clear();
                    txtMeta.Clear();
                    txtRuta.Clear();
                    ListarMetas();
                }
                else
                { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgMetasRendimiento_DoubleClick(object sender, EventArgs e)
        {
            DataTable dtMetasFiltro = new DataTable();

            BuscarRutas();

            dtgRutasEnlazadas.DataSource = null;
            gRutas.Enabled = false;
            btnGuardar.Enabled = false;
            btnActualizar.Visible = false;

            txtTitulo.ReadOnly = true;
            txtOP.Visible = true;
            txtRE.Visible = true;
            txtMA.Visible = true;
            txtMO.Visible = true;
            label9.Visible = true;
            txtRendimiento.Visible = true;
            txtRendimiento.ReadOnly = true;

            txtMeta.ReadOnly = true;

            int idMetaRendimiento = Convert.ToInt32(dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "idMetaRendimiento"));
            int Anio = Convert.ToInt32(dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "Anio"));

            dtMetasFiltro = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_FiltrarMetas(idMetaRendimiento, Anio);
            dtgRutasEnlazadas.DataSource = dtMetasFiltro;
            if (dtMetasFiltro.Rows.Count > 0)
            {
                txtTitulo.Text = dtMetasFiltro.Rows[0]["Ruta"].ToString();
                txtOP.Text = dtMetasFiltro.Rows[0]["Operacion"].ToString();
                txtRE.Text = dtMetasFiltro.Rows[0]["Region"].ToString();
                txtMA.Text = dtMetasFiltro.Rows[0]["Marca"].ToString();
                txtMO.Text = dtMetasFiltro.Rows[0]["Modelo"].ToString();
                txtRendimiento.Text = dtMetasFiltro.Rows[0]["Rend. Promedio"].ToString();
                txtMeta.Text = dtMetasFiltro.Rows[0]["Meta"].ToString();

                dgvRutasEnlazadasView.Columns["Operacion"].Visible = false;
                dgvRutasEnlazadasView.Columns["Region"].Visible = false;
                dgvRutasEnlazadasView.Columns["Ruta"].Visible = false;
                dgvRutasEnlazadasView.Columns["Marca"].Visible = false;
                dgvRutasEnlazadasView.Columns["Modelo"].Visible = false;
                dgvRutasEnlazadasView.Columns["Rend. Promedio"].Visible = false;
                dgvRutasEnlazadasView.Columns["Meta"].Visible = false;

                dgvRutasEnlazadasView.BestFitColumns();
                dgvRutasEnlazadasView.ExpandAllGroups();
            }

            dgvRutasEnlazadasView.Columns["Rend. Ruta"].Summary.Clear();
            dgvRutasEnlazadasView.Columns["Rend. Ruta"].Summary.Add(DevExpress.Data.SummaryItemType.Average, "Rend. Ruta", "Prom = {0:N2}");

            pIngresarMetas.Visible = true;
        }

        private void dtgMetasRendimiento_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string ES = dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "idMetaRendimiento").ToString();

                if (ES != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarToolStripMenuItem.Enabled = true; }
                }
                else
                {
                    eliminarToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                eliminarToolStripMenuItem.Enabled = false;
            }
        }

        private void editarMetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataTable dtMetasFiltro = new DataTable();

            BuscarRutas();

            dtgRutasEnlazadas.DataSource = null;
            gRutas.Enabled = false;
            btnGuardar.Enabled = false;
            btnActualizar.Visible = true;

            txtTitulo.ReadOnly = true;
            txtOP.Visible = true;
            txtRE.Visible = true;
            txtMA.Visible = true;
            txtMO.Visible = true;
            label9.Visible = true;
            txtRendimiento.Visible = true;
            txtRendimiento.ReadOnly = false;
            txtMeta.ReadOnly = false;

            int idMetaRendimiento = Convert.ToInt32(dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "idMetaRendimiento"));
            int Anio = Convert.ToInt32(dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "Anio"));

            dtMetasFiltro = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_FiltrarMetas(idMetaRendimiento, Anio);
            dtgRutasEnlazadas.DataSource = dtMetasFiltro;
            if (dtMetasFiltro.Rows.Count > 0)
            {
                txtTitulo.Text = dtMetasFiltro.Rows[0]["Ruta"].ToString();
                txtOP.Text = dtMetasFiltro.Rows[0]["Operacion"].ToString();
                txtRE.Text = dtMetasFiltro.Rows[0]["Region"].ToString();
                txtMA.Text = dtMetasFiltro.Rows[0]["Marca"].ToString();
                txtMO.Text = dtMetasFiltro.Rows[0]["Modelo"].ToString();
                txtRendimiento.Text = dtMetasFiltro.Rows[0]["Rend. Promedio"].ToString();
                txtMeta.Text = dtMetasFiltro.Rows[0]["Meta"].ToString();

                dgvRutasEnlazadasView.Columns["Operacion"].Visible = false;
                dgvRutasEnlazadasView.Columns["Region"].Visible = false;
                dgvRutasEnlazadasView.Columns["Ruta"].Visible = false;
                dgvRutasEnlazadasView.Columns["Marca"].Visible = false;
                dgvRutasEnlazadasView.Columns["Modelo"].Visible = false;
                dgvRutasEnlazadasView.Columns["Rend. Promedio"].Visible = false;
                dgvRutasEnlazadasView.Columns["Meta"].Visible = false;

                dgvRutasEnlazadasView.BestFitColumns();
                dgvRutasEnlazadasView.ExpandAllGroups();
            }

            pIngresarMetas.Visible = true;
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar esta meta de forma permanente?", "ELIMINAR META DE RENDIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idMetaRendimiento = Convert.ToInt32(dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "idMetaRendimiento"));
                int Anio = Convert.ToInt32(dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "Anio"));
                string ExisteMeta = Convert.ToString(dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "idRutas"));
                int idMetaEnlazada;

                if (ExisteMeta == "") { idMetaEnlazada = 0; }
                else { idMetaEnlazada = Convert.ToInt32(dgvMetasRendimientoVista.GetRowCellValue(dgvMetasRendimientoVista.FocusedRowHandle, "idRutas")); }

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsCombustibleBL.Instancia.ReportesApp_Combustible_MetasRendimiento_EliminarMetas(idMetaRendimiento, Anio, idMetaEnlazada);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarMetas();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
