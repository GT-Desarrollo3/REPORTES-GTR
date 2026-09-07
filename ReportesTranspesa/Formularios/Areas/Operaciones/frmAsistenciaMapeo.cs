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
using System.Globalization;
using System.Diagnostics;
using Comun;
using ReportesTranspesa.Sistema;
using DevExpress.Utils;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmAsistenciaMapeo : Form 
    {
        int IDPersona;
        string NombrePersona;
        string FechaFijaSelec;
        byte OperacionesCargadas=0;
        private frmAsistenciaModificar frmAsistenciaModif;
        private frmAsistenciaCompensar frmAsistenciaCompensar;
        private frmAsistenciaCompensarNoche frmAsistenciaCompensarNoche;

        int numeroCol = 0;
        int saveRow = 0;
        int saveCol = 0;
        public int Cesados = 0;

        public frmAsistenciaMapeo()
        {
            InitializeComponent();
            cbxOperacion2.SelectedIndexChanged -= cbxOperacion2_SelectedIndexChanged;
        }

        private void cbxOperacion2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void frmAsistenciaMapeo_Load(object sender, EventArgs e)
        {
            toolStrip2.Text = toolStrip2.Text +" PARA PERIODO: "+ dtpPeriodo.Text;

            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime oPrimerDiaDelMes = new DateTime(date.Year, date.Month, 1);
            dtpPeriodo.Value = oPrimerDiaDelMes;
            dtpFechaRetorno.Value = oPrimerDiaDelMes;

            cargarMarcas();
            cargarMapeados();
            CargarComboOperacion();
            cbxOperacion2.Text = "TODAS";

            dgvAsistenciaView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvAsistenciaView.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvAsistenciaView.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvIndicador.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvIndicador.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvIndicador.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dgvPlanConductor.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPlanConductor.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dgvPlanConductor.RowHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            dtpFechaBusqueda.Value = DateTime.Now;
            btnActualizar_Click(sender, e);
            dtpFechaPlan.Value = DateTime.Now;
            btnActualizar2_Click(sender, e);
        }

        private void CargarComboOperacion()
        {
            DataTable dtOperacion = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasListarOperaciones(Utilitario.Instancia.SesionUsuario.usuario);
            cbxOperacion2.DataSource = dtOperacion;
            cbxOperacion2.DisplayMember = "DESCRIPCION";
            cbxOperacion2.ValueMember = "ID";
        }

        void cargarMarcas()
        {
            string Planilla;
            Planilla = "";

            if (cbPlanilla.Text == "CONDUCTORES")
            { Planilla = "CD"; }

            if (cbPlanilla.Text == "EMPLEADOS")
            { Planilla = "EM"; }

            if (cbPlanilla.Text == "OBREROS")
            { Planilla = "OB"; }

            if (chkCesados.Checked == true) { Cesados = 1; }
            else { Cesados = 0; }

            DataTable dtOperaciones;
            DataTable dt;

            DataTable dtListaCompensacion = new DataTable();
            dtListaCompensacion = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ContarCompensaciones(0);

            dtOperaciones = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasListarOperaciones(Utilitario.Instancia.SesionUsuario.usuario);
            dt = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasCargar("10000000", dtpPeriodo.Text, Planilla, Utilitario.Instancia.SesionUsuario.usuario, Cesados, Convert.ToInt32(cbxOperacion.SelectedValue), txtNombre.Text, txtCargo.Text);

            dgvAsistenciaView.DataSource = null;
            dgvAsistenciaView.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                if(OperacionesCargadas==0)
                {
                    cbxOperacion.DataSource = dtOperaciones;
                    cbxOperacion.DisplayMember = "Descripcion";
                    cbxOperacion.ValueMember = "ID";
                    cbxOperacion.SelectedIndex = 0;

                    OperacionesCargadas = 1;
                }

                numeroCol = dt.Columns.Count;
                dgvAsistenciaView.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#2ACAF9");
                dgvAsistenciaView.EnableHeadersVisualStyles = false;
                
                dgvAsistenciaView.DataSource = dt;
                dgvAsistenciaView.AutoResizeColumns();
                dgvAsistenciaView.Columns["IdPersona"].Visible = false;
                dgvAsistenciaView.Columns["Nombre"].Frozen = true;
                dgvAsistenciaView.Columns["FIngreso"].Frozen = true;
                dgvAsistenciaView.Columns["Operacion"].Frozen = true;
                dgvAsistenciaView.Columns["Puesto"].Frozen = true;
                dgvAsistenciaView.Columns["DIASXCOMP"].Frozen = true;
                dgvAsistenciaView.Columns["DIAS_ADELA"].Frozen = true;
                dgvAsistenciaView.Columns["Nombre"].ReadOnly = true;
                dgvAsistenciaView.Columns["FIngreso"].ReadOnly = true;
                dgvAsistenciaView.Columns["Operacion"].ReadOnly = true;
                dgvAsistenciaView.Columns["Puesto"].ReadOnly = true;
                dgvAsistenciaView.Columns["DIASXCOMP"].ReadOnly = true;
                dgvAsistenciaView.Columns["DIAS_ADELA"].ReadOnly = true;
                //dgvAsistenciaView.Rows[0].Frozen = true;
                
                //dgvAsistenciaView.Rows[-1].DefaultCellStyle.BackColor = Color.Orange;
                //MessageBox.Show("NumeroCol=" + numeroCol.ToString());
                //for (int i = 0; i < numeroCol; i++)
                //{
                //    dgvAsistenciaView.Columns[i].HeaderText = dtCabecera.Rows[0][i].ToString();
                //}
                
                FiltrarOperacion();
                //dgvAsistenciaView.Rows.RemoveAt(0);
                ubicarCelda();

                cargarIndicadores();
                ListarRetornos();
            }   
            else
            {
                MessageBox.Show("No hay Datos", "Aviso");
                btnBuscar.Focus();
            }
        }

        void ubicarCelda()
        {
            try
            {
                if (saveRow != 0 && saveRow < dgvAsistenciaView.Rows.Count)
                {
                    dgvAsistenciaView.FirstDisplayedScrollingColumnIndex = saveCol;
                    dgvAsistenciaView.FirstDisplayedScrollingRowIndex = saveRow;
                }
            }
            catch (Exception ex) //bloque catch para captura de error
            {
                string error = ex.Message;//acción para manejar el error
                MessageBox.Show(error);
            }
        }

        void FiltrarOperacion()
        {         

            string dato = cbxOperacion.Text.ToString();
            string colFiltrar = "Operacion";

            if (dato!="TODAS")
            {
                ((DataTable)dgvAsistenciaView.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}' OR [{0}] LIKE 'Operacion'", colFiltrar, dato);
            }
            
        }
            
        void cargarMapeados()
        {
            string Planilla;
            Planilla = "";

            if (cbPlanilla.Text == "CONDUCTORES")
            { Planilla = "CD"; }

            if (cbPlanilla.Text == "EMPLEADOS")
            { Planilla = "EM"; }

            if (cbPlanilla.Text == "OBREROS")
            { Planilla = "OB"; }

            DataTable dt;

            dt = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasListarMapeados("10000000", dtpPeriodo.Text , Planilla, Utilitario.Instancia.SesionUsuario.usuario);

            if (dt.Rows.Count > 0)
            {
                dtgvDataMapeados.DataSource = dt;
                dtgvDataMapeadosView.Columns["IDPERSONA"].Visible = false;
                dtgvDataMapeadosView.UpdateSummary();
                dtgvDataMapeadosView.BestFitColumns();
            }
            else
            {
                MessageBox.Show("Aún No hay trabajadores Mapeados", "Aviso");
                dtgvDataMapeados.DataSource = null;
            }
        }

        public void cargarIndicadores()
        {
            DataTable dt = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_AsistenciasView_ListarIndicadores(dtpFechaBusqueda.Value);

            dgvIndicador.DataSource = null;
            dgvIndicador.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                int totalAsistio = dt.AsEnumerable().Where(r => !string.Equals(Convert.ToString(r["OPERACION"]), "TOTAL", StringComparison.OrdinalIgnoreCase))
                                                    .Where(r => r["A"] != DBNull.Value).Sum(r => Convert.ToInt32(r["A"]));

                int totalGeneral = dt.AsEnumerable().Where(r => !string.Equals(Convert.ToString(r["OPERACION"]), "TOTAL", StringComparison.OrdinalIgnoreCase))
                                                    .Where(r => r["TOTAL"] != DBNull.Value).Sum(r => Convert.ToInt32(r["TOTAL"]));

                double porcentaje = totalGeneral == 0 ? 0 : (totalAsistio * 100.0) / totalGeneral;

                dgvIndicador.DataSource = dt;
                dgvIndicador.AutoResizeColumns();
                dgvIndicador.Columns["FECHA"].Frozen = true;
                dgvIndicador.Columns["FECHA"].ReadOnly = true;
                dgvIndicador.Columns["OPERACION"].Frozen = true;
                dgvIndicador.Columns["OPERACION"].ReadOnly = true;

                DataRow drMeta = dt.NewRow();
                drMeta["FECHA"] = "% DE ASISTENCIA";
                drMeta["OPERACION"] = "META: 90%";
                drMeta["A"] = Convert.ToInt32(porcentaje.ToString("0"));
                dt.Rows.Add(drMeta);

                dgvIndicador.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFF12B");
                dgvIndicador.EnableHeadersVisualStyles = false;
            }
        }

        public void cargarPlanConductores()
        {
            DataTable dt = clsRecursosHumanosBL.Instancia.ReportesApp_Operaciones_PlanConductores_Listar(dtpFechaPlan.Value);

            dgvPlanConductor.DataSource = null;
            dgvPlanConductor.Columns.Clear();

            if (dt.Rows.Count > 0)
            {
                dgvPlanConductor.DataSource = dt;
                dgvPlanConductor.AutoResizeColumns();
                dgvPlanConductor.Columns["OPERACION"].Frozen = true;
                dgvPlanConductor.Columns["OPERACION"].ReadOnly = true;
                dgvPlanConductor.Columns["REGIMEN"].Frozen = true;
                dgvPlanConductor.Columns["REGIMEN"].ReadOnly = true;

                dgvPlanConductor.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FA7070");
                dgvPlanConductor.EnableHeadersVisualStyles = false;
            }
        }

        public void ListarRetornos()
        {
            DataTable dtListaRetornos = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_ListarRetornos(cbxOperacion2.Text, "01/" + dtpFechaRetorno.Text);
            dtgRetornos.DataSource = dtListaRetornos;
            
            if (dtListaRetornos.Rows.Count > 0)
            {
                dgvRetornosVista.Columns["FECHA_RETORNO"].Summary.Clear();
                dgvRetornosVista.Columns["FECHA_RETORNO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "FECHA_RETORNO", "Total = {0}");

                dgvRetornosVista.BestFitColumns();
            }
        }


        private void dgvAsistenciaView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
               for (int i = 3; i < numeroCol; i++)
               { dgvAsistenciaView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable; }

                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("01"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("02"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("03"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("04"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("05"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("06"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("07"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("08"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("09"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("10"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("11"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("12"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("13"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("14"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("15"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("16"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("17"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("18"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("19"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("20"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("21"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("22"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("23"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("24"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("25"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("26"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("27"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("28"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("29"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }

                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("30"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
                
                if (this.dgvAsistenciaView.Columns[e.ColumnIndex].Name.Contains("31"))
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
                            else if (Convert.ToString(e.Value) == "A" || Convert.ToString(e.Value) == "AE")
                            {
                                e.CellStyle.BackColor = Color.LightGreen;
                                e.CellStyle.ForeColor = Color.DarkBlue;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "SB")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.Red;
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
                            else if (Convert.ToString(e.Value) == "S")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CN")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.Black;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "CA")
                            {
                                e.CellStyle.BackColor = Color.LightSalmon;
                                e.CellStyle.ForeColor = Color.DarkViolet;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LT")
                            {
                                e.CellStyle.BackColor = Color.Gray;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                            else if (Convert.ToString(e.Value) == "LP")
                            {
                                e.CellStyle.BackColor = Color.LightSkyBlue;
                                e.CellStyle.ForeColor = Color.White;
                                e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception) { throw; }
         }


        string xmlAsistencias;
        private void mnuModAsistencia_Click(object sender, EventArgs e)
        {

            DataTable workTable = new DataTable("Marcaciones");
            DataColumn column1 = new DataColumn("IDPersona");
            DataColumn column2 = new DataColumn("Fecha");

            string Planilla;
            Planilla = "";

            if (cbPlanilla.Text == "CONDUCTORES")
            { Planilla = "CD"; }

            if (cbPlanilla.Text == "EMPLEADOS")
            { Planilla = "EM"; }

            if (cbPlanilla.Text == "OBREROS")
            { Planilla = "OB"; }

            
            workTable.Columns.Add(column1);
            workTable.Columns.Add(column2);
            DataRow row1 = workTable.NewRow();
            row1["IDPersona"] = "1";
            row1["Fecha"] = "2";
            workTable.Rows.Add(row1);

            Int32 selectedCellCount = dgvAsistenciaView.GetCellCount(DataGridViewElementStates.Selected);
            //MessageBox.Show(selectedCellCount.ToString(), "Total Columns");
            if (selectedCellCount > 0)
            {
                if (dgvAsistenciaView.AreAllCellsSelected(true))
                {
                    MessageBox.Show("All cells are selected", "Selected Cells");
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<r>");
                    for (int i = 0;
                        i < selectedCellCount; i++)
                    {
                        int col = Convert.ToInt32(dgvAsistenciaView.SelectedCells[i].ColumnIndex.ToString()) - 6;
                        string dia = Dia(col);
                        dia = dia + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");//dtpPeriodo.Text.Substring(dtpPeriodo.Text.Length - 2, 2) + "/" + dtpPeriodo.Text.Substring(0, 4);
                        sb.Append("<d ");
                        sb.Append("IDPersona=\"");
                        sb.Append(dgvAsistenciaView.Rows[dgvAsistenciaView.SelectedCells[i].RowIndex].Cells[0].Value.ToString()); //sb.Append(dgvAsistenciaView.SelectedCells[i].RowIndex.ToString());
                        sb.Append("\"");
                        sb.Append(" Fecha=\"");
                        sb.Append(dia);
                        sb.Append("\"");        
                        sb.Append(" />");
                                               
                        //sb.Append(Environment.NewLine);
                    }
                    sb.Append("</r>"); 
                    //sb.Append("Total: " + selectedCellCount.ToString());
                    //MessageBox.Show(sb.ToString(), "Selected Cells");
                    xmlAsistencias = sb.ToString();
                }
            }

            //Formulario Modificar Asistencia
            //if (txtIdEmpleado.Text != "")
            //{
                frmAsistenciaModif = new frmAsistenciaModificar();
                frmAsistenciaModif.IDPersona = IDPersona;
                frmAsistenciaModif.NombrePersona = NombrePersona;
                frmAsistenciaModif.Periodo = dtpPeriodo.Text;
                frmAsistenciaModif.Planilla = Planilla;
                frmAsistenciaModif.XmlAsistencias = xmlAsistencias;
                frmAsistenciaModif.ShowDialog();
                if (frmAsistenciaModif.Refrescar==true)
                { cargarMarcas(); }
                      
            //}
            //else
            //{
            //    MessageBox.Show("Debe seleccionar a un empleado de la lista o buscar uno.");
            //}
            
        }

        private static string Dia(int col)
        {
            string respuesta;
             if(col<10)
             {
                 respuesta = "0" + col.ToString();
             }
             else
             {
                 respuesta = col.ToString();
             }
             return respuesta;
        }

        private void dgvAsistenciaView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                        
            if (dgvAsistenciaView.RowCount > 0)
            {
                                
                if (e.RowIndex != -1)
                {

                    if (e.ColumnIndex < 7)
                    {
                        dgvAsistenciaView.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                        //dgvAsistenciaView.CurrentRow.Cells[4].Selected = true;
                    }

                    IDPersona = Convert.ToInt32(dgvAsistenciaView.CurrentRow.Cells["IdPersona"].Value.ToString());
                    NombrePersona = dgvAsistenciaView.CurrentRow.Cells["Nombre"].Value.ToString();
                    int dia = Convert.ToInt32(e.ColumnIndex-6);
                    FechaFijaSelec = Dia(dia); 
                    FechaFijaSelec = FechaFijaSelec+"/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                        
                    //Captura de la Posicion en DataGridView                  
                    if (dgvAsistenciaView.Rows.Count > 0 && dgvAsistenciaView.FirstDisplayedCell != null)
                    { //saveRow = dgvAsistenciaView.FirstDisplayedCell.RowIndex;
                        saveRow = e.RowIndex;
                      saveCol = e.ColumnIndex;
                      //MessageBox.Show("Fila: " + saveRow.ToString() + ", Col: " + saveCol.ToString(), "Aviso");
                    //Captura de la Posicion en DataGridView
                    }
                        
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

            LeerTrabajadores();
            cargarMapeados();
        }

        void LeerTrabajadores()
        {

            string Planilla;
            Planilla="";

            if (cbPlanilla.Text=="CONDUCTORES")
            { Planilla = "CD"; }

            if (cbPlanilla.Text == "EMPLEADOS")
            { Planilla = "EM"; }

            if (cbPlanilla.Text == "OBREROS")
            { Planilla = "OB"; }

            DataTable dt;

      
            dt = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasListarTrabajadores("10000000", dtpPeriodo.Text, Planilla, Utilitario.Instancia.SesionUsuario.usuario);

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dgvDataView.Columns["IDPERSONA"].Visible = false;
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
                dgvDataView.UpdateSummary();
                dgvDataView.BestFitColumns();

            }
            else
            {
                MessageBox.Show("No se encontraron Datos", "Aviso");
                dtgvData.DataSource = null;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {


            string Planilla;

            Planilla = "";

            if (cbPlanilla.Text == "CONDUCTORES")
            { Planilla = "CD"; }

            if (cbPlanilla.Text == "EMPLEADOS")
            { Planilla = "EM"; }

            if (cbPlanilla.Text == "OBREROS")
            { Planilla = "OB"; }

            string Persona;
            string ok;

            int[] filas = dgvDataView.GetSelectedRows();
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    Persona = dgvDataView.GetRowCellValue(filas[i], "IDPERSONA").ToString();

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasMapearTrabajadores("10000000", Persona, dtpPeriodo.Text, Planilla, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    
                    if (NroRPTA == "0") { ok = Respuesta; }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }
                cargarMapeados();
                LeerTrabajadores();
            }
            else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtpPeriodo_KeyPress(object sender, KeyPressEventArgs e)
        {
           if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Up) ||e.KeyChar == (char)Keys.Down) { cargarMarcas(); }
        }

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e)
        {
            cargarMarcas();

            /*
            string dato = cbxOperacion.Text.ToString();
            string colFiltrar = "Operacion";

            if (cbPlanilla.Text != "CONDUCTORES")
            {
                return;
            }

            if (dato != "TODAS")
            {
                ((DataTable)dgvAsistenciaView.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}' OR [{0}] LIKE 'Operacion'", colFiltrar, dato);
            }
            else
            {
                ((DataTable)dgvAsistenciaView.DataSource).DefaultView.RowFilter = string.Format("1=1", colFiltrar, dato);
            }
            */
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                cargarMarcas();

                //((DataTable)dgvAsistenciaView.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "Nombre", txtNombre.Text);
            }
        }

        private void txtCargo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter))
            {
                cargarMarcas();

                //((DataTable)dgvAsistenciaView.DataSource).DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", "Puesto", txtCargo.Text);
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            string Planilla;

            Planilla = "";

            if (cbPlanilla.Text == "CONDUCTORES")
            { Planilla = "CD"; }

            if (cbPlanilla.Text == "EMPLEADOS")
            { Planilla = "EM"; }

            if (cbPlanilla.Text == "OBREROS")
            { Planilla = "OB"; }

            int Mes;
            string NombreMes;
            NombreMes = "MES";

            Mes = dtpPeriodo.Value.Month;
            if(Mes== 1)
            {NombreMes = "ENERO";}
            else if (Mes == 2) {NombreMes = "FEBRERO";}
            else if (Mes == 3) { NombreMes = "MARZO"; }
            else if (Mes == 4) { NombreMes = "ABRIL"; }
            else if (Mes == 5) { NombreMes = "MAYO"; }
            else if (Mes == 6) { NombreMes = "JUNIO"; }
            else if (Mes == 7) { NombreMes = "JULIO"; }
            else if (Mes == 8) { NombreMes = "AGOSTO"; }
            else if (Mes == 9) { NombreMes = "SEPTIEMBRE"; }
            else if (Mes == 10) { NombreMes = "OCTUBRE"; }
            else if (Mes == 11) { NombreMes = "NOVIEMBRE"; }
            else if (Mes == 12) { NombreMes = "DICIEMBRE"; }

            DialogResult dialogResult = MessageBox.Show("Seguro de Procesar Licencias y Vacaciones Del Periodo: " + NombreMes +  " " + dtpPeriodo.Value.Year.ToString(), "Consulta", MessageBoxButtons.YesNo);
            
            if (dialogResult == DialogResult.Yes)
            {
                ProcesarLicenciasYVacaciones("10000000", dtpPeriodo.Text, Planilla);
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        }
        
        void ProcesarLicenciasYVacaciones(string Empresa, string Periodo, string Plla)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsRecursosHumanosBL.Instancia.GetDataPlanillasAsistenciasProcesarLicenciasYVacaciones(Empresa, Periodo, Plla, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cargarMarcas();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void compensarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Planilla;
            Planilla = "";

            if (cbPlanilla.Text == "CONDUCTORES")
            { Planilla = "CD"; }

            if (cbPlanilla.Text == "EMPLEADOS")
            { Planilla = "EM"; }

            if (cbPlanilla.Text == "OBREROS")
            { Planilla = "OB"; }

            Int32 selectedCellCount = dgvAsistenciaView.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount == 0)
            { MessageBox.Show("No seleccionado ningún día.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (selectedCellCount==1)
            {
                frmAsistenciaCompensar = new frmAsistenciaCompensar();
                frmAsistenciaCompensar.IDPersona = IDPersona;
                frmAsistenciaCompensar.NombrePersona = NombrePersona;
                frmAsistenciaCompensar.Periodo = dtpPeriodo.Text;
                frmAsistenciaCompensar.FechaCompensa = FechaFijaSelec;
                frmAsistenciaCompensar.Planilla = Planilla;
                frmAsistenciaCompensar.ShowDialog();
                if (frmAsistenciaCompensar.Refrescar == true)
                { cargarMarcas(); }
            }
            else { MessageBox.Show("Solo debe ecoger 1 día para compesar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }

        private void regularizarDíaParaCompenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            cargarMarcas();
        }

        private void dtpPeriodo_Enter(object sender, EventArgs e)
        {
           // cargarMarcas();
        }

        private void maestroCompensacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void compensarXNocheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Planilla;
            Planilla = "";

            if (cbPlanilla.Text == "CONDUCTORES")
            { Planilla = "CD"; }

            if (cbPlanilla.Text == "EMPLEADOS")
            { Planilla = "EM"; }

            if (cbPlanilla.Text == "OBREROS")
            { Planilla = "OB"; }

            Int32 selectedCellCount = dgvAsistenciaView.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount == 0)
            { MessageBox.Show("No seleccionado ningún día.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (selectedCellCount == 1)
            {
                frmAsistenciaCompensarNoche = new frmAsistenciaCompensarNoche();
                frmAsistenciaCompensarNoche.IDPersona = IDPersona;
                frmAsistenciaCompensarNoche.NombrePersona = NombrePersona;
                frmAsistenciaCompensarNoche.Periodo = dtpPeriodo.Text;
                frmAsistenciaCompensarNoche.FechaCompensa = FechaFijaSelec;
                frmAsistenciaCompensarNoche.Planilla = Planilla;
                frmAsistenciaCompensarNoche.ShowDialog();
                if (frmAsistenciaCompensarNoche.Refrescar == true) { cargarMarcas(); }
            }
            else { MessageBox.Show("Solo debe ecoger 1 día para compesar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsAsistenciaExtendida_Click(object sender, EventArgs e)
        {
            string xmlAsistencias2;
            int selectedCellCount = dgvAsistenciaView.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                if (dgvAsistenciaView.AreAllCellsSelected(true))
                {
                    MessageBox.Show("Todas las celdas están seleccionadas.", "ASISTENCIA EXTENDIDA");
                }
                else
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<r>");
                    for (int i = 0; i < selectedCellCount; i++)
                    {
                        int col = Convert.ToInt32(dgvAsistenciaView.SelectedCells[i].ColumnIndex.ToString()) - 6;
                        string dia = Dia(col);
                        dia = dia + "/" + dtpPeriodo.Value.ToString("MM") + "/" + dtpPeriodo.Value.ToString("yyyy");
                        sb.Append("<d ");
                        sb.Append("IDPersona=\"");
                        sb.Append(dgvAsistenciaView.Rows[dgvAsistenciaView.SelectedCells[i].RowIndex].Cells[0].Value.ToString());
                        sb.Append("\"");
                        sb.Append(" Fecha=\"");
                        sb.Append(dia);
                        sb.Append("\"");
                        sb.Append(" Operacion=\"");
                        sb.Append(dgvAsistenciaView.Rows[dgvAsistenciaView.SelectedCells[i].RowIndex].Cells[4].Value.ToString());
                        sb.Append("\"");
                        sb.Append(" />");
                    }
                    sb.Append("</r>");
                    xmlAsistencias2 = sb.ToString();

                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_AsistenciaExtendida(dtpPeriodo.Text, xmlAsistencias2, Utilitario.Instancia.SesionUsuario.usuario);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cargarMarcas();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void tsCompensacionV_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvAsistenciaView.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount == 0)
            { MessageBox.Show("No ha seleccionado ningún día.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (selectedCellCount == 1)
            {
                frmAsistenciaCompVolcan frmAsistenciaCompVolcan = new frmAsistenciaCompVolcan();
                frmAsistenciaCompVolcan.Opcion = 1;
                frmAsistenciaCompVolcan.idPersona = IDPersona;
                frmAsistenciaCompVolcan.lblTitulo.Text = "COMPENSACIÓN";
                frmAsistenciaCompVolcan.lblNombre.Text = NombrePersona;
                frmAsistenciaCompVolcan.lblFecha.Text = FechaFijaSelec;
                frmAsistenciaCompVolcan.FechaSeleccionada = Convert.ToDateTime(FechaFijaSelec);
                frmAsistenciaCompVolcan.ShowDialog();

                if (frmAsistenciaCompVolcan.Refrescar == true) { cargarMarcas(); }
            }
            else { MessageBox.Show("Solo debe escoger 1 día para compensar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void tsCompAdelantada_Click(object sender, EventArgs e)
        {
            Int32 selectedCellCount = dgvAsistenciaView.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount == 0)
            { MessageBox.Show("No ha seleccionado ningún día.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (selectedCellCount == 1)
            {
                frmAsistenciaCompVolcan frmAsistenciaCompVolcan = new frmAsistenciaCompVolcan();
                frmAsistenciaCompVolcan.Opcion = 2;
                frmAsistenciaCompVolcan.idPersona = IDPersona; frmAsistenciaCompVolcan.lblTitulo.Text = "COMP. ADELANTADA";
                frmAsistenciaCompVolcan.lblNombre.Text = NombrePersona;
                frmAsistenciaCompVolcan.lblFecha.Text = FechaFijaSelec;
                frmAsistenciaCompVolcan.FechaSeleccionada = Convert.ToDateTime(FechaFijaSelec);
                frmAsistenciaCompVolcan.pCompAdelantada.Visible = true;
                frmAsistenciaCompVolcan.pCompAdelantada.BringToFront();
                frmAsistenciaCompVolcan.ShowDialog();

                if (frmAsistenciaCompVolcan.Refrescar == true) { cargarMarcas(); }
            }
            else { MessageBox.Show("Solo debe escoger 1 día para compensar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExportarExcel_Click(object sender, EventArgs e)
        {
             try
            {
               if (dgvAsistenciaView.DataSource == null)
                {

                    MessageBox.Show("No hay data para exportar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DataTable dtExcel = new DataTable();
                    dgvAsistenciaView.Columns["IdPersona"].Visible = true;

                    gcExcel.DataSource = null;
                    gvexcel.Columns.Clear();

                    dtExcel = Utilitario.Instancia.GetContentAsDataTable(dgvAsistenciaView,true);
                    gcExcel.DataSource = dtExcel;

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Reporte de Asistencias" + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    gvexcel.ExportToXlsx(nombre);
                    Process.Start(nombre);
                    dgvAsistenciaView.Columns["IdPersona"].Visible = false;
                }

           
            }
            catch (Exception ex )
            {
                
               MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            cargarMarcas();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            cargarMarcas();
        }

        private void reporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAsistenciaReporte frmReporte = new frmAsistenciaReporte();
            frmReporte.ShowDialog();

        }

        private void button4_Click_1(object sender, EventArgs e) { cargarMarcas(); }

        private void compensaAdelantadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Planilla;
            Planilla = "";

            if (cbPlanilla.Text == "CONDUCTORES")
            { Planilla = "CD"; }

            if (cbPlanilla.Text == "EMPLEADOS")
            { Planilla = "EM"; }

            if (cbPlanilla.Text == "OBREROS")
            { Planilla = "OB"; }

            Int32 selectedCellCount = dgvAsistenciaView.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount == 0)
            { MessageBox.Show("No seleccionado ningún día.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (selectedCellCount == 1)
            {
                frmAsistenciasCompensarAdelantado frm = new frmAsistenciasCompensarAdelantado();
                frm.IDPersona = IDPersona;
                frm.NombrePersona = NombrePersona;
                frm.Periodo = dtpPeriodo.Text;
                frm.FechaCompensa = FechaFijaSelec;
                frm.Planilla = Planilla;
                frm.ShowDialog();
                if (frm.Refrescar == true)
                { cargarMarcas(); }
            }
            else { MessageBox.Show("Solo debe escoger 1 día para compesar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }

        private void reportePorCompensarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmReporteAsistenciaxCompensar frmOpen = new frmReporteAsistenciaxCompensar();
                frmOpen.ShowDialog();

            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void reporteCesadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmReporteVacaciones frmOpen = new frmReporteVacaciones();
                frmOpen.ShowDialog();
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvAsistenciaView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvAsistenciaView.RowCount > 0)
                {
                    if (e.RowIndex != -1)
                    {
                        if (e.ColumnIndex < 7)
                        {
                            dgvAsistenciaView.CurrentRow.Cells[e.ColumnIndex].Selected = false;
                            dgvAsistenciaView.ContextMenuStrip = null;
                        }
                        else
                        {
                            dgvAsistenciaView.ContextMenuStrip = contextMenuStrip1;

                            string Programacion = dgvAsistenciaView.CurrentRow.Cells["OPERACION"].Value.ToString();

                            if (Programacion == "VOLCAN")
                            {
                                compensarToolStripMenuItem.Enabled = false;
                                compensarXNocheToolStripMenuItem.Enabled = false;
                                compensaAdelantadoToolStripMenuItem.Enabled = false;
                                tsCompensacionVolcan.Enabled = true;
                                tsAsistenciaExtendida.Enabled = true;
                            }
                            else
                            {
                                compensarToolStripMenuItem.Enabled = true;
                                compensarXNocheToolStripMenuItem.Enabled = true;
                                compensaAdelantadoToolStripMenuItem.Enabled = true;
                                tsCompensacionVolcan.Enabled = false;
                                tsAsistenciaExtendida.Enabled = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void dtpFechaBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter)) { cargarIndicadores(); }
        }

        private void dgvIndicador_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvIndicador.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (dgvIndicador.Columns[e.ColumnIndex].Name == "TOTAL")
                {
                    e.CellStyle.BackColor = Color.Gold;
                    e.CellStyle.ForeColor = Color.DarkBlue;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }

                if (Convert.ToString(dgvIndicador.Rows[e.RowIndex].Cells["OPERACION"].Value) == "TOTAL")
                {
                    e.CellStyle.BackColor = Color.Gold;
                    e.CellStyle.ForeColor = Color.DarkBlue;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }

                if (Convert.ToString(dgvIndicador.Rows[e.RowIndex].Cells["FECHA"].Value).Contains("%"))
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                    e.CellStyle.ForeColor = Color.DarkBlue;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }
            }
            catch (Exception) { throw; }
        }

        private void btnActualizar_Click(object sender, EventArgs e) { cargarIndicadores(); }

        private void btnExcel2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvIndicador.DataSource == null)
                { MessageBox.Show("No hay data para exportar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    DataTable dtExcel = new DataTable();

                    dtgIndicador.DataSource = null;
                    dgvIndicadorVista.Columns.Clear();

                    dtExcel = Utilitario.Instancia.GetContentAsDataTable(dgvIndicador, true);
                    dtgIndicador.DataSource = dtExcel;

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "INDICADOR DE ASISTENCIA - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvIndicadorVista.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtpFechaPlan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter)) { cargarPlanConductores(); }
        }

        private void btnActualizar2_Click(object sender, EventArgs e) { cargarPlanConductores(); }

        private void btnExcel3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPlanConductor.DataSource == null)
                { MessageBox.Show("No hay data para exportar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else
                {
                    DataTable dtExcel = new DataTable();

                    dtgPlanConductores.DataSource = null;
                    dgvPlanConductorVista.Columns.Clear();

                    dtExcel = Utilitario.Instancia.GetContentAsDataTable(dgvPlanConductor, true);
                    dtgPlanConductores.DataSource = dtExcel;

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "PLAN DE CONDUCTORES - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dgvPlanConductorVista.ExportToXlsx(nombre);
                    Process.Start(nombre);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dgvPlanConductor_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                dgvPlanConductor.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;

                if (Convert.ToString(dgvPlanConductor.Rows[e.RowIndex].Cells["OPERACION"].Value) == "TOTAL")
                {
                    e.CellStyle.BackColor = Color.Gainsboro;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                }

                if (dgvPlanConductor.Columns[e.ColumnIndex].Name == "NRO UT")
                {
                    e.CellStyle.BackColor = Color.Bisque;
                    e.CellStyle.ForeColor = Color.DarkRed;
                }

                if (dgvPlanConductor.Columns[e.ColumnIndex].Name == "VARIACION")
                {
                    e.CellStyle.BackColor = Color.Bisque;
                    e.CellStyle.ForeColor = Color.DarkRed;
                }

                if (dgvPlanConductor.Columns[e.ColumnIndex].Name == "COND REQUERIDOS")
                {
                    e.CellStyle.BackColor = Color.PaleTurquoise;
                    e.CellStyle.ForeColor = Color.Blue;
                }

                if (dgvPlanConductor.Columns[e.ColumnIndex].Name == "COND ACTUALES")
                {
                    e.CellStyle.BackColor = Color.PaleGreen;
                    e.CellStyle.ForeColor = Color.DarkGreen;
                }
            }
            catch (Exception) { throw; }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea quitar a este personal de la tabla?", "QUITAR EMPLEADO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int IDPersona;
                int[] filas = dtgvDataMapeadosView.GetSelectedRows();

                if (filas.Length != 0)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        IDPersona = Convert.ToInt32(dtgvDataMapeadosView.GetRowCellValue(filas[i], "IDPERSONA"));

                        DataTable dtRespuesta = new DataTable();
                        string Respuesta;
                        dtRespuesta = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Asistencias_QuitarMapeo(IDPersona, dtpPeriodo.Text, Utilitario.Instancia.SesionUsuario.usuario);
                        Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                        string NroRPTA = Respuesta.Substring(0, 1);
                        if (NroRPTA == "0") { }
                        else
                        {
                            MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }

                    LeerTrabajadores();
                    cargarMapeados();
                }
                else { MessageBox.Show("No ha seleccionado ningún registro", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtpFechaRetorno_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter)) { ListarRetornos(); }
        }

        private void cbxOperacion2_DropDownClosed(object sender, EventArgs e) { ListarRetornos(); }

        private void btnBuscar3_Click(object sender, EventArgs e) { ListarRetornos(); }

        private void btnExcel4_Click(object sender, EventArgs e)
        {
            if (dtgRetornos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE INGRESOS DE DESCANSOS " + DateTime.Now.ToString("dd-MM-yyyy") + " - " + Utilitario.Instancia.SesionUsuario.usuario + ".xlsx");
                dtgRetornos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
