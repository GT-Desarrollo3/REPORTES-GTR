using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Formularios.Areas.Mantenimiento
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string fechainicio = "01/01/1980";
            string fechafin = "31/12/2030";
            fechainicio = dtpFechaIni.Value.ToShortDateString() + " 00:00:00";
            fechafin = dtpFechaFin.Value.ToShortDateString() + " 23:59:59";
            DataTable dt = new DataTable();
            string connstring = "Data Source= 192.168.4.234;initial catalog=spring;Persist Security Info=True; User ID=sa; Password=s!stema5";
            SqlConnection conexion = new SqlConnection(connstring);
            conexion.Open();
            SqlCommand comando;
            comando = new SqlCommand("ReportesApp_Mantenimiendo_Reporte_Consumo", conexion);
            comando.Parameters.AddWithValue("@FECHA_INI", fechainicio);
            comando.Parameters.AddWithValue("@FECHA_FIN", fechafin);
            //comando.Parameters.Add(new SqlParameter("@FECHA_INI", fechainicio));
            //comando.Parameters.Add(new SqlParameter("@FECHA_FIN", fechafin));
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandTimeout = 900000;
            //comando.ExecuteNonQuery();
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                gridControl1.DataSource = dt;
                gridView1.BestFitColumns();
            }
            else
            {
                MessageBox.Show("No hay Data");
            }
        }
    }
}
