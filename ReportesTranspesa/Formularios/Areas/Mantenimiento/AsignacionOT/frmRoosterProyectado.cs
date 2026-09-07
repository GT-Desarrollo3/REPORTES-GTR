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
using System.IO;
using Microsoft.Office;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.AsignacionOT
{
    public partial class frmRoosterProyectado : Form
    {
        DataTable dtPermisos = new DataTable();
        int saveRow = 0, saveCol = 0;
        string FechaFijaSelec, Nombre;
        int xClick = 0, yClick = 0;
        int xClick2 = 0, yClick2 = 0;
        String CarpetaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),"Downloads");
        DataTable dtListaAsist;
        int IDPersona;
        string xmlAsistencia;
        String xmlAsist;

        public frmRoosterProyectado()
        {
            InitializeComponent();
            cbxEstado.SelectedIndexChanged -= cbxEstado_SelectedIndexChanged;
        }

        private void cbxEstado_SelectedIndexChanged(object sender, EventArgs e) { CargarComboEstado(); }

        private void frmRoosterProyectado_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmRegistroOT");
            if (dtPermisos != null)
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]) == true) { btnMapear.Enabled = true; }
                    else { btnMapear.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { tsIngresarAsistencia.Enabled = true; }
                    else { tsIngresarAsistencia.Enabled = false; }

                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true)
                    {
                        btnQuitar.Enabled = true;
                        tsEliminarAsistencia.Enabled = true;
                    }
                    else
                    {
                        btnQuitar.Enabled = false;
                        tsEliminarAsistencia.Enabled = false;
                    }
                }
            }

            DateTime date = DateTime.Now;
            dtpPeriodo.Value = new DateTime(date.Year, date.Month, 1);
            CargarTabla();
            CargarComboEstado();
        }


        private void CargarComboEstado()
        {
            DataTable dtEstado = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_ListarMecanicos(3, dtpPeriodo.Text);
            cbxEstado.DataSource = dtEstado;
            cbxEstado.DisplayMember = "Estado";
            cbxEstado.ValueMember = "Codigo";
        }

        public void LeerTrabajadores()
        {
            DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_ListarMecanicos(1, dtpPeriodo.Text);
            dtgvData.DataSource = null;

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dgvDataView.Columns["Persona"].Visible = false;
                dgvDataView.UpdateSummary();
                dgvDataView.BestFitColumns();
            }
        }

        public void CargarMapeados()
        {
            DataTable dt2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_ListarMecanicos(2, dtpPeriodo.Text);

            if (dt2.Rows.Count > 0)
            {
                dtgvDataMapeados.DataSource = dt2;
                dtgvDataMapeadosView.Columns["Persona"].Visible = false;
                dtgvDataMapeadosView.UpdateSummary();
                dtgvDataMapeadosView.BestFitColumns();
            }
            else
            {
                MessageBox.Show("Aún no hay personal mapeado.", "Aviso");
                dtgvDataMapeados.DataSource = null;
            }
        }

        public void CargarTabla()
        {
            DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_ListarTablaMecanicos(dtpPeriodo.Text, txtNombre.Text, txtPuesto.Text);

            dgvPersonal.DataSource = null;
            dgvPersonal.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                dgvPersonal.DataSource = dt;
                dgvPersonal.AutoResizeColumns();
                dgvPersonal.Columns["NOMBRE"].Frozen = true;
                dgvPersonal.Columns["NOMBRE"].ReadOnly = true;
                dgvPersonal.Columns["PUESTO"].Frozen = true;
                dgvPersonal.Columns["PUESTO"].ReadOnly = true;
                dgvPersonal.Columns["COMPANIA"].Frozen = true;
                dgvPersonal.Columns["COMPANIA"].ReadOnly = true;

                int numeroCol = 0;
                numeroCol = dt.Columns.Count;
                dgvPersonal.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#9786F7");
                dgvPersonal.EnableHeadersVisualStyles = false;

                dgvPersonal.Columns["IDPersona"].Visible = false;

                //TITULO COLUMNAS
                try
                {
                    if (saveRow != 0 && saveRow < dgvPersonal.Rows.Count)
                    {
                        dgvPersonal.FirstDisplayedScrollingColumnIndex = saveCol;
                        dgvPersonal.FirstDisplayedScrollingRowIndex = saveRow;
                    }
                }
                catch (Exception ex)
                {
                    string error = ex.Message;
                    MessageBox.Show(error);
                }
            }
        }

        public static string CalcularDia(int col)
        {
            string respuesta;

            if (col < 10) { respuesta = "0" + col.ToString(); }
            else { respuesta = col.ToString(); }

            return respuesta;
        }

        public void CargarArchivo()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.InitialDirectory = CarpetaDestino;
                op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                op.Title = "Archivo.xlsx";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        txtRuta.Text = op.FileName;
                        btnGenerar.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void btnCargarCondiciones_Click(object sender, EventArgs e)
        {
            LeerTrabajadores();
            CargarMapeados();
        }

        private void btnMapear_Click(object sender, EventArgs e)
        {
            int IDPersona;
            int[] filas = dgvDataView.GetSelectedRows();

            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    IDPersona = Convert.ToInt32(dgvDataView.GetRowCellValue(filas[i], "Persona"));

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_MapearMecanicos(IDPersona, dtpPeriodo.Text, Utilitario.Instancia.SesionUsuario.usuario);
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
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea quitar a este personal de la tabla?", "QUITAR MECÁNICO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int IDPersona;
                int[] filas = dtgvDataMapeadosView.GetSelectedRows();

                if (filas.Length != 0)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        IDPersona = Convert.ToInt32(dtgvDataMapeadosView.GetRowCellValue(filas[i], "Persona"));

                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_QuitarMecánicos(IDPersona, dtpPeriodo.Text, Utilitario.Instancia.SesionUsuario.usuario);
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
                else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dgvPersonal_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvPersonal.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("01"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("02"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("03"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("04"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("05"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("06"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("07"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("08"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("09"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("10"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("11"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("12"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("13"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("14"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("15"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("16"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("17"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("18"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("19"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("20"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("21"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("22"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("23"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("24"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("25"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("26"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("27"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("28"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("29"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("30"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvPersonal.Columns[e.ColumnIndex].Name.Contains("31"))
                {
                    if (e.Value != null)
                    {
                        if (e.Value.GetType() != typeof(System.DBNull))
                        {
                            if (Convert.ToString(e.Value) == "DF")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "M")
                            {
                                e.CellStyle.BackColor = Color.DeepSkyBlue;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "T")
                            {
                                e.CellStyle.BackColor = Color.DarkOrange;
                                e.CellStyle.ForeColor = Color.Gold;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "N")
                            {
                                e.CellStyle.BackColor = Color.MidnightBlue;
                                e.CellStyle.ForeColor = Color.Yellow;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "DM")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.OrangeRed;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "V")
                            {
                                e.CellStyle.BackColor = Color.Yellow;
                                e.CellStyle.ForeColor = Color.Red;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "F")
                            {
                                e.CellStyle.BackColor = Color.Black;
                                e.CellStyle.ForeColor = Color.Gray;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CO" || Convert.ToString(e.Value) == "COH")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Magenta;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
        }

        private void dgvPersonal_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvPersonal.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 4)
                        {
                            dgvPersonal.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvPersonal.ContextMenuStrip = null;
                        }
                        else
                        {
                            IDPersona = Convert.ToInt32(dgvPersonal.CurrentRow.Cells["IDPersona"].Value.ToString());
                            Nombre = dgvPersonal.CurrentRow.Cells["NOMBRE"].Value.ToString();
                            int dia = Convert.ToInt32(e.ColumnIndex - 3);

                            FechaFijaSelec = CalcularDia(dia);
                            FechaFijaSelec = FechaFijaSelec + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                            dgvPersonal.ContextMenuStrip = contextMenuStrip1;
                            lblFecha.Text = FechaFijaSelec;

                            if (dgvPersonal.Rows.Count > 0 && dgvPersonal.FirstDisplayedCell != null)
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

        private void tsIngresarAsistencia_Click(object sender, EventArgs e)
        {
            DataTable workTable = new DataTable("Marcaciones");
            DataColumn column1 = new DataColumn("IDPersona");
            DataColumn column2 = new DataColumn("Fecha");

            workTable.Columns.Add(column1);
            workTable.Columns.Add(column2);
            DataRow row1 = workTable.NewRow();
            row1["IDPersona"] = "1";
            row1["Fecha"] = "2";
            workTable.Rows.Add(row1);

            Int32 selectedCellCount = dgvPersonal.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                if (dgvPersonal.AreAllCellsSelected(true)) { MessageBox.Show("Todas las celdas están seleccionadas.", "Selected Cells"); }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<r>");

                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        int col = Convert.ToInt32(dgvPersonal.SelectedCells[i].ColumnIndex.ToString()) - 2;
                        string dia = CalcularDia(col);
                        dia = dia + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                        sb.Append("<d ");
                        sb.Append("IDPersona=\"");
                        sb.Append(dgvPersonal.Rows[dgvPersonal.SelectedCells[i].RowIndex].Cells[0].Value.ToString());
                        sb.Append("\"");
                        sb.Append(" Fecha=\"");
                        sb.Append(dia);
                        sb.Append("\"");
                        sb.Append(" />");
                    }

                    sb.Append("</r>");
                    xmlAsistencia = sb.ToString();

                    CargarComboEstado();
                    groupBox1.Enabled = false;
                    pAgregarEstado.Location = new System.Drawing.Point(565, 155);
                    pAgregarEstado.Visible = true;
                    pAgregarEstado.BringToFront();
                    lblEmpleado.Text = Nombre;
                }
            }
            else
            { MessageBox.Show("No puede ingresar una cantidad en una casilla sin seleccionar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void pAgregarEstado_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pAgregarEstado.Left = pAgregarEstado.Left + (e.X - xClick2);
                pAgregarEstado.Top = pAgregarEstado.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            pAgregarEstado.Visible = false;
            pAgregarEstado.SendToBack();
            txtCodigo.Clear();
            txtDescripcion.Clear();
        }

        private void btnNuevoEstado_Click(object sender, EventArgs e)
        {
            if (groupBox1.Enabled == false)
            {
                txtCodigo.Clear();
                txtDescripcion.Clear();
                groupBox1.Enabled = true;
            }
            else
            {
                txtCodigo.Clear();
                txtDescripcion.Clear();
                groupBox1.Enabled = false;
            }
        }

        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtDescripcion.Focus(); }
        }

        private void btnGuardarEstado_Click(object sender, EventArgs e)
        {
            if (txtCodigo.Text.Length == 0 || txtDescripcion.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtCodigo.Text.Length == 0) { txtCodigo.Focus(); }
                else { txtDescripcion.Focus(); }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_IngresarEstadoAsistencia(txtCodigo.Text, txtDescripcion.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    CargarComboEstado();
                    groupBox1.Enabled = false;
                    txtCodigo.Clear();
                    txtDescripcion.Clear();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_RegistrarAsistencia(dtpPeriodo.Text, xmlAsistencia, Convert.ToString(cbxEstado.SelectedValue), Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            
            if (NroRPTA == "0")
            {
                btnCerrar_Click(sender, e);
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarTabla();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsEliminarAsistencia_Click(object sender, EventArgs e)
        {
            DataTable workTable = new DataTable("Marcaciones");
            DataColumn column1 = new DataColumn("IDPersona");
            DataColumn column2 = new DataColumn("Fecha");

            workTable.Columns.Add(column1);
            workTable.Columns.Add(column2);
            DataRow row1 = workTable.NewRow();
            row1["IDPersona"] = "1";
            row1["Fecha"] = "2";
            workTable.Rows.Add(row1);

            Int32 selectedCellCount = dgvPersonal.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                if (dgvPersonal.AreAllCellsSelected(true)) { MessageBox.Show("Todas las celdas están seleccionadas.", "Selected Cells"); }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<r>");

                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        int col = Convert.ToInt32(dgvPersonal.SelectedCells[i].ColumnIndex.ToString()) - 2;
                        string dia = CalcularDia(col);
                        dia = dia + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                        sb.Append("<d ");
                        sb.Append("IDPersona=\"");
                        sb.Append(dgvPersonal.Rows[dgvPersonal.SelectedCells[i].RowIndex].Cells[0].Value.ToString());
                        sb.Append("\"");
                        sb.Append(" Fecha=\"");
                        sb.Append(dia);
                        sb.Append("\"");
                        sb.Append(" />");
                    }

                    sb.Append("</r>");
                    xmlAsistencia = sb.ToString();

                    if (MessageBox.Show("¿Desea quitar la asistencia de este mecánico?", "ELIMINAR ASISTENCIA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_EliminarAsistencia(dtpPeriodo.Text, xmlAsistencia, Usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);

                        if (NroRPTA == "0")
                        {
                            MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarTabla();
                        }
                        else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
            else
            { MessageBox.Show("No puede ingresar una cantidad en una casilla sin seleccionar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtpPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { CargarTabla(); }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { CargarTabla(); }
        }  

        private void txtPuesto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { CargarTabla(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { CargarTabla(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            System.Data.DataTable dt2 = new System.Data.DataTable();
            dt2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_ListarTablaMecanicos(dtpPeriodo.Text, txtNombre.Text, txtPuesto.Text);
            gcExcelPersonal.DataSource = null;
            gvExcelPersonal.Columns.Clear();
            gcExcelPersonal.DataSource = dt2;
            if (dt2.Rows.Count > 0) { gvExcelPersonal.Columns["IDPersona"].Visible = false; }

            if (dt2.Rows.Count == 0)
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
                string nombre = System.IO.Path.Combine(desktop, "ROOSTER PROYECTADO DE MTTO - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                gcExcelPersonal.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImportarAsist_Click(object sender, EventArgs e)
        {
            lblPeriodo.Text = dtpPeriodo.Text;
            pImportarAsistencia.Location = new System.Drawing.Point(565, 155);
            btnGenerar.Enabled = false;

            pImportarAsistencia.Visible = true;
            pImportarAsistencia.BringToFront();
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pImportarAsistencia.Visible = false;
            pImportarAsistencia.SendToBack();
        }

        private void pImportarAsistencia_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pImportarAsistencia.Left = pImportarAsistencia.Left + (e.X - xClick);
                pImportarAsistencia.Top = pImportarAsistencia.Top + (e.Y - yClick);
            }
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            try
            {
                CargarArchivo();

                if (System.IO.File.Exists(txtRuta.Text))
                {
                    string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtRuta.Text);
                    OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                    string queryDetalle = String.Format("select * from [{0}$]", "IMPORTAR");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtListaAsist = dataSetDetalle.Tables[0];
                }

                if (dtListaAsist.Rows.Count > 0)
                {
                    xmlAsist = "";
                    dgvAsistencias.DataSource = dtListaAsist;
                    xmlAsist = Comun.Utilitario.Instancia.DatatableToXml(dtListaAsist);
                    btnGenerar.Enabled = true;
                }
                else { dgvAsistencias.DataSource = null; }
            }
            catch (Exception ex)
            {
                btnGenerar.Enabled = false;
                dgvAsistencias.DataSource = null;
                MessageBox.Show("El archivo seleccionado no es el correcto.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta = "";

            try
            {
                if (MessageBox.Show("¿Desea registrar las asistencias de este periodo?", "IMPORTAR ASISTENCIAS", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_RoosterProyectado_ImportarAsistencia(lblPeriodo.Text, xmlAsist, Usuario);

                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        txtRuta.Clear();
                        dgvAsistencias.DataSource = null;
                        btnGenerar.Enabled = false;
                        btnCerrar2_Click(sender, e);
                        CargarTabla();
                        MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
            catch (Exception ex) { MessageBox.Show(Respuesta, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
