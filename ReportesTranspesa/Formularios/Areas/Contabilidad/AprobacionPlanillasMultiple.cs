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
using DevExpress.Utils;
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using Entidades;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Contabilidad
{
    public partial class AprobacionPlanillasMultiple : MetroFramework.Forms.MetroForm
    {
        public AprobacionPlanillasMultiple()
        {
            InitializeComponent();
        }

        DateTime FechaObligacion;
        string usuario = "";
        int cliente = -1;

        private void AprobacionPlanillasMultiple_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            cboUsuario.SelectedIndex = 0;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            #region Lista Planillas
            string usuario = "";
            switch (cboUsuario.SelectedIndex)
            {
                case 0: usuario = "";
                    break;

                case 1: usuario = cboUsuario.Text;
                    break;

                case 2: usuario = cboUsuario.Text;
                    break;
            }
            if (txtCliente.Text.Trim() == "")
            {
                cliente = -1;
            }
            //usuario = cboUsuario.Text;
            dtgvData.DataSource = null;
            dtgvDataAprobadas.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataAprobadasView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt1 = clsContabilidadBL.Instancia.GetCListaPlanillas(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", "Por_Aprobar", "", cliente);
            dt2 = clsContabilidadBL.Instancia.GetCListaPlanillas(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", "Aprobadas", usuario, cliente);
            if (dt1.Rows.Count > 0 || dt2.Rows.Count > 0)
            {
                dtgvData.DataSource = dt1;
                dtgvDataView.BestFitColumns();
                dtgvDataAprobadas.DataSource = dt2;
                string filtro = "";
                int contafiltros = 0;
                if (contafiltros > 0)
                {
                    filtro = filtro + " AND ";
                }
                filtro = filtro + "[NumeroAdelanto] LIKE '%" + txtAdelanto.Text + "%'"; 
                contafiltros = contafiltros + 1;
                if (filtro != "")
                {
                    dtgvDataAprobadasView.Columns["NumeroAdelanto"].FilterInfo = new ColumnFilterInfo(filtro);
                }
                string filtro2 = "";
                int contafiltros2 = 0;
                if (contafiltros2 > 0)
                {
                    filtro2 = filtro2 + " AND ";
                }
                filtro2 = filtro2 + "[NumeroDocumento] LIKE '%" + txtDocumento.Text + "%'";
                contafiltros2 = contafiltros2 + 1;
                if (filtro2 != "")
                {
                    dtgvDataAprobadasView.Columns["NumeroDocumento"].FilterInfo = new ColumnFilterInfo(filtro2);
                }
                dtgvDataAprobadasView.Columns["Monto"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataAprobadasView.Columns["Monto"].DisplayFormat.FormatString = "c2";
                dtgvDataAprobadasView.Columns["MontoObligacion"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataAprobadasView.Columns["MontoObligacion"].DisplayFormat.FormatString = "c2";
                dtgvDataAprobadasView.Columns["MontoAdelantos"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvDataAprobadasView.Columns["MontoAdelantos"].DisplayFormat.FormatString = "c2";
                dtgvDataAprobadasView.Columns["CodigoProveedor"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CodigoProveedor", "Total de Planillas = {0}");
                dtgvDataAprobadasView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
            #endregion
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardaPlanilla();
            MuestraPlanillas();
            //foreach (int indice in dtgvDataView.GetSelectedRows())
            //{
            //    //DateTime año = DateTime.Now;
            //    DateTime año = DateTime.Now.AddYears(-1);
            //    var años = año.Year+"12";
            //    var documento = dtgvDataView.GetRowCellValue(indice, "NumeroDocumento").ToString();
            //    var periodo = dtgvDataView.GetRowCellValue(indice, "Voucher").ToString().Trim();
            //    //MessageBox.Show(años+"12");
            //    if (periodo == años)
            //    {
            //        ConfirmacionAprobacion frm = new ConfirmacionAprobacion();
            //        frm.Show();
            //    }
            //    //else
            //    //{
            //    //    Mensaje m = new Mensaje();
            //    //    m.mensaje = "El Periodo: " + periodo.Trim() + " es Correcto " + Environment.NewLine + " del Documento. " + documento + "";
            //    //    m.ShowDialog();
            //    //}
            //}
        }

        public void ValidarPeriodo()
        {
            foreach (int indice in dtgvDataView.GetSelectedRows())
            {
                //DateTime año = DateTime.Now;
                DateTime año = DateTime.Now.AddYears(-1);
                var años = año.Year + "12";
                var documento = dtgvDataView.GetRowCellValue(indice, "NumeroDocumento").ToString();
                var periodo = dtgvDataView.GetRowCellValue(indice, "Voucher").ToString().Trim();
                //MessageBox.Show(años+"12");
                if (periodo == años)
                {
                    ConfirmacionAprobacion frm = new ConfirmacionAprobacion();
                    frm.Show();
                }
                //else
                //{
                //    Mensaje m = new Mensaje();
                //    m.mensaje = "El Periodo: " + periodo.Trim() + " es Correcto " + Environment.NewLine + " del Documento. " + documento + "";
                //    m.ShowDialog();
                //}
            }
        }

        public void GuardaPlanilla()
        {
            DataTable dt = new DataTable();
            int renglonesSeleccionados = dtgvDataView.SelectedRowsCount;
            if (renglonesSeleccionados == 0)
            {
                MessageBox.Show("Tienes que seleccionar por lo menos una fila.");
            }
            else
            {
                foreach (int indice in dtgvDataView.GetSelectedRows())
                {
                    //try
                    //
                        var documento = dtgvDataView.GetRowCellValue(indice, "NumeroDocumento").ToString();
                        var proveedor = dtgvDataView.GetRowCellValue(indice, "CodigoProveedor").ToString();
                        FechaObligacion = DateTime.Now;
                        usuario = Utilitario.Instancia.SesionUsuario.usuario;
                        var NumeroCaja = Convert.ToInt32(documento.Substring(5, 6));
                        clsClaseCompartida.CajaChica = NumeroCaja;
                        clsClaseCompartida.Obligacion = documento;
                        clsClaseCompartida.Proveedor = Convert.ToInt32(proveedor);
                        //MessageBox.Show(documento + FechaObligacion.ToString().Substring(0, 20) + " " + proveedor + " " + usuario + " " + NumeroCaja);
                        //ValidarPeriodo();
                        ///////////////////////////////////////////VALIDAR PERIODO DE OBLIGACION//////////////////////////////////////////////////
                        #region Valida Periodo
                        //DateTime año = DateTime.Now.AddYears(-1);
                        //var años = año.Year + "12";
                        ////var documento = dtgvDataView.GetRowCellValue(indice, "NumeroDocumento").ToString();
                        //var periodo = dtgvDataView.GetRowCellValue(indice, "Voucher").ToString().Trim();
                        ////MessageBox.Show(años+"12");
                        //if (periodo == años)
                        //{
                        //    ConfirmacionAprobacion frm = new ConfirmacionAprobacion();
                        //    //frm.Show();
                        //    dt = clsContabilidadBL.Instancia.GetGuardaObligaciones(documento, proveedor, FechaObligacion.ToString().Substring(0, 20), usuario);
                        //    if (dt.Rows.Count > 0)
                        //    {
                        //        dtgvDataAprobadas.DataSource = dt;
                        //        dtgvDataAprobadasView.BestFitColumns();
                        //    }
                        //    frm.Show();
                        //}
                        //else
                        //{
                        //    dt = clsContabilidadBL.Instancia.GetGuardaObligaciones(documento, proveedor, FechaObligacion.ToString().Substring(0, 20), usuario);
                        //    if (dt.Rows.Count > 0)
                        //    {
                        //        dtgvDataAprobadas.DataSource = dt;
                        //        dtgvDataAprobadasView.BestFitColumns();
                        //        Mensaje m = new Mensaje();
                        //        m.mensaje = "Se Aprobarón las Obligaciones: ";
                        //        //m.mensaje = "Se Aprobo la Obligación: " + Environment.NewLine + documento;
                        //        m.ShowDialog();
                        //    }
                        //    else
                        //    {
                        //        Mensaje m = new Mensaje();
                        //        m.mensaje = "No Se Aprobo la Obligación";
                        //        m.ShowDialog();
                        //    }
                        //}
                        #endregion
                        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        dt = clsContabilidadBL.Instancia.GetGuardaObligaciones(documento, proveedor, FechaObligacion.ToString().Substring(0, 20), usuario);
                        if (dt.Rows.Count > 0)
                        {
                            dtgvDataAprobadas.DataSource = dt;
                            dtgvDataAprobadasView.BestFitColumns();
                            Mensaje m = new Mensaje();
                            //m.mensaje = "Se Aprobo la Obligación: " + documento;
                            m.mensaje = "Se Aprobarón las Obligaciones";
                            m.ShowDialog();
                        }
                        else
                        {
                            Mensaje m = new Mensaje();
                            m.mensaje = "No Se Aprobo la Obligación";
                            m.ShowDialog();
                        }
                    //}
                    //catch
                    //{
                    //    Mensaje m = new Mensaje();
                    //    m.mensaje = "Seleccionar bien el Documento";
                    //    m.ShowDialog();
                    //}
                }
            }
        }

        public void MuestraPlanillas()
        {
            string usuario = ""; 
            switch (cboUsuario.SelectedIndex)
            {
                case 0: usuario = "";
                    break;

                case 1: usuario = cboUsuario.Text;
                    break;

                case 2: usuario = cboUsuario.Text;
                    break;
            }
            if (txtCliente.Text.Trim() == "")
            {
                cliente = -1;
            }
            //usuario = cboUsuario.Text;
            dtgvData.DataSource = null;
            dtgvDataAprobadas.DataSource = null;
            dtgvDataView.Columns.Clear();
            dtgvDataAprobadasView.Columns.Clear();
            dtgvDataView.GroupSummary.Clear();
            System.Data.DataTable dt1 = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt1 = clsContabilidadBL.Instancia.GetCListaPlanillas(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", "Por_Aprobar", "", cliente);
            dt2 = clsContabilidadBL.Instancia.GetCListaPlanillas(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
                dtpFechaFin.Value.ToShortDateString() + " 23:59:59", "Aprobadas", usuario, cliente);
            if (dt1.Rows.Count > 0 || dt2.Rows.Count > 0)
            {
                dtgvData.DataSource = dt1;
                dtgvDataView.BestFitColumns();
                dtgvDataAprobadas.DataSource = dt2;
                dtgvDataAprobadasView.BestFitColumns();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvDataAprobadas.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Obligaciones Aprobadas " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                //string nombre = System.IO.Path.Combine(desktop, "Lista de Obligaciones Aprobadas " + dtpFechaIni.Value.ToShortDateString() + " al " + dtpFechaFin.Value.ToShortDateString() + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvDataAprobadas.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void txtCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvCliente, clsConsultaBL.Instancia.GetPersona(txtCliente.Text), true, false, false);
                //clsVisuales.Instancia.LlenarLw(lvCliente, clsConsultaBL.Instancia.GetConductores(txtCliente.Text), true, false, false);

                lvCliente.Columns[0].Width = 0;
                lvCliente.Columns[1].Width = 206;
                lvCliente.Columns[2].Width = 110;

                lvCliente.BringToFront();
                lvCliente.Visible = true;
                lvCliente.Focus();
                splitContainer1.SplitterDistance = lvCliente.Top + lvCliente.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void lvCliente_Enter(object sender, EventArgs e)
        {
            if (!lvCliente.Items.Count.Equals(0))
            {
                lvCliente.Items[0].Selected = true;
            }
        }

        private void lvCliente_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCliente.SelectedItems[0];

                cliente = Int32.Parse(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void lvCliente_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvCliente.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvCliente.SelectedItems[0];

                cliente = Int32.Parse(ItemActual.Text);
                txtCliente.Text = ItemActual.SubItems[1].Text;
                lvCliente.Visible = false;
                txtCliente.Focus();
                splitContainer1.SplitterDistance = 70;
            }
        }

        private void txtDocumento_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void dtgvData_Click(object sender, EventArgs e)
        {

        }

        private void dtgvDataAprobadas_Click(object sender, EventArgs e)
        {

        }
    }
}
