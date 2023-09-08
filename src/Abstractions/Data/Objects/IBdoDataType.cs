using BindOpen.Kernel.Data.Assemblies;
using System;

namespace BindOpen.Kernel.Data
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

        bool IsCompatibleWith(IBdoDataType dataType);

        bool IsCompatibleWith(Type type);
    }
}
