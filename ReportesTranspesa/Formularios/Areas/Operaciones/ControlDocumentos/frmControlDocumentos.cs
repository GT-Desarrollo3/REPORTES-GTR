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
using ReportesTranspesa.Formularios.Areas.Operaciones.ControlDocumentos;  // GERARDO - 18/09/23

namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class frmControlDocumentos : Form
    {
        public DataTable DtCompania;
        public DataTable DtTipoDoc;
        public DataTable DtSucursal;
        public DataTable DtTipoRelacion;
        public DataTable DtTipoMoneda;
        public DataTable DtAccesos;
        public DataTable DtTipoUnidades;
        public DataTable DtCategoria;

        public int AccionTipoDoc = 0;
        public int cnt;
        public int Opcion = 0;

        public frmControlDocumentos()
        {
            InitializeComponent();
        }

        private void frmControlDocumentos_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            splitContainer1.SplitterDistance = 87;

            try
            {
                CargarControles();
                cbxFiltro.SelectedIndex = 0;
                txtFiltro.Text = "";
                txtFiltroUnidad.Text = "";
            }
            catch
            {
                MessageBox.Show("No tiene accesos para este módulo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
            }
        }

        private void CargarControles()
        {
            DataTable dtControl = new DataTable();
            dtControl.Clear();
            dtControl = clsControlDocumentosBL.Instancia.getDocumentos_LlenarControlesDocumentos(Utilitario.Instancia.SesionUsuario.usuario);

            if (dtControl == null)
            {
                MessageBox.Show("No se cargaron controles", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (dtControl.Rows.Count > 0)
                {
                    DtCompania = ConvertirXmlToDataTable(dtControl.Rows[0][0].ToString());
                    DtSucursal = ConvertirXmlToDataTable(dtControl.Rows[0][1].ToString());
                    DtTipoDoc = ConvertirXmlToDataTable(dtControl.Rows[0][2].ToString());

                    DtTipoRelacion = ConvertirXmlToDataTable(dtControl.Rows[0][3].ToString());
                    DtTipoMoneda = ConvertirXmlToDataTable(dtControl.Rows[0][4].ToString());
                    DtAccesos = ConvertirXmlToDataTable(dtControl.Rows[0][5].ToString());
                    DtTipoUnidades = ConvertirXmlToDataTable(dtControl.Rows[0][6].ToString());
                    DtCategoria = ConvertirXmlToDataTable(dtControl.Rows[0][7].ToString());

                    //Llenado de Empresas
                    cbxCompania.DataSource = DtCompania;
                    cbxCompania.DisplayMember = "Compania";
                    cbxCompania.ValueMember = "Codigo";
                    cbxCompania.SelectedIndex = 0;

                    //Llenado de TiposDocumentos 
                    cbxTipoDocumento.DataSource = DtTipoDoc;
                    cbxTipoDocumento.DisplayMember = "Descripcion";
                    cbxTipoDocumento.ValueMember = "IdTipoDocumento";
                    cbxTipoDocumento.SelectedIndex = 0;

                    //Llenado de TiposDocumentos 
                    cbxFiltro.DataSource = DtTipoRelacion;
                    cbxFiltro.DisplayMember = "Descripcion";
                    cbxFiltro.ValueMember = "Codigo";
                    cbxFiltro.SelectedIndex = 0;

                    //Llenado de Tipos de Unidades
                    cbxTipoVehiculo.DataSource = DtTipoUnidades;
                    cbxTipoVehiculo.DisplayMember = "Descripcion";
                    cbxTipoVehiculo.ValueMember = "SubTipoVehiculo";
                    cbxTipoVehiculo.SelectedIndex = 0;

                    tsBtnNuevo.Visible = false;
                    actualizarToolStripMenuItem.Visible = false;
                    renovarToolStripMenuItem.Visible = false;
                    anularToolStripMenuItem.Visible = false;
                    //tiposDocumentosToolStripMenuItem.Visible = false;

                    if (DtAccesos != null)
                    {
                        if (DtAccesos.Rows.Count > 0)
                        {
                            int Registrar = Convert.ToInt32(DtAccesos.Rows[0][2].ToString());
                            int Actualizar = Convert.ToInt32(DtAccesos.Rows[0][3].ToString());
                            int Renovar = Convert.ToInt32(DtAccesos.Rows[0][4].ToString());
                            int Anular = Convert.ToInt32(DtAccesos.Rows[0][5].ToString());
                            AccionTipoDoc = Convert.ToInt32(DtAccesos.Rows[0][6].ToString());
                            int Requerimiento = Convert.ToInt32(DtAccesos.Rows[0][7].ToString());

                            tsBtnNuevo.Visible = Convert.ToBoolean(Registrar);
                            actualizarToolStripMenuItem.Visible = Convert.ToBoolean(Actualizar);
                            renovarToolStripMenuItem.Visible = Convert.ToBoolean(Renovar);
                            anularToolStripMenuItem.Visible = Convert.ToBoolean(Anular);
                            tsGenerarRequerimiento.Enabled = Convert.ToBoolean(Requerimiento);
                            //tiposDocumentosToolStripMenuItem.Visible = Convert.ToBoolean(AccionTipoDoc);
                        }
                    }

                    VerDocumentosVencidos();
                }
            }
        }


        private DataTable ConvertirXmlToDataTable(string pXml)
        {
            string xml = pXml;

            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(xml));

            DataTable Dt = new DataTable(Name);

            try
            {
                XmlNode NodoEstructura = doc.FirstChild.FirstChild;
                //  Table structure (columns definition) 
                foreach (XmlNode columna in NodoEstructura.ChildNodes)
                {
                    Dt.Columns.Add(columna.Name, typeof(String));
                }

                XmlNode Filas = doc.FirstChild;
                //  Data Rows 
                foreach (XmlNode Fila in Filas.ChildNodes)
                {
                    List<string> Valores = new List<string>();
                    foreach (XmlNode Columna in Fila.ChildNodes)
                    {
                        Valores.Add(Columna.InnerText);
                    }

                    Dt.Rows.Add(Valores.ToArray());
                }
            }
            catch (Exception)
            { }

            return Dt;
        }


        private void ListarDocumentos()
        {
            DataTable dt = new DataTable();

            dt.Clear();

            int tipoDocumento = Convert.ToInt32(cbxTipoDocumento.SelectedValue.ToString());
            int tipoFiltro = Convert.ToInt32(cbxFiltro.SelectedIndex);

            int tipoUnidad = Convert.ToInt32(cbxTipoVehiculo.SelectedValue.ToString());

            string filtro = "";

            if (cbxFiltro.SelectedIndex == 1)
            {
                filtro = txtFiltroUnidad.Text;
            }
            else
            {
                filtro = txtFiltro.Text;
            }

            dt = clsControlDocumentosBL.Instancia.getDocumentos_ListarDocumentos(cbxCompania.SelectedValue.ToString(), tipoDocumento, tipoFiltro, filtro, tipoUnidad);

            if (dt.Rows.Count > 0)
            {

                dgvDocumentos.DataSource = dt;

                dgvDocumentosView.Columns["IDDOCUMENTO"].Visible = false;
              //  dgvDocumentosView.Columns["SUCURSAL_DESCRIPCION"].Visible = false;
                dgvDocumentosView.Columns["IdRelacion"].Visible = false;
                dgvDocumentosView.Columns["TIPO_RELACION"].Visible = false;
                dgvDocumentosView.Columns["RELACION_CODIGO"].Visible = false;
                dgvDocumentosView.Columns["TipoDocumento"].Visible = false;
                dgvDocumentosView.Columns["TIENE_VENCIMIENTO"].Visible = false;
                dgvDocumentosView.Columns["Marca"].Visible = false;
                dgvDocumentosView.Columns["TipoVehiculo"].Visible = false;
                dgvDocumentosView.Columns["Compania"].Visible = false;
                dgvDocumentosView.Columns["Sucursal"].Visible = false;
                dgvDocumentosView.Columns["MONEDA"].Visible = false;
                dgvDocumentosView.Columns["ES_AFECTOVALIDACION"].Visible = false;
                dgvDocumentosView.Columns["IdCategoria"].Visible = false;
                if (cbxFiltro.Text == "VEHICULO")
                {
                    dgvDocumentosView.Columns["UBICACION"].Visible = true;
                    //dgvDocumentosView.Columns["OPERACION"].Visible = false;
                }
                if (cbxFiltro.Text == "CONDUCTOR")
                {
                    dgvDocumentosView.Columns["UBICACION"].Visible = false;
                    dgvDocumentosView.Columns["OPERACION"].Visible = true;
                }
                if (cbxFiltro.Text == "TODOS")
                {
                    dgvDocumentosView.Columns["UBICACION"].Visible = true;
                    dgvDocumentosView.Columns["OPERACION"].Visible = true;
                }


                GridFormatRule gridFormatRule = new GridFormatRule();
                FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                //Choose predefined icons.
                icon1.PredefinedName = "TrafficLights3_1.png";
                icon2.PredefinedName = "TrafficLights3_2.png";
                icon3.PredefinedName = "TrafficLights3_3.png";

                //Specify the type of threshold values.
                //iconSet.ValueType = FormatConditionValueType.Percent;
                iconSet.ValueType = FormatConditionValueType.Number;

                //Define ranges to which icons are applied by setting threshold values.
                icon1.Value = 1; // target range: 67% <= value
                icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon2.Value = 2; // target range: 33% <= value < 67%
                icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon3.Value = 3; // target range: 0% <= value < 33%
                icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                //Add icons to the icon set.
                iconSet.Icons.Add(icon1);
                iconSet.Icons.Add(icon2);
                iconSet.Icons.Add(icon3);

                //Specify the rule type.
                gridFormatRule.Rule = formatConditionRuleIconSet;
                //Specify the column to which formatting is applied.
                gridFormatRule.Column = dgvDocumentosView.Columns["SEMAFORO"];
                //Add the formatting rule to the GridView.
                dgvDocumentosView.FormatRules.Add(gridFormatRule);

                dgvDocumentosView.BestFitColumns();
            }
            else
            {
                dgvDocumentos.DataSource = null;
                MessageBox.Show("No hay data para mostrar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ListarDocumentos();
        }

        private void VerDocumentosVencidos()
        {
            //vERIFICA CUANTAS PROGRAMACIONES PENDIENTES TIENE EL PROGRAMADOR
            DataTable DtDocVencidos = new DataTable();
            DtDocVencidos = clsControlDocumentosBL.Instancia.getDocumento_VerDocumentosVencidos(Utilitario.Instancia.SesionUsuario.usuario);

            if (DtDocVencidos.Rows.Count > 0)
            {
                for (int i = 0; i < DtDocVencidos.Rows.Count; i++)
                {
                    string valConteo = DtDocVencidos.Rows.Count.ToString();

                    if (!valConteo.Equals("0"))
                    {
                        timer1.Start();
                        lblMensajeAlerta.Visible = true;
                        lblMensajeAlerta.Text = "ADVERTENCIA: Hay " + DtDocVencidos.Rows.Count + " documentos vencidos, favor de regularizar.";
                    }
                    else
                    {
                        timer1.Stop();
                        lblMensajeAlerta.Visible = false;
                    }
                }
            }
        }


        private void tsBtnNuevo_Click(object sender, EventArgs e)
        {
            frmRegistroDocumentos frm = new frmRegistroDocumentos();

            DataRow[] resultRow = DtTipoDoc.Select("IdTipoDocumento>0");
            DataTable DtTipoDocumento = resultRow.CopyToDataTable();

            DataRow[] resultRow2 = DtTipoRelacion.Select("Codigo<>'AL'");
            DataTable DtTipoRela = resultRow2.CopyToDataTable();

            frm.DtCompanias = DtCompania;
            frm.DtSucursal = DtSucursal;
            frm.DtTipoRelacion = DtTipoRela;
            frm.DtTipoMoneda = DtTipoMoneda;
            frm.DtTipoDocumentos = DtTipoDocumento;
            frm.DtCategoria = DtCategoria;
            frm._IdDocumento = 0;
            frm._tipo = 1;
            frm.ShowDialog();

            if (frm.NrRPTA == "0")
            {
                ListarDocumentos();
            }
        }

        private void tsBtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dgvDocumentos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Reporte Control Docuemtos " + DateTime.Now.Year + " " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dgvDocumentos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void tiposDocumentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTipoDocumentos frm = new frmTipoDocumentos();
            frm.DtTipoRelacion = DtTipoRelacion;
            frm.tsBtnNuevo.Visible = Convert.ToBoolean(AccionTipoDoc);
            frm.actualizarToolStripMenuItem.Visible = Convert.ToBoolean(AccionTipoDoc);
            frm.ShowDialog();
        }

        private void anularToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int IdDocumento = 0;
            string usuarioAnula = Utilitario.Instancia.SesionUsuario.usuario;

            foreach(var i in dgvDocumentosView.GetSelectedRows())
            {
                IdDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IDDOCUMENTO"].ToString());
            }


            if (IdDocumento == 0)
            {
                MessageBox.Show("No se seleccionó ningún documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            if (MessageBox.Show("Desea anular documento?", "ANULAR DOCUMENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string Rpta;
                DataTable dtRpta = clsControlDocumentosBL.Instancia.getDocumento_AnularDocumento(IdDocumento, usuarioAnula);

                Rpta = Convert.ToString(dtRpta.Rows[0]["exito"]);
                string NrRPTA = Rpta.Substring(0, 1);

                if (NrRPTA == "0")
                {
                    ListarDocumentos();
                    MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }  
            }
        }

        private void actualizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistroDocumentos frm = new frmRegistroDocumentos();

            DataRow[] resultRow = DtTipoDoc.Select("IdTipoDocumento>0");
            DataTable DtTipoDocumento = resultRow.CopyToDataTable();

            DataRow[] resultRow2 = DtTipoRelacion.Select("Codigo<>'AL'");
            DataTable DtTipoRela = resultRow2.CopyToDataTable();

            frm.DtCompanias = DtCompania;
            frm.DtSucursal = DtSucursal;
            frm.DtTipoRelacion = DtTipoRela;
            frm.DtTipoMoneda = DtTipoMoneda;
            frm.DtTipoDocumentos = DtTipoDocumento;
            frm.DtCategoria = DtCategoria;

            frm.dtpFechaEmision.Enabled = false;
            frm.dtpFechaInicia.Enabled = false;
            frm.dtpFechaVencimiento.Enabled = false;
            frm._tipo = 2;

            int IdDocumento = 0;

            foreach (var i in dgvDocumentosView.GetSelectedRows())
            {
                IdDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IDDOCUMENTO"].ToString());
                frm._IdDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IDDOCUMENTO"].ToString());
                frm._compania = dgvDocumentosView.GetDataRow(i)["Compania"].ToString();
                frm._sucursal = dgvDocumentosView.GetDataRow(i)["Sucursal"].ToString();            
                frm._tipoRelacion = dgvDocumentosView.GetDataRow(i)["TIPO_RELACION"].ToString();
                frm._idRelacion = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IdRelacion"].ToString());
                frm._RelacionNombre = dgvDocumentosView.GetDataRow(i)["RELACION_NOMBRE"].ToString();
                frm._idTipoDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["TipoDocumento"].ToString());
                frm._codigo = dgvDocumentosView.GetDataRow(i)["CODIGO"].ToString();
                frm._centroCosto = dgvDocumentosView.GetDataRow(i)["CENTRO_COSTO"].ToString();
                frm._centroCosto_descripcion = dgvDocumentosView.GetDataRow(i)["CENTROCOSTO_DESCRIPCION"].ToString();
                frm._fechaEmision = dgvDocumentosView.GetDataRow(i)["FECHA_EMISION"].ToString();
                frm._fechaInicioValidez = dgvDocumentosView.GetDataRow(i)["FECHA_INICIO_VALIDEZ"].ToString();
                frm._fechaFinValidez = dgvDocumentosView.GetDataRow(i)["FECHA_FIN_VALIDEZ"].ToString();
                frm._moneda = dgvDocumentosView.GetDataRow(i)["MONEDA"].ToString();
                frm._montoTotal = Convert.ToDecimal(dgvDocumentosView.GetDataRow(i)["MONTO_TOTAL"].ToString());
                frm._esAfectoValidacion = Convert.ToBoolean(dgvDocumentosView.GetDataRow(i)["ES_AFECTOVALIDACION"]);
                frm._esVencimiento = Convert.ToBoolean(dgvDocumentosView.GetDataRow(i)["TIENE_VENCIMIENTO"]);
                frm._marca = dgvDocumentosView.GetDataRow(i)["Marca"].ToString();
                frm._tipoVehiculo = dgvDocumentosView.GetDataRow(i)["TipoVehiculo"].ToString();
                frm.txtObservacion.Text = dgvDocumentosView.GetDataRow(i)["Observacion"].ToString();
                frm._observacion = dgvDocumentosView.GetDataRow(i)["OBSERVACION"].ToString();
                frm._RutaLocal = dgvDocumentosView.GetDataRow(i)["DIRECTORIO_LOCAL"].ToString();
                frm._RutaNube = dgvDocumentosView.GetDataRow(i)["DIRECTORIO_NUBE"].ToString();
                frm._idCategoria = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IdCategoria"].ToString());
                frm._Operacion = dgvDocumentosView.GetDataRow(i)["OPERACION"].ToString();
            }

            if (IdDocumento == 0)
            {
                MessageBox.Show("No se seleccionó ningún documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frm.ShowDialog();

            if (frm.NrRPTA == "0")
            {
               // ListarDocumentos();
            }
        }

        private void renovarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistroDocumentos frm = new frmRegistroDocumentos();

            DataRow[] resultRow = DtTipoDoc.Select("IdTipoDocumento>0");
            DataTable DtTipoDocumento = resultRow.CopyToDataTable();

            DataRow[] resultRow2 = DtTipoRelacion.Select("Codigo<>'AL'");
            DataTable DtTipoRela = resultRow2.CopyToDataTable();

            frm.DtCompanias = DtCompania;
            frm.DtSucursal = DtSucursal;
            frm.DtTipoRelacion = DtTipoRela;
            frm.DtTipoMoneda = DtTipoMoneda;
            frm.DtTipoDocumentos = DtTipoDocumento;
            frm.DtCategoria = DtCategoria;

            frm.dtpFechaEmision.Enabled = false;
            frm.dtpFechaInicia.Enabled = false;
            frm.dtpFechaVencimiento.Enabled = false;
            frm._tipo = 3;

            int IdDocumento = 0; 

            foreach (var i in dgvDocumentosView.GetSelectedRows())
            {
                IdDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IDDOCUMENTO"].ToString());
                frm._IdDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IDDOCUMENTO"].ToString());
                frm._compania = dgvDocumentosView.GetDataRow(i)["Compania"].ToString();
                frm._sucursal = dgvDocumentosView.GetDataRow(i)["Sucursal"].ToString();
                frm._tipoRelacion = dgvDocumentosView.GetDataRow(i)["TIPO_RELACION"].ToString();
                frm._idRelacion = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IdRelacion"].ToString());
                frm._RelacionNombre = dgvDocumentosView.GetDataRow(i)["RELACION_NOMBRE"].ToString();
                frm._idTipoDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["TipoDocumento"].ToString());
                frm._codigo = dgvDocumentosView.GetDataRow(i)["CODIGO"].ToString();
                frm._centroCosto = dgvDocumentosView.GetDataRow(i)["CENTRO_COSTO"].ToString();
                frm._centroCosto_descripcion = dgvDocumentosView.GetDataRow(i)["CENTROCOSTO_DESCRIPCION"].ToString();
                frm._fechaEmision = dgvDocumentosView.GetDataRow(i)["FECHA_EMISION"].ToString();
                frm._fechaInicioValidez = dgvDocumentosView.GetDataRow(i)["FECHA_INICIO_VALIDEZ"].ToString();
                frm._fechaFinValidez = dgvDocumentosView.GetDataRow(i)["FECHA_FIN_VALIDEZ"].ToString();
                frm._moneda = dgvDocumentosView.GetDataRow(i)["MONEDA"].ToString();
                frm._montoTotal = Convert.ToDecimal(dgvDocumentosView.GetDataRow(i)["MONTO_TOTAL"].ToString());
                frm._esAfectoValidacion = Convert.ToBoolean(dgvDocumentosView.GetDataRow(i)["ES_AFECTOVALIDACION"]);
                frm._esVencimiento = Convert.ToBoolean(dgvDocumentosView.GetDataRow(i)["TIENE_VENCIMIENTO"]);
                frm._marca = dgvDocumentosView.GetDataRow(i)["Marca"].ToString();
                frm._tipoVehiculo = dgvDocumentosView.GetDataRow(i)["TipoVehiculo"].ToString();
                frm.txtObservacion.Text = dgvDocumentosView.GetDataRow(i)["OBSERVACION"].ToString();
                frm._observacion = dgvDocumentosView.GetDataRow(i)["OBSERVACION"].ToString();
                frm._RutaLocal = dgvDocumentosView.GetDataRow(i)["DIRECTORIO_LOCAL"].ToString();
                frm._RutaNube = dgvDocumentosView.GetDataRow(i)["DIRECTORIO_NUBE"].ToString();
                frm._idCategoria = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IdCategoria"].ToString());
                frm._Operacion = dgvDocumentosView.GetDataRow(i)["OPERACION"].ToString();
            }

            if (IdDocumento == 0)
            {
                MessageBox.Show("No se seleccionó ningún documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frm.ShowDialog();

            if (frm.NrRPTA == "0")
            {
               // ListarDocumentos();
            }
        }

        private void verDetalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistroDocumentos frm = new frmRegistroDocumentos();

            DataRow[] resultRow = DtTipoDoc.Select("IdTipoDocumento>0");
            DataTable DtTipoDocumento = resultRow.CopyToDataTable();

            DataRow[] resultRow2 = DtTipoRelacion.Select("Codigo<>'AL'");
            DataTable DtTipoRela = resultRow2.CopyToDataTable();

            frm.DtCompanias = DtCompania;
            frm.DtSucursal = DtSucursal;
            frm.DtTipoRelacion = DtTipoRela;
            frm.DtTipoMoneda = DtTipoMoneda;
            frm.DtTipoDocumentos = DtTipoDocumento;
            frm.DtCategoria = DtCategoria;

            frm.tsBtnGuardar.Visible = false;

            frm.dtpFechaEmision.Enabled = false;
            frm.dtpFechaInicia.Enabled = false;
            frm.dtpFechaVencimiento.Enabled = false;
            frm._tipo = 4;

            int IdDocumento = 0;

            foreach (var i in dgvDocumentosView.GetSelectedRows())
            {
                IdDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IDDOCUMENTO"].ToString());
                frm._IdDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IDDOCUMENTO"].ToString());
                frm._compania = dgvDocumentosView.GetDataRow(i)["Compania"].ToString();
                frm._sucursal = dgvDocumentosView.GetDataRow(i)["Sucursal"].ToString();
                frm._tipoRelacion = dgvDocumentosView.GetDataRow(i)["TIPO_RELACION"].ToString();
                frm._idRelacion = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IdRelacion"].ToString());
                frm._RelacionNombre = dgvDocumentosView.GetDataRow(i)["RELACION_NOMBRE"].ToString();
                frm._idTipoDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["TipoDocumento"].ToString());
                frm._codigo = dgvDocumentosView.GetDataRow(i)["CODIGO"].ToString();
                frm._centroCosto = dgvDocumentosView.GetDataRow(i)["CENTRO_COSTO"].ToString();
                frm._centroCosto_descripcion = dgvDocumentosView.GetDataRow(i)["CENTROCOSTO_DESCRIPCION"].ToString();
                frm._fechaEmision = dgvDocumentosView.GetDataRow(i)["FECHA_EMISION"].ToString();
                frm._fechaInicioValidez = dgvDocumentosView.GetDataRow(i)["FECHA_INICIO_VALIDEZ"].ToString();
                frm._fechaFinValidez = dgvDocumentosView.GetDataRow(i)["FECHA_FIN_VALIDEZ"].ToString();
                frm._moneda = dgvDocumentosView.GetDataRow(i)["MONEDA"].ToString();
                frm._montoTotal = Convert.ToDecimal(dgvDocumentosView.GetDataRow(i)["MONTO_TOTAL"].ToString());
                frm._esAfectoValidacion = Convert.ToBoolean(dgvDocumentosView.GetDataRow(i)["ES_AFECTOVALIDACION"]);
                frm._esVencimiento = Convert.ToBoolean(dgvDocumentosView.GetDataRow(i)["TIENE_VENCIMIENTO"]);
                frm._marca = dgvDocumentosView.GetDataRow(i)["Marca"].ToString();
                frm._tipoVehiculo = dgvDocumentosView.GetDataRow(i)["TipoVehiculo"].ToString();
                frm._observacion = dgvDocumentosView.GetDataRow(i)["OBSERVACION"].ToString();
                frm._RutaLocal = dgvDocumentosView.GetDataRow(i)["DIRECTORIO_LOCAL"].ToString();
                frm._RutaNube = dgvDocumentosView.GetDataRow(i)["DIRECTORIO_NUBE"].ToString();
                frm._idCategoria = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IdCategoria"].ToString());
                frm._Operacion = dgvDocumentosView.GetDataRow(i)["OPERACION"].ToString();
            }

            if (IdDocumento == 0)
            {
                MessageBox.Show("No se seleccionó ningún documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frm.ShowDialog();
        }

        private void dgvDocumentos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            frmRegistroDocumentos frm = new frmRegistroDocumentos();

            DataRow[] resultRow = DtTipoDoc.Select("IdTipoDocumento>0");
            DataTable DtTipoDocumento = resultRow.CopyToDataTable();

            DataRow[] resultRow2 = DtTipoRelacion.Select("Codigo<>'AL'");
            DataTable DtTipoRela = resultRow2.CopyToDataTable();

            frm.DtCompanias = DtCompania;
            frm.DtSucursal = DtSucursal;
            frm.DtTipoRelacion = DtTipoRela;
            frm.DtTipoMoneda = DtTipoMoneda;
            frm.DtTipoDocumentos = DtTipoDocumento;
            frm.DtCategoria = DtCategoria;

            frm.tsBtnGuardar.Visible = false;

            frm.dtpFechaEmision.Enabled = false;
            frm.dtpFechaInicia.Enabled = false;
            frm.dtpFechaVencimiento.Enabled = false;
            frm._tipo = 4;

            int IdDocumento = 0;

            foreach (var i in dgvDocumentosView.GetSelectedRows())
            {
                IdDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IDDOCUMENTO"].ToString());
                frm._IdDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IDDOCUMENTO"].ToString());
                frm._compania = dgvDocumentosView.GetDataRow(i)["Compania"].ToString();
                frm._sucursal = dgvDocumentosView.GetDataRow(i)["Sucursal"].ToString();
                frm._tipoRelacion = dgvDocumentosView.GetDataRow(i)["TIPO_RELACION"].ToString();
                frm._idRelacion = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IdRelacion"].ToString());
                frm._RelacionNombre = dgvDocumentosView.GetDataRow(i)["RELACION_NOMBRE"].ToString();
                frm._idTipoDocumento = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["TipoDocumento"].ToString());
                frm._codigo = dgvDocumentosView.GetDataRow(i)["CODIGO"].ToString();
                frm._centroCosto = dgvDocumentosView.GetDataRow(i)["CENTRO_COSTO"].ToString();
                frm._centroCosto_descripcion = dgvDocumentosView.GetDataRow(i)["CENTROCOSTO_DESCRIPCION"].ToString();
                frm._fechaEmision = dgvDocumentosView.GetDataRow(i)["FECHA_EMISION"].ToString();
                frm._fechaInicioValidez = dgvDocumentosView.GetDataRow(i)["FECHA_INICIO_VALIDEZ"].ToString();
                frm._fechaFinValidez = dgvDocumentosView.GetDataRow(i)["FECHA_FIN_VALIDEZ"].ToString();
                frm._moneda = dgvDocumentosView.GetDataRow(i)["MONEDA"].ToString();
                frm._montoTotal = Convert.ToDecimal(dgvDocumentosView.GetDataRow(i)["MONTO_TOTAL"].ToString());
                frm._esAfectoValidacion = Convert.ToBoolean(dgvDocumentosView.GetDataRow(i)["ES_AFECTOVALIDACION"]);
                frm._esVencimiento = Convert.ToBoolean(dgvDocumentosView.GetDataRow(i)["TIENE_VENCIMIENTO"]);
                frm._marca = dgvDocumentosView.GetDataRow(i)["Marca"].ToString();
                frm._tipoVehiculo = dgvDocumentosView.GetDataRow(i)["TipoVehiculo"].ToString();
                frm._observacion = dgvDocumentosView.GetDataRow(i)["OBSERVACION"].ToString();
                frm._RutaLocal = dgvDocumentosView.GetDataRow(i)["DIRECTORIO_LOCAL"].ToString();
                frm._RutaNube = dgvDocumentosView.GetDataRow(i)["DIRECTORIO_NUBE"].ToString();
                frm._idCategoria = Convert.ToInt32(dgvDocumentosView.GetDataRow(i)["IdCategoria"].ToString());
                frm._Operacion = dgvDocumentosView.GetDataRow(i)["OPERACION"].ToString();
            }

            if (IdDocumento == 0)
            {
                MessageBox.Show("No se seleccionó ningún documento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            frm.ShowDialog(); 
        }

        private void cbxFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxFiltro.SelectedIndex == 1)
            {
                cbxTipoVehiculo.Visible = true ;
                txtFiltro.Visible = false;
                txtFiltroUnidad.Visible = true;
                txtFiltroUnidad.Text = "";
                txtFiltro.Text = "";
            }
            else
            {
                cbxTipoVehiculo.Visible = false;
                txtFiltro.Visible = true;
                txtFiltroUnidad.Visible = false;
                txtFiltroUnidad.Text = "";
                txtFiltro.Text = "";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cnt == 0)
            {
                lblMensajeAlerta.Visible = true;
            }

            cnt += 1;
            if (cnt == 4)
            {
                lblMensajeAlerta.Visible = false;
                cnt = 0;
            }
        }

        private void conductoresSinEMOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRptConductoresSinEMO frm = new frmRptConductoresSinEMO();
            frm.DtCompania = DtCompania;
            frm.ShowDialog();
        }

        private void historialDeActualizacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHistorialDocumentos frm = new frmHistorialDocumentos();
            frm.ShowDialog();
        }

        private void tsGenerarRequerimiento_Click(object sender, EventArgs e)
        {
            try
            {
                if (Opcion == 0)
                {
                    tsGenerarRequerimiento.Text = "&Generar Requerimiento";
                    dgvDocumentosView.OptionsSelection.MultiSelect = true;
                    dgvDocumentosView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
                    Opcion = 1;
                }
                else
                {
                    int[] filas = dgvDocumentosView.GetSelectedRows();
                    if (filas.Length != 0)
                    {
                        if (MessageBox.Show("¿Desea generar un requerimiento para estos documentos?", "GENERAR REQUERIMIENTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            for (int i = 0; i < filas.Length; i++)
                            {
                                int idDocumento = Convert.ToInt32(dgvDocumentosView.GetRowCellValue(filas[i], "IDDOCUMENTO"));
                                string Tipo = Convert.ToString(dgvDocumentosView.GetRowCellValue(filas[i], "TIPO"));
                                string TipoDocumento = Convert.ToString(dgvDocumentosView.GetRowCellValue(filas[i], "TIPO_DOCUMENTO"));
                                int idUnidad = Convert.ToInt32(dgvDocumentosView.GetRowCellValue(filas[i], "IdRelacion"));
                                string Placa = Convert.ToString(dgvDocumentosView.GetRowCellValue(filas[i], "RELACION_NOMBRE"));
                                string CentroCosto = Convert.ToString(dgvDocumentosView.GetRowCellValue(filas[i], "CENTRO_COSTO"));
                                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;

                                if ((dgvDocumentosView.GetRowCellValue(filas[i], "TIPO").ToString()) == "CONDUCTOR")
                                { MessageBox.Show("No se puede generar el requerimiento porque ha seleccionado a un conductor.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                                else
                                {
                                    DataTable dtRespuesta = new DataTable();
                                    string respta;

                                    dtRespuesta = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ControlDocumentos_GenerarRequerimiento(idDocumento, TipoDocumento, idUnidad, Placa, CentroCosto, Usuario);
                                    respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                                    string NroRspta = respta.Substring(0, 1);
                                    if (NroRspta == "0")
                                    {
                                        MessageBox.Show(respta, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }
                            }

                            tsGenerarRequerimiento.Text = "&Seleccionar Unidades";
                            dgvDocumentosView.OptionsSelection.MultiSelect = false;
                            dgvDocumentosView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                            Opcion = 0;

                            ListarDocumentos();
                        }
                    }
                    else
                    {
                        tsGenerarRequerimiento.Text = "&Seleccionar Unidades";
                        dgvDocumentosView.OptionsSelection.MultiSelect = false;
                        dgvDocumentosView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
                        Opcion = 0;
                    }
                }
            }
            catch { MessageBox.Show("Se produjo un error al generar el requerimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
