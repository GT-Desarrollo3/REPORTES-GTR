using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Comun;

namespace ReportesTranspesa.Sistema
{
    public partial class NuevaReunion : Form
    {
       
        DataSet ds;
        DataTable dtOficina;
        DataTable dtEquipo;
        DataTable dtSalas;
        DataTable dtEquiposEdicion;
        public int tipoOperacion;

        public class AgendarReunion
        {
            public int idAgendarReunion { get; set; }
            public string EstadoReunion { get; set; }
            public string FechaInicio { get; set; }
            public string HoraInicio { get; set; }
            public string HoraFin { get; set; }
            public string TotalHoras { get; set; }
            public string UsuarioSolicitante { get; set; }
            public string EstadoSolicitante { get; set; }
            public string UsuarioAutoriza { get; set; }
            public string NombreSala { get; set; }
            public string LinkReunion { get; set; }
            public int NumeroSala { get; set; }
            public int idOficina { get; set; }
            public string EstadoSala { get; set; }
            public string Equipos_Solicitados { get; set; }
            public string NombreOficina { get; set; }
            public string idEquiposXML { get; set; }
        }

        public AgendarReunion entAgendarReunion = new AgendarReunion();
      
        public NuevaReunion()
        {
            InitializeComponent();
        }

        private void NuevaReunion_Load(object sender, EventArgs e)
        {


            
            if (tipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                cbxOficina.SelectedIndexChanged -= cbxOficina_SelectedIndexChanged;
                dtpHoraFin.Value = DateTime.Now.AddHours(1);
                dtpHoraFin.MinDate = dtpHoraInicio.Value.AddHours(1);
                CargarCombos();
            }
      

            if (tipoOperacion == Utilitario.TipoOperacion.Editar)
            {

                cbxOficina.SelectedIndexChanged -= cbxOficina_SelectedIndexChanged;
                CargarCombos();
                CargarDatosEdicion();
               
                
            }

        }

        private void CargarDatosEdicion()
        {
            cbxOficina.SelectedValue = entAgendarReunion.idOficina;
            cbxSala.SelectedValue = entAgendarReunion.NumeroSala;
            txtLinkReunion.Text = entAgendarReunion.LinkReunion;
            dtpFechaReunion.Value = Convert.ToDateTime(entAgendarReunion.FechaInicio);
            dtpHoraInicio.Value = Convert.ToDateTime(entAgendarReunion.HoraInicio);
            dtpHoraFin.Value = Convert.ToDateTime(entAgendarReunion.HoraFin);


            if (entAgendarReunion.idEquiposXML != "")
            {
                dtEquiposEdicion = Utilitario.Instancia.ConvertirXMLaDatatable(entAgendarReunion.idEquiposXML);
            }
            else
            {
                dtEquiposEdicion = null;
            }
           



            if (dtEquiposEdicion != null)
            {
                if (dtEquiposEdicion.Rows.Count > 0)
                {

                    for (int i = 0; i < dgvEquipos.Rows.Count; i++)
                    {
                        for (int j = 0; j < dtEquiposEdicion.Rows.Count; j++)
                        {
                            if (Convert.ToInt32(dgvEquipos.Rows[i].Cells["idEquipo"].Value) == Convert.ToInt32(dtEquiposEdicion.Rows[j]["idEquipo"]))
                            {
                                dgvEquipos.Rows[i].Cells["Check"].Value = true;
                            }
                        }


                    }
                    ActivarDesactivarCheck();
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
                dtSalas = ds.Tables["Sala"];


                cbxOficina.DataSource = dtOficina;
                cbxOficina.DisplayMember = "NombreOficina";
                cbxOficina.ValueMember = "idOficina";


                if (dtOficina.Rows.Count > 0)
                {
                    cbxOficina.SelectedIndexChanged += cbxOficina_SelectedIndexChanged;
                    cbxOficina.SelectedIndex = 0;
                    cbxOficina_SelectedIndexChanged(null, null);
                }

                if (dtEquipo.Rows.Count > 0)
                {
                    for (int i = 0; i < dtEquipo.Rows.Count; i++)
                    {
                        dgvEquipos.Rows.Add(Convert.ToBoolean(dtEquipo.Rows[i]["Check"]),
                                            dtEquipo.Rows[i]["idEquipo"].ToString(),
                                            dtEquipo.Rows[i]["CodEquipo"].ToString(),
                                            dtEquipo.Rows[i]["NombreEquipo"].ToString(),
                                            dtEquipo.Rows[i]["Disponibilidad"].ToString(),
                                            dtEquipo.Rows[i]["idOficina"].ToString(),
                                            dtEquipo.Rows[i]["NombreOficina"].ToString(),
                                            dtEquipo.Rows[i]["NumeroSala"].ToString(),
                                            dtEquipo.Rows[i]["NombreSala"].ToString(),
                                            dtEquipo.Rows[i]["Estado"].ToString(),
                                            dtEquipo.Rows[i]["idEquipoMaestro"].ToString()
                                            );

                       
                    }
                    
                    dgvEquipos.Columns["NombreEquipo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgvEquipos.Columns["NombreEquipo"].ReadOnly = true;
                    dgvEquipos.Columns["Disponibilidad"].ReadOnly = true;
                    dgvEquipos.Columns["NombreOficina"].ReadOnly = true;
                    dgvEquipos.Columns["NombreSala"].ReadOnly = true;
                    dgvEquipos.Columns["Check"].ReadOnly = false;



                    ActivarDesactivarCheck();
                        
                }


            }
            else
            {
                MessageBox.Show("Combobox de TipoProducto no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void ActivarDesactivarCheck()
        {
            if (dtEquipo.Rows.Count > 0)
            {
                if (Convert.ToInt32(cbxOficina.SelectedValue) == 1 && Convert.ToInt32(cbxSala.SelectedValue) == 1)
                {
                    for (int i = 0; i < dgvEquipos.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgvEquipos.Rows[i].Cells["idEquipo"].Value) == 1)
                        {
                            dgvEquipos.Rows[i].Cells["Check"].Value = true;
                            dgvEquipos.Rows[i].Cells["Check"].ReadOnly = true;
                        }

                    }

                }
                else
                {
                    for (int i = 0; i < dgvEquipos.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgvEquipos.Rows[i].Cells["idEquipo"].Value) == 1)
                        {
                            dgvEquipos.Rows[i].Cells["Check"].Value = false;
                            dgvEquipos.Rows[i].Cells["Check"].ReadOnly = false;
                        }

                    }

                }

            }
        }
        private void cbxOficina_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataView view = dtSalas.AsDataView();
                view.RowFilter = "idOficina=" + cbxOficina.SelectedValue.ToString();
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

                MessageBox.Show("Combobox  no se cargó, verificar permisos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }



        private void AgendarReunon_Click(object sender, EventArgs e)
        {



            DataTable dtEquipoCheck = null;

            dtEquipoCheck = DgvToDataTable(dgvEquipos);
            string xmlEquipo;

            if (dtEquipoCheck.Rows.Count > 0)
            {

                xmlEquipo = Comun.Utilitario.Instancia.DatatableToXml(dtEquipoCheck);
            }
            else
            {
                xmlEquipo = null;
            }




            try
            {
                if (tipoOperacion == Utilitario.TipoOperacion.Editar)
                {
                    if (dtpFechaReunion.Value.Date < DateTime.Now.Date)
                    {
                        MessageBox.Show("La fecha seleccionada no puede ser menor a la fecha actual", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dtpFechaReunion.Value = DateTime.Now.Date;
                        return;
                    }
                    else
                    {

                        Boolean respuesta = clsSistemasBL.Instancia.ReportesApp_Actualizar_AgendarReuninon(Convert.ToInt32(entAgendarReunion.idAgendarReunion),Utilitario.Instancia.SesionUsuario.usuario, dtpFechaReunion.Text, dtpHoraInicio.Text, dtpHoraFin.Text,
                                                                                                    Convert.ToInt32(cbxOficina.SelectedValue), Convert.ToInt32(cbxSala.SelectedValue),
                                                                                                    txtLinkReunion.Text, xmlEquipo);

                        if (respuesta)
                        {
                            MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                if (tipoOperacion == Utilitario.TipoOperacion.Registrar)
                {


                    Boolean respuesta = clsSistemasBL.Instancia.ReportesApp_Registrar_AgendarReuninon(Utilitario.Instancia.SesionUsuario.usuario, dtpFechaReunion.Text, dtpHoraInicio.Text, dtpHoraFin.Text,
                                                                                                Convert.ToInt32(cbxOficina.SelectedValue), Convert.ToInt32(cbxSala.SelectedValue),
                                                                                                txtLinkReunion.Text, xmlEquipo);

                    if (respuesta)
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
              
            }
            catch (Exception ex)
            {
                
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private DataTable DgvToDataTable(DataGridView dataGridView)
        {
            var dt = new DataTable();
            int columnCount = 0;
            List<int> columnNumbers = new List<int>();

            foreach (DataGridViewColumn dataGridViewColumn in dataGridView.Columns)
            {
                
                    dt.Columns.Add(dataGridViewColumn.Name);
                    columnNumbers.Add(columnCount);
                


                columnCount++;
            }

            var cell = new object[dataGridView.Columns.Count];
            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
            {
                if (Convert.ToBoolean(dataGridViewRow.Cells["Check"].Value))
                {
                    for (int i = 0; i < dataGridViewRow.Cells.Count; i++)
                    {
                        cell[i] = dataGridViewRow.Cells[i].Value;
                    }
                    dt.Rows.Add(cell);
                }

            }

            return dt;
            
        }

        private void dtpHoraFin_ValueChanged(object sender, EventArgs e)
        {

            dtpHoraFin.MinDate = dtpHoraInicio.Value.AddHours(1);

        }

        private void dtpHoraFin_KeyUp(object sender, KeyEventArgs e)
        {


        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            if (tipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                if (dtpFechaReunion.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("La fecha seleccionada no puede ser menor a la fecha actual", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    dtpFechaReunion.Value = DateTime.Now.Date;
                    return;
                }
            }

        }

        private void dtpHoraInicio_ValueChanged(object sender, EventArgs e)
        {
            if (tipoOperacion == Utilitario.TipoOperacion.Registrar)
            {
                dtpHoraFin.MinDate = dtpHoraInicio.Value.AddHours(1);
                dtpHoraFin.Value = dtpHoraInicio.Value.AddHours(1);
            }

            if (tipoOperacion == Utilitario.TipoOperacion.Editar)
            {
                dtpHoraFin.MinDate = dtpHoraInicio.Value.AddHours(1);
                dtpHoraFin.Value = dtpHoraInicio.Value.AddHours(1);
            } 
            
        }

        private void cbxSala_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxSala_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dtEquipo.Rows.Count > 0)
            {
                if (Convert.ToInt32(cbxOficina.SelectedValue) == 1 && Convert.ToInt32(cbxSala.SelectedValue) == 1)
                {
                    for (int i = 0; i < dgvEquipos.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgvEquipos.Rows[i].Cells["idEquipo"].Value) == 1)
                        {
                            dgvEquipos.Rows[i].Cells["Check"].Value = true;
                            dgvEquipos.Rows[i].Cells["Check"].ReadOnly = true;
                        }

                    }

                }
                else
                {
                    for (int i = 0; i < dgvEquipos.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgvEquipos.Rows[i].Cells["idEquipo"].Value) == 1)
                        {
                            dgvEquipos.Rows[i].Cells["Check"].Value = false;
                            dgvEquipos.Rows[i].Cells["Check"].ReadOnly = false;
                        }

                    }

                }

            }
        }



    }
}
