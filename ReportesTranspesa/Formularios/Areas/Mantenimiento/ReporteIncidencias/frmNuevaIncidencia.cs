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
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias
{
    public partial class frmNuevaIncidencia : Form
    {
        public frmListaIncidencias formulario;
        public int idIncidenteC;
        public byte[] byteArrayImagen = null, byteArrayFalla = null;
        public string TipoFalla;

        public frmNuevaIncidencia()
        {
            InitializeComponent();
            cbxTipoAuxilio.SelectedIndexChanged -= cbxTipoAuxilio_SelectedIndexChanged;
        }

        private void cbxTipoAuxilio_SelectedIndexChanged(object sender, EventArgs e) { CargarComboAuxilio(); }

        private void frmNuevaIncidencia_Load(object sender, EventArgs e) { }


        public void CargarComboAuxilio()
        {
            DataTable dtTipoAuxilio = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarAuxilio();
            cbxTipoAuxilio.DataSource = dtTipoAuxilio;
            cbxTipoAuxilio.DisplayMember = "Descripcion";
            cbxTipoAuxilio.ValueMember = "idTipoAuxilio";
        }

        public void rbFallaParada_Click(object sender, EventArgs e) { TipoFalla = "PARADA"; }

        public void rbLeve_Click(object sender, EventArgs e) { TipoFalla = "LEVE"; }

        public void cbxTipoDanio_DropDownClosed(object sender, EventArgs e)
        {
            /*
            if (cbxTipoDanio.Text == "NINGUNO") { txtDanio.Text = "NINGUNO"; }
            else { txtDanio.Clear(); }
            */ 
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

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            byteArrayImagen = null;
            pbImagen.Image = null;
            pbImagen.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnBuscarFalla_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";

                if (getImage.ShowDialog() == DialogResult.OK)
                {
                    byteArrayFalla = File.ReadAllBytes(getImage.FileName);

                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteArrayFalla));
                    pbFalla.Image = x;
                    pbFalla.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            byteArrayFalla = null;
            pbFalla.Image = null;
            pbFalla.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (txtMotivo.Text.Length == 0 || txtUbicacion.Text.Length == 0 || txtDIncidente.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtMotivo.Text.Length == 0) { txtMotivo.Focus(); }
                else
                {
                    if (txtUbicacion.Text.Length == 0) { txtUbicacion.Focus(); }
                    else { txtDIncidente.Focus(); }
                }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RegistroIncidencias_ActualizarIncidencias(idIncidenteC, Convert.ToInt32(cbxTipoAuxilio.SelectedValue), txtEstadoUnidad.Text,
                              TipoFalla, Convert.ToDateTime(dtpFechaInicio.Value), Convert.ToDateTime(dtpHoraInicio.Value), txtUbicacion.Text, txtMotivo.Text, txtDIncidente.Text, cbxTipoDanio.Text, txtDanio.Text,
                              txtGPS.Text, txtRecursos.Text, txtObservacion.Text, byteArrayImagen, byteArrayFalla, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formulario.ListarIncidencias();
                    Close();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMotivo.Focus();
                }
            }
        }
    }
}
