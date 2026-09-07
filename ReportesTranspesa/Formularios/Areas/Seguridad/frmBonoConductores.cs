using ReportesTranspesa.Formularios.Areas.Seguridad.CalificativoConductores;
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
using ReportesTranspesa.Formularios.Areas.RecursosHumanos;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;

namespace ReportesTranspesa.Formularios.Areas.Seguridad
{
    public partial class frmBonoConductores : Form
    {
        int conEdita = 0;
        public string Val_Respuesta = "0";
        public frmBonoConductores()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmCargarExcelGeotab frmExporta = new frmCargarExcelGeotab();
            //frmExporta.ShowDialog();
            frmExporta.ShowDialog(this);
            if (Val_Respuesta.Equals("1"))
            {
                CargarCailificativos();
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarCailificativos();
        }

        private void CargarCailificativos()
        {
            DataTable DTCalificativos = new DataTable();
            DTCalificativos = clsSeguridadBL.Instancia.GetListaCalificativoConductores(dateTimePicker1.Text,comboBox1.Text);
            //string respta = Convert.ToString(DTCalificativos.Rows[0],[])
            if (DTCalificativos.Rows.Count > 0)
            {
                gridControl1.DataSource = DTCalificativos;
                gridView1.Columns["IdConductor"].Visible = false;
                gridView1.Columns["IdOperacion"].Visible = false;
                gridView1.Columns["IDUNIDAD"].Visible = false;
                gridView1.Columns["Conductor"].Width = 250;

                GridFormatRule gridFormatRule = new GridFormatRule();
                FormatConditionRuleIconSet formatConditionRuleIconSet = new FormatConditionRuleIconSet();
                FormatConditionIconSet iconSet = formatConditionRuleIconSet.IconSet = new FormatConditionIconSet();
                FormatConditionIconSetIcon icon1 = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon2 = new FormatConditionIconSetIcon();
                FormatConditionIconSetIcon icon3 = new FormatConditionIconSetIcon();

                //Choose predefined icons.
                icon1.PredefinedName = "TrafficLights3_1.png";
                icon2.PredefinedName = "TrafficLights3_2.png";
                icon3.PredefinedName = "TrafficLights3_3.png";

                //Specify the type of threshold values.
                //iconSet.ValueType = FormatConditionValueType.Percent;
                iconSet.ValueType = FormatConditionValueType.Number;

                //Define ranges to which icons are applied by setting threshold values.
                icon1.Value = 1; // target range: >=75
                icon1.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon2.Value = 2; // NO APLICA
                icon2.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;
                icon3.Value = 3; // target range: >75
                icon3.ValueComparison = FormatConditionComparisonType.GreaterOrEqual;

                //Add icons to the icon set.
                iconSet.Icons.Add(icon1);
                iconSet.Icons.Add(icon2);
                iconSet.Icons.Add(icon3);

                //Specify the rule type.
                gridFormatRule.Rule = formatConditionRuleIconSet;
                //Specify the column to which formatting is applied.
                gridFormatRule.Column = gridView1.Columns["Icono"];
                
                //Add the formatting rule to the GridView.
                gridView1.FormatRules.Add(gridFormatRule);

                gridView1.BestFitColumns();

            }
            else
            {
                gridControl1.DataSource = null;
            }
        }

        private void frmBonoConductores_Load(object sender, EventArgs e)
        {
            gridView1.OptionsBehavior.Editable = false;
            comboBox1.SelectedIndex = 0;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (conEdita == 0)
            {
                gridView1.OptionsBehavior.Editable = true;
                conEdita = 1;
                btnActualizar.Text = "Guardar";
            }
            else 
            {
                gridView1.OptionsBehavior.Editable = false;
                conEdita = 0;
                btnActualizar.Text = "Actualizar";
            }
        }      
    }
}
