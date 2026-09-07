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
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.ProgramacionViajes
{
    
    
    public partial class frmTicket : Form
    {
        string _codigo, _sucursal, _tipoprogramacion, _fechaprogramacion, _placa, _ruta, _cliente, _conductor, _producto, _programador, _dni, _EsConsolidado;
        string NombreImpresora, _observacion;
        public frmTicket()
        {
            InitializeComponent();
        }

        private void frmTicket_Load(object sender, EventArgs e)
        {
            DataTable dtConsultarImpresora = new DataTable();
            dtConsultarImpresora = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ListarImpresora(Utilitario.Instancia.SesionUsuario.usuario);
            NombreImpresora = Convert.ToString(dtConsultarImpresora.Rows[0]["impresora"]);                      

            lblTicket.Text = _codigo;
            txtNroCopias.Text = "1";
        }
        
        public void Enviardatosticket( string codigo, string sucursal, string tipoprogramacion,string fechaprogramacion,string placa,string ruta, string cliente,
                                       string conductor, string producto, string programador, string dni, string EsConsolidado, string observacion)
        {
            _codigo = codigo;
            _sucursal = sucursal;
            _tipoprogramacion = tipoprogramacion;
            _fechaprogramacion = fechaprogramacion;
            _placa = placa;
            _ruta = ruta;
            _cliente = cliente;
            _conductor = conductor;
            _producto = producto;
            _programador = programador;
            _dni = dni;
            _EsConsolidado = EsConsolidado;
            _observacion = observacion;
        }
     
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dtImpreso = new DataTable();
            dtImpreso = clsOperacionesBL.Instancia.GetOperaciones_ListarPreviajeImpreso(_codigo);

            if (dtImpreso.Rows.Count > 0)
            {
                string rspta = Convert.ToString(dtImpreso.Rows[0]["IMPRESO"]);

                if (rspta == "1")
                {
                    MessageBox.Show("La programación ya fue impresa, Para reimprimir comunicarse con su Supervisor");
                    return;                   
                }                
               
            }

            if (_codigo.Equals(""))
            {
                MessageBox.Show("No existe codigo para la impresión");
                return;
            }
            if (_tipoprogramacion.Equals(""))
            {
                MessageBox.Show("Falta Tipo de Programación  para la impresión");
                return;
            }
            if (_fechaprogramacion.Equals(""))
            {
                MessageBox.Show("No existe fecha para la impresión");
                return;
            }
            if (_dni.Equals(""))
            {
                MessageBox.Show("No existe DNI para la impresión");
                return;
            }
            if (_conductor.Equals(""))
            {
                MessageBox.Show("No existe conductor para la impresión");
                return;
            }
            if (_programador.Equals(""))
            {
                MessageBox.Show("No existe programador para la impresión");
                return;
            }
            if (NombreImpresora.Equals("") || NombreImpresora.Equals("NO ASIGNADO"))
            {
                MessageBox.Show("No tiene Impresora asignada");
                return;
            }
            

            /*//Creamos una instancia d ela clase CrearTicket
            CrearTicket ticket = new CrearTicket();
            //Ya podemos usar todos sus metodos
           // ticket.AbreCajon();//Para abrir el cajon de dinero.

            //De aqui en adelante pueden formar su ticket a su gusto... Les muestro un ejemplo
                       

            //Datos de la cabecera del Ticket.
            ticket.TextoCentro("GRUPO TRANSPESA SAC.");
            ticket.TextoIzquierda("SEDE: " + _sucursal);
            ticket.TextoIzquierda("DIREC: FUNDO LARREA");
            //ticket.TextoIzquierda("TELEF: 4530000");
            //ticket.TextoIzquierda("R.F.C: XXXXXXXXX-XX");
            //ticket.TextoIzquierda("EMAIL: cmcmarce14@gmail.com");//Es el mio por si me quieren contactar ...
            ticket.TextoIzquierda("");
            ticket.TextoCentro("TICKET PREVIAJES");
            ticket.TextoExtremos("CODIGO", _codigo);
            ticket.lineasAsteriscos();

            //Sub cabecera.
            ticket.TextoIzquierda("");
            ticket.TextoIzquierda("FECHA PROGRAMACION: "+_fechaprogramacion);
            ticket.TextoIzquierda("PLACA: "+_placa);
            ticket.TextoIzquierda("RUTA: "+_ruta);
            ticket.TextoIzquierda("CLIENTE: "+_cliente);
            ticket.TextoIzquierda("CONDUCTOR: "+_conductor);
            ticket.TextoIzquierda("DNI: "+_dni);
            ticket.TextoIzquierda("PROGRAMADOR: "+_programador);
            ticket.TextoExtremos("FECHA: " + DateTime.Now.ToShortDateString(), "HORA: " + DateTime.Now.ToShortTimeString());
            ticket.lineasAsteriscos();

            //Articulos a vender.
           // ticket.EncabezadoVenta();//NOMBRE DEL ARTICULO, CANT, PRECIO, IMPORTE
            ticket.lineasAsteriscos();
            //Si tiene una DataGridView donde estan sus articulos a vender pueden usar esta manera para agregarlos al ticket.
            //foreach (DataGridViewRow fila in dgvLista.Rows)//dgvLista es el nombre del datagridview
            //{
            //ticket.AgregaArticulo(fila.Cells[2].Value.ToString(), int.Parse(fila.Cells[5].Value.ToString()),
            //decimal.Parse(fila.Cells[4].Value.ToString()), decimal.Parse(fila.Cells[6].Value.ToString()));
            //}
        //    ticket.AgregaArticulo("Combustible", 2, 20, 40);
           // ticket.AgregaArticulo("Articulo B", 1, 10, 10);
            //ticket./("Este es un nombre largo del articulo, para mostrar como se bajan las lineas", 1, 30, 30);
           // ticket.lineasIgual();

            //Resumen de la venta. Sólo son ejemplos
           // ticket.AgregarTotales("         SUBTOTAL......$", 100);
           // ticket.AgregarTotales("         IVA...........$", 10.04M);//La M indica que es un decimal en C#
           // ticket.AgregarTotales("         TOTAL.........$", 200);
          //  ticket.TextoIzquierda("");
          //  ticket.AgregarTotales("         EFECTIVO......$", 200);
          //  ticket.AgregarTotales("         CAMBIO........$", 0);

            //Texto final del Ticket.
            //ticket.TextoIzquierda("");
            //ticket.TextoIzquierda(": 3");
           // ticket.TextoIzquierda("");
            ticket.TextoCentro("¡VIAJA CON CUIDADO!");
            ticket.CortaTicket();
            ticket.ImprimirTicket("POS-80-Series (1)");//Nombre de la impresora ticketera*/
            int nroCopias = 0;
            nroCopias= Convert.ToInt32(txtNroCopias.Text);

            if (nroCopias == 2)
            {
                //Imprimir();
                //Imprimir();
            }

            else if (nroCopias == 1)
            {
                Imprimir();
              
            }
            else  
            {
               
                MessageBox.Show("Maximo 2 copias");
            }
       
        }

        private void Imprimir()
        {

            Ticket ticket = new Ticket();
            //ticket.HeaderImage = picturebox1.Image;
            ticket.AddHeaderLine("GRUPO TRANSPESA S.A.C.");
            ticket.AddSubHeaderLine2("CODIGO: " + _codigo + "                         ");
            ticket.AddSubHeaderLine("SEDE:" + _sucursal + "  PLACA: " + _placa);
            ticket.AddSubHeaderLine("FECHAPROG: " + _fechaprogramacion);
            ticket.AddSubHeaderLine("DNI: " + _dni + "                         ");
            ticket.AddSubHeaderLine("CHOFER: " + _conductor);
            if (_ruta.Equals("") && !_tipoprogramacion.Equals("COMBUSTIBLE"))
            {
                ticket.AddSubHeaderLine("RUTA: FULLEO COMBUSTIBLE                  ");
            }
            else if (_ruta.Equals("") && _tipoprogramacion.Equals("COMBUSTIBLE"))
            {
                ticket.AddSubHeaderLine("OBSERV: "+_observacion);
            }
            else
            {
                ticket.AddSubHeaderLine("RUTA: " + _ruta);
            }
            ticket.AddSubHeaderLine("PROGRAMADOR: " + _programador+"                   ");
            ticket.AddSubHeaderLine("PROGRAMACION: " + _tipoprogramacion);
            //ticket.AddItem(cantidad, "  " + producto, cantidad);
            ticket.AddSubHeaderLine("Fecha Ticket:" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            // ticket.AddFooterLine(pistola + " : " + totalizador);
            // ticket.AddFooterLine("");
            // ticket.AddFooterLine(Frase01);
            ticket.AddFooterLine("    ** VIAJA CON CUIDADO **");
            ticket.PrintTicket(NombreImpresora);
            //ticket.PrintTicket();

            /***ACTUALIZAR EL CAMPO IMPRESO EN LA TABLA PREVIAJES******/
            string RPTA;
            DataTable dtImpreso = new DataTable();
            dtImpreso = clsOperacionesBL.Instancia.GetOperaciones_Previajes_ActualizarImpresion(_codigo);

            RPTA = Convert.ToString(dtImpreso.Rows[0]["exito"]);
            string val = RPTA.Substring(0, 1);
            if (val == "0")
            {
                MessageBox.Show("Ticket Impreso....!");
                this.Close(); 
            }

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

           /* if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Return))
            {

            }*/
        }

    }
}
