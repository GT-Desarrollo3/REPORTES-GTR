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
using DevExpress.Data;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class Consumo_Mantenimiento : MetroFramework.Forms.MetroForm
    {
        public int idGrupo;

        public Consumo_Mantenimiento()
        {
            InitializeComponent();
            cbxGrupo.SelectedIndexChanged -= cbxGrupo_SelectedIndexChanged;
            cbxTipoMaquina.SelectedIndexChanged -= cbxTipoMaquina_SelectedIndexChanged;
        }

        private void cbxGrupo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboGrupo(); }

        private void cbxTipoMaquina_SelectedIndexChanged(object sender, EventArgs e) { CargarComboTipo(); }

        private void Consumo_Mantenimiento_Load(object sender, EventArgs e)
        {
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
            CargarComboGrupo();
            cbxGrupo_DropDownClosed(sender, e);
        }

        private void CargarComboGrupo()
        {
            DataTable dtGrupo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Consumo_ListarGrupoTipoMaquina(1,0);
            cbxGrupo.DataSource = dtGrupo;
            cbxGrupo.DisplayMember = "Descripcion";
            cbxGrupo.ValueMember = "idGrupo";
        }

        private void CargarComboTipo()
        {
            DataTable dtTipo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Consumo_ListarGrupoTipoMaquina(2,idGrupo);
            cbxTipoMaquina.DataSource = dtTipo;
            cbxTipoMaquina.DisplayMember = "Descripcion";
            cbxTipoMaquina.ValueMember = "idTipo";
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechainicio = "01/01/1980";
            string fechafin = "31/12/2030";
            fechainicio = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechafin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsMantenimientoBL.Instancia.GetDataConsumo(fechainicio, fechafin, cbxGrupo.Text, cbxTipoMaquina.Text, txtPlaca.Text);
            //dt = clsMantenimientoBL.Instancia.GetDataConsumo(dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
               // dtpFechaFin.Value.ToShortDateString() + " 23:59:59");
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();
            }
            /*
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
            */
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Consumo de Mantenimiento del " + dtpFechaIni.Value.ToString("dd_MM_yyyy") + 
                    " al " + dtpFechaFin.Value.ToString("dd_MM_yyyy") + " " + 
                    Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else { dtgvData.ShowPrintPreview(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
        }

        private void cbxGrupo_DropDownClosed(object sender, EventArgs e)
        {
            idGrupo = Convert.ToInt32(cbxGrupo.SelectedValue);
            CargarComboTipo();
        }

        // GERARDO - 16/08
        private void txtPlaca_KeyUp(object sender, KeyEventArgs e) { btnBuscar_Click(sender, e); }
        // GERARDO - 16/08
    }
}
