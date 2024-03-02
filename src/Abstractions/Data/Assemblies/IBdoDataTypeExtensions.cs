using BindOpen.Scoping;

namespace BindOpen.Data
{
    /// <summary>
    /// This class provides extensions of IBdoDataType class.
    /// </summary>
    public static class IBdoDataTypeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataType"></param>
        /// <param name="valueType"></param>
        /// <returns></returns>
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
        /// <typeparam name="T"></typeparam>
        /// <param name="dataType"></param>
        /// <param name="definitionExtensionKind"></param>
        /// <param name="definitionUniqueName"></param>
        /// <returns></returns>
        public static T WithDefinition<T>(
            this T dataType,
            BdoExtensionKinds definitionExtensionKind,
            string definitionUniqueName = null)
            where T : IBdoDataType
        {
            if (dataType != null)
            {
                dataType.ValueType = definitionExtensionKind.GetValueType();

                if (definitionUniqueName != null)
                {
                    dataType.WithDefinition(definitionUniqueName);
                }
            }
            return dataType;
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
