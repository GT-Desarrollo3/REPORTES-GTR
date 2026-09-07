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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class FrmLiquidaPlanilla : Form
    {
        public int IdPersona;
        public string CompaniaSocio;
        public DataTable dtProgramacionViaje;
        public DataTable DtConceptoGastos;

        private DataTable dtDetalleLiquidacion = new DataTable();

        public FrmLiquidaPlanilla()
        {
            InitializeComponent();
        }

        private void FrmLiquidacionViaticos_Load(object sender, EventArgs e)
        {
            ListarConceptoGastos();
            ListarViajesPorConductor();
            txtImporte.Text = "";
            ListarDetalleLiquidacion();
        }

        private void ListarViajesPorConductor()
        {
            dtProgramacionViaje = clsLiquidacionPlanillaBL.Instancia.ListarProgramacionViajePorConductor(IdPersona);     
        }



        private void ListarConceptoGastos()
        {
            if (DtConceptoGastos != null && DtConceptoGastos.Rows.Count> 0)
            {
               
                cbxGastos.DataSource = DtConceptoGastos;
                cbxGastos.DisplayMember = "Descripcion";
                cbxGastos.AccessibleName = "Descripcion";
                cbxGastos.ValueMember = "ConceptoGasto";
                //cbxGastos.Text =  "Descripcion";

                cbxGastos.SelectedIndex = -1;

            }
        }

        private void tsBtnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscarViajes_Click(object sender, EventArgs e)
        {

            if (dtProgramacionViaje != null || dtProgramacionViaje.Rows.Count > 0)
            {
                FrmBuscaPreviajes frm = new FrmBuscaPreviajes();
                frm.dtProgramacionViaje = dtProgramacionViaje;
                frm.ShowDialog();

                lblPlaca.Text = frm.Placa;
                lblRuta.Text = frm.Ruta;
                lblCodigoViaje.Text = frm.NroViaje.ToString();

            }
            else
            {
                MessageBox.Show("No tiene viajes programados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            if (cbxGastos.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un concepto de gasto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cbxGastos.Focus();
                return;
            }

            if (txtImporte.Text.Length == 0)
            {
                MessageBox.Show("Ingrese importe de gasto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtImporte.Focus();
                return;
            }


            int item = dtDetalleLiquidacion.Rows.Count + 1;
            string DescripcionGasto = cbxGastos.Text;
            string ConceptoGasto = cbxGastos.SelectedValue.ToString();
            int Importe = Convert.ToInt32(txtImporte.Text);

            MessageBox.Show(DescripcionGasto, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            for (int i = 0; i < dgvDetalleliquidacion.Rows.Count - 1; i++)
            {
                string datoConcepto = dgvDetalleliquidacion.Rows[i].Cells["ConceptoGasto"].ToString();

                if (datoConcepto == ConceptoGasto)
                {
                    MessageBox.Show("Este concepto ya ha sido agregado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbxGastos.Focus();
                    return;
                }
            }


            DataRow Liquida = dtDetalleLiquidacion.NewRow();

            Liquida[0] = item;
            Liquida[1] = ConceptoGasto;
            Liquida[2] = DescripcionGasto;
            Liquida[3] = Importe;
            Liquida[4] = dtpFechaGasto.Text;

            dtDetalleLiquidacion.Rows.Add(Liquida);

               
            dgvDetalleliquidacion.DataSource = dtDetalleLiquidacion;

            MessageBox.Show(dgvDetalleliquidacion.Rows.Count.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            dtDetalleLiquidacion.AcceptChanges();
            dtDetalleLiquidacion.EndInit();

        }

        private void ListarDetalleLiquidacion()
        {           
            dtDetalleLiquidacion.Columns.Add("Item");
            dtDetalleLiquidacion.Columns.Add("ConceptoGasto");
            dtDetalleLiquidacion.Columns.Add("Descripcion");
            dtDetalleLiquidacion.Columns.Add("Importe");
            dtDetalleLiquidacion.Columns.Add("Fecha");

            dgvDetalleliquidacion.DataSource = dtDetalleLiquidacion;
            //dgvDetalleliquidacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }



        private void txtImporte_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void cbxGastos_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if ((e.KeyChar == (char) Keys.Enter))
	        {

                MessageBox.Show("df", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  

	        }

        }

        private void txtImporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                btnAgregar.Focus();
            }

            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                return;
            }
        }


    }
}
