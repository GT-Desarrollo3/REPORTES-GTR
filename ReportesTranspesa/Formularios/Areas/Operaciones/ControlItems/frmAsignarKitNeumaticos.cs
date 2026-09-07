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
    public partial class frmAsignarKitNeumaticos : Form
    {
        public int Persona;
        public frmListaControlItems frmListaControlItems = new frmListaControlItems();
        public int idVehiculo;

        public frmAsignarKitNeumaticos()
        {
            InitializeComponent();
        }

        private void frmAsignarKitNeumaticos_Load(object sender, EventArgs e)
        {
            CargarKitNeumaticos();
        }


        public void CargarKitNeumaticos()
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_ListarKitNeumatico(1, "", txtHtta.Text, "");

            if (dtRespuesta.Rows.Count > 0)
            {
                dgvItems.DataSource = dtRespuesta;
                dgvItemsView.Columns["IdHerramienta"].Visible = false;
                dgvItemsView.UpdateSummary();
                dgvItemsView.BestFitColumns();
                tsHttasD.Text = "Herramientas disponibles: " + dtRespuesta.Rows.Count.ToString();
            }
            else
            {
                dgvItems.DataSource = null;
                tsHttasD.Text = "Herramientas disponibles: ";
            }
        }


        private void txtEmpleado_Enter(object sender, EventArgs e) { txtEmpleado.BackColor = Color.FromArgb(255, 192, 192); }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstEmpleado, clsConsultaBL.Instancia.GetEmpleado(txtEmpleado.Text), true, false, false);
            lstEmpleado.Columns[0].Width = 0;
            lstEmpleado.Columns[1].Width = 300;
            lstEmpleado.Columns[2].Width = 110;
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
                btnAsignar.Focus();
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
            btnAsignar.Focus();
        }

        private void txtTracto_Enter(object sender, EventArgs e) { txtTracto.BackColor = Color.FromArgb(255, 192, 192); }

        private void txtTracto_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTracto, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtTracto.Text), true, false, false);
            lstTracto.Columns[0].Width = 0;
            lstTracto.Columns[1].Width = 80;
            lstTracto.Columns[2].Width = 100;
            lstTracto.Columns[3].Width = 0;
            lstTracto.Columns[4].Width = 130;
            lstTracto.Columns[5].Width = 0;
            lstTracto.BringToFront();
            lstTracto.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo = -1;
                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }
        }

        private void txtTracto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTracto.Focus(); }
        }

        private void txtTracto_Leave(object sender, EventArgs e) { txtTracto.BackColor = Color.White; }

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

                idVehiculo = Int32.Parse(ItemActual.Text);
                txtTracto.Text = ItemActual.SubItems[1].Text;
                txtEmpleado.Focus();

                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculo = -1;
                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }
        }

        private void lstTracto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTracto.SelectedItems[0];

            idVehiculo = Int32.Parse(ItemActual.Text);
            txtTracto.Text = ItemActual.SubItems[1].Text;
            txtEmpleado.Focus();

            lstTracto.Visible = false;
            lstTracto.SendToBack();
        }

        private void btnBuscarHtta_Click(object sender, EventArgs e) { CargarKitNeumaticos(); }

        private void txtHtta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { CargarKitNeumaticos(); }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            int[] filas = dgvItemsView.GetSelectedRows();

            if (txtTracto.Text.Length == 0 || txtEmpleado.Text.Length == 0)
            {
                if (txtTracto.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese el tracto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtTracto.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione a un empleado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtEmpleado.Focus();
                }

                return;
            }

            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    string CodHta = dgvItemsView.GetRowCellValue(filas[i], "IdHerramienta").ToString();

                    DataTable dtRespuesta = new DataTable();
                    DataTable dtRespuesta2 = new DataTable();
                    string Respuesta, Respuesta2;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlItems_AsignarKitNeumatico(1, Convert.ToInt32(CodHta), Persona, idVehiculo, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA != "0")
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                    else
                    {
                        dtRespuesta2 = clsMantenimientoBL.Instancia.GetDataMantenimiento_ControlHerramientas_HerramPersona_Vincula(Convert.ToInt32(CodHta), Persona, Utilitario.Instancia.SesionUsuario.usuario, 1);
                        Respuesta2 = Convert.ToString(dtRespuesta2.Rows[0]["exito"]);
                        string NroRPTA2 = Respuesta.Substring(0, 1);
                        if (NroRPTA2 != "0")
                        {
                            MessageBox.Show(Respuesta2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }

                CargarKitNeumaticos();
                frmListaControlItems.ListarKitNeumaticos();
                txtEmpleado.Clear();
                txtTracto.Clear();
                Persona = -1; idVehiculo = -1;
            }
            else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            int[] filas = dgvItemsView.GetSelectedRows();

            if (filas.Length != 0)
            {
                if (MessageBox.Show("¿Desea quitar esta herramienta del kit de neumáticos?", "DESVINCULAR HERRAMIENTA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        int idHerramienta = Convert.ToInt32(dgvItemsView.GetRowCellValue(filas[i], "IdHerramienta").ToString());

                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlHerramientas_AsignarEliminarMaletaHtta(2, idHerramienta, 0, "", Utilitario.Instancia.SesionUsuario.usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA != "0")
                        {
                            MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }

                CargarKitNeumaticos();
                frmListaControlItems.ListarKitNeumaticos();
            }
            else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvItems.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Herramientas Disponibles - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss", dtfi) + ".xlsx");
                dgvItems.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
