using System;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoDataTypedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithDataType<T>(
            this T dataTyped,
            BdoDataType dataType)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = dataType;
            }

            return dataTyped;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataType<T>(
            this T dataTyped,
            DataValueTypes valueType,
            Type type = null)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = new()
                {
                    ClassType = type,
                    ValueType = type != null ? DataValueTypes.Object : valueType
                };
            }

            return dataTyped;
        }
    }
}
