using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Neumaticos
{
    public partial class frmControlNeumaticos : Form
    {
        int OpcionT = 1, OpcionM = 1, OpcionC = 0, OpcionM2 = 0;
        int L1E1 = 0, L1E2 = 0, L1E3 = 0, L2E1 = 0, L2E2 = 0, L2E3 = 0;
        int OpcionEditar = 0;
        int idRegistro;
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public int xClick3 = 0, yClick3 = 0;
        int saveRow = 0, saveCol = 0;
        int TotalMttoProg = 0, TotalMttoEje = 0;
        DataTable dtNeumaticosT = new DataTable();
        DataTable dtNeumaticosM = new DataTable();
        DataTable dtHistorialT = new DataTable();
        DataTable dtHistorialM = new DataTable();
        DataTable dtPermisos = new DataTable();
        string Area;

        public frmControlNeumaticos()
        {
            InitializeComponent();
            cbxTipoMaquina.SelectedIndexChanged -= cbxTipoMaquina_SelectedIndexChanged;
        }

        private void cbxTipoMaquina_SelectedIndexChanged(object sender, EventArgs e) { CargarComboMaquinas(); }

        private void frmControlNeumaticos_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmControlNeumaticos");

            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true)
                {
                    btnProgramarCumplimiento.Enabled = true;
                    btnProgramarCumplimiento2.Enabled = true;
                    btnActualizar.Enabled = true;
                    tsActualizarEstado.Enabled = true;
                    tsEliminarCump.Enabled = true;
                }
                else
                {
                    btnProgramarCumplimiento.Enabled = false;
                    btnProgramarCumplimiento2.Enabled = false;
                    btnActualizar.Enabled = false;
                    tsActualizarEstado.Enabled = false;
                    tsEliminarCump.Enabled = false;
                }
            }

            dtpFechaInicio.Value = new DateTime(dtpFechaInicio.Value.Year, dtpFechaInicio.Value.Month, 1);
            dtpFechaFin.Value = new DateTime(dtpFechaFin.Value.Year, dtpFechaFin.Value.Month, 1).AddMonths(1);
            dtpInicioMaquina.Value = new DateTime(dtpInicioMaquina.Value.Year, dtpInicioMaquina.Value.Month, 1);
            dtpFinMaquina.Value = new DateTime(dtpFinMaquina.Value.Year, dtpFinMaquina.Value.Month, 1).AddMonths(1);
            dtpAnio.Value = DateTime.Now;
            txtNroSemana.Text = "0";

            CargarComboMaquinas();
            cbxTipoMaquina.Text = "TODOS";

            string UsuarioAcceso = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dtAreaUsuario = new DataTable();
            dtAreaUsuario = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_BuscarUsuarios(UsuarioAcceso);

            if (dtAreaUsuario.Rows.Count > 0) { Area = dtAreaUsuario.Rows[0]["AREA"].ToString(); }

            if (Area == "MANTENIMIENTO" || Area == "SISTEMAS")
            {
                ListarRegistrosT();
                ListarRegistrosM();
            }
            else
            {
                groupBox2.Enabled = false;
                groupBox1.Enabled = false;
                btnBuscar.Enabled = false;
                btnExcel.Enabled = false;
                txtMaquina.Enabled = false;
                cbxTipoMaquina.Enabled = false;
                groupBox3.Enabled = false;
                btnBuscarMaquina.Enabled = false;
                btnExcelMaquina.Enabled = false;

                toolStrip1.Enabled = false;
                toolStrip2.Enabled = false;
                dtgAlineamientoT.ContextMenuStrip = null;
                dtgAlineamientoM.ContextMenuStrip = null;
            }

            dgvCumplimiento.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCumplimiento.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvCumplimiento.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvPorcentaje.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPorcentaje.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPorcentaje.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }


        public void CargarComboMaquinas()
        {
            DataTable dtTipo2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarGrupoMaquina(2);
            cbxTipoMaquina.DataSource = dtTipo2;
            cbxTipoMaquina.DisplayMember = "DescripcionLocal";
            cbxTipoMaquina.ValueMember = "TipoMaquina";
        }

        public void ListarRegistrosT()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtNeumaticosT = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ListarRegistros(1, txtPlaca.Text, "", dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgAlineamientoT.DataSource = dtNeumaticosT;
                
                if (dtNeumaticosT.Rows.Count > 0)
                {
                    dgvAlineamientoTVista.Columns["idRegistro"].Visible = false;
                    dgvAlineamientoTVista.Columns["idVehiculo"].Visible = false;
                    dgvAlineamientoTVista.Columns["L1E1"].Visible = false;
                    dgvAlineamientoTVista.Columns["L1E2"].Visible = false;
                    dgvAlineamientoTVista.Columns["L1E3"].Visible = false;
                    dgvAlineamientoTVista.Columns["L2E1"].Visible = false;
                    dgvAlineamientoTVista.Columns["L2E2"].Visible = false;
                    dgvAlineamientoTVista.Columns["L2E3"].Visible = false;

                    dgvAlineamientoTVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAlineamientoTVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvAlineamientoTVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAlineamientoTVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvAlineamientoTVista.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAlineamientoTVista.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    
                    dgvAlineamientoTVista.Columns["ESTADO"].Summary.Clear();
                    dgvAlineamientoTVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvAlineamientoTVista.BestFitColumns();
                }
            }
        }

        public void ListarHistorialT()
        {
            if (dtpFechaInicio.Value > dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaInicio.Focus();
                return;
            }
            else
            {
                dtHistorialT = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ListarRegistros(3, txtPlaca.Text, "", dtpFechaInicio.Text, dtpFechaFin.Text);
                dtgAlineamientoT.DataSource = dtHistorialT;

                if (dtHistorialT.Rows.Count > 0)
                {
                    dgvAlineamientoTVista.Columns["idRegistro"].Visible = false;
                    dgvAlineamientoTVista.Columns["L1E1"].Visible = false;
                    dgvAlineamientoTVista.Columns["L1E2"].Visible = false;
                    dgvAlineamientoTVista.Columns["L1E3"].Visible = false;
                    dgvAlineamientoTVista.Columns["L2E1"].Visible = false;
                    dgvAlineamientoTVista.Columns["L2E2"].Visible = false;
                    dgvAlineamientoTVista.Columns["L2E3"].Visible = false;

                    dgvAlineamientoTVista.Columns["KM_ANTERIOR"].Summary.Clear();
                    dgvAlineamientoTVista.Columns["KM_ANTERIOR"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvAlineamientoTVista.BestFitColumns();
                }
            }
        }

        public void ListarRegistrosM()
        {
            if (dtpInicioMaquina.Value > dtpFinMaquina.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpInicioMaquina.Focus();
                return;
            }
            else
            {
                dtNeumaticosM = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ListarRegistros(2, txtMaquina.Text, Convert.ToString(cbxTipoMaquina.SelectedValue), dtpInicioMaquina.Text, dtpFinMaquina.Text);
                dtgAlineamientoM.DataSource = dtNeumaticosM;

                if (dtNeumaticosM.Rows.Count > 0)
                {
                    dgvAlineamientoMVista.Columns["idRegistro"].Visible = false;
                    dgvAlineamientoMVista.Columns["L1E1"].Visible = false;
                    dgvAlineamientoMVista.Columns["L1E2"].Visible = false;
                    dgvAlineamientoMVista.Columns["L1E3"].Visible = false;
                    dgvAlineamientoMVista.Columns["L2E1"].Visible = false;
                    dgvAlineamientoMVista.Columns["L2E2"].Visible = false;
                    dgvAlineamientoMVista.Columns["L2E3"].Visible = false;

                    dgvAlineamientoMVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAlineamientoMVista.Columns["FECHA_ANTERIOR"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvAlineamientoMVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAlineamientoMVista.Columns["FECHA_ACTUAL"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvAlineamientoMVista.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvAlineamientoMVista.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatString = "dd/MM/yyyy";

                    dgvAlineamientoMVista.Columns["ESTADO"].Summary.Clear();
                    dgvAlineamientoMVista.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvAlineamientoMVista.BestFitColumns();
                }
            }
        }

        public void ListarHistorialM()
        {
            if (dtpInicioMaquina.Value > dtpFinMaquina.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor o igual que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpInicioMaquina.Focus();
                return;
            }
            else
            {
                dtHistorialM = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ListarRegistros(4, txtMaquina.Text, Convert.ToString(cbxTipoMaquina.SelectedValue), dtpInicioMaquina.Text, dtpFinMaquina.Text);
                dtgAlineamientoM.DataSource = dtHistorialM;

                if (dtHistorialM.Rows.Count > 0)
                {
                    dgvAlineamientoMVista.Columns["idRegistro"].Visible = false;
                    dgvAlineamientoMVista.Columns["L1E1"].Visible = false;
                    dgvAlineamientoMVista.Columns["L1E2"].Visible = false;
                    dgvAlineamientoMVista.Columns["L1E3"].Visible = false;
                    dgvAlineamientoMVista.Columns["L2E1"].Visible = false;
                    dgvAlineamientoMVista.Columns["L2E2"].Visible = false;
                    dgvAlineamientoMVista.Columns["L2E3"].Visible = false;

                    dgvAlineamientoMVista.Columns["KM_ANTERIOR"].Summary.Clear();
                    dgvAlineamientoMVista.Columns["KM_ANTERIOR"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL", "Total = {0}");

                    dgvAlineamientoMVista.BestFitColumns();
                }
            }
        }

        public void ListarCumplimientos()
        {
            try
            {
                if (txtNroSemana.Text.Length != 0)
                {
                    DataTable dt = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ListarCumplimiento(Convert.ToInt32(dtpAnio.Text), Convert.ToInt32(txtNroSemana.Text));
                    DataTable dtContador = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgruparPorcentaje(4, Convert.ToInt32(dtpAnio.Text), Convert.ToInt32(txtNroSemana.Text));
                    DataTable dtPorcentaje = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_AgruparPorcentaje(5, Convert.ToInt32(dtpAnio.Text), Convert.ToInt32(txtNroSemana.Text));

                    dgvCumplimiento.DataSource = null;
                    dgvCumplimiento.Columns.Clear();

                    if (dt.Rows.Count > 0)
                    {
                        dgvCumplimiento.DataSource = dt;
                        dgvCumplimiento.AutoResizeColumns();

                        dgvCumplimiento.Columns["Nro"].Frozen = true;
                        dgvCumplimiento.Columns["Nro"].ReadOnly = true;
                        dgvCumplimiento.Columns["PLACA"].Frozen = true;
                        dgvCumplimiento.Columns["PLACA"].ReadOnly = true;
                        dgvCumplimiento.Columns["OPERACION"].Frozen = true;
                        dgvCumplimiento.Columns["OPERACION"].ReadOnly = true;
                        dgvCumplimiento.Columns["TIPO_UNIDAD"].Frozen = true;
                        dgvCumplimiento.Columns["TIPO_UNIDAD"].ReadOnly = true;
                        dgvCumplimiento.Columns["MARCA"].Frozen = true;
                        dgvCumplimiento.Columns["MARCA"].ReadOnly = true;
                        dgvCumplimiento.Columns["SEMANA"].Frozen = true;
                        dgvCumplimiento.Columns["SEMANA"].ReadOnly = true;
                        dgvCumplimiento.Columns["FECHA_PROG"].Frozen = true;
                        dgvCumplimiento.Columns["FECHA_PROG"].ReadOnly = true;
                        dgvCumplimiento.Columns["FECHA_INICIO"].Frozen = true;
                        dgvCumplimiento.Columns["FECHA_INICIO"].ReadOnly = true;
                        dgvCumplimiento.Columns["FECHA_FIN"].Frozen = true;
                        dgvCumplimiento.Columns["FECHA_FIN"].ReadOnly = true;
                        dgvCumplimiento.Columns["ESTADO"].Frozen = true;
                        dgvCumplimiento.Columns["ESTADO"].ReadOnly = true;
                        dgvCumplimiento.Columns["FECHA_INGRESO"].Frozen = true;
                        dgvCumplimiento.Columns["FECHA_INGRESO"].ReadOnly = true;

                        int numeroCol = dt.Columns.Count;

                        dgvCumplimiento.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7AF8FA");
                        dgvCumplimiento.EnableHeadersVisualStyles = false;

                        dgvCumplimiento.Columns["Nro"].Visible = false;
                        dgvCumplimiento.Columns["FECHA_INICIO"].Visible = false;
                        dgvCumplimiento.Columns["FECHA_FIN"].Visible = false;

                        //TITULO COLUMNAS
                        try
                        {
                            if (saveRow != 0 && saveRow < dgvCumplimiento.Rows.Count)
                            {
                                dgvCumplimiento.FirstDisplayedScrollingColumnIndex = saveCol;
                                dgvCumplimiento.FirstDisplayedScrollingRowIndex = saveRow;
                            }
                        }
                        catch (Exception ex)
                        {
                            string error = ex.Message;
                            MessageBox.Show(error);
                        }
                    }

                    if (dtContador.Rows.Count > 0)
                    {
                        TotalMttoProg = Convert.ToInt32(dtContador.Rows[0]["TOTAL_UNIDADES"]);
                        TotalMttoEje = Convert.ToInt32(dtContador.Rows[0]["TOTAL_EJECUTADOS"]);
                    }
                    else { TotalMttoProg = 0; TotalMttoEje = 0; }

                    tsTotalUnidades.Text = "TOTAL: " + TotalMttoProg + " Unidades";
                    tsTotalEjecutadas.Text = "EJECUTADAS: " + TotalMttoEje + " Unidades";

                    Double Porcentaje = (Convert.ToDouble(TotalMttoEje) * 100) / Convert.ToDouble(TotalMttoProg);
                    lblPorcentaje.Text = Convert.ToDouble(Porcentaje).ToString("F2") + " %";

                    dgvPorcentaje.DataSource = null;
                    dgvPorcentaje.Columns.Clear();
                    dgvPorcentaje.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#7AF8FA");
                    dgvPorcentaje.EnableHeadersVisualStyles = false;

                    if (dtPorcentaje.Rows.Count > 0)
                    {
                        dgvPorcentaje.DataSource = dtPorcentaje;
                        dgvPorcentaje.AutoResizeColumns();
                    }
                }
                else { MessageBox.Show("Por favor, ingrese el número de semana.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { }
        }


        private void dgvAlineamientoTVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE (%)")
            {
                if (Convert.ToDecimal(e.CellValue) < 60)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) >= 60 && Convert.ToDecimal(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) >= 100 || Convert.ToDecimal(e.CellValue) < 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CONFORME")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "POR VENCER")
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "VENCIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void dgvAlineamientoMVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "PORCENTAJE (%)")
            {
                if (Convert.ToDecimal(e.CellValue) < 60)
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToDecimal(e.CellValue) >= 60 && Convert.ToDecimal(e.CellValue) < 100)
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToDecimal(e.CellValue) >= 100 || Convert.ToDecimal(e.CellValue) < 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "CONFORME")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "POR VENCER")
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "VENCIDO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void btnProgramarCumplimiento_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpcionC == 0)
                {
                    btnProgramarCumplimiento.Text = "Generar Cumplimiento";
                    dtpFCInicio.Value = DateTime.Now;
                    dtpFCFin.Value = DateTime.Now.AddDays(6);
                    groupBox7.Visible = true;
                    dgvAlineamientoTVista.OptionsSelection.MultiSelect = true;
                    dgvAlineamientoTVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    OpcionC = 1;
                }
                else
                {
                    if (dtpFCFin.Value != dtpFCInicio.Value.AddDays(6))
                    {
                        MessageBox.Show("No puede ingresar un rango mayor o menor a 6 días.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnProgramarCumplimiento.Text = "Programar Cumplimiento";
                        groupBox7.Visible = false;
                        dgvAlineamientoTVista.OptionsSelection.MultiSelect = false;
                        dgvAlineamientoTVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        OpcionC = 0;
                        return;
                    }
                    else
                    {
                        int[] filas = dgvAlineamientoTVista.GetSelectedRows();

                        if (filas.Length != 0)
                        {
                            DataTable dtRespuesta = new DataTable();
                            string respta;
                            int Correcto = 0;

                            if (MessageBox.Show("¿Desea generar un cumplimiento para estos registros?", "GENERAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                for (int i = 0; i < filas.Length; i++)
                                {
                                    string Placa = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(filas[i], "PLACA"));
                                    string Operacion = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(filas[i], "OPERACION"));
                                    string TipoUnidad = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(filas[i], "SUBTIPO_UNIDAD"));
                                    string Marca = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(filas[i], "MARCA"));
                                    DateTime FechaProgramada = Convert.ToDateTime(dgvAlineamientoTVista.GetRowCellValue(filas[i], "FECHA_PROYECTADA"));

                                    dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ControlNeumaticos_GenerarCumplimiento(Placa, Operacion, TipoUnidad, Marca, "ALINEAMIENTO",
                                                  FechaProgramada, dtpFCInicio.Value, dtpFCFin.Value);
                                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0") { Correcto = Correcto + 1; }
                                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }

                                if (Correcto == filas.Length) { MessageBox.Show("0 = El cumplimiento semanal ha sido generado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                btnProgramarCumplimiento.Text = "Programar Cumplimiento";
                                groupBox7.Visible = false;
                                dgvAlineamientoTVista.OptionsSelection.MultiSelect = false;
                                dgvAlineamientoTVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                                OpcionC = 0;

                                ListarCumplimientos();
                            }
                        }
                        else
                        {
                            btnProgramarCumplimiento.Text = "Programar Cumplimiento";
                            groupBox7.Visible = false;
                            dgvAlineamientoTVista.OptionsSelection.MultiSelect = false;
                            dgvAlineamientoTVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                            OpcionC = 0;
                        }
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el reporte de cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            dtgAlineamientoT.DataSource = null;
            dgvAlineamientoTVista.Columns.Clear();
            
            if (OpcionT == 1)
            {
                OpcionT = 2;
                ListarHistorialT();
                btnHistorial.Text = "Registro";
            }
            else
            {
                OpcionT = 1;
                ListarRegistrosT();
                btnHistorial.Text = "Historial";
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (OpcionT == 1) { ListarRegistrosT(); }
                else { ListarHistorialT(); }
            }
        }

        private void dtpFechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (OpcionT == 1) { ListarRegistrosT(); }
                else { ListarHistorialT(); }
            }
        }

        private void dtpFechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (OpcionT == 1) { ListarRegistrosT(); }
                else { ListarHistorialT(); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (OpcionT == 1) { ListarRegistrosT(); }
            else { ListarHistorialT(); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgAlineamientoT.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                if (OpcionT == 1)
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "REPORTE DE ALINEAMIENTO DE TRACTOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgAlineamientoT.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "HISTORIAL DE ALINEAMIENTO DE TRACTOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgAlineamientoT.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
        }

        private void dtgAlineamientoT_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string Vacio = dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "idRegistro").ToString();

                if (Vacio != "")
                {
                    if (OpcionT == 1)
                    {
                        OpcionEditar = 1;

                        idRegistro = Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "idRegistro"));
                        txtPlacaN.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "PLACA"));
                        txtMarca.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "MARCA"));
                        txtModelo.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "MODELO"));
                        txtProgramacion.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "OPERACION"));
                        txtFrecuencia.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "FREC/KM"));

                        dtpUltFecha.Value = Convert.ToDateTime(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "FECHA_ANTERIOR"));
                        txtUltKM.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "KM_ANTERIOR"));

                        dtpNuevaFecha.Value = Convert.ToDateTime(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "FECHA_ACTUAL"));
                        txtNuevoKM.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "KM_ACTUAL"));

                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L1E1")) == 1) { cbL1E1.Checked = true; }
                        else { cbL1E1.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L1E2")) == 1) { cbL1E2.Checked = true; }
                        else { cbL1E2.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L1E3")) == 1) { cbL1E3.Checked = true; }
                        else { cbL1E3.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L2E1")) == 1) { cbL2E1.Checked = true; }
                        else { cbL2E1.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L2E2")) == 1) { cbL2E2.Checked = true; }
                        else { cbL2E2.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L2E3")) == 1) { cbL2E3.Checked = true; }
                        else { cbL2E3.Checked = false; }

                        groupBox6.Visible = true;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { btnActualizar.Enabled = true; }
                        pActualizar.Visible = true;
                        pActualizar.BringToFront();
                    }
                    else
                    {
                        txtPlacaN.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "PLACA"));
                        txtMarca.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "MARCA"));
                        txtModelo.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "MODELO"));
                        txtProgramacion.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "OPERACION"));

                        dtpUltFecha.Value = Convert.ToDateTime(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "FECHA_ANTERIOR"));
                        txtUltKM.Text = Convert.ToString(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "KM_ANTERIOR"));

                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L1E1")) == 1) { cbL1E1.Checked = true; }
                        else { cbL1E1.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L1E2")) == 1) { cbL1E2.Checked = true; }
                        else { cbL1E2.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L1E3")) == 1) { cbL1E3.Checked = true; }
                        else { cbL1E3.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L2E1")) == 1) { cbL2E1.Checked = true; }
                        else { cbL2E1.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L2E2")) == 1) { cbL2E2.Checked = true; }
                        else { cbL2E2.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoTVista.GetRowCellValue(dgvAlineamientoTVista.FocusedRowHandle, "L2E3")) == 1) { cbL2E3.Checked = true; }
                        else { cbL2E3.Checked = false; }

                        groupBox6.Visible = false;
                        btnActualizar.Enabled = false;
                        pActualizar.Visible = true;
                        pActualizar.BringToFront();
                    }
                }
            }
            catch { }
        }

        private void btnProgramarCumplimiento2_Click(object sender, EventArgs e)
        {
            try
            {
                if (OpcionM2 == 0)
                {
                    btnProgramarCumplimiento2.Text = "Generar Cumplimiento";
                    dtpFCInicio2.Value = DateTime.Now;
                    dtpFCFin2.Value = DateTime.Now.AddDays(6);
                    groupBox11.Visible = true;
                    dgvAlineamientoMVista.OptionsSelection.MultiSelect = true;
                    dgvAlineamientoMVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    OpcionM2 = 1;
                }
                else
                {
                    if (dtpFCFin2.Value != dtpFCInicio2.Value.AddDays(6))
                    {
                        MessageBox.Show("No puede ingresar un rango mayor o menor a 6 días.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        btnProgramarCumplimiento2.Text = "Programar Cumplimiento";
                        groupBox11.Visible = false;
                        dgvAlineamientoMVista.OptionsSelection.MultiSelect = false;
                        dgvAlineamientoMVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        OpcionM2 = 0;
                        return;
                    }
                    else
                    {
                        int[] filas = dgvAlineamientoMVista.GetSelectedRows();

                        if (filas.Length != 0)
                        {
                            DataTable dtRespuesta = new DataTable();
                            string respta;
                            int Correcto = 0;

                            if (MessageBox.Show("¿Desea generar un cumplimiento para estos registros?", "GENERAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                for (int i = 0; i < filas.Length; i++)
                                {
                                    string Placa = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(filas[i], "MAQUINA"));
                                    string TipoUnidad = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(filas[i], "TIPO_MAQUINA"));
                                    string Marca = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(filas[i], "MARCA"));
                                    DateTime FechaProgramada = Convert.ToDateTime(dgvAlineamientoMVista.GetRowCellValue(filas[i], "FECHA_PROYECTADA"));

                                    dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ControlNeumaticos_GenerarCumplimiento(Placa, "SIN OPERACION", TipoUnidad, Marca, "ALINEAMIENTO",
                                                  FechaProgramada, dtpFCInicio2.Value, dtpFCFin2.Value);
                                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0") { Correcto = Correcto + 1; }
                                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }

                                if (Correcto == filas.Length) { MessageBox.Show("0 = El cumplimiento semanal ha sido generado exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                                btnProgramarCumplimiento2.Text = "Programar Cumplimiento";
                                groupBox11.Visible = false;
                                dgvAlineamientoMVista.OptionsSelection.MultiSelect = false;
                                dgvAlineamientoMVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                                OpcionM2 = 0;

                                ListarCumplimientos();
                            }
                        }
                        else
                        {
                            btnProgramarCumplimiento2.Text = "Programar Cumplimiento";
                            groupBox11.Visible = false;
                            dgvAlineamientoMVista.OptionsSelection.MultiSelect = false;
                            dgvAlineamientoMVista.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                            OpcionM2 = 0;
                        }
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el reporte de cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
            
        }

        private void btnHistorial2_Click(object sender, EventArgs e)
        {
            dtgAlineamientoM.DataSource = null;
            dgvAlineamientoMVista.Columns.Clear();

            if (OpcionM == 1)
            {
                OpcionM = 2;
                ListarHistorialM();
                btnHistorial2.Text = "Registro";
            }
            else
            {
                OpcionM = 1;
                ListarRegistrosM();
                btnHistorial2.Text = "Historial";
            }
        }

        private void txtMaquina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (OpcionM == 1) { ListarRegistrosM(); }
                else { ListarHistorialM(); }
            }
        }

        private void cbxTipoMaquina_DropDownClosed(object sender, EventArgs e)
        {
            if (OpcionM == 1) { ListarRegistrosM(); }
            else { ListarHistorialM(); }
        }

        private void dtpInicioMaquina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (OpcionM == 1) { ListarRegistrosM(); }
                else { ListarHistorialM(); }
            }
        }

        private void dtpFinMaquina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (OpcionM == 1) { ListarRegistrosM(); }
                else { ListarHistorialM(); }
            }
        }

        private void btnBuscarMaquina_Click(object sender, EventArgs e)
        {
            if (OpcionM == 1) { ListarRegistrosM(); }
            else { ListarHistorialM(); }
        }

        private void btnExcelMaquina_Click(object sender, EventArgs e)
        {
            if (dtgAlineamientoM.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                if (OpcionM == 1)
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "REPORTE DE ALINEAMIENTO DE MAQUINAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgAlineamientoM.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "HISTORIAL DE ALINEAMIENTO DE MAQUINAS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgAlineamientoM.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
        }

        private void dtgAlineamientoM_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string Vacio = dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "idRegistro").ToString();

                if (Vacio != "")
                {
                    if (OpcionM == 1)
                    {
                        OpcionEditar = 1;

                        idRegistro = Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "idRegistro"));
                        txtPlacaN.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "MAQUINA"));
                        txtMarca.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "MARCA"));
                        txtModelo.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "MODELO"));
                        txtProgramacion.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "OPERACION"));
                        txtFrecuencia.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "FREC/KM"));

                        dtpUltFecha.Value = Convert.ToDateTime(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "FECHA_ANTERIOR"));
                        txtUltKM.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "KM_ANTERIOR"));

                        dtpNuevaFecha.Value = Convert.ToDateTime(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "FECHA_ACTUAL"));
                        txtNuevoKM.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "KM_ACTUAL"));

                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L1E1")) == 1) { cbL1E1.Checked = true; }
                        else { cbL1E1.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L1E2")) == 1) { cbL1E2.Checked = true; }
                        else { cbL1E2.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L1E3")) == 1) { cbL1E3.Checked = true; }
                        else { cbL1E3.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L2E1")) == 1) { cbL2E1.Checked = true; }
                        else { cbL2E1.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L2E2")) == 1) { cbL2E2.Checked = true; }
                        else { cbL2E2.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L2E3")) == 1) { cbL2E3.Checked = true; }
                        else { cbL2E3.Checked = false; }

                        groupBox6.Visible = true;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { btnActualizar.Enabled = true; }
                        pActualizar.Visible = true;
                        pActualizar.BringToFront();
                    }
                    else
                    {
                        txtPlacaN.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "PLACA"));
                        txtMarca.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "MARCA"));
                        txtModelo.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "MODELO"));
                        txtProgramacion.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "OPERACION"));

                        dtpUltFecha.Value = Convert.ToDateTime(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "FECHA_ANTERIOR"));
                        txtUltKM.Text = Convert.ToString(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "KM_ANTERIOR"));

                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L1E1")) == 1) { cbL1E1.Checked = true; }
                        else { cbL1E1.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L1E2")) == 1) { cbL1E2.Checked = true; }
                        else { cbL1E2.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L1E3")) == 1) { cbL1E3.Checked = true; }
                        else { cbL1E3.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L2E1")) == 1) { cbL2E1.Checked = true; }
                        else { cbL2E1.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L2E2")) == 1) { cbL2E2.Checked = true; }
                        else { cbL2E2.Checked = false; }
                        if (Convert.ToInt32(dgvAlineamientoMVista.GetRowCellValue(dgvAlineamientoMVista.FocusedRowHandle, "L2E3")) == 1) { cbL2E3.Checked = true; }
                        else { cbL2E3.Checked = false; }

                        groupBox6.Visible = false;
                        btnActualizar.Enabled = false;
                        pActualizar.Visible = true;
                        pActualizar.BringToFront();
                    }
                }
            }
            catch { }
        }

        private void pActualizar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pActualizar.Left = pActualizar.Left + (e.X - xClick);
                pActualizar.Top = pActualizar.Top + (e.Y - yClick);
            }
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pActualizar.Left = pActualizar.Left + (e.X - xClick);
                pActualizar.Top = pActualizar.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pActualizar.Visible = false;
            pActualizar.SendToBack();
            pActualizar.Location = new System.Drawing.Point(860, 304);

            txtPlacaN.Clear();
            txtMarca.Clear();
            txtModelo.Clear();
            txtProgramacion.Clear();
            txtFrecuencia.Clear();

            cbL1E1.Checked = false; L1E1 = 0;
            cbL2E1.Checked = false; L2E1 = 0;
            cbL1E2.Checked = false; L1E2 = 0;
            cbL2E2.Checked = false; L2E2 = 0;
            cbL1E3.Checked = false; L1E3 = 0;
            cbL2E3.Checked = false; L2E3 = 0;

            dtpUltFecha.Value = DateTime.Now;
            txtUltKM.Clear();
            dtpNuevaFecha.Value = DateTime.Now;
            txtNuevoKM.Clear();
            OpcionEditar = 0;
            idRegistro = 0;
        }

        private void cbL1E1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbL1E1.Checked == true) { L1E1 = 1; }

            if (cbL1E1.Checked == false) { L1E1 = 0; }
        }

        private void cbL2E1_CheckedChanged(object sender, EventArgs e)
        {
            if (cbL2E1.Checked == true) { L2E1 = 1; }

            if (cbL2E1.Checked == false) { L2E1 = 0; }
        }

        private void cbL1E2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbL1E2.Checked == true) { L1E2 = 1; }

            if (cbL1E2.Checked == false) { L1E2 = 0; }
        }

        private void cbL2E2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbL2E2.Checked == true) { L2E2 = 1; }

            if (cbL2E2.Checked == false) { L2E2 = 0; }
        }

        private void cbL1E3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbL1E3.Checked == true) { L1E3 = 1; }

            if (cbL1E3.Checked == false) { L1E3 = 0; }
        }

        private void cbL2E3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbL2E3.Checked == true) { L2E3 = 1; }

            if (cbL2E3.Checked == false) { L2E3 = 0; }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (txtNuevoKM.Text.Length == 0)
            {
                MessageBox.Show("El KM de la unidad no puede estar vacío.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNuevoKM.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ActualizarRegistros(idRegistro, dtpNuevaFecha.Value,
                              Convert.ToDecimal(txtNuevoKM.Text), L1E1, L1E2, L1E3, L2E1, L2E2, L2E3, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);

                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (OpcionEditar == 1) { ListarRegistrosT(); }
                    else { ListarRegistrosM(); }
                    btnCerrar_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtpAnio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCumplimientos(); }
        }

        private void txtNroSemana_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarCumplimientos(); }
        }

        private void btnBuscar2_Click(object sender, EventArgs e) { ListarCumplimientos(); }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvCumplimiento.DataSource == null) { MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    DataTable dtExcel = new DataTable();
                    gcExcelCump.DataSource = null;
                    gvExcelCump.Columns.Clear();
                    dtExcel = Utilitario.Instancia.GetContentAsDataTable(dgvCumplimiento, true);
                    gcExcelCump.DataSource = dtExcel;
                    gvExcelCump.BestFitColumns();

                    DataTable dtExcelTotal = new DataTable();
                    gcExcelCump2.DataSource = null;
                    gvExcelCump2.Columns.Clear();
                    dtExcelTotal = Utilitario.Instancia.GetContentAsDataTable(dgvPorcentaje, true);
                    gcExcelCump2.DataSource = dtExcelTotal;
                    gvExcelCump2.BestFitColumns();

                    gcExcelCump.ForceInitialize();
                    gcExcelCump2.ForceInitialize();

                    compositeLink1.CreatePageForEachLink();

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                    XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                    options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                    string nombre = System.IO.Path.Combine(desktop, "Cumplimiento Semanal de Alineamientos - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    compositeLink1.ExportToXlsx(nombre, options);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvCumplimiento_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvCumplimiento.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("ESTADO"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("REPROGRAMADO"))
                            {
                                e.CellStyle.BackColor = Color.Pink;
                                e.CellStyle.ForeColor = Color.Red;
                            }
                            else if (Convert.ToString(e.Value).Contains("EJECUTADO"))
                            {
                                e.CellStyle.BackColor = Color.PaleGreen;
                                e.CellStyle.ForeColor = Color.Green;
                            }
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("LUN"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("MAR"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value).Contains("ALI"))
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("MIÉ"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("JUE"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("VIE"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("SÁB"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }

                if (this.dgvCumplimiento.Columns[e.ColumnIndex].Name.Contains("DOM"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                            e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvCumplimiento_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (dgvCumplimiento.RowCount > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { dgvCumplimiento.ContextMenuStrip = contextMenuStrip7; }
                }
                else { dgvCumplimiento.ContextMenuStrip = null; }
            }
            catch (Exception ex) { }
        }

        private void dgvCumplimiento_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvCumplimiento.RowCount > 0)
                {
                    if (e.ColumnIndex < 9 || e.ColumnIndex > 15)
                    {
                        dgvCumplimiento.ContextMenuStrip = contextMenuStrip7;
                        tsActualizarEstado.Enabled = false;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsEliminarCump.Enabled = true; }
                    }
                    else
                    {
                        dgvCumplimiento.ContextMenuStrip = contextMenuStrip7;
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsActualizarEstado.Enabled = true; }
                        if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsEliminarCump.Enabled = true; }
                    }
                }
                else { dgvCumplimiento.ContextMenuStrip = null; }
            }
            catch (Exception ex) { }
        }

        private void dgvPorcentaje_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try { dgvPorcentaje.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable; }
            catch (Exception) { throw; }
        }

        private void tsRegistrarFechas_Click(object sender, EventArgs e)
        {
            try
            {
                lblPlaca.Text = dgvCumplimiento.CurrentRow.Cells["PLACA"].Value.ToString();
                dtpFechaCumplimiento.Value = Convert.ToDateTime(dgvCumplimiento.CurrentRow.Cells["FECHA_PROG"].Value.ToString());
                cbxEstado.Text = dgvCumplimiento.CurrentRow.Cells["ESTADO"].Value.ToString();
                txtObservacion.Text = dgvCumplimiento.CurrentRow.Cells["OBSERVACION"].Value.ToString();
                
                pRegistrarCump.Visible = true;
                pRegistrarCump.BringToFront();
                pRegistrarCump.Location = new Point(725, 258);
                dtpFechaCumplimiento.Focus();
            }
            catch { }
        }

        private void pRegistrarCump_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pRegistrarCump.Left = pRegistrarCump.Left + (e.X - xClick2);
                pRegistrarCump.Top = pRegistrarCump.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pRegistrarCump.Visible = false;
            pRegistrarCump.SendToBack();

            dtpFechaCumplimiento.Value = DateTime.Now;
            cbxEstado.Text = "PROGRAMADO";
            txtObservacion.Clear();
        }

        private void dtpFechaProgramada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxEstado.Focus(); }
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e) { txtObservacion.Focus(); }

        private void btnGuardarCump_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                int Nro = Convert.ToInt32(dgvCumplimiento.CurrentRow.Cells["Nro"].Value.ToString());

                dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ProgramarCumplimiento(1, Nro, dtpFechaCumplimiento.Value, cbxEstado.Text, txtObservacion.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    btnCerrar2_Click(sender, e);
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCumplimientos();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            catch { MessageBox.Show("Se produjo un error al actualizar el estado del cumplimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsFechaIngreso_Click(object sender, EventArgs e)
        {
            try
            {
                lblPlaca2.Text = dgvCumplimiento.CurrentRow.Cells["PLACA"].Value.ToString();
                dtpFechaIngreso.Value = Convert.ToDateTime(dgvCumplimiento.CurrentRow.Cells["FECHA_INGRESO"].Value.ToString() == "" ? Convert.ToString(DateTime.Now) : dgvCumplimiento.CurrentRow.Cells["FECHA_INGRESO"].Value.ToString());

                pFechaIngreso.Visible = true;
                pFechaIngreso.BringToFront();
                pFechaIngreso.Location = new Point(725, 258);
                dtpFechaIngreso.Focus();
            }
            catch { }
        }

        private void pFechaIngreso_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick3 = e.X; yClick3 = e.Y; }
            else
            {
                pFechaIngreso.Left = pFechaIngreso.Left + (e.X - xClick3);
                pFechaIngreso.Top = pFechaIngreso.Top + (e.Y - yClick3);
            }
        }

        private void btnCerrar3_Click(object sender, EventArgs e)
        {
            pFechaIngreso.Visible = false;
            pFechaIngreso.SendToBack();

            dtpFechaIngreso.Value = DateTime.Now;
        }

        private void dtpFechaIngreso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardarIngreso_Click(sender, e); }
        }

        private void btnGuardarIngreso_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            int Nro = Convert.ToInt32(dgvCumplimiento.CurrentRow.Cells["Nro"].Value.ToString());

            dtRespuesta = clsNeumaticoBL.Instancia.ReportesApp_Neumatico_ControlNeumaticos_ProgramarCumplimiento(2, Nro, dtpFechaIngreso.Value, " ", " ", Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                btnCerrar3_Click(sender, e);
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarCumplimientos();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsEliminarCump_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("¿Desea eliminar este registro de cumplimiento?", "ELIMINAR CUMPLIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int Nro = Convert.ToInt32(dgvCumplimiento.CurrentRow.Cells["Nro"].Value.ToString());

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_EliminarCumplimiento(4, Nro);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0") { ListarCumplimientos(); }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch { }
        }
    }
}
