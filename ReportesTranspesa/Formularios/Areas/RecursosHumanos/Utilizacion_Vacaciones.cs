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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class Utilizacion_Vacaciones : MetroFramework.Forms.MetroForm
    {
        public Utilizacion_Vacaciones()
        {
            InitializeComponent();
        }
        int empleado = -1;
        int periodoempleado = 0;


        private void Utilizacion_Vacaciones_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("RRHH_Utilizacion_Vacaciones");
                DataTable dtEspeciales = null;
                if (dtPermisos != null)
                {
                    if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                    {
                        dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());
                    }
                    else { btnAgregar.Enabled = false; }
                }

                if (dtEspeciales != null)
                {
                    if (dtEspeciales.Rows[0]["NombrePermiso"].ToString() == "Programar Vacaciones")
                    {
                        btnAgregar.Enabled = true;
                    }
                    else { btnAgregar.Enabled = false; }
                }
            }
            catch
            {
                btnAgregar.Enabled = false;
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
                splitContainer1.SplitterDistance = 46;
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
                empleado = Int32.Parse(ItemActual.Text);
                txtEmpleado.Text = ItemActual.SubItems[1].Text;
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                splitContainer1.SplitterDistance = 46;
                btnBuscar_Click(sender, e);
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                splitContainer1.SplitterDistance = 46;
            }
        }

        private void lvEmpleado_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !lvEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lvEmpleado.SelectedItems[0];
                empleado = Int32.Parse(ItemActual.Text);
                txtEmpleado.Text = ItemActual.SubItems[1].Text;
                lvEmpleado.Visible = false;
                txtEmpleado.Focus();
                splitContainer1.SplitterDistance = 46;
                btnBuscar_Click(sender, e);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (txtEmpleado.Text.Trim() != "")
            {
                dtgvData.DataSource = null;
                dtgvDataView.Columns.Clear();
                dtgvDataView.GroupSummary.Clear();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsRecursosHumanosBL.Instancia.GetPeriodosVacaciones(empleado);
                if (dt.Rows.Count > 0)
                {
                    dtgvData.DataSource = dt;
                    dtgvDataView.BestFitColumns();
                }
                else
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hay data para mostrar";
                    m.AutoSize = true;
                    m.ShowDialog();
                }
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "Debe seleccionar un empleado";
                m.AutoSize = true;
                m.ShowDialog();
            }
        }

        //GERARDO - 21/08
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmProgramarVacaciones frmProgramarVacaciones = new frmProgramarVacaciones();
            frmProgramarVacaciones.Show();
        }
        //GERARDO - 21/08

        private void dtgvDataView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (dtgvData.DataSource != null)
            {
                periodoempleado = Convert.ToInt32(dtgvDataView.GetRowCellValue(e.FocusedRowHandle, "N° PERIODO").ToString());
                dtgvData2.DataSource = null;
                dtgvDataView2.Columns.Clear();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsRecursosHumanosBL.Instancia.GetUtilizacionVacaciones(empleado, periodoempleado);
                dtgvData2.DataSource = dt;
                dtgvDataView2.Columns["CREADO"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dtgvDataView2.Columns["CREADO"].DisplayFormat.FormatString = "g";
                dtgvDataView2.Columns["SECUENCIA"].Visible = false;
                dtgvDataView2.BestFitColumns();
                //////////////////////////////
                dtgvData3.DataSource = null;
                dtgvDataView3.Columns.Clear();
                System.Data.DataTable dt2 = new System.Data.DataTable();
                dt2 = clsRecursosHumanosBL.Instancia.GetPagosVacaciones(empleado, periodoempleado);
                dtgvData3.DataSource = dt2;
                dtgvDataView3.BestFitColumns();
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource != null && periodoempleado != 0)
            {
                Nueva_utilizacion nuevo = new Nueva_utilizacion();
                nuevo.MdiParent = this.ParentForm;
                nuevo.empleado = empleado;
                nuevo.usuario = Utilitario.Instancia.SesionUsuario.usuario;
                nuevo.periodo = periodoempleado;
                nuevo.RegistroGuardado += new EventHandler(ActualizarDatos);
                nuevo.Show();
            }
        }

        void ActualizarDatos(object sender, EventArgs e)
        {
            dtgvData2.DataSource = null;
            dtgvDataView2.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsRecursosHumanosBL.Instancia.GetUtilizacionVacaciones(empleado, periodoempleado);
            dtgvData2.DataSource = dt;
            dtgvDataView2.Columns["CREADO"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dtgvDataView2.Columns["CREADO"].DisplayFormat.FormatString = "g";
            dtgvDataView2.Columns["SECUENCIA"].Visible = false;
            dtgvDataView2.BestFitColumns();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            bool resultado;
            if (dtgvDataView2.GetFocusedRowCellValue("ORIGEN").ToString() == "PLANILLAS")
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No se puede borrar una utilización ingresada en Planillas";
                m.AutoSize = true;
                m.ShowDialog();
            }
            else 
            {
                resultado = clsRecursosHumanosBL.Instancia.BorrarUtilizacion(periodoempleado, empleado,
                Convert.ToInt32(dtgvDataView2.GetFocusedRowCellValue("SECUENCIA").ToString()));
                if (resultado != true)
                {
                    Mensaje m = new Mensaje();
                    m.mensaje = "Error al guardar";
                    m.AutoSize = true;
                    m.ShowDialog();
                }
                else
                {
                    dtgvData2.DataSource = null;
                    dtgvDataView2.Columns.Clear();
                    System.Data.DataTable dt = new System.Data.DataTable();
                    dt = clsRecursosHumanosBL.Instancia.GetUtilizacionVacaciones(empleado, periodoempleado);
                    dtgvData2.DataSource = dt;
                    dtgvDataView2.Columns["CREADO"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    dtgvDataView2.Columns["CREADO"].DisplayFormat.FormatString = "g";
                    dtgvDataView2.Columns["SECUENCIA"].Visible = false;
                    dtgvDataView2.BestFitColumns();
                }
            }
        }
    }
}
