using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using DevExpress.Data;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Globalization;
using System.Diagnostics;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Sistemas
{
    public partial class frmInventarioEquipos : Form
    {
        public frmInventarioEquipos()
        {
            InitializeComponent();
        }

        private void frmInventarioEquipos_Load(object sender, EventArgs e)
        {
            CargarCategorias();
            CargarRegistros();
        }
        void CargarRegistros()
        {
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.GetGestionEquipos_Listar();
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["IDEquipo"].Visible = false;
                dtgvDataView.Columns["IDCategoria"].Visible = false;
                dtgvDataView.BestFitColumns();
                //toolStripLabel2.Text = "Herramientas Entregadas: " + dt.Rows.Count.ToString();
            }
            else
            {
                dtgvData.DataSource = null;
            }
        }

        void CargarCategorias()
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsSistemasBL.Instancia.GetGestionEquipos_CategoriasListar();
            if (dtRespuesta.Rows.Count > 0)
            {
                cbxCategoria.DataSource = dtRespuesta;
                cbxCategoria.ValueMember = "IDCategoria";
                cbxCategoria.DisplayMember = "Descripcion";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HabilitarControles();
            SetearControles();
            button3.Enabled = true;
            button6.Visible = true;
            txtDescripcion.Focus();
        }
        void HabilitarControles()
        {
            cbxCategoria.Enabled = true;
            txtDescripcion.Enabled = true;
            txtMarca.Enabled = true;
            txtModelo.Enabled = true;
            txtProcesador.Enabled = true;
            txtPlaca.Enabled = true;
            txtMemoria.Enabled = true;
            txtDiscoDuro.Enabled = true;
            txtCase.Enabled = true;
            txtLector.Enabled = true;
            txtMonitor.Enabled = true;
            txtTeclado.Enabled = true;
            txtMouse.Enabled = true;
            txtSistemaOperativo.Enabled = true;
            txtUsuarioWindows.Enabled = true;
            txtClaveWindows.Enabled = true;
            txtOtros.Enabled = true;
            txtIp.Enabled = true;
            txtAnyDesk.Enabled = true;
            txtHost.Enabled = true;
            txtMac.Enabled = true;
            txtPersona.Enabled = true;
            txtAnydeskClave.Enabled = true;
            txtArea.Enabled = true;

        }
        void SetearControles()
        {
            txtDescripcion.Text = "";
            txtMarca.Text = "";
            txtModelo.Text = "";
            txtProcesador.Text = "";
            txtPlaca.Text = "";
            txtMemoria.Text = "";
            txtDiscoDuro.Text = "";
            txtCase.Text = "";
            txtLector.Text = "";
            txtMonitor.Text = "";
            txtTeclado.Text = "";
            txtMouse.Text = "";
            txtSistemaOperativo.Text = "";
            txtUsuarioWindows.Text = "";
            txtClaveWindows.Text = "";
            txtOtros.Text = "";
            txtIp.Text = "";
            txtAnyDesk.Text = "";
            txtHost.Text = "";
            txtMac.Text = "";
            txtPersona.Text = "";
            txtAnydeskClave.Text = "";
            txtArea.Text = "";
        }
        void DesabilitarControles()
        {
            cbxCategoria.Enabled = false;
            txtDescripcion.Enabled = false;
            txtMarca.Enabled = false;
            txtModelo.Enabled = false;
            txtProcesador.Enabled = false;
            txtPlaca.Enabled = false;
            txtMemoria.Enabled = false;
            txtDiscoDuro.Enabled = false;
            txtCase.Enabled = false;
            txtLector.Enabled = false;
            txtMonitor.Enabled = false;
            txtTeclado.Enabled = false;
            txtMouse.Enabled = false;
            txtSistemaOperativo.Enabled = false;
            txtUsuarioWindows.Enabled = false;
            txtClaveWindows.Enabled = false;
            txtOtros.Enabled = false;
            txtIp.Enabled = false;
            txtAnyDesk.Enabled = false;
            txtHost.Enabled = false;
            txtMac.Enabled = false;
            txtPersona.Enabled = false;
            txtAnydeskClave.Enabled = false;
            txtArea.Enabled = false;
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDescripcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtMarca.Focus();
            }
        }

        private void txtMarca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtModelo.Focus();
            }
        }

        private void txtModelo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtProcesador.Focus();
            }
        }

        private void txtProcesador_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtPlaca.Focus();
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtMemoria.Focus();
            }
        }

        private void txtMemoria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtDiscoDuro.Focus();
            }
        }

        private void txtDiscoDuro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtCase.Focus();
            }
        }

        private void txtCase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtLector.Focus();
            }
        }

        private void txtLector_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtMonitor.Focus();
            }
        }

        private void txtMonitor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtTeclado.Focus();
            }
        }

        private void txtTeclado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtMouse.Focus();
            }
        }

        private void txtMouse_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtSistemaOperativo.Focus();
            }
        }

        private void txtSistemaOperativo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtUsuarioWindows.Focus();
            }
        }

        private void txtUsuarioWindows_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtClaveWindows.Focus();
            }
        }

        private void txtClaveWindows_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtOtros.Focus();
            }
        }

        private void txtOtros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtIp.Focus();
            }
        }

        private void txtIp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtHost.Focus();
            }
        }

        private void txtHost_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtPersona.Focus();
            }
        }

        private void txtPersona_TextChanged(object sender, EventArgs e)
        {
            int length = txtPersona.Text.Length;
            if (length == 0)
            {
                txtPersona.Tag = -1;
            }
        }

        private void txtPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lvEmpleado, clsConsultaBL.Instancia.GetPersona(txtPersona.Text), true, false, false);

                lvEmpleado.Columns[0].Width = 0;
                lvEmpleado.Columns[1].Width = 206;
                lvEmpleado.Columns[2].Width = 110;
                lvEmpleado.Size = new Size(343, 64);

                lvEmpleado.BringToFront();
                lvEmpleado.Visible = true;
                lvEmpleado.Focus();
                //splitContainer1.SplitterDistance = lvEmpleado.Top + lvEmpleado.Height + 10;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvEmpleado.Visible = false;
                lvEmpleado.Size = new Size(343, 10);
                txtPersona.Focus();

                //buscar(0);
            }
        }

        private void lvEmpleado_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvEmpleado_Enter(object sender, EventArgs e)
        {
            if (!lvEmpleado.Items.Count.Equals(0))
            {
                lvEmpleado.Items[0].Selected = true;
            }
        }

        private void lvEmpleado_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvEmpleado.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvEmpleado.SelectedItems[0];

                txtPersona.Text = ItemActual.SubItems[1].Text;
                txtPersona.Tag = Convert.ToInt32(ItemActual.Text);
               
                lvEmpleado.Visible = false;
                txtPersona.Focus();
                
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvEmpleado.Visible = false;
                txtPersona.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int IDCategoria;
            string Descripcion;
            string Marca;
            string Modelo;
            string Procesador;
            string Placa;
            string Memoria;
            string DiscoDuro;
            string Case;
            string Lector;
            string Monitor;
            string Teclado;
            string Mouse;
            string SistemaOP;
            string UsuarioWin;
            string ClaveWin;
            string Otros;
            string Ip;
            string Hostname;
            string Mac;
            int Persona;
            string AnyDesk_Nro;
            string AnyDesk_Pass;
            string Area;

            IDCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
            Descripcion = txtDescripcion.Text;
            Marca = txtMarca.Text;
            Modelo = txtModelo.Text;
            Procesador = txtProcesador.Text;
            Placa = txtPlaca.Text;
            Memoria = txtMemoria.Text;
            DiscoDuro = txtDiscoDuro.Text;
            Case = txtCase.Text;
            Lector = txtLector.Text;
            Monitor = txtMonitor.Text;
            Teclado = txtTeclado.Text;
            Mouse = txtMouse.Text;
            Otros = txtOtros.Text;
            Ip = txtIp.Text;
            Hostname = txtHost.Text;
            Mac = txtMac.Text;
            SistemaOP = txtSistemaOperativo.Text;
            UsuarioWin = txtUsuarioWindows.Text;
            ClaveWin = txtClaveWindows.Text;
            AnyDesk_Nro = txtAnyDesk.Text;
            AnyDesk_Pass = txtAnydeskClave.Text;
            Persona = Convert.ToInt32(txtPersona.Tag);
            Area = txtArea.Text;

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsSistemasBL.Instancia.GetGestionEquipos_Registra_Modifica_Elimina(1, 0, IDCategoria, Descripcion, Marca, Modelo, Procesador,
               Placa, Memoria, DiscoDuro, Case, Lector, Monitor, Teclado, Mouse, Otros, Ip, Hostname,Mac,
               SistemaOP, UsuarioWin, ClaveWin, AnyDesk_Nro, AnyDesk_Pass, Persona, Area, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DesabilitarControles();
                CargarRegistros();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtAnyDesk_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtAnydeskClave.Focus();
            }
        }

        private void txtAnydeskClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                txtArea.Focus();
            }
        }
        int IDEquipoSelec = 0;
        private void dtgvData_Click(object sender, EventArgs e)
        {
            if (dtgvDataView.RowCount > 0)
            {
                IDEquipoSelec = Convert.ToInt32((dtgvDataView.GetFocusedRowCellValue("IDEquipo").ToString()).Trim());
                cbxCategoria.SelectedValue = Convert.ToInt32((dtgvDataView.GetFocusedRowCellValue("IDCategoria").ToString()).Trim());
                txtDescripcion.Text = (dtgvDataView.GetFocusedRowCellValue("Descripcion").ToString()).Trim();
                txtMarca.Text = (dtgvDataView.GetFocusedRowCellValue("Marca").ToString()).Trim();
                txtModelo.Text = (dtgvDataView.GetFocusedRowCellValue("Modelo").ToString()).Trim();
                txtProcesador.Text = (dtgvDataView.GetFocusedRowCellValue("Procesador").ToString()).Trim();
                txtPlaca.Text = (dtgvDataView.GetFocusedRowCellValue("Placa").ToString()).Trim();
                txtMemoria.Text = (dtgvDataView.GetFocusedRowCellValue("Memoria").ToString()).Trim();
                txtDiscoDuro.Text = (dtgvDataView.GetFocusedRowCellValue("Disco").ToString()).Trim();
                txtCase.Text = (dtgvDataView.GetFocusedRowCellValue("Case").ToString()).Trim();
                txtLector.Text = (dtgvDataView.GetFocusedRowCellValue("Lector").ToString()).Trim();
                txtMonitor.Text = (dtgvDataView.GetFocusedRowCellValue("Monitor").ToString()).Trim();
                txtTeclado.Text = (dtgvDataView.GetFocusedRowCellValue("Teclado").ToString()).Trim();
                txtMouse.Text = (dtgvDataView.GetFocusedRowCellValue("Mouse").ToString()).Trim();
                txtOtros.Text = (dtgvDataView.GetFocusedRowCellValue("Otros").ToString()).Trim();
                txtIp.Text = (dtgvDataView.GetFocusedRowCellValue("IP").ToString()).Trim();
                txtHost.Text = (dtgvDataView.GetFocusedRowCellValue("Hostname").ToString()).Trim();
                txtMac.Text = (dtgvDataView.GetFocusedRowCellValue("Mac").ToString()).Trim();
                txtSistemaOperativo.Text = (dtgvDataView.GetFocusedRowCellValue("SistemaOP").ToString()).Trim();
                txtUsuarioWindows.Text = (dtgvDataView.GetFocusedRowCellValue("UsuarioWin").ToString()).Trim();
                txtClaveWindows.Text = (dtgvDataView.GetFocusedRowCellValue("ClaveWin").ToString()).Trim();
                txtAnyDesk.Text = (dtgvDataView.GetFocusedRowCellValue("AnyDesk_Nro").ToString()).Trim();
                txtAnydeskClave.Text = (dtgvDataView.GetFocusedRowCellValue("AnyDesk_Pass").ToString()).Trim();
                txtPersona.Tag = (dtgvDataView.GetFocusedRowCellValue("Persona").ToString()).Trim();
                txtPersona.Text = (dtgvDataView.GetFocusedRowCellValue("Trabajador").ToString()).Trim();
                txtArea.Text = (dtgvDataView.GetFocusedRowCellValue("Area").ToString()).Trim();

                button2.Enabled = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            HabilitarControles();
            button5.Visible = true;
            button6.Visible = true;
            txtDescripcion.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int IDCategoria;
            string Descripcion;
            string Marca;
            string Modelo;
            string Procesador;
            string Placa;
            string Memoria;
            string DiscoDuro;
            string Case;
            string Lector;
            string Monitor;
            string Teclado;
            string Mouse;
            string SistemaOP;
            string UsuarioWin;
            string ClaveWin;
            string Otros;
            string Ip;
            string Hostname;
            string Mac;
            int Persona;
            string AnyDesk_Nro;
            string AnyDesk_Pass;
            string Area;

            IDCategoria = Convert.ToInt32(cbxCategoria.SelectedValue);
            Descripcion = txtDescripcion.Text;
            Marca = txtMarca.Text;
            Modelo = txtModelo.Text;
            Procesador = txtProcesador.Text;
            Placa = txtPlaca.Text;
            Memoria = txtMemoria.Text;
            DiscoDuro = txtDiscoDuro.Text;
            Case = txtCase.Text;
            Lector = txtLector.Text;
            Monitor = txtMonitor.Text;
            Teclado = txtTeclado.Text;
            Mouse = txtMouse.Text;
            Otros = txtOtros.Text;
            Ip = txtIp.Text;
            Hostname = txtHost.Text;
            Mac = txtMac.Text;
            SistemaOP = txtSistemaOperativo.Text;
            UsuarioWin = txtUsuarioWindows.Text;
            ClaveWin = txtClaveWindows.Text;
            AnyDesk_Nro = txtAnyDesk.Text;
            AnyDesk_Pass = txtAnydeskClave.Text;
            Persona = Convert.ToInt32(txtPersona.Tag);
            Area = txtArea.Text;

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsSistemasBL.Instancia.GetGestionEquipos_Registra_Modifica_Elimina(2, IDEquipoSelec, IDCategoria, Descripcion, Marca, Modelo, Procesador,
               Placa, Memoria, DiscoDuro, Case, Lector, Monitor, Teclado, Mouse, Otros, Ip, Hostname,Mac,
               SistemaOP, UsuarioWin, ClaveWin, AnyDesk_Nro, AnyDesk_Pass, Persona, Area, Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DesabilitarControles();
                CargarRegistros();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
            button5.Visible = false;
            button6.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsSistemasBL.Instancia.GetGestionEquipos_Registra_Modifica_Elimina(3, IDEquipoSelec, 0, "", "", "", "","", "", "", "", "", "", "", "", "", "","", "","", "", "", "", "", 0,"", Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DesabilitarControles();
                CargarRegistros();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
