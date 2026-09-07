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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ClientesProveedores
{
    public partial class frmListaContratos : MetroFramework.Forms.MetroForm
    {
        public int esValido;
        public char TipoContrato;
        public byte Estado, Valido;
        DataTable dtPermisos = new DataTable();

        public frmListaContratos()
        {
            InitializeComponent();
            cbxTipo.SelectedIndexChanged -= cbxTipo_SelectedIndexChanged;
            cbxCompania.SelectedIndexChanged -= cbxCompania_SelectedIndexChanged;
        }

        private void cbxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboTipo();
        }

        private void cbxCompania_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboCompania();
        }

        private void frmListaContratos_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaContratos");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                {
                    AnularToolStripMenuItem.Enabled = true;
                }
                else { AnularToolStripMenuItem.Enabled = false; }
            }

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, 1, 1);
            dtpFechaFin.Value = new DateTime(dtpFechaFin.Value.Year, 12, 31);
            cbValido.Checked = true;
            cbValido_CheckedChanged(sender, e);
            CargarComboTipo();
            CargarComboCompania();
        }


        public void CargarComboTipo()
        {
            DataTable dtTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarTipoContratos(2);
            cbxTipo.DataSource = dtTipo;
            cbxTipo.DisplayMember = "Descripcion";
            cbxTipo.ValueMember = "idTipoPersonal";
        }

        public void CargarComboCompania()
        {
            DataTable dtCompania = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarTipoContratos(3);
            cbxCompania.DataSource = dtCompania;
            cbxCompania.DisplayMember = "Nombre";
            cbxCompania.ValueMember = "IdCompania";
        }

        public void ListarContratos()
        {
            if (Convert.ToInt32(cbxTipo.SelectedValue) == 1) { TipoContrato = 'C'; }
            else
            {
                if (Convert.ToInt32(cbxTipo.SelectedValue) == 2) { TipoContrato = 'P'; }
                else { TipoContrato = 'X'; }
            }

            string fechin, fechfin;
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";

            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtgListaContratos.DataSource = null;
                dgvListaContratosVista.Columns.Clear();

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_FiltrarContratos(TipoContrato, txtPersonal.Text, dtpFechaIni.Text, dtpFechaFin.Text, Convert.ToInt32(cbxCompania.SelectedValue), Convert.ToByte(esValido));
                if (dt.Rows.Count > 0)
                {
                    dtgListaContratos.DataSource = dt;

                    RepositoryItemHyperLinkEdit RutaEnlace = new RepositoryItemHyperLinkEdit();
                    dgvListaContratosVista.Columns["RutaEnlace"].ColumnEdit = RutaEnlace;

                    dgvListaContratosVista.Columns["IdContrato"].Visible = false;
                    dgvListaContratosVista.Columns["IdContacto"].Visible = false;
                    dgvListaContratosVista.Columns["IdContactoDetalle"].Visible = false;
                    dgvListaContratosVista.Columns["IdPersona"].Visible = false;
                    dgvListaContratosVista.Columns["IdCompania"].Visible = false;
                    dgvListaContratosVista.Columns["Tipo"].Visible = false;

                    dgvListaContratosVista.BestFitColumns();
                }
            }
        }


        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                ListarContratos();
            }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                ListarContratos();
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                ListarContratos();
            }
        }

        private void cbValido_CheckedChanged(object sender, EventArgs e)
        {
            if (cbValido.Checked == true)
            {
                esValido = 1;
            }
            else
            {
                esValido = 0;
            }
        }

        //GERARDO - 07/11
        private void dtgListaContratos_DoubleClick(object sender, EventArgs e)
        {
            int idContrato = Convert.ToInt32(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "IdContrato"));
            if (idContrato != 0)
            {
                frmInsertarContrato frmInsertarContrato = new frmInsertarContrato();
                frmInsertarContrato.txtRuc.Text = Convert.ToString(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "RUC"));
                frmInsertarContrato.txtTipo.Text = Convert.ToString(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "Persona"));
                frmInsertarContrato.txtRazonSocial.Text = Convert.ToString(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "RazónSocial"));
                frmInsertarContrato.txtCompania.Text = Convert.ToString(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "Compañía"));
                frmInsertarContrato.txtTelCel.Text = Convert.ToString(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "Telefono")) + " - " + Convert.ToString(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "Celular"));
                frmInsertarContrato.CargarComboTipo();
                frmInsertarContrato.ListarContrato(idContrato);
                frmInsertarContrato.ListarAreasInvolucradas(idContrato);
                frmInsertarContrato.RecibirContrato(idContrato);
                frmInsertarContrato._razonsocial = Convert.ToString(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "RazónSocial"));
                frmInsertarContrato._tipo = Convert.ToString(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "Persona"));
                frmInsertarContrato.cbValido.Visible = true;
                frmInsertarContrato.ContarAdendas(idContrato);

                frmInsertarContrato.txtTitulo.ReadOnly = true;
                frmInsertarContrato.cbValido.Enabled = false;
                frmInsertarContrato.txtDescripcion.ReadOnly = true;
                frmInsertarContrato.txtConsideracion.ReadOnly = true;
                frmInsertarContrato.txtBeneficios.ReadOnly = true;
                frmInsertarContrato.cbxTipoContrato.Enabled = false;
                frmInsertarContrato.dtpFechaInicio.Enabled = false;
                frmInsertarContrato.dtpFechaFin.Enabled = false;
                frmInsertarContrato.txtDiasAlerta.ReadOnly = true;
                frmInsertarContrato.cbTodos.Enabled = false;
                frmInsertarContrato.txtDirectorio.ReadOnly = true;
                frmInsertarContrato.btnBuscar.Enabled = false;
                frmInsertarContrato.btnCancelar.Enabled = false;
                frmInsertarContrato.btnRegistrar.Enabled = false;

                frmInsertarContrato.ShowDialog();
            }
            else
            {
                MessageBox.Show("El contrato seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //GERARDO - 07/11

        private void dtgListaContratos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idContrato = Convert.ToInt32(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "IdContrato"));
                Estado = Convert.ToByte(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "Validacion"));

                if (idContrato != 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { AnularToolStripMenuItem.Enabled = true; }

                    if (Estado == 1)
                    {
                        AnularToolStripMenuItem.Text = "Anular Contrato";
                        Valido = 0;
                    }
                    else
                    {
                        AnularToolStripMenuItem.Text = "Validar Contrato";
                        Valido = 1;
                    }
                }
                else
                {
                    AnularToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                AnularToolStripMenuItem.Enabled = false;
            }
        }

        private void AnularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idContrato = Convert.ToInt32(dgvListaContratosVista.GetRowCellValue(dgvListaContratosVista.FocusedRowHandle, "IdContrato"));
            
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_AnularContratos(idContrato, Valido, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarContratos();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarContratos();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaContratos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Contratos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaContratos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
