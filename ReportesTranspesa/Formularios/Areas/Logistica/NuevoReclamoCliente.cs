using Negocio;
using ReportesTranspesa.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;
using ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class NuevoReclamoCliente : Form
    {
        public byte[] byteArrayImagen = null;
        public byte[] byteArrayPDF = null;
        public string nombreImagen = "";
        public string operacion  = "NUEVO";
        public Stream stream = new MemoryStream();
        public NuevoReclamoCliente()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtCorreo.Focus();
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (clsLogisticaBL.Instancia.ReportesApp_Logistica_RegistrarReclamoCliente(Convert.ToInt32(txtCliente.Tag), txtCliente.Text, txtCorreo.Text, txtTelefono.Text, txtReclamo.Text,txtContacto.Text, txtDetalleReclamo.Text, byteArrayImagen, nombreImagen, dtpFechaInicidente.Text,txtObservacion.Text,txtNombrePDF.Text,byteArrayPDF))
            {
                MessageBox.Show(Utilitario.Instancia.Advertencia, "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show(Utilitario.Instancia.Advertencia, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSubirImagen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog getImage = new OpenFileDialog();

                getImage.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getImage.Filter = "Archivos de Imagen (*.jpg)(*.jpeg)|*.jpg;*.jpeg|PNG(*.png)|*.png";
          
                if (getImage.ShowDialog() == DialogResult.OK) {
                    nombreImagen = getImage.SafeFileName;
                    txtNombreImagen.Text = nombreImagen;
                    byteArrayImagen = File.ReadAllBytes(getImage.FileName); 
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvCliente, clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarClientesReclamos(txtCliente.Text), true, false, false);
                lvCliente.Columns[0].Width = 0;
                lvCliente.Columns[1].Width = 206;
                lvCliente.Columns[2].Width = 110;

                lvCliente.BringToFront();
                lvCliente.Visible = true;
                lvCliente.Focus();

                if (e.KeyChar == (char)Keys.Back)
                {
                    lvCliente.Visible = false;
                    lvCliente.SendToBack();
                    txtCliente.Tag = -1;
                }
            }


            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
            }

        }

        private void lvCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            ListViewItem ItemActual;
            // int idConductor;
            ItemActual = lvCliente.SelectedItems[0];
            txtCliente.Text = ItemActual.SubItems[1].Text;
            txtCliente.Tag = Convert.ToInt32(ItemActual.Text);
            txtCorreo.Text = ItemActual.SubItems[3].Text;
            txtTelefono.Text = ItemActual.SubItems[4].Text;

            lvCliente.Visible = false;
            txtContacto.Focus();
          
            
        }

        private void txtCorreo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtTelefono.Focus();
            }
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtReclamo.Focus();
            }
        }

        private void txtReclamo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                dtpFechaInicidente.Focus();
            }
        }

        private void txtObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                btnGuardar.Focus();
                btnGuardar.Select();
            }
        }

        private void dtpFechaInicidente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtDetalleReclamo.Focus();
            }
        }

        private void NuevoReclamoCliente_Load(object sender, EventArgs e)
        {
            if (operacion == "VER")
            {
                btnPDF.Text = "VER PDF";
                label2.Text = "VER RECLAMO";
            }
           
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (operacion == "VER")
            {

                if (stream != null)
                {
                    AbrirPdf open = new AbrirPdf();
                    open.pdfViewer1.LoadDocument(stream);
                    //saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    open.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No contiene pdf", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                }
           
            }
            else
            {
                OpenFileDialog getPDF = new OpenFileDialog();

                getPDF.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                getPDF.Filter = "Archivos de Imagen (*.pdf)(*.pdf)|*.pdf;*.pdf|pdf(*.pdf)|*.pdf";

                if (getPDF.ShowDialog() == DialogResult.OK)
                {
                    nombreImagen = getPDF.SafeFileName;
                    txtNombrePDF.Text = nombreImagen;
                    byteArrayPDF = File.ReadAllBytes(getPDF.FileName);
                }
               
            }
           
            
        }
    }
}
