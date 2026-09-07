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
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Administrador
{
    public partial class NuevosReportes : MetroFramework.Forms.MetroForm
    {
        public NuevosReportes()
        {
            InitializeComponent();
        }

        List<clsArea> listaArea = null;
        //List<clsFormulario> listaFormulario = null;

        private void setComboBox(ComboBox cbo, List<String> lista)
        {
            AutoCompleteStringCollection listaAutoComplete = new AutoCompleteStringCollection();

            foreach (String cadena in lista)
                listaAutoComplete.Add(cadena);

            cbo.DataSource = listaAutoComplete;
            cbo.AutoCompleteCustomSource = listaAutoComplete;
        }

        private void setComboBoxArea()
        {
            List<String> listaCadena = new List<string>();
            listaArea = clsAreaBL.Instancia.consulta_Area_Activas();

            listaCadena.Clear();

            foreach (clsArea obj in listaArea)
                listaCadena.Add(obj.descripcion);

            setComboBox(cboArea, listaCadena);
        }

        private void obtenerCodigoArea()
        {
            DataTable dt = new DataTable();
            string Areas = cboArea.Text;
            dt = clsAreaBL.Instancia.AreasPorCodigo(Areas);
            if (dt.Rows.Count > 0)
            {
                cboIdArea.DataSource = dt;
                cboIdArea.ValueMember = "description";
                cboIdArea.DisplayMember = "department";
            }
        }

        private void NuevosReportes_Load(object sender, EventArgs e)
        {
            setComboBoxArea();
            cboEstado.SelectedIndex = 0;
        }

        private void cboArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            obtenerCodigoArea();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string formulario = txtFormulario.Text.Trim();
            string reporte = txtReporte.Text.Trim();
            string areas = cboArea.Text;
            string idarea = cboIdArea.Text;
            switch (cboEstado.SelectedIndex)
            {
                case 0: cboEstado.Text = "1";
                    break;

                case 1: cboEstado.Text = "0";
                    break;
            }
            dtgvReportes.DataSource = null;
            clsReporteBL.Instancia.Registrar_Nuevo_Reporte(formulario, reporte, areas, idarea,txtDescripcionReporte.Text);
            Mensaje m = new Mensaje();
            m.mensaje = "Se Registro Exitosamente";
            m.ShowDialog();
            txtFormulario.Clear();
            txtReporte.Clear();
            buscarReporte();
            clsDetalleReporteUsuarioBL.Instancia.consulta_Reporte_Todos();
        }

        private void txtFormulario_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void txtReporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            buscarReporte();
        }

        private void buscarReporte()
        {
            List<clsReporte> lista = clsReporteBL.Instancia.consulta_Reporte_Por_Codigo_Nombre(txtBuscarCodigoReporte.Text, txtReporte.Text);
            //dgvReporte
            dtgvReportes.DataSource = lista;
            dtgvReportesView.BestFitColumns();
        }

        private void dtgvReportesView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            try
            {
                //int posicionFilaSeleccionada = e.RowHandle;
                DataRow row = dtgvReportesView.GetDataRow(dtgvReportesView.GetSelectedRows()[0]);
                //txtCodigoReporte.Text = row["codigo"].ToString();
                txtReporte.Text = row["nombre"].ToString();
                txtCodigoReporte.Text = row["codigo"].ToString();
                txtDescripcionReporte.Text = row["descripcion"].ToString();
            }
            catch
            {
                //No devuelve valor
            }
        }

        private void txtDescripcionReporte_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

    }
}
