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
using Comun;
using Entidades;

namespace ReportesTranspesa.Sistema
{
    public partial class AgendarReuniones : Form
    {
        DataSet ds;
        DataTable dtOficina;
        DataTable dtEquipo;
        DataTable dtSalas;
        DataTable dtPermisos;


        // entidad agendar reuniones

        public AgendarReuniones()
        {
            InitializeComponent();
        }

        private void AgendarReuniones_Load(object sender, EventArgs e)
        {
            try
            {
                
                dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("AgendarReuniones");
                dtpFechaIni.MaxDate = DateTime.Now.Date;
                dtpFechaFin.MinDate = dtpFechaIni.Value;
                cbxSolicitud.SelectedIndexChanged -= cbxSolicitud_SelectedIndexChanged;
                cbxOficina.SelectedIndexChanged -= cbxOficina_SelectedIndexChanged;
                cbxSala.SelectedIndexChanged -= cbxSala_SelectedIndexChanged;
                CargarCombos();
                ListarSolicitudReuniones();
                cbxSolicitud.SelectedIndexChanged += cbxSolicitud_SelectedIndexChanged;
                cbxSala.SelectedIndexChanged += cbxSala_SelectedIndexChanged;
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           
        }

   
        private void ListarSolicitudReuniones()
        {
            DataTable dtReuniones;
            dtReuniones = clsSistemasBL.Instancia.ReportesApp_ListarReunionesAgendadas(dtpFechaIni.Text.ToString(), dtpFechaFin.Text.ToString(), 
                         cbxSolicitud.Text == "" ? null : cbxSolicitud.Text,
                         cbxOficina.SelectedValue == null ? -1 : Convert.ToInt32(cbxOficina.SelectedValue),
                         cbxSala.SelectedValue == null ? -1 : Convert.ToInt32(cbxSala.SelectedValue));
            if (dtReuniones.Rows.Count > 0)
            {
                dgvReuniones.DataSource = dtReuniones;
                //dgvReuniones.Columns["EstadoReunion"].DefaultCellStyle.BackColor = Color.Green;
                dgvReuniones.Columns["idAgendarReunion"].Visible = false;
                dgvReuniones.Columns["NumeroSala"].Visible = false;
                dgvReuniones.Columns["idOficina"].Visible = false;
                dgvReuniones.Columns["EstadoSala"].Visible = false;
                dgvReuniones.Columns["TotalHoras"].Visible = false;
                dgvReuniones.Columns["idEquiposXML"].Visible = false;
                dgvReuniones.Columns["EQUIPOS_SOLICITADOS"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvReuniones.Focus();
                dgvReuniones.Rows[0].Selected = true;

                if (dtPermisos.Rows.Count > 0)
                {

                    contextMenuStrip1.Enabled = true;
                   

                }

                    
            }
            else 
            {
                contextMenuStrip1.Enabled = false;
                dgvReuniones.DataSource = null; 
            
            }

        }

        private void ActivarDesactivarOpciones()
        {
           
            if (dtPermisos != null )
            {
                if (dtPermisos.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]))
                    {
                        editarToolStripMenuItem.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]);
                    }
                    else
                    {
                        editarToolStripMenuItem.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]);
                    }

                    if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                    {
                        DataTable dtPermisosEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());

                        if (dgvReuniones.Rows.Count > 0)
                        {

                            for (int i = 0; i < dtPermisosEspeciales.Rows.Count; i++)
                            {
                                if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Autorizar")
                                {
                                    aPROBARToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(dtPermisosEspeciales.Rows[i]["Activo"]));
                                }
                                if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Rechazar")
                                {
                                    dESAPROBARToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(dtPermisosEspeciales.Rows[i]["Activo"]));
                                }
                                if (dtPermisosEspeciales.Rows[i]["NombrePermiso"].ToString() == "Finalizar")
                                {
                                    fINALIZARToolStripMenuItem.Enabled = Convert.ToBoolean(Convert.ToInt32(dtPermisosEspeciales.Rows[i]["Activo"]));
                                }

                            }
                        }

                    }
                    else
                    {
                        aPROBARToolStripMenuItem.Enabled = false;
                        dESAPROBARToolStripMenuItem.Enabled = false;
                        fINALIZARToolStripMenuItem.Enabled = false;
                        editarToolStripMenuItem.Enabled = false;
                    }

                    if (Convert.ToDateTime(dgvReuniones.CurrentRow.Cells["FechaInicio"].Value) == DateTime.Now.Date && dgvReuniones.CurrentRow.Cells["EstadoReunion"].Value.ToString().TrimEnd() == "PENDIENTE" && dgvReuniones.CurrentRow.Cells["EstadoSolicitud"].Value.ToString().TrimEnd() == "PENDIENTE")
                    {
                        editarToolStripMenuItem.Enabled = true;
                    }
                    else
                    {
                        editarToolStripMenuItem.Enabled = false;
                    }           
                }      

            }
        }

        private void CargarCombos()
        {

             ds = clsSistemasBL.Instancia.ListarEquipos_Sala();



             if (ds.Tables.Count > 0)
            {



    
                dtOficina = ds.Tables["Oficina"];
                dtEquipo = ds.Tables["Equipo"];
                dtSalas =  ds.Tables["Sala"];


                cbxOficina.DataSource = dtOficina;
                cbxOficina.DisplayMember = "NombreOficina";
                cbxOficina.ValueMember = "idOficina";
                if (dtOficina.Rows.Count > 0)
                {
                    cbxOficina.SelectedIndexChanged += cbxOficina_SelectedIndexChanged;
                    cbxOficina.SelectedIndex = 0;
                    cbxOficina_SelectedIndexChanged(null, null);
                }
               
            


            }
            else
            {
                MessageBox.Show("Combobox de TipoProducto no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cbxSala_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarSolicitudReuniones();
        }

        private void cbxOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataView view = dtSalas.AsDataView();
                view.RowFilter = "idOficina=" + cbxOficina.SelectedValue.ToString() ;
                DataTable dt = view.ToTable();
                cbxSala.DataSource = dt;
                cbxSala.DisplayMember = "NombreSala";
                cbxSala.ValueMember = "NumeroSala";
                if (dt.Rows.Count > 0)
                {
                    cbxSala.SelectedIndex = 0;
                }

       


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnNuevaReunion_Click(object sender, EventArgs e)
        {
            NuevaReunion open = new NuevaReunion();
            if (open.ShowDialog() == DialogResult.OK)
            {
                ListarSolicitudReuniones();
            }
            
        }

        private void cbxSolicitud_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarSolicitudReuniones();
        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {

            ListarSolicitudReuniones();
        }

        private void dtpFechaIni_ValueChanged(object sender, EventArgs e)
        {

            ListarSolicitudReuniones();
        }

        private void aPROBARToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Boolean respuesta = false;
            try
            {

                respuesta = clsSistemasBL.Instancia.ReportesApp_Autorizar_Rechazar_Finalizar_SolicitudReunion(Utilitario.Instancia.SesionUsuario.usuario, 
                            Convert.ToInt32(dgvReuniones.CurrentRow.Cells["idAgendarReunion"].Value),"AUTORIZAR");

                if (respuesta)
                {

                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarSolicitudReuniones();
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message,"Mensaje" ,MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void dESAPROBARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Boolean respuesta = false;

            try
            {
                 respuesta = clsSistemasBL.Instancia.ReportesApp_Autorizar_Rechazar_Finalizar_SolicitudReunion(Utilitario.Instancia.SesionUsuario.usuario, 
                             Convert.ToInt32(dgvReuniones.CurrentRow.Cells["idAgendarReunion"].Value),"RECHAZAR");

                if (respuesta)
                {

                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarSolicitudReuniones();
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fINALIZARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Boolean respuesta = false;

            try
            {
                respuesta = clsSistemasBL.Instancia.ReportesApp_Autorizar_Rechazar_Finalizar_SolicitudReunion(Utilitario.Instancia.SesionUsuario.usuario,
                            Convert.ToInt32(dgvReuniones.CurrentRow.Cells["idAgendarReunion"].Value), "FINALIZAR");

                if (respuesta)
                {

                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarSolicitudReuniones();
                }
                else
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToDateTime(dgvReuniones.CurrentRow.Cells["FechaInicio"].Value) == DateTime.Now.Date && dgvReuniones.CurrentRow.Cells["EstadoReunion"].Value.ToString().TrimEnd() == "PENDIENTE" && dgvReuniones.CurrentRow.Cells["EstadoSolicitud"].Value.ToString().TrimEnd() == "PENDIENTE")
                {

                    NuevaReunion open = new NuevaReunion();
                    if (dgvReuniones.CurrentRow.Selected)
                    {

                        open.entAgendarReunion.idAgendarReunion = Convert.ToInt32(dgvReuniones.CurrentRow.Cells["idAgendarReunion"].Value);
                        open.entAgendarReunion.EstadoReunion = dgvReuniones.CurrentRow.Cells["EstadoReunion"].Value.ToString();
                        open.entAgendarReunion.FechaInicio = Convert.ToString(dgvReuniones.CurrentRow.Cells["FechaInicio"].Value);
                        open.entAgendarReunion.HoraInicio = Convert.ToString(dgvReuniones.CurrentRow.Cells["HoraInicio"].Value);
                        open.entAgendarReunion.HoraFin = Convert.ToString(dgvReuniones.CurrentRow.Cells["HoraFin"].Value);
                        open.entAgendarReunion.TotalHoras = Convert.ToString(dgvReuniones.CurrentRow.Cells["TotalHoras"].Value);
                        open.entAgendarReunion.UsuarioSolicitante = dgvReuniones.CurrentRow.Cells["UsuarioSolicitante"].Value.ToString();
                        open.entAgendarReunion.EstadoSolicitante = dgvReuniones.CurrentRow.Cells["EstadoSolicitud"].Value.ToString();
                        open.entAgendarReunion.UsuarioAutoriza = dgvReuniones.CurrentRow.Cells["UsuarioAutoriza"].Value.ToString();
                        open.entAgendarReunion.NombreSala = dgvReuniones.CurrentRow.Cells["NombreSala"].Value.ToString();
                        open.entAgendarReunion.LinkReunion = dgvReuniones.CurrentRow.Cells["LinkReunion"].Value.ToString();
                        open.entAgendarReunion.NumeroSala = Convert.ToInt32(dgvReuniones.CurrentRow.Cells["NumeroSala"].Value);
                        open.entAgendarReunion.idOficina = Convert.ToInt32(dgvReuniones.CurrentRow.Cells["idOficina"].Value);
                        open.entAgendarReunion.EstadoSala = dgvReuniones.CurrentRow.Cells["EstadoSala"].Value.ToString();
                        open.entAgendarReunion.Equipos_Solicitados = dgvReuniones.CurrentRow.Cells["EQUIPOS_SOLICITADOS"].Value == null ? "" : dgvReuniones.CurrentRow.Cells["EQUIPOS_SOLICITADOS"].Value.ToString();
                        open.entAgendarReunion.NombreOficina = dgvReuniones.CurrentRow.Cells["NombreOficina"].Value.ToString();
                        open.entAgendarReunion.idEquiposXML = dgvReuniones.CurrentRow.Cells["idEquiposXML"].Value.ToString();
                        open.tipoOperacion = Utilitario.TipoOperacion.Editar;
                    }
                    
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        ListarSolicitudReuniones();
                    }

                }
                else
                {
                    MessageBox.Show("REUNION EN CURSO, NO ES POSIBLE EDITAR", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception ex)
            {
                
               MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            ActivarDesactivarOpciones();
        }

        private void dgvReuniones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvReuniones.RowCount > 0)
            {
                DataGridView dgv = sender as DataGridView;

                if (dgv.Columns[e.ColumnIndex].Name == "EstadoReunion")  //Si es la columna a evaluar
                {
                    if (e.Value.ToString().Contains("ACTIVO"))   //Si el valor de la celda contiene la palabra hora
                    {
                        e.CellStyle.BackColor = Color.DodgerBlue;
                    }
                    if (e.Value.ToString().Contains("PENDIENTE"))   //Si el valor de la celda contiene la palabra hora
                    {
                        e.CellStyle.BackColor = Color.GreenYellow;
                    }
                    if (e.Value.ToString().Contains("RECHAZADO"))   //Si el valor de la celda contiene la palabra hora
                    {
                        e.CellStyle.BackColor = Color.Crimson;
                    }
                }
            }
        }


    }
}
