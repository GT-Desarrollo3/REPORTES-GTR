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

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.MovimientoComponentes
{
    public partial class frmNuevoMovimiento : Form
    {
        public int idSistema, idVehiculoO, idVehiculoD, Persona, Opcion, idMovimientoC;
        public frmRegistroMovimientos formulario;
        
        public frmNuevoMovimiento()
        {
            InitializeComponent();
            cbxSistema.SelectedIndexChanged -= cbxSistema_SelectedIndexChanged;
            cbxSubSistema.SelectedIndexChanged -= cbxSubSistema_SelectedIndexChanged;
        }

        public void cbxSistema_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSistema(); }

        private void cbxSubSistema_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSubSistema(); }

        private void frmNuevoMovimiento_Load(object sender, EventArgs e)
        {
            dtpFechaEjecucion.CustomFormat = "dd/MM/yyyy HH:mm:ss";
        }


        public void CargarComboSistema()
        {
            DataTable dtSistema = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSistemasVehiculos();
            cbxSistema.DataSource = dtSistema;
            cbxSistema.DisplayMember = "Descripcion";
            cbxSistema.ValueMember = "idSistemaVehiculo";
        }

        public void CargarComboSubSistema()
        {
            DataTable dtSubSistema = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSubSistemasVehiculos(idSistema);
            cbxSubSistema.DataSource = dtSubSistema;
            cbxSubSistema.DisplayMember = "Descripcion";
            cbxSubSistema.ValueMember = "idSubSistema";
        }


        private void cbxSistema_DropDownClosed(object sender, EventArgs e)
        {
            idSistema = Convert.ToInt32(cbxSistema.SelectedValue);
            CargarComboSubSistema();
        }

        private void txtPlacaProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca1, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtPlacaProd.Text), true, false, false);
            lstPlaca1.Columns[0].Width = 0;
            lstPlaca1.Columns[1].Width = 80;
            lstPlaca1.Columns[2].Width = 0;
            lstPlaca1.Columns[3].Width = 0;
            lstPlaca1.Columns[4].Width = 95;
            lstPlaca1.Columns[5].Width = 0;
            lstPlaca1.BringToFront();
            lstPlaca1.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculoO = -1;
                lstPlaca1.Visible = false;
                lstPlaca1.SendToBack();
            }
        }

        private void txtPlacaProd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca1.Focus(); }
        }

        private void lstPlaca1_Enter(object sender, EventArgs e)
        {
            if (!lstPlaca1.Items.Count.Equals(0)) { lstPlaca1.Items[0].Selected = true; }
        }

        private void lstPlaca1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlaca1.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlaca1.SelectedItems[0];

                idVehiculoO = Int32.Parse(ItemActual.Text);
                txtPlacaProd.Text = ItemActual.SubItems[1].Text;
                txtPlacaDest.Focus();

                lstPlaca1.Visible = false;
                lstPlaca1.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculoO = -1;
                lstPlaca1.Visible = false;
                lstPlaca1.SendToBack();
            }
        }

        private void lstPlaca1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca1.SelectedItems[0];

            idVehiculoO = Int32.Parse(ItemActual.Text);
            txtPlacaProd.Text = ItemActual.SubItems[1].Text;
            txtPlacaDest.Focus();

            lstPlaca1.Visible = false;
            lstPlaca1.SendToBack();
        }

        private void txtPlacaDest_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPlaca2, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidades(txtPlacaDest.Text), true, false, false);
            lstPlaca2.Columns[0].Width = 0;
            lstPlaca2.Columns[1].Width = 80;
            lstPlaca2.Columns[2].Width = 0;
            lstPlaca2.Columns[3].Width = 0;
            lstPlaca2.Columns[4].Width = 95;
            lstPlaca2.Columns[5].Width = 0;
            lstPlaca2.BringToFront();
            lstPlaca2.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculoD = -1;
                lstPlaca2.Visible = false;
                lstPlaca2.SendToBack();
            }
        }

        private void txtPlacaDest_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPlaca2.Focus(); }
        }

        private void lstPlaca2_Enter(object sender, EventArgs e)
        {
            if (!lstPlaca2.Items.Count.Equals(0)) { lstPlaca2.Items[0].Selected = true; }
        }

        private void lstPlaca2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPlaca2.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPlaca2.SelectedItems[0];

                idVehiculoD = Int32.Parse(ItemActual.Text);
                txtPlacaDest.Text = ItemActual.SubItems[1].Text;
                dtpFechaEjecucion.Focus();

                lstPlaca2.Visible = false;
                lstPlaca2.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                idVehiculoD = -1;
                lstPlaca2.Visible = false;
                lstPlaca2.SendToBack();
            }
        }

        private void lstPlaca2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPlaca2.SelectedItems[0];

            idVehiculoD = Int32.Parse(ItemActual.Text);
            txtPlacaDest.Text = ItemActual.SubItems[1].Text;
            dtpFechaEjecucion.Focus();

            lstPlaca2.Visible = false;
            lstPlaca2.SendToBack();
        }

        private void txtNroReq_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstRequerimiento, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_AsignacionOT_ListarRequerimientos(txtNroReq.Text), true, false, false);
            lstRequerimiento.Columns[0].Width = 120;
            lstRequerimiento.Columns[1].Width = 250;
            lstRequerimiento.Columns[2].Width = 0;
            lstRequerimiento.Columns[3].Width = 0;
            lstRequerimiento.Columns[4].Width = 0;
            lstRequerimiento.Columns[5].Width = 0;
            lstRequerimiento.BringToFront();
            lstRequerimiento.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                txtDescipcionR.Clear();
                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();
            }

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }
        }

        private void txtNroReq_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstRequerimiento.Focus(); }
        }

        private void lstRequerimiento_Enter(object sender, EventArgs e)
        {
            if (!lstRequerimiento.Items.Count.Equals(0)) { lstRequerimiento.Items[0].Selected = true; }
        }

        private void lstRequerimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstRequerimiento.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstRequerimiento.SelectedItems[0];
                txtNroReq.Text = ItemActual.SubItems[0].Text;
                txtDescipcionR.Text = ItemActual.SubItems[1].Text;

                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();
                txtNombre.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                txtDescipcionR.Clear();
                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();
            }
        }

        private void lstRequerimiento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstRequerimiento.SelectedItems[0];
            txtNroReq.Text = ItemActual.SubItems[0].Text;
            txtDescipcionR.Text = ItemActual.SubItems[1].Text;

            lstRequerimiento.Visible = false;
            lstRequerimiento.SendToBack();
            txtNombre.Focus();
        }

        private void dtpFechaEjecucion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) { txtNroReq.Focus(); }
        }

        private void txtPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal, clsSeguridadBL.Instancia.ReportesApp_Seguridad_GestionSeguridad_ListarPersonal(1, txtNombre.Text), true, false, false);
            lstPersonal.Columns[0].Width = 0;
            lstPersonal.Columns[1].Width = 400;
            lstPersonal.Columns[2].Width = 0;
            lstPersonal.Columns[3].Width = 0;
            lstPersonal.Columns[4].Width = 0;
            lstPersonal.BringToFront();
            lstPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void txtPersona_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal.Focus(); }
        }

        private void lstPersonal_Enter(object sender, EventArgs e)
        {
            if (!lstPersonal.Items.Count.Equals(0)) { lstPersonal.Items[0].Selected = true; }
        }

        private void lstPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersonal.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersonal.SelectedItems[0];

                Persona = Int32.Parse(ItemActual.Text);
                txtNombre.Text = ItemActual.SubItems[1].Text;

                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
                txtMotivo.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona = -1;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void lstPersonal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersonal.SelectedItems[0];

            Persona = Int32.Parse(ItemActual.Text);
            txtNombre.Text = ItemActual.SubItems[1].Text;

            lstPersonal.Visible = false;
            lstPersonal.SendToBack();
            txtMotivo.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            idSistema = 3;
            CargarComboSistema();
            CargarComboSubSistema();
            txtComponente.Clear();

            idVehiculoO = -1;
            txtPlacaProd.Clear();
            idVehiculoD = -1;
            txtPlacaDest.Clear();
            dtpFechaEjecucion.Value = DateTime.Now;
            txtNroReq.Clear();
            txtDescipcionR.Clear();
            Persona = -1;
            txtNombre.Clear();
            txtMotivo.Clear();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtComponente.Text.Length == 0 || txtPlacaProd.Text.Length == 0 || txtPlacaDest.Text.Length == 0 || txtNombre.Text.Length == 0 || txtMotivo.Text.Length == 0)
            {
                MessageBox.Show("Los datos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (txtComponente.Text.Length == 0) { txtComponente.Focus(); }
                else
                {
                    if (txtPlacaProd.Text.Length == 0) { txtPlacaProd.Focus(); }
                    else
                    {
                        if (txtPlacaDest.Text.Length == 0) { txtPlacaDest.Focus(); }
                        else
                        {
                            if (txtNombre.Text.Length == 0) { txtNombre.Focus(); }
                            else { txtMotivo.Focus(); }
                        }
                    }
                }

                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MovimientoC_RegistrarEditarComponentes(Opcion, idMovimientoC, Convert.ToInt32(cbxSistema.SelectedValue), Convert.ToInt32(cbxSubSistema.SelectedValue),
                                                           txtComponente.Text, idVehiculoO, idVehiculoD, dtpFechaEjecucion.Value, txtMotivo.Text, Persona, txtNroReq.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    formulario.ListarMovimientos();
                    this.Close();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
