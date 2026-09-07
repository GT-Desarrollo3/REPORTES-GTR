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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmDisponibilidad : Form
    {
        string TipoVehiculo;
        int saveRow = 0, saveCol = 0;
        int saveRow2 = 0, saveCol2 = 0;
        
        public frmDisponibilidad()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
            cbxCarreta.SelectedIndexChanged -= cbxCarreta_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperaciones(); }

        private void cbxCarreta_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSubTipo(); }

        private void frmDisponibilidad_Load(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            dtpPeriodo.Value = new DateTime(date.Year, date.Month, 1);
            CargarComboOperaciones();
            CargarComboSubTipo();
            cbxOperaciones.SelectedValue = 5;
            rbTractos.Checked = true;
            rbTractos_Click(sender, e);

            dgvDHoras.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDHoras.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDHoras.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvDPorc.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDPorc.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvDPorc.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvPromedioDia.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPromedioDia.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPromedioDia.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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

        public void CargarTablaHoras()
        {
            DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosH(dtpPeriodo.Text, txtPlaca.Text, TipoVehiculo);

            dgvDHoras.DataSource = null;
            dgvDHoras.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                dgvDHoras.DataSource = dt;
                dgvDHoras.AutoResizeColumns();
                dgvDHoras.Columns["PROGRAMACION"].Frozen = true;
                dgvDHoras.Columns["PROGRAMACION"].ReadOnly = true;
                dgvDHoras.Columns["PLACA"].Frozen = true;
                dgvDHoras.Columns["PLACA"].ReadOnly = true;
                dgvDHoras.Columns["TIPO"].Frozen = true;
                dgvDHoras.Columns["TIPO"].ReadOnly = true;
                dgvDHoras.Columns["SUBTIPO"].Frozen = true;
                dgvDHoras.Columns["SUBTIPO"].ReadOnly = true;
                dgvDHoras.Columns["MARCA"].Frozen = true;
                dgvDHoras.Columns["MARCA"].ReadOnly = true;
                dgvDHoras.Columns["MODELO"].Frozen = true;
                dgvDHoras.Columns["MODELO"].ReadOnly = true;

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvDHoras.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F5B289");
                dgvDHoras.EnableHeadersVisualStyles = false;

                dgvDHoras.Columns["IdUnidad"].Visible = false;

                try
                {
                    if (saveRow != 0 && saveRow < dgvDHoras.Rows.Count)
                    {
                        dgvDHoras.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvDHoras.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
        }

        public void CargarTablaPorcentaje()
        {
            DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosP(dtpPeriodo.Text, txtPlaca.Text, TipoVehiculo);

            dgvDPorc.DataSource = null;
            dgvDPorc.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                dgvDPorc.DataSource = dt;
                dgvDPorc.AutoResizeColumns();
                dgvDPorc.Columns["PROGRAMACION"].Frozen = true;
                dgvDPorc.Columns["PROGRAMACION"].ReadOnly = true;
                dgvDPorc.Columns["PLACA"].Frozen = true;
                dgvDPorc.Columns["PLACA"].ReadOnly = true;
                dgvDPorc.Columns["TIPO"].Frozen = true;
                dgvDPorc.Columns["TIPO"].ReadOnly = true;
                dgvDPorc.Columns["SUBTIPO"].Frozen = true;
                dgvDPorc.Columns["SUBTIPO"].ReadOnly = true;
                dgvDPorc.Columns["MARCA"].Frozen = true;
                dgvDPorc.Columns["MARCA"].ReadOnly = true;
                dgvDPorc.Columns["MODELO"].Frozen = true;
                dgvDPorc.Columns["MODELO"].ReadOnly = true;

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvDPorc.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#899DF5");
                dgvDPorc.EnableHeadersVisualStyles = false;

                dgvDPorc.Columns["IdUnidad"].Visible = false;

                try
                {
                    if (saveRow2 != 0 && saveRow2 < dgvDPorc.Rows.Count)
                    {
                        dgvDPorc.FirstDisplayedScrollingColumnIndex = saveCol2;
                        dgvDPorc.FirstDisplayedScrollingRowIndex = saveRow2;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
        }

        public void CargarTablaPromedioMes()
        {
            DataTable dt2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Disponibilidad_PromedioMes(dtpPeriodo.Text);

            dgvPromedioDia.DataSource = null;
            dgvPromedioDia.Columns.Clear();

            if (dt2.Rows.Count > 0)
            {
                dgvPromedioDia.DataSource = dt2;
                dgvPromedioDia.AutoResizeColumns();
                dgvPromedioDia.EnableHeadersVisualStyles = false;
                dgvPromedioDia.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F9FF89");

                dgvPromedioDia.Columns["PROGRAMACION"].Frozen = true;
                dgvPromedioDia.Columns["PROGRAMACION"].ReadOnly = true;
            }
        }


        private void dtpPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                CargarTablaHoras();
                CargarTablaPorcentaje();
                CargarTablaPromedioMes();
            }
        }

        private void rbTractos_Click(object sender, EventArgs e)
        {
            TipoVehiculo = "TRACTO";
            rbTractos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            rbCarretas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            CargarTablaHoras();
            CargarTablaPorcentaje();
            CargarTablaPromedioMes();
        }

        private void rbCarretas_Click(object sender, EventArgs e)
        {
            TipoVehiculo = "SEMIRREMOLQUE";
            rbTractos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbCarretas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

            CargarTablaHoras();
            CargarTablaPorcentaje();
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                if (dgvDHoras.DataSource != null)
                { ((DataTable)dgvDHoras.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PLACA", txtPlaca.Text); }

                if (dgvDPorc.DataSource != null)
                {
                    ((DataTable)dgvDPorc.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PLACA", txtPlaca.Text);
                    dgvDPorc.Columns["IdUnidad"].Visible = false;
                }
            }
        }

        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxOperaciones.Text == "TODO")
            {
                if (dgvDHoras.DataSource != null)
                { ((DataTable)dgvDHoras.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "TIPO", TipoVehiculo); }

                if (dgvDPorc.DataSource != null)
                {
                    ((DataTable)dgvDPorc.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "TIPO", TipoVehiculo);
                    dgvDPorc.Columns["IdUnidad"].Visible = false;
                }
            }
            else
            {
                if (dgvDHoras.DataSource != null)
                { ((DataTable)dgvDHoras.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PROGRAMACION", cbxOperaciones.Text); }

                if (dgvDPorc.DataSource != null)
                {
                    ((DataTable)dgvDPorc.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "PROGRAMACION", cbxOperaciones.Text);
                    dgvDPorc.Columns["IdUnidad"].Visible = false;
                }
            }
        }

        private void cbxCarreta_DropDownClosed(object sender, EventArgs e)
        {
            if (TipoVehiculo == "SEMIRREMOLQUE")
            {
                if (cbxCarreta.Text == "TODOS")
                {
                    if (dgvDHoras.DataSource != null)
                    { ((DataTable)dgvDHoras.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "TIPO", TipoVehiculo); }

                    if (dgvDPorc.DataSource != null)
                    {
                        ((DataTable)dgvDPorc.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "TIPO", TipoVehiculo);
                        dgvDPorc.Columns["IdUnidad"].Visible = false;
                    }
                }
                else
                {
                    if (dgvDHoras.DataSource != null)
                    { ((DataTable)dgvDHoras.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "SUBTIPO", cbxCarreta.Text); }

                    if (dgvDPorc.DataSource != null)
                    {
                        ((DataTable)dgvDPorc.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "SUBTIPO", cbxCarreta.Text);
                        dgvDPorc.Columns["IdUnidad"].Visible = false;
                    }
                }
            }
        }

        private void dgvDHoras_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvDHoras.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvDHoras.Columns[e.ColumnIndex].Name.Contains("PLACA"))
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.SeaShell;
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                if (this.dgvDHoras.Columns[e.ColumnIndex].Name.Contains("PROGRAMACION"))
                {
                    if (e.Value != null) { e.CellStyle.BackColor = Color.SeaShell; }
                }

                if (this.dgvDHoras.Columns[e.ColumnIndex].Name.Contains("TIPO"))
                {
                    if (e.Value != null) { e.CellStyle.BackColor = Color.SeaShell; }
                }

                if (this.dgvDHoras.Columns[e.ColumnIndex].Name.Contains("SUBTIPO"))
                {
                    if (e.Value != null) { e.CellStyle.BackColor = Color.SeaShell; }
                }

                if (this.dgvDHoras.Columns[e.ColumnIndex].Name.Contains("MARCA"))
                {
                    if (e.Value != null) { e.CellStyle.BackColor = Color.SeaShell; }
                }

                if (this.dgvDHoras.Columns[e.ColumnIndex].Name.Contains("MODELO"))
                {
                    if (e.Value != null) { e.CellStyle.BackColor = Color.SeaShell; }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvDPorc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvDPorc.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvDPorc.Columns[e.ColumnIndex].Name.Contains("PLACA"))
                {
                    if (e.Value != null)
                    {
                        e.CellStyle.BackColor = Color.LightCyan;
                        e.CellStyle.ForeColor = Color.Blue;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }

                if (this.dgvDPorc.Columns[e.ColumnIndex].Name.Contains("PROGRAMACION"))
                {
                    if (e.Value != null) { e.CellStyle.BackColor = Color.LightCyan; }
                }

                if (this.dgvDPorc.Columns[e.ColumnIndex].Name.Contains("TIPO"))
                {
                    if (e.Value != null) { e.CellStyle.BackColor = Color.LightCyan; }
                }

                if (this.dgvDPorc.Columns[e.ColumnIndex].Name.Contains("SUBTIPO"))
                {
                    if (e.Value != null) { e.CellStyle.BackColor = Color.LightCyan; }
                }

                if (this.dgvDPorc.Columns[e.ColumnIndex].Name.Contains("MARCA"))
                {
                    if (e.Value != null) { e.CellStyle.BackColor = Color.LightCyan; }
                }

                if (this.dgvDPorc.Columns[e.ColumnIndex].Name.Contains("MODELO"))
                {
                    if (e.Value != null) { e.CellStyle.BackColor = Color.LightCyan; }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvPromedioDia_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvPromedioDia.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                for (int i = 0; i < dgvPromedioDia.RowCount; i++)
                {
                    if (Convert.ToString(dgvPromedioDia.Rows[e.RowIndex].Cells[i].Value) == "TOTAL: ")
                    {
                        e.CellStyle.BackColor = Color.LemonChiffon;
                        e.CellStyle.ForeColor = Color.DarkOrange;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarTablaHoras();
            CargarTablaPorcentaje();
            CargarTablaPromedioMes();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Disponibilidad_ActualizarHorasDisp();
            DataTable dt2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Disponibilidad_ActualizarPorcDisp();

            CargarTablaHoras();
            CargarTablaPorcentaje();
            CargarTablaPromedioMes();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                System.Data.DataTable dt1 = new System.Data.DataTable();
                dt1 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosH(dtpPeriodo.Text, txtPlaca.Text, TipoVehiculo);
                gcExcelHoras.DataSource = null;
                gvExcelHoras.Columns.Clear();
                gcExcelHoras.DataSource = dt1;
                gvExcelHoras.BestFitColumns();

                System.Data.DataTable dt2 = new System.Data.DataTable();
                dt2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Disponibilidad_ListarDisponibilidadTractosP(dtpPeriodo.Text, txtPlaca.Text, TipoVehiculo);
                gcExcelPorc.DataSource = null;
                gvExcelPorc.Columns.Clear();
                gcExcelPorc.DataSource = dt2;
                gvExcelPorc.BestFitColumns();

                gcExcelHoras.ForceInitialize();
                gcExcelPorc.ForceInitialize();

                compositeLink1.CreatePageForEachLink();

                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

                XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.ExportMode = XlsxExportMode.SingleFilePageByPage;
                string nombre = System.IO.Path.Combine(desktop, "Disponibilidad Mecánica de Unidades - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                compositeLink1.ExportToXlsx(nombre, options);
                Process.Start(nombre);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
    }
}
