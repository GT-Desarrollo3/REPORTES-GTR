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
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraEditors.Repository;
using Comun;



namespace ReportesTranspesa.Sistema
{
    public partial class Permisos : MetroFramework.Forms.MetroForm
    {
        string REPORTE, USUARIO, EMPLEADO;
        int idReporte;
        string rpta; 
        public int idPermisoEspecial;
        DataTable dt2;
        int idEmpleado = 0;

        public Permisos()
        {
            InitializeComponent();
        }

        private void frmPermisos_Load(object sender, EventArgs e)
        {
            try
            {
                dtgvData.DataSource = null;
                dtgvDataView.Columns.Clear();
                dtgvDataView.GroupSummary.Clear();
                DataTable dt = new DataTable();

                if (Utilitario.Instancia.SesionUsuario.usuario == "JMARQUINA" || Utilitario.Instancia.SesionUsuario.usuario == "ADMINISTRADOR")
                {
                    dt = clsUsuarioBL.Instancia.GetUsuariosActivos();
                    dtgvData.DataSource = dt;
                    dtgvDataView.BestFitColumns();
                }
                else
                {
                    dt = clsUsuarioBL.Instancia.GetUsuarios();
                    dtgvData.DataSource = dt;

                    dtgvDataView.BestFitColumns();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void dtgvDataView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                dtgvData2.DataSource = null;
                dtgvDataView2.Columns.Clear();
                dtgvDataView2.GroupSummary.Clear();
                dt2 = new DataTable();
                System.Data.DataRow row = dtgvDataView.GetDataRow(dtgvDataView.FocusedRowHandle);
                if (Utilitario.Instancia.SesionUsuario.usuario == "JMARQUINA" || Utilitario.Instancia.SesionUsuario.usuario == "ADMINISTRADOR")
                {
                    USUARIO = row[0].ToString();

                    dtgvData2.DataSource = dt2;
                    foreach (DataColumn dc in dt2.Columns) { dc.ReadOnly = false; }
                    dtgvDataView2.Columns["ID"].Visible = false;
                    dtgvDataView2.BestFitColumns();
                }
                else
                {
                    USUARIO = row[0].ToString();
                    dt2 = clsUsuarioBL.Instancia.GetPermisos(USUARIO);
                    dtgvData2.DataSource = dt2;
                    foreach (DataColumn dc in dt2.Columns) { dc.ReadOnly = false; }

                    dtgvDataView2.Columns["PermisosEspeciales"].Visible = false;
                    dtgvDataView2.Columns["ID"].Visible = false;
                    Convert.ToInt32(dtgvDataView2.GetRowCellValue(dtgvDataView2.FocusedRowHandle, "idPermisoEspecial"));
                    dtgvDataView2.BestFitColumns();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        private void cargadatos()
        {
            try
            {
                dtgvData2.DataSource = null;
                dtgvDataView2.Columns.Clear();
                dtgvDataView2.GroupSummary.Clear();
                dt2 = new DataTable();
                System.Data.DataRow row = dtgvDataView.GetDataRow(dtgvDataView.FocusedRowHandle);
                if (Utilitario.Instancia.SesionUsuario.usuario == "JMARQUINA" || Utilitario.Instancia.SesionUsuario.usuario == "ADMINISTRADOR")
                {
                    USUARIO = row[0].ToString();

                    dtgvData2.DataSource = dt2;
                    foreach (DataColumn dc in dt2.Columns)
                    {
                        dc.ReadOnly = false;
                    }
                    dtgvDataView2.Columns["ID"].Visible = false;
                    dtgvDataView2.Columns["PermisosEspeciales"].Visible = false;
                    dtgvDataView2.BestFitColumns();
                }
                else
                {
                    USUARIO = row[0].ToString();
                    dt2 = clsUsuarioBL.Instancia.GetPermisos(USUARIO);
                    dtgvData2.DataSource = dt2;
                    foreach (DataColumn dc in dt2.Columns)
                    {
                        dc.ReadOnly = false;
                    }

                    dtgvDataView2.Columns["PermisosEspeciales"].Visible = false;
                    dtgvDataView2.Columns["ID"].Visible = false;
                    dtgvDataView2.BestFitColumns();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
            }
        }
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            DataTable dtPermisosUsuario = new DataTable();

            dtPermisosUsuario.Columns.Add("ID", typeof(int));
            dtPermisosUsuario.Columns.Add("Nuevo", typeof(bool));
            dtPermisosUsuario.Columns.Add("Editar", typeof(bool));
            dtPermisosUsuario.Columns.Add("Leer", typeof(bool));
            dtPermisosUsuario.Columns.Add("Anular", typeof(bool));
            dtPermisosUsuario.Columns.Add("PermisosEspeciales", typeof(string));
            try
            {
                System.Data.DataRow row = dtgvDataView.GetDataRow(dtgvDataView.FocusedRowHandle);
                string usuario = row[0].ToString();
                int[] filas = dtgvDataView2.GetSelectedRows();
                for (int i = 0; i < dtgvDataView2.RowCount; i++)
                {
                    if (dtgvDataView2.SelectedRowsCount > 0 && Convert.ToBoolean(dtgvDataView2.GetRowCellValue(i, "CHECK")) == true)
                       {
                    dtPermisosUsuario.Rows.Add(Convert.ToInt32(dtgvDataView2.GetRowCellValue(i, "ID")),
                    dtgvDataView2.GetRowCellValue(i, "Nuevo"),
                    dtgvDataView2.GetRowCellValue(i, "Editar"),
                    dtgvDataView2.GetRowCellValue(i, "Leer"),
                    dtgvDataView2.GetRowCellValue(i, "Anular"),
                    dtgvDataView2.GetRowCellValue(i, "PermisosEspeciales"));
                       }
                }
                string rpta;
                string XMLPermisoUsuario;
                XMLPermisoUsuario = Utilitario.Instancia.DatatableToXml(dtPermisosUsuario);
                DataTable dt = clsUsuarioBL.Instancia.UpdatePermisos(usuario, XMLPermisoUsuario, Utilitario.Instancia.SesionUsuario.usuario);
                rpta = Convert.ToString(dt.Rows[0]["exitos"]);
                string NrRPTA = rpta.Substring(0, 1);
                if (NrRPTA == "0")
                {
                    MessageBox.Show(rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dtgvDataView2_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                
                if (e.Column.FieldName == "CHECK")
                {
                   
                    Convert.ToBoolean(dt2.Rows[e.RowHandle]["Nuevo"] = false);
                    Convert.ToBoolean(dt2.Rows[e.RowHandle]["Editar"] = false);
                    Convert.ToBoolean(dt2.Rows[e.RowHandle]["Leer"] = false);
                    Convert.ToBoolean(dt2.Rows[e.RowHandle]["Anular"] = false);
                  
                  
                    if (Convert.ToBoolean(dt2.Rows[e.RowHandle]["CHECK"]) == false)
                    {
                        dt2.Rows[e.RowHandle]["CHECK"] = true;
                        dt2.Rows[e.RowHandle]["Nuevo"] = true;
                        dt2.Rows[e.RowHandle]["Editar"] = true;
                        dt2.Rows[e.RowHandle]["Leer"] = true;
                        dt2.Rows[e.RowHandle]["Anular"] = true;
                    }
                    else
                    {
                        dt2.Rows[e.RowHandle]["CHECK"] = false;
                        dt2.Rows[e.RowHandle]["Nuevo"] = false;
                        dt2.Rows[e.RowHandle]["Editar"] = false;
                        dt2.Rows[e.RowHandle]["Leer"] = false;
                        dt2.Rows[e.RowHandle]["Anular"] = false;
                    }
                }

                if (e.Column.FieldName == "Nuevo")
                {
                    if (Convert.ToBoolean(dt2.Rows[e.RowHandle]["Nuevo"]) == false)
                    {
                        dt2.Rows[e.RowHandle]["Nuevo"] = true;
                        dt2.Rows[e.RowHandle]["CHECK"] = true;
                    }
                    else
                    {
                        dt2.Rows[e.RowHandle]["Nuevo"] = false;
                        dt2.Rows[e.RowHandle]["CHECK"] = true;

                    }
                }

                if (e.Column.FieldName == "Editar")
                {
                    if (Convert.ToBoolean(dt2.Rows[e.RowHandle]["Editar"]) == false)
                    {
                        dt2.Rows[e.RowHandle]["Editar"] = true;
                        dt2.Rows[e.RowHandle]["CHECK"] = true;

                    }
                    else
                    {
                        dt2.Rows[e.RowHandle]["Editar"] = false;
                        dt2.Rows[e.RowHandle]["CHECK"] = true;

                    }
                }
                
                if (e.Column.FieldName == "Leer")
                {
                    if (Convert.ToBoolean(dt2.Rows[e.RowHandle]["Leer"]) == false)
                    {
                        dt2.Rows[e.RowHandle]["Leer"] = true;
                        dt2.Rows[e.RowHandle]["CHECK"] = false;

                    }
                    else
                    {
                        dt2.Rows[e.RowHandle]["Leer"] = false;
                        dt2.Rows[e.RowHandle]["CHECK"] = true;

                    }
                }

                if (e.Column.FieldName == "Anular")
                {
                    if (Convert.ToBoolean(dt2.Rows[e.RowHandle]["Anular"]) == false)
                    {
                        dt2.Rows[e.RowHandle]["Anular"] = true;
                        dt2.Rows[e.RowHandle]["CHECK"] = false;
                    }
                    else
                    {
                        dt2.Rows[e.RowHandle]["Anular"] = false;
                        dt2.Rows[e.RowHandle]["CHECK"] = true;
                    }
                }

                dt2.AcceptChanges();
                dtgvDataView2.UpdateCurrentRow();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void permisosEspecialesMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                REPORTE = dtgvDataView2.GetRowCellValue(dtgvDataView2.FocusedRowHandle, "REPORTE").ToString();
                idReporte = Convert.ToInt32(dtgvDataView2.GetRowCellValue(dtgvDataView2.FocusedRowHandle, "ID"));
                frnPermisos_Especiales_Master frmpermisos = new frnPermisos_Especiales_Master();
                frmpermisos.setearvariable(REPORTE, idReporte);
                frmpermisos.Show();
            }
            catch (Exception ex)
            {
                cargadatos();
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void permisosEspecialesUsuarioToolStripMenuItem_Click2(object sender, EventArgs e)
        {
            try
            {
                string NrRPTA;
                DataTable dtVerificaFrmUsuario = new DataTable();
                idReporte = Convert.ToInt32(dtgvDataView2.GetRowCellValue(dtgvDataView2.FocusedRowHandle, "ID"));
                dtVerificaFrmUsuario = clsUsuarioBL.Instancia.GetVerificarExistenciaFormularioPorUsuario(USUARIO, idReporte);

                rpta = Convert.ToString(dtVerificaFrmUsuario.Rows[0]["exito"]);
                NrRPTA = rpta.Substring(0, 1);
                if (NrRPTA == "0")
                {
                    EMPLEADO = dtgvDataView.GetRowCellValue(dtgvDataView.FocusedRowHandle, "EMPLEADO").ToString();
                    REPORTE = dtgvDataView2.GetRowCellValue(dtgvDataView2.FocusedRowHandle, "REPORTE").ToString();
                    idReporte = Convert.ToInt32(dtgvDataView2.GetRowCellValue(dtgvDataView2.FocusedRowHandle, "ID"));
                    idPermisoEspecial = Convert.ToInt32(dtgvDataView2.GetRowCellValue(dtgvDataView2.FocusedRowHandle, "idPermisoEspecial"));
                    frmPermisosEspeciales permisosEspeciales = new frmPermisosEspeciales();
                    permisosEspeciales.setearvariable(USUARIO, EMPLEADO, REPORTE, idReporte,idPermisoEspecial);
                    
                    if (permisosEspeciales.ShowDialog() == System.Windows.Forms.DialogResult.OK) { cargadatos(); }
                }
                else { MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void coparPermisosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                panel1.Visible = true;
                lblUsuario.Text =  dtgvDataView.GetRowCellValue(dtgvDataView.FocusedRowHandle, "EMPLEADO").ToString();
                lblUsuario.Tag = dtgvDataView.GetRowCellValue(dtgvDataView.FocusedRowHandle, "USUARIO").ToString();
                idEmpleado = Convert.ToInt32(dtgvDataView.GetRowCellValue(dtgvDataView.FocusedRowHandle, "idEmpleado").ToString());
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrar_Click(object sender, EventArgs e) { panel1.Visible = false; }

        private void txtPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataTable dt = clsConsultaBL.Instancia.ReportesApp_CargarPersonalSinUsuario();

                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtPersonal, ref lstPersonal, null,dt))
                {
                    if (txtPersonal.Tag == null) { txtPersonal.Clear(); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataTable dt = clsConsultaBL.Instancia.ReportesApp_CargarPersonalSinUsuario();
                Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtPersonal, ref lstPersonal, null, dt);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPersonal_Enter(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = clsConsultaBL.Instancia.ReportesApp_CargarPersonalSinUsuario();
                Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtPersonal, ref lstPersonal, null, dt);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataTable dt = clsConsultaBL.Instancia.ReportesApp_CargarPersonalSinUsuario();

                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtPersonal, ref  lstPersonal, null, dt))
                {
                    if (txtPersonal.Tag == null)
                    { MessageBox.Show("No a seleccionado correctamente el personal", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (idEmpleado > 0)
                {
                    if (clsConsultaBL.Instancia.ReportesApp_Permisos_CopiarPermiso(lblUsuario.Text, Convert.ToString(lblUsuario.Tag), idEmpleado, txtPersonal.Text, Convert.ToInt32(txtPersonal.Tag)))
                    {
                        txtPersonal.Clear();
                        txtPersonal.Tag = null;
                        lblUsuario.Text = "";
                        lblUsuario.Tag = null;
                        idEmpleado = -1;

                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        panel1.Visible = false;
                    }
                    else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}