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
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using System.IO;
using Comun;
using ReportesTranspesa.Properties;
using System.Drawing.Printing;

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmControlAlcoholTest : Form
    {
        int xClick = 0, yClick = 0;
        int idPersonal = -1;
        DataTable dtUsuario = new DataTable();
        string Sucursal;
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        public byte[] byteArrayImagen = null;
        public int posx = 0, posy = 0;
        int idAlcoholTestR = 0;

        public frmControlAlcoholTest()
        {
            InitializeComponent();
            cbxSucursal.SelectedIndexChanged -= cbxSucursal_SelectedIndexChanged;
        }

        private void cbxSucursal_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSucursal(); }

        private void frmControlAlcoholTest_Load(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToShortDateString();

            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmControlAlcoholTest");

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                    {
                        txtDNI.Enabled = true;
                        btnPersonal.Enabled = true;
                    }
                    else
                    {
                        txtDNI.Enabled = false;
                        btnPersonal.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true) { groupBox2.Visible = true; }
                    else { groupBox2.Visible = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { modificarEstadoToolStripMenuItem.Enabled = true; }
                    else { modificarEstadoToolStripMenuItem.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarRegistroToolStripMenuItem.Enabled = true; }
                    else { eliminarRegistroToolStripMenuItem.Enabled = false; }
                }

                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

                if (dtEspeciales.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                    {
                        if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Sucursal Trujillo")
                        {
                            Sucursal = "AB"; label4.Text = "TRUJILLO";
                            i = 999;
                        }
                    }

                    for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                    {
                        if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Sucursal Salaverry")
                        {
                            Sucursal = "SAL"; label4.Text = "SALAVERRY";
                            i = 999;
                        }
                    }

                    for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                    {
                        if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Sucursal Lima")
                        {
                            Sucursal = "BLIM"; label4.Text = "LIMA";
                            i = 999;
                        }
                    }

                    for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                    {
                        if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Sucursal La Encalada")
                        {
                            Sucursal = "ENCA"; label4.Text = "LA ENCALADA";
                            i = 999;
                        }
                    }

                    for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                    {
                        if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Sucursal Larrea 1")
                        {
                            Sucursal = "LAR1"; label4.Text = "LARREA 1";
                            i = 999;
                        }
                    }

                    for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                    {
                        if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Sucursal Larrea 2")
                        {
                            Sucursal = "LAR2"; label4.Text = "LARREA 2";
                            i = 999;
                        }
                    }
                }
                else { Sucursal = " "; label4.Text = " "; }
            }
            else
            {
                txtDNI.Enabled = false;
                groupBox2.Enabled = false;
                eliminarRegistroToolStripMenuItem.Enabled = false;
            }

            FechaProgIni.Value = DateTime.Now;
            FechaProgFin.Value = DateTime.Now;
            CargarComboSucursal();
            ListarResultados(Sucursal);
        }


        private void CargarComboSucursal()
        {
            DataTable dtConcepto = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_BuscarSede(Utilitario.Instancia.SesionUsuario.usuario, 2);
            cbxSucursal.DataSource = dtConcepto;
            cbxSucursal.DisplayMember = "Descripcion";
            cbxSucursal.ValueMember = "Sucursal";
        }

        public void ListarResultados(string s)
        {
            if (FechaProgIni.Value > FechaProgFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                FechaProgIni.Focus();
                return;
            }
            else
            {
                dtgHistorial.DataSource = null;
                dgvHistorialVista.Columns.Clear();

                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Clear();
                dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_ListarResultados(s, FechaProgIni.Text, FechaProgFin.Text);
                if (dt.Rows.Count > 0)
                {
                    dtgHistorial.DataSource = dt;

                    dgvHistorialVista.Columns["idAlcoholTest"].Visible = false;
                    dgvHistorialVista.Columns["Imagen"].Visible = false;

                    dgvHistorialVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvHistorialVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvHistorialVista.BestFitColumns();
                }
            }
        }


        private void txtDNI_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            { e.Handled = true; }
            else
            { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                // LLENAR DATOS DE PERSONAL CON DNI
                DataTable dtPersonal = new DataTable();
                dtPersonal = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_BuscarPersonal(txtDNI.Text);

                if (dtPersonal.Rows.Count > 0)
                {
                    idPersonal = Convert.ToInt32(dtPersonal.Rows[0]["Persona"]);
                    txtID.Text = dtPersonal.Rows[0]["Documento"].ToString();
                    txtNombre.Text = dtPersonal.Rows[0]["NombreCompleto"].ToString();
                    txtArea.Text = dtPersonal.Rows[0]["Area"].ToString();
                    txtCargo.Text = dtPersonal.Rows[0]["Cargo"].ToString();

                    pDatosPersonal.Visible = true;
                    pDatosPersonal.BringToFront();

                    btnNegativo.Focus();
                    //btnNegativo.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                }
                else
                {
                    MessageBox.Show("El DNI ingresado no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDNI.Clear();
                    return;
                }
            }
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            DataTable dtPersonal = new DataTable();
            dtPersonal = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_BuscarPersonal(txtDNI.Text);

            if (dtPersonal.Rows.Count > 0)
            {
                idPersonal = Convert.ToInt32(dtPersonal.Rows[0]["Persona"]);
                txtID.Text = dtPersonal.Rows[0]["Documento"].ToString();
                txtNombre.Text = dtPersonal.Rows[0]["NombreCompleto"].ToString();
                txtArea.Text = dtPersonal.Rows[0]["Area"].ToString();
                txtCargo.Text = dtPersonal.Rows[0]["Cargo"].ToString();

                pDatosPersonal.Visible = true;
                pDatosPersonal.BringToFront();

                btnNegativo.Focus();
                //btnNegativo.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            }
            else
            {
                MessageBox.Show("El DNI ingresado no existe.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDNI.Clear();
                return;
            }
        }

        private void btnPositivo_Click(object sender, EventArgs e)
        {
            DataTable dtAgregar = new DataTable();
            string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            dtAgregar = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_IngresarResultado(idPersonal, 1, "INGRESO", Sucursal, Usuario);
            respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
            string NroRspta = respta.Substring(0, 1);
            
            if (NroRspta == "0")
            {
                MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCerrar_Click(sender, e);
                txtDNI.Clear();
                FechaProgIni.Value = DateTime.Now;
                FechaProgFin.Value = DateTime.Now;
                label3.Text = DateTime.Now.ToShortDateString();
                ListarResultados(Sucursal);
            }
            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnNegativo_Click(object sender, EventArgs e)
        {
            DataTable dtAgregar = new DataTable();
            string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            dtAgregar = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_IngresarResultado(idPersonal, 0, "INGRESO", Sucursal, Usuario);
            respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
            string NroRspta = respta.Substring(0, 1);

            if (NroRspta == "0")
            {
                MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCerrar_Click(sender, e);
                txtDNI.Clear();
                FechaProgIni.Value = DateTime.Now;
                FechaProgFin.Value = DateTime.Now;
                label3.Text = DateTime.Now.ToShortDateString();
                ListarResultados(Sucursal);
            }
            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnPositivoS_Click(object sender, EventArgs e)
        {
            DataTable dtAgregar = new DataTable();
            string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            dtAgregar = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_IngresarResultado(idPersonal, 1, "SALIDA", Sucursal, Usuario);
            respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
            string NroRspta = respta.Substring(0, 1);

            if (NroRspta == "0")
            {
                MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCerrar_Click(sender, e);
                txtDNI.Clear();
                FechaProgIni.Value = DateTime.Now;
                FechaProgFin.Value = DateTime.Now;
                label3.Text = DateTime.Now.ToShortDateString();
                ListarResultados(Sucursal);
            }
            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnNegativoS_Click(object sender, EventArgs e)
        {
            DataTable dtAgregar = new DataTable();
            string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            dtAgregar = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_IngresarResultado(idPersonal, 0, "SALIDA", Sucursal, Usuario);
            respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
            string NroRspta = respta.Substring(0, 1);

            if (NroRspta == "0")
            {
                MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCerrar_Click(sender, e);
                txtDNI.Clear();
                FechaProgIni.Value = DateTime.Now;
                FechaProgFin.Value = DateTime.Now;
                label3.Text = DateTime.Now.ToShortDateString();
                ListarResultados(Sucursal);
            }
            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /*
        private void btnNegativo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                btnPositivo.Focus();
                btnNegativo.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                btnPositivo.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            }
        }

        private void btnPositivo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Right)
            {
                btnNegativo.Focus();
                btnPositivo.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Regular);
                btnNegativo.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            }
        }
        */

        private void dgvHistorialVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "RESULTADO")
            {
                if (e.CellValue.ToString() == "POSITIVO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "NEGATIVO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void txtDNI_Click(object sender, EventArgs e)
        {
            FechaProgIni.Value = DateTime.Now;
            FechaProgFin.Value = DateTime.Now;
            label3.Text = DateTime.Now.ToShortDateString();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pDatosPersonal.Visible = false;
            pDatosPersonal.SendToBack();
            txtID.Clear();
            txtNombre.Clear();
            txtArea.Clear();
            txtCargo.Clear();
            idPersonal = -1;
        }

        private void pDatosPersonal_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pDatosPersonal.Left = pDatosPersonal.Left + (e.X - xClick);
                pDatosPersonal.Top = pDatosPersonal.Top + (e.Y - yClick);
            }
        }

        private void FechaProgIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarResultados(Convert.ToString(cbxSucursal.SelectedValue)); }
        }

        private void FechaProgFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarResultados(Convert.ToString(cbxSucursal.SelectedValue)); }
        }

        private void cbxSucursal_DropDownClosed(object sender, EventArgs e)
        { ListarResultados(Convert.ToString(cbxSucursal.SelectedValue)); }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarResultados(Convert.ToString(cbxSucursal.SelectedValue)); }

        private void dtgHistorial_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true || Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                {
                    idAlcoholTestR = Convert.ToInt32(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "idAlcoholTest"));
                    txtFechaR.Text = Convert.ToString(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "FECHA"));
                    txtDNIR.Text = Convert.ToString(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "DNI"));
                    txtNombresR.Text = Convert.ToString(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "NOMBRE"));
                    txtAreaR.Text = Convert.ToString(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "AREA"));
                    txtCargoR.Text = Convert.ToString(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "CARGO"));
                    txtSedeR.Text = Convert.ToString(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "SEDE"));
                    txtHorarioR.Text = Convert.ToString(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "HORARIO"));

                    if (dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "Imagen").ToString() != "")
                    {
                        byteArrayImagen = (Byte[])dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "Imagen");
                        Byte[] byteBLOBData = (Byte[])dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "Imagen");
                        Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                        pbImagen.Image = x;
                        pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                    }

                    pRegistroAlcoholTest.Location = new System.Drawing.Point(574, 239);
                    pRegistroAlcoholTest.Visible = true;
                    pRegistroAlcoholTest.BringToFront();
                }
            }
            catch { }
        }

        private void btnBuscarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK)
                {
                    byteArrayImagen = File.ReadAllBytes(getImage.FileName);

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayImagen));
                    pbImagen.Image = x;
                    pbImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrarPB_Click(object sender, EventArgs e)
        {
            byteArrayImagen = null;
            pbImagen.Image = null;
        }

        private void btnCerrarR_Click(object sender, EventArgs e)
        {
            byteArrayImagen = null;
            pbImagen.Image = null;
            
            pRegistroAlcoholTest.Visible = false;
            pRegistroAlcoholTest.SendToBack();

            idAlcoholTestR = -1;
            txtFechaR.Clear();
            txtDNIR.Clear();
            txtNombresR.Clear();
            txtAreaR.Clear();
            txtCargoR.Clear();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            DataTable dtImagenAT = new DataTable();
            string Respuesta;

            dtImagenAT = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_AgregarImagen(idAlcoholTestR, byteArrayImagen, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtImagenAT.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarResultados(Convert.ToString(cbxSucursal.SelectedValue));
                btnCerrarR_Click(sender, e);
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pRegistroAlcoholTest_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { posx = e.X; posy = e.Y; }
            else
            {
                pRegistroAlcoholTest.Left = pRegistroAlcoholTest.Left + (e.X - posx);
                pRegistroAlcoholTest.Top = pRegistroAlcoholTest.Top + (e.Y - posy);
            }
        }

        private void modificarEstadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idAlcoholTest = Convert.ToInt32(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "idAlcoholTest"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (idAlcoholTest != 0)
            {
                if (MessageBox.Show("¿Desea cambiar el estado de este registro?", "MODIFICAR ESTADO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_EliminarResultados(2, idAlcoholTest, Usuario);
                    string Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarResultados(Sucursal); }
                }
            }
            else { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        } 

        private void eliminarRegistroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int idAlcoholTest = Convert.ToInt32(dgvHistorialVista.GetRowCellValue(dgvHistorialVista.FocusedRowHandle, "idAlcoholTest"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (idAlcoholTest != 0)
            {
                if (MessageBox.Show("¿Desea eliminar este registro?", "ELIMINAR REGISTRO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_AlcoholTest_EliminarResultados(1, idAlcoholTest, Usuario);
                    string Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarResultados(Sucursal); }
                }
            }
            else { MessageBox.Show("El elemento seleccionado no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgHistorial.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Resultados de Alcohol Test - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgHistorial.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
