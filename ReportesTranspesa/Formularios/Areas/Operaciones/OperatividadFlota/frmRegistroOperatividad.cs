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
﻿using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.OperatividadFlota
{
    public partial class frmRegistroOperatividad : Form
    {
        int idCondicion, idCondicionOP, IdUnidad;
        string Placa, Programacion, FechaFijaSelec;
        int saveRow = 0, saveCol = 0;
        int saveRow2 = 0, saveCol2 = 0;
        int saveRow3 = 0, saveCol3 = 0;
        int xClick = 0, yClick = 0;
        int xClick2 = 0, yClick2 = 0;
        string xmlOperatividad;
        string TipoVehiculo, TipoUnidad;
        DataTable dtPermisos = new DataTable();

        public frmRegistroOperatividad()
        {
            InitializeComponent();
            cbxCondicion.SelectedIndexChanged -= cbxCondicion_SelectedIndexChanged;
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
            cbxCarreta.SelectedIndexChanged -= cbxCarreta_SelectedIndexChanged;
        }

        private void cbxCondicion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboCondicion(); }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones(); }

        private void cbxCarreta_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSubTipo(); }

        private void frmRegistroOperatividad_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistroOperatividad");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnMapear.Enabled = true; }
                    else { btnMapear.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                    {
                        ingresarCantidadToolStripMenuItem.Enabled = true;
                        tsIngresarCantidad.Enabled = true;
                    }
                    else
                    {
                        ingresarCantidadToolStripMenuItem.Enabled = false;
                        tsIngresarCantidad.Enabled = false;
                    }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { btnQuitar.Enabled = true; }
                    else { btnQuitar.Enabled = false; }
                }
            }
            
            DateTime date = DateTime.Now;
            dtpPeriodo.Value = new DateTime(date.Year, date.Month, 1);
            rbTractos.Checked = true;
            rbTractos_Click(sender, e);
            CargarComboCondicion();
            CargarComboOperacion();
            CargarComboOperaciones();
            CargarComboSubTipo();
            cbxOperaciones.SelectedValue = 5;

            dgvOperatividadPLView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperatividadPLView.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperatividadPLView.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvOperatividadTLView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperatividadTLView.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperatividadTLView.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvOperatividadCSView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperatividadCSView.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperatividadCSView.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvOperatividadCRView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperatividadCRView.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvOperatividadCRView.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }


        private void CargarComboCondicion()
        {
            DataTable dtCondicion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(3, "");
            cbxCondicion.DataSource = dtCondicion;
            cbxCondicion.DisplayMember = "Condicion";
            cbxCondicion.ValueMember = "idCondicion";
        }

        private void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(4, "");
            cbxOperacion.DataSource = dtOperacion;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        private void CargarComboOperaciones()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperaciones;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        private void CargarComboSubTipo()
        {
            DataTable dtSubTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(2, 2);
            cbxCarreta.DataSource = dtSubTipo;
            cbxCarreta.DisplayMember = "Descripcion";
            cbxCarreta.ValueMember = "idSubTipoVehiculo";
        }

        public void CargarMapeados()
        {
            DataTable dt2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(2, dtpPeriodo.Text);

            if (dt2.Rows.Count > 0)
            {
                dtgvDataMapeados.DataSource = dt2;
                dtgvDataMapeadosView.Columns["IdUnidad"].Visible = false;
                dtgvDataMapeadosView.UpdateSummary();
                dtgvDataMapeadosView.BestFitColumns();
            }
            else
            {
                MessageBox.Show("Aún no hay condiciones mapeadas.", "Aviso");
                dtgvDataMapeados.DataSource = null;
            }
        }

        public void CargarTablaPlacas()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadTractos(dtpPeriodo.Text, txtPlaca.Text, cbxOperaciones.Text);

            dgvPlacas.DataSource = null;
            dgvPlacas.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                dgvPlacas.DataSource = dt;
                dgvPlacas.AutoResizeColumns();
                dgvPlacas.Columns["PROGRAMACION"].Frozen = true;
                dgvPlacas.Columns["PROGRAMACION"].ReadOnly = true;
                dgvPlacas.Columns["PLACA"].Frozen = true;
                dgvPlacas.Columns["PLACA"].ReadOnly = true;

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvPlacas.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#B2B1B3");
                dgvPlacas.EnableHeadersVisualStyles = false;

                dgvPlacas.Columns["IdUnidad"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow != 0 && saveRow < dgvPlacas.Rows.Count)
                    {
                        dgvPlacas.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvPlacas.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
        }

        public void CargarTablaCarretas()
        {
            DataTable dt2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadCarreta(dtpPeriodo.Text, txtPlaca.Text, cbxOperaciones.Text, cbxCarreta.Text);

            dgvCarretas.DataSource = null;
            dgvCarretas.Columns.Clear();

            if (dt2.Rows.Count > 0)
            {
                dgvCarretas.DataSource = dt2;
                dgvCarretas.AutoResizeColumns();
                dgvCarretas.Columns["PROGRAMACION"].Frozen = true;
                dgvCarretas.Columns["PROGRAMACION"].ReadOnly = true;
                dgvCarretas.Columns["PLACA"].Frozen = true;
                dgvCarretas.Columns["PLACA"].ReadOnly = true;
                dgvCarretas.Columns["TIPO_VEHICULO"].Frozen = true;
                dgvCarretas.Columns["TIPO_VEHICULO"].ReadOnly = true;

                int numeroCol = 0;
                numeroCol = dt2.Columns.Count;
                dgvCarretas.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#B2B1B3");
                dgvCarretas.EnableHeadersVisualStyles = false;

                dgvCarretas.Columns["IdUnidad"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow2 != 0 && saveRow2 < dgvCarretas.Rows.Count)
                    {
                        dgvCarretas.FirstDisplayedScrollingColumnIndex = saveCol2;
                        dgvCarretas.FirstDisplayedScrollingRowIndex = saveRow2;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
        }

        public void CargarTabla()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaOperatividad(dtpPeriodo.Text);
           
            dgvOperatividadView.DataSource = null;
            dgvOperatividadView.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                DataRow operativas, total, porcentaje;

                operativas = dt.NewRow();
                operativas[0] = 997;
                operativas[1] = "TOTAL EN RUTA";
                operativas[2] = " ";
                
                total = dt.NewRow();
                total[0] = 998;
                total[1] = "TOTAL UNIDADES";
                total[2] = " ";

                porcentaje = dt.NewRow();
                porcentaje[0] = 999;
                porcentaje[1] = "% REA / ASIG";
                porcentaje[2] = "META: 95%";

                dt.Rows.Add(operativas);
                dt.Rows.Add(total);
                dt.Rows.Add(porcentaje);

                dgvOperatividadView.DataSource = dt;
                dgvOperatividadView.AutoResizeColumns();
                dgvOperatividadView.Columns["CONCEPTO"].Frozen = true;
                dgvOperatividadView.Columns["CONCEPTO"].ReadOnly = true;
                dgvOperatividadView.Columns["CONDICION"].Frozen = true;
                dgvOperatividadView.Columns["CONDICION"].ReadOnly = true;

                int sum = 0, porc = 0, f, c, divisor, ope = 0;
                for (c = 3; c <= dgvOperatividadView.Columns.Count - 1; c++)
                {
                    for (f = 0; f < dgvOperatividadView.Rows.Count - 3; f++)
                    {
                        if (f < 7)
                        {
                            ope += Convert.ToInt32(dgvOperatividadView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadView.Rows[f].Cells[c].Value);
                            dgvOperatividadView.Rows[dgvOperatividadView.Rows.Count - 3].Cells[c].Value = ope;
                        }
                        
                        sum += Convert.ToInt32(dgvOperatividadView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadView.Rows[f].Cells[c].Value);
                        dgvOperatividadView.Rows[dgvOperatividadView.Rows.Count - 2].Cells[c].Value = sum;

                        int vendida = Convert.ToInt32(dgvOperatividadView.Rows[dgvOperatividadView.Rows.Count - 4].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadView.Rows[dgvOperatividadView.Rows.Count - 4].Cells[c].Value);
                        dgvOperatividadView.Rows[dgvOperatividadView.Rows.Count - 2].Cells[c].Value = sum - vendida;

                        if (Convert.ToInt32(dgvOperatividadView.Rows[dgvOperatividadView.Rows.Count - 2].Cells[c].Value) == 0) { divisor = 1; }
                        else { divisor = Convert.ToInt32(dgvOperatividadView.Rows[dgvOperatividadView.Rows.Count - 2].Cells[c].Value); }

                        porc = (Convert.ToInt32(dgvOperatividadView.Rows[14].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadView.Rows[14].Cells[c].Value) * 100) / divisor;
                        dgvOperatividadView.Rows[dgvOperatividadView.Rows.Count - 1].Cells[c].Value = porc;
                    }

                    sum = 0; porc = 0; ope = 0;
                }

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvOperatividadView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2BFF39");
                dgvOperatividadView.EnableHeadersVisualStyles = false;

                dgvOperatividadView.Columns["idCondicion"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow != 0 && saveRow < dgvOperatividadView.Rows.Count)
                    {
                        dgvOperatividadView.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvOperatividadView.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }

                // TABLA DE INGRESOS
                DataTable dt2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaIngresos(dtpPeriodo.Text);

                dgvIngresosView.DataSource = null;
                dgvIngresosView.Columns.Clear();

                if (dt2.Rows.Count > 0)
                {
                    DataRow totalI;

                    totalI = dt2.NewRow();
                    totalI[0] = 997;
                    totalI[1] = "TOTAL: ";
                    totalI[2] = " ";

                    dt2.Rows.Add(totalI);
                    
                    dgvIngresosView.DataSource = dt2;
                    dgvIngresosView.AutoResizeColumns();
                    dgvIngresosView.Columns["CONCEPTO"].Frozen = true;
                    dgvIngresosView.Columns["CONCEPTO"].ReadOnly = true;
                    dgvIngresosView.Columns["OPERACION"].Frozen = true;
                    dgvIngresosView.Columns["OPERACION"].ReadOnly = true;
                    dgvIngresosView.EnableHeadersVisualStyles = false;
                    dgvIngresosView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FF8989");

                    int f2, c2;
                    Decimal sum2 = 0;
                    for (c2 = 3; c2 <= dgvIngresosView.Columns.Count - 1; c2++)
                    {
                        for (f2 = 0; f2 < dgvIngresosView.Rows.Count - 1; f2++)
                        {
                            sum2 += Convert.ToDecimal(dgvIngresosView.Rows[f2].Cells[c2].Value == DBNull.Value ? 0 : dgvIngresosView.Rows[f2].Cells[c2].Value);
                            dgvIngresosView.Rows[dgvIngresosView.Rows.Count - 1].Cells[c2].Value = sum2;
                        }

                        sum2 = 0;
                    }

                    dgvIngresosView.Columns["idOperacion"].Visible = false;
                    dgvIngresosView.Columns["CONCEPTO"].ReadOnly = true;
                    dgvIngresosView.Columns["OPERACION"].ReadOnly = true;
                }
            }
            else { btnBuscar.Focus(); }
        }

        public void CargarIndicadores()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarIndicadores(1, dtpPeriodo.Text);

            dgvIndicadorUnidad.DataSource = null;
            dgvIndicadorUnidad.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                DataRow operativas, total, porcentaje;

                operativas = dt.NewRow();
                operativas[0] = 997;
                operativas[1] = dtpPeriodo.Value.Year;
                operativas[2] = "PROMEDIO EN RUTA";

                total = dt.NewRow();
                total[0] = 998;
                total[1] = dtpPeriodo.Value.Year;
                total[2] = "CANTIDAD TOTAL";

                porcentaje = dt.NewRow();
                porcentaje[0] = 999;
                porcentaje[1] = dtpPeriodo.Value.Year;
                porcentaje[2] = "% PROMEDIO";

                dt.Rows.Add(operativas);
                dt.Rows.Add(total);
                dt.Rows.Add(porcentaje);

                dgvIndicadorUnidad.DataSource = dt;
                dgvIndicadorUnidad.AutoResizeColumns();
                dgvIndicadorUnidad.Columns["CONCEPTO"].Frozen = true;
                dgvIndicadorUnidad.Columns["CONCEPTO"].ReadOnly = true;
                dgvIndicadorUnidad.Columns["CONDICION"].Frozen = true;
                dgvIndicadorUnidad.Columns["CONDICION"].ReadOnly = true;

                int sum = 0, porc = 0, f, c, divisor, ope = 0;
                for (c = 4; c <= dgvIndicadorUnidad.Columns.Count - 1; c++)
                {
                    for (f = 0; f < dgvIndicadorUnidad.Rows.Count - 4; f++)
                    {
                        if (f < 6)
                        {
                            ope += Convert.ToInt32(dgvIndicadorUnidad.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvIndicadorUnidad.Rows[f].Cells[c].Value);
                            dgvIndicadorUnidad.Rows[dgvIndicadorUnidad.Rows.Count - 3].Cells[c].Value = ope;
                        }

                        DataTable dtRespuesta = new DataTable();
                        int Respuesta;

                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractosTotal(c - 3, Convert.ToInt32(dtpPeriodo.Text.Substring(2,4)));
                        Respuesta = Convert.ToInt32(dtRespuesta.Rows[0]["NRO"]);
                        sum = Respuesta;

                        dgvIndicadorUnidad.Rows[dgvIndicadorUnidad.Rows.Count - 2].Cells[c].Value = sum;

                        if (Convert.ToInt32(dgvIndicadorUnidad.Rows[dgvIndicadorUnidad.Rows.Count - 2].Cells[c].Value) == 0) { divisor = 1; }
                        else { divisor = Convert.ToInt32(dgvIndicadorUnidad.Rows[dgvIndicadorUnidad.Rows.Count - 2].Cells[c].Value); }

                        porc = (Convert.ToInt32(dgvIndicadorUnidad.Rows[18].Cells[c].Value == DBNull.Value ? 0 : dgvIndicadorUnidad.Rows[18].Cells[c].Value) * 100) / divisor;
                        dgvIndicadorUnidad.Rows[dgvIndicadorUnidad.Rows.Count - 1].Cells[c].Value = porc;
                    }

                    sum = 0; porc = 0; ope = 0;
                }

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvIndicadorUnidad.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F1FF2B");
                dgvIndicadorUnidad.EnableHeadersVisualStyles = false;

                dgvIndicadorUnidad.Columns["idCondicion"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow3 != 0 && saveRow3 < dgvIndicadorUnidad.Rows.Count)
                    {
                        dgvIndicadorUnidad.FirstDisplayedScrollingColumnIndex = saveCol3;
                        dgvIndicadorUnidad.FirstDisplayedScrollingRowIndex = saveRow3;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }

                // TABLA DE INGRESOS
                DataTable dt2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarIndicadores(2, dtpPeriodo.Text);

                dgvIndicadorIngreso.DataSource = null;
                dgvIndicadorIngreso.Columns.Clear();

                if (dt2.Rows.Count > 0)
                {
                    DataRow totalI;

                    totalI = dt2.NewRow();
                    totalI[0] = 997;
                    totalI[1] = dtpPeriodo.Value.Year;
                    totalI[2] = "SUMA TOTAL: ";
                    totalI[3] = " ";

                    dt2.Rows.Add(totalI);

                    dgvIndicadorIngreso.DataSource = dt2;
                    dgvIndicadorIngreso.AutoResizeColumns();
                    dgvIndicadorIngreso.Columns["CONCEPTO"].Frozen = true;
                    dgvIndicadorIngreso.Columns["CONCEPTO"].ReadOnly = true;
                    dgvIndicadorIngreso.Columns["OPERACION"].Frozen = true;
                    dgvIndicadorIngreso.Columns["OPERACION"].ReadOnly = true;
                    dgvIndicadorIngreso.EnableHeadersVisualStyles = false;
                    dgvIndicadorIngreso.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#898FFF");

                    int f2, c2;
                    Decimal sum2 = 0;
                    for (c2 = 4; c2 <= dgvIndicadorIngreso.Columns.Count - 1; c2++)
                    {
                        for (f2 = 0; f2 < dgvIndicadorIngreso.Rows.Count - 1; f2++)
                        {
                            sum2 += Convert.ToDecimal(dgvIndicadorIngreso.Rows[f2].Cells[c2].Value == DBNull.Value ? 0 : dgvIndicadorIngreso.Rows[f2].Cells[c2].Value);
                            dgvIndicadorIngreso.Rows[dgvIndicadorIngreso.Rows.Count - 1].Cells[c2].Value = sum2;
                        }

                        sum2 = 0;
                    }

                    dgvIndicadorIngreso.Columns["idOperacion"].Visible = false;
                    dgvIndicadorIngreso.Columns["CONCEPTO"].ReadOnly = true;
                    dgvIndicadorIngreso.Columns["OPERACION"].ReadOnly = true;
                }
            }
            else { btnBuscar.Focus(); }
        }

        public void CargarTablaOPC()
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadCarreta(dtpPeriodo.Text);

            dgvOperatividadCView.DataSource = null;
            dgvOperatividadCView.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                DataRow operativas, total, porcentaje;

                operativas = dt.NewRow();
                operativas[0] = 997;
                operativas[1] = "TOTAL EN RUTA";
                operativas[2] = " ";
                operativas[2] = " ";

                total = dt.NewRow();
                total[0] = 998;
                total[1] = "TOTAL UNIDADES";
                total[2] = " ";
                total[2] = " ";

                porcentaje = dt.NewRow();
                porcentaje[0] = 999;
                porcentaje[1] = "% REA / ASIG";
                porcentaje[2] = " ";

                dt.Rows.Add(operativas);
                dt.Rows.Add(total);
                dt.Rows.Add(porcentaje);

                dgvOperatividadCView.DataSource = dt;
                dgvOperatividadCView.AutoResizeColumns();
                dgvOperatividadCView.Columns["CONCEPTO"].Frozen = true;
                dgvOperatividadCView.Columns["CONCEPTO"].ReadOnly = true;
                dgvOperatividadCView.Columns["CONDICION"].Frozen = true;
                dgvOperatividadCView.Columns["CONDICION"].ReadOnly = true;

                int sum = 0, porc = 0, f, c, divisor, ope = 0;
                for (c = 3; c <= dgvOperatividadCView.Columns.Count - 1; c++)
                {
                    for (f = 0; f < dgvOperatividadCView.Rows.Count - 3; f++)
                    {
                        if (f < 7)
                        {
                            ope += Convert.ToInt32(dgvOperatividadCView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadCView.Rows[f].Cells[c].Value);
                            dgvOperatividadCView.Rows[dgvOperatividadCView.Rows.Count - 3].Cells[c].Value = ope;
                        }

                        sum += Convert.ToInt32(dgvOperatividadCView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadCView.Rows[f].Cells[c].Value);
                        dgvOperatividadCView.Rows[dgvOperatividadCView.Rows.Count - 2].Cells[c].Value = sum;

                        int vendida = Convert.ToInt32(dgvOperatividadCView.Rows[dgvOperatividadCView.Rows.Count - 4].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadCView.Rows[dgvOperatividadCView.Rows.Count - 4].Cells[c].Value);
                        dgvOperatividadCView.Rows[dgvOperatividadCView.Rows.Count - 2].Cells[c].Value = sum - vendida;

                        if (Convert.ToInt32(dgvOperatividadCView.Rows[dgvOperatividadCView.Rows.Count - 2].Cells[c].Value) == 0) { divisor = 1; }
                        else { divisor = Convert.ToInt32(dgvOperatividadCView.Rows[dgvOperatividadCView.Rows.Count - 2].Cells[c].Value); }

                        porc = (Convert.ToInt32(dgvOperatividadCView.Rows[14].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadCView.Rows[14].Cells[c].Value) * 100) / divisor;
                        dgvOperatividadCView.Rows[dgvOperatividadCView.Rows.Count - 1].Cells[c].Value = porc;
                    }

                    sum = 0; porc = 0; ope = 0;
                }

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvOperatividadCView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2BFF39");
                dgvOperatividadCView.EnableHeadersVisualStyles = false;

                dgvOperatividadCView.Columns["idCondicion"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow != 0 && saveRow < dgvOperatividadCView.Rows.Count)
                    {
                        dgvOperatividadCView.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvOperatividadCView.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
            else { btnBuscar.Focus(); }
        }

        public void CargarTablaOPPL()       // PLATAFORMAS
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadPlataformas(dtpPeriodo.Text);

            dgvOperatividadPLView.DataSource = null;
            dgvOperatividadPLView.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                DataRow operativas, total, porcentaje;

                operativas = dt.NewRow();
                operativas[0] = 997;
                operativas[1] = "TOTAL EN RUTA";
                operativas[2] = " ";
                operativas[2] = " ";

                total = dt.NewRow();
                total[0] = 998;
                total[1] = "TOTAL UNIDADES";
                total[2] = " ";
                total[2] = " ";

                porcentaje = dt.NewRow();
                porcentaje[0] = 999;
                porcentaje[1] = "% REA / ASIG";
                porcentaje[2] = " ";

                dt.Rows.Add(operativas);
                dt.Rows.Add(total);
                dt.Rows.Add(porcentaje);

                dgvOperatividadPLView.DataSource = dt;
                dgvOperatividadPLView.AutoResizeColumns();
                dgvOperatividadPLView.Columns["CONCEPTO"].Frozen = true;
                dgvOperatividadPLView.Columns["CONCEPTO"].ReadOnly = true;
                dgvOperatividadPLView.Columns["CONDICION"].Frozen = true;
                dgvOperatividadPLView.Columns["CONDICION"].ReadOnly = true;

                int sum = 0, porc = 0, f, c, divisor, ope = 0;
                for (c = 3; c <= dgvOperatividadPLView.Columns.Count - 1; c++)
                {
                    for (f = 0; f < dgvOperatividadPLView.Rows.Count - 3; f++)
                    {
                        if (f < 2)
                        {
                            ope += Convert.ToInt32(dgvOperatividadPLView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadPLView.Rows[f].Cells[c].Value);
                            dgvOperatividadPLView.Rows[dgvOperatividadPLView.Rows.Count - 3].Cells[c].Value = ope;
                        }

                        sum += Convert.ToInt32(dgvOperatividadPLView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadPLView.Rows[f].Cells[c].Value);
                        dgvOperatividadPLView.Rows[dgvOperatividadPLView.Rows.Count - 2].Cells[c].Value = sum;

                        if (Convert.ToInt32(dgvOperatividadPLView.Rows[dgvOperatividadPLView.Rows.Count - 2].Cells[c].Value) == 0) { divisor = 1; }
                        else { divisor = Convert.ToInt32(dgvOperatividadPLView.Rows[dgvOperatividadPLView.Rows.Count - 2].Cells[c].Value); }

                        porc = (Convert.ToInt32(dgvOperatividadPLView.Rows[6].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadPLView.Rows[6].Cells[c].Value) * 100) / divisor;
                        dgvOperatividadPLView.Rows[dgvOperatividadPLView.Rows.Count - 1].Cells[c].Value = porc;
                    }

                    sum = 0; porc = 0; ope = 0;
                }

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvOperatividadPLView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2BFF39");
                dgvOperatividadPLView.EnableHeadersVisualStyles = false;

                dgvOperatividadPLView.Columns["idCondicion"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow != 0 && saveRow < dgvOperatividadPLView.Rows.Count)
                    {
                        dgvOperatividadPLView.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvOperatividadPLView.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
            else { btnBuscar.Focus(); }
        }

        public void CargarTablaOPTL()       // TOLVAS
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadTolvas(dtpPeriodo.Text);

            dgvOperatividadTLView.DataSource = null;
            dgvOperatividadTLView.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                DataRow operativas, total, porcentaje;

                operativas = dt.NewRow();
                operativas[0] = 997;
                operativas[1] = "TOTAL EN RUTA";
                operativas[2] = " ";
                operativas[2] = " ";

                total = dt.NewRow();
                total[0] = 998;
                total[1] = "TOTAL UNIDADES";
                total[2] = " ";
                total[2] = " ";

                porcentaje = dt.NewRow();
                porcentaje[0] = 999;
                porcentaje[1] = "% REA / ASIG";
                porcentaje[2] = " ";

                dt.Rows.Add(operativas);
                dt.Rows.Add(total);
                dt.Rows.Add(porcentaje);

                dgvOperatividadTLView.DataSource = dt;
                dgvOperatividadTLView.AutoResizeColumns();
                dgvOperatividadTLView.Columns["CONCEPTO"].Frozen = true;
                dgvOperatividadTLView.Columns["CONCEPTO"].ReadOnly = true;
                dgvOperatividadTLView.Columns["CONDICION"].Frozen = true;
                dgvOperatividadTLView.Columns["CONDICION"].ReadOnly = true;

                int sum = 0, porc = 0, f, c, divisor, ope = 0;
                for (c = 3; c <= dgvOperatividadTLView.Columns.Count - 1; c++)
                {
                    for (f = 0; f < dgvOperatividadTLView.Rows.Count - 3; f++)
                    {
                        if (f < 2)
                        {
                            ope += Convert.ToInt32(dgvOperatividadTLView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadTLView.Rows[f].Cells[c].Value);
                            dgvOperatividadTLView.Rows[dgvOperatividadTLView.Rows.Count - 3].Cells[c].Value = ope;
                        }

                        sum += Convert.ToInt32(dgvOperatividadTLView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadTLView.Rows[f].Cells[c].Value);
                        dgvOperatividadTLView.Rows[dgvOperatividadTLView.Rows.Count - 2].Cells[c].Value = sum;

                        if (Convert.ToInt32(dgvOperatividadTLView.Rows[dgvOperatividadTLView.Rows.Count - 2].Cells[c].Value) == 0) { divisor = 1; }
                        else { divisor = Convert.ToInt32(dgvOperatividadTLView.Rows[dgvOperatividadTLView.Rows.Count - 2].Cells[c].Value); }

                        porc = (Convert.ToInt32(dgvOperatividadTLView.Rows[6].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadTLView.Rows[6].Cells[c].Value) * 100) / divisor;
                        dgvOperatividadTLView.Rows[dgvOperatividadTLView.Rows.Count - 1].Cells[c].Value = porc;
                    }

                    sum = 0; porc = 0; ope = 0;
                }

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvOperatividadTLView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2BFF39");
                dgvOperatividadTLView.EnableHeadersVisualStyles = false;

                dgvOperatividadTLView.Columns["idCondicion"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow != 0 && saveRow < dgvOperatividadTLView.Rows.Count)
                    {
                        dgvOperatividadTLView.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvOperatividadTLView.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
            else { btnBuscar.Focus(); }
        }

        public void CargarTablaOPCS()       // CISTERNAS
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadCisternas(dtpPeriodo.Text);

            dgvOperatividadCSView.DataSource = null;
            dgvOperatividadCSView.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                DataRow operativas, total, porcentaje;

                operativas = dt.NewRow();
                operativas[0] = 997;
                operativas[1] = "TOTAL EN RUTA";
                operativas[2] = " ";
                operativas[2] = " ";

                total = dt.NewRow();
                total[0] = 998;
                total[1] = "TOTAL UNIDADES";
                total[2] = " ";
                total[2] = " ";

                porcentaje = dt.NewRow();
                porcentaje[0] = 999;
                porcentaje[1] = "% REA / ASIG";
                porcentaje[2] = " ";

                dt.Rows.Add(operativas);
                dt.Rows.Add(total);
                dt.Rows.Add(porcentaje);

                dgvOperatividadCSView.DataSource = dt;
                dgvOperatividadCSView.AutoResizeColumns();
                dgvOperatividadCSView.Columns["CONCEPTO"].Frozen = true;
                dgvOperatividadCSView.Columns["CONCEPTO"].ReadOnly = true;
                dgvOperatividadCSView.Columns["CONDICION"].Frozen = true;
                dgvOperatividadCSView.Columns["CONDICION"].ReadOnly = true;

                int sum = 0, porc = 0, f, c, divisor, ope = 0;
                for (c = 3; c <= dgvOperatividadCSView.Columns.Count - 1; c++)
                {
                    for (f = 0; f < dgvOperatividadCSView.Rows.Count - 3; f++)
                    {
                        if (f < 2)
                        {
                            ope += Convert.ToInt32(dgvOperatividadCSView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadCSView.Rows[f].Cells[c].Value);
                            dgvOperatividadCSView.Rows[dgvOperatividadCSView.Rows.Count - 3].Cells[c].Value = ope;
                        }

                        sum += Convert.ToInt32(dgvOperatividadCSView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadCSView.Rows[f].Cells[c].Value);
                        dgvOperatividadCSView.Rows[dgvOperatividadCSView.Rows.Count - 2].Cells[c].Value = sum;

                        if (Convert.ToInt32(dgvOperatividadCSView.Rows[dgvOperatividadCSView.Rows.Count - 2].Cells[c].Value) == 0) { divisor = 1; }
                        else { divisor = Convert.ToInt32(dgvOperatividadCSView.Rows[dgvOperatividadCSView.Rows.Count - 2].Cells[c].Value); }

                        porc = (Convert.ToInt32(dgvOperatividadCSView.Rows[7].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadCSView.Rows[7].Cells[c].Value) * 100) / divisor;
                        dgvOperatividadCSView.Rows[dgvOperatividadCSView.Rows.Count - 1].Cells[c].Value = porc;
                    }

                    sum = 0; porc = 0; ope = 0;
                }

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvOperatividadCSView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2BFF39");
                dgvOperatividadCSView.EnableHeadersVisualStyles = false;

                dgvOperatividadCSView.Columns["idCondicion"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow != 0 && saveRow < dgvOperatividadCSView.Rows.Count)
                    {
                        dgvOperatividadCSView.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvOperatividadCSView.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
            else { btnBuscar.Focus(); }
        }

        public void CargarTablaOPCR()       // CORTINERAS
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadCortineras(dtpPeriodo.Text);

            dgvOperatividadCRView.DataSource = null;
            dgvOperatividadCRView.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                DataRow operativas, total, porcentaje;

                operativas = dt.NewRow();
                operativas[0] = 997;
                operativas[1] = "TOTAL EN RUTA";
                operativas[2] = " ";
                operativas[2] = " ";

                total = dt.NewRow();
                total[0] = 998;
                total[1] = "TOTAL UNIDADES";
                total[2] = " ";
                total[2] = " ";

                porcentaje = dt.NewRow();
                porcentaje[0] = 999;
                porcentaje[1] = "% REA / ASIG";
                porcentaje[2] = " ";

                dt.Rows.Add(operativas);
                dt.Rows.Add(total);
                dt.Rows.Add(porcentaje);

                dgvOperatividadCRView.DataSource = dt;
                dgvOperatividadCRView.AutoResizeColumns();
                dgvOperatividadCRView.Columns["CONCEPTO"].Frozen = true;
                dgvOperatividadCRView.Columns["CONCEPTO"].ReadOnly = true;
                dgvOperatividadCRView.Columns["CONDICION"].Frozen = true;
                dgvOperatividadCRView.Columns["CONDICION"].ReadOnly = true;

                int sum = 0, porc = 0, f, c, divisor, ope = 0;
                for (c = 3; c <= dgvOperatividadCRView.Columns.Count - 1; c++)
                {
                    for (f = 0; f < dgvOperatividadCRView.Rows.Count - 3; f++)
                    {
                        if (f < 2)
                        {
                            ope += Convert.ToInt32(dgvOperatividadCRView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadCRView.Rows[f].Cells[c].Value);
                            dgvOperatividadCRView.Rows[dgvOperatividadCRView.Rows.Count - 3].Cells[c].Value = ope;
                        }

                        sum += Convert.ToInt32(dgvOperatividadCRView.Rows[f].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadCRView.Rows[f].Cells[c].Value);
                        dgvOperatividadCRView.Rows[dgvOperatividadCRView.Rows.Count - 2].Cells[c].Value = sum;

                        if (Convert.ToInt32(dgvOperatividadCRView.Rows[dgvOperatividadCRView.Rows.Count - 2].Cells[c].Value) == 0) { divisor = 1; }
                        else { divisor = Convert.ToInt32(dgvOperatividadCRView.Rows[dgvOperatividadCRView.Rows.Count - 2].Cells[c].Value); }

                        porc = (Convert.ToInt32(dgvOperatividadCRView.Rows[7].Cells[c].Value == DBNull.Value ? 0 : dgvOperatividadCRView.Rows[7].Cells[c].Value) * 100) / divisor;
                        dgvOperatividadCRView.Rows[dgvOperatividadCRView.Rows.Count - 1].Cells[c].Value = porc;
                    }

                    sum = 0; porc = 0; ope = 0;
                }

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvOperatividadCRView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2BFF39");
                dgvOperatividadCRView.EnableHeadersVisualStyles = false;

                dgvOperatividadCRView.Columns["idCondicion"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow != 0 && saveRow < dgvOperatividadCRView.Rows.Count)
                    {
                        dgvOperatividadCRView.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvOperatividadCRView.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
            else { btnBuscar.Focus(); }
        }

        public static string CalcularDia(int col)
        {
            string respuesta;
            
            if (col < 10) { respuesta = "0" + col.ToString(); }
            else { respuesta = col.ToString(); }

            return respuesta;
        }


        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (TipoVehiculo == "TRACTOS")
                {
                    if (dgvPlacas.DataSource != null)
                    { ((DataTable)dgvPlacas.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PLACA", txtPlaca.Text); }
                }

                if (TipoVehiculo == "CARRETAS")
                {
                    if (dgvCarretas.DataSource != null)
                    { ((DataTable)dgvCarretas.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PLACA", txtPlaca.Text); }
                }
            }
        }

        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e)
        {
            if (TipoVehiculo == "TRACTOS")
            {
                if (cbxOperaciones.Text == "TODO")
                {
                    if (dgvPlacas.DataSource != null)
                    { ((DataTable)dgvPlacas.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PLACA", txtPlaca.Text); }
                }
                else
                {
                    if (dgvPlacas.DataSource != null)
                    { ((DataTable)dgvPlacas.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PROGRAMACION", cbxOperaciones.Text); }
                }
            }

            if (TipoVehiculo == "CARRETAS")
            {
                if (cbxOperaciones.Text == "TODO")
                {
                    if (dgvCarretas.DataSource != null)
                    { ((DataTable)dgvCarretas.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PLACA", txtPlaca.Text); }
                }
                else
                {
                    if (dgvCarretas.DataSource != null)
                    { ((DataTable)dgvCarretas.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PROGRAMACION", cbxOperaciones.Text); }
                }
            }
        }

        private void btnCargarCondiciones_Click(object sender, EventArgs e)
        {
            DataTable dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(1, dtpPeriodo.Text);
            dtgvData.DataSource = null;
            
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dgvDataView.Columns["IdUnidad"].Visible = false;
                dgvDataView.UpdateSummary();
                dgvDataView.BestFitColumns();

                CargarMapeados();
            }
        }

        private void btnMapear_Click(object sender, EventArgs e)
        {
            int IdUnidad, idCondicion;
            string TipoUnidad;
            int[] filas = dgvDataView.GetSelectedRows();

            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    IdUnidad = Convert.ToInt32(dgvDataView.GetRowCellValue(filas[i], "IdUnidad"));
                    TipoUnidad = Convert.ToString(dgvDataView.GetRowCellValue(filas[i], "TIPO_UNIDAD"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_MapearTractos(IdUnidad, TipoUnidad, dtpPeriodo.Text, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { CargarMapeados(); }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

                for (int j = 1; j <= 17; j++)
                {
                    idCondicion = j;

                    DataTable dtRespuesta2 = new DataTable();
                    string Respuesta2;
                    dtRespuesta2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_MapearCondiciones(idCondicion, dtpPeriodo.Text, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta2 = Convert.ToString(dtRespuesta2.Rows[0]["exito"]);
                    string NroRPTA2 = Respuesta2.Substring(0, 1);
                    if (NroRPTA2 == "0") { }
                    else
                    {
                        MessageBox.Show(Respuesta2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }

                    DataTable dtRespuesta3 = new DataTable();
                    string Respuesta3;
                    dtRespuesta3 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_MapearCondicionesCarretas(idCondicion, dtpPeriodo.Text, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta3 = Convert.ToString(dtRespuesta3.Rows[0]["exito"]);
                    string NroRPTA3 = Respuesta3.Substring(0, 1);
                    if (NroRPTA3 == "0") { }
                    else
                    {
                        MessageBox.Show(Respuesta3, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

                for (int k = 1; k <= 6; k++)
                {
                    DataTable dtRespuesta4 = new DataTable();
                    string Respuesta4;
                    dtRespuesta4 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_InsertarIngresosOperacion(k, dtpPeriodo.Text, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta4 = Convert.ToString(dtRespuesta4.Rows[0]["exito"]);
                    string NroRPTA4 = Respuesta4.Substring(0, 1);
                    if (NroRPTA4 == "0") { }
                    else
                    {
                        MessageBox.Show(Respuesta4, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

                DataTable dtRespuesta5 = new DataTable();
                string Respuesta5;
                dtRespuesta5 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_MapearIndicadores(dtpPeriodo.Text, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta5 = Convert.ToString(dtRespuesta5.Rows[0]["exito"]);
                string NroRPTA5 = Respuesta5.Substring(0, 1);
                if (NroRPTA5 == "0") { }

                btnCargarCondiciones_Click(sender, e);
            }
            else
            { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea quitar esta unidad de la tabla?", "QUITAR UNIDAD OPERATIVA", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int IdUnidad;
                string TipoUnidad;
                int[] filas = dtgvDataMapeadosView.GetSelectedRows();

                if (filas.Length != 0)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        IdUnidad = Convert.ToInt32(dtgvDataMapeadosView.GetRowCellValue(filas[i], "IdUnidad"));
                        TipoUnidad = Convert.ToString(dtgvDataMapeadosView.GetRowCellValue(filas[i], "TIPO_UNIDAD"));

                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_QuitarTractos(IdUnidad, TipoUnidad, dtpPeriodo.Text, Utilitario.Instancia.SesionUsuario.usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0") { }
                        else
                        {
                            MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }

                    btnCargarCondiciones_Click(sender, e);
                }
                else
                { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtpPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (TipoVehiculo == "TRACTOS")
                {
                    CargarTablaPlacas();
                    CargarTabla();
                    CargarIndicadores();
                }

                if (TipoVehiculo == "CARRETAS")
                {
                    CargarTablaCarretas();
                    CargarTablaOPC();
                    CargarTablaOPPL();
                    CargarTablaOPTL();
                    CargarTablaOPCS();
                    CargarTablaOPCR();
                }

                CargarMapeados();
                btnCargarCondiciones_Click(sender, e);
            }
        }

        private void dgvPlacas_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvPlacas.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 3)
                        {
                            dgvPlacas.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvPlacas.ContextMenuStrip = null;
                        }
                        else
                        {
                            IdUnidad = Convert.ToInt32(dgvPlacas.CurrentRow.Cells["IdUnidad"].Value.ToString());
                            Placa = dgvPlacas.CurrentRow.Cells["PLACA"].Value.ToString();
                            Programacion = dgvPlacas.CurrentRow.Cells["PROGRAMACION"].Value.ToString();
                            int dia = Convert.ToInt32(e.ColumnIndex - 2);
                            FechaFijaSelec = CalcularDia(dia);
                            FechaFijaSelec = FechaFijaSelec + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                            dgvPlacas.ContextMenuStrip = contextMenuStrip1;
                            lblFecha.Text = FechaFijaSelec;

                            if (dgvPlacas.Rows.Count > 0 && dgvPlacas.FirstDisplayedCell != null)
                            {
                                saveRow = e.RowIndex;
                                saveCol = e.ColumnIndex;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dgvPlacas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvPlacas.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                // LINDLEY: Rojo  LIMAGAS: Azul  TOLVAS: Verde  GENERAL: Amarillo
                
                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPlacas.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvOperatividadView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvOperatividadView.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 3)
                        {
                            dgvOperatividadView.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvPlacas.ContextMenuStrip = null;
                            dgvOperatividadView.ContextMenuStrip = null;
                        }
                        else
                        {
                            idCondicionOP = Convert.ToInt32(dgvOperatividadView.CurrentRow.Cells["idCondicion"].Value.ToString());
                            int dia = Convert.ToInt32(e.ColumnIndex - 2);
                            string FechaOP = CalcularDia(dia);
                            FechaOP = FechaOP + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");

                            DataTable dtListaTractos = new DataTable();
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                            dtListaTractos.Clear();
                            dtgListaTractos.DataSource = null;
                            dgvListaTractosView.Columns.Clear();
                            dtListaTractos = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractosOP(1, idCondicionOP, Convert.ToDateTime(FechaOP), "", Usuario);
                            
                            if (dtListaTractos.Rows.Count > 0)
                            {    
                                lblEstado.Text = dgvOperatividadView.CurrentRow.Cells["CONDICION"].Value.ToString();
                                lblFecha2.Text = FechaOP;
                                dtgListaTractos.DataSource = dtListaTractos;

                                dgvListaTractosView.Columns["idOperatividad"].Visible = false;
                                dgvListaTractosView.Columns["IdUnidad"].Visible = false;
                                
                                dgvListaTractosView.Columns["PLACA"].Summary.Clear();
                                dgvListaTractosView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");
                                dgvListaTractosView.BestFitColumns();

                                pListarUnidades.Visible = true;
                                pListarUnidades.BringToFront();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dgvOperatividadView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvOperatividadView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < dgvOperatividadView.RowCount; i++)
                {
                    if (Convert.ToString(dgvOperatividadView.Rows[e.RowIndex].Cells[i].Value) == "OPERATIVAS")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL EN RUTA")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL UNIDADES")
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvOperatividadView.Rows[e.RowIndex].Cells[i].Value) == "% REA / ASIG")
                    {
                        e.CellStyle.BackColor = Color.LightSkyBlue;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvOperatividadView.Rows[e.RowIndex].Cells[i].Value) == "UT SIN PROGRAMACION")
                    {
                        e.CellStyle.BackColor = Color.Cyan;
                        e.CellStyle.ForeColor = Color.Blue;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvOperatividadView.Rows[e.RowIndex].Cells[i].Value) == "UT SIN CONDUCTOR")
                    {
                        e.CellStyle.BackColor = Color.Cyan;
                        e.CellStyle.ForeColor = Color.Blue;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadView.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void ingresarCantidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TipoUnidad = "T";
            rbTractos.Checked = true;
            rbTractos_Click(sender, e);
            DataTable workTable = new DataTable("Marcaciones");
            DataColumn column1 = new DataColumn("IdUnidad");
            DataColumn column2 = new DataColumn("Fecha");

            workTable.Columns.Add(column1);
            workTable.Columns.Add(column2);
            DataRow row1 = workTable.NewRow();
            row1["IdUnidad"] = "1";
            row1["Fecha"] = "2";
            workTable.Rows.Add(row1);

            Int32 selectedCellCount = dgvPlacas.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                if (dgvPlacas.AreAllCellsSelected(true)) { MessageBox.Show("Todas las celdas están seleccionadas.", "Selected Cells"); }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<r>");

                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        int col = Convert.ToInt32(dgvPlacas.SelectedCells[i].ColumnIndex.ToString()) - 2;
                        string dia = CalcularDia(col);
                        dia = dia + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                        sb.Append("<d ");
                        sb.Append("IdUnidad=\"");
                        sb.Append(dgvPlacas.Rows[dgvPlacas.SelectedCells[i].RowIndex].Cells[0].Value.ToString());
                        sb.Append("\"");
                        sb.Append(" Fecha=\"");
                        sb.Append(dia);
                        sb.Append("\"");
                        sb.Append(" />");
                    }

                    sb.Append("</r>");
                    xmlOperatividad = sb.ToString();

                    pAgregarCantidad.Visible = true;
                    pAgregarCantidad.BringToFront();
                    lblProgramacion.Text = Programacion;
                    lblPlaca.Text = Placa;
                    cbxCondicion_DropDownClosed(sender, e);
                    cbxOperacion.Text = Programacion;
                }
            }
            else
            { MessageBox.Show("No puede ingresar una cantidad en una casilla sin seleccionar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }



        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pAgregarCantidad.Visible = false;
            pAgregarCantidad.SendToBack();
            CargarComboCondicion();
            CargarComboOperacion();
            lblPlaca.Text = "";
            xmlOperatividad = "";
            IdUnidad = -1;
        }

        private void pAgregarCantidad_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pAgregarCantidad.Left = pAgregarCantidad.Left + (e.X - xClick);
                pAgregarCantidad.Top = pAgregarCantidad.Top + (e.Y - yClick);
            }
        }

        private void cbxCondicion_DropDownClosed(object sender, EventArgs e)
        {
            int idCondicion = Convert.ToInt32(cbxCondicion.SelectedValue);

            if (idCondicion == 1 || idCondicion == 2 || idCondicion == 3 || idCondicion == 4 || idCondicion == 16) { cbxOperacion.Enabled = false; }
            else { cbxOperacion.Enabled = true; }
        }

        private void cbxCarreta_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxCarreta.Text == "TODOS")
            {
                if (dgvCarretas.DataSource != null)
                { ((DataTable)dgvCarretas.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PLACA", txtPlaca.Text); }
            }
            else
            {
                if (dgvCarretas.DataSource != null)
                { ((DataTable)dgvCarretas.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "TIPO_VEHICULO", cbxCarreta.Text); }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (TipoUnidad == "T")
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_RegistrarPlacas(dtpPeriodo.Text, xmlOperatividad,
                              Convert.ToInt32(cbxCondicion.SelectedValue), Convert.ToInt32(cbxOperacion.SelectedValue), Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbTractos.Checked = true;
                    rbTractos_Click(sender, e);
                    CargarTablaPlacas();
                    CargarTabla();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            
            if (TipoUnidad == "C")
            {
                DataTable dtRespuesta2 = new DataTable();
                string Respuesta2;
                string Usuario2 = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta2 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_RegistrarCarretas(dtpPeriodo.Text, xmlOperatividad,
                              Convert.ToInt32(cbxCondicion.SelectedValue), Convert.ToInt32(cbxOperacion.SelectedValue), Usuario2);
                Respuesta2 = Convert.ToString(dtRespuesta2.Rows[0]["exito"]);
                string NroRPTA = Respuesta2.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar_Click(sender, e);
                    MessageBox.Show(Respuesta2, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    rbCarretas.Checked = true;
                    rbCarretas_Click(sender, e);
                    CargarTablaCarretas();
                    CargarTablaOPC();
                    CargarTablaOPPL();
                    CargarTablaOPTL();
                    CargarTablaOPCS();
                    CargarTablaOPCR();
                }
                else { MessageBox.Show(Respuesta2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (TipoVehiculo == "TRACTOS")
            {
                CargarTablaPlacas();
                CargarTabla();
                CargarIndicadores();
            }

            if (TipoVehiculo == "CARRETAS")
            {
                CargarTablaCarretas();
                CargarTablaOPC();
                CargarTablaOPPL();
                CargarTablaOPTL();
                CargarTablaOPCS();
                CargarTablaOPCR();
            }
            
            CargarMapeados();
            btnCargarCondiciones_Click(sender, e);
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable dt6 = new System.Data.DataTable();
                dt6 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaOperatividad(dtpPeriodo.Text);
                gcExcel.DataSource = null;
                gvexcel.Columns.Clear();
                gcExcel.DataSource = dt6;
                gvexcel.BestFitColumns();

                System.Data.DataTable dt7 = new System.Data.DataTable();
                dt7 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaIngresos(dtpPeriodo.Text);
                gcIngresosT.DataSource = null;
                gvIngresosT.Columns.Clear();
                gcIngresosT.DataSource = dt7;
                gvIngresosT.BestFitColumns();

                System.Data.DataTable dt8 = new System.Data.DataTable();
                dt8 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadCarreta(dtpPeriodo.Text);
                gcExcel2.DataSource = null;
                gvExcel2.Columns.Clear();
                gcExcel2.DataSource = dt8;
                gvExcel2.BestFitColumns();

                System.Data.DataTable dt9 = new System.Data.DataTable();
                dt9 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadPlataformas(dtpPeriodo.Text);
                gcExcel3.DataSource = null;
                gvExcel3.Columns.Clear();
                gcExcel3.DataSource = dt9;
                gvExcel3.BestFitColumns();

                System.Data.DataTable dt10 = new System.Data.DataTable();
                dt10 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadTolvas(dtpPeriodo.Text);
                gcExcel4.DataSource = null;
                gvExcel4.Columns.Clear();
                gcExcel4.DataSource = dt10;
                gvExcel4.BestFitColumns();

                System.Data.DataTable dt11 = new System.Data.DataTable();
                dt11 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadCisternas(dtpPeriodo.Text);
                gcExcel5.DataSource = null;
                gvExcel5.Columns.Clear();
                gcExcel5.DataSource = dt11;
                gvExcel5.BestFitColumns();

                System.Data.DataTable dt12 = new System.Data.DataTable();
                dt12 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarOperatividadCortineras(dtpPeriodo.Text);
                gcExcel6.DataSource = null;
                gvExcel6.Columns.Clear();
                gcExcel6.DataSource = dt12;
                gvExcel6.BestFitColumns();

                System.Data.DataTable dt13 = new System.Data.DataTable();
                dt13 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarIndicadores(1, dtpPeriodo.Text);
                gcExcelI.DataSource = null;
                gvExcelI.Columns.Clear();
                gcExcelI.DataSource = dt13;
                gvExcelI.BestFitColumns();

                System.Data.DataTable dt14 = new System.Data.DataTable();
                dt14 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarIndicadores(2, dtpPeriodo.Text);
                gcIngresosI.DataSource = null;
                gvIngresosI.Columns.Clear();
                gcIngresosI.DataSource = dt14;
                gvIngresosI.BestFitColumns();

                gcExcel.ForceInitialize();
                gcIngresosT.ForceInitialize();
                gcExcelI.ForceInitialize();
                gcIngresosI.ForceInitialize();
                gcExcel2.ForceInitialize();
                gcExcel3.ForceInitialize();
                gcExcel4.ForceInitialize();
                gcExcel5.ForceInitialize();
                gcExcel6.ForceInitialize();

                compositeLink1.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "Registro de Operatividad - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink1.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExcelTractos_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable dt8 = new System.Data.DataTable();
                dt8 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadTractos(dtpPeriodo.Text, txtPlaca.Text, cbxOperaciones.Text);
                gcExcelPlacas.DataSource = null;
                gvExcelPlacas.Columns.Clear();
                gcExcelPlacas.DataSource = dt8;
                gvExcelPlacas.BestFitColumns();

                System.Data.DataTable dt9 = new System.Data.DataTable();
                dt9 = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTablaOperatividadCarreta(dtpPeriodo.Text, txtPlaca.Text, cbxOperaciones.Text, cbxCarreta.Text);
                gcExcelC.DataSource = null;
                gvExcelC.Columns.Clear();
                gcExcelC.DataSource = dt9;
                gvExcelC.BestFitColumns();

                gcExcelPlacas.ForceInitialize();
                gcExcelC.ForceInitialize();
                compositeLink2.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "Estado de Placas - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink2.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pListarUnidades_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pListarUnidades.Left = pListarUnidades.Left + (e.X - xClick2);
                pListarUnidades.Top = pListarUnidades.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pListarUnidades.Visible = false;
            pListarUnidades.SendToBack();
            lblEstado.Text = "";
            lblFecha2.Text = "";
        }

        private void dgvListaTractosView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var row = (DataRowView)e.Row;

                int idOperatividad = Convert.ToInt32(row["idOperatividad"].ToString());
                string TipoUnidad = row["TIPO_UNIDAD"].ToString();
                string Comentario = row["COMENTARIO"].ToString();

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_IngresarComentarios(idOperatividad, TipoUnidad, Comentario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            }
            catch { }
        }

        private void rbTractos_Click(object sender, EventArgs e)
        {
            TipoVehiculo = "TRACTOS";
            rbTractos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            rbCarretas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
        }

        private void rbCarretas_Click(object sender, EventArgs e)
        {
            TipoVehiculo = "CARRETAS";
            rbTractos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbCarretas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
        }

        private void dgvCarretas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvCarretas.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                // LINDLEY: Rojo  LIMAGAS: Azul  TOLVAS: Verde  GENERAL: Amarillo

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCarretas.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("DF"))
                            {
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(TL)"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(G)"))
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(LG)"))
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(ACL)"))
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(V)"))
                            {
                                e.CellStyle.BackColor = Color.Plum;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value).Contains("(SG)"))
                            {
                                e.CellStyle.BackColor = Color.Aqua;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvOperatividadCView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvOperatividadCView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < dgvOperatividadCView.RowCount; i++)
                {
                    if (Convert.ToString(dgvOperatividadCView.Rows[e.RowIndex].Cells[i].Value) == "OPERATIVAS")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadCView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL EN RUTA")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadCView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL UNIDADES")
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvOperatividadCView.Rows[e.RowIndex].Cells[i].Value) == "% REA / ASIG")
                    {
                        e.CellStyle.BackColor = Color.LightSkyBlue;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCView.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvCarretas_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvCarretas.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 4)
                        {
                            dgvCarretas.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvCarretas.ContextMenuStrip = null;
                        }
                        else
                        {
                            IdUnidad = Convert.ToInt32(dgvCarretas.CurrentRow.Cells["IdUnidad"].Value.ToString());
                            Placa = dgvCarretas.CurrentRow.Cells["PLACA"].Value.ToString();
                            Programacion = dgvCarretas.CurrentRow.Cells["PROGRAMACION"].Value.ToString();
                            int dia = Convert.ToInt32(e.ColumnIndex - 3);
                            FechaFijaSelec = CalcularDia(dia);
                            FechaFijaSelec = FechaFijaSelec + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                            dgvCarretas.ContextMenuStrip = contextMenuStrip2;
                            lblFecha.Text = FechaFijaSelec;

                            if (dgvCarretas.Rows.Count > 0 && dgvCarretas.FirstDisplayedCell != null)
                            {
                                saveRow = e.RowIndex;
                                saveCol = e.ColumnIndex;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void tsIngresarCantidad_Click(object sender, EventArgs e)
        {
            TipoUnidad = "C";
            rbCarretas.Checked = true;
            rbCarretas_Click(sender, e);
            DataTable workTable = new DataTable("Marcaciones");
            DataColumn column1 = new DataColumn("IdUnidad");
            DataColumn column2 = new DataColumn("Fecha");

            workTable.Columns.Add(column1);
            workTable.Columns.Add(column2);
            DataRow row1 = workTable.NewRow();
            row1["IdUnidad"] = "1";
            row1["Fecha"] = "2";
            workTable.Rows.Add(row1);

            Int32 selectedCellCount = dgvCarretas.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                if (dgvCarretas.AreAllCellsSelected(true)) { MessageBox.Show("Todas las celdas están seleccionadas.", "Selected Cells"); }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<r>");

                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        int col = Convert.ToInt32(dgvCarretas.SelectedCells[i].ColumnIndex.ToString()) - 3;
                        string dia = CalcularDia(col);
                        dia = dia + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                        sb.Append("<d ");
                        sb.Append("IdUnidad=\"");
                        sb.Append(dgvCarretas.Rows[dgvCarretas.SelectedCells[i].RowIndex].Cells[0].Value.ToString());
                        sb.Append("\"");
                        sb.Append(" Fecha=\"");
                        sb.Append(dia);
                        sb.Append("\"");
                        sb.Append(" />");
                    }

                    sb.Append("</r>");
                    xmlOperatividad = sb.ToString();

                    pAgregarCantidad.Visible = true;
                    pAgregarCantidad.BringToFront();
                    lblProgramacion.Text = Programacion;
                    lblPlaca.Text = Placa;
                    cbxCondicion_DropDownClosed(sender, e);
                    cbxOperacion.Text = Programacion;
                }
            }
            else
            { MessageBox.Show("No puede ingresar una cantidad en una casilla sin seleccionar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvOperatividadCView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvOperatividadCView.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 3)
                        {
                            dgvOperatividadCView.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvCarretas.ContextMenuStrip = null;
                            dgvOperatividadCView.ContextMenuStrip = null;
                        }
                        else
                        {
                            idCondicionOP = Convert.ToInt32(dgvOperatividadCView.CurrentRow.Cells["idCondicion"].Value.ToString());
                            int dia = Convert.ToInt32(e.ColumnIndex - 2);
                            string FechaOP = CalcularDia(dia);
                            FechaOP = FechaOP + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");

                            DataTable dtListaCarretas = new DataTable();
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                            dtListaCarretas.Clear();
                            dtgListaTractos.DataSource = null;
                            dgvListaTractosView.Columns.Clear();
                            dtListaCarretas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractosOP(2, idCondicionOP, Convert.ToDateTime(FechaOP), "TODOS", Usuario);
                            
                            if (dtListaCarretas.Rows.Count > 0)
                            {
                                lblEstado.Text = dgvOperatividadCView.CurrentRow.Cells["CONDICION"].Value.ToString();
                                lblFecha2.Text = FechaOP;
                                dtgListaTractos.DataSource = dtListaCarretas;

                                dgvListaTractosView.Columns["idOperatividad"].Visible = false;
                                dgvListaTractosView.Columns["IdUnidad"].Visible = false;

                                dgvListaTractosView.Columns["PLACA"].Summary.Clear();
                                dgvListaTractosView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");
                                dgvListaTractosView.BestFitColumns();

                                pListarUnidades.Visible = true;
                                pListarUnidades.BringToFront();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dgvIngresosView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvIngresosView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < dgvIngresosView.RowCount; i++)
                {
                    if (Convert.ToString(dgvIngresosView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL: ")
                    {
                        e.CellStyle.BackColor = Color.MistyRose;
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvOperatividadPLView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvOperatividadPLView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < dgvOperatividadPLView.RowCount; i++)
                {
                    if (Convert.ToString(dgvOperatividadPLView.Rows[e.RowIndex].Cells[i].Value) == "OPERATIVAS")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadPLView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL EN RUTA")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadPLView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL UNIDADES")
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvOperatividadPLView.Rows[e.RowIndex].Cells[i].Value) == "% REA / ASIG")
                    {
                        e.CellStyle.BackColor = Color.LightSkyBlue;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadPLView.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvOperatividadTLView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvOperatividadTLView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < dgvOperatividadTLView.RowCount; i++)
                {
                    if (Convert.ToString(dgvOperatividadTLView.Rows[e.RowIndex].Cells[i].Value) == "OPERATIVAS")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadTLView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL EN RUTA")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadTLView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL UNIDADES")
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvOperatividadTLView.Rows[e.RowIndex].Cells[i].Value) == "% REA / ASIG")
                    {
                        e.CellStyle.BackColor = Color.LightSkyBlue;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadTLView.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvOperatividadCSView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvOperatividadCSView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < dgvOperatividadCSView.RowCount; i++)
                {
                    if (Convert.ToString(dgvOperatividadCSView.Rows[e.RowIndex].Cells[i].Value) == "OPERATIVAS")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadCSView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL EN RUTA")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadCSView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL UNIDADES")
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvOperatividadCSView.Rows[e.RowIndex].Cells[i].Value) == "% REA / ASIG")
                    {
                        e.CellStyle.BackColor = Color.LightSkyBlue;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCSView.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvOperatividadCRView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvOperatividadCRView.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < dgvOperatividadCRView.RowCount; i++)
                {
                    if (Convert.ToString(dgvOperatividadCRView.Rows[e.RowIndex].Cells[i].Value) == "OPERATIVAS")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadCRView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL EN RUTA")
                    {
                        if (Convert.ToString(e.Value) != "0")
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.ForeColor = Color.DarkBlue;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }

                    if (Convert.ToString(dgvOperatividadCRView.Rows[e.RowIndex].Cells[i].Value) == "TOTAL UNIDADES")
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvOperatividadCRView.Rows[e.RowIndex].Cells[i].Value) == "% REA / ASIG")
                    {
                        e.CellStyle.BackColor = Color.LightSkyBlue;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvOperatividadCRView.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "0")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvOperatividadPLView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvOperatividadPLView.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 3)
                        {
                            dgvOperatividadPLView.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvCarretas.ContextMenuStrip = null;
                            dgvOperatividadPLView.ContextMenuStrip = null;
                        }
                        else
                        {
                            idCondicionOP = Convert.ToInt32(dgvOperatividadPLView.CurrentRow.Cells["idCondicion"].Value.ToString());
                            int dia = Convert.ToInt32(e.ColumnIndex - 2);
                            string FechaOP = CalcularDia(dia);
                            FechaOP = FechaOP + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");

                            DataTable dtListaCarretas = new DataTable();
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                            dtListaCarretas.Clear();
                            dtgListaTractos.DataSource = null;
                            dgvListaTractosView.Columns.Clear();
                            dtListaCarretas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractosOP(2, idCondicionOP, Convert.ToDateTime(FechaOP), "PLATAFORMA", Usuario);
                            
                            if (dtListaCarretas.Rows.Count > 0)
                            {
                                lblEstado.Text = dgvOperatividadPLView.CurrentRow.Cells["CONDICION"].Value.ToString();
                                lblFecha2.Text = FechaOP;
                                dtgListaTractos.DataSource = dtListaCarretas;

                                dgvListaTractosView.Columns["idOperatividad"].Visible = false;
                                dgvListaTractosView.Columns["IdUnidad"].Visible = false;

                                dgvListaTractosView.Columns["PLACA"].Summary.Clear();
                                dgvListaTractosView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");
                                dgvListaTractosView.BestFitColumns();

                                pListarUnidades.Visible = true;
                                pListarUnidades.BringToFront();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dgvOperatividadTLView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvOperatividadTLView.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 3)
                        {
                            dgvOperatividadTLView.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvCarretas.ContextMenuStrip = null;
                            dgvOperatividadTLView.ContextMenuStrip = null;
                        }
                        else
                        {
                            idCondicionOP = Convert.ToInt32(dgvOperatividadTLView.CurrentRow.Cells["idCondicion"].Value.ToString());
                            int dia = Convert.ToInt32(e.ColumnIndex - 2);
                            string FechaOP = CalcularDia(dia);
                            FechaOP = FechaOP + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");

                            DataTable dtListaCarretas = new DataTable();
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                            dtListaCarretas.Clear();
                            dtgListaTractos.DataSource = null;
                            dgvListaTractosView.Columns.Clear();
                            dtListaCarretas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractosOP(2, idCondicionOP, Convert.ToDateTime(FechaOP), "TOLVA", Usuario);
                            
                            if (dtListaCarretas.Rows.Count > 0)
                            {
                                lblEstado.Text = dgvOperatividadTLView.CurrentRow.Cells["CONDICION"].Value.ToString();
                                lblFecha2.Text = FechaOP;
                                dtgListaTractos.DataSource = dtListaCarretas;

                                dgvListaTractosView.Columns["idOperatividad"].Visible = false;
                                dgvListaTractosView.Columns["IdUnidad"].Visible = false;

                                dgvListaTractosView.Columns["PLACA"].Summary.Clear();
                                dgvListaTractosView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");
                                dgvListaTractosView.BestFitColumns();

                                pListarUnidades.Visible = true;
                                pListarUnidades.BringToFront();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dgvOperatividadCSView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvOperatividadCSView.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 3)
                        {
                            dgvOperatividadCSView.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvCarretas.ContextMenuStrip = null;
                            dgvOperatividadCSView.ContextMenuStrip = null;
                        }
                        else
                        {
                            idCondicionOP = Convert.ToInt32(dgvOperatividadCSView.CurrentRow.Cells["idCondicion"].Value.ToString());
                            int dia = Convert.ToInt32(e.ColumnIndex - 2);
                            string FechaOP = CalcularDia(dia);
                            FechaOP = FechaOP + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");

                            DataTable dtListaCarretas = new DataTable();
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                            dtListaCarretas.Clear();
                            dtgListaTractos.DataSource = null;
                            dgvListaTractosView.Columns.Clear();
                            dtListaCarretas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractosOP(2, idCondicionOP, Convert.ToDateTime(FechaOP), "CISTERNA", Usuario);
                            
                            if (dtListaCarretas.Rows.Count > 0)
                            {
                                lblEstado.Text = dgvOperatividadCSView.CurrentRow.Cells["CONDICION"].Value.ToString();
                                lblFecha2.Text = FechaOP;
                                dtgListaTractos.DataSource = dtListaCarretas;

                                dgvListaTractosView.Columns["idOperatividad"].Visible = false;
                                dgvListaTractosView.Columns["IdUnidad"].Visible = false;

                                dgvListaTractosView.Columns["PLACA"].Summary.Clear();
                                dgvListaTractosView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");
                                dgvListaTractosView.BestFitColumns();

                                pListarUnidades.Visible = true;
                                pListarUnidades.BringToFront();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dgvOperatividadCRView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvOperatividadCRView.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 3)
                        {
                            dgvOperatividadCRView.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvCarretas.ContextMenuStrip = null;
                            dgvOperatividadCRView.ContextMenuStrip = null;
                        }
                        else
                        {
                            idCondicionOP = Convert.ToInt32(dgvOperatividadCRView.CurrentRow.Cells["idCondicion"].Value.ToString());
                            int dia = Convert.ToInt32(e.ColumnIndex - 2);
                            string FechaOP = CalcularDia(dia);
                            FechaOP = FechaOP + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");

                            DataTable dtListaCarretas = new DataTable();
                            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                            dtListaCarretas.Clear();
                            dtgListaTractos.DataSource = null;
                            dgvListaTractosView.Columns.Clear();
                            dtListaCarretas = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractosOP(2, idCondicionOP, Convert.ToDateTime(FechaOP), "CORTINERA", Usuario);
                            
                            if (dtListaCarretas.Rows.Count > 0)
                            {
                                lblEstado.Text = dgvOperatividadCRView.CurrentRow.Cells["CONDICION"].Value.ToString();
                                lblFecha2.Text = FechaOP;
                                dtgListaTractos.DataSource = dtListaCarretas;

                                dgvListaTractosView.Columns["idOperatividad"].Visible = false;
                                dgvListaTractosView.Columns["IdUnidad"].Visible = false;

                                dgvListaTractosView.Columns["PLACA"].Summary.Clear();
                                dgvListaTractosView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");
                                dgvListaTractosView.BestFitColumns();

                                pListarUnidades.Visible = true;
                                pListarUnidades.BringToFront();
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dgvIndicadorUnidad_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvIndicadorUnidad.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < dgvIndicadorUnidad.ColumnCount; i++)
                {
                    if (Convert.ToString(dgvIndicadorUnidad.Rows[e.RowIndex].Cells[i].Value) == "OPERATIVAS")
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.DarkBlue;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvIndicadorUnidad.Rows[e.RowIndex].Cells[i].Value) == "PROMEDIO EN RUTA")
                    {
                        e.CellStyle.BackColor = Color.LightGreen;
                        e.CellStyle.ForeColor = Color.DarkBlue;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvIndicadorUnidad.Rows[e.RowIndex].Cells[i].Value) == "CANTIDAD TOTAL")
                    {
                        e.CellStyle.BackColor = Color.Yellow;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvIndicadorUnidad.Rows[e.RowIndex].Cells[i].Value) == "% PROMEDIO")
                    {
                        e.CellStyle.BackColor = Color.LightSkyBlue;
                        e.CellStyle.ForeColor = Color.Black;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvIndicadorUnidad.Rows[e.RowIndex].Cells[i].Value) == "UT SIN PROGRAMACION")
                    {
                        e.CellStyle.BackColor = Color.Cyan;
                        e.CellStyle.ForeColor = Color.Blue;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }

                    if (Convert.ToString(dgvIndicadorUnidad.Rows[e.RowIndex].Cells[i].Value) == "UT SIN CONDUCTOR")
                    {
                        e.CellStyle.BackColor = Color.Cyan;
                        e.CellStyle.ForeColor = Color.Blue;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvIndicadorIngreso_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvIndicadorIngreso.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < dgvIndicadorIngreso.RowCount; i++)
                {
                    if (Convert.ToString(dgvIndicadorIngreso.Rows[e.RowIndex].Cells[i].Value) == "SUMA TOTAL: ")
                    {
                        e.CellStyle.BackColor = Color.Thistle;
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception) { throw; }
        }
    }
}
