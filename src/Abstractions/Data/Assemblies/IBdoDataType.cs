using BindOpen.Data.Assemblies;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data element specification.
    /// </summary>
    public interface IBdoDataType : IBdoClassReference, IBdoDefinable
    {
        /// <summary>
        /// The value type of this instance.
        /// </summary>
        DataValueTypes ValueType { get; set; }

        bool IsCompatibleWithType(IBdoDataType dataType);

        bool IsCompatibleWithData(object data);
    }
}
