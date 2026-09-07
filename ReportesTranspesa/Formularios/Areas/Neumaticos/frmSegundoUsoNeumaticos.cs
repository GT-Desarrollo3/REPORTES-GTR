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
using System.IO;
using System.Drawing.Imaging;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Neumaticos
{
    public partial class frmSegundoUsoNeumaticos : Form
    {
        int xClick = 0, yClick = 0;
        int xClick2 = 0, yClick2 = 0;
        int idIngreso = 0, idSalida = 0, Operacion = 0, Mecanico = -1;
        string Nombre, Codigo;
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        int e1 = 0;

        public frmSegundoUsoNeumaticos()
        {
            InitializeComponent();
        }

        private void frmSegundoUsoNeumaticos_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmSegundoUsoNeumaticos");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnIngresarNeumaticos.Enabled = true; }
                    else { btnIngresarNeumaticos.Enabled = false; }
                }

                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

                if (dtEspeciales.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                    {
                        if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Gestionar Salidas y Retornos")
                        {
                            btnSalida.Enabled = true;
                            btnRetorno.Enabled = true;
                            i = 999; e1 = 1;
                        }
                        else
                        {
                            btnSalida.Enabled = false;
                            btnRetorno.Enabled = false;
                        }
                    }
                }
                else
                {
                    btnSalida.Enabled = false;
                    btnRetorno.Enabled = false;
                }
            }

            dtpSalidaIni.Value = new DateTime(dtpSalidaIni.Value.Year, dtpSalidaIni.Value.Month, 1);
            dtpSalidaFin.Value = DateTime.Now;
            dtpFechaIngreso.Value = DateTime.Now;
            dtpFechaSalida.Value = DateTime.Now;

            ListarNeumaticos();
            ListarIngresos();
            ListarSalida();
            //cargarImagen();
        }


        public void ListarNeumaticos()
        {
            DataTable dt = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_Listar(1);
            if (dt.Rows.Count > 0)
            {
                dtgListaNeumaticos.DataSource = dt;

                //dgvListaNeumaticos.Columns["FECHA_CREA"].DisplayFormat.FormatType = FormatType.DateTime;
                //dgvListaNeumaticos.Columns["FECHA_CREA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                //dgvListaNeumaticos.Columns["IMAGEN"].Visible = false;

                dgvListaNeumaticos.BestFitColumns();
            }
        }

        public void ListarIngresos()
        {
            DataTable dt = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ListarIngreso_SegundoUso(1, txtGuiaRemitente.Text, dtpSalidaIni.Text, dtpSalidaFin.Text);
            dtgIngreso.DataSource = dt;
            dgvIngresoVista.Columns["idIngresoNeu"].Visible = false;
            dgvIngresoVista.BestFitColumns();
        }

        public void ListarSalida()
        {
            DataTable dt = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ListarIngreso_SegundoUso(2, txtPlaca.Text, dtpSalidaIni.Text, dtpSalidaFin.Text);
            dtgSalida.DataSource = dt;
            dgvSalidaVista.Columns["idSalida"].Visible = false;
            dgvSalidaVista.Columns["idIngresoNeu"].Visible = false;
            dgvSalidaVista.BestFitColumns();
        }

        public void cargarImagen()
        {
            /*
            if (dgvVinculo.GetRowCellValue(dgvVinculo.FocusedRowHandle, "IMAGEN").ToString() != "")
            {
                Byte[] byteBLOBData;
                byteBLOBData = (Byte[])dgvVinculo.GetRowCellValue(dgvVinculo.FocusedRowHandle, "IMAGEN");
                Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                pbImagen.Image = x;
                pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            */
        }


        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                frmMaestroLlantas frmMaestro = new frmMaestroLlantas();
                frmMaestro.frmSegundoUsoNeumaticos = this;
                frmMaestro.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnIngresarNeumaticos_Click(object sender, EventArgs e)
        {
            try
            {
                pIngresarNeumaticos.Visible = true;
                pIngresarNeumaticos.BringToFront();

                ListarNeumaticos();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnRegistroIngresos_Click(object sender, EventArgs e)
        {
            try
            {
                frmRegistroIngresos frmRegistroIngresos = new frmRegistroIngresos();
                frmRegistroIngresos.formulario = this;
                frmRegistroIngresos.Show();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void pIngresarNeumaticos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pIngresarNeumaticos.Left = pIngresarNeumaticos.Left + (e.X - xClick2);
                pIngresarNeumaticos.Top = pIngresarNeumaticos.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            txtGRR.Clear();
            txtCantidad.Clear();
            txtItemNeumatico.Clear();
            txtDescripcion.Clear();
            dtpFechaIngreso.Value = DateTime.Now;
            pIngresarNeumaticos.Visible = false;
            pIngresarNeumaticos.SendToBack();
        }

        private void txtGRR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaIngreso.Focus(); }
        }

        private void dtpFechaIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtCantidad.Focus(); }
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnAgregar_Click(sender, e); }
        }

        private void txtItemNeumatico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (txtItemNeumatico.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese el código del neumático.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtItemNeumatico.Focus();
                }
                else
                {
                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_FiltrarNeumatico_SegundoUso(txtItemNeumatico.Text);
                    if (dtRespuesta.Rows.Count > 0)
                    {
                        txtItemNeumatico.Text = dtRespuesta.Rows[0]["CODIGO"].ToString().TrimEnd();
                        txtDescripcion.Text = dtRespuesta.Rows[0]["NEUMATICO"].ToString().TrimEnd();
                        txtGRR.Focus();
                    }
                }
            }

            if (e.KeyChar == Convert.ToChar(Keys.Back)) { txtDescripcion.Clear(); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGRR.Text.Length == 0 || txtCantidad.Text.Length == 0 || txtDescripcion.Text.Length == 0)
                {
                    MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (txtGRR.Text.Length == 0) { txtGRR.Focus(); }
                    else
                    {
                        if (txtCantidad.Text.Length == 0) { txtCantidad.Focus(); }
                        else { txtItemNeumatico.Focus(); }
                    }
                }
                else
                {
                    //string Medida = dgvListaNeumaticos.GetRowCellValue(dgvListaNeumaticos.FocusedRowHandle, "MEDIDA").ToString();
                    //int Total = Convert.ToInt32(dgvListaNeumaticos.GetRowCellValue(dgvListaNeumaticos.FocusedRowHandle, "TOTAL_NEUMATICOS"));
                    string GRR = txtGRR.Text;
                    int Cantidad = Convert.ToInt32(txtCantidad.Text);

                    if (clsNeumaticoBL.Instancia.ReportesApp_Neumatico_RegistrarIngreso_SegundoUso(GRR, dtpFechaIngreso.Value, Cantidad, txtItemNeumatico.Text))
                    {
                        txtCantidad.Clear();
                        txtItemNeumatico.Clear();
                        txtDescripcion.Clear();
                        dtpFechaIngreso.Value = DateTime.Now;
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarIngresos();
                    }
                    else { MessageBox.Show(Utilitario.Instancia.Advertencia, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

                    /*
                    if (Cantidad > Total)
                    { MessageBox.Show("No puede ingresar una cantidad mayor al total.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                    */ 
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void txtGuiaRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarIngresos(); }
        }

        private void btnIExcel_Click(object sender, EventArgs e)
        {
            if (dtgIngreso.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE NEUMÁTICOS EN ALMACÉN - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgIngreso.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnSExcel_Click(object sender, EventArgs e)
        {
            if (dtgSalida.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE SALIDAS DE NEUMÁTICOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgSalida.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgListaNeumaticos_Click(object sender, EventArgs e)
        {
            /*
            try { cargarImagen(); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            */
        }

        private void txtNombreSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarSalida(); }
        }

        private void dtpSalidaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarSalida(); }
        }

        private void dtpSalidaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarSalida(); }
        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            try
            {
                Operacion = 1;
                idIngreso = Convert.ToInt32(dgvIngresoVista.GetRowCellValue(dgvIngresoVista.FocusedRowHandle, "idIngresoNeu").ToString());
                Codigo = Convert.ToString(dgvIngresoVista.GetRowCellValue(dgvIngresoVista.FocusedRowHandle, "ITEM"));

                lblTitulo.Text = "DATOS DE SALIDA";
                lblNombreNeumatico.Text = dgvIngresoVista.GetRowCellValue(dgvIngresoVista.FocusedRowHandle, "NEUMÁTICO").ToString();
                lblCantidadNeumatico.Text = dgvIngresoVista.GetRowCellValue(dgvIngresoVista.FocusedRowHandle, "CANTIDAD").ToString();
                dtpFechaSalida.Value = DateTime.Now;
                txtOrden.ReadOnly = false;

                pRegistrarSalida.Visible = true;
                pRegistrarSalida.BringToFront();
            }
            catch (Exception ex) { MessageBox.Show("No ha seleccionado ningún neumático.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pRegistrarSalida.Visible = false;
            pRegistrarSalida.SendToBack();
            lblNombreNeumatico.Text = "";
            lblCantidadNeumatico.Text = "";
            idSalida = 0;
            idIngreso = 0;
            Operacion = 0;
            Mecanico = -1;
            Nombre = "";
            txtOrden.Clear();
            txtMecanico.Clear();
            txtVehiculo.Clear();
            dtpFechaSalida.Value = DateTime.Now;
            txtCantidadS.Clear();
            dgvDetalleOT.DataSource = null;
        }

        private void pRegistrarSalida_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pRegistrarSalida.Left = pRegistrarSalida.Left + (e.X - xClick);
                pRegistrarSalida.Top = pRegistrarSalida.Top + (e.Y - yClick);
            }
        }

        private void txtOrden_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstOT, clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ListarOT_SegundoUso(txtOrden.Text), true, false, false);
            lstOT.Columns[0].Width = 0;
            lstOT.Columns[1].Width = 100;
            lstOT.Columns[2].Width = 350;
            lstOT.Columns[3].Width = 0;
            lstOT.Columns[4].Width = 0;
            lstOT.BringToFront();
            lstOT.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Mecanico = -1;
                dgvDetalleOT.DataSource = null;
                lstOT.Visible = false;
                lstOT.SendToBack();
            }
        }

        private void txtOrden_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstOT.Focus(); }
        }

        private void lstOT_Enter(object sender, EventArgs e)
        {
            if (!lstOT.Items.Count.Equals(0)) { lstOT.Items[0].Selected = true; }
        }

        private void lstOT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstOT.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstOT.SelectedItems[0];

                Mecanico = Int32.Parse(ItemActual.Text);
                txtOrden.Text = ItemActual.SubItems[1].Text;
                txtVehiculo.Text = ItemActual.SubItems[3].Text;
                txtMecanico.Text = ItemActual.SubItems[4].Text;
                dgvDetalleOT.DataSource = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ListarDetalleOTxItemSegundoUso(txtOrden.Text, Codigo);
                
                lstOT.Visible = false;
                lstOT.SendToBack();
                dtpFechaSalida.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                Mecanico = -1;
                lstOT.Visible = false;
                lstOT.SendToBack();
            }
        }

        private void lstOT_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstOT.SelectedItems[0];

            Mecanico = Int32.Parse(ItemActual.Text);
            txtOrden.Text = ItemActual.SubItems[1].Text;
            txtVehiculo.Text = ItemActual.SubItems[3].Text;
            txtMecanico.Text = ItemActual.SubItems[4].Text;
            dgvDetalleOT.DataSource = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ListarDetalleOTxItemSegundoUso(txtOrden.Text, Codigo);
            
            lstOT.Visible = false;
            lstOT.SendToBack();
            dtpFechaSalida.Focus();
        }

        private void dtpFechaSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            { txtCantidadS.Focus(); }
        }

        private void txtCantidadS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            { btnGuardarSalida_Click(sender, e); }
        }

        private void btnRetorno_Click(object sender, EventArgs e)
        {
            try
            {
                Operacion = 2;
                idSalida = Convert.ToInt32(dgvSalidaVista.GetRowCellValue(dgvSalidaVista.FocusedRowHandle, "idSalida"));
                idIngreso = Convert.ToInt32(dgvSalidaVista.GetRowCellValue(dgvSalidaVista.FocusedRowHandle, "idIngresoNeu"));

                lblTitulo.Text = "DATOS DE RETORNO";
                lblCantidadNeumatico.Text = dgvSalidaVista.GetRowCellValue(dgvSalidaVista.FocusedRowHandle, "CANTIDAD").ToString();
                txtOrden.Text = dgvSalidaVista.GetRowCellValue(dgvSalidaVista.FocusedRowHandle, "OT").ToString();
                txtMecanico.Text = dgvSalidaVista.GetRowCellValue(dgvSalidaVista.FocusedRowHandle, "MECANICO").ToString();
                txtVehiculo.Text = dgvSalidaVista.GetRowCellValue(dgvSalidaVista.FocusedRowHandle, "PLACA").ToString();
                Codigo = Convert.ToString(dgvSalidaVista.GetRowCellValue(dgvSalidaVista.FocusedRowHandle, "ITEM"));
                dtpFechaSalida.Value = DateTime.Now;
                txtOrden.ReadOnly = true;
                dgvDetalleOT.DataSource = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ListarDetalleOTxItemSegundoUso(txtOrden.Text, Codigo);

                pRegistrarSalida.Visible = true;
                pRegistrarSalida.BringToFront();
            }
            catch (Exception ex) { MessageBox.Show("No ha seleccionado ningún neumático.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnGuardarSalida_Click(object sender, EventArgs e)
        {
            try
            {
                if (Operacion == 1)     // REGISTRAR SALIDA DE NEUMATICOS
                {
                    if (txtOrden.Text.Length != 0 && txtCantidadS.Text.Length != 0)
                    {
                        if (dgvDetalleOT.Rows.Count > 0)
                        {
                            if (Convert.ToString(dgvDetalleOT.CurrentRow.Cells["Recurso"].Value).TrimEnd() == Codigo &&
                            Convert.ToDecimal(dgvDetalleOT.CurrentRow.Cells["Cantidad"].Value) < Convert.ToDecimal(txtCantidadS.Text))
                            {
                                MessageBox.Show("La cantidad ingresada no puede ser mayor que la cantidad solicitada en la OT.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                DataTable dtRespuesta = new DataTable();
                                string Respuesta;

                                string orden = txtOrden.Text;
                                int cantidad = Convert.ToInt32(txtCantidadS.Text);

                                dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_RegistrarSalida_SegundoUso(idIngreso, orden, cantidad, dtpFechaSalida.Value);
                                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["Mensaje"]);
                                string NroRPTA = Respuesta.Substring(0, 1);
                                if (NroRPTA == "0")
                                {
                                    btnCerrar_Click(sender, e);
                                    ListarIngresos();
                                    ListarSalida();
                                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            }
                        }
                        else { MessageBox.Show("La OT ingresada no necesita ningún neumático.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else
                    {
                        MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        if (txtOrden.Text.Length == 0) { txtOrden.Focus(); }
                        else { txtCantidadS.Focus(); }
                        return;
                    }
                }

                if (Operacion == 2)     // REGISTRAR RETORNO DE NEUMATICOS
                {
                    if (txtCantidadS.Text.Length != 0)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        int cantidad = Convert.ToInt32(txtCantidadS.Text);

                        dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_RegistrarRetorno_SegundoUso(idSalida, idIngreso, cantidad);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0")
                        {
                            btnCerrar_Click(sender, e);
                            ListarIngresos();
                            ListarSalida();
                            MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese la cantidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtCantidadS.Focus();
                        return;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
    }
}
