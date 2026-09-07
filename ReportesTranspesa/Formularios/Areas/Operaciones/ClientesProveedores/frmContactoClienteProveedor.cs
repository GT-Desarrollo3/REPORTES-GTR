using System;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using System.Diagnostics;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils.Serializing;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;
using ReportesTranspesa.Sistema;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ClientesProveedores
{
    public partial class frmContactoClienteProveedor : Form
    {
        string UsuarioModulo;
        int _idpersona, _idcontacto;
        string _ruc, _compania;
        string _razonsocial, _Observacion;
        string _tipo;
        string _telefono, _celular;     // GERARDO - 07/11
        char CaracterTipo;
        public int xClick = 0, yClick = 0;

        public bool AccesoAgregar;
        public bool AccesoModifica;
        public bool Accesoquitar;

        public frmContactoClienteProveedor()
        {
            InitializeComponent();
        }

        private void frmContactoClienteProveedor_Load(object sender, EventArgs e)
        {
            grdvListaContactos.OptionsBehavior.Editable = false;
            ConsultaUsuarioSpringxWindows();
            if (UsuarioModulo == null)
            {
                UsuarioModulo = Utilitario.Instancia.SesionUsuario.usuario;
            }

            //VALIDACION DE ACCESOS
            if (AccesoAgregar == true)
            {
                btnAgregar.Enabled = true;
                grdvListaContactos.OptionsBehavior.Editable = true;
                btnAgregarC.Enabled = true;
            }
            else
            {
                btnAgregar.Enabled = false;
                btnAgregarC.Enabled = false;
            }

            if (AccesoModifica == true)
            {
                btnModificar.Enabled = true;
                grdvListaContactos.OptionsBehavior.Editable = true;
                btnModificarC.Enabled = true;
                txtObservacion.ReadOnly = false;
                btnGuardarObserv.Visible = true;
            }
            else
            {
                btnModificar.Enabled = false;
                btnModificarC.Enabled = false;
            }

            if (Accesoquitar == true)
            {
                btnQuitar.Enabled = true;
                grdvListaContactos.OptionsBehavior.Editable = true;
                btnQuitarC.Enabled = true;
            }
            else
            {
                btnQuitar.Enabled = false;
                btnQuitarC.Enabled = false;
            }

            gridControl1.DataSource = null;
            txtTipo.Text = _tipo;
            txtRuc.Text = _ruc;
            txtRazonSocial.Text = _razonsocial;
            txtCompania.Text = _compania;
            txtObservacion.Text = _Observacion;
            txtTelefono.Text = _telefono + " - " + _celular;     // GERARDO - 07/11

            if (_tipo == "CLIENTE")
            {
                CaracterTipo = 'C';
                label1.Text = "LISTA DE CONTACTOS DEL CLIENTE";
                labelContratos.Text = "LISTA DE CONTRATOS DEL CLIENTE";
            }
            else
            {
                CaracterTipo = 'P';
                label1.Text = "LISTA DE CONTACTOS DEL PROVEEDOR";
                labelContratos.Text = "LISTA DE CONTRATOS DEL PROVEEDOR";
            }

            listarContactos();
            ListarContratos();
        }

        // GERARDO - 07/11
        public void datos(int idpersona, int idcontacto, string ruc, string razonsocial, string tipo, string Compania, string Observacion, string telefono, string celular)
        {
            _idpersona = idpersona;
            _ruc = ruc;
            _razonsocial = razonsocial;
            _tipo = tipo;
            _idcontacto = idcontacto;
            _Observacion = Observacion;
            _compania = Compania;
            _telefono = telefono;
            _celular = celular;
        }
        // GERARDO - 07/11

        void ConsultaUsuarioSpringxWindows()
        {
            DataTable dtUserWind = new DataTable();
            dtUserWind = clsUsuarioBL.Instancia.GetListaUsuariosModulo(Utilitario.Instancia.SesionUsuario.usuario);
            for (int i = 0; i < dtUserWind.Rows.Count; i++)
            {
                UsuarioModulo = dtUserWind.Rows[i]["Usuario"].ToString();
            }
        }

        private void listarContactos()
        {
            gridControl1.DataSource = null;
            DataTable dtListContactos = new DataTable();
            dtListContactos = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_ContactosListar(_idpersona, CaracterTipo, 1);
            if (dtListContactos.Rows.Count > 0)
            {
                gridControl1.DataSource = dtListContactos;
                grdvListaContactos.Columns["N°"].Width = 50;
                grdvListaContactos.Columns["NombreContacto"].Width = 200;
                grdvListaContactos.Columns["Cargo"].Width = 200;
                grdvListaContactos.Columns["Correo"].Width = 200;
                grdvListaContactos.Columns["Direccion"].Width = 200;

                grdvListaContactos.Columns["IdRegistro"].Visible = false;
                grdvListaContactos.Columns["IdContacto"].Visible = false;

                lblTotalContactos.Text = "Total Contactos: " + dtListContactos.Rows.Count.ToString();
            }
        }

        public void ListarContratos()
        {
            dtgListaContratos.DataSource = null;
            DataTable dtListaContratos = new DataTable();
            dtListaContratos = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_ContactosListar(_idpersona, CaracterTipo, 2);
            if (dtListaContratos.Rows.Count > 0)
            {
                dtgListaContratos.DataSource = dtListaContratos;
                
                RepositoryItemHyperLinkEdit RutaEnlace = new RepositoryItemHyperLinkEdit();
                dtgListaContratosView.Columns["RutaEnlace"].ColumnEdit = RutaEnlace;

                /*
                RepositoryItemComboBox TipoContrato = new RepositoryItemComboBox();
                TipoContrato.Items.Add("PRODUCTO");
                TipoContrato.Items.Add("SERVICIO");
                dtgListaContratosView.Columns["TipoContrato"].ColumnEdit = TipoContrato;
                */

                dtgListaContratosView.Columns["N°"].Width = 50;
                dtgListaContratosView.Columns["TipoContrato"].Width = 180;
                dtgListaContratosView.Columns["Titulo"].Width = 180;
                dtgListaContratosView.Columns["Descripcion"].Width = 180;
                dtgListaContratosView.Columns["Consideracion"].Width = 180;
                dtgListaContratosView.Columns["Beneficios"].Width = 180;
                dtgListaContratosView.Columns["DiasAlerta"].Width = 70;
                dtgListaContratosView.Columns["RutaEnlace"].Width = 300;
                dtgListaContratosView.Columns["FechaInicio"].Width = 100;
                dtgListaContratosView.Columns["FechaFin"].Width = 100;

                dtgListaContratosView.Columns["IdContrato"].Visible = false;
                dtgListaContratosView.Columns["IdContacto"].Visible = false;
                dtgListaContratosView.Columns["Tipo"].Visible = false;

                lblTotalContratos.Text = "Total Contratos: " + dtListaContratos.Rows.Count.ToString();
            }
        } 


        // AGREGAR
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DataTable dtAgregar = new DataTable();
            string respta;
            dtAgregar = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_Contactos_AgregarModificarQuitar(1, CaracterTipo, 0, _idpersona, _idcontacto, 0, "", "", "", "", "", "", "", Utilitario.Instancia.SesionUsuario.usuario);
            respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
            string NroRspta = respta.Substring(0, 1);
            if (NroRspta == "0")
            {
                listarContactos();
            }
            else             
            {
                MessageBox.Show("Error al agregar nuevo contacto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // GERARDO - 07/11
        private void btnAgregarC_Click(object sender, EventArgs e)
        {
            frmInsertarContrato frmInsertarContrato = new frmInsertarContrato();
            frmInsertarContrato.RecibirDatos(_idpersona, _idcontacto, _ruc, _razonsocial, _tipo, _compania, _telefono, _celular, this);
            frmInsertarContrato.opcion = 1;
            frmInsertarContrato.CargarComboTipo();
            frmInsertarContrato.cbValido.Visible = false;
            frmInsertarContrato.dtpFechaInicio.Value = DateTime.Now;
            frmInsertarContrato.dtpFechaFin.Value = DateTime.Now;
            frmInsertarContrato.btnAdendas.Visible = false;
            frmInsertarContrato.label13.Visible = false;
            frmInsertarContrato.label14.Visible = false;

            DataTable dtAreas = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_ListarAreas(1);
            frmInsertarContrato.dtgListaAreas.Rows.Clear();
            if (dtAreas.Rows.Count > 0)
            {
                frmInsertarContrato.dtgListaAreas.Rows.Clear();
                for (int i = 0; i < dtAreas.Rows.Count; i++)
                {
                    frmInsertarContrato.dtgListaAreas.Rows.Add(false, dtAreas.Rows[i]["Area"], dtAreas.Rows[i]["Nombre"]);
                }
            }
            else
            {
                frmInsertarContrato.dtgListaAreas.DataSource = null;
            }

            frmInsertarContrato.ShowDialog();
        }
        // GERARDO - 07/11

        // QUITAR CONTACTOS
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el contrato de forma permanente?", "REMOVER CONTACTO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string mensaje = "";
                int[] filas = grdvListaContactos.GetSelectedRows();
                string respta = "";
                if (filas.Length != 0)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        int idregistro = Convert.ToInt32(grdvListaContactos.GetRowCellValue(filas[i], "IdRegistro").ToString());
                        int idcontactodetalle = Convert.ToInt32(grdvListaContactos.GetRowCellValue(filas[i], "N°").ToString());

                        DataTable dtAgregar = new DataTable();
                        dtAgregar = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_Contactos_AgregarModificarQuitar(3, CaracterTipo, idregistro, _idpersona, _idcontacto
                                                                                                                                , idcontactodetalle, "", "", "", "", "", "", "", "");
                        respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            mensaje = "S";
                        }
                        else
                        {
                            MessageBox.Show("Error al remover contacto(s)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }

                    if (mensaje == "S")
                    {                        
                        MessageBox.Show(respta, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        listarContactos();
                    }
                }
                else
                {
                    MessageBox.Show("No ha seleccionado ningún registro para Remover", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnQuitarC_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea eliminar el contrato de forma permanente?", "ELIMINAR CONTRATO", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string mensaje = "";
                int[] filas = dtgListaContratosView.GetSelectedRows();
                string respta = "";
                if (filas.Length != 0)
                {
                    for (int i = 0; i < filas.Length; i++)
                    {
                        int idContrato = Convert.ToInt32(dtgListaContratosView.GetRowCellValue(filas[i], "IdContrato").ToString());
                        int idcontactodetalle = Convert.ToInt32(dtgListaContratosView.GetRowCellValue(filas[i], "N°").ToString());

                        DataTable dtAgregar = new DataTable();
                        dtAgregar = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_AgregaModificaContrato(3, CaracterTipo, idContrato, _idpersona, _idcontacto, idcontactodetalle,
                                                                                                                                  0, "", "", "", "", DateTime.Now, DateTime.Now, 0, 0, "", "", 0, "");
                        respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                        string NroRspta = respta.Substring(0, 1);
                        if (NroRspta == "0")
                        {
                            mensaje = "S";
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el contrato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    }

                    if (mensaje == "S")
                    {
                        MessageBox.Show(respta, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarContratos();
                    }
                }
                else
                {
                    MessageBox.Show("No ha seleccionado ningún contrato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            btnGuardar.Visible = true;
            btnCancelar.Visible = true;
            btnGuardar.Location = new Point(981, 7);
            btnCancelar.Location = new Point(1089, 7);
            btnModificar.Visible = false;
            btnAgregar.Enabled = false;
            btnQuitar.Enabled = false;
        }

        // GERARDO - 07/11
        private void btnModificarC_Click(object sender, EventArgs e)
        {
            int idContrato = Convert.ToInt32(dtgListaContratosView.GetRowCellValue(dtgListaContratosView.FocusedRowHandle, "IdContrato"));
            if (idContrato != 0)
            {
                frmInsertarContrato frmInsertarContrato = new frmInsertarContrato();
                frmInsertarContrato.RecibirDatos(_idpersona, _idcontacto, _ruc, _razonsocial, _tipo, _compania, _telefono, _celular, this);
                frmInsertarContrato.RecibirContrato(idContrato);
                frmInsertarContrato.opcion = 2;
                frmInsertarContrato.IdContrato = idContrato;
                frmInsertarContrato.CargarComboTipo();
                frmInsertarContrato.ListarContrato(idContrato);
                frmInsertarContrato.cbValido.Visible = true;
                frmInsertarContrato.ContarAdendas(idContrato);

                DataTable dtAreas = clsRecursosHumanosBL.Instancia.ReportesApp_RRHH_Vacaciones_ListarAreas(1);
                frmInsertarContrato.dtgListaAreas.Rows.Clear();
                if (dtAreas.Rows.Count > 0)
                {
                    frmInsertarContrato.dtgListaAreas.Rows.Clear();
                    for (int i = 0; i < dtAreas.Rows.Count; i++)
                    {
                        frmInsertarContrato.dtgListaAreas.Rows.Add(false, dtAreas.Rows[i]["Area"], dtAreas.Rows[i]["Nombre"]);
                    }
                }
                else
                {
                    frmInsertarContrato.dtgListaAreas.DataSource = null;
                }

                frmInsertarContrato.ShowDialog();
                frmInsertarContrato.ListarContrato(idContrato);
            }
            else
            {
                MessageBox.Show("El contrato seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            /*
            RepositoryItemTextEdit Normal = new RepositoryItemTextEdit();
            dtgListaContratosView.Columns["RutaEnlace"].ColumnEdit = Normal;
            */
        }
        // GERARDO - 07/11

        // MODIFICAR CONTACTOS
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            
            int[] filas = grdvListaContactos.GetSelectedRows();
            string respta = "";
            if (filas.Length != 0)
            {
                for (int i = 0; i < filas.Length; i++)
                {
                    int idregistro = Convert.ToInt32(grdvListaContactos.GetRowCellValue(filas[i], "IdRegistro").ToString());
                    int idcontactodetalle = Convert.ToInt32(grdvListaContactos.GetRowCellValue(filas[i], "N°").ToString());
                    string nombre = grdvListaContactos.GetRowCellValue(filas[i], "NombreContacto").ToString();
                    string cargo = grdvListaContactos.GetRowCellValue(filas[i], "Cargo").ToString();
                    string telefono = grdvListaContactos.GetRowCellValue(filas[i], "Telefono").ToString();
                    string celular = grdvListaContactos.GetRowCellValue(filas[i], "Celular").ToString();                     
                    string direccion = grdvListaContactos.GetRowCellValue(filas[i], "Direccion").ToString();
                    string lugar = grdvListaContactos.GetRowCellValue(filas[i], "Lugar").ToString();
                    string correo = grdvListaContactos.GetRowCellValue(filas[i], "Correo").ToString();

                    DataTable dtAgregar = new DataTable();
                    dtAgregar = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_Contactos_AgregarModificarQuitar(2, CaracterTipo, idregistro, _idpersona, _idcontacto, idcontactodetalle,
                                                                                          nombre.ToUpper(), cargo.ToUpper(), telefono, celular, correo.ToUpper(), direccion.ToUpper(),
                                                                                          lugar.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                    respta = Convert.ToString(dtAgregar.Rows[0]["exito"]);
                    string NroRspta = respta.Substring(0, 1);
                    if (NroRspta == "0")
                    {
                        mensaje = "S";
                    }
                    else
                    {
                        MessageBox.Show("Error al modificar contacto(s)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                }

                if (mensaje == "S")
                {
                    listarContactos();
                    MessageBox.Show(respta, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (AccesoAgregar == true)
                    {
                        btnAgregar.Enabled = true;
                    }
                    else
                    {
                        btnAgregar.Enabled = false;
                    }

                    if (AccesoModifica == true)
                    {
                        btnModificar.Enabled = true;
                    }
                    else
                    {
                        btnModificar.Enabled = false;
                    }

                    if (Accesoquitar == true)
                    {
                        btnQuitar.Enabled = true;
                    }
                    else
                    {
                        btnQuitar.Enabled = false;
                    }

                    btnModificar.Visible = true;
                    btnGuardar.Visible = false;
                    btnCancelar.Visible = false;
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningún registro para modificar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // GERARDO - 07/11
        private void dtgListaContratos_DoubleClick(object sender, EventArgs e)
        {
            int idContrato = Convert.ToInt32(dtgListaContratosView.GetRowCellValue(dtgListaContratosView.FocusedRowHandle, "IdContrato"));
            if (idContrato != 0)
            {
                frmInsertarContrato frmInsertarContrato = new frmInsertarContrato();
                frmInsertarContrato.RecibirDatos(_idpersona, _idcontacto, _ruc, _razonsocial, _tipo, _compania, _telefono, _celular, this);
                frmInsertarContrato.RecibirContrato(idContrato);
                frmInsertarContrato.CargarComboTipo();
                frmInsertarContrato.ListarContrato(idContrato);
                frmInsertarContrato.ListarAreasInvolucradas(idContrato);
                frmInsertarContrato.cbValido.Visible = true;
                frmInsertarContrato.ContarAdendas(idContrato);

                frmInsertarContrato.txtTitulo.ReadOnly = true;
                frmInsertarContrato.cbValido.Enabled = false;
                frmInsertarContrato.txtDescripcion.ReadOnly = true;
                frmInsertarContrato.txtConsideracion.ReadOnly = true;
                frmInsertarContrato.txtBeneficios.ReadOnly = true;
                frmInsertarContrato.cbxTipoContrato.Enabled = false;
                frmInsertarContrato.dtpFechaInicio.Enabled = false;
                frmInsertarContrato.dtpFechaFin.Enabled = false;
                frmInsertarContrato.txtDiasAlerta.ReadOnly = true;
                frmInsertarContrato.cbTodos.Enabled = false;
                frmInsertarContrato.txtDirectorio.ReadOnly = true;
                frmInsertarContrato.btnBuscar.Enabled = false;
                frmInsertarContrato.btnCancelar.Enabled = false;
                frmInsertarContrato.btnRegistrar.Enabled = false;
                
                frmInsertarContrato.ShowDialog();
            }
            else
            {
                MessageBox.Show("El contrato seleccionado no existe.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // GERARDO - 07/11

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnModificar.Visible = true;
            btnGuardar.Visible = false;
            btnCancelar.Visible = false;

            if (AccesoAgregar == true)
            {
                btnAgregar.Enabled = true;
            }
            else
            {
                btnAgregar.Enabled = false;
            }

            if (AccesoModifica == true)
            {
                btnModificar.Enabled = true;
            }
            else
            {
                btnModificar.Enabled = false;
            }

            if (Accesoquitar == true)
            {
                btnQuitar.Enabled = true;
            }
            else
            {
                btnQuitar.Enabled = false;
            }
        }

        private void btnGuardarObserv_Click(object sender, EventArgs e)
        {
            string respuesta;
            DataTable dt = new DataTable();
            dt = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_Contactos_AgregarCabcera(2, CaracterTipo, _idcontacto, _idpersona, 1, txtObservacion.Text.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
            respuesta = Convert.ToString(dt.Rows[0]["exito"]);
            string NrRpta = respuesta.Substring(0,1);
            if(NrRpta == "0")
            {
                MessageBox.Show(respuesta, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (gridControl1.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Contactos del " + txtTipo.Text + txtRazonSocial.Text + " - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                gridControl1.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void btnExcelC_Click(object sender, EventArgs e)
        {
            if (dtgListaContratos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Lista de Contratos del " + txtTipo.Text + txtRazonSocial.Text + " - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaContratos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }
    }
}
