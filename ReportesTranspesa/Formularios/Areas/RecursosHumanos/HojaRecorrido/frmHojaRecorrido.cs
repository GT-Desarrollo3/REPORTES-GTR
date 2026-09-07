using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Negocio;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReportesTranspesa.Sistema;
using System.Globalization;
using System.Drawing.Printing;
using System.IO;
using System.Drawing.Imaging;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmHojaRecorrido : Form
    {
        public int TipoRegistro = 0, IDPERSONFIRMAR= 0,Actualizar = 0;
        private PrintDocument DocumentoParaImprimir = new PrintDocument();
        private PrintDialog Impresora = new PrintDialog();
        private PrintPreviewDialog VistaPrevia = new PrintPreviewDialog();
        private Bitmap bmp;

        public int posicion = 0,AccesoFirmar=0,AccesoImprimir=0;
        string input = "";
        int anio = 0, idproceso, idgrupo, idpersona, idpersona1, idpersona2, idpersona3, idpersona4, idpersona5, idpersona6, idpersona7, idpersona8, idpersona9, idpersona10,
            idpersona11, idpersona12, _idpersona, _idpersona1, _idpersona2, _idpersona3, _idpersona4, _idpersona5, _idpersona6, _idpersona7, _idpersona8, _idpersona9, _idpersona10,
            _idpersona11, _idpersona12;
        string compania = "10000000", codigohoja ="00",fecha="10-10-2022", Area1, Area2, Area3, Area4, Area5, Area6, Area7, Area8, Area9, Area10, Area11, Area12, Observacion1, Observacion2, Observacion3, Observacion4, Observacion5,
            Observacion6, Observacion7, Observacion8, Observacion9, Observacion10, Observacion11, Observacion12, _NommbrePersona1, _NommbrePersona2, _NommbrePersona3,
            _NommbrePersona4, _NommbrePersona5, _NommbrePersona6, _NommbrePersona7, _NommbrePersona8, _NommbrePersona9, _NommbrePersona10,_NommbrePersona11,
            _NommbrePersona12, personaNombre;
        bool firma1 = false, firma2 = false, firma3 = false, firma4 = false, firma5 = false, firma6 = false, firma7 = false, firma8 = false, firma9 = false, firma10 = false,
            firma11 = false, firma12 = false;
        DataTable dtFirmar;

        public frmHojaRecorrido()
        {
            InitializeComponent();

            DocumentoParaImprimir.PrintPage += new PrintPageEventHandler(DocumentoParaImprimir_PrintPage);
        }


        public void DatosEDITAR(string CompaniaEditar, string CodigoHojaEditar, string IdProcesoEditar, string ProcesoEditar, string IdGrupoEditar,string GrupoEdiatr,
                                string AnioEditar, string PersonaNombre,string IDPERSONAVER)
        {
            compania = CompaniaEditar;
            codigohoja = CodigoHojaEditar;
            cboProcesos.Text = ProcesoEditar;
            cboGrupo.Text = GrupoEdiatr;
            idproceso = Convert.ToInt32(IdProcesoEditar);
            idgrupo = Convert.ToInt32(IdGrupoEditar);
            anio = Convert.ToInt32(AnioEditar);
            personaNombre = PersonaNombre;
            idpersona = Convert.ToInt32(IDPERSONAVER);
        }

        void DocumentoParaImprimir_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
        }

        private void frmHojaRecorrido_Load(object sender, EventArgs e)
        {
            if (AccesoImprimir == 1)
            {
                cbxImprimir.Visible = true;
            }
            else
            {
                cbxImprimir.Visible = false;
            }

            //Llenado del combo TipoPreviajes
            DataTable dt = new DataTable();
            dt = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_ListarProcesos("10000000", Utilitario.Instancia.SesionUsuario.usuario);

            cboProcesos.DisplayMember = "Proceso";
            cboProcesos.ValueMember = "IdProceso";
            cboProcesos.DataSource = dt;

            if (TipoRegistro == 1)//el tipo 1 permite la creacion del registro
            {
               
                cboProcesos.SelectedIndex = idproceso - 1;
                ListarGrupos(Convert.ToInt32(cboProcesos.SelectedValue));
                cboGrupo.SelectedIndex = idgrupo - 1;               
            }

            if (TipoRegistro == 2)//el tipo 2 permite la edicion del registro
            {
                //cboProcesos.SelectedValue =
                //txtIdEmpleado.Visible = true;
                txtNumero.Visible = true;
                txtPersona.ReadOnly = true;

                //CONSULTAMOS EL EL IDDEL USUARIO PARA ACTIVAR LAS FIRMAS
                DataTable dtFirmarPersona = new DataTable();
                dtFirmarPersona = clsRecursosHumanosBL.Instancia.GetAreaPersonaFirmar(Utilitario.Instancia.SesionUsuario.usuario);
                if (dtFirmarPersona.Rows.Count > 0)
                {
                    for (int i = 0; i < dtFirmarPersona.Rows.Count; i++)
                    {
                        IDPERSONFIRMAR = Convert.ToInt32(dtFirmarPersona.Rows[i]["IDPERSONA"].ToString());
                    }
                }

                //CONSULTAMOS EL CARGO DEL EMPLEADO A EDITAR
                DataTable dtCargo = new DataTable();
                dtCargo = clsRecursosHumanosBL.Instancia.GetAreaPersona(idpersona);

                if (dtCargo.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCargo.Rows.Count; i++)
                    {
                        lblCargo.Text = dtCargo.Rows[i]["Cargo"].ToString();
                        //  idcargo = dtCargo.Rows[i]["CodigoPuesto"].ToString();
                        dtpFechaIngreso.Text = dtCargo.Rows[i]["FechaIngreso"].ToString();
                        lblAreaVer.Text = dtCargo.Rows[i]["Area"].ToString();
                    }
                }

                ListarHojaEditar();
                cboProcesos.SelectedIndex = idproceso - 1;
                ListarGrupos(Convert.ToInt32(cboProcesos.SelectedValue));
                cboGrupo.SelectedIndex = idgrupo - 1;
                txtPersona.Text = personaNombre;              

                txtIdEmpleado.Text = idpersona.ToString();
                txtNumero.Text = anio.ToString() +"-" +codigohoja;
                txtCodigo.Text =  idpersona.ToString();
                if (idproceso == 1)
                {
                    lblNomHoja.Text = "HOJA DE RECORRIDO DE " + cboProcesos.Text + " DE PERSONAL " + cboGrupo.Text;
                }
                else
                {
                    lblNomHoja.Text = "HOJA DE CONFORMIDAD - " + cboProcesos.Text + " - " + cboGrupo.Text;
                }

                //Definir tamaño de textbox
                txtObs1.Location = new Point(218, 28);
                txtObs1.Multiline = true;
                txtObs1.Size = new Size(170, 80);
                txtObs2.Location = new Point(218, 28);
                txtObs2.Multiline = true;
                txtObs2.Size = new Size(170, 80);
                txtObs3.Location = new Point(218, 28);
                txtObs3.Multiline = true;
                txtObs3.Size = new Size(170, 80);
                txtObs4.Location = new Point(218, 28);
                txtObs4.Multiline = true;
                txtObs4.Size = new Size(170, 80);
                txtObs5.Location = new Point(218, 28);
                txtObs5.Multiline = true;
                txtObs5.Size = new Size(170, 80);
                txtObs6.Location = new Point(218, 28);
                txtObs6.Multiline = true;
                txtObs6.Size = new Size(170, 80);
                txtObs7.Location = new Point(218, 28);
                txtObs7.Multiline = true;
                txtObs7.Size = new Size(170, 80);
                txtObs8.Location = new Point(218, 28);
                txtObs8.Multiline = true;
                txtObs8.Size = new Size(170, 80);
                txtObs9.Location = new Point(218, 28);
                txtObs9.Multiline = true;
                txtObs9.Size = new Size(170, 80);
                txtObs10.Location = new Point(218, 28);
                txtObs10.Multiline = true;
                txtObs10.Size = new Size(170, 80);
                txtObs11.Location = new Point(218, 28);
                txtObs11.Multiline = true;
                txtObs11.Size = new Size(170, 80);
                txtObs12.Location = new Point(218, 28);
                txtObs12.Multiline = true;
                txtObs12.Size = new Size(170, 80);            
            }

            if (idproceso == 1)
            {
                lblNomHoja.Text = "HOJA DE RECORRIDO DE " + cboProcesos.Text + " DE PERSONAL " + cboGrupo.Text;
            }
            else
            {
                lblNomHoja.Text = "HOJA DE CONFORMIDAD - " + cboProcesos.Text + " - " + cboGrupo.Text;
            }
        }
        
        private void cboProcesos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListarGrupos(Convert.ToInt32(cboProcesos.SelectedValue));
        }

        void ListarHojaEditar()
        {
            /*try
            {*/


                DataTable dtHojaEditar = new DataTable();
                dtHojaEditar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_ListarHojaEditar(compania, anio, codigohoja, idproceso, idgrupo, Utilitario.Instancia.SesionUsuario.usuario);
                if (dtHojaEditar.Rows.Count > 0)
                {

                    label1.Text = dtHojaEditar.Rows[0]["AREA1"].ToString();
                    label2.Text = dtHojaEditar.Rows[0]["AREA2"].ToString();
                    label3.Text = dtHojaEditar.Rows[0]["AREA3"].ToString();
                    label4.Text = dtHojaEditar.Rows[0]["AREA4"].ToString();
                    label5.Text = dtHojaEditar.Rows[0]["AREA5"].ToString();
                    label6.Text = dtHojaEditar.Rows[0]["AREA6"].ToString();
                    label7.Text = dtHojaEditar.Rows[0]["AREA7"].ToString();
                    label8.Text = dtHojaEditar.Rows[0]["AREA8"].ToString();
                    label9.Text = dtHojaEditar.Rows[0]["AREA9"].ToString();
                    label10.Text = dtHojaEditar.Rows[0]["AREA10"].ToString();
                    label11.Text = dtHojaEditar.Rows[0]["AREA11"].ToString();
                    label12.Text = dtHojaEditar.Rows[0]["AREA12"].ToString();

                    lnom1.Text = dtHojaEditar.Rows[0]["NOMBRE1"].ToString();
                    lnom2.Text = dtHojaEditar.Rows[0]["NOMBRE2"].ToString();
                    lnom3.Text = dtHojaEditar.Rows[0]["NOMBRE3"].ToString();
                    lnom4.Text = dtHojaEditar.Rows[0]["NOMBRE4"].ToString();
                    lnom5.Text = dtHojaEditar.Rows[0]["NOMBRE5"].ToString();
                    lnom6.Text = dtHojaEditar.Rows[0]["NOMBRE6"].ToString();
                    lnom7.Text = dtHojaEditar.Rows[0]["NOMBRE7"].ToString();
                    lnom8.Text = dtHojaEditar.Rows[0]["NOMBRE8"].ToString();
                    lnom9.Text = dtHojaEditar.Rows[0]["NOMBRE9"].ToString();
                    lnom10.Text = dtHojaEditar.Rows[0]["NOMBRE10"].ToString();
                    lnom11.Text = dtHojaEditar.Rows[0]["NOMBRE11"].ToString();
                    lnom12.Text = dtHojaEditar.Rows[0]["NOMBRE12"].ToString();


                    tscboAreas1.Text = dtHojaEditar.Rows[0]["AREA1"].ToString();
                    tscboAreas2.Text = dtHojaEditar.Rows[0]["AREA2"].ToString();
                    tscboAreas3.Text = dtHojaEditar.Rows[0]["AREA3"].ToString();
                    tscboAreas4.Text = dtHojaEditar.Rows[0]["AREA4"].ToString();
                    tscboAreas5.Text = dtHojaEditar.Rows[0]["AREA5"].ToString();
                    tscboAreas6.Text = dtHojaEditar.Rows[0]["AREA6"].ToString();
                    tscboAreas7.Text = dtHojaEditar.Rows[0]["AREA7"].ToString();
                    tscboAreas8.Text = dtHojaEditar.Rows[0]["AREA8"].ToString();
                    tscboAreas9.Text = dtHojaEditar.Rows[0]["AREA9"].ToString();
                    tscboAreas10.Text = dtHojaEditar.Rows[0]["AREA10"].ToString();
                    tscboAreas11.Text = dtHojaEditar.Rows[0]["AREA11"].ToString();
                    tscboAreas12.Text = dtHojaEditar.Rows[0]["AREA12"].ToString();

                    txtObs1.Text = dtHojaEditar.Rows[0]["OBS1"].ToString();
                    txtObs2.Text = dtHojaEditar.Rows[0]["OBS2"].ToString();
                    txtObs3.Text = dtHojaEditar.Rows[0]["OBS3"].ToString();
                    txtObs4.Text = dtHojaEditar.Rows[0]["OBS4"].ToString();
                    txtObs5.Text = dtHojaEditar.Rows[0]["OBS5"].ToString();
                    txtObs6.Text = dtHojaEditar.Rows[0]["OBS6"].ToString();
                    txtObs7.Text = dtHojaEditar.Rows[0]["OBS7"].ToString();
                    txtObs8.Text = dtHojaEditar.Rows[0]["OBS8"].ToString();
                    txtObs9.Text = dtHojaEditar.Rows[0]["OBS9"].ToString();
                    txtObs10.Text = dtHojaEditar.Rows[0]["OBS10"].ToString();
                    txtObs11.Text = dtHojaEditar.Rows[0]["OBS11"].ToString();
                    txtObs12.Text = dtHojaEditar.Rows[0]["OBS12"].ToString();
                    
                    _NommbrePersona1 = dtHojaEditar.Rows[0]["NOMBRE1"].ToString();
                    _NommbrePersona2 = dtHojaEditar.Rows[0]["NOMBRE2"].ToString();
                    _NommbrePersona3 = dtHojaEditar.Rows[0]["NOMBRE3"].ToString();
                    _NommbrePersona4 = dtHojaEditar.Rows[0]["NOMBRE4"].ToString();
                    _NommbrePersona5 = dtHojaEditar.Rows[0]["NOMBRE5"].ToString();
                    _NommbrePersona6 = dtHojaEditar.Rows[0]["NOMBRE6"].ToString();
                    _NommbrePersona7 = dtHojaEditar.Rows[0]["NOMBRE7"].ToString();
                    _NommbrePersona8 = dtHojaEditar.Rows[0]["NOMBRE8"].ToString();
                    _NommbrePersona9 = dtHojaEditar.Rows[0]["NOMBRE9"].ToString();
                    _NommbrePersona10 = dtHojaEditar.Rows[0]["NOMBRE10"].ToString();
                    _NommbrePersona11 = dtHojaEditar.Rows[0]["NOMBRE11"].ToString();
                    _NommbrePersona12 = dtHojaEditar.Rows[0]["NOMBRE12"].ToString();

                    _idpersona1 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA1"].ToString());
                    _idpersona2 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA2"].ToString());
                    _idpersona3 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA3"].ToString());
                    _idpersona4 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA4"].ToString());
                    _idpersona5 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA5"].ToString());
                    _idpersona6 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA6"].ToString());
                    _idpersona7 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA7"].ToString());
                    _idpersona8 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA8"].ToString());
                    _idpersona9 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA9"].ToString());
                    _idpersona10 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA10"].ToString());
                    _idpersona11 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA11"].ToString());
                    _idpersona12 = Convert.ToInt32(dtHojaEditar.Rows[0]["PERSONA12"].ToString());

                    if (_idpersona1 == IDPERSONFIRMAR)
                    {
                        chbFirma1.Enabled = true;
                      
                    }
                    if (_idpersona2 == IDPERSONFIRMAR)
                    {
                        cbhFirma2.Enabled = true;
                      
                    }
                    if (_idpersona3 == IDPERSONFIRMAR)
                    {
                        cbhFirma3.Enabled = true;
                        
                    }
                    if (_idpersona4 == IDPERSONFIRMAR)
                    {
                        cbhFirma4.Enabled = true;
                        
                    }
                    if (_idpersona5 == IDPERSONFIRMAR)
                    {
                        cbhFirma5.Enabled = true;
                       
                    }
                    if (_idpersona6 == IDPERSONFIRMAR)
                    {
                        cbhFirma6.Enabled = true;
                        
                    }
                    if (_idpersona7 == IDPERSONFIRMAR)
                    {
                        cbhFirma7.Enabled = true;                                             
                    }
                    if (_idpersona8 == IDPERSONFIRMAR)
                    {
                        cbhFirma8.Enabled = true;                        
                    }
                    if (_idpersona9 == IDPERSONFIRMAR)
                    {
                        cbhFirma9.Enabled = true;                       
                    }
                    if (_idpersona10 == IDPERSONFIRMAR)
                    {
                        cbhFirma10.Enabled = true;
                       
                    }
                    if (_idpersona11== IDPERSONFIRMAR)
                    {
                        cbhFirma11.Enabled = true;
                       
                    } 
                    if (_idpersona12 == IDPERSONFIRMAR)
                    {
                        cbhFirma12.Enabled = true;
                       
                    }

                    foreach (DataRow r in dtHojaEditar.Rows)
                    {
                        if (r["IMAGEN1"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN1"];
                            pictureBox1.Image = ImageHelper.ByteArrayToImage(datos);                           
                        }
                        
                        if (r["IMAGEN2"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN2"];
                            pictureBox2.Image = ImageHelper.ByteArrayToImage(datos);                           
                        }
                        if (r["IMAGEN3"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN3"];
                            pictureBox3.Image = ImageHelper.ByteArrayToImage(datos);                          
                        }
                        if (r["IMAGEN4"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN4"];
                            pictureBox4.Image = ImageHelper.ByteArrayToImage(datos);                           
                        }
                        if (r["IMAGEN5"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN5"];
                            pictureBox5.Image = ImageHelper.ByteArrayToImage(datos);                           
                        }
                        if (r["IMAGEN6"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN6"];
                            pictureBox6.Image = ImageHelper.ByteArrayToImage(datos);                           
                        }
                        if (r["IMAGEN7"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN7"];
                            pictureBox7.Image = ImageHelper.ByteArrayToImage(datos);                           
                        }
                        if (r["IMAGEN8"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN8"];
                            pictureBox8.Image = ImageHelper.ByteArrayToImage(datos);                           
                        }
                        if (r["IMAGEN9"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN9"];
                            pictureBox9.Image = ImageHelper.ByteArrayToImage(datos);                            
                        }
                        if (r["IMAGEN10"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN10"];
                            pictureBox10.Image = ImageHelper.ByteArrayToImage(datos);                            
                        }
                        if (r["IMAGEN11"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN11"];
                            pictureBox11.Image = ImageHelper.ByteArrayToImage(datos);                            
                        }
                        if (r["IMAGEN12"] != DBNull.Value)
                        {
                            byte[] datos = (byte[])r["IMAGEN12"];
                            pictureBox12.Image = ImageHelper.ByteArrayToImage(datos);
                            /*if (_idpersona12 == IDPERSONFIRMAR)
                            {
                                cbhFirma12.Enabled = true;
                            }
                            else
                            {
                                cbhFirma12.Enabled = false;
                            }*/
                        }  
                    }          
                
                }
                else
                {
                    label1.Text = "";
                    label2.Text = "";
                    label3.Text = "";
                    label4.Text = "";
                    label5.Text = "";
                    label6.Text = "";
                    label7.Text = "";
                    label8.Text = "";
                    label9.Text = "";
                    label10.Text = "";
                    label11.Text = "";
                    label12.Text = "";

                    lnom1.Text = "";
                    lnom2.Text = "";
                    lnom3.Text = "";
                    lnom4.Text = "";
                    lnom5.Text = "";
                    lnom6.Text = "";
                    lnom7.Text = "";
                    lnom8.Text = "";
                    lnom9.Text = "";
                    lnom10.Text = "";
                    lnom11.Text = "";
                    lnom12.Text = "";

                    tscboAreas1.Text = "";
                    tscboAreas2.Text = "";
                    tscboAreas3.Text = "";
                    tscboAreas4.Text = "";
                    tscboAreas5.Text = "";
                    tscboAreas6.Text = "";
                    tscboAreas7.Text = "";
                    tscboAreas8.Text = "";
                    tscboAreas9.Text = "";
                    tscboAreas10.Text = "";
                    tscboAreas11.Text = "";
                    tscboAreas12.Text = "";

                    txtObs1.Text = "";
                    txtObs2.Text = "";
                    txtObs3.Text = "";
                    txtObs4.Text = "";
                    txtObs5.Text = "";
                    txtObs6.Text = "";
                    txtObs7.Text = "";
                    txtObs8.Text = "";
                    txtObs9.Text = "";
                    txtObs10.Text = "";
                    txtObs11.Text = "";
                    txtObs12.Text = "";

                    _NommbrePersona1 = "";
                    _NommbrePersona2 = "";
                    _NommbrePersona3 = "";
                    _NommbrePersona4 = "";
                    _NommbrePersona5 = "";
                    _NommbrePersona6 = "";
                    _NommbrePersona7 = "";
                    _NommbrePersona8 = "";
                    _NommbrePersona9 = "";
                    _NommbrePersona10 = "";
                    _NommbrePersona11 = "";
                    _NommbrePersona12 = "";

                    _idpersona1 = 0;
                    _idpersona2 = 0;
                    _idpersona3 = 0;
                    _idpersona4 = 0;
                    _idpersona5 = 0;
                    _idpersona6 = 0;
                    _idpersona7 = 0;
                    _idpersona8 = 0;
                    _idpersona9 = 0;
                    _idpersona10 = 0;
                    _idpersona11 = 0;
                    _idpersona12 = 0;
                }
           /* }
            catch (Exception)
            {

                throw;
            }*/

        }

        public class ImageHelper
        {
            public static Image ByteArrayToImage(byte[] byteArrayIn)
            {
                MemoryStream ms = new MemoryStream(byteArrayIn);
                return Image.FromStream(ms);
            }

            public static byte[] ImageToByteArray(Image imageIn)
            {
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }

        }

        void ListarGrupos(int IdProceso)
        {
            //Llenado del combo TipoPreviajes
            DataTable dt = new DataTable();
            dt = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_ListarGrupos(IdProceso, Utilitario.Instancia.SesionUsuario.usuario);

            cboGrupo.DisplayMember = "Grupo";
            cboGrupo.ValueMember = "IdGrupo";
            cboGrupo.DataSource = dt;
        }

        void ListarFormato(int IdProceso, int IdGrupo)
        {
            //Llenado del combo TipoPreviajes
            DataTable dt = new DataTable();
            dt = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_ListarFormato(IdProceso, IdGrupo);

            if (dt.Rows.Count > 0)
            {
                
                label1.Text = dt.Rows[0]["AREA1"].ToString();
                label2.Text = dt.Rows[0]["AREA2"].ToString();
                label3.Text = dt.Rows[0]["AREA3"].ToString();
                label4.Text = dt.Rows[0]["AREA4"].ToString();
                label5.Text = dt.Rows[0]["AREA5"].ToString();
                label6.Text = dt.Rows[0]["AREA6"].ToString();
                label7.Text = dt.Rows[0]["AREA7"].ToString();
                label8.Text = dt.Rows[0]["AREA8"].ToString();
                label9.Text = dt.Rows[0]["AREA9"].ToString();
                label10.Text = dt.Rows[0]["AREA10"].ToString();
                label11.Text = dt.Rows[0]["AREA11"].ToString();
                label12.Text = dt.Rows[0]["AREA12"].ToString();

                lnom1.Text = dt.Rows[0]["NOMBRE1"].ToString();
                lnom2.Text = dt.Rows[0]["NOMBRE2"].ToString();
                lnom3.Text = dt.Rows[0]["NOMBRE3"].ToString();
                lnom4.Text = dt.Rows[0]["NOMBRE4"].ToString();
                lnom5.Text = dt.Rows[0]["NOMBRE5"].ToString();
                lnom6.Text = dt.Rows[0]["NOMBRE6"].ToString();
                lnom7.Text = dt.Rows[0]["NOMBRE7"].ToString();
                lnom8.Text = dt.Rows[0]["NOMBRE8"].ToString();
                lnom9.Text = dt.Rows[0]["NOMBRE9"].ToString();
                lnom10.Text = dt.Rows[0]["NOMBRE10"].ToString();
                lnom11.Text = dt.Rows[0]["NOMBRE11"].ToString();
                lnom12.Text = dt.Rows[0]["NOMBRE12"].ToString();


                tscboAreas1.Text = dt.Rows[0]["AREA1"].ToString();
                tscboAreas2.Text = dt.Rows[0]["AREA2"].ToString();
                tscboAreas3.Text = dt.Rows[0]["AREA3"].ToString();
                tscboAreas4.Text = dt.Rows[0]["AREA4"].ToString();
                tscboAreas5.Text = dt.Rows[0]["AREA5"].ToString();
                tscboAreas6.Text = dt.Rows[0]["AREA6"].ToString();
                tscboAreas7.Text = dt.Rows[0]["AREA7"].ToString();
                tscboAreas8.Text = dt.Rows[0]["AREA8"].ToString();
                tscboAreas9.Text = dt.Rows[0]["AREA9"].ToString();
                tscboAreas10.Text = dt.Rows[0]["AREA10"].ToString();
                tscboAreas11.Text = dt.Rows[0]["AREA11"].ToString();
                tscboAreas12.Text = dt.Rows[0]["AREA12"].ToString();

                txtObs1.Text = dt.Rows[0]["NOMBRE1"].ToString();
                txtObs2.Text = dt.Rows[0]["NOMBRE2"].ToString();
                txtObs3.Text = dt.Rows[0]["NOMBRE3"].ToString();
                txtObs4.Text = dt.Rows[0]["NOMBRE4"].ToString();
                txtObs5.Text = dt.Rows[0]["NOMBRE5"].ToString();
                txtObs6.Text = dt.Rows[0]["NOMBRE6"].ToString();
                txtObs7.Text = dt.Rows[0]["NOMBRE7"].ToString();
                txtObs8.Text = dt.Rows[0]["NOMBRE8"].ToString();
                txtObs9.Text = dt.Rows[0]["NOMBRE9"].ToString();
                txtObs10.Text = dt.Rows[0]["NOMBRE10"].ToString();
                txtObs11.Text = dt.Rows[0]["NOMBRE11"].ToString();
                txtObs12.Text = dt.Rows[0]["NOMBRE12"].ToString();

                _NommbrePersona1= dt.Rows[0]["NOMBRE1"].ToString();
                _NommbrePersona2 = dt.Rows[0]["NOMBRE2"].ToString();
                _NommbrePersona3 = dt.Rows[0]["NOMBRE3"].ToString();
                _NommbrePersona4 = dt.Rows[0]["NOMBRE4"].ToString();
                _NommbrePersona5 = dt.Rows[0]["NOMBRE5"].ToString();
                _NommbrePersona6 = dt.Rows[0]["NOMBRE6"].ToString();
                _NommbrePersona7 = dt.Rows[0]["NOMBRE7"].ToString();
                _NommbrePersona8 = dt.Rows[0]["NOMBRE8"].ToString();
                _NommbrePersona9 = dt.Rows[0]["NOMBRE9"].ToString();
                _NommbrePersona10 = dt.Rows[0]["NOMBRE10"].ToString();
                _NommbrePersona11= dt.Rows[0]["NOMBRE11"].ToString();
                _NommbrePersona12 = dt.Rows[0]["NOMBRE12"].ToString();

                _idpersona1 = Convert.ToInt32(dt.Rows[0]["PERSONA1"].ToString());
                _idpersona2 = Convert.ToInt32(dt.Rows[0]["PERSONA2"].ToString());
                _idpersona3 = Convert.ToInt32(dt.Rows[0]["PERSONA3"].ToString());
                _idpersona4 = Convert.ToInt32(dt.Rows[0]["PERSONA4"].ToString());
                _idpersona5 = Convert.ToInt32(dt.Rows[0]["PERSONA5"].ToString());
                _idpersona6 = Convert.ToInt32(dt.Rows[0]["PERSONA6"].ToString());
                _idpersona7 = Convert.ToInt32(dt.Rows[0]["PERSONA7"].ToString());
                _idpersona8 = Convert.ToInt32(dt.Rows[0]["PERSONA8"].ToString());
                _idpersona9 = Convert.ToInt32(dt.Rows[0]["PERSONA9"].ToString());
                _idpersona10 = Convert.ToInt32(dt.Rows[0]["PERSONA10"].ToString());
                _idpersona11 = Convert.ToInt32(dt.Rows[0]["PERSONA11"].ToString());
                _idpersona12 = Convert.ToInt32(dt.Rows[0]["PERSONA12"].ToString());
                
            }
            else
            {
                //MessageBox.Show("No hay Datos", "Aviso");

                label1.Text = "";
                label2.Text = "";
                label3.Text = "";
                label4.Text = "";
                label5.Text = "";
                label6.Text = "";
                label7.Text = "";
                label8.Text = "";
                label9.Text = "";
                label10.Text = "";
                label11.Text = "";
                label12.Text = "";

                lnom1.Text = "";
                lnom2.Text = "";
                lnom3.Text = "";
                lnom4.Text = "";
                lnom5.Text = "";
                lnom6.Text = "";
                lnom7.Text = "";
                lnom8.Text = "";
                lnom9.Text = "";
                lnom10.Text = "";
                lnom11.Text = "";
                lnom12.Text = "";

                tscboAreas1.Text = "";
                tscboAreas2.Text = "";
                tscboAreas3.Text = "";
                tscboAreas4.Text = "";
                tscboAreas5.Text = "";
                tscboAreas6.Text = "";
                tscboAreas7.Text = "";
                tscboAreas8.Text = "";
                tscboAreas9.Text = "";
                tscboAreas10.Text = "";
                tscboAreas11.Text = "";
                tscboAreas12.Text = "";

                txtObs1.Text = "";
                txtObs2.Text = "";
                txtObs3.Text = "";
                txtObs4.Text = "";
                txtObs5.Text = "";
                txtObs6.Text = "";
                txtObs7.Text = "";
                txtObs8.Text = "";
                txtObs9.Text = "";
                txtObs10.Text = "";
                txtObs11.Text = "";
                txtObs12.Text = "";

                _NommbrePersona1 = "";
                _NommbrePersona2 = "";
                _NommbrePersona3 = "";
                _NommbrePersona4 = "";
                _NommbrePersona5 = "";
                _NommbrePersona6 = "";
                _NommbrePersona7 = "";
                _NommbrePersona8 = "";
                _NommbrePersona9 = "";
                _NommbrePersona10 = "";
                _NommbrePersona11 = "";
                _NommbrePersona12 = "";

                _idpersona1 = 0;
                _idpersona2 = 0;
                _idpersona3 = 0;
                _idpersona4 = 0;
                _idpersona5 = 0;
                _idpersona6 = 0;
                _idpersona7 = 0;
                _idpersona8 = 0;
                _idpersona9 = 0;
                _idpersona10 = 0;
                _idpersona11 = 0;
                _idpersona12 = 0;
            }

        }

        private void cboGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TipoRegistro == 1 || TipoRegistro == 0)
            {
                ListarFormato(Convert.ToInt32(cboProcesos.SelectedValue), Convert.ToInt32(cboGrupo.SelectedValue));
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

            button1.Visible = false;
            button8.Visible = false;
            button3.Visible = false;
            btnGuardarFormato.Visible = false;
            cbxImprimir.Visible = false;
            CapturaFormulario();

            Impresora.Document = DocumentoParaImprimir;
            DialogResult Result = Impresora.ShowDialog();

            if (Result == DialogResult.OK)
            {
                DocumentoParaImprimir.DefaultPageSettings.Landscape = false;
                DocumentoParaImprimir.Print();
            }

            splitContainer1.Panel1.BackColor = Color.PaleGoldenrod;
            //groupBox1.BackColor = Color.PaleGoldenrod;
            // groupBox3.BackColor = Color.PaleGoldenrod;
            lblFechaIngreso.BackColor = Color.PaleGoldenrod;
            label13.BackColor = Color.PaleGoldenrod;
            label15.BackColor = Color.PaleGoldenrod;
            // label14.BackColor = Color.PaleGoldenrod;
            lblArea.BackColor = Color.PaleGoldenrod;
            lblAreaVer.BackColor = Color.PaleGoldenrod;
            lblCargo.BackColor = Color.PaleGoldenrod;
            button1.Visible = true;
            button8.Visible = true;
            cbxImprimir.Visible = true;
            cbxImprimir.Checked = false;
            if (TipoRegistro == 1)
            {
                button3.Visible = true;
            }

            if (TipoRegistro == 0)
            {
                btnGuardarFormato.Visible = true;
            }

            if (TipoRegistro == 2)
            {
                chbFirma1.Visible = true;
                cbhFirma2.Visible = true;
                cbhFirma3.Visible = true;
                cbhFirma4.Visible = true;
                cbhFirma5.Visible = true;
                cbhFirma6.Visible = true;
                cbhFirma7.Visible = true;
                cbhFirma8.Visible = true;
                cbhFirma9.Visible = true;
                cbhFirma10.Visible = true;
                cbhFirma11.Visible = true;
                cbhFirma12.Visible = true;
            }

            splitContainer5.Visible = true;
            splitContainer7.Visible = true;
            splitContainer9.Visible = true;
            splitContainer11.Visible = true;
            splitContainer10.Visible = true;
            splitContainer12.Visible = true;

        }

        private void CapturaFormulario()
        {
            //groupBox1.BackColor = Color.White;
            //groupBox3.BackColor = Color.White;
            lblFechaIngreso.BackColor = Color.White;
            label3.BackColor = Color.White;
            label5.BackColor = Color.White;

            Graphics mygraphics = this.CreateGraphics();
            Size sz = this.ClientRectangle.Size;
            bmp = new Bitmap(sz.Width, sz.Height, mygraphics);
            Graphics memoryGraphics = Graphics.FromImage(bmp);
            IntPtr dc1 = mygraphics.GetHdc();
            IntPtr dc2 = memoryGraphics.GetHdc();
            BitBlt(dc2, 0, 0, this.ClientRectangle.Width,
                   this.ClientRectangle.Height, dc1, 0, 0, 13369376);
            mygraphics.ReleaseHdc(dc1);
            memoryGraphics.ReleaseHdc(dc2);

            //bmp.Save("prueba.bmp", ImageFormat.Bmp);
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern long BitBlt(IntPtr hdcDest,
            int nXDest,
            int nYDest,
            int nWidth,
            int nHeight,
            IntPtr hdcSrc,
            int nXSrc,
            int nYSrc,
            int dwRop);

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button8.Visible = false;
            button3.Visible = false;
            btnGuardarFormato.Visible = false;
            cbxImprimir.Visible = false;
            CapturaFormulario();

            //VALIDANDO SI HAY DATOS
            VistaPrevia.Document = DocumentoParaImprimir;
            VistaPrevia.ShowDialog();
           
            splitContainer1.Panel1.BackColor = Color.PaleGoldenrod;
                //groupBox1.BackColor = Color.PaleGoldenrod;
               // groupBox3.BackColor = Color.PaleGoldenrod;
                lblFechaIngreso.BackColor = Color.PaleGoldenrod;
                label13.BackColor = Color.PaleGoldenrod;
                label15.BackColor = Color.PaleGoldenrod;
               // label14.BackColor = Color.PaleGoldenrod;
                lblArea.BackColor = Color.PaleGoldenrod;
                lblAreaVer.BackColor = Color.PaleGoldenrod;
                lblCargo.BackColor = Color.PaleGoldenrod;
                button1.Visible = true;
                button8.Visible = true;
                cbxImprimir.Visible = true;
                cbxImprimir.Checked = false;
                if (TipoRegistro == 1)
                {
                    button3.Visible = true;
                }

                if (TipoRegistro == 0)
                {
                    btnGuardarFormato.Visible = true;
                }

                if (TipoRegistro == 2)
                {                    
                    chbFirma1.Visible = true;
                    cbhFirma2.Visible = true;
                    cbhFirma3.Visible = true;
                    cbhFirma4.Visible = true;
                    cbhFirma5.Visible = true;
                    cbhFirma6.Visible = true;
                    cbhFirma7.Visible = true;
                    cbhFirma8.Visible = true;
                    cbhFirma9.Visible = true;
                    cbhFirma10.Visible = true;
                    cbhFirma11.Visible = true;
                    cbhFirma12.Visible = true;
                }

                splitContainer5.Visible = true;
                splitContainer7.Visible = true;
                splitContainer9.Visible = true;
                splitContainer11.Visible = true;
                splitContainer10.Visible = true;
                splitContainer12.Visible = true;
        }

        private void tool2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tool1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!txtObs1.Text.Equals(_NommbrePersona1))
            {
                _idpersona1 = idpersona1;
            }

            if (txtPersona.Text.Length == 0)
            {
                MessageBox.Show("Ingresar nombre del personal", "Mensaje");
                txtPersona.Focus();
                txtPersona.BackColor = Color.Aquamarine;
                return;
            }
            

            idproceso = Convert.ToInt32(cboProcesos.SelectedValue);
            idgrupo = Convert.ToInt32(cboGrupo.SelectedValue);
            fecha = dtpFechaSalida.Text;
          
            DataTable dtRegistroHoja = new DataTable();
            dtRegistroHoja = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Registrar(1, compania, codigohoja, anio, idproceso, idgrupo, idpersona, fecha, _idpersona1, label1.Text, firma1,
                                        "", _idpersona2, label2.Text, firma2, "", _idpersona3, label3.Text, firma3, "", _idpersona4, label4.Text, firma4, "",
                                        _idpersona5, label5.Text, firma5, "", _idpersona6, label6.Text, firma6, "", _idpersona7, label7.Text, firma7, "",
                                        _idpersona8, label8.Text, firma8, "", _idpersona9, label9.Text, firma9, "", _idpersona10, label10.Text, firma10,
                                         "", _idpersona11, label11.Text, firma11, "", _idpersona12, label12.Text, firma12, ""
                                        ,Utilitario.Instancia.SesionUsuario.usuario);

            string Rpta = Convert.ToString(dtRegistroHoja.Rows[0]["exito"]);
            string NrRPTA = Rpta.Substring(0, 1);

            if (NrRPTA == "0")
            {
                Actualizar = 1;
                MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);              
                this.Close();
                
            }
            else
            {
                MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void lstPersona_Enter(object sender, EventArgs e)
        {
            if (!lstPersona.Items.Count.Equals(0))
            {
                lstPersona.Items[0].Selected = true;
            }
        }

        private void lstPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            
                if (posicion == 0)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona = Convert.ToInt32(ItemActual.Text);
                        txtCodigo.Text = idpersona.ToString();
                        txtPersona.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;

                        DataTable dtCargo = new DataTable();
                        dtCargo = clsRecursosHumanosBL.Instancia.GetAreaPersona(idpersona);

                        if (dtCargo.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtCargo.Rows.Count; i++)
                            {
                                lblCargo.Text = dtCargo.Rows[i]["Cargo"].ToString();
                                //  idcargo = dtCargo.Rows[i]["CodigoPuesto"].ToString();
                                dtpFechaIngreso.Text = dtCargo.Rows[i]["FechaIngreso"].ToString();
                                lblAreaVer.Text = dtCargo.Rows[i]["Area"].ToString();
                            }
                        }
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtPersona.Focus();
                    }
                }
                if (posicion == 1)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona1 = Convert.ToInt32(ItemActual.Text);
                        txtObs1.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs1.Focus();
                    }
                }
                if (posicion == 2)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona2 = Convert.ToInt32(ItemActual.Text);
                        txtObs2.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs2.Focus();
                    }
                }
                if (posicion == 3)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona3 = Convert.ToInt32(ItemActual.Text);
                        txtObs3.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs3.Focus();
                    }

                }
                if (posicion == 4)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona4 = Convert.ToInt32(ItemActual.Text);
                        txtObs4.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs4.Focus();
                    }
                  
                }
                if (posicion ==5)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona5 = Convert.ToInt32(ItemActual.Text);
                        txtObs5.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs5.Focus();
                    }
                }
                if (posicion == 6)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona6 = Convert.ToInt32(ItemActual.Text);
                        txtObs6.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs6.Focus();
                    }
                 
                }
                if (posicion == 7)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona7 = Convert.ToInt32(ItemActual.Text);
                        txtObs7.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs7.Focus();
                    }
                }
                if (posicion == 8)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona8 = Convert.ToInt32(ItemActual.Text);
                        txtObs8.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs8.Focus();
                    }
                }
                if (posicion == 9)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona9 = Convert.ToInt32(ItemActual.Text);
                        txtObs9.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs9.Focus();
                    }
                }
                if (posicion == 10)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona10 = Convert.ToInt32(ItemActual.Text);
                        txtObs10.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs10.Focus();
                    }
                }
                if (posicion == 11)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona11 = Convert.ToInt32(ItemActual.Text);
                        txtObs11.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs11.Focus();
                    }
                }
                if (posicion == 12)
                {
                    if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return) && !lstPersona.Items.Count.Equals(0))
                    {
                        ListViewItem ItemActual;

                        ItemActual = lstPersona.SelectedItems[0];

                        idpersona12 = Convert.ToInt32(ItemActual.Text);
                        txtObs12.Text = ItemActual.SubItems[1].Text;
                        lstPersona.Visible = false;
                    }
                    if (e.KeyChar == (char)Keys.Escape)
                    {
                        lstPersona.Visible = false;
                        txtObs12.Focus();
                    }
                }      

           
        }

        private void lstPersona_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (posicion == 0)
            {
                ListViewItem ItemActual;

                ItemActual = lstPersona.SelectedItems[0];

                idpersona = Convert.ToInt32(ItemActual.Text);
                txtPersona.Text = ItemActual.SubItems[1].Text;
                lstPersona.Visible = false;

                DataTable dtCargo = new DataTable();
                dtCargo = clsRecursosHumanosBL.Instancia.GetAreaPersona(idpersona);

                if (dtCargo.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCargo.Rows.Count; i++)
                    {
                        lblCargo.Text = dtCargo.Rows[i]["Cargo"].ToString();
                        //  idcargo = dtCargo.Rows[i]["CodigoPuesto"].ToString();
                        dtpFechaIngreso.Text = dtCargo.Rows[i]["FechaIngreso"].ToString();
                    }
                }

            }
           
        }

        private void txtPersona_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtPersona.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(350, 111);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
               
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtPersona.Focus();
            }
        }

        private void btnGuardarFormato_Click(object sender, EventArgs e)
        {
            idproceso = Convert.ToInt32(cboProcesos.SelectedValue.ToString());
            idgrupo = Convert.ToInt32(cboGrupo.SelectedValue.ToString());

            //VALIDANDO DATOS MODIFICADOS
            if (txtObs1.Text.Equals(_NommbrePersona1))
            {
                idpersona1 = _idpersona1;
            }
            if (txtObs2.Text.Equals(_NommbrePersona2))
            {
                idpersona2 = _idpersona2;
            }
            if (txtObs3.Text.Equals(_NommbrePersona3))
            {
                idpersona3 = _idpersona3;
            }
            if (txtObs4.Text.Equals(_NommbrePersona4))
            {
                idpersona4 = _idpersona4;
            }
            if (txtObs5.Text.Equals(_NommbrePersona5))
            {
                idpersona5 = _idpersona5;
            }
            if (txtObs6.Text.Equals(_NommbrePersona6))
            {
                idpersona6 = _idpersona6;
            }
            if (txtObs7.Text.Equals(_NommbrePersona7))
            {
                idpersona7 = _idpersona7;
            }
            if (txtObs8.Text.Equals(_NommbrePersona8))
            {
                idpersona8 = _idpersona8;
            }
            if (txtObs9.Text.Equals(_NommbrePersona9))
            {
                idpersona9 = _idpersona9;
            }
            if (txtObs10.Text.Equals(_NommbrePersona10))
            {
                idpersona10 = _idpersona10;
            } 
            if (txtObs11.Text.Equals(_NommbrePersona11))
            {
                idpersona11 = _idpersona11;
            } 
            if (txtObs12.Text.Equals(_NommbrePersona12))
            {
                idpersona12 = _idpersona12;
            }
                       

            DataTable dtGuardarFormato = new DataTable();
            dtGuardarFormato = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_RegistrarPorFormato(1, idproceso, idgrupo, 0, fecha, idpersona1, tscboAreas1.Text, idpersona2, tscboAreas2.Text, idpersona3,
                                        tscboAreas3.Text, idpersona4, tscboAreas4.Text, idpersona5, tscboAreas5.Text, idpersona6, tscboAreas6.Text, idpersona7, tscboAreas7.Text, idpersona8, tscboAreas8.Text,
                                        idpersona9, tscboAreas9.Text, idpersona10, tscboAreas10.Text, idpersona11, tscboAreas11.Text, idpersona12, tscboAreas12.Text, Utilitario.Instancia.SesionUsuario.usuario);

            string Rpta = Convert.ToString(dtGuardarFormato.Rows[0]["exito"]);
            string NrRPTA = Rpta.Substring(0, 1);

            if (NrRPTA == "0")
            {
                MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void txtObs1_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 1;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs1.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(0, 230);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus(); 
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs1.Focus();               
            }
        }

        private void txtObs2_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 2;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs2.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(420, 230);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs2.Focus();
            }
        }

        private void txtObs3_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 3;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs3.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(0, 360);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs3.Focus();
            }
        }

        private void txtObs4_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 4;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs4.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(420, 360);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs4.Focus();
            }
        }

        private void txtObs5_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 5;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs5.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(0, 500);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs5.Focus();
            }
        }

        private void txtObs6_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 6;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs6.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(420, 500);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs6.Focus();
            }
        }

        private void txtObs7_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 7;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs7.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(0, 640);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs7.Focus();
            }
        }

        private void txtObs8_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 8;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs8.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(420, 640);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs8.Focus();
            }
        }

        private void txtObs9_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 9;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs9.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(0, 790);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs9.Focus();
            }
        }

        private void txtObs10_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 10;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs10.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(420, 790);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs10.Focus();
            }
        }

        private void txtObs11_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 11;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs11.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(0, 810);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs11.Focus();
            }
        }

        private void txtObs12_KeyPress(object sender, KeyPressEventArgs e)
        {
            posicion = 12;
            if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {
                clsVisuales.Instancia.LlenarLw(lstPersona, clsConsultaBL.Instancia.GetPersona(txtObs12.Text), true, false, false);

                lstPersona.Columns[0].Width = 0;
                lstPersona.Columns[1].Width = 206;
                lstPersona.Columns[2].Width = 110;

                lstPersona.Size = new System.Drawing.Size(380, 111);
                lstPersona.Location = new Point(420, 810);

                lstPersona.BringToFront();
                lstPersona.Visible = true;
                lstPersona.Focus();
            }

            if (e.KeyChar == (char)Keys.Escape)
            {
                lstPersona.Visible = false;
                txtObs12.Focus();
            }
        }

        private void chbFirmar_CheckedChanged(object sender, EventArgs e)
        {            
            if (chbFirma1.Checked == true)
            {                       
                    if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {                        
                        if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                        {
                            byte[] imagen1 = null;
                            dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(1, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                            string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                            string NrRPTA = Rpta.Substring(0, 1);

                            if (NrRPTA == "0")
                            {
                                MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ListarHojaEditar();
                            }
                            else
                            {
                                MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                    else
                    {
                        this.pictureBox1.Image = null;
                        chbFirma1.Checked = false;
                        return;
                    }
                   
                /*OpenFileDialog dialog = new OpenFileDialog();

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.pictureBox1.Image = Image.FromFile(dialog.FileName);
                    if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.IO.MemoryStream ms1 = new System.IO.MemoryStream();
                        this.pictureBox1.Image.Save(ms1, System.Drawing.Imaging.ImageFormat.Png);
                        byte[] imagen1 = ms1.GetBuffer();
                        if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                        {
                            dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(1, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input, Utilitario.Instancia.SesionUsuario.usuario);
                            string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                            string NrRPTA = Rpta.Substring(0, 1);

                            if (NrRPTA == "0")
                            {
                                MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ListarHojaEditar();
                            }
                            else
                            {
                                MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                    }
                    else 
                    {
                        this.pictureBox1.Image = null;
                        chbFirma1.Checked = false;
                        return;
                    }
                }*/   
            }
            else
            {               
                this.pictureBox1.Image = null;
            }
        }

        private void cbhFirma2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma2.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(2, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {                
                this.pictureBox2.Image = null;
            }
        }

        private void cbhFirma3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma3.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(3, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {
                //this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pictureBox3.Image = null;
            }
        }

        private void cbhFirma4_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma4.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(4, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {
                //this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pictureBox4.Image = null;
            }
        }

        private void cbhFirma5_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma5.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(5, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {
                //this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pictureBox5.Image = null;
            }
        }

        private void cbhFirma6_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma6.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(6, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {
                //this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pictureBox6.Image = null;
            }
        }

        private void cbhFirma7_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma7.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(7, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {
                this.pictureBox7.Image = null;
            }
        }

        private void cbhFirma8_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma8.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(8, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {
                this.pictureBox8.Image = null;
            }
        }

        private void cbhFirma9_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma9.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(9, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {                
                this.pictureBox9.Image = null;
            }
        }

        private void cbhFirma10_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma10.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(10, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {
                this.pictureBox10.Image = null;
            }
        }

        private void cbhFirma11_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma11.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(11, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {
                this.pictureBox11.Image = null;
            }
        }

        private void cbhFirma12_CheckedChanged(object sender, EventArgs e)
        {
            if (cbhFirma12.Checked == true)
            {
                if (MessageBox.Show("Desea firmar la Hoja de Recorrido...?", "FIRMA", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (ShowInputDialogBox(ref input, "Agregar Observación..", "OBSERVACION", 300, 200) == DialogResult.OK)
                    {
                        byte[] imagen1 = null;
                        dtFirmar = clsRecursosHumanosBL.Instancia.GetDataHojaRecorrido_Firmar(12, compania, anio, codigohoja, idproceso, idgrupo, IDPERSONFIRMAR, imagen1, input.ToUpper(), Utilitario.Instancia.SesionUsuario.usuario);
                        string Rpta = Convert.ToString(dtFirmar.Rows[0]["exito"]);
                        string NrRPTA = Rpta.Substring(0, 1);

                        if (NrRPTA == "0")
                        {
                            MessageBox.Show(Rpta, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ListarHojaEditar();
                        }
                        else
                        {
                            MessageBox.Show(Rpta, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                }
                else
                {
                    this.pictureBox1.Image = null;
                    chbFirma1.Checked = false;
                    return;
                }
            }
            else
            {
                this.pictureBox12.Image = null;
            }
        }

        private static DialogResult ShowInputDialogBox(ref string input, string prompt, string title = "Agregar Observación", int width = 100, int height = 200)
        {
            Size size = new Size(width, height);
            Form inputBox = new Form();

            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.ClientSize = size;
            inputBox.Height = 150;
            inputBox.StartPosition = FormStartPosition.CenterScreen;
            inputBox.Text = title;

            //Create a new label to hold the prompt
            Label label = new Label();
            label.Text = prompt;
            label.Location = new Point(5, 5);
            label.Width = size.Width - 10;
            label.Margin = new System.Windows.Forms.Padding(3, 25, 2, 35);
            inputBox.Controls.Add(label);

            //Create a textbox to accept the user's input
            TextBox textBox = new TextBox();
            textBox.Size = new Size(260, 23);
            textBox.Location = new Point(20, label.Location.Y + 20);
            textBox.Text = input.ToUpper();
            textBox.Location = new Point(20, 40);
            inputBox.Controls.Add(textBox);

            //Create an OK Button 
            Button okButton = new Button();
            okButton.DialogResult = DialogResult.OK;
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.Text = "&OK";
            okButton.Location = new Point(size.Width - 80 - 80, 100 - 30);
            inputBox.Controls.Add(okButton);

            //Create a Cancel Button
            Button cancelButton = new Button();
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.Text = "&Cancel";
            cancelButton.Location = new Point(size.Width - 80, 100 - 30);
            inputBox.Controls.Add(cancelButton);

            //Set the input box's buttons to the created OK and Cancel Buttons respectively so the window appropriately behaves with the button clicks
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            //Show the window dialog box 
            DialogResult result = inputBox.ShowDialog();
            input = textBox.Text;
            string obser = textBox.Text;
            return result;
        }

        private void cbxImprimir_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxImprimir.Checked == true)
            {
                splitContainer1.Panel1.BackColor = Color.White;
                lblFechaIngreso.BackColor = Color.White;
                label13.BackColor = Color.White;
                label15.BackColor = Color.White;
                //label14.BackColor = Color.White;
                lblCargo.BackColor = Color.White;
                button1.Enabled = true;
                button8.Enabled = true;
                lblArea.BackColor = Color.White;
                lblAreaVer.BackColor = Color.White;

                if (TipoRegistro == 2)
                {
                    chbFirma1.Visible = false;
                    cbhFirma2.Visible = false;
                    cbhFirma3.Visible = false;
                    cbhFirma4.Visible = false;
                    cbhFirma5.Visible = false;
                    cbhFirma6.Visible = false;
                    cbhFirma7.Visible = false;
                    cbhFirma8.Visible = false;
                    cbhFirma9.Visible = false;
                    cbhFirma10.Visible = false;
                    cbhFirma11.Visible = false;
                    cbhFirma12.Visible = false;

                    if (lnom1.Text.Equals(""))
                    {
                        splitContainer5.Visible = false;
                    }
                    if (lnom2.Text.Equals(""))
                    {
                        splitContainer7.Visible = false;
                    }
                    if (lnom5.Text.Equals(""))
                    {
                        splitContainer9.Visible = false;
                    }
                    if (lnom6.Text.Equals(""))
                    {
                        splitContainer11.Visible = false;
                    }
                    if (lnom9.Text.Equals(""))
                    {
                        splitContainer10.Visible = false;
                    }
                    if (lnom11.Text.Equals(""))
                    {
                        splitContainer12.Visible = false;
                    }
                }
            }
            else 
            {
                splitContainer1.Panel1.BackColor = Color.PaleGoldenrod;

                lblFechaIngreso.BackColor = Color.PaleGoldenrod;
                label13.BackColor = Color.PaleGoldenrod;
                label15.BackColor = Color.PaleGoldenrod;
                //label14.BackColor = Color.PaleGoldenrod;
                lblArea.BackColor = Color.PaleGoldenrod;
                lblAreaVer.BackColor = Color.PaleGoldenrod;
                lblCargo.BackColor = Color.PaleGoldenrod;
                button1.Enabled = false;
                button8.Enabled = false;

                if (TipoRegistro == 2)
                {
                    chbFirma1.Visible = true;
                    cbhFirma2.Visible = true;
                    cbhFirma3.Visible = true;
                    cbhFirma4.Visible = true;
                    cbhFirma5.Visible = true;
                    cbhFirma6.Visible = true;
                    cbhFirma7.Visible = true;
                    cbhFirma8.Visible = true;
                    cbhFirma9.Visible = true;
                    cbhFirma10.Visible = true;
                    cbhFirma11.Visible = true;
                    cbhFirma12.Visible = true;
                }
                splitContainer5.Visible = true;
                splitContainer7.Visible = true;     
                splitContainer9.Visible = true;
                splitContainer11.Visible = true;
                splitContainer10.Visible = true;
                splitContainer12.Visible = true;
                
            }
        }             
    }
}
