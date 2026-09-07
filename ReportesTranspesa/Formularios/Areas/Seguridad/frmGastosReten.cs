using ReportesTranspesa.Properties;
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
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using Comun;
using System.Globalization;

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmGastosReten : Form
    {

        int idGasto = 0;
        public frmGastosReten()
        {
            InitializeComponent();
            dtpFecha.ValueChanged -= dtpFecha_ValueChanged;
            txtNombrePersonal.KeyPress -= txtNombrePersonal_KeyPress;
        }

        private void frmGastosReten_Load(object sender, EventArgs e)
        {
            ListarGastosRetenes();
            dtpFecha.ValueChanged += dtpFecha_ValueChanged;
            txtNombrePersonal.KeyPress += txtNombrePersonal_KeyPress;
        }

        private void ListarGastosRetenes()
        {
            try
            {
               
                DataTable dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_ListarRetenes(dtpFecha.Text, dtpFechaFin.Text, txtNombrePersonal.Text);

                if (dt.Rows.Count > 0)
                {
                    
                    dtgLista.DataSource = dt;
                    dgvListaExpressVista.Columns["idGasto"].Visible = false;
                    dgvListaExpressVista.Columns["idPersona"].Visible = false;
                    dgvListaExpressVista.Columns["Importe"].DisplayFormat.FormatType = FormatType.Numeric;
                    dgvListaExpressVista.Columns["Importe"].DisplayFormat.FormatString = "N2";

                    //dgvListaExpressVista.GroupSummary.Clear(); // limpia de la grilla
                    dgvListaExpressVista.SortInfo.ClearAndAddRange(new GridColumnSortInfo[] { 
                    new GridColumnSortInfo(dgvListaExpressVista.Columns["FechaRegistro"], DevExpress.Data.ColumnSortOrder.Descending)
                    }, 1);

                        dgvListaExpressVista.Columns["Importe"].Summary.Clear(); // limpia del total parte inferior
                        dgvListaExpressVista.Columns["Importe"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Importe", "Total = {0:N2}");
                    }

                dgvListaExpressVista.BestFitColumns();
                dgvListaExpressVista.ExpandAllGroups();
            }
            catch (Exception ex )
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void nuevaGuiaRemitenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNuevoGastoReten open = new frmNuevoGastoReten();
            open.esVALE = "NO";
            if(open.ShowDialog() == System.Windows.Forms.DialogResult.OK){

                ListarGastosRetenes();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtNombrePersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            ListarGastosRetenes();
        }

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            ListarGastosRetenes();
        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            ListarGastosRetenes();

        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                txtEmpleadoEditar.Text  = dgvListaExpressVista.GetRowCellValue(dgvListaExpressVista.FocusedRowHandle, "NombrePersona").ToString();
                txtEmpleadoEditar.Tag = dgvListaExpressVista.GetRowCellValue(dgvListaExpressVista.FocusedRowHandle, "idPersona").ToString();
                txtImporteActual.Text = dgvListaExpressVista.GetRowCellValue(dgvListaExpressVista.FocusedRowHandle, "Importe").ToString();
                idGasto = Convert.ToInt32(dgvListaExpressVista.GetRowCellValue(dgvListaExpressVista.FocusedRowHandle, "idGasto"));

                pActualizar.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNuevoImporte.Text.Length == 0)
                {
                    MessageBox.Show("Usted no ha llenado el nuevo importe", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (Convert.ToDecimal(txtNuevoImporte.Text) == 0)
                {
                    MessageBox.Show("El importe no puede ser 0", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Boolean respuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_Registrar_Elimina_EditarGastoReten(idGasto, "", Convert.ToInt32(txtEmpleadoEditar.Tag), "", DateTime.Now, Convert.ToDecimal(txtNuevoImporte.Text), "", "", Utilitario.TipoOperacion.Editar, "");
                if (respuesta)
                {

                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pActualizar.Visible = false;
                    ListarGastosRetenes();
                }
                else
                {
                    
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            pActualizar.Visible = false;
        }

        private void txtNuevoImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && e.KeyChar != '.' && e.KeyChar != (char)Keys.Enter)
            {
                MessageBox.Show("Solo se permiten numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                idGasto = Convert.ToInt32(dgvListaExpressVista.GetRowCellValue(dgvListaExpressVista.FocusedRowHandle, "idGasto"));
                txtEmpleadoEditar.Tag = dgvListaExpressVista.GetRowCellValue(dgvListaExpressVista.FocusedRowHandle, "idPersona").ToString();
            
                
                Boolean respuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_Registrar_Elimina_EditarGastoReten(idGasto, "", Convert.ToInt32(txtEmpleadoEditar.Tag), "",DateTime.Now, 0, "", "", Utilitario.TipoOperacion.Anular,"");
                if (respuesta)
                {

                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pActualizar.Visible = false;
                    ListarGastosRetenes();


                }
                else
                {
                   
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
            
            frmNuevoGastoReten open = new frmNuevoGastoReten();
            open.esVALE = "SI";
            if(open.ShowDialog() == System.Windows.Forms.DialogResult.OK){

                ListarGastosRetenes();
            }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgvListaExpressVista.DataSource == null)
                {
                    MessageBox.Show("No hay data para exportar", "AVISO");
                }
                else
                {

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte Gastos  del " + dtpFecha.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvListaExpressVista.ExportToXlsx(nombre);
                    System.Diagnostics.Process.Start(nombre);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void historialRetenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmHistorialReten open = new frmHistorialReten();
                open.ShowDialog();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
