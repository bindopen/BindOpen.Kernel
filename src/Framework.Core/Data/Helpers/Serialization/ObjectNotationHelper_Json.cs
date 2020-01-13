using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using BindOpen.Framework.Data.Common;
using BindOpen.Framework.Data.Helpers.Objects;
using BindOpen.Framework.Data.Helpers.Strings;
using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Data.Helpers.Serialization
{

    /// <summary>
    /// This class represents a helper for object notation.
    /// </summary>
    public static class ObjectNotationHelper_Json
    {

        // ------------------------------------------
        // DESERIALIZATION / SERIALIZAION
        // ------------------------------------------

        #region Deserialization / Serialization

        private static String GetTextValue(String text)
        {
            return "\"" + text.Replace("\"", "\"\"") + "\"";
        }

        /// <summary>
        /// Gets the result of the serialization of the specified object.
        /// </summary>
        /// <param name="object1">The object to serialize.</param>
        /// <param name="objectName">The name of the output tag to use.</param>
        /// <returns>The Json string serializing the specified object.</returns>
        public static string ToJson(this object object1, String objectName)
        {
            String resultString = "";
            IFormatProvider cultureInfo = new CultureInfo("en-US", true);

            resultString += ObjectNotationHelper_Json.GetTextValue(objectName) + ":";
            if (object1 == DBNull.Value || object1 == null)
                resultString += "null";
            else
            {
                DataValueType dataValueType = object1.GetValueType();
                if ((dataValueType == DataValueType.Date) ||
                    (dataValueType == DataValueType.Text))
                    resultString += ObjectNotationHelper_Json.GetTextValue(ObjectHelper.ToString(object1));
                else
                    resultString += ObjectHelper.ToString(object1);
            }
            return resultString;
        }

            /// <summary>
            /// Gets the result of the serialization of the specified data table.
            /// </summary>
            /// <param name="dataTable">The data table to serialize.</param>
            /// <param name="isFiltered">Indicates whether only relevant information is put in the xml string.</param>
            /// <returns>The Json string serializing the specified data table.</returns>
            public static string ToJson(this DataTable dataTable, bool isFiltered = false)
        {
            String resultString = "";
            IFormatProvider cultureInfo = new CultureInfo("en-US", true);

            resultString += "{" + ObjectNotationHelper_Json.GetTextValue(dataTable.TableName) + ":{\"item\":[";

            // we go through the data table rows
            int j = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                resultString += (j > 0 ? "," : "") + "{";
                // we go through the data table columns
                int i = 0;
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    if ((!isFiltered) |
                        ((!string.Equals(dataColumn.ColumnName, "PASSWORD", StringComparison.OrdinalIgnoreCase)) &
                            (!string.Equals(dataColumn.ColumnName, "DESCRIPTION", StringComparison.OrdinalIgnoreCase)) &
                            (!string.Equals(dataColumn.ColumnName, "CREATIONDATE", StringComparison.OrdinalIgnoreCase)) &
                            (!string.Equals(dataColumn.ColumnName, "MODIFICATIONDATE", StringComparison.OrdinalIgnoreCase))))
                        resultString += (i > 0 ? "," : "") + ObjectNotationHelper_Json.ToJson(
                            dataRow[dataColumn.ColumnName], dataColumn.ColumnName);
                    i++;
                }
                resultString += "}";
                j++;
            }
            resultString += "]}}";
            return resultString;
        }

        ///// <summary>
        ///// Gets the result of the serialization of the specified data table.
        ///// </summary>
        ///// <param name="oleDbDataReader">The OLEDB data reader to serialize.</param>
        ///// <param name="nodeName">The name of the node to consider.</param>
        ///// <param name="count">The number of items to consider.</param>
        ///// <param name="isFiltered">Indicates whether only relevant information is put in the xml string.</param>
        ///// <returns>The Xml string serializing the specified data table.</returns>
        //public static string ToJson(this OleDbDataReader oleDbDataReader, String nodeName, out int count, bool isFiltered = false)
        //{
        //    String resultString = "";
        //    IFormatProvider cultureInfo = new CultureInfo("en-US", true);

        //    count = 0;

        //    resultString += "{" + (nodeName ?? "node") + ":{\"item\":[";

        //    // we go through the data table rows
        //    if (oleDbDataReader != null)
        //    {
        //        List<string> columnNames = Enumerable.Range(0, oleDbDataReader.FieldCount).Select(oleDbDataReader.GetName).ToList();
        //        int j = 0;
        //        while (oleDbDataReader.Read())
        //        {
        //            resultString += (j > 0 ? "," : "") + "{";
        //            // we go through the data table columns
        //            int i = 0;
        //            foreach (String columnName in columnNames)
        //            {
        //                if ((!isFiltered) |
        //                    ((columnName.ToUpper() != "PASSWORD") &
        //                        (columnName.ToUpper() != "DESCRIPTION") &
        //                        (columnName.ToUpper() != "CREATIONDATE") &
        //                        (columnName.ToUpper() != "MODIFICATIONDATE")))
        //                    resultString += (i > 0 ? "," : "") + ObjectNotationHelper_Json.ToJson(
        //                        oleDbDataReader[columnName].GetString(), columnName);
        //                i++;
        //            }
        //            resultString += "}";
        //            j++;
        //        }
        //    }
        //    resultString += "]}}";

        //    return resultString;
        //}

        ///// <summary>
        ///// Gets the result of the serialization of the specified data table.
        ///// </summary>
        ///// <param name="oleDbDataReader">The OLEDB data reader to serialize.</param>
        ///// <param name="nodeName">The name of the node to consider.</param>
        ///// <param name="isFiltered">Indicates whether only relevant information is put in the xml string.</param>
        ///// <returns>The Xml string serializing the specified data table.</returns>
        //public static string ToJson(this OleDbDataReader oleDbDataReader, String nodeName, bool isFiltered = false)
        //{
        //    int count = 0;
        //    return ObjectNotationHelper_Json.ToJson(oleDbDataReader, nodeName, out count, isFiltered);
        //}

        ///// <summary>
        ///// Gets the JOSN entry from the specified string.
        ///// </summary>
        ///// <param name="jsonString">The JSON string to consider.</param>
        ///// <returns>Returns the JSON entry.</returns>
        //public static DataKeyValue GetJSonEntry(this String jsonString)
        //{
        //    DataKeyValue dataKeyValue = null;
        //    int i = 0; int j = 0;
        //    jsonString.GetIndexOfNextString("\"", ref i);
        //    j = jsonString.GetIndexOfNextString("\"", i+1);
        //    if (j < jsonString.Length)
        //    {
        //        dataKeyValue = new DataKeyValue(jsonString.GetSubstring(i, j).GetUnquotedString());
        //        jsonString.GetIndexOfNextString(":", ref i);
        //        if (i+1 < jsonString.Length)
        //            dataKeyValue.Content = jsonString.Substring(i + 1).Trim().GetUnquotedString();
        //    }

        //    return dataKeyValue;
        //}

        /// <summary>
        /// Updates table of the specified data set from JSON string.
        /// </summary>
        /// <param name="jsonString">The xml string.</param>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="tableName">The name of the output data table.</param>
        /// <param name="isAppend">Indicates whether the update consists in inserting a new row.</param>
        public static void FromJson(
            this DataSet dataSet,
            String jsonString,
            String tableName,
            bool isAppend = false)
        {
            if ((jsonString == null)||(dataSet==null)|| (dataSet.Tables[tableName] == null))
                return;

            if (!isAppend)
                dataSet.Tables[tableName].Clear();

            DataTable dataTable = dataSet.Tables[tableName].Clone();
            IFormatProvider cultureInfo = new CultureInfo("en-US", true);

            if (jsonString != "")
            {
                int i = 0; int j = 0; int k = 0; int l = 0; int m = 0;
                i = jsonString.IndexOf("\"" + tableName + "\"");
                if (i>-1)
                {
                    jsonString.GetIndexOfNextString(":", ref i);
                    jsonString.GetIndexOfNextString("{", ref i);

                    i = jsonString.IndexOf("\"item\"", i);
                    if (i > -1)
                    {
                        jsonString.GetIndexOfNextString(":", ref i);
                        jsonString.GetIndexOfNextString("[", ref i);
                        jsonString.GetIndexOfNextString("{", ref i);
                        i++;

                        l = jsonString.GetIndexOfNextString("]", i);
                        while ((j = jsonString.GetIndexOfNextString("}", i)) < l)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataTable.Rows.Add(dataRow);

                            m = i;
                            jsonString.GetIndexOfNextString("{", ref i);
                            i++;
                            while (m <= j)
                            {
                                k = jsonString.GetIndexOfNextString(",", m);

                                DataKeyValue dataKeyValue = new DataKeyValue(); // jsonString.GetSubstring(m, StringHelper.GetMinimumIndex(j, k) - 1).GetJSonEntry();

                                DataColumn dataColumn = null;
                                if (dataKeyValue != null && (dataColumn = dataTable.Columns[dataKeyValue.Key]) != null)
                                {
                                    Object object1 = null;
                                    if ((dataKeyValue.Content == null) || (string.Equals(dataKeyValue.Content, "%NULL()", StringComparison.OrdinalIgnoreCase)))
                                        object1 = null;
                                    else
                                        object1 = dataKeyValue.Content.ToObject() ?? DBNull.Value;
                                    dataRow[dataKeyValue.Key] = object1;
                                    m = StringHelper.GetMinimumIndex(k, j)+1;
                                }
                            }
                        }
                    }
                }

                //XDocument aJsonDocument = XDocument.Parse(jsonString, LoadOptions.PreserveWhitespace);
                //            IEnumerable<XElement> someJsonElements =
                //                from e in aJsonDocument.Root.Descendants("item")
                //                where e.Parent.Name.ToString().ToUpper() == tableName.ToUpper()
                //                select e;
                //            foreach (XElement currentJsonElement in someJsonElements)
                //            {
                //                DataRow dataRow = dataTable.NewRow();

                //                foreach (DataColumn dataColumn in dataTable.Columns)
                //                {
                //                    XElement aSubElement =
                //                        (from e in currentJsonElement.Descendants(dataColumn.ColumnName.ToUpper()) select e).FirstOrDefault();
                //                    if (aSubElement != null)
                //                        try
                //                        {
                //                            if (aSubElement.Value == "%NULL()")
                //                                dataRow[dataColumn.ColumnName] = DBNull.Value;

                //                            else if (dataColumn.DataType == System.Type.GetType("System.DateTime"))
                //                                if (aSubElement.Value == String.Empty)
                //                                    dataRow[dataColumn.ColumnName] = DBNull.Value;
                //                                else
                //                                    dataRow[dataColumn.ColumnName] = DateTime.ParseExact(aSubElement.Value, StringHelper.DateFormat, cultureInfo);

                //                            else if (dataColumn.DataType == System.Type.GetType("System.Single"))
                //                                if (aSubElement.Value == String.Empty)
                //                                    dataRow[dataColumn.ColumnName] = DBNull.Value;
                //                                else
                //                                    dataRow[dataColumn.ColumnName] = Single.Parse(aSubElement.Value, cultureInfo);

                //                            else if (dataColumn.DataType == System.Type.GetType("System.Double"))
                //                                if (aSubElement.Value == String.Empty)
                //                                    dataRow[dataColumn.ColumnName] = DBNull.Value;
                //                                else
                //                                    dataRow[dataColumn.ColumnName] = Double.Parse(aSubElement.Value, cultureInfo);

                //                            else
                //                                dataRow[dataColumn.ColumnName] = aSubElement.Value;
                //                        }
                //                        catch
                //                        {
                //                        }
                //                }
                //                dataTable.Rows.Add(dataRow);
                //            }

                dataSet.Merge(dataTable);
            }
        }

        /// <summary>
        /// Updates table of the specified data set from JSON string.
        /// </summary>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="jsonString">The xml string.</param>
        /// <param name="tableNames">The names of the output data table.</param>
        /// <param name="isAppend">Indicates whether the update consists in inserting a new row.</param>
        public static void FromJson(
            this DataSet dataSet,
            String jsonString,
            List<string> tableNames,
            bool isAppend = false)
        {
            foreach (String tableName in tableNames)
                ObjectNotationHelper_Json.FromJson(dataSet, jsonString, tableName, isAppend);
        }

        /// <summary>
        /// Gets the value of the specified Json node in the specified xml string.
        /// </summary>
        /// <param name="jsonString">The xml string to parse.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <returns>The value of the specified Json node in the specified xml string.</returns>
        public static string GetJsonNodeValue(
            this String jsonString,
            String nodeName)
        {
            if (jsonString == String.Empty)
                return null;
            IFormatProvider cultureInfo = new CultureInfo("en-US", true);

            DataKeyValue dataKeyValue = null;

            int i = 0;
            while ((i > -1)&&(i<jsonString.Length))
            {
                i = jsonString.IndexOf("\"" + nodeName + "\"", i);
                if (i>-1)
                {
                    int k = jsonString.IndexOf("]", i) - 1;
                    int l = jsonString.IndexOf("}", i) - 1;
                    int m = jsonString.IndexOf(",", i) - 1;
                    int n = jsonString.IndexOf(":", i);
                    if (n < StringHelper.GetMinimumIndex(k, l, m))
                    {
                        i = n + 1;
                        k = jsonString.IndexOf("[", i);
                        l = jsonString.IndexOf("{", i);
                        m = jsonString.IndexOf("\"", i);
                        i = StringHelper.GetMinimumIndex(k, l, m);
                        n = 0;
                        if (i == k)
                            n = jsonString.GetIndexOfNextString("]", i + 1);
                        else if (i == l)
                            n = jsonString.GetIndexOfNextString("}", i + 1);
                        else if (i == m)
                            n = jsonString.GetIndexOfNextString("\"", i+1);
                        if (n > 0)
                            dataKeyValue = new DataKeyValue(nodeName, jsonString.GetSubstring(i, n).GetUnquotedString());
                        i = n + 1;
                    }
                }
            }

            return (dataKeyValue != null ? dataKeyValue.Content : null);
        }

        #endregion

    }
}
