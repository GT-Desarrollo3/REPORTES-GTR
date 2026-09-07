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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
   
    public partial class frmListarCompromisosMemorandums : Form
    {
        int Opcion = 1, IDPESONA;
        string UsuarioModulo;
        public frmListarCompromisosMemorandums()
        {
            InitializeComponent();
        }

        public void DatoOpcion(int _opcion)
        {
            if (_opcion == 3) 
            {
                Opcion = 3;
            }
           
        }

        private void frmListarCompromisosMemorandums_Load(object sender, EventArgs e)
        {
           
            ConsultaUsuarioSpringxWindows();
            if (UsuarioModulo == null)
            {
                UsuarioModulo = Utilitario.Instancia.SesionUsuario.usuario;
            }

            buscarDatos();

        }

        void buscarDatos()
        {

            string fechin = "01/01/1980";
            string fechfin = "31/12/2030";
            fechin = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechfin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";


            if (dtpFechaIni.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();

                return;
            }          

            DataTable dtListarMemos = new DataTable();
            dtListarMemos = clsRecursosHumanosBL.Instancia.GetListarMemosCompromisos(UsuarioModulo, Opcion, fechin, fechfin, IDPESONA);
            if (dtListarMemos != null && dtListarMemos.Rows.Count > 0)
            {
                for (int i = 0; i < dtListarMemos.Rows.Count; i++)
                {
                    dgvListaMemos.DataSource = dtListarMemos;
                    gvListaMemos.OptionsBehavior.Editable = false;
                }
            }
            else
            {
                dgvListaMemos.DataSource = null;
                MessageBox.Show("No se encontraron registros.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        void ConsultaUsuarioSpringxWindows()
        {
            DataTable dtUserWind = new DataTable();
            dtUserWind = clsUsuarioBL.Instancia.GetListaUsuariosModulo(Utilitario.Instancia.SesionUsuario.usuario);
            for (int i = 0; i < dtUserWind.Rows.Count; i++)
            {
                UsuarioModulo = dtUserWind.Rows[i]["Usuario"].ToString();
                //  MessageBox.Show("tu Usuario spring es "+UsuarioModulo);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvListaMemos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Memorandum" +" " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dgvListaMemos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                if (Opcion == 3)
                {
                    clsVisuales.Instancia.LlenarLw(lvConductor, clsConsultaBL.Instancia.GetConductores(txtConductor.Text), true, false, false);

                    lvConductor.Columns[0].Width = 0;
                    lvConductor.Columns[1].Width = 206;
                    lvConductor.Columns[2].Width = 110;

                    lvConductor.Size = new System.Drawing.Size(400, 200);

                    lvConductor.BringToFront();
                    lvConductor.Visible = true;
                    lvConductor.Focus();
                }
                else 
                {
                    clsVisuales.Instancia.LlenarLw(lvConductor, clsConsultaBL.Instancia.GetPersona(txtConductor.Text), true, false, false);

                    lvConductor.Columns[0].Width = 0;
                    lvConductor.Columns[1].Width = 206;
                    lvConductor.Columns[2].Width = 110;

                    lvConductor.Size = new System.Drawing.Size(400, 200);

                    lvConductor.BringToFront();
                    lvConductor.Visible = true;
                    lvConductor.Focus();
                }
                //ListaConductoresBloqueados();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvConductor.Visible = false;
                txtConductor.Focus();
            }
        }

        private void lvConductor_Enter(object sender, EventArgs e)
        {
            if (!lvConductor.Items.Count.Equals(0))
            {
                lvConductor.Items[0].Selected = true;
            }
        }

        private void lvConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvConductor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvConductor.SelectedItems[0];

                IDPESONA = Int32.Parse(ItemActual.Text);
                txtConductor.Text = ItemActual.SubItems[1].Text;
                lvConductor.Visible = false;
                txtConductor.Focus();

                buscarDatos();

            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvConductor.Visible = false;
                txtConductor.Focus();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Opcion == 3)
            {
                Opcion = 4;
            }
            else
            {
                Opcion = 2;
            }
            buscarDatos();
        }
    }
}
