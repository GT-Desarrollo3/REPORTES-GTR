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
﻿using DevExpress.Export;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using System.IO;
using Microsoft.Office;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;


namespace ReportesTranspesa.Formularios.Areas.Operaciones.ItinerarioViajes
{
    public partial class frmRegistrarItinerario : Form
    {
        public int NroProgramacion, idConductorViaje, idTracto, idCarreta, IdRuta;
        public DateTime FechaProgramacion;

        
        public frmRegistrarItinerario()
        {
            InitializeComponent();
        }

        private void frmRegistrarItinerario_Load(object sender, EventArgs e)
        {
            dtpFechaViaje.Value = DateTime.Now;
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea generar el itinerario de este viaje?", "GENERAR ITINERARIO DE VIAJE", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ItinerarioViajes_RegistrarConsolidado(1, 0, NroProgramacion, FechaProgramacion, idTracto, idCarreta,
                                                         IdRuta, idConductorViaje, dtpFechaViaje.Value, Utilitario.Instancia.SesionUsuario.usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }        
        }
    }
}
