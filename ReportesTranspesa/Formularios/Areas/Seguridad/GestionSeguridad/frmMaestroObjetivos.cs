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

namespace ReportesTranspesa.Formularios.Areas.Seguridad.GestionSeguridad
{
    public partial class frmMaestroObjetivos : Form
    {
        public DataTable dtListaObjetivosE = new DataTable();
        public DataTable dtListaObjetivosO = new DataTable();
        public DataTable dtListaActividades = new DataTable();
        private DataTable dtEstrategico = new DataTable();
        private DataTable dtOperativo = new DataTable();
        string xmlOE = "", xmlAO = "";

        public frmMaestroObjetivos()
        {
            InitializeComponent();

            dtEstrategico.Columns.Add("Marca", typeof(bool));
            dtEstrategico.Columns.Add("idObjetivoE", typeof(String));
            dtEstrategico.Columns.Add("ObjetivoE", typeof(String));

            dtOperativo.Columns.Add("Marca", typeof(bool));
            dtOperativo.Columns.Add("idObjetivoO", typeof(String));
            dtOperativo.Columns.Add("ObjetivoO", typeof(String));
        }

        private void frmMaestroObjetivos_Load(object sender, EventArgs e)
        {
            ListarObjetivosActividades(1);
            ListarTablaOE();
            ListarObjetivosActividades(2);
            ListarTablaOA();
            ListarObjetivosActividades(3);
        }


        public void RegistrarObjetivos(int Opcion)
        {
            if (Opcion == 1 && txtObjetivoE.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese un objetivo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtObjetivoE.Focus();
                return;
            }
            else
            {
                if (Opcion == 1)
                {
                    DataTable dtActividad = new DataTable();
                    string respta;
                    dtActividad = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_InsertarObjetivosActividades(Opcion, txtObjetivoE.Text, "");
                    respta = Convert.ToString(dtActividad.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        ListarObjetivosActividades(Opcion);
                        ListarTablaOE();
                        txtObjetivoE.Clear();
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            if (Opcion == 2)
            {
                for (int i = 0; i < dtgListaObjetivosE.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtgListaObjetivosE.Rows[i].Cells["Marca"].Value))
                    { dtEstrategico.Rows.Add(Convert.ToBoolean(dtgListaObjetivosE.Rows[i].Cells["Marca"].Value), dtgListaObjetivosE.Rows[i].Cells["idObjetivoE"].Value.ToString(), dtgListaObjetivosE.Rows[i].Cells["ObjetivoE"].Value.ToString().TrimEnd()); }
                }

                if (dtEstrategico.Rows.Count > 0) { xmlOE = Utilitario.Instancia.DatatableToXml(dtEstrategico); }
            }

            if (Opcion == 2 && (txtObjetivoO.Text.Length == 0 || xmlOE == ""))
            {
                MessageBox.Show("Por favor, ingrese y asigne un objetivo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtObjetivoO.Focus();
                return;
            }
            else
            {
                if (Opcion == 2)
                {
                    DataTable dtActividad = new DataTable();
                    string respta;
                    dtActividad = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_InsertarObjetivosActividades(Opcion, txtObjetivoO.Text, xmlOE);
                    respta = Convert.ToString(dtActividad.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        ListarObjetivosActividades(Opcion);
                        ListarTablaOE();
                        ListarTablaOA();
                        txtObjetivoO.Clear();
                        dtEstrategico.Clear();
                        xmlOE = "";
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            if (Opcion == 3)
            {
                for (int i = 0; i < dtgListaObjetivosO.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtgListaObjetivosO.Rows[i].Cells["Marca2"].Value))
                    { dtOperativo.Rows.Add(Convert.ToBoolean(dtgListaObjetivosO.Rows[i].Cells["Marca2"].Value), dtgListaObjetivosO.Rows[i].Cells["idObjetivoO"].Value.ToString(), dtgListaObjetivosO.Rows[i].Cells["ObjetivoO"].Value.ToString().TrimEnd()); }
                }

                if (dtOperativo.Rows.Count > 0) { xmlAO = Utilitario.Instancia.DatatableToXml(dtOperativo); }
            }

            if (Opcion == 3 && (txtActividad.Text.Length == 0 || xmlAO == ""))
            {
                MessageBox.Show("Por favor, ingrese y asigne una actividad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtActividad.Focus();
                return;
            }
            else
            {
                if (Opcion == 3)
                {
                    DataTable dtActividad = new DataTable();
                    string respta;
                    dtActividad = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_InsertarObjetivosActividades(Opcion, txtActividad.Text, xmlAO);
                    respta = Convert.ToString(dtActividad.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        ListarObjetivosActividades(Opcion);
                        txtActividad.Clear();
                        ListarTablaOE();
                        ListarTablaOA();
                        dtOperativo.Clear();
                        xmlAO = "";
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        public void ListarTablaOE()
        {
            DataTable dtOE = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarObjetivosActividades(1, txtActividad.Text);
            dtgListaObjetivosE.Rows.Clear();
            if (dtOE.Rows.Count > 0)
            {
                dtgListaObjetivosE.Rows.Clear();
                for (int i = 0; i < dtOE.Rows.Count; i++)
                { dtgListaObjetivosE.Rows.Add(false, dtOE.Rows[i]["NRO"], dtOE.Rows[i]["OBJETIVO_ESTRATEGICO"]); }
            }
            else { dtgListaObjetivosE.DataSource = null; }
        }

        public void ListarTablaOA()
        {
            DataTable dtOO = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarObjetivosActividades(21, txtActividad.Text);
            dtgListaObjetivosO.Rows.Clear();
            if (dtOO.Rows.Count > 0)
            {
                dtgListaObjetivosO.Rows.Clear();
                for (int i = 0; i < dtOO.Rows.Count; i++)
                { dtgListaObjetivosO.Rows.Add(false, dtOO.Rows[i]["NRO"], dtOO.Rows[i]["OBJETIVO_OPERATIVO"]); }
            }
            else { dtgListaObjetivosO.DataSource = null; }
        }

        public void EliminarObjetivosActividades(int Opcion)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este elemento?", "ELIMINAR OBJETIVOS / ACTIVIDADES", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (Opcion == 1)
                    {
                        int idAO = Convert.ToInt32(dgvOEstrategicosVista.GetRowCellValue(dgvOEstrategicosVista.FocusedRowHandle, "NRO"));

                        DataTable dtRespuesta = new DataTable();
                        string respta;
                        dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_EliminarObjetivosActividades(Opcion, idAO);
                        respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            ListarObjetivosActividades(Opcion);
                            ListarTablaOE();
                        }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    if (Opcion == 2)
                    {
                        int idAO = Convert.ToInt32(dgvOOperativosVista.GetRowCellValue(dgvOOperativosVista.FocusedRowHandle, "NRO"));

                        DataTable dtRespuesta = new DataTable();
                        string respta;
                        dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_EliminarObjetivosActividades(Opcion, idAO);
                        respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            ListarObjetivosActividades(Opcion);
                            ListarTablaOA();
                        }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    if (Opcion == 3)
                    {
                        int idAO = Convert.ToInt32(dgvActividadesVista.GetRowCellValue(dgvActividadesVista.FocusedRowHandle, "NRO"));

                        DataTable dtRespuesta = new DataTable();
                        string respta;
                        dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_EliminarObjetivosActividades(Opcion, idAO);
                        respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0") { ListarObjetivosActividades(Opcion); }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
            catch { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void ListarObjetivosActividades(int Opcion)
        {
            if (Opcion == 1)
            {
                dtListaObjetivosE = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarObjetivosActividades(Opcion, txtBuscarObjetivoE.Text);
                dtgOEstrategicos.DataSource = dtListaObjetivosE;
                if (dtListaObjetivosE.Rows.Count > 0) { dgvOEstrategicosVista.BestFitColumns(); }
            }

            if (Opcion == 2)
            {
                dtListaObjetivosO = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarObjetivosActividades(Opcion, txtBuscarObjetivoO.Text);
                dtgOOperativos.DataSource = dtListaObjetivosO;
                if (dtListaObjetivosO.Rows.Count > 0)
                {
                    dgvOOperativosVista.Columns["idObjetivoE"].Visible = false;
                    dgvOOperativosVista.BestFitColumns();
                }
            }

            if (Opcion == 3)
            {
                dtListaActividades = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarObjetivosActividades(Opcion, txtBuscarActividad.Text);
                dtgActividades.DataSource = dtListaActividades;
                if (dtListaActividades.Rows.Count > 0)
                {
                    dgvActividadesVista.Columns["idObjetivoO"].Visible = false;
                    dgvActividadesVista.BestFitColumns();
                }
            }
        }

        public void VincularObjetivosActividades(int Opcion)
        {
            if (Opcion == 2)
            {
                for (int i = 0; i < dtgListaObjetivosE.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtgListaObjetivosE.Rows[i].Cells["Marca"].Value))
                    { dtEstrategico.Rows.Add(Convert.ToBoolean(dtgListaObjetivosE.Rows[i].Cells["Marca"].Value), dtgListaObjetivosE.Rows[i].Cells["idObjetivoE"].Value.ToString(), dtgListaObjetivosE.Rows[i].Cells["ObjetivoE"].Value.ToString().TrimEnd()); }
                }

                if (dtEstrategico.Rows.Count > 0) { xmlOE = Utilitario.Instancia.DatatableToXml(dtEstrategico); }

                if (xmlOE == "")
                {
                    MessageBox.Show("Por favor, asigne un objetivo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtObjetivoO.Focus();
                    return;
                }
                else
                {
                    int idAO = Convert.ToInt32(dgvOOperativosVista.GetRowCellValue(dgvOOperativosVista.FocusedRowHandle, "NRO"));
                    DataTable dtActividad = new DataTable();
                    string respta;

                    dtActividad = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_AsignarObjetivosActividades(Opcion, idAO, xmlOE);
                    respta = Convert.ToString(dtActividad.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        ListarObjetivosActividades(Opcion);
                        dtEstrategico.Clear();
                        ListarTablaOE();
                        xmlOE = "";
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            if (Opcion == 3)
            {
                for (int i = 0; i < dtgListaObjetivosO.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dtgListaObjetivosO.Rows[i].Cells["Marca2"].Value))
                    { dtOperativo.Rows.Add(Convert.ToBoolean(dtgListaObjetivosO.Rows[i].Cells["Marca2"].Value), dtgListaObjetivosO.Rows[i].Cells["idObjetivoO"].Value.ToString(), dtgListaObjetivosO.Rows[i].Cells["ObjetivoO"].Value.ToString().TrimEnd()); }
                }

                if (dtOperativo.Rows.Count > 0) { xmlAO = Utilitario.Instancia.DatatableToXml(dtOperativo); }

                if (xmlAO == "")
                {
                    MessageBox.Show("Por favor, asigne una actividad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtActividad.Focus();
                    return;
                }
                else
                {
                    int idAO = Convert.ToInt32(dgvActividadesVista.GetRowCellValue(dgvActividadesVista.FocusedRowHandle, "NRO"));
                    DataTable dtActividad = new DataTable();
                    string respta;

                    dtActividad = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_AsignarObjetivosActividades(Opcion, idAO, xmlAO);
                    respta = Convert.ToString(dtActividad.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        ListarObjetivosActividades(Opcion);
                        dtOperativo.Clear();
                        ListarTablaOA();
                        xmlAO = "";
                    }
                    else
                    { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        public void DesvincularObjetivosActividades(int Opcion)
        {
            try
            {
                if (MessageBox.Show("¿Desea desvincular este elemento?", "DESVINCULAR OBJETIVOS / ACTIVIDADES", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (Opcion == 2)
                    {
                        int idObjetivosO = Convert.ToInt32(dgvOOperativosVista.GetRowCellValue(dgvOOperativosVista.FocusedRowHandle, "NRO"));
                        int idObjetivosE = Convert.ToInt32(Convert.ToString(dgvOOperativosVista.GetRowCellValue(dgvOOperativosVista.FocusedRowHandle, "idObjetivoE")) == "" ? 0 : dgvOOperativosVista.GetRowCellValue(dgvOOperativosVista.FocusedRowHandle, "idObjetivoE"));

                        DataTable dtRespuesta = new DataTable();
                        string respta;
                        dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_DesvincularObjetivosActividades(Opcion, idObjetivosO, idObjetivosE);
                        respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            ListarObjetivosActividades(Opcion);
                        }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                    if (Opcion == 3)
                    {
                        int Actividad = Convert.ToInt32(dgvActividadesVista.GetRowCellValue(dgvActividadesVista.FocusedRowHandle, "NRO"));
                        int idObjetivosO = Convert.ToInt32(Convert.ToString(dgvActividadesVista.GetRowCellValue(dgvActividadesVista.FocusedRowHandle, "idObjetivoO")) == "" ? 0 : dgvActividadesVista.GetRowCellValue(dgvActividadesVista.FocusedRowHandle, "idObjetivoO"));

                        DataTable dtRespuesta = new DataTable();
                        string respta;
                        dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_DesvincularObjetivosActividades(Opcion, Actividad, idObjetivosO);
                        respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            ListarObjetivosActividades(Opcion);
                        }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
            catch { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void btnGuardarE_Click(object sender, EventArgs e) { RegistrarObjetivos(1); }

        private void txtObjetivoE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { RegistrarObjetivos(1); }
        }

        private void txtBuscarObjetivoE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarObjetivosActividades(1); }
        }

        private void dtgOEstrategicos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idObjetivo = Convert.ToInt32(dgvOEstrategicosVista.GetRowCellValue(dgvOEstrategicosVista.FocusedRowHandle, "NRO"));

                if (idObjetivo != 0) { eliminarOEstrategicoToolStripMenuItem.Enabled = true; }
                else { eliminarOEstrategicoToolStripMenuItem.Enabled = false; }
            }
            catch { eliminarOEstrategicoToolStripMenuItem.Enabled = false; }
        }

        private void dtgOOperativos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idObjetivo = Convert.ToInt32(dgvOOperativosVista.GetRowCellValue(dgvOOperativosVista.FocusedRowHandle, "NRO"));

                if (idObjetivo != 0)
                {
                    vincularOEToolStripMenuItem.Enabled = true;
                    desvincularOEToolStripMenuItem.Enabled = true;
                    eliminarOOperativoToolStripMenuItem.Enabled = true;
                }
                else
                {
                    vincularOEToolStripMenuItem.Enabled = false;
                    desvincularOEToolStripMenuItem.Enabled = false;
                    eliminarOOperativoToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                vincularOEToolStripMenuItem.Enabled = false;
                desvincularOEToolStripMenuItem.Enabled = false;
                eliminarOOperativoToolStripMenuItem.Enabled = false;
            }
        }

        private void eliminarOEstrategicoToolStripMenuItem_Click(object sender, EventArgs e) { EliminarObjetivosActividades(1); }

        private void dtgListaObjetivosE_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaObjetivosE.CurrentRow.Cells["Marca"].Value)) { dtgListaObjetivosE.CurrentRow.Cells["Marca"].Value = false; }
            else { dtgListaObjetivosE.CurrentRow.Cells["Marca"].Value = true; }
        }

        private void dtgListaObjetivosE_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaObjetivosE.CurrentRow.Cells["Marca"].Value)) { dtgListaObjetivosE.CurrentRow.Cells["Marca"].Value = false; }
            else { dtgListaObjetivosE.CurrentRow.Cells["Marca"].Value = true; }

            foreach (DataGridViewRow row in dtgListaObjetivosE.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value);
            }
        }

        private void btnGuardarO_Click(object sender, EventArgs e) { RegistrarObjetivos(2); }

        private void txtObjetivoO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { RegistrarObjetivos(2); }
        }

        private void txtBuscarObjetivoO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarObjetivosActividades(2); }
        }

        private void desvincularOEToolStripMenuItem_Click(object sender, EventArgs e) { DesvincularObjetivosActividades(2); }

        private void eliminarOOperativoToolStripMenuItem_Click(object sender, EventArgs e) { EliminarObjetivosActividades(2); }

        private void dtgListaObjetivosO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaObjetivosO.CurrentRow.Cells["Marca2"].Value)) { dtgListaObjetivosO.CurrentRow.Cells["Marca2"].Value = false; }
            else { dtgListaObjetivosO.CurrentRow.Cells["Marca2"].Value = true; }
        }

        private void vincularOEToolStripMenuItem_Click(object sender, EventArgs e) { VincularObjetivosActividades(2); }

        private void dtgListaObjetivosO_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaObjetivosO.CurrentRow.Cells["Marca2"].Value)) { dtgListaObjetivosO.CurrentRow.Cells["Marca2"].Value = false; }
            else { dtgListaObjetivosO.CurrentRow.Cells["Marca2"].Value = true; }

            foreach (DataGridViewRow row in dtgListaObjetivosO.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value);
            }
        }

        private void btnActividad_Click(object sender, EventArgs e) { RegistrarObjetivos(3); }

        private void txtActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { RegistrarObjetivos(3); }
        }

        private void vincularOOToolStripMenuItem_Click(object sender, EventArgs e) { VincularObjetivosActividades(3); }

        private void desvincularOOToolStripMenuItem_Click(object sender, EventArgs e) { DesvincularObjetivosActividades(3); }

        private void eliminarActividadToolStripMenuItem_Click(object sender, EventArgs e) { EliminarObjetivosActividades(3); }

        private void dtgActividades_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idObjetivo = Convert.ToInt32(dgvActividadesVista.GetRowCellValue(dgvActividadesVista.FocusedRowHandle, "NRO"));

                if (idObjetivo != 0)
                {
                    vincularOOToolStripMenuItem.Enabled = true;
                    desvincularOOToolStripMenuItem.Enabled = true;
                    eliminarActividadToolStripMenuItem.Enabled = true;
                }
                else
                {
                    vincularOOToolStripMenuItem.Enabled = false;
                    desvincularOOToolStripMenuItem.Enabled = false;
                    eliminarActividadToolStripMenuItem.Enabled = false;
                }
            }
            catch
            {
                vincularOOToolStripMenuItem.Enabled = false;
                desvincularOOToolStripMenuItem.Enabled = false;
                eliminarActividadToolStripMenuItem.Enabled = false;
            }
        }

        private void txtBuscarActividad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarObjetivosActividades(3); }
        }
    }
}
