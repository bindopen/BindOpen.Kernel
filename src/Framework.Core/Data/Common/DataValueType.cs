using BindOpen.Framework.Data.Elements;
using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Extensions.Runtime;
using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Data.Common
{
    /// <summary>
    /// This enumeration represents the possible data value types.
    /// </summary>
    [Serializable()]
    [XmlType("DataValueType", Namespace = "https://bindopen.org/xsd")]
    public enum DataValueType
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Any,

        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Boolean.
        /// </summary>
        Boolean,

        /// <summary>
        /// Data carrier.
        /// </summary>
        Carrier,

        /// <summary>
        /// Data source.
        /// </summary>
        Datasource,

        /// <summary>
        /// Element.
        /// </summary>
        Element,

        /// <summary>
        /// Date.
        /// </summary>
        Date,

        /// <summary>
        /// Dictionary.
        /// </summary>
        Dictionary,

        /// <summary>
        /// Document.
        /// </summary>
        Document,

        /// <summary>
        /// Integer.
        /// </summary>
        Integer,

        /// <summary>
        /// Long.
        /// </summary>
        Long,

        /// <summary>
        /// Ultra long.
        /// </summary>
        ULong,

        /// <summary>
        /// Number value.
        /// </summary>
        Number,

        /// <summary>
        /// Object.
        /// </summary>
        Object,

        /// <summary>
        /// Schema.
        /// </summary>
        Schema,

        /// <summary>
        /// Schema zone.
        /// </summary>
        SchemaZone,

        /// <summary>
        /// Schema.
        /// </summary>
        StringValued,

        /// <summary>
        /// Text.
        /// </summary>
        Text,

        /// <summary>
        /// Time.
        /// </summary>
        Time,
    }


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension of the DataValueType enumeration.
    /// </summary>
    public static class DataValueTypeExtension
    {
        /// <summary>
        /// Indicates whether the specified value type corresponds to a scalar.
        /// </summary>
        /// <param name="valueType">The object to consider.</param>
        /// <returns>The result object.</returns>
        public static bool IsScalar(this DataValueType valueType)
        {
            switch (valueType)
            {
                case DataValueType.Boolean:
                case DataValueType.Date:
                case DataValueType.Integer:
                case DataValueType.Number:
                case DataValueType.Text:
                case DataValueType.Time:
                case DataValueType.Long:
                case DataValueType.ULong:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the specified value type corresponds to a scalar.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        /// <returns>The result object.</returns>
        public static bool IsScalar(this Object object1)
        {
            return object1.GetValueType().IsScalar();
        }

        /// <summary>
        /// Returns the object type of the specified data value type.
        /// </summary>
        /// <param name="dataValueType">The value type to consider.</param>
        /// <returns>The result object.</returns>
        public static Type GetObjectType(this DataValueType dataValueType)
        {
            switch (dataValueType)
            {
                case DataValueType.Boolean:
                    return typeof(Boolean);
                case DataValueType.Carrier:
                    return typeof(BdoCarrierConfiguration);
                case DataValueType.Datasource:
                    return typeof(Datasource);
                case DataValueType.Date:
                    return typeof(DateTime);
                case DataValueType.Dictionary:
                    return typeof(DictionaryDataItem);
                case DataValueType.Document:
                    return typeof(Document);
                case DataValueType.Integer:
                    return typeof(int);
                case DataValueType.Number:
                    return typeof(float);
                case DataValueType.Schema:
                    return typeof(String);
                case DataValueType.Text:
                    return typeof(String);
                case DataValueType.Time:
                    return typeof(TimeSpan);
                case DataValueType.Long:
                    return typeof(long);
                case DataValueType.ULong:
                    return typeof(ulong);
            }

            return typeof(Object);
        }

        /// <summary>
        /// Returns the value type of the specified object.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="refValueType">The value type to consider.</param>
        /// <returns>The result object.</returns>
        public static bool IsCompatibleWith(this DataValueType valueType, DataValueType refValueType)
        {
            if (valueType == refValueType)
            {
                return true;
            }
            else if (refValueType == DataValueType.Number
                && (valueType == DataValueType.Integer
                || valueType == DataValueType.Long))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns the value type of the specified object.
        /// </summary>
        /// <param name="type">The type to consider.</param>
        /// <returns>The result object.</returns>
        public static DataValueType GetValueType(this Type type)
        {
            if (type == null) return DataValueType.None;

            if (type.IsArray)
                type = type.GetElementType();

            if (type == typeof(Boolean) || type == typeof(Boolean?))
                return DataValueType.Boolean;
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
                return DataValueType.Date;
            else if (type == typeof(int) || type == typeof(int?))
                return DataValueType.Integer;
            else if (type == typeof(float) || type == typeof(float?) || (type == typeof(double)) || (type == typeof(double?)))
                return DataValueType.Number;
            else if (type.IsEnum)
                return DataValueType.Text;
            else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
                return DataValueType.Time;
            else if (type == typeof(String))
                return DataValueType.Text;
            else if (type == typeof(DictionaryDataItem))
                return DataValueType.Dictionary;
            else if (typeof(IDocument).IsAssignableFrom(type))
                return DataValueType.Document;
            else if (typeof(IBdoCarrier).IsAssignableFrom(type) || typeof(IBdoCarrierConfiguration).IsAssignableFrom(type))
                return DataValueType.Carrier;
            else if (typeof(IDatasource).IsAssignableFrom(type) || typeof(IBdoConnectorConfiguration).IsAssignableFrom(type))
                return DataValueType.Datasource;
            else if (typeof(ICollectionElement).IsAssignableFrom(type))
                return DataValueType.Element;
            else if (typeof(IDataItem).IsAssignableFrom(type))
                return DataValueType.Object;
            else if (type == typeof(long) || type == typeof(long?))
                return DataValueType.Long;
            else if (type == typeof(ulong) || type == typeof(ulong?))
                return DataValueType.ULong;
            else
                return DataValueType.None;
        }

        /// <summary>
        /// Returns the value type of the specified object.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        /// <returns>The result object.</returns>
        public static DataValueType GetValueType(this Object object1)
        {
            var objectType = object1?.GetType();
            return objectType == null ? DataValueType.None : objectType.GetValueType();
        }

        /// <summary>
        /// Returns the value type of the specified object.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <returns>The result object.</returns>
        public static DataValueType GetValueType(this object[] objects)
        {
            DataValueType dataValueType = DataValueType.Any;
            foreach (object object1 in objects)
            {
                DataValueType currentDataValueType = DataValueTypeExtension.GetValueType(object1);
                if ((dataValueType != DataValueType.Any) && (currentDataValueType != dataValueType))
                    return DataValueType.Any;
                else
                    dataValueType = currentDataValueType;
            }

            return dataValueType;
        }
    }

    #endregion
}
