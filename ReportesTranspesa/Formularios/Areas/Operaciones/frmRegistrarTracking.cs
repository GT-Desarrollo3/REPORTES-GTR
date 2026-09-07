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

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class frmRegistrarTracking : Form
    {
        string HoraEvento, _var_nomProducto, _anio, UBIGoogle, UBIGps,
                _codigo, _conductor, _placa, _var_TopoSemir,
                _ruta, _programacion, _idproducto, _OT, guia1, guia2, CNT, NomProduc, UnidadMedida;
        int ConteoProgresoEstadoEvento, _idTipoProg, opcion = 1, idpunto, TipoBusqueda = 1;
        public string placagps;
        public int TipoProgAgregar = 0, _var_IdProg, idTracking = 0, nuevo_modifica = 0;
        public string TipoViaje;
        int CodTipodeViaje, _idcliente;
        public frmRegistrarTracking()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1020, 600);
        }
        
        private void frmRegistrarTracking_Load(object sender, EventArgs e)
        {
            groupBox2.Location = new Point(3, 134);
            
            //Crgado del encabezado
            DataTable DetalleProgramacion = new DataTable();
            DetalleProgramacion = clsOperacionesBL.Instancia.GetListarDetalleProgramacion(_var_IdProg);
            if (DetalleProgramacion.Rows.Count > 0)
            {
                for (int i = 0; i < DetalleProgramacion.Rows.Count; i++)
                {
                    lbCodigoPreViaje.Text = "Codigo PreViaje: " + DetalleProgramacion.Rows[i]["NroTicket"].ToString();
                    lbRuta.Text = "Ruta: " + DetalleProgramacion.Rows[i]["RUTA"].ToString();
                    TipoProgAgregar = Convert.ToInt32(DetalleProgramacion.Rows[i]["idtipoProgramacion"].ToString());
                    lbConductor.Text = "Conductor: " + DetalleProgramacion.Rows[i]["Nombre Conductor"].ToString();
                    lbSemire.Text = "Semire: " + DetalleProgramacion.Rows[i]["Carreta"].ToString();
                    _placa = DetalleProgramacion.Rows[i]["TRACTO"].ToString();
                    lbTracto.Text = "Tracto:" + DetalleProgramacion.Rows[i]["TRACTO"].ToString();
                    lbOperacion.Text = "Operación: " + DetalleProgramacion.Rows[i]["Operacion"].ToString();
                    lbProducto.Text = "Producto: " + DetalleProgramacion.Rows[i]["Producto"].ToString();
                    _anio = DetalleProgramacion.Rows[i]["Anio"].ToString();
                    _idproducto = DetalleProgramacion.Rows[i]["TipoCarga"].ToString();
                }

                ListadoDeEventos();

                comboBox1.Text = "IDA";
                if (_idTipoProg == 0)
                {
                    _idTipoProg = TipoProgAgregar;
                }
                
            }                   

            //////////////////////////////////////////////////////////////////////MODIFICAR//////////////////////////////////////////////////////////////////////
            if (nuevo_modifica == 2)  //CUANDO LLAMAMOS A EDITAR
            {
                opcion = 2; //Cuando es dos modifica el tracking

                DataTable dteditartracking = new DataTable();
                dteditartracking = clsOperacionesBL.Instancia.GetOperaciones_ListarTrackingEditar(idTracking);
                if (dteditartracking.Rows.Count > 0)
                {
                    for (int i = 0; i < dteditartracking.Rows.Count; i++)
                    {
                        idTracking = Convert.ToInt32(dteditartracking.Rows[i]["idTracking"].ToString());
                        _var_IdProg = Convert.ToInt32(dteditartracking.Rows[i]["IdProgramacion"].ToString());
                        _codigo = dteditartracking.Rows[i]["Numero de programacion"].ToString();
                        _idTipoProg = Convert.ToInt32(dteditartracking.Rows[i]["IdTipoProgramacion"].ToString());
                        lbCodigoPreViaje.Text = "Codigo PreViaje:" + Convert.ToInt32(dteditartracking.Rows[i]["Numero de programacion"].ToString());
                        lbRuta.Text = "Ruta: " + dteditartracking.Rows[i]["RUTA"].ToString();
                        lbConductor.Text = "Conductor: " + dteditartracking.Rows[i]["Nombre Conductor"].ToString();
                        lbSemire.Text = "Semire: " + dteditartracking.Rows[i]["Carreta"].ToString();
                        _anio = dteditartracking.Rows[i]["Anio"].ToString();
                        _idproducto = dteditartracking.Rows[i]["TipoCarga"].ToString();
                        lbProducto.Text = "Producto: " + dteditartracking.Rows[i]["Producto"].ToString();
                        lbTracto.Text = "Tracto: " + dteditartracking.Rows[i]["NumeroPlaca"].ToString();
                        lbOperacion.Text = "Operacion: " + dteditartracking.Rows[i]["Operacion"].ToString();
                        txtUbicacion.Text = dteditartracking.Rows[i]["Ubicacion"].ToString();
                        DataTable dtEstado = new DataTable();
                        dtEstado = clsOperacionesBL.Instancia.GetPreviajes_ListarEstadoPorOperacion(_idTipoProg);
                        cboEstadoEvent.DisplayMember = "Descripcion";
                        cboEstadoEvent.ValueMember = "IdEstadoEvento";
                        cboEstadoEvent.DataSource = dtEstado;
                        cboEstadoEvent.Text = dteditartracking.Rows[i]["Estado"].ToString();
                        comboBox1.Text = TipoViaje;
                        TipoBusqueda = 2;

                        ListadoDeEventos();
                    }
                }
            }

            //listado de los consolidados 
            DataTable dtComboConsolidados = new DataTable();
            dtComboConsolidados = clsOperacionesBL.Instancia.GetOperaciones_ListarPorCliente(Convert.ToInt32(_OT), Convert.ToInt32(_anio), Convert.ToInt32(_var_IdProg));
            cbCliente.DisplayMember = "DATOS";
            cbCliente.ValueMember = "IdProducto";
            cbCliente.DataSource = dtComboConsolidados;

            //cargado de los estados para el tracking
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetPreviajes_ListarEstadoPorOperacion(_idTipoProg);
            cboEstadoEvent.DisplayMember = "Descripcion";
            cboEstadoEvent.ValueMember = "IdEstadoEvento";
            cboEstadoEvent.DataSource = dt;       
        }
        public void RegistrarTicket(int nuevomodifca, string codigo, string conductor, string placa, string var_TopoSemir, string ruta, string programacion, string anio, int idTipoProg, string var_nomProducto, string idproducto, int var_IdProg, string OT, int idcliente)
        {
            _codigo = codigo;
            _conductor = conductor;
            _placa = placa;
            _var_IdProg = var_IdProg;
            _var_TopoSemir = var_TopoSemir;
            _ruta = ruta;
            _programacion = programacion;
            _anio = anio;
            _idTipoProg = idTipoProg;
            _idproducto = idproducto;
            _var_nomProducto = var_nomProducto;
            nuevo_modifica = nuevomodifca;
            _OT = OT;
            _idcliente = idcliente;
        }

        private void ListadoDeEventos()
        {
            //cargado de los eventos 
            DataTable dtListadoEventosNuevo = new DataTable();
            dtListadoEventosNuevo = clsOperacionesBL.Instancia.GetPreviajes_ListarEventoPorOperacion(TipoBusqueda, _idTipoProg, Convert.ToInt32(_var_IdProg)
                                                                                                    ,Convert.ToInt32(_anio),_idproducto);
            if (dtListadoEventosNuevo.Rows.Count > 0)
            {
                for (int j = 0; j < dtListadoEventosNuevo.Rows.Count; j++)
                {
                    if (Convert.ToInt32(dtListadoEventosNuevo.Rows[j]["Consolidado"].ToString()) == 1)
                    {
                        gbConsolidado.Visible = true;
                        groupBox2.Location = new Point(3, 250);
                        this.Size = new System.Drawing.Size(1020, 714);
                    }
                    gridControl1.DataSource = dtListadoEventosNuevo;
                    gridView1.Columns["CodigoEvento"].Width = 20;
                    gridView1.OptionsBehavior.Editable = true;
                    gridView1.Columns["FechaEvento"].DisplayFormat.FormatType = FormatType.DateTime;
                    gridView1.Columns["FechaEvento"].DisplayFormat.FormatString = "dd/MM/yyyy";
                   /* gridView1.Columns["HoraEvento"].DisplayFormat.FormatType = FormatType.DateTime;
                    gridView1.Columns["HoraEvento"].DisplayFormat.FormatString = "HH:mm:ss";*/
                    gridView1.Columns["Agregado"].Visible = false;
                    gridView1.Columns["Consolidado"].Visible = false;
                    gridView1.Columns["IdProducto"].Visible = false;
                    for (int i = 0; i < gridView1.DataRowCount; i++)
                    {
                        if (Convert.ToInt32(gridView1.GetRowCellValue(i, "Agregado")) == 1)
                        {
                            gridView1.SelectRow(i);
                        }
                    }

                    RepositoryItemTimeSpanEdit formtoHora = new RepositoryItemTimeSpanEdit();
                    formtoHora.EditFormat.FormatType = FormatType.DateTime;
                    formtoHora.DisplayFormat.FormatString = "HH:mm:ss";
                    gridControl1.RepositoryItems.Add(formtoHora);
                    gridView1.Columns["HoraEvento"].ColumnEdit = formtoHora;             
                }                
            }
        }
        
        private void tmrPorcentaje_Tick(object sender, EventArgs e)
        {
            ConteoProgresoEstadoEvento++;
        }
        private void cboEstadoEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            tmrPorcentaje.Enabled = true;
            pbrEstadoDelEvento.Value = 0;
            if (cboEstadoEvent.Text.Equals("Finalizado"))
            {
                pbrEstadoDelEvento.Value = 100;
            }
            if (cboEstadoEvent.Text.Equals("Pendiente"))
            {
                pbrEstadoDelEvento.Value = 50;
            }
            if (cboEstadoEvent.Text.Equals("Cargado"))
            {
                pbrEstadoDelEvento.Value = 5;
            }
            if (cboEstadoEvent.Text.Equals("Cargado Base"))
            {
                pbrEstadoDelEvento.Value = 1;
            }
            if (cboEstadoEvent.Text.Equals("Descargando"))
            {
                pbrEstadoDelEvento.Value = 100;
            }
            if (cboEstadoEvent.Text.Equals("En Transito"))
            {
                pbrEstadoDelEvento.Value = 70;
            }
            if (cboEstadoEvent.Text.Equals("En transito"))
            {
                pbrEstadoDelEvento.Value = 60;
            }
            if (cboEstadoEvent.Text.Equals("Espera Descarga"))
            {
                pbrEstadoDelEvento.Value = 90;
            }
            if (cboEstadoEvent.Text.Equals("Detenido Ret"))
            {
                pbrEstadoDelEvento.Value = 80;
            }
            if (cboEstadoEvent.Text.Equals("Retorno"))
            {
                pbrEstadoDelEvento.Value = 100;
            }
            if (cboEstadoEvent.Text.Equals("Stand by"))
            {
                pbrEstadoDelEvento.Value = 50;
            }
            if (cboEstadoEvent.Text.Equals(" Retorno"))
            {
                pbrEstadoDelEvento.Value = 80;
            }
        }

        private void lstUbicacion_Enter(object sender, EventArgs e)
        {
            if (!lstUbicacion.Items.Count.Equals(0))
            {
                lstUbicacion.Items[0].Selected = true;
            }
        }
        private void lstUbicacion_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstUbicacion.SelectedItems[0];
            txtUbicacion.Text = ItemActual.SubItems[1].Text;
            idpunto = Convert.ToInt32(ItemActual.Text);
            lstUbicacion.Visible = false;
            txtUbicacion.Focus();
        }
        private void txtUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstUbicacion, clsOperacionesBL.Instancia.GetPuntosPorRutas(txtUbicacion.Text), true, false, false);
                lstUbicacion.Columns[0].Width = 0;
                lstUbicacion.Columns[1].Width = 300;
                lstUbicacion.Size = new System.Drawing.Size(200, 100);
                lstUbicacion.BringToFront();
                lstUbicacion.Visible = true;
                lstUbicacion.Focus();
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                lstUbicacion.Visible = false;
                txtUbicacion.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lstUbicacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstUbicacion.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstUbicacion.SelectedItems[0];
                idpunto = Convert.ToInt32(ItemActual.Text);
                txtUbicacion.Text = ItemActual.SubItems[1].Text;
                lstUbicacion.Visible = false;
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstUbicacion.Visible = false;
                txtUbicacion.Focus();
            }
        }
       
        private void chkConsolidado_CheckedChanged(object sender, EventArgs e)
        {
            gbConsolidado.Visible = chkConsolidado.Checked;
            if (chkConsolidado.Checked == true)
            {
                groupBox2.Location = new Point(3, 250);
                this.Size = new System.Drawing.Size(1020, 714);
            }
            else
            {
                groupBox2.Location = new Point(3, 134);
                this.Size = new System.Drawing.Size(1020, 600);
            }
        }

        private void btnGuardarTrcking_Click(object sender, EventArgs e)
        {
            string Item ="1";
            int SeguimientoxProducto = 0;
            if (txtUbicacion.Text.Length == 0)
            {
                MessageBox.Show("Ingresar Punto","Alerta");
                txtUbicacion.Focus();
                return;
            }

            string idcarga = _idproducto;
            if (chkConsolidado.Checked == true)
            {
                SeguimientoxProducto = 1;
                idcarga = cbCliente.SelectedValue.ToString();
                Item = cbCliente.Text.ToString().Substring(0,1);
            }

            //Modificar contactos 
            string mensaje = "1";
            string rpta = "";
            int[] filas = gridView1.GetSelectedRows();

            if (filas.Length > 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    string IdEvento = gridView1.GetRowCellValue(filas[i], "CodigoEvento").ToString();
                    string FechaEvento = gridView1.GetRowCellValue(filas[i], "FechaEvento").ToString().Substring(0,10) + " " + gridView1.GetRowCellValue(filas[i], "HoraEvento").ToString();
                    string ubiGoolge = gridView1.GetRowCellValue(filas[i], "UbicacionGoogle").ToString();
                    string UbiGeotab = gridView1.GetRowCellValue(filas[i], "UbicacionGeotab").ToString();
                    switch (comboBox1.Text)
                    {
                        case "IDA": CodTipodeViaje = 1;
                            break;

                        case "VUELTA": CodTipodeViaje = 2;
                            break;
                    }

                    DataTable dtGuardar = new DataTable();
                    dtGuardar = clsOperacionesBL.Instancia.GetPreviajes_RegistrarTracking(opcion, Utilitario.Instancia.SesionUsuario.usuario, _var_IdProg, _idTipoProg, FechaEvento, _anio,
                                                                                               idcarga, txtUbicacion.Text,IdEvento, cboEstadoEvent.SelectedValue.ToString(), 
                                                                                               idTracking, ubiGoolge,UbiGeotab, idpunto, CodTipodeViaje, idcarga, 
                                                                                               SeguimientoxProducto,Item);

                    rpta = Convert.ToString(dtGuardar.Rows[0]["exito"]);
                    string NrRPTA = rpta.Substring(0, 1);
                    if (NrRPTA == "0")
                    {
                        mensaje = "S";
                    }
                    else
                    {
                        MessageBox.Show(rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                if (mensaje == "S")
                {
                    MessageBox.Show(rpta, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningún Evento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }

        private void cbCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            _idproducto = cbCliente.SelectedValue.ToString();
            ListadoDeEventos();
        }
    }        
}
    

