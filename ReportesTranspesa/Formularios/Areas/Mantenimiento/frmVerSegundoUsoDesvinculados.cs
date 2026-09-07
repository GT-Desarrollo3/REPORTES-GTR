using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using Negocio;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class frmVerSegundoUsoDesvinculados : Form
    {
        public frmVerSegundoUsoDesvinculados()
        {
            InitializeComponent();
        }

        private void frmVerSegundoUsoDesvinculados_Load(object sender, EventArgs e)
        {
            DataTable dt = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Historial_SegundoUso_Desvinculados();
            DataTable dt2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_ActivoSegundoUso_HistorialRequerimientos();
            
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dgvVinculo.Columns["idActivo"].Visible = false;
                dgvVinculo.Columns["idEmpleado"].Visible = false;
                dgvVinculo.FixedLineWidth = 1;
                dgvVinculo.BestFitColumns();
            }
            else { dtgvData.DataSource = null; }

            if (dt2.Rows.Count > 0)
            {
                dtgvLogistica.DataSource = dt2;
                dgvVinculoR.Columns["idActivo"].Visible = false;
                dgvVinculoR.Columns["idEmpleado"].Visible = false;
                dgvVinculoR.Columns["FechaDesvincula"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvVinculoR.Columns["FechaDesvincula"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvVinculoR.FixedLineWidth = 1;
                dgvVinculoR.BestFitColumns();
            }
            else { dtgvLogistica.DataSource = null; }
        }


        private void btnBuscar_Click(object sender, EventArgs e) { frmVerSegundoUsoDesvinculados_Load(sender, e); }
    }
}
