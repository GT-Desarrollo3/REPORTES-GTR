using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using DevExpress.Export.Xl;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Finanzas.ControlCotizacion
{
    public partial class frmCuadroCostos : Form
    {
        public int idCotizacionC, idRuta;

        public frmCuadroCostos()
        {
            InitializeComponent();
        }

        private void frmCuadroCostos_Load(object sender, EventArgs e)
        {

        }


        private void txtTarifa_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
                else { e.Handled = false; }

                //if (e.KeyChar == Convert.ToChar(Keys.Enter)) { txtRendCargado.Focus(); } 
            }
            catch { }
        }

        private void txtRendCargado_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.')) { e.Handled = true; }
                else { e.Handled = false; }

                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    if (txtRendCargado.Text.Length != 0)
                    {
                        if ((Convert.ToDecimal(txtRendCargado.Text) >= Convert.ToDecimal(txtMin.Text)) && (Convert.ToDecimal(txtRendCargado.Text) <= Convert.ToDecimal(txtMax.Text)))
                        {
                            txtRendimiento.Text = txtRendCargado.Text;
                            txtGalones.Text = Math.Round((Convert.ToDecimal(txtKilometraje.Text) / Convert.ToDecimal(txtRendCargado.Text)), 2).ToString();
                            txtTotalCombustible.Text = Convert.ToString(Convert.ToDecimal(txtRendCargado.Text) + Convert.ToDecimal(txtReefer.Text) + Convert.ToDecimal(txtRendimiento.Text)
                                                       + Convert.ToDecimal(txtGalones.Text) + Convert.ToDecimal(txtCosto.Text));
                            txtReefer.Focus();
                        }
                        else
                        {
                            MessageBox.Show("El rendimiento tiene que estar entre el valor máximo y mínimo permitidos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtRendCargado.Text = "0.00";
                            txtRendimiento.Text = "0.00";
                            txtGalones.Text = "0.00";

                            txtTotalCombustible.Text = Convert.ToString(Convert.ToDecimal(txtRendCargado.Text) + Convert.ToDecimal(txtReefer.Text) + Convert.ToDecimal(txtRendimiento.Text)
                                                       + Convert.ToDecimal(txtGalones.Text) + Convert.ToDecimal(txtCosto.Text));
                        }
                    }
                }

                if (e.KeyChar == Convert.ToChar(Keys.Back))
                {
                    txtRendimiento.Text = "0.00";
                    txtGalones.Text = "0.00";

                    txtTotalCombustible.Text = Convert.ToString(Convert.ToDecimal(txtRendCargado.Text) + Convert.ToDecimal(txtReefer.Text) + Convert.ToDecimal(txtRendimiento.Text)
                                                       + Convert.ToDecimal(txtGalones.Text) + Convert.ToDecimal(txtCosto.Text));
                }
            }
            catch { }
        }

        private void txtReefer_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
                { e.Handled = true; }
                else { e.Handled = false; }

                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    txtTotalCombustible.Text = Convert.ToString(Convert.ToDecimal(txtRendCargado.Text) + Convert.ToDecimal(txtReefer.Text) + Convert.ToDecimal(txtRendimiento.Text)
                                               + Convert.ToDecimal(txtGalones.Text) + Convert.ToDecimal(txtCosto.Text));
                    txtPorc.Focus();
                }
            }
            catch { }
        }

        private void txtPorc_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != Convert.ToChar('.'))
                { e.Handled = true; }
                else { e.Handled = false; }

                if (e.KeyChar == Convert.ToChar(Keys.Enter))
                {
                    txtCosto.Text = Math.Round((Convert.ToDecimal(txtPrecioOriginal.Text) + (Convert.ToDecimal(txtPorc.Text) * Convert.ToDecimal(txtPrecioOriginal.Text) / 100)), 2).ToString();
                    txtTotalCombustible.Text = Convert.ToString(Convert.ToDecimal(txtRendCargado.Text) + Convert.ToDecimal(txtReefer.Text) + Convert.ToDecimal(txtRendimiento.Text)
                                               + Convert.ToDecimal(txtGalones.Text) + Convert.ToDecimal(txtCosto.Text));
                    txtPorc.Focus();
                }

                if (e.KeyChar == Convert.ToChar(Keys.Back))
                {
                    txtCosto.Text = Math.Round(Convert.ToDecimal(txtPrecioOriginal.Text), 2).ToString();

                    txtTotalCombustible.Text = Convert.ToString(Convert.ToDecimal(txtRendCargado.Text) + Convert.ToDecimal(txtReefer.Text) + Convert.ToDecimal(txtRendimiento.Text)
                                                       + Convert.ToDecimal(txtGalones.Text) + Convert.ToDecimal(txtCosto.Text));
                }
            }
            catch { }
        }
    }
}
