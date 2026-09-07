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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.CumplimientoViajes
{
    public partial class frmRegistrarCumplimiento : Form
    {
        public int OpcionTL;
        public int Editar;
        public frmRegistroCumplimiento formulario;

        public frmRegistrarCumplimiento()
        {
            InitializeComponent();
            cbxTipoViaje.SelectedIndexChanged -= cbxTipoViaje_SelectedIndexChanged;
        }

        private void cbxTipoViaje_SelectedIndexChanged(object sender, EventArgs e) { CargarComboGrupo2(); }

        private void frmRegistrarCumplimiento_Load(object sender, EventArgs e) { }


        public void CargarComboGrupo2()
        {
            DataTable dtGrupo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_ListarGrupoViaje(OpcionTL);
            cbxTipoViaje.DataSource = dtGrupo;
            cbxTipoViaje.DisplayMember = "Descripcion";
            cbxTipoViaje.ValueMember = "idGrupoViaje";
        }


        private void txtViDisponible_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void txtProyReq_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cbxTipoViaje.SelectedValue = "1";
            dtpFechaCumplimiento.Value = DateTime.Now;
            txtViDisponible.Clear();
            txtProyReq.Clear();
            txtDetalle.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtViDisponible.Text.Length == 0 || txtProyReq.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtViDisponible.Text.Length == 0) { txtViDisponible.Focus(); }
                else { txtProyReq.Focus(); }
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_CumplimientoViajes_RegistrarEditarCumplimiento(1, Convert.ToInt32(cbxTipoViaje.SelectedValue), dtpFechaCumplimiento.Value,
                                                                                             Convert.ToInt32(txtViDisponible.Text), Convert.ToInt32(txtProyReq.Text), txtDetalle.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (OpcionTL == 1) { formulario.btnBuscar_Click(sender, e); }
                    if (OpcionTL == 2) { formulario.btnBuscar2_Click(sender, e); }

                    btnCancelar_Click(sender, e);
                    if (Editar == 1) { this.Close(); }
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
