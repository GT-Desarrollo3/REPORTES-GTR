using System;
using System.Text;
using Negocio;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Grid;
using ReportesTranspesa.Sistema;
using System.Globalization;
using FMBUtilitario;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Data;
using Word = Microsoft.Office.Interop.Word;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.Map.Native;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DevExpress.XtraGrid;
using DevExpress.Utils.Menu;
using System.Drawing;     
using Comun;
using Microsoft.VisualBasic;


namespace ReportesTranspesa.Formularios.Areas.Operaciones
{
    public partial class Ficha_Conductores : MetroFramework.Forms.MetroForm
    {
        string UsuarioModulo;
        private Word.Application objWord;
        private Object oMissing = System.Reflection.Missing.Value;

        DataTable dtOperacion = new DataTable();
        int IdPersonaOperacion; //VARIABLE PARA ASIGNAR EL ID DEL CONDUCTOR       
        char activos = 'S'; 
        private RecursosHumanos.Memos_Compromisos frmMemos_Compromisos;
        int AccesoModificar = 0;//VARIABLE PARA ACCESOS A MOFICAR OPERACIONES
        bool ModificarTransmision = false;
        bool ModificarBrevete = false;
        bool DesbloquearRuta = false;
        int PersonaActivar, PersonaInactivar, idBloqueo;
        string permisoMemos = "";

        public Ficha_Conductores()
        {
            InitializeComponent();
            cbxMotivoBloqueo.SelectedIndexChanged -= cbxMotivoBloqueo_SelectedIndexChanged;
            cbxEstadoDesbloqueo.SelectedIndexChanged -= cbxEstadoDesbloqueo_SelectedIndexChanged;
        }

        private void cbxMotivoBloqueo_SelectedIndexChanged(object sender, EventArgs e) { CargarCombo(); }

        private void cbxEstadoDesbloqueo_SelectedIndexChanged(object sender, EventArgs e) { CargarCombo2(); }

        private void CargarCombo()
        {
            DataTable dtMotivoBloqueo = clsOperacionesBL.Instancia.ReportesApp_ListarMotivoBloqueo();
            cbxMotivoBloqueo.DataSource = dtMotivoBloqueo;
            cbxMotivoBloqueo.DisplayMember = "Descripcion";
            cbxMotivoBloqueo.ValueMember = "IdMotivo";
        }

        private void CargarCombo2()
        {
            DataTable dtEstadoDesbloq = clsOperacionesBL.Instancia.ReportesApp_ListarEstadoDesbloqueo();
            cbxEstadoDesbloqueo.DataSource = dtEstadoDesbloq;
            cbxEstadoDesbloqueo.DisplayMember = "Descripcion";
            cbxEstadoDesbloqueo.ValueMember = "IdDesbloqueo";
        }

        private void CalcularTotales(bool reset)
        {
            if (reset == true) { lblDespachos2.Text = "0"; }
            else
            {
                int total = dtgvDataView.RowCount;
                lblDespachos2.Text = total.ToString();
            }

            PersonaInactivar = -1;
            txtMotivoInactivar.Text="";
            grouper1.GroupTitle = "BLOQUEO DE CONDUCTORES";
        }

        private void Ficha_Conductores_Load(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            char activos;

            if (rbActivos.Checked == true)
            {
                activos = 'S';
                CalcularTotales(false);
            }
            else
            {
                activos = 'N';
                CalcularTotales(false);
            }
            
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetListaConductores(activos);

            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;

                dtgvDataView.Columns["IND. ACL"].Summary.Clear();
                dtgvDataView.Columns["IND. ACL"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "IND. ACL", "Total: {0}");
                dtgvDataView.Columns["CAP. LG"].Summary.Clear();
                dtgvDataView.Columns["CAP. LG"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CAP. LG", "Total: {0}");
                dtgvDataView.Columns["CSP. PUERTO"].Summary.Clear();
                dtgvDataView.Columns["CSP. PUERTO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CSP. PUERTO", "Total: {0}");
                dtgvDataView.Columns["CAP. DIVEMOTOR"].Summary.Clear();
                dtgvDataView.Columns["CAP. DIVEMOTOR"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CAP. DIVEMOTOR", "Total: {0}");

                dtgvDataView.BestFitColumns();
                CalcularTotales(false);
            }
            else
            {
                CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }

            ConsultaUsuarioSpringxWindows();
            if (UsuarioModulo == null) { UsuarioModulo = Utilitario.Instancia.SesionUsuario.usuario; }

            VerificarPermisoBloquearConductor();
            VerificarPermisoCompromisoMemo();
            VerificarPermisoModificarOperacion();
            VerificarPermisosAgregarBrevete();
            CargarCombo();
            CargarCombo2();

            button1.Enabled = true;
            button5.Enabled = false;

            ListarAdministrativos();
        }

        public void ListarAdministrativos()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetViajes_Conductor_ListarAdministrativos();
            
            if (dt.Rows.Count > 0)
            {
                dtgvData3.DataSource = dt;
                dtgvData3View.BestFitColumns();

                int total = dtgvData3View.RowCount;
                lblDespachos3.Text = total.ToString();
            }
            else
            {
                CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void VerificarPermisosAgregarBrevete()
        {
            if (clsOperacionesBL.Instancia.ReportesApp_OperacionesVerificarTipoBreveteAdicional(UsuarioModulo)) { ModificarBrevete = true; }
            else { ModificarBrevete = false; }
        }

        void VerificarPermisoBloquearConductor()
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsOperacionesBL.Instancia.GetViajes_Conductor_VerificaPermisoBloquear(UsuarioModulo);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0") { grouper1.Visible = true; }
            else { grouper1.Visible = false; }
        }

        void VerificarPermisoCompromisoMemo()
        {
            DataTable dtRespuesta = new DataTable();
            string Respuesta;           

            dtRespuesta = clsOperacionesBL.Instancia.GetViajes_Conductor_VerificaPermisoCompromisoMemo(UsuarioModulo);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);

            if (NroRPTA == "0") { btnMemos2.Visible = true; }
            else { btnMemos2.Visible = false; }
        }

        void VerificarPermisoModificarOperacion()
        {
            //VERIFICA SI EL USUARIO TIENE ACCESOS A MODIFCAR DE OPERACION AL CONDUCTOR
            DataTable dtRespuesta = new DataTable();
            string Respuesta;

            dtRespuesta = clsOperacionesBL.Instancia.GetViajes_Conductor_VerificaPermisoModificarOperacion(UsuarioModulo);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            
            if (NroRPTA == "0") { AccesoModificar = 1; }
            else { AccesoModificar = 0; }

            if (clsOperacionesBL.Instancia.ReportesApp_OperacionesVerificarPermisoTransmision(UsuarioModulo)) { ModificarTransmision = true; }
            else { ModificarTransmision = false; }

            if (clsOperacionesBL.Instancia.ReportesApp_OperacionesVerificarPermisoRutaxConductor(UsuarioModulo)) { DesbloquearRuta = true; }
            else { DesbloquearRuta = false; }
        }

        void ConsultaUsuarioSpringxWindows() 
        {
            DataTable dtUserWind = new DataTable();
            dtUserWind = clsUsuarioBL.Instancia.GetListaUsuariosModulo(Utilitario.Instancia.SesionUsuario.usuario);
            
            for (int i = 0; i < dtUserWind.Rows.Count; i++)
            { UsuarioModulo = dtUserWind.Rows[i]["Usuario"].ToString(); }
        }

        private Microsoft.Office.Interop.Excel.Application app;

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null && dtgvData2.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para exportar";
                m.ShowDialog();
            }
            else
            {
                string tipo = "";

                if (rbActivos.Checked == true) { tipo = "Activos"; }
                else
                {
                    if (rbCesados.Checked == true) { tipo = "Cesados"; }
                    else { tipo = "Bloqueados"; }
                }

                if (tipo == "Activos" || tipo == "Bloqueados")
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Listado conductores " + tipo + " " + UsuarioModulo + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvData.ExportToXlsx(nombre);
                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Visible = true;
                    app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));
                }
                else
                {
                    CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                    DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                    dtfi.TimeSeparator = ".";
                    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                    string nombre = System.IO.Path.Combine(desktop, "Listado conductores " + tipo + " " + UsuarioModulo + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                    dtgvData2.ExportToXlsx(nombre);
                    app = new Microsoft.Office.Interop.Excel.Application();
                    app.Visible = true;
                    app.Workbooks.Open(System.IO.Path.GetFullPath(nombre));
                }
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else { dtgvData.ShowPrintPreview(); }
        }

        private void rbActivos_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            button1.Enabled = true;
            button5.Enabled = false;

            dtgvData.BringToFront();
            dtgvData2.SendToBack();

            char activos = 'S';
            if (rbActivos.Checked == true)
            {
                activos = 'S';
                CalcularTotales(false);
            }

            if (rbCesados.Checked == true)
            {
                activos = 'N';
                CalcularTotales(false);
            }

            if (rbActivos.Checked == true || rbCesados.Checked == true)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsOperacionesBL.Instancia.GetListaConductores(activos);

                if (dt.Rows.Count > 0)
                {
                    dtgvData.DataSource = dt;

                    if (rbActivos.Checked == true)
                    {
                        dtgvDataView.Columns["IND. ACL"].Summary.Clear();
                        dtgvDataView.Columns["IND. ACL"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "IND. ACL", "Total: {0}");
                        dtgvDataView.Columns["CAP. LG"].Summary.Clear();
                        dtgvDataView.Columns["CAP. LG"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CAP. LG", "Total: {0}");
                        dtgvDataView.Columns["CSP. PUERTO"].Summary.Clear();
                        dtgvDataView.Columns["CSP. PUERTO"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CSP. PUERTO", "Total: {0}");
                        dtgvDataView.Columns["CAP. DIVEMOTOR"].Summary.Clear();
                        dtgvDataView.Columns["CAP. DIVEMOTOR"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "CAP. DIVEMOTOR", "Total: {0}");
                    }

                    dtgvDataView.BestFitColumns();
                    CalcularTotales(false);
                }
                else
                {
                    CalcularTotales(true);
                    Mensaje m = new Mensaje();
                    m.mensaje = "No hubo resultados";
                    m.ShowDialog();
                }
            }
        }

        private void rbCesados_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            button1.Enabled = false;
            button5.Enabled = false;
            groupBox1.SendToBack();

            dtgvData.BringToFront();
            dtgvData2.SendToBack();

            char activos;
            if (rbActivos.Checked == true)
            {
                activos = 'S';
                CalcularTotales(false);
            }
            else
            {
                activos = 'N';
                CalcularTotales(false);
            }

            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetListaConductores(activos);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();
                CalcularTotales(false);
            }
            else
            {
                CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void rbBloqueados_Click(object sender, EventArgs e)
        {
            ListarConductoresBloqueados();

            dtgvData2.BringToFront();
            dtgvData.SendToBack();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dtgvDataView.OptionsSelection.MultiSelect = true;
            dtgvDataView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            groupBox1.Text = "MOTIVO BLOQUEO:";
            cbxMotivoBloqueo.Visible = true;
            cbxEstadoDesbloqueo.Visible = false;
            groupBox1.Visible = true;
            groupBox1.BringToFront();
            button6.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox1.SendToBack();
            txtMotivoInactivar.Clear();
            PersonaInactivar = -1;
            PersonaActivar = -1;

            dtgvDataView.OptionsSelection.MultiSelect = false;
            dtgvDataView.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
            dtgvDataView2.OptionsSelection.MultiSelect = false;
            dtgvDataView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.RowSelect;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (txtMotivoInactivar.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese un motivo de bloqueo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                int[] filas = dtgvDataView.GetSelectedRows();
                if (filas.Length != 0)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;
                    int Correcto = 0;

                    if (MessageBox.Show("¿Desea bloquear a los conductores seleccionados?", "BLOQUEAR CONDUCTORES", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < filas.Length; i++)
                        {
                            PersonaInactivar = Convert.ToInt32(dtgvDataView.GetRowCellValue(filas[i], "IDPERSONA"));
                            dtRespuesta = clsOperacionesBL.Instancia.GetViajes_Conductor_BloqueaDesbloquea(1, 0, PersonaInactivar, txtMotivoInactivar.Text, Utilitario.Instancia.SesionUsuario.usuario, Convert.ToInt32(cbxMotivoBloqueo.SelectedValue), 1);
                            respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                            string NroRspta = respta.Substring(0, 1);
                            if (NroRspta == "0") { Correcto = Correcto + 1; }
                            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }

                        if (Correcto == filas.Length) { MessageBox.Show("0 = Los conductores han sido bloqueados exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                        button3_Click(sender, e);
                        ListarConductores();
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un conductor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }

        void ListarConductoresBloqueados()
        {
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetViajes_Conductor_ListarConductoresBloqueados();

            if (dt.Rows.Count > 0)
            {
                dtgvData2.DataSource = dt;
                dtgvDataView2.BestFitColumns();
                lblDespachos2.Text = dt.Rows.Count.ToString();
                button5.Enabled = true;
                button1.Enabled = false;
                txtMotivoInactivar.Text = "";
                PersonaInactivar = -1;
                PersonaActivar = -1;
                grouper1.GroupTitle = "BLOQUEO DE CONDUCTORES";
                dtgvDataView2.Columns["IDBLOQUEO"].Visible = false;
                dtgvDataView2.Columns["IDPERSONA"].Visible = false;
                dtgvDataView2.Columns["IDConductor"].Visible = false;
                
            }
            else
            {
                dtgvData2.DataSource = null;
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dtgvDataView2.OptionsSelection.MultiSelect = true;
            dtgvDataView2.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;

            groupBox1.Text = "MOTIVO DESBLOQUEO:";
            cbxMotivoBloqueo.Visible = false;
            cbxEstadoDesbloqueo.Visible = true;
            groupBox1.Visible = true;
            groupBox1.BringToFront();
            button6.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (txtMotivoInactivar.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese un motivo de desbloqueo.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                int[] filas = dtgvDataView2.GetSelectedRows();
                if (filas.Length != 0)
                {
                    DataTable dtRespuesta = new DataTable();
                    string respta;
                    int Correcto = 0;

                    if (MessageBox.Show("¿Desea desbloquear a los conductores seleccionados?", "DESBLOQUEAR CONDUCTORES", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        for (int i = 0; i < filas.Length; i++)
                        {
                            PersonaActivar = Convert.ToInt32(dtgvDataView2.GetRowCellValue(filas[i], "IDPERSONA"));
                            idBloqueo = Convert.ToInt32(dtgvDataView2.GetRowCellValue(filas[i], "IDBLOQUEO"));

                            dtRespuesta = clsOperacionesBL.Instancia.GetViajes_Conductor_BloqueaDesbloquea(2, idBloqueo, PersonaActivar, txtMotivoInactivar.Text, UsuarioModulo, Convert.ToInt32(cbxMotivoBloqueo.SelectedValue), Convert.ToInt32(cbxEstadoDesbloqueo.SelectedValue));
                            respta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                            string NroRspta = respta.Substring(0, 1);
                            if (NroRspta == "0") { Correcto = Correcto + 1; }
                            else { MessageBox.Show(respta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }

                        if (Correcto == filas.Length) { MessageBox.Show("0 = Los conductores han sido desbloqueados exitosamente.", "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                        button3_Click(sender, e);
                        ListarConductoresBloqueados();
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, seleccione un conductor.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetDataListarUsuariocrearMemos(UsuarioModulo);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                { permisoMemos = Convert.ToString(dt.Rows[i]["carpeta"].ToString()); }
            }

            if (permisoMemos.Equals("")) { MessageBox.Show("No tiene ruta definida para guardar documentos. Comunicarse con Sistemas.", "Aviso"); }
            else { AgregarDocumento(); }
        }

        private void ListarConductores()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsOperacionesBL.Instancia.GetListaConductores(activos);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.BestFitColumns();
                CalcularTotales(false);
            }
            else
            {
                CalcularTotales(true);
                Mensaje m = new Mensaje();
                m.mensaje = "No hubo resultados";
                m.ShowDialog();
            }
        }

        public void AgregarDocumento()
        {
            if (rbActivos.Checked == true)
            {
                int[] filas = dtgvDataView.GetSelectedRows();
                if (filas.Length != 0)
                {
                    if (frmMemos_Compromisos == null || frmMemos_Compromisos.IsDisposed)
                    {
                        frmMemos_Compromisos = new RecursosHumanos.Memos_Compromisos();
                        frmMemos_Compromisos.CargarDoc += new RecursosHumanos.Memos_Compromisos.CargarDocEventHandler(PrepararDoc);
                        //frmMemos_Compromisos.MdiParent = this.ParentForm;
                        //frmMemos_Compromisos.Tipodoc = Tipodoc;
                        //frmMemos_Compromisos.Asuntodoc = Asuntodoc;
                        //frmMemos_Compromisos.Fechadoc = Fechadoc;
                        //frmMemos_Compromisos.Cuerpodoc = Cuerpodoc;
                        frmMemos_Compromisos.Show();
                    }
                    else
                    {
                        frmMemos_Compromisos.Activate();
                    }
                }
            }

            if (rbBloqueados.Checked == true)
            {
                int[] filas = dtgvDataView2.GetSelectedRows();
                if (filas.Length != 0)
                {
                    if (frmMemos_Compromisos == null || frmMemos_Compromisos.IsDisposed)
                    {
                        frmMemos_Compromisos = new RecursosHumanos.Memos_Compromisos();
                        frmMemos_Compromisos.CargarDoc += new RecursosHumanos.Memos_Compromisos.CargarDocEventHandler(PrepararDoc);
                        //frmMemos_Compromisos.MdiParent = this.ParentForm;
                        //frmMemos_Compromisos.Tipodoc = Tipodoc;
                        //frmMemos_Compromisos.Asuntodoc = Asuntodoc;
                        //frmMemos_Compromisos.Fechadoc = Fechadoc;
                        //frmMemos_Compromisos.Cuerpodoc = Cuerpodoc;
                        frmMemos_Compromisos.Show();
                    }
                    else
                    {
                        frmMemos_Compromisos.Activate();
                    }
                }
            }
           
        }

        public void PrepararDoc(Boolean EsCorrecto)
        {

            if (rbActivos.Checked == true)
            {
                if (EsCorrecto)
                {

                    List<int> filas = new List<int>(dtgvDataView.GetSelectedRows());
                    //int[] filas = dtgvDataView.GetSelectedRows();
                    List<string> listadocumentos = new List<string>();
                    string filename = "";
                    int correlativo;
                    //int contador;
                    string empleado = "";
                    string saludo = "";
                    bool resultado;
                    if (frmMemos_Compromisos.Tipodoc == "C")
                    {
                        correlativo = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("CX") + 1;
                        //contador = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("CX") + 1;

                        filename = "T:\\MEMORAMDUM\\" + permisoMemos + "\\Comunicado Interno ";

                        /* if (UsuarioModulo.Equals("AGUERRA"))
                         {
                             filename = "T:\\MEMORAMDUM\\"+permisoMemos+"\\Comunicado Interno ";
                         }
                         if (UsuarioModulo.Equals("JMIRANDA"))
                         {
                             filename = "T:\\MEMORAMDUM\\JMIRANDA\\Comunicado Interno ";
                         }
                         if (UsuarioModulo.Equals("JMIGUEL"))
                         {
                             filename = "T:\\MEMORAMDUM\\JMIGUEL\\Comunicado Interno ";
                         }
                         if (UsuarioModulo.Equals("LLEYTHON"))
                         {
                             filename = "T:\\MEMORAMDUM\\LLEYTHON\\Comunicado Interno ";
                         }*/
                        //filename = "D:\\sescobedo\\MEMORANDUMS\\AÑO 2017\\GERENCIA GENERAL\\COMPROMISOS\\Comunicado Interno ";
                    }
                    else
                    {
                        correlativo = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("MX") + 1;
                        //contador = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("MX") + 1;
                        /* if (UsuarioModulo.Equals("AGUERRA"))
                         {
                             filename = "T:\\MEMORAMDUM\\AGUERRA\\Memorandum ";
                         }
                         if (UsuarioModulo.Equals("JMIRANDA"))
                         {
                             filename = "T:\\MEMORAMDUM\\JMIRANDA\\Memorandum ";
                         }
                         if (UsuarioModulo.Equals("JMIGUEL"))
                         {
                             filename = "T:\\MEMORAMDUM\\JMIGUEL\\Memorandum ";
                         }
                         if (UsuarioModulo.Equals("LLEYTHON"))
                         {
                             filename = "T:\\MEMORAMDUM\\LLEYTHON\\Memorandum ";
                         }*/

                        filename = "T:\\MEMORAMDUM\\" + permisoMemos + "\\Memorandum ";
                        //filename = "T:\\MEMORAMDUM\\AGUERRA\\Memorandum ";
                        //filename = "D:\\sescobedo\\MEMORANDUMS\\AÑO 2017\\GERENCIA GENERAL\\MEMORANDUMS\\Memorandum ";
                    }
                    if (filas[0] == -1)
                    {
                        for (int i = 0; i < filas.Count - 1; i++)
                        {
                            filas[i] = filas[i + 1];
                        }
                        filas.RemoveAt(filas.Count - 1);
                    }
                    for (int i = 0; i < filas.Count; i++)
                    {
                        //if (dtgvDataView.GetRowCellValue(filas[i], "SEXO").ToString() == "M")
                        //{
                        //    empleado = "Sr. " + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString();
                        //    saludo = "Estimado " + empleado; 
                        //}
                        //else 
                        //{
                        //    empleado = "Sra./Srta. " + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString();
                        //    saludo = "Estimada " + empleado; 
                        //}

                        empleado = "Sr(a). " + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString();
                        saludo = "Estimado(a) " + empleado;

                        listadocumentos.Add(generaPDFDocumento(
                            filename + (correlativo + i).ToString() + " " + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString() + ".pdf",
                            (correlativo + i).ToString(),
                            frmMemos_Compromisos.Tipodoc,
                            empleado,
                            dtgvDataView.GetRowCellValue(filas[i], "CARGO").ToString(),
                            frmMemos_Compromisos.Asuntodoc,
                            frmMemos_Compromisos.Fechadoc,
                            saludo,
                            frmMemos_Compromisos.Cuerpodoc,
                            frmMemos_Compromisos.Firma));
                        resultado = clsRecursosHumanosBL.Instancia.InsertDocumento(correlativo + i,
                        Convert.ToInt32(dtgvDataView.GetRowCellValue(filas[i], "IDPERSONA")),
                        frmMemos_Compromisos.Tipodoc, frmMemos_Compromisos.Asuntodoc,
                        frmMemos_Compromisos.Fechadoc, frmMemos_Compromisos.Cuerpodoc, UsuarioModulo);
                        if (resultado != true)
                        {
                            MessageBox.Show("Error al guardar", "Mensaje");
                            break;
                        }
                    }
                    /* DialogResult dialogResult = MessageBox.Show("Se generaro " + filas.Count + " documento satisfactoriamente. ¿Desea imprimir?", "Confirmación", MessageBoxButtons.YesNo);
                     if (dialogResult == DialogResult.Yes)
                     {
                         for (int i = 0; i < listadocumentos.Count; i++)
                         {
                            ImprimirPDF(listadocumentos[i]);
                         }
                     }*/
                    dtgvDataView.ClearSelection();
                }
                else
                {
                    dtgvDataView.ClearSelection();
                }
            }

            if (rbBloqueados.Checked == true)
            {
                if (EsCorrecto)
                {

                    List<int> filas = new List<int>(dtgvDataView2.GetSelectedRows());
                    //int[] filas = dtgvDataView.GetSelectedRows();
                    List<string> listadocumentos = new List<string>();
                    string filename = "";
                    int correlativo;
                    //int contador;
                    string empleado = "";
                    string saludo = "";
                    bool resultado;
                    if (frmMemos_Compromisos.Tipodoc == "C")
                    {
                        correlativo = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("CX") + 1;
                        //contador = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("CX") + 1;

                        filename = "T:\\MEMORAMDUM\\" + permisoMemos + "\\Comunicado Interno ";

                        /* if (UsuarioModulo.Equals("AGUERRA"))
                         {
                             filename = "T:\\MEMORAMDUM\\"+permisoMemos+"\\Comunicado Interno ";
                         }
                         if (UsuarioModulo.Equals("JMIRANDA"))
                         {
                             filename = "T:\\MEMORAMDUM\\JMIRANDA\\Comunicado Interno ";
                         }
                         if (UsuarioModulo.Equals("JMIGUEL"))
                         {
                             filename = "T:\\MEMORAMDUM\\JMIGUEL\\Comunicado Interno ";
                         }
                         if (UsuarioModulo.Equals("LLEYTHON"))
                         {
                             filename = "T:\\MEMORAMDUM\\LLEYTHON\\Comunicado Interno ";
                         }*/
                        //filename = "D:\\sescobedo\\MEMORANDUMS\\AÑO 2017\\GERENCIA GENERAL\\COMPROMISOS\\Comunicado Interno ";
                    }
                    else
                    {
                        correlativo = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("MX") + 1;
                        //contador = clsRecursosHumanosBL.Instancia.GetCorrelativoDocumento("MX") + 1;
                        /* if (UsuarioModulo.Equals("AGUERRA"))
                         {
                             filename = "T:\\MEMORAMDUM\\AGUERRA\\Memorandum ";
                         }
                         if (UsuarioModulo.Equals("JMIRANDA"))
                         {
                             filename = "T:\\MEMORAMDUM\\JMIRANDA\\Memorandum ";
                         }
                         if (UsuarioModulo.Equals("JMIGUEL"))
                         {
                             filename = "T:\\MEMORAMDUM\\JMIGUEL\\Memorandum ";
                         }
                         if (UsuarioModulo.Equals("LLEYTHON"))
                         {
                             filename = "T:\\MEMORAMDUM\\LLEYTHON\\Memorandum ";
                         }*/

                        filename = "T:\\MEMORAMDUM\\" + permisoMemos + "\\Memorandum ";
                        //filename = "T:\\MEMORAMDUM\\AGUERRA\\Memorandum ";
                        //filename = "D:\\sescobedo\\MEMORANDUMS\\AÑO 2017\\GERENCIA GENERAL\\MEMORANDUMS\\Memorandum ";
                    }
                    if (filas[0] == -1)
                    {
                        for (int i = 0; i < filas.Count - 1; i++)
                        {
                            filas[i] = filas[i + 1];
                        }
                        filas.RemoveAt(filas.Count - 1);
                    }
                    for (int i = 0; i < filas.Count; i++)
                    {
                        //if (dtgvDataView.GetRowCellValue(filas[i], "SEXO").ToString() == "M")
                        //{
                        //    empleado = "Sr. " + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString();
                        //    saludo = "Estimado " + empleado; 
                        //}
                        //else 
                        //{
                        //    empleado = "Sra./Srta. " + dtgvDataView.GetRowCellValue(filas[i], "NOMBRE").ToString();
                        //    saludo = "Estimada " + empleado; 
                        //}

                        empleado = "Sr(a). " + dtgvDataView2.GetRowCellValue(filas[i], "NOMBRE").ToString();
                        saludo = "Estimado(a) " + empleado;

                        listadocumentos.Add(generaPDFDocumento(
                            filename + (correlativo + i).ToString() + " " + dtgvDataView2.GetRowCellValue(filas[i], "NOMBRE").ToString() + ".pdf",
                            (correlativo + i).ToString(),
                            frmMemos_Compromisos.Tipodoc,
                            empleado,
                            dtgvDataView2.GetRowCellValue(filas[i], "CARGO").ToString(),
                            frmMemos_Compromisos.Asuntodoc,
                            frmMemos_Compromisos.Fechadoc,
                            saludo,
                            frmMemos_Compromisos.Cuerpodoc,
                            frmMemos_Compromisos.Firma));
                        resultado = clsRecursosHumanosBL.Instancia.InsertDocumento(correlativo + i,
                        Convert.ToInt32(dtgvDataView2.GetRowCellValue(filas[i], "IDPERSONA")),
                        frmMemos_Compromisos.Tipodoc, frmMemos_Compromisos.Asuntodoc,
                        frmMemos_Compromisos.Fechadoc, frmMemos_Compromisos.Cuerpodoc, UsuarioModulo);
                        if (resultado != true)
                        {
                            MessageBox.Show("Error al guardar", "Mensaje");
                            break;
                        }
                    }
                    /* DialogResult dialogResult = MessageBox.Show("Se generaro " + filas.Count + " documento satisfactoriamente. ¿Desea imprimir?", "Confirmación", MessageBoxButtons.YesNo);
                     if (dialogResult == DialogResult.Yes)
                     {
                         for (int i = 0; i < listadocumentos.Count; i++)
                         {
                            ImprimirPDF(listadocumentos[i]);
                         }
                     }*/
                    dtgvDataView2.ClearSelection();
                }
                else
                {
                    dtgvDataView2.ClearSelection();
                }
            }



           
        }

        private string generaPDFDocumento(string filename, string correlativo, string tipodoc, string empleado, string cargo,
            string asunto, string fecha, string saludo, string cuerpo, bool firma)
        {
            try
            {
                foreach (Process proceso in Process.GetProcesses())
                    if (proceso.ProcessName.ToLower().CompareTo("winword") == 0)
                        proceso.Kill();

                string plantilla;
                string Reporte = "C:\\Transpesa\\Plantillas\\reporte.docx";

                //////Eleccion de la plantilla
                if (tipodoc == "C") //compromiso
                {

                    if (firma == true)
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\Comun_interno_firma.docx";
                    }
                    else
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\Comun_interno.docx";
                    }
                }
                else //memo
                {
                    if (firma == true)
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\Memorandum_firma.docx";
                    }
                    else
                    {
                        plantilla = "C:\\Transpesa\\Plantillas\\Memorandum.docx";
                    }
                }
                ///////////////////////////////////////////

                System.IO.File.Copy(plantilla, Reporte, true);

                objWord = new Word.Application();
                objWord.Documents.Open(Reporte, oMissing, oMissing);

                #region Markers

                object Correlativo = "Correlativo";
                object Empleado = "Empleado";
                object Cargo = "Cargo";
                object Asunto = "Asunto";
                object Fecha = "Fecha";
                object Saludo = "Saludo";
                object Cuerpo = "Cuerpo";

                if (objWord.ActiveDocument.Bookmarks.Count > 0)
                {
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Correlativo).Range.Text = correlativo;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Empleado).Range.Text = empleado;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Cargo).Range.Text = cargo;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Asunto).Range.Text = asunto;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Fecha).Range.Text = fecha;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Saludo).Range.Text = saludo;
                    objWord.ActiveDocument.Bookmarks.get_Item(ref Cuerpo).Range.Text = cuerpo;
                }

                #endregion

                objWord.ActiveDocument.Save();

                permisoMemos = "";

                Export.ToPDF(objWord, filename, false, true);
                Process.Start(filename);

                //foreach (Process proceso in Process.GetProcesses())
                //    if (proceso.ProcessName.ToLower().CompareTo("winword") == 0)
                //        proceso.Kill();

                return filename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw;
            }

            try
            {
                objWord.Documents.Close();
                objWord.Quit();
            }
            catch (Exception x)
            {

            }
        }

        private void ImprimirPDF(string ruta)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = ruta;
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();

            p.WaitForInputIdle();
            System.Threading.Thread.Sleep(3000);
            //if (false == p.CloseMainWindow())
            //    p.Kill();
        }

        private void btnListaDesbloqueos_Click(object sender, EventArgs e)
        {
            FrmRptListaHistorialDesbloqueos frm = new FrmRptListaHistorialDesbloqueos();
            frm.ShowDialog();
        }

        private void btnHistorialMemos_Click(object sender, EventArgs e)
        {
            RecursosHumanos.frmListarCompromisosMemorandums frmLisMemos = new RecursosHumanos.frmListarCompromisosMemorandums();
            frmLisMemos.DatoOpcion(3);
            frmLisMemos.WindowState = FormWindowState.Normal;
            frmLisMemos.ShowDialog();

        }

        private void dtgvDataView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {           
            try
            {
                if (AccesoModificar == 1)
                {
                    int[] filass = dtgvDataView.GetSelectedRows();
                    string datoseleccionado = dtgvDataView.GetFocusedValue().ToString();

                    for (int i = 0; i < filass.Length; i++)
                    {
                        IdPersonaOperacion = Convert.ToInt32(dtgvDataView.GetRowCellValue(filass[i], "IDPERSONA").ToString());

                        if (dtgvDataView.FocusedColumn.FieldName == "OPERACION")
                        {
                            e.Menu.Items.Add(new DXMenuItem("SELECCIONAR OPERACION:",
                             new EventHandler((snd, evt) =>
                             {

                             })));
                            e.Menu.Items.Add(new DXMenuItem("&TOLVAS",
                                new EventHandler((snd, evt) =>
                                {
                                    RegistrarOperacion(IdPersonaOperacion, 1);
                                }

                                )));
                            e.Menu.Items.Add(new DXMenuItem("&LINDLEY",
                                new EventHandler(
                                    (snd, evt) =>
                                    {
                                        RegistrarOperacion(IdPersonaOperacion, 2);
                                    }
                                )));
                            e.Menu.Items.Add(new DXMenuItem("&LIMAGAS",
                                new EventHandler(
                                    (snd, evt) =>
                                    {
                                        RegistrarOperacion(IdPersonaOperacion, 3);
                                    }
                                )));
                            e.Menu.Items.Add(new DXMenuItem("&GENERAL",
                                new EventHandler(
                                    (snd, evt) =>
                                    {
                                        RegistrarOperacion(IdPersonaOperacion, 4);
                                    }
                                )));
                            e.Menu.Items.Add(new DXMenuItem("&SIN OPERACION",
                                new EventHandler(
                                    (snd, evt) =>
                                    {
                                        RegistrarOperacion(IdPersonaOperacion, 5);
                                    }
                                )));
                            e.Menu.Items.Add(new DXMenuItem("&MAQ/CAMIONETAS",
                                new EventHandler(
                                    (snd, evt) =>
                                    {
                                        RegistrarOperacion(IdPersonaOperacion, 6);
                                    }
                                )));
                            e.Menu.Items.Add(new DXMenuItem("&LOCAL",
                                new EventHandler(
                                    (snd, evt) =>
                                    {
                                        RegistrarOperacion(IdPersonaOperacion, 9);
                                    }
                                )));

                            e.Menu.Items.Add(new DXMenuItem("&VOLCAN",
                               new EventHandler(
                                   (snd, evt) =>
                                   {
                                       RegistrarOperacion(IdPersonaOperacion, 10);
                                   }
                               )));

                            e.Menu.Items.Add(new DXMenuItem("&SOLGAS",
                               new EventHandler(
                                   (snd, evt) =>
                                   {
                                       RegistrarOperacion(IdPersonaOperacion, 11);
                                   }
                               )));

                            e.Menu.Items.Add(new DXMenuItem("&SOLGAS-GNL",
                               new EventHandler(
                                   (snd, evt) =>
                                   {
                                       RegistrarOperacion(IdPersonaOperacion, 13);
                                   }
                               )));

                            e.Menu.Items.Add(new DXMenuItem("&ESCUELA",
                               new EventHandler(
                                   (snd, evt) =>
                                   {
                                       RegistrarOperacion(IdPersonaOperacion, 12);
                                   }
                               )));
                        }
                    }
                }

                if (ModificarTransmision)
                {
                    int[] filass = dtgvDataView.GetSelectedRows();
                    string datoseleccionado = dtgvDataView.GetFocusedValue().ToString();

                    IdPersonaOperacion = Convert.ToInt32(dtgvDataView.GetRowCellValue(filass[0], "IDPERSONA").ToString());

                    if (dtgvDataView.FocusedColumn.FieldName == "TRANSMISION")
                    {
                        e.Menu.Items.Add(new DXMenuItem("SELECCIONE TIPO DE TRANSMISION:",
                         new EventHandler((snd, evt) =>
                         {

                         })));
                        e.Menu.Items.Add(new DXMenuItem("&MECANICO",
                            new EventHandler((snd, evt) =>
                            {
                                RegistrarTransmision(IdPersonaOperacion, "MECANICO");
                            }

                            )));
                        e.Menu.Items.Add(new DXMenuItem("&AUTOMATICO",
                          new EventHandler((snd, evt) =>
                          {
                              RegistrarTransmision(IdPersonaOperacion, "AUTOMATICO");
                          }

                          )));
                        e.Menu.Items.Add(new DXMenuItem("&MECANICO Y AUTOMATICO",
                          new EventHandler((snd, evt) =>
                          {
                              RegistrarTransmision(IdPersonaOperacion, "MECANICO Y AUTO");
                          }

                          )));
                    }
                }


                if (ModificarBrevete)
                {
                    int[] filass = dtgvDataView.GetSelectedRows();
                    string datoseleccionado = dtgvDataView.GetFocusedValue().ToString();

                    IdPersonaOperacion = Convert.ToInt32(dtgvDataView.GetRowCellValue(filass[0], "IDPERSONA").ToString());

                    if (dtgvDataView.FocusedColumn.FieldName == "TipoBREV.ADD")
                    {
                        string adicionar = Interaction.InputBox("Ingresar", "Tipo de Brevete Adicional").ToUpper();
                        if (adicionar.Length == 0)
                        {
                            MessageBox.Show("El campo no puede estar vacio.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_RegistrarBreveteAdicional(IdPersonaOperacion, adicionar))
                        {
                            MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarConductores();
                        }

                    }
                }


                if (DesbloquearRuta)
                {
 

                    int[] filass = dtgvDataView.GetSelectedRows();
                    string datoseleccionado = dtgvDataView.GetFocusedValue().ToString();

                    IdPersonaOperacion = Convert.ToInt32(dtgvDataView.GetRowCellValue(filass[0], "IDPERSONA").ToString());

                    if (dtgvDataView.FocusedColumn.FieldName == "RUTAS")
                    {

                        e.Menu.Items.Add(new DXMenuItem("AGREGAR O QUITAR RUTA",
                         new EventHandler((snd, evt) =>
                           {
                               frmDesbloquarRutaXConductor frmOpen = new frmDesbloquarRutaXConductor();
                               frmOpen.dniPersona = dtgvDataView.GetRowCellValue(filass[0], "DNI").ToString();
                               frmOpen.nombreConductor = dtgvDataView.GetRowCellValue(filass[0], "NOMBRE").ToString();

                               if (frmOpen.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                               {
                                  
                               }
                               
                           } )));
                
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RegistrarTransmision(int idpersona, string transmision)
        {

            if (clsOperacionesBL.Instancia.ReportesApp_Operaciones_RegistrarTransmision(idpersona, transmision))
            {
                MessageBox.Show(Utilitario.Instancia.Advertencia, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ListarConductores();
            }

        }
        private void RegistrarOperacion(int idpersona, int operacion) 
        {
            DataTable dtGuardarDatos = new DataTable();
            dtGuardarDatos = clsOperacionesBL.Instancia.GetOperaciones_Conductor_AsignarOperacion(idpersona, operacion);

            string Rpta = Convert.ToString(dtGuardarDatos.Rows[0]["Exito"]);
            string valor = Rpta.Substring(0, 1);

            if (valor == "0")
            {
                MessageBox.Show(Rpta, "OPERACION EXITOSA");
                ListarConductores();
            }
            else
            {
                MessageBox.Show(Rpta, "ALERTA");
            }
        }

        private void dtgvDataView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            GridView currentView = sender as GridView;
            DataRow dr = currentView.GetFocusedDataRow();

            if (e.Column.FieldName == "IND. ACL")
            {
                if (e.CellValue.ToString() == "VIGENTE") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "POR VENCER") { e.Appearance.BackColor = Color.FromArgb(255, 255, 0); }

                if (e.CellValue.ToString() == "VENCIDO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }

            if (e.Column.FieldName == "CAP. LG")
            {
                if (e.CellValue.ToString() == "VIGENTE") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "POR VENCER") { e.Appearance.BackColor = Color.FromArgb(255, 255, 0); }

                if (e.CellValue.ToString() == "VENCIDO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }

            if (e.Column.FieldName == "CSP. PUERTO")
            {
                if (e.CellValue.ToString() == "VIGENTE") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "POR VENCER") { e.Appearance.BackColor = Color.FromArgb(255, 255, 0); }

                if (e.CellValue.ToString() == "VENCIDO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }

            if (e.Column.FieldName == "CAP. DIVEMOTOR")
            {
                if (e.CellValue.ToString() == "VIGENTE") { e.Appearance.BackColor = Color.FromArgb(31, 255, 0); }

                if (e.CellValue.ToString() == "POR VENCER") { e.Appearance.BackColor = Color.FromArgb(255, 255, 0); }

                if (e.CellValue.ToString() == "VENCIDO") { e.Appearance.BackColor = Color.FromArgb(255, 0, 0); }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarAdministrativos(); }

        private void btnImprimir3_Click(object sender, EventArgs e)
        {
            if (dtgvData3.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else { dtgvData3.ShowPrintPreview(); }
        }

        private void btnExcel3_Click(object sender, EventArgs e)
        {
            if (dtgvData3.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE ADMINISTRATIVOS - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvData3.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
