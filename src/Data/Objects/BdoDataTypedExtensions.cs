using BindOpen.Data;
using BindOpen.Scoping;
using System;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoDataTypedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithDataType<T>(
            this T dataTyped,
            DataValueTypes valueType)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType(valueType);
            }

            return dataTyped;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataType<T>(
            this T dataTyped,
            Type type)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType(type);
            }

            return dataTyped;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataType<T>(
            this T dataTyped)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType<T>();
            }

            return dataTyped;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithDataType<T>(
            this T dataTyped,
            BdoExtensionKinds definitionExtensionKind,
            string definitionUniqueName = null)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType(definitionExtensionKind, definitionUniqueName);
            }

            return dataTyped;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithNullValue<T>(
            this T dataTyped)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType(DataValueTypes.Null);
            }

            return dataTyped;
        }
    }
}
