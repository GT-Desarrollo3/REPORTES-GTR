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
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmIngresoTerceros : MetroFramework.Forms.MetroForm
    {
        DataTable dtListaIngresos = new DataTable();
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        string Area;
        public int xClick = 0, yClick = 0;

        public frmIngresoTerceros()
        {
            InitializeComponent();
            cbxArea.SelectedIndexChanged -= cbxArea_SelectedIndexChanged;
        }

        private void cbxArea_SelectedIndexChanged(object sender, EventArgs e) { CargarComboArea(); }

        private void frmIngresoTerceros_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmIngresoTerceros");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnRegistrarIngreso.Enabled = true; }
                else { btnRegistrarIngreso.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsValidarAcceso.Enabled = true; }
                else { tsValidarAcceso.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true) { tsAniadirIngreso.Enabled = true; }
                else { tsAniadirIngreso.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarRegistro.Enabled = true; }
                else { tsEliminarRegistro.Enabled = false; }
            }
            
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            CargarComboArea();

            DataTable dtAreaUsuario = new DataTable();
            dtAreaUsuario = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios(Utilitario.Instancia.SesionUsuario.usuario);

            if (dtAreaUsuario.Rows.Count > 0)
            {
                Area = dtAreaUsuario.Rows[0]["AREA"].ToString();
                cbxArea.Text = Area;
                cbxArea.Enabled = false;
            }

            if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
            { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }

            if (dtEspeciales != null)
            {
                if (dtEspeciales.Rows.Count > 0)
                {
                    cbxArea.Text = "TODAS";
                    cbxArea.Enabled = true;
                }
            }

            ListarExternos();
        }


        private void CargarComboArea()
        {
            DataTable dtArea = clsSeguridadBL.Instancia.ReportesApp_Seguridad_DocumentosSIG_ListarAreas(1);
            cbxArea.DataSource = dtArea;
            cbxArea.DisplayMember = "AREA";
            cbxArea.ValueMember = "CODIGO";
        }

        public void ListarExternos()
        {
            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                dtListaIngresos = clsSeguridadBL.Instancia.ReportesApp_Seguridad_PersonalExterno_ListarExterno(dtpFechaIni.Text, dtpFechaFin.Text, cbxArea.Text,
                                                           txtPerExt.Text, txtEmpExt.Text);
                dtgRegistroExterno.DataSource = null;
                dgvRegistroExternoVista.Columns.Clear();
                dtgRegistroExterno.DataSource = dtListaIngresos;
                if (dtListaIngresos.Rows.Count > 0)
                {
                    dgvRegistroExternoVista.Columns["EXTENSION"].Visible = false;
                    dgvRegistroExternoVista.Columns["PersonaResp"].Visible = false;

                    dgvRegistroExternoVista.Columns["FECHA_INGRESO"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRegistroExternoVista.Columns["FECHA_INGRESO"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvRegistroExternoVista.Columns["FechaCrea"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRegistroExternoVista.Columns["FechaCrea"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                    dgvRegistroExternoVista.Columns["FechaModifica"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvRegistroExternoVista.Columns["FechaModifica"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                    dgvRegistroExternoVista.Columns["VALIDACION"].Summary.Clear();
                    dgvRegistroExternoVista.Columns["VALIDACION"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "VALIDACION", "Total = {0}");

                    dgvRegistroExternoVista.BestFitColumns();
                }
            }
        }


        private void btnRegistrarIngreso_Click(object sender, EventArgs e)
        {
            frmNuevoTercero frmNuevoTercero = new frmNuevoTercero();
            frmNuevoTercero.Opcion = 1;
            frmNuevoTercero.dtpFechaIngreso.Value = DateTime.Now;
            frmNuevoTercero.ListarExternos();
            frmNuevoTercero.CargarComboArea2();

            DataTable dtAreaUsuario = new DataTable();
            dtAreaUsuario = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios(Utilitario.Instancia.SesionUsuario.usuario);
            if (dtAreaUsuario.Rows.Count > 0)
            {
                string Area2 = dtAreaUsuario.Rows[0]["AREA"].ToString();
                frmNuevoTercero.cbxArea2.Text = Area2;
            }

            frmNuevoTercero.frmIngresoTerceros = this;
            frmNuevoTercero.Show(this);
        }

        private void dtgRegistroExterno_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Nro = dgvRegistroExternoVista.GetRowCellValue(dgvRegistroExternoVista.FocusedRowHandle, "NRO").ToString();
                string Validacion = dgvRegistroExternoVista.GetRowCellValue(dgvRegistroExternoVista.FocusedRowHandle, "VALIDACION").ToString();

                if (Nro != "")
                {
                    if (Validacion == "NO")
                    {
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsValidarAcceso.Enabled = true; }
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminarRegistro.Enabled = true; }
                        tsAniadirIngreso.Enabled = false;
                    }
                    else
                    {
                        tsValidarAcceso.Enabled = false;
                        tsEliminarRegistro.Enabled = false;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true) { tsAniadirIngreso.Enabled = true; }
                    }
                }
                else
                {
                    tsValidarAcceso.Enabled = false;
                    tsAniadirIngreso.Enabled = false;
                    tsEliminarRegistro.Enabled = false;
                }
            }
            catch
            {
                tsValidarAcceso.Enabled = false;
                tsAniadirIngreso.Enabled = false;
                tsEliminarRegistro.Enabled = false;
            }
        }

        private void dgvRegistroExternoVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "DOCUMENTO")
            {
                e.Appearance.ForeColor = Color.DodgerBlue;
                //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            
            if (e.Column.FieldName == "VALIDACION")
            {
                if (Convert.ToString(e.CellValue) == "SÍ") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (Convert.ToString(e.CellValue) == "NO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 0, 0);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void dgvRegistroExternoVista_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            if (e.Column.FieldName != "DOCUMENTO") return;

            var view = (GridView)sender;
            var row = view.GetDataRow(e.RowHandle);
            if (row == null) return;

            int idNro3 = Convert.ToInt32(row["NRO"]);
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dt = clsSeguridadBL.Instancia.ReportesApp_Seguridad_PersonalExterno_BuscarDocumentos(1, idNro3, Usuario);

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No se encontró el archivo.");
                return;
            }

            byte[] bytes = dt.Rows[0]["ARCHIVO"] as byte[];

            if (bytes == null || bytes.Length == 0)
            {
                MessageBox.Show("El archivo está vacío.");
                return;
            }

            string titulo = Convert.ToString(row["DOCUMENTO"]);
            string extension = Convert.ToString(row["EXTENSION"]);

            using (var sfd = new SaveFileDialog())
            {
                sfd.FileName = titulo + extension;
                sfd.Filter = "ARCHIVO (*" + extension + ")|*" + extension + "|Todos los archivos (*.*)|*.*";
                sfd.AddExtension = true;
                sfd.OverwritePrompt = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(sfd.FileName, bytes);

                    try
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = sfd.FileName,
                            UseShellExecute = true
                        });
                    }
                    catch { }
                }
            }
        }

        private void dtpFechaIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarExternos(); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarExternos(); }
        }

        private void cbxArea_DropDownClosed(object sender, EventArgs e) { ListarExternos(); }

        private void txtPerExt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarExternos(); }
        }

        private void txtEmpExt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarExternos(); }
        }

        private void tsValidarAcceso_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea aprobar este ingreso?", "VALIDAR ACCESO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int Nro2 = Convert.ToInt32(dgvRegistroExternoVista.GetRowCellValue(dgvRegistroExternoVista.FocusedRowHandle, "NRO"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_PersonalExterno_BuscarDocumentos(2, Nro2, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0") { ListarExternos(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsAniadirIngreso_Click(object sender, EventArgs e)
        {
            dtpFechaIngreso.Value = DateTime.Today;
            dtpFechaSalida.Value = DateTime.Today;

            dtpFechaIngreso.Value = Convert.ToDateTime(dgvRegistroExternoVista.GetRowCellValue(dgvRegistroExternoVista.FocusedRowHandle, "FECHA_ENTRADA") == DBNull.Value ? DateTime.Today : dgvRegistroExternoVista.GetRowCellValue(dgvRegistroExternoVista.FocusedRowHandle, "FECHA_ENTRADA"));
            dtpFechaSalida.Value = Convert.ToDateTime(dgvRegistroExternoVista.GetRowCellValue(dgvRegistroExternoVista.FocusedRowHandle, "FECHA_SALIDA") == DBNull.Value ? DateTime.Today : dgvRegistroExternoVista.GetRowCellValue(dgvRegistroExternoVista.FocusedRowHandle, "FECHA_SALIDA"));
            
            pIngresarFechas.Location = new System.Drawing.Point(781, 326);
            pIngresarFechas.Visible = true;
            pIngresarFechas.BringToFront();
        }

        private void pIngresarFechas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pIngresarFechas.Left = pIngresarFechas.Left + (e.X - xClick);
                pIngresarFechas.Top = pIngresarFechas.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pIngresarFechas.Visible = false;
            pIngresarFechas.SendToBack();
        }

        private void dtpFechaIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFechaSalida.Focus(); }
        }

        private void dtpFechaSalida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar.Focus(); }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int Nro3 = Convert.ToInt32(dgvRegistroExternoVista.GetRowCellValue(dgvRegistroExternoVista.FocusedRowHandle, "NRO"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_PersonalExterno_IngresarTiempos(Nro3, dtpFechaIngreso.Value, dtpFechaSalida.Value, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0")
            {
                btnCerrar_Click(sender, e);
                ListarExternos();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsEliminarRegistro_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este ingreso?", "ELIMINAR REGISTRO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int Nro2 = Convert.ToInt32(dgvRegistroExternoVista.GetRowCellValue(dgvRegistroExternoVista.FocusedRowHandle, "NRO"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsSeguridadBL.Instancia.ReportesApp_Seguridad_PersonalExterno_BuscarDocumentos(3, Nro2, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0") { ListarExternos(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarExternos(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgRegistroExterno.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Ingreso de Empresas Externas - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgRegistroExterno.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
