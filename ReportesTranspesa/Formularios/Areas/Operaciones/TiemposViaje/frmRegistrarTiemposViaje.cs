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
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using ReportesTranspesa.Properties;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.TiemposViaje
{
    public partial class frmRegistrarTiemposViaje : Form
    {
        public int Opcion = 0, idRuta;
        public string EstadoV;
        public decimal Avance;
        DataTable dtFiltroTiempos = new DataTable();
        public frmListaTiemposViaje formulario;

        public frmRegistrarTiemposViaje()
        {
            InitializeComponent();
            //cbxUbicacion.SelectedIndexChanged -= cbxUbicacion_SelectedIndexChanged;
        }

        private void cbxUbicacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboUbicacion(); }

        private void frmRegistrarTiemposViaje_Shown(object sender, EventArgs e)
        {
            if (txtOperacion.Text == "LINDLEY") { dtpSalidaBase.Focus(); }
            else { dtpSalidaBase2.Focus(); }
        }

        private void frmRegistrarTiemposViaje_Load(object sender, EventArgs e)
        {
            FiltrarTiemposViajes(Convert.ToInt32(txtPreviaje.Text));
        }


        public void CargarComboUbicacion()
        {
            /*
            DataTable dtUbicacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarUbicaciones(1, idRuta);
            cbxUbicacion.DataSource = dtUbicacion;
            cbxUbicacion.DisplayMember = "Ubicacion";
            cbxUbicacion.ValueMember = "idUbicacion";
            */
        }

        public void FiltrarTiemposViajes(int Previaje)
        {
            dtFiltroTiempos = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_FiltrarTiemposViajes(Previaje);

            if (txtOperacion.Text == "LINDLEY")
            {
                groupBox4.Enabled = false;
                groupBox4.Visible = false;

                if (dtFiltroTiempos.Rows.Count > 0)
                {
                    for (int i = 0; i < dtFiltroTiempos.Rows.Count; i++)
                    {
                        dtpSalidaBase.Text = dtFiltroTiempos.Rows[i]["SALIDA_BASE"].ToString();
                        dtpHoraSalidaBase.Text = dtFiltroTiempos.Rows[i]["SALIDA_BASE"].ToString();
                        dtpLlegadaPlanta.Text = dtFiltroTiempos.Rows[i]["LLEGADA_PLANTA"].ToString();
                        dtpHoraLlegadaPlanta.Text = dtFiltroTiempos.Rows[i]["LLEGADA_PLANTA"].ToString();
                        dtpIngresoPlanta.Text = dtFiltroTiempos.Rows[i]["INGRESO_PLANTA"].ToString();
                        dtpHoraIngresoPlanta.Text = dtFiltroTiempos.Rows[i]["INGRESO_PLANTA"].ToString();
                        dtpInicioAtencion.Text = dtFiltroTiempos.Rows[i]["INICIO_ATENCION"].ToString();
                        dtpHoraInicioAtencion.Text = dtFiltroTiempos.Rows[i]["INICIO_ATENCION"].ToString();
                        dtpFinAtencion.Text = dtFiltroTiempos.Rows[i]["FIN_ATENCION"].ToString();
                        dtpHoraFinAtencion.Text = dtFiltroTiempos.Rows[i]["FIN_ATENCION"].ToString();
                        dtpEntregaGuia.Text = dtFiltroTiempos.Rows[i]["ENTREGA_GUIA"].ToString();
                        dtpHoraEntregaGuia.Text = dtFiltroTiempos.Rows[i]["ENTREGA_GUIA"].ToString();
                        dtpSalidaPlanta.Text = dtFiltroTiempos.Rows[i]["SALIDA_PLANTA"].ToString();
                        dtpHoraSalidaPlanta.Text = dtFiltroTiempos.Rows[i]["SALIDA_PLANTA"].ToString();

                        dtpSalidaRuta.Text = dtFiltroTiempos.Rows[i]["SALIDA_RUTA"].ToString();
                        dtpHoraSalidaRuta.Text = dtFiltroTiempos.Rows[i]["SALIDA_RUTA"].ToString();
                        dtpLlegadaCDA.Text = dtFiltroTiempos.Rows[i]["LLEGADA_CDA"].ToString();
                        dtpHoraLlegadaCDA.Text = dtFiltroTiempos.Rows[i]["LLEGADA_CDA"].ToString();
                        dtpInicioDescarga.Text = dtFiltroTiempos.Rows[i]["INICIO_DESCARGA"].ToString();
                        dtpHoraInicioDescarga.Text = dtFiltroTiempos.Rows[i]["INICIO_DESCARGA"].ToString();
                        dtpFinDescarga.Text = dtFiltroTiempos.Rows[i]["FIN_DESCARGA"].ToString();
                        dtpHoraFinDescarga.Text = dtFiltroTiempos.Rows[i]["FIN_DESCARGA"].ToString();
                        dtpLlegadaCDA2.Text = dtFiltroTiempos.Rows[i]["LLEGADA_CDA_2"].ToString();
                        dtpHoraLlegadaCDA2.Text = dtFiltroTiempos.Rows[i]["LLEGADA_CDA_2"].ToString();
                        dtpInicioDesc2.Text = dtFiltroTiempos.Rows[i]["INICIO_DESCARGA_2"].ToString();
                        dtpHoraInicioDesc2.Text = dtFiltroTiempos.Rows[i]["INICIO_DESCARGA_2"].ToString();
                        dtpFinDesc2.Text = dtFiltroTiempos.Rows[i]["FIN_DESCARGA_2"].ToString();
                        dtpHoraFinDesc2.Text = dtFiltroTiempos.Rows[i]["FIN_DESCARGA_2"].ToString();
                        dtpLlegadaBase.Text = dtFiltroTiempos.Rows[i]["LLEGADA_BASE"].ToString();
                        dtpHoraLlegadaBase.Text = dtFiltroTiempos.Rows[i]["LLEGADA_BASE"].ToString();

                        //cbxEstado.Text = dtFiltroTiempos.Rows[i]["ESTADO"].ToString();
                        //cbxUbicacion.Text = dtFiltroTiempos.Rows[i]["UBICACION"].ToString();
                        //lblporcentaje.Text = dtFiltroTiempos.Rows[i]["PORCENTAJE"].ToString();
                    }
                }

                if (txtRuta.Text.Contains("/"))
                {
                    dtpLlegadaCDA2.Enabled = true;
                    dtpHoraLlegadaCDA2.Enabled = true;
                    dtpInicioDesc2.Enabled = true;
                    dtpHoraInicioDesc2.Enabled = true;
                    dtpFinDesc2.Enabled = true;
                    dtpHoraFinDesc2.Enabled = true;
                }
                else
                {
                    dtpLlegadaCDA2.Enabled = false;
                    dtpHoraLlegadaCDA2.Enabled = false;
                    dtpInicioDesc2.Enabled = false;
                    dtpHoraInicioDesc2.Enabled = false;
                    dtpFinDesc2.Enabled = false;
                    dtpHoraFinDesc2.Enabled = false;
                }
            }
            else
            {
                groupBox4.Visible = true;
                groupBox4.Enabled = true;
                groupBox4.BringToFront();
                groupBox2.Enabled = false;
                groupBox2.Visible = false;

                tabSalidaDescarga.Enabled = false;

                if (dtFiltroTiempos.Rows.Count > 0)
                {
                    for (int i = 0; i < dtFiltroTiempos.Rows.Count; i++)
                    {
                        dtpSalidaBase2.Text = dtFiltroTiempos.Rows[i]["SALIDA_BASE"].ToString();
                        dtpHoraSalidaBase2.Text = dtFiltroTiempos.Rows[i]["SALIDA_BASE"].ToString();
                        dtpLlegadaCarga.Text = dtFiltroTiempos.Rows[i]["LLEGADA_CARGA"].ToString();
                        dtpHoraLlegadaCarga.Text = dtFiltroTiempos.Rows[i]["LLEGADA_CARGA"].ToString();
                        dtpCarga.Text = dtFiltroTiempos.Rows[i]["CARGA"].ToString();
                        dtpHoraCarga.Text = dtFiltroTiempos.Rows[i]["CARGA"].ToString();
                        dtpSalidaPlanta2.Text = dtFiltroTiempos.Rows[i]["SALIDA_PLANTA"].ToString();
                        dtpHoraSalidaPlanta2.Text = dtFiltroTiempos.Rows[i]["SALIDA_PLANTA"].ToString();
                        dtpLlegadaDescarga.Text = dtFiltroTiempos.Rows[i]["LLEGADA_DESCARGA"].ToString();
                        dtpHoraLlegadaDescarga.Text = dtFiltroTiempos.Rows[i]["LLEGADA_DESCARGA"].ToString();
                        dtpInicioDescarga2.Text = dtFiltroTiempos.Rows[i]["INICIO_DESCARGA"].ToString();
                        dtpHoraInicioDescarga2.Text = dtFiltroTiempos.Rows[i]["INICIO_DESCARGA"].ToString();
                        dtpSalidaDescarga.Text = dtFiltroTiempos.Rows[i]["SALIDA_DESCARGA"].ToString();
                        dtpHoraSalidaDescarga.Text = dtFiltroTiempos.Rows[i]["SALIDA_DESCARGA"].ToString();
                        dtpLlegadaBase2.Text = dtFiltroTiempos.Rows[i]["LLEGADA_BASE"].ToString();
                        dtpHoraLlegadaBase2.Text = dtFiltroTiempos.Rows[i]["LLEGADA_BASE"].ToString();

                        //cbxEstadoL.Text = dtFiltroTiempos.Rows[i]["ESTADO"].ToString();
                        //txtUbicacionL.Text = dtFiltroTiempos.Rows[i]["UBICACION"].ToString();
                    }
                }
            }
        }

        private void btnIngresarTiempos_Click(object sender, EventArgs e)
        {
            if (txtOperacion.Text == "LINDLEY")
            {
                /*
                if (cbxUbicacion.Text.Length == 0)
                {
                    MessageBox.Show("Por favor, ingrese la ubicación del viaje.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cbxUbicacion.Focus();
                    return;
                }
                */

                DataTable dtRegistrar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                DateTime SalidaBase = Convert.ToDateTime(dtpSalidaBase.Text + " " + dtpHoraSalidaBase.Text);
                DateTime LlegadaPlanta = Convert.ToDateTime(dtpLlegadaPlanta.Text + " " + dtpHoraLlegadaPlanta.Text);
                DateTime IngresoPlanta = Convert.ToDateTime(dtpIngresoPlanta.Text + " " + dtpHoraIngresoPlanta.Text);
                DateTime InicioAtencion = Convert.ToDateTime(dtpInicioAtencion.Text + " " + dtpHoraInicioAtencion.Text);
                DateTime FinAtencion = Convert.ToDateTime(dtpFinAtencion.Text + " " + dtpHoraFinAtencion.Text);
                DateTime EntregaGuia = Convert.ToDateTime(dtpEntregaGuia.Text + " " + dtpHoraEntregaGuia.Text);
                DateTime SalidaPlanta = Convert.ToDateTime(dtpSalidaPlanta.Text + " " + dtpHoraSalidaPlanta.Text);
                DateTime SalidaRuta = Convert.ToDateTime(dtpSalidaRuta.Text + " " + dtpHoraSalidaRuta.Text);
                DateTime LlegadaCDA = Convert.ToDateTime(dtpLlegadaCDA.Text + " " + dtpHoraLlegadaCDA.Text);
                DateTime InicioDescarga = Convert.ToDateTime(dtpInicioDescarga.Text + " " + dtpHoraInicioDescarga.Text);
                DateTime FinDescarga = Convert.ToDateTime(dtpFinDescarga.Text + " " + dtpHoraFinDescarga.Text);
                DateTime LlegadaCDA2;
                DateTime InicioDescarga2;
                DateTime FinDescarga2;

                if (dtpLlegadaCDA2.Text.Length != 0)
                {
                    LlegadaCDA2 = Convert.ToDateTime(dtpLlegadaCDA2.Text + " " + dtpHoraLlegadaCDA2.Text);
                    InicioDescarga2 = Convert.ToDateTime(dtpInicioDesc2.Text + " " + dtpHoraInicioDesc2.Text);
                    FinDescarga2 = Convert.ToDateTime(dtpFinDesc2.Text + " " + dtpHoraFinDesc2.Text);
                }
                else
                {
                    LlegadaCDA2 = DateTime.Now.Date;
                    InicioDescarga2 = DateTime.Now.Date;
                    FinDescarga2 = DateTime.Now.Date;
                }

                DateTime LlegadaBase = Convert.ToDateTime(dtpLlegadaBase.Text + " " + dtpHoraLlegadaBase.Text);

                dtRegistrar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajes(Opcion, Convert.ToInt32(txtPreviaje.Text), EstadoV, SalidaBase, LlegadaPlanta, IngresoPlanta, InicioAtencion,
                                               FinAtencion, EntregaGuia, SalidaPlanta, SalidaRuta, LlegadaCDA, InicioDescarga, FinDescarga, LlegadaCDA2, InicioDescarga2, FinDescarga2, LlegadaBase, Usuario);
                respta = Convert.ToString(dtRegistrar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (formulario != null)
                    {
                        formulario.Filtro = 1;
                        formulario.ListarTiemposViaje();
                    }
                    this.Close();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                DataTable dtRegistrar = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                DateTime SalidaBase = Convert.ToDateTime(dtpSalidaBase2.Text + " " + dtpHoraSalidaBase2.Text);
                DateTime LlegadaCarga = Convert.ToDateTime(dtpLlegadaCarga.Text + " " + dtpHoraLlegadaCarga.Text);
                DateTime Carga = Convert.ToDateTime(dtpCarga.Text + " " + dtpHoraCarga.Text);
                DateTime SalidaPlanta = Convert.ToDateTime(dtpSalidaPlanta2.Text + " " + dtpHoraSalidaPlanta2.Text);
                DateTime LlegadaDescarga = Convert.ToDateTime(dtpLlegadaDescarga.Text + " " + dtpHoraLlegadaDescarga.Text);
                DateTime InicioDescarga = Convert.ToDateTime(dtpInicioDescarga2.Text + " " + dtpHoraInicioDescarga2.Text);
                DateTime SalidaDescarga = Convert.ToDateTime(dtpSalidaDescarga.Text + " " + dtpHoraSalidaDescarga.Text);
                DateTime LlegadaBase = Convert.ToDateTime(dtpLlegadaBase2.Text + " " + dtpHoraLlegadaBase2.Text);

                dtRegistrar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_RegistrarEliminarTiempoViajesLimagas(Opcion, Convert.ToInt32(txtPreviaje.Text), EstadoV, SalidaBase, LlegadaCarga, Carga,
                                                         SalidaPlanta, LlegadaDescarga, InicioDescarga, SalidaDescarga, LlegadaBase, Usuario);
                respta = Convert.ToString(dtRegistrar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);

                if (NroRspta == "0")
                {
                    MessageBox.Show(respta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (formulario != null)
                    {
                        formulario.Filtro = 1;
                        formulario.ListarTiemposViaje();
                    }
                    this.Close();
                }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void cbxUbicacion_DropDownClosed(object sender, EventArgs e)
        {
            /*
            try
            {
                int idUbicacion = Convert.ToInt32(cbxUbicacion.SelectedValue);
                DataTable dtUbicacion = new DataTable();

                dtUbicacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarUbicaciones(2, idUbicacion);

                if (cbxEstado.Text == "EN TRÁNSITO")
                {
                    Avance = Convert.ToDecimal(dtUbicacion.Rows[0]["Avance"]);
                    lblporcentaje.Text = Convert.ToString(dtUbicacion.Rows[0]["Avance"]) + " %";
                }
                else
                {
                    Avance = Convert.ToDecimal("100");
                    lblporcentaje.Text = "100.00 %";
                }
            }
            catch { }
            */
        }

        private void cbxUbicacion_MouseClick(object sender, MouseEventArgs e)
        {
            /*
            try
            {
                int idUbicacion = Convert.ToInt32(cbxUbicacion.SelectedValue);
                DataTable dtUbicacion = new DataTable();

                dtUbicacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarUbicaciones(2, idUbicacion);
                
                if (cbxEstado.Text == "EN TRÁNSITO")
                {
                    Avance = Convert.ToDecimal(dtUbicacion.Rows[0]["Avance"]);
                    lblporcentaje.Text = Convert.ToString(dtUbicacion.Rows[0]["Avance"]) + " %";
                }
                else
                {
                    Avance = Convert.ToDecimal("100");
                    lblporcentaje.Text = "100.00 %";
                }
            }
            catch { }
            */
        }

        private void cbxUbicacion_KeyUp(object sender, KeyEventArgs e)
        {
            /*
            try
            {
                int idUbicacion = Convert.ToInt32(cbxUbicacion.SelectedValue);
                DataTable dtUbicacion = new DataTable();

                dtUbicacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarUbicaciones(2, idUbicacion);

                if (cbxEstado.Text == "EN TRÁNSITO")
                {
                    Avance = Convert.ToDecimal(dtUbicacion.Rows[0]["Avance"]);
                    lblporcentaje.Text = Convert.ToString(dtUbicacion.Rows[0]["Avance"]) + " %";
                }
                else
                {
                    Avance = Convert.ToDecimal("100");
                    lblporcentaje.Text = "100.00 %";
                }
            }
            catch { }
            */
        }

        private void cbxEstado_DropDownClosed(object sender, EventArgs e)
        {
            /*
            try
            {
                int idUbicacion = Convert.ToInt32(cbxUbicacion.SelectedValue);
                DataTable dtUbicacion = new DataTable();

                dtUbicacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Previajes_ListarUbicaciones(2, idUbicacion);

                if (cbxEstado.Text == "EN TRÁNSITO")
                {
                    Avance = Convert.ToDecimal(dtUbicacion.Rows[0]["Avance"]);
                    lblporcentaje.Text = Convert.ToString(dtUbicacion.Rows[0]["Avance"]) + " %";
                }
                else
                {
                    Avance = Convert.ToDecimal("100");
                    lblporcentaje.Text = "100.00 %";
                }

                dtpSalidaBase.Focus();
            }
            catch { }
            */ 
        }

        private void txtUbicacionL_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxEstadoL.Focus(); }
        }

        private void cbxEstadoL_DropDownClosed(object sender, EventArgs e) { /*dtpSalidaBase2.Focus();*/ }

        private void dtpSalidaBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraSalidaBase.Focus(); }
        }

        private void dtpHoraSalidaBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpLlegadaPlanta.Focus(); }
        }

        private void dtpLlegadaPlanta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraLlegadaPlanta.Focus(); }
        }

        private void dtpHoraLlegadaPlanta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpIngresoPlanta.Focus(); }
        }

        private void dtpIngresoPlanta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraIngresoPlanta.Focus(); }
        }

        private void dtpHoraIngresoPlanta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpInicioAtencion.Focus(); }
        }

        private void dtpInicioAtencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraInicioAtencion.Focus(); }
        }

        private void dtpHoraInicioAtencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFinAtencion.Focus(); }
        }

        private void dtpFinAtencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraFinAtencion.Focus(); }
        }

        private void dtpHoraFinAtencion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpEntregaGuia.Focus(); }
        }

        private void dtpEntregaGuia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraEntregaGuia.Focus(); }
        }

        private void dtpHoraEntregaGuia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpSalidaPlanta.Focus(); }
        }

        private void dtpSalidaPlanta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraSalidaPlanta.Focus(); }
        }

        private void dtpHoraSalidaPlanta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                dtpSalidaRuta.Focus();
                dtpLlegadaCDA2.Focus();
            }
        }

        private void dtpSalidaRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraSalidaRuta.Focus(); }
        }

        private void dtpHoraSalidaRuta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpLlegadaCDA.Focus(); }
        }

        private void dtpLlegadaCDA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraLlegadaCDA.Focus(); }
        }

        private void dtpHoraLlegadaCDA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpInicioDescarga.Focus(); }
        }

        private void dtpInicioDescarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraInicioDescarga.Focus(); }
        }

        private void dtpHoraInicioDescarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFinDescarga.Focus(); }
        }

        private void dtpFinDescarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraFinDescarga.Focus(); }
        }

        private void dtpHoraFinDescarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpLlegadaBase.Focus(); }
        }

        private void dtpLlegadaBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraLlegadaBase.Focus(); }
        }

        private void dtpHoraLlegadaBase_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnIngresarTiempos.Focus(); }
        }

        private void dtpLlegadaCDA2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraLlegadaCDA2.Focus(); }
        }

        private void dtpHoraLlegadaCDA2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpInicioDesc2.Focus(); }
        }

        private void dtpInicioDesc2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraInicioDesc2.Focus(); }
        }

        private void dtpHoraInicioDesc2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpFinDesc2.Focus(); }
        }

        private void dtpFinDesc2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraFinDesc2.Focus(); }
        }

        private void dtpHoraFinDesc2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnIngresarTiempos.Focus(); }
        }

        private void dtpSalidaBase2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraSalidaBase2.Focus(); }
        }

        private void dtpHoraSalidaBase2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpLlegadaCarga.Focus(); }
        }

        private void dtpLlegadaCarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraLlegadaCarga.Focus(); }
        }

        private void dtpHoraLlegadaCarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpCarga.Focus(); }
        }

        private void dtpCarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraCarga.Focus(); }
        }

        private void dtpHoraCarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpSalidaPlanta2.Focus(); }
        }

        private void dtpSalidaPlanta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraSalidaPlanta2.Focus(); }
        }

        private void dtpHoraSalidaPlanta2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpLlegadaDescarga.Focus(); }
        }

        private void dtpLlegadaDescarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraLlegadaDescarga.Focus(); }
        }

        private void dtpHoraLlegadaDescarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpInicioDescarga2.Focus(); }
        }

        private void dtpInicioDescarga2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraInicioDescarga2.Focus(); }
        }

        private void dtpHoraInicioDescarga2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpSalidaDescarga.Focus(); }
        }

        private void dtpSalidaDescarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraSalidaDescarga.Focus(); }
        }

        private void dtpHoraSalidaDescarga_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpLlegadaBase2.Focus(); }
        }

        private void dtpLlegadaBase2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraLlegadaBase2.Focus(); }
        }

        private void dtpHoraLlegadaBase2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnIngresarTiempos.Focus(); }
        }
    }
}
