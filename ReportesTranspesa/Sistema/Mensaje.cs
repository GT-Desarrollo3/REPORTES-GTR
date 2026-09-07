using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Sistema
{
    public partial class Mensaje : MetroFramework.Forms.MetroForm
    {
        public string mensaje = "";
        public Mensaje()
        {
            InitializeComponent();
        }

        private void Mensaje_Load(object sender, EventArgs e)
        {
            lblMensaje.Text = mensaje;
            btnOK.Left = this.Width / 2 - 38;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
