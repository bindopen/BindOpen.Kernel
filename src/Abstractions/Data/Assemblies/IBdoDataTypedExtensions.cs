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
        public static T WithDataType<T>(
            this T dataTyped,
            IBdoDataType dataType)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = dataType;
            }

            return dataTyped;
        }

        /// <summary>
        /// Indicates whether the specified data type is a scalar.
        /// </summary>
        /// <param key="dataType">The data type to consider.</param>
        /// <returns>True if the specified data type is a scalar.</returns>
        public static bool IsCompatibleWithType(
            this IBdoDataTyped dataTyped,
            IBdoDataType dataType)
        {
            return dataTyped?.DataType.IsCompatibleWithType(dataType) ?? false;
        }

        public static bool IsCompatibleWithData(
            this IBdoDataTyped dataTyped,
            object data)
        {
            return dataTyped?.DataType.IsCompatibleWithData(data) ?? false;
        }
    }
}
