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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using System.Drawing.Imaging;
using System.IO;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias;

namespace ReportesTranspesa.Formularios.Areas.Seguridad.RegistroAccidentes
{
    public partial class frmResumenEstado : Form
    {
        public int idRegistroInc;
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        DataTable dtListaEstados = new DataTable();
        public byte[] byteArrayImagen = null;
        public frmListaIncidentesSSOMAC frmListaIncidentesSSOMAC = new frmListaIncidentesSSOMAC();
        int e1 = 0, e2 = 0, e3 = 0;

        public frmResumenEstado()
        {
            InitializeComponent();
        }

        private void frmResumenEstado_Shown(object sender, EventArgs e)
        {
            txtEstadoSSOMAC.Focus();
        }

        private void frmResumenEstado_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaIncidentesSSOMAC");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Costos")
                    {
                        groupBox5.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else { groupBox5.Enabled = false; }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Gerencia Operaciones")
                    {
                        groupBox6.Enabled = true;
                        i = 999; e2 = 1;
                    }
                    else { groupBox6.Enabled = false; }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "GTH")
                    {
                        groupBox7.Enabled = true;
                        btnCerrar.Enabled = true;
                        i = 999; e3 = 1;
                    }
                    else
                    {
                        groupBox7.Enabled = false;
                        btnCerrar.Enabled = false;
                    }
                }
            }
            else
            {
                groupBox5.Enabled = false;
                groupBox6.Enabled = false;
                groupBox7.Enabled = false;
                btnCerrar.Enabled = false;
            }

            ListarEstado();
        }


        public void ListarEstado()
        {
            dtListaEstados = clsMantenimientoBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ListarEstadoIncidencias(idRegistroInc);
            
            if (dtListaEstados.Rows.Count > 0)
            {
                cbxEstadoCosto.Text = dtListaEstados.Rows[0]["EstadoCostos"].ToString();
                txtObservacionCosto.Text = dtListaEstados.Rows[0]["ObservacionCostos"].ToString();
                cbxEstadoGer.Text = dtListaEstados.Rows[0]["ResponsableGEROP"].ToString();
                txtObservacionGer.Text = dtListaEstados.Rows[0]["ObservacionGEROP"].ToString();
                txtObservacionGTH.Text = dtListaEstados.Rows[0]["ObservacionGTH"].ToString();
                txtRutaLocal2.Text = dtListaEstados.Rows[0]["Descargo"].ToString();

                if (dtListaEstados.Rows[0]["ImagenGTH"].ToString() != "")
                {
                    Byte[] byteBLOBData;
                    byteBLOBData = (Byte[])dtListaEstados.Rows[0]["ImagenGTH"];
                    Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                    pbVale.Image = x;
                    pbVale.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dtEstado = new DataTable();
            string REstado;

            dtEstado = clsMantenimientoBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ActualizarEstadoIncidencias(1, idRegistroInc, cbxEstadoCosto.Text, txtObservacionCosto.Text,
                                                    byteArrayImagen, Utilitario.Instancia.SesionUsuario.usuario);
            REstado = Convert.ToString(dtEstado.Rows[0]["exito"]);
            string NroRPTA = REstado.Substring(0, 1);

            if (NroRPTA == "0")
            {
                MessageBox.Show(REstado, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListaIncidentesSSOMAC.ListarIncidencias();
                Close();
            }
            else
            {
                MessageBox.Show(REstado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxEstadoCosto.Text = "-";
                txtObservacionCosto.Clear();
            }
        }

        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            DataTable dtEstado = new DataTable();
            string REstado;

            dtEstado = clsMantenimientoBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ActualizarEstadoIncidencias(2, idRegistroInc, cbxEstadoGer.Text, txtObservacionGer.Text,
                                                    byteArrayImagen, Utilitario.Instancia.SesionUsuario.usuario);
            REstado = Convert.ToString(dtEstado.Rows[0]["exito"]);
            string NroRPTA = REstado.Substring(0, 1);

            if (NroRPTA == "0")
            {
                MessageBox.Show(REstado, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListaIncidentesSSOMAC.ListarIncidencias();
                Close();
            }
            else
            {
                MessageBox.Show(REstado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbxEstadoGer.Text = "-";
                txtObservacionGer.Clear();
            }
        }

        private void btnGuardar3_Click(object sender, EventArgs e)
        {
            DataTable dtEstado = new DataTable();
            string REstado;

            dtEstado = clsMantenimientoBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_ActualizarEstadoIncidencias(3, idRegistroInc, "", txtObservacionGTH.Text,
                                                    byteArrayImagen, Utilitario.Instancia.SesionUsuario.usuario);
            REstado = Convert.ToString(dtEstado.Rows[0]["exito"]);
            string NroRPTA = REstado.Substring(0, 1);

            if (NroRPTA == "0")
            {
                MessageBox.Show(REstado, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmListaIncidentesSSOMAC.ListarIncidencias();
                Close();
            }
            else
            {
                MessageBox.Show(REstado, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCerrar_Click(sender, e);
                txtObservacionGTH.Clear();
            }
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
                    pbVale.Image = x;
                    pbVale.SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            byteArrayImagen = null;
            pbVale.Image = null;
            pbVale.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevoEnlace;
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        nuevoEnlace = op.FileName.Replace(" ", "%20");
                        txtRutaLocal2.Clear();
                        txtRutaLocal2.Text = "file:///" + nuevoEnlace;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarLocal_Click(object sender, EventArgs e) { txtRutaLocal2.Clear(); }

        private void txtRutaLocal2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try { System.Diagnostics.Process.Start(txtRutaLocal2.Text); }
            catch { }
        }

        private void btnDescargo_Click(object sender, EventArgs e)
        {
            if (txtRutaLocal2.Text.Length != 0)
            {
                if (txtRutaLocal2.Text.Contains("DESCARGO") || txtRutaLocal2.Text.Contains("Descargo") || txtRutaLocal2.Text.Contains("descargo"))
                {
                    DataTable dtAgregar = new DataTable();
                    string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtAgregar = clsMantenimientoBL.Instancia.ReportesApp_Seguridad_RegistroIncidencias_DesbloquearDescargo(idRegistroInc, txtRutaLocal2.Text, Usuario);
                    respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    
                    if (NroRspta == "0") { MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show("El documento adjunto no es un descargo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            else { MessageBox.Show("Por favor, adjunte el archivo de descargo del documento.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
    }
}
