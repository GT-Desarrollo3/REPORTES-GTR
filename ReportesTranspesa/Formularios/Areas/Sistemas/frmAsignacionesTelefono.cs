using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.Data;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Sistemas
{
    public partial class frmAsignacionesTelefono : MetroFramework.Forms.MetroForm
    {
        public frmAsignacionesTelefono()
        {
            InitializeComponent();
        }

        private char boton;
        private frmTelefonos frmTelefonos;
        //variables utilizadas al momento de editar
        private string id;
        private string idempleado;
        private string numero;
        private string idtelefono;
        private string imei;
        //Contar Registro de Numeros Asignados
        List<string> NumeroCelulares;
        //Cuenta Celulares Activos e Inactivos
        decimal sumaActivos;
        decimal sumaInactivos;
        int CuentaActivos;
        //int CuentaNoActivos;

        private void frmAsignacionesTelefono_Load(object sender, EventArgs e)
        {
            //if (Utilitario.Instancia.SesionUsuario.usuario == "DLIZA" || Utilitario.Instancia.SesionUsuario.usuario == "JMARQUINA" || Utilitario.Instancia.SesionUsuario.usuario == "JROJAS")
            if (Utilitario.Instancia.SesionUsuario.usuario == "DALLING" || Utilitario.Instancia.SesionUsuario.usuario == "SCHAVEZ" || Utilitario.Instancia.SesionUsuario.usuario == "GREYES")
            {
                
            }
            else
            {
                OcultaControles(false);            
            }

            ActivaControles(false);
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.GetAsignacionesCel(Utilitario.Instancia.SesionUsuario.usuario, 0);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["IDPersona"].Visible = false;
                //dtgvDataView.Columns["CONTADOR"].Visible = false;
                //dtgvDataView.Columns["CONTADOR"].DisplayFormat.FormatType = FormatType.Numeric;
                //dtgvDataView.Columns["CONTADOR"].DisplayFormat.FormatString = "N2";
                //Celulares Activos
                dtgvDataView.Columns["Descripcion"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "Equipo", "Equipo={0}");
                dtgvDataView.Columns["Descripcion"].SummaryItem.Tag = 1;
                //Celulares Inactivos
                //dtgvDataView.Columns["Estado"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CONTADOR", "Cel. Inactivos={0}");
                //dtgvDataView.Columns["Estado"].SummaryItem.Tag = 2;
                //Cuenta N° de Lineas
                //dtgvDataView.Columns["Numero"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "Numero", "N° Lineas={0}");
                //dtgvDataView.Columns["Numero"].SummaryItem.Tag = 3;
                //Cuenta N° de Lineas
                //dtgvDataView.Columns["Estado"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "Estado", "ACTIVAS={0}");
                //dtgvDataView.Columns["Estado"].SummaryItem.Tag = 4;
                dtgvDataView.UpdateSummary();
                dtgvDataView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
            
        }

        private void txtEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvEmpleado, clsConsultaBL.Instancia.GetPersona(txtEmpleado.Text), true, false, false);

                lvEmpleado.Columns[0].Width = 0;
                lvEmpleado.Columns[1].Width = 206;
                lvEmpleado.Columns[2].Width = 110;

                lvEmpleado.BringToFront();
                lvEmpleado.Visible = true;
                lvEmpleado.Focus();
                splitContainer1.SplitterDistance = lvEmpleado.Top + lvEmpleado.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                splitContainer1.SplitterDistance = 77;
                buscar(0);
            }
        }

        private void lvEmpleado_Enter(object sender, EventArgs e)
        {
            if (!lvEmpleado.Items.Count.Equals(0))
            {
                lvEmpleado.Items[0].Selected = true;
            }
        }

        private void lvEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvEmpleado.SelectedItems[0];

                txtIdEmpleado.Text = ItemActual.Text;
                txtEmpleado.Text = ItemActual.SubItems[1].Text;
                txtNumero.Text = txtIdEmpleado.Text;
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                splitContainer1.SplitterDistance = 77;
                buscar(Convert.ToInt32(txtIdEmpleado.Text));
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                splitContainer1.SplitterDistance = 77;
            }
        }

        private void buscar(int op)
        {
           
                 DataTable dt = new DataTable();
                 dt = clsSistemasBL.Instancia.GetAsignacionesCel(Utilitario.Instancia.SesionUsuario.usuario, op);
                if (dt.Rows.Count > 0)
                {
                    dtgvData.DataSource = dt;
                    dtgvDataView.Columns["IDPersona"].Visible = false;
                    //dtgvDataView.Columns["CONTADOR"].Visible = false;
                    //dtgvDataView.Columns["CONTADOR"].DisplayFormat.FormatType = FormatType.Numeric;
                    //dtgvDataView.Columns["CONTADOR"].DisplayFormat.FormatString = "N2";
                    //Celulares Activos
                    //dtgvDataView.Columns["Estado"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CONTADOR", "Cel. Activos={0}");
                    //dtgvDataView.Columns["Estado"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CONTADOR", "Contador={0}");
                    //dtgvDataView.Columns["Estado"].SummaryItem.Tag = 1;
                    //Celulares Inactivos
                    //dtgvDataView.Columns["Estado"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "CONTADOR", "Cel. Inactivos={0}");
                    //dtgvDataView.Columns["Estado"].SummaryItem.Tag = 2;
                    //Cuenta N° de Lineas
                    //dtgvDataView.Columns["Numero"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "Numero", "N° Lineas={0}");
                    //dtgvDataView.Columns["Numero"].SummaryItem.Tag = 3;
                    //Cuenta N° de Lineas
                    //dtgvDataView.Columns["Estado"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "Estado", "ACTIVAS={0}");
                    //dtgvDataView.Columns["Estado"].SummaryItem.Tag = 4;
                    dtgvDataView.UpdateSummary();
                    dtgvDataView.BestFitColumns();
               }
               else
               {
                    dtgvData.DataSource = null;
               }
           
        }

        private void lvEmpleado_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!lvEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvEmpleado.SelectedItems[0];

                txtIdEmpleado.Text = ItemActual.Text;
                txtEmpleado.Text = ItemActual.SubItems[1].Text;
                txtNumero.Text = txtIdEmpleado.Text;
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                splitContainer1.SplitterDistance = 77;
                buscar(Convert.ToInt32(txtIdEmpleado.Text));
            }
       
        }

        private void txtIMEI_Leave(object sender, EventArgs e)
        {
            if (txtIMEI.Text.Length == 15)
            {
                if (imei != txtIMEI.Text)
                {
                    string resultado = clsSistemasBL.Instancia.ValidaTelefono(txtIMEI.Text);
                    if (resultado == "OK")
                    {
                        DataTable dt = new DataTable();
                        dt = clsSistemasBL.Instancia.GetTelefono(txtIMEI.Text);
                        txtIdCelular.Text = dt.Rows[0]["Id"].ToString();
                        txtMarca.Text = dt.Rows[0]["Marca"].ToString();
                        txtModelo.Text = dt.Rows[0]["Modelo"].ToString();
                    }
                    else
                    {
                        txtIdCelular.Text = "0";
                        txtMarca.Text = "";
                        txtModelo.Text = "";
                        MessageBox.Show(resultado + "No se ha cargado ningún teléfono", "Mensaje");
                    }
                }
            }
            else
            {
                txtIdCelular.Text = "0";
                txtMarca.Text = "";
                txtModelo.Text = "";
                MessageBox.Show("No ha ingresado ningún equipo (IMEI), esto no imposibilita guardar el registro", "Mensaje");
            }
        }

        private void ActivaControles(bool valor)
        {
            txtEmpleado.Text = "";
            txtIdEmpleado.Text = "0";
            txtNumero.Text = "";
            txtNumero.Enabled = valor;
            txtIMEI.Text = "";
            txtIMEI.Enabled = valor;
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtObservaciones.Text = "";
            txtIdCelular.Text = "0";
            btnGuardar.Visible = valor;
            btnCancelar.Visible = valor;

            boton = ' ';
            id = "";
            idempleado = "";
            numero = "";
            idtelefono = "";
            imei = "";
        }

        private void OcultaControles(bool valor)
        {
            panelControl1.Visible = false;
            if (valor == false)
            {
                btnExcel.Location = new Point(this.Width - 100, 26);
            }
            else
            {
                btnExcel.Location = new Point(1101, 26);
            }
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            AgregarTelefono();
        }
        private void AgregarTelefono() 
        {
            if (frmTelefonos == null || frmTelefonos.IsDisposed)
            {
                frmTelefonos = new frmTelefonos();
                frmTelefonos.origen = boton;
                frmTelefonos.Show();
            }
            else
            {
                frmTelefonos.Activate();
            }
        }

        private void CargarTelefono(Boolean EsCorrecto)
        {
            if (EsCorrecto)
            {
                txtIMEI.Text = frmTelefonos.imeitransf;

                if (imei != txtIMEI.Text)
                {
                    string resultado = clsSistemasBL.Instancia.ValidaTelefono(txtIMEI.Text);
                    if (resultado == "OK")
                    {
                        DataTable dt = new DataTable();
                        dt = clsSistemasBL.Instancia.GetTelefono(txtIMEI.Text);
                        txtIdCelular.Text = dt.Rows[0]["Id"].ToString();
                        txtMarca.Text = dt.Rows[0]["Marca"].ToString();
                        txtModelo.Text = dt.Rows[0]["Modelo"].ToString();
                    }
                    else
                    {
                        txtIdCelular.Text = "0";
                        txtMarca.Text = "";
                        txtModelo.Text = "";
                        txtIMEI.Text = "";
                        MessageBox.Show(resultado + "No se ha cargado ningún teléfono", "Mensaje");
                    }
                }

                txtIMEI.Focus();
            }
            else
            {
                btnCancelar.PerformClick();
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ActivaControles(true);
            boton = 'N';
            btnNuevo.Visible = false;
            btnModificar.Visible = false;
            btnEliminar.Visible = false;
            txtEmpleado.Focus();
        }
        private frmModificarAsignacion frmModificarAsignacion;
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (txtIdEmpleado.Text!="")
            { 
            frmModificarAsignacion = new frmModificarAsignacion();
            frmModificarAsignacion.idempleado = Convert.ToInt32(txtIdEmpleado.Text);
            frmModificarAsignacion.empleado = txtEmpleado.Text;
            frmModificarAsignacion.ShowDialog();
            } 
            else
            {
                MessageBox.Show("Debe seleccionar a un empleado de la lista o buscar uno.");
            } 
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dtgvDataView.GetFocusedRowCellValue("Estado").ToString() == "ACTIVA")
            {
                DialogResult dialogResult = MessageBox.Show("Este proceso eliminará el registro de este empleado y liberará el número y equipo asignados," +
                                         "¿Desea continuar?", "Confirmación", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    ActivaControles(true);
                    boton = 'E';
                    btnNuevo.Visible = false;
                    btnModificar.Visible = false;
                    btnEliminar.Visible = false;

                    txtEmpleado.Text = dtgvDataView.GetFocusedRowCellValue("NombreCompleto").ToString();
                    txtIdEmpleado.Text = dtgvDataView.GetFocusedRowCellValue("Persona").ToString();
                    txtNumero.Text = dtgvDataView.GetFocusedRowCellValue("Numero").ToString();
                    txtIMEI.Text = dtgvDataView.GetFocusedRowCellValue("IMEI").ToString();
                    txtIdCelular.Text = dtgvDataView.GetFocusedRowCellValue("Telefono").ToString();

                    txtMarca.Text = dtgvDataView.GetFocusedRowCellValue("Marca").ToString();
                    txtModelo.Text = dtgvDataView.GetFocusedRowCellValue("Modelo").ToString();
                    txtObservaciones.Text = dtgvDataView.GetFocusedRowCellValue("Observaciones").ToString();
                    dtpFecha.Text = dtgvDataView.GetFocusedRowCellValue("Fecha Asignación").ToString();

                    txtEmpleado.Enabled = false;
                    txtNumero.Enabled = false;
                    txtIMEI.Enabled = false;
                    txtMarca.Enabled = false;
                    txtModelo.Enabled = false;
                    txtObservaciones.Enabled = false;
                    dtpFecha.Enabled = false;
                }
            }
            else
            {
                
                MessageBox.Show("No puede eliminar una asignación inactiva", "Mensaje");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            ActivaControles(false);
            btnNuevo.Visible = true;
            btnModificar.Visible = true;
            btnEliminar.Visible = true;
        }


        private Microsoft.Office.Interop.Excel.Application app;

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Listado teléfonos del personal " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                app = new Microsoft.Office.Interop.Excel.Application();
                app.Visible = true;
                app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));
            }
        }

        private void dtgvDataView_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            // ID = TAG 
            int summaryID = Convert.ToInt32((e.Item as GridSummaryItem).Tag);
            GridView View = sender as GridView;

            // INICIALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                sumaActivos = 0;
                sumaInactivos = 0;
                CuentaActivos = 0;
                NumeroCelulares = new List<string>();
            }
            // CALCULO 
            if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                switch (summaryID)
                {
                    case 1:
                        if (View.GetRowCellValue(e.RowHandle, "Estado").ToString() == "ACTIVA") sumaActivos += Convert.ToDecimal(e.FieldValue);
                        break;
                    case 2:
                        if (View.GetRowCellValue(e.RowHandle, "Estado").ToString() == "INACTIVA") { sumaInactivos += Convert.ToDecimal(e.FieldValue); }
                        break;
                    case 3:
                        NumeroCelulares.Add(View.GetRowCellValue(e.RowHandle, "Numero").ToString());
                        break;
                    case 4:
                        if (View.GetRowCellValue(e.RowHandle, "Estado").ToString() != "") { CuentaActivos = CuentaActivos + 1; }
                        break;
                }
            }
            // FINALIZACION 
            if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                switch (summaryID)
                {
                    case 1:
                        e.TotalValue = sumaActivos;
                        break;
                    case 2:
                        e.TotalValue = sumaInactivos;
                        break;
                    case 3:
                        e.TotalValue = NumeroCelulares.Distinct().Count();
                        break;
                    case 4:
                        e.TotalValue = CuentaActivos;
                        break;
                }
            }     
        }

        private void txtNumero_Click(object sender, EventArgs e)
        {

        }

        private void dtgvData_Click(object sender, EventArgs e)
        {
            txtEmpleado.Text = (dtgvDataView.GetFocusedRowCellValue("Nombre").ToString()).Trim();
            txtIdEmpleado.Text = (dtgvDataView.GetFocusedRowCellValue("IDPersona").ToString()).Trim();
            txtNumero.Text = (txtIdEmpleado.Text).Trim();
        }

        private void txtEmpleado_Click(object sender, EventArgs e)
        {

        }

        private void txtEmpleado_TextChanged(object sender, EventArgs e)
        {
            int length = txtEmpleado.Text.Length;
            if (length==0)
            { 
                buscar(0);
                txtIdEmpleado.Text ="";
                txtNumero.Text = "";
            }
        }

        private void lvEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
