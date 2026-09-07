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
using System.Runtime.InteropServices;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class RegistrarGuiasViajeRetorno : Form
    {

        public string codigoViajeGuia;
        public int idviajeGuia;
        public string fecha;
        public string conductorGuia;
        public int idConductorGuia;
        public string tracto;
        public int idVehiculo;
        public int idEstado;
        public int idRuta;
        public string ruta;
        bool guiarepetia = false;
        public RegistrarGuiasViajeRetorno()
        {
            InitializeComponent();
        }


        private void RegistrarGuiasViaje_Load(object sender, EventArgs e)
        {
            try
            {
                dgvGuias.EnableHeadersVisualStyles = false;
                dgvGuias.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#bfdbff");

                lblCodigoViaje.Text = codigoViajeGuia;
                lblConductor.Text = conductorGuia;
                lblFechaViaje.Text = fecha;
                lblRuta.Text = ruta;
                cargarOTes();
                dgvGuias.Focus();
                dgvGuias.Select();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void cargarOTes()
        {
          DataTable dtOt = clsContabilidadBL.Instancia.ReportesApp_ListarOT_Viaje(idviajeGuia, idConductorGuia, fecha.Remove(10));
          if (dtOt.Rows.Count > 0)
          {

              if (dtOt != null)
              {
                  if (dtOt.Rows.Count > 0)
                  {
                      dgvGuias.Rows.Clear();

                      for (int i = 0; i < dtOt.Rows.Count; i++)
                      {
                          dgvGuias.Rows.Add(dtOt.Rows[i]["IdOT"].ToString(),
                              dtOt.Rows[i]["Tipo"].ToString(),
                              dtOt.Rows[i]["FechaAsignacion"].ToString(),
                              dtOt.Rows[i]["IdGuia"].ToString(),
                              dtOt.Rows[i]["GUIA/TR"].ToString(),
                              dtOt.Rows[i]["SERIE"].ToString(),
                              dtOt.Rows[i]["NUMERO"].ToString(),
                              dtOt.Rows[i]["CodigoViaje"].ToString(),
                              dtOt.Rows[i]["GUIAREMITENTE"].ToString(),
                              dtOt.Rows[i]["GuiaOtros"].ToString().TrimEnd(),
                              dtOt.Rows[i]["Observaciones"].ToString(),
                              dtOt.Rows[i]["IdViaje"].ToString(),
                              dtOt.Rows[i]["IdConductor"].ToString(),
                              dtOt.Rows[i]["Conductor"].ToString());

                          if (dtOt.Rows[i]["IdViaje"].ToString() != "")
                          {
                              dgvGuias.Rows[i].Cells["IdViaje"].ReadOnly = true;
                              dgvGuias.Rows[i].Cells["GUIAREMITENTE"].ReadOnly = true;
                              dgvGuias.Rows[i].Cells["GuiaOtros"].ReadOnly = true;
                              dgvGuias.Rows[i].Cells["Observaciones"].ReadOnly = true;
                              dgvGuias.Rows[i].Cells["CodigoViaje"].ReadOnly = true;
                              dgvGuias.Rows[i].Cells["CodigoViaje"].ReadOnly = true;
                          }
                          else
                          {
                              if (dtOt.Rows[i]["IdViaje"].ToString() == "")
                              {
                                  dgvGuias.Rows[i].Cells["IdViaje"].ReadOnly = false;
                                  dgvGuias.Rows[i].Cells["GUIAREMITENTE"].ReadOnly = false;
                                  dgvGuias.Rows[i].Cells["GuiaOtros"].ReadOnly = false;
                                  dgvGuias.Rows[i].Cells["Observaciones"].ReadOnly = false;
                                  //dgvGuias.Rows[i].Cells["CodigoViaje"].ReadOnly = false;
                              }
                          }
                      }
                     
                  }
              }

          }
          else
          {
              dgvGuias.DataSource = null;
          }
        }

     

 



        private void dgvGuias_CellValidating_1(object sender, DataGridViewCellValidatingEventArgs e)
        {

            //if (dgvGuias.Columns[e.ColumnIndex].Name == "CodigoViaje")
            //{
            //    if (guiarepetia == false)
            //    {
            //        e.Cancel = true;
            //        dgvGuias.Rows[e.RowIndex].ErrorText = "Error";
            //    }
            //    else
            //    {
            //        e.Cancel = false;
            //    }


            //    //Si el campo esta vacio no lo marco como error
               
            //    if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            //    {
            //        return;

            //    }

                // Solo se valida ante el ingreso de un valor en el campo
               
               /* decimal pedido = 0;
                DataGridViewRow row;

                if (!decimal.TryParse(e.FormattedValue.ToString(), out pedido))
                {
                    row = dgvGuias.Rows[e.RowIndex];

                    row.ErrorText = "Debe ingresar un numero valido";
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }*/



        }

        private void dgvGuias_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dgvGuias.Columns[dgvGuias.CurrentCell.ColumnIndex].Name == "GuiaRemitente" || 
                    dgvGuias.Columns[dgvGuias.CurrentCell.ColumnIndex].Name == "CodigoViaje" ||
                    dgvGuias.Columns[dgvGuias.CurrentCell.ColumnIndex].Name == "GuiaRemitente"||
                    dgvGuias.Columns[dgvGuias.CurrentCell.ColumnIndex].Name == "GuiaOtros"||
                    dgvGuias.Columns[dgvGuias.CurrentCell.ColumnIndex].Name == "Observaciones")
                {
                    
                        Boolean respuesta = clsContabilidadBL.Instancia.ReportesApp_Actualizar_GuiaPorViaje(
                            dgvGuias.CurrentRow.Cells["IdGuia"].Value == "" ? 0 : Convert.ToInt32(dgvGuias.CurrentRow.Cells["IdGuia"].Value),
                            dgvGuias.CurrentRow.Cells["CodigoViaje"].Value == null ? "" : dgvGuias.CurrentRow.Cells["CodigoViaje"].Value.ToString(),
                            dgvGuias.CurrentRow.Cells["GuiaRemitente"].Value == null ? "" : dgvGuias.CurrentRow.Cells["GuiaRemitente"].Value.ToString(),
                            dgvGuias.CurrentRow.Cells["GuiaOtros"].Value == null ? "" : dgvGuias.CurrentRow.Cells["GuiaOtros"].Value.ToString(),
                            dgvGuias.CurrentRow.Cells["Observaciones"].Value == null ? "" : dgvGuias.CurrentRow.Cells["Observaciones"].Value.ToString(), "0",
                            dgvGuias.CurrentRow.Cells["GUIATRANSP"].Value == null ? "" : dgvGuias.CurrentRow.Cells["GUIATRANSP"].Value.ToString(),
                            dgvGuias.CurrentRow.Cells["SERIE"].Value == null ? "" : dgvGuias.CurrentRow.Cells["SERIE"].Value.ToString(),
                           dgvGuias.CurrentRow.Cells["Numero"].Value == null ? "" : dgvGuias.CurrentRow.Cells["Numero"].Value.ToString());
                        if (respuesta)
                        {
                            guiarepetia = false;
                            MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                SendKeys.Send("{UP}");
                                SendKeys.Send("{TAB}");


                        }
                        else
                        {
                            dgvGuias.CurrentRow.Cells["CodigoViaje"].Value = "";
                            MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //dgvGuias.EditMode = DataGridViewEditMode.EditProgrammatically;
                            SendKeys.Send("{UP}");
                            guiarepetia = true;
                           
                        }
                    

                }

                dgvGuias.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




          

        }

        private void dgvGuias_CellBeginEdit_1(object sender, DataGridViewCellCancelEventArgs e)
        {
            dgvGuias.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Beige;// .DefaultCellStyle.BackColor = Color.Beige;
            dgvGuias.Rows[e.RowIndex].ErrorText = String.Empty;

        }

        private void dgvGuias_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int columnIndex = dgvGuias.CurrentCell.ColumnIndex;
            DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;

            if (dgvGuias.Columns[columnIndex].Name == "CodigoViaje")
            {
                

                if (dText != null)
                {
                    dText.KeyPress -= new KeyPressEventHandler(Grid_KeyPress);
                    dText.KeyPress += new KeyPressEventHandler(Grid_KeyPress);
                }

            }
            else
            {
                dText.KeyPress -= new KeyPressEventHandler(Grid_KeyPress);
            }
        }

        private void Grid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !((Char.IsNumber(e.KeyChar) || e.KeyChar == (Char)Keys.Back) || e.KeyChar == (Char)Keys.Enter || e.KeyChar == (Char)Keys.Escape);

        }

        private void label1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        
            
        }

        private void RegistrarGuiasViaje_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Escape)
            {
                this.Close();
            }
        }

        private void dgvGuias_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == (Char)Keys.Escape)
            {
                if (!dgvGuias.CurrentRow.Cells["GuiaRemitente"].IsInEditMode ||
                    !dgvGuias.CurrentRow.Cells["CodigoViaje"].IsInEditMode ||
                    !dgvGuias.CurrentRow.Cells["GuiaRemitente"].IsInEditMode ||
                    !dgvGuias.CurrentRow.Cells["GuiaOtros"].IsInEditMode ||
                    !dgvGuias.CurrentRow.Cells["Observaciones"].IsInEditMode)
                {
                    this.Close();
                }
            
            }




        }

        private void btnAnexar_Click(object sender, EventArgs e)
        {
            int i = 0;
            try
            {
                if (dgvGuias.CurrentRow.Cells["CodigoViaje"].Value != "" || dgvGuias.CurrentRow.Cells["CodigoViaje"].Value != null)
                {

                    DataGridViewSelectedRowCollection Seleccionados = dgvGuias.SelectedRows;
                    foreach (DataGridViewRow item in Seleccionados)
                    {

                        string PESO = Microsoft.VisualBasic.Interaction.InputBox("Ingrese Peso","Peso Guia","0");
                        if (PESO != "")
                        {
                            Boolean respuesta = clsContabilidadBL.Instancia.ReportesApp_Actualizar_GuiaPorViaje(
                           item.Cells["IdGuia"].Value == "" ? 0 : Convert.ToInt32(item.Cells["IdGuia"].Value),
                           codigoViajeGuia,
                           item.Cells["GuiaRemitente"].Value == null ? "" : item.Cells["GuiaRemitente"].Value.ToString(),
                           item.Cells["GuiaOtros"].Value == null ? "" : item.Cells["GuiaOtros"].Value.ToString(),
                           item.Cells["Observaciones"].Value == null ? "" : item.Cells["Observaciones"].Value.ToString(),
                           PESO, 
                           item.Cells["GUIATRANSP"].Value == null ? "" : item.Cells["GUIATRANSP"].Value.ToString(),
                           item.Cells["SERIE"].Value == null ? "" : item.Cells["SERIE"].Value.ToString(),
                           item.Cells["Numero"].Value == null ? "" : item.Cells["Numero"].Value.ToString());
                            if (respuesta)
                            {
                                i++;
                                item.Cells["CodigoViaje"].Value = codigoViajeGuia;

                                if (i == (Seleccionados.Count))
                                {
                                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    guiarepetia = false;
                                    SendKeys.Send("{UP}");
                                    SendKeys.Send("{TAB}");
                                }



                            }
                            else
                            {
                                item.Cells["CodigoViaje"].Value = "";
                                MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                SendKeys.Send("{UP}");
                                guiarepetia = true;

                            }
                        }
                        else
                        {
                            MessageBox.Show("Campo Peso es obligatorio ingresarlo", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                       

                       
                    }


                }

               
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btnDesvincular_Click(object sender, EventArgs e)
        {
            try
            {
                
                Boolean respuesta = clsContabilidadBL.Instancia.ReportesApp_Desvncular_GuiaRetorno(dgvGuias.CurrentRow.Cells["IdGuia"].Value == "" ? 0 : Convert.ToInt32(dgvGuias.CurrentRow.Cells["IdGuia"].Value));
                if (respuesta)
                {
                    cargarOTes();
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                dgvGuias.CurrentRow.Cells["GuiaRemitente"].ReadOnly = false;
                dgvGuias.CurrentRow.Cells["GuiaOtros"].ReadOnly = false;
                dgvGuias.CurrentRow.Cells["Observaciones"].ReadOnly = false;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }
    }
}
