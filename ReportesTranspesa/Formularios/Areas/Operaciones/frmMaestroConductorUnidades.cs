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
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmMaestroConductorUnidades : Form
    {
        int PermisoVer1_CreaModifica2= 0;
        string placa;
        int T = 0, L, LM, G, SIN, C, M, LO, MTO;
        public int Tipo;

        public frmMaestroConductorUnidades()
        {
            InitializeComponent();
            cbxTipo.SelectedIndexChanged -= cbxTipo_SelectedIndexChanged;
            cbxSubTipo.SelectedIndexChanged -= cbxSubTipo_SelectedIndexChanged;
            cbxOperaciones.SelectedIndexChanged -= cbxOperaciones_SelectedIndexChanged;
        }

        private void cbxTipo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboTipo(); }

        private void cbxSubTipo_SelectedIndexChanged(object sender, EventArgs e) { CargarComboSubTipo(); }

        private void cbxOperaciones_SelectedIndexChanged(object sender, EventArgs e) { CargarComboOperacion(); }

        private void frmMaestroConductorUnidades_Load(object sender, EventArgs e)
        {
            CargarComboTipo();
            cbxTipo.Text = "TRACTO";
            CargarComboOperacion();
            cbxOperaciones.Text = "TODOS";
            cbxOperaciones_DropDownClosed(sender, e);
            CargarComboSubTipo();
            
            string Respuesta;
            DataTable dtPermiso = new DataTable();
            dtPermiso = clsOperacionesBL.Instancia.GetLista_Consulta_Permiso_AsignarUnidadesConductor(Utilitario.Instancia.SesionUsuario.usuario);
            Respuesta = Convert.ToString(dtPermiso.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0") { PermisoVer1_CreaModifica2 = 2; }
            else { PermisoVer1_CreaModifica2 = 1; }

            ListarDatos();
        }


        private void CargarComboTipo()
        {
            DataTable dtTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(1, 1);
            cbxTipo.DataSource = dtTipo;
            cbxTipo.DisplayMember = "Descripcion";
            cbxTipo.ValueMember = "idTipoVehiculo";
        }

        private void CargarComboSubTipo()
        {
            DataTable dtSubTipo = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(2, Tipo);
            cbxSubTipo.DataSource = dtSubTipo;
            cbxSubTipo.DisplayMember = "Descripcion";
            cbxSubTipo.ValueMember = "idSubTipoVehiculo";
        }

        private void CargarComboOperacion()
        {
            DataTable dtOperaciones = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ConductorUnidades_ListarTipoUnidad(3, 1);
            cbxOperaciones.DataSource = dtOperaciones;
            cbxOperaciones.DisplayMember = "DESCRIPCION";
            cbxOperaciones.ValueMember = "ID";
        }

        private void ListarDatos() 
        {
            DataTable dtListaConductorUnidades = new DataTable();
            dtListaConductorUnidades = clsOperacionesBL.Instancia.GetOperaciones_ListarUnidadesConductor(Convert.ToInt32(cbxTipo.SelectedValue), Convert.ToInt32(cbxSubTipo.SelectedValue), Convert.ToInt32(cbxOperaciones.SelectedValue), txtPlaca.Text);

            T = 0;
            L = 0;
            LM = 0;
            G = 0;
            SIN = 0;
            C = 0;
            M = 0;
            LO = 0;
            MTO = 0;
            LBLCANTIDADES.Text ="TOTAL:"+ dtListaConductorUnidades.Rows.Count.ToString()+ " Unidades";
            if (dtListaConductorUnidades.Rows.Count >= 0)
            {
                for (int i = 0; i < dtListaConductorUnidades.Rows.Count; i++)
                {
                    string Operacion = dtListaConductorUnidades.Rows[i]["IDOPERACION"].ToString();

                    if (Operacion.Equals("1")) { T = T + 1; }
                    if (Operacion.Equals("2")) { L = L + 1; }
                    if (Operacion.Equals("3")) { LM = LM + 1; } 
                    if (Operacion.Equals("4")) { G = G + 1; }
                    if (Operacion.Equals("5")) { SIN = SIN + 1; }
                    if (Operacion.Equals("6")) { M = M + 1; }
                    /*
                    if (Operacion.Equals("7")) { MTO = MTO + 1; }
                    if (Operacion.Equals("8")) { C = C + 1; }
                    */
                    if (Operacion.Equals("9")) { LO = LO + 1; }
                }

                lblTotales.Text = "TOLVAS: " + T + " | LINDLEY: " + L + " | LIMAGAS: " + LM + " | GENERAL: " + G + " | SIN OPERACION: " + SIN + " | MAQ/CAMIONETAS: "
                    + M /*+ " | MANTENIMIENTO: " +MTO+ " | COMBUSTIBLE: "+C*/ + " | LOCAL: " + LO;

                gvcListaConductorUnidades.DataSource = dtListaConductorUnidades;
                grvLsitaConductorUnidades.OptionsBehavior.Editable = false;
                grvLsitaConductorUnidades.Columns["ID"].Visible = false;
                grvLsitaConductorUnidades.Columns["IDOPERACION"].Visible = false;
                grvLsitaConductorUnidades.Columns["idTipoVehiculo"].Visible = false;
                grvLsitaConductorUnidades.Columns["SubTipoVehiculo"].Visible = false;
                grvLsitaConductorUnidades.Columns["PLANOS"].Visible = false;
                /*
                grvLsitaConductorUnidades.Columns["TipoCortina"].Visible = false;
                grvLsitaConductorUnidades.Columns["ModeloChasis"].Visible = false;
                grvLsitaConductorUnidades.Columns["Nivel"].Visible = false;
                grvLsitaConductorUnidades.Columns["TipoNivel"].Visible = false;
                grvLsitaConductorUnidades.Columns["Suspension"].Visible = false;
                grvLsitaConductorUnidades.Columns["Piso"].Visible = false;
                grvLsitaConductorUnidades.Columns["MaterialPiso"].Visible = false;
                */
                grvLsitaConductorUnidades.Columns["FECHA_CREA"].DisplayFormat.FormatType = FormatType.DateTime;
                grvLsitaConductorUnidades.Columns["FECHA_CREA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                grvLsitaConductorUnidades.Columns["FECHA_MODIFICA"].DisplayFormat.FormatType = FormatType.DateTime;
                grvLsitaConductorUnidades.Columns["FECHA_MODIFICA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";
                grvLsitaConductorUnidades.Columns["ULTIMA_FECHA"].DisplayFormat.FormatType = FormatType.DateTime;
                grvLsitaConductorUnidades.Columns["ULTIMA_FECHA"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm:ss";


                /*
                grvLsitaConductorUnidades.Columns[0].Width = 50;
                grvLsitaConductorUnidades.Columns["CONDUCTOR"].Width = 300;
                grvLsitaConductorUnidades.Columns["OPERACION_UNIDAD"].Width = 190;
                grvLsitaConductorUnidades.Columns["OPERACION_CONDUCTOR"].Width = 190;
                grvLsitaConductorUnidades.Columns["USUARIO CREA"].Width = 100;
                grvLsitaConductorUnidades.Columns["FECHA CREA"].Width = 100;
                grvLsitaConductorUnidades.Columns["OBSERVACION"].Width = 200;
                grvLsitaConductorUnidades.Columns["TRANSMISION"].Width = 100;
                grvLsitaConductorUnidades.Columns[14].Width = 150;
                grvLsitaConductorUnidades.Columns[15].Width = 150;
                grvLsitaConductorUnidades.Columns[11].Width = 100;

                grvLsitaConductorUnidades.Columns["UNIDAD"].Summary.Add(DevExpress.Data.SummaryItemType.Custom, "UNIDAD", "Cantidad={0}");
                grvLsitaConductorUnidades.Columns["UNIDAD"].SummaryItem.Tag = 4;
                grvLsitaConductorUnidades.UpdateSummary();
                */

                var rep = gvcListaConductorUnidades.RepositoryItems.Add("CheckEdit") as RepositoryItemCheckEdit;
                rep.ValueChecked = 1;
                rep.ValueUnchecked = 0;

                grvLsitaConductorUnidades.Columns["MOCHILA"].ColumnEdit = rep;
                grvLsitaConductorUnidades.Columns["TOMAFUERZA"].ColumnEdit = rep;
                grvLsitaConductorUnidades.Columns["UREA"].ColumnEdit = rep;
                grvLsitaConductorUnidades.Columns["MANGUERA DE AIRE"].ColumnEdit = rep;
                grvLsitaConductorUnidades.Columns["SEÑALÉTICA ACL"].ColumnEdit = rep;
                grvLsitaConductorUnidades.Columns["LLAVE ORIGINAL"].ColumnEdit = rep;
                grvLsitaConductorUnidades.Columns["LLAVE DUPLICADA"].ColumnEdit = rep;
                grvLsitaConductorUnidades.Columns["CAMARAS"].ColumnEdit = rep;

                grvLsitaConductorUnidades.BestFitColumns();
            }
        }


        private void btnNuevoVehículo_Click(object sender, EventArgs e)
        {
            MaestroUnidadConductor.frmNuevaUnidadConductor frmMaestroUnidades = new MaestroUnidadConductor.frmNuevaUnidadConductor();
            frmMaestroUnidades.DatosEDITAR(PermisoVer1_CreaModifica2, 1, 0, 0, "", 0, "", "", 0, 0, 0, 0, 0, 0, 0, 0, "", 0.00M, 0.00M,"","");
            frmMaestroUnidades.groupBox6.Enabled = false;
            frmMaestroUnidades.cbxTipoCortina.Text = "";
            frmMaestroUnidades.groupBox7.Enabled = false;
            frmMaestroUnidades.cbxModeloChasis.Text = "";
            frmMaestroUnidades.groupBox8.Enabled = false;
            frmMaestroUnidades.cbxNivel.Text = "";
            frmMaestroUnidades.cbxNivel_DropDownClosed(sender, e);
            frmMaestroUnidades.groupBox9.Enabled = false;
            frmMaestroUnidades.groupBox12.Enabled = false;
            frmMaestroUnidades.cbxSuspension.Text = "";
            frmMaestroUnidades.groupBox10.Enabled = false;
            frmMaestroUnidades.groupBox11.Enabled = false;
            frmMaestroUnidades.txtPlanos.Text = "";
            frmMaestroUnidades.txtBitacora.Text = "";
            frmMaestroUnidades.cbxPiso1.Text = "";
            frmMaestroUnidades.cbxPiso_DropDownClosed(sender, e);
            frmMaestroUnidades.cbxPiso2.Text = "";
            frmMaestroUnidades.cbxPiso2_DropDownClosed(sender, e);
            frmMaestroUnidades.ShowDialog();
            ListarDatos();
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarDatos(); }

        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarDatos(); }
        }

        private void cbxOperaciones_DropDownClosed(object sender, EventArgs e) { ListarDatos(); }

        private void cbxTipo_DropDownClosed(object sender, EventArgs e)
        {
            Tipo = Convert.ToInt32(cbxTipo.SelectedValue);
            CargarComboSubTipo();
            //ListarDatos();
        }

        private void cbxSubTipo_DropDownClosed(object sender, EventArgs e) { /*ListarDatos();*/ }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (grvLsitaConductorUnidades.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "Maestro de Unidades en Flota - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                grvLsitaConductorUnidades.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void gvcListaConductorUnidades_DoubleClick(object sender, EventArgs e)
        {
            int ID;
            try
            {
                int[] filass = grvLsitaConductorUnidades.GetSelectedRows();
                string datoseleccionado = grvLsitaConductorUnidades.GetFocusedValue().ToString();

                for (int i = 0; i < filass.Length; i++)
                {
                    int nro = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "N°").ToString());
                    ID = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "ID").ToString());
                    placa = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "UNIDAD").ToString();
                    int idoperacion = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "IDOPERACION").ToString());
                    string OPERACION = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "OPERACION_UNIDAD").ToString();
                    string OBSERVACION = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "OBSERVACION").ToString();
                    int Mochila = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "MOCHILA").ToString());
                    int TomaFuerza = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "TOMAFUERZA").ToString());
                    int Urea = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "UREA").ToString());
                    int Manguera = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "MANGUERA DE AIRE").ToString());
                    int Senaletica = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "SEÑALÉTICA ACL").ToString());
                    int LlaveOriginal = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "LLAVE ORIGINAL").ToString());
                    int LlaveDuplicada = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "LLAVE DUPLICADA").ToString());
                    int Camaras = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "CAMARAS").ToString());
                    string Transmision = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "TRANSMISION").ToString();
                    decimal Peso = Convert.ToDecimal(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "PESO").ToString());
                    decimal Galones = Convert.ToDecimal(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "GALONES").ToString());
                    int NroLlantas = Convert.ToInt32(grvLsitaConductorUnidades.GetRowCellValue(filass[i], "NRO_LLANTAS").ToString());
                    string Planos = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "PLANOS").ToString();
                    string Bitacora = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "BITACORA").ToString();
                    string Bocamaza = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "BOCAMAZA").ToString();

                    // OTselec = gvrUnidades.GetRowCellValue(filas[i], "ID").ToString();
                    MaestroUnidadConductor.frmNuevaUnidadConductor frmNuevo = new MaestroUnidadConductor.frmNuevaUnidadConductor();
                    frmNuevo.Text = "Edicion Bloquear Unidades";
                    frmNuevo.TipoUnidad = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "TIPO_UNIDAD").ToString();
                    frmNuevo.DatosEDITAR(PermisoVer1_CreaModifica2, 2, nro, idoperacion, OPERACION, ID, placa, OBSERVACION, Mochila, TomaFuerza, Urea,
                                         Manguera, Senaletica, LlaveOriginal, LlaveDuplicada, Camaras, Transmision, Peso, Galones, Bitacora, Bocamaza); 

                    if (frmNuevo.TipoUnidad != "SEMIRREMOLQUE")
                    {
                        frmNuevo.cbxTransmision.Enabled = true;
                        frmNuevo.groupBox1.Enabled = true;

                        frmNuevo.groupBox6.Enabled = false;
                        frmNuevo.cbxTipoCortina.Text = "";
                        frmNuevo.groupBox7.Enabled = false;
                        frmNuevo.cbxModeloChasis.Text = "";
                        frmNuevo.groupBox8.Enabled = false;
                        frmNuevo.cbxNivel.Text = "";
                        frmNuevo.cbxNivel_DropDownClosed(sender, e);
                        frmNuevo.groupBox9.Enabled = false;
                        frmNuevo.groupBox12.Enabled = false;
                        frmNuevo.cbxSuspension.Text = "";
                        frmNuevo.groupBox10.Enabled = false;
                        frmNuevo.groupBox11.Enabled = false;
                        frmNuevo.txtPlanos.Text = "";
                        frmNuevo.cbxPiso1.Text = "";
                        frmNuevo.cbxPiso_DropDownClosed(sender, e);
                
                        frmNuevo.cbxPiso2_DropDownClosed(sender, e);
                        frmNuevo.txtNroLlantas.Text = Convert.ToString(NroLlantas);
                    }
                    else
                    {
                        frmNuevo.cbxTransmision.Enabled = false;
                        frmNuevo.groupBox1.Enabled = false;

                        frmNuevo.cbxTipoCortina.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "CORTINA").ToString();
                        frmNuevo.cbxModeloChasis.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "CHASIS").ToString();
                        frmNuevo.cbxNivel.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "NIVEL").ToString();
                        frmNuevo.cbxTipoNivel.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "TIPO_NIVEL").ToString();
                        frmNuevo.cbxSuspension.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "SUSPENSION").ToString();
                        frmNuevo.txtBitacora.Text = Bitacora;
                        frmNuevo.txtNroLlantas.Text = Convert.ToString(NroLlantas);

                        if (frmNuevo.cbxNivel.Text == "02")
                        {
                            frmNuevo.cbxPiso1.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "PISO").ToString();
                            frmNuevo.cbxPisoMaterial1.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "MATERIAL_PISO").ToString();
                            frmNuevo.cbxPiso2.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "PISO_2").ToString();
                            frmNuevo.cbxPisoMaterial2.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "MATERIAL_PISO_2").ToString();
                            frmNuevo.cbxPiso1.Enabled = true;
                            frmNuevo.cbxPisoMaterial1.Enabled = true;
                            frmNuevo.cbxPiso2.Enabled = true;
                            frmNuevo.cbxPisoMaterial2.Enabled = true;
                        }

                        if (frmNuevo.cbxNivel.Text == "01")
                        {
                            frmNuevo.cbxPiso1.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "PISO").ToString();
                            frmNuevo.cbxPisoMaterial1.Text = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "MATERIAL_PISO").ToString();
                            frmNuevo.cbxPiso1.Enabled = true;
                            frmNuevo.cbxPisoMaterial1.Enabled = true;
                            frmNuevo.cbxPiso2.Enabled = false;
                            frmNuevo.cbxPisoMaterial2.Enabled = false;
                        }

                        if (frmNuevo.cbxNivel.Text == "")
                        {
                            frmNuevo.cbxPiso1.Enabled = false;
                            frmNuevo.cbxPisoMaterial1.Enabled = false;
                            frmNuevo.cbxPiso2.Enabled = false;
                            frmNuevo.cbxPisoMaterial2.Enabled = false;
                        }

                        string SubTipo = grvLsitaConductorUnidades.GetRowCellValue(filass[i], "SUBTIPO_UNIDAD").ToString();

                        if (SubTipo == "CORTINERA")
                        {
                            frmNuevo.groupBox11.Enabled = true;
                            frmNuevo.txtPlanos.Text = Planos;
                        }
                        else { frmNuevo.groupBox11.Enabled = false; }
                    }
                    
                    frmNuevo.ShowDialog();
                    ListarDatos();
                }
            }
            catch (Exception) { }
        }

        private void grvLsitaConductorUnidades_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "BLOQUEADO")
            {
                if (e.CellValue.ToString() == "NO") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "SÍ") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }
    }    
}
