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
using DevExpress.Utils;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class MaestroParametrosTractos : MetroFramework.Forms.MetroForm
    {
        public MaestroParametrosTractos()
        {
            InitializeComponent();
        }

        double n1 = 0, n2 = 0;
        #region Mantenimiento_M1
        double acetmotor = 20000;
        double filtCombt = 20000;
        double boyas = 20000;
        double baterias = 20000;
        #endregion

        #region Mantenimiento_M2
        double calibmotor = 60000;
        double filtaire1 = 60000;
        double filtaire2 = 60000;
        double filtSecado = 60000;
        double intercooler = 60000;
        double puentdelat = 60000;
        double aliniamiento = 60000;
        double sensores = 60000;
        double alternador = 60000;
        double arrancador = 60000;
        double fajaAlter = 60000;
        double fajaVent = 60000;
        double fajaBomba = 60000;
        #endregion

        #region Mantenimiento_M3
        double aceitCamb = 120000;
        double aceiteDireccion = 120000;
        double Bocamasa = 120000;
        double aceiteCorona = 120000;
        #endregion

        #region Porciento %
        decimal Porcentaje;
        decimal Porcentaje1;
        decimal Porcentaje2;
        decimal Porcentaje3;
        decimal Porcentaje4;
        decimal Porcentaje5;
        decimal Porcentaje6;
        decimal Porcentaje7;
        decimal Porcentaje8;
        decimal Porcentaje9;
        decimal Porcentaje10;
        decimal Porcentaje11;
        decimal Porcentaje12;
        decimal Porcentaje13;
        decimal Porcentaje14;
        decimal Porcentaje15;
        decimal Porcentaje16;
        decimal Porcentaje17;
        decimal Porcentaje18;
        decimal Porcentaje19;
        decimal Porcentaje20;
        #endregion

        System.Data.DataTable dt = new System.Data.DataTable();
        int tipo = 0;
        int mantenimiento = 0;

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string tracto="";
            tracto = cboTracto.Text;
            string AceitMotor="";
            string FiltComb="";
            string Boyas="";
            string Baterias="";
            string CalibMotor="";
            string Filtro1="";
            string Filtro2="";
            string FiltroAire="";
            string Intercooler="";
            string PuenteDelantero="";
            string Alineamiento="";
            string Sensores="";
            string Alternador="";
            string Arrancador="";
            string TemplFajaAlternador="";
            string TemplFajaVentilador="";
            string TemplFajaBombaAgua="";
            string Aceitaja_de_Cambios="";
            string AceitCajaDirec="";
            string MantBocamasa="";
            string AceitCorona="";
            string KMCambioAceiteMotor="";
            KMCambioAceiteMotor = txtCambioMotor.Text;
            string ViajesRestante="";
            ViajesRestante = txtViajes.Text;
            string ActUnidadOperativa="";
            ActUnidadOperativa = txtActivUnidOperativa.Text;
            string Marca="";
            Marca = txtMarca.Text;
            string Modelo="";
            Modelo = txtModelo.Text;
            //string Porcentaje = "";
            //Porcentaje = Regex.Replace(txtPorcentaje.Text, @"[.,]", "", RegexOptions.None, TimeSpan.FromSeconds(1.5));
            #region Porcentajes
            //Porcentaje
            if (String.IsNullOrEmpty(txtPorcentaje.Text))
            {
                txtPorcentaje.Text = "0.00";
            }
            else
            {
                //if (Porcentaje == 0)
                //{
                //    Porcentaje = 0;
                //}
                //else
                //{
                //    Porcentaje = Math.Round(Convert.ToDecimal(txtPorcentaje.Text), 0);
                //}
                if (txtPorcentaje.Text == "0")
                {
                    txtPorcentaje.Text = "0.00";
                }
                else
                {
                    Porcentaje = Math.Round(Convert.ToDecimal(txtPorcentaje.Text), 0);
                }
                //decimal Porcentaje;
                //Porcentaje = Math.Round(Convert.ToDecimal(txtPorcentaje.Text), 0);
            }

            //Porcentaje1
            if (String.IsNullOrEmpty(txtPorcentaje1.Text))
            {
                txtPorcentaje1.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje1.Text == "0")
                {
                    txtPorcentaje1.Text = "0.00";
                }
                else
                {
                    Porcentaje1 = Math.Round(Convert.ToDecimal(txtPorcentaje1.Text), 0);
                }
                //decimal Porcentaje1;
                //Porcentaje1 = Math.Round(Convert.ToDecimal(txtPorcentaje1.Text), 0);
            }

            //Porcentaje2
            if (String.IsNullOrEmpty(txtPorcentaje2.Text))
            {
                txtPorcentaje2.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje2.Text == "0")
                {
                    txtPorcentaje2.Text = "0.00";
                }
                else
                {
                    Porcentaje2 = Math.Round(Convert.ToDecimal(txtPorcentaje20.Text), 0);
                }
                //decimal Porcentaje2;
                //Porcentaje2 = Math.Round(Convert.ToDecimal(txtPorcentaje2.Text), 0);
            }

            //Porcentaje3
            if (String.IsNullOrEmpty(txtPorcentaje3.Text))
            {
                txtPorcentaje3.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje3.Text == "0")
                {
                    txtPorcentaje3.Text = "0.00";
                }
                else
                {
                    Porcentaje3 = Math.Round(Convert.ToDecimal(txtPorcentaje3.Text), 0);
                }
                //decimal Porcentaje3;
                //Porcentaje3 = Math.Round(Convert.ToDecimal(txtPorcentaje3.Text), 0);
            }

            //Porcentaje4
            if (String.IsNullOrEmpty(txtPorcentaje4.Text))
            {
                txtPorcentaje4.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje4.Text == "0")
                {
                    txtPorcentaje4.Text = "0.00";
                }
                else
                {
                    Porcentaje4 = Math.Round(Convert.ToDecimal(txtPorcentaje4.Text), 0);
                }
                //decimal Porcentaje4;
                //Porcentaje4 = Math.Round(Convert.ToDecimal(txtPorcentaje4.Text), 0);
            }

            //Porcentaje5
            if (String.IsNullOrEmpty(txtPorcentaje5.Text))
            {
                txtPorcentaje5.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje5.Text == "0")
                {
                    txtPorcentaje5.Text = "0.00";
                }
                else
                {
                    Porcentaje5 = Math.Round(Convert.ToDecimal(txtPorcentaje5.Text), 0);
                }
                //decimal Porcentaje5;
                //Porcentaje5 = Math.Round(Convert.ToDecimal(txtPorcentaje5.Text), 0);
            }

            //Porcentaje6
            if (String.IsNullOrEmpty(txtPorcentaje6.Text))
            {
                txtPorcentaje6.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje6.Text == "0")
                {
                    txtPorcentaje6.Text = "0.00";
                }
                else
                {
                    Porcentaje6 = Math.Round(Convert.ToDecimal(txtPorcentaje6.Text), 0);
                }
                //decimal Porcentaje6;
                //Porcentaje6 = Math.Round(Convert.ToDecimal(txtPorcentaje6.Text), 0);
            }

            //Porcentaje7
            if (String.IsNullOrEmpty(txtPorcentaje7.Text))
            {
                txtPorcentaje7.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje7.Text == "0")
                {
                    txtPorcentaje7.Text = "0.00";
                }
                else
                {
                    Porcentaje7 = Math.Round(Convert.ToDecimal(txtPorcentaje7.Text), 0);
                }
                //decimal Porcentaje7;
                //Porcentaje7 = Math.Round(Convert.ToDecimal(txtPorcentaje7.Text), 0);
            }

            //Porcentaje8
            if (String.IsNullOrEmpty(txtPorcentaje8.Text))
            {
                txtPorcentaje8.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje8.Text == "0")
                {
                    txtPorcentaje8.Text = "0.00";
                }
                else
                {
                    Porcentaje8 = Math.Round(Convert.ToDecimal(txtPorcentaje8.Text), 0);
                }
                //decimal Porcentaje8;
                //Porcentaje8 = Math.Round(Convert.ToDecimal(txtPorcentaje8.Text), 0);
            }

            //Porcentaje9
            if (String.IsNullOrEmpty(txtPorcentaje9.Text))
            {
                txtPorcentaje9.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje9.Text == "0")
                {
                    txtPorcentaje9.Text = "0.00";
                }
                else
                {
                    Porcentaje9 = Math.Round(Convert.ToDecimal(txtPorcentaje9.Text), 0);
                }
                //decimal Porcentaje9;
                //Porcentaje9 = Math.Round(Convert.ToDecimal(txtPorcentaje9.Text), 0);
            }

            //Porcentaje10
            if (String.IsNullOrEmpty(txtPorcentaje10.Text))
            {
                txtPorcentaje10.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje10.Text == "0")
                {
                    txtPorcentaje10.Text = "0.00";
                }
                else
                {
                    Porcentaje10 = Math.Round(Convert.ToDecimal(txtPorcentaje10.Text), 0);
                }
                //decimal Porcentaje10;
                //Porcentaje10 = Math.Round(Convert.ToDecimal(txtPorcentaje10.Text), 0);
            }

            //Porcentaje11
            if (String.IsNullOrEmpty(txtPorcentaje11.Text))
            {
                txtPorcentaje11.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje11.Text == "0")
                {
                    txtPorcentaje11.Text = "0.00";
                }
                else
                {
                    Porcentaje11 = Math.Round(Convert.ToDecimal(txtPorcentaje11.Text), 0);
                }
                //decimal Porcentaje11;
                //Porcentaje11 = Math.Round(Convert.ToDecimal(txtPorcentaje11.Text), 0);
            }

            //Porcentaje12
            if (String.IsNullOrEmpty(txtPorcentaje12.Text))
            {
                txtPorcentaje12.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje12.Text == "0")
                {
                    txtPorcentaje12.Text = "0.00";
                }
                else
                {
                    Porcentaje12 = Math.Round(Convert.ToDecimal(txtPorcentaje12.Text), 0);
                }
                //decimal Porcentaje12;
                //Porcentaje12 = Math.Round(Convert.ToDecimal(txtPorcentaje12.Text), 0);
            }

            //Porcentaje13
            if (String.IsNullOrEmpty(txtPorcentaje13.Text))
            {
                txtPorcentaje13.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje13.Text == "0")
                {
                    txtPorcentaje13.Text = "0.00";
                }
                else
                {
                    Porcentaje13 = Math.Round(Convert.ToDecimal(txtPorcentaje13.Text), 0);
                }
                //decimal Porcentaje13;
                //Porcentaje13 = Math.Round(Convert.ToDecimal(txtPorcentaje13.Text), 0);
            }

            //Porcentaje14
            if (String.IsNullOrEmpty(txtPorcentaje14.Text))
            {
                txtPorcentaje14.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje14.Text == "0")
                {
                    txtPorcentaje14.Text = "0.00";
                }
                else
                {
                    Porcentaje14 = Math.Round(Convert.ToDecimal(txtPorcentaje14.Text), 0);
                }
                //decimal Porcentaje14;
                //Porcentaje14 = Math.Round(Convert.ToDecimal(txtPorcentaje14.Text), 0);
            }

            //Porcentaje15
            if (String.IsNullOrEmpty(txtPorcentaje15.Text))
            {
                txtPorcentaje15.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje15.Text == "0")
                {
                    txtPorcentaje15.Text = "0.00";
                }
                else
                {
                    Porcentaje15 = Math.Round(Convert.ToDecimal(txtPorcentaje15.Text), 0);
                }
                //decimal Porcentaje15;
                //Porcentaje15 = Math.Round(Convert.ToDecimal(txtPorcentaje15.Text), 0);
            }

            //Porcentaje16
            if (String.IsNullOrEmpty(txtPorcentaje16.Text))
            {
                txtPorcentaje16.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje16.Text == "0")
                {
                    txtPorcentaje16.Text = "0.00";
                }
                else
                {
                    Porcentaje16 = Math.Round(Convert.ToDecimal(txtPorcentaje16.Text), 0);
                }
                //decimal Porcentaje16;
                //Porcentaje16 = Math.Round(Convert.ToDecimal(txtPorcentaje16.Text), 0);
            }

            //Porcentaje17
            if (String.IsNullOrEmpty(txtPorcentaje17.Text))
            {
                txtPorcentaje17.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje17.Text == "0")
                {
                    txtPorcentaje17.Text = "0.00";
                }
                else
                {
                    Porcentaje17 = Math.Round(Convert.ToDecimal(txtPorcentaje17.Text), 0);
                }
                //decimal Porcentaje17;
                //Porcentaje17 = Math.Round(Convert.ToDecimal(txtPorcentaje17.Text), 0);
            }

            //Porcentaje18
            if (String.IsNullOrEmpty(txtPorcentaje18.Text))
            {
                txtPorcentaje18.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje18.Text == "0")
                {
                    txtPorcentaje18.Text = "0.00";
                }
                else
                {
                    Porcentaje18 = Math.Round(Convert.ToDecimal(txtPorcentaje18.Text), 0);
                }
                //decimal Porcentaje18;
                //Porcentaje18 = Math.Round(Convert.ToDecimal(txtPorcentaje18.Text), 0);
            }

            //Porcentaje19
            if (String.IsNullOrEmpty(txtPorcentaje19.Text))
            {
                txtPorcentaje19.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje19.Text == "0")
                {
                    txtPorcentaje19.Text = "0.00";
                }
                else
                {
                    Porcentaje19 = Math.Round(Convert.ToDecimal(txtPorcentaje19.Text), 0);
                }
                //decimal Porcentaje19;
                //Porcentaje19 = Math.Round(Convert.ToDecimal(txtPorcentaje19.Text), 0);
            }

            //Porcentaje20
            if (String.IsNullOrEmpty(txtPorcentaje20.Text))
            {
                txtPorcentaje20.Text = "0.00";
            }
            else
            {
                if (txtPorcentaje20.Text == "0")
                {
                    txtPorcentaje20.Text = "0.00";
                }
                else
                {
                    Porcentaje20 = Math.Round(Convert.ToDecimal(txtPorcentaje20.Text), 0);
                }
                //decimal Porcentaje20;
                //Porcentaje20 = Math.Round(Convert.ToDecimal(txtPorcentaje20.Text), 0);
            }          
            
            #endregion
            #region Filtros
            if (cboTipo.SelectedIndex == 0)
            {
                #region PlanM1
                switch (cboMantenimiento.SelectedIndex)
                {
                    case 0: AceitMotor = Convert.ToString(Porcentaje);//Porcentaje;//txtPorcentaje.Text;
                        break;

                    case 1: FiltComb = Convert.ToString(Porcentaje1);//Porcentaje;//txtPorcentaje.Text;
                        break;

                    case 2: Boyas = Convert.ToString(Porcentaje2);//Porcentaje;//txtPorcentaje.Text;
                        break;

                    case 3: Baterias = Convert.ToString(Porcentaje3);//Porcentaje;//txtPorcentaje.Text;
                        break;
                }
                #endregion
            }
            else
            {
                if (cboTipo.SelectedIndex == 1)
                {
                    #region PlanM2
                    switch (cboMantenimiento.SelectedIndex)
                    {
                        case 0: CalibMotor = Convert.ToString(Porcentaje4);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 1: Filtro1 = Convert.ToString(Porcentaje5);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 2: Filtro2 = Convert.ToString(Porcentaje6);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 3: FiltroAire = Convert.ToString(Porcentaje7);//Porcentaje;//txtPorcentaje.Text; 
                            break;

                        case 4: Intercooler = Convert.ToString(Porcentaje8);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 5: PuenteDelantero = Convert.ToString(Porcentaje9);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 6: Alineamiento = Convert.ToString(Porcentaje10);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 7: Sensores = Convert.ToString(Porcentaje11);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 8: Alternador = Convert.ToString(Porcentaje12);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 9: Arrancador = Convert.ToString(Porcentaje13);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 10: TemplFajaAlternador = Convert.ToString(Porcentaje14);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 11: TemplFajaVentilador = Convert.ToString(Porcentaje15);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 12: TemplFajaBombaAgua = Convert.ToString(Porcentaje16);//Porcentaje;//txtPorcentaje.Text;
                            break;
                    }
                    #endregion
                }
                else
                {
                    #region PlanM3
                    switch (cboMantenimiento.SelectedIndex)
                    {
                        case 0: Aceitaja_de_Cambios = Convert.ToString(Porcentaje17);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 1: AceitCajaDirec = Convert.ToString(Porcentaje18);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 2: MantBocamasa = Convert.ToString(Porcentaje19);//Porcentaje;//txtPorcentaje.Text;
                            break;

                        case 3: AceitCorona = Convert.ToString(Porcentaje20);//Porcentaje;//txtPorcentaje.Text;
                            break;
                    }
                    #endregion
                }
            }
            #endregion
            DataTable dt = new DataTable();
            dt = clsMantenimientoBL.Instancia.GetInsertMantenimientoTracto(tracto, AceitMotor, FiltComb, Boyas, Baterias, CalibMotor, Filtro1, Filtro2, FiltroAire, Intercooler, PuenteDelantero, Alineamiento, Sensores, Alternador, Arrancador, TemplFajaAlternador, TemplFajaVentilador, TemplFajaBombaAgua, Aceitaja_de_Cambios, AceitCajaDirec, MantBocamasa, AceitCorona, KMCambioAceiteMotor, ViajesRestante, ActUnidadOperativa, Marca, Modelo);
            Mensaje m = new Mensaje();
            m.mensaje = "Se Grabo con Exito";
            m.ShowDialog();
            PLanTractos();
            Lista_Mantenimientos();
            //if (dt.Rows.Count > 0)
            //{
            //    Mensaje m = new Mensaje();
            //    m.mensaje = "Se Grabo con Exito";
            //    m.ShowDialog();
            //}
            //else
            //{
            //    Mensaje m = new Mensaje();
            //    m.mensaje = "No se Guardo Datos";
            //    m.ShowDialog(); 
            //}
        }

        private void cboTracto_SelectedIndexChanged(object sender, EventArgs e)
        {
            MaestroMantenimiento();
            string tracto = "";
            tracto = cboTracto.Text;
            DataTable dt1 = new DataTable();
            dt1 = clsMantenimientoBL.Instancia.GetKilometraje(tracto);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                txtKMActual.Text = dt1.Rows[i]["Odometro"].ToString();
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

        public void PLanTractos()
        {
            dtgvPlanTractos.DataSource = null;
            dtgvViewPlanTractos.Columns.Clear();
            DataTable dt = new DataTable();
            dt = clsMantenimientoBL.Instancia.GetPlanTracto();
            if (dt.Rows.Count > 0)
            {
                dtgvPlanTractos.DataSource = dt;
                dtgvViewPlanTractos.Columns["Aceite_Motor"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Aceite_Motor"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Filtro_Combustible"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Filtro_Combustible"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Limpieza_de_Boyas"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Limpieza_de_Boyas"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Mantenimiento_de_Baterias"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Mantenimiento_de_Baterias"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Calibracion_de_Motor"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Calibracion_de_Motor"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Filtro_de_Aire_1"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Filtro_de_Aire_1"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Filtro_de_Aire_2"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Filtro_de_Aire_2"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Filtro_Secado_de_Aire"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Filtro_Secado_de_Aire"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Intercooler"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Intercooler"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Puente_Delantero"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Puente_Delantero"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Alineamiento"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Alineamiento"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Sensores"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Sensores"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Alternador"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Alternador"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Arrancador"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Arrancador"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Templador_Faja_Alternador"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Templador_Faja_Alternador"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Templador_Faja_Ventilador"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Templador_Faja_Ventilador"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Templador_Faja_Bomba_de_Agua"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Templador_Faja_Bomba_de_Agua"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Aceite_Caja_de_Cambios"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Aceite_Caja_de_Cambios"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Aceite_Caja_de_Direccion"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Aceite_Caja_de_Direccion"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Mantenimiento_Bocamasa_Delantera"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Mantenimiento_Bocamasa_Delantera"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Aceite_Corona"].DisplayFormat.FormatType = FormatType.Numeric;
                dtgvViewPlanTractos.Columns["Aceite_Corona"].DisplayFormat.FormatString = "{0} %";
                dtgvViewPlanTractos.Columns["Placa"].Summary.Add(DevExpress.Data.SummaryItemType.Count, "Placa", "Cantidad={0} Unidades");
                dtgvViewPlanTractos.BestFitColumns();
            }
            var Cantidad = dtgvViewPlanTractos.Columns["Placa"].SummaryItem.SummaryValue;
                txtCantidad.EditValue = Cantidad + " Unidades";
        }

        public void OcultaPorcentaje()
        {
            txtPorcentaje.Visible = true;
            txtPorcentaje1.Visible = false;
            txtPorcentaje2.Visible = false;
            txtPorcentaje3.Visible = false;
            txtPorcentaje4.Visible = false;
            txtPorcentaje5.Visible = false;
            txtPorcentaje6.Visible = false;
            txtPorcentaje7.Visible = false;
            txtPorcentaje8.Visible = false;
            txtPorcentaje9.Visible = false;
            txtPorcentaje10.Visible = false;
            txtPorcentaje11.Visible = false;
            txtPorcentaje12.Visible = false;
            txtPorcentaje13.Visible = false;
            txtPorcentaje14.Visible = false;
            txtPorcentaje15.Visible = false;
            txtPorcentaje16.Visible = false;
            txtPorcentaje17.Visible = false;
            txtPorcentaje18.Visible = false;
            txtPorcentaje19.Visible = false;
            txtPorcentaje20.Visible = false;
        }

        public void Lista_Mantenimientos()
        {
            tipo = cboTipo.SelectedIndex;
            mantenimiento = cboMantenimiento.SelectedIndex;
            string tracto = cboTracto.Text;
            dt = clsMantenimientoBL.Instancia.GetDataMantenimientos(tipo, mantenimiento, tracto);
            if (dt.Rows.Count > 0)
            {
                switch (cboTipo.SelectedIndex)
                {
                    case 0:
                    #region Mantenimiento M1
                        switch (cboMantenimiento.SelectedIndex)
                        {
                            case 0: txtPorcentaje.Text = dt.Rows[0]["Aceite_Motor"].ToString();
                                break;

                            case 1: txtPorcentaje1.Text = dt.Rows[0]["Filtro_Combustible"].ToString();
                                break;

                            case 2: txtPorcentaje2.Text = dt.Rows[0]["Limpieza_de_Boyas"].ToString();
                                break;

                            case 3: txtPorcentaje3.Text = dt.Rows[0]["Mantenimiento_de_Baterias"].ToString();
                                break;
                        }
                    #endregion
                        break;

                    case 1:
                        #region Mantenimiento M2
                        switch (cboMantenimiento.SelectedIndex)
                        {
                            case 0: txtPorcentaje4.Text = dt.Rows[0]["Calibracion_de_Motor"].ToString();
                                break;

                            case 1: txtPorcentaje5.Text = dt.Rows[0]["Filtro_de_Aire_1"].ToString();
                                break;

                            case 2: txtPorcentaje6.Text = dt.Rows[0]["Filtro_de_Aire_2"].ToString();
                                break;

                            case 3: txtPorcentaje7.Text = dt.Rows[0]["Filtro_Secado_de_Aire"].ToString();
                                break;

                            case 4: txtPorcentaje8.Text = dt.Rows[0]["Intercooler"].ToString();
                                break;

                            case 5: txtPorcentaje9.Text = dt.Rows[0]["Puente_Delantero"].ToString();
                                break;

                            case 6: txtPorcentaje10.Text = dt.Rows[0]["Alineamiento"].ToString();
                                break;

                            case 7: txtPorcentaje11.Text = dt.Rows[0]["Sensores"].ToString();
                                break;

                            case 8: txtPorcentaje12.Text = dt.Rows[0]["Alternador"].ToString();
                                break;

                            case 9: txtPorcentaje13.Text = dt.Rows[0]["Arrancador"].ToString();
                                break;

                            case 10: txtPorcentaje14.Text = dt.Rows[0]["Templador_Faja_Alternador"].ToString();
                                break;

                            case 11: txtPorcentaje15.Text = dt.Rows[0]["Templador_Faja_Ventilador"].ToString();
                                break;

                            case 12: txtPorcentaje16.Text = dt.Rows[0]["Templador_Faja_Bomba_de_Agua"].ToString();
                                break;
                        }
                        #endregion
                        break;

                    case 3:
                        #region Mantenimiento M3
                        switch (cboMantenimiento.SelectedIndex)
                        {
                            case 0: txtPorcentaje17.Text = dt.Rows[0]["Aceite_Caja_de_Cambios"].ToString();
                                break;

                            case 1: txtPorcentaje18.Text = dt.Rows[0]["Aceite_Caja_de_Direccion"].ToString();
                                break;

                            case 2: txtPorcentaje19.Text = dt.Rows[0]["Mantenimiento_Bocamasa_Delantera"].ToString();
                                break;

                            case 3: txtPorcentaje20.Text = dt.Rows[0]["Aceite_Corona"].ToString();
                                break;
                        }
                        #endregion
                        break;
                }
                //txtPorcentaje.Text = dt.Rows[0][""].ToString();
            }
        }

        private void MaestroParametrosTractos_Load(object sender, EventArgs e)
        {
            //cboTipo.SelectedIndex = 0;
            cboMantenimiento.SelectedIndex = 0;
            ComboTractos();
            PLanTractos();
            OcultaPorcentaje();
            Lista_Mantenimientos();
        }

        public void MaestroMantenimiento()
        {
            DataTable dt = new DataTable();
            dt = clsMantenimientoBL.Instancia.GetMaestroMantenimiento();
            if (dt.Rows.Count > 0)
            {
                cboTipo.DataSource = dt;
                cboTipo.ValueMember = "Tipo";
                cboTipo.DisplayMember = "Tipo";
            }
        }

        public void PorcentajeTotal(double v1,double v2)
        {
            if (cboTipo.SelectedIndex == 0)
            {
                #region PlanM1
                switch (cboMantenimiento.SelectedIndex)
                {
                    case 0: double Porciento = (v1 - v2) * 100 / acetmotor;
                        txtPorcentaje.Text = Porciento.ToString("N");
                        break;

                    case 1: double Porciento1 = (v1 - v2) * 100 / filtCombt;
                        txtPorcentaje1.Text = Porciento1.ToString("N");
                        break;

                    case 2: double Porciento2 = (v1 - v2) * 100 / boyas;
                        txtPorcentaje2.Text = Porciento2.ToString("N");
                        break;

                    case 3: double Porciento3 = (v1 - v2) * 100 / baterias;
                        txtPorcentaje3.Text = Porciento3.ToString("N");
                        break;
                }
                #endregion
            }
            else
            {
                if (cboTipo.SelectedIndex == 1)
                {
                    #region PlanM2
                    switch (cboMantenimiento.SelectedIndex)
                    {
                        case 0: double Porciento4 = (v1 - v2) * 100 / calibmotor;
                            txtPorcentaje4.Text = Porciento4.ToString("N");
                            break;

                        case 1: double Porciento5 = (v1 - v2) * 100 / filtaire1;
                            txtPorcentaje5.Text = Porciento5.ToString("N");
                            break;

                        case 2: double Porciento6 = (v1 - v2) * 100 / filtaire2;
                            txtPorcentaje6.Text = Porciento6.ToString("N");
                            break;

                        case 3: double Porciento7 = (v1 - v2) * 100 / filtSecado;
                            txtPorcentaje7.Text = Porciento7.ToString("N");
                            break;

                        case 4: double Porciento8 = (v1 - v2) * 100 / intercooler;
                            txtPorcentaje8.Text = Porciento8.ToString("N");
                            break;

                        case 5: double Porciento9 = (v1 - v2) * 100 / puentdelat;
                            txtPorcentaje9.Text = Porciento9.ToString("N");
                            break;

                        case 6: double Porciento10 = (v1 - v2) * 100 / aliniamiento;
                            txtPorcentaje10.Text = Porciento10.ToString("N");
                            break;

                        case 7: double Porciento11 = (v1 - v2) * 100 / sensores;
                            txtPorcentaje11.Text = Porciento11.ToString("N");
                            break;

                        case 8: double Porciento12 = (v1 - v2) * 100 / alternador;
                            txtPorcentaje12.Text = Porciento12.ToString("N");
                            break;

                        case 9: double Porciento13 = (v1 - v2) * 100 / arrancador;
                            txtPorcentaje13.Text = Porciento13.ToString("N");
                            break;

                        case 10: double Porciento14 = (v1 - v2) * 100 / fajaAlter;
                            txtPorcentaje14.Text = Porciento14.ToString("N");
                            break;

                        case 11: double Porciento15 = (v1 - v2) * 100 / fajaVent;
                            txtPorcentaje15.Text = Porciento15.ToString("N");
                            break;

                        case 12: double Porciento16 = (v1 - v2) * 100 / fajaBomba;
                            txtPorcentaje16.Text = Porciento16.ToString("N");
                            break;
                    }
                    #endregion
                }
                else
                {
                    #region PlanM3
                    switch (cboMantenimiento.SelectedIndex)
                    {
                        case 0: double Porciento17 = (v1 - v2) * 100 / aceitCamb;
                            txtPorcentaje17.Text = Porciento17.ToString("N");
                            break;

                        case 1: double Porciento18 = (v1 - v2) * 100 / aceiteDireccion;
                            txtPorcentaje18.Text = Porciento18.ToString("N");
                            break;

                        case 2: double Porciento19 = (v1 - v2) * 100 / Bocamasa;
                            txtPorcentaje19.Text = Porciento19.ToString("N");
                            break;

                        case 3: double Porciento20 = (v1 - v2) * 100 / aceiteCorona;
                            txtPorcentaje20.Text = Porciento20.ToString("N");
                            break;
                    }
                    #endregion
                }
            }
            #region ListaMantenimiento
            //switch (cboMantenimiento.SelectedIndex)
            //{
            //    case 0: double Porciento = (v1 - v2) * 100 / acetmotor;
            //            txtPorcentaje.Text = Porciento.ToString("N");
            //            break;

            //    case 1: double Porciento1 = (v1 - v2) * 100 / filtCombt;
            //            txtPorcentaje.Text = Porciento1.ToString("N");
            //            break;

            //    case 2: double Porciento2 = (v1 - v2) * 100 / boyas;
            //            txtPorcentaje.Text = Porciento2.ToString("N");
            //            break;

            //    case 3: double Porciento3 = (v1 - v2) * 100 / baterias;
            //            txtPorcentaje.Text = Porciento3.ToString("N");
            //            break;

            //    case 4: double Porciento4 = (v1 - v2) * 100 / calibmotor;
            //            txtPorcentaje.Text = Porciento4.ToString("N");
            //            break;

            //    case 5: double Porciento5 = (v1 - v2) * 100 / filtaire1;
            //            txtPorcentaje.Text = Porciento5.ToString("N");
            //            break;

            //    case 6: double Porciento6 = (v1 - v2) * 100 / filtaire2;
            //            txtPorcentaje.Text = Porciento6.ToString("N");
            //            break;

            //    case 7: double Porciento7 = (v1 - v2) * 100 / filtSecado;
            //            txtPorcentaje.Text = Porciento7.ToString("N");
            //            break;

            //    case 8: double Porciento8 = (v1 - v2) * 100 / intercooler;
            //            txtPorcentaje.Text = Porciento8.ToString("N");
            //            break;

            //    case 9: double Porciento9 = (v1 - v2) * 100 / puentdelat;
            //            txtPorcentaje.Text = Porciento9.ToString("N");
            //            break;

            //    case 10: double Porciento10 = (v1 - v2) * 100 / aliniamiento;
            //            txtPorcentaje.Text = Porciento10.ToString("N");
            //            break;

            //    case 11: double Porciento11 = (v1 - v2) * 100 / sensores;
            //            txtPorcentaje.Text = Porciento11.ToString("N");
            //            break;

            //    case 12: double Porciento12 = (v1 - v2) * 100 / alternador;
            //            txtPorcentaje.Text = Porciento12.ToString("N");
            //            break;

            //    case 13: double Porciento13 = (v1 - v2) * 100 / arrancador;
            //            txtPorcentaje.Text = Porciento13.ToString("N");
            //            break;

            //    case 14: double Porciento14 = (v1 - v2) * 100 / fajaAlter;
            //            txtPorcentaje.Text = Porciento14.ToString("N");
            //            break;

            //    case 15: double Porciento15 = (v1 - v2) * 100 / fajaVent;
            //            txtPorcentaje.Text = Porciento15.ToString("N");
            //            break;

            //    case 16: double Porciento16 = (v1 - v2) * 100 / fajaBomba;
            //            txtPorcentaje.Text = Porciento16.ToString("N");
            //            break;

            //    case 17: double Porciento17 = (v1 - v2) * 100 / aceitCamb;
            //            txtPorcentaje.Text = Porciento17.ToString("N");
            //            break;

            //    case 18: double Porciento18 = (v1 - v2) * 100 / aceiteDireccion;
            //            txtPorcentaje.Text = Porciento18.ToString("N");
            //            break;

            //    case 19: double Porciento19 = (v1 - v2) * 100 / Bocamasa;
            //            txtPorcentaje.Text = Porciento19.ToString("N");
            //            break;

            //    case 20: double Porciento20 = (v1 - v2) * 100 / aceiteCorona;
            //            txtPorcentaje.Text = Porciento20.ToString("N");
            //            break;
            //}
            #endregion
            //double Porciento = (v1 - v2) * 100 / acetmotor;
            //txtPorcentaje.Text = Porciento.ToString("N");
        }

        private void txtKilometraje_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtKilometraje.Text))
            {
                txtKilometraje.Text = "";
            }
            else
            {
                n2 = Convert.ToDouble(txtKilometraje.Text.ToString());
                PorcentajeTotal(n1, n2);
            }
            //n2 = Convert.ToDouble(txtKilometraje.Text.ToString());
            //Porcentaje(n1, n2);
        }

        private void txtKMActual_TextChanged(object sender, EventArgs e)
        {
            n1 = Convert.ToDouble(txtKMActual.Text.ToString());
            PorcentajeTotal(n1, n2);
        }

        private void cboTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //cboMantenimiento.Items.Clear();
            cboMantenimiento.DataSource = null;
            if (cboTipo.SelectedIndex == 0)
            {
                #region Mantenimiento M1
                DataTable dt = new DataTable();
                dt = clsMantenimientoBL.Instancia.GetMantenimientoM1();
                cboMantenimiento.DataSource = dt;
                cboMantenimiento.DisplayMember = "TipoMantenimiento";
                //cboMantenimiento.Items.Insert(0, "Aceite Motor");
                //cboMantenimiento.Items.Insert(1, "Filtro de Combustible");
                //cboMantenimiento.Items.Insert(2, "Limpieza de Boyas");
                //cboMantenimiento.Items.Insert(3, "Mantenimiento de Baterias");
                cboMantenimiento.SelectedIndex = 0;
                #endregion
            }
            else
            {
                if (cboTipo.SelectedIndex == 1)
                {
                    #region Mantenimiento M2
                    DataTable dt = new DataTable();
                    dt = clsMantenimientoBL.Instancia.GetMantenimientoM2();
                    cboMantenimiento.DataSource = dt;
                    cboMantenimiento.DisplayMember = "TipoMantenimiento";
                    //cboMantenimiento.Items.Insert(0, "Calibracion de Motor");
                    //cboMantenimiento.Items.Insert(1, "Filtro de Aire 1°");
                    //cboMantenimiento.Items.Insert(2, "Filtro de Aire 2°");
                    //cboMantenimiento.Items.Insert(3, "Filtro Secado de Aire");
                    //cboMantenimiento.Items.Insert(4, "Intercooler");
                    //cboMantenimiento.Items.Insert(5, "Puente Delantero");
                    //cboMantenimiento.Items.Insert(6, "Alineamiento");
                    //cboMantenimiento.Items.Insert(7, "Sensores");
                    //cboMantenimiento.Items.Insert(8, "Alternador");
                    //cboMantenimiento.Items.Insert(9, "Arrancador");
                    //cboMantenimiento.Items.Insert(10, "Templador Faja Alternador");
                    //cboMantenimiento.Items.Insert(11, "Templador Faja Ventilador");
                    //cboMantenimiento.Items.Insert(12, "Templador Faja Bomba de Agua");
                    cboMantenimiento.SelectedIndex = 0;
                    #endregion
                }
                else
                {
                    #region Mantenimiento M3
                    DataTable dt = new DataTable();
                    dt = clsMantenimientoBL.Instancia.GetMantenimientoM3();
                    cboMantenimiento.DataSource = dt;
                    cboMantenimiento.DisplayMember = "TipoMantenimiento";
                    //cboMantenimiento.Items.Insert(0, "Aceite Caja de Cambios");
                    //cboMantenimiento.Items.Insert(1, "Filtro de Combustible");
                    //cboMantenimiento.Items.Insert(2, "Mantenimiento Bocamasa Delantera");
                    //cboMantenimiento.Items.Insert(3, "Aceite Corona");
                    cboMantenimiento.SelectedIndex = 0;
                    #endregion
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgvPlanTractos.DataSource == null)
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
                string nombre = System.IO.Path.Combine(desktop, "Plan de Mantenimiento de Tractos " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgvPlanTractos.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void cboMantenimiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipo.SelectedIndex == 0)
            {
                switch (cboMantenimiento.SelectedIndex)
                {
                    case 0:
                        #region Porcentaje
                        txtPorcentaje.Visible = true;
                        txtPorcentaje1.Visible = false;
                        txtPorcentaje2.Visible = false;
                        txtPorcentaje3.Visible = false;
                        txtPorcentaje4.Visible = false;
                        txtPorcentaje5.Visible = false;
                        txtPorcentaje6.Visible = false;
                        txtPorcentaje7.Visible = false;
                        txtPorcentaje8.Visible = false;
                        txtPorcentaje9.Visible = false;
                        txtPorcentaje10.Visible = false;
                        txtPorcentaje11.Visible = false;
                        txtPorcentaje12.Visible = false;
                        txtPorcentaje13.Visible = false;
                        txtPorcentaje14.Visible = false;
                        txtPorcentaje15.Visible = false;
                        txtPorcentaje16.Visible = false;
                        txtPorcentaje17.Visible = false;
                        txtPorcentaje18.Visible = false;
                        txtPorcentaje19.Visible = false;
                        txtPorcentaje20.Visible = false;
                        #endregion
                        break;

                    case 1:
                        #region Porcentaje1
                        txtPorcentaje.Visible = false;
                        txtPorcentaje1.Visible = true;
                        txtPorcentaje2.Visible = false;
                        txtPorcentaje3.Visible = false;
                        txtPorcentaje4.Visible = false;
                        txtPorcentaje5.Visible = false;
                        txtPorcentaje6.Visible = false;
                        txtPorcentaje7.Visible = false;
                        txtPorcentaje8.Visible = false;
                        txtPorcentaje9.Visible = false;
                        txtPorcentaje10.Visible = false;
                        txtPorcentaje11.Visible = false;
                        txtPorcentaje12.Visible = false;
                        txtPorcentaje13.Visible = false;
                        txtPorcentaje14.Visible = false;
                        txtPorcentaje15.Visible = false;
                        txtPorcentaje16.Visible = false;
                        txtPorcentaje17.Visible = false;
                        txtPorcentaje18.Visible = false;
                        txtPorcentaje19.Visible = false;
                        txtPorcentaje20.Visible = false;
                        #endregion
                        break;

                    case 2:
                        #region Porcentaje2
                        txtPorcentaje.Visible = false;
                        txtPorcentaje1.Visible = false;
                        txtPorcentaje2.Visible = true;
                        txtPorcentaje3.Visible = false;
                        txtPorcentaje4.Visible = false;
                        txtPorcentaje5.Visible = false;
                        txtPorcentaje6.Visible = false;
                        txtPorcentaje7.Visible = false;
                        txtPorcentaje8.Visible = false;
                        txtPorcentaje9.Visible = false;
                        txtPorcentaje10.Visible = false;
                        txtPorcentaje11.Visible = false;
                        txtPorcentaje12.Visible = false;
                        txtPorcentaje13.Visible = false;
                        txtPorcentaje14.Visible = false;
                        txtPorcentaje15.Visible = false;
                        txtPorcentaje16.Visible = false;
                        txtPorcentaje17.Visible = false;
                        txtPorcentaje18.Visible = false;
                        txtPorcentaje19.Visible = false;
                        txtPorcentaje20.Visible = false;
                        #endregion
                        break;

                    case 3:
                        #region Porcentaje3
                        txtPorcentaje.Visible = false;
                        txtPorcentaje1.Visible = false;
                        txtPorcentaje2.Visible = false;
                        txtPorcentaje3.Visible = true;
                        txtPorcentaje4.Visible = false;
                        txtPorcentaje5.Visible = false;
                        txtPorcentaje6.Visible = false;
                        txtPorcentaje7.Visible = false;
                        txtPorcentaje8.Visible = false;
                        txtPorcentaje9.Visible = false;
                        txtPorcentaje10.Visible = false;
                        txtPorcentaje11.Visible = false;
                        txtPorcentaje12.Visible = false;
                        txtPorcentaje13.Visible = false;
                        txtPorcentaje14.Visible = false;
                        txtPorcentaje15.Visible = false;
                        txtPorcentaje16.Visible = false;
                        txtPorcentaje17.Visible = false;
                        txtPorcentaje18.Visible = false;
                        txtPorcentaje19.Visible = false;
                        txtPorcentaje20.Visible = false;
                        #endregion
                        break;
                }
            }
            else
            {
                if (cboTipo.SelectedIndex == 1)
                {
                    switch (cboMantenimiento.SelectedIndex)
                    {
                        case 0:
                            #region Porcentaje4
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = true;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 1:
                            #region Porcentaje5
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = true;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 2:
                            #region Porcentaje6
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = true;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = true;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 3:
                            #region Porcentaje7
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = true;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 4:
                            #region Porcentaje8
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = true;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 5:
                            #region Porcentaje9
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = true;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 6:
                            #region Porcentaje10
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = true;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 7:
                            #region Porcentaje11
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = true;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 8:
                            #region Porcentaje12
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = true;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 9:
                            #region Porcentaje13
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = true;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 10:
                            #region Porcentaje14
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = true;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 11:
                            #region Porcentaje15
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = true;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 12:
                            #region Porcentaje16
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = true;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;
                    }
                }
                else
                {
                    switch (cboMantenimiento.SelectedIndex)
                    {
                        case 0:
                            #region Porcentaje17
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = true;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 1:
                            #region Porcentaje18
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = true;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 2:
                            #region Porcentaje19
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = true;
                            txtPorcentaje20.Visible = false;
                            #endregion
                            break;

                        case 3:
                            #region Porcentaje20
                            txtPorcentaje.Visible = false;
                            txtPorcentaje1.Visible = false;
                            txtPorcentaje2.Visible = false;
                            txtPorcentaje3.Visible = false;
                            txtPorcentaje4.Visible = false;
                            txtPorcentaje5.Visible = false;
                            txtPorcentaje6.Visible = false;
                            txtPorcentaje7.Visible = false;
                            txtPorcentaje8.Visible = false;
                            txtPorcentaje9.Visible = false;
                            txtPorcentaje10.Visible = false;
                            txtPorcentaje11.Visible = false;
                            txtPorcentaje12.Visible = false;
                            txtPorcentaje13.Visible = false;
                            txtPorcentaje14.Visible = false;
                            txtPorcentaje15.Visible = false;
                            txtPorcentaje16.Visible = false;
                            txtPorcentaje17.Visible = false;
                            txtPorcentaje18.Visible = false;
                            txtPorcentaje19.Visible = false;
                            txtPorcentaje20.Visible = true;
                            #endregion
                            break;
                    }
                }
            }
            
        }

        private void dtgvPlanTractos_DoubleClick(object sender, EventArgs e)
        {
            Mantenimiento_Tractos frm = new Mantenimiento_Tractos();
            DataRow row = dtgvViewPlanTractos.GetDataRow(dtgvViewPlanTractos.GetSelectedRows()[0]);
            frm.cboTracto.Items.Add(row["Placa"].ToString());
            frm.txtMarca.Text = row["Marca"].ToString();
            frm.txtModelo.Text = row["Modelo"].ToString();
            frm.txtKilometraje.Text = txtKMActual.Text;
            frm.Show();
        }

    }
}



