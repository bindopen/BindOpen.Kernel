namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a data typed object.
    /// </summary>
    public interface IBdoDataTyped
    {
        /// <summary>
        /// The data type.
        /// </summary>
        IBdoDataType DataType { get; set; }
    }
}