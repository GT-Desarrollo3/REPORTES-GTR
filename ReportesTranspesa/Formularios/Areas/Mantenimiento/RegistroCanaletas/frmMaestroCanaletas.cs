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
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using System.IO;
using System.Drawing.Imaging;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.RegistroCanaletas
{
    public partial class frmMaestroCanaletas : Form
    {
        DataTable dtListaCanaletas = new DataTable();
        public frmListaCanaletas formulario;
        public int xClick = 0, yClick = 0;
        public int idCanaleta, Opcion;

        public frmMaestroCanaletas()
        {
            InitializeComponent();
        }

        private void frmMaestroCanaletas_Load(object sender, EventArgs e)
        {
            cbxSucursal.Text = "LARREA";
            ListarCanaletas();
        }


        public void ListarCanaletas()
        {
            dtListaCanaletas = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_ListarCanaletas(1, cbxSucursal.Text);
            dtgMaestroCanaletas.DataSource = dtListaCanaletas;
            if (dtListaCanaletas.Rows.Count > 0)
            {
                dgvMaestroCanaletasVista.Columns["idCanaleta"].Visible = false;

                dgvMaestroCanaletasVista.BestFitColumns();
            }
        }


        private void cbxSucursal_DropDownClosed(object sender, EventArgs e) { ListarCanaletas(); }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarCanaletas(); }

        private void tsProgramarLimpieza_Click(object sender, EventArgs e)
        {
            Opcion = 1;
            label3.Text = "PROGRAMAR LIMPIEZA";
            
            idCanaleta = Convert.ToInt32(dgvMaestroCanaletasVista.GetRowCellValue(dgvMaestroCanaletasVista.FocusedRowHandle, "idCanaleta"));
            lblDescripcion.Text = Convert.ToString(dgvMaestroCanaletasVista.GetRowCellValue(dgvMaestroCanaletasVista.FocusedRowHandle, "CANALETA"));
            lblSucursal.Text = Convert.ToString(dgvMaestroCanaletasVista.GetRowCellValue(dgvMaestroCanaletasVista.FocusedRowHandle, "SUCURSAL"));

            pNuevaProgramacion.Visible = true;
            pNuevaProgramacion.BringToFront();
        }

        private void tsProgramarCambio_Click(object sender, EventArgs e)
        {
            Opcion = 2;
            label3.Text = "PROGRAMAR CAMBIO";

            idCanaleta = Convert.ToInt32(dgvMaestroCanaletasVista.GetRowCellValue(dgvMaestroCanaletasVista.FocusedRowHandle, "idCanaleta"));
            lblDescripcion.Text = Convert.ToString(dgvMaestroCanaletasVista.GetRowCellValue(dgvMaestroCanaletasVista.FocusedRowHandle, "CANALETA"));
            lblSucursal.Text = Convert.ToString(dgvMaestroCanaletasVista.GetRowCellValue(dgvMaestroCanaletasVista.FocusedRowHandle, "SUCURSAL"));

            pNuevaProgramacion.Visible = true;
            pNuevaProgramacion.BringToFront();
        }

        private void pNuevaProgramacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevaProgramacion.Left = pNuevaProgramacion.Left + (e.X - xClick);
                pNuevaProgramacion.Top = pNuevaProgramacion.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            idCanaleta = -1;
            lblDescripcion.Text = "";
            lblSucursal.Text = "";

            pNuevaProgramacion.Visible = false;
            pNuevaProgramacion.SendToBack();
            dtpFProgramacion.Value = DateTime.Now;
        }

        private void dtpFProgramacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar_Click(sender, e); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dtAgregar = new DataTable();
            string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            
            if (Opcion == 1)
            {
                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarProgramacion(1, 0, idCanaleta, dtpFProgramacion.Value, "", "", Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formulario.ListarProgramaciones();
                    btnCerrar_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            
            if (Opcion == 2)
            {
                dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlCanaletas_RegistrarEditarCambios(1, 0, idCanaleta, dtpFProgramacion.Value, " ", Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formulario.ListarCambios();
                    btnCerrar_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
