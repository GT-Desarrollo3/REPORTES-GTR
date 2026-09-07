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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmListaMaletasHerramientas : Form
    {
        public int idEmpleado, Opcion = 1, idMaletaC;
        public int xClick = 0, yClick = 0;
        public string CodMaleta, TipoMaleta;

        public frmListaMaletasHerramientas()
        {
            InitializeComponent();
        }

        private void frmListaMaletasHerramientas_Load(object sender, EventArgs e)
        {
            rbMaleta.Checked = true;
            rbMaleta_Click(sender, e);
            ListarMaletaHerramientas();
        }


        public void ListarMaletaHerramientas()
        {
            DataTable dtListaMaleta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Herramientas_ListarMaletas(2, txtEmpleadoB.Text, 0, Utilitario.Instancia.SesionUsuario.usuario);
            dtgvMaletas.DataSource = dtListaMaleta;
            if (dtListaMaleta.Rows.Count > 0)
            {
                dtgvMaletasView.Columns["idMaletaC"].Visible = false;
                dtgvMaletasView.Columns["idPersona"].Visible = false;

                dtgvMaletasView.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvMaletasView.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                dtgvMaletasView.Columns["NRO_HERRAMIENTAS"].Summary.Clear();
                dtgvMaletasView.Columns["NRO_HERRAMIENTAS"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "NRO_HERRAMIENTAS", "Total Herramientas: {0}");

                dtgvMaletasView.BestFitColumns();
            }
        }

        public void ListarHerramientas()
        {
            DataTable dtListaHttas = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Herramientas_ListarMaletas(3, txtHerramienta.Text, idMaletaC, Utilitario.Instancia.SesionUsuario.usuario);
            dtgListaHerramientas.DataSource = dtListaHttas;
            if (dtListaHttas.Rows.Count > 0)
            {
                dgvListaHerramientasView.Columns["idMaletaC"].Visible = false;
                dgvListaHerramientasView.Columns["idMaletaD"].Visible = false;
                dgvListaHerramientasView.Columns["IdHerramienta"].Visible = false;
                dgvListaHerramientasView.Columns["CÓDIGO"].Visible = false;
                dgvListaHerramientasView.Columns["EMPLEADO"].Visible = false;

                dgvListaHerramientasView.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaHerramientasView.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                dgvListaHerramientasView.BestFitColumns();
            }
        }


        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtEmpleado.Focus(); }

            if (e.KeyChar == Convert.ToChar(Keys.Back)) { Opcion = 1; }
        }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lvEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtEmpleado.Text), true, false, false);

            lvEmpleado.Columns[0].Width = 0;
            lvEmpleado.Columns[1].Width = 206;
            lvEmpleado.Columns[2].Width = 110;

            lvEmpleado.BringToFront();
            lvEmpleado.Visible = true;
            
            if (e.KeyChar == (char)Keys.Back)
            {
                lvEmpleado.Visible = false;
                lvEmpleado.SendToBack();
                idEmpleado = -1;
            }
        }

        private void txtEmpleado_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lvEmpleado.Focus(); }
        }

        private void lvEmpleado_Enter(object sender, EventArgs e)
        {
            if (!lvEmpleado.Items.Count.Equals(0)) { lvEmpleado.Items[0].Selected = true; }
        }

        private void lvEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lvEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lvEmpleado.SelectedItems[0];

                idEmpleado = Int32.Parse(ItemActual.Text);
                txtEmpleado.Text = ItemActual.SubItems[1].Text;
                btnGuardar.Focus();

                lvEmpleado.Visible = false;
                lvEmpleado.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idEmpleado = -1;
                lvEmpleado.Visible = false;
                lvEmpleado.SendToBack();
            }
        }

        private void lvEmpleado_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lvEmpleado.SelectedItems[0];

            idEmpleado = Int32.Parse(ItemActual.Text);
            txtEmpleado.Text = ItemActual.SubItems[1].Text;
            btnGuardar.Focus();

            lvEmpleado.Visible = false;
            lvEmpleado.SendToBack();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtEmpleado.Text.Length == 0)
            {
                if (txtCodigo.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese el código de la maleta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCodigo.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese el nombre del empleado.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEmpleado.Focus();
                }
                return;
            }
            else
            {
                if (Opcion == 1)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Herramientas_IngresarMaletas(1, txtCodigo.Text, TipoMaleta, idEmpleado, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodigo.Clear();
                        idEmpleado = -1;
                        txtEmpleado.Clear();
                        ListarMaletaHerramientas();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    txtCodigo.ReadOnly = false;
                    Opcion = 1;
                }

                if (Opcion == 2)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Herramientas_IngresarMaletas(2, txtCodigo.Text, TipoMaleta, idEmpleado, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodigo.Clear();
                        idEmpleado = -1;
                        txtEmpleado.Clear();
                        ListarMaletaHerramientas();

                        txtCodigo.ReadOnly = false;
                        Opcion = 1;
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtCodigo.ReadOnly = true;
                        Opcion = 2;
                    }
                }
            }
        }

        private void txtEmpleadoB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarMaletaHerramientas(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarMaletaHerramientas(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvMaletas.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE MALETAS DE HERRAMIENTAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvMaletas.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgvMaletas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idMaletaC = dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "idMaletaC").ToString();
                string Estado = dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "ESTADO").ToString();
                string NroHttas = dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "NRO_HERRAMIENTAS").ToString();

                if (idMaletaC != "")
                {
                    if (Estado == "DISPONIBLE")
                    {
                        if (NroHttas != "0") { tsAsignarTrabajador.Enabled = true; }
                        else { tsAsignarTrabajador.Enabled = false; }
                        
                        tsDevolverAlmacen.Enabled = false;
                    }
                    else
                    {
                        tsAsignarTrabajador.Enabled = false;
                        tsDevolverAlmacen.Enabled = true;
                    }

                    tsCambiarEmpleado.Enabled = true;
                }
            }
            catch
            {
                tsAsignarTrabajador.Enabled = false;
                tsDevolverAlmacen.Enabled = false;
                tsCambiarEmpleado.Enabled = false;
            }
        }

        private void dtgvMaletas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string NroHttas = dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "NRO_HERRAMIENTAS").ToString();
            idMaletaC = Convert.ToInt32(dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "idMaletaC"));

            if (NroHttas != "0")
            {
                lblCodigo.Text = dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "CÓDIGO").ToString();
                lblEmpleado.Text = dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "EMPLEADO").ToString();
                
                pListarHerramientas.Visible = true;
                pListarHerramientas.BringToFront();
                ListarHerramientas();
            }
        }

        private void pListarHerramientas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pListarHerramientas.Left = pListarHerramientas.Left + (e.X - xClick);
                pListarHerramientas.Top = pListarHerramientas.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pListarHerramientas.Visible = false;
            pListarHerramientas.SendToBack();
            lblCodigo.Text = "";
            lblEmpleado.Text = "";
            dtgListaHerramientas.DataSource = null;
            txtHerramienta.Clear();
        }

        private void dtgvMaletasView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "DISPONIBLE") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "ENTREGADO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void tsQuitarHtta_Click(object sender, EventArgs e)
        {
            string Herramienta = dgvListaHerramientasView.GetRowCellValue(dgvListaHerramientasView.FocusedRowHandle, "HERRAMIENTA").ToString();
            int idHerramienta = Convert.ToInt32(dgvListaHerramientasView.GetRowCellValue(dgvListaHerramientasView.FocusedRowHandle, "IdHerramienta"));

            if (Herramienta == "")
            {
                MessageBox.Show("Esta maleta no tiene herramientas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (MessageBox.Show("¿Desea quitar esta herramienta de la maleta?", "DESVINCULAR HERRAMIENTA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlHerramientas_AsignarEliminarMaletaHtta(2, idHerramienta, 0, "", Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        ListarHerramientas();
                        ListarMaletaHerramientas();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void tsCambiarEmpleado_Click(object sender, EventArgs e)
        {
            string CodMaletaCA = Convert.ToString(dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "CÓDIGO"));

            CodMaletaCA = CodMaleta;
            txtCodigo.ReadOnly = true;
            txtCodigo.Text = Convert.ToString(dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "CÓDIGO"));
            txtEmpleado.Text = Convert.ToString(dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "EMPLEADO"));
            Opcion = 2;
        }

        private void tsAsignarTrabajador_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea asignar esta maleta al empleado?", "ASIGNAR MALETA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idMaletaC = Convert.ToInt32(dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "idMaletaC"));
                
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlHerramientas_AsignarDevolverMaleta(1, idMaletaC, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarMaletaHerramientas(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void txtHerramienta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarHerramientas(); }
        }

        private void btnExportar2_Click(object sender, EventArgs e)
        {
            DataTable dtListaHttas = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Herramientas_ListarMaletas(3, txtHerramienta.Text, idMaletaC, Utilitario.Instancia.SesionUsuario.usuario);
            gridControl1.DataSource = dtListaHttas;
            if (dtListaHttas.Rows.Count > 0)
            {
                gridView1.Columns["idMaletaC"].Visible = false;
                gridView1.Columns["idMaletaD"].Visible = false;
                gridView1.Columns["IdHerramienta"].Visible = false;

                gridView1.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                gridView1.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                gridView1.BestFitColumns();
            }

            if (gridControl1.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE HERRAMIENTAS EN MALETA - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                gridControl1.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void tsDevolverAlmacen_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea devolver esta maleta al almacén?", "DEVOLVER MALETA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idMaletaC = Convert.ToInt32(dtgvMaletasView.GetRowCellValue(dtgvMaletasView.FocusedRowHandle, "idMaletaC"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlHerramientas_AsignarDevolverMaleta(2, idMaletaC, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    //MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarMaletaHerramientas();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void rbMaleta_Click(object sender, EventArgs e)
        {
            if (rbMaleta.Checked == true) { TipoMaleta = "MH"; }
        }

        private void rbKitN_Click(object sender, EventArgs e)
        {
            if (rbKitN.Checked == true) { TipoMaleta = "KN"; }
        }
    }
}
