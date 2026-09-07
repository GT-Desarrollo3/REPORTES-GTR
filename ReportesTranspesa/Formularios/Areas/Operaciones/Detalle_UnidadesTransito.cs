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
using DevExpress.Data;
using System.Globalization;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using Entidades;
using System.Diagnostics;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class Detalle_UnidadesTransito : MetroFramework.Forms.MetroForm
    {

        public string mensaje;
        public Detalle_UnidadesTransito()
        {
            InitializeComponent();
           
        }
        
        private void Detalle_UnidadesTransito_Load(object sender, EventArgs e)
        {
            lblmensaje.Text = mensaje;
            dtpFechaIni.Value = new DateTime(dtpFechaIni.Value.Year, dtpFechaIni.Value.Month, 1);
        
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string fechafin = "";
            //fechafin = dtpFechaFin.Value.ToShortDateString();
            dtgvDataViewUnidTransito.Columns.Clear();
            dtgvUnidadTransito.DataSource = null;
            //dtgvDataViewUnidTransito.Columns.Clear();
            //dtgvDataViewUnidTransito.GroupSummary.Clear();
            System.Data.DataTable dt = new System.Data.DataTable();
            //dt = clsOperacionesBL.Instancia.GetUnidades_Transito();
            dt = clsOperacionesBL.Instancia.GetUnidades_Transito(dtpFechaIni.Value.ToShortDateString(),fechafin);
            //dt = clsOperacionesBL.Instancia.GetUnidades_Transito(dtpFechaIni.Value.ToShortDateString(), dtpFechaFin.Value.ToShortDateString());
            if (dt.Rows.Count > 0)
            {
                dtgvUnidadTransito.DataSource = dt;

                //DataTable dt2 = new DataTable();
                //dt2.Clear();
                //DataRow row = dt.Rows[2];
                //mensaje = row["FechaProgramada"].ToString();
                //dt2.Columns.Add("FECHA");
                //DataRow fila = dt2.NewRow();
                //fila["FECHA"] = mensaje;    // Create an in-place LookupEdit control.
                //RepositoryItemLookUpEdit riLookup = new RepositoryItemLookUpEdit();
                //riLookup.DataSource = dt2;
                //riLookup.ValueMember = "FECHA";
                //riLookup.DisplayMember = "FECHA";

                // Enable the "best-fit" functionality mode in which columns have proportional widths and the popup window is resized to fit all the columns.
                //riLookup.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
                // Specify the dropdown height.
                //riLookup.DropDownRows = dt2.Rows.Count;

                // Enable the automatic completion feature. In this mode, when the dropdown is closed, 
                // the text in the edit box is automatically completed if it matches a DisplayMember field value of one of dropdown rows. 
                //riLookup.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoComplete;
                // Specify the column against which an incremental search is performed in SearchMode.AutoComplete and SearchMode.OnlyInPopup modes
                //riLookup.AutoSearchColumnIndex = 1;
                //dtgvDataViewUnidTransito.Columns["FECHA"].Width = 120;
                dtgvDataViewUnidTransito.Columns["FechaProgramada"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataViewUnidTransito.Columns["FechaProgramada"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
                dtgvDataViewUnidTransito.Columns["FechaLlegada"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataViewUnidTransito.Columns["FechaLlegada"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
                dtgvDataViewUnidTransito.BestFitColumns();
                Mensaje m = new Mensaje();
                m.mensaje = dtpFechaIni.Value.ToShortDateString();
                m.ShowDialog();
                //lblTotal.Text = dt.Rows.Count.ToString();
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
                //lblTotal.Text = "0";
            }
        }
    }
}
