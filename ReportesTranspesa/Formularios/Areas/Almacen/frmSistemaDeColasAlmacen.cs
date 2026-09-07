using Negocio;
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using System.Web.UI.WebControls;
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using System.Globalization;
using System.Diagnostics;

namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    public partial class frmSistemaDeColasAlmacen : Form
    {
        public frmSistemaDeColasAlmacen()
        {
            InitializeComponent();
        }

        private void frmSistemaDeColasAlmacen_Load(object sender, EventArgs e)
        {
            try
            {
                ListarIngresoSalida();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

        private void ListarIngresoSalida()
        {
            DataTable dt = clsAlmacenBL.Instancia.ReportesApp_Almacen_ListarIngresoSalida(dtpFechaInicio.Text,dtpFechaFin.Text);
            dgvListar.DataSource = dt;
            gridView1.Columns["idProveedor"].Visible = false;
            gridView1.Columns["idVehiculo"].Visible = false;
            gridView1.Columns["idConductor"].Visible = false;
            gridView1.Columns["idCliente"].Visible = false;
            gridView1.Columns["idSistemaGestion"].Visible = false;
            gridView1.Columns["UsuarioCrea"].Visible = false;

            foreach (GridColumn column in gridView1.Columns)
            {
                if (Type.GetTypeCode(column.ColumnType) == TypeCode.DateTime)
                {
                    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    column.DisplayFormat.FormatString = @"dd/MM/yyyy HH:mm:ss";
                }
            }

            int countEspera = dt.AsEnumerable()
               .Count(row => row.Field<string>("EstadoAtencion") == "EN ESPERA");

            int countIngreso = dt.AsEnumerable()
                .Count(row => row.Field<string>("EstadoAtencion") == "INGRESO");

            int countSalida = dt.AsEnumerable()
                .Count(row => row.Field<string>("EstadoAtencion") == "SALIDA");

            contadorEspera.Text = countEspera.ToString();
            contadorIngreso.Text = countIngreso.ToString();
            contadorSalida.Text = countSalida.ToString();
 
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmRegistrar_IngresoSalidaAlmacen frm = new frmRegistrar_IngresoSalidaAlmacen();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ListarIngresoSalida();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                 ListarIngresoSalida();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void imprimirTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            


            PrintDocument printDoc = new PrintDocument();
            Ticket ticket = new Ticket();
            ticket.MaxChar = 40;
            ticket.MaxCharDescription = 30;
            ticket.AddHeaderLine("GRUPO: " + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Grupo")) + "                          ");
            ticket.AddSubHeaderLine2("GRUPO TRANSPESA S.A.C.");
            ticket.FontCodigo = 12;
            ticket.AddSubHeaderLine("        CONTROL DE INGRESO / SALIDA" + "                                                               ");
            ticket.AddSubHeaderLine2("NRO ORDEN: " + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Orden")) + "                          ");
            ticket.FontSize = 8;
            ticket.AddSubHeaderLine("CODIGO: " + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo")) + "                          ");
            ticket.AddSubHeaderLine("ORDEN CLIENTE: " + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "OrdenCliente")) + "                          ");
            ticket.AddSubHeaderLine("CLIENTE: " + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Cliente")) + "                          ");
            ticket.AddSubHeaderLine("CONDUCTOR: " + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Conductor")));
            ticket.AddSubHeaderLine("VEHICULO" + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Vehiculo")) + "                    ");
            ticket.AddSubHeaderLine("                                         ");
            ticket.AddSubHeaderLine("TRANSPORTISTA: " + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Transportista")) + "                    ");
            ticket.AddSubHeaderLine("FECHA LLEGADA: " + Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Llegada")) + "                    ");
            ticket.AddSubHeaderLine("                                         ");


            /* for (int i = 0; i < dgvProductosGuia.Rows.Count; i++)
             {
                 ticket.AddItem(dgvProductosGuia.Rows[i].Cells["Cantidad"].Value.ToString(),dgvProductosGuia.Rows[i].Cells["Descripcion"].Value.ToString(),dgvProductosGuia.Rows[i].Cells["Codigo"].Value.ToString());
                       
             }*/


            ticket.AddFooterLine("                                   ");

            int Encontrado = 0;
            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters) 
            { 
                if(printer == "POS-80-Series")
                {
                    ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
                    Encontrado = 1;
                    break;
                }

                if(printer == "POS-80-Series (1)")
                {
                     ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
                    Encontrado = 1;
                    break;
                }

                if(printer == "POS-80-Series (2)")
                {
                    ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
                    Encontrado = 1;
                    break;
                }
            }

            if(Encontrado==0)
            {
                ticket.PrintTicket(printDoc.PrinterSettings.PrinterName);
            }

            


        
        }

        private void atendidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo"));

                if (clsAlmacenBL.Instancia.ReportesApp_Almacen_EnPesajeIngresoSalida(codigo,"INGRESO"))
                {
                    ListarIngresoSalida();
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pesajeFinalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string codigo = Convert.ToString(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Codigo"));
                if (clsAlmacenBL.Instancia.ReportesApp_Almacen_EnPesajeIngresoSalida(codigo, "SALIDA"))
                {
                    ListarIngresoSalida();
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = gridView1.GetFocusedDataRow();

            if (e.Column.FieldName == "EstadoAtencion")
            {
                if (e.CellValue.ToString() == "EN ESPERA")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 155, 155);
                   
                }

            }

            if (e.Column.FieldName == "EstadoAtencion")
            {
                if (e.CellValue.ToString() == "INGRESO")
                {
                    e.Appearance.BackColor = Color.FromArgb(129, 199, 132);
                }

            }
            if (e.Column.FieldName == "EstadoAtencion")
            {
                if (e.CellValue.ToString() == "SALIDA")
                {
                    e.Appearance.BackColor = Color.FromArgb(77, 208, 225);
                }

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {



            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCorreos_Click(object sender, EventArgs e)
        {
            try
            {
                frmRegistroCorreosSistemaColas frm = new frmRegistroCorreosSistemaColas();
                frm.Show();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Sistema Colas " + dtpFechaInicio.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvListar.ExportToXlsx(nombre);
                    Process.Start(nombre);
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
