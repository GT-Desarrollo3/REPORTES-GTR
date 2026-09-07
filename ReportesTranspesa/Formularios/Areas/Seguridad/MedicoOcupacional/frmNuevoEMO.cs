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

using System.Xml;
using System.IO;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Seguridad.MedicoOcupacional
{
    public partial class frmNuevoEMO : Form
    {
        public int Persona = 0, Opcion;
        public frmListaEMO Formulario;
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();        
        int e1 = 0;

        public frmNuevoEMO()
        {
            InitializeComponent();
        }

        private void frmNuevoEMO_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaEMO");

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Resultados Examenes")
                    {
                        groupBox3.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else { groupBox3.Enabled = false; }
                }
            }
            else { groupBox3.Enabled = false; }
        }


        private void txtPersonal_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersona, clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroEMO_ListarPersonal(txtPersonal.Text), true, false, false);
            lstPersona.Columns[0].Width = 0;
            lstPersona.Columns[1].Width = 400;
            lstPersona.Columns[2].Width = 0;
            lstPersona.Columns[3].Width = 0;
            lstPersona.Columns[4].Width = 0;
            lstPersona.BringToFront();
            lstPersona.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPersona.Visible = false;
                txtArea.Clear();
                txtPuesto.Clear();
                Persona = 0;
            }
        }

        private void txtPersonal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersona.Focus(); }
        }

        private void lstPersona_Enter(object sender, EventArgs e)
        {
            if (!lstPersona.Items.Count.Equals(0)) { lstPersona.Items[0].Selected = true; }
        }

        private void lstPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstPersona.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstPersona.SelectedItems[0];
                Persona = Int32.Parse(ItemActual.Text);
                txtPersonal.Text = ItemActual.SubItems[1].Text;
                txtDNI.Text = ItemActual.SubItems[2].Text;
                txtArea.Text = ItemActual.SubItems[3].Text;
                txtPuesto.Text = ItemActual.SubItems[4].Text;
                lstPersona.Visible = false;
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstPersona.Visible = false;
                txtPersonal.Focus();
            }
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersona.SelectedItems[0];
            Persona = Int32.Parse(ItemActual.Text);
            txtPersonal.Text = ItemActual.SubItems[1].Text;
            txtDNI.Text = ItemActual.SubItems[2].Text;
            txtArea.Text = ItemActual.SubItems[3].Text;
            txtPuesto.Text = ItemActual.SubItems[4].Text;
            lstPersona.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Persona = 0;
            txtPersonal.Clear();
            txtArea.Clear();
            txtPuesto.Clear();
            txtTipoEnfermedad.Clear();
            cbxCategoria.Text = "APTO";
            dtpFechaIni.Value = DateTime.Now;
            dtpFechaFin.Value = DateTime.Now.Date.AddYears(1);
            btnCerrarLocal_Click(sender, e);
            btnCerrarResultado_Click(sender, e);
            btnCerrarResultado2_Click(sender, e);
            btnCerrarResultado3_Click(sender, e);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (dtpFechaIni.Value >= dtpFechaFin.Value)
            {
                MessageBox.Show("La Fecha Inicial debe ser menor que la Fecha Fin.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                dtpFechaIni.Focus();
                return;
            }
            else
            {
                if (txtPersonal.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, escriba el nombre de un empleado.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPersonal.Focus();
                }
                else
                {
                    if (txtResultados.Text.Length != 0 || txtResultados2.Text.Length != 0 || txtResultados3.Text.Length != 0)
                    {
                        DataTable dtAgregar = new DataTable();
                        string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtAgregar = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroEMO_RegistrarEMOResultado(Opcion, Persona, cbxTipoSangre.Text,
                                                             txtTipoEnfermedad.Text, dtpFechaIni.Value, dtpFechaFin.Value, cbxCategoria.Text, Usuario, 
                                                             txtRutaLocal.Text, txtResultados.Text, txtResultados2.Text, txtResultados3.Text);
                        respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Formulario.ListarEMO();
                            this.Close();
                        }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else
                    {
                        DataTable dtAgregar = new DataTable();
                        string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                        dtAgregar = clsSeguridadBL.Instancia.ReportesApp_Seguridad_RegistroEMO_RegistrarEMO(Opcion, Persona, cbxTipoSangre.Text, txtTipoEnfermedad.Text,
                                                             dtpFechaIni.Value, dtpFechaFin.Value, cbxCategoria.Text, Usuario, txtRutaLocal.Text);
                        respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Formulario.ListarEMO();
                            this.Close();
                        }
                        else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                }
            }
        }

        private void txtRutaLocal_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try { System.Diagnostics.Process.Start(e.LinkText); }
            catch { MessageBox.Show("No se puede abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevoEnlace;
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        nuevoEnlace = op.FileName.Replace(" ", "%20");
                        txtRutaLocal.Clear();
                        txtRutaLocal.Text = "file:///" + nuevoEnlace;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarLocal_Click(object sender, EventArgs e) { txtRutaLocal.Clear(); }

        private void txtResultados_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try { System.Diagnostics.Process.Start(e.LinkText); }
            catch { MessageBox.Show("No se puede abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar2_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevoEnlace;
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        nuevoEnlace = op.FileName.Replace(" ", "%20");
                        txtResultados.Clear();
                        txtResultados.Text = "file:///" + nuevoEnlace;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarResultado_Click(object sender, EventArgs e) { txtResultados.Clear(); }

        private void txtResultados2_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try { System.Diagnostics.Process.Start(e.LinkText); }
            catch { MessageBox.Show("No se puede abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar3_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevoEnlace;
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        nuevoEnlace = op.FileName.Replace(" ", "%20");
                        txtResultados2.Clear();
                        txtResultados2.Text = "file:///" + nuevoEnlace;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarResultado2_Click(object sender, EventArgs e) { txtResultados2.Clear(); }

        private void txtResultados3_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            try { System.Diagnostics.Process.Start(e.LinkText); }
            catch { MessageBox.Show("No se puede abrir el archivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnBuscar4_Click(object sender, EventArgs e)
        {
            try
            {
                string nuevoEnlace;
                OpenFileDialog op = new OpenFileDialog();

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        nuevoEnlace = op.FileName.Replace(" ", "%20");
                        txtResultados3.Clear();
                        txtResultados3.Text = "file:///" + nuevoEnlace;
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void btnCerrarResultado3_Click(object sender, EventArgs e) { txtResultados3.Clear(); }
    }
}
