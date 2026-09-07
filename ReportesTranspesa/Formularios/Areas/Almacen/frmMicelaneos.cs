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
using ReportesTranspesa.Sistema;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrintingLinks;
using Comun;



namespace ReportesTranspesa.Formularios.Areas.Almacen
{
    public partial class frmMicelaneos : Form
    {
        string NumeroTicket;
        string FechaInicio;
        string FechaFinal;
        string RazonSocial;

        decimal PesoInicial; decimal PesoFinal; decimal CantidadBase; decimal CantidadUso; string GuiaC; string GuiaT; string FechaIniciop; string FechafinP; string Lote; string Ubicacion;
        int ot;
        bool permitir = true;
        string NumeroPesaje;
        DataTable dtPermisos;

        public bool AccesoAgregar;
        public bool AccesoModifica;
        public bool Accesoquitar;
        public frmMicelaneos()
        {
            InitializeComponent();
        }
        /*-----------------CARGA LA DATA CUANDO ABRES EL MODULO EN LA PARTE DE MODIFICAR  FECHA --------------------------*/
        private void frmMicelaneos_Load(object sender, EventArgs e)
        {


            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("MISCELANEOS");



            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    btnBuscar.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]);
                    BtnBuscarRazonSocial.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]);
                    btnGuardar.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]);
                    //button1.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]);
                    btnSave.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]);
                    //btnCancel.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]);
                    btnBuscarTick.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]);
                    btnTransferir.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]);
                }
            }

            dtFechainicio.CustomFormat = "dd/MM/yyyy";
            dtFechainicio.Format = DateTimePickerFormat.Custom;

            dtFechaFin.CustomFormat = "dd/MM/yyyy";
            dtFechaFin.Format = DateTimePickerFormat.Custom;


            DataTable dtModTicket = new DataTable();
            dtgvData.DataSource = null;
            dtModTicket = clsAlmacenBL.Instancia.GetListarTicketTop30();
            if (dtModTicket.Rows.Count > 0)
            {

                dtgvData.DataSource = dtModTicket;
                dtViewFecha.Columns["Horas"].Visible = false;
                dtViewFecha.Columns["Horai"].Visible = false;
                dtViewFecha.Columns["Fechai"].Visible = false;
                dtViewFecha.Columns["Fechas"].Visible = false;
                dtViewFecha.Columns["NroSalida"].Visible = false;
                dtViewFecha.Columns["IdSalida"].Visible = false;
                dtViewFecha.BestFitColumns();
            }

        }

        void refresh()
        {
            dtFechainicio.CustomFormat = "dd/MM/yyyy";
            dtFechainicio.Format = DateTimePickerFormat.Custom;

            dtFechaFin.CustomFormat = "dd/MM/yyyy ";
            dtFechaFin.Format = DateTimePickerFormat.Custom;


            DataTable dtModTicket = new DataTable();
            dtgvData.DataSource = null;
            dtModTicket = clsAlmacenBL.Instancia.GetListarTicketTop30();
            if (dtModTicket.Rows.Count > 0)
            {

                dtgvData.DataSource = dtModTicket;
                dtViewFecha.Columns["Horas"].Visible = false;
                dtViewFecha.Columns["Horai"].Visible = false;
                dtViewFecha.Columns["NroSalida"].Visible = false;
                dtViewFecha.Columns["IdSalida"].Visible = false;
                dtViewFecha.BestFitColumns();
            }
        }
        /*VALIDA NUMERO Y PUNTO*/
        public static void ValidarTextbox(KeyPressEventArgs e)
        {
            CultureInfo cc = System.Threading.Thread.CurrentThread.CurrentCulture;

            if (char.IsNumber(e.KeyChar) || e.KeyChar.ToString() == cc.NumberFormat.NumberDecimalSeparator)

                e.Handled = false;

            else

                e.Handled = true;
        }

        public bool solonumerosPrecio(int code)
        {
            bool resultado;

            if (code == 46 && (txtCantidadBase.Text.Contains(".") && txtPesoInicial.Text.Contains(".") && txtPesoFinal.Text.Contains(".") && txtCantidadUso.Text.Contains(".")))//se evalua si es punto y si es punto se revisa si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;

        }

        /*-----------------BUSCA LOS TICKETS DESDE TAL FECHA INI HASTA FECHA SALIDA SELECCIONADA EN LA PESTAÑA 'LISTAR TICKETS' --------------------------*/
        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            if (dtpFechaIni.Value.Date > dtpFechaFin.Value.Date)
            {
                MessageBox.Show("La fecha final tiene que ser mayor que la fecha inicio");
                return;
            }

            dtgListarTicket.DataSource = null;
            dtgvListarTicket.Columns.Clear();
            System.Data.DataTable dtlistTicket = new System.Data.DataTable();
            dtlistTicket = clsAlmacenBL.Instancia.GetListarTicket(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            dtpFechaFin.Value.ToShortDateString() + " 23:59:59");


            if (dtlistTicket.Rows.Count > 0)
            {
                dtgListarTicket.DataSource = dtlistTicket;
                dtgvListarTicket.ExpandAllGroups();
                dtgvListarTicket.BestFitColumns();

            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }


        /*-----------------EXPORTAR A EXCEL LOS DATOS MOSTRADOS EN EL DATAVIEW--------------------------*/
        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvListarTicket.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de ticket " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + Environment.UserName.ToUpper() + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvListarTicket.ExportToXlsx(nombre);
                Process.Start(nombre);
            }

        }
        /*-----------------GRABA LA FECHA MODIFICADA  --------------------------*/
        private void button1_Click(object sender, EventArgs e)
        {
            //dtViewFecha.ClearSelection();
            if (dtFechainicio.Value.Date > dtFechaFin.Value.Date)
            {
                MessageBox.Show("La fecha final tiene que ser mayor que la fecha inicio");
                return;
            }
            if (dtViewFecha.SelectedRowsCount == 0)
            {
                
                MessageBox.Show("Tienes que seleccionar por lo menos una fila.");
                return;
            }
            else
            {
                DataTable dtModificarTicket = new DataTable();
                string Respuesta;
                dtModificarTicket = clsAlmacenBL.Instancia.GetModificarFecha(dtFechainicio.Text,
                 dtFechaFin.Text, txtTicket.Text, Environment.UserName.ToUpper());
                Respuesta = Convert.ToString(dtModificarTicket.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    refresh();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void dtgvDataView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            string NumeroTicket = txtTicket.Text;
            int[] filass = dtViewFecha.GetSelectedRows();
            string datoseleccionado = dtViewFecha.GetFocusedValue().ToString();

            for (int i = 0; i < filass.Length; i++)
            {
                txtTicket.Text = dtViewFecha.GetRowCellValue(filass[i], "NroSalida").ToString();
            }
        }

        private void dtgvData_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int[] filass = dtViewFecha.GetSelectedRows();
                string datoseleccionado = dtViewFecha.GetFocusedValue().ToString();
                for (int i = 0; i < filass.Length; i++)
                {
                    NumeroTicket = dtViewFecha.GetRowCellValue(filass[i], "NroSalida").ToString();
                    FechaInicio = dtViewFecha.GetRowCellValue(filass[i], "Fechai").ToString();
                    FechaFinal = dtViewFecha.GetRowCellValue(filass[i], "Fechas").ToString();
                }
                txtTicket.Text = NumeroTicket;
                dtFechainicio.Text = FechaInicio;
                dtFechaFin.Text = FechaFinal;
            }
            catch (Exception)
            {

            }
        }

        /*-----------------------------------GRABA LA MODIFICACION DEL  TICKET DE BALANZA----------------------------------------------------------*/


        private void button1_Click_1(object sender, EventArgs e)
        {

            if (txtPesoInicial.Text == "" && txtCantidadBase.Text == "" && txtCantidadUso.Text == "" && txtPesoFinal.Text == "")
            {
                MessageBox.Show("Los campos  tienen que estar cubiertos");

            }

            else
            {
                DataTable dtModTicket = new DataTable();
                string Respuesta;
                dtModTicket = clsAlmacenBL.Instancia.GetModificarTicket(Convert.ToInt32(txtOT.Text), txtNumeroPeaje.Text, Convert.ToDecimal(txtPesoInicial.Text), Convert.ToDecimal(txtPesoFinal.Text),
                                                                        Convert.ToDecimal(txtCantidadBase.Text), Convert.ToDecimal(txtCantidadUso.Text), txtGuiaC.Text, TxtGuiaT.Text, Environment.UserName.ToUpper(),txtFechaInicio.Text,txtFechaFin.Text,txtLote.Text,txtUbicacion.Text);
                Respuesta = Convert.ToString(dtModTicket.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtOT.ReadOnly = false;
                    txtNumeroPeaje.ReadOnly = false;
                }
                else
                {

                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (txtOT.Text != "" && txtNumeroPeaje.Text != "")
            {
                dtModificarTicket.DataSource = null;
                gwModificaTicket.Columns.Clear();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsAlmacenBL.Instancia.GetListarTickets(Convert.ToInt32(txtOT.Text), txtNumeroPeaje.Text);
                if (dt.Rows.Count > 0)
                {
                    dtModificarTicket.DataSource = dt;
                    gwModificaTicket.Columns["IdOT"].Visible = false;
                    txtPesoFinal.ReadOnly = false;
                    txtPesoInicial.ReadOnly = false;
                    txtCantidadBase.ReadOnly = false;
                    txtCantidadUso.ReadOnly = false;
                    txtGuiaC.ReadOnly = false;
                    TxtGuiaT.ReadOnly = false;
                    txtFechaFin.ReadOnly = false;
                    txtFechaInicio.ReadOnly = false;
                }
                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hay data para mostrar";
                    m.ShowDialog();

                }
            }
            else
            {
                MessageBox.Show("Ingrese datos correctamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            limpiarTextBoxes(this);

            // Y opcionalmente…
            this.txtOT.Focus();
            //this.dtModificarTicket.Focus();
        }

        //Limpiar de manera rapida
        private void limpiarTextBoxes(Control parent)
        {


            TextBox t;

            //Limpiar de manera rapida
            foreach (Control c in parent.Controls)
            {

                t = c as TextBox;

                if (t != null)
                {
                    t.Clear();
                    gwModificaTicket.Columns.Clear();
                }

                if (c.Controls.Count > 0)
                {
                    limpiarTextBoxes(c);
                }
            }


            txtOT.ReadOnly = false;
            txtNumeroPeaje.ReadOnly = false;
            txtGuiaC.ReadOnly = true;
            TxtGuiaT.ReadOnly = true;
            txtPesoFinal.ReadOnly = true;
            txtPesoInicial.ReadOnly = true;
            txtCantidadBase.ReadOnly = true;
            txtCantidadUso.ReadOnly = true;
        }




        private void dtModificarTicket_DoubleClick(object sender, EventArgs e)
        {

            try
            {
                int[] filass = gwModificaTicket.GetSelectedRows();
                string datoseleccionado = gwModificaTicket.GetFocusedValue().ToString();

                for (int i = 0; i < filass.Length; i++)
                {
                    PesoInicial = Convert.ToDecimal(gwModificaTicket.GetRowCellValue(filass[i], "PesoInicial").ToString());
                    CantidadBase = Convert.ToDecimal(gwModificaTicket.GetRowCellValue(filass[i], "CantidadBase").ToString());
                    CantidadUso = Convert.ToDecimal(gwModificaTicket.GetRowCellValue(filass[i], "CantidadUso").ToString());
                    PesoFinal = Convert.ToDecimal(gwModificaTicket.GetRowCellValue(filass[i], "PesoFinal").ToString());
                    GuiaC = gwModificaTicket.GetRowCellValue(filass[i], "Guia1").ToString();
                    GuiaT = gwModificaTicket.GetRowCellValue(filass[i], "Guia2").ToString();
                    FechaIniciop = gwModificaTicket.GetRowCellValue(filass[i], "FechaInicio").ToString();
                    FechafinP = gwModificaTicket.GetRowCellValue(filass[i], "FechaFin").ToString();
                    Lote = gwModificaTicket.GetRowCellValue(filass[i], "Lote").ToString();
                    Ubicacion = gwModificaTicket.GetRowCellValue(filass[i], "Ubicacion").ToString();

                }
                txtOT.ReadOnly = true;
                txtNumeroPeaje.ReadOnly = true;
                txtPesoInicial.Text = Convert.ToString(PesoInicial);
                txtCantidadBase.Text = Convert.ToString(CantidadBase);
                txtCantidadUso.Text = Convert.ToString(CantidadUso);
                txtPesoFinal.Text = Convert.ToString(PesoFinal);
                txtGuiaC.Text = GuiaC;
                TxtGuiaT.Text = GuiaT;
                txtFechaInicio.Text = FechaIniciop;
                txtFechaFin.Text = FechafinP;
                txtFechaInicio.ReadOnly = false;
                txtFechaFin.ReadOnly = false;
                txtGuiaC.ReadOnly = false;
                TxtGuiaT.ReadOnly = false;
                txtLote.Text = Lote;
                txtLote.ReadOnly = false;
                txtUbicacion.Text = Ubicacion;
                txtUbicacion.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void BtnBuscarRazonSocial_Click(object sender, EventArgs e)
        {

            if (txtRazonSocial2.Text != "")
            {

                dtPersonaSpring.DataSource = null;
                dtgvConductor.DataSource = null;
                dtPersonaSalaverry.DataSource = null;

                dtPersonaSpring.Columns.Clear();
                dtPersonaSalaverry.Columns.Clear();
                dtgvConductor.Columns.Clear();

                System.Data.DataTable dtPers = new System.Data.DataTable();
                dtPers = clsAlmacenBL.Instancia.GetListarClientes(Convert.ToInt32(txtCodigo2.Text), txtRazonSocial2.Text, txtDocumentoFiscal.Text);
                if (dtPers.Rows.Count > 0)
                {
                    dtPersonaSpring.DataSource = dtPers;
                }
                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hay data para mostrar";
                    m.ShowDialog();
                }

                System.Data.DataTable dtPersSalaverry = new System.Data.DataTable();
                dtPersSalaverry = clsAlmacenBL.Instancia.GetListarClientesSalaverry(Convert.ToInt32(txtCodigo2.Text), txtRazonSocial2.Text, txtDocumentoFiscal.Text);
                if (dtPersSalaverry.Rows.Count > 0)
                {
                    dtPersonaSalaverry.DataSource = dtPersSalaverry;
                }
                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "La persona aun no esta en la base de datos de Salaverry";
                    m.ShowDialog();
                }

                System.Data.DataTable dtConductor = new System.Data.DataTable();
                dtConductor = clsAlmacenBL.Instancia.GetListarConductoressSalaverry(txtDocumentoFiscal.Text);
                if (dtConductor.Rows.Count > 0)
                {
                    dtgvConductor.DataSource = dtConductor;
                }
                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "El coductor aun no esta registrado";
                    m.ShowDialog();
                }


            }
            else
            {
                MessageBox.Show("Ingresar datos para buscar a la persona", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }





        }


        private void lsRazonSocial_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lsRazonSocial.SelectedItems[0];
            RazonSocial = ItemActual.Text;
            txtRazonSocial2.Text = ItemActual.SubItems[0].Text;
            txtCodigo2.Text = ItemActual.SubItems[1].Text;
            txtDocumentoFiscal.Text = ItemActual.SubItems[2].Text;
            lsRazonSocial.Visible = false;
            txtRazonSocial2.Focus();
        }

        private void txtRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lsRazonSocial, clsConsultaBL.Instancia.GetRazonSocial(txtRazonSocial2.Text), true, false, false);

                lsRazonSocial.Columns[0].Width = 206;
                lsRazonSocial.Columns[1].Width = 100;
                lsRazonSocial.Columns[2].Width = 110;

                lsRazonSocial.Size = new System.Drawing.Size(351, 103);

                lsRazonSocial.BringToFront();
                lsRazonSocial.Visible = true;
                lsRazonSocial.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lsRazonSocial.Visible = false;
                txtRazonSocial2.Focus();

            }
        }

        void MostrarUltimoClienteTransferido()
        {
            dtPersonaSalaverry.DataSource = null;
            dtPersonaSalaverry.Columns.Clear();
            System.Data.DataTable dtUltimoCli = new System.Data.DataTable();
            dtUltimoCli = clsAlmacenBL.Instancia.GetListarUltimoCliente();
            if (dtUltimoCli.Rows.Count > 0)
            {
                dtPersonaSalaverry.DataSource = dtUltimoCli;



            }

        }



        /*-------------------------TRANSFIERE EL CLIENTE A LA BD SALAVERRY-------------------------*/
        private void btnTransferir_Click(object sender, EventArgs e)
        {

            if (dtPersonaSpring.CurrentRow == null)
            {
                MessageBox.Show("Seleccionar el cliente de la lista mostrada ");
                return;
            }

            DataTable dtPersonSpring = new DataTable();
            string Respuesta;
            dtPersonSpring = clsAlmacenBL.Instancia.GetTransferirCliente(Convert.ToInt32(txtCodigo2.Text), Environment.UserName.ToUpper());
            Respuesta = Convert.ToString(dtPersonSpring.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarUltimoClienteTransferido();

            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lsRazonSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                ListViewItem ItemActual;
                ItemActual = lsRazonSocial.SelectedItems[0];
                RazonSocial = ItemActual.Text;
                txtRazonSocial2.Text = ItemActual.SubItems[0].Text;
                txtCodigo2.Text = ItemActual.SubItems[1].Text;
                txtDocumentoFiscal.Text = ItemActual.SubItems[2].Text;
                lsRazonSocial.Visible = false;
                txtRazonSocial2.Focus();
            }
        }

        private void dtgvData_Click(object sender, EventArgs e)
        {
            try
            {
                int[] filass = dtViewFecha.GetSelectedRows();
                string datoseleccionado = dtViewFecha.GetFocusedValue().ToString();
                for (int i = 0; i < filass.Length; i++)
                {
                    NumeroTicket = dtViewFecha.GetRowCellValue(filass[i], "NroSalida").ToString();
                    FechaInicio = dtViewFecha.GetRowCellValue(filass[i], "Fechai").ToString();
                    FechaFinal = dtViewFecha.GetRowCellValue(filass[i], "Fechas").ToString();
                }
                txtTicket.Text = NumeroTicket;
                dtFechainicio.Text = FechaInicio;


                dtFechaFin.Text = FechaFinal;
            }
            catch (Exception)
            {

            }
        }

        private void txtPesoInicial_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Escape)
            {
                btnSave.PerformClick();
            }
            else
            {
                ValidarTextbox(e);
                e.Handled = solonumerosPrecio(Convert.ToInt32(e.KeyChar));
            }
        }

        private void txtCantidadBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Escape)
            {
                btnSave.PerformClick();
            }
            else
            {
                ValidarTextbox(e);
                e.Handled = solonumerosPrecio(Convert.ToInt32(e.KeyChar));
            }

        }

        private void txtPesoFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Escape)
            {
                btnSave.PerformClick();
            }
            else
            {
                ValidarTextbox(e);
                e.Handled = solonumerosPrecio(Convert.ToInt32(e.KeyChar));
            }
        }

        private void txtCantidadUso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Escape)
            {
                btnSave.PerformClick();
            }
            else
            {
                ValidarTextbox(e);
                e.Handled = solonumerosPrecio(Convert.ToInt32(e.KeyChar));
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            limpiarTextBoxes2(this);

            // Y opcionalmente…
            this.txtTicket.Focus();

        }

        //Limpiar de manera rapida
        private void limpiarTextBoxes2(Control parent)
        {
            TextBox t;
            DateTimePicker X;
            //Limpiar de manera rapida
            foreach (Control c in parent.Controls)
            {
                t = c as TextBox;
                if (t != null)
                {
                    t.Clear();
                    dtpFechaIni.Value = DateTime.Now;
                    dtFechaFin.Value = DateTime.Now;
                }
                if (c.Controls.Count > 0)
                {
                    limpiarTextBoxes(c);
                }
            }
        }



        void Mostrar_UltimoConductor()
        {
            dtgvConductor.DataSource = null;
            dtgvConductor.Columns.Clear();
            System.Data.DataTable dtUltimoCondcutorReg = new System.Data.DataTable();
            dtUltimoCondcutorReg = clsAlmacenBL.Instancia.GetListarUltimoConductor();
            if (dtUltimoCondcutorReg.Rows.Count > 0)
            {
                dtgvConductor.DataSource = dtUltimoCondcutorReg;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            DataTable dtPersonSpring = new DataTable();
            string Respuesta;
            dtPersonSpring = clsAlmacenBL.Instancia.GetRegistrarConductor(txtDocumento.Text, Environment.UserName.ToUpper());
            Respuesta = Convert.ToString(dtPersonSpring.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mostrar_UltimoConductor();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRegistrarConductor.Checked)
            {
                btnRegistrarConductor.Enabled = true;
                var result = (MessageBox.Show("Desea registrar al conducto:", "Registro de Conductor", MessageBoxButtons.OKCancel));
                if (result == DialogResult.Cancel)
                {
                    btnRegistrarConductor.Enabled = false;
                    chkRegistrarConductor.Checked = false;
                }
            }
            else
            {
                btnRegistrarConductor.Enabled = false;
            }
        }
        private void dtPersonaSalaverry_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //txtDocumento.Text = txtDocumento.Text.Trim();
            txtDocumento.Text = dtPersonaSalaverry.Rows[e.RowIndex].Cells["Documento"].Value.ToString();
        }

        private void Transferir_Cliente_Click(object sender, EventArgs e)
        {

        }

        private void dtPersonaSalaverry_SelectionChanged(object sender, EventArgs e)
        {

            txtDocumento.Text = dtPersonaSalaverry.CurrentRow.Cells["Documento"].Value.ToString();
        }

    }
}
