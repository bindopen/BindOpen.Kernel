using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Helpers.Strings;

namespace BindOpen.Framework.Core.Data.Helpers.Serialization
{

    /// <summary>
    /// This class represents a helper for object notation.
    /// </summary>
    public static class ObjectNotationHelper_Xml
    {

        // ------------------------------------------
        // DESERIALIZATION / SERIALIZAION
        // ------------------------------------------

        #region Deserialization / Serialization

        /// <summary>
        /// Gets the result of the serialization of the specified object.
        /// </summary>
        /// <param name="object1">The object to serialize.</param>
        /// <param name="objectName">The name of the output tag to use.</param>
        /// <returns>The Xml string serializing the specified object.</returns>
        private static String ToXml(this object object1, String objectName)
        {
            String resultString = "";
            IFormatProvider cultureInfo = new CultureInfo("en-US", true);

            resultString += "<" + objectName + ">";
            if (object1 == DBNull.Value)
                resultString += "%NULL()";
            else
            {
                DataValueType dataValueType = object1.GetValueType();
                if (dataValueType == DataValueType.Text)
                    resultString += "<![CDATA[" + object1.ToString() + "]]>";
                else
                    resultString += ObjectHelper.ToString(object1);
            }
            resultString += "</" + objectName + ">";
            return resultString;
        }

        /// <summary>
        /// Gets the result of the serialization of the specified data table.
        /// </summary>
        /// <param name="dataTable">The data table to serialize.</param>
        /// <param name="isFiltered">Indicates whether only relevant information is put in the xml string.</param>
        /// <returns>The Xml string serializing the specified data table.</returns>
        public static string ToXml(this DataTable dataTable, bool isFiltered = false)
        {
            String resultString = "";
            IFormatProvider cultureInfo = new CultureInfo("en-US", true);

            resultString += "<" + dataTable.TableName + ">";

            // we go through the data table rows
            foreach (DataRow dataRow in dataTable.Rows)
            {
                resultString += "<item>";
                // we go through the data table columns
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    if ((!isFiltered) |
                        ((!string.Equals(dataColumn.ColumnName, "PASSWORD", StringComparison.OrdinalIgnoreCase)) &
                            (!string.Equals(dataColumn.ColumnName, "DESCRIPTION", StringComparison.OrdinalIgnoreCase)) &
                            (!string.Equals(dataColumn.ColumnName, "CREATIONDATE", StringComparison.OrdinalIgnoreCase)) &
                            (!string.Equals(dataColumn.ColumnName, "MODIFICATIONDATE", StringComparison.OrdinalIgnoreCase))))
                    {
                        resultString += "<" + dataColumn.ColumnName.ToUpper() + ">";

                        if (dataRow[dataColumn.ColumnName] == DBNull.Value)
                            resultString += "%NULL()";
                        else
                            resultString += ObjectHelper.ToString(dataRow[dataColumn.ColumnName]);

                        resultString += "</" + dataColumn.ColumnName.ToUpper() + ">";
                    }
                    else
                        resultString += "<" + dataColumn.ColumnName.ToUpper() + " />";
                }
                resultString += "</item>";
            }

            resultString += "</" + dataTable.TableName + ">";
            return resultString;
        }

        ///// <summary>
        ///// Gets the result of the serialization of the specified data table.
        ///// </summary>
        ///// <param name="oleDbDataReader">The OLEDB data reader to serialize.</param>
        ///// <param name="nodeName">The name of the node to consider.</param>
        ///// <param name="isFiltered">Indicates whether only relevant information is put in the xml string.</param>
        ///// <returns>The Xml string serializing the specified data table.</returns>
        //public static string ToXml(this OleDbDataReader oleDbDataReader, String nodeName, bool isFiltered = false)
        //{
        //    int count = 0;
        //    return ObjectNotationHelper_Xml.ToXml(oleDbDataReader, nodeName, out count, isFiltered);
        //}

        ///// <summary>
        ///// Gets the result of the serialization of the specified data table.
        ///// </summary>
        ///// <param name="oleDbDataReader">The OLEDB data reader to serialize.</param>
        ///// <param name="nodeName">The name of the node to consider.</param>
        ///// <param name="count">The number of items to consider.</param>
        ///// <param name="isFiltered">Indicates whether only relevant information is put in the xml string.</param>
        ///// <returns>The Xml string serializing the specified data table.</returns>
        //public static string ToXml(this OleDbDataReader oleDbDataReader, String nodeName, out int count, bool isFiltered = false)
        //{
        //    String resultString = "";
        //    IFormatProvider cultureInfo = new CultureInfo("en-US", true);

        //    count = 0;

        //    resultString += "<" + (nodeName ?? "node") + ">";

        //    // we go through the data table rows
        //    if (oleDbDataReader != null)
        //    {
        //        List<string> columnNames = Enumerable.Range(0, oleDbDataReader.FieldCount).Select(oleDbDataReader.GetName).ToList();
        //        while (oleDbDataReader.Read())
        //        {
        //            resultString += "<item>";
        //            // we go through the data table columns
        //            foreach (String columnName in columnNames)
        //            {
        //                if ((!isFiltered) |
        //                    ((columnName.ToUpper() != "PASSWORD") &
        //                        (columnName.ToUpper() != "DESCRIPTION") &
        //                        (columnName.ToUpper() != "CREATIONDATE") &
        //                        (columnName.ToUpper() != "MODIFICATIONDATE")))
        //                {
        //                    resultString += "<" + columnName.ToUpper() + ">";

        //                    if (oleDbDataReader[columnName] == DBNull.Value)
        //                        resultString += "%NULL()";
        //                    else
        //                        resultString += oleDbDataReader[columnName].GetString();

        //                    resultString += "</" + columnName.ToUpper() + ">";
        //                }
        //                else
        //                    resultString += "<" + columnName.ToUpper() + " />";
        //            }
        //            resultString += "</item>";
        //            count++;
        //        }
        //    }

        //    resultString += "</" + (nodeName ?? "node") + ">";
        //    return resultString;
        //}

        /// <summary>
        /// Deserializes the specified xml string and puts the result into the
        /// specified data table.
        /// </summary>
        /// <param name="xmlString">The xml string.</param>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="tableNames">The names of the output data table.</param>
        /// <param name="isAppend">Indicates whether the update consists in inserting a new row.</param>
        public static void FromXml(
            this DataSet dataSet,
            String xmlString,
            List<string> tableNames,
            bool isAppend = false)
        {
            if ((xmlString == null) | (dataSet == null))
                return;

            XDocument xmlDocument = XDocument.Parse(xmlString, LoadOptions.PreserveWhitespace);
            foreach (String tableName in tableNames)
                if (dataSet.Tables[tableName] != null)
                {
                    if (!isAppend)
                        dataSet.Tables[tableName].Clear();

                    DataTable dataTable = dataSet.Tables[tableName].Clone();
                    IFormatProvider cultureInfo = new CultureInfo("en-US", true);

                    if (xmlString != "")
                    {
                        IEnumerable<XElement> xmlElements =
                            from e in xmlDocument.Root.Descendants("item")
                            where string.Equals(e.Parent.Name.ToString(), tableName, StringComparison.OrdinalIgnoreCase)
                            select e;
                        foreach (XElement currentXmlElement in xmlElements)
                        {
                            DataRow dataRow = dataTable.NewRow();

                            foreach (DataColumn dataColumn in dataTable.Columns)
                            {
                                XElement aSubElement =
                                    (from e in currentXmlElement.Descendants(dataColumn.ColumnName.ToUpper()) select e).FirstOrDefault();
                                if (aSubElement != null)
                                    try
                                    {
                                        if ((aSubElement.Value == null) || (aSubElement.Value == "%NULL()"))
                                            dataRow[dataColumn.ColumnName] = DBNull.Value;
                                        else
                                            dataRow[dataColumn.ColumnName] = aSubElement.Value.ToObject() ?? DBNull.Value;
                                    }
                                    catch
                                    {
                                    }
                            }
                            dataTable.Rows.Add(dataRow);
                        }

                        dataSet.Merge(dataTable);
                    }
                }

        }

        /// <summary>
        /// Deserializes the specified xml string and puts the result into the
        /// specified data table.
        /// </summary>
        /// <param name="xmlString">The xml string.</param>
        /// <param name="dataSet">The data set to update.</param>
        /// <param name="tableName">The name of the output data table.</param>
        /// <param name="isAppend">Indicates whether the update consists in inserting a new row.</param>
        public static void FromXml(
            this DataSet dataSet,
            String xmlString,
            String tableName,
            bool isAppend = false)
        {
            if (dataSet != null)
                dataSet.FromXml(xmlString, new List<string>() { tableName }, isAppend);
        }

        /// <summary>
        /// Gets the value of the specified Xml node in the specified xml string.
        /// </summary>
        /// <param name="xmlString">The xml string to parse.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <returns>The value of the specified Xml node in the specified xml string.</returns>
        public static string GetXmlNodeValue(
            this String xmlString,
            String nodeName)
        {
            if (xmlString == String.Empty)
                return null;
            IFormatProvider cultureInfo = new CultureInfo("en-US", true);

            XDocument xmlDocument = XDocument.Parse(xmlString, LoadOptions.PreserveWhitespace);
            XElement xmlElement = xmlDocument.Root.Descendants(nodeName)
                .FirstOrDefault(e => string.Equals(e.Name.ToString(), nodeName, StringComparison.OrdinalIgnoreCase));
            if (xmlElement != null)
                return xmlElement.Value;
            return null;
        }

        #endregion

    }
}
