using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using ReportesTranspesa.Sistema;
using System.Text.RegularExpressions;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class Mantenimiento_Tractos : MetroFramework.Forms.MetroForm
    {
        public Mantenimiento_Tractos()
        {
            InitializeComponent();
        }

        double n1 = 0, n2 = 0;
        double var1 = 0, var2 = 0, var3 = 0;

        #region Mantenimiento_M1
        const double acetmotor = 20000;
        const double filtCombt = 20000;
        const double boyas = 20000;
        const double baterias = 20000;
        #endregion

        #region Mantenimiento_M2
        const double calibmotor = 60000;
        const double filtaire1 = 60000;
        const double filtaire2 = 60000;
        const double filtSecado = 60000;
        const double intercooler = 60000;
        const double puentdelat = 60000;
        const double alineamiento = 60000;
        const double sensores = 60000;
        const double alternador = 60000;
        const double arrancador = 60000;
        const double fajaAlter = 60000;
        const double fajaVent = 60000;
        const double fajaBomba = 60000;
        #endregion

        #region Mantenimiento_M3
        const double aceitCamb = 120000;
        const double aceiteDireccion = 120000;
        const double Bocamasa = 120000;
        const double aceiteCorona = 120000;
        #endregion


        private void Mantenimiento_Tractos_Load(object sender, EventArgs e)
        {
            ComboTractos();
            cboTracto.SelectedIndex = 0;
            splitContainer2.Visible = false;
            Actividades();
        }

        private void rbMantenimiento_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer2.Visible = false;
        }

        private void rbActividades_CheckedChanged(object sender, EventArgs e)
        {
            splitContainer2.Visible = true;
        }

        public void PorcentajesM1(double v1, double v2)
        {
            double Porciento = (v1 - v2) * 100 / acetmotor;
            txtAceiteMotor.Text = Porciento.ToString("N");

            double Porciento1 = (v1 - v2) * 100 / filtCombt;
            txtFiltroCombustible.Text = Porciento1.ToString("N");

            double Porciento2 = (v1 - v2) * 100 / boyas;
            txtLimpiezaBoya.Text = Porciento2.ToString("N");

            double Porciento3 = (v1 - v2) * 100 / baterias;
            txtMttoBaterias.Text = Porciento3.ToString("N");

        }

        public void PorcentajesM2(double v1, double v2)
        {
            double Porciento4 = (v1 - v2) * 100 / calibmotor;
            txtCalibracionMotor.Text = Porciento4.ToString("N");

            double Porciento5 = (v1 - v2) * 100 / filtaire1;
            txtFiltro1.Text = Porciento5.ToString("N");

            double Porciento6 = (v1 - v2) * 100 / filtaire2;
            txtFiltro2.Text = Porciento6.ToString("N");

            double Porciento7 = (v1 - v2) * 100 / filtSecado;
            txtFiltroSecador.Text = Porciento7.ToString("N");

            double Porciento8 = (v1 - v2) * 100 / intercooler;
            txtIntercooler.Text = Porciento8.ToString("N");

            double Porciento9 = (v1 - v2) * 100 / puentdelat;
            txtPuenteDelantero.Text = Porciento9.ToString("N");

            double Porciento10 = (v1 - v2) * 100 / alineamiento;
            txtAlineamiento.Text = Porciento10.ToString("N");

            double Porciento11 = (v1 - v2) * 100 / sensores;
            txtSensores.Text = Porciento11.ToString("N");

            double Porciento12 = (v1 - v2) * 100 / alternador;
            txtAlternador.Text = Porciento12.ToString("N");

            double Porciento13 = (v1 - v2) * 100 / arrancador;
            txtArrancador.Text = Porciento13.ToString("N");

            double Porciento14 = (v1 - v2) * 100 / fajaAlter;
            txtTFA.Text = Porciento14.ToString("N");

            double Porciento15 = (v1 - v2) * 100 / fajaVent;
            txtTFV.Text = Porciento15.ToString("N");

            double Porciento16 = (v1 - v2) * 100 / fajaBomba;
            txtTBA.Text = Porciento16.ToString("N");
        }

        public void PorcentajesM3(double v1, double v2)
        {
            double Porciento17 = (v1 - v2) * 100 / aceitCamb;
            txtAceiteCC.Text = Porciento17.ToString("N");

            double Porciento18 = (v1 - v2) * 100 / aceiteDireccion;
            txtAceiteCD.Text = Porciento18.ToString("N");

            double Porciento19 = (v1 - v2) * 100 / Bocamasa;
            txtBocamasa.Text = Porciento19.ToString("N");

            double Porciento20 = (v1 - v2) * 100 / aceiteCorona;
            txtAceiteCorona.Text = Porciento20.ToString("N");
        }

        private void txtKM1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtKM1.Text))
            {
                txtKM1.Text = "";
            }
            else
            {
                n2 = Convert.ToDouble(txtKM1.Text.ToString());
                PorcentajesM1(n1, n2);
            }
        }

        private void txtKilometraje_TextChanged(object sender, EventArgs e)
        {
            n1 = Convert.ToDouble(txtKilometraje.Text.ToString());
            PorcentajesM1(n1, n2);
            PorcentajesM2(n1, n2);
            PorcentajesM3(n1, n2);

            //var1 = Convert.ToDouble(txtKilometraje.Text);
            //KMCambioMotor(var1, var2);
        }

        private void txtKM3_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtKM3.Text))
            {
                txtKM3.Text = "";
            }
            else
            {
                n2 = Convert.ToDouble(txtKM3.Text.ToString());
                PorcentajesM3(n1, n2);
            }
        }

        private void txtKM2_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtKM2.Text))
            {
                txtKM2.Text = "";
            }
            else
            {
                n2 = Convert.ToDouble(txtKM2.Text.ToString());
                PorcentajesM2(n1, n2);
            }
        }

        public void KMCambioMotor(double r1, double r2)
        {
            double Cambio = ((100 - r2) / 100) * r1;
            txtCambioAceite.Text = Cambio.ToString("N2");
        }

        private void txtFiltroCombustible_TextChanged(object sender, EventArgs e)
        {
            var1 = filtCombt;
            var2 = Convert.ToDouble(txtFiltroCombustible.Text.ToString());
            KMCambioMotor(var1, var2);
        }

        public void Actividades()
        {
            string AceitMotor="";
            AceitMotor = Regex.Replace(txtAceiteMotor.Text,@"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));//.Remove(txtAceiteMotor.Text.Length - 1);//.TrimEnd(',');
            string FiltComb="";
            //FiltComb = txtFiltroCombustible.Text.Remove(txtFiltroCombustible.Text.Length - 1);//.TrimEnd(',');
            FiltComb = Regex.Replace(txtFiltroCombustible.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string Boyas="";
            //Boyas = txtLimpiezaBoya.Text.Remove(txtAceiteMotor.Text.Length - 1);//.TrimEnd(',');
            Boyas = Regex.Replace(txtFiltroCombustible.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string Baterias="";
            //Baterias = txtMttoBaterias.Text.Remove(txtMttoBaterias.Text.Length - 1);//.TrimEnd(',');
            Baterias = Regex.Replace(txtMttoBaterias.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string CalibMotor="";
            //CalibMotor = txtCalibracionMotor.Text.Remove(txtCalibracionMotor.Text.Length - 1);//.TrimEnd(',');
            CalibMotor = Regex.Replace(txtCalibracionMotor.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string Filtro1="";
            //Filtro1 = txtFiltro1.Text.Remove(txtFiltro1.Text.Length - 1);//.TrimEnd(',');
            Filtro1 = Regex.Replace(txtFiltro1.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string Filtro2="";
            //Filtro2 = txtFiltro2.Text.Remove(txtFiltro2.Text.Length - 1);//.TrimEnd(',');
            Filtro2 = Regex.Replace(txtFiltro2.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string FiltroAire="";
            //FiltroAire = txtFiltroSecador.Text.Remove(txtFiltroSecador.Text.Length - 1);//.TrimEnd(',');
            FiltroAire = Regex.Replace(txtFiltroSecador.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string Intercooler="";
            //Intercooler = txtIntercooler.Text.Remove(txtIntercooler.Text.Length - 1);//.TrimEnd(',');
            Intercooler = Regex.Replace(txtIntercooler.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string PuenteDelantero="";
            //PuenteDelantero = txtPuenteDelantero.Text.Remove(txtPuenteDelantero.Text.Length - 1);//.TrimEnd(',');
            PuenteDelantero = Regex.Replace(txtPuenteDelantero.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string Alineamiento="";
            //Alineamiento = txtAlineamiento.Text.Remove(txtAlineamiento.Text.Length - 1);//.TrimEnd(',');
            Alineamiento = Regex.Replace(txtAlineamiento.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string Sensores="";
            //Sensores = txtSensores.Text.Remove(txtSensores.Text.Length - 1);//.TrimEnd(',');
            Sensores = Regex.Replace(txtSensores.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string Alternador="";
            //Alternador = txtAlternador.Text.Remove(txtAlternador.Text.Length - 1);//.TrimEnd(',');
            Alternador = Regex.Replace(txtAlternador.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string Arrancador="";
            //Arrancador = txtArrancador.Text.Remove(txtArrancador.Text.Length - 1);//.TrimEnd(',');
            Arrancador = Regex.Replace(txtArrancador.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string TemplFajaAlternador="";
            //TemplFajaAlternador = txtTFA.Text.Remove(txtTFA.Text.Length - 1);//.TrimEnd(',');
            TemplFajaAlternador = Regex.Replace(txtTFA.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string TemplFajaVentilador="";
            //TemplFajaVentilador = txtTFV.Text.Remove(txtTFV.Text.Length - 1);//.TrimEnd(',');
            TemplFajaVentilador = Regex.Replace(txtTFV.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string TemplFajaBombaAgua="";
            //TemplFajaBombaAgua = txtTBA.Text.Remove(txtTBA.Text.Length - 1);//.TrimEnd(',');
            TemplFajaBombaAgua = Regex.Replace(txtTBA.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string Aceitaja_de_Cambios="";
            //Aceitaja_de_Cambios = txtAceiteCC.Text.Remove(txtAceiteCC.Text.Length - 1);//.TrimEnd(',');
            Aceitaja_de_Cambios = Regex.Replace(txtAceiteCC.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string AceitCajaDirec="";
            //AceitCajaDirec = txtAceiteCD.Text.Remove(txtAceiteCD.Text.Length - 1);//.TrimEnd(',');
            AceitCajaDirec = Regex.Replace(txtAceiteCD.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string MantBocamasa="";
            //MantBocamasa = txtBocamasa.Text.Remove(txtBocamasa.Text.Length - 1);//.TrimEnd(',');
            MantBocamasa = Regex.Replace(txtBocamasa.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            string AceitCorona="";
            //AceitCorona = txtAceiteCorona.Text.Remove(txtAceiteCorona.Text.Length - 1);//.TrimEnd(',');
            AceitCorona = Regex.Replace(txtAceiteCorona.Text, @"[,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            //string viajes = "";
            //viajes = txtViajesRestantes.Text;
            DataTable dt = new DataTable();
            dt = clsMantenimientoBL.Instancia.GetActividadxUnidadOperativa(AceitMotor, FiltComb, Boyas, Baterias, CalibMotor, Filtro1, Filtro2, FiltroAire, Intercooler, PuenteDelantero, Alineamiento, Sensores, Alternador, Arrancador, TemplFajaAlternador, TemplFajaVentilador, TemplFajaBombaAgua, Aceitaja_de_Cambios, AceitCajaDirec, MantBocamasa, AceitCorona);
            //var actividad = dt;
            //txtAxUnidadOperativa.Text = actividad.ToString();
            //txtAxUnidadOperativa.Text = dt.Rows[0][0].ToString();
            //txtAxUnidadOperativa.Text = dt.Rows[0]["ActividadxUnidadOperativa"].ToString();
            if (dt.Rows.Count > 0)
            {
                txtAxUnidadOperativa.Text = dt.Rows[0][0].ToString();
            }
        }

        private void txtViajesRestantes_TextChanged(object sender, EventArgs e)
        {
            //Actividades();
        }

        public void ViajesRestantes(double d1)
        {
            double viaje = d1 / 1100;
            var vr = Math.Truncate(viaje);
            txtViajesRestantes.Text = vr.ToString() + " viajes";
        }

        private void txtCambioAceite_TextChanged(object sender, EventArgs e)
        {
            var3 = Convert.ToDouble(txtCambioAceite.Text.ToString());
            ViajesRestantes(var3);
        }

        private void cboTracto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tracto = "";
            tracto = cboTracto.Text;
            DataTable dt1 = new DataTable();
            dt1 = clsMantenimientoBL.Instancia.GetKilometraje(tracto);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                txtKilometraje.Text = dt1.Rows[i]["Odometro"].ToString();
            }
            ListaMarca();
        }

        public void ComboTractos()
        {
            cboTracto.DataSource = null;
            DataTable dt = new DataTable();
            dt = clsMantenimientoBL.Instancia.GetListaTractos();
            if (dt.Rows.Count > 0)
            {
                cboTracto.DataSource = dt;
                cboTracto.ValueMember = "NumeroPlaca";
                cboTracto.DisplayMember = "NumeroPlaca";
            }
        }

        public void ListaMarca()
        {
            string tracto = "";
            tracto = cboTracto.Text;
            DataTable dt = new DataTable();
            dt = clsMantenimientoBL.Instancia.GetMarcaTracto(tracto);
            if (dt.Rows.Count > 0)
            {
                txtMarca.Text = dt.Rows[0]["MARCA"].ToString();
                txtModelo.Text = dt.Rows[0]["MODELO"].ToString();
            }
        }

    }
}
