using System;
using BindOpen.Kernel.Data;

namespace BindOpen.Kernel.Data.Assemblies
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoAssemblyReference :
        IBdoObject, IReferenced
    {
        /// <summary>
        /// The library name of this instance.
        /// </summary>
        string AssemblyName { get; set; }

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        Version AssemblyVersion { get; set; }

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        string AssemblyFileName { get; set; }

        bool IsEmpty();
    }
}