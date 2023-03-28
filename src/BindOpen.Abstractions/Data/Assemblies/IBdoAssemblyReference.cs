﻿namespace BindOpen.Data.Assemblies
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoAssemblyReference :
        IBdoItem, IReferenced, IBdoDefinable
    {
        /// <summary>
        /// The library name of this instance.
        /// </summary>
        string AssemblyName { get; }

        /// <summary>
        /// The library version of this instance.
        /// </summary>
        string AssemblyVersion { get; }

        /// <summary>
        /// The file name of this instance.
        /// </summary>
        string AssemblyFileName { get; set; }

        bool IsEmpty();
    }
}