using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;
using Negocio;
using ReportesTranspesa.Sistema;
using ReportesTranspesa.Formularios.Areas.Contabilidad;

namespace ReportesTranspesa.Formularios.Areas
{
    public partial class ConfirmacionAprobacion : MetroFramework.Forms.MetroForm
    {
        public ConfirmacionAprobacion()
        {
            InitializeComponent();
        }

        string obligacion;

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string periodo = "";
            periodo = txtPeriodo.Text;
            clsContabilidadBL.Instancia.GetCambiarPeriodoVoucher(obligacion, periodo);
            this.Close();
            Mensaje m = new Mensaje();
            m.mensaje = "Se Aprobo la Obligación: " + Environment.NewLine + obligacion;
            m.ShowDialog();
            //Mensaje m = new Mensaje();
            //m.mensaje = "Se actualizo el Periodo: " + periodo + " del Documento: " + obligacion;
            //m.ShowDialog();
            //AprobacionPlanillasMultiple frm = new AprobacionPlanillasMultiple();
            //frm.dtgvDataAprobadas.DataSource = null;
            //DataTable dt = new DataTable();
            //dt = clsContabilidadBL.Instancia.GetCListaPlanillas(frm.dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            //    frm.dtpFechaFin.Value.ToShortDateString() + " 23:59:59", "Aprobadas");
            //if (dt.Rows.Count > 0)
            //{
            //    frm.dtgvDataAprobadas.DataSource = dt;
            //    frm.dtgvDataAprobadasView.BestFitColumns();
            //}
        }

        private void ConfirmacionAprobacion_Load(object sender, EventArgs e)
        {
            txtCajaChica.Text = clsClaseCompartida.CajaChica.ToString();
            Periodo();
            Adelantos();
            txtDocumento.Text = clsClaseCompartida.Obligacion;
        }

        public void Adelantos()
        {
            string proveedor="";
            obligacion = clsClaseCompartida.Obligacion;
            proveedor = Convert.ToString(clsClaseCompartida.Proveedor);
            //MessageBox.Show(obligacion + " " + proveedor);
            DataTable dt = new DataTable();
            dt = clsContabilidadBL.Instancia.GetListaAdelantos(obligacion, proveedor);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["Secuencia"].Visible = false;
                dtgvDataView.Columns["ObligacionPagoFlag"].Visible = false;
                dtgvDataView.Columns["PagoUnidadReplicacion"].Visible = false;
                dtgvDataView.Columns["PagoNumeroTransaccion"].Visible = false;
                dtgvDataView.Columns["PagoSecuencia"].Visible = false;
                dtgvDataView.Columns["Monto"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "Monto", "Total={0:c2}");
                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        public void Periodo()
        {
            string CajaChica = "";
            CajaChica = txtCajaChica.Text;
            DataTable dt = new DataTable();
            dt = clsContabilidadBL.Instancia.GetVoucherPeriodo(CajaChica);
            if (dt.Rows.Count > 0)
            {
                txtPeriodo.Text = dt.Rows[0][0].ToString();
            }
        }
    }
}
