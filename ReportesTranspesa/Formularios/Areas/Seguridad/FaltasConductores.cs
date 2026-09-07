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


namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class FaltasConductores : MetroFramework.Forms.MetroForm
    {
        public FaltasConductores()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string idcondutor = "";
            idcondutor = txtIdConductor.Text;
            string tipo = "";
            tipo = cboTipo.Text;
            string Descripcion = "";
            Descripcion = txtDescripcion.Text;
            string fecha = "";
            //fecha = dtpFechaRegistro.Text;
            fecha = dtpFechaRegistro.Value.ToString();
            clsRecursosHumanosBL.Instancia.GetFaltaConductor(idcondutor, fecha, tipo, Descripcion);
            dt = clsRecursosHumanosBL.Instancia.GetMuestraFaltaConductores();
            if(dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                //dtgvDataView.Columns["Hora"].DisplayFormat.FormatString = "HH:MM";
                dtgvDataView.BestFitColumns();
            }
            Mensaje m = new Mensaje();
            m.mensaje = "Se Registro Exitosamente";
            m.ShowDialog();
            txtDescripcion.Clear();
        }

        public void MuestaDatos()
        {
            DataTable dt = new DataTable();
            dt = clsRecursosHumanosBL.Instancia.GetMuestraFaltaConductores();
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                //dtgvDataView.Columns["Hora"].DisplayFormat.FormatString = "HH:MM";
                dtgvDataView.BestFitColumns();
            }
        }

        private void FaltasConductores_Load(object sender, EventArgs e)
        {
            MuestaDatos();
            cboTipo.SelectedIndex = 0;
            DataTable dt = new DataTable();
            dt = clsRecursosHumanosBL.Instancia.GetListCondutoresTextbox();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                txtConductor.Text = dt.Rows[i]["Nombre"].ToString();
                txtConductor.AutoCompleteCustomSource.Add(txtConductor.Text);
            }
        }

        public void MuestraID()
        {
            string nombre = "";
            nombre = txtConductor.Text;
            DataTable dt1 = new DataTable();
            dt1 = clsRecursosHumanosBL.Instancia.GetIDCondutoresTextbox(nombre);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                txtIdConductor.Text = dt1.Rows[i]["IdConductor"].ToString();
            }
        }

        private void txtConductor_TextChanged(object sender, EventArgs e)
        {
            txtConductor.CharacterCasing = CharacterCasing.Upper;
            MuestraID();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (dtgvData.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data a imprimir";
                m.ShowDialog();
            }
            else
            {
                dtgvData.ShowPrintPreview();
            }
        }
    }
}

