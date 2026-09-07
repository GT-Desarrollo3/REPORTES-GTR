using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class frmConstanciaImprimir : MetroFramework.Forms.MetroForm
    {
       
        public frmConstanciaImprimir()
        {
            InitializeComponent();
        }

        private string _Tipodoc;

        public string Tipodoc
        {
            get { return _Tipodoc; }
            set { _Tipodoc = value; }
        }
        private string _Asuntodoc;

        public string Asuntodoc
        {
            get { return _Asuntodoc; }
            set { _Asuntodoc = value; }
        }
        private string _Fechadoc;

        public string Fechadoc
        {
            get { return _Fechadoc; }
            set { _Fechadoc = value; }
        }
        private string _Cuerpodoc;

        public string Cuerpodoc
        {
            get { return _Cuerpodoc; }
            set { _Cuerpodoc = value; }
        }

        private bool _Firma;

        public bool Firma
        {
            get { return _Firma; }
            set { _Firma = value; }
        }

        public delegate void CargarDocEventHandler(Boolean EsCorrecto);
        public event CargarDocEventHandler CargarDoc;
        private void Memos_Compromisos_Load(object sender, EventArgs e)
        {
            txtAsunto.Text = Asuntodoc;
            txtCuerpo.Text = Cuerpodoc;
            Tipodoc = "C";
            Firma = true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtAsunto.Text.Trim() != "" && txtCuerpo.Text.Trim() != "")
            {
                
                    Asuntodoc = txtAsunto.Text;
                    Fechadoc = dtpFecha.Value.ToShortDateString();
                    Cuerpodoc = txtCuerpo.Text;
                    Firma = cboFirma.Checked;
                    CargarDoc(true);
               

                this.Dispose();
            }
        }


    }
}
