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
    public partial class Productividad : MetroFramework.Forms.MetroForm
    {
        public Productividad()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvDataViewUnidTransito.Columns.Clear();
            dtgvUnidadTransito.DataSource = null;
            System.Data.DataTable dt = new System.Data.DataTable();
            //dt = clsOperacionesBL.Instancia.GetUnidades_Transito();
            dt = clsOperacionesBL.Instancia.GetUnidades_Transito(dtpFechaIni.Value.ToShortDateString(),dtpFechaFin.Value.ToShortDateString());
            if (dt.Rows.Count > 0)
            {
                dtgvUnidadTransito.DataSource = dt;
                dtgvDataViewUnidTransito.Columns["FechaProgramada"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataViewUnidTransito.Columns["FechaProgramada"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
                dtgvDataViewUnidTransito.Columns["FechaLlegada"].DisplayFormat.FormatType = FormatType.DateTime;
                dtgvDataViewUnidTransito.Columns["FechaLlegada"].DisplayFormat.FormatString = "dd/MM/yyyy hh:mm tt";
                dtgvDataViewUnidTransito.BestFitColumns();
                //Mensaje m = new Mensaje();
                //m.mensaje = dtpFechaIni.Value.ToShortDateString();
                //m.ShowDialog();

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
