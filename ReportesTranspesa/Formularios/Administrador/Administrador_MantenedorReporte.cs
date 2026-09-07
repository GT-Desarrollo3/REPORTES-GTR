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
using Entidades;

namespace ReportesTranspesa.Formularios.Administrador
{
    public partial class Administrador_MantenedorReporte : MetroFramework.Forms.MetroForm
    {
        public Administrador_MantenedorReporte()
        {
            InitializeComponent();
        }

        List<clsArea> listaArea = null;
        List<clsFormulario> listaFormulario = null;

        private void buscarReporte()
        {
            List<clsReporte> lista = clsReporteBL.Instancia.consulta_Reporte_Por_Codigo_Nombre(txtBuscarCodigoReporte.Text, txtNombreReporte.Text);
            dgvReporte.AutoGenerateColumns = false;
            //dgvReporte
            dgvReporte.DataSource = lista;
        }

        private void limpiaTextBox()
        {
            txtDescripcionReporte.Text = String.Empty;
            txtNombreReporte.Text = String.Empty;
            txtFormula.Text = string.Empty;
            //foreach (Control ctrl in this.Controls)
            //    if (ctrl is TextBox)
            //        if (ctrl.Enabled)
            //            ((TextBox)ctrl).Text = String.Empty;
        }

        private void setComboBox(ComboBox cbo, List<String> lista)
        {
            cbo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cbo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            AutoCompleteStringCollection listaAutoComplete = new AutoCompleteStringCollection();

            foreach (String cadena in lista)
                listaAutoComplete.Add(cadena);

            cbo.DataSource = listaAutoComplete;
            cbo.AutoCompleteCustomSource = listaAutoComplete;
        }

        private int obtenerIdFormulario(ComboBox cbo)
        {
            if (cbo.DataSource != null)
                foreach (clsFormulario obj in listaFormulario)
                    if (obj.nombre.CompareTo(cbo.Text) == 0)
                        return obj.idFormulario;
            return 0;
        }

        private String obtenerCodigoArea(ComboBox cbo)
        {
            foreach (clsArea objArea in listaArea)
                if (objArea.descripcion.CompareTo(cbo.Text) == 0)
                    return objArea.idArea;
            return null;
        }

        private clsReporte obtenerReporte()
        {
            clsReporte objReporte = new clsReporte();

            objReporte.formula = txtFormula.Text;
            objReporte.idReporte = Int32.Parse(lblCodigoReporte.Text);
            objReporte.codigo = txtCodigoReporte.Text;
            objReporte.nombre = txtNombreReporte.Text;
            objReporte.descripcion = txtDescripcionReporte.Text;

            objReporte.objArea.idArea = obtenerCodigoArea(cbArea);

            objReporte.objFormulario.idFormulario = obtenerIdFormulario(cbFormulario);

            return objReporte;
        }

        private void setComboBoxFormulario()
        {
            List<String> listaCadena = new List<string>();

            listaFormulario = clsFormularioBL.Instancia.consulta_Formularios_NoAsignados();

            if (listaFormulario != null)
            {
                foreach (clsFormulario obj in listaFormulario)
                    listaCadena.Add(obj.nombre);

                setComboBox(cbFormulario, listaCadena);
            }
            else
                cbFormulario.DataSource = null;
        }

        private void setComboBoxArea()
        {
            List<String> listaCadena = new List<string>();
            listaArea = clsAreaBL.Instancia.consulta_Area_Activas();

            listaCadena.Clear();

            foreach (clsArea obj in listaArea)
                listaCadena.Add(obj.descripcion);

            setComboBox(cbArea, listaCadena);
        }

        private void Administrador_MantenedorReporte_Load(object sender, EventArgs e)
        {
            setComboBoxArea();
            setComboBoxFormulario();
            buscarReporte();
        }

        private void btnRegistrarReporte_Click(object sender, EventArgs e)
        {
            clsReporte obj = obtenerReporte();
            MessageBox.Show(clsReporteBL.Instancia.registrar_Reporte(obj));
            txtCodigoReporte.Text = obj.codigo;
            limpiaTextBox();
            setComboBoxFormulario();
            buscarReporte();
        }

        private void btnBuscarReporte_Click(object sender, EventArgs e)
        {
            buscarReporte();
        }

        private void dgvReporte_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int posicionFilaSeleccionada = e.RowIndex;

            clsReporte objeto = (clsReporte)dgvReporte.Rows[posicionFilaSeleccionada].DataBoundItem;

            lblCodigoReporte.Text = objeto.idReporte.ToString();
            txtCodigoReporte.Text = objeto.codigo;
            txtNombreReporte.Text = objeto.nombre;
            txtDescripcionReporte.Text = objeto.descripcion;
            txtFormula.Text = objeto.formula;
            cbArea.Text = objeto.objArea.descripcion;
        }

        private void btnModificarReporte_Click(object sender, EventArgs e)
        {
            MessageBox.Show(clsReporteBL.Instancia.modificar_Reporte(obtenerReporte()));
            limpiaTextBox();
            setComboBoxFormulario();
            buscarReporte();
        }

        private void dgvReporte_Scroll(object sender, ScrollEventArgs e)
        {
            this.dgvReporte.AutoResizeRows(DataGridViewAutoSizeRowsMode.DisplayedCells);
        }
    }
}
