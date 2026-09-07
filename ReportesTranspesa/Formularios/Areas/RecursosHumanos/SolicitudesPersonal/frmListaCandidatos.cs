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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos.SolicitudesPersonal
{
    public partial class frmListaCandidatos : MetroFramework.Forms.MetroForm
    {
        public string _Area, _Puesto, _Solicitud, _Estado;
        public int e3 = 0, e4 = 0;
        public int _idSolicitudPersonal;
        public frmListaSolicitudesPersonal _formulario;
        public DataTable dtPermisos2 = new DataTable();
        public DataTable dtEspeciales2 = new DataTable();

        public frmListaCandidatos()
        {
            InitializeComponent();
        }

        private void frmListaCandidatos_Load(object sender, EventArgs e)
        {
            lblSolicitud.Text = _Solicitud;
            lblArea.Text = _Area;
            lblPuesto.Text = _Puesto;

            dgvListaCandidatosVista.OptionsBehavior.Editable = true;
            dtgListaCandidatos.DataSource = null;
            ListarCandidatos();
        }


        public void RecibirDatos(string Area, string Puesto, string Solicitud, string Estado, int idSolicitudPersonal, frmListaSolicitudesPersonal formulario)
        {
            _Area = Area;
            _Puesto = Puesto;
            _Solicitud = Solicitud;
            _Estado = Estado;
            _idSolicitudPersonal = idSolicitudPersonal;
            _formulario = formulario;
        }

        public void ListarCandidatos()
        {
            dtgListaCandidatos.DataSource = null;
            DataTable dtLista = new DataTable();
            dtLista = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_ListarCandidatos(_idSolicitudPersonal);
            if (dtLista.Rows.Count > 0)
            {
                dtgListaCandidatos.DataSource = dtLista;
                dgvListaCandidatosVista.Columns["N°"].Width = 30;
                dgvListaCandidatosVista.Columns["DNI"].Width = 70;
                dgvListaCandidatosVista.Columns["Nombre"].Width = 220;
                dgvListaCandidatosVista.Columns["FechaEntrevista"].Width = 90;
                dgvListaCandidatosVista.Columns["Estado"].Width = 100;
                dgvListaCandidatosVista.Columns["Observacion"].Width = 300;

                dgvListaCandidatosVista.Columns["idSolicitudPersonal"].Visible = false;
            }
        }


        private void dtgListaCandidatos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (_Estado == "EN PROCESO")
                {
                    string ES = dgvListaCandidatosVista.GetRowCellValue(dgvListaCandidatosVista.FocusedRowHandle, "Estado").ToString();
                    if (ES == " ") { if (e4 == 1) { seleccionarToolStripMenuItem.Enabled = true; } }
                    else { seleccionarToolStripMenuItem.Enabled = false; }
                }
                else { seleccionarToolStripMenuItem.Enabled = false; }
            }
            catch
            {
                seleccionarToolStripMenuItem.Enabled = false;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DataTable dtAgregar = new DataTable();
            string respta;
            dtAgregar = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_AgregaModificaCandidato(1, 0, _idSolicitudPersonal, "", "", DateTime.Now, "", "", Utilitario.Instancia.SesionUsuario.usuario);
            respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
            string NroRspta = respta.Substring(0, 1);
            if (NroRspta == "0")
            {
                ListarCandidatos();
                _formulario.ListarSolicitudes();
            }
            else
            {
                MessageBox.Show("Error al agregar nuevo candidato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            int[] filas = dgvListaCandidatosVista.GetSelectedRows();
            string respta = "";
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    if (dgvListaCandidatosVista.GetRowCellValue(filas[i], "FechaEntrevista").ToString() == "")
                    {
                        MessageBox.Show("La fecha no puede estar vacía.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        int idCandidato = Convert.ToInt32(dgvListaCandidatosVista.GetRowCellValue(filas[i], "N°").ToString());
                        string DNI = dgvListaCandidatosVista.GetRowCellValue(filas[i], "DNI").ToString();
                        string nombre = dgvListaCandidatosVista.GetRowCellValue(filas[i], "Nombre").ToString();
                        DateTime fechaEntrevista = Convert.ToDateTime(dgvListaCandidatosVista.GetRowCellValue(filas[i], "FechaEntrevista"));
                        string Observacion = dgvListaCandidatosVista.GetRowCellValue(filas[i], "Observacion").ToString();

                        DataTable dtModificar = new DataTable();
                        dtModificar = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_AgregaModificaCandidato(2, idCandidato, _idSolicitudPersonal, DNI, nombre, fechaEntrevista, "", Observacion,
                                                                                                                                  Utilitario.Instancia.SesionUsuario.usuario);
                        respta = Convert.ToString(dtModificar.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            mensaje = "S";
                        }
                        else
                        {
                            MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }
                }

                if (mensaje == "S")
                {
                    MessageBox.Show(respta, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCandidatos();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningún candidato para modificar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar al candidato de forma permanente?", "ELIMINAR CANDIDATO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int[] filas = dgvListaCandidatosVista.GetSelectedRows();
                string respta = "";

                if (filas.Length > 1)
                {
                    MessageBox.Show("Solo puede eliminar uno a la vez.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    if (filas.Length != 0)
                    {
                        for (int i = 0; i < filas.Length; i++)
                        {
                            int idCandidato = Convert.ToInt32(dgvListaCandidatosVista.GetRowCellValue(filas[i], "N°").ToString());

                            DataTable dtEliminar = new DataTable();
                            dtEliminar = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_AgregaModificaCandidato(3, idCandidato, _idSolicitudPersonal, "", "", DateTime.Now, "", "", Utilitario.Instancia.SesionUsuario.usuario);
                            respta = Convert.ToString(dtEliminar.Rows[0]["exito"]);
                            string NroRspta = respta.Substring(0, 1);
                            if (NroRspta == "0")
                            {
                                ListarCandidatos();
                            }
                            else
                            {
                                MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No ha seleccionado ningún candidato para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void seleccionarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Nombre = dgvListaCandidatosVista.GetRowCellValue(dgvListaCandidatosVista.FocusedRowHandle, "Nombre").ToString();
            if (MessageBox.Show("¿Desea elegir al candidato " + Nombre + "?", "SELECCIONAR CANDIDATO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idCandidato = Convert.ToInt32(dgvListaCandidatosVista.GetRowCellValue(dgvListaCandidatosVista.FocusedRowHandle, "N°"));
                DataTable dtSeleccionar = new DataTable();
                string respta;

                dtSeleccionar = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_SolicitudesPersonal_AgregaModificaCandidato(4, idCandidato, _idSolicitudPersonal, "", "", DateTime.Now, "", "", Utilitario.Instancia.SesionUsuario.usuario);
                respta = Convert.ToString(dtSeleccionar.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0")
                {
                    MessageBox.Show("Candidato seleccionado.", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarCandidatos();
                    this.Close();
                    _formulario.ListarSolicitudes();
                }
                else
                {
                    MessageBox.Show("Error al agregar nuevo candidato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
