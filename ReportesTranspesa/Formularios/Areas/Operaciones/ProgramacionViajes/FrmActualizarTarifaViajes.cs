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
using Entidades;
using Negocio;
using System.Drawing.Printing;
using ReportesTranspesa.Properties;
using System.IO;
using System.Diagnostics;
using System.Data.OleDb;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;
using ReportesTranspesa.Formularios.Areas.Operaciones.OperacionPreViajes;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class FrmActualizarTarifaViajes : Form
    {
        DataTable dtListaTarifas;
        string xmlTarifas = string.Empty;

        DataTable dtListaFacturas;
        string xmlFacturas = string.Empty;

        DataTable dtListaConversion;
        string xmlConversion = string.Empty;

        public FrmActualizarTarifaViajes()
        {
            InitializeComponent();
        }

        private void FrmActualizarTarifaViajes_Load(object sender, EventArgs e)
        {

        }


        public void CargarArchivo()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                op.Title = "Archivo.xlsx";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        txtUbicacion.Text = op.FileName;
                        btnActualizar.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }


        private void btnImportar_Click(object sender, EventArgs e)
        {
            try
            {
                CargarArchivo();

                if (System.IO.File.Exists(txtUbicacion.Text))
                {
                    string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtUbicacion.Text);
                    OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                    string queryDetalle = String.Format("select * from [{0}$]", "ACTUALIZAR");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtListaTarifas = dataSetDetalle.Tables[0];

                    if (!dtListaTarifas.Columns.Contains("VIAJE") || !dtListaTarifas.Columns.Contains("OT") || !dtListaTarifas.Columns.Contains("NUEVOPESO") || !dtListaTarifas.Columns.Contains("NUEVAOT"))
                    {
                        MessageBox.Show("Excel no contiene las columnas necesarias.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    for (int i = dtListaTarifas.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtListaTarifas.Rows[i]["VIAJE"].ToString() == "")  { dtListaTarifas.Rows[i].Delete(); }
                    }
                }

                if (dtListaTarifas != null && dtListaTarifas.Rows.Count > 0)
                {
                    xmlTarifas = "";
                    dtgvData.DataSource = dtListaTarifas;
                    dtgvDataView.BestFitColumns();
                    btnActualizar.Enabled = true;
                    xmlTarifas = Comun.Utilitario.Instancia.DatatableToXml(dtListaTarifas);

                    dtgvDataDespues.DataSource = null;
                    Application.DoEvents();

                    CargarDetalleOTs(dtListaTarifas, 1, dtgvDataAntes, dtgvDataViewAntes);
                }
                else 
                { 
                    dtgvData.DataSource = null; 
                    dtgvDataAntes.DataSource = null;
                    dtgvDataDespues.DataSource = null;
                    btnActualizar.Enabled = false; 
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private DataTable CargarDetalleOTs(DataTable dtExcel, int opcion, DevExpress.XtraGrid.GridControl gridDestino, DevExpress.XtraGrid.Views.Grid.GridView viewDestino)
        {
            DataTable dtResultado = new DataTable();
            try
            {
                if (gridDestino != null)
                {
                    gridDestino.DataSource = null;
                }

                if (dtExcel == null || dtExcel.Rows.Count == 0 || !dtExcel.Columns.Contains("VIAJE"))
                    return dtResultado;

                var listaViajes = dtExcel.AsEnumerable()
                    .Where(r => r.RowState != DataRowState.Deleted && r["VIAJE"] != null && !string.IsNullOrWhiteSpace(r["VIAJE"].ToString()))
                    .Select(r => r["VIAJE"].ToString().Trim())
                    .Distinct()
                    .ToList();

                Cursor.Current = Cursors.WaitCursor;
                bool columnasAsignadas = false;

                foreach (var viaje in listaViajes)
                {
                    DataTable dtViaje = clsOperacionesBL.Instancia.ReportesApp_Operaciones_DatosOT_ListarOTDetalle(opcion, viaje);
                    if (dtViaje != null && dtViaje.Rows.Count > 0)
                    {
                        if (dtResultado.Columns.Count == 0)
                        {
                            dtResultado = dtViaje.Clone();
                            if (gridDestino != null)
                            {
                                gridDestino.DataSource = dtResultado;
                                if (viewDestino != null)
                                {
                                    viewDestino.PopulateColumns();
                                }
                            }
                        }

                        foreach (DataRow row in dtViaje.Rows)
                        {
                            dtResultado.ImportRow(row);
                        }

                        if (gridDestino != null)
                        {
                            gridDestino.RefreshDataSource();
                            if (!columnasAsignadas && viewDestino != null)
                            {
                                viewDestino.BestFitColumns();
                                columnasAsignadas = true;
                            }
                        }
                    }
                    Application.DoEvents();
                }

                if (gridDestino != null)
                {
                    gridDestino.DataSource = dtResultado;
                    gridDestino.RefreshDataSource();
                    if (viewDestino != null)
                    {
                        viewDestino.BestFitColumns();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
            return dtResultado;
        }

        private void ProcesarAccionUnoAUno(
            DataTable dtExcel, 
            int opcion, 
            Func<string, bool> ejecutarAccionSP, 
            DevExpress.XtraGrid.GridControl gridDespues, 
            DevExpress.XtraGrid.Views.Grid.GridView viewDespues)
        {
            try
            {
                if (dtExcel == null || dtExcel.Rows.Count == 0 || !dtExcel.Columns.Contains("VIAJE"))
                {
                    MessageBox.Show("No hay datos importados", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (gridDespues != null)
                {
                    gridDespues.DataSource = null;
                }

                DataTable dtResultado = new DataTable();
                Cursor.Current = Cursors.WaitCursor;
                bool columnasAsignadas = false;
                int exitos = 0;
                int errores = 0;

                var gruposViajes = dtExcel.AsEnumerable()
                    .Where(r => r.RowState != DataRowState.Deleted && r["VIAJE"] != null && !string.IsNullOrWhiteSpace(r["VIAJE"].ToString()))
                    .GroupBy(r => r["VIAJE"].ToString().Trim())
                    .ToList();

                foreach (var grupo in gruposViajes)
                {
                    string viaje = grupo.Key;

                    // Generar XML de forma individual por viaje
                    DataTable dtViajeXml = dtExcel.Clone();
                    foreach (var fila in grupo)
                    {
                        dtViajeXml.ImportRow(fila);
                    }

                    string xmlViaje = Comun.Utilitario.Instancia.DatatableToXml(dtViajeXml);

                    if (!string.IsNullOrEmpty(xmlViaje))
                    {
                        bool res = ejecutarAccionSP(xmlViaje);
                        if (res) exitos++; else errores++;
                    }

                    // Consultar y actualizar la tabla DESPUÉS viaje por viaje en tiempo real
                    DataTable dtDetalleViaje = clsOperacionesBL.Instancia.ReportesApp_Operaciones_DatosOT_ListarOTDetalle(opcion, viaje);
                    if (dtDetalleViaje != null && dtDetalleViaje.Rows.Count > 0)
                    {
                        if (dtResultado.Columns.Count == 0)
                        {
                            dtResultado = dtDetalleViaje.Clone();
                            if (gridDespues != null)
                            {
                                gridDespues.DataSource = dtResultado;
                                if (viewDespues != null)
                                {
                                    viewDespues.PopulateColumns();
                                }
                            }
                        }

                        foreach (DataRow row in dtDetalleViaje.Rows)
                        {
                            dtResultado.ImportRow(row);
                        }

                        if (gridDespues != null)
                        {
                            gridDespues.RefreshDataSource();
                            if (!columnasAsignadas && viewDespues != null)
                            {
                                viewDespues.BestFitColumns();
                                columnasAsignadas = true;
                            }
                        }
                    }

                    Application.DoEvents();
                }

                if (gridDespues != null)
                {
                    gridDespues.DataSource = dtResultado;
                    gridDespues.RefreshDataSource();
                    if (viewDespues != null)
                    {
                        viewDespues.BestFitColumns();
                    }
                }

                if (errores == 0)
                {
                    MessageBox.Show(Utilitario.Instancia.Advertencia ?? "Proceso completado exitosamente.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(string.Format("Proceso finalizado. Exitosos: {0}, con errores: {1}. {2}", exitos, errores, Utilitario.Instancia.Advertencia), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ProcesarAccionUnoAUno(
                dtListaTarifas,
                1,
                xml => clsOperacionesBL.Instancia.ReportesApp_Operaciones_ListarDatosViajesPorFecha_Viaje(xml),
                dtgvDataDespues,
                dtgvDataViewDespues);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                string RUTA = @"\\192.168.4.200\temporal\FormatoActualizarTarifa.xlsx";

                if (System.IO.File.Exists(RUTA)) { System.Diagnostics.Process.Start(RUTA); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnAsignarGrupo_Click(object sender, EventArgs e)
        {
            frmActualizarDatosViajes frmActualizarDatosViajes = new frmActualizarDatosViajes();
            frmActualizarDatosViajes.ShowDialog();
        }

        public void CargarArchivoFacturas()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                op.Title = "Archivo.xlsx";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        txtUbicacionFacturas.Text = op.FileName;
                        btnEnlazarFacturas.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnImportarFacturas_Click(object sender, EventArgs e)
        {
            try
            {
                CargarArchivoFacturas();

                if (System.IO.File.Exists(txtUbicacionFacturas.Text))
                {
                    string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtUbicacionFacturas.Text);
                    OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                    string queryDetalle = String.Format("select * from [{0}$]", "ENLAZAR");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtListaFacturas = dataSetDetalle.Tables[0];

                    if (!dtListaFacturas.Columns.Contains("VIAJE") || !dtListaFacturas.Columns.Contains("DOCUMENTO"))
                    {
                        MessageBox.Show("Excel no contiene las columnas necesarias.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    for (int i = dtListaFacturas.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtListaFacturas.Rows[i]["VIAJE"].ToString() == "") { dtListaFacturas.Rows[i].Delete(); }
                    }
                }

                if (dtListaFacturas != null && dtListaFacturas.Rows.Count > 0)
                {
                    xmlFacturas = "";
                    dtgvDataFacturas.DataSource = dtListaFacturas;
                    dtgvDataViewFacturas.BestFitColumns();
                    btnEnlazarFacturas.Enabled = true;
                    xmlFacturas = Comun.Utilitario.Instancia.DatatableToXml(dtListaFacturas);

                    dtgvDataFacturasDespues.DataSource = null;
                    Application.DoEvents();

                    CargarDetalleOTs(dtListaFacturas, 2, dtgvDataFacturasAntes, dtgvDataViewFacturasAntes);
                }
                else 
                { 
                    dtgvDataFacturas.DataSource = null; 
                    dtgvDataFacturasAntes.DataSource = null;
                    dtgvDataFacturasDespues.DataSource = null;
                    btnEnlazarFacturas.Enabled = false; 
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnEnlazarFacturas_Click(object sender, EventArgs e)
        {
            ProcesarAccionUnoAUno(
                dtListaFacturas,
                2,
                xml => clsOperacionesBL.Instancia.ReportesApp_Operaciones_DatosOT_EnlazarFacturasViaje(xml),
                dtgvDataFacturasDespues,
                dtgvDataViewFacturasDespues);
        }

        private void btnDescargarFormatoFacturas_Click(object sender, EventArgs e)
        {
            try
            {
                string RUTA = @"\\192.168.4.200\temporal\FormatoActualizarTarifa.xlsx";

                if (System.IO.File.Exists(RUTA)) { System.Diagnostics.Process.Start(RUTA); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void CargarArchivoConversion()
        {
            try
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel Sheet(*.xlsx)|*.xlsx|All Files(*.*)|*.*";
                op.Title = "Archivo.xlsx";

                if (op.ShowDialog() == DialogResult.OK)
                {
                    if (op.FileName != "")
                    {
                        txtUbicacionConversion.Text = op.FileName;
                        btnConvertir.Enabled = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnImportarConversion_Click(object sender, EventArgs e)
        {
            try
            {
                CargarArchivoConversion();

                if (System.IO.File.Exists(txtUbicacionConversion.Text))
                {
                    string connectionStringDetalle = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 8.0;HDR=YES;IMEX=1;""", txtUbicacionConversion.Text);
                    OleDbConnection conexion_OleDbDetalle = new OleDbConnection(connectionStringDetalle);
                    string queryDetalle = String.Format("select * from [{0}$]", "CONVERTIR");
                    OleDbDataAdapter dataAdapterDetalle = new OleDbDataAdapter(queryDetalle, conexion_OleDbDetalle);
                    DataSet dataSetDetalle = new DataSet();
                    dataAdapterDetalle.Fill(dataSetDetalle);
                    dataSetDetalle.Tables[0].AsEnumerable().Where(row => row.ItemArray.All(field => field == null || field == DBNull.Value || field.Equals(string.Empty) || field.Equals("#REF!") || string.IsNullOrWhiteSpace(field.ToString()))).ToList().ForEach(row => row.Delete());
                    dataSetDetalle.Tables[0].AcceptChanges();
                    dtListaConversion = dataSetDetalle.Tables[0];

                    if (!dtListaConversion.Columns.Contains("VIAJE") || !dtListaConversion.Columns.Contains("MONEDA") || !dtListaConversion.Columns.Contains("GUIAT"))
                    {
                        MessageBox.Show("Excel no contiene las columnas necesarias (VIAJE, MONEDA, GUIAT).", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    for (int i = dtListaConversion.Rows.Count - 1; i >= 0; i--)
                    {
                        if (dtListaConversion.Rows[i]["VIAJE"].ToString() == "") { dtListaConversion.Rows[i].Delete(); }
                    }
                }

                if (dtListaConversion != null && dtListaConversion.Rows.Count > 0)
                {
                    xmlConversion = "";
                    dtgvDataConversion.DataSource = dtListaConversion;
                    dtgvDataViewConversion.BestFitColumns();
                    btnConvertir.Enabled = true;
                    xmlConversion = Comun.Utilitario.Instancia.DatatableToXml(dtListaConversion);

                    dtgvDataConversionDespues.DataSource = null;
                    Application.DoEvents();

                    CargarDetalleOTs(dtListaConversion, 3, dtgvDataConversionAntes, dtgvDataViewConversionAntes);
                }
                else 
                { 
                    dtgvDataConversion.DataSource = null; 
                    dtgvDataConversionAntes.DataSource = null;
                    dtgvDataConversionDespues.DataSource = null;
                    btnConvertir.Enabled = false; 
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnConvertir_Click(object sender, EventArgs e)
        {
            ProcesarAccionUnoAUno(
                dtListaConversion,
                3,
                xml => clsOperacionesBL.Instancia.ReportesApp_Operaciones_DatosOT_ConvertirFacturasViaje(xml),
                dtgvDataConversionDespues,
                dtgvDataViewConversionDespues);
        }

        private void btnDescargarFormatoConversion_Click(object sender, EventArgs e)
        {
            try
            {
                string RUTA = @"\\192.168.4.200\temporal\FormatoActualizarTarifa.xlsx";

                if (System.IO.File.Exists(RUTA)) { System.Diagnostics.Process.Start(RUTA); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
