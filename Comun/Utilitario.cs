using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Entidades;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Xml;


namespace Comun
{
    public class Utilitario
    {
        #region singleton
        private static readonly Utilitario _instancia = new Utilitario();
        public String Advertencia;
        public static Utilitario Instancia
        {
            get { return Utilitario._instancia; }
        }
        #endregion singleton

        public class TipoOperacion // estas opciones son inmovibles
        {
            public static int Registrar = 0;
            public static int Lectura = 1;
            public static int Editar = 2;
            public static int Anular = 3;
            public static int Nuevo = 4;
            public static int Activar = 5;
        }

        public DataTable ListaPermisos;
        public String TextoMenuServidor;
        public String BaseDatos;
        public String ipServidor;
        public String UsuarioConexion;
        public String ClaveConexion;
        public clsUsuario SesionUsuario;

        // Se tiene que crear primero la columna asignandole Nombre y Tipo de datos    



        #region Obtener Codigo de Retorno
        public static bool CodigoRetorno(string codigo, ref String retorno)
        {

            int pos;

            pos = codigo.IndexOf("=");


            if (pos > 0)
            {
                retorno = codigo.Substring(pos + 1);
                if (Convert.ToInt32(codigo.Substring(0, pos)) == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                retorno = "No se puedo determinar el código de retorno desde el servidor.";
                return false;
            }

        }
        #endregion

        #region Convertir datatble a XML
        public String DatatableToXml(DataTable dt)
        {

            var dtResultado = dt.Rows.Cast<DataRow>().Where(row => !Array.TrueForAll(row.ItemArray, value => { return value.ToString().Length == 0; }));
            DataTable tabla = dtResultado.CopyToDataTable();
            //String formatoTildes = @"<?xml version=""1.0""encoding=""iso-8859-1""?>";
            DataSet ds = new DataSet("r");
            tabla.TableName = "d";

            for (int i = 0; i < tabla.Columns.Count; i++)
            {
                tabla.Columns[i].ColumnMapping = MappingType.Attribute;
            }

            StringWriter SW = new StringWriter();
            //SW.WriteLine(formatoTildes);
            ds.Tables.Add(tabla.Copy());
            ds.WriteXml(SW);

            String xml = SW.ToString().Trim();
            return xml;

        }

        #endregion

        #region  Convertir XML a Datatable
        public DataTable ConvertirXMLaDatatable(String xml)
        {
            StringReader theReader = new StringReader(xml);
            DataSet theDataSet = new DataSet();
            theDataSet.ReadXml(theReader);

            return theDataSet.Tables[0];
        }

        #endregion

        #region Covertir Lista a Tabla
        public static DataTable ConvertEntidadATabla<TItemType>(List<TItemType> list)
        {
            DataTable convertedData = new DataTable();

            // Get List Item Properties info
            Type itemType = typeof(TItemType);
            PropertyInfo[] publicProperties =
                // Only public non inherited properties
                itemType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            // Create Table Columns
            foreach (PropertyInfo property in publicProperties)
            {
                // DataSet does not support System.Nullable<>
                if (property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // Set the column datatype as the nullable value type
                    convertedData.Columns.Add(property.Name, property.PropertyType.GetGenericArguments()[0]);
                }
                else
                {
                    convertedData.Columns.Add(property.Name, property.PropertyType);
                }
            }

            // Convert the Data
            foreach (TItemType item in list)
            {
                object[] rowData = new object[convertedData.Columns.Count];
                int rowDataIndex = 0;
                // Iterate through Item Properties
                foreach (PropertyInfo property in publicProperties)
                {
                    // Add a single cell data
                    rowData[rowDataIndex] = property.GetValue(item, null);
                    rowDataIndex++;
                }
                convertedData.Rows.Add(rowData);
            }

            return convertedData;
        }
        #endregion

        #region obtener permisos de formulario segun usuario
        public DataTable ObtenerPermisosPorFormulario(String nombreFormulario)
        {
            DataTable dt = Utilitario.Instancia.ListaPermisos;
            DataTable dtPermisos;

            var query = (from order in dt.AsEnumerable()
                         where order.Field<string>("nombrefrm") == nombreFormulario
                         select order).ToList();

            /*var count = (from order in dt.AsEnumerable()
                         where order.Field<string>("nombrefrm") == nombreFormulario
                         select order).ToList();*/


            if (query.Count > 0)
            {
                dtPermisos = query.CopyToDataTable();
            }
            else
            {
                dtPermisos = null;
            }


            return dtPermisos;
        }
        #endregion

        #region Llenar un ListView a con combobox


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



        #endregion

        public Boolean AutoCompletadoListView(object sender, KeyPressEventArgs keyPress, KeyEventArgs keyUp, EventArgs FocusEnter, ref TextBox controlTBox, ref ListView controlLV, Func<String, DataTable> function, DataTable dt = null)
        {
            bool PresionoEnter = false;
            // keypress listview
            if (keyPress != null)
            {
                if (keyPress.KeyChar != (char)Keys.Enter)
                {
                    controlLV.Visible = true;
                    if (dt == null)
                    {
                        Utilitario.Instancia.LlenarLw(controlLV, function(controlTBox.Text), true, false, false);
                    }
                    else
                    {
                        Utilitario.Instancia.LlenarLw(controlLV, dt, true, false, false);
                    }

                    controlLV.Visible = true;
                    controlLV.Columns[0].Width = 0;
                    controlLV.Columns[1].Width = 350;
                    controlLV.Focus();

                }


                if ((keyPress.KeyChar == (char)Keys.Enter && !controlLV.Items.Count.Equals(0)))
                {

                    ListViewItem ItemActual;
                    ItemActual = controlLV.SelectedItems[0];
                    controlTBox.Tag = Convert.ToString(ItemActual.Text);
                    controlTBox.Text = ItemActual.SubItems[1].Text;
                    controlLV.Visible = false;
                    PresionoEnter = true;

                }


                if (keyPress.KeyChar == (char)Keys.Delete)
                {
                    controlLV.Visible = true;
                    Utilitario.Instancia.LlenarLw(controlLV, function(controlTBox.Text), true, false, false);
                    controlLV.Visible = true;
                    controlLV.Columns[0].Width = 0;
                    controlLV.Columns[1].Width = 350;


                }
                if (keyPress.KeyChar == (char)Keys.Escape)
                {
                    controlLV.Visible = false;
                    controlTBox.Focus();
                    controlTBox.Select(controlTBox.Text.Length, 0);
                    controlTBox.Tag = null;
                }
            }

            if (FocusEnter != null)
            {

                if (!controlLV.Items.Count.Equals(0))
                {
                    controlLV.Items[0].Selected = true;
                }
            }

            if (keyUp != null)
            {
                if (char.IsLetterOrDigit((char)(keyUp.KeyValue)) || keyUp.KeyValue == (char)Keys.Delete || keyUp.KeyValue == (char)Keys.Enter)
                {
                    controlTBox.Focus();
                }

            }

            return PresionoEnter;
        }

        public DataTable LINQResultToDataTable<T>(IEnumerable<T> Linqlist)
        {
            DataTable dt = new DataTable();
            PropertyInfo[] columns = null;

            if (Linqlist == null) return dt;

            foreach (T Record in Linqlist)
            {

                if (columns == null)
                {
                    columns = ((Type)Record.GetType()).GetProperties();
                    foreach (PropertyInfo GetProperty in columns)
                    {
                        Type colType = GetProperty.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                               == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dt.Columns.Add(new DataColumn(GetProperty.Name, colType));
                    }
                }

                DataRow dr = dt.NewRow();

                foreach (PropertyInfo pinfo in columns)
                {
                    dr[pinfo.Name] = pinfo.GetValue(Record, null) == null ? DBNull.Value : pinfo.GetValue
                           (Record, null);
                }

                dt.Rows.Add(dr);
            }
            return dt;
        }
        public bool AutoCompletadoTexBox(object sender, KeyPressEventArgs keyPress, KeyEventArgs keyUp, EventArgs FocusEnter, ref TextBox controlTBox, ref ListView controlLV, Func<String, DataTable> function = null, DataTable dt = null)
        {
            bool PresionoEnter = false;

            if (keyUp != null)
            {
                // keypress textbox
                if (keyUp.KeyValue != (char)Keys.Enter && controlTBox.Text.Length >= 2)
                {
                    if (dt == null && function != null)
                    {
                        Utilitario.Instancia.LlenarLw(controlLV, function(controlTBox.Text), true, false, false);

                        controlLV.Columns[0].Width = 0;
                        controlLV.Columns[1].Width = 350;

                        if (controlLV.Columns.Count >= 6) { controlLV.Columns[5].Width = 250; controlLV.Columns[2].Width = 0; }
                        controlLV.Size = new System.Drawing.Size(350, 103);
                        controlLV.BringToFront();
                        controlLV.Visible = true;
                    }
                    else
                    {
                        if (dt != null && function == null)
                        {
                            string valor = controlTBox.Text;

                            var objResult1 = from tbl in dt.AsEnumerable()
                                             where tbl.Field<string>(1).Contains(valor)
                                             select tbl;

                            if (objResult1.Count() > 0)
                            {
                                //DataTable temp = LINQResultToDataTable(objResult1);
                                DataTable dt2 = objResult1.CopyToDataTable<DataRow>();
                                Utilitario.Instancia.LlenarLw(controlLV, dt2, true, false, false);
                            }
                            else
                            {
                                Utilitario.Instancia.LlenarLw(controlLV, dt, true, false, false);
                            }

                            controlLV.Columns[0].Width = 0;
                            controlLV.Columns[1].Width = 350;
                            controlLV.Size = new System.Drawing.Size(350, 103);
                            controlLV.BringToFront();
                            controlLV.Visible = true;
                        }


                    }
                }

                /*if (controlTBox.Text.Length == 0)
                {

                    controlTBox.Tag = null;
                }*/
            }


            if (keyUp != null)
            {
                if (keyUp.KeyValue == (char)Keys.Enter && controlTBox.Text.Length > 0)
                {
                    PresionoEnter = true;
                }
                if (keyUp.KeyValue == (char)Keys.Escape)
                {
                    controlLV.Visible = false;
                    controlTBox.Focus();
                    controlTBox.Tag = null;
                }
            }


            if (keyUp != null)
            {
                if (keyUp.KeyValue == (char)Keys.Down)
                {

                    controlLV.Focus();

                }
                if (controlTBox.Text.Length == 0)
                {
                    controlLV.Visible = false;
                    controlTBox.Tag = null;
                    controlTBox.Focus();
                }
            
          
            }

            return PresionoEnter;
        }


        public String QuitarTildes(String xml)
        {
            //String xmlsinTilde = Regex.Replace(xml.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9""=<>?'/:. ]+", "");
            String xmlsinTilde = Regex.Replace(xml.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9""=<>?'\-/:. ]+", "");
            return xmlsinTilde;
        }


        public DataTable GetContentAsDataTable(DataGridView dgv, bool IgnoreHideColumns = false)
        {
            try
            {
                if (dgv.ColumnCount == 0) return null;
                DataTable dtSource = new DataTable();
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (IgnoreHideColumns & !col.Visible) continue;
                    if (col.Name == string.Empty) continue;
                    if (col.ValueType == null)
                    {
                        dtSource.Columns.Add(col.Name, typeof(String));
                    }
                    else
                    {
                        dtSource.Columns.Add(col.Name, col.ValueType);
                    }

                    dtSource.Columns[col.Name].Caption = col.HeaderText;
                }
                if (dtSource.Columns.Count == 0) return null;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    DataRow drNewRow = dtSource.NewRow();
                    foreach (DataColumn col in dtSource.Columns)
                    {
                        drNewRow[col.ColumnName] = row.Cells[col.ColumnName].Value;
                    }
                    dtSource.Rows.Add(drNewRow);
                }
                return dtSource;
            }
            catch { return null; }
        }


        public String ObtenerNodoXML_RespuestaCDR(String archivoXML)
        {
            DataTable dt = new DataTable(); ;//= new DataTable();
            dt.Columns.Add("Descripcion", typeof(String));
            string respuesta = string.Empty;


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(archivoXML);
            //NamespaceManager  para resolver cada namespace
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            //nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            nsmgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");


            //XmlNodeList nodeList;
            //XmlNode root = doc.DocumentElement;
            //nodeList = root.SelectNodes("ns", nsmgr);
            //root = root.LastChild.ChildNodes.Item(0);

            //nodeList.Count
            //Seleccionar todos los nodos que coinciden
            XmlElement elementoPadre = doc.DocumentElement;
            XmlNodeList nodeList = elementoPadre.SelectNodes("//cac:DocumentResponse", nsmgr);
            //XmlNodeList nodeList = elementoPadre.SelectNodes("//cbc:DocumentDescription", nsmgr);



            respuesta = "";

            foreach (XmlNode nodo in nodeList)
            {
               
                //dt.Rows.Add(nodo.InnerXml);
                respuesta = respuesta + nodo.FirstChild.InnerText.ToString() + " | ";
                //nodo.LastChild.InnerText = "15.95";
            }


            return respuesta;
        }

        public DataTable ObtenerNodoXML(String archivoXML)
        {
            DataTable dt = new DataTable(); ;//= new DataTable();
            dt.Columns.Add("Descripcion", typeof(String));


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(archivoXML);
            //NamespaceManager  para resolver cada namespace
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            //nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");


            //XmlNodeList nodeList;
            //XmlNode root = doc.DocumentElement;
            //nodeList = root.SelectNodes("ns", nsmgr);
            //root = root.LastChild.ChildNodes.Item(0);

            //nodeList.Count
            //Seleccionar todos los nodos que coinciden
            XmlElement elementoPadre = doc.DocumentElement;
            //XmlNodeList nodeList = elementoPadre.SelectNodes("//cac:DocumentResponse", nsmgr);
            XmlNodeList nodeList = elementoPadre.SelectNodes("//cbc:DocumentDescription", nsmgr);




            foreach (XmlNode nodo in nodeList)
            {

                dt.Rows.Add(nodo.InnerXml);
                //nodo.LastChild.InnerText = "15.95";
            }



            return dt;
        }

        public void ExportarDataGridViewExcel(DataGridView grd)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application(); // Instancia a la libreria de Microsoft Office
            excel.Application.Workbooks.Add(true); //Con esto añadimos una hoja en el Excel para exportar los archivos
            int IndiceColumna = 0;

            foreach (DataGridViewColumn columna in grd.Columns) //Aquí empezamos a leer las columnas del listado a exportar
            {
                IndiceColumna++;
                excel.Cells[1, IndiceColumna] = columna.Name;
            }
          
            int IndiceFila = 0;
            foreach (DataGridViewRow fila in grd.Rows) //Aquí leemos las filas de las columnas leídas
            {
                IndiceFila++;
                IndiceColumna = 0;
                foreach (DataGridViewColumn columna in grd.Columns)
                {
                    IndiceColumna++;
                    //excel.Cells[IndiceFila + 1, IndiceColumna] = fila.Cells[columna.Name].Value; // Con el +1 sale cabecera
                    excel.Cells[IndiceFila, IndiceColumna] = fila.Cells[columna.Name].Value; // Sin el +1 No sale cabecera
                }
            }
            excel.Visible = true;
        }




    }
}

