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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.HojaRecorrido
{
    public partial class frmMaestroFirmas : Form
    {       
        string personaNobre;
        int idpersona;
        public frmMaestroFirmas()
        {
            InitializeComponent();
        }
        
       
        private void btnCargarFirma_Click(object sender, EventArgs e)
        {           
                OpenFileDialog dialog = new OpenFileDialog();

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.pictureBox1.Image = Image.FromFile(dialog.FileName);                       
                }
                else
                {
                    this.pictureBox1.Image = null;
                    return;
                }            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dtFirmar = new DataTable();
            if (MessageBox.Show("Desea guardar la Firma...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                System.IO.MemoryStream ms1 = new System.IO.MemoryStream();
                this.pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imagen1 = ms1.GetBuffer();

                dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_RegistroFirma(1, idpersona, imagen1, Utilitario.Instancia.SesionUsuario.usuario);
                string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                string NrRPTA = Rpta.Substring(0, 1);

                if (NrRPTA == "0")
                {
                    MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }

        private void frmMaestroFirmas_Load(object sender, EventArgs e)
        {
            //CONSULTAMOS EL EL IDDEL USUARIO PARA ACTIVAR LAS FIRMAS
            DataTable dtFirmarPersona = new DataTable();
            dtFirmarPersona = clsRecursosHumanosBL.Instancia.GetAreaPersonaFirmar(Utilitario.Instancia.SesionUsuario.usuario);
            if (dtFirmarPersona.Rows.Count > 0)
            {
                for (int i = 0; i < dtFirmarPersona.Rows.Count; i++)
                {
                    idpersona = Convert.ToInt32(dtFirmarPersona.Rows[i]["IDPERSONA"].ToString());
                }

                label3.Text = Utilitario.Instancia.SesionUsuario.usuario;
            }
        }
    }
}
