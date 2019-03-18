using BindOpen.Framework.Core.Data.Elements.Schema;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Items.Documents;
using BindOpen.Framework.Core.Data.Items.Source;
using BindOpen.Framework.Core.Data.Items.Strings;
using BindOpen.Framework.Core.Extensions.Configuration.Carriers;
using BindOpen.Framework.Core.Extensions.Configuration.Entities;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Data.Common
{

    /// <summary>
    /// This enumeration represents the possible data value types.
    /// </summary>
    [Serializable()]
    [XmlType("DataValueType", Namespace = "http://meltingsoft.com/bindopen/xsd")]
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
        CarrierConfiguration,
        /// <summary>
        /// Data source.
        /// </summary>
        DataSource,
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
        /// Entity.
        /// </summary>
        Entity,
        /// <summary>
        /// Integer.
        /// </summary>
        Integer,
        /// <summary>
        /// Long.
        /// </summary>
        Long,
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
        public static Boolean IsScalar(this DataValueType valueType)
        {
            switch (valueType)
            {
                case DataValueType.Boolean:
                case DataValueType.Date:
                case DataValueType.Integer:
                case DataValueType.Number:
                case DataValueType.Text:
                case DataValueType.Time:
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the specified value type corresponds to a scalar.
        /// </summary>
        /// <param name="object1">The object to consider.</param>
        /// <returns>The result object.</returns>
        public static Boolean IsScalar(this Object object1)
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
                case DataValueType.CarrierConfiguration:
                    return typeof(CarrierConfiguration);
                case DataValueType.DataSource:
                    return typeof(DataSource);
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
                case DataValueType.StringValued:
                    return typeof(StringValuedDataItem);
                case DataValueType.Text:
                    return typeof(String);
                case DataValueType.Time:
                    return typeof(TimeSpan);
            }

            return typeof(Object);
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
            else if (type.IsSubclassOf(typeof(Document)))
                return DataValueType.Document;
            else if (type.IsSubclassOf(typeof(CarrierConfiguration)))
                return DataValueType.CarrierConfiguration;
            else if (type.IsSubclassOf(typeof(DataSource)))
                return DataValueType.DataSource;
            else if (type == typeof(SchemaElement))
                return DataValueType.Schema;
            else if (type == typeof(SchemaZoneElement))
                return DataValueType.SchemaZone;
            else if (type == typeof(StringValuedDataItem))
                return DataValueType.StringValued;
            else if  (type.IsSubclassOf(typeof(EntityConfiguration)))
                return DataValueType.Entity;
            else if (type.IsSubclassOf(typeof(DataItem)))
                return DataValueType.Object;
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
            return (object1 == null ? DataValueType.None : object1.GetType().GetValueType());
        }

        /// <summary>
        /// Returns the value type of the specified object.
        /// </summary>
        /// <param name="objects">The objects to consider.</param>
        /// <returns>The result object.</returns>
        public static DataValueType GetValueType(this List<Object> objects)
        {
            DataValueType dataValueType = DataValueType.Any;
            foreach (Object object1 in objects)
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
