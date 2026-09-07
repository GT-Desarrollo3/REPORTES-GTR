using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Comun;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmVidaUtilEPPS : Form
    {
        int TipoEPPS;
        int VidaUtilEPPS;

        public frmVidaUtilEPPS()
        {
            InitializeComponent();
            cbxTipoEPPS.SelectedIndexChanged -= cbxTipoEPPS_SelectedIndexChanged;
            cbxTipoEPPS_Act.SelectedIndexChanged -= cbxTipoEPPS_Act_SelectedIndexChanged;
            cbxAreaProceso.SelectedIndexChanged -= cbxAreaProceso_SelectedIndexChanged;
            cbxAreaProceso_Act.SelectedIndexChanged -= cbxAreaProceso_Act_SelectedIndexChanged;
        }

        private void cbxTipoEPPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void cbxTipoEPPS_Act_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo();
        }

        private void cbxAreaProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo2();
        }

        private void cbxAreaProceso_Act_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCombo2();
        }

        private void frmVidaUtilEPPS_Shown(object sender, EventArgs e)
        {
            cbxAreaProceso.Focus();
        }

        private void frmVidaUtilEPPS_Load(object sender, EventArgs e)
        {
            try
            {
                CargarCombo();
                CargarCombo2();
                ListarVidaUtil();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CargarCombo()
        {
            DataTable dtTipoEPPS = clsSeguridadBL.Instancia.ReportesApp_ListarComboTiposEPPS();
            cbxTipoEPPS.DataSource = dtTipoEPPS;
            cbxTipoEPPS.DisplayMember = "Nombre";
            cbxTipoEPPS.ValueMember = "TipoEPPS";

            cbxTipoEPPS_Act.DataSource = dtTipoEPPS;
            cbxTipoEPPS_Act.DisplayMember = "Nombre";
            cbxTipoEPPS_Act.ValueMember = "TipoEPPS";
        }

        private void CargarCombo2()
        {
            DataTable dtPuestos = clsSeguridadBL.Instancia.GetListarPuestos();
            cbxAreaProceso.DataSource = dtPuestos;
            cbxAreaProceso.DisplayMember = "Descripcion";
            cbxAreaProceso.ValueMember = "CodigoPuesto";

            cbxAreaProceso_Act.DataSource = dtPuestos;
            cbxAreaProceso_Act.DisplayMember = "Descripcion";
            cbxAreaProceso_Act.ValueMember = "CodigoPuesto";
        }

        private void ListarVidaUtil()
        {
            DataTable dtVidaUtil = clsSeguridadBL.Instancia.GetListarVidaUtilEPPS();
            dtgVidaUtilEPPS.DataSource = dtVidaUtil;
            if (dtVidaUtil.Rows.Count > 0)
            {
                dgvExpressVista.Columns["Numero"].Visible = false;
                dgvExpressVista.Columns["ID"].Visible = false;

                dgvExpressVista.SortInfo.ClearAndAddRange(new GridColumnSortInfo[]
                { 
                    new GridColumnSortInfo(dgvExpressVista.Columns["TipoEPP"], DevExpress.Data.ColumnSortOrder.Descending)
                }, 1);

                dgvExpressVista.BestFitColumns();
                dgvExpressVista.ExpandAllGroups();
            }
        }

        private void GuardarVidaUtil()
        {
            if (cbxAreaProceso.Text.Length == 0 || txtVidaUtilEPPS.Text.Length == 0 || cbxArea2.Text.Length == 0 || cbxTipoEPPS.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtVidaUtilEPPS.Text.Length == 0)
                {
                    txtVidaUtilEPPS.Focus();
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                TipoEPPS = Convert.ToInt32(cbxTipoEPPS.SelectedValue);
                VidaUtilEPPS = Convert.ToInt32(txtVidaUtilEPPS.Text);

                dtRespuesta = clsSeguridadBL.Instancia.GetRegistrarVidaUtilEPPS(TipoEPPS, cbxAreaProceso.Text, cbxArea2.Text, VidaUtilEPPS);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarVidaUtil();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxAreaProceso.Focus();
                }
            }
        }

        private void EditarVidaUtil()
        {
            if (cbxAreaProceso_Act.Text.Length == 0 || cbxArea2_Act.Text.Length == 0 || cbxTipoEPPS_Act.Text.Length == 0 || txtVidaUtilEPPS_Act.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtVidaUtilEPPS_Act.Text.Length == 0)
                {
                    txtVidaUtilEPPS_Act.Focus();
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsSeguridadBL.Instancia.GetEditarVidaUtilEPPS(Convert.ToInt32(dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "Numero")), Convert.ToInt32(cbxTipoEPPS_Act.SelectedValue), cbxAreaProceso_Act.Text, cbxArea2_Act.Text, Convert.ToInt32(txtVidaUtilEPPS_Act.Text));
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarVidaUtil();
                    pActualizarVidaUtil.Visible = false;
                    btnGuardar.Enabled = true;
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    cbxAreaProceso_Act.Focus();
                }
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarVidaUtil();
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pActualizarVidaUtil.Visible = true;
                btnGuardar.Enabled = false;
                txtVidaUtilEPPS_Act.Text = dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "MesesVidaUtil").ToString();
                if (dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "CategoriaVidaUtil").ToString() == "")
                {
                    cbxAreaProceso_Act.SelectedIndex = 0;
                }
                else
                {
                    cbxAreaProceso_Act.Text = dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "CategoriaVidaUtil").ToString();
                }
                cbxArea2_Act.Text = dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "AreaProceso").ToString();
                if (dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "TipoEPP").ToString() == "")
                {
                    cbxTipoEPPS_Act.SelectedIndex = 0;
                }
                else
                {
                    cbxTipoEPPS_Act.SelectedValue = dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "ID").ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            pActualizarVidaUtil.Visible = false;
            btnGuardar.Enabled = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            EditarVidaUtil();
        }

        private void txtVidaUtilEPPS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                GuardarVidaUtil();
            }
        }

        private void txtVidaUtilEPPS_Act_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                EditarVidaUtil();
            }
        }
    }
}
