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
using System.Globalization;
using System.Diagnostics;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;


namespace ReportesTranspesa.Formularios.Areas.Sistemas
{
    public partial class frmLineasRPC : MetroFramework.Forms.MetroForm
    {
        public frmLineasRPC()
        {
            InitializeComponent();
            //GridColumn colCounter = dtgvDataView.Columns.AddVisible("RowHandle");
            //colCounter.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            //colCounter.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;

            //dtgvDataView.CustomUnboundColumnData += (sender, e) =>
            //{
            //    GridView view = sender as GridView;
            //    if (e.Column.FieldName == "RowHandle" && e.IsGetData)
            //        e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            //};
        }

        private void frmLineasRPC_Load(object sender, EventArgs e)
        {
            cboCompañia.SelectedIndex = 0;
            gbFiltro.Size = new System.Drawing.Size(286, 55);
            rbPlan75.Location = new System.Drawing.Point(219, 26);
            ListaRPC();
        }

        //public void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        //{
        //    gridView1.SetRowCellValue(e.RowHandle, "ID", gridView1.RowCount);
        //}

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            string compañia="";
            if (chkCompania.Checked == true)
            {
                switch (cboCompañia.SelectedIndex)
                {
                    case 0: compañia = "10000000";
                        break;

                    case 1: compañia = "60000000";
                        break;

                    case 2: compañia = "70000000";
                        break;
                }
            }
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.ListaRPC(compañia);
            if (dt.Rows.Count > 0)
            {
                //DataTable myDt = new DataTable();
                //DataColumn dc = new DataColumn();
                //dc.ColumnName = "#";
                //dc.DataType = typeof(int);
                //dc.AutoIncrement = true;
                //dc.AutoIncrementSeed = 1;
                //dc.AutoIncrementStep = 1;               
                ////dt.Columns.Add(dc);
                //myDt.Columns.Add(dc);

                //myDt.Merge(dt);
                ////DataRow newRow = myDt.NewRow();
                ////myDt.Rows.Add(newRow);
                //this.dtgvData.DataSource = myDt;

                dtgvData.DataSource = dt;
                dtgvDataView.Columns["IdLinea"].Visible = false;
                dtgvDataView.Columns["CompañiaSocio"].Visible = false;
                //for (int i = 0; i < dtgvDataView.DataRowCount; i++)
                //{
                //    if (dtgvDataView.GetRowCellValue(i, "ColumnFieldName").ToString() == "IdLinea")
                //    {
                //        //  Your code here  
                //    }
                //} 
                GridColumn colCounter = dtgvDataView.Columns.AddVisible("N°");
                colCounter.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                colCounter.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                dtgvDataView.Columns["N°"].VisibleIndex = 0;
                //dtgvDataView.Columns.ColumnByName("N°").VisibleIndex = 0;
                //colCounter.SortIndex = 0;
                dtgvDataView.BestFitColumns();
                //************************FILTRO*******************************************************
                string filtro = "";
                int contafiltros = 0;
                if (rbTodos.Checked)
                {
                    filtro = "[Plan_RPC] LIKE '% %'";
                    contafiltros = contafiltros + 1;
                }
                if (rbPlan12.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "[Plan_RPC] = 'PLAN 12'";
                    contafiltros = contafiltros + 1;
                }
                if (rbPlan29.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "[Plan_RPC] = 'PLAN 29'";
                    contafiltros = contafiltros + 1;
                }
                if (rbPlan69.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "[Plan_RPC] = 'PLAN 69'";
                    contafiltros = contafiltros + 1;
                }
                if (rbPlan75.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "[Plan_RPC] = 'PLAN 75'";
                    contafiltros = contafiltros + 1;
                }
                if (rbPlan79.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "[Plan_RPC] = 'PLAN 79'";
                    contafiltros = contafiltros + 1;
                }
                if (rbPlan180.Checked)
                {
                    if (contafiltros > 0)
                    {
                        filtro = filtro + " AND ";
                    }
                    filtro = filtro + "[Plan_RPC] = 'PLAN 180'";
                    contafiltros = contafiltros + 1;
                }
                //**********************FIN DEL FILTRO*************************************************
                if (filtro != "")
                {
                    dtgvDataView.Columns["Plan_RPC"].FilterInfo = new ColumnFilterInfo(filtro);
                }
            }
            else
            {
                Mensaje m = new Mensaje();
                m.mensaje = "No hay data para mostrar";
                m.ShowDialog();
            }
        }

        private void chkCompania_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCompania.Checked)
            {
                cboCompañia.Enabled = true;
                cboCompañia.SelectedIndex = 0;
            }
            else
            {
                cboCompañia.Enabled = false;
                cboCompañia.SelectedIndex = -1;
            }
        }

        private void dtgvDataView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //e.DisplayText = e.RowHandle.ToString();
            //e.DisplayText = "IdLinea " + e.RowHandle.ToString();
        }

        private void dtgvDataView_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            //dtgvDataView.SetRowCellValue(e.RowHandle, "IdLinea", dtgvDataView.RowCount);  
        }

        private void dtgvDataView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Column.FieldName == "N°" && e.IsGetData)
                e.Value = view.GetRowHandle(e.ListSourceRowIndex) + 1;
            //if (e.IsGetData && e.Column.FieldName == "RecordNumber")
            //{
            //    e.Value = e.RowHandle + 1;
            //} 
        }

        public void ListaRPC()
        {
            dtgvData.DataSource = null;
            dtgvDataView.Columns.Clear();
            string compañia = "";
            if (chkCompania.Checked == true)
            {
                switch (cboCompañia.SelectedIndex)
                {
                    case 0: compañia = "10000000";
                        break;

                    case 1: compañia = "60000000";
                        break;

                    case 2: compañia = "70000000";
                        break;
                }
            }
            DataTable dt = new DataTable();
            dt = clsSistemasBL.Instancia.ListaRPC(compañia);
            if (dt.Rows.Count > 0)
            {
                dtgvData.DataSource = dt;
                dtgvDataView.Columns["IdLinea"].Visible = false;
                dtgvDataView.Columns["CompañiaSocio"].Visible = false;
                GridColumn colCounter = dtgvDataView.Columns.AddVisible("N°");
                colCounter.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
                colCounter.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                dtgvDataView.Columns["N°"].VisibleIndex = 0;
                dtgvDataView.BestFitColumns();
            }
        }
    }
}
