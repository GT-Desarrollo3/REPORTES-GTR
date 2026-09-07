using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using Negocio;
namespace ReportesTranspesa.Sistema
{
    public partial class frmPermisosEspeciales : Form
    {
        public string _EMPLEADO, _USUARIO, _REPORTE, _NombreDePermiso, _NombreDePermisos;
        public int _idReporte;
        public int _idPermisoEspecial;
        public bool _Estado, habilitar;
        public string rpta;
        int opcion = 1;
        public frmPermisosEspeciales()
        {
            InitializeComponent();
        }
        private void frmPermisosEspeciales_Load(object sender, EventArgs e)
        {
             CargarDatos();
            CargarDatos2();
            txtUsuario.Text = _EMPLEADO;
            txtFormulario.Text = _REPORTE;
        }
        private void CargarDatos()
        {
            try
            {
                DataTable dtPermisoEspeciales = new DataTable();
                dtPermisoEspeciales = clsUsuarioBL.Instancia.ReportesApp_Master_ListarPermisosEspeciales(_idReporte);
                if (dtPermisoEspeciales.Rows.Count > 0)
                {
                    dtPermisosEsp.DataSource = dtPermisoEspeciales;
                    dtPermisosEsp.Columns["UsuarioModifica"].Visible = false;
                    dtPermisosEsp.Columns["idPermisoEspecial"].Visible = false;
                    dtPermisosEsp.Columns["FechaModifica"].Visible = false;
                    dtPermisosEsp.Columns["idReporte"].Visible = false;
                    dtPermisosEsp.Columns["Estado"].Visible = false;
                }
                else
                {
                    MessageBox.Show("En este Reporte no hay permisos especiales , ingresalos aqui", "Mensaje Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarDatos2()
        {
            try
            {
                DataTable dtListarPermisos = new DataTable();
                dtListarPermisos = clsUsuarioBL.Instancia.GetUsuariosActivos();
                dtListarPermisos = clsUsuarioBL.Instancia.ReportesApp_ListarPermisosEspeciales(_USUARIO, _idReporte);
                if (dtListarPermisos != null)
                {
                    dtgvLsitarPermisos.DataSource = dtListarPermisos;
                    dtgvLsitarPermisos.Columns["idUsuario"].Visible = false;
                    dtgvLsitarPermisos.Columns["idPermisoEspecial"].Visible = false;
                    dtgvLsitarPermisos.Columns["idReporte"].Visible = false;
                    dtgvLsitarPermisos.Columns["Estado"].Visible = false;

                }
                else
                {
                    //MessageBox.Show("En este Reporte no hay permisos especiales , ingresalos aqui", "Mensaje Informativo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception ex)
            {

               MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void setearvariable(string USUARIO, string EMPLEADO, string REPORTE, int idReporte, int idPermisoEspecial)
        {
            _EMPLEADO = EMPLEADO;
            _USUARIO = USUARIO;
            _REPORTE = REPORTE;
            _idReporte = idReporte;
            _idPermisoEspecial = idPermisoEspecial;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
           
           

            int contExito = 0;
            string NrRPTA;
            for (int i = 0; i < dtPermisosEsp.Rows.Count; i++)
            {
                _NombreDePermiso = dtPermisosEsp.Rows[i].Cells["NombrePermiso"].Value.ToString();
                _idPermisoEspecial = Convert.ToInt32(dtPermisosEsp.Rows[i].Cells["idPermisoEspecial"].Value.ToString());
                bool permisoespe = Convert.ToBoolean(dtPermisosEsp.Rows[i].Cells["CHECK"].Value);

                if (Convert.ToBoolean(dtPermisosEsp.Rows[i].Cells["CHECK"].Value) == true)
                {
                    DataTable dt = new DataTable();
                    dt = clsUsuarioBL.Instancia.Registrar_PermisosEspecialesXUsuario(_idReporte, opcion, _NombreDePermiso, _idPermisoEspecial, _USUARIO, true, Utilitario.Instancia.SesionUsuario.usuario);
                    rpta = Convert.ToString(dt.Rows[0]["exito"]);
                    NrRPTA = rpta.Substring(0, 1);
                    if (NrRPTA == "0")
                    {
                        CargarDatos2();
                        contExito = contExito + 1;
                    }
                    else
                    {
                       
                        MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            if (contExito > 0)
            {

                MessageBox.Show(rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                CargarDatos2();
            }

        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            
            
            string NrRPTA;
            for (int i = 0; i < dtgvLsitarPermisos.Rows.Count; i++)
            {
                _NombreDePermiso = dtgvLsitarPermisos.Rows[i].Cells["NombrePermiso"].Value.ToString();
                _idPermisoEspecial = Convert.ToInt32(dtgvLsitarPermisos.Rows[i].Cells["idPermisoEspecial"].Value.ToString());
                bool permisoespe = Convert.ToBoolean(dtgvLsitarPermisos.Rows[i].Cells["Check2"].Value);
                if (Convert.ToBoolean(dtgvLsitarPermisos.Rows[i].Cells["Check2"].Value) == true)
                {
                    DataTable dt = new DataTable();
                    dt = clsUsuarioBL.Instancia.Registrar_PermisosEspecialesXUsuario(_idReporte, 2, _NombreDePermiso, _idPermisoEspecial, _USUARIO, false, Utilitario.Instancia.SesionUsuario.usuario);
                    rpta = Convert.ToString(dt.Rows[0]["exito"]);
                    NrRPTA = rpta.Substring(0, 1);
                    if (NrRPTA == "0")
                    {
                        CargarDatos();
                        CargarDatos2();
                        MessageBox.Show("exito", "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }

        private void frmPermisosEspeciales_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
