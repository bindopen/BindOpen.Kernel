using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using BindOpen.Scoping;
using System;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDepot : IIdentified, IBdoScoped
    {
        /// <summary>
        /// Add the items from all the assemblies.
        /// </summary>
        /// <param key="log">The log to append.</param>
        IBdoDepot AddFromAllAssemblies(IBdoLog log = null);

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param key="assemblyName">The name of the assembly.</param>
        /// <param key="log">The log to append.</param>
        IBdoDepot AddFromAssembly(IBdoAssemblyReference reference, IBdoLog log = null);

        /// <summary>
        /// Add the items from the assembly of the specified type.
        /// </summary>
        /// <param key="log">The log to append.</param>
        IBdoDepot AddFromAssembly<T>(IBdoLog log = null) where T : class;

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <param key="log">The log to append.</param>
        bool LoadLazy(IBdoLog log);

        /// <summary>
        /// The initialization function.
        /// </summary>
        Func<IBdoDepot, IBdoLog, int> LazyLoadFunction { get; set; }
    }
}