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
using DevExpress.XtraEditors;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmAsignarEPPS_Personal : Form
    {
        private DataTable dt = new DataTable();
        
        public frmAsignarEPPS_Personal()
        {
            InitializeComponent();
            dt.Columns.Add("Marca", typeof(bool));
            dt.Columns.Add("Numero", typeof(String));
            dt.Columns.Add("ID", typeof(String));
            dt.Columns.Add("TipoEPP", typeof(String));
            dt.Columns.Add("CategoriaVidaUtil", typeof(String));
            dt.Columns.Add("AreaProceso", typeof(String));
            dt.Columns.Add("MesesVidaUtil", typeof(String));
        }

        private void frmAsignarEPPS_Personal_Shown(object sender, EventArgs e)
        {
            txtPersonal.Focus();
        }

        private void frmAsignarEPPS_Personal_Load(object sender, EventArgs e)
        {
            try
            {
                ReportesApp_Seguridad_ListarEmpleados();
                //ReportesApp_Seguridad_ListarVidaUtilEPPS();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ReportesApp_Seguridad_ListarEmpleados()
        {
            DataTable dtEPPS = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ListarEmpleados(txtPersonal.Text);
            dtgPersonalData.DataSource = dtEPPS;
            if (dtEPPS.Rows.Count > 0)
            {
                dgvExpressVista.Columns["ID"].Visible = false;

                dgvExpressVista.SortInfo.ClearAndAddRange(new GridColumnSortInfo[]
                { 
                    new GridColumnSortInfo(dgvExpressVista.Columns["PUESTO"], DevExpress.Data.ColumnSortOrder.Ascending)
                }, 1);

                dgvExpressVista.BestFitColumns();
                dgvExpressVista.ExpandAllGroups();
            }
        }

        /*
        private void ReportesApp_Seguridad_ListarVidaUtilEPPS()
        {
            DataTable dtEPPS = clsSeguridadBL.Instancia.GetListarVidaUtilEPPS();
            if (dtEPPS.Rows.Count > 0)
            {
                for (int i = 0; i < dtEPPS.Rows.Count; i++)
                {
                    dtgListaEPPS.Rows.Add(false, dtEPPS.Rows[i]["Numero"], dtEPPS.Rows[i]["ID"], dtEPPS.Rows[i]["TipoEPP"], dtEPPS.Rows[i]["CategoriaVidaUtil"], dtEPPS.Rows[i]["AreaProceso"], dtEPPS.Rows[i]["MesesVidaUtil"]);
                }
            }
        }
        */

        private void FiltrarVidaUtilEPPS(string puesto)
        {
            DataTable dtEPPS;
            dtEPPS = clsSeguridadBL.Instancia.FiltrarVidaUtilEPPS(puesto);
            dtgListaEPPS.Rows.Clear();
            if (dtEPPS.Rows.Count > 0)
            {
                dtgListaEPPS.Rows.Clear();
                for (int i = 0; i < dtEPPS.Rows.Count; i++)
                {
                    dtgListaEPPS.Rows.Add(false, dtEPPS.Rows[i]["Numero"], dtEPPS.Rows[i]["ID"], dtEPPS.Rows[i]["TipoEPP"], dtEPPS.Rows[i]["CategoriaVidaUtil"], dtEPPS.Rows[i]["AreaProceso"], dtEPPS.Rows[i]["MesesVidaUtil"]);
                }
            }
            else
            {
                dtgListaEPPS.DataSource = null;
            }
        }

        private void AsignarEPP()
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            
            int id = Convert.ToInt32(dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "ID"));
            dt.Rows.Clear();
            for (int i = 0; i < dtgListaEPPS.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dtgListaEPPS.Rows[i].Cells["Marca"].Value))
                {
                    dt.Rows.Add(Convert.ToBoolean(dtgListaEPPS.Rows[i].Cells["Marca"].Value), dtgListaEPPS.Rows[i].Cells["Numero"].Value.ToString(), dtgListaEPPS.Rows[i].Cells["ID"].Value.ToString(), dtgListaEPPS.Rows[i].Cells["TipoEPP"].Value.ToString(), dtgListaEPPS.Rows[i].Cells["CategoriaVidaUtil"].Value.ToString(), dtgListaEPPS.Rows[i].Cells["AreaProceso"].Value.ToString(), dtgListaEPPS.Rows[i].Cells["MesesVidaUtil"].Value.ToString());
                }
            }
            string xml = Utilitario.Instancia.DatatableToXml(dt);
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            try
            {
                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_InsertarEPPSPersonal(id, xml, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPersonal.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Seleccione un EPP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dtgListaEPPS_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaEPPS.CurrentRow.Cells["Marca"].Value))
            {
                dtgListaEPPS.CurrentRow.Cells["Marca"].Value = false;
            }
            else
            {
                dtgListaEPPS.CurrentRow.Cells["Marca"].Value = true;
            }
        }

        private void dtgPersonalData_Click(object sender, EventArgs e)
        {
            try
            {
                string puesto = dgvExpressVista.GetRowCellValue(dgvExpressVista.FocusedRowHandle, "PUESTO").ToString();
                FiltrarVidaUtilEPPS(puesto);
                for (int i = 0; i < dtgListaEPPS.Rows.Count; i++)
                {
                    dtgListaEPPS.Rows[i].Cells["Marca"].Value = false;
                }
            }
            catch
            {}
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                AsignarEPP();
            }
            catch
            {
                MessageBox.Show("Seleccione un EPP.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            ReportesApp_Seguridad_ListarEmpleados();
        }
    }
}
