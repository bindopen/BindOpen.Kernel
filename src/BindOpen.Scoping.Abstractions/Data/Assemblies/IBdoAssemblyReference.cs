using System;

namespace BindOpen.Scoping.Data.Assemblies
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoAssemblyReference :
        IBdoObject, IReferenced, IBdoDefinable
    {
        /// <summary>
        /// The library name of this instance.
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        Version AssemblyVersion { get; }

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        string AssemblyFileName { get; set; }

        bool IsEmpty();

        bool IsCompatibleWith(IBdoAssemblyReference reference);
    }
}