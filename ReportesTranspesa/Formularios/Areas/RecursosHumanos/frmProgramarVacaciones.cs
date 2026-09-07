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
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmProgramarVacaciones : MetroFramework.Forms.MetroForm
    {
        public string esVALE;
        int empleado, reemplazo;
        int alerta, todos;
        decimal anio;
        private DataTable dt = new DataTable();

        public frmProgramarVacaciones()
        {
            InitializeComponent();
            dt.Columns.Add("Marca", typeof(bool));
            dt.Columns.Add("Codigo", typeof(String));
            dt.Columns.Add("Area", typeof(String));
        }

        private void frmProgramarVacaciones_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now;
            alerta = 0;

            DataTable dtAreas = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_ListarAreas(1);
            dtgListaAreas.Rows.Clear();
            if (dtAreas.Rows.Count > 0)
            {
                dtgListaAreas.Rows.Clear();
                for (int i = 0; i < dtAreas.Rows.Count; i++)
                {
                    dtgListaAreas.Rows.Add(false, dtAreas.Rows[i]["Area"], dtAreas.Rows[i]["Nombre"]);
                }
            }
            else
            {
                dtgListaAreas.DataSource = null;
            }

            ListarPendientes();
            ListarPendientesExportar();
        } 

        private void InsertarVacaciones(object sender, EventArgs e)
        {
            if (txtAnticipacion.Text.Length == 0 )
            {
                MessageBox.Show("Por favor, ingrese los datos faltantes.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtAnticipacion.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                try
                {
                    dt.Rows.Clear();
                    string xml = "";
                    empleado = Convert.ToInt32(dtgvPendientesGoceView.GetRowCellValue(dtgvPendientesGoceView.FocusedRowHandle, "Codigo"));

                    if(todos == 0)
                    {
                        for (int i = 0; i < dtgListaAreas.Rows.Count; i++)
                        {
                            if (Convert.ToBoolean(dtgListaAreas.Rows[i].Cells["Marca"].Value))
                            {
                                dt.Rows.Add(Convert.ToBoolean(dtgListaAreas.Rows[i].Cells["Marca"].Value), dtgListaAreas.Rows[i].Cells["Codigo"].Value.ToString(), dtgListaAreas.Rows[i].Cells["Area"].Value.ToString().TrimEnd());
                            }
                        }

                        xml = Utilitario.Instancia.DatatableToXml(dt);
                    }

                    dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_InsertarVacaciones(empleado, dtpFechaIni.Value, dtpFechaFin.Value, Convert.ToInt32(lblDias.Text), xml,
                                                                                                                Convert.ToInt32(txtAnticipacion.Text), alerta, todos, reemplazo);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        ListarVacaciones();
                        dtgListaAreas.Visible = false;
                        txtAnticipacion.Focus();
                        dt.Rows.Clear();
                        ListarPendientes();
                        ListarPendientesExportar();
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    MessageBox.Show("Por favor, seleccione un área.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ListarVacaciones()
        {
            DataTable dtListaVacaciones = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_ListarVacaciones(txtEmpleado.Text,1);
            dtgListaVacaciones.DataSource = dtListaVacaciones;
            if (dtListaVacaciones.Rows.Count > 0)
            {
                dtgvListaVacacionesView.Columns["Persona"].Visible = false;
                dtgvListaVacacionesView.Columns["idVacaciones"].Visible = false;
                dtgvListaVacacionesView.BestFitColumns();
            }
        }

        private void ListarPendientes()
        {
            DataTable dtListaPendientes = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_ListarVacaciones(txtEmpleado.Text, 2);
            dtgPendientesGoce.DataSource = dtListaPendientes;

            if (dtListaPendientes.Rows.Count > 0)
            {
                dtgvPendientesGoceView.Columns["2024-2025"].Summary.Clear();
                dtgvPendientesGoceView.Columns["2024-2025"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "2024-2025", "{0:N2}");
                dtgvPendientesGoceView.Columns["2025-2026"].Summary.Clear();
                dtgvPendientesGoceView.Columns["2025-2026"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "2025-2026", "{0:N2}");
                dtgvPendientesGoceView.Columns["Total"].Summary.Clear();
                dtgvPendientesGoceView.Columns["Total"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Total", "{0:N2}");
                
                dtgvPendientesGoceView.BestFitColumns();
            }
        }

        private void ListarPendientesExportar()
        {
            DataTable dtListaExportar = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_ListarVacaciones(txtEmpleado.Text, 3);
            dtgExportar.DataSource = dtListaExportar;

            if (dtListaExportar.Rows.Count > 0)
            {
                dtgvExportarView.Columns["2024-2025"].Summary.Clear();
                dtgvExportarView.Columns["2024-2025"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "2024-2025", "{0:N2}");
                dtgvExportarView.Columns["2025-2026"].Summary.Clear();
                dtgvExportarView.Columns["2025-2026"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "2025-2026", "{0:N2}");
                dtgvExportarView.Columns["Total"].Summary.Clear();
                dtgvExportarView.Columns["Total"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Total", "{0:N2}");

                dtgvExportarView.BestFitColumns();
            }
        }

        private void EliminarRegistro()
        {
            int Persona = Convert.ToInt32(dtgvListaVacacionesView.GetRowCellValue(dtgvListaVacacionesView.FocusedRowHandle, "Persona"));
            int idVacaciones = Convert.ToInt32(dtgvListaVacacionesView.GetRowCellValue(dtgvListaVacacionesView.FocusedRowHandle, "idVacaciones"));

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_EliminarVacaciones(Persona, idVacaciones);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                ListarVacaciones();
                txtEmpleado.Focus();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                ListarVacaciones();
                ListarPendientes();
                ListarPendientesExportar();
            }
        }

        private void txtReemplazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                clsVisuales.Instancia.LlenarLw(lvReemplazo, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte(txtReemplazo.Text), true, false, false);
                lvReemplazo.Columns[0].Width = 100;
                lvReemplazo.Columns[1].Width = 400;
                lvReemplazo.Columns[2].Width = 0;
                lvReemplazo.Columns[3].Width = 0;
                lvReemplazo.BringToFront();
                lvReemplazo.Visible = true;
                lvReemplazo.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lvReemplazo.Visible = false;
            }
        }

        private void lvReemplazo_Enter(object sender, EventArgs e)
        {
            if (!lvReemplazo.Items.Count.Equals(0))
            {
                lvReemplazo.Items[0].Selected = true;
            }
        }

        private void lvReemplazo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lvReemplazo.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lvReemplazo.SelectedItems[0];
                reemplazo = Int32.Parse(ItemActual.Text);
                txtReemplazo.Text = ItemActual.SubItems[1].Text;
                lvReemplazo.Visible = false;
                btnGuardar.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lvReemplazo.Visible = false;
                txtReemplazo.Focus();
            }
        }

        private void lvReemplazo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lvReemplazo.SelectedItems[0];
            reemplazo = Int32.Parse(ItemActual.Text);
            txtReemplazo.Text = ItemActual.SubItems[1].Text;
            lvReemplazo.Visible = false;
            btnGuardar.Focus();
        }

        private void dtpFechaIni_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan diferencia = dtpFechaFin.Value.Date - dtpFechaIni.Value.Date;
            lblDias.Text = Convert.ToString(diferencia.Days + 1);
        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan diferencia = dtpFechaFin.Value.Date - dtpFechaIni.Value.Date;
            lblDias.Text = Convert.ToString(diferencia.Days + 1);
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                if (dtpFechaIni.Value < DateTime.Now || dtpFechaFin.Value < DateTime.Now)
                {
                    MessageBox.Show("No puede seleccionar fechas pasadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtpFechaIni.Value = DateTime.Now;
                    return;
                }
                else
                {
                    dtpFechaFin.Focus();
                }
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                if (dtpFechaIni.Value < DateTime.Now || dtpFechaFin.Value < DateTime.Now)
                {
                    MessageBox.Show("No puede seleccionar fechas pasadas.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    dtpFechaFin.Value = DateTime.Now;
                    return;
                }
                else
                {
                    btnArea.Focus();
                }
            }
        }

        private void btnArea_Click(object sender, EventArgs e)
        {
            dtgListaAreas.Visible = true;
            dtgListaAreas.BringToFront();
            dtgListaAreas.Focus();
        }

        private void cbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTodos.Checked == true)
            {
                todos = 1;
                btnArea.Enabled = false;
            }

            if (cbTodos.Checked == false)
            {
                todos = 0;
                btnArea.Enabled = true;
            }
        }

        private void dtgListaAreas_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) || (e.KeyChar == (char)Keys.Back))
            {
                dtgListaAreas.SendToBack();
            }
        }

        private void dtgListaAreas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaAreas.CurrentRow.Cells["Marca"].Value))
            {
                dtgListaAreas.CurrentRow.Cells["Marca"].Value = false;
            }
            else
            {
                dtgListaAreas.CurrentRow.Cells["Marca"].Value = true;
            }
        }

        private void dtgListaAreas_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Convert.ToBoolean(dtgListaAreas.CurrentRow.Cells["Marca"].Value))
            {
                dtgListaAreas.CurrentRow.Cells["Marca"].Value = false;
            }
            else
            {
                dtgListaAreas.CurrentRow.Cells["Marca"].Value = true;
            }

            foreach (DataGridViewRow row in dtgListaAreas.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                chk.Value = !(chk.Value == null ? false : (bool)chk.Value);
            }
        }

        private void txtAnticipacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }

            if ((e.KeyChar == (char)Keys.Enter))
            {
                txtReemplazo.Focus();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                if (txtReemplazo.Text.Length == 0)
                {
                    reemplazo = 0;
                }

                InsertarVacaciones(sender, e);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarVacaciones();
            ListarPendientes();
            ListarPendientesExportar();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaVacaciones.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Vacaciones Programadas - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaVacaciones.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            if (dtgExportar.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Vacaciones Pendientes hasta Fin de Mes - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgExportar.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void cbActivar_CheckedChanged(object sender, EventArgs e)
        {
            if (cbActivar.Checked == true)
            {
                alerta = 1;
            }

            if (cbActivar.Checked == false)
            {
                alerta = 0;
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                EliminarRegistro();
            }
            catch
            {
                MessageBox.Show("El registro seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
