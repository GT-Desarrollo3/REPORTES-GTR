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
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmListadoUnidades : Form
    {
        int ID, IdBloqueo, PermisoBloqueo;
        string placa, motivo;

        public frmListadoUnidades()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
            cbxTipo.SelectedIndexChanged -= cbxTipo_SelectedIndexChanged;
        }

        private void cbxTipo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboTipo(); }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones(); }

        private void frmListadoUnidades_Load(object sender, EventArgs e)
        {
            CargarComboTipo();
            CargarComboOperaciones();
            cbxTipo.Text = "TODOS";
            cbxOperaciones.SelectedValue = 5;
            Listardatos();
            gvrUnidades.OptionsBehavior.Editable = false;
            grvUnidadesBloqueadas.OptionsBehavior.Editable = false;

            string Respuesta;
            DataTable dtPermiso = new DataTable();
            dtPermiso = clsOperacionesBL.Instancia.GetLista_Consulta_Permiso_BloqueoDesbloqueo(Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtPermiso.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0") { PermisoBloqueo = 1; }
            else { PermisoBloqueo = 0; }
        }


        public void CargarComboTipo()
        {
            DataTable dtSubTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(6, 2);
            cbxTipo.DataSource = dtSubTipo;
            cbxTipo.DisplayMember = "Descripcion";
            cbxTipo.ValueMember = "idSubTipoVehiculo";
        }

        public void CargarComboOperaciones()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperaciones;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        private void Listardatos()
        {
            DataTable dtUnidadesListadas = new DataTable();
            DataTable dtUniBloqueadas = new DataTable();

            dtUnidadesListadas = clsOperacionesBL.Instancia.GetOperaciones_ListarUnidades(txtPlaca.Text, cbxTipo.Text, cbxOperaciones.Text);
            if (dtUnidadesListadas.Rows.Count > 0)
            {
                gvUnidades.DataSource = dtUnidadesListadas;
                gvrUnidades.BestFitColumns();
            }
            else { gvUnidades.DataSource = null; }

            dtUniBloqueadas = clsOperacionesBL.Instancia.GetOperaciones_ListarUnidadesBloqueadas(txtPlaca.Text, cbxTipo.Text, cbxOperaciones.Text);
            if (dtUniBloqueadas.Rows.Count > 0)
            {
                gvUnidadesBloqueadas.DataSource = dtUniBloqueadas;
                grvUnidadesBloqueadas.Columns["ID"].Visible = false;
                grvUnidadesBloqueadas.Columns["FECHA_INICIO"].Visible = false;
                grvUnidadesBloqueadas.Columns["FECHA_FIN"].Visible = false;
                grvUnidadesBloqueadas.BestFitColumns();
            }
            else { gvUnidadesBloqueadas.DataSource = null; }
        }


        private void btnBloquear_Click(object sender, EventArgs e)
        {          
            try
            {
                if (PermisoBloqueo == 1)
                {
                    int[] filass = gvrUnidades.GetSelectedRows();
                    string datoseleccionado = gvrUnidades.GetFocusedValue().ToString();

                    for (int i = 0; i < filass.Length; i++)
                    {
                        ID = Convert.ToInt32(gvrUnidades.GetRowCellValue(filass[i], "ID").ToString());
                        placa = gvrUnidades.GetRowCellValue(filass[i], "UNIDAD").ToString();
                       // motivo = gvrUnidades.GetRowCellValue(filass[i], "MOTIVO").ToString();

                        if (Convert.ToInt32(ID) > 0)
                        {
                            // OTselec = gvrUnidades.GetRowCellValue(filas[i], "ID").ToString();
                            BloqueUnidades.frmNuevoBloqueoUnidades frmBloque = new BloqueUnidades.frmNuevoBloqueoUnidades();
                            frmBloque.Text = "Bloquear Unidades";
                            frmBloque.setearvariable(1, 0, "", ID, placa, "");
                            frmBloque.ShowDialog();
                            Listardatos();
                        }
                    }
                }
                else { MessageBox.Show("No tienes permiso para Bloquear Unidades", "ADVERTENCIA"); }
            }
            catch (Exception) { }

        }
        int count = 1;
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (count == 1)
            {
                btnDesbloquear.Visible = true;
                btnBloquear.Visible = false;
                btnExcel.Visible = true;
                count = 0;
            }
            else 
            {
                btnDesbloquear.Visible = false;
                btnBloquear.Visible =true;
                btnExcel.Visible = false;
                count = 1;
            }
        }

        private void btnDesbloquear_Click(object sender, EventArgs e)
        {           
            try
            {
                if (PermisoBloqueo == 1)
                {
                    int[] filass = grvUnidadesBloqueadas.GetSelectedRows();
                    string datoseleccionado = grvUnidadesBloqueadas.GetFocusedValue().ToString();

                    for (int i = 0; i < filass.Length; i++)
                    {
                        ID = Convert.ToInt32(grvUnidadesBloqueadas.GetRowCellValue(filass[i], "ID").ToString());
                        placa = grvUnidadesBloqueadas.GetRowCellValue(filass[i], "UNIDAD").ToString();
                        IdBloqueo = Convert.ToInt32(grvUnidadesBloqueadas.GetRowCellValue(filass[i], "N°").ToString());
                        motivo = grvUnidadesBloqueadas.GetRowCellValue(filass[i], "MOTIVO").ToString();
                        if (Convert.ToInt32(ID) > 0)
                        {
                            // OTselec = gvrUnidades.GetRowCellValue(filas[i], "ID").ToString();
                            BloqueUnidades.frmNuevoBloqueoUnidades frmBloque = new BloqueUnidades.frmNuevoBloqueoUnidades();
                            frmBloque.Text = "Desbloquear Unidades";
                            frmBloque.setearvariable(2, IdBloqueo, "", ID, placa,motivo);
                            frmBloque.ShowDialog();
                            Listardatos();
                        }
                    }
                }
                else { MessageBox.Show("No tienes permiso para Desbloquear Unidades", "ADVERTENCIA"); }
            }
            catch (Exception) { }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (gvUnidadesBloqueadas.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte Unidades Bloqueadas " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                gvUnidadesBloqueadas.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            BloqueUnidades.frmHistorialBloqueo frmBloque = new BloqueUnidades.frmHistorialBloqueo();
            frmBloque.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e) { Listardatos(); }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { Listardatos(); }
        }

        private void cbxTipo_DropDownClosed(object sender, EventArgs e) { Listardatos(); }

        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e) { Listardatos(); }
    }
}
