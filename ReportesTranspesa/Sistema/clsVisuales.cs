using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using DevExpress.Utils;
using System.IO;

namespace ReportesTranspesa.Sistema
{
    public class clsVisuales
    {
        private readonly static clsVisuales instancia = new clsVisuales();

        public static clsVisuales Instancia
        {
            get { return instancia; }
        }
        public enum TipoSelectID
        {
            AutoID = 1,
            ManualID = 2
        }
        public void LlenarTreeView(TreeNode NodoPadre, TreeView TV, DataTable Padre, DataTable Hijos)
        {
            try
            {
                DataTable dtHijos;

                //Iniciamos la actualizacion del TreeView
                TV.BeginUpdate();
                //Limpiamos lo que tenga el TreeView
                TV.Nodes.Clear();
                //Recorremos cada fila del datatable
                foreach (DataRow drPAdre in Padre.Rows)
                {
                    TreeNode NodoCategoria;
                    //Verificamos que no exista un nivel superior (que no exista un nodo padre), para que el nodo categoria sea el nodo padre
                    if (NodoPadre == null)
                    {
                        //Lllenamos los nodos padre (Las categorias); Columna[0] = codigo (Esto estará Oculto); Columna[1] = Descripcion (esto se mostrará)
                        NodoCategoria = TV.Nodes.Add(drPAdre[0].ToString(), drPAdre[1].ToString().Trim(), 0, 1);
                        NodoCategoria.ForeColor = Color.Blue;
                        //Filtramos los los hijos que corresponden al padre
                        String Filtro = Padre.Columns[0].ColumnName + "=";
                        dtHijos = SelectDataTable(Hijos, Filtro + drPAdre[0].ToString(), Hijos.Columns[1].ColumnName + " ASC");
                        //Llenamos los nodos hijos (Los productos)
                        foreach (DataRow drHijo in dtHijos.Rows)
                        {
                            TreeNode NodoProducto;
                            NodoProducto = NodoCategoria.Nodes.Add(drHijo[0].ToString(), drHijo[1].ToString().Trim(), 2, 3);
                        }//Fin del segundo foreach
                        //Limpiamos el objeto para que este limpio para la proxima carga de datos
                        dtHijos.Clear();
                    }
                }//Fin del primer foreach

                //Expandemos el arbol
                TV.ExpandAll();
                //Finalizamos la actualizacion del TreeView
                TV.EndUpdate();
                TV.SelectedNode = TV.Nodes[0].Nodes[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public DataTable SelectDataTable(DataTable DT, String Filter, String Sort)
        {
            try
            {
                DataRow[] Rows;
                DataTable dtNew;

                dtNew = DT.Clone();

                Rows = DT.Select(Filter, Sort);

                foreach (DataRow dr in Rows)
                {
                    dtNew.ImportRow(dr);
                }

                return dtNew;
            }
            catch
            {
                return new DataTable();
            }
        }

        public void LlenarLw(ListView MyLista, DataTable Registro, Boolean LlenaCabezeras, Boolean CheckedItems, Boolean TamanioAut)
        {
            DataTable dtTamanioColumns;
            Int32 TamanioActual = 0;
            Int32 TamanioTitulo = 0;
            Int32 Tamanio = 0;

            int Columnas;
            ListViewItem ItemLista;

            MyLista.BeginUpdate();

            Columnas = Registro.Columns.Count;

            dtTamanioColumns = MakeDtLongitud(Columnas);

            if (LlenaCabezeras)
            {
                MyLista.Columns.Clear();
                foreach (DataColumn dtc in Registro.Columns)
                {
                    MyLista.Columns.Add(dtc.ColumnName.ToString(), 100, 0).Name = "ch" + dtc.ColumnName.ToString();
                }
            }

            MyLista.Items.Clear();

            if (Registro.Rows.Count <= 0) { MyLista.EndUpdate(); return; }

            DataRow dtrTC = dtTamanioColumns.NewRow();

            foreach (DataRow dr in Registro.Rows)
            {
                ItemLista = MyLista.Items.Add(dr[0].ToString());

                dtrTC["C" + 0] = 0;

                for (int I = 1; I < Columnas; I++)
                {
                    if (dr[I] != null)
                    {
                        if (dr[I].GetType() == typeof(DateTime))
                        {
                            ItemLista.SubItems.Add(DateTime.Parse(dr[I].ToString()).ToShortDateString());
                        }
                        else
                        {
                            ItemLista.SubItems.Add(dr[I].ToString());
                        }

                        //Guardamos el tamanio
                        Tamanio = dr[I].ToString().Length;
                        TamanioTitulo = Registro.Columns[I].ColumnName.Length;

                        if (Tamanio > TamanioTitulo)
                        {
                            if (Tamanio > TamanioActual)
                            {
                                dtrTC["C" + I] = Tamanio;
                                TamanioActual = Tamanio;
                            }
                        }
                        else
                        {
                            dtrTC["C" + I] = TamanioTitulo;
                            TamanioActual = TamanioTitulo;
                        }
                    }
                    else
                    {
                        ItemLista.SubItems.Add("");
                        TamanioTitulo = Registro.Columns[I].ColumnName.Length;
                        if (TamanioTitulo > TamanioActual)
                        {
                            dtrTC["C" + I] = TamanioTitulo;
                            TamanioActual = TamanioTitulo;
                        }
                    }
                }
            }
            dtTamanioColumns.Rows.Add(dtrTC);

            if (TamanioAut) FormatLV(MyLista, dtTamanioColumns);

            MyLista.EndUpdate();
        }

        private void FormatLV(ListView MyList, DataTable WidthColumns)
        {
            try
            {
                if (WidthColumns.Rows.Count <= 0) return;

                for (int i = 0; i < MyList.Columns.Count; i++)
                {
                    MyList.Columns[i].Width = Int32.Parse(WidthColumns.Rows[0]["C" + i].ToString());
                }
            }
            catch
            {

            }
        }

        private DataTable MakeDtLongitud(Int32 Columns)
        {
            try
            {
                DataTable dt = new DataTable("WidthColumns");
                for (int i = 0; i < Columns; i++)
                {
                    dt.Columns.Add("C" + i.ToString(), typeof(Object));
                }
                return dt;
            }
            catch
            {
                return new DataTable();
            }
        }
        public Boolean CreaXML(DataSet dtsSource, String Path, String Name, String nameRelation, String campoRelation, Int32 nroTablas)
        {
            try
            {

                //DataRelation dtrRelacion1 = dtsSource.Relations.Add(nameRelation, dtsSource.Tables[0].Columns[campoRelation], dtsSource.Tables[1].Columns[campoRelation]);
                for (Int32 i = 1; i < nroTablas; i++)
                {
                    DataRelation FX_Relation = new DataRelation(nameRelation + i.ToString(), dtsSource.Tables[0].Columns[campoRelation], dtsSource.Tables[i].Columns[campoRelation]);
                    dtsSource.Relations.Add(FX_Relation);
                    //DataRelation FX_Relation = dtsSource.Relations.Add(nameRelation + i.ToString(), dtsSource.Tables[0].Columns[campoRelation], dtsSource.Tables[i].Columns[campoRelation]);
                }

                dtsSource.WriteXmlSchema(Path + "\\Reportes\\Source\\" + Name + ".xsd");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
    }
}
