using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.Data;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Globalization;
using System.Diagnostics;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.Windows.Forms;
using System.IO;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmControlHerramientas : Form
    {
        DataTable DtItemsFiltro;
        DataTable DtHtasFiltro;
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        int e1 = 0, e2 = 0;

        public frmControlHerramientas()
        {
            InitializeComponent();
            cbxMaleta.SelectedIndexChanged -= cbxMaleta_SelectedIndexChanged;
        }

        private void cbxMaleta_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMaleta(); }

        private void frmControlHerramientas_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("Mantenimiento_ControlHerramientas");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { button6.Enabled = true; }
                else { button6.Enabled = false; }
            }

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Registro de Maletas")
                    {
                        tsRegistroMaletas.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else { tsRegistroMaletas.Enabled = false; }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Inventario de Sillas")
                    {
                        tsSillas.Enabled = true;
                        i = 999; e2 = 1;
                    }
                    else { tsSillas.Enabled = false; }
                }
            }
            else
            {
                tsRegistroMaletas.Enabled = false;
                tsSillas.Enabled = false;
            }

            splitContainer2.Panel1Collapsed = true;
            CargarComboMaleta();
            buscar(0);
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = false;
            splitContainer3.Panel1Collapsed = false;
            splitContainer3.Panel2Collapsed = true;
            splitContainer5.Panel1Collapsed = false;
            splitListaItems.Panel2Collapsed = false;

            CargarCategoriasACombo();
            cargarHerramientas();
        }

        void CargarCategoriasACombo()
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_CategoriaListar();
            if (dtRespuesta.Rows.Count > 0)
            {
                //No visibles
                comboBox1.DataSource = dtRespuesta;
                comboBox1.ValueMember = "IDCategoria";
                comboBox1.DisplayMember = "Descripcion";
            }
        }

        private void maestroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = false;
            splitContainer3.Panel2Collapsed = false;
            splitContainer3.Panel1Collapsed = true;
            buscarCategorias();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtCategoriaNombre.Text.Length == 0)
            {
                MessageBox.Show("No ha ingresado la descripción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_CategoriaRegistra(txtCategoriaNombre.Text);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCategoriaNombre.Text = "";
                buscarCategorias();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void buscarCategorias()
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_CategoriaListar();
            if (dtRespuesta.Rows.Count > 0)
            {
                //No visibles
                dgvCategorias.DataSource = dtRespuesta;
                dgvCategorias.Columns["IdCategoria"].Visible = false;
                dgvCategorias.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }
        }

        private void btnCerrarVentana1_Click(object sender, EventArgs e)
        {
            splitContainer3.Panel1Collapsed = false;
            splitContainer3.Panel2Collapsed = true;
            splitContainer2.Panel1Collapsed = true;
        }

        private void splitContainer4_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlHerramientas_ListarHerramientasXAlmacen(Utilitario.Instancia.SesionUsuario.usuario);
            if (dtRespuesta.Rows.Count > 0)
            {
                //No visibles
                splitListaItems.Panel1Collapsed = true;
                splitListaItems.Panel2Collapsed = false;
                dgvItemsAlmacen.DataSource = dtRespuesta;
                dgvItemsAlmacen.Columns["CodigoInternoMAX"].Visible = false;
                dgvItemsAlmacen.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                DtItemsFiltro = dtRespuesta;
            }
        }

        private void splitContainer6_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }



        private void dgvItemsAlmacen_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvItemsAlmacen.Columns["Item"] != null)
            {
                if (e.RowIndex != -1)
                {
                    string Item = dgvItemsAlmacen.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string Descripcion = dgvItemsAlmacen.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string max = dgvItemsAlmacen.Rows[e.RowIndex].Cells[3].Value.ToString();

                    txtCodigo.Text = Item.ToString().Trim();
                    txtDescripcion.Text = Descripcion.ToString().Trim();
                    txtCodMax.Text = max;
                }
            }
        }

        private void dgvItemsAlmacen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvItemsAlmacen_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                dgvItemsAlmacen.DataSource = DtItemsFiltro;
                return;
            }   
        }
        void filtroItems()
        {
            //Si no hay filtro, restauramos el grid original y salimos
            if (textBox2.Text == "")
            {
                dgvItemsAlmacen.DataSource = DtItemsFiltro;
                return;
            }

            string busqueda = textBox2.Text;

            try
            {
                
                if(cboFiltro.SelectedIndex==0)
                {
                    //Con LinQ buscamos las rows que coincidan
                    DataTable df = (from item in DtItemsFiltro.Rows.Cast<DataRow>()
                                    let codigo = Convert.ToString(item["Descripcion"] == null ? string.Empty : item["Descripcion"].ToString())
                                    where codigo.Contains(busqueda)
                                    select item).CopyToDataTable();
                    //Mostramos las coincidencias

                    if (df.Rows.Count > 0)
                    {
                        //No visibles
                        dgvItemsAlmacen.DataSource = df;
                    }
                }
                else
                {
                    //Con LinQ buscamos las rows que coincidan
                    DataTable df2 = (from item in DtItemsFiltro.Rows.Cast<DataRow>()
                                    let codigo = Convert.ToString(item["Item"] == null ? string.Empty : item["Item"].ToString())
                                    where codigo.Contains(busqueda)
                                    select item).CopyToDataTable();
                    //Mostramos las coincidencias

                    if (df2.Rows.Count > 0)
                    {
                        //No visibles
                        dgvItemsAlmacen.DataSource = df2;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("No hay datos.", ex);
            }
        }

        private void textBox2_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                filtroItems();
            }
        }

        private void toolStripSplitButton1_ButtonClick(object sender, EventArgs e)
        {

        }

        private void btnCerrarVentana2_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = true;
            splitContainer2.Panel2Collapsed = false;
        }

        private void txtIdEmpleado_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpleado_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpleado_TextChanged(object sender, EventArgs e)
        {
            int length = txtEmpleado.Text.Length;
            if (length == 0)
            {
                buscar(0);
                txtIdEmpleado.Text = "";
                txtNumero.Text = "";
            }
        }

        private void buscar(int Persona)
        {
            DataTable dt = new DataTable();
            dt = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_Herramientas_PersonaHta_Listar(Persona, Utilitario.Instancia.SesionUsuario.usuario);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dgvDataView.Columns["Persona"].Visible = false;
                dgvDataView.Columns["IDHerramienta"].Visible = false;
                dgvDataView.UpdateSummary();
                dgvDataView.BestFitColumns();
                dgvDataView.Columns["Trabajador"].Width = 220;
                toolStripLabel2.Text = "Herramientas Entregadas: " + dt.Rows.Count.ToString();
            }
            else
            {
                dtgvData.DataSource = null;
            }
        }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtEmpleado.Text), true, false, false);

                lvEmpleado.Columns[0].Width = 0;
                lvEmpleado.Columns[1].Width = 206;
                lvEmpleado.Columns[2].Width = 110;

                lvEmpleado.BringToFront();
                lvEmpleado.Visible = true;
                lvEmpleado.Focus();
                //splitContainer1.SplitterDistance = lvEmpleado.Top + lvEmpleado.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                //splitContainer1.SplitterDistance = 77;
                buscar(0);
            }

        }

        private void lvEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvEmpleado_DoubleClick(object sender, EventArgs e)
        {
            if (!lvEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvEmpleado.SelectedItems[0];

                txtIdEmpleado.Text = ItemActual.Text;
                txtEmpleado.Text = ItemActual.SubItems[1].Text;
                txtNumero.Text = txtIdEmpleado.Text;
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                //splitContainer1.SplitterDistance = 77;
                buscar(Convert.ToInt32(txtIdEmpleado.Text));
            }
        }

        private void lvEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvEmpleado.SelectedItems[0];

                txtIdEmpleado.Text = ItemActual.Text;
                txtEmpleado.Text = ItemActual.SubItems[1].Text;
                txtNumero.Text = txtIdEmpleado.Text;
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                //splitContainer1.SplitterDistance = 77;
                buscar(Convert.ToInt32(txtIdEmpleado.Text));
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                //splitContainer1.SplitterDistance = 77;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length == 0)
            {
                MessageBox.Show("No ha ingresado la descripción", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("No ha ingresado Codigo Interno", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int EsDeAlmacen = 0;
            if (txtCodigo.Text.Length != 0) { EsDeAlmacen = 1; }

            int IDCategoria;
            IDCategoria = Convert.ToInt32(comboBox1.SelectedValue);

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_HerramientaRegistro(txtDescripcion.Text, txtCodigo.Text, textBox1.Text, EsDeAlmacen, IDCategoria, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescripcion.Text = "";
                txtCodigo.Text = "";
                textBox1.Text = "";
                cargarHerramientas();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        void cargarHerramientas()
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_Herramientas_Listar(1, Utilitario.Instancia.SesionUsuario.usuario);

            if (dtRespuesta.Rows.Count > 0)
            {
                dgvItems.DataSource = dtRespuesta;
                dgvItemsView.Columns["IdHerramienta"].Visible = false;
                dgvItemsView.Columns["EsDeAlmacen"].Visible = false;
                dgvItemsView.UpdateSummary();
                dgvItemsView.BestFitColumns();
                toolStripLabel1.Text = dtRespuesta.Rows.Count.ToString() + " Hrrtas. Disponibles";
                DtHtasFiltro = dtRespuesta;
            }
            else
            {
                dgvItems.DataSource = null;
                toolStripLabel1.Text = " Hrrtas. Disponibles";
            }
        }

        private void btnVerHerramientas_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = false;
            splitContainer3.Panel1Collapsed = false;
            splitContainer3.Panel2Collapsed = true;
            splitContainer5.Panel1Collapsed = true;
            splitListaItems.Panel2Collapsed = true;
            btnVerHerramientas.Visible = false;
            btnOcultarHerramientas.Visible = true;
            cargarHerramientas();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string CodHta;
            string Persona;

            int[] filasPersonas = dgvDataView.GetSelectedRows();
            if (filasPersonas.Count() > 1)
            {
                MessageBox.Show("Debe seleccionar solo una persona a la vez, para asignación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmpleado.Text = "";
                txtNumero.Text = "";
                dgvDataView.CancelSelection();
                return;
            }

            if (txtEmpleado.Text.Length==0)
            {
                MessageBox.Show("Debe seleccionar una persona, para asignación.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            int[] filas = dgvItemsView.GetSelectedRows();
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    CodHta = dgvItemsView.GetRowCellValue(filas[i], "IdHerramienta").ToString();
                    Persona = txtNumero.Text;

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_HerramPersona_Vincula(Convert.ToInt32(CodHta), Convert.ToInt32(Persona), Utilitario.Instancia.SesionUsuario.usuario, 1);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { txtNombreHerramienta.Text = Respuesta; }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
                cargarHerramientas();
                buscar(Convert.ToInt32(txtNumero.Text));
            }
            else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            string CodHta;
            string Persona;

            int[] filas = dgvDataView.GetSelectedRows();
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    CodHta = dgvDataView.GetRowCellValue(filas[i], "IDHerramienta").ToString();
                    Persona = dgvDataView.GetRowCellValue(filas[i], "Persona").ToString();

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_HerramPersona_Vincula(Convert.ToInt32(CodHta), Convert.ToInt32(Persona), Utilitario.Instancia.SesionUsuario.usuario, 2);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { txtNombreHerramienta.Text = Respuesta; }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
                cargarHerramientas();
                buscar(0);
            }
            else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnOcultarHerramientas_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel1Collapsed = true;
            btnVerHerramientas.Visible = true;
            btnOcultarHerramientas.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            filtroItems();
        }
        private Microsoft.Office.Interop.Excel.Application app;
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Listado Personal con Herramientas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtIDHerramienta.Text.Length == 0)
            {
                MessageBox.Show("Debe seleccionar una herramienta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult resul = MessageBox.Show("Seguro que quiere eliminar el Registro?", "Eliminar Registro", MessageBoxButtons.YesNo);
            if (resul == DialogResult.Yes)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_HerramientaElimina(Convert.ToInt32(txtIDHerramienta.Text));
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cargarHerramientas();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Desea exportar lista General de Herramientas? Si=Lista General No=Solo Hrrtas. Disponibles.", "Exportado", MessageBoxButtons.YesNo);


            if (result == DialogResult.No)
            {
                if (dgvItems.DataSource == null)
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hay data para exportar";
                    m.ShowDialog();
                }
                else
                {
                    //ExportToExcel(dgvItems, progressBar1);  // Es para DataGridView Normales, no de dgvExpress
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Listado de Herramientas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvItems.ExportToXlsx(nombre);
                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Visible = true;
                    app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));

                }
            }
            else
            { 
                DataTable dtRespuesta = new DataTable();
                dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_Herramientas_Listar(2, Utilitario.Instancia.SesionUsuario.usuario);

                if (dtRespuesta.Rows.Count > 0)
                {
                    exportDataTableToExcel(dtRespuesta);
                }
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
                string nombre = System.IO.Path.Combine(desktop, "Listado Herramientas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");

                libro.SaveAs(nombre, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error");
                app.Quit();
            }
        }

        private void ExportToExcel(DataGridView dgView, ProgressBar pBar)//ExportarDataGridViewExcel(DataGridView grd)
        {
            try
            {
                if (pBar != null)
                {
                    pBar.Maximum = dgView.RowCount;
                    pBar.Value = 0;
                    if (!pBar.Visible) pBar.Visible = true;
                }
                if (dgView.Rows.Count == 0)
                    return;

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Listado de Herramientas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");

                // Instanciar un objeto Excel.Application
                //SaveFileDialog fichero = new SaveFileDialog();
                //fichero.Filter = "Excel (*.xls)|*.xls";
                //fichero.FileName = "";

                //if (fichero.ShowDialog() == DialogResult.OK)
                //{
                    Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel._Workbook libro = app.Workbooks.Add(Type.Missing);
                    Microsoft.Office.Interop.Excel._Worksheet hoja = null;
                    app.Visible = true;
                    hoja = libro.Sheets["Hoja1"];
                    hoja = libro.ActiveSheet;

                    for (int i = 1; i <= dgView.Columns.Count ; i++) { hoja.Cells[1, i] = dgView.Columns[i-1].HeaderText; }

                    for (int i = 0; i < dgView.Rows.Count - 1; i++)
                    {
                        for (int j = 0; j < dgView.Columns.Count ; j++)
                        {
                            if (dgView.Rows[i].Cells[j].Value != null) { hoja.Cells[i + 2, j + 1] = dgView.Rows[i].Cells[j].Value.ToString(); }
                            else { hoja.Cells[i + 2, j + 1] = ""; }
                        }
                        pBar.Value += 1;
                    }
                    libro.SaveAs(nombre, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal);
                    //libro.Close(true);
                    //app.Quit();
                //}

            } 
            catch (Exception e) { MessageBox.Show(e.Message, "Error"); }
            finally
            {
                if (pBar != null)
                {
                    pBar.Value = 0;
                    pBar.Visible = false;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int length = textBox3.Text.Length;
            if (length == 0) { dgvItems.DataSource = DtHtasFiltro; }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { filtroHta(); }
        }

        void filtroHta()
        {
            //Si no hay filtro, restauramos el grid original y salimos
            if (textBox3.Text == "")
            {
                dgvItems.DataSource = DtHtasFiltro;
                return;
            }

            string busqueda = textBox3.Text;

            try
            {

                if (cboFiltroHta.SelectedIndex == 0)
                {
                    //Con LinQ buscamos las rows que coincidan
                    DataTable df = (from item in DtHtasFiltro.Rows.Cast<DataRow>()
                                    let codigo = Convert.ToString(item["Descripcion"] == null ? string.Empty : item["Descripcion"].ToString())
                                    where codigo.Contains(busqueda)
                                    select item).CopyToDataTable();
                    //Mostramos las coincidencias

                    if (df.Rows.Count > 0)
                    {
                        //No visibles
                        toolStripLabel1.Text = df.Rows.Count.ToString() + " Hrrtas. Disponibles";
                        dgvItems.DataSource = df;
                    }
                    else { toolStripLabel1.Text = "Hrrtas. Disponibles"; }
                }
                else
                {
                    //Con LinQ buscamos las rows que coincidan
                    DataTable df2 = (from item in DtHtasFiltro.Rows.Cast<DataRow>()
                                     let codigo = Convert.ToString(item["CodigoInterno"] == null ? string.Empty : item["CodigoInterno"].ToString())
                                     where codigo.Contains(busqueda)
                                     select item).CopyToDataTable();
                    //Mostramos las coincidencias

                    if (df2.Rows.Count > 0)
                    {
                        //No visibles
                        dgvItems.DataSource = df2;
                    }
                }

            }
            catch (Exception ex) { Console.WriteLine("No hay datos.", ex); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CargarComboMaleta();
            cargarHerramientas();
            filtroHta();
        }

        private void dtgvData_Click_1(object sender, EventArgs e)
        {
            if (dgvDataView.RowCount > 0)
            {
                txtPersona_v.Text = (dgvDataView.GetFocusedRowCellValue("Persona").ToString()).Trim();
                txtPersonaNombre_v.Text = (dgvDataView.GetFocusedRowCellValue("Trabajador").ToString()).Trim();
                txtHta_v.Text = (dgvDataView.GetFocusedRowCellValue("IDHerramienta").ToString()).Trim();
                txtIdEmpleado.Text = (dgvDataView.GetFocusedRowCellValue("Persona").ToString()).Trim();

                txtNumero.Text = txtPersona_v.Text;
                txtEmpleado.Text = txtPersonaNombre_v.Text;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            txtEmpleado.Text = "";
            txtPersona_v.Text = "";
            txtEmpleado.Focus();
            buscar(0);
        }

        private void dtgvData_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDataView.RowCount > 0) { buscar(Convert.ToInt32(txtIdEmpleado.Text)); }
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategorias.RowCount > 0)
            {
                string IDCategoria = dgvCategorias.Rows[e.RowIndex].Cells[0].Value.ToString();
                string Descripcion = dgvCategorias.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtCategoriaNombre.Text = Descripcion;
                txtCategoriaNombre.Tag = IDCategoria;
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (txtCategoriaNombre.Text.Length == 0)
            {
                MessageBox.Show("No ha seleccionado una categoria a modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_CategoriaModificaElimina(Convert.ToInt32(txtCategoriaNombre.Tag), txtCategoriaNombre.Text, 1, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCategoriaNombre.Text = "";
                buscarCategorias();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (txtCategoriaNombre.Text.Length == 0)
            {
                MessageBox.Show("No ha seleccionado una categoria a modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_CategoriaModificaElimina(Convert.ToInt32(txtCategoriaNombre.Tag), txtCategoriaNombre.Text,2, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCategoriaNombre.Text = "";
                buscarCategorias();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvItems_Click(object sender, EventArgs e)
        { txtIDHerramienta.Text = (dgvItemsView.GetFocusedRowCellValue("IdHerramienta").ToString()).Trim(); }

        private void tsRegistroMaletas_Click(object sender, EventArgs e)
        {
            frmListaMaletasHerramientas frmListaMaletasHerramientas = new frmListaMaletasHerramientas();
            frmListaMaletasHerramientas.ShowDialog();
        }

        private void CargarComboMaleta()
        {
            DataTable dtMaleta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Herramientas_ListarMaletas(1, "", 0, Utilitario.Instancia.SesionUsuario.usuario);
            cbxMaleta.DataSource = dtMaleta;
            cbxMaleta.DisplayMember = "CodMaleta";
            cbxMaleta.ValueMember = "idMaletaC";
        }

        private void btnAsignarMaleta_Click(object sender, EventArgs e)
        {
            int CodHta;
            string Maleta;
            int CodMaleta;

            int[] filasMaletas = dgvItemsView.GetSelectedRows();
            if (filasMaletas.Length != 0)
            {
                for (int i = 0; i < filasMaletas.Length; i++)
                {
                    CodHta = Convert.ToInt32(dgvItemsView.GetRowCellValue(filasMaletas[i], "IdHerramienta"));
                    CodMaleta = Convert.ToInt32(cbxMaleta.SelectedValue);
                    Maleta = cbxMaleta.Text;

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlHerramientas_AsignarEliminarMaletaHtta(1, CodHta, CodMaleta, Maleta, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { txtNombreHerramienta.Text = Respuesta; }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
                cargarHerramientas();
            }
            else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsSillas_Click(object sender, EventArgs e)
        {
            frmInventarioSillas frmInventarioSillas = new frmInventarioSillas();
            frmInventarioSillas.ShowDialog();
        }
    }
}
