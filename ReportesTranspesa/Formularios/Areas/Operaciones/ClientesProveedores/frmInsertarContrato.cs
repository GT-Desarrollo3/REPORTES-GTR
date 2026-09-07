using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using System.Diagnostics;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Serializing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using ReportesTranspesa.Sistema;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ClientesProveedores
{
    public partial class frmInsertarContrato : Form
    {
        int todos, validacion;
        public int opcion, IdContrato, _IdContrato;
        char CaracterTipo;
        public int _idpersona, _idcontacto;
        public string _ruc, _razonsocial, _compania, _tipo;
        public string _telefono, _celular;      // GERARDO - 07/11
        frmContactoClienteProveedor _formulario;
        private DataTable dt = new DataTable();
        DataTable dtContrato, dtAreasInv, dtAdendas;

        public frmInsertarContrato()
        {
            InitializeComponent();
            dt.Columns.Add("Marca", typeof(bool));
            dt.Columns.Add("Codigo", typeof(String));
            dt.Columns.Add("Area", typeof(String));
            cbxTipoContrato.SelectedIndexChanged -= cbxTipoContrato_SelectedIndexChanged;
        }

        private void cbxTipoContrato_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboTipo();
        }

        private void frmInsertarContrato_Load(object sender, EventArgs e)
        {
            cbTodos.Checked = false;
            cbTodos_CheckedChanged(sender, e);

            if (_tipo == "CLIENTE")
            {
                CaracterTipo = 'C';
            }
            else
            {
                CaracterTipo = 'P';
            }

            txtTitulo.Focus();
        }


        // GERARDO - 07/11
        public void RecibirDatos(int idpersona, int idcontacto, string ruc, string razonsocial, string tipo, string Compania, string Telefono, string Celular, frmContactoClienteProveedor formulario)
        {
            _idpersona = idpersona;
            _ruc = ruc;
            _razonsocial = razonsocial;
            _tipo = tipo;
            _idcontacto = idcontacto;
            _formulario = formulario;
            _compania = Compania;
            _telefono = Telefono;
            _celular = Celular;
            txtTipo.Text = _tipo;
            txtRuc.Text = _ruc;
            txtRazonSocial.Text = _razonsocial;
            txtCompania.Text = _compania;
            txtTelCel.Text = _telefono + " - " + _celular;
        }
        // GERARDO - 07/11

        public void RecibirContrato (int idContrato)
        {
            _IdContrato = idContrato;
        }

        public void CargarComboTipo()
        {
            DataTable dtTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarTipoContratos(1);
            cbxTipoContrato.DataSource = dtTipo;
            cbxTipoContrato.DisplayMember = "Descripcion";
            cbxTipoContrato.ValueMember = "idTipoContrato";
        }

        public void ListarContrato(int IdContrato)
        {
            dtContrato = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarContrato(IdContrato);
            if (dtContrato.Rows.Count > 0)
            {
                txtTitulo.Text = dtContrato.Rows[0]["Titulo"].ToString();
                txtDescripcion.Text = dtContrato.Rows[0]["Descripcion"].ToString();
                txtConsideracion.Text = dtContrato.Rows[0]["Consideracion"].ToString();
                txtBeneficios.Text = dtContrato.Rows[0]["Beneficios"].ToString();
                dtpFechaInicio.Value = Convert.ToDateTime(dtContrato.Rows[0]["FechaInicio"]);
                dtpFechaFin.Value = Convert.ToDateTime(dtContrato.Rows[0]["FechaFin"]);
                cbxTipoContrato.Text = Convert.ToString(dtContrato.Rows[0]["TipoContrato"]);
                txtDiasAlerta.Text = dtContrato.Rows[0]["DiasAlerta"].ToString();
                txtDirectorio.Text = dtContrato.Rows[0]["RutaEnlace"].ToString();
                cbValido.Checked = Convert.ToBoolean(dtContrato.Rows[0]["Validacion"]);
            }
        }

        public void ListarAreasInvolucradas(int IdContrato)
        {
            dtAreasInv = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarAreasInvolucradas(IdContrato);
            if (dtContrato.Rows.Count > 0)
            {
                dtgListaAreas.Rows.Clear();
                for (int i = 0; i < dtAreasInv.Rows.Count; i++)
                {
                    dtgListaAreas.Rows.Add(true, dtAreasInv.Rows[i]["CodAreaSpring"], dtAreasInv.Rows[i]["Area"]);
                }
            }
            else
            {
                dtgListaAreas.DataSource = null;
            }
        }

        public void ContarAdendas(int IdContrato)
        {
            dtAdendas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ModificarAdendas(3, 0, IdContrato);
            if (dtAdendas.Rows.Count > 0)
            {
                label14.Text = dtAdendas.Rows[0]["Nro"].ToString() + " ADENDA(S)";
            }
        }


        private void txtTitulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                txtDescripcion.Focus();
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                txtConsideracion.Focus();
            }
        }

        private void txtConsideracion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                txtBeneficios.Focus();
            }
        }

        private void txtBeneficios_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                cbxTipoContrato.Focus();
            }
        }

        private void txtDiasAlerta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                dtpFechaFin.Focus();
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                txtDiasAlerta.Focus();
            }
        }

        private void txtDirectorio_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
            catch
            {
                MessageBox.Show("No se puede abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgListaAreas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) || (e.KeyChar == (char)Keys.Back))
            {
                dtgListaAreas.Visible = false;
                dtgListaAreas.SendToBack();
            }
        }

        private void dtgListaAreas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaAreas.CurrentRow.Cells["Marca"].Value))
            {
                dtgListaAreas.CurrentRow.Cells["Marca"].Value = false;
            }
            else
            {
                dtgListaAreas.CurrentRow.Cells["Marca"].Value = true;
            }
        }

        private void dtgListaAreas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaAreas.CurrentRow.Cells["Marca"].Value))
            {
                dtgListaAreas.CurrentRow.Cells["Marca"].Value = false;
            }
            else
            {
                dtgListaAreas.CurrentRow.Cells["Marca"].Value = true;
            }

            foreach (DataGridViewRow row in dtgListaAreas.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value);
            }
        }

        private void cbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTodos.Checked == true)
            {
                todos = 1;
                DataTable dtAreas = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_ListarAreas(1);
                dtgListaAreas.Rows.Clear();
                if (dtAreas.Rows.Count > 0)
                {
                    dtgListaAreas.Rows.Clear();
                    for (int i = 0; i < dtAreas.Rows.Count; i++)
                    {
                        dtgListaAreas.Rows.Add(false, dtAreas.Rows[i]["Area"], dtAreas.Rows[i]["Nombre"]);
                    }
                }
                else
                {
                    dtgListaAreas.DataSource = null;
                }
                dtgListaAreas.Enabled = false;
            }

            if (cbTodos.Checked == false)
            {
                todos = 0;
                dtgListaAreas.Enabled = true;
            }
        }

        private void cbValido_CheckedChanged(object sender, EventArgs e)
        {
            if (cbValido.Checked == true)
            {
                validacion = 1;
            }

            if (cbTodos.Checked == false)
            {
                validacion = 0;
            }
        }

        // GERARDO - 07/11
        private void btnAdendas_Click(object sender, EventArgs e)
        {
            frmListaAdendas FormAdendas = new frmListaAdendas();
            FormAdendas.RecibirDatos(_razonsocial, _tipo, txtTitulo.Text, _IdContrato, txtTelCel.Text, this);
            FormAdendas.ShowDialog();
        }
        // GERARDO - 07/11

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevoEnlace;
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        nuevoEnlace = op.FileName.Replace(" ", "%20");
                        txtDirectorio.Clear();
                        txtDirectorio.Text = "file:///" + nuevoEnlace;
                        btnRegistrar.Focus();
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmInsertarContrato_Load(sender, e);
            txtTitulo.Clear();
            txtDescripcion.Clear();
            cbxTipoContrato.SelectedValue = "1";
            txtConsideracion.Clear();
            txtBeneficios.Clear();
            txtDiasAlerta.Clear();
            txtDirectorio.Clear();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                dt.Rows.Clear();
                string xml = "";

                if (todos == 0)
                {
                    for (int i = 0; i < dtgListaAreas.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgListaAreas.Rows[i].Cells["Marca"].Value))
                        {
                            dt.Rows.Add(Convert.ToBoolean(dtgListaAreas.Rows[i].Cells["Marca"].Value), dtgListaAreas.Rows[i].Cells["Codigo"].Value.ToString(), dtgListaAreas.Rows[i].Cells["Area"].Value.ToString().TrimEnd());
                        }
                    }

                    xml = Utilitario.Instancia.DatatableToXml(dt);
                }

                if (txtTitulo.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtDiasAlerta.Text.Length == 0)
                {
                    MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    DataTable dtAgregarC = new DataTable();
                    string respta;
                    if (opcion == 1)
                    {
                        dtAgregarC = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_AgregaModificaContrato(1, CaracterTipo, 0, _idpersona, _idcontacto, 0,
                                 Convert.ToInt32(cbxTipoContrato.SelectedValue), txtTitulo.Text, txtDescripcion.Text, txtConsideracion.Text, txtBeneficios.Text, dtpFechaInicio.Value,
                                 dtpFechaFin.Value, Convert.ToInt32(txtDiasAlerta.Text), todos, xml, txtDirectorio.Text, 1, Utilitario.Instancia.SesionUsuario.usuario);
                    }
                    if (opcion == 2)
                    {
                        dtAgregarC = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_AgregaModificaContrato(2, CaracterTipo, IdContrato, _idpersona, _idcontacto, 0,
                                 Convert.ToInt32(cbxTipoContrato.SelectedValue), txtTitulo.Text, txtDescripcion.Text, txtConsideracion.Text, txtBeneficios.Text, dtpFechaInicio.Value,
                                 dtpFechaFin.Value, Convert.ToInt32(txtDiasAlerta.Text), todos, xml, txtDirectorio.Text, Convert.ToByte(cbValido.Checked), Utilitario.Instancia.SesionUsuario.usuario);
                    }
                    respta = Convert.ToString(dtAgregarC.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _formulario.ListarContratos();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Por favor, seleccione un área.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
