using BindOpen.Data.Assemblies;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines a data type.
    /// </summary>
    public interface IBdoDataType : IBdoClassReference, IBdoDefinable
    {
        /// <summary>
        /// The value type.
        /// </summary>
        DataValueTypes ValueType { get; set; }

        /// <summary>
        /// Indicates whether this object is compatible with the specified data type.
        /// </summary>
        /// <param name="dataType">The data type to consider.</param>
        /// <returns>Returns True whether this object is compatible.</returns>
        bool IsCompatibleWithType(IBdoDataType dataType);

        /// <summary>
        /// Indicates whether this object is compatible with the specified data.
        /// </summary>
        /// <param name="data">The data to consider.</param>
        /// <returns>Returns True whether this object is compatible.</returns>
        bool IsCompatibleWithData(object data);
    }
}
