using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using System;

namespace BindOpen.Framework.Core.Data.Depots
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDepot : IIdentifiedDataItem
    {
        /// <summary>
        /// Add the items from all the assemblies.
        /// </summary>
        IBdoLog AddFromAllAssemblies();

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        IBdoLog AddFromAssembly(string assemblyName);

        /// <summary>
        /// Add the items from the assembly of the specified type.
        /// </summary>
        IBdoLog AddFromAssembly<T>() where T : class;

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <param name="log">The log to append.</param>
        void LoadLazy(IBdoLog log);

        /// <summary>
        /// The initialization function.
        /// </summary>
        Func<IBdoDepot, IBdoLog, int> LazyLoadFunction { get; set; }
    }
}