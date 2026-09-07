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
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento.ControlMantenimiento
{
    public partial class frmInspeccionUnidades : Form
    {
        public int Tecnico, Persona1 = -1, Persona2 = -1, Persona3 = -1, Persona4 = -1;
        public int Opcion, idInspeccionC, idInspeccionD, EliminarPedido, TipoInspeccion = 0;
        public int xClick = 0, yClick = 0, xClick2 = 0, yClick2 = 0;
        public string Item;
        DataTable dtListaInspeccion = new DataTable();
        public frmListaMantenimiento frmListaMantenimiento = new frmListaMantenimiento();

        public frmInspeccionUnidades()
        {
            InitializeComponent();
            dgvListaInspeccionesVista.RowUpdated += dgvListaInspeccionesVista_RowUpdated;
        }

        private void frmInspeccionUnidades_Load(object sender, EventArgs e) { }


        public void ListarInspeccion()
        {
            if (TipoInspeccion == 1)
            {
                dtListaInspeccion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(2, lblPlaca.Text, 0);
                dtgListaInspecciones.DataSource = dtListaInspeccion;
                if (dtListaInspeccion.Rows.Count > 0)
                {
                    dgvListaInspeccionesVista.Columns["idInspeccionC"].Visible = false;
                    dgvListaInspeccionesVista.Columns["idInspeccionD"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Placa"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Mecanico1"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Electrico2"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Neumatico3"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Soldador4"].Visible = false;
                    dgvListaInspeccionesVista.Columns["MECÁNICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["ELÉCTRICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["NEUMÁTICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["SOLDADOR"].Visible = false;
                    dgvListaInspeccionesVista.Columns["TURNO"].Visible = false;

                    dgvListaInspeccionesVista.Columns["ESTADO"].OptionsColumn.AllowEdit = true;
                    dgvListaInspeccionesVista.Columns["OBSERVACION"].OptionsColumn.AllowEdit = true;

                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].Visible = false;
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].Visible = false;

                    dgvListaInspeccionesVista.BestFitColumns();
                }
            }
            else
            {
                dtListaInspeccion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(5, lblPlaca.Text, 0);
                dtgListaInspecciones.DataSource = dtListaInspeccion;
                if (dtListaInspeccion.Rows.Count > 0)
                {
                    dgvListaInspeccionesVista.Columns["idInspeccionC"].Visible = false;
                    dgvListaInspeccionesVista.Columns["idInspeccionD"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Placa"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Mecanico1"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Electrico2"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Neumatico3"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Soldador4"].Visible = false;
                    dgvListaInspeccionesVista.Columns["MECÁNICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["ELÉCTRICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["NEUMÁTICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["SOLDADOR"].Visible = false;
                    dgvListaInspeccionesVista.Columns["TURNO"].Visible = false;

                    dgvListaInspeccionesVista.Columns["ESTADO"].OptionsColumn.AllowEdit = true;
                    dgvListaInspeccionesVista.Columns["OBSERVACION"].OptionsColumn.AllowEdit = true;

                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].Visible = false;
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].Visible = false;

                    dgvListaInspeccionesVista.BestFitColumns();
                }
            }
        }

        public void BuscarInspeccion()
        {
            if (TipoInspeccion == 1)
            {
                dtListaInspeccion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(3, lblPlaca.Text, idInspeccionC);
                dtgListaInspecciones.DataSource = dtListaInspeccion;
                if (dtListaInspeccion.Rows.Count > 0)
                {
                    dgvListaInspeccionesVista.Columns["idInspeccionC"].Visible = false;
                    dgvListaInspeccionesVista.Columns["idInspeccionD"].Visible = false;
                    dgvListaInspeccionesVista.Columns["idInspeccionC"].Visible = false;
                    dgvListaInspeccionesVista.Columns["idInspeccionD"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Placa"].Visible = false;

                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].Visible = false;
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].Visible = false;

                    dgvListaInspeccionesVista.Columns["Mecanico1"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Electrico2"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Neumatico3"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Soldador4"].Visible = false;
                    dgvListaInspeccionesVista.Columns["MECÁNICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["ELÉCTRICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["NEUMÁTICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["SOLDADOR"].Visible = false;
                    dgvListaInspeccionesVista.Columns["TURNO"].Visible = false;

                    dgvListaInspeccionesVista.BestFitColumns();
                }
            }
            else
            {
                dtListaInspeccion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(6, lblPlaca.Text, idInspeccionC);
                dtgListaInspecciones.DataSource = dtListaInspeccion;
                if (dtListaInspeccion.Rows.Count > 0)
                {
                    dgvListaInspeccionesVista.Columns["idInspeccionC"].Visible = false;
                    dgvListaInspeccionesVista.Columns["idInspeccionD"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Placa"].Visible = false;

                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaInspeccionesVista.Columns["INICIO_INSPECCION"].Visible = false;
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatType = FormatType.DateTime;
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].DisplayFormat.FormatString = "dd/MM/yyyy";
                    dgvListaInspeccionesVista.Columns["FIN_INSPECCION"].Visible = false;

                    dgvListaInspeccionesVista.Columns["Mecanico1"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Electrico2"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Neumatico3"].Visible = false;
                    dgvListaInspeccionesVista.Columns["Soldador4"].Visible = false;
                    dgvListaInspeccionesVista.Columns["MECÁNICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["ELÉCTRICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["NEUMÁTICO"].Visible = false;
                    dgvListaInspeccionesVista.Columns["SOLDADOR"].Visible = false;
                    dgvListaInspeccionesVista.Columns["TURNO"].Visible = false;

                    dgvListaInspeccionesVista.BestFitColumns();
                }
            }
        }

        public void ListarPedidos()
        {
            DataTable dtListaInspeccion = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarPedidosInspecciones(idInspeccionC);
            dtgListaPedidos.DataSource = dtListaInspeccion;
            if (dtListaInspeccion.Rows.Count > 0)
            {
                dgvListaPedidosVista.Columns["idInspeccionC"].Visible = false;
                dgvListaPedidosVista.Columns["PLACA"].Visible = false;

                dgvListaPedidosVista.Columns["FECHA_PEDIDO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaPedidosVista.Columns["FECHA_PEDIDO"].DisplayFormat.FormatString = "dd/MM/yyyy";
                dgvListaPedidosVista.Columns["FECHA_DESPACHO"].DisplayFormat.FormatType = FormatType.DateTime;
                dgvListaPedidosVista.Columns["FECHA_DESPACHO"].DisplayFormat.FormatString = "dd/MM/yyyy";

                dgvListaPedidosVista.BestFitColumns();
            }
        }


        private void txtTecnico1_Enter(object sender, EventArgs e)
        {
            Tecnico = 1;
            txtMecanico.BackColor = Color.FromArgb(192, 255, 192);
            lstPersonal.Location = new System.Drawing.Point(84, 201);
        }

        private void txtTecnico1_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(1, txtMecanico.Text, 0), true, false, false);
            lstPersonal.Columns[0].Width = 0;
            lstPersonal.Columns[1].Width = 250;
            lstPersonal.Columns[2].Width = 200;
            lstPersonal.BringToFront();
            lstPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona1 = -1;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void txtTecnico1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal.Focus(); }
        }

        private void txtTecnico1_Leave(object sender, EventArgs e) { txtMecanico.BackColor = Color.White; }

        private void txtTecnico2_Enter(object sender, EventArgs e)
        {
            Tecnico = 2;
            txtElectrico.BackColor = Color.FromArgb(192, 255, 192);
            lstPersonal.Location = new System.Drawing.Point(84, 229);
        }

        private void txtTecnico2_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(1, txtElectrico.Text, 0), true, false, false);
            lstPersonal.Columns[0].Width = 0;
            lstPersonal.Columns[1].Width = 250;
            lstPersonal.Columns[2].Width = 200;
            lstPersonal.BringToFront();
            lstPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona2 = -1;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void txtTecnico2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal.Focus(); }
        }

        private void txtTecnico2_Leave(object sender, EventArgs e) { txtElectrico.BackColor = Color.White; }

        private void txtTecnico3_Enter(object sender, EventArgs e)
        {
            Tecnico = 3;
            txtNeumatico.BackColor = Color.FromArgb(192, 255, 192);
            lstPersonal.Location = new System.Drawing.Point(84, 257);
        }

        private void txtTecnico3_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(1, txtNeumatico.Text, 0), true, false, false);
            lstPersonal.Columns[0].Width = 0;
            lstPersonal.Columns[1].Width = 250;
            lstPersonal.Columns[2].Width = 200;
            lstPersonal.BringToFront();
            lstPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona3 = -1;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void txtTecnico3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal.Focus(); }
        }

        private void txtTecnico3_Leave(object sender, EventArgs e) { txtNeumatico.BackColor = Color.White; }

        private void txtSoldador_Enter(object sender, EventArgs e)
        {
            Tecnico = 4;
            txtSoldador.BackColor = Color.FromArgb(192, 255, 192);
            lstPersonal.Location = new System.Drawing.Point(84, 286);
        }

        private void txtSoldador_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstPersonal, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarInspeccionesUnidad(1, txtSoldador.Text, 0), true, false, false);
            lstPersonal.Columns[0].Width = 0;
            lstPersonal.Columns[1].Width = 250;
            lstPersonal.Columns[2].Width = 200;
            lstPersonal.BringToFront();
            lstPersonal.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                Persona4 = -1;
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void txtSoldador_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstPersonal.Focus(); }
        }

        private void txtSoldador_Leave(object sender, EventArgs e) { txtSoldador.BackColor = Color.White; }

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

                if (Tecnico == 1)
                {
                    if (Int32.Parse(ItemActual.Text) == Persona2 || Int32.Parse(ItemActual.Text) == Persona3 || Int32.Parse(ItemActual.Text) == Persona4)
                    {
                        MessageBox.Show("Este mecánico ya ha sido registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtMecanico.Focus();
                        return;
                    }
                    else
                    {
                        Persona1 = Int32.Parse(ItemActual.Text);
                        txtMecanico.Text = ItemActual.SubItems[1].Text;
                    }
                }

                if (Tecnico == 2)
                {
                    if (Int32.Parse(ItemActual.Text) == Persona1 || Int32.Parse(ItemActual.Text) == Persona3 || Int32.Parse(ItemActual.Text) == Persona4)
                    {
                        MessageBox.Show("Este mecánico ya ha sido registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtElectrico.Focus();
                        return;
                    }
                    else
                    {
                        Persona2 = Int32.Parse(ItemActual.Text);
                        txtElectrico.Text = ItemActual.SubItems[1].Text;
                    }
                }

                if (Tecnico == 3)
                {
                    if (Int32.Parse(ItemActual.Text) == Persona1 || Int32.Parse(ItemActual.Text) == Persona2 || Int32.Parse(ItemActual.Text) == Persona4)
                    {
                        MessageBox.Show("Este mecánico ya ha sido registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtNeumatico.Focus();
                        return;
                    }
                    else
                    {
                        Persona3 = Int32.Parse(ItemActual.Text);
                        txtNeumatico.Text = ItemActual.SubItems[1].Text;
                    }
                }

                if (Tecnico == 4)
                {
                    if (Int32.Parse(ItemActual.Text) == Persona1 || Int32.Parse(ItemActual.Text) == Persona2 || Int32.Parse(ItemActual.Text) == Persona3)
                    {
                        MessageBox.Show("Este mecánico ya ha sido registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtSoldador.Focus();
                        return;
                    }
                    else
                    {
                        Persona4 = Int32.Parse(ItemActual.Text);
                        txtSoldador.Text = ItemActual.SubItems[1].Text;
                    }
                }

                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
                btnAgregar.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                if (Tecnico == 1) { Persona1 = -1; }
                if (Tecnico == 2) { Persona2 = -1; }
                if (Tecnico == 3) { Persona3 = -1; }
                if (Tecnico == 4) { Persona4 = -1; }
                
                lstPersonal.Visible = false;
                lstPersonal.SendToBack();
            }
        }

        private void lstPersonal_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstPersonal.SelectedItems[0];

            if (Tecnico == 1)
            {
                if (Int32.Parse(ItemActual.Text) == Persona2 || Int32.Parse(ItemActual.Text) == Persona3)
                {
                    MessageBox.Show("Este empleado ya ha sido registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMecanico.Focus();
                    return;
                }
                else
                {
                    Persona1 = Int32.Parse(ItemActual.Text);
                    txtMecanico.Text = ItemActual.SubItems[1].Text;
                }
            }

            if (Tecnico == 2)
            {
                if (Int32.Parse(ItemActual.Text) == Persona1 || Int32.Parse(ItemActual.Text) == Persona3)
                {
                    MessageBox.Show("Este empleado ya ha sido registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtElectrico.Focus();
                    return;
                }
                else
                {
                    Persona2 = Int32.Parse(ItemActual.Text);
                    txtElectrico.Text = ItemActual.SubItems[1].Text;
                }
            }

            if (Tecnico == 3)
            {
                if (Int32.Parse(ItemActual.Text) == Persona1 || Int32.Parse(ItemActual.Text) == Persona2)
                {
                    MessageBox.Show("Este empleado ya ha sido registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNeumatico.Focus();
                    return;
                }
                else
                {
                    Persona3 = Int32.Parse(ItemActual.Text);
                    txtNeumatico.Text = ItemActual.SubItems[1].Text;
                }
            }

            if (Tecnico == 4)
            {
                if (Int32.Parse(ItemActual.Text) == Persona1 || Int32.Parse(ItemActual.Text) == Persona2)
                {
                    MessageBox.Show("Este empleado ya ha sido registrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoldador.Focus();
                    return;
                }
                else
                {
                    Persona4 = Int32.Parse(ItemActual.Text);
                    txtSoldador.Text = ItemActual.SubItems[1].Text;
                }
            }

            lstPersonal.Visible = false;
            lstPersonal.SendToBack();
            btnAgregar.Focus();
        }

        private void pCambiarEstado_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pCambiarEstado.Left = pCambiarEstado.Left + (e.X - xClick);
                pCambiarEstado.Top = pCambiarEstado.Top + (e.Y - yClick);
            }
        }

        private void dtgListaInspecciones_DoubleClick(object sender, EventArgs e)
        {
            /*
            if (TipoInspeccion == 1)
            { lblComponente.Text = dgvListaInspeccionesVista.GetRowCellValue(dgvListaInspeccionesVista.FocusedRowHandle, "COMPONENTE").ToString(); }
            else { lblComponente.Text = " "; }
            
            lblProceso.Text = dgvListaInspeccionesVista.GetRowCellValue(dgvListaInspeccionesVista.FocusedRowHandle, "PROCESO").ToString();
            idInspeccionC = Convert.ToInt32(dgvListaInspeccionesVista.GetRowCellValue(dgvListaInspeccionesVista.FocusedRowHandle, "idInspeccionC"));
            idInspeccionD = Convert.ToInt32(dgvListaInspeccionesVista.GetRowCellValue(dgvListaInspeccionesVista.FocusedRowHandle, "idInspeccionD"));

            string Estado = dgvListaInspeccionesVista.GetRowCellValue(dgvListaInspeccionesVista.FocusedRowHandle, "ESTADO").ToString();
            if (Estado == "") { cbxEstado.Text = "BUENO"; }
            else { cbxEstado.Text = Estado; }

            string Observacion = dgvListaInspeccionesVista.GetRowCellValue(dgvListaInspeccionesVista.FocusedRowHandle, "OBSERVACION").ToString();
            txtObservacion.Text = Observacion;

            pCambiarEstado.Visible = true;
            pCambiarEstado.BringToFront();
            */
        }

        private void dgvListaInspeccionesVista_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            try
            {
                var row = (DataRowView)e.Row;

                int idInspeccionC = Convert.ToInt32(row["idInspeccionC"].ToString());
                int idInspeccionD = Convert.ToInt32(row["idInspeccionD"].ToString());
                string Estado = row["ESTADO"].ToString();
                string Observacion = row["OBSERVACION"].ToString();

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                if (TipoInspeccion == 1)
                { dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion(1, idInspeccionC, idInspeccionD, Estado, Observacion, 0, 0, 0, 0,
                                                             dtpNuevaFechaI.Value, dtpNuevaFechaF.Value, cbxTurno.Text, cbxSucursal.Text); }
                else
                { dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion(5, idInspeccionC, idInspeccionD, Estado, Observacion, 0, 0, 0, 0,
                                                             dtpNuevaFechaI.Value, dtpNuevaFechaF.Value, cbxTurno.Text, cbxSucursal.Text); }
                
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                frmListaMantenimiento.ListarInspecciones();
            }
            catch { }
        }

        private void dgvListaInspeccionesVista_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "ESTADO")
            {
                if (Convert.ToString(e.CellValue) == "BUENO")
                { e.Appearance.BackColor = Color.FromArgb(192, 255, 192); }

                if (Convert.ToString(e.CellValue) == "REGULAR")
                { e.Appearance.BackColor = Color.FromArgb(255, 255, 128); }

                if (Convert.ToString(e.CellValue) == "MALO")
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 128, 128);
                    e.Appearance.ForeColor = Color.White;
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            lblComponente.Text = "";
            lblProceso.Text = "";
            idInspeccionC = -1;
            idInspeccionD = -1;
            cbxEstado.Text = "BUENO";
            txtObservacion.Clear();
            pCambiarEstado.Visible = false;
            pCambiarEstado.SendToBack();
            pCambiarEstado.Location = new System.Drawing.Point(141, 249);

            idInspeccionC = Convert.ToInt32(dtListaInspeccion.Rows[0]["idInspeccionC"]);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            idInspeccionC = Convert.ToInt32(dtListaInspeccion.Rows[0]["idInspeccionC"]);

            DataTable dtRespuesta = new DataTable();
            string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion(3, idInspeccionC, 0, "", "", Persona1, Persona2, Persona3, Persona4,
                                                       dtpNuevaFechaI.Value, dtpNuevaFechaF.Value, cbxTurno.Text, cbxSucursal.Text);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                if (Opcion == 1) { ListarInspeccion(); }
                if (Opcion == 2) { BuscarInspeccion(); }
                frmListaMantenimiento.ListarInspecciones();
                this.Close();
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaInspecciones.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                dtgLista.DataSource = dtListaInspeccion;
                
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "REGISTRO DE INSPECCIÓN DE UNIDAD - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgLista.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnVerPedidos_Click(object sender, EventArgs e)
        {
            pListaPedidos.Visible = true;
            pListaPedidos.BringToFront();
            ListarPedidos();
        }

        private void txtObservacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardar_Click(sender, e); }
        }

        private void btnAnular_Click(object sender, EventArgs e)
        {
            /*
            DataTable dtRespuesta = new DataTable();
            string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (TipoInspeccion == 1) { dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion(2, idInspeccionC, idInspeccionD, cbxEstado.Text, txtObservacion.Text, 0, 0, 0, 0, dtpFechaRealizacion.Value); }
            else { dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion(4, idInspeccionC, idInspeccionD, cbxEstado.Text, txtObservacion.Text, 0, 0, 0, 0, dtpFechaRealizacion.Value); }

            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                if (Opcion == 1) { ListarInspeccion(); }
                if (Opcion == 2) { BuscarInspeccion(); }
                btnCerrar_Click(sender, e);
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            */ 
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            /*
            DataTable dtRespuesta = new DataTable();
            string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

            if (TipoInspeccion == 1) { dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion(1, idInspeccionC, idInspeccionD, cbxEstado.Text, txtObservacion.Text, 0, 0, 0, 0, dtpFechaRealizacion.Value); }
            else { dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ActualizarInspeccion(5, idInspeccionC, idInspeccionD, cbxEstado.Text, txtObservacion.Text, 0, 0, 0, 0, dtpFechaRealizacion.Value); }

            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0")
            {
                if (Opcion == 1) { ListarInspeccion(); }
                if (Opcion == 2) { BuscarInspeccion(); }
                btnCerrar_Click(sender, e);
            }
            else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            */
        }

        private void txtCodigoItem_Enter(object sender, EventArgs e) { txtCodigoItem.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtCodigoItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstItemsAlmacen, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_ListarMaestroItems(txtCodigoItem.Text), true, false, false);
            lstItemsAlmacen.Columns[0].Width = 80;
            lstItemsAlmacen.Columns[1].Width = 400;
            lstItemsAlmacen.BringToFront();
            lstItemsAlmacen.Visible = true;

            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                Item = "";
                txtCodigoItem.Focus();
            }
        }

        private void txtCodigoItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstItemsAlmacen.Focus(); }
        }

        private void txtCodigoItem_Leave(object sender, EventArgs e) { txtCodigoItem.BackColor = Color.White; }

        private void lstItemsAlmacen_Enter(object sender, EventArgs e)
        {
            if (!lstItemsAlmacen.Items.Count.Equals(0)) { lstItemsAlmacen.Items[0].Selected = true; }
        }

        private void lstItemsAlmacen_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstItemsAlmacen.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstItemsAlmacen.SelectedItems[0];

                Item = ItemActual.SubItems[0].Text;
                txtCodigoItem.Text = ItemActual.SubItems[0].Text;
                txtDescripcion.Text = ItemActual.SubItems[1].Text;

                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                txtCantidad.Focus();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstItemsAlmacen.Visible = false;
                lstItemsAlmacen.SendToBack();
                Item = "";
                txtCodigoItem.Focus();
            }
        }

        private void lstItemsAlmacen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstItemsAlmacen.SelectedItems[0];

            Item = ItemActual.SubItems[0].Text;
            txtCodigoItem.Text = ItemActual.SubItems[0].Text;
            txtDescripcion.Text = ItemActual.SubItems[1].Text;

            lstItemsAlmacen.Visible = false;
            lstItemsAlmacen.SendToBack();
            txtCantidad.Focus();
        }

        private void txtCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.') && e.KeyChar != Convert.ToChar('.'))
            { e.Handled = true; }
            else { e.Handled = false; }

            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { btnGuardarPedido_Click(sender, e); }
        }

        private void pListaPedidos_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick2 = e.X; yClick2 = e.Y; }
            else
            {
                pListaPedidos.Left = pListaPedidos.Left + (e.X - xClick2);
                pListaPedidos.Top = pListaPedidos.Top + (e.Y - yClick2);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            dtgListaPedidos.DataSource = null;
            pListaPedidos.Visible = false;
            pListaPedidos.SendToBack();
            Item = "";
            txtCodigoItem.Clear();
            txtCantidad.Clear();
            txtDescripcion.Clear();
        }

        private void btnGuardarPedido_Click(object sender, EventArgs e)
        {
            if (txtDescripcion.Text.Length == 0 || txtCantidad.Text.Length == 0 || txtCantidad.Text == "0")
            {
                if (txtDescripcion.Text.Length == 0)
                {
                    MessageBox.Show("Por favor ingrese un ítem.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDescripcion.Focus();
                }
                else
                {
                    MessageBox.Show("Por favor ingrese la cantidad.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCantidad.Focus();
                }
                return;
            }
            else
            {
                idInspeccionC = Convert.ToInt32(dtListaInspeccion.Rows[0]["idInspeccionC"]);

                DataTable dtRespuesta = new DataTable();
                string respta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_InsertarPedidosInspecciones(1, 0, idInspeccionC, Item,
                                                             txtDescripcion.Text, Convert.ToDecimal(txtCantidad.Text), Usuario);
                respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRspta = respta.Substring(0, 1);
                if (NroRspta == "0") { ListarPedidos(); }
                else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void dtgListaPedidos_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                string idPedido = dgvListaPedidosVista.GetRowCellValue(dgvListaPedidosVista.FocusedRowHandle, "NRO").ToString();

                if (idPedido != "")
                {
                    if (EliminarPedido == 1) { tsEliminarPedido.Enabled = true; }
                }
                else { tsEliminarPedido.Enabled = false; }
            }
            catch { tsEliminarPedido.Enabled = false; }
        }

        private void tsEliminarPedido_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el pedido seleccionado?", "ELIMINAR PEDIDO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int idNroPedido = Convert.ToInt32(dgvListaPedidosVista.GetRowCellValue(dgvListaPedidosVista.FocusedRowHandle, "NRO"));
                int idInspeccionC = Convert.ToInt32(dgvListaPedidosVista.GetRowCellValue(dgvListaPedidosVista.FocusedRowHandle, "idInspeccionC"));

                DataTable dtRespuesta = new DataTable();
                string Respuesta, Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_MttoPreventivo_InsertarPedidosInspecciones(2, idNroPedido, idInspeccionC, "", "", 0, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { ListarPedidos(); }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
