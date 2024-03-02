using BindOpen.Logging;
using BindOpen.Scoping;
using System;

namespace BindOpen.Data.Assemblies
{
    /// <summary>
    /// This interface defines a class reference.
    /// </summary>
    public interface IBdoClassReference : IBdoAssemblyReference
    {
        /// <summary>
        /// The class name.
        /// </summary>
        string ClassName { get; set; }

        /// <summary>
        /// Gets the runtime type of this object.
        /// </summary>
        /// <param name="scope">The BindOpen scope to consider.</param>
        /// <param name="log">The BindOpen log to consider.</param>
        /// <returns>Returns the runtime type.</returns>
        Type GetRuntimeType(IBdoScope scope = null, IBdoLog log = null);
    }
}