using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmRegistroFaltantesMercaderia : Form
    {
        int _NroProgramacion;
        string _Usuario;

        public frmRegistroFaltantesMercaderia()
        {
            InitializeComponent();
            cbxMotivo.SelectedIndexChanged -= cbxMotivo_SelectedIndexChanged;
        }

        private void cbxMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboMotivo();
        }

        private void frmRegistroFaltantesMercaderia_Shown(object sender, EventArgs e)
        {
            cbxMotivo.Focus();
        }

        private void frmRegistroFaltantesMercaderia_Load(object sender, EventArgs e)
        {
            CargarComboMotivo();
            ListarRegistro();
        }


        public void EnviarDatos(int NroProgramacion, string Usuario)
        {
            _NroProgramacion = NroProgramacion;
            _Usuario = Usuario;
        }

        private void CargarComboMotivo()
        {
            DataTable dtMotivo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarMotivo();
            cbxMotivo.DataSource = dtMotivo;
            cbxMotivo.DisplayMember = "Descripcion";
            cbxMotivo.ValueMember = "idMotivo";
        }

        private void InsertarRegistroFaltante()
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_Insertar(_NroProgramacion, _Usuario, Convert.ToInt32(cbxMotivo.SelectedValue));
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListarRegistro()
        {
            DataTable dtListaRegistros = new DataTable();
            dtListaRegistros = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_Faltantes_ListarRegistro(_NroProgramacion);

            if (dtListaRegistros.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaRegistros.Rows.Count; i++)
                {
                    txtFechaViaje.Text = dtListaRegistros.Rows[i]["FECHA_VIAJE"].ToString();
                    txtTracto.Text = dtListaRegistros.Rows[i]["TRACTO"].ToString();
                    txtSemirremolque.Text = dtListaRegistros.Rows[i]["SEMIRREMOLQUE"].ToString();
                    txtConductor.Text = dtListaRegistros.Rows[i]["CONDUCTOR"].ToString();
                    txtRuta.Text = dtListaRegistros.Rows[i]["RUTA"].ToString();
                    txtCliente.Text = dtListaRegistros.Rows[i]["CLIENTE"].ToString();
                    txtCodigoViaje.Text = dtListaRegistros.Rows[i]["CODIGO_VIAJE"].ToString();
                    txtFechaDescarga.Text = dtListaRegistros.Rows[i]["FECHA_DESCARGA"].ToString();
                    txtGT.Text = dtListaRegistros.Rows[i]["GT"].ToString();
                    txtGR.Text = dtListaRegistros.Rows[i]["GR"].ToString();
                }
            }

            lblNroTicket.Text = _NroProgramacion.ToString();
        }

        
        private void btnAgregarPreviaje_Click(object sender, EventArgs e)
        {
            InsertarRegistroFaltante();
        }


        private void cbxMotivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                InsertarRegistroFaltante();
            }
        }
    }
}
