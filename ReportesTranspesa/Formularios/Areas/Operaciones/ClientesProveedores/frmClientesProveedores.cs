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
using DevExpress.XtraGrid.Columns;
using Comun;
namespace ReportesTranspesa.Formularios.Areas.Operaciones.ClientesProveedores
{
    public partial class frmClientesProveedores : Form
    {
        string UsuarioModulo, AccesoAgregar;
        public char tipo='C';
        int idpersona, IdContacto, Accion = 1;
        string ruc;
        string razonsocial;
        string tipoClienteProveedor;
        string Compania;
        string Observacion;
        int IDPERSONAS;
        string telefono, celular;   // GERARDO - 07/11
        DataTable dtPermisos, dtEspeciales;
        int e1 = 0, e2 = 0, e3 = 0, e4 = 0;

        public frmClientesProveedores()
        {
            InitializeComponent();
            cbxCompania.SelectedIndexChanged -= cbxCompania_SelectedIndexChanged;
        }

        private void cbxCompania_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarComboCompania();
        }

        private void frmClientesProveedores_Load(object sender, EventArgs e)
        {
            dtPermisos = Utilitario.Instancia.ObtenerPermisosPorFormulario("ClientesProveedores");
            
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.Panel2Collapsed = false;

            btnCerrar.Visible = false;

            ConsultaUsuarioSpringxWindows();
            if (UsuarioModulo == null)
            {
                UsuarioModulo = Utilitario.Instancia.SesionUsuario.usuario;
            }

            VerificarPermisoeModificar();
            if (AccesoAgregar == "1")
            {
                btnAgregar.Enabled = true;               
            }
            else
            {
                btnAgregar.Enabled = false;
            }

            CargarComboCompania();
            groupBox1.Text = "Buscar para Agregar a la Lista";
            Clientes.BackColor = Color.LawnGreen;
            proveedores.BackColor = Color.Transparent;

            if (dtPermisos != null)
            {
                if (dtPermisos.Rows[0]["PermisosEspeciales"].ToString() != "")
                {
                    dtEspeciales = Utilitario.Instancia.ConvertirXMLaDatatable(dtPermisos.Rows[0]["PermisosEspeciales"].ToString());

                    if (dtEspeciales.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                        {
                            if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "TRANSPESA")
                            {
                                cbxCompania.Text = "TRANSPESA";
                                cbxCompania.Enabled = false;
                                Accion = 1;
                                i = 999; e1 = 1;
                            }
                        }

                        for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                        {
                            if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "ALTRA")
                            {
                                cbxCompania.Text = "ALTRA";
                                cbxCompania.Enabled = false;
                                Accion = 2;
                                i = 999; e2 = 1;
                            }
                        }

                        for (int i = 0; i < dtEspeciales.Rows.Count; i++)
                        {
                            if (dtEspeciales.Rows[i]["NombrePermiso"].ToString() == "AMT")
                            {
                                cbxCompania.Text = "AMT";
                                cbxCompania.Enabled = false;
                                Accion = 3;
                                i = 999; e3 = 1;
                            }
                        }
                    }
                }
            }

            listarDatoContactos();
        }


        public void CargarComboCompania()
        {
            DataTable dtCompania = clsOperacionesBL.Instancia.ReportesApp_Operaciones_ClientesProveedores_ListarTipoContratos(4);
            cbxCompania.DataSource = dtCompania;
            cbxCompania.DisplayMember = "Nombre";
            cbxCompania.ValueMember = "IdCompania";
        }

        void ConsultaUsuarioSpringxWindows()
        {
            DataTable dtUserWind = new DataTable();
            dtUserWind = clsUsuarioBL.Instancia.GetListaUsuariosModulo(Utilitario.Instancia.SesionUsuario.usuario);
            for (int i = 0; i < dtUserWind.Rows.Count; i++)
            {
                UsuarioModulo = dtUserWind.Rows[i]["Usuario"].ToString();
                //  MessageBox.Show("tu Usuario spring es "+UsuarioModulo);
            }
        }

        void VerificarPermisoeModificar()
        {
            DataTable dtRespuesta = new DataTable();
            dtRespuesta = clsOperacionesBL.Instancia.GetViajes_ClienteProveedor_VerificaPermisoModificar(UsuarioModulo);
            if (dtRespuesta.Rows.Count > 0)
            {
                for (int i = 0; i < dtRespuesta.Rows.Count; i++)
                {
                    AccesoAgregar = dtRespuesta.Rows[i]["AGREGAR"].ToString();
                }
            }
            else
            {               
                btnAgregar.Enabled = false;                
            }
        }
        private void listarDato()
        {
            gridControl1.DataSource = null;

            DataTable dtClienteProveedor = new DataTable();

            dtClienteProveedor = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_Listar(tipo, IDPERSONAS);

            if (dtClienteProveedor.Rows.Count > 0) 
            {
                gridControl1.DataSource = dtClienteProveedor;
                gridView1.Columns["DocumentoFiscal"].Width = 110;
                gridView1.Columns["IdPersona"].Visible = false;
                gridView1.Columns["TipoDocumento"].Width = 110;
                gridView1.Columns["Razon Social"].Width = 300;
                gridView1.Columns["Telefono"].Width = 100;
                gridView1.Columns["Celular"].Width = 100;
                gridView1.Columns["Lugar"].Width = 300;
                gridView1.Columns["DocumentoIdentidad"].Width = 130;
                gridView1.Columns["Estado"].Width = 70;
                gridView1.Columns["Celular"].Width = 100;
                gridView1.Columns["Direccion"].Width = 500;
                int Contador = Convert.ToInt32(dtClienteProveedor.Rows.Count);
                lblContador.Text ="TOTAL: " + Contador.ToString();
            }
        }

        private void listarDatoTodos()
        {
            splitContainer1.Panel1Collapsed = false;
           // splitContainer1.Panel2Collapsed = true;
            gridControl1.DataSource = null;

            DataTable dtClienteProveedor = new DataTable();

            dtClienteProveedor = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_Listar(tipo, IDPERSONAS);

            if (dtClienteProveedor.Rows.Count > 0)
            {
                gridControl2.DataSource = dtClienteProveedor;
                gridView2.Columns["DocumentoFiscal"].Width = 110;
                gridView2.Columns["IdPersona"].Visible = false;
                gridView2.Columns["TipoDocumento"].Width = 110;
                gridView2.Columns["Razon Social"].Width = 300;
                gridView2.Columns["Telefono"].Width = 100;
                gridView2.Columns["Celular"].Width = 100;
                gridView2.Columns["Lugar"].Width = 300;
                gridView2.Columns["DocumentoIdentidad"].Width = 130;
                gridView2.Columns["Estado"].Width = 70;
                gridView2.Columns["Celular"].Width = 100;
                gridView2.Columns["Direccion"].Width = 500;
                int Contador = Convert.ToInt32(dtClienteProveedor.Rows.Count);
                lblContador.Text = "TOTAL: " + Contador.ToString();
            }
        }

        private void listarDatoContactos()
        {
            gridControl1.DataSource = null;

            DataTable dtClienteProveedor = new DataTable();

            dtClienteProveedor = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_ListarContactos(tipo, IDPERSONAS, Accion);

            if (dtClienteProveedor.Rows.Count > 0)
            {
                gridControl1.DataSource = dtClienteProveedor;
                gridView1.Columns["DocumentoFiscal"].Width = 110;
                gridView1.Columns["IdPersona"].Visible = false;
                gridView1.Columns["IdContacto"].Visible = false;
                gridView1.Columns["TipoDocumento"].Width = 110;
                gridView1.Columns["Razon Social"].Width = 300;
                gridView1.Columns["Telefono"].Width = 100;
                gridView1.Columns["Celular"].Width = 100;
                gridView1.Columns["Lugar"].Width = 300;
                gridView1.Columns["DocumentoIdentidad"].Width = 130;
                gridView1.Columns["Estado"].Width = 70;
                gridView1.Columns["Celular"].Width = 100;
                gridView1.Columns["Direccion"].Width = 500;               

                int Contador = Convert.ToInt32(dtClienteProveedor.Rows.Count);                
                lblContador.Text = "TOTAL: " + Contador.ToString();
            }
        }
        
        private void Clientes_Click(object sender, EventArgs e)
        {
            Clientes.BackColor = Color.LawnGreen;
            proveedores.BackColor = Color.Transparent;
            tipo = 'C';
            IDPERSONAS = 0;
            textBox1.Text = "";
            //listarDato();
            listarDatoContactos();
            btnAgregar.Text = "Agregar Cliente";
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.Panel2Collapsed = false;
        }

        private void proveedores_Click(object sender, EventArgs e)
        {
            Clientes.BackColor = Color.Transparent;
            proveedores.BackColor = Color.LawnGreen;
            tipo = 'P';
            textBox1.Text = "";
            IDPERSONAS = 0;
            //listarDato();
            listarDatoContactos();
            btnAgregar.Text = "Agregar Proveedor";
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.Panel2Collapsed = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            textBox1.BackColor = Color.PaleGreen;
            
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {                
                clsVisuales.Instancia.LlenarLw(lvClienteProveedor, clsConsultaBL.Instancia.GetDataPersonaOperaciones(textBox1.Text), true, false, false);

                lvClienteProveedor.Columns[0].Width = 0;
                lvClienteProveedor.Columns[1].Width = 206;
                lvClienteProveedor.Columns[2].Width = 110;

                lvClienteProveedor.Size = new System.Drawing.Size(400, 200);

                lvClienteProveedor.BringToFront();
                lvClienteProveedor.Visible = true;
                lvClienteProveedor.Focus();                      
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvClienteProveedor.Visible = false;
                textBox1.Focus();
            }
        }

        private void lvClienteProveedor_Enter(object sender, EventArgs e)
        {
            if (!lvClienteProveedor.Items.Count.Equals(0))
            {
                lvClienteProveedor.Items[0].Selected = true;
            }
        }

        private void lvClienteProveedor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lvClienteProveedor.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;

                ItemActual = lvClienteProveedor.SelectedItems[0];

                IDPERSONAS = Int32.Parse(ItemActual.Text);
                textBox1.Text = ItemActual.SubItems[1].Text;
                lvClienteProveedor.Visible = false;
                textBox1.Focus();
                splitContainer1.Panel1Collapsed = true;
                splitContainer1.Panel2Collapsed = false;
                listarDato();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lvClienteProveedor.Visible = false;
                textBox1.Focus();
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try 
	        {
                int[] filass = gridView1.GetSelectedRows();
                string datoseleccionado = gridView1.GetFocusedValue().ToString();

                for (int i = 0; i < filass.Length; i++)
                {
                    idpersona = Convert.ToInt32(gridView1.GetRowCellValue(filass[i], "IdPersona").ToString());
                    IdContacto = Convert.ToInt32(gridView1.GetRowCellValue(filass[i], "IdContacto").ToString());
                    ruc = gridView1.GetRowCellValue(filass[i], "DocumentoFiscal").ToString();
                    razonsocial = gridView1.GetRowCellValue(filass[i], "Razon Social").ToString();
                    tipoClienteProveedor = gridView1.GetRowCellValue(filass[i], "Tipo").ToString();
                    Compania = gridView1.GetRowCellValue(filass[i], "Compañía").ToString();
                    Observacion = gridView1.GetRowCellValue(filass[i], "OBSERVACION").ToString();
                    // GERARDO - 07/11
                    telefono = gridView1.GetRowCellValue(filass[i], "Telefono").ToString();
                    celular = gridView1.GetRowCellValue(filass[i], "Celular").ToString();
                   

                    frmContactoClienteProveedor frmContactos = new frmContactoClienteProveedor();
                    frmContactos.datos(idpersona, IdContacto, ruc, razonsocial, tipoClienteProveedor, Compania, Observacion, telefono, celular);
                    // GERARDO - 07/11

                    if (dtPermisos != null)
                    {
                        if (dtPermisos.Rows.Count > 0)
                        {
                            frmContactos.btnGuardarObserv.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]);

                            frmContactos.btnAgregar.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]);
                            frmContactos.btnGuardar.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]);
                            frmContactos.btnModificar.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]);
                            frmContactos.btnQuitar.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]);

                            frmContactos.btnAgregarC.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]);
                            frmContactos.btnModificarC.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]);
                            frmContactos.btnQuitarC.Enabled = Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]);

                            frmContactos.AccesoAgregar = Convert.ToBoolean(dtPermisos.Rows[0]["Nuevo"]);
                            frmContactos.AccesoModifica = Convert.ToBoolean(dtPermisos.Rows[0]["Editar"]);
                            frmContactos.Accesoquitar = Convert.ToBoolean(dtPermisos.Rows[0]["Anular"]);
                        }
                    }

                    frmContactos.ShowDialog();
                    listarDatoContactos();
                }
	        }
	        catch (Exception)
	        {		
		       
	        }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
           // tipo = 'B';
           // splitContainer1.Panel1Collapsed = true;
            listarDatoTodos();
            listarDatoContactos();
            btnCerrar.Visible = true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.Equals(""))
                {
                    int[] filass = gridView2.GetSelectedRows();
                    string datoseleccionado = gridView2.GetFocusedValue().ToString();
                    if (filass.Length > 0)
                    {
                        for (int i = 0; i < filass.Length; i++)
                        {
                            idpersona = Convert.ToInt32(gridView2.GetRowCellValue(filass[i], "IdPersona").ToString());
                            tipoClienteProveedor = gridView2.GetRowCellValue(filass[i], "Tipo").ToString();

                            if (tipoClienteProveedor.Equals("CLIENTE")) { tipo = 'C'; }
                            else { tipo = 'P'; }
                            string respuesta = "";
                            

                            DataTable dtContactoCabecera = new DataTable();
                            dtContactoCabecera = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_Contactos_AgregarCabcera(1, tipo, 0, idpersona,Convert.ToInt32(cbxCompania.SelectedValue), "", Utilitario.Instancia.SesionUsuario.usuario);
                            respuesta = Convert.ToString(dtContactoCabecera.Rows[0]["exito"]);
                            string NrRpta = respuesta.Substring(0, 1);
                            if (NrRpta == "0")
                            {
                                textBox1.Text = "";
                                MessageBox.Show(respuesta, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                splitContainer1.Panel1Collapsed = true;
                                splitContainer1.Panel2Collapsed = false;
                                listarDatoContactos();
                                btnCerrar.Visible = false;
                                return;
                            }
                            else
                            {
                                MessageBox.Show(respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                splitContainer1.Panel1Collapsed = true;
                                splitContainer1.Panel2Collapsed = false;
                                listarDatoContactos();
                                btnCerrar.Visible = true;
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No hay datos ingresados...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Focus();
                        textBox1.BackColor = Color.LightCoral;
                        return;
                    }
                }

                if (IDPERSONAS == 0)
                {
                    MessageBox.Show("Datos incorrectos...!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Focus();
                    textBox1.BackColor = Color.LightCoral;
                    return;
                }

                string respuestas = "";
                DataTable dtContactoCabecers = new DataTable();
                dtContactoCabecers = clsOperacionesBL.Instancia.GetLista_Operaciones_Clientes_Contactos_AgregarCabcera(1, tipo, 0, IDPERSONAS, Accion, "", Utilitario.Instancia.SesionUsuario.usuario);
                respuestas = Convert.ToString(dtContactoCabecers.Rows[0]["exito"]);
                string NrRptas = respuestas.Substring(0, 1);
                if (NrRptas == "0")
                {
                    textBox1.Text = "";
                    textBox1.BackColor = Color.White;
                    MessageBox.Show(respuestas, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listarDatoContactos();
                }
                else
                {
                    MessageBox.Show(respuestas, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

            }
        }

        private void lvClienteProveedor_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem ItemActual;

            ItemActual = lvClienteProveedor.SelectedItems[0];

            IDPERSONAS = Int32.Parse(ItemActual.Text);
            textBox1.Text = ItemActual.SubItems[1].Text;
            lvClienteProveedor.Visible = false;
            textBox1.Focus();

            splitContainer1.Panel1Collapsed = true;
            splitContainer1.Panel2Collapsed = false;
            listarDato();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = true;
            splitContainer1.Panel2Collapsed = false;
            listarDatoContactos();
            btnCerrar.Visible = false;
        }

        private void cbxCompania_DropDownClosed(object sender, EventArgs e)
        {
            Accion = Convert.ToInt32(cbxCompania.SelectedValue);
            listarDatoContactos();
        }
    }
}
