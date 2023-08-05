using BindOpen.System.Scoping;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoDataTypedExtensions
    {
        public static T WithDataType<T>(
            this T dataTyped,
            string definitionUniqueName)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = new()
                {
                    ValueType = DataValueTypes.Object,
                    ClassReference = BdoData.Class(definitionUniqueName)
                };
            }
            return dataTyped;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T WithDataType<T>(
            this T dataTyped,
            BdoExtensionKind definitionExtensionKind,
            string definitionUniqueName = null)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = new()
                {
                    ValueType = DataValueTypes.Object,
                    ClassReference = BdoData.Class(definitionExtensionKind, definitionUniqueName)
                };
            }
            return dataTyped;
        }
    }
}
