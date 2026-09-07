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
    public partial class frmListaAdendas : Form
    {
        int _idContrato;
        string _razonsocial, _tipo, _titulo, _telefono;     // GERARDO - 07/11
        int xClick, yClick;
        DataTable dtAdenda;
        frmInsertarContrato _formulario;

        public frmListaAdendas()
        {
            InitializeComponent();
        }

        private void frmListaAdendas_Load(object sender, EventArgs e)
        {
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            pNuevaAdenda.Visible = false;
            ListarAdendas();
        }


        // GERARDO - 07/11
        public void RecibirDatos(string razonsocial, string tipo, string Titulo, int idContrato, string Telefono, frmInsertarContrato formulario)
        {
            _razonsocial = razonsocial;
            _tipo = tipo;
            _idContrato = idContrato;
            _titulo = Titulo;
            _formulario = formulario;
            _telefono = Telefono;

            txtTipo.Text = _tipo;
            txtRazonSocial.Text = _razonsocial;
            txtTitulo.Text = _titulo;
            txtTelefono.Text = _telefono;
        }
        // GERARDO - 07/11

        public void ListarAdendas()
        {
            dtgListaAdendas.DataSource = null;
            dgvListaAdendasVista.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarAdendas(1, _idContrato);
            if (dt.Rows.Count > 0)
            {
                dtgListaAdendas.DataSource = dt;
                dgvListaAdendasVista.Columns["IdContrato"].Visible = false;
                dgvListaAdendasVista.Columns["Estado"].Visible = false;

                RepositoryItemHyperLinkEdit RutaEnlace = new RepositoryItemHyperLinkEdit();
                dgvListaAdendasVista.Columns["RutaEnlace"].ColumnEdit = RutaEnlace;

                dgvListaAdendasVista.BestFitColumns();
            }
        }

        public void ListarAdendasAnuladas()
        {
            dtgListaAdendas.DataSource = null;
            dgvListaAdendasVista.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarAdendas(2, _idContrato);
            if (dt.Rows.Count > 0)
            {
                dtgListaAdendas.DataSource = dt;
                dgvListaAdendasVista.Columns["IdContrato"].Visible = false;
                dgvListaAdendasVista.Columns["Estado"].Visible = false;

                RepositoryItemHyperLinkEdit RutaEnlace = new RepositoryItemHyperLinkEdit();
                dgvListaAdendasVista.Columns["RutaEnlace"].ColumnEdit = RutaEnlace;

                dgvListaAdendasVista.BestFitColumns();
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            btnBuscar.Enabled = true;
            btnCancelar.Enabled = true;
            btnRegistrar.Enabled = true;
            btnCancelar_Click(sender, e);

            txtTituloAdenda.ReadOnly = false;
            txtDescripcion.ReadOnly = false;
            txtConsideracion.ReadOnly = false;
            txtBeneficios.ReadOnly = false;
            dtpFechaInicio.Enabled = true;
            dtpFechaFin.Enabled = true;
            txtDiasAlerta.ReadOnly = false;
            txtDirectorio.ReadOnly = false;

            pNuevaAdenda.Visible = true;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pNuevaAdenda.Visible = false;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtTituloAdenda.Text.Length == 0 || txtDescripcion.Text.Length == 0 || txtDiasAlerta.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                DataTable dtAgregarA = new DataTable();
                string respta;
                dtAgregarA = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_InsertarAdenda(_idContrato, txtTituloAdenda.Text, txtDescripcion.Text, txtConsideracion.Text, txtBeneficios.Text, dtpFechaInicio.Value,
                                                                                                                   dtpFechaFin.Value, Convert.ToInt32(txtDiasAlerta.Text), txtDirectorio.Text, Utilitario.Instancia.SesionUsuario.usuario);
                respta = Convert.ToString(dtAgregarA.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarAdendas();
                    pNuevaAdenda.Visible = false;
                    _formulario.ContarAdendas(_idContrato);
                }
                else
                {
                    MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtTituloAdenda.Clear();
            txtDescripcion.Clear();
            txtConsideracion.Clear();
            txtBeneficios.Clear();
            dtpFechaInicio.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            txtDiasAlerta.Clear();
            txtDirectorio.Clear();
        }

        private void cbValido_CheckedChanged(object sender, EventArgs e)
        {
            if (cbValido.Checked == true)
            {
                ListarAdendasAnuladas();
            }
            else
            {
                ListarAdendas();
            }
        }

        private void dtgListaAdendas_DoubleClick(object sender, EventArgs e)
        {
            int idAdenda = Convert.ToInt32(dgvListaAdendasVista.GetRowCellValue(dgvListaAdendasVista.FocusedRowHandle, "#"));
            int idContrato = Convert.ToInt32(dgvListaAdendasVista.GetRowCellValue(dgvListaAdendasVista.FocusedRowHandle, "IdContrato"));

            if (idAdenda != 0)
            {
                dtAdenda = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ModificarAdendas(1, idAdenda, idContrato);
                if (dtAdenda.Rows.Count > 0)
                {
                    txtTituloAdenda.Text = dtAdenda.Rows[0]["Titulo"].ToString();
                    txtDescripcion.Text = dtAdenda.Rows[0]["Descripcion"].ToString();
                    txtConsideracion.Text = dtAdenda.Rows[0]["Consideracion"].ToString();
                    txtBeneficios.Text = dtAdenda.Rows[0]["Beneficios"].ToString();
                    dtpFechaInicio.Value = Convert.ToDateTime(dtAdenda.Rows[0]["FechaInicio"]);
                    dtpFechaFin.Value = Convert.ToDateTime(dtAdenda.Rows[0]["FechaFin"]);
                    txtDiasAlerta.Text = dtAdenda.Rows[0]["DiasAlerta"].ToString();
                    txtDirectorio.Text = dtAdenda.Rows[0]["RutaEnlace"].ToString();

                    btnBuscar.Enabled = false;
                    btnCancelar.Enabled = false;
                    btnRegistrar.Enabled = false;
                    txtTituloAdenda.ReadOnly = true;
                    txtDescripcion.ReadOnly = true;
                    txtConsideracion.ReadOnly = true;
                    txtBeneficios.ReadOnly = true;
                    dtpFechaInicio.Enabled = false;
                    dtpFechaFin.Enabled = false;
                    txtDiasAlerta.ReadOnly = true;
                    txtDirectorio.ReadOnly = true;

                    pNuevaAdenda.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Este contrato no tiene adendas.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AnularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idAdenda = Convert.ToInt32(dgvListaAdendasVista.GetRowCellValue(dgvListaAdendasVista.FocusedRowHandle, "#"));
            int idContrato = Convert.ToInt32(dgvListaAdendasVista.GetRowCellValue(dgvListaAdendasVista.FocusedRowHandle, "IdContrato"));
            int estado = Convert.ToInt32(dgvListaAdendasVista.GetRowCellValue(dgvListaAdendasVista.FocusedRowHandle, "Estado"));
            DataTable dtRespuesta = new DataTable();

            if (estado == 1)
            {
                if (MessageBox.Show("¿Desea anular la adenda de este contrato?", "ANULAR ADENDA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ModificarAdendas(2, idAdenda, idContrato);
                    cbValido_CheckedChanged(sender, e);
                    ListarAdendas();
                    _formulario.ContarAdendas(_idContrato);
                }
            }
            else
            {
                if (MessageBox.Show("¿Desea recuperar la adenda de este contrato?", "RESTAURAR ADENDA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ModificarAdendas(4, idAdenda, idContrato);
                    cbValido_CheckedChanged(sender, e);
                    ListarAdendasAnuladas();
                    _formulario.ContarAdendas(_idContrato);
                }
            }
        }

        private void dtgListaAdendas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int estado = Convert.ToInt32(dgvListaAdendasVista.GetRowCellValue(dgvListaAdendasVista.FocusedRowHandle, "Estado"));

                if (estado == 1)
                {
                    AnularToolStripMenuItem.Text = "Anular Adenda";
                    AnularToolStripMenuItem.Enabled = true;
                }
                else
                {
                    AnularToolStripMenuItem.Text = "Restaurar Adenda";
                    AnularToolStripMenuItem.Enabled = true;
                }
            }
            catch
            {
                AnularToolStripMenuItem.Enabled = false;
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

        private void pNuevaAdenda_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
            {
                xClick = e.X; yClick = e.Y;
            }
            else
            {
                pNuevaAdenda.Left = pNuevaAdenda.Left + (e.X - xClick);
                pNuevaAdenda.Top = pNuevaAdenda.Top + (e.Y - yClick);
            }
        }
    }
}
