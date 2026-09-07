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
    public partial class frnPermisos_Especiales_Master : Form
    {
        public string _REPORTE;
        int _idReporte, idPermisoEspecial, opcion = 1;
        bool Estado = true;
        DataTable dtPermisoEspeciale = new DataTable();
        public frnPermisos_Especiales_Master()
        {
            InitializeComponent();

        }
        private void frnPermisos_Especiales_Master_Load(object sender, EventArgs e)
        {
            CargarDatos();
            txtFormulario.Text = _REPORTE;
            if (txtNombreDePermiso.Text.Length == 0)
            {
                btnEditar.Enabled = false;
            }

         
        }
        private void CargarDatos()
        {
            
            dtPermisoEspeciale = clsUsuarioBL.Instancia.ReportesApp_Master_ListarPermisosEspeciales(_idReporte);
            if (dtPermisoEspeciale.Rows.Count > 0)
            {
                dataGridView1.DataSource = dtPermisoEspeciale;
                dataGridView1.Columns["UsuarioModifica"].Visible = false;
                dataGridView1.Columns["FechaModifica"].Visible = false;
                dataGridView1.Columns["idReporte"].Visible = false;
                dataGridView1.Columns["idPermisoEspecial"].Visible = false;
            }
            else
            {
                MessageBox.Show("No hay Accesos");
            }
        }
        public void setearvariable(string REPORTE, int idReporte)
        {
            _REPORTE = REPORTE;
            _idReporte = idReporte;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombreDePermiso.Text))
            {
                MessageBox.Show("debe completar el campo nombre Permiso");
                return;
            }

                try
            {
                string rpta;
                DataTable dt = new DataTable();
                dt = clsUsuarioBL.Instancia.RegistrarPermisosEspeciales(_idReporte, Estado, opcion, idPermisoEspecial, txtNombreDePermiso.Text, Utilitario.Instancia.SesionUsuario.usuario, false);
                rpta = Convert.ToString(dt.Rows[0]["exito"]);
                string NrRPTA = rpta.Substring(0, 1);
                if (NrRPTA == "0")
                {
                    MessageBox.Show(rpta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDatos();
                    if (txtNombreDePermiso.Text == " ")
                    {
                        txtNombreDePermiso.Clear();
                    }
                    else
                    {
                        txtNombreDePermiso.Clear();
                    }
                }
                else
                {
                    MessageBox.Show(rpta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                CargarDatos();
            }
        }
        private void btnEditar_Click(object sender, EventArgs e)
        {
            
            DataTable dteditar = new DataTable();
            string Respuesta;
            dteditar = clsUsuarioBL.Instancia.RegistrarPermisosEspeciales(_idReporte, Convert.ToBoolean(dataGridView1.CurrentRow.Cells["Estado"].Value), 2, idPermisoEspecial, txtNombreDePermiso.Text, Utilitario.Instancia.SesionUsuario.usuario, false);
            Respuesta = Convert.ToString(dteditar.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                if (txtNombreDePermiso.Text == " ")
                {
                    txtNombreDePermiso.Clear();
                }
                else
                {
                    txtNombreDePermiso.Clear();
                }
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.RowCount > 0)
                {
                    idPermisoEspecial = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["idPermisoEspecial"].Value.ToString());
                    string NombreDePermiso = dataGridView1.Rows[e.RowIndex].Cells["NombrePermiso"].Value.ToString();

                    bool Estado = dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    txtNombreDePermiso.Text = NombreDePermiso;
                    

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Text.Length == 0)
            {
                btnEditar.Enabled = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dataGridView1.Columns["Estado"].Index)
                {
                    if (Convert.ToBoolean(dtPermisoEspeciale.Rows[e.RowIndex]["Estado"]) == false)
                    {
                        dtPermisoEspeciale.Rows[e.RowIndex]["Estado"]= true;
  
                    }
                    else
                    {
                        dtPermisoEspeciale.Rows[e.RowIndex]["Estado"] = false;

                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                
        }
    }
}

