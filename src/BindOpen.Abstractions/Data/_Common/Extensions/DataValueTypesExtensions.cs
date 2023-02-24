using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using System;
using System.Collections;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents an extension of the DataValueType enumeration.
    /// </summary>
    public static class DataValueTypesExtensions
    {
        /// <summary>
        /// Indicates whether the specified value type corresponds to a scalar.
        /// </summary>
        /// <param key="valueType">The object to consider.</param>
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
                default:
                    break;
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the specified value type corresponds to a scalar.
        /// </summary>
        /// <param key="obj">The object to consider.</param>
        /// <returns>The result object.</returns>
        public static bool IsScalar(this object obj)
        {
            return obj.GetValueType().IsScalar();
        }

        public static bool IsScalar(this Type type)
        {
            var valueType = type?.GetValueType() ?? DataValueTypes.None;
            return valueType.IsScalar();
        }

        /// <summary>
        /// Returns the value type of the specified object.
        /// </summary>
        /// <param key="valueType">The value type to consider.</param>
        /// <param key="refValueType">The value type to consider.</param>
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
        /// <param key="type">The type to consider.</param>
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
            else if (type == typeof(float) || type == typeof(float?) || type == typeof(double) || type == typeof(double?))
                return DataValueTypes.Number;
            else if (type.IsEnum)
                return DataValueTypes.Text;
            else if (type == typeof(TimeSpan) || type == typeof(TimeSpan?))
                return DataValueTypes.Time;
            else if (type == typeof(string))
                return DataValueTypes.Text;
            else if (typeof(IBdoMetaData).IsAssignableFrom(type))
                return DataValueTypes.MetaData;
            else if (typeof(IBdoDocument).IsAssignableFrom(type))
                return DataValueTypes.Document;
            else if (typeof(IBdoEntity).IsAssignableFrom(type))
                return DataValueTypes.Entity;
            else if (typeof(IBdoTask).IsAssignableFrom(type))
                return DataValueTypes.Task;
            else if (typeof(IBdoConnector).IsAssignableFrom(type))
                return DataValueTypes.Connector;
            else if (type == typeof(long) || type == typeof(long?))
                return DataValueTypes.Long;
            else if (type == typeof(ulong) || type == typeof(ulong?))
                return DataValueTypes.ULong;
            else
                return DataValueTypes.Object;
        }

        /// <summary>
        /// Returns the value type of the specified object.
        /// </summary>
        /// <param key="obj">The object to consider.</param>
        /// <param key="guessRowCount">The number of items used to guess the object's value type if it is a list.</param>
        /// <returns>The result object.</returns>
        public static DataValueTypes GetValueType(
            this object obj,
            int guessRowCount = 20)
        {
            if (obj is IEnumerable arr)
            {
                var valueType = DataValueTypes.Any;

                int i = 0;
                foreach (var o in arr)
                {
                    if (i == guessRowCount) break;

                    var currValueType = o?.GetType().GetValueType() ?? DataValueTypes.None;
                    if (valueType != DataValueTypes.Any && currValueType != valueType)
                    {
                        return DataValueTypes.Any;
                    }
                    valueType = currValueType;

                    i++;
                }

                return valueType;
            }

            return obj?.GetType().GetValueType() ?? DataValueTypes.None;
        }
    }
}
