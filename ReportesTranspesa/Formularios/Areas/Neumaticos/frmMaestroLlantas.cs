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
    public partial class frmMaestroLlantas : Form
    {
        public int idReencauche = 0;
        int xClick = 0, yClick = 0;
        byte[] byteArrayImagen = null;
        public frmSegundoUsoNeumaticos frmSegundoUsoNeumaticos = new frmSegundoUsoNeumaticos();
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        int e1 = 0;

        public frmMaestroLlantas()
        {
            InitializeComponent();
            cbxMedida.SelectedIndexChanged -= cbxMedida_SelectedIndexChanged;
            cbxMarca.SelectedIndexChanged -= cbxMarca_SelectedIndexChanged;
            cbxMedida2.SelectedIndexChanged -= cbxMedida2_SelectedIndexChanged;
            cbxDisenio.SelectedIndexChanged -= cbxDisenio_SelectedIndexChanged;
        }

        private void cbxMedida_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMedida(); }

        private void cbxMarca_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMarca(); }

        private void cbxMedida2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMedida2(); }

        private void cbxDisenio_SelectedIndexChanged(object sender, EventArgs e) { CargarComboDisenio(); }

        private void frmMaestroLlantas_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmSegundoUsoNeumaticos");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        actualizarEstadoToolStripMenuItem.Enabled = true;
                        eliminarNeumaticoToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        actualizarEstadoToolStripMenuItem.Enabled = false;
                        eliminarNeumaticoToolStripMenuItem.Enabled = false;
                    }
                }

                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

                if (dtEspeciales.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                    {
                        if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Solicitar Reencauche")
                        {
                            groupBox1.Enabled = true;
                            i = 999; e1 = 1;
                        }
                        else { groupBox1.Enabled = false; }
                    }
                }
                else { groupBox1.Enabled = false; }
            }
            
            dtpFechaEnvio.Value = DateTime.Now;
            dtpFechaRecepcion.Value = DateTime.Now;
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            cbxObservacion.Text = "TODOS";
            
            CargarComboMedida();
            CargarComboMarca();
            CargarComboMedida2();

            ListarNeumaticos();
        }


        public void CargarComboMedida()
        {
            DataTable dtMedida = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_Listar(2);
            cbxMedida.DataSource = dtMedida;
            cbxMedida.DisplayMember = "Descripcion";
            cbxMedida.ValueMember = "idMedida";
        }

        public void CargarComboMarca()
        {
            DataTable dtMarca = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_Listar(3);
            cbxMarca.DataSource = dtMarca;
            cbxMarca.DisplayMember = "Descripcion";
            cbxMarca.ValueMember = "idMarca";
        }

        public void CargarComboMedida2()
        {
            DataTable dtMedida2 = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_Listar(4);
            cbxMedida2.DataSource = dtMedida2;
            cbxMedida2.DisplayMember = "Descripcion";
            cbxMedida2.ValueMember = "idMedida";
        }

        public void CargarComboDisenio()
        {
            DataTable dtDisenio = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_Listar(5);
            cbxDisenio.DataSource = dtDisenio;
            cbxDisenio.DisplayMember = "Descripcion";
            cbxDisenio.ValueMember = "Costo";
        }

        private void ListarNeumaticos()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                DataTable dt = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ListarReencauche_SegundoUso(txtCodigo2.Text, cbxMedida2.Text, cbxObservacion.Text, dtpFechaIni.Text, dtpFechaFin.Text);
                dtgListaNeumaticos.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    dgvListaNeumaticos.Columns["idReencauche"].Visible = false;
                    dgvListaNeumaticos.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaNeumaticos.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvListaNeumaticos.Columns["FechaModif"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaNeumaticos.Columns["FechaModif"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                }
            }
        }


        private void txtCodigo_Enter(object sender, EventArgs e) { txtCodigo.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtCodigo_Leave(object sender, EventArgs e) { txtCodigo.BackColor = Color.White; }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }
            */

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtProveedor.Focus(); }
        }

        private void txtProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaEnvio.Focus(); }
        }

        private void dtpFechaEnvio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtEstado.Focus(); }
        }

        private void txtEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtDEvaluacion.Focus(); }
        }

        private void txtDEvaluacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtGRR.Focus(); }
        }

        private void txtGRR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnImagen.Focus(); }
        }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK) { byteArrayImagen = File.ReadAllBytes(getImage.FileName); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigo.Text.Length == 0 || txtProveedor.Text.Length == 0 || txtEstado.Text.Length == 0 || txtDEvaluacion.Text.Length == 0 || txtGRR.Text.Length == 0)
                {
                    MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (txtCodigo.Text.Length == 0) { txtCodigo.Focus(); }
                    else
                    {
                        if (txtProveedor.Text.Length == 0) { txtProveedor.Focus(); }
                        else
                        {
                            if (txtEstado.Text.Length == 0) { txtEstado.Focus(); }
                            else
                            {
                                if (txtDEvaluacion.Text.Length == 0) { txtDEvaluacion.Focus(); }
                                else { txtGRR.Focus(); }
                            }
                        }
                    }
                    return;
                }
                else
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_Registrar_Neumatico_SegundoUso(Convert.ToInt32(txtCodigo.Text), cbxMedida.Text,
                    cbxMarca.Text, txtProveedor.Text, dtpFechaEnvio.Value, txtEstado.Text, txtDEvaluacion.Text, txtGRR.Text, byteArrayImagen);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodigo.Clear();
                        cbxMarca.SelectedValue = 1;
                        cbxMedida.SelectedValue = 1;
                        dtpFechaEnvio.Value = DateTime.Now;
                        txtEstado.Clear();
                        txtDEvaluacion.Clear();
                        txtGRR.Clear();
                        byteArrayImagen = null;
                        ListarNeumaticos();
                        frmSegundoUsoNeumaticos.ListarNeumaticos();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtCodigo2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarNeumaticos(); }
        }

        private void cbxMedida2_DropDownClosed(object sender, EventArgs e) { ListarNeumaticos(); }

        private void cbxObservacion_DropDownClosed(object sender, EventArgs e) { ListarNeumaticos(); }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarNeumaticos(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarNeumaticos(); }
        }

        private void dgvListaNeumaticos_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "OBSERVACION")
            {
                if (Convert.ToString(e.CellValue) == "REENCAUCHADO")
                { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "PENDIENTE")
                { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (Convert.ToString(e.CellValue) == "RECHAZADO")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (Convert.ToString(e.CellValue) == "RECLAMO")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (Convert.ToString(e.CellValue) == "DE BAJA")
                { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (Convert.ToString(e.CellValue) == "REPARACIÓN")
                { e.Appearance.BackColor = Color.FromArgb(255, 165, 0); }

                if (Convert.ToString(e.CellValue) == "SACAR BANDA")
                { e.Appearance.BackColor = Color.FromArgb(255, 165, 0); }

                if (Convert.ToString(e.CellValue) == "APLICACIÓN")
                { e.Appearance.BackColor = Color.FromArgb(255, 165, 0); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarNeumaticos(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaNeumaticos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Neumáticos en Reencauche - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaNeumaticos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void actualizarEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            idReencauche = Convert.ToInt32(dgvListaNeumaticos.GetRowCellValue(dgvListaNeumaticos.FocusedRowHandle, "idReencauche"));
            lblMedida.Text = Convert.ToString(dgvListaNeumaticos.GetRowCellValue(dgvListaNeumaticos.FocusedRowHandle, "MEDIDA"));
            lblMarca.Text = Convert.ToString(dgvListaNeumaticos.GetRowCellValue(dgvListaNeumaticos.FocusedRowHandle, "MARCA"));
            lblCodigo.Text = Convert.ToString(dgvListaNeumaticos.GetRowCellValue(dgvListaNeumaticos.FocusedRowHandle, "CODIGO"));

            cbxObservacion2.Text = "REENCAUCHADO";
            txtGRR2.Text = Convert.ToString(dgvListaNeumaticos.GetRowCellValue(dgvListaNeumaticos.FocusedRowHandle, "GUIA_RECEPCION"));
            CargarComboDisenio();
            cbxDisenio_DropDownClosed(sender, e);


            pActualizarReencauche.Visible = true;
            pActualizarReencauche.BringToFront();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            idReencauche = 0;
            lblMedida.Text = "";
            lblMarca.Text = "";
            lblCodigo.Text = "";
            txtGRR2.Clear();
            txtCosto.Clear();
            cbxObservacion2.Text = "REENCAUCHADO";
            dtpFechaRecepcion.Value = DateTime.Now;

            pActualizarReencauche.Visible = false;
            pActualizarReencauche.SendToBack();
        }

        private void pActualizarReencauche_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pActualizarReencauche.Left = pActualizarReencauche.Left + (e.X - xClick);
                pActualizarReencauche.Top = pActualizarReencauche.Top + (e.Y - yClick);
            }
        }

        private void txtGRR2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaRecepcion.Focus(); }
        }

        private void dtpFechaRecepcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtCosto.Focus(); }
        }

        private void cbxDisenio_DropDownClosed(object sender, EventArgs e) { txtCosto.Text = Convert.ToString(cbxDisenio.SelectedValue); }

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar_Click(sender, e); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtCosto.Text.Length == 0)
            {
                MessageBox.Show("Este campo no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cbxDisenio.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta2 = new DataTable();
                string Respuesta2;
                dtRespuesta2 = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_SegundoUso_InsertarReencauchados(idReencauche, dtpFechaRecepcion.Value, cbxObservacion2.Text, txtGRR2.Text,
                               cbxDisenio.Text, Convert.ToDecimal(txtCosto.Text));
                Respuesta2 = Convert.ToString(dtRespuesta2.Rows[0]["exito"]);
                string NroRPTA2 = Respuesta2.Substring(0, 1);
                if (NroRPTA2 == "0")
                {
                    MessageBox.Show(Respuesta2, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCerrar_Click(sender, e);
                    ListarNeumaticos();
                    frmSegundoUsoNeumaticos.ListarNeumaticos();
                }
                else { MessageBox.Show(Respuesta2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void eliminarNeumaticoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este neumático del registro?", "ELIMINAR NEUMÁTICO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int idReencauche = Convert.ToInt32(dgvListaNeumaticos.GetRowCellValue(dgvListaNeumaticos.FocusedRowHandle, "idReencauche"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_Maestro_SegundoUso_EliminarNeumaticos(idReencauche);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        //MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        byteArrayImagen = null;
                        ListarNeumaticos();
                        frmSegundoUsoNeumaticos.ListarNeumaticos();

                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
