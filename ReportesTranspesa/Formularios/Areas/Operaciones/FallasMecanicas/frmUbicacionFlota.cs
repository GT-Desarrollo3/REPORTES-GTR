using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.Utils;
using ReportesTranspesa.Sistema;
using Negocio;
using Comun;

namespace ReportesTranspesa.Formularios.Areas.Operaciones.FallasMecanicas
{
    public partial class frmUbicacionFlota : Form
    {
        public DataTable dtListaTalleres = new DataTable();
        public int xClick = 0, yClick = 0;
        public int idSolicitud = -1;

        public frmUbicacionFlota()
        {
            InitializeComponent();
            cbxBase.SelectedIndexChanged -= cbxBase_SelectedIndexChanged;
        }

        private void cbxBase_SelectedIndexChanged(object sender, EventArgs e) { CargarComboBase(2); }

        private void frmUbicacionFlota_Load(object sender, EventArgs e)
        {
            ListarTalleres();
            CargarComboBase(2);
        }


        public void CargarComboBase(int Opcion)
        {
            DataTable dtBase = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarBases(Opcion);
            cbxBase.DataSource = dtBase;
            cbxBase.DisplayMember = "DescripcionBase";
            cbxBase.ValueMember = "idBase";
        }

        public void ListarTalleres()
        {
            dtListaTalleres = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarUnidadTaller(txtPlaca.Text);
            dtgListaTaller.DataSource = dtListaTalleres;
            if (dtListaTalleres.Rows.Count > 0)
            {
                dgvListaTallerView.Columns["idSolicitud"].Visible = false;
                dgvListaTallerView.BestFitColumns();

                btn1A.Text = ""; btn1B.Text = ""; btn1C.Text = ""; btn2A.Text = ""; btn2B.Text = ""; btn2C.Text = ""; btn3A.Text = ""; btn3B.Text = ""; btn3C.Text = "";
                btn4A.Text = ""; btn4B.Text = ""; btn4C.Text = ""; btn5A.Text = ""; btn5B.Text = ""; btn5C.Text = ""; btn6A.Text = ""; btn6B.Text = ""; btn6C.Text = "";
                btn7A.Text = ""; btn7B.Text = ""; btn7C.Text = ""; btn8A.Text = ""; btn8B.Text = ""; btn8C.Text = ""; btn9A.Text = ""; btn9B.Text = ""; btn9C.Text = "";
                btn10A.Text = ""; btn10B.Text = ""; btn10C.Text = ""; btn11A.Text = ""; btn11B.Text = ""; btn11C.Text = ""; btn12A.Text = ""; btn12B.Text = ""; btn12C.Text = "";
                btn13A.Text = ""; btn13B.Text = ""; btn13C.Text = ""; btn14A.Text = ""; btn14B.Text = ""; btn14C.Text = ""; btn15A.Text = ""; btn15B.Text = ""; btn15C.Text = "";
                btnBRA1.Text = ""; btnBRA2.Text = ""; btnBRA3.Text = ""; btnBRA4.Text = ""; btnBRA5.Text = ""; btnBRA6.Text = "";
                btnLIMA1.Text = ""; btnLIMA2.Text = ""; btnLIMA3.Text = ""; btnLIMA4.Text = "";

                btn1A.Visible = false; btn1B.Visible = false; btn1C.Visible = false; btn2A.Visible = false; btn2B.Visible = false; btn2C.Visible = false; btn3A.Visible = false; btn3B.Visible = false; btn3C.Visible = false;
                btn4A.Visible = false; btn4B.Visible = false; btn4C.Visible = false; btn5A.Visible = false; btn5B.Visible = false; btn5C.Visible = false; btn6A.Visible = false; btn6B.Visible = false; btn6C.Visible = false;
                btn7A.Visible = false; btn7B.Visible = false; btn7C.Visible = false; btn8A.Visible = false; btn8B.Visible = false; btn8C.Visible = false; btn9A.Visible = false; btn9B.Visible = false; btn9C.Visible = false;
                btn10A.Visible = false; btn10B.Visible = false; btn10C.Visible = false; btn11A.Visible = false; btn11B.Visible = false; btn11C.Visible = false; btn12A.Visible = false; btn12B.Visible = false; btn12C.Visible = false;
                btn13A.Visible = false; btn13B.Visible = false; btn13C.Visible = false; btn14A.Visible = false; btn14B.Visible = false; btn14C.Visible = false; btn15A.Visible = false; btn15B.Visible = false; btn15C.Visible = false;
                btnBRA1.Visible = false; btnBRA2.Visible = false; btnBRA3.Visible = false; btnBRA4.Visible = false; btnBRA5.Visible = false; btnBRA6.Visible = false;
                btnLIMA1.Visible = false; btnLIMA2.Visible = false; btnLIMA3.Visible = false; btnLIMA4.Visible = false;

                btn1A.ForeColor = Color.Black; btn1B.ForeColor = Color.Black; btn1C.ForeColor = Color.Black; btn2A.ForeColor = Color.Black; btn2B.ForeColor = Color.Black; btn2C.ForeColor = Color.Black; btn3A.ForeColor = Color.Black; btn3B.ForeColor = Color.Black; btn3C.ForeColor = Color.Black;
                btn4A.ForeColor = Color.Black; btn4B.ForeColor = Color.Black; btn4C.ForeColor = Color.Black; btn5A.ForeColor = Color.Black; btn5B.ForeColor = Color.Black; btn5C.ForeColor = Color.Black; btn6A.ForeColor = Color.Black; btn6B.ForeColor = Color.Black; btn6C.ForeColor = Color.Black;
                btn7A.ForeColor = Color.Black; btn7B.ForeColor = Color.Black; btn7C.ForeColor = Color.Black; btn8A.ForeColor = Color.Black; btn8B.ForeColor = Color.Black; btn8C.ForeColor = Color.Black; btn9A.ForeColor = Color.Black; btn9B.ForeColor = Color.Black; btn9C.ForeColor = Color.Black;
                btn10A.ForeColor = Color.Black; btn10B.ForeColor = Color.Black; btn10C.ForeColor = Color.Black; btn11A.ForeColor = Color.Black; btn11B.ForeColor = Color.Black; btn11C.ForeColor = Color.Black; btn12A.ForeColor = Color.Black; btn12B.ForeColor = Color.Black; btn12C.ForeColor = Color.Black;
                btn13A.ForeColor = Color.Black; btn13B.ForeColor = Color.Black; btn13C.ForeColor = Color.Black; btn14A.ForeColor = Color.Black; btn14B.ForeColor = Color.Black; btn14C.ForeColor = Color.Black; btn15A.ForeColor = Color.Black; btn15B.ForeColor = Color.Black; btn15C.ForeColor = Color.Black;
                btnBRA1.ForeColor = Color.Black; btnBRA2.ForeColor = Color.Black; btnBRA3.ForeColor = Color.Black; btnBRA4.ForeColor = Color.Black; btnBRA5.ForeColor = Color.Black; btnBRA6.ForeColor = Color.Black;
                btnLIMA1.ForeColor = Color.Black; btnLIMA2.ForeColor = Color.Black; btnLIMA3.ForeColor = Color.Black; btnLIMA4.ForeColor = Color.Black;

                for (int i = 1; i <= dtListaTalleres.Rows.Count; i++)
                {
                    string Taller = dtListaTalleres.Rows[i- 1]["TALLER"].ToString();
                    string Placa = dtListaTalleres.Rows[i - 1]["PLACA"].ToString();
                    string Tipo = dtListaTalleres.Rows[i - 1]["TIPO"].ToString();

                    switch (Taller)
                    {
                        case "MTTO-1A": btn1A.Text = Placa; if (Tipo == "TRACTO") btn1A.ForeColor = Color.Red; else btn1A.ForeColor = Color.Blue; btn1A.Visible = true; break;
                        case "MTTO-1B": btn1B.Text = Placa; if (Tipo == "TRACTO") btn1B.ForeColor = Color.Red; else btn1B.ForeColor = Color.Blue; btn1B.Visible = true; break;
                        case "MTTO-1C": btn1C.Text = Placa; if (Tipo == "TRACTO") btn1C.ForeColor = Color.Red; else btn1C.ForeColor = Color.Blue; btn1C.Visible = true; break;
                        case "MTTO-2A": btn2A.Text = Placa; if (Tipo == "TRACTO") btn2A.ForeColor = Color.Red; else btn2A.ForeColor = Color.Blue; btn2A.Visible = true; break;
                        case "MTTO-2B": btn2B.Text = Placa; if (Tipo == "TRACTO") btn2B.ForeColor = Color.Red; else btn2B.ForeColor = Color.Blue; btn2B.Visible = true; break;
                        case "MTTO-2C": btn2C.Text = Placa; if (Tipo == "TRACTO") btn2C.ForeColor = Color.Red; else btn2C.ForeColor = Color.Blue; btn2C.Visible = true; break;
                        case "MTTO-3A": btn3A.Text = Placa; if (Tipo == "TRACTO") btn3A.ForeColor = Color.Red; else btn3A.ForeColor = Color.Blue; btn3A.Visible = true; break;
                        case "MTTO-3B": btn3B.Text = Placa; if (Tipo == "TRACTO") btn3B.ForeColor = Color.Red; else btn3B.ForeColor = Color.Blue; btn3B.Visible = true; break;
                        case "MTTO-3C": btn3C.Text = Placa; if (Tipo == "TRACTO") btn3C.ForeColor = Color.Red; else btn3C.ForeColor = Color.Blue; btn3C.Visible = true; break;
                        case "MTTO-4A": btn4A.Text = Placa; if (Tipo == "TRACTO") btn4A.ForeColor = Color.Red; else btn4A.ForeColor = Color.Blue; btn4A.Visible = true; break;
                        case "MTTO-4B": btn4B.Text = Placa; if (Tipo == "TRACTO") btn4B.ForeColor = Color.Red; else btn4B.ForeColor = Color.Blue; btn4B.Visible = true; break;
                        case "MTTO-4C": btn4C.Text = Placa; if (Tipo == "TRACTO") btn4C.ForeColor = Color.Red; else btn4C.ForeColor = Color.Blue; btn4C.Visible = true; break;
                        case "MTTO-5A": btn5A.Text = Placa; if (Tipo == "TRACTO") btn5A.ForeColor = Color.Red; else btn5A.ForeColor = Color.Blue; btn5A.Visible = true; break;
                        case "MTTO-5B": btn5B.Text = Placa; if (Tipo == "TRACTO") btn5B.ForeColor = Color.Red; else btn5B.ForeColor = Color.Blue; btn5B.Visible = true; break;
                        case "MTTO-5C": btn5C.Text = Placa; if (Tipo == "TRACTO") btn5C.ForeColor = Color.Red; else btn5C.ForeColor = Color.Blue; btn5C.Visible = true; break;
                        case "MTTO-6A": btn6A.Text = Placa; if (Tipo == "TRACTO") btn6A.ForeColor = Color.Red; else btn6A.ForeColor = Color.Blue; btn6A.Visible = true; break;
                        case "MTTO-6B": btn6B.Text = Placa; if (Tipo == "TRACTO") btn6B.ForeColor = Color.Red; else btn6B.ForeColor = Color.Blue; btn6B.Visible = true; break;
                        case "MTTO-6C": btn6C.Text = Placa; if (Tipo == "TRACTO") btn6C.ForeColor = Color.Red; else btn6C.ForeColor = Color.Blue; btn6C.Visible = true; break;
                        case "MTTO-7A": btn7A.Text = Placa; if (Tipo == "TRACTO") btn7A.ForeColor = Color.Red; else btn7A.ForeColor = Color.Blue; btn7A.Visible = true; break;
                        case "MTTO-7B": btn7B.Text = Placa; if (Tipo == "TRACTO") btn7B.ForeColor = Color.Red; else btn7B.ForeColor = Color.Blue; btn7B.Visible = true; break;
                        case "MTTO-7C": btn7C.Text = Placa; if (Tipo == "TRACTO") btn7C.ForeColor = Color.Red; else btn7C.ForeColor = Color.Blue; btn7C.Visible = true; break;
                        case "MTTO-8A": btn8A.Text = Placa; if (Tipo == "TRACTO") btn8A.ForeColor = Color.Red; else btn8A.ForeColor = Color.Blue; btn8A.Visible = true; break;
                        case "MTTO-8B": btn8B.Text = Placa; if (Tipo == "TRACTO") btn8B.ForeColor = Color.Red; else btn8B.ForeColor = Color.Blue; btn8B.Visible = true; break;
                        case "MTTO-8C": btn8C.Text = Placa; if (Tipo == "TRACTO") btn8C.ForeColor = Color.Red; else btn8C.ForeColor = Color.Blue; btn8C.Visible = true; break;
                        case "MTTO-9A": btn9A.Text = Placa; if (Tipo == "TRACTO") btn9A.ForeColor = Color.Red; else btn9A.ForeColor = Color.Blue; btn9A.Visible = true; break;
                        case "MTTO-9B": btn9B.Text = Placa; if (Tipo == "TRACTO") btn9B.ForeColor = Color.Red; else btn9B.ForeColor = Color.Blue; btn9B.Visible = true; break;
                        case "MTTO-9C": btn9C.Text = Placa; if (Tipo == "TRACTO") btn9C.ForeColor = Color.Red; else btn9C.ForeColor = Color.Blue; btn9C.Visible = true; break;
                        case "MTTO-10A": btn10A.Text = Placa; if (Tipo == "TRACTO") btn10A.ForeColor = Color.Red; else btn10A.ForeColor = Color.Blue; btn10A.Visible = true; break;
                        case "MTTO-10B": btn10B.Text = Placa; if (Tipo == "TRACTO") btn10B.ForeColor = Color.Red; else btn10B.ForeColor = Color.Blue; btn10B.Visible = true; break;
                        case "MTTO-10C": btn10C.Text = Placa; if (Tipo == "TRACTO") btn10C.ForeColor = Color.Red; else btn10C.ForeColor = Color.Blue; btn10C.Visible = true; break;
                        case "MTTO-11A": btn11A.Text = Placa; if (Tipo == "TRACTO") btn11A.ForeColor = Color.Red; else btn11A.ForeColor = Color.Blue; btn11A.Visible = true; break;
                        case "MTTO-11B": btn11B.Text = Placa; if (Tipo == "TRACTO") btn11B.ForeColor = Color.Red; else btn11B.ForeColor = Color.Blue; btn11B.Visible = true; break;
                        case "MTTO-11C": btn11C.Text = Placa; if (Tipo == "TRACTO") btn11C.ForeColor = Color.Red; else btn11C.ForeColor = Color.Blue; btn11C.Visible = true; break;
                        case "MTTO-12A": btn12A.Text = Placa; if (Tipo == "TRACTO") btn12A.ForeColor = Color.Red; else btn12A.ForeColor = Color.Blue; btn12A.Visible = true; break;
                        case "MTTO-12B": btn12B.Text = Placa; if (Tipo == "TRACTO") btn12B.ForeColor = Color.Red; else btn12B.ForeColor = Color.Blue; btn12B.Visible = true; break;
                        case "MTTO-12C": btn12C.Text = Placa; if (Tipo == "TRACTO") btn12C.ForeColor = Color.Red; else btn12C.ForeColor = Color.Blue; btn12C.Visible = true; break;
                        case "MTTO-13A": btn13A.Text = Placa; if (Tipo == "TRACTO") btn13A.ForeColor = Color.Red; else btn13A.ForeColor = Color.Blue; btn13A.Visible = true; break;
                        case "MTTO-13B": btn13B.Text = Placa; if (Tipo == "TRACTO") btn13B.ForeColor = Color.Red; else btn13B.ForeColor = Color.Blue; btn13B.Visible = true; break;
                        case "MTTO-13C": btn13C.Text = Placa; if (Tipo == "TRACTO") btn13C.ForeColor = Color.Red; else btn13C.ForeColor = Color.Blue; btn13C.Visible = true; break;
                        case "MTTO-14A": btn14A.Text = Placa; if (Tipo == "TRACTO") btn14A.ForeColor = Color.Red; else btn14A.ForeColor = Color.Blue; btn14A.Visible = true; break;
                        case "MTTO-14B": btn14B.Text = Placa; if (Tipo == "TRACTO") btn14B.ForeColor = Color.Red; else btn14B.ForeColor = Color.Blue; btn14B.Visible = true; break;
                        case "MTTO-14C": btn14C.Text = Placa; if (Tipo == "TRACTO") btn14C.ForeColor = Color.Red; else btn14C.ForeColor = Color.Blue; btn14C.Visible = true; break;
                        case "MTTO-15A": btn15A.Text = Placa; if (Tipo == "TRACTO") btn15A.ForeColor = Color.Red; else btn15A.ForeColor = Color.Blue; btn15A.Visible = true; break;
                        case "MTTO-15B": btn15B.Text = Placa; if (Tipo == "TRACTO") btn15B.ForeColor = Color.Red; else btn15B.ForeColor = Color.Blue; btn15B.Visible = true; break;
                        case "MTTO-15C": btn15C.Text = Placa; if (Tipo == "TRACTO") btn15C.ForeColor = Color.Red; else btn15C.ForeColor = Color.Blue; btn15C.Visible = true; break;

                        case "BRA-1": btnBRA1.Text = Placa; if (Tipo == "TRACTO") btnBRA1.ForeColor = Color.Red; else btnBRA1.ForeColor = Color.Blue; btnBRA1.Visible = true; break;
                        case "BRA-2": btnBRA2.Text = Placa; if (Tipo == "TRACTO") btnBRA2.ForeColor = Color.Red; else btnBRA2.ForeColor = Color.Blue; btnBRA2.Visible = true; break;
                        case "BRA-3": btnBRA3.Text = Placa; if (Tipo == "TRACTO") btnBRA3.ForeColor = Color.Red; else btnBRA3.ForeColor = Color.Blue; btnBRA3.Visible = true; break;
                        case "BRA-4": btnBRA4.Text = Placa; if (Tipo == "TRACTO") btnBRA4.ForeColor = Color.Red; else btnBRA4.ForeColor = Color.Blue; btnBRA4.Visible = true; break;
                        case "BRA-5": btnBRA5.Text = Placa; if (Tipo == "TRACTO") btnBRA5.ForeColor = Color.Red; else btnBRA5.ForeColor = Color.Blue; btnBRA5.Visible = true; break;
                        case "BRA-6": btnBRA6.Text = Placa; if (Tipo == "TRACTO") btnBRA6.ForeColor = Color.Red; else btnBRA6.ForeColor = Color.Blue; btnBRA6.Visible = true; break;

                        case "LIMA-1": btnLIMA1.Text = Placa; if (Tipo == "TRACTO") btnLIMA1.ForeColor = Color.Red; else btnLIMA1.ForeColor = Color.Blue; btnLIMA1.Visible = true; break;
                        case "LIMA-2": btnLIMA2.Text = Placa; if (Tipo == "TRACTO") btnLIMA2.ForeColor = Color.Red; else btnLIMA2.ForeColor = Color.Blue; btnLIMA2.Visible = true; break;
                        case "LIMA-3": btnLIMA3.Text = Placa; if (Tipo == "TRACTO") btnLIMA3.ForeColor = Color.Red; else btnLIMA3.ForeColor = Color.Blue; btnLIMA3.Visible = true; break;
                        case "LIMA-4": btnLIMA4.Text = Placa; if (Tipo == "TRACTO") btnLIMA4.ForeColor = Color.Red; else btnLIMA4.ForeColor = Color.Blue; btnLIMA4.Visible = true; break;
                    }
                }
            }
        }


        private void txtPlaca_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) { ListarTalleres(); }
        }

        private void btnBuscar_Click(object sender, EventArgs e) { ListarTalleres(); }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (dtgListaTaller.DataSource == null)
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay datos para exportar.";
                m.ShowDialog();
            }
            else
            {
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                dtfi.TimeSeparator = ".";
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                string nombre = System.IO.Path.Combine(desktop, "LISTA DE UNIDADES EN TALLER - " + Utilitario.Instancia.SesionUsuario.usuario + " " + DateTime.Now.ToString("T", dtfi) + ".xlsx");
                dtgListaTaller.ExportToXlsx(nombre);
                Process.Start(nombre);
            }
        }

        private void tsCambiarTaller_Click(object sender, EventArgs e)
        {
            pCambiarUbicacion.Location = new System.Drawing.Point(501, 190);
            lblPlaca.Text = Convert.ToString(dgvListaTallerView.GetRowCellValue(dgvListaTallerView.FocusedRowHandle, "PLACA"));
            lblTaller.Text = Convert.ToString(dgvListaTallerView.GetRowCellValue(dgvListaTallerView.FocusedRowHandle, "TALLER"));
            idSolicitud = Convert.ToInt32(dgvListaTallerView.GetRowCellValue(dgvListaTallerView.FocusedRowHandle, "idSolicitud"));
            pCambiarUbicacion.Visible = true;
            pCambiarUbicacion.BringToFront();
            cbxBase.SelectedValue = 1;
            cbxBase_DropDownClosed(sender, e);
        }

        private void pCambiarUbicacion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { xClick = e.X; yClick = e.Y; }
            else
            {
                pCambiarUbicacion.Left = pCambiarUbicacion.Left + (e.X - xClick);
                pCambiarUbicacion.Top = pCambiarUbicacion.Top + (e.Y - yClick);
            }
        }

        private void btnCerrar2_Click(object sender, EventArgs e)
        {
            pCambiarUbicacion.Visible = false;
            pCambiarUbicacion.SendToBack();
            txtUbicacionTaller.Clear();
            lblPlaca.Text = "";
            lblTaller.Text = "";
            idSolicitud = -1;
        }

        private void cbxBase_DropDownClosed(object sender, EventArgs e)
        {
            if (cbxBase.Text == "GT TRU (MTTO)" || cbxBase.Text == "GT TRU (BRA)" || cbxBase.Text == "GT LIMA (MTTO)")
            {
                label20.Visible = true;
                txtUbicacionTaller.Visible = true;
                txtUbicacionTaller.Clear();
            }
            else
            {
                label20.Visible = false;
                txtUbicacionTaller.Visible = false;
                txtUbicacionTaller.Clear();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e) { ListarTalleres(); }

        private void btnImagen_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Png Image (.png)|*.png|JPG Image (.jpg)|*.jpg|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (var bmp = new Bitmap(panel4.Width, panel4.Height))
                {
                    pbImagen.BringToFront();
                    panel4.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    pbImagen.SendToBack();
                }
            }
        }

        private void btnActualizar2_Click(object sender, EventArgs e) { ListarTalleres(); }

        private void btnImagen2_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Png Image (.png)|*.png|JPG Image (.jpg)|*.jpg|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (var bmp = new Bitmap(panel5.Width, panel5.Height))
                {
                    pbImagen2.BringToFront();
                    panel5.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    pbImagen2.SendToBack();
                }
            }
        }

        private void btnActualizar3_Click(object sender, EventArgs e) { ListarTalleres(); }

        private void btnImagen3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Png Image (.png)|*.png|JPG Image (.jpg)|*.jpg|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (var bmp = new Bitmap(panel6.Width, panel6.Height))
                {
                    pbImagen3.BringToFront();
                    panel6.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));
                    bmp.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    pbImagen3.SendToBack();
                }
            }
        }

        private void txtUbicacionTaller_Enter(object sender, EventArgs e) { txtUbicacionTaller.BackColor = Color.FromArgb(192, 255, 192); }

        private void txtUbicacionTaller_KeyPress(object sender, KeyPressEventArgs e)
        {
            clsVisuales.Instancia.LlenarLw(lstTaller, clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ListarTalleres(txtUbicacionTaller.Text), true, false, false);
            lstTaller.Columns[0].Width = 0;
            lstTaller.Columns[1].Width = 90;
            lstTaller.BringToFront();
            lstTaller.Visible = true;

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTaller.Visible = false;
                lstTaller.SendToBack();
            }
        }

        private void txtUbicacionTaller_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down) { lstTaller.Focus(); }
        }

        private void txtUbicacionTaller_Leave(object sender, EventArgs e) { txtUbicacionTaller.BackColor = Color.White; }

        private void lstTaller_Enter(object sender, EventArgs e)
        {
            if (!lstTaller.Items.Count.Equals(0)) { lstTaller.Items[0].Selected = true; } 
        }

        private void lstTaller_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == (char)Keys.Enter) && !lstTaller.Items.Count.Equals(0))
            {
                ListViewItem ItemActual;
                ItemActual = lstTaller.SelectedItems[0];
                txtUbicacionTaller.Text = ItemActual.SubItems[1].Text;

                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres(txtUbicacionTaller.Text);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0") { btnGuardar.Focus(); }
                else
                {
                    MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUbicacionTaller.Clear();
                    txtUbicacionTaller.Focus();
                }

                lstTaller.Visible = false;
                lstTaller.SendToBack();
            }

            if (e.KeyChar == (char)Keys.Back)
            {
                lstTaller.Visible = false;
                lstTaller.SendToBack();
            }
        }

        private void lstTaller_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem ItemActual;
            ItemActual = lstTaller.SelectedItems[0];
            txtUbicacionTaller.Text = ItemActual.SubItems[1].Text;

            DataTable dtRespuesta = new DataTable();
            string Respuesta;
            dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_BuscarTalleres(txtUbicacionTaller.Text);
            Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
            string NroRPTA = Respuesta.Substring(0, 1);
            if (NroRPTA == "0") { btnGuardar.Focus(); }
            else
            {
                MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUbicacionTaller.Clear();
                txtUbicacionTaller.Focus();
            }

            lstTaller.Visible = false;
            lstTaller.SendToBack();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if ((Convert.ToInt32(cbxBase.SelectedValue) == 1 || Convert.ToInt32(cbxBase.SelectedValue) == 8) && txtUbicacionTaller.Text.Length == 0)
            {
                MessageBox.Show("Por favor, ingrese la ubicación del taller.", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUbicacionTaller.Focus();
            }
            else
            {
                DataTable dtRespuesta = new DataTable();
                string Respuesta;
                string Usuario = Utilitario.Instancia.SesionUsuario.usuario;
                dtRespuesta = clsMantenimientoBL.Instancia.ReportesApp_Mantenimiento_Solicitudes_ModificarUbicacion(idSolicitud, Convert.ToInt32(cbxBase.SelectedValue), txtUbicacionTaller.Text, Usuario);
                Respuesta = Convert.ToString(dtRespuesta.Rows[0]["exito"]);
                string NroRPTA = Respuesta.Substring(0, 1);
                if (NroRPTA == "0")
                {
                    MessageBox.Show(Respuesta, "Operacion Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarTalleres();
                    btnCerrar2_Click(sender, e);
                }
                else { MessageBox.Show(Respuesta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
    }
}
