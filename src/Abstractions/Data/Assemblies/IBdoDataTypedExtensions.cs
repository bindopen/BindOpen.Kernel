namespace BindOpen.Data
{
    /// <summary>
    /// This class provides extensions of IBdoDataTyped class.
    /// </summary>
    public static partial class IBdoDataTypedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTyped"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
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
        /// <param name="dataType"></param>
        /// <param key="dataType">The data type to consider.</param>
        /// <returns>True if the specified data type is a scalar.</returns>
        public static bool IsCompatibleWithType(
            this IBdoDataTyped dataTyped,
            IBdoDataType dataType)
        {
            return dataTyped?.DataType.IsCompatibleWithType(dataType) ?? false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataTyped"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsCompatibleWithData(
            this IBdoDataTyped dataTyped,
            object data)
        {
            return dataTyped?.DataType.IsCompatibleWithData(data) ?? false;
        }
    }
}
