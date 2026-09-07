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
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing.Imaging;
using System.IO;

namespace ReportesTranspesa.Formularios.Areas.Logistica
{
    public partial class frmListarReclamosClientes : Form
    {
        public int idReclamo = 0;
        public DataTable dt = null;
        public byte[] byteArrayPDF = null;
        public frmListarReclamosClientes()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                NuevoReclamoCliente nuevoCliente = new NuevoReclamoCliente();
                if (nuevoCliente.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ListarReclamo();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmListarReclamosClientes_Load(object sender, EventArgs e)
        {
            try
            {
                ListarPermisosReclamo();
                ListarReclamo();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ListarPermisosReclamo()
        {
            clsDetalleReporteUsuarioBL.Instancia.consulta_DetalleReporte_Por_Usuario_PermisosFormlario(Utilitario.Instancia.SesionUsuario.usuario); //Trae los permisos del usuario
            DataTable dtPermisosEspeciales = null;
            DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListarReclamosClientes");

            registrarDescargoToolStripMenuItem.Enabled = false;
            subsanarToolStripMenuItem.Enabled = false;
            noSolucionadoToolStripMenuItem.Enabled = false;

            if (dtPermisos.Rows.Count > 0)
            {


                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                {
                    dtPermisosEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());
                }

                if (dtPermisosEspeciales != null)
                {
                    if (dtPermisosEspeciales.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtPermisosEspeciales.Rows.Count; i++)
                        {
                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "RegistrarDescargo")
                            {
                                registrarDescargoToolStripMenuItem.Enabled = true;
                            }


                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "RegistrarSolucion")
                            {
                                subsanarToolStripMenuItem.Enabled = true;
                              
                            }


                            if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "noSolucion")
                            {
                                noSolucionadoToolStripMenuItem.Enabled = true;

                            }
                        }
                
                    }
                }
            }
        }
        public void ListarReclamo()
        {
            dt = clsLogisticaBL.Instancia.ReportesApp_Logistica_ListarReclamos(dtpFechaInicio.Text, dtpFechaFin.Text);
            if (dt.Rows.Count > 0)
            {
                dtgLista.DataSource = dt;
                dgvListaVista.Columns["idCliente"].Visible = false;
                dgvListaVista.Columns["idReclamo"].Visible = false;
                dgvListaVista.Columns["NombreImagen"].Visible = false;
                dgvListaVista.Columns["NombrePDF"].Visible = false;
                dgvListaVista.Columns["ArchivoPDF"].Visible = false;
                dgvListaVista.BestFitColumns();
                dgvListaVista.Columns["Imagen"].Width = 80;

            }
            else
            {
                dtgLista.DataSource = null;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ListarReclamo();
        }

        private void registrarDescargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                idReclamo = Convert.ToInt32(dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "idReclamo"));
                p_Descargo.Visible = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                p_Descargo.Visible = false;
                txtDescargo.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnGuardarDescargo_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDescargo.Text.Length == 0)
                {
                    MessageBox.Show("Debe ingresar detalle del descargo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (clsLogisticaBL.Instancia.ReportesApp_Logistica_RegistrarDesargoCliente(dtpFechaProyectado.Text, txtDescargo.Text, idReclamo,txtResponsable.Text,byteArrayPDF))
                {
                    ListarReclamo();
                    p_Descargo.Visible = false;
                    txtDescargo.Text = "";
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void subsanarToolStripMenuItem_Click(object sender, EventArgs e)
        {


            idReclamo = Convert.ToInt32(dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "idReclamo"));
            panel1.Visible = true;
            txtSolucion.Text = "";




        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (clsLogisticaBL.Instancia.ReportesApp_Logistica_RegistrarSolucionCliente(txtSolucion.Text, idReclamo))
                {

                    panel1.Visible = false;
                    txtSolucion.Text = "";
                    ListarReclamo();

                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void noSolucionadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Estado").ToString() == "SOLUCIONADO")
                {
                    MessageBox.Show("RECLAMO YA SE ENCUENTRA SOLUCIONADO", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                String Respuesta = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Motivo", "Motivo Reversion");

                if (Respuesta.Length > 0)
                {
                    if (clsLogisticaBL.Instancia.ReportesApp_Logistica_RegistrarNoSolucionCliente(Respuesta, Convert.ToInt32(dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "idReclamo"))))
                    {



                        ListarReclamo();
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListaVista_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {

        }

        private void dgvListaVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

        }

        private void dtgLista_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                NuevoReclamoCliente nc = new NuevoReclamoCliente();
                nc.txtCliente.Tag = Convert.ToInt32(dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "idCliente"));
                nc.txtCliente.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Cliente").ToString();
                nc.txtContacto.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Contacto").ToString();
                nc.txtCorreo.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Correo").ToString();
                nc.txtReclamo.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Reclamo").ToString();
                nc.txtDescargo.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Descargo").ToString();
                nc.txtDetalleReclamo.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "DetalleReclamo").ToString();
                nc.txtTelefono.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Telefono").ToString();
                nc.txtObservacion.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Observacion").ToString();
                nc.dtpFechaInicidente.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "fechaIncidente").ToString();
                nc.txtFechaProyectada.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "FProyectado").ToString();
                nc.txtFechaSolucion.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "FechaSolucion").ToString();
                nc.txtSolucion.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Solucion").ToString();
                nc.txtNoSolucion.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "NoSolucion").ToString();
                nc.btnSubirImagen.Enabled = false;
                nc.btnGuardar.Enabled = false;
                nc.operacion = "VER";
                if (dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "PDF").ToString() == "SI")
                {
                    Byte[] data = new Byte[0];
                    nc.txtNombrePDF.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "NombrePDF").ToString();
                    data = (Byte[])dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "ArchivoPDF");

                    MemoryStream mem = new MemoryStream(data);
                    nc.stream = mem;
                }
                if (dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Imagen").ToString().Length > 0)
                {
                    Byte[] data = new Byte[0];
                    data = (Byte[])dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "Imagen");
                    nc.txtNombreImagen.Text = dgvListaVista.GetRowCellValue(dgvListaVista.FocusedRowHandle, "NombreImagen").ToString();
                    MemoryStream mem = new MemoryStream(data);
                    nc.imagen.Image = Image.FromStream(mem);
                    nc.imagen.SizeMode = PictureBoxSizeMode.StretchImage;

                }
                nc.Show();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListaVista_CustomDrawCell_1(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "Estado")
            {
                if (e.CellValue.ToString() == "NO SOLUCIONADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 155, 155);
                }

            }

            if (e.Column.FieldName == "Estado")
            {
                if (e.CellValue.ToString() == "EN PROCESO")
                {
                    e.Appearance.BackColor = Color.FromArgb(129, 199, 132);
                }

            }

            if (e.Column.FieldName == "Estado")
            {
                if (e.CellValue.ToString() == "SOLUCIONADO")
                {
                    e.Appearance.BackColor = Color.FromArgb(77, 208, 225);
                }

            }

            if (e.Column.FieldName == "Estado")
            {
                if (e.CellValue.ToString() == "PENDIENTE")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 183, 77);
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            txtSolucion.Text = "";
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreImagen = "";
               
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
            catch (Exception ex)
            {
                
                 MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
