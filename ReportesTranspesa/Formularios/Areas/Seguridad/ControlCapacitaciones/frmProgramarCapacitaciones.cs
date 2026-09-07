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


namespace ReportesTranspesa.Formularios.Areas.Seguridad.ControlCapacitaciones
{
    public partial class frmProgramarCapacitaciones : Form
    {
        private DataTable dt = new DataTable();
        private DataTable dt2 = new DataTable();
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        public string Operacion = "TODOS";
        public int xClick, yClick;
        public int TipoProg = 0, Especifico = 0;
        public string L = "", M = "", X = "", J = "", V = "", S = "";

        public frmProgramarCapacitaciones()
        {
            InitializeComponent();

            dt.Columns.Add("Marca", typeof(bool));
            dt.Columns.Add("idOperacion", typeof(int));
            dt.Columns.Add("Grupo", typeof(String));
            dt.Columns.Add("Area", typeof(String));

            dt2.Columns.Add("Marca2", typeof(bool));
            dt2.Columns.Add("idPersona", typeof(int));
            dt2.Columns.Add("idGrupo", typeof(int));
            dt2.Columns.Add("idArea", typeof(int));
            dt2.Columns.Add("Area2", typeof(String));
            dt2.Columns.Add("Nombres", typeof(String));

            cbxMes.SelectedIndexChanged -= cbxMes_SelectedIndexChanged;
            cbxLugar.SelectedIndexChanged -= cbxLugar_SelectedIndexChanged;
            cbxProceso.SelectedIndexChanged -= cbxProceso_SelectedIndexChanged;
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
        }

        private void cbxMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboMes();
        }

        private void cbxLugar_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboLugar();
        }

        private void cbxProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboProceso();
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboArea();
        }

        private void frmProgramarCapacitaciones_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListarCapacitaciones");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                    {
                        btnGuardar.Enabled = true;
                        txtTema.Enabled = true;
                        dtgListaAreas.Enabled = true;
                        btnAgregar.Enabled = true;
                    }
                    else
                    {
                        btnGuardar.Enabled = false;
                        txtTema.Enabled = false;
                        dtgListaAreas.Enabled = false;
                        btnAgregar.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarToolStripMenuItem.Enabled = true; }
                    else { eliminarToolStripMenuItem.Enabled = false; }
                }

                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                {
                    dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());
                }

                if (dtEspeciales != null)
                {
                    if (dtEspeciales.Rows.Count > 0)
                    {
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Todos") { Operacion = "TODOS"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Almacenes") { Operacion = "ALM"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Contabilidad") { Operacion = "CON"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "RRHH") { Operacion = "GTH"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Mantenimiento") { Operacion = "MAN"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Lindley") { Operacion = "LND"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Limagas") { Operacion = "LMG"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Tolvas") { Operacion = "TLV"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Finanzas") { Operacion = "FNZ"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Logistica") { Operacion = "LOG"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Combustible") { Operacion = "COM"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "TI") { Operacion = "TI"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Legal") { Operacion = "LGL"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Aduanas") { Operacion = "ADU"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Produccion") { Operacion = "PD"; }
                        if (dtEspeciales.Rows[dtEspeciales.Rows.Count - 1]["NombrePermiso"].ToString() == "Operaciones") { Operacion = "OP"; }
                    }
                }
            }

            CargarComboMes();
            cbxMes.SelectedValue = DateTime.Now.Month;
            CargarComboLugar();
            CargarComboProceso();
            
            CargarComboArea();
            cbxArea_DropDownClosed(sender, e);
            cbEspecifico.Checked = false;

        }

        private void CargarComboMes()
        {
            DataTable dtMes = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarCombo(2);
            cbxMes.DataSource = dtMes;
            cbxMes.DisplayMember = "Mes";
            cbxMes.ValueMember = "idMes";
        }

        private void CargarComboLugar()
        {
            DataTable dtLugar = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarCombo(3);
            cbxLugar.DataSource = dtLugar;
            cbxLugar.DisplayMember = "Descripcion";
            cbxLugar.ValueMember = "idLugar";
        }

        private void CargarComboProceso()
        {
            DataTable dtProceso = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarCombo(4);
            cbxProceso.DataSource = dtProceso;
            cbxProceso.DisplayMember = "Descripcion";
            cbxProceso.ValueMember = "idProceso";
        }

        private void CargarComboArea()
        {
            DataTable dtArea = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarAreasPermisos(Operacion);
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "Descripcion";
            cbxArea.ValueMember = "idOperacion";
        }

        public void ListarProgramaciones()
        {
            dtgvListaProgramaciones.DataSource = null;
            dtgvListaProgramacionesView.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarProgramaciones(TipoProg, cbxArea.Text);
            if (dt.Rows.Count > 0)
            {
                dtgvListaProgramaciones.DataSource = dt;

                dtgvListaProgramacionesView.Columns["Nro"].Visible = false;
                dtgvListaProgramacionesView.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvListaProgramacionesView.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dtgvListaProgramacionesView.BestFitColumns();
            }
        }

        public void CargarAreas()
        {
            DataTable dtAreas = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarOperacionGrupo();
            dtgListaAreas.Rows.Clear();
            if (dtAreas.Rows.Count > 0)
            {
                dtgListaAreas.Rows.Clear();
                for (int i = 0; i < dtAreas.Rows.Count; i++)
                { dtgListaAreas.Rows.Add(false, dtAreas.Rows[i]["idOperacion"], dtAreas.Rows[i]["GRUPO"], dtAreas.Rows[i]["OPERACION"]); }
            }
            else { dtgListaAreas.DataSource = null; }
        }

        public void CargarPersonal()
        {
            DataTable dtPersonal = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ListarPersonal("  ");
            dtgListaPersonal.Rows.Clear();
            if (dtPersonal.Rows.Count > 0)
            {
                dtgListaPersonal.Rows.Clear();
                for (int i = 0; i < dtPersonal.Rows.Count; i++)
                {
                    dtgListaPersonal.Rows.Add(false, dtPersonal.Rows[i]["idPersona"], dtPersonal.Rows[i]["idGrupo"], dtPersonal.Rows[i]["idArea"],
                                              dtPersonal.Rows[i]["PUESTO"], dtPersonal.Rows[i]["NombreCompleto"]);
                }
            }
            else { dtgListaPersonal.DataSource = null; }
        }


        private void chkLunes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLunes.Checked == true)
            { L = "L"; }

            if (chkLunes.Checked == false)
            { L = " "; }
        }

        private void chkMartes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMartes.Checked == true)
            { M = "M"; }

            if (chkMartes.Checked == false)
            { M = " "; }
        }

        private void chkMiercoles_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMiercoles.Checked == true)
            { X = "Mi"; }

            if (chkMiercoles.Checked == false)
            { X = " "; }
        }

        private void chkJueves_CheckedChanged(object sender, EventArgs e)
        {
            if (chkJueves.Checked == true)
            { J = "J"; }

            if (chkJueves.Checked == false)
            { J = " "; }
        }

        private void chkViernes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkViernes.Checked == true)
            { V = "V"; }

            if (chkViernes.Checked == false)
            { V = " "; }
        }

        private void chkSabado_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSabado.Checked == true)
            { S = "S"; }

            if (chkSabado.Checked == false)
            { S = " "; }
        }

        private void dtgListaAreas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaAreas.CurrentRow.Cells["Marca"].Value))
            { dtgListaAreas.CurrentRow.Cells["Marca"].Value = false; }
            else
            { dtgListaAreas.CurrentRow.Cells["Marca"].Value = true; }
        }

        private void dtgListaAreas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaAreas.CurrentRow.Cells["Marca"].Value))
            { dtgListaAreas.CurrentRow.Cells["Marca"].Value = false; }
            else
            { dtgListaAreas.CurrentRow.Cells["Marca"].Value = true; }

            foreach (DataGridViewRow row in dtgListaAreas.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value);
            }
        }

        private void dtgListaPersonal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaPersonal.CurrentRow.Cells["Marca2"].Value))
            { dtgListaPersonal.CurrentRow.Cells["Marca2"].Value = false; }
            else
            { dtgListaPersonal.CurrentRow.Cells["Marca2"].Value = true; }
        }

        private void dtgListaPersonal_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaPersonal.CurrentRow.Cells["Marca2"].Value))
            { dtgListaPersonal.CurrentRow.Cells["Marca2"].Value = false; }
            else
            { dtgListaPersonal.CurrentRow.Cells["Marca2"].Value = true; }

            foreach (DataGridViewRow row in dtgListaAreas.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value);
            }
        }

        private void dtgvListaProgramaciones_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idProgramacion = Convert.ToString(dtgvListaProgramacionesView.GetRowCellValue(dtgvListaProgramacionesView.FocusedRowHandle, "Codigo"));

                if (idProgramacion != "")
                { if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarToolStripMenuItem.Enabled = true; } }
                else
                { eliminarToolStripMenuItem.Enabled = false; }
            }
            catch { eliminarToolStripMenuItem.Enabled = false; }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            pNuevoTitulo.Visible = true;
            pNuevoTitulo.BringToFront();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (txtTitulo.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtTitulo.Focus();
                return;
            }
            else
            {
                DataTable dtAgregarT = new DataTable();
                string respta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtAgregarT = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_InsertarTitulo(Especifico, txtTitulo.Text, Usuario);
                respta = Convert.ToString(dtAgregarT.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Programación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTitulo.Clear();
                    pNuevoTitulo.Visible = false;
                    pNuevoTitulo.SendToBack();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pNuevoTitulo.Visible = false;
            pNuevoTitulo.SendToBack();
        }

        private void pNuevoTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevoTitulo.Left = pNuevoTitulo.Left + (e.X - xClick);
                pNuevoTitulo.Top = pNuevoTitulo.Top + (e.Y - yClick);
            }
        }

        private void txtTema_Enter(object sender, EventArgs e) { txtTema.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtTema_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTemas, clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_FiltrarTemas(txtTema.Text), true, false, false);
            if (lstTemas.Columns.Count > 0)
            {
                lstTemas.Columns[0].Width = 0;
                lstTemas.Columns[1].Width = 400;
            }
            lstTemas.BringToFront();
            lstTemas.Visible = true;

            if (e.KeyChar == (char)Keys.Back) { lstTemas.Visible = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { chkLunes.Focus(); }
        }

        private void txtTema_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTemas.Focus(); }
        }

        private void txtTema_Leave(object sender, EventArgs e)
        { txtTema.BackColor = Color.White; }

        private void lstTemas_Enter(object sender, EventArgs e)
        {
            if (!lstTemas.Items.Count.Equals(0))
            {
                lstTemas.Items[0].Selected = true;
            }
        }

        private void lstTemas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTemas.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTemas.SelectedItems[0];
                txtTema.Text = ItemActual.SubItems[1].Text;
                lstTemas.Visible = false;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTemas.Visible = false;
                txtTema.Focus();
            }
        }

        private void lstTemas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTemas.SelectedItems[0];
            txtTema.Text = ItemActual.SubItems[1].Text;
            lstTemas.Visible = false;
        }

        private void cbEspecifico_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEspecifico.Checked == true) { Especifico = 1; }
            else { Especifico = 0; }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int idProgramacion = Convert.ToInt32(dtgvListaProgramacionesView.GetRowCellValue(dtgvListaProgramacionesView.FocusedRowHandle, "Codigo"));
                int Nro = Convert.ToInt32(dtgvListaProgramacionesView.GetRowCellValue(dtgvListaProgramacionesView.FocusedRowHandle, "Nro"));
                if (MessageBox.Show("¿Desea eliminar esta programación de forma permanente?", "ELIMINAR PROGRAMACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_EliminarProgramacion(TipoProg, idProgramacion, Nro);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarProgramaciones(); }
                    else
                    { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch
            { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cbxArea_DropDownClosed(object sender, EventArgs e)
        {
            ListarProgramaciones();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (TipoProg == 1)
            {
                try
                {
                    dt.Rows.Clear();
                    string xml = "";

                    for (int i = 0; i < dtgListaAreas.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgListaAreas.Rows[i].Cells["Marca"].Value))
                        {
                            dt.Rows.Add(Convert.ToBoolean(dtgListaAreas.Rows[i].Cells["Marca"].Value), Convert.ToInt32(dtgListaAreas.Rows[i].Cells["idOperacion"].Value),
                                dtgListaAreas.Rows[i].Cells["Grupo"].Value.ToString().TrimEnd(), dtgListaAreas.Rows[i].Cells["Area"].Value.ToString().TrimEnd());
                        }
                    }

                    if (chkLunes.Checked == false && chkMartes.Checked == false && chkMiercoles.Checked == false && chkJueves.Checked == false && chkViernes.Checked == false && chkSabado.Checked == false)
                    {
                        MessageBox.Show("Por favor, marque los días de la programación.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (txtTema.Text.Length == 0 || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (txtTema.Text.Length == 0) { txtTema.Focus(); }
                        return;
                    }
                    else
                    {
                        DataTable dtAgregarC = new DataTable();
                        string respta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                        xml = Utilitario.Instancia.DatatableToXml(dt);
                        dtAgregarC = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ProgramarCapacitacion(1, txtTema.Text, Convert.ToInt32(cbxProceso.SelectedValue),
                                     L, M, X, J, V, S, Convert.ToInt32(cbxMes.SelectedValue), dtpHoraProgrI.Value, dtpHoraProgrF.Value, Convert.ToInt32(cbxLugar.SelectedValue), xml, Usuario);
                        respta = Convert.ToString(dtAgregarC.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            MessageBox.Show(respta, "Programación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cbxMes.SelectedValue = DateTime.Now.Month;
                            dtpHoraProgrI.Text = "00:00:00";
                            dtpHoraProgrF.Text = "00:00:00";
                            CargarComboLugar();
                            CargarComboProceso();
                            CargarAreas();
                            chkLunes.Checked = false; chkLunes_CheckedChanged(sender, e);
                            chkMartes.Checked = false; chkMartes_CheckedChanged(sender, e);
                            chkMiercoles.Checked = false; chkMiercoles_CheckedChanged(sender, e);
                            chkJueves.Checked = false; chkJueves_CheckedChanged(sender, e);
                            chkViernes.Checked = false; chkViernes_CheckedChanged(sender, e);
                            chkSabado.Checked = false; chkSabado_CheckedChanged(sender, e);
                            ListarProgramaciones();
                        }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
                catch { MessageBox.Show("Por favor, seleccione un área.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }

            if (TipoProg == 2)
            {
                try
                {
                    dt2.Rows.Clear();
                    string xml2 = "";

                    for (int i = 0; i < dtgListaPersonal.Rows.Count; i++)
                    {
                        if (Convert.ToBoolean(dtgListaPersonal.Rows[i].Cells["Marca2"].Value))
                        {
                            dt2.Rows.Add(Convert.ToBoolean(dtgListaPersonal.Rows[i].Cells["Marca2"].Value), Convert.ToInt32(dtgListaPersonal.Rows[i].Cells["idPersona"].Value),
                                Convert.ToInt32(dtgListaPersonal.Rows[i].Cells["idGrupo"].Value), Convert.ToInt32(dtgListaPersonal.Rows[i].Cells["idArea"].Value),
                                dtgListaPersonal.Rows[i].Cells["Area2"].Value.ToString().TrimEnd(), dtgListaPersonal.Rows[i].Cells["Nombres"].Value.ToString().TrimEnd());
                        }
                    }

                    if (chkLunes.Checked == false && chkMartes.Checked == false && chkMiercoles.Checked == false && chkJueves.Checked == false && chkViernes.Checked == false && chkSabado.Checked == false)
                    {
                        MessageBox.Show("Por favor, marque los días de la programación.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (txtTema.Text.Length == 0 || dt2.Rows.Count == 0)
                    {
                        MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (txtTema.Text.Length == 0) { txtTema.Focus(); }
                        return;
                    }
                    else
                    {
                        DataTable dtAgregarU = new DataTable();
                        string respta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                        xml2 = Utilitario.Instancia.DatatableToXml(dt2);
                        dtAgregarU = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ControlCapacitaciones_ProgramarCapacitacion(2, txtTema.Text, Convert.ToInt32(cbxProceso.SelectedValue),
                                     L, M, X, J, V, S, Convert.ToInt32(cbxMes.SelectedValue), dtpHoraProgrI.Value, dtpHoraProgrF.Value, Convert.ToInt32(cbxLugar.SelectedValue), xml2, Usuario);
                        respta = Convert.ToString(dtAgregarU.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            MessageBox.Show(respta, "Programación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cbxMes.SelectedValue = DateTime.Now.Month;
                            dtpHoraProgrI.Text = "00:00:00";
                            dtpHoraProgrF.Text = "00:00:00";
                            CargarComboLugar();
                            CargarComboProceso();
                            CargarPersonal();
                            chkLunes.Checked = false; chkLunes_CheckedChanged(sender, e);
                            chkMartes.Checked = false; chkMartes_CheckedChanged(sender, e);
                            chkMiercoles.Checked = false; chkMiercoles_CheckedChanged(sender, e);
                            chkJueves.Checked = false; chkJueves_CheckedChanged(sender, e);
                            chkViernes.Checked = false; chkViernes_CheckedChanged(sender, e);
                            chkSabado.Checked = false; chkSabado_CheckedChanged(sender, e);
                            ListarProgramaciones();
                        }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                }
                catch { MessageBox.Show("Por favor, seleccione a un empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvListaProgramaciones.DataSource == null)
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
                string nombreMes = dtfi.GetMonthName(DateTime.Now.Month);
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Capacitaciones Programadas - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvListaProgramaciones.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
