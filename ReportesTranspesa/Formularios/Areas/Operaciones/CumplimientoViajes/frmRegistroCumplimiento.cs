using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
﻿using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using System.IO;
using Microsoft.Office;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.CumplimientoViajes
{
    public partial class frmRegistroCumplimiento : Form
    {
        public int idRuta, OpcionTL = 0;
        public int xClick = 0, yClick = 0, xClick2 = 0, yClick2 = 0;
        DataTable dtListaCumplimiento = new DataTable();
        DataTable dtPermisos = new DataTable();

        public frmRegistroCumplimiento()
        {
            InitializeComponent();
            cbxGrupo.SelectedIndexChanged -= cbxGrupo_SelectedIndexChanged;
        }

        private void cbxGrupo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboGrupo(); }

        private void frmRegistroCumplimiento_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistroCumplimiento");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true)
                    {
                        btnAgregarRuta.Enabled = true;
                        btnNuevoRegistro.Enabled = true;
                    }
                    else
                    {
                        btnAgregarRuta.Enabled = false;
                        btnNuevoRegistro.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                    {
                        tsEliminar1.Enabled = true;
                        tsEliminar2.Enabled = true;
                        tsEliminar3.Enabled = true;
                        tsQuitarRuta.Enabled = true;
                    }
                    else
                    {
                        tsEliminar1.Enabled = false;
                        tsEliminar2.Enabled = false;
                        tsEliminar3.Enabled = false;
                        tsQuitarRuta.Enabled = false;
                    }
                }
            }

            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = DateTime.Now;
            dtpFechaInicio2.Value = new DateTime(dtpFechaInicio2.Value.Year, dtpFechaInicio2.Value.Month, 1);
            dtpFechaFin2.Value = DateTime.Now;
            btnBuscar_Click(sender, e);
            btnBuscar2_Click(sender, e);
        }


        private void CargarComboGrupo()
        {
            DataTable dtGrupo = new DataTable();
            if (OpcionTL == 1) { dtGrupo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarGrupoViaje(1); }
            if (OpcionTL == 2) { dtGrupo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarGrupoViaje(2); }
 
            cbxGrupo.DataSource = dtGrupo;
            cbxGrupo.DisplayMember = "Descripcion";
            cbxGrupo.ValueMember = "idGrupoViaje";
        }

        public void ListarRutas()
        {
            DataTable dtListaRutas = new DataTable();
            if (OpcionTL == 1) { dtListaRutas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarGrupoViaje(3); }
            if (OpcionTL == 2) { dtListaRutas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarGrupoViaje(4); }
            
            dtgListaRutas.DataSource = dtListaRutas;
            if (dtListaRutas.Rows.Count > 0)
            {
                dgvListaRutas.Columns["idGrupoViaje"].Visible = false;
                dgvListaRutas.BestFitColumns();
            }
        }

        public void ListarCumplimientos(int TipoViaje)
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                if (TipoViaje == 0)
                {
                    dtListaCumplimiento = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento(TipoViaje, dtpFechaInicio.Text, dtpFechaFin.Text);
                    dtgListarTodo.DataSource = dtListaCumplimiento;
                    if (dtListaCumplimiento.Rows.Count > 0)
                    {
                        dgvListarTodo.Columns["idCumplimiento"].Visible = false;
                        dgvListarTodo.Columns["idGrupoViaje"].Visible = false;

                        dgvListarTodo.Columns["PROY / REQ"].Summary.Clear();
                        dgvListarTodo.Columns["PROY / REQ"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PROY / REQ", "Total Req.: {0}");
                        dgvListarTodo.Columns["CUMPLIMIENTO"].Summary.Clear();
                        dgvListarTodo.Columns["CUMPLIMIENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CUMPLIMIENTO", "Total Cump.: {0}");

                        var Proyectado = dgvListarTodo.Columns["PROY / REQ"].SummaryItem.SummaryValue;
                        var Cumplimiento = dgvListarTodo.Columns["CUMPLIMIENTO"].SummaryItem.SummaryValue;
                        double Porcentaje;

                        if (Convert.ToString(Proyectado) == "" || Convert.ToString(Cumplimiento) == "") { Porcentaje = 0.00; }
                        else { Porcentaje = Math.Round(Convert.ToDouble(Cumplimiento) * 100 / Convert.ToDouble(Proyectado),2); }
                        dgvListarTodo.Columns["PORCENTAJE"].Summary.Clear();
                        dgvListarTodo.Columns["PORCENTAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCENTAJE", "Porcentaje: " + Convert.ToString(Porcentaje) + "%");

                        dgvListarTodo.BestFitColumns();
                    }
                }

                if (TipoViaje == 1)
                {
                    dtListaCumplimiento = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento(TipoViaje, dtpFechaInicio.Text, dtpFechaFin.Text);
                    dtgLimaNorteBG.DataSource = dtListaCumplimiento;
                    if (dtListaCumplimiento.Rows.Count > 0)
                    {
                        dgvLimaNorteBG.Columns["idCumplimiento"].Visible = false;
                        dgvLimaNorteBG.Columns["idGrupoViaje"].Visible = false;
                        dgvLimaNorteBG.Columns["GRUPO_VIAJE"].Visible = false;

                        dgvLimaNorteBG.Columns["PROY / REQ"].Summary.Clear();
                        dgvLimaNorteBG.Columns["PROY / REQ"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PROY / REQ", "Total Req.: {0}");
                        dgvLimaNorteBG.Columns["CUMPLIMIENTO"].Summary.Clear();
                        dgvLimaNorteBG.Columns["CUMPLIMIENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CUMPLIMIENTO", "Total Cump.: {0}");

                        var Proyectado = dgvLimaNorteBG.Columns["PROY / REQ"].SummaryItem.SummaryValue;
                        var Cumplimiento = dgvLimaNorteBG.Columns["CUMPLIMIENTO"].SummaryItem.SummaryValue;
                        double Porcentaje;

                        if (Convert.ToString(Proyectado) == "" || Convert.ToString(Cumplimiento) == "") { Porcentaje = 0.00; }
                        else { Porcentaje = Math.Round(Convert.ToDouble(Cumplimiento) * 100 / Convert.ToDouble(Proyectado), 2); }
                        dgvLimaNorteBG.Columns["PORCENTAJE"].Summary.Clear();
                        dgvLimaNorteBG.Columns["PORCENTAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCENTAJE", "Porcentaje: " + Convert.ToString(Porcentaje) + "%");

                        dgvLimaNorteBG.BestFitColumns();
                    }
                }

                if (TipoViaje == 2)
                {
                    dtListaCumplimiento = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento(TipoViaje, dtpFechaInicio.Text, dtpFechaFin.Text);
                    dtgTrujilloLimaBG.DataSource = dtListaCumplimiento;
                    if (dtListaCumplimiento.Rows.Count > 0)
                    {
                        dgvTrujilloLimaBG.Columns["idCumplimiento"].Visible = false;
                        dgvTrujilloLimaBG.Columns["idGrupoViaje"].Visible = false;
                        dgvTrujilloLimaBG.Columns["GRUPO_VIAJE"].Visible = false;

                        dgvTrujilloLimaBG.Columns["PROY / REQ"].Summary.Clear();
                        dgvTrujilloLimaBG.Columns["PROY / REQ"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PROY / REQ", "Total Req.: {0}");
                        dgvTrujilloLimaBG.Columns["CUMPLIMIENTO"].Summary.Clear();
                        dgvTrujilloLimaBG.Columns["CUMPLIMIENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CUMPLIMIENTO", "Total Cump.: {0}");

                        var Proyectado = dgvTrujilloLimaBG.Columns["PROY / REQ"].SummaryItem.SummaryValue;
                        var Cumplimiento = dgvTrujilloLimaBG.Columns["CUMPLIMIENTO"].SummaryItem.SummaryValue;
                        double Porcentaje;

                        if (Convert.ToString(Proyectado) == "" || Convert.ToString(Cumplimiento) == "") { Porcentaje = 0.00; }
                        else { Porcentaje = Math.Round(Convert.ToDouble(Cumplimiento) * 100 / Convert.ToDouble(Proyectado), 2); }
                        dgvTrujilloLimaBG.Columns["PORCENTAJE"].Summary.Clear();
                        dgvTrujilloLimaBG.Columns["PORCENTAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCENTAJE", "Porcentaje: " + Convert.ToString(Porcentaje) + "%");

                        dgvTrujilloLimaBG.BestFitColumns();
                    }
                }

                if (TipoViaje == 3)
                {
                    dtListaCumplimiento = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento(TipoViaje, dtpFechaInicio.Text, dtpFechaFin.Text);
                    dtgLimaNorteC.DataSource = dtListaCumplimiento;
                    if (dtListaCumplimiento.Rows.Count > 0)
                    {
                        dgvLimaNorteC.Columns["idCumplimiento"].Visible = false;
                        dgvLimaNorteC.Columns["idGrupoViaje"].Visible = false;
                        dgvLimaNorteC.Columns["GRUPO_VIAJE"].Visible = false;

                        dgvLimaNorteC.Columns["PROY / REQ"].Summary.Clear();
                        dgvLimaNorteC.Columns["PROY / REQ"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PROY / REQ", "Total Req.: {0}");
                        dgvLimaNorteC.Columns["CUMPLIMIENTO"].Summary.Clear();
                        dgvLimaNorteC.Columns["CUMPLIMIENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CUMPLIMIENTO", "Total Cump.: {0}");

                        var Proyectado = dgvLimaNorteC.Columns["PROY / REQ"].SummaryItem.SummaryValue;
                        var Cumplimiento = dgvLimaNorteC.Columns["CUMPLIMIENTO"].SummaryItem.SummaryValue;
                        double Porcentaje;

                        if (Convert.ToString(Proyectado) == "" || Convert.ToString(Cumplimiento) == "") { Porcentaje = 0.00; }
                        else { Porcentaje = Math.Round(Convert.ToDouble(Cumplimiento) * 100 / Convert.ToDouble(Proyectado), 2); }
                        dgvLimaNorteC.Columns["PORCENTAJE"].Summary.Clear();
                        dgvLimaNorteC.Columns["PORCENTAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCENTAJE", "Porcentaje: " + Convert.ToString(Porcentaje) + "%");

                        dgvLimaNorteC.BestFitColumns();
                    }
                }

                if (TipoViaje == 4)
                {
                    dtListaCumplimiento = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento(TipoViaje, dtpFechaInicio2.Text, dtpFechaFin2.Text);
                    dtgTrujilloNorte.DataSource = dtListaCumplimiento;
                    if (dtListaCumplimiento.Rows.Count > 0)
                    {
                        dgvTrujilloNorte.Columns["idCumplimiento"].Visible = false;
                        dgvTrujilloNorte.Columns["idGrupoViaje"].Visible = false;

                        dgvTrujilloNorte.Columns["PROY / REQ"].Summary.Clear();
                        dgvTrujilloNorte.Columns["PROY / REQ"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PROY / REQ", "Total Req.: {0}");
                        dgvTrujilloNorte.Columns["CUMPLIMIENTO"].Summary.Clear();
                        dgvTrujilloNorte.Columns["CUMPLIMIENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CUMPLIMIENTO", "Total Cump.: {0}");

                        var Proyectado = dgvTrujilloNorte.Columns["PROY / REQ"].SummaryItem.SummaryValue;
                        var Cumplimiento = dgvTrujilloNorte.Columns["CUMPLIMIENTO"].SummaryItem.SummaryValue;
                        double Porcentaje;

                        if (Convert.ToString(Proyectado) == "" || Convert.ToString(Cumplimiento) == "") { Porcentaje = 0.00; }
                        else { Porcentaje = Math.Round(Convert.ToDouble(Cumplimiento) * 100 / Convert.ToDouble(Proyectado), 2); }
                        dgvTrujilloNorte.Columns["PORCENTAJE"].Summary.Clear();
                        dgvTrujilloNorte.Columns["PORCENTAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCENTAJE", "Porcentaje: " + Convert.ToString(Porcentaje) + "%");

                        dgvTrujilloNorte.SortInfo.ClearAndAddRange(new GridColumnSortInfo[]
                        { 
                            new GridColumnSortInfo(dgvTrujilloNorte.Columns["FECHA"], DevExpress.Data.ColumnSortOrder.Descending)
                        }, 0);

                        dgvTrujilloNorte.BestFitColumns();
                    }
                }

                if (TipoViaje == 5)
                {
                    dtListaCumplimiento = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento(TipoViaje, dtpFechaInicio2.Text, dtpFechaFin2.Text);
                    dtgTrujilloResumen.DataSource = dtListaCumplimiento;
                    if (dtListaCumplimiento.Rows.Count > 0)
                    {
                        dgvTrujilloResumen.Columns["PROY / REQ"].Summary.Clear();
                        dgvTrujilloResumen.Columns["PROY / REQ"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PROY / REQ", "Total Req.: {0}");
                        dgvTrujilloResumen.Columns["CUMPLIMIENTO"].Summary.Clear();
                        dgvTrujilloResumen.Columns["CUMPLIMIENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CUMPLIMIENTO", "Total Cump.: {0}");

                        var Proyectado = dgvTrujilloResumen.Columns["PROY / REQ"].SummaryItem.SummaryValue;
                        var Cumplimiento = dgvTrujilloResumen.Columns["CUMPLIMIENTO"].SummaryItem.SummaryValue;
                        double Porcentaje;

                        if (Convert.ToString(Proyectado) == "" || Convert.ToString(Cumplimiento) == "") { Porcentaje = 0.00; }
                        else { Porcentaje = Math.Round(Convert.ToDouble(Cumplimiento) * 100 / Convert.ToDouble(Proyectado), 2); }
                        dgvTrujilloResumen.Columns["PORCENTAJE"].Summary.Clear();
                        dgvTrujilloResumen.Columns["PORCENTAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCENTAJE", "Porcentaje: " + Convert.ToString(Porcentaje) + "%");

                        dgvTrujilloResumen.BestFitColumns();
                    }
                }

                if (TipoViaje == 6)
                {
                    dtListaCumplimiento = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarCumplimiento(TipoViaje, dtpFechaInicio2.Text, dtpFechaFin2.Text);
                    dtgTrujilloRuta.DataSource = dtListaCumplimiento;
                    if (dtListaCumplimiento.Rows.Count > 0)
                    {
                        dgvTrujilloRuta.Columns["PROY / REQ"].Summary.Clear();
                        dgvTrujilloRuta.Columns["PROY / REQ"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PROY / REQ", "Total Req.: {0}");
                        dgvTrujilloRuta.Columns["CUMPLIMIENTO"].Summary.Clear();
                        dgvTrujilloRuta.Columns["CUMPLIMIENTO"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "CUMPLIMIENTO", "Total Cump.: {0}");

                        var Proyectado = dgvTrujilloRuta.Columns["PROY / REQ"].SummaryItem.SummaryValue;
                        var Cumplimiento = dgvTrujilloRuta.Columns["CUMPLIMIENTO"].SummaryItem.SummaryValue;
                        double Porcentaje;

                        if (Convert.ToString(Proyectado) == "" || Convert.ToString(Cumplimiento) == "") { Porcentaje = 0.00; }
                        else { Porcentaje = Math.Round(Convert.ToDouble(Cumplimiento) * 100 / Convert.ToDouble(Proyectado), 2); }
                        dgvTrujilloRuta.Columns["PORCENTAJE"].Summary.Clear();
                        dgvTrujilloRuta.Columns["PORCENTAJE"].Summary.Add(DevExpress.Data.SummaryItemType.Sum, "PORCENTAJE", "Porcentaje: " + Convert.ToString(Porcentaje) + "%");

                        dgvTrujilloRuta.BestFitColumns();
                    }
                }
            }
        }


        private void btnAgregarRuta_Click(object sender, EventArgs e)
        {
            OpcionTL = 1;
            CargarComboGrupo();
            ListarRutas();
            cbxGrupo.SelectedValue = "1";
            pAgregarRuta.Visible = true;
            pAgregarRuta.BringToFront();
        }

        private void btnNuevoRegistro_Click(object sender, EventArgs e)
        {
            frmRegistrarCumplimiento frmRegistrarCumplimiento = new frmRegistrarCumplimiento();
            frmRegistrarCumplimiento.OpcionTL = 1;
            frmRegistrarCumplimiento.Editar = 0;
            frmRegistrarCumplimiento.formulario = this;
            frmRegistrarCumplimiento.dtpFechaCumplimiento.Value = DateTime.Now;
            frmRegistrarCumplimiento.CargarComboGrupo2();
            frmRegistrarCumplimiento.ShowDialog();
        }

        private void pAgregarRuta_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pAgregarRuta.Left = pAgregarRuta.Left + (e.X - xClick);
                pAgregarRuta.Top = pAgregarRuta.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            OpcionTL = 0;
            idRuta = -1;
            txtRutasActivas.Clear();
            
            pAgregarRuta.Visible = false;
            pAgregarRuta.SendToBack();
        }

        private void txtRutasActivas_Enter(object sender, EventArgs e) { txtRutasActivas.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtRutasActivas_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lvRuta, clsConsultaBL.Instancia.GetRutasActivas(txtRutasActivas.Text), true, false, false);
            lvRuta.Columns[0].Width = 0;
            lvRuta.Columns[1].Width = 350;
            lvRuta.BringToFront();
            lvRuta.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRuta.Visible = false;
                lvRuta.SendToBack();
                idRuta = -1;
            }
        }

        private void txtRutasActivas_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lvRuta.Focus(); }
        }

        private void txtRutasActivas_Leave(object sender, EventArgs e) { txtRutasActivas.BackColor = Color.White; }

        private void lvRuta_Enter(object sender, EventArgs e)
        {
            if (!lvRuta.Items.Count.Equals(0)) { lvRuta.Items[0].Selected = true; }
        }

        private void lvRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lvRuta.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lvRuta.SelectedItems[0];
                idRuta = Int32.Parse(ItemActual.Text);
                txtRutasActivas.Text = ItemActual.SubItems[1].Text;

                lvRuta.Visible = false;
                lvRuta.SendToBack();
                cbxGrupo.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lvRuta.Visible = false;
                lvRuta.SendToBack();
                txtRutasActivas.Focus();
                idRuta = -1;
            }
        }

        private void lvRuta_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lvRuta.SelectedItems[0];
            idRuta = Int32.Parse(ItemActual.Text);
            txtRutasActivas.Text = ItemActual.SubItems[1].Text;

            lvRuta.Visible = false;
            lvRuta.SendToBack();
            cbxGrupo.Focus();
        }

        private void btnGuardar2_Click(object sender, EventArgs e)
        {
            if (txtRutasActivas.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese una ruta.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRutasActivas.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_AsignarGrupo(idRuta, Convert.ToInt32(cbxGrupo.SelectedValue));
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    idRuta = -1;
                    txtRutasActivas.Clear();
                    if (OpcionTL == 1) { btnBuscar_Click(sender, e); }
                    if (OpcionTL == 2) { btnBuscar2_Click(sender, e); }
                    ListarRutas();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }

        public void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarCumplimientos(0);
            ListarCumplimientos(1);
            ListarCumplimientos(2);
            ListarCumplimientos(3);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListarTodo.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                ListarCumplimientos(0);

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Registro de Cumplimiento (LINDLEY - LIMA) - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListarTodo.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dgvCumplimientoVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE")
            {
                if (Convert.ToInt32(e.CellValue) >= 100)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToInt32(e.CellValue) >= 50 && Convert.ToInt32(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToInt32(e.CellValue) > 0 && Convert.ToInt32(e.CellValue) < 50)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CUMPLIDO")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "NO CUMPLIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "FECHA") { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }
        }

        private void dgvLimaNorteC_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE")
            {
                if (Convert.ToInt32(e.CellValue) >= 100)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToInt32(e.CellValue) >= 50 && Convert.ToInt32(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToInt32(e.CellValue) > 0 && Convert.ToInt32(e.CellValue) < 50)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CUMPLIDO")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "NO CUMPLIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "FECHA") { e.Appearance.BackColor = Color.FromArgb(255, 192, 128); }
        }

        private void dgvTrujilloLimaBG_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE")
            {
                if (Convert.ToInt32(e.CellValue) >= 100)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToInt32(e.CellValue) >= 50 && Convert.ToInt32(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToInt32(e.CellValue) > 0 && Convert.ToInt32(e.CellValue) < 50)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CUMPLIDO")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "NO CUMPLIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "FECHA") { e.Appearance.BackColor = Color.FromArgb(192, 192, 255); }
        }

        private void dtgListaCumplimiento_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Codigo = dgvLimaNorteBG.GetRowCellValue(dgvLimaNorteBG.FocusedRowHandle, "idCumplimiento").ToString();

                if (Codigo != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminar1.Enabled = true; }
                }
                else { tsEliminar1.Enabled = false; }
            }
            catch { tsEliminar1.Enabled = false; }
        }

        private void dtgLimaNorteC_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Codigo = dgvLimaNorteC.GetRowCellValue(dgvLimaNorteC.FocusedRowHandle, "idCumplimiento").ToString();

                if (Codigo != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminar2.Enabled = true; }
                }
                else { tsEliminar2.Enabled = false; }
            }
            catch { tsEliminar2.Enabled = false; }
        }

        private void dtgTrujilloLimaBG_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Codigo = dgvTrujilloLimaBG.GetRowCellValue(dgvTrujilloLimaBG.FocusedRowHandle, "idCumplimiento").ToString();

                if (Codigo != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminar3.Enabled = true; }
                }
                else { tsEliminar3.Enabled = false; }
            }
            catch { tsEliminar3.Enabled = false; }
        }

        private void tsEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idGrupoViaje = Convert.ToInt32(dgvLimaNorteBG.GetRowCellValue(dgvLimaNorteBG.FocusedRowHandle, "idGrupoViaje"));
                DateTime FechaCumplimiento = Convert.ToDateTime(dgvLimaNorteBG.GetRowCellValue(dgvLimaNorteBG.FocusedRowHandle, "FECHA"));

                if (MessageBox.Show("¿Desea quitar los datos de esta fecha de cumplimiento?", "ELIMINAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento(2, idGrupoViaje, FechaCumplimiento, 1, 1, " ", " ");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarCumplimientos(1); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsEliminar2_Click(object sender, EventArgs e)
        {
            try
            {
                int idGrupoViaje = Convert.ToInt32(dgvLimaNorteC.GetRowCellValue(dgvLimaNorteC.FocusedRowHandle, "idGrupoViaje"));
                DateTime FechaCumplimiento = Convert.ToDateTime(dgvLimaNorteC.GetRowCellValue(dgvLimaNorteC.FocusedRowHandle, "FECHA"));

                if (MessageBox.Show("¿Desea quitar los datos de esta fecha de cumplimiento?", "ELIMINAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento(2, idGrupoViaje, FechaCumplimiento, 1, 1, " ", " ");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarCumplimientos(3); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsEliminar3_Click(object sender, EventArgs e)
        {
            try
            {
                int idGrupoViaje = Convert.ToInt32(dgvTrujilloLimaBG.GetRowCellValue(dgvTrujilloLimaBG.FocusedRowHandle, "idGrupoViaje"));
                DateTime FechaCumplimiento = Convert.ToDateTime(dgvTrujilloLimaBG.GetRowCellValue(dgvTrujilloLimaBG.FocusedRowHandle, "FECHA"));

                if (MessageBox.Show("¿Desea quitar los datos de esta fecha de cumplimiento?", "ELIMINAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento(2, idGrupoViaje, FechaCumplimiento, 1, 1, " ", " ");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0") { ListarCumplimientos(2); }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgListaCumplimiento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    frmRegistrarCumplimiento frmRegistrarCumplimiento = new frmRegistrarCumplimiento();
                    frmRegistrarCumplimiento.OpcionTL = 1;
                    frmRegistrarCumplimiento.Editar = 1;
                    frmRegistrarCumplimiento.formulario = this;
                    frmRegistrarCumplimiento.dtpFechaCumplimiento.Value = Convert.ToDateTime(dgvLimaNorteBG.GetRowCellValue(dgvLimaNorteBG.FocusedRowHandle, "FECHA"));
                    frmRegistrarCumplimiento.dtpFechaCumplimiento.Enabled = false;
                    frmRegistrarCumplimiento.CargarComboGrupo2();
                    frmRegistrarCumplimiento.cbxTipoViaje.SelectedValue = Convert.ToString(dgvLimaNorteBG.GetRowCellValue(dgvLimaNorteBG.FocusedRowHandle, "idGrupoViaje"));
                    frmRegistrarCumplimiento.cbxTipoViaje.Enabled = false;
                    frmRegistrarCumplimiento.txtViDisponible.Text = Convert.ToString(dgvLimaNorteBG.GetRowCellValue(dgvLimaNorteBG.FocusedRowHandle, "DISPONIBLE"));
                    frmRegistrarCumplimiento.txtProyReq.Text = Convert.ToString(dgvLimaNorteBG.GetRowCellValue(dgvLimaNorteBG.FocusedRowHandle, "PROY / REQ"));
                    frmRegistrarCumplimiento.txtDetalle.Text = Convert.ToString(dgvLimaNorteBG.GetRowCellValue(dgvLimaNorteBG.FocusedRowHandle, "DETALLE"));
                    frmRegistrarCumplimiento.btnCancelar.Enabled = false;
                    frmRegistrarCumplimiento.ShowDialog();
                }
            }
            catch { MessageBox.Show("La fecha seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgLimaNorteC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    frmRegistrarCumplimiento frmRegistrarCumplimiento = new frmRegistrarCumplimiento();
                    frmRegistrarCumplimiento.OpcionTL = 1;
                    frmRegistrarCumplimiento.Editar = 1;
                    frmRegistrarCumplimiento.formulario = this;
                    frmRegistrarCumplimiento.dtpFechaCumplimiento.Value = Convert.ToDateTime(dgvLimaNorteC.GetRowCellValue(dgvLimaNorteC.FocusedRowHandle, "FECHA"));
                    frmRegistrarCumplimiento.dtpFechaCumplimiento.Enabled = false;
                    frmRegistrarCumplimiento.CargarComboGrupo2();
                    frmRegistrarCumplimiento.cbxTipoViaje.SelectedValue = Convert.ToString(dgvLimaNorteC.GetRowCellValue(dgvLimaNorteC.FocusedRowHandle, "idGrupoViaje"));
                    frmRegistrarCumplimiento.cbxTipoViaje.Enabled = false;
                    frmRegistrarCumplimiento.txtViDisponible.Text = Convert.ToString(dgvLimaNorteC.GetRowCellValue(dgvLimaNorteC.FocusedRowHandle, "DISPONIBLE"));
                    frmRegistrarCumplimiento.txtProyReq.Text = Convert.ToString(dgvLimaNorteC.GetRowCellValue(dgvLimaNorteC.FocusedRowHandle, "PROY / REQ"));
                    frmRegistrarCumplimiento.txtDetalle.Text = Convert.ToString(dgvLimaNorteC.GetRowCellValue(dgvLimaNorteC.FocusedRowHandle, "DETALLE"));
                    frmRegistrarCumplimiento.btnCancelar.Enabled = false;
                    frmRegistrarCumplimiento.ShowDialog();
                }
            }
            catch { MessageBox.Show("La fecha seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgTrujilloLimaBG_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    frmRegistrarCumplimiento frmRegistrarCumplimiento = new frmRegistrarCumplimiento();
                    frmRegistrarCumplimiento.OpcionTL = 1;
                    frmRegistrarCumplimiento.Editar = 1;
                    frmRegistrarCumplimiento.formulario = this;
                    frmRegistrarCumplimiento.dtpFechaCumplimiento.Value = Convert.ToDateTime(dgvTrujilloLimaBG.GetRowCellValue(dgvTrujilloLimaBG.FocusedRowHandle, "FECHA"));
                    frmRegistrarCumplimiento.dtpFechaCumplimiento.Enabled = false;
                    frmRegistrarCumplimiento.CargarComboGrupo2();
                    frmRegistrarCumplimiento.cbxTipoViaje.SelectedValue = Convert.ToString(dgvTrujilloLimaBG.GetRowCellValue(dgvTrujilloLimaBG.FocusedRowHandle, "idGrupoViaje"));
                    frmRegistrarCumplimiento.cbxTipoViaje.Enabled = false;
                    frmRegistrarCumplimiento.txtViDisponible.Text = Convert.ToString(dgvTrujilloLimaBG.GetRowCellValue(dgvTrujilloLimaBG.FocusedRowHandle, "DISPONIBLE"));
                    frmRegistrarCumplimiento.txtProyReq.Text = Convert.ToString(dgvTrujilloLimaBG.GetRowCellValue(dgvTrujilloLimaBG.FocusedRowHandle, "PROY / REQ"));
                    frmRegistrarCumplimiento.txtDetalle.Text = Convert.ToString(dgvTrujilloLimaBG.GetRowCellValue(dgvTrujilloLimaBG.FocusedRowHandle, "DETALLE"));
                    frmRegistrarCumplimiento.btnCancelar.Enabled = false;
                    frmRegistrarCumplimiento.ShowDialog();
                }
            }
            catch { MessageBox.Show("La fecha seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgListaRutas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Nro = dgvListaRutas.GetRowCellValue(dgvListaRutas.FocusedRowHandle, "NRO").ToString();

                if (Nro != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsQuitarRuta.Enabled = true; }
                }
                else { tsQuitarRuta.Enabled = false; }
            }
            catch { tsQuitarRuta.Enabled = false; }
        }

        private void tsQuitarRuta_Click(object sender, EventArgs e)
        {
            try
            {
                int idGrupoViaje = Convert.ToInt32(dgvListaRutas.GetRowCellValue(dgvListaRutas.FocusedRowHandle, "idGrupoViaje"));
                int idGrupoViajeD = Convert.ToInt32(dgvListaRutas.GetRowCellValue(dgvListaRutas.FocusedRowHandle, "NRO"));

                if (MessageBox.Show("¿Desea quitar esta ruta de la lista?", "QUITAR RUTA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_EliminarGrupo(idGrupoViajeD, idGrupoViaje);
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        ListarRutas();
                        btnBuscar_Click(sender, e);
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar la ruta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAgregarRuta2_Click(object sender, EventArgs e)
        {
            OpcionTL = 2;
            CargarComboGrupo();
            ListarRutas();
            cbxGrupo.SelectedValue = "4";
            pAgregarRuta.Visible = true;
            pAgregarRuta.BringToFront();
        }

        private void btnNuevoRegistro2_Click(object sender, EventArgs e)
        {
            frmRegistrarCumplimiento frmRegistrarCumplimiento = new frmRegistrarCumplimiento();
            frmRegistrarCumplimiento.OpcionTL = 2;
            frmRegistrarCumplimiento.Editar = 0;
            frmRegistrarCumplimiento.formulario = this;
            frmRegistrarCumplimiento.dtpFechaCumplimiento.Value = DateTime.Now;
            frmRegistrarCumplimiento.CargarComboGrupo2();
            frmRegistrarCumplimiento.ShowDialog();
        }

        private void dtpFechaInicio2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar2_Click(sender, e); }
        }

        private void dtpFechaFin2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar2_Click(sender, e); }
        }

        public void btnBuscar2_Click(object sender, EventArgs e)
        {
            ListarCumplimientos(4);
            ListarCumplimientos(5);
            ListarCumplimientos(6);
        }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            if (dtgTrujilloNorte.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Registro de Cumplimiento (LINDLEY - NORTE) - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgTrujilloNorte.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void dtgTrujilloNorte_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    frmRegistrarCumplimiento frmRegistrarCumplimiento = new frmRegistrarCumplimiento();
                    frmRegistrarCumplimiento.OpcionTL = 2;
                    frmRegistrarCumplimiento.Editar = 1;
                    frmRegistrarCumplimiento.formulario = this;
                    frmRegistrarCumplimiento.dtpFechaCumplimiento.Value = Convert.ToDateTime(dgvTrujilloNorte.GetRowCellValue(dgvTrujilloNorte.FocusedRowHandle, "FECHA"));
                    frmRegistrarCumplimiento.dtpFechaCumplimiento.Enabled = false;
                    frmRegistrarCumplimiento.CargarComboGrupo2();
                    frmRegistrarCumplimiento.cbxTipoViaje.SelectedValue = Convert.ToString(dgvTrujilloNorte.GetRowCellValue(dgvTrujilloNorte.FocusedRowHandle, "idGrupoViaje"));
                    frmRegistrarCumplimiento.cbxTipoViaje.Enabled = false;
                    frmRegistrarCumplimiento.txtViDisponible.Text = Convert.ToString(dgvTrujilloNorte.GetRowCellValue(dgvTrujilloNorte.FocusedRowHandle, "DISPONIBLE"));
                    frmRegistrarCumplimiento.txtProyReq.Text = Convert.ToString(dgvTrujilloNorte.GetRowCellValue(dgvTrujilloNorte.FocusedRowHandle, "PROY / REQ"));
                    frmRegistrarCumplimiento.txtDetalle.Text = Convert.ToString(dgvTrujilloNorte.GetRowCellValue(dgvTrujilloNorte.FocusedRowHandle, "DETALLE"));
                    frmRegistrarCumplimiento.btnCancelar.Enabled = false;
                    frmRegistrarCumplimiento.ShowDialog();
                }
            }
            catch { MessageBox.Show("La fecha seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgTrujilloNorte_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string Codigo = dgvTrujilloNorte.GetRowCellValue(dgvTrujilloNorte.FocusedRowHandle, "idCumplimiento").ToString();

                if (Codigo != "")
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { tsEliminar4.Enabled = true; }
                }
                else { tsEliminar4.Enabled = false; }
            }
            catch { tsEliminar4.Enabled = false; }
        }

        private void dgvTrujilloNorte_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE")
            {
                if (Convert.ToInt32(e.CellValue) >= 100)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToInt32(e.CellValue) >= 50 && Convert.ToInt32(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToInt32(e.CellValue) > 0 && Convert.ToInt32(e.CellValue) < 50)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CUMPLIDO")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "NO CUMPLIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "FECHA") { e.Appearance.BackColor = Color.FromArgb(192, 192, 255); }
        }

        private void dgvTrujilloResumen_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE")
            {
                if (Convert.ToInt32(e.CellValue) >= 100)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToInt32(e.CellValue) >= 50 && Convert.ToInt32(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToInt32(e.CellValue) > 0 && Convert.ToInt32(e.CellValue) < 50)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CUMPLIDO")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "NO CUMPLIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "FECHA") { e.Appearance.BackColor = Color.FromArgb(255, 192, 128); }
        }

        private void dgvTrujilloRuta_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE")
            {
                if (Convert.ToInt32(e.CellValue) >= 100)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToInt32(e.CellValue) >= 50 && Convert.ToInt32(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToInt32(e.CellValue) > 0 && Convert.ToInt32(e.CellValue) < 50)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CUMPLIDO")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "NO CUMPLIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "GRUPO_VIAJE") { e.Appearance.BackColor = Color.FromArgb(255, 192, 128); }
        }

        private void tsEliminar4_Click(object sender, EventArgs e)
        {
            try
            {
                int idGrupoViaje = Convert.ToInt32(dgvTrujilloNorte.GetRowCellValue(dgvTrujilloNorte.FocusedRowHandle, "idGrupoViaje"));
                DateTime FechaCumplimiento = Convert.ToDateTime(dgvTrujilloNorte.GetRowCellValue(dgvTrujilloNorte.FocusedRowHandle, "FECHA"));

                if (MessageBox.Show("¿Desea quitar los datos de esta fecha de cumplimiento?", "ELIMINAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;

                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento(2, idGrupoViaje, FechaCumplimiento, 1, 1, " ", " ");
                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        ListarCumplimientos(4);
                        ListarCumplimientos(5);
                        ListarCumplimientos(6);
                    }
                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { MessageBox.Show("Se produjo un error al eliminar el cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
