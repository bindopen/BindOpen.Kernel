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
            this T spec,
            BdoDataType dataType)
            where T : IBdoDataTyped
        {
            if (spec != null)
            {
                spec.DataType = dataType;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataType<T>(
            this T spec,
            DataValueTypes valueType,
            Type type = null)
            where T : IBdoDataTyped
        {
            if (spec != null)
            {
                spec.DataType = new()
                {
                    ClassType = type,
                    ValueType = type != null ? DataValueTypes.Object : valueType
                };
            }

            return spec;
        }
    }
}
