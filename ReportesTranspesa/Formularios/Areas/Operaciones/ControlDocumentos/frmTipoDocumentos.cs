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
using ReportesTranspesa.Sistema;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using System.Xml;
using System.IO;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmTipoDocumentos : Form
    {
        public DataTable DtTipoRelacion;
        public int EsVencimiento;

        public frmTipoDocumentos()
        {
            InitializeComponent();
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
            cbxOperacion2.SelectedIndexChanged -= cbxOperacion2_SelectedIndexChanged;
            cbxResponsable.SelectedIndexChanged -= cbxResponsable_SelectedIndexChanged;
            cbxDocumentos.SelectedIndexChanged -= cbxDocumentos_SelectedIndexChanged;
            cbxRelacion.SelectedIndexChanged -= cbxRelacion_SelectedIndexChanged;
        }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarOperaciones(); }

        private void cbxOperacion2_SelectedIndexChanged(object sender, EventArgs e) { CargarOperaciones2(); }

        private void cbxResponsable_SelectedIndexChanged(object sender, EventArgs e) { CargarResponsable(); }

        private void cbxDocumentos_SelectedIndexChanged(object sender, EventArgs e) { CargarDocumento(); }

        private void cbxRelacion_SelectedIndexChanged(object sender, EventArgs e) { CargarRelacion(); }

        private void frmTipoDocumentos_Load(object sender, EventArgs e)
        {
            CargarControles();
            cargarListaDocumentos();
            CargarOperaciones();
            CargarOperaciones2();
            CargarResponsable();

            cbxOperacion.Text = "LINDLEY";
            cbxOperacion_DropDownClosed(sender, e);

            cbxRelacion.Text = "CONDUCTOR";
            cbxRelacion_DropDownClosed(sender, e);

            cbxOperacion2.Text = "TODO";

            ListarVencimientos();
        }


        public void ListarVencimientos()
        {
            DataTable dtVencimientos = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarVencimientos(cbxOperacion2.Text);
            dgvVencimientos.DataSource = dtVencimientos;
            if (dtVencimientos.Rows.Count > 0)
            {
                dgvVencimientosVista.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvVencimientosVista.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvVencimientosVista.BestFitColumns();
            }
        }

        public void CargarOperaciones()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarRelaciones(1, 0);
            cbxOperacion.DataSource = dtOperaciones;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void CargarOperaciones2()
        {
            DataTable dtOperaciones2 = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarRelaciones(2, 0);
            cbxOperacion2.DataSource = dtOperaciones2;
            cbxOperacion2.DisplayMember = "Descripcion";
            cbxOperacion2.ValueMember = "IdOperacion";
        }

        public void CargarResponsable()
        {
            DataTable dtResponsable = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarRelaciones(4, 0);
            cbxResponsable.DataSource = dtResponsable;
            cbxResponsable.DisplayMember = "DESCRIPCION";
            cbxResponsable.ValueMember = "department";
        }

        public void CargarDocumento()
        {
            if (cbxRelacion.Text == "CONDUCTOR")
            {
                DataTable dtDocumento = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarRelaciones(5, 1);
                cbxDocumentos.DataSource = dtDocumento;
                cbxDocumentos.DisplayMember = "DESCRIPCION";
                cbxDocumentos.ValueMember = "IdTipoDocumento";
            }
            else
            {
                DataTable dtDocumento = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarRelaciones(5, 0);
                cbxDocumentos.DataSource = dtDocumento;
                cbxDocumentos.DisplayMember = "DESCRIPCION";
                cbxDocumentos.ValueMember = "IdTipoDocumento";
            }
        }

        public void CargarRelacion()
        {
            DataTable dtRelacion = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_ListarRelaciones(3, Convert.ToInt32(cbxOperacion.SelectedValue));
            cbxRelacion.DataSource = dtRelacion;
            cbxRelacion.DisplayMember = "TipoRelacion";
            cbxRelacion.ValueMember = "idMRelacion";

            CargarDocumento();
        } 

        private void CargarControles()
        {
            if (DtTipoRelacion == null) { MessageBox.Show("No se cargaron controles", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            else
            {
                if (DtTipoRelacion.Rows.Count > 0)
                {
                    //Llenado de TiposDocumentos 
                    cbxTipoRelacion.DataSource = DtTipoRelacion;
                    cbxTipoRelacion.DisplayMember = "Descripcion";
                    cbxTipoRelacion.ValueMember = "Codigo";
                    cbxTipoRelacion.SelectedIndex = 0;
                }
            }
        }

        private void cargarListaDocumentos()
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt = clsControlDocumentosBL.Instancia.getDocumentos_ListarTiposDocumentos(cbxTipoRelacion.SelectedValue.ToString());

            if (dt.Rows.Count > 0)
            {
                dgvTipoDocs.DataSource = dt;

                dgvTipoDocsView.Columns["IdTipoDocumento"].Visible = false;

                dgvTipoDocsView.BestFitColumns();
            }
            else
            {
                dgvTipoDocs.DataSource = null;
                MessageBox.Show("No hay data para mostrar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /*
        private DataTable ConvertirXmlToDataTable(string pXml)
        {
            string xml = pXml;

            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(xml));

            DataTable Dt = new DataTable(Name);

            try
            {
                XmlNode NodoEstructura = doc.FirstChild.FirstChild;
                //  Table structure (columns definition) 
                foreach (XmlNode columna in NodoEstructura.ChildNodes) { Dt.Columns.Add(columna.Name, typeof(String)); }

                XmlNode Filas = doc.FirstChild;
                //  Data Rows 
                foreach (XmlNode Fila in Filas.ChildNodes)
                {
                    List<string> Valores = new List<string>();
                    foreach (XmlNode Columna in Fila.ChildNodes) { Valores.Add(Columna.InnerText); }
                    Dt.Rows.Add(Valores.ToArray());
                }
            }
            catch (Exception) { }

            return Dt;
        }
        */


        private void cbxOperacion_DropDownClosed(object sender, EventArgs e) { CargarRelacion(); }

        private void cbxRelacion_DropDownClosed(object sender, EventArgs e) { CargarDocumento(); }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            frmRegistroTiposDocumentos frm = new frmRegistroTiposDocumentos();
            DataRow[] resultRow = DtTipoRelacion.Select("Codigo<>'TD'");
            DataTable DtTipoRela = resultRow.CopyToDataTable();

            frm.DtTipoRelacion = DtTipoRela;
            frm._IdTipoDocumento = 0;
            frm.ShowDialog();

            if (frm.NrRPTA == "0") { cargarListaDocumentos(); }
        }

        private void cbxTipoRelacion_DropDownClosed(object sender, EventArgs e) { cargarListaDocumentos(); }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistroTiposDocumentos frm = new frmRegistroTiposDocumentos();

            DataRow[] resultRow = DtTipoRelacion.Select("Codigo<>'TD'");

            DataTable DtTipoRe = resultRow.CopyToDataTable();
            frm.DtTipoRelacion = DtTipoRe;


            foreach (var i in dgvTipoDocsView.GetSelectedRows())
            {
                frm._IdTipoDocumento = Convert.ToInt32(dgvTipoDocsView.GetDataRow(i)["IdTipoDocumento"].ToString());
                frm._Nemonico = dgvTipoDocsView.GetDataRow(i)["NEMONICO"].ToString();
                frm._Descripcion = dgvTipoDocsView.GetDataRow(i)["DESCRIPCION"].ToString();
                frm._TipoRelacion = dgvTipoDocsView.GetDataRow(i)["TIPO_RELACION"].ToString();
                frm._Dias = Convert.ToInt32(dgvTipoDocsView.GetDataRow(i)["DIAS_ALERTA"].ToString());
            }

            frm.ShowDialog();

            if (frm.NrRPTA == "0") { cargarListaDocumentos(); }
        }

        private void dgvVencimientos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                int idVencimiento = Convert.ToInt32(dgvVencimientosVista.GetRowCellValue(dgvVencimientosVista.FocusedRowHandle, "NRO"));

                if (idVencimiento != 0) { tsEliminar.Enabled = true; }
                else { tsEliminar.Enabled = false; }
            }
            catch { tsEliminar.Enabled = false; }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtVencimiento.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese los meses de vencimiento", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtVencimiento.Focus();
                return;
            }
            else
            {
                try
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_InsertarEliminarVencimiento(1, 0, Convert.ToInt32(cbxRelacion.SelectedValue),
                                                             Convert.ToInt32(cbxDocumentos.SelectedValue), Convert.ToInt32(txtVencimiento.Text), Convert.ToString(cbxResponsable.SelectedValue), Usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = Respuesta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarOperaciones();
                        CargarResponsable();

                        cbxOperacion.Text = "LINDLEY";
                        cbxOperacion_DropDownClosed(sender, e);
                        cbxRelacion.Text = "CONDUCTOR";
                        cbxRelacion_DropDownClosed(sender, e);
                        txtVencimiento.Text = "0";
                        ListarVencimientos();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                catch { MessageBox.Show("No se pudo registrar el vencimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void tsEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar este vencimiento?", "ELIMINAR VENCIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idVencimiento = Convert.ToInt32(dgvVencimientosVista.GetRowCellValue(dgvVencimientosVista.FocusedRowHandle, "NRO"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operacion_ControlDocumentos_InsertarEliminarVencimiento(2, idVencimiento, 0, 0, 0, " ", Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarVencimientos(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void cbxOperacion2_DropDownClosed(object sender, EventArgs e) { ListarVencimientos(); }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarVencimientos(); }
    }
}
