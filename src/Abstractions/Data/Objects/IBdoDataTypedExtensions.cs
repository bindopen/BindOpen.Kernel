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
            IBdoDataType dataType)
            where T : IBdoDataTyped
        {
            if (dataTyped != null)
            {
                dataTyped.DataType = dataType;
            }

            return dataTyped;
        }
    }
}
