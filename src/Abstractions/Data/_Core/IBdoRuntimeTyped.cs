using System;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This interface defines an object that defines a runtimee type.
    /// </summary>
    public interface IBdoRuntimeTyped
    {
        /// <summary>
        /// The runtime type of this object.
        /// </summary>
        Type RuntimeType { get; set; }
    }
}
