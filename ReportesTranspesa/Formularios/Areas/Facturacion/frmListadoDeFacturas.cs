using DevExpress.Utils;
using Negocio;
using ReportesTranspesa.Sistema;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Facturacion
{
    public partial class frmListadoDeFacturas : Form
    {
        int idProveedor = -1;
        int cliente = -1;

        bool Accesos;
        bool Modificar;

        public frmListadoDeFacturas()
        {
            InitializeComponent();
        }

        private void btn_BuscarClick(object sender, EventArgs e)
        {
            Buscar();
        }

        private void Buscar()
        {
            gridControl1.DataSource = null;            

            System.Data.DataTable dt = new System.Data.DataTable();
            string compania = "";
            switch (cboCompania.SelectedIndex)
            {

                case 0:
                    compania = "100000";
                    break;
                case 1:
                    compania = "200000";
                    break;
                case 2:
                    compania = "300000";
                    break;
                case 3:
                    compania = "400000";
                    break;
                case 4:
                    compania = "500000";
                    break;
                case 5:
                    compania = "600000";
                    break;
                case 6:
                    compania = "700000";
                    break;
                case 7:
                    compania = "800000";
                    break;
                case 8:
                    compania = "ALQALM";
                    break;
            }
            string tipodocumento = "";
            switch (comboBox1.SelectedIndex)
            {

                case 0:
                    tipodocumento = "NC";
                    break;
                case 1:
                    tipodocumento = "FC";
                    break;
                case 2:
                    tipodocumento = "BV";
                    break;  
            }

            dt = clsFinanzasBL.Instancia.GetListadoDeFacturas(compania, dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                 dtpFechaFin.Value.ToShortDateString() + " 23:59:59", tipodocumento, idProveedor);

            if (dt.Rows.Count > 0)
            {
                gridControl1.DataSource = dt;
                gridView1.Columns["Compania"].Visible = false;
                gridView1.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void frmListadoDeFacturas_Load(object sender, EventArgs e)
        {
            DataTable dtCompania = new DataTable();
            dtCompania = clsFinanzasBL.Instancia.GetOperaciones_ListarCompania(Utilitario.Instancia.SesionUsuario.usuario);
            cboCompania.DisplayMember = "DescripcionCorta";
            cboCompania.ValueMember = "CompaniaCodigo";
            cboCompania.DataSource = dtCompania;

            //BUSCAMOS LOS ACCESOS DE LAS FACTURAS
            DataTable dtVerAccesosXfactura = new DataTable();
            dtVerAccesosXfactura = clsFinanzasBL.Instancia.GetAccesosxFacturas(1, "", "", "", Utilitario.Instancia.SesionUsuario.usuario);
            if (dtVerAccesosXfactura.Rows.Count > 0)
            {
                for (int i = 0; i < dtVerAccesosXfactura.Rows.Count; i++)
                {
                    Accesos = Convert.ToBoolean(dtVerAccesosXfactura.Rows[i]["ACCESOS"].ToString());
                    Modificar = Convert.ToBoolean(dtVerAccesosXfactura.Rows[i]["MODIFICA"].ToString());
                   
                }
            }

            //ACTIVAMOS LOS BOTONES SEGUN EL ACCESO
            if (Accesos == true)
            {
                btnAccesos.Visible = true;
            }
            if (Modificar == true)
            {
                btn_ModificarFactura.Visible = true;
            }

        }

        private void btn_ModificarFactura_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridControl1.DataSource == null) { return; }
                
                int[] filass = gridView1.GetSelectedRows();
                string datoseleccionado = gridView1.GetFocusedValue().ToString();

                for (int i = 0; i < filass.Length; i++)
                {
                    string factura = gridView1.GetRowCellValue(filass[i], "N° Documento").ToString();
                    string compania = gridView1.GetRowCellValue(filass[i], "Compania").ToString();
                    string CompaniaDescripcion = gridView1.GetRowCellValue(filass[i], "CompaniaDescripcion").ToString();
                    string tipodocumento = gridView1.GetRowCellValue(filass[i], "TipoDocumento").ToString();
                                       
                    if (!factura.Equals(""))
                    {
                        // OTselec = gvrUnidades.GetRowCellValue(filas[i], "ID").ToString();
                        frmModificarFactura frmModificar = new frmModificarFactura();
                        frmModificar.setearvariable(factura, compania, CompaniaDescripcion, tipodocumento);
                        frmModificar.ShowDialog(this);
                        Buscar();
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void lstCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstCliente.SelectedItems[0];
                idProveedor = Convert.ToInt32(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;

                lstCliente.Visible = false;
                btnBuscar.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstCliente.Visible = false;
                txtCliente.Focus();
            }
        }
        
        private void btnBuscar_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void lstCliente_Enter(object sender, EventArgs e)
        {
            if (!lstCliente.Items.Count.Equals(0))
            {
                lstCliente.Items[0].Selected = true;
            }
        }
        
        private void lstCliente_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            ListViewItem ItemActual;
            ItemActual = lstCliente.SelectedItems[0];
            idProveedor = Int32.Parse(ItemActual.Text);
            txtCliente.Text = ItemActual.SubItems[1].Text;
            lstCliente.Visible = false;
            txtCliente.Focus();
        }

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstCliente, clsConsultaBL.Instancia.GetPersona(txtCliente.Text), true, false, false);

                lstCliente.Columns[0].Width = 0;
                lstCliente.Columns[1].Width = 206;
                lstCliente.Columns[2].Width = 110;

                lstCliente.Size = new System.Drawing.Size(351, 103);

                lstCliente.BringToFront();
                lstCliente.Visible = true;
                lstCliente.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstCliente.Visible = false;
                txtCliente.Focus();

            }
        }

        private void btnAccesos_Click(object sender, EventArgs e)
        {
            if (gridControl1.DataSource == null)
            {
                return;
            }
            
            int[] filass = gridView1.GetSelectedRows();
            string datoseleccionado = gridView1.GetFocusedValue().ToString();

            for (int i = 0; i < filass.Length; i++)
            {
                string factura = gridView1.GetRowCellValue(filass[i], "N° Documento").ToString();
                string compania = gridView1.GetRowCellValue(filass[i], "Compania").ToString();
                string CompaniaDescripcion = gridView1.GetRowCellValue(filass[i], "CompaniaDescripcion").ToString();
                string tipodocumento = gridView1.GetRowCellValue(filass[i], "TipoDocumento").ToString();

                if (!factura.Equals(""))
                {
                    frmAccesoFacturas frmAccesoFac = new frmAccesoFacturas();
                    frmAccesoFac.compania = compania;
                    frmAccesoFac.serie = tipodocumento;
                    frmAccesoFac.Factura = factura;
                    frmAccesoFac.ShowDialog();
                }
            }           
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (gridControl1.DataSource == null)
                {
                    return;
                }

                int[] filass = gridView1.GetSelectedRows();
                string datoseleccionado = gridView1.GetFocusedValue().ToString();

                for (int i = 0; i < filass.Length; i++)
                {
                    string factura = gridView1.GetRowCellValue(filass[i], "N° Documento").ToString();
                    string compania = gridView1.GetRowCellValue(filass[i], "Compania").ToString();
                    string CompaniaDescripcion = gridView1.GetRowCellValue(filass[i], "CompaniaDescripcion").ToString();
                    string tipodocumento = gridView1.GetRowCellValue(filass[i], "TipoDocumento").ToString();

                    if (!factura.Equals(""))
                    {
                        // OTselec = gvrUnidades.GetRowCellValue(filas[i], "ID").ToString();
                        frmModificarFactura frmModificar = new frmModificarFactura();
                        frmModificar.setearvariable(factura, compania, CompaniaDescripcion, tipodocumento);
                        frmModificar.Ver = 1;
                        frmModificar.ShowDialog(this);
                        Buscar();

                    }
                }
            }

            catch (Exception)
            {

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmFacturasHistorial frmHistorico = new frmFacturasHistorial();
            frmHistorico.ShowDialog();
        }
    }
}
