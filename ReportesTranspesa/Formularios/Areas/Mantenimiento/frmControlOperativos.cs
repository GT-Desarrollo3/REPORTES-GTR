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
using System.Xml;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
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


namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmControlOperativos : MetroFramework.Forms.MetroForm
    {
        int xClick = 0, yClick = 0;
        int xClick2 = 0, yClick2 = 0;
        int xClick3 = 0, yClick3 = 0;
        int e1 = 0;
        int Persona = -1, Conductor = -1, Unidad = -1, idNroTicket = -1;
        DataTable dtListaOperativos = new DataTable();
        DataTable dtListaTickets = new DataTable();
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        
        public frmControlOperativos()
        {
            InitializeComponent();
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void frmControlOperativos_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmControlOperativos");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    btnNuevaSolicitud.Enabled = true;
                    tsEliminarSolicitud.Enabled = true;
                }
                else
                {
                    btnNuevaSolicitud.Enabled = false;
                    tsEliminarSolicitud.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    tsImprimirTicket.Enabled = true;
                    btnAsignarConductor.Enabled = true;
                }
                else
                {
                    tsImprimirTicket.Enabled = false;
                    btnAsignarConductor.Enabled = false;
                }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsAnularSolicitud.Enabled = true; }
                else { tsAnularSolicitud.Enabled = false; }
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
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Registrar KM")
                    {
                        tsRegistrarKM.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else { tsRegistrarKM.Enabled = false; }
                }
            }
            else { tsRegistrarKM.Enabled = false; }
            
            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;

            cbxEstado.Text = "TODOS";
            CargarComboArea();
            cbxArea.Text = "TODOS";

            ListarSolicitudes();
            ListarTickets();
        }


        private void CargarComboArea()
        {
            DataTable dtArea = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarTablas(2);
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "Nombre";
            cbxArea.ValueMember = "CodAreaSpring";
        }

        public void ListarSolicitudes()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaOperativos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlOperativos_Listar(dtpFechaInicio.Text, dtpFechaFin.Text,
                                    txtTicket.Text, txtBuscaConductor.Text, cbxArea.Text, cbxEstado.Text);
                dtgControlOperativos.DataSource = dtListaOperativos;
                if (dtListaOperativos.Rows.Count > 0)
                {
                    dgvControlOperativosView.Columns["FECHA_REQUERIDA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvControlOperativosView.Columns["FECHA_REQUERIDA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvControlOperativosView.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvControlOperativosView.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvControlOperativosView.BestFitColumns();
                }
            }
        }

        public void ListarTickets()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtListaTickets = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlOperativos_ListarTickets(dtpFechaInicio.Text, dtpFechaFin.Text,
                                 txtTicket.Text, txtBuscaConductor.Text);
                dtgKilometraje.DataSource = dtListaTickets;
                if (dtListaTickets.Rows.Count > 0)
                {
                    dgvKilometrajeView.Columns["FECHA_CREA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvKilometrajeView.Columns["FECHA_CREA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvKilometrajeView.BestFitColumns();
                }
            }
        }

        public void Imprimir(string NroTicket)
        {
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                DataTable dtListaTicket = new DataTable();
                dtListaTicket = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlOperativos_Filtrar(NroTicket);

                string NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);

                if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
                {
                    MessageBox.Show("No tiene una impresora asignada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (dtListaTicket.Rows.Count > 0)
                    {
                        Ticket ticket = new Ticket();

                        ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                        ticket.AddSubHeaderLine2("SOLICITUD DE");
                        ticket.AddSubHeaderLine2("OPERATIVOS");
                        ticket.AddSubHeaderLine2("N° " + NroTicket + "                         ");
                        ticket.AddSubHeaderLine("Empleado: " + dtListaTicket.Rows[0]["NOMBRE_USUARIO"].ToString());
                        ticket.AddSubHeaderLine("Área: " + dtListaTicket.Rows[0]["AREA"].ToString());
                        ticket.AddSubHeaderLine("F. Requerida: " + dtListaTicket.Rows[0]["FECHA_REQUERIDA"].ToString());
                        ticket.AddSubHeaderLine("Direccion: " + dtListaTicket.Rows[0]["DIRECCION"].ToString());
                        ticket.AddSubHeaderLine("Detalle: " + dtListaTicket.Rows[0]["DETALLE"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("Conductor: " + dtListaTicket.Rows[0]["CONDUCTOR_ASIGNADO"].ToString());
                        ticket.AddSubHeaderLine("Unidad: " + dtListaTicket.Rows[0]["UNIDAD_ASIGNADA"].ToString());
                        ticket.AddSubHeaderLine("                              ");
                        ticket.AddSubHeaderLine("F. Emision: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                        ticket.PrintTicket(NombreImpresora);
                    }
                }
            }
            catch { MessageBox.Show("No tiene asignada una impresora para imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public string xmlTickets()
        {
            string Tickets = "";
            try
            {
                int[] filas = dgvControlOperativosView.GetSelectedRows();
                if (filas.Length > 0)
                {
                    XmlDocument doc = new XmlDocument();
                    XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                    XmlElement root = doc.DocumentElement;
                    doc.InsertBefore(xmlDeclaration, root);

                    XmlElement r = doc.CreateElement(string.Empty, "r", string.Empty);
                    doc.AppendChild(r);

                    for (int i = 0; i < filas.Length; i++)
                    {
                        XmlElement items = doc.CreateElement(string.Empty, "items", string.Empty);

                        XmlAttribute attribute_nro = doc.CreateAttribute("CODIGO");
                        attribute_nro.Value = dgvControlOperativosView.GetRowCellValue(filas[i], "CODIGO").ToString();
                        items.Attributes.Append(attribute_nro);

                        r.AppendChild(items);
                    }

                    Tickets = doc.OuterXml;
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message, "Error"); }

            return Tickets;
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarSolicitudes();
                ListarTickets();
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarSolicitudes();
                ListarTickets();
            }
        }

        private void txtTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarSolicitudes();
                ListarTickets();
            }
        }

        private void txtBuscaConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarSolicitudes();
                ListarTickets();
            }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { ListarSolicitudes(); }

        private void cbxArea_DropDownClosed(object sender, EventArgs e) { ListarSolicitudes(); }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarSolicitudes();
            ListarTickets();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                dtgControlOperativos.ForceInitialize();
                dtgKilometraje.ForceInitialize();

                compositeLink1.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "Control de Operativos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink1.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }


            
            /*
            if (dtgControlOperativos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Control de Operativos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgControlOperativos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
            */ 
        }

        private void dgvControlOperativosView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "PENDIENTE")
                { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (Convert.ToString(e.CellValue) == "ASIGNADO")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "ANULADO")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void dtgControlOperativos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Codigo = Convert.ToString(dgvControlOperativosView.GetRowCellValue(dgvControlOperativosView.FocusedRowHandle, "CODIGO"));

                if (Codigo != "")
                {
                    string Estado = Convert.ToString(dgvControlOperativosView.GetRowCellValue(dgvControlOperativosView.FocusedRowHandle, "ESTADO"));

                    if (Estado == "PENDIENTE")
                    {
                        tsImprimirTicket.Enabled = false;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsAnularSolicitud.Enabled = true; }
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { tsEliminarSolicitud.Enabled = true; }
                    }

                    if (Estado == "ASIGNADO")
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsImprimirTicket.Enabled = true; }
                        tsAnularSolicitud.Enabled = false;
                        tsEliminarSolicitud.Enabled = false;
                    }

                    if (Estado == "ANULADO")
                    {
                        tsImprimirTicket.Enabled = false;
                        tsAnularSolicitud.Enabled = false;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { tsEliminarSolicitud.Enabled = true; }
                    }
                }
                else
                {
                    tsImprimirTicket.Enabled = false;
                    tsAnularSolicitud.Enabled = false;
                    tsEliminarSolicitud.Enabled = false;
                }
            }
            catch
            {
                tsImprimirTicket.Enabled = false;
                tsAnularSolicitud.Enabled = false;
                tsEliminarSolicitud.Enabled = false;
            }
        }

        private void btnNuevaSolicitud_Click(object sender, EventArgs e)
        {
            dtpFechaRequerida.Value = DateTime.Now;
            
            pNuevaSolicitud.Visible = true;
            pNuevaSolicitud.BringToFront();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            btnCancelar_Click(sender, e);
            
            pNuevaSolicitud.Visible = false;
            pNuevaSolicitud.SendToBack();
        }

        private void pNuevaSolicitud_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevaSolicitud.Left = pNuevaSolicitud.Left + (e.X - xClick);
                pNuevaSolicitud.Top = pNuevaSolicitud.Top + (e.Y - yClick);
            }
        }

        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal, clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(1, txtPersonal.Text), true, false, false);
            lstPersonal.Columns[0].Width = 0;
            lstPersonal.Columns[1].Width = 300;
            lstPersonal.Columns[2].Width = 150;
            lstPersonal.Columns[3].Width = 0;
            lstPersonal.Columns[4].Width = 0;
            lstPersonal.BringToFront();
            lstPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void txtPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal.Focus(); }
        }

        private void lstPersonal_Enter(object sender, EventArgs e)
        {
            if (!lstPersonal.Items.Count.Equals(0)) { lstPersonal.Items[0].Selected = true; }
        }

        private void lstPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersonal.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersonal.SelectedItems[0];

                Persona = Int32.Parse(ItemActual.Text);
                txtPersonal.Text = ItemActual.SubItems[1].Text;
                txtArea.Text = ItemActual.SubItems[2].Text;

                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
                dtpFechaRequerida.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void lstPersonal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersonal.SelectedItems[0];

            Persona = Int32.Parse(ItemActual.Text);
            txtPersonal.Text = ItemActual.SubItems[1].Text;
            txtArea.Text = ItemActual.SubItems[2].Text;

            lstPersonal.Visible = false;
            lstPersonal.SendToBack();
            dtpFechaRequerida.Focus();
        }

        private void dtpFechaRequerida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtDireccion.Focus(); }
        }

        private void txtDireccion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtDetalle.Focus(); }
        }

        private void txtDetalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnRegistrar.Focus(); }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Persona = -1;
            txtPersonal.Clear();
            txtArea.Clear();
            dtpFechaRequerida.Value = DateTime.Now;
            txtDireccion.Clear();
            txtDetalle.Clear();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (txtPersonal.Text.Length == 0 || txtDireccion.Text.Length == 0 || txtDetalle.Text.Length == 0)
            {
                if (txtPersonal.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese a un empleado.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPersonal.Focus();
                }
                else
                {
                    if (txtDireccion.Text.Length == 0)
                    {
                        MessageBox.Show("Por favor, ingrese la dirección.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtDireccion.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese el detalle.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtDetalle.Focus();
                    }
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
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlOperativos_GenerarSolicitud(Persona, dtpFechaRequerida.Value,
                    txtDireccion.Text, txtDetalle.Text, Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA != "-")
                    {
                        MessageBox.Show("Solicitud de Operativo N° " + Respuesta + " generada correctamente.", "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCerrar_Click(sender, e);
                        ListarSolicitudes();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    
                }
                catch { MessageBox.Show("Error generando la solicitud de operativos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsImprimirTicket_Click(object sender, EventArgs e)
        {
            try
            {
                string Codigo = Convert.ToString(dgvControlOperativosView.GetRowCellValue(dgvControlOperativosView.FocusedRowHandle, "CODIGO"));

                if (MessageBox.Show("¿Desea imprimir esta solicitud de operativo?", "IMPRIMIR TICKET", MessageBoxButtons.YesNo) == DialogResult.Yes)
                { Imprimir(Codigo); }
            }
            catch { MessageBox.Show("Se produjo un error al imprimir el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsAnularSolicitud_Click(object sender, EventArgs e)
        {
            string Codigo = Convert.ToString(dgvControlOperativosView.GetRowCellValue(dgvControlOperativosView.FocusedRowHandle, "CODIGO"));

            if (MessageBox.Show("¿Desea anular esta solicitud de operativo?", "ANULAR SOLICITUD", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dtRespuesta = new DataTable();
                string respta;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlOperativos_EliminarSolicitudes(1, Codigo);
                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    //MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarSolicitudes();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminarSolicitud_Click(object sender, EventArgs e)
        {
            string Codigo = Convert.ToString(dgvControlOperativosView.GetRowCellValue(dgvControlOperativosView.FocusedRowHandle, "CODIGO"));

            if (MessageBox.Show("¿Desea eliminar esta solicitud de operativo?", "ELIMINAR SOLICITUD", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dtRespuesta = new DataTable();
                string respta;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlOperativos_EliminarSolicitudes(2, Codigo);
                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0") { ListarSolicitudes(); }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnAsignarConductor_Click(object sender, EventArgs e)
        {
            int[] filas = dgvControlOperativosView.GetSelectedRows();
            
            if (filas.Length > 0)
            {
                pAsignarConductor.Visible = true;
                pAsignarConductor.BringToFront();
                txtConductor.Focus();
            }
            else { MessageBox.Show("No se ha seleccionado ninguna solicitud.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pAsignarConductor.Visible = false;
            pAsignarConductor.SendToBack();
            Conductor = -1; Unidad = -1;
            txtConductor.Clear();
            txtUnidad.Clear();
        }

        private void pAsignarConductor_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pAsignarConductor.Left = pAsignarConductor.Left + (e.X - xClick2);
                pAsignarConductor.Top = pAsignarConductor.Top + (e.Y - yClick2);
            }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal2, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte(txtConductor.Text), true, false, false);
            lstPersonal2.Columns[0].Width = 0;
            lstPersonal2.Columns[1].Width = 330;
            lstPersonal2.Columns[2].Width = 0;
            lstPersonal2.Columns[3].Width = 0;
            lstPersonal2.BringToFront();
            lstPersonal2.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Conductor = -1;
                lstPersonal2.Visible = false;
                lstPersonal2.SendToBack();
            }
        }

        private void txtConductor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal2.Focus(); }
        }

        private void lstPersonal2_Enter(object sender, EventArgs e)
        {
            if (!lstPersonal2.Items.Count.Equals(0)) { lstPersonal2.Items[0].Selected = true; }
        }

        private void lstPersonal2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersonal2.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersonal2.SelectedItems[0];

                Conductor = Int32.Parse(ItemActual.Text);
                txtConductor.Text = ItemActual.SubItems[1].Text;

                lstPersonal2.Visible = false;
                lstPersonal2.SendToBack();
                txtUnidad.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                Conductor = -1;
                lstPersonal2.Visible = false;
                lstPersonal2.SendToBack();
            }
        }

        private void lstPersonal2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersonal2.SelectedItems[0];

            Conductor = Int32.Parse(ItemActual.Text);
            txtConductor.Text = ItemActual.SubItems[1].Text;

            lstPersonal2.Visible = false;
            lstPersonal2.SendToBack();
            txtUnidad.Focus();
        }

        private void txtUnidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTracto, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte2(txtUnidad.Text), true, false, false);
            lstTracto.Columns[0].Width = 0;
            lstTracto.Columns[1].Width = 100;
            lstTracto.BringToFront();
            lstTracto.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Unidad = -1;
                lstTracto.Visible = false;
                lstTracto.SendToBack();
            }
        }

        private void txtUnidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTracto.Focus(); }
        }

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

                Unidad = Int32.Parse(ItemActual.Text);
                txtUnidad.Text = ItemActual.SubItems[1].Text;

                lstTracto.Visible = false;
                lstTracto.SendToBack();
                btnAsignar.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                Unidad = -1;
                lstPersonal2.Visible = false;
                lstPersonal2.SendToBack();
            }
        }

        private void lstTracto_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTracto.SelectedItems[0];

            Unidad = Int32.Parse(ItemActual.Text);
            txtUnidad.Text = ItemActual.SubItems[1].Text;

            lstTracto.Visible = false;
            lstTracto.SendToBack();
            btnAsignar.Focus();
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            string Codigo, Estado;

            int[] filas = dgvControlOperativosView.GetSelectedRows();
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    Codigo = dgvControlOperativosView.GetRowCellValue(filas[i], "CODIGO").ToString();
                    Estado = dgvControlOperativosView.GetRowCellValue(filas[i], "ESTADO").ToString();

                    if (Estado == "PENDIENTE")
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;

                        dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlOperativos_AsignarConductor(Codigo, Conductor, Unidad);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA != "0")
                        {
                            MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        else { Imprimir(Codigo); }
                    }
                }

                string xmlTicket = xmlTickets();
                if (xmlTicket != "")
                {
                    DataTable dtListaTicket = new DataTable();
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtListaTicket = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlOperativos_CrearTicket(xmlTicket, Conductor, Unidad, Usuario);
                    string Respuesta2 = Convert.ToString(dtListaTicket.Rows[0]["exito"]);
                    string NroRPTA2 = Respuesta2.Substring(0, 1);
                    if (NroRPTA2 != "0") { MessageBox.Show(Respuesta2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    else { ListarTickets(); }
                }
                else { MessageBox.Show("No se ha seleccionado ninguna solicitud.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                btnCerrar2_Click(sender, e);
                ListarSolicitudes();
            }
            else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgKilometraje_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Nro = Convert.ToString(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "NRO"));

                if (Nro != "")
                {
                    if (e1 == 1) { tsRegistrarKM.Enabled = true; }
                }
                else { tsRegistrarKM.Enabled = false; }
            }
            catch { tsRegistrarKM.Enabled = false; }
        }

        private void pKMUnidades_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick3 = e.X; yClick3 = e.Y; }
            else
            {
                pKMUnidades.Left = pKMUnidades.Left + (e.X - xClick3);
                pKMUnidades.Top = pKMUnidades.Top + (e.Y - yClick3);
            }
        }

        private void tsRegistrarKM_Click(object sender, EventArgs e)
        {
            idNroTicket = Convert.ToInt32(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "NRO"));
            lblConductor.Text = Convert.ToString(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "CONDUCTOR"));
            lblUnidad.Text = Convert.ToString(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "UNIDAD"));
            txtKMSalida.Text = Convert.ToString(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "KM_SALIDA"));
            txtKMIngreso.Text = Convert.ToString(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "KM_INGRESO"));

            pKMUnidades.Visible = true;
            pKMUnidades.BringToFront();
            txtKMSalida.Focus();
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            pKMUnidades.Visible = false;
            pKMUnidades.SendToBack();

            idNroTicket = -1;
            lblConductor.Text = "";
            lblUnidad.Text = "";
            txtKMSalida.Clear();
            txtKMIngreso.Clear();
        }

        private void txtKMInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Enter) { txtKMIngreso.Focus(); }
        }

        private void txtKMFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Enter) { btnGuardar.Focus(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtKMSalida.Text.Length == 0 || txtKMIngreso.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese el kilometraje.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtKMSalida.Text.Length == 0) { txtKMSalida.Focus(); }
                else { txtKMIngreso.Focus(); }
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ControlOperativos_IngresarKMs(idNroTicket, Convert.ToDecimal(txtKMSalida.Text),
                    Convert.ToDecimal(txtKMIngreso.Text), Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnCerrar3_Click(sender, e);
                        ListarTickets();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                }
                catch { MessageBox.Show("Error registrando los kilometrajes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgKilometraje_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string Nro = Convert.ToString(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "NRO"));

                if (Nro != "")
                {
                    if (e1 == 1)
                    {
                        idNroTicket = Convert.ToInt32(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "NRO"));
                        lblConductor.Text = Convert.ToString(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "CONDUCTOR"));
                        lblUnidad.Text = Convert.ToString(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "UNIDAD"));
                        txtKMSalida.Text = Convert.ToString(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "KM_SALIDA"));
                        txtKMIngreso.Text = Convert.ToString(dgvKilometrajeView.GetRowCellValue(dgvKilometrajeView.FocusedRowHandle, "KM_INGRESO"));

                        pKMUnidades.Visible = true;
                        pKMUnidades.BringToFront();
                        txtKMSalida.Focus();
                    }
                }
            }
            catch {  }
        }
    }
}
