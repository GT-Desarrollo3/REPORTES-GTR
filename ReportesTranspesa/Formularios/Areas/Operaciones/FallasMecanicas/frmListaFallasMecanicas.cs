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
using System.IO;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;
using ReportesTranspesa.Formularios.Areas.Mantenimiento.ReporteIncidencias;
using Word = Microsoft.Office.Interop.Word;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    public partial class frmListaFallasMecanicas : Form
    {
        public string esVALE, EstadoSolicitud, TipoFalla;
        public int valorEstado, OpcionFecha = 0, MttoCorrectivo, BloquearTracto, BloquearCarreta;
        public int valorSalida = 1, idSistema;
        DataTable dtPermisos = new DataTable();
        DataTable dtEspeciales = new DataTable();
        DataTable dtListaFallasMecanicas = new DataTable();
        DataTable dtListaSolicitudMantenimiento = new DataTable();
        public int xClick = 0, yClick = 0;
        public int xClick2 = 0, yClick2 = 0;
        public int idFalla = -1, OpcionU = 1;
        int e1 = 0, e2 = 0, e3 = 0, e4 = 0, e5 = 0, e6 = 0;

        public frmListaFallasMecanicas()
        {
            InitializeComponent();
            cbxTipoAuxilio_Act.SelectedIndexChanged -= cbxTipoAuxilio_Act_SelectedIndexChanged;
            cbxRecibo.SelectedIndexChanged -= cbxRecibo_SelectedIndexChanged;
            cbxEstadoLiquidacion.SelectedIndexChanged -= cbxEstadoLiquidación_SelectedIndexChanged;
            cbxSistema.SelectedIndexChanged -= cbxSistema_SelectedIndexChanged;
            cbxSubSistema.SelectedIndexChanged -= cbxSubSistema_SelectedIndexChanged;
            cbxBase.SelectedIndexChanged -= cbxBase_SelectedIndexChanged;
            cbxBase2.SelectedIndexChanged -= cbxBase2_SelectedIndexChanged;
            cbxOperacion.SelectedIndexChanged -= cbxOperacion_SelectedIndexChanged;
            cbxOperacion2.SelectedIndexChanged -= cbxOperacion2_SelectedIndexChanged;
        }

        private void cbxTipoAuxilio_Act_SelectedIndexChanged(object sender, EventArgs e) { CargarComboAuxilio(); }

        private void cbxRecibo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboRecibo(); }

        private void cbxEstadoLiquidación_SelectedIndexChanged(object sender, EventArgs e) { CargarComboLiquidacion(); }

        private void cbxSistema_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSistema(); }

        private void cbxSubSistema_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSubSistema(); }

        private void cbxBase_SelectedIndexChanged(object sender, EventArgs e) { CargarComboBase(1); }

        private void cbxBase2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboBase2(2); }

        private void cbxOperacion_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void cbxOperacion2_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion2(); }

        private void frmListaFallasMecanicas_Shown(object sender, EventArgs e) { txtPlaca.Focus(); }

        private void frmListaFallasMecanicas_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("frmListaFallasMecanicas");
            if (dtPermisos.Rows.Count > 0)
            {
                if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { editarFallaMecanicaToolStripMenuItem.Enabled = true; }
                else { editarFallaMecanicaToolStripMenuItem.Enabled = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Leer"]) == true) { rbFalla.Visible = true; }
                else { rbFalla.Visible = false; }

                if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarFallaToolStripMenuItem.Enabled = true; }
                else { eliminarFallaToolStripMenuItem.Enabled = false; }
            }

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                { dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString()); }
            }

            if (dtEspeciales.Rows.Count > 0)
            {
                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Cerrar Falla Mecanica")
                    {
                        cerrarFallaToolStripMenuItem.Enabled = true;
                        i = 999; e1 = 1;
                    }
                    else { cerrarFallaToolStripMenuItem.Enabled = false; }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Asignar Solucion")
                    {
                        asignarSolucionToolStripMenuItem.Enabled = true;
                        generarPresupuestoToolStripMenuItem.Enabled = true;
                        i = 999; e2 = 1;
                    }
                    else
                    {
                        asignarSolucionToolStripMenuItem.Enabled = false;
                        generarPresupuestoToolStripMenuItem.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Liquidar Falla Mecanica")
                    {
                        liquidarFallaMecanicaToolStripMenuItem.Enabled = true;
                        generarPresupuestoToolStripMenuItem.Enabled = true;
                        i = 999; e3 = 1;
                    }
                    else
                    {
                        liquidarFallaMecanicaToolStripMenuItem.Enabled = false;
                        generarPresupuestoToolStripMenuItem.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Nueva Solicitud")
                    {
                        btnNuevaSolicitud.Enabled = true;
                        recepcionarUnidadToolStripMenuItem.Enabled = true;
                        agregarFechaEstimadaToolStripMenuItem.Enabled = true;
                        anularSolicitudToolStripMenuItem.Enabled = true;
                        terminarMantenimientoToolStripMenuItem.Enabled = true;
                        tsCambiarUbicacion.Enabled = true;
                        i = 999; e4 = 1;
                    }
                    else
                    {
                        btnNuevaSolicitud.Enabled = false;
                        recepcionarUnidadToolStripMenuItem.Enabled = false;
                        agregarFechaEstimadaToolStripMenuItem.Enabled = false;
                        anularSolicitudToolStripMenuItem.Enabled = false;
                        terminarMantenimientoToolStripMenuItem.Enabled = false;
                        tsCambiarUbicacion.Enabled = false;
                    }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Cerrar Repuesto")
                    {
                        btnRegistrarLlegada.Enabled = true;
                        i = 999; e5 = 1;
                    }
                    else { btnRegistrarLlegada.Enabled = false; }
                }

                for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                {
                    if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "Programar Solicitud")
                    {
                        btnRegistrarLogistica.Enabled = true;
                        programarSolicitudToolStripMenuItem.Enabled = true;
                        i = 999; e6 = 1;
                    }
                    else
                    {
                        btnRegistrarLogistica.Enabled = false;
                        programarSolicitudToolStripMenuItem.Enabled = false;
                    }
                }
            }
            else
            {
                cerrarFallaToolStripMenuItem.Enabled = false;
                asignarSolucionToolStripMenuItem.Enabled = false;
                liquidarFallaMecanicaToolStripMenuItem.Enabled = false;
                generarPresupuestoToolStripMenuItem.Enabled = false;
                btnNuevaSolicitud.Enabled = false;
                anularSolicitudToolStripMenuItem.Enabled = false;
                btnRegistrarLlegada.Enabled = false;
                recepcionarUnidadToolStripMenuItem.Enabled = false;
                btnRegistrarLogistica.Enabled = false;
                programarSolicitudToolStripMenuItem.Enabled = false;
                terminarMantenimientoToolStripMenuItem.Enabled = false;
                tsCambiarUbicacion.Enabled = false;
            }

            fechaInicio.Value = new DateTime(fechaInicio.Value.Year, fechaInicio.Value.Month, 1);
            fechaFin.Value = DateTime.Now;
            dtpFechaSalida.Value = DateTime.Now;
            dtpHoraSalida.Value = DateTime.Now;
            dtpFechaLiquidacion.Value = DateTime.Now;
            dtpFechaEstimada.Value = DateTime.Now;
            dtpHoraEstimada.Value = new DateTime(dtpHoraEstimada.Value.Year, dtpHoraEstimada.Value.Month, 1, 0, 0, 0);
            dtpFechaRecepcion.Value = DateTime.Now;
            dtpHoraRecepcion.Value = new DateTime(dtpHoraEstimada.Value.Year, dtpHoraEstimada.Value.Month, 1, 0, 0, 0);
            rbSolicitud.Checked = true;
            rbSolicitud_Click(sender, e);
            rbTodas_Click(sender, e);

            FechaProgIni.Value = new DateTime(FechaProgIni.Value.Year, FechaProgIni.Value.Month, 1);
            FechaProgFin.Value = DateTime.Now;

            idSistema = 3;
            CargarComboAuxilio();
            CargarComboRecibo();
            CargarComboLiquidacion();
            CargarComboSistema();
            CargarComboSubSistema();
            CargarComboBase(1);
            cbxValidacionB.Text = "TODOS";
            CargarComboOperacion();
            CargarComboOperacion2();
            cbxOperacion.Text = "TODO";
            cbxOperacion2.Text = "TODO";
        }


        private void CargarComboAuxilio()
        {
            DataTable dtTipoAuxilio = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarAuxilio();
            cbxTipoAuxilio_Act.DataSource = dtTipoAuxilio;
            cbxTipoAuxilio_Act.DisplayMember = "Descripcion";
            cbxTipoAuxilio_Act.ValueMember = "idTipoAuxilio";
        }

        private void CargarComboRecibo()
        {
            DataTable dtRecibo = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarRecibos();
            cbxRecibo.DataSource = dtRecibo;
            cbxRecibo.DisplayMember = "Descripcion";
            cbxRecibo.ValueMember = "idTipoRecibo";
        }

        private void CargarComboLiquidacion()
        {
            DataTable dtLiquidacion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarLiquidacion();
            cbxEstadoLiquidacion.DataSource = dtLiquidacion;
            cbxEstadoLiquidacion.DisplayMember = "Descripcion";
            cbxEstadoLiquidacion.ValueMember = "idEstadoLiquidacion";
        }

        private void CargarComboSistema()
        {
            DataTable dtSistema = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSistemasVehiculos();
            cbxSistema.DataSource = dtSistema;
            cbxSistema.DisplayMember = "Descripcion";
            cbxSistema.ValueMember = "idSistemaVehiculo";
        }

        private void CargarComboSubSistema()
        {
            DataTable dtSubSistema = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSubSistemasVehiculos(idSistema);
            cbxSubSistema.DataSource = dtSubSistema;
            cbxSubSistema.DisplayMember = "Descripcion";
            cbxSubSistema.ValueMember = "idSubSistema";
        }

        public void CargarComboBase(int Opcion)
        {
            DataTable dtBase = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarBases(Opcion);
            cbxBase.DataSource = dtBase;
            cbxBase.DisplayMember = "DescripcionBase";
            cbxBase.ValueMember = "idBase";
        }

        public void CargarComboBase2(int Opcion)
        {
            DataTable dtBase2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarBases(Opcion);

            if (Opcion == 1 || Opcion == 2)
            {
                cbxBase2.DataSource = dtBase2;
                cbxBase2.DisplayMember = "DescripcionBase";
                cbxBase2.ValueMember = "idBase";
            }
        }

        public void CargarComboOperacion()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(5, "");
            cbxOperacion.DataSource = dtOperacion;
            cbxOperacion.DisplayMember = "Descripcion";
            cbxOperacion.ValueMember = "IdOperacion";
        }

        public void CargarComboOperacion2()
        {
            DataTable dtOperacion = clsOperacionesBL.Instancia.ReportesApp_Operaciones_Operatividad_ListarTractos(5, "");
            cbxOperacion2.DataSource = dtOperacion;
            cbxOperacion2.DisplayMember = "Descripcion";
            cbxOperacion2.ValueMember = "IdOperacion";
        }

        public void ListarFallasMecanicas()
        {
            dtListaFallasMecanicas = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarFallas(txtPlaca.Text, fechaInicio.Text, fechaFin.Text, valorEstado, cbxValidacionB.Text, cbxOperacion2.Text);
            dtgListaFallasMecanicas.DataSource = dtListaFallasMecanicas;
            if (dtListaFallasMecanicas.Rows.Count > 0)
            {
                dgvListaFallasMecanicasView.Columns["ID"].Visible = false;
                dgvListaFallasMecanicasView.Columns["idIncidenteC"].Visible = false;
                dgvListaFallasMecanicasView.Columns["CodFalla"].Visible = false;
                dgvListaFallasMecanicasView.Columns["CodEstado"].Visible = false;
                dgvListaFallasMecanicasView.Columns["UsuarioCreacion"].Visible = false;
                dgvListaFallasMecanicasView.Columns["FechaCreacion"].Visible = false;
                dgvListaFallasMecanicasView.Columns["UltimoUsuario"].Visible = false;
                dgvListaFallasMecanicasView.Columns["UltimaModificacion"].Visible = false;

                /*
                dgvListaFallasMecanicasView.SortInfo.ClearAndAddRange(new GridColumnSortInfo[]
                { 
                    new GridColumnSortInfo(dgvListaFallasMecanicasView.Columns["TIPO_FALLA"], DevExpress.Data.ColumnSortOrder.Descending)
                }, 1);
                */

                dgvListaFallasMecanicasView.Columns["FECHA_LIQUIDACIÓN"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaFallasMecanicasView.Columns["FECHA_LIQUIDACIÓN"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaFallasMecanicasView.Columns["FechaCreacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaFallasMecanicasView.Columns["FechaCreacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaFallasMecanicasView.Columns["UltimaModificacion"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaFallasMecanicasView.Columns["UltimaModificacion"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvListaFallasMecanicasView.Columns["ESTADO_AUXILIO"].Summary.Clear();
                dgvListaFallasMecanicasView.Columns["ESTADO_AUXILIO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CONDUCTOR", "Total = {0}");

                dgvListaFallasMecanicasView.BestFitColumns();
                dgvListaFallasMecanicasView.ExpandAllGroups();
            }
        }

        public void ListarSolicitudes()
        {
            dtListaSolicitudMantenimiento = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Listar(txtPlacaS.Text, FechaProgIni.Text, FechaProgFin.Text, Convert.ToInt32(cbxBase.SelectedValue), EstadoSolicitud, cbxOperacion.Text);
            dtgListaSolicitud.DataSource = dtListaSolicitudMantenimiento;
            if (dtListaSolicitudMantenimiento.Rows.Count > 0)
            {
                dgvListaSolicitudView.Columns["idSolicitud"].Visible = false;
                /*
                dgvListaSolicitudView.Columns["UsuarioCreacion"].Visible = false;
                dgvListaSolicitudView.Columns["FechaCreacion"].Visible = false;
                dgvListaSolicitudView.Columns["UltimoUsuario"].Visible = false;
                dgvListaSolicitudView.Columns["UltimaModificacion"].Visible = false;
                */

                dgvListaSolicitudView.Columns["ULTIMA_FECHA_PROG"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaSolicitudView.Columns["ULTIMA_FECHA_PROG"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaSolicitudView.Columns["FECHA_SOLICITUD"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaSolicitudView.Columns["FECHA_SOLICITUD"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaSolicitudView.Columns["FECHA_ESTIMADA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaSolicitudView.Columns["FECHA_ESTIMADA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaSolicitudView.Columns["FECHA_RECEPCION"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaSolicitudView.Columns["FECHA_RECEPCION"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaSolicitudView.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaSolicitudView.Columns["FECHA_PROYECTADA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaSolicitudView.Columns["FECHA_REPROGRAMADA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaSolicitudView.Columns["FECHA_REPROGRAMADA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                dgvListaSolicitudView.Columns["FECHA_ENTREGADA"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaSolicitudView.Columns["FECHA_ENTREGADA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";

                dgvListaSolicitudView.Columns["ESTADO"].Summary.Clear();
                dgvListaSolicitudView.Columns["ESTADO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "TOTAL_SOLICITUDES", "Total = {0}");

                dgvListaSolicitudView.BestFitColumns();
                dgvListaSolicitudView.ExpandAllGroups();
            }
        }

        private void ListarFallaEditar(object sender, EventArgs e)
        {
            DataTable dtListaFallas = new DataTable();
            dtListaFallas = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarFallaEditar(Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID")));

            if (dtListaFallas.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaFallas.Rows.Count; i++)
                {
                    if (dtListaFallas.Rows[i]["TIPO"].ToString() == "PARADA")
                    {
                        rbFallaParada.Checked = true;
                        rbFallaParada_Click(sender, e);
                    }
                    else
                    {
                        rbLeve.Checked = true;
                        rbLeve_Click(sender, e);
                    }

                    cbxTipoAuxilio_Act.SelectedValue = Convert.ToInt32(dtListaFallas.Rows[i]["idTipoAuxilio"]);
                    txtMotivo_Act.Text = dtListaFallas.Rows[i]["MOTIVO"].ToString();
                    txtUbicacion_Act.Text = dtListaFallas.Rows[i]["UBICACIÓN"].ToString();
                    cbxUnidadAfectada.Text = "TRACTO";
                }
            }
        }

        private void EditarFalla()
        {
            if (txtMotivo_Act.Text.Length == 0 || txtUbicacion_Act.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                if (txtMotivo_Act.Text.Length == 0) { txtMotivo_Act.Focus(); }
                else { txtUbicacion_Act.Focus(); }
                return;
            }
            else
            {
                int idFalla = Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ModificarFalla(idFalla, Convert.ToInt32(cbxTipoAuxilio_Act.SelectedValue),
                                                           TipoFalla, txtMotivo_Act.Text, txtUbicacion_Act.Text, cbxUnidadAfectada.Text, cbxValidacion.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pEditarFalla.Visible = false;
                    pEditarFalla.SendToBack();
                    ListarFallasMecanicas();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMotivo_Act.Focus();
                }
            }
        }

        private void EliminarFalla()
        {
            if (MessageBox.Show("¿Desea eliminar este auxilio mecánico?", "ELIMINAR AUXILIO MECÁNICO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idFalla = Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID"));
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_EliminarFalla(idFalla);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarFallasMecanicas();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void CerrarFalla()
        {
            int idFalla = Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_CerrarFallaMecanica(idFalla, valorSalida, cbxEstadoAuxilio.Text, Convert.ToDateTime(dtpFechaSalida.Value), Convert.ToDateTime(dtpHoraSalida.Value), Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pCerrarFalla.Visible = false;
                pCerrarFalla.SendToBack();
                ListarFallasMecanicas();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpFechaSalida.Focus();
            }
        }

        private void InsertarSolucion()
        {
            if (groupBox6.Enabled == true)
            {
                if (txtTecnico.Text.Length == 0 || txtPlacaTecnico.Text.Length == 0 || txtMonto.Text.Length == 0 || txtSolucion.Text.Length == 0)
                {
                    MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (txtTecnico.Text.Length == 0) { txtTecnico.Focus(); }
                    else
                    {
                        if(txtPlacaTecnico.Text.Length == 0) { txtPlacaTecnico.Focus(); }
                        else
                        {
                            if(txtMonto.Text.Length == 0) { txtMonto.Focus(); }
                            else { txtSolucion.Focus(); }
                        }
                    }
                    return;
                }
                else
                {
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    decimal vacio = 0.00M;
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarSolucion(idFalla, txtNombre.Text, txtNumero.Text, 4, txtFactura.Text, vacio,
                                  txtTecnico.Text, txtPlacaTecnico.Text, Convert.ToDecimal(txtGalones.Text), Convert.ToDecimal(txtSoles.Text), Convert.ToDecimal(txtTotal.Text), Convert.ToDecimal(txtMonto.Text),
                                  Convert.ToInt32(cbxSistema.SelectedValue), Convert.ToInt32(cbxSubSistema.SelectedValue), txtSolucion.Text, Usuario, MttoCorrectivo);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (BloquearTracto == 1)
                        {
                            DataTable dtBloqueoVehiculos = new DataTable();
                            int idVehiculo;

                            dtBloqueoVehiculos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ObtenerCodigoVehiculo(txtPlaca1.Text);
                            idVehiculo = Convert.ToInt32(dtBloqueoVehiculos.Rows[0]["IdVehiculo"]);

                            DataTable dtBloqueoUnidad = new DataTable();
                            string BloqueoUnidad;

                            dtBloqueoUnidad = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(1, 0, 0, 0, idVehiculo, "FALLA MECÁNICA", "MANTENIMIENTO", txtSolucion.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                                                                                            0, DateTime.Now.ToString(), DateTime.Now.ToString());

                            BloqueoUnidad = Convert.ToString(dtBloqueoUnidad.Rows[0]["exito"]);
                            string NroRPTA3 = BloqueoUnidad.Substring(0, 1);

                            if (NroRPTA3 == "0") { MessageBox.Show(BloqueoUnidad, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            else { MessageBox.Show(BloqueoUnidad, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }

                        if (BloquearCarreta == 1)
                        {
                            DataTable dtBloqueoVehiculos2 = new DataTable();
                            int idVehiculo2;

                            dtBloqueoVehiculos2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ObtenerCodigoVehiculo(txtSemirremolque.Text);
                            idVehiculo2 = Convert.ToInt32(dtBloqueoVehiculos2.Rows[0]["IdVehiculo"]);

                            DataTable dtBloqueoUnidad2 = new DataTable();
                            string BloqueoUnidad2;

                            dtBloqueoUnidad2 = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(1, 0, 0, 0, idVehiculo2, "FALLA MECÁNICA", "MANTENIMIENTO", txtSolucion.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                                                                                             0, DateTime.Now.ToString(), DateTime.Now.ToString());

                            BloqueoUnidad2 = Convert.ToString(dtBloqueoUnidad2.Rows[0]["exito"]);
                            string NroRPTA3 = BloqueoUnidad2.Substring(0, 1);

                            if (NroRPTA3 == "0") { MessageBox.Show(BloqueoUnidad2, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            else { MessageBox.Show(BloqueoUnidad2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }

                        pFacturarFaltante.Visible = false;
                        pFacturarFaltante.SendToBack();
                        lblNroPregunta.Text = "1";
                        pAnalisisFallas.Visible = false;
                        pAnalisisFallas.SendToBack();
                        Limpiar();
                        ListarFallasMecanicas();
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSolucion.Focus();
                    }
                }
            }

            if (groupBox5.Enabled == true)
            {
                if (txtMontoComprobante.Text.Length == 0 || txtSolucion.Text.Length == 0)
                {
                    MessageBox.Show("Los campos no pueden estar vacíos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                    if (txtMontoComprobante.Text.Length == 0) { txtMontoComprobante.Focus(); }
                    else { txtSolucion.Focus(); }
                    return;
                }
                else
                {
                    int idFalla = Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID"));
                    string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                    decimal vacio = 0.00M;
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarSolucion(idFalla, txtNombre.Text, txtNumero.Text, Convert.ToInt32(cbxRecibo.SelectedValue),
                                  txtFactura.Text, Convert.ToDecimal(txtMontoComprobante.Text), txtTecnico.Text, txtPlacaTecnico.Text, vacio, vacio, vacio, vacio, Convert.ToInt32(cbxSistema.SelectedValue),
                                  Convert.ToInt32(cbxSubSistema.SelectedValue), txtSolucion.Text, Usuario, MttoCorrectivo);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (BloquearTracto == 1)
                        {
                            DataTable dtBloqueoVehiculos = new DataTable();
                            int idVehiculo;

                            dtBloqueoVehiculos = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ObtenerCodigoVehiculo(txtPlaca1.Text);
                            idVehiculo = Convert.ToInt32(dtBloqueoVehiculos.Rows[0]["IdVehiculo"]);

                            DataTable dtBloqueoUnidad = new DataTable();
                            string BloqueoUnidad;

                            dtBloqueoUnidad = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(1, 0, 0, 0, idVehiculo, "FALLA MECÁNICA", "MANTENIMIENTO", txtSolucion.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                                                                                            0, DateTime.Now.ToString(), DateTime.Now.ToString());

                            BloqueoUnidad = Convert.ToString(dtBloqueoUnidad.Rows[0]["exito"]);
                            string NroRPTA3 = BloqueoUnidad.Substring(0, 1);

                            if (NroRPTA3 == "0") { MessageBox.Show(BloqueoUnidad, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            else { MessageBox.Show(BloqueoUnidad, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }

                        if (BloquearCarreta == 1)
                        {
                            DataTable dtBloqueoVehiculos2 = new DataTable();
                            int idVehiculo2;

                            dtBloqueoVehiculos2 = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ObtenerCodigoVehiculo(txtSemirremolque.Text);
                            idVehiculo2 = Convert.ToInt32(dtBloqueoVehiculos2.Rows[0]["IdVehiculo"]);

                            DataTable dtBloqueoUnidad2 = new DataTable();
                            string BloqueoUnidad2;

                            dtBloqueoUnidad2 = clsMantenimientoBL.Instancia.GetMantenimiento_BloqueoUnidades(1, 0, 0, 0, idVehiculo2, "FALLA MECÁNICA", "MANTENIMIENTO", txtSolucion.Text, Utilitario.Instancia.SesionUsuario.usuario,
                                                                                                             0, DateTime.Now.ToString(), DateTime.Now.ToString());

                            BloqueoUnidad2 = Convert.ToString(dtBloqueoUnidad2.Rows[0]["exito"]);
                            string NroRPTA3 = BloqueoUnidad2.Substring(0, 1);

                            if (NroRPTA3 == "0") { MessageBox.Show(BloqueoUnidad2, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            else { MessageBox.Show(BloqueoUnidad2, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        
                        pFacturarFaltante.Visible = false;
                        pFacturarFaltante.SendToBack();
                        lblNroPregunta.Text = "1";
                        pAnalisisFallas.Visible = false;
                        pAnalisisFallas.SendToBack();
                        Limpiar();
                        ListarFallasMecanicas();
                    }
                    else
                    {
                        MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSolucion.Focus();
                    }
                }
            }
        }

        private void LiquidarFalla()
        {
            int idFalla = Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_LiquidarFallaMecanica(idFalla, Convert.ToDateTime(dtpFechaLiquidacion.Value), Convert.ToInt32(cbxEstadoLiquidacion.SelectedValue), Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pFacturarFaltante.Visible = false;
                pFacturarFaltante.SendToBack();
                ListarFallasMecanicas();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpFechaLiquidacion.Focus();
            }
        }

        private void VerSolucion()
        {
            DataTable dtListaSolucion = new DataTable();
            dtListaSolucion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucion(Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID")));
            if (dtListaSolucion.Rows.Count > 0)
            {
                for (int i = 0; i < dtListaSolucion.Rows.Count; i++)
                {
                    if (dtListaSolucion.Rows[i]["CLASE_SERVICIO"].ToString() == "GT TRANSPESA" || dtListaSolucion.Rows[i]["CLASE_SERVICIO"].ToString() == "TERCERO")
                    {
                        groupBox10.Enabled = false;
                        groupBox5.Enabled = false;
                        groupBox2.Enabled = false;
                        groupBox9.Enabled = false;
                        txtTecnico.Enabled = false;
                        txtPlacaTecnico.Enabled = false;
                        txtGalones.Enabled = false;

                        txtProgramacion.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "PROGRAMACION"));
                        txtFechaViaje.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_VIAJE"));
                        txtPlaca1.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "PLACA"));
                        txtSemirremolque.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "SEMIRREMOLQUE"));
                        txtConductor.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CONDUCTOR"));
                        txtRuta.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "RUTA"));

                        txtFechaIncidente.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_INICIO"));
                        txtFechaSalida.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_SALIDA"));
                        txtFechaLlegada.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_LLEGADA"));
                        txtFechaTermino.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_TÉRMINO"));
                        UCrea.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "UsuarioCreacion"));
                        UCreaFecha.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FechaCreacion"));
                        UModif.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "UltimoUsuario"));
                        UModifFecha.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "UltimaModificacion"));

                        txtTecnico.Text = dtListaSolucion.Rows[i]["CONDUCTOR_CAMIONETA"].ToString();
                        txtPlacaTecnico.Text = dtListaSolucion.Rows[i]["PLACA_CAMIONETA"].ToString();
                        txtFactura.Text = dtListaSolucion.Rows[i]["COMPROBANTE"].ToString();
                        cbxSistema.SelectedValue = Convert.ToInt32(dtListaSolucion.Rows[i]["SISTEMA_VEHÍCULO"]).ToString();
                        idSistema = Convert.ToInt32(dtListaSolucion.Rows[i]["SISTEMA_VEHÍCULO"]);
                        CargarComboSubSistema();
                        cbxSubSistema.SelectedValue = Convert.ToInt32(dtListaSolucion.Rows[i]["SUB_SISTEMA"]).ToString();
                        txtSolucion.Text = dtListaSolucion.Rows[i]["SOLUCIÓN"].ToString();
                        txtNombre.Text = dtListaSolucion.Rows[i]["NOMBRE"].ToString();
                        txtNumero.Text = dtListaSolucion.Rows[i]["NUMERO"].ToString();
                        cbxRecibo.SelectedValue = Convert.ToInt32(dtListaSolucion.Rows[i]["RECIBO"] is DBNull ? 1 : dtListaSolucion.Rows[i]["RECIBO"]).ToString();
                        cbxEstadoLiquidacion.SelectedValue = Convert.ToInt32(dtListaSolucion.Rows[i]["LIQUIDACION"]).ToString();

                        if (Convert.ToInt32(dtListaSolucion.Rows[i]["LIQUIDACION"]) == 1) { dtpFechaLiquidacion.Text = DateTime.Now.ToString(); }
                        else { dtpFechaLiquidacion.Text = Convert.ToDateTime(dtListaSolucion.Rows[i]["FECHA_LIQUIDACIÓN"]).ToString(); }

                        if (dtListaSolucion.Rows[i]["CLASE_SERVICIO"].ToString() == "GT TRANSPESA")
                        {
                            txtMonto.Text = Convert.ToDecimal(dtListaSolucion.Rows[i]["MONTO"]).ToString();
                            txtGalones.Text = Convert.ToDecimal(dtListaSolucion.Rows[i]["GALONES"]).ToString();
                            txtSoles.Text = Convert.ToDecimal(dtListaSolucion.Rows[i]["PRECIO_UNITARIO"]).ToString();
                            txtTotal.Text = Convert.ToDecimal(dtListaSolucion.Rows[i]["PRECIO_TOTAL"]).ToString();
                        }

                        if (dtListaSolucion.Rows[i]["CLASE_SERVICIO"].ToString() == "TERCERO")
                        { txtMontoComprobante.Text = Convert.ToDecimal(dtListaSolucion.Rows[i]["MONTO_COMPROBANTE"]).ToString(); }
                    }
                }
            }
        }

        private void AnularSolicitud()
        {
            if (MessageBox.Show("¿Desea eliminar esta solicitud de mantenimiento?", "ELIMINAR SOLICITUD", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Modificar(idSolicitud, 1, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarSolicitudes(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        /*
        private void Recepcionar()
        {
            int idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Modificar(idSolicitud, 2, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarSolicitudes();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        */

        private void ProgramarSolicitud()
        {
            try
            {
                frmSolicitudMantenimiento f1 = new frmSolicitudMantenimiento();
                f1.ListarSolicitud(Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud")));
                f1.idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
                f1.frmListarSolicitudes(this);

                f1.dtpFechaProgrI.Value = DateTime.Now;
                f1.dtpHoraProgrI.Value = DateTime.Now;

                f1.groupBox12.Enabled = false;
                f1.groupBox3.Enabled = false;
                f1.groupBox4.Enabled = false;
                f1.eliminarToolStripMenuItem.Enabled = true;
                f1.asignarOTToolStripMenuItem.Enabled = true;
                f1.terminarToolStripMenuItem.Enabled = true;
                f1.btnCancelar.Enabled = false;
                f1.btnAniadir.Enabled = false;
                f1.btnRegistrar.Enabled = false;
                f1.btnProgramar.Enabled = true;
                f1.btnTerminar.Enabled = false;
                f1.Visible = true;
            }
            catch { MessageBox.Show("La fila seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void ReprogramarSolicitud()
        {
            try
            {
                frmSolicitudMantenimiento f1 = new frmSolicitudMantenimiento();
                f1.ListarSolicitud(Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud")));
                f1.idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
                f1.frmListarSolicitudes(this);

                f1.dtpFechaReprog.Value = DateTime.Now;
                f1.dtpHoraReprog.Value = DateTime.Now;

                f1.groupBox12.Enabled = false;
                f1.groupBox3.Enabled = false;
                f1.groupBox1.Enabled = false;
                f1.eliminarToolStripMenuItem.Enabled = true;
                f1.asignarOTToolStripMenuItem.Enabled = true;
                f1.terminarToolStripMenuItem.Enabled = true;
                f1.btnCancelar.Enabled = false;
                f1.btnAniadir.Enabled = false;
                f1.btnRegistrar.Enabled = false;
                f1.btnProgramar.Enabled = true;
                f1.btnTerminar.Enabled = false;
                f1.Visible = true;
            }
            catch { MessageBox.Show("La fila seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void TerminarSolicitud()
        {
            try
            {
                frmSolicitudMantenimiento f1 = new frmSolicitudMantenimiento();
                f1.ListarSolicitud(Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud")));
                f1.idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
                f1.frmListarSolicitudes(this);

                f1.dtpFechaEntrega.Value = DateTime.Now;
                f1.dtpHoraEntrega.Value = DateTime.Now;

                f1.groupBox12.Enabled = false;
                f1.groupBox1.Enabled = false;
                f1.groupBox4.Enabled = false;
                f1.eliminarToolStripMenuItem.Enabled = true;
                f1.asignarOTToolStripMenuItem.Enabled = true;
                f1.terminarToolStripMenuItem.Enabled = true;
                f1.btnCancelar.Enabled = false;
                f1.btnAniadir.Enabled = false;
                f1.btnRegistrar.Enabled = false;
                f1.btnProgramar.Enabled = false;
                f1.btnTerminar.Enabled = true;
                f1.Visible = true;
            }
            catch { MessageBox.Show("La fila seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void InsertarSistema()
        {
            if (txtNuevoSistema.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingrese un sistema.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNuevoSistema.Focus();
                return;
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarSistema(txtNuevoSistema.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pNuevo.Visible = false;
                    pNuevo.SendToBack();
                    CargarComboSistema();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNuevoSistema.Focus();
                }
            }
        }

        public void Limpiar()
        {
            dtpFechaSalida.Value = DateTime.Now;
            dtpHoraSalida.Value = DateTime.Now;

            idSistema = 3;
            idFalla = -1;
            CargarComboSistema();
            CargarComboSubSistema();

            txtTecnico.Clear();
            txtPlacaTecnico.Clear();
            txtMonto.Clear();
            txtFactura.Clear();
            txtMontoComprobante.Clear();
            txtSolucion.Clear();
            txtGalones.Clear();
            txtSoles.Clear();
            txtTotal.Clear();
            txtNombre.Clear();
            txtNumero.Clear();

            cbMttoCorrectivo.Checked = false;
            MttoCorrectivo = 0;
            cbBloquearT.Checked = false;
            BloquearTracto = 0;
            cbBloquearC.Checked = false;
            BloquearCarreta = 0;
        }

        public void InsertarMarcador(Word.Document doc, string MarcadorNombre, string Valor)
        {
            if (doc.Bookmarks.Exists(MarcadorNombre))
            {
                Word.Range r = doc.Bookmarks[MarcadorNombre].Range;
                r.Text = Valor ?? "";

                doc.Bookmarks.Add(MarcadorNombre, r);
            }
        }

        public void InsertarImagenMarcador(Word.Document doc, string MarcadorNombre, PictureBox pb)
        {
            if (pb.Image == null) return;
            if (!doc.Bookmarks.Exists(MarcadorNombre)) return;

            string tempPath = Path.Combine(Path.GetTempPath(), MarcadorNombre + ".jpg");
            pb.Image.Save(tempPath, System.Drawing.Imaging.ImageFormat.Jpeg);

            Word.Range r = doc.Bookmarks[MarcadorNombre].Range;
            Word.InlineShape pic = r.InlineShapes.AddPicture(tempPath);
            pic.Width = 250;
            pic.Height = 150;

            Word.Range newRange = pic.Range;
            doc.Bookmarks.Add(MarcadorNombre, newRange);
        }


        private void rbListaTodas_Click(object sender, EventArgs e)
        {
            valorEstado = 0;
            dtListaFallasMecanicas = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarFallas(txtPlaca.Text, fechaInicio.Text, fechaFin.Text, valorEstado, cbxValidacionB.Text, cbxOperacion2.Text);
        }

        private void rbListaPendientes_Click(object sender, EventArgs e)
        {
            valorEstado = 1;
            dtListaFallasMecanicas = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarFallas(txtPlaca.Text, fechaInicio.Text, fechaFin.Text, valorEstado, cbxValidacionB.Text, cbxOperacion2.Text);
        }

        private void rbListaSolucionadas_Click(object sender, EventArgs e)
        {
            valorEstado = 2;
            dtListaFallasMecanicas = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarFallas(txtPlaca.Text, fechaInicio.Text, fechaFin.Text, valorEstado, cbxValidacionB.Text, cbxOperacion2.Text);
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarFallasMecanicas(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaFallasMecanicas.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Fallas Mecánicas - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaFallasMecanicas.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnExcelS_Click(object sender, EventArgs e)
        {
            if (dtgListaSolicitud.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Reporte de Solicitudes de Mantenimiento - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaSolicitud.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        public void btnBuscarS_Click(object sender, EventArgs e) { ListarSolicitudes(); }

        private void btnEstadoUnidades_Click(object sender, EventArgs e)
        {
            frmEstadoUnidades frmEstadoUnidades = new frmEstadoUnidades();
            frmEstadoUnidades._frmLista = this;
            frmEstadoUnidades.ShowDialog();
        }

        private void btnUbicacionFlota_Click(object sender, EventArgs e)
        {
            frmUbicacionFlota frmUbicacionFlota = new frmUbicacionFlota();
            frmUbicacionFlota.ShowDialog();
        }

        private void btnAgenda_Click(object sender, EventArgs e)
        {
            frmAgendaContactos frmAgendaContactos = new frmAgendaContactos();
            frmAgendaContactos.ListarContactos();
            frmAgendaContactos.ShowDialog();
        }

        private void btnComponentes_Click(object sender, EventArgs e)
        {
            frmComponentesDefectuosos frmComponentesDefectuosos = new frmComponentesDefectuosos();
            frmComponentesDefectuosos.ShowDialog();
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            frmAnalisisFallas frmComponentesDefectuosos = new frmAnalisisFallas();
            frmComponentesDefectuosos.ShowDialog();
        }

        private void dgvListaSolicitudView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (e.CellValue.ToString() == "SOLICITADO") { e.Appearance.BackColor = Color.FromArgb(0, 213, 255); }

                if (e.CellValue.ToString() == "RECEPCIONADO") { e.Appearance.BackColor = Color.FromArgb(255, 255, 0); }

                if (e.CellValue.ToString() == "PROGRAMADO") { e.Appearance.BackColor = Color.FromArgb(255, 151, 0); }

                if (e.CellValue.ToString() == "REPROGRAMADO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (e.CellValue.ToString() == "COMPLETADO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void editarFallaMecánicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListarFallaEditar(sender, e);
            
            string CodAuxilio = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CODIGO"));
            label18.Text = "EDITAR AUXILIO: " + CodAuxilio;
            cbxValidacion.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "VALIDACION"));

            pEditarFalla.Visible = true;
            pEditarFalla.BringToFront();
        }

        private void cerrarFallaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ClaseServicio = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CLASE_SERVICIO"));

            if (ClaseServicio != "TERCERO")
            {
                rbFechaSalidaGT.Checked = true;
                rbFechaSalidaGT_Click(sender, e);
            }
            else
            {
                rbFechaSalidaTerceros.Checked = true;
                rbFechaSalidaTerceros_Click(sender, e);
            }

            cbxEstadoAuxilio.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ESTADO_AUXILIO"));
            cbxEstadoAuxilio_DropDownClosed(sender, e);

            pCerrarFalla.Visible = true;
            pCerrarFalla.BringToFront();
            dtpFechaSalida.Focus();
        }

        private void tsGenerarReporte_Click(object sender, EventArgs e)
        {
            DataTable dtListaSolucion = new DataTable();
            int ID = Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID"));
            string Servicio = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CLASE_SERVICIO"));
            dtListaSolucion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarSolucionAuxilio(ID, Servicio);

            if (dtListaSolucion.Rows.Count > 0)
            {
                string plantilla = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FORMATO DE AUXILIOS.docx");

                Word.Application wordApp = new Word.Application();
                wordApp.Visible = false;
                Word.Document doc = null;

                try
                {
                    doc = wordApp.Documents.Open(plantilla);
                    InsertarMarcador(doc, "NroTicket", dtListaSolucion.Rows[0]["NroTicket"].ToString());
                    InsertarMarcador(doc, "Programacion", dtListaSolucion.Rows[0]["Programacion"].ToString());
                    InsertarMarcador(doc, "NumeroPlaca", dtListaSolucion.Rows[0]["NumeroPlaca"].ToString());
                    InsertarMarcador(doc, "Semirremolque", dtListaSolucion.Rows[0]["Semirremolque"].ToString());
                    InsertarMarcador(doc, "UnidadAfectada", dtListaSolucion.Rows[0]["UnidadAfectada"].ToString());
                    InsertarMarcador(doc, "FechaViaje", dtListaSolucion.Rows[0]["FechaViaje"].ToString());
                    InsertarMarcador(doc, "Conductor", dtListaSolucion.Rows[0]["Conductor"].ToString());
                    InsertarMarcador(doc, "Ruta", dtListaSolucion.Rows[0]["Ruta"].ToString());
                    InsertarMarcador(doc, "TipoFalla", dtListaSolucion.Rows[0]["TipoFalla"].ToString());
                    InsertarMarcador(doc, "Motivo", dtListaSolucion.Rows[0]["Motivo"].ToString());
                    InsertarMarcador(doc, "Ubicacion", dtListaSolucion.Rows[0]["Ubicacion"].ToString());
                    InsertarMarcador(doc, "FechaInicio", dtListaSolucion.Rows[0]["FechaInicio"].ToString());
                    InsertarMarcador(doc, "ClaseServicio", dtListaSolucion.Rows[0]["ClaseServicio"].ToString());
                    InsertarMarcador(doc, "SistemaVehiculo", dtListaSolucion.Rows[0]["SistemaVehiculo"].ToString());
                    InsertarMarcador(doc, "Solucion", dtListaSolucion.Rows[0]["Solucion"].ToString());
                    InsertarMarcador(doc, "FechaTermino", dtListaSolucion.Rows[0]["FechaTermino"].ToString());
                    InsertarMarcador(doc, "Duracion", dtListaSolucion.Rows[0]["Duracion"].ToString());

                    if (Servicio == "GT TRANSPESA")
                    {
                        InsertarMarcador(doc, "Tecnico", dtListaSolucion.Rows[0]["Tecnico"].ToString());
                        InsertarMarcador(doc, "PlacaTecnico", dtListaSolucion.Rows[0]["PlacaTecnico"].ToString());
                        InsertarMarcador(doc, "FechaSalida", dtListaSolucion.Rows[0]["FechaSalida"].ToString());
                        InsertarMarcador(doc, "FechaLlegada", dtListaSolucion.Rows[0]["FechaLlegada"].ToString());
                        InsertarMarcador(doc, "Galones", dtListaSolucion.Rows[0]["Galones"].ToString());
                        InsertarMarcador(doc, "PrecioTotal", dtListaSolucion.Rows[0]["PrecioTotal"].ToString());
                        InsertarMarcador(doc, "Monto", dtListaSolucion.Rows[0]["Monto"].ToString());
                    }
                    else
                    {
                        InsertarMarcador(doc, "NombreTercero", dtListaSolucion.Rows[0]["NombreTercero"].ToString());
                        InsertarMarcador(doc, "TelefonoTercero", dtListaSolucion.Rows[0]["TelefonoTercero"].ToString());
                        InsertarMarcador(doc, "FechaLlegada", dtListaSolucion.Rows[0]["FechaLlegada"].ToString());
                        InsertarMarcador(doc, "TipoRecibo", dtListaSolucion.Rows[0]["TipoRecibo"].ToString());
                        InsertarMarcador(doc, "Comprobante", dtListaSolucion.Rows[0]["Comprobante"].ToString());
                        InsertarMarcador(doc, "MontoComprobante", dtListaSolucion.Rows[0]["MontoComprobante"].ToString());
                    }

                    if (dtListaSolucion.Rows[0]["Imagen"].ToString() != "")
                    {
                        Byte[] byteBLOBData;
                        byteBLOBData = (Byte[])dtListaSolucion.Rows[0]["Imagen"];
                        Image x = (Bitmap)((new ImageConverter()).ConvertFrom(byteBLOBData));
                        pbFalla.Image = x;
                        pbFalla.SizeMode = PictureBoxSizeMode.StretchImage;
                        InsertarImagenMarcador(doc, "Imagen", pbFalla);
                    }

                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string destino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "ReporteAuxilio_" + Utilitario.Instancia.SesionUsuario.usuario + "_" + DateTime.Now.ToString("T", dtfi) + ".docx");
                    doc.SaveAs2(destino);
                    doc.Close();
                    wordApp.Quit();
                    MessageBox.Show("Documento generado:\n" + destino, "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start(destino);
                }
                catch (Exception ex)
                {
                    if (doc != null) { doc.Close(false); }
                    wordApp.Quit(false);
                    MessageBox.Show("Error exportando a Word: " + ex.Message);
                }
            }
        }

        private void asignarSolucionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Limpiar();
            txtProgramacion.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "PROGRAMACION"));
            txtFechaViaje.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_VIAJE"));
            txtPlaca1.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "PLACA"));
            txtSemirremolque.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "SEMIRREMOLQUE"));
            txtConductor.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CONDUCTOR"));
            txtRuta.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "RUTA"));

            txtFechaIncidente.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_INICIO"));
            txtFechaSalida.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_SALIDA"));
            txtFechaLlegada.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_LLEGADA"));
            txtFechaTermino.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_TÉRMINO"));
            UCrea.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "UsuarioCreacion"));
            UCreaFecha.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FechaCreacion"));
            UModif.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "UltimoUsuario"));
            UModifFecha.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "UltimaModificacion"));

            idFalla = Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID"));

            groupBox2.Enabled = true;
            groupBox10.Enabled = true;
            rbGrupoTranspesa.Checked = true;
            rbGrupoTranspesa_Click(sender, e);
            btnAgregar.Enabled = true;
            btnCancelar.Enabled = true;

            if (txtSemirremolque.Text.Length == 0)
            {
                cbBloquearC.Checked = false;
                BloquearCarreta = 0;
                cbBloquearC.Enabled = false;
            }
            else
            {
                cbBloquearC.Checked = false;
                BloquearCarreta = 0;
                cbBloquearC.Enabled = true;
            }

            pFacturarFaltante.Visible = true;
            pFacturarFaltante.BringToFront();
        }

        private void liquidarFallaMecanicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string estado2 = dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CLASE_SERVICIO").ToString();

            Limpiar();
            pFacturarFaltante.Visible = true;
            pFacturarFaltante.BringToFront();
            btnAgregar.Enabled = false;
            btnCancelar.Enabled = false;

            if (estado2 == "GT TRANSPESA")
            {
                rbGrupoTranspesa.Checked = true;
                btnSeleccionar.Enabled = true;
            }

            if (estado2 == "TERCERO")
            {
                rbTerceros.Checked = true;
                btnSeleccionar.Enabled = false;
            }

            VerSolucion();
            groupBox9.Enabled = true;
        }

        private void eliminarFallaToolStripMenuItem_Click(object sender, EventArgs e) { EliminarFalla(); }

        private void agregarFechaEstimadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpcionFecha = 1;
            label61.Text = "FECHA DE ESTIMACIÓN";
            pFechaEstimada.Visible = true;
            pFechaEstimada.BringToFront();
        }

        private void recepcionarUnidadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpcionU = 1;
            CargarComboBase2(2);
            cbxBase2.Text = "GT TRU (MTTO)";
            cbxBase2_DropDownClosed(sender, e);

            label77.Text = "RECEPCIÓN DE UNIDAD";
            label78.Visible = false;
            txtNuevaUbicacion.Clear();
            txtNuevaUbicacion.Visible = false;
            btnGuardarUbicacion.Visible = false;
            
            pAsignacionUnidad.Visible = true;
            pAsignacionUnidad.BringToFront();
        }

        private void tsCambiarUbicacion_Click(object sender, EventArgs e)
        {
            OpcionU = 2;
            CargarComboBase2(2);
            cbxBase2.Text = dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "UBICACIÓN").ToString();
            cbxBase2_DropDownClosed(sender, e);
            txtUbicacionTaller.Text = dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "TALLER").ToString();
            dtpFechaRecepcion.Value = Convert.ToDateTime(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "FECHA_RECEPCION"));
            dtpHoraRecepcion.Value = Convert.ToDateTime(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "FECHA_RECEPCION"));

            label77.Text = "CAMBIAR UBICACIÓN";
            label78.Visible = false;
            txtNuevaUbicacion.Clear();
            txtNuevaUbicacion.Visible = false;
            btnGuardarUbicacion.Visible = false;

            pAsignacionUnidad.Visible = true;
            pAsignacionUnidad.BringToFront();
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pAsignacionUnidad.Visible = false;
            pAsignacionUnidad.SendToBack();
            dtpFechaRecepcion.Value = DateTime.Now;
            dtpHoraRecepcion.Value = new DateTime(dtpHoraEstimada.Value.Year, dtpHoraEstimada.Value.Month, 1, 0, 0, 0);
            txtUbicacionTaller.Clear();
            txtNuevaUbicacion.Clear();
        }

        private void cbxBase2_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxBase2.Text == "GT TRU (MTTO)" || cbxBase2.Text == "GT TRU (BRA)" || cbxBase2.Text == "GT LIMA (MTTO)")
            {
                txtUbicacionTaller.Enabled = true;
                txtUbicacionTaller.Clear();
            }
            else
            {
                txtUbicacionTaller.Enabled = false;
                txtUbicacionTaller.Clear();
            }
        }

        private void btnAgregarUbicacion_Click(object sender, EventArgs e)
        {
            if (txtNuevaUbicacion.Visible == false)
            {
                label78.Visible = true;
                txtNuevaUbicacion.Clear();
                txtNuevaUbicacion.Visible = true;
                btnGuardarUbicacion.Visible = true;
            }
            else
            {
                label78.Visible = false;
                txtNuevaUbicacion.Clear();
                txtNuevaUbicacion.Visible = false;
                btnGuardarUbicacion.Visible = false;
            }
        }

        private void pAsignacionUnidad_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pAsignacionUnidad.Left = pAsignacionUnidad.Left + (e.X - xClick2);
                pAsignacionUnidad.Top = pAsignacionUnidad.Top + (e.Y - yClick2);
            }
        }

        private void dtpFechaRecepcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraRecepcion.Focus(); }
        }

        private void dtpHoraRecepcion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { cbxBase2.Focus(); }
        }

        private void txtUbicacionTaller_Enter(object sender, EventArgs e) { txtUbicacionTaller.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtUbicacionTaller_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTaller, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarTalleres(txtUbicacionTaller.Text), true, false, false);
            lstTaller.Columns[0].Width = 0;
            lstTaller.Columns[1].Width = 90;
            lstTaller.BringToFront();
            lstTaller.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTaller.Visible = false;
                lstTaller.SendToBack();
            }
        }

        private void txtUbicacionTaller_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTaller.Focus(); }
        }

        private void txtUbicacionTaller_Leave(object sender, EventArgs e) { txtUbicacionTaller.BackColor = Color.White; }

        private void lstTaller_Enter(object sender, EventArgs e)
        {
            if (!lstTaller.Items.Count.Equals(0)) { lstTaller.Items[0].Selected = true; } 
        }

        private void lstTaller_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTaller.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTaller.SelectedItems[0];
                txtUbicacionTaller.Text = ItemActual.SubItems[1].Text;

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres(txtUbicacionTaller.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { btnFechaAsignacion.Focus(); }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUbicacionTaller.Clear();
                    txtUbicacionTaller.Focus();
                }

                lstTaller.Visible = false;
                lstTaller.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTaller.Visible = false;
                lstTaller.SendToBack();
            }
        }

        private void lstTaller_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTaller.SelectedItems[0];
            txtUbicacionTaller.Text = ItemActual.SubItems[1].Text;

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres(txtUbicacionTaller.Text);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0") { btnFechaAsignacion.Focus(); }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUbicacionTaller.Clear();
                txtUbicacionTaller.Focus();
            }

            lstTaller.Visible = false;
            lstTaller.SendToBack();
        }

        private void txtNuevaUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardarUbicacion_Click(sender, e); }
        }

        private void btnGuardarUbicacion_Click(object sender, EventArgs e)
        {
            if (txtNuevaUbicacion.Text.Length == 0)
            {
                MessageBox.Show("Por favor ingrese una ubicación.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtNuevaUbicacion.Focus();
                return;
            }
            else
            {
                if (MessageBox.Show("¿Desea ingresar esta nueva ubicación?", "AGREGAR NUEVA UBICACIÓN", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    DataTable dtRespuesta = new DataTable();
                    string Respuesta;
                    dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarUbicacion(txtNuevaUbicacion.Text);
                    Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                    string NroRPTA = Respuesta.Substring(0, 1);
                    if (NroRPTA == "0")
                    {
                        MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarComboBase2(2);
                        label78.Visible = false;
                        txtNuevaUbicacion.Visible = false;
                        btnGuardarUbicacion.Visible = false;
                        txtNuevaUbicacion.Clear();
                    }
                    else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void btnFechaAsignacion_Click(object sender, EventArgs e)
        {
            int idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
            string FechaRecepcion = dtpFechaRecepcion.Text + ' ' + dtpHoraRecepcion.Text;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dtRespuesta = new DataTable();
            string Respuesta;

            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarFechaRecepcion(OpcionU, idSolicitud, Convert.ToDateTime(FechaRecepcion),
                                                       Convert.ToInt32(cbxBase2.SelectedValue), txtUbicacionTaller.Text, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnCerrar2_Click(sender, e);
                ListarSolicitudes();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        
        private void solicitudDeLogísticaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string EPedido2 = dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "ESTADO_REPUESTO").ToString();
            int idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarPedido(idSolicitud);
            if (dtRespuesta.Rows.Count > 0)
            {
                txtRequerimiento.Text = dtRespuesta.Rows[0]["Requerimiento"].ToString();
                txtRepuesto.Text = dtRespuesta.Rows[0]["Descripcion"].ToString();
                txtFechaSolicitada.Text = dtRespuesta.Rows[0]["FechaPedido"].ToString();

                if (EPedido2 == "ENTREGADO") { txtFLlegada.Text = dtRespuesta.Rows[0]["FechaLlegadaP"].ToString(); }
            }
            
            if (EPedido2 == "")
            {
                groupBox16.Enabled = true;
                groupBox17.Enabled = false;
                btnEliminarLogistica.Enabled = false;
            }

            if (EPedido2 == "PEDIDO")
            {
                groupBox16.Enabled = true;
                groupBox17.Enabled = true;
                btnEliminarLogistica.Enabled = true;
            }

            if (EPedido2 == "ENTREGADO")
            {
                groupBox16.Enabled = false;
                groupBox17.Enabled = false;
                btnEliminarLogistica.Enabled = false;
            }

            pLogistica.Visible = true;
            pLogistica.BringToFront();
        }

        private void programarSolicitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ES2 = dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "ESTADO").ToString();

            if(ES2 == "RECEPCIONADO") { ProgramarSolicitud(); }

            if (ES2 == "PROGRAMADO" || ES2 == "REPROGRAMADO") { ReprogramarSolicitud(); }
        }

        private void anularSolicitudToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { AnularSolicitud(); }
            catch { MessageBox.Show("La solicitud seleccionada no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void terminarMantenimientoToolStripMenuItem_Click(object sender, EventArgs e) { TerminarSolicitud(); }

        private void rbGrupoTranspesa_Click(object sender, EventArgs e)
        {
            if (rbGrupoTranspesa.Checked == true)
            {
                txtTecnico.Enabled = true;
                txtPlacaTecnico.Enabled = true;
                txtGalones.Enabled = true;
                groupBox6.Enabled = true;
                groupBox5.Enabled = false;
                groupBox9.Enabled = false;
                btnSeleccionar.Enabled = true;

                DataTable dtListaOC = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPrecioPetroleo();
                if (dtListaOC.Rows.Count > 0)
                {
                    for (int i = 0; i < dtListaOC.Rows.Count; i++) { txtSoles.Text = dtListaOC.Rows[i]["PrecioUnitario"].ToString(); }
                }
                dtpFechaLiquidacion.Value = DateTime.Now;
                txtGalones.Text = Convert.ToString(0);
                txtTotal.Text = Convert.ToString(0);
                txtTecnico.Focus();

                txtNombre.Clear();
                txtNumero.Clear();
                txtFactura.Clear();
                txtMontoComprobante.Clear();

            }
        }

        private void rbTerceros_Click(object sender, EventArgs e)
        {
            if (rbTerceros.Checked == true)
            {
                groupBox6.Enabled = false;
                groupBox5.Enabled = true;
                btnSeleccionar.Enabled = false;

                txtTecnico.Clear();
                txtPlacaTecnico.Clear();
                txtMonto.Clear();
                txtGalones.Clear();
                txtSoles.Clear();
                txtTotal.Clear();
                txtNombre.Focus();
            }
        }

        private void rbFechaSalidaGT_Click(object sender, EventArgs e)
        {
            valorSalida = 1;
            cbxEstadoAuxilio.Items.Clear();
            cbxEstadoAuxilio.Items.AddRange(new object[] { "EN COORDINACIÓN", "EN RUTA", "EN ATENCIÓN", "ATENDIDO" });
            cbxEstadoAuxilio.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ESTADO_AUXILIO"));
            cbxEstadoAuxilio_DropDownClosed(sender, e);
        }

        private void rbFechaSalidaTerceros_Click(object sender, EventArgs e)
        {
            valorSalida = 0;
            cbxEstadoAuxilio.Items.Clear();
            cbxEstadoAuxilio.Items.AddRange(new object[] { "EN COORDINACIÓN", "EN ATENCIÓN", "ATENDIDO" });
            cbxEstadoAuxilio.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ESTADO_AUXILIO"));
            cbxEstadoAuxilio_DropDownClosed(sender, e);
        }

        private void rbTodas_Click(object sender, EventArgs e)
        {
            if (rbTodas.Checked == true) { EstadoSolicitud = " "; }
        }

        private void rbSolicitadas_Click(object sender, EventArgs e)
        {
            if (rbSolicitadas.Checked == true) { EstadoSolicitud = "SOLICITADO"; }
        }

        private void rbRecepcionadas_Click(object sender, EventArgs e)
        {
            if (rbRecepcionadas.Checked == true) { EstadoSolicitud = "RECEPCIONADO"; }
        }

        public void rbProgramadas_Click(object sender, EventArgs e)
        {
            if (rbProgramadas.Checked == true) { EstadoSolicitud = "PROGRAMADO"; }
        }

        private void rbCompletadas_Click(object sender, EventArgs e)
        {
            if (rbCompletadas.Checked == true) { EstadoSolicitud = "COMPLETADO"; }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pFacturarFaltante.Visible = false;
            pFacturarFaltante.SendToBack();
            lblNroPregunta.Text = "1";
            idFalla = -1;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pEditarFalla.Visible = false;
            pEditarFalla.SendToBack();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pCerrarFalla.Visible = false;
            pCerrarFalla.SendToBack();
            dtpFechaSalida.Value = DateTime.Now;
            dtpHoraSalida.Value = DateTime.Now;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            txtRequerimiento.Clear();
            txtRepuesto.Clear();
            txtFechaSolicitada.Clear();
            txtFLlegada.Clear();
            pLogistica.Visible = false;
            pLogistica.SendToBack();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pNuevo.Visible = true;
            pNuevo.BringToFront();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pNuevo.Visible = false;
            pNuevo.SendToBack();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pFechaEstimada.Visible = false;
            pFechaEstimada.SendToBack();
            dtpFechaEstimada.Value = DateTime.Now;
            dtpHoraEstimada.Value = new DateTime(dtpHoraEstimada.Value.Year, dtpHoraEstimada.Value.Month, 1, 0, 0, 0);
        }

        private void btnCerrarFalla_Click(object sender, EventArgs e) { CerrarFalla(); }

        private void btnModificar_Click(object sender, EventArgs e) { EditarFalla(); }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            DataTable dtRespuesta = new DataTable();
            string Respuesta;

            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_CrearAnalisisFallas(idFalla, txtPlaca1.Text, txtSemirremolque.Text, txtProgramacion.Text, Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0")
            {
                lblNroPregunta.Text = "1";
                lblPregunta.Text = "Por qué " + txtSolucion.Text;

                pAnalisisFallas.Visible = true;
                pAnalisisFallas.BringToFront();
            }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSolucion.Focus();
            }
        }

        private void btnGuardarRespuesta_Click(object sender, EventArgs e)
        {
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            DataTable dtRespuesta = new DataTable();
            string Respuesta;

            if (Convert.ToInt32(lblNroPregunta.Text) != 4)
            {
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarAnalisisFallas(idFalla, Convert.ToInt32(lblNroPregunta.Text), txtRespuesta.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    lblPregunta.Text = "Por qué " + txtRespuesta.Text;
                    lblNroPregunta.Text = Convert.ToString(Convert.ToInt32(lblNroPregunta.Text) + 1);
                    txtRespuesta.Clear();
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRespuesta.Focus();
                }
            }
            else
            {
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_InsertarAnalisisFallas(idFalla, Convert.ToInt32(lblNroPregunta.Text), txtRespuesta.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);

                if (NroRPTA == "0")
                {
                    pAnalisisFallas.Visible = false;
                    pAnalisisFallas.SendToBack();
                    txtRespuesta.Clear();
                    lblNroPregunta.Text = "1";
                    InsertarSolucion(); 
                }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtRespuesta.Focus();
                }
            }
        }

        private void btnCerrar4_Click(object sender, EventArgs e)
        {
            pAnalisisFallas.Visible = false;
            pAnalisisFallas.SendToBack();
            txtRespuesta.Clear();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();

            if(rbGrupoTranspesa.Checked == true)
            {
                DataTable dtListaOC = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPrecioPetroleo();
                if (dtListaOC.Rows.Count > 0)
                {
                    for (int i = 0; i < dtListaOC.Rows.Count; i++) { txtSoles.Text = dtListaOC.Rows[i]["PrecioUnitario"].ToString(); }
                }
                txtGalones.Text = Convert.ToString(0);
                txtTotal.Text = Convert.ToString(0);
            }
        }

        private void btnLiquidar_Click(object sender, EventArgs e) { LiquidarFalla(); }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            frmCalcularMonto frmCalcularMonto = new frmCalcularMonto();
            frmCalcularMonto.EnviarFalla(Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID")), dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CONDUCTOR_CAMIONETA").ToString());
            if (frmCalcularMonto.ShowDialog() == System.Windows.Forms.DialogResult.OK) { txtMonto.Text = frmCalcularMonto.txtTotal.Text; }
        }

        private void txtNuevoSistema_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { InsertarSistema(); }
        }

        private void btnAgregarSistema_Click(object sender, EventArgs e) { InsertarSistema(); }

        private void cbxRecibo_DropDownClosed(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbxRecibo.SelectedValue) == 1)
            {
                txtFactura.Enabled = false;
                txtFactura.Clear();
                txtMontoComprobante.Focus();
            }
            else
            {
                if (Convert.ToInt32(cbxRecibo.SelectedValue) == 2 || Convert.ToInt32(cbxRecibo.SelectedValue) == 3 || Convert.ToInt32(cbxRecibo.SelectedValue) == 5)
                {
                    txtFactura.Enabled = true;
                    txtFactura.Focus();
                }
            }
        }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarFallasMecanicas(); }
        }

        private void fechaInicio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarFallasMecanicas(); }
        }

        private void fechaFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarFallasMecanicas(); }
        }

        private void cbxValidacionB_DropDownClosed(object sender, EventArgs e) { ListarFallasMecanicas(); }

        private void cbxOperacion2_DropDownClosed(object sender, EventArgs e) { ListarFallasMecanicas(); }

        private void txtPlacaS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarSolicitudes(); }
        }

        private void FechaProgIni_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarSolicitudes(); }
        }

        private void FechaProgFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarSolicitudes(); }
        }

        private void cbxBase_DropDownClosed(object sender, EventArgs e) { ListarSolicitudes(); }

        private void cbxOperacion_DropDownClosed(object sender, EventArgs e) { ListarSolicitudes(); }

        private void txtMotivo_Act_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtUbicacion_Act.Focus(); }
        }

        private void txtUbicacion_Act_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { EditarFalla(); }
        }

        private void lstPersona_Enter(object sender, EventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtTecnico, ref lstPersona, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtTecnico, ref lstPersona, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte))
                {
                    if (esVALE == "SI") { txtPlacaTecnico.Focus(); }
                    else { txtTecnico.Select(); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPersona_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtTecnico, ref lstPersona, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem PersonaActual;
            PersonaActual = lstPersona.SelectedItems[0];
            txtTecnico.Text = PersonaActual.SubItems[1].Text;
            lstPersona.Visible = false;
            txtPlacaTecnico.Focus();
        }

        private void txtTecnico_Enter(object sender, EventArgs e) { txtTecnico.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtTecnico_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtTecnico, ref lstPersona, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtTecnico_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtTecnico, ref  lstPersona, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPersonalTransporte))
                {
                    if (esVALE == "SI") { txtPlacaTecnico.Focus(); }
                    else { txtTecnico.Select(); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtPlacaTecnico.Focus(); }
        }

        private void txtTecnico_Leave(object sender, EventArgs e) { txtTecnico.BackColor = Color.White; }

        private void lstPlaca_Enter(object sender, EventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, null, e, ref txtPlacaTecnico, ref lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoListView(sender, e, null, null, ref txtPlacaTecnico, ref lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte))
                {
                    if (esVALE == "SI") { txtMonto.Focus(); }
                    else { txtPlacaTecnico.Select(); }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
     
        private void lstPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            try
            { Utilitario.Instancia.AutoCompletadoListView(sender, null, e, null, ref txtPlacaTecnico, ref lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte); }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void lstPlaca_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem PlacaActual;
            PlacaActual = lstPlaca.SelectedItems[0];
            txtPlacaTecnico.Text = PlacaActual.SubItems[0].Text;
            lstPlaca.Visible = false;
            txtGalones.Focus();
        }

        private void txtPlacaTecnico_Enter(object sender, EventArgs e) { txtPlacaTecnico.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtPlacaTecnico_Leave(object sender, EventArgs e) { txtPlacaTecnico.BackColor = Color.White; }

        private void txtPlacaTecnico_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Utilitario.Instancia.AutoCompletadoTexBox(sender, e, null, null, ref txtPlacaTecnico, ref lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte))
                {
                    lstPlaca.Columns[1].Width = -1;
                    lstPlaca.Size = new System.Drawing.Size(90, 150);

                    if (esVALE == "SI") { txtMonto.Focus(); }
                    else { txtPlacaTecnico.Select(); }
                }
            }
            catch (Exception ex) { }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtGalones.Focus(); }
        }

        private void txtPlacaTecnico_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                Utilitario.Instancia.AutoCompletadoTexBox(sender, null, e, null, ref txtPlacaTecnico, ref lstPlaca, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_FallasMecanicas_ListarPlacasTransporte);
                lstPlaca.Columns[1].Width = -1;
                lstPlaca.Size = new System.Drawing.Size(90, 150);
            }
            catch (Exception ex) { } 
        }

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtSolucion.Focus(); }
        }

        private void txtMontoComprobante_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtSolucion.Focus(); }
        }

        private void txtGalones_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtSoles.Focus(); }
        }

        private void txtSoles_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtSolucion.Focus(); }
        }

        private void txtGalones_Leave(object sender, EventArgs e)
        {
            if (txtGalones.Text == "")
            {
                txtGalones.Text = Convert.ToString(0);
                txtTotal.Text = Math.Round((Convert.ToDecimal(txtGalones.Text) * (Convert.ToDecimal(txtSoles.Text) * Convert.ToDecimal(1.18))),2).ToString();
            }
            else
            { txtTotal.Text = Math.Round((Convert.ToDecimal(txtGalones.Text) * (Convert.ToDecimal(txtSoles.Text) * Convert.ToDecimal(1.18))),2).ToString(); }
        }

        private void txtSoles_Leave(object sender, EventArgs e)
        {
            if (txtSoles.Text == "")
            {
                txtSoles.Text = Convert.ToString(0);
                txtTotal.Text = Math.Round((Convert.ToDecimal(txtGalones.Text) * (Convert.ToDecimal(txtSoles.Text) * Convert.ToDecimal(1.18))),2).ToString();
            }
            else
            { txtTotal.Text = Math.Round((Convert.ToDecimal(txtGalones.Text) * (Convert.ToDecimal(txtSoles.Text) * Convert.ToDecimal(1.18))),2).ToString(); }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtNumero.Focus(); }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtMontoComprobante.Focus(); }
        }

        private void txtFactura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtMontoComprobante.Focus(); }
        }

        private void txtSolucion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { InsertarSolucion(); }
        }

        private void dtpFechaLiquidacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { LiquidarFalla(); }
        }

        private void rbSolicitud_Click(object sender, EventArgs e)
        {
            if(rbSolicitud.Checked == true)
            {
                panel1.Visible = true;
                label43.Visible = true;
                label42.Visible = false;
                btnNuevaSolicitud.Visible = true;
                dtgListaSolicitud.Visible = true;
                btnEstadoUnidades.Enabled = true;
                btnUbicacionFlota.Enabled = true;
                btnComponentes.Enabled = true;
                btnAnalisis.Enabled = false;
            }
        }

        private void rbFalla_Click(object sender, EventArgs e)
        {
            if (rbFalla.Checked == true)
            {
                panel1.Visible = false;
                label43.Visible = false;
                label42.Visible = true;
                btnNuevaSolicitud.Visible = false;
                dtgListaSolicitud.Visible = false;
                btnEstadoUnidades.Enabled = false;
                btnUbicacionFlota.Enabled = false;
                btnComponentes.Enabled = false;
                btnAnalisis.Enabled = true;
            }
        }

        private void btnNuevaSolicitud_Click(object sender, EventArgs e)
        {
            frmSolicitudMantenimiento frmSolicitudMantenimiento = new frmSolicitudMantenimiento();
            frmSolicitudMantenimiento.dtpFechaProgrI.Value = DateTime.Now;
            frmSolicitudMantenimiento.dtpHoraProgrI.Value = DateTime.Now;
            frmSolicitudMantenimiento.dtpFechaReprog.Value = DateTime.Now;
            frmSolicitudMantenimiento.dtpHoraReprog.Value = DateTime.Now;
            frmSolicitudMantenimiento.dtpFechaEntrega.Value = DateTime.Now;
            frmSolicitudMantenimiento.dtpHoraEntrega.Value = DateTime.Now;

            frmSolicitudMantenimiento.ListarDetalle(1, 0, 0);
            frmSolicitudMantenimiento.groupBox12.Enabled = true;
            frmSolicitudMantenimiento.groupBox1.Enabled = false;
            frmSolicitudMantenimiento.groupBox3.Enabled = false;
            frmSolicitudMantenimiento.groupBox4.Enabled = false;
            frmSolicitudMantenimiento.asignarOTToolStripMenuItem.Enabled = false;
            frmSolicitudMantenimiento.eliminarToolStripMenuItem.Enabled = true;
            frmSolicitudMantenimiento.terminarToolStripMenuItem.Enabled = false;
            frmSolicitudMantenimiento.btnCancelar.Enabled = true;
            frmSolicitudMantenimiento.btnAniadir.Enabled = true;
            frmSolicitudMantenimiento.btnRegistrar.Enabled = true;
            frmSolicitudMantenimiento.btnProgramar.Enabled = false;
            frmSolicitudMantenimiento.btnTerminar.Enabled = false;

            frmSolicitudMantenimiento.label4.Visible = false;
            frmSolicitudMantenimiento.cbxCisterna.Visible = false;
            frmSolicitudMantenimiento.txtCisterna.Visible = false;
            frmSolicitudMantenimiento.cbxTipoMtto.Text = "MTTO. CORRECTIVO";
            frmSolicitudMantenimiento.cbxTipoTrabajo.Text = "NO PROGRAMADO";

            frmSolicitudMantenimiento.frmListarSolicitudes(this);
            frmSolicitudMantenimiento.rbTracto.Checked = true;
            frmSolicitudMantenimiento.rbTracto_Click(sender, e);
            frmSolicitudMantenimiento.Opcion = 0;

            frmSolicitudMantenimiento.CargarComboComponente();
            frmSolicitudMantenimiento.CargarComboDetalle();
            frmSolicitudMantenimiento.CargarComboPosicion();
            frmSolicitudMantenimiento.CargarComboBase(2);
            frmSolicitudMantenimiento.CargarComboBase(3);

            frmSolicitudMantenimiento.ShowDialog();
        }

        private void btnRegistrarLogistica_Click(object sender, EventArgs e)
        {
            if (txtRepuesto.Text.Length == 0 || txtFechaSolicitada.Text.Length == 0)
            {
                MessageBox.Show("Los campos no pueden estar vacíos.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRequerimiento.Focus();
                return;
            }
            else
            {
                int idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Pedidos(1, idSolicitud, txtRequerimiento.Text, txtRepuesto.Text, Convert.ToDateTime(txtFechaSolicitada.Text));
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pLogistica.Visible = false;
                    pLogistica.SendToBack();
                    txtRequerimiento.Clear();
                    txtRepuesto.Clear();
                    txtFechaSolicitada.Clear();
                    ListarSolicitudes();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnEliminarLogistica_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea desvincular este requerimiento?", "ELIMINAR AUXILIO MECÁNICO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
                DataTable dtRespuesta = new DataTable();
                string Respuesta;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Pedidos(4, idSolicitud, txtRequerimiento.Text, txtRepuesto.Text, Convert.ToDateTime(txtFechaSolicitada.Text));
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    pLogistica.Visible = false;
                    pLogistica.SendToBack();
                    txtRequerimiento.Clear();
                    txtRepuesto.Clear();
                    txtFechaSolicitada.Clear();
                    ListarSolicitudes();
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btnRegistrarLlegada_Click(object sender, EventArgs e)
        {
            int idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
            DataTable dtRespuesta = new DataTable();

            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_BuscarNotaIngreso(1, idSolicitud, txtRequerimiento.Text);
            if (dtRespuesta.Rows.Count > 0)
            {
                MessageBox.Show("Fecha de Ingreso encontrada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFLlegada.Text = dtRespuesta.Rows[0]["FechaLlegadaP"].ToString();
                ListarSolicitudes();
                btnRegistrarLlegada.Enabled = false;
            }
            else { MessageBox.Show("Aún no se ha registrado la Nota de Ingreso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtpFechaEstimada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { dtpHoraEstimada.Focus(); }
        }

        private void dtpHoraEstimada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnFechaEstimada.Focus(); }
        }

        private void btnFechaEstimada_Click(object sender, EventArgs e)
        {
            int idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
            string FechaEstimada = dtpFechaEstimada.Text + ' ' + dtpHoraEstimada.Text;
            string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            DataTable dtRespuesta = new DataTable();
            string Respuesta;

            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_InsertarFechaEstimada(OpcionFecha, idSolicitud, Convert.ToDateTime(FechaEstimada), Usuario);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pFechaEstimada.Visible = false;
                pFechaEstimada.SendToBack();
                dtpFechaEstimada.Value = DateTime.Now;
                dtpHoraEstimada.Value = new DateTime(dtpHoraEstimada.Value.Year, dtpHoraEstimada.Value.Month, 1, 0, 0, 0);
                ListarSolicitudes();
            }
            else
            { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /*
        private void btnRegistrarLlegada_Click(object sender, EventArgs e)
        {
            int idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
            string FechaLlegada = dtpFLlegadaPedido.Text + ' ' + dtpHLlegadaPedido.Text;

            DataTable dtRespuesta = new DataTable();
            string Respuesta;

            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Pedidos(3, idSolicitud, txtRepuesto.Text, Convert.ToDateTime(FechaLlegada));
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                MessageBox.Show(Respuesta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pLogistica.Visible = false;
                pLogistica.SendToBack();
                txtRepuesto.Clear();
                ListarSolicitudes();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
        */

        private void rbFallaParada_Click(object sender, EventArgs e) { TipoFalla = "PARADA"; }

        private void rbLeve_Click(object sender, EventArgs e) { TipoFalla = "LEVE"; }

        private void dtgListaFallasMecanicas_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string estado = dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ESTADO").ToString();
                string estado2 = dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CLASE_SERVICIO").ToString();

                if (estado == "SOLUCIONADO")
                {
                    Limpiar();
                    pFacturarFaltante.Visible = true;
                    pFacturarFaltante.BringToFront();
                    btnAgregar.Enabled = false;
                    btnCancelar.Enabled = false;

                    if (estado2 == "GT TRANSPESA")
                    {
                        rbGrupoTranspesa.Checked = true;
                        btnSeleccionar.Enabled = true;
                    }

                    if (estado2 == "TERCERO")
                    {
                        rbTerceros.Checked = true;
                        btnSeleccionar.Enabled = false;
                    }

                    VerSolucion();
                }
                else
                {
                    Limpiar();
                    pFacturarFaltante.Visible = true;
                    pFacturarFaltante.BringToFront();
                    btnAgregar.Enabled = false;
                    btnCancelar.Enabled = false;

                    groupBox10.Enabled = false;
                    groupBox5.Enabled = false;
                    groupBox2.Enabled = false;
                    groupBox9.Enabled = false;
                    txtTecnico.Enabled = false;
                    txtPlacaTecnico.Enabled = false;
                    txtGalones.Enabled = false;

                    txtProgramacion.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "PROGRAMACION"));
                    txtFechaViaje.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_VIAJE"));
                    txtPlaca1.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "PLACA"));
                    txtSemirremolque.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "SEMIRREMOLQUE"));
                    txtConductor.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CONDUCTOR"));
                    txtRuta.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "RUTA"));

                    txtFechaIncidente.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_INICIO"));
                    txtFechaSalida.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_SALIDA"));
                    txtFechaLlegada.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_LLEGADA"));
                    txtFechaTermino.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_TÉRMINO"));
                    UCrea.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "UsuarioCreacion"));
                    UCreaFecha.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FechaCreacion"));
                    UModif.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "UltimoUsuario"));
                    UModifFecha.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "UltimaModificacion"));
                }
            }
            catch
            {
                MessageBox.Show("La fila seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgListaSolicitud_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                frmSolicitudMantenimiento f1 = new frmSolicitudMantenimiento();

                f1.ListarSolicitud(Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud")));
                f1.idSolicitud = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "idSolicitud"));
                f1.idc = Convert.ToInt32(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "IDC"));
                f1.Estado = Convert.ToString(dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "ESTADO"));

                f1.groupBox12.Enabled = false;
                f1.groupBox1.Enabled = false;
                f1.groupBox3.Enabled = false;
                f1.groupBox4.Enabled = false;
                f1.asignarOTToolStripMenuItem.Enabled = true;
                f1.terminarToolStripMenuItem.Enabled = true;
                f1.btnProgramar.Enabled = false;
                f1.btnTerminar.Enabled = false;
                f1.btnRegistrar.Enabled = false;

                f1.label4.Visible = true;
                f1.cbxCisterna.Visible = false;
                f1.txtCisterna.Visible = true;

                if (f1.Estado == "SOLICITADO" || f1.Estado == "RECEPCIONADO" || f1.Estado == "PROGRAMADO" || f1.Estado == "REPROGRAMADO")
                {
                    f1.btnAniadir.Enabled = true;
                    f1.btnCancelar.Enabled = true;
                    f1.eliminarToolStripMenuItem.Enabled = true;
                    f1.Opcion = 1;
                }
                else
                {
                    f1.btnAniadir.Enabled = false;
                    f1.btnCancelar.Enabled = false;
                    f1.eliminarToolStripMenuItem.Enabled = false;
                    f1.Opcion = 0;
                }

                f1.CargarComboComponente();
                f1.CargarComboDetalle();
                f1.CargarComboPosicion();
                f1.CargarComboBase(2);
                f1.CargarComboBase(3);
                f1.ShowDialog();
            }
            catch { MessageBox.Show("La fila seleccionada no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void dtgListaFallasMecanicas_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                string codigo = dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CodEstado").ToString();

                if(codigo == "FM") { dtgListaFallasMecanicas.ContextMenuStrip = contextMenuStrip1; }
                else { dtgListaFallasMecanicas.ContextMenuStrip = contextMenuStrip2; }
            }
            catch
            { }
        }

        private void dtgListaFallasMecanicas_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string estado = dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ESTADO").ToString();
                string estado2 = dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ESTADO_LIQUIDACIÓN").ToString();

                string fecha = dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_TÉRMINO").ToString();

                int idIncidenteC = Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "idIncidenteC"));

                if (estado == "SOLUCIONADO")
                {
                    editarFallaMecanicaToolStripMenuItem.Enabled = false;
                    generarPresupuestoToolStripMenuItem.Enabled = true;
                    if (idIncidenteC != 0) { imprimirReporteToolStripMenuItem.Enabled = true; }
                    else { imprimirReporteToolStripMenuItem.Enabled = false; }

                    asignarSolucionToolStripMenuItem.Enabled = false;
                    cerrarFallaToolStripMenuItem.Enabled = false;
                    eliminarFallaToolStripMenuItem.Enabled = false;
                    tsGenerarReporte.Enabled = true;
                    
                    if (estado2 == "PENDIENTE")
                    {
                        if (e3 == 1)
                        {
                            liquidarFallaMecanicaToolStripMenuItem.Enabled = true;
                            generarPresupuestoToolStripMenuItem.Enabled = true;
                            if (idIncidenteC != 0) { imprimirReporteToolStripMenuItem.Enabled = true; }
                            else { imprimirReporteToolStripMenuItem.Enabled = false; }
                        }
                    }
                    else
                    {
                        liquidarFallaMecanicaToolStripMenuItem.Enabled = false;
                        generarPresupuestoToolStripMenuItem.Enabled = true;
                    }
                }
                else
                {
                    if (fecha != "")
                    {
                        cerrarFallaToolStripMenuItem.Enabled = false;
                        if (e2 == 1)
                        {
                            asignarSolucionToolStripMenuItem.Enabled = true;
                            generarPresupuestoToolStripMenuItem.Enabled = true;
                            if (idIncidenteC != 0) { imprimirReporteToolStripMenuItem.Enabled = true; }
                            else { imprimirReporteToolStripMenuItem.Enabled = false; }
                        }
                    }
                    else
                    {
                        if (e1 == 1) { cerrarFallaToolStripMenuItem.Enabled = true; }
                        
                        asignarSolucionToolStripMenuItem.Enabled = false;
                        generarPresupuestoToolStripMenuItem.Enabled = false;
                    }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]) == true) { editarFallaMecanicaToolStripMenuItem.Enabled = true; }
                    if (Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]) == true) { eliminarFallaToolStripMenuItem.Enabled = true; }
                    liquidarFallaMecanicaToolStripMenuItem.Enabled = false;
                    tsGenerarReporte.Enabled = false;
                }
            }
            catch
            {
                editarFallaMecanicaToolStripMenuItem.Enabled = false;
                asignarSolucionToolStripMenuItem.Enabled = false;
                cerrarFallaToolStripMenuItem.Enabled = false;
                eliminarFallaToolStripMenuItem.Enabled = false;
                liquidarFallaMecanicaToolStripMenuItem.Enabled = false;
                generarPresupuestoToolStripMenuItem.Enabled = false;
                tsGenerarReporte.Enabled = false;
            }
        }

        private void dtgListaSolicitud_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string ES = dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "ESTADO").ToString();
                string EPedido = dgvListaSolicitudView.GetRowCellValue(dgvListaSolicitudView.FocusedRowHandle, "ESTADO_REPUESTO").ToString();
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                programarSolicitudToolStripMenuItem.Text = "Programar Solicitud";

                if (ES == "SOLICITADO")
                {
                    programarSolicitudToolStripMenuItem.Enabled = false;
                    solicitudDeLogísticaToolStripMenuItem.Enabled = false;
                    if (e6 == 1) { btnRegistrarLogistica.Enabled = true; }
                    btnEliminarLogistica.Enabled = true;
                    if (e5 == 1) { btnRegistrarLlegada.Enabled = true; }
                    if (e4 == 1)
                    {
                        recepcionarUnidadToolStripMenuItem.Enabled = true;
                        agregarFechaEstimadaToolStripMenuItem.Enabled = true;
                        anularSolicitudToolStripMenuItem.Enabled = true;
                    }
                    tsCambiarUbicacion.Enabled = false;
                    terminarMantenimientoToolStripMenuItem.Enabled = false;
                }
                else
                {
                    if (ES == "RECEPCIONADO")
                    {
                        if(EPedido == "PEDIDO")
                        {
                            programarSolicitudToolStripMenuItem.Enabled = false;
                            solicitudDeLogísticaToolStripMenuItem.Enabled = true;
                            groupBox16.Enabled = true;
                            groupBox17.Enabled = true;
                        }
                        else
                        {
                            if (e6 == 1) { programarSolicitudToolStripMenuItem.Enabled = true; }
                            groupBox16.Enabled = false;
                            groupBox17.Enabled = false;
                        }
                        solicitudDeLogísticaToolStripMenuItem.Enabled = true;
                        agregarFechaEstimadaToolStripMenuItem.Enabled = false;
                        recepcionarUnidadToolStripMenuItem.Enabled = false;
                        if (e6 == 1) { btnRegistrarLogistica.Enabled = true; }
                        btnEliminarLogistica.Enabled = true;
                        if (e5 == 1) { btnRegistrarLlegada.Enabled = true; }
                        if (e4 == 1)
                        {
                            anularSolicitudToolStripMenuItem.Enabled = true;
                            tsCambiarUbicacion.Enabled = true;
                        }
                        terminarMantenimientoToolStripMenuItem.Enabled = false;
                    }
                    else
                    {
                        if (ES == "PROGRAMADO" || ES == "REPROGRAMADO")
                        {
                            if (EPedido == "PEDIDO")
                            {
                                programarSolicitudToolStripMenuItem.Enabled = false;
                                solicitudDeLogísticaToolStripMenuItem.Enabled = true;
                                groupBox16.Enabled = true;
                                groupBox17.Enabled = true;
                            }
                            else
                            {
                                if (e6 == 1) { programarSolicitudToolStripMenuItem.Enabled = true; }
                                else { programarSolicitudToolStripMenuItem.Enabled = false; }

                                groupBox16.Enabled = false;
                                groupBox17.Enabled = false;
                            }
                            solicitudDeLogísticaToolStripMenuItem.Enabled = true;
                            agregarFechaEstimadaToolStripMenuItem.Enabled = false;
                            recepcionarUnidadToolStripMenuItem.Enabled = false;
                            if (e6 == 1) { btnRegistrarLogistica.Enabled = true; }
                            btnEliminarLogistica.Enabled = true;
                            btnRegistrarLlegada.Enabled = false;
                            if (e6 == 1) { programarSolicitudToolStripMenuItem.Enabled = true; }
                            programarSolicitudToolStripMenuItem.Text = "Reprogramar Solicitud";
                            anularSolicitudToolStripMenuItem.Enabled = false;
                            if (e4 == 1)
                            {
                                terminarMantenimientoToolStripMenuItem.Enabled = true;
                                tsCambiarUbicacion.Enabled = true;
                            }
                        }
                        else
                        {
                            agregarFechaEstimadaToolStripMenuItem.Enabled = false;
                            recepcionarUnidadToolStripMenuItem.Enabled = false;
                            btnRegistrarLogistica.Enabled = false;
                            solicitudDeLogísticaToolStripMenuItem.Enabled = false;
                            btnEliminarLogistica.Enabled = false;
                            btnRegistrarLlegada.Enabled = false;
                            programarSolicitudToolStripMenuItem.Enabled = false;
                            anularSolicitudToolStripMenuItem.Enabled = false;
                            terminarMantenimientoToolStripMenuItem.Enabled = false;
                            tsCambiarUbicacion.Enabled = false;
                        }
                    }
                }
            }
            catch
            {
                agregarFechaEstimadaToolStripMenuItem.Enabled = false;
                recepcionarUnidadToolStripMenuItem.Enabled = false;
                solicitudDeLogísticaToolStripMenuItem.Enabled = false;
                programarSolicitudToolStripMenuItem.Enabled = false;
                anularSolicitudToolStripMenuItem.Enabled = false;
                terminarMantenimientoToolStripMenuItem.Enabled = false;
                tsCambiarUbicacion.Enabled = false;
            }
        }

        private void pFacturarFaltante_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pFacturarFaltante.Left = pFacturarFaltante.Left + (e.X - xClick);
                pFacturarFaltante.Top = pFacturarFaltante.Top + (e.Y - yClick);
            }
        }

        private void pEditarFalla_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pEditarFalla.Left = pEditarFalla.Left + (e.X - xClick);
                pEditarFalla.Top = pEditarFalla.Top + (e.Y - yClick);
            }
        }

        private void pCerrarFalla_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pCerrarFalla.Left = pCerrarFalla.Left + (e.X - xClick);
                pCerrarFalla.Top = pCerrarFalla.Top + (e.Y - yClick);
            }
        }

        private void pLogistica_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pLogistica.Left = pLogistica.Left + (e.X - xClick);
                pLogistica.Top = pLogistica.Top + (e.Y - yClick);
            }
        }

        private void pNuevo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pNuevo.Left = pNuevo.Left + (e.X - xClick);
                pNuevo.Top = pNuevo.Top + (e.Y - yClick);
            }
        }

        private void pFechaEstimada_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pFechaEstimada.Left = pFechaEstimada.Left + (e.X - xClick);
                pFechaEstimada.Top = pFechaEstimada.Top + (e.Y - yClick);
            }
        }

        private void ingresarPresupuestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGenerarPresupuesto frmGenerarPresupuesto = new frmGenerarPresupuesto();
            frmGenerarPresupuesto.Opcion = 0;
            frmGenerarPresupuesto.Elimino = 1;
            frmGenerarPresupuesto.formulario2 = this;
            frmGenerarPresupuesto.idFalla = Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "ID"));
            frmGenerarPresupuesto._idIncidencia = Convert.ToInt32(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "idIncidenteC"));
            frmGenerarPresupuesto.txtOperacion2.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "PROGRAMACION"));
            frmGenerarPresupuesto.dtpFechaIncidente.Value = Convert.ToDateTime(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "FECHA_INICIO"));
            frmGenerarPresupuesto.txtTracto2.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "PLACA"));
            frmGenerarPresupuesto.txtCarreta2.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "SEMIRREMOLQUE"));
            frmGenerarPresupuesto.txtConductor2.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "CONDUCTOR"));
            frmGenerarPresupuesto.txtIncidente.Text = Convert.ToString(dgvListaFallasMecanicasView.GetRowCellValue(dgvListaFallasMecanicasView.FocusedRowHandle, "MOTIVO"));
            frmGenerarPresupuesto.ShowDialog();
        }

        private void imprimirReporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void cbxSistema_DropDownClosed(object sender, EventArgs e)
        {
            idSistema = Convert.ToInt32(cbxSistema.SelectedValue);
            CargarComboSubSistema();
        }

        private void txtRequerimiento_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstRequerimiento, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_Pedidos(5,0,txtRequerimiento.Text,"",DateTime.Now), true, false, false);
            lstRequerimiento.Columns[0].Width = 70;
            lstRequerimiento.Columns[1].Width = 250;
            lstRequerimiento.Columns[2].Width = 150;
            lstRequerimiento.BringToFront();
            lstRequerimiento.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();

                txtRepuesto.Clear();
                txtFechaSolicitada.Clear();
            }
        }

        private void txtRequerimiento_KeyUp(object sender, KeyEventArgs e)
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

                txtRequerimiento.Text = ItemActual.SubItems[0].Text;
                txtRepuesto.Text = ItemActual.SubItems[1].Text;
                txtFechaSolicitada.Text = ItemActual.SubItems[2].Text;

                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();
                btnRegistrarLogistica.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstRequerimiento.Visible = false;
                lstRequerimiento.SendToBack();

                txtRepuesto.Clear();
                txtFechaSolicitada.Clear();
                txtRequerimiento.Focus();
            }
        }

        private void lstRequerimiento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstRequerimiento.SelectedItems[0];

            txtRequerimiento.Text = ItemActual.SubItems[0].Text;
            txtRepuesto.Text = ItemActual.SubItems[1].Text;
            txtFechaSolicitada.Text = ItemActual.SubItems[2].Text;

            lstRequerimiento.Visible = false;
            lstRequerimiento.SendToBack();
            btnRegistrarLogistica.Focus();
        }

        private void dgvListaFallasMecanicasView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO_AUXILIO")
            {
                if (e.CellValue.ToString() == "EN COORDINACIÓN") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (e.CellValue.ToString() == "EN RUTA") { e.Appearance.BackColor = Color.FromArgb(255, 128, 0); }

                if (e.CellValue.ToString() == "EN ATENCIÓN") { e.Appearance.BackColor = Color.FromArgb(255, 255, 0); }

                if (e.CellValue.ToString() == "ATENDIDO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }

            if (e.Column.FieldName == "KIT_NEUMATICO")
            {
                if (e.CellValue.ToString() == "NO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }

                if (e.CellValue.ToString() == "SÍ") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }
            }
        }

        private void cbxEstadoAuxilio_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxEstadoAuxilio.Text == "EN COORDINACIÓN")
            {
                groupBox11.Text = "Salida de Auxilio: ";
                btnCerrarFalla.Enabled = false;
            }

            if (cbxEstadoAuxilio.Text == "EN RUTA")
            {
                groupBox11.Text = "Salida de Auxilio: ";
                btnCerrarFalla.Enabled = true;
            }

            if (cbxEstadoAuxilio.Text == "EN ATENCIÓN")
            {
                groupBox11.Text = "Llegada de Auxilio: ";
                btnCerrarFalla.Enabled = true;
            }

            if (cbxEstadoAuxilio.Text == "ATENDIDO")
            {
                groupBox11.Text = "Término de Auxilio: ";
                btnCerrarFalla.Enabled = true;
            }
        }

        private void cbMttoCorrectivo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbMttoCorrectivo.Checked == true) { MttoCorrectivo = 1; }

            if (cbMttoCorrectivo.Checked == false) { MttoCorrectivo = 0; }
        }

        private void cbBloquearT_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBloquearT.Checked == true) { BloquearTracto = 1; }

            if (cbBloquearT.Checked == false) { BloquearTracto = 0; }
        }

        private void cbBloquearC_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBloquearC.Checked == true) { BloquearCarreta = 1; }

            if (cbBloquearC.Checked == false) { BloquearCarreta = 0; }
        }
    }
}
