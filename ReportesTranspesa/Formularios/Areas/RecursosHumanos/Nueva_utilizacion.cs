using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Negocio;
using System.Windows.Forms;
using ReportesTranspesa.Sistema;

namespace ReportesTranspesa.Formularios.Areas.RecursosHumanos
{
    public partial class Nueva_utilizacion : MetroFramework.Forms.MetroForm
    {
        public Nueva_utilizacion()
        {
            InitializeComponent();
        }
        public event EventHandler RegistroGuardado;
        private void Nueva_utilizacion_Load(object sender, EventArgs e)
        {
            cboTipo.SelectedIndex = 0;
        }

        public int empleado;
        public string usuario;
        public int periodo;

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool resultado;
            resultado = clsRecursosHumanosBL.Instancia.InsertUtilizacion(periodo, empleado, dtpFechaIni.Value.ToShortDateString() + " 00:00:00",
            dtpFechaFin.Value.ToShortDateString() + " 23:59:59",cboTipo.Text,usuario,chkMedioDia.Checked);
            if (resultado != true)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "Error al guardar";
                m.ShowDialog();
            }
            else
            {
                this.Close();
                if (RegistroGuardado != null)
                    RegistroGuardado(this, e);
            }
        }

        private void dtpFechaIni_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaIni.Value.ToShortDateString() == dtpFechaFin.Value.ToShortDateString())
            {
                chkMedioDia.Visible = true;
                chkMedioDia.Checked = false;
            }
            else
            {
                chkMedioDia.Visible = false;
                chkMedioDia.Checked = false;
            }
        }

        private void dtpFechaFin_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFechaFin.Value.ToShortDateString() == dtpFechaIni.Value.ToShortDateString())
            {
                chkMedioDia.Visible = true;
                chkMedioDia.Checked = false;
            }
            else
            {
                chkMedioDia.Visible = false;
                chkMedioDia.Checked = false;
            }
        }
    }
}
