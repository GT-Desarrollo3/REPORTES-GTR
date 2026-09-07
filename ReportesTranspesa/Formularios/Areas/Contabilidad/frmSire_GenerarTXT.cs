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
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class frmSire_GenerarTXT : Form
    {
        string Check_compras_ventas_noDomi = string.Empty;
        DataTable dt ;
        string RUC = string.Empty;
        string direccion = string.Empty;
        public frmSire_GenerarTXT()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbxEmpresa.Text != "TODOS")
                {
                    if (rbCompras.Checked)
                    {
                        generartxtCompras();
                    }
                    else if (rbVentas.Checked)
                    {
                        generartxtVentas();
                    }
                    else
                    {
                        generartxtNoDomi();
                    }
                    
                }
                else
                {
                    MessageBox.Show("No es posible generar .txt de todas las empresas, seleccione solo una", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void generartxtNoDomi()
        {
            try
            {
                if (!Directory.Exists(@"C:\Sire\No Domiciliado"))
                {
                    Directory.CreateDirectory(@"C:\Sire\No Domiciliado");
                }

                TextWriter sw;


                if (cbxEmpresa.SelectedValue.ToString() != "40000000")
                {
                    direccion = @"C:\Sire\No Domiciliado\" + "LE" + dt.Rows[0]["RucEmpresa"].ToString().Replace("|", "") + dtpPeriodo.Text + "00" + "080500" + "02" + "1" + "1" + "1" + "2" + ".txt";
                }
                else
                {
                    direccion = @"C:\Sire\No Domiciliado\" + "LE" + dt.Rows[0]["RucEmpresa"].ToString().Replace("|", "") + dtpPeriodo.Text + "00" + "080500" + "02" + "1" + "1" + "1" + "2" + ".txt";
                }

                sw = new StreamWriter(System.IO.Path.Combine(direccion));

                if (dt.Rows.Count > 0)
                {


                    foreach (DataRow x in dt.Rows)
                    {


                        sw.WriteLine(x.ItemArray[0].ToString() + "|" +
                                     x.ItemArray[1].ToString() + "|" +
                                     x.ItemArray[2].ToString() + "|" +
                                     x.ItemArray[3].ToString() + "|" +
                                     x.ItemArray[4].ToString() + "|" +
                                     x.ItemArray[5].ToString() + "|" +
                                     x.ItemArray[6].ToString() + "|" +
                                     x.ItemArray[7].ToString() + "|" +
                                     x.ItemArray[8].ToString() + "|" +
                                     x.ItemArray[9].ToString() + "|" +
                                     x.ItemArray[10].ToString() + "|" +
                                     x.ItemArray[11].ToString() + "|" +
                                     x.ItemArray[12].ToString() + "|" +
                                     x.ItemArray[13].ToString() + "|" +
                                     x.ItemArray[14].ToString() + "|" +
                                     x.ItemArray[15].ToString() + "|" +
                                     x.ItemArray[16].ToString() + "|" +
                                     x.ItemArray[17].ToString() + "|" +
                                     x.ItemArray[18].ToString() + "|" +
                                     x.ItemArray[19].ToString() + "|" +
                                     x.ItemArray[20].ToString() + "|" +
                                     x.ItemArray[21].ToString() + "|" +
                                     x.ItemArray[22].ToString() + "|" +
                                     x.ItemArray[23].ToString() + "|" +
                                     x.ItemArray[24].ToString() + "|" +
                                     x.ItemArray[25].ToString() + "|" +
                                     x.ItemArray[26].ToString() + "|" +
                                     x.ItemArray[27].ToString() + "|" +
                                     x.ItemArray[28].ToString() + "|" +
                                     x.ItemArray[29].ToString() + "|" +
                                     x.ItemArray[30].ToString() + "|" 
                                     );

                    }
                    sw.Close();
                }

                MessageBox.Show(direccion, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void generartxtVentas()
        {
            if (!Directory.Exists(@"C:\Sire\Ventas"))
            {
                Directory.CreateDirectory(@"C:\Sire\Ventas");
            }

            TextWriter sw;


            if (cbxEmpresa.SelectedValue.ToString() != "40000000")
            {
                direccion = @"C:\Sire\Ventas\" + "LE" + dt.Rows[0]["RucEmpresa"].ToString().Replace("|", "") + dtpPeriodo.Text + "00" + "140400" + "02" + "1" + "1" + "1" + "2" + ".txt";
            }
            else
            {
                direccion = @"C:\Sire\Ventas\" + "LE" + "20477313214" + dtpPeriodo.Text + "00" + "140400" + "02" + "1" + "0" + "1" + "2" + ".txt";
            }

            sw = new StreamWriter(System.IO.Path.Combine(direccion));

            if (dt.Rows.Count > 0)
            {


                foreach (DataRow x in dt.Rows)
                {


                    sw.WriteLine(x.ItemArray[0].ToString() + "|"+
                                 x.ItemArray[1].ToString() + "|" +
                                 x.ItemArray[2].ToString() + "|" +
                                 x.ItemArray[3].ToString() + "|" +
                                 x.ItemArray[4].ToString() + "|" +
                                 x.ItemArray[5].ToString() + "|" +
                                 x.ItemArray[6].ToString() + "|" +
                                 x.ItemArray[7].ToString() + "|" +
                                 x.ItemArray[8].ToString() + "|" +
                                 x.ItemArray[9].ToString() + "|" +
                                 x.ItemArray[10].ToString() + "|" +
                                 x.ItemArray[11].ToString() + "|" +
                                 x.ItemArray[12].ToString() + "|" +
                                 x.ItemArray[13].ToString() + "|" +
                                 x.ItemArray[14].ToString() + "|" +
                                 x.ItemArray[15].ToString() + "|" +
                                 x.ItemArray[16].ToString() + "|" +
                                 x.ItemArray[17].ToString() + "|" +
                                 x.ItemArray[18].ToString() + "|" +
                                 x.ItemArray[19].ToString() + "|" +
                                 x.ItemArray[20].ToString() + "|" +
                                 x.ItemArray[21].ToString() + "|" +
                                 x.ItemArray[22].ToString() + "|" +
                                 x.ItemArray[23].ToString() + "|" +
                                 x.ItemArray[24].ToString() + "|" +
                                 x.ItemArray[25].ToString() + "|" +
                                 x.ItemArray[26].ToString() + "|" +
                                 x.ItemArray[27].ToString() + "|" +
                                 x.ItemArray[28].ToString() + "|" +
                                 x.ItemArray[29].ToString() + "|" +
                                 x.ItemArray[30].ToString() + "|" +
                                 x.ItemArray[31].ToString() + "|" +
                                 x.ItemArray[32].ToString() + "|" +
                                 x.ItemArray[32].ToString() + "|" 
                                 );

                }
                sw.Close();
            }

            MessageBox.Show(direccion, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void generartxtCompras()
        {
            if (!Directory.Exists(@"C:\Sire\Compras"))
            {
                Directory.CreateDirectory(@"C:\Sire\Compras");
            }

            TextWriter sw;

       
                if (cbxEmpresa.SelectedValue.ToString() != "40000000")
                {
                    direccion = @"C:\Sire\Compras\" + "LE" + dt.Rows[0]["RucEmpresa"].ToString().Replace("|", "") + dtpPeriodo.Text + "00" + "080400" + "02" + "1" + "1" + "1" + "2" + ".txt";
                }
                else
                {
                    direccion = @"C:\Sire\Compras\" + "LE" + "20477313214" + dtpPeriodo.Text + "00" + "080400" + "02" + "1" + "0" + "1" + "2" + ".txt";
                }

                sw = new StreamWriter(System.IO.Path.Combine(direccion));

            if (dt.Rows.Count > 0)
            {
                

                foreach (DataRow x in dt.Rows)
                {


                    sw.WriteLine(x.ItemArray[0].ToString() + "|" +
                                 x.ItemArray[1].ToString() + "|" +
                                 x.ItemArray[2].ToString() + "|" +
                                 x.ItemArray[3].ToString() + "|" +
                                 x.ItemArray[4].ToString() + "|" +
                                 x.ItemArray[5].ToString() + "|" +
                                 x.ItemArray[6].ToString() + "|" +
                                 x.ItemArray[7].ToString() + "|" +
                                 x.ItemArray[8].ToString() + "|" +
                                 x.ItemArray[9].ToString() + "|" +
                                 x.ItemArray[10].ToString() + "|" +
                                 x.ItemArray[11].ToString() + "|" +
                                 x.ItemArray[12].ToString() + "|" +
                                 x.ItemArray[13].ToString() + "|" +
                                 x.ItemArray[14].ToString() + "|" +
                                 x.ItemArray[15].ToString() + "|" +
                                 x.ItemArray[16].ToString() + "|" +
                                 x.ItemArray[17].ToString() + "|" +
                                 x.ItemArray[18].ToString() + "|" +
                                 x.ItemArray[19].ToString() + "|" +
                                 x.ItemArray[20].ToString() + "|" +
                                 x.ItemArray[21].ToString() + "|" +
                                 x.ItemArray[22].ToString() + "|" +
                                 x.ItemArray[23].ToString() + "|" +
                                 x.ItemArray[24].ToString() + "|" +
                                 x.ItemArray[25].ToString() + "|" +
                                 x.ItemArray[26].ToString() + "|" +
                                 x.ItemArray[27].ToString() + "|" +// TipoCambio
                                 x.ItemArray[28].ToString() + "|" +
                                 x.ItemArray[29].ToString() + "|" +
                                 x.ItemArray[30].ToString() + "|" +
                                 x.ItemArray[31].ToString() + "|" +
                                 x.ItemArray[32].ToString() + "|" +
                                 x.ItemArray[33].ToString() + "|" +
                                 x.ItemArray[34].ToString() + "|" +
                                 x.ItemArray[35].ToString() + "|" +
                                 x.ItemArray[36].ToString() + "|" +
                                 x.ItemArray[37].ToString() + "|" 
                                 );

                }
                sw.Close();
            }

            MessageBox.Show(direccion, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (rbCompras.Checked)
                {
                    Check_compras_ventas_noDomi = "COMPRAS";
                }
                else if (rbVentas.Checked)
                {
                    Check_compras_ventas_noDomi = "VENTAS";
                }
                else
                {
                    Check_compras_ventas_noDomi = "NO DOMICILIADO"; 
                }

                this.Cursor = Cursors.WaitCursor;
                dt = clsContabilidadBL.Instancia.ReportesApp_ListarComprobantesSire(cbxEmpresa.SelectedValue.ToString(),Check_compras_ventas_noDomi,dtpPeriodo.Text);
                

                if (dt.Rows.Count > 0)
                {
                    dtgSire.DataSource = dt;
                    dgvListaSireVista.BestFitColumns();
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    dtgSire.DataSource = null;
                    this.Cursor = Cursors.Default;

                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmSire_GenerarTXT_Load(object sender, EventArgs e)
        {
            cbxEmpresa.DisplayMember = "COMPDESC";
            cbxEmpresa.ValueMember = "IDCOMP";
            cbxEmpresa.DataSource = clsConsultaBL.Instancia.GetCompañias(); 

         
        }

        private void dgvListaSireVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();


        }
    }
}
