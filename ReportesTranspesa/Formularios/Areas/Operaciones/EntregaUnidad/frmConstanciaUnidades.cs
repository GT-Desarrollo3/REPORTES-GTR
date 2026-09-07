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
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.EntregaUnidad
{
    public partial class frmConstanciaUnidades : MetroFramework.Forms.MetroForm
    {
        DataTable dtListaConstancia = new DataTable();
        DataTable dtListaAsignacion = new DataTable();
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        public int idTracto, idRemolque, idConductor;
        public int xClick = 0, yClick = 0;
        public int OpcionC = 0, e1 = 0;

        public frmConstanciaUnidades()
        {
            InitializeComponent();
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
            cbxOperacion2.SelectedIndexChanged -= cbxOperacion2_SelectedIndexChanged;
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void cbxOperacion2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion2(); }

        private void frmConstanciaUnidades_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmConstanciaUnidades");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevaAsignacion.Enabled = true; }
                else { btnNuevaAsignacion.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    tsAprobarConstancia.Enabled = true;
                    tsRegistrarHoras.Enabled = true;
                    tsAñadirObservación.Enabled = true;
                }
                else
                {
                    tsAprobarConstancia.Enabled = false;
                    tsRegistrarHoras.Enabled = false;
                    tsAñadirObservación.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                {
                    tsEliminarConstancia.Enabled = true;
                    tsEliminarAsignacion.Enabled = true;
                }
                else
                {
                    tsEliminarConstancia.Enabled = false;
                    tsEliminarAsignacion.Enabled = false;
                }
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
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Aprobar Constancias")
                    {
                        btnAprobar.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else { btnAprobar.Enabled = false; }
                }
            }
            else { btnAprobar.Enabled = false; }

            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            dtpFechaInicio2.Value = new DateTime(dtpFechaInicio2.Value.Year, dtpFechaInicio2.Value.Month, 1);
            dtpFechaFin2.Value = DateTime.Now;

            CargarComboOperacion();
            CargarComboOperacion2();

            cbxEstado.Text = "TODOS";
            cbxOperacion.Text = "TODO";
            cbxOperacion2.Text = "TODO";

            ListarConstancias();
            ListarAsignacion();
        }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(5, "");
            cbxOperacion.DataSource = dtOperacion;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void CargarComboOperacion2()
        {
            DataTable dtOperacion2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(5, "");
            cbxOperacion2.DataSource = dtOperacion2;
            cbxOperacion2.DisplayMember = "Descripcion";
            cbxOperacion2.ValueMember = "IdOperacion";
        }

        public void ListarConstancias()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaConstancia = clsOperacionesBL.Instancia.ReportesApp_Mantenimiento_EntregaUnidad_ListarConstancia(txtNroTicket.Text, txtBuscarPlaca.Text,
                                 dtpFechaIni.Text, dtpFechaFin.Text, cbxEstado.Text, cbxOperacion.Text);
                dtgListaConstancias.DataSource = dtListaConstancia;
                if (dtListaConstancia.Rows.Count > 0)
                {
                    dgvListaConstanciasVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaConstanciasVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";
                    dgvListaConstanciasVista.Columns["FechaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaConstanciasVista.Columns["FechaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm:ss";

                    dgvListaConstanciasVista.BestFitColumns();
                }
            }
        }

        public void ListarAsignacion()
        {
            if (dtpFechaInicio2.Value > dtpFechaFin2.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio2.Focus();
                return;
            }
            else
            {
                dtListaAsignacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ListarAsignaciones(txtConductor2.Text, txtTracto2.Text,
                                                               dtpFechaInicio2.Text, dtpFechaFin2.Text, cbxOperacion2.Text);
                dtgAsignaciones.DataSource = dtListaAsignacion;
                if (dtListaAsignacion.Rows.Count > 0) { dgvAsignacionesVista.BestFitColumns(); }
            }
        }

        public void ImprimirConstancia(string CodConstanciaA, string Unidad, string Operacion, string ConductorA, string ConductorN, string Motivo)
        {
            try
            {
                string CodCostancia = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "CODIGO"));

                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                //string NombreImpresora = "EPSON L380 Series";
                string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

                if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
                {
                    MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    Ticket ticket = new Ticket();

                    ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                    ticket.AddSubHeaderLine2("CONSTANCIA DE");
                    ticket.AddSubHeaderLine2("ENTREGA UNIDAD");
                    ticket.AddSubHeaderLine2("EU - " + CodConstanciaA + "                         ");
                    ticket.AddSubHeaderLine("Unidad: " + Unidad);
                    ticket.AddSubHeaderLine("Operación: " + Operacion);
                    ticket.AddSubHeaderLine("Conductor Anterior: " + ConductorA);
                    ticket.AddSubHeaderLine("Conductor Nuevo: " + ConductorN);
                    ticket.AddSubHeaderLine("Motivo: " + Motivo);
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("______________________________");
                    ticket.AddSubHeaderLine("RESPONSABLE DE OPERACIONES");
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("______________________________");
                    ticket.AddSubHeaderLine("FIRMA DE CONDUCTOR");
                    ticket.AddSubHeaderLine("                              ");
                    ticket.AddSubHeaderLine("F.Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                    ticket.PrintTicket(NombreImpresora);
                }
            }
            catch { MessageBox.Show("Error tratando de imprimir la constancia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /*
        private void btnAsignar_Click(object sender, EventArgs e)
        {
            if (txtPlaca.Text.Length == 0 || txtConductor.Text.Length == 0)
            {
                if (txtPlaca.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese la placa de la unidad a entregar.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPlaca.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor, ingrese el nombre del conductor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtConductor.Focus();
                }
                return;
            }

            if (dtpFechaProg.Value.Date < DateTime.Now.Date)
            {
                MessageBox.Show("No puede generar una constancia en una fecha menor a la actual.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaProg.Focus();
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarConstancia(1, 0, idTracto, idRemolque, -1,
                                  idConductor, dtpFechaProg.Value.ToShortDateString(), dtpHoraProg.Value.ToShortTimeString(), "ENTREGA DE UNIDAD", Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    MessageBox.Show("Constancia Generada. N° " + Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Imprimir(Respuesta);
                }
                catch { MessageBox.Show("Error generando la constancia de entrega.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        */

        private void txtBuscarPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConstancias(); }
        }

        private void txtNroTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConstancias(); }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaFin.Focus(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarConstancias(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarConstancias(); }

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e) { ListarConstancias(); }

        private void dgvListaConstanciasVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "PENDIENTE")
                { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (Convert.ToString(e.CellValue) == "APROBADA")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarConstancias(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaConstancias.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Constancias de Unidades - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaConstancias.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void tsImprimirTicket_Click(object sender, EventArgs e)
        {
            
        }

        private void tsAprobarConstancia_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string respta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                string CodCostancia = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "CODIGO"));

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ModificarEliminarConstancia(1,CodCostancia,DateTime.Now,DateTime.Now,"",Usuario);
                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarConstancias();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("Se produjo un error al aprobar la constancia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsRegistrarHoraInicio_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string respta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                string CodCostancia = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "CODIGO"));

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ModificarEliminarConstancia(2, CodCostancia, DateTime.Now, DateTime.Now, "", Usuario);
                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarConstancias();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("Se produjo un error al registrar la fecha.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void horaFinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string respta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                string CodCostancia = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "CODIGO"));

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ModificarEliminarConstancia(3, CodCostancia, DateTime.Now, DateTime.Now, "", Usuario);
                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarConstancias();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("Se produjo un error al aprobar la constancia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsAñadirObservación_Click(object sender, EventArgs e)
        {
            lblNroConstancia.Text = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "CODIGO"));
            lblTracto.Text = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "TRACTO"));
            lblSR.Text = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "CARRETA"));
            lblMotivo.Text = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "MOTIVO"));
            lblFechaProg.Text = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "FECHA_PROG"));

            dtpHoraInicio.Value = dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "HORA_INICIO").ToString() == "" ? Convert.ToDateTime("01/01/2000 00:00:00") : Convert.ToDateTime(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "HORA_INICIO"));
            dtpHoraFin.Value = dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "HORA_FIN").ToString() == "" ? Convert.ToDateTime("01/01/2000 00:00:00") : Convert.ToDateTime(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "HORA_FIN"));
            txtObservacion.Text = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "OBSERVACION"));

            pConstancia.Visible = true;
            pConstancia.BringToFront();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pConstancia.Visible = false;
            pConstancia.SendToBack();

            lblNroConstancia.Text = "";
            lblTracto.Text = "";
            lblSR.Text = "";
            lblMotivo.Text = "";
            lblFechaProg.Text = "";

            dtpHoraInicio.Value = Convert.ToDateTime("20/05/2024 00:00:00");
            dtpHoraFin.Value = Convert.ToDateTime("20/05/2024 00:00:00");
            txtObservacion.Text = "";
        }

        private void pConstancia_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pConstancia.Left = pConstancia.Left + (e.X - xClick);
                pConstancia.Top = pConstancia.Top + (e.Y - yClick);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string respta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ModificarEliminarConstancia(4, lblNroConstancia.Text, dtpHoraInicio.Value, dtpHoraFin.Value,
                              txtObservacion.Text, Usuario);
                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    ListarConstancias();
                    btnCerrar_Click(sender, e);
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("Se produjo un error al actualizar la constancia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsEliminarConstancia_Click(object sender, EventArgs e)
        {
            try
            {
                string CodCostancia = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "CODIGO"));

                if (MessageBox.Show("¿Desea eliminar esta constancia del registro?", "ELIMINAR CONSTANCIA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_ModificarEliminarConstancia(5, CodCostancia, DateTime.Now, DateTime.Now, "", Usuario);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarConstancias(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar la constancia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgListaConstancias_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string CodConstancia = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "CODIGO"));
                string Estado = Convert.ToString(dgvListaConstanciasVista.GetRowCellValue(dgvListaConstanciasVista.FocusedRowHandle, "ESTADO"));

                if (CodConstancia != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsAprobarConstancia.Enabled = true; }
                    
                    if (Estado == "PENDIENTE")
                    {
                        tsImprimirTicket.Enabled = true;
                        tsRegistrarHoras.Enabled = false;
                        tsAñadirObservación.Enabled = false;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarConstancia.Enabled = true; }
                    }
                    else
                    {
                        tsImprimirTicket.Enabled = false; 
                       if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                        {
                            tsRegistrarHoras.Enabled = true;
                            tsAñadirObservación.Enabled = true;
                        }
                        tsEliminarConstancia.Enabled = false;
                    }
                }
                else
                {
                    tsImprimirTicket.Enabled = false; 
                    tsAprobarConstancia.Enabled = false;
                    tsRegistrarHoras.Enabled = false;
                    tsAñadirObservación.Enabled = false;
                    tsEliminarConstancia.Enabled = false;
                }
            }
            catch
            {
                tsImprimirTicket.Enabled = false; 
                tsAprobarConstancia.Enabled = false;
                tsRegistrarHoras.Enabled = false;
                tsAñadirObservación.Enabled = false;
                tsEliminarConstancia.Enabled = false;
            }
        }

        private void dtgAsignaciones_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Codigo = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(dgvAsignacionesVista.FocusedRowHandle, "ESTADO"));

                if (Codigo != "APROBADA")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarAsignacion.Enabled = true; }
                }
                else { tsEliminarAsignacion.Enabled = false; }
            }
            catch { tsEliminarAsignacion.Enabled = false; }
        }

        private void txtConductor2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarAsignacion(); }
        }

        private void txtTracto2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarAsignacion(); }
        }

        private void dtpFechaInicio2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarAsignacion(); }
        }

        private void dtpFechaFin2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarAsignacion(); }
        }

        private void cbxOperacion2_DropDownClosed(object sender, EventArgs e) { ListarAsignacion(); }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpcionC == 0)
                {
                    dgvAsignacionesVista.OptionsSelection.MultiSelect = true;
                    dgvAsignacionesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    OpcionC = 1;
                }
                else
                {
                    int[] filas = dgvAsignacionesVista.GetSelectedRows();

                    if (filas.Length != 0)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string respta;
                        int Correcto = 0;

                        if (MessageBox.Show("¿Desea aprobar estas asignaciones?", "APROBAR ASIGNACIONES", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            for (int i = 0; i < filas.Length; i++)
                            {
                                string Estado = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "ESTADO"));
                                   
                                if (Estado == "PENDIENTE")
                                {
                                    string CodConstanciaA = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "CODIGO"));
                                    int UltimoPreviaje = Convert.ToInt32(dgvAsignacionesVista.GetRowCellValue(filas[i], "ULTIMA_ASIGNACION"));
                                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                                    string Unidad = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "UNIDAD"));
                                    string Operacion = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "OPERACION"));
                                    string ConductorA = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "COND_ANTERIOR"));
                                    string ConductorN = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "COND_NUEVO"));
                                    string Motivo = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "OBSERVACION"));
                                    string Observacion = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "DETALLE"));

                                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarAsignacion(3, CodConstanciaA, 0, UltimoPreviaje,
                                                                             0, 0, "", "", Usuario);
                                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);

                                    if (NroRspta == "0")
                                    {
                                        ImprimirConstancia(CodConstanciaA, Unidad, Operacion, ConductorA, ConductorN, Motivo);
                                        Correcto = Correcto + 1;
                                    }
                                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }
                                else
                                {
                                    string CodConstanciaA = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "CODIGO"));
                                    string Unidad = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "UNIDAD"));
                                    string Operacion = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "OPERACION"));
                                    string ConductorA = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "COND_ANTERIOR"));
                                    string ConductorN = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "COND_NUEVO"));
                                    string Motivo = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(filas[i], "OBSERVACION"));

                                    ImprimirConstancia(CodConstanciaA, Unidad, Operacion, ConductorA, ConductorN, Motivo);
                                    Correcto = Correcto + 1;
                                }
                            }

                            if (Correcto == filas.Length) { MessageBox.Show("0 = El cumplimiento semanal ha sido generado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                            dgvAsignacionesVista.OptionsSelection.MultiSelect = false;
                            dgvAsignacionesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                            OpcionC = 0;

                            ListarAsignacion();
                            ListarConstancias();
                        }
                    }
                    else
                    {
                        dgvAsignacionesVista.OptionsSelection.MultiSelect = false;
                        dgvAsignacionesVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        OpcionC = 0;
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al aprobar las asignaciones.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvAsignacionesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "PENDIENTE")
                { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (Convert.ToString(e.CellValue) == "APROBADA")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void btnBuscar2_Click(object sender, EventArgs e) { ListarAsignacion(); }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            if (dtgAsignaciones.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Constancias de Asignaciones - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgAsignaciones.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnNuevaAsignacion_Click(object sender, EventArgs e)
        {
            frmConstanciaAsignacion frmConstanciaAsignacion = new frmConstanciaAsignacion();
            frmConstanciaAsignacion.frmConstanciaUnidades = this;
            frmConstanciaAsignacion.CargarComboMotivo();
            frmConstanciaAsignacion.Show();
        }

        private void tsEliminarAsignacion_Click(object sender, EventArgs e)
        {
            try
            {
                string CodCostanciaA = Convert.ToString(dgvAsignacionesVista.GetRowCellValue(dgvAsignacionesVista.FocusedRowHandle, "CODIGO"));

                if (MessageBox.Show("¿Desea eliminar esta constancia de asignación?", "ELIMINAR ASIGNACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_EntregaUnidad_RegistrarEditarAsignacion(2, CodCostanciaA, 0, 0, 0, 0, "", "", "");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarAsignacion(); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar la constancia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
