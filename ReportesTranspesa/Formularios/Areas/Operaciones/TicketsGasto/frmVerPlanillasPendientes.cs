using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using Comun;


namespace ReportesTranspesa.Formularios.Areas.Operaciones.TicketsGasto
{
    public partial class frmVerPlanillasPendientes : Form
    {
        public frmVerPlanillasPendientes()
        {
            InitializeComponent();
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
        }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        { CargarComboOperacion(); }

        private void frmVerPlanillasPendientes_Load(object sender, EventArgs e)
        {
            CargarComboOperacion();
            cbxOperaciones.SelectedValue = 5;
            ListarPlanillas();
        }


        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarOperaciones();
            cbxOperaciones.DataSource = dtOperacion;
            cbxOperaciones.DisplayMember = "Descripcion";
            cbxOperaciones.ValueMember = "IdOperacion";
        }

        public void ListarPlanillas()
        {
            dtgPlanillas.DataSource = null;
            dtgvPlanillasVista.Columns.Clear();

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            dt = clsOperacionesBL.Instancia.ReportesApp_Operaciones_TicketGasto_ListarPlanillasPendientes(txtConductor.Text, Convert.ToInt32(cbxOperaciones.SelectedValue));
            if (dt.Rows.Count > 0)
            {
                dtgPlanillas.DataSource = dt;
                
                dtgvPlanillasVista.Columns["FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvPlanillasVista.Columns["FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy";
                
                dtgvPlanillasVista.BestFitColumns();
            }
        }

        private void txtConductor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarPlanillas(); }
        }

        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e) { ListarPlanillas(); }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarPlanillas(); }
    }
}
