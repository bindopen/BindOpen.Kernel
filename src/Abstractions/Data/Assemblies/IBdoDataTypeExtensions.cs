using BindOpen.Scoping;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoDataTypedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithValueType<T>(
            this T dataType,
            DataValueTypes valueType)
            where T : IBdoDataType
        {
            if (dataType != null)
            {
                dataType.ValueType = valueType;
            }

            return dataType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T WithDefinition<T>(
            this T obj,
            BdoExtensionKinds definitionExtensionKind,
            string definitionUniqueName = null)
            where T : IBdoDataType
        {
            if (obj != null)
            {
                obj.ValueType = definitionExtensionKind.GetValueType();

                if (definitionUniqueName != null)
                {
                    obj.WithDefinition(definitionUniqueName);
                }
            }
            return obj;
        }

        /// <summary>
        /// Indicates whether the specified data type is a scalar.
        /// </summary>
        /// <param key="dataType">The data type to consider.</param>
        /// <returns>True if the specified data type is a scalar.</returns>
        public static bool IsScalar(this IBdoDataType dataType)
        {
            return dataType?.ValueType.IsScalar() ?? false;
        }
    }
}
