using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using Entidades;
using Negocio;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{  
    public partial class frmRegistroTiposDocumentos : Form
    {

        public DataTable DtTipoRelacion;

        public int _IdTipoDocumento;
        public string _Nemonico;
        public string _Descripcion;
        public string _TipoRelacion;
        public int _Dias;
        public string NrRPTA;

        bool permitir = true;

        public frmRegistroTiposDocumentos()
        {
            InitializeComponent();
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRegistroDocumentos_Load(object sender, EventArgs e)
        {
            CargarControles();
            txtDescripcion.Text = "";
            txtNemonico.Text = "";
            txtDias.Text = "0";

            if (_IdTipoDocumento > 0)
            {
                cargarDatosDocumento();
            }
            
        }

        public void cargarDatosDocumento()
        {

            cbxTipoRelacion.SelectedValue = _TipoRelacion;
            txtDescripcion.Text = _Descripcion;
            txtNemonico.Text = _Nemonico;
            txtDias.Text = Convert.ToString(_Dias);

        }


        public void CargarControles()
        {

            //Llenado de Tipo de Relacion
            cbxTipoRelacion.DataSource = DtTipoRelacion;
            cbxTipoRelacion.DisplayMember = "Descripcion";
            cbxTipoRelacion.ValueMember = "Codigo";
            cbxTipoRelacion.SelectedIndex = 0;

        }


        private void tsBtnGuardar_Click(object sender, EventArgs e)
        {

            if(txtNemonico.Text.Length == 0){
                MessageBox.Show("Debe ingresar Nemonico", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNemonico.Focus();
                return;
            }

            if (txtDescripcion.Text.Length <= 0)
            {
                MessageBox.Show("Debe ingresar descripción de Tipo Documento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDescripcion.Focus();
                return;
            }

            string tipoRelacion = cbxTipoRelacion.SelectedValue.ToString();
            string Descripcion = txtDescripcion.Text;
            string Nemonico = txtNemonico.Text;     
            string usuarioCrea = Utilitario.Instancia.SesionUsuario.usuario;
            int dias = Convert.ToInt32(txtDias.Text);

            string Rpta;
            DataTable dtRpta;

            if (_IdTipoDocumento <= 0)
            {
                dtRpta = clsControlDocumentosBL.Instancia.getDocumento_TipoDocumento_Registrar(tipoRelacion, Nemonico, Descripcion, dias, usuarioCrea);
            }
            else
            {
                dtRpta = clsControlDocumentosBL.Instancia.getDocumento_TipoDocumento_Actualizar(_IdTipoDocumento, tipoRelacion, Nemonico, Descripcion, dias, usuarioCrea);
            }
            
            
            Rpta = Convert.ToString(dtRpta.Rows[0]["exito"]);
            NrRPTA = Rpta.Substring(0, 1);

            if (NrRPTA == "0")
            {
                this.Close();
                MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            else
            {
                MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }


        public static void ValidarTextbox(KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)

                e.Handled = false;

            else

                e.Handled = true;
        }


        public bool solonumeros(int code)
        {
            bool resultado;

            if (code == 46 && txtDias.Text.Contains(".") && txtDias.Text.Contains("."))//se evalua si es punto y si es punto se revisa si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;

        }

        private void cbxTipoRelacion_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtDescripcion.Text = "";
            txtNemonico.Text = "";

        }

        private void cbxTipoRelacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtDias.Focus();
            } 
        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                tsBtnGuardar.PerformClick();
            } 
        }

        private void txtNemonico_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtDescripcion.Focus();
            } 
        }

        private void txtDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Escape)
            {
                txtNemonico.Focus();
            }
            else
            {
                ValidarTextbox(e);
                e.Handled = solonumeros(Convert.ToInt32(e.KeyChar));
            }
        }


    }
}
