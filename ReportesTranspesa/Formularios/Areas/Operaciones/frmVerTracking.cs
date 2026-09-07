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
using ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes;
using System.Globalization;
using System.Diagnostics;
using DevExpress.Utils;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class frmVerTracking : Form
    {
        int _var_IdProg;
        string _codigo;
        string  _anio, _ruta;       
        int idTracking;
        int idTrackingEditar;
        DataTable dtColumnasSeleccionadasExcel = new DataTable();
        string tipoviaje;       
        int _idTipoProg;

        public frmVerTracking()
        {
            InitializeComponent();
        }
        public void setearvariable(int var_IdProg, string codigo, string anio, string ruta)
        {
            _var_IdProg = var_IdProg;
            _codigo = codigo;
            _anio = anio;
            _ruta = ruta;
        }

        private void frmVerTracking_Load(object sender, EventArgs e)
        {
            ListarTracking();
        }

        void ListarTracking()
        {
            gridListar.DataSource = null;
            listarTracking.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetPreviajes_ListarTracking(_var_IdProg,_anio);

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    lblConductor.Text = dt.Rows[i]["Conductor"].ToString().ToString();
                    lblTracto.Text = dt.Rows[i]["TRACTO"].ToString().ToString();
                    lblCarreta.Text = dt.Rows[i]["Carreta"].ToString().ToString();
                    lblProgramacion.Text = dt.Rows[i]["IdOperacion"].ToString().ToString();
                    gridListar.DataSource = dt;
                    listarTracking.Columns["FechaRegistro"].DisplayFormat.FormatType = FormatType.DateTime;
                     listarTracking.Columns["FechaRegistro"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";                    
                    listarTracking.OptionsBehavior.Editable = false;
                    listarTracking.Columns["idTracking"].Visible = false;
                    listarTracking.Columns["UsuarioCrea"].Visible = false;
                    listarTracking.Columns["FechaCrea"].Visible = false;
                    listarTracking.Columns["idpunto"].Visible = false;
                    listarTracking.Columns["CodigoEvento"].Visible = false;
                    listarTracking.Columns["Conductor"].Visible = false;
                    listarTracking.Columns["TRACTO"].Visible = false;
                    listarTracking.Columns["Carreta"].Visible = false;
                    listarTracking.Columns["IdOperacion"].Visible = false;
                    label2.Text = "Numero de Programacion:" + _anio.Substring(2, 2) + _var_IdProg;
                }
            }
           /* else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
               // this.Close();
            }*/

        }
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            int[] filass = listarTracking.GetSelectedRows();
            string datoseleccionado = listarTracking.GetFocusedValue().ToString();

            for (int i = 0; i < filass.Length; i++)
            {
                idTracking = Convert.ToInt32(listarTracking.GetRowCellValue(filass[i], "idTracking").ToString());
            }
            if (MessageBox.Show("Esta seguro de eliminar el tracking?", "Eliminar tracking", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string rpta;
                DataTable dt = new DataTable();
                dt = clsOperacionesBL.Instancia.GetPreviajes_RegistrarTracking(3, Utilitario.Instancia.SesionUsuario.usuario, _var_IdProg, _idTipoProg, DateTime.Now.ToString(), "0", "0", "0",
                                                                                       "0", "0", idTracking, "0", "0", 0,0,"",0,"0");
                rpta = Convert.ToString(dt.Rows[0]["exito"]);
                string NrRPTA = rpta.Substring(0, 1);
                if (NrRPTA == "0")
                {
                    MessageBox.Show(rpta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarTracking();
                }
                else
                {
                    MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btmModificar_Click(object sender, EventArgs e)
        {
            if (listarTracking.DataSource == null)
            {
                MessageBox.Show("No hay datos para modificar...", "Alerta");
            }
            else
            {
                int[] filass = listarTracking.GetSelectedRows();
                string datoseleccionado = listarTracking.GetFocusedValue().ToString();

                for (int i = 0; i < filass.Length; i++)
                {
                    idTrackingEditar = Convert.ToInt32(listarTracking.GetRowCellValue(filass[i], "idTracking").ToString());
                    tipoviaje = listarTracking.GetRowCellValue(filass[i], "TipoViaje").ToString();
                }

                ProgramacionViajes.frmRegistrarTracking frmEditarRegistroTracking = new ProgramacionViajes.frmRegistrarTracking();
                frmEditarRegistroTracking.Text = "Editar Tracking: " + idTrackingEditar;
                frmEditarRegistroTracking.idTracking = idTrackingEditar;
                frmEditarRegistroTracking.TipoViaje = tipoviaje;
                frmEditarRegistroTracking.nuevo_modifica = 2;
                frmEditarRegistroTracking.ShowDialog(this);
                ListarTracking();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            //////////SE EXPORTARA TODO LOS TRACKING DE UN VIAJE A UN EXCEL ////////////
            if (gridListar.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Tracking" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                gridListar.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProgramacionViajes.frmRegistrarTracking frmAgregarTracking = new ProgramacionViajes.frmRegistrarTracking();
            frmAgregarTracking.Text = "Agregar Tracking: ";
            frmAgregarTracking._var_IdProg = _var_IdProg;
            frmAgregarTracking.ShowDialog(this);
            ListarTracking();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
