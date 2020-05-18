using BindOpen.Data.Elements;
using BindOpen.Data.Items;
using BindOpen.Extensions.Runtime;
using System;
using System.Xml.Serialization;

namespace BindOpen.Data.Common
{
    /// <summary>
    /// This enumeration represents the possible data value types.
    /// </summary>
    [XmlType("DataValueTypes", Namespace = "https://docs.bindopen.org/xsd")]
    [Flags]
    public enum DataValueTypes
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// None.
        /// </summary>
        None = 0x01 << 0,

        /// <summary>
        /// Boolean.
        /// </summary>
        Boolean = 0x01 << 1,

        /// <summary>
        /// Data carrier.
        /// </summary>
        Carrier = 0x01 << 2,

        /// <summary>
        /// Connector.
        /// </summary>
        Connector = 0x01 << 3,

        /// <summary>
        /// Data source.
        /// </summary>
        Datasource = 0x01 << 4,

        /// <summary>
        /// Element.
        /// </summary>
        Element = 0x01 << 5,

        /// <summary>
        /// Entity.
        /// </summary>
        Entity = 0x01 << 6,

        /// <summary>
        /// Date.
        /// </summary>
        Date = 0x01 << 7,

        /// <summary>
        /// Document.
        /// </summary>
        Document = 0x01 << 8,

        /// <summary>
        /// Integer.
        /// </summary>
        Integer = 0x01 << 9,

        /// <summary>
        /// Long.
        /// </summary>
        Long = 0x01 << 10,

        /// <summary>
        /// Ultra long.
        /// </summary>
        ULong = 0x01 << 11,

        /// <summary>
        /// Byte array.
        /// </summary>
        ByteArray = 0x01 << 12,

        /// <summary>
        /// Number value.
        /// </summary>
        Number = 0x01 << 13,

        /// <summary>
        /// Object.
        /// </summary>
        Object = 0x01 << 14,

        /// <summary>
        /// Schema.
        /// </summary>
        Schema = 0x01 << 15,

        /// <summary>
        /// Schema zone.
        /// </summary>
        SchemaZone = 0x01 << 16,

        /// <summary>
        /// Text.
        /// </summary>
        Text = 0x01 << 17,

        /// <summary>
        /// Time.
        /// </summary>
        Time = 0x01 << 18,

        /// <summary>
        /// Any data operator.
        /// </summary>
        Any = Boolean | Carrier | Connector | Datasource | Element | Entity | Date | Document | Integer | Long
            | ULong | ByteArray | Number | Object | Schema | SchemaZone | Text | Time

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
        public static bool IsScalar(this DataValueTypes valueType)
        {
            switch (valueType)
            {
                case DataValueTypes.Boolean:
                case DataValueTypes.Date:
                case DataValueTypes.Integer:
                case DataValueTypes.Number:
                case DataValueTypes.Text:
                case DataValueTypes.ByteArray:
                case DataValueTypes.Time:
                case DataValueTypes.Long:
                case DataValueTypes.ULong:
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
        public static Type GetObjectType(this DataValueTypes dataValueType)
        {
            return dataValueType switch
            {
                DataValueTypes.Boolean => typeof(Boolean),
                DataValueTypes.Carrier => typeof(BdoCarrierConfiguration),
                DataValueTypes.Datasource => typeof(Datasource),
                DataValueTypes.Date => typeof(DateTime),
                DataValueTypes.Document => typeof(Document),
                DataValueTypes.Integer => typeof(int),
                DataValueTypes.Number => typeof(float),
                DataValueTypes.Schema => typeof(String),
                DataValueTypes.Text => typeof(String),
                DataValueTypes.Time => typeof(TimeSpan),
                DataValueTypes.Long => typeof(long),
                DataValueTypes.ULong => typeof(ulong),
                DataValueTypes.ByteArray => typeof(byte[]),
                _ => typeof(Object),
            };
        }

        /// <summary>
        /// Returns the value type of the specified object.
        /// </summary>
        /// <param name="valueType">The value type to consider.</param>
        /// <param name="refValueType">The value type to consider.</param>
        /// <returns>The result object.</returns>
        public static bool IsCompatibleWith(this DataValueTypes valueType, DataValueTypes refValueType)
        {
            if (valueType == refValueType)
            {
                return true;
            }
            else if (refValueType == DataValueTypes.Number
                && (valueType == DataValueTypes.Integer
                || valueType == DataValueTypes.Long))
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
        public static DataValueTypes GetValueType(this Type type)
        {
            if (type == null) return DataValueTypes.None;

            if (type == typeof(byte[]))
                return DataValueTypes.ByteArray;
            else if (type.IsArray)
                type = type.GetElementType();

            if (type == typeof(bool) || type == typeof(bool?))
                return DataValueTypes.Boolean;
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
                return DataValueTypes.Date;
            else if (type == typeof(int) || type == typeof(int?))
                return DataValueTypes.Integer;
            else if (type == typeof(float) || type == typeof(float?) || (type == typeof(double)) || (type == typeof(double?)))
                return DataValueTypes.Number;
            else if (type.IsEnum)
                return DataValueTypes.Text;
            else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
                return DataValueTypes.Time;
            else if (type == typeof(string))
                return DataValueTypes.Text;
            else if (typeof(IDocument).IsAssignableFrom(type))
                return DataValueTypes.Document;
            else if (typeof(IBdoCarrier).IsAssignableFrom(type) || typeof(IBdoCarrierConfiguration).IsAssignableFrom(type))
                return DataValueTypes.Carrier;
            else if (typeof(IDatasource).IsAssignableFrom(type) || typeof(IBdoConnectorConfiguration).IsAssignableFrom(type))
                return DataValueTypes.Datasource;
            else if (typeof(ICollectionElement).IsAssignableFrom(type))
                return DataValueTypes.Element;
            else if (typeof(IDataItem).IsAssignableFrom(type))
                return DataValueTypes.Object;
            else if (type == typeof(long) || type == typeof(long?))
                return DataValueTypes.Long;
            else if (type == typeof(ulong) || type == typeof(ulong?))
                return DataValueTypes.ULong;
            else
                return DataValueTypes.None;
        }

        /// <summary>
        /// Returns the value type of the specified object.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        /// <returns>The result object.</returns>
        public static DataValueTypes GetValueType(this Object object1)
        {
            var objectType = object1?.GetType();
            return objectType == null ? DataValueTypes.None : objectType.GetValueType();
        }

        /// <summary>
        /// Returns the value type of the specified object.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <returns>The result object.</returns>
        public static DataValueTypes GetValueType(this object[] objects)
        {
            DataValueTypes dataValueType = DataValueTypes.Any;

            if (objects != null)
            {
                foreach (object object1 in objects)
                {
                    DataValueTypes currentDataValueType = DataValueTypeExtension.GetValueType(object1);
                    if ((dataValueType != DataValueTypes.Any) && (currentDataValueType != dataValueType))
                        return DataValueTypes.Any;
                    else
                        dataValueType = currentDataValueType;
                }
            }

            return dataValueType;
        }
    }

    #endregion
}
