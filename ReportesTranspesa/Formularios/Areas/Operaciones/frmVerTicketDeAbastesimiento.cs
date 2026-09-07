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
using System.Globalization;
using Negocio;
namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    public partial class frmVerTicketDeAbastesimiento : Form
    {
        string _codigo, _Sucursal, _placa, _conductor;
        string fecha, hora;
        DateTime MyDateTime;
        public frmVerTicketDeAbastesimiento()
        {
            InitializeComponent();
        }

        private void frmVerTicketDeAbastesimiento_Load(object sender, EventArgs e)
        {
            DataTable dtlistarticket = new DataTable();
            dtlistarticket = clsOperacionesBL.Instancia.GetPreviajes_ListarTickets(_codigo);
            if (dtlistarticket.Rows.Count > 0)
            {
                for (int i = 0; i < dtlistarticket.Rows.Count; i++)
                {

                    fecha = dtlistarticket.Rows[i]["Fecha"].ToString();
                    hora = dtlistarticket.Rows[i]["Hora"].ToString();
                    txtOdometro.Text = dtlistarticket.Rows[i]["Odrometro"].ToString();
                    txtCantidad.Text = dtlistarticket.Rows[i]["Cantidad"].ToString();
                    txtDispensador.Text = dtlistarticket.Rows[i]["Dispensador"].ToString();
                    txtPistola.Text = dtlistarticket.Rows[i]["Pistola"].ToString();
                    txtTicket.Text = dtlistarticket.Rows[i]["Ticket"].ToString();
                    txtTotalizador.Text = dtlistarticket.Rows[i]["Totalizador"].ToString();
                    txtProducto.Text = dtlistarticket.Rows[i]["Producto"].ToString();
                    txtAbastecedor.Text = dtlistarticket.Rows[i]["Abastesedor"].ToString();
                }                
            }

            //DateTime to String
            MyDateTime = Convert.ToDateTime(hora);           
            hora = MyDateTime.ToString(" HH:mm:ss tt");
            txtFecha.Text = fecha.Substring(0,11);
            txtHora.Text = hora; 
            TxtCodigoPV.Text = _codigo;          
            txtSucursal.Text = _Sucursal;
            txtPlaca.Text = _placa;
            txtConductor.Text = _conductor;    
           }
       
        public void Enviardatostickets(string codigo, string sucursal, string placa,
                                       string conductor )
        {
            _codigo = codigo;
            _Sucursal = sucursal;           
            _placa = placa;
            _conductor = conductor;            
        }
    }
}
