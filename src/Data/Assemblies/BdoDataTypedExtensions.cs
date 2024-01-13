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

        public static bool IsCompatibleWithType(
            this IBdoDataTyped dataTyped,
            Type type)
        {
            return dataTyped?.DataType.IsCompatibleWithType(type) ?? false;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T AsNullValue<T>(
            this T dataTyped)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType(DataValueTypes.Null);
            }

            return dataTyped;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T AsText<T>(
            this T dataTyped)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType(DataValueTypes.Text);
            }

            return dataTyped;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T AsBinary<T>(
            this T dataTyped)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType(DataValueTypes.Binary);
            }

            return dataTyped;
        }

        public static T AsBoolean<T>(
            this T dataTyped)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType(DataValueTypes.Boolean);
            }

            return dataTyped;
        }

        public static T AsInteger<T>(
            this T dataTyped)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType(DataValueTypes.Integer);
            }

            return dataTyped;
        }

        public static T AsNumber<T>(
            this T dataTyped)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = BdoData.NewDataType(DataValueTypes.Number);
            }

            return dataTyped;
        }
    }
}
