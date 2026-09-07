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
using System.Xml;
using System.IO;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class FrmLiquidacionPlanilla : Form
    {

        public DataTable DtCompania;
        public DataTable DtConceptoGastos;

        public FrmLiquidacionPlanilla()
        {
            InitializeComponent();
        }

        private void FrmLiquidacionPlanilla_Load(object sender, EventArgs e)
        {
            LlenarControles();

            ListarConductores();

            this.WindowState = FormWindowState.Maximized;
            splitContainer.SplitterDistance = 300;

        }

        private void LlenarControles()
        {
            DataTable dtControl = clsLiquidacionPlanillaBL.Instancia.LlenarControlFormLiquidacionPlanilla();

            if (dtControl == null)
            {
                MessageBox.Show("No se puedo cargar controles");
            }
            else
            {
                if (dtControl.Rows.Count > 0)
                {

                    DtCompania = ConvertirXmlToDataTable(dtControl.Rows[0][0].ToString());
                    DtConceptoGastos = ConvertirXmlToDataTable(dtControl.Rows[0][1].ToString());

                    tsCbxCompaniaSocio.ComboBox.DataSource = DtCompania;
                    tsCbxCompaniaSocio.ComboBox.DisplayMember = "Descripcion";
                    tsCbxCompaniaSocio.ComboBox.ValueMember = "CodEmpresa";

                    tsCbxCompaniaSocio.ComboBox.SelectedIndex = 0;

                }

            }

        }

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
                foreach (XmlNode columna in NodoEstructura.ChildNodes)
                {
                    Dt.Columns.Add(columna.Name, typeof(String));
                }

                XmlNode Filas = doc.FirstChild;
                //  Data Rows 
                foreach (XmlNode Fila in Filas.ChildNodes)
                {
                    List<string> Valores = new List<string>();
                    foreach (XmlNode Columna in Fila.ChildNodes)
                    {
                        Valores.Add(Columna.InnerText);
                    }
                    Dt.Rows.Add(Valores.ToArray());
                }
            }
            catch (Exception)
            {

            }

            return Dt;

        }


        private void btnBuscarConductor_Click(object sender, EventArgs e)
        {
            ListarConductores();
        }

        private void ListarConductores()
        {
            string codEmpresa = tsCbxCompaniaSocio.ComboBox.SelectedValue.ToString();

            DataTable dtConductores = clsLiquidacionPlanillaBL.Instancia.ListarConductores(codEmpresa, txtconductor.Text);

            if (dtConductores != null || dtConductores.Rows.Count > 0)
            {
                dgvconductor.DataSource = dtConductores;

                if (dgvconductor.Rows.Count > 0)
                {
                    dgvconductor.Columns["CODTRABAJADOR"].Visible = false;
                    dgvconductor.Columns["CONDUCTOR"].Width = 250;

                    dgvconductor.Rows[0].Selected = true;
                    dgvconductor.CurrentCell = dgvconductor.Rows[0].Cells[1];

                    int CodConductor = Convert.ToInt16(dgvconductor.Rows[0].Cells[0].Value);

                    ListarGastosAdelantoPorConductor(CodConductor, "PA");
                    ListarPlanillasLiquidadasPorConductor(codEmpresa, CodConductor);

                }
                else
                {
                    dgvconductor.DataSource = null;
                    dgvLiquidacion.DataSource = null;
                    dgvAdelantoGastos.DataSource = null;

                }


            }

        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListarProgramacionViajesPorConductor(int CodConductor)
        {
            DataTable dtProgramacionViaje = clsLiquidacionPlanillaBL.Instancia.ListarProgramacionViajePorConductor(CodConductor);

            if (dtProgramacionViaje != null || dtProgramacionViaje.Rows.Count > 0)
            {
                dgvLiquidacion.DataSource = dtProgramacionViaje;

                if (dgvLiquidacion.Rows.Count > 0)
                {
                    dgvLiquidacion.Columns["CODIGOVIAJE"].Width = 100;
                    dgvLiquidacion.Columns["ESTADO"].Width = 90;
                    dgvLiquidacion.Columns["FECHAVIAJE"].Width = 130;
                    dgvLiquidacion.Columns["PLACA"].Width = 80;
                    
                    dgvLiquidacion.Rows[0].Selected = true;
                    dgvLiquidacion.CurrentCell = dgvLiquidacion.Rows[0].Cells[1];


                }
                else
                {
                    dgvLiquidacion.DataSource = null;

                }


            }
            else
            {
                dgvLiquidacion.DataSource = null;
               
            }

        }

        private void ListarGastosAdelantoPorConductor(int CodConductor, string Estado)
        {
            DataTable dtGastosAdelanto = clsLiquidacionPlanillaBL.Instancia.ListarGastosAdelantoPorConductor("10000000", CodConductor, Estado);

            if (dtGastosAdelanto != null || dtGastosAdelanto.Rows.Count > 0)
            {
                dgvAdelantoGastos.DataSource = dtGastosAdelanto;

                if (dgvAdelantoGastos.Rows.Count > 0)
                {
                    dgvAdelantoGastos.Columns["ImporteTrujillo"].HeaderText = "Importe Trujillo";
                    dgvAdelantoGastos.Columns["ImporteLima"].HeaderText = "Importe Lima";

                    dgvAdelantoGastos.Columns["Planilla"].Width = 100;
                    dgvAdelantoGastos.Columns["ImporteTrujillo"].Width = 150;
                    dgvAdelantoGastos.Columns["ImporteLima"].Width = 150;

                    dgvAdelantoGastos.Columns["persona"].Visible = false;
                   
                    dgvAdelantoGastos.Rows[0].Selected = true;
                    dgvAdelantoGastos.CurrentCell = dgvAdelantoGastos.Rows[0].Cells[1];

                }
                else
                {
                    dgvAdelantoGastos.DataSource = null;

                }

                

            }
            else
            {
                dgvAdelantoGastos.DataSource = null;

            }

        }

        private void ListarPlanillasLiquidadasPorConductor(string CompaniaSocio, int CodConductor)
        {
            DataTable dtLiquidaciones = clsLiquidacionPlanillaBL.Instancia.ListarPlanillasLiquidadasPorConductor(CompaniaSocio, CodConductor);

            if (dtLiquidaciones != null || dtLiquidaciones.Rows.Count > 0)
            {
                dgvLiquidacion.DataSource = dtLiquidaciones;

                if (dgvLiquidacion.Rows.Count > 0)
                {

                    dgvLiquidacion.Rows[0].Selected = true;
                    dgvLiquidacion.CurrentCell = dgvLiquidacion.Rows[0].Cells[1];

                }
                else
                {
                    dgvLiquidacion.DataSource = null;

                }



            }
            else
            {
                dgvLiquidacion.DataSource = null;

            }

        }

        private void liquidarPlanillaToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            if (!dgvconductor.CurrentRow.Selected)
            {
                MessageBox.Show("Debe seleccionar un conductor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvconductor.Focus();
                return;
            }

            if (!dgvLiquidacion.CurrentRow.Selected)
            {
                MessageBox.Show("Debe seleccionar una programación de Viaje a Liquidar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvconductor.Focus();
                return;
            }

            if (!dgvAdelantoGastos.CurrentRow.Selected)
            {
                MessageBox.Show("Debe seleccionar un adelanto de gasto a Liquidar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dgvconductor.Focus();
                return;
            }

        }

      
        private void dgvconductor_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvconductor.CurrentRow.Selected)
            {
                int CodConductor = Convert.ToInt16(dgvconductor.CurrentRow.Cells[0].Value);
                //ListarProgramacionViajesPorConductor(CodConductor);
                ListarGastosAdelantoPorConductor(CodConductor, "PA");
            }
        }


        private void dgvconductor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvconductor.CurrentRow.Selected)
            {
                int CodConductor = Convert.ToInt16(dgvconductor.CurrentRow.Cells[0].Value);
                //ListarProgramacionViajesPorConductor(CodConductor);
                ListarGastosAdelantoPorConductor(CodConductor, "PA");
            }
        }

        private void dgvconductor_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvconductor.CurrentRow.Selected)
            {
                int CodConductor = Convert.ToInt16(dgvconductor.CurrentRow.Cells[0].Value);
                //ListarProgramacionViajesPorConductor(CodConductor);
                ListarGastosAdelantoPorConductor(CodConductor, "PA");
            }
        }

        private void tsBtnReportes_Click(object sender, EventArgs e)
        {
            FrmLiquidacionReportes frm = new FrmLiquidacionReportes();

            frm.ShowDialog();

        }


        private void dgvconductor_CellContextMenuStripChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvconductor.CurrentRow.Selected)
            {
                int CodConductor = Convert.ToInt16(dgvconductor.CurrentRow.Cells[0].Value);
                //ListarProgramacionViajesPorConductor(CodConductor);
                ListarGastosAdelantoPorConductor(CodConductor, "PA");
            }
        }

        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            if (dgvAdelantoGastos ==null || dgvAdelantoGastos.Rows.Count <= 0)
            {
                MessageBox.Show("No hay Planilla a Liquidar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (dgvAdelantoGastos.CurrentRow.Selected == false)
            {
                MessageBox.Show("Seleccione una Planilla a Liquidar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {

                FrmLiquidaPlanilla frm = new FrmLiquidaPlanilla();
                frm.IdPersona = Convert.ToInt32(dgvAdelantoGastos.CurrentRow.Cells["persona"].Value.ToString());
                frm.CompaniaSocio = tsCbxCompaniaSocio.ComboBox.SelectedValue.ToString();

                frm.lblPlanilla.Text = dgvAdelantoGastos.CurrentRow.Cells["Planilla"].Value .ToString();
                frm.lblImporteTrujillo.Text = dgvAdelantoGastos.CurrentRow.Cells["ImporteTrujillo"].Value.ToString();
                frm.lblImporteLima.Text = dgvAdelantoGastos.CurrentRow.Cells["ImporteLima"].Value.ToString();
                frm.DtConceptoGastos = DtConceptoGastos;
                frm.ShowDialog();

            }
            
            
        }

        private void tsCbxCompaniaSocio_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarConductores();
        }



    }
}
