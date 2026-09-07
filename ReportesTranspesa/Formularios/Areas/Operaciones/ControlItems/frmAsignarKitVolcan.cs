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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using Comun;
using Negocio;
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ControlItems
{
    public partial class frmAsignarKitVolcan : Form
    {
        public frmListaControlItems frmListaControlItems = new frmListaControlItems();
        DataTable dtListaImplemento = new DataTable();
        public int Opcion;
        int xClick = 0, yClick = 0;
        public int TipoVH, idTracto = -1, idCarreta = -1, Persona = -1;
        private Microsoft.Office.Interop.Excel.Application app;

        public frmAsignarKitVolcan()
        {
            InitializeComponent();
            cbxCategoria.SelectedIndexChanged -= cbxCategoria_SelectedIndexChanged;
        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e) { CargarCategoriasACombo(); }

        private void frmAsignarKitVolcan_Load(object sender, EventArgs e)
        {
            ListarImplementos();
            CargarCategoriasACombo();
        }


        public void CargarCategoriasACombo()
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_CategoriaListar();
            if (dtRespuesta.Rows.Count > 0)
            {
                cbxCategoria.DataSource = dtRespuesta;
                cbxCategoria.ValueMember = "IDCategoria";
                cbxCategoria.DisplayMember = "Descripcion";
            }
        }

        public void ListarImplementos()
        {
            if (Opcion == 1) { dtListaImplemento = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_ListarItems(2, ""); }
            if (Opcion == 2) { dtListaImplemento = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_ListarItems(2, ""); }
            
            dtgImplementos.DataSource = dtListaImplemento;
            
            if (dtListaImplemento.Rows.Count > 0)
            {
                dgvImplementosView.Columns["NRO"].Visible = false;
                
                dgvImplementosView.ExpandAllGroups();
                dgvImplementosView.BestFitColumns();
            }
        }

        private void exportDataTableToExcel(DataTable dt)
        {
            try
            {
                Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel._Workbook libro = app.Workbooks.Add(Type.Missing);
                Microsoft.Office.Interop.Excel._Worksheet hoja = null;
                app.Visible = true;
                hoja = libro.Sheets["Hoja1"];
                hoja = libro.ActiveSheet;

                int colIndex = 0;
                int rowIndex = 1;

                foreach (DataColumn dc in dt.Columns)
                {
                    colIndex++;
                    hoja.Cells[1, colIndex] = dc.ColumnName;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    rowIndex++;
                    colIndex = 0;

                    foreach (DataColumn dc in dt.Columns)
                    {
                        colIndex++;
                        hoja.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                    }
                }

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE HERRAMIENTAS " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");

                libro.SaveAs(nombre, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
                app.Quit();
            }
        }


        private void txtEmpleado_Enter(object sender, EventArgs e) { txtEmpleado.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtEmpleado.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 320;
            lstEmpleado.Columns[2].Width = 0;
            lstEmpleado.BringToFront();
            lstEmpleado.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                Persona = -1;
            }
        }

        private void txtEmpleado_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstEmpleado.Focus(); }
        }

        private void txtEmpleado_Leave(object sender, EventArgs e) { txtEmpleado.BackColor = Color.White; }

        private void lstEmpleado_Enter(object sender, EventArgs e)
        {
            if (!lstEmpleado.Items.Count.Equals(0)) { lstEmpleado.Items[0].Selected = true; }
        }

        private void lstEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstEmpleado.SelectedItems[0];
                Persona = Int32.Parse(ItemActual.Text);
                txtEmpleado.Text = ItemActual.SubItems[1].Text;

                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                txtTracto.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstEmpleado.Visible = false;
                lstEmpleado.SendToBack();
                txtEmpleado.Focus();
                Persona = -1;
            }
        }

        private void lstEmpleado_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstEmpleado.SelectedItems[0];
            Persona = Int32.Parse(ItemActual.Text);
            txtEmpleado.Text = ItemActual.SubItems[1].Text;

            lstEmpleado.Visible = false;
            lstEmpleado.SendToBack();
            txtTracto.Focus();
        }

        private void txtTracto_Enter(object sender, EventArgs e)
        {
            TipoVH = 1;
            txtTracto.BackColor = Color.FromArgb(192, 255, 192);
            lstTracto.Location = new System.Drawing.Point(85, 110);
        }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTracto, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtTracto.Text), true, false, false);
            lstTracto.Columns[0].Width = 0;
            lstTracto.Columns[1].Width = 80;
            lstTracto.Columns[2].Width = 100;
            lstTracto.Columns[3].Width = 0;
            lstTracto.Columns[4].Width = 0;
            lstTracto.Columns[5].Width = 0;
            lstTracto.BringToFront();
            lstTracto.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idTracto = -1;
                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }
        }

        private void txtTracto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTracto.Focus(); }
        }

        private void txtTracto_Leave(object sender, EventArgs e) { txtTracto.BackColor = Color.White; }

        private void txtCarreta_Enter(object sender, EventArgs e)
        {
            TipoVH = 2;
            txtCarreta.BackColor = Color.FromArgb(192, 255, 192);
            lstTracto.Location = new System.Drawing.Point(301, 110);
        }

        private void txtCarreta_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTracto, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtCarreta.Text), true, false, false);
            lstTracto.Columns[0].Width = 0;
            lstTracto.Columns[1].Width = 80;
            lstTracto.Columns[2].Width = 100;
            lstTracto.Columns[3].Width = 0;
            lstTracto.Columns[4].Width = 0;
            lstTracto.Columns[5].Width = 0;
            lstTracto.BringToFront();
            lstTracto.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idCarreta = -1;
                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }
        }

        private void txtCarreta_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTracto.Focus(); }
        }

        private void txtCarreta_Leave(object sender, EventArgs e) { txtCarreta.BackColor = Color.White; }

        private void lstTracto_Enter(object sender, EventArgs e)
        {
            if (!lstTracto.Items.Count.Equals(0)) { lstTracto.Items[0].Selected = true; }
        }

        private void lstTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTracto.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTracto.SelectedItems[0];

                if (TipoVH == 1)
                {
                    idTracto = Int32.Parse(ItemActual.Text);
                    txtTracto.Text = ItemActual.SubItems[1].Text;
                }

                if (TipoVH == 2)
                {
                    idCarreta = Int32.Parse(ItemActual.Text);
                    txtCarreta.Text = ItemActual.SubItems[1].Text;
                }

                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                if (TipoVH == 1) { idTracto = -1; }
                if (TipoVH == 2) { idCarreta = -1; }

                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }
        }

        private void lstTracto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTracto.SelectedItems[0];

            if (TipoVH == 1)
            {
                idTracto = Int32.Parse(ItemActual.Text);
                txtTracto.Text = ItemActual.SubItems[1].Text;
            }

            if (TipoVH == 2)
            {
                idCarreta = Int32.Parse(ItemActual.Text);
                txtCarreta.Text = ItemActual.SubItems[1].Text;
            }

            lstTracto.Visible = false;
            lstTracto.SendToBack();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (txtEmpleado.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese un empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmpleado.Focus();

                return;
            }
            else
            {
                int[] filas = dgvImplementosView.GetSelectedRows();

                if (filas.Length != 0)
                {
                    int Correcto = 0;
                    string Respuesta = "0 = Implementos asignados correctamente.";

                    for (int i = 0; i < filas.Length; i++)
                    {
                        DataTable dtRespuesta = new DataTable();
                        int NumeroItem = Convert.ToInt32(dgvImplementosView.GetRowCellValue(filas[i], "NRO"));

                        if (Opcion == 1)
                        {
                            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarEliminarItems(1, NumeroItem, Persona, idTracto, idCarreta, Utilitario.Instancia.SesionUsuario.usuario);
                            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        }

                        if (Opcion == 2)
                        {
                            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarEliminarItems(1, NumeroItem, Persona, idTracto, idCarreta, Utilitario.Instancia.SesionUsuario.usuario);
                            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        }

                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0") { Correcto = Correcto + 1; }
                    }

                    if (Correcto == filas.Length)
                    {
                        MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (Opcion == 1) { frmListaControlItems.ListarKitVolcan(); }
                        if (Opcion == 2) { frmListaControlItems.ListarKitLimagas(); }

                        txtTracto.Clear();
                        txtCarreta.Clear();
                        txtEmpleado.Clear();
                        idTracto = -1; idCarreta = -1; Persona = -1;
                        ListarImplementos();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnNuevoImp_Click(object sender, EventArgs e)
        {
            pNuevoImplemento.Visible = true;
            pNuevoImplemento.BringToFront();
            btnNuevoImp.Enabled = false;
            txtCodAlmacen.Focus();
        }

        private void pNuevoImplemento_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevoImplemento.Left = pNuevoImplemento.Left + (e.X - xClick);
                pNuevoImplemento.Top = pNuevoImplemento.Top + (e.Y - yClick);
            }
        }

        private void txtCodAlmacen_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstItems, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMaestroItems(txtCodAlmacen.Text), true, false, false);
            lstItems.Columns[0].Width = 80;
            lstItems.Columns[1].Width = 400;
            lstItems.BringToFront();
            lstItems.Visible = true;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Back)
            {
                txtItem.Clear();
                lstItems.Visible = false;
                lstItems.SendToBack();
            }
        }

        private void txtCodAlmacen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstItems.Focus(); }
        }

        private void lstItems_Enter(object sender, EventArgs e)
        {
            if (!lstItems.Items.Count.Equals(0)) { lstItems.Items[0].Selected = true; }
        }

        private void lstItems_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstItems.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstItems.SelectedItems[0];

                txtCodAlmacen.Text = ItemActual.SubItems[0].Text;
                txtItem.Text = ItemActual.SubItems[1].Text;

                lstItems.Visible = false;
                lstItems.SendToBack();

                DataTable dtUltimo = new DataTable();
                if (Opcion == 1) { dtUltimo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_ListarItems(1, txtCodAlmacen.Text); }
                if (Opcion == 2) { dtUltimo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_ListarItems(1, txtCodAlmacen.Text); }

                if (dtUltimo.Rows.Count > 0) { txtCodMax.Text = dtUltimo.Rows[0]["CODIGO"].ToString(); }
                txtCodInterno.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                txtItem.Clear();
                lstItems.Visible = false;
                lstItems.SendToBack();
            }
        }

        private void lstItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstItems.SelectedItems[0];

            txtCodAlmacen.Text = ItemActual.SubItems[0].Text;
            txtItem.Text = ItemActual.SubItems[1].Text;

            lstItems.Visible = false;
            lstItems.SendToBack();

            DataTable dtUltimo = new DataTable();
            if (Opcion == 1) { dtUltimo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_ListarItems(1, txtCodAlmacen.Text); }
            if (Opcion == 2) { dtUltimo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_ListarItems(1, txtCodAlmacen.Text); }

            if (dtUltimo.Rows.Count > 0) { txtCodMax.Text = dtUltimo.Rows[0]["CODIGO"].ToString(); }
            txtCodInterno.Focus();
        }

        private void txtCodInterno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxCategoria.Focus(); }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            txtItem.Clear();
            pNuevoImplemento.Location = new System.Drawing.Point(100, 131);
            txtCodAlmacen.Clear();
            lstItems.Visible = false;
            txtItem.Clear();
            txtCodInterno.Clear();
            txtCodMax.Clear();
            cbxCategoria.Text = "MANUALES";

            btnNuevoImp.Enabled = true;
            pNuevoImplemento.Visible = false;
            pNuevoImplemento.SendToBack();
        }

        private void txtImplemento_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar_Click(sender, e); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCodAlmacen.Text.Length == 0 || txtItem.Text.Length == 0 || txtCodInterno.Text.Length == 0)
            {
                if (txtCodAlmacen.Text.Length == 0 || txtItem.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese un implemento.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCodAlmacen.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese el código interno.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCodInterno.Focus();
                }

                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    if (Opcion == 1)
                    { dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarImplemento(1, 0, txtCodAlmacen.Text, txtCodInterno.Text, Convert.ToInt32(cbxCategoria.SelectedValue), txtItem.Text, Usuario); }
                    
                    if (Opcion == 2)
                    { dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarImplemento(1, 0, txtCodAlmacen.Text, txtCodInterno.Text, Convert.ToInt32(cbxCategoria.SelectedValue), txtItem.Text, Usuario); }

                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        //MessageBox.Show(Respuesta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCerrar_Click(sender, e);
                        ListarImplementos();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch { MessageBox.Show("No se pudo registrar el implemento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnEliminarImp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar estos implementos?", "ELIMINAR IMPLEMENTOS", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int NroI;
                int[] filas = dgvImplementosView.GetSelectedRows();

                if (filas.Length != 0)
                {
                    int Correcto = 0;
                    string Respuesta = "0 = Implementos eliminados correctamente.";

                    for (int i = 0; i < filas.Length; i++)
                    {
                        NroI = Convert.ToInt32(dgvImplementosView.GetRowCellValue(filas[i], "NRO"));

                        DataTable dtRespuesta = new DataTable();

                        if (Opcion == 1)
                        { dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_RegistrarImplemento(2, NroI, " ", " ", 0, " ", " "); }

                        if (Opcion == 2)
                        { dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_RegistrarImplemento(2, NroI, " ", " ", 0, " ", " "); }
                        
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA == "0") { Correcto = Correcto + 1; }
                    }

                    if (Correcto == filas.Length) { ListarImplementos(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("No ha seleccionado ningún implemento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Desea exportar la lista general de herramientas? SÍ = Lista General, NO = Solo Hrrtas. Disponibles.", "EXPORTAR HERRAMIENTAS", MessageBoxButtons.YesNo);

            if (result == DialogResult.No)
            {
                if (dtgImplementos.DataSource == null)
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hay data para exportar";
                    m.ShowDialog();
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "LISTA DE HERRAMIENTAS DISPONIBLES " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgImplementos.ExportToXlsx(nombre);
                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Visible = true;
                    app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));

                }
            }
            else
            {
                DataTable dtRespuesta = new DataTable();

                if (Opcion == 1) { dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitVolcan_ListarItems(3, ""); }
                if (Opcion == 2) { dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_KitLimagas_ListarItems(3, ""); }

                if (dtRespuesta.Rows.Count > 0) { exportDataTableToExcel(dtRespuesta); }
            }
        }
    }
}
