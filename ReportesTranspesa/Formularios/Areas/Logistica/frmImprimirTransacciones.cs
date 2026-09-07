using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using System.Xml;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class frmImprimirTransacciones : MetroFramework.Forms.MetroForm
    {
        string Fecha, CentroCosto, Proyecto, Unidad;
        int Opcion = 0, xClick = 0, yClick = 0;
        string xmlItem = "";
        DataTable dtPermisos = new DataTable();

        public frmImprimirTransacciones()
        {
            InitializeComponent();
        }

        private void frmImprimirTransacciones_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmImprimirTransacciones");
            
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnNuevoItem.Enabled = true; }
                    else { btnNuevoItem.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarItem.Enabled = true; }
                    else { tsEliminarItem.Enabled = false; }
                }
            }
            
            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            ListarTransacciones();
            ListarRegistroOT();
        }


        public void ListarTransacciones()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                DataTable dtListaTransacciones = clsLogisticaBL.Instancia.ReportesApp_Logistica_TransaccionesMtto_ListarTransacciones(txtOrdenTrabajo.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgTransacciones.DataSource = dtListaTransacciones;
                if (dtListaTransacciones.Rows.Count > 0)
                {
                    //dtgvTardanzasView.Columns["DNI"].Visible = false;
                    dtgvTransaccionesView.Columns["FECHA"].Summary.Clear();
                    dtgvTransaccionesView.Columns["FECHA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "FECHA", "Total: {0}");

                    dtgvTransaccionesView.BestFitColumns();
                }
            }
        }

        public void ListarDetalleTransaccion(string NroOT)
        {
            DataTable dtDetalleTransacciones = clsLogisticaBL.Instancia.ReportesApp_Logistica_TransaccionesMtto_ListarDetalleTransaccion(1, NroOT);
            dtgDetalleTransaccion.DataSource = dtDetalleTransacciones;
            if (dtDetalleTransacciones.Rows.Count > 0)
            {
                dgvDetalleTransaccionVista.Columns["NRO"].Visible = false;
                
                dgvDetalleTransaccionVista.BestFitColumns();
            }
        }

        public void ListarItemsDivemotor()
        {
            DataTable dtListaItems = clsLogisticaBL.Instancia.ReportesApp_Logistica_TransaccionesMtto_ListarDetalleTransaccion(2, " ");
            dtgDetalleTransaccion.DataSource = dtListaItems;
            if (dtListaItems.Rows.Count > 0)
            {
                dgvDetalleTransaccionVista.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvDetalleTransaccionVista.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvDetalleTransaccionVista.BestFitColumns();
            }
        }

        public void ListarRegistroOT()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                DataTable dtListaRegistros = clsLogisticaBL.Instancia.ReportesApp_Logistica_TransaccionesMtto_ListarRegistroOT(txtOrdenTrabajo.Text, dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgRegistroOT.DataSource = dtListaRegistros;
                if (dtListaRegistros.Rows.Count > 0)
                {
                    dgvRegistroOTView.Columns["idRegistroOT"].Visible = false;

                    dgvRegistroOTView.Columns["FECHA_IMPRESION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRegistroOTView.Columns["FECHA_IMPRESION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    
                    dgvRegistroOTView.Columns["CANTIDAD_PEDIDA"].Summary.Clear();
                    dgvRegistroOTView.Columns["CANTIDAD_PEDIDA"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CANTIDAD_PEDIDA", "Total: {0:N2}");

                    dgvRegistroOTView.BestFitColumns();
                }
            }
        }

        public string xmlItems()
        {
            string Items = "";
            try
            {
                int[] filas = dgvDetalleTransaccionVista.GetSelectedRows();
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

                        string Estado = dgvDetalleTransaccionVista.GetRowCellValue(filas[i], "ESTADO").ToString();

                        if (Estado != "IMPRESO")
                        {
                            XmlAttribute attribute_nro = doc.CreateAttribute("NRO");
                            attribute_nro.Value = dgvDetalleTransaccionVista.GetRowCellValue(filas[i], "NRO").ToString();
                            items.Attributes.Append(attribute_nro);

                            XmlAttribute attribute_codigo = doc.CreateAttribute("CODIGO");
                            attribute_codigo.Value = dgvDetalleTransaccionVista.GetRowCellValue(filas[i], "CODIGO").ToString();
                            items.Attributes.Append(attribute_codigo);

                            XmlAttribute attribute_estado = doc.CreateAttribute("ESTADO");
                            attribute_estado.Value = dgvDetalleTransaccionVista.GetRowCellValue(filas[i], "ESTADO").ToString();
                            items.Attributes.Append(attribute_estado);

                            r.AppendChild(items);
                        }
                    }

                    Items = doc.OuterXml;
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message, "Error"); }

            return Items;
        }

        public void Imprimir(string NroOT)
        {
            try
            {
                DataTable dtConsultarImpresora = new DataTable();
                dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);

                string Codigo = dgvDetalleTransaccionVista.GetRowCellValue(dgvDetalleTransaccionVista.FocusedRowHandle, "CODIGO").ToString();

                xmlItem = xmlItems();
                if (xmlItem != "")
                {
                    DataTable dtListaTicket = new DataTable();
                    dtListaTicket = clsLogisticaBL.Instancia.ReportesApp_Logistica_TransaccionesMtto_ImprimirTicket(txtNroOT.Text, xmlItem, Utilitario.Instancia.SesionUsuario.usuario);

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
                            Ticket ticket2 = new Ticket();

                            ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                            ticket.AddSubHeaderLine2("OT-" + dtListaTicket.Rows[0]["ORDEN_TRABAJO"].ToString() + "                         ");
                            ticket.AddSubHeaderLine("Unidad: " + dtListaTicket.Rows[0]["UNIDAD"].ToString());
                            ticket.AddSubHeaderLine("Centro Costo: " + dtListaTicket.Rows[0]["CENTRO_COSTO"].ToString());

                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("ÍTEMS EN DIVEMOTOR:");

                            for (int i = 0; i < dtListaTicket.Rows.Count; i++)
                            {
                                ticket.AddSubHeaderLine("                              ");
                                ticket.AddSubHeaderLine("Código: " + dtListaTicket.Rows[i]["CODIGO"].ToString());
                                ticket.AddSubHeaderLine("Unidad: " + dtListaTicket.Rows[i]["UND"].ToString());
                                ticket.AddSubHeaderLine("Ítem: " + dtListaTicket.Rows[i]["ITEM"].ToString());
                                ticket.AddSubHeaderLine("Cantidad: " + dtListaTicket.Rows[i]["CANTIDAD_PEDIDA"].ToString());
                                ticket.AddSubHeaderLine("                              ");
                                ticket.AddSubHeaderLine("==============================");
                            }

                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("                              ");
                            ticket.AddSubHeaderLine("F.Impresion: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket.PrintTicket(NombreImpresora);


                            ticket2.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
                            ticket2.AddSubHeaderLine2("OT-" + dtListaTicket.Rows[0]["ORDEN_TRABAJO"].ToString() + "                         ");
                            ticket2.AddSubHeaderLine("Unidad: " + dtListaTicket.Rows[0]["UNIDAD"].ToString());
                            ticket2.AddSubHeaderLine("Centro Costo: " + dtListaTicket.Rows[0]["CENTRO_COSTO"].ToString());

                            ticket2.AddSubHeaderLine("                              ");
                            ticket2.AddSubHeaderLine("                              ");
                            ticket2.AddSubHeaderLine("ÍTEMS EN DIVEMOTOR:");

                            for (int i = 0; i < dtListaTicket.Rows.Count; i++)
                            {
                                ticket2.AddSubHeaderLine("                              ");
                                ticket2.AddSubHeaderLine("Código: " + dtListaTicket.Rows[i]["CODIGO"].ToString());
                                ticket2.AddSubHeaderLine("Unidad: " + dtListaTicket.Rows[i]["UND"].ToString());
                                ticket2.AddSubHeaderLine("Ítem: " + dtListaTicket.Rows[i]["ITEM"].ToString());
                                ticket2.AddSubHeaderLine("Cantidad: " + dtListaTicket.Rows[i]["CANTIDAD_PEDIDA"].ToString());
                                ticket2.AddSubHeaderLine("                              ");
                                ticket2.AddSubHeaderLine("==============================");
                            }

                            ticket2.AddSubHeaderLine("                              ");
                            ticket2.AddSubHeaderLine("                              ");
                            ticket2.AddSubHeaderLine("F.Impresion: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                            ticket2.PrintTicket(NombreImpresora);
                        }

                        tabDivemotor.SelectedTab = tabListaTickets;
                        ListarRegistroOT();
                    }
                }
                else { MessageBox.Show("No se ha seleccionado ningún ítem.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            }
            catch { MessageBox.Show("Error imprimiendo el ticket.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private void txtOrdenTrabajo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarTransacciones();
                ListarRegistroOT();
            }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarTransacciones();
                ListarRegistroOT();
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                ListarTransacciones();
                ListarRegistroOT();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarTransacciones();
            ListarRegistroOT();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgRegistroOT.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE ÓRDENES DE TRABAJO IMPRESAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgRegistroOT.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnNuevoItem_Click(object sender, EventArgs e)
        {
            btnCerrar_Click(sender, e);

            lblTitulo.Text = "ASIGNAR ÍTEM A DIVEMOTOR: ";
            btnAgregar.Visible = true;
            btnAgregar.BringToFront();
            txtNroOT.ReadOnly = true;
            txtDescripcion.ReadOnly = false;
            Opcion = 1;

            ListarItemsDivemotor();
            dtgDetalleTransaccion.ContextMenuStrip = contextMenuStrip1;
            pDetalleTransaccion.Visible = true;
            pDetalleTransaccion.BringToFront();
        }

        private void dtgTransacciones_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                btnCerrar_Click(sender, e);

                string NroOT = dtgvTransaccionesView.GetRowCellValue(dtgvTransaccionesView.FocusedRowHandle, "ORDEN_TRABAJO").ToString();

                txtNroOT.Text = NroOT;
                txtDescripcion.Text = dtgvTransaccionesView.GetRowCellValue(dtgvTransaccionesView.FocusedRowHandle, "DESCRIPCION").ToString();
                Fecha = dtgvTransaccionesView.GetRowCellValue(dtgvTransaccionesView.FocusedRowHandle, "FECHA").ToString();
                CentroCosto = dtgvTransaccionesView.GetRowCellValue(dtgvTransaccionesView.FocusedRowHandle, "CENTRO_COSTO").ToString();
                Proyecto = dtgvTransaccionesView.GetRowCellValue(dtgvTransaccionesView.FocusedRowHandle, "PROYECTO").ToString();
                Unidad = dtgvTransaccionesView.GetRowCellValue(dtgvTransaccionesView.FocusedRowHandle, "UNIDAD").ToString();

                lblTitulo.Text = "N° DE ORDEN DE TRABAJO: ";
                btnAgregar.Visible = false;
                btnAgregar.SendToBack();
                txtNroOT.ReadOnly = true;
                txtDescripcion.ReadOnly = true;
                Opcion = 2;

                ListarDetalleTransaccion(NroOT);
                dtgDetalleTransaccion.ContextMenuStrip = null;
                pDetalleTransaccion.Visible = true;
                pDetalleTransaccion.BringToFront();
            }
            catch { MessageBox.Show("No se ha seleccionado ninguna OT.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pDetalleTransaccion.Visible = false;
            pDetalleTransaccion.SendToBack();
            dtgDetalleTransaccion.DataSource = null;
            dgvDetalleTransaccionVista.Columns.Clear();
            Fecha = null; CentroCosto = null; Proyecto = null; Unidad = null;

            txtNroOT.Clear();
            txtDescripcion.Clear();
            lstItems.Visible = false;
            lstItems.SendToBack();
        }

        private void pDetalleTransaccion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pDetalleTransaccion.Left = pDetalleTransaccion.Left + (e.X - xClick);
                pDetalleTransaccion.Top = pDetalleTransaccion.Top + (e.Y - yClick);
            }
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Opcion == 1)
            {
                clsVisuales.Instancia.LlenarLw(lstItems, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ListarMaestroItems(txtDescripcion.Text), true, false, false);
                lstItems.Columns[0].Width = 100;
                lstItems.Columns[1].Width = 323;
                lstItems.Columns[2].Width = 0;
                lstItems.Columns[3].Width = 0;
                lstItems.Columns[4].Width = 0;
                lstItems.BringToFront();
                lstItems.Visible = true;

                if (e.KeyChar == (char)Keys.Back)
                {
                    txtNroOT.Clear();
                    lstItems.Visible = false;
                    lstItems.SendToBack();
                }
            }
        }

        private void txtDescripcion_KeyUp(object sender, KeyEventArgs e)
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

                txtNroOT.Text = ItemActual.SubItems[0].Text;
                txtDescripcion.Text = ItemActual.SubItems[1].Text;

                lstItems.Visible = false;
                lstItems.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                txtNroOT.Clear();
                lstItems.Visible = false;
                lstItems.SendToBack();
            }
        }

        private void lstItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstItems.SelectedItems[0];

            txtNroOT.Text = ItemActual.SubItems[0].Text;
            txtDescripcion.Text = ItemActual.SubItems[1].Text;

            lstItems.Visible = false;
            lstItems.SendToBack();
        }

        private void dtgDetalleTransaccion_MouseUp(object sender, MouseEventArgs e)
        {
            if (Opcion == 1)
            {
                try
                {
                    string Codigo = dgvDetalleTransaccionVista.GetRowCellValue(dgvDetalleTransaccionVista.FocusedRowHandle, "CODIGO").ToString();

                    if (Codigo != "")
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarItem.Enabled = true; }
                    }
                    else { tsEliminarItem.Enabled = false; }
                }
                catch { tsEliminarItem.Enabled = false; }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtNroOT.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingrese un ítem.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDescripcion.Focus();
            }
            else
            {
                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtAgregar = clsLogisticaBL.Instancia.ReportesApp_Logistica_TransaccionesMtto_InsertarEliminarItem(1, txtNroOT.Text, txtDescripcion.Text, Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarItemsDivemotor();
                    ListarTransacciones();
                    txtNroOT.Clear();
                    txtDescripcion.Clear();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminarItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea quitar este ítem de la lista?", "QUITAR ÍTEM", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string Codigo = dgvDetalleTransaccionVista.GetRowCellValue(dgvDetalleTransaccionVista.FocusedRowHandle, "CODIGO").ToString();

                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtAgregar = clsLogisticaBL.Instancia.ReportesApp_Logistica_TransaccionesMtto_InsertarEliminarItem(2, Codigo, "", Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    ListarItemsDivemotor();
                    ListarTransacciones();
                    txtNroOT.Clear();
                    txtDescripcion.Clear();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Imprimir(txtNroOT.Text);
            btnCerrar_Click(sender, e);
        }

        private void dgvDetalleTransaccionVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "IMPRESO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void dtgRegistroOT_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Codigo = dgvRegistroOTView.GetRowCellValue(dgvRegistroOTView.FocusedRowHandle, "ITEM").ToString();

                if (Codigo != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsAnularImpresion.Enabled = true; }
                }
                else { tsAnularImpresion.Enabled = false; }
            }
            catch { tsAnularImpresion.Enabled = false; }
        }

        private void tsAnularImpresion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea anular la impresión de este ticket?", "ANULAR IMPRESIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idRegistroOT = Convert.ToInt32(dgvRegistroOTView.GetRowCellValue(dgvRegistroOTView.FocusedRowHandle, "idRegistroOT"));
                string CodigoItem = dgvRegistroOTView.GetRowCellValue(dgvRegistroOTView.FocusedRowHandle, "ITEM").ToString();

                DataTable dtAgregar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtAgregar = clsLogisticaBL.Instancia.ReportesApp_Logistica_TransaccionesMtto_EliminarTicketsImpresos(idRegistroOT, CodigoItem, Usuario);
                respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);

                if (NroRspta == "0") { ListarRegistroOT(); }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
