using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using System;

namespace BindOpen.System.Data.Assemblies
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoClassReference : IBdoAssemblyReference
    {
        /// <summary>
        /// The library name of this instance.
        /// </summary>
        string ClassName { get; set; }

        Type GetRuntimeType(IBdoScope scope = null, IBdoLog log = null);
    }
}