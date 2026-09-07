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

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmUnidadesDisponibles : Form
    {
        string TipoVehiculo;

        public frmUnidadesDisponibles()
        {
            InitializeComponent();
        }

        private void frmUnidadesDisponibles_Load(object sender, EventArgs e)
        {
            rbTractos.Checked = true;
            rbTractos_Click(sender, e);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable dtListaTractos = new DataTable();

            dtListaTractos.Clear();
            dtListaTractos = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarDisponibles(TipoVehiculo, txtPlaca.Text);
            if (dtListaTractos.Rows.Count > 0)
            {
                dtgListaTractos.DataSource = null;
                dtgListaTractos.DataSource = dtListaTractos;
                dgvListaTractosView.Columns["PLACA"].Summary.Clear();
                dgvListaTractosView.Columns["PLACA"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "PLACA", "Total: {0}");
                dgvListaTractosView.BestFitColumns();
            }
            else { dtgListaTractos.DataSource = null; }
        }

        private void rbTractos_Click(object sender, EventArgs e)
        {
            TipoVehiculo = "TRACTOS";
            rbTractos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
            rbCarretas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);

            btnBuscar_Click(sender, e);
        }

        private void rbCarretas_Click(object sender, EventArgs e)
        {
            TipoVehiculo = "CARRETAS";
            rbTractos.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            rbCarretas.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);

            btnBuscar_Click(sender, e);
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnBuscar_Click(sender, e); }
        }
    }
}
