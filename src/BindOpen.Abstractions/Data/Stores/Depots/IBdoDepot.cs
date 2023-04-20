using BindOpen.Scopes;
using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using System;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDepot :
        IIdentified, ITBdoScoped<IBdoDepot>
    {
        /// <summary>
        /// Add the items from all the assemblies.
        /// </summary>
        /// <param key="log">The log to append.</param>
        IBdoDepot AddFromAllAssemblies(IBdoBaseLog log = null);

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param key="assemblyName">The name of the assembly.</param>
        /// <param key="log">The log to append.</param>
        IBdoDepot AddFromAssembly(IBdoAssemblyReference reference, IBdoBaseLog log = null);

        /// <summary>
        /// Add the items from the assembly of the specified type.
        /// </summary>
        /// <param key="log">The log to append.</param>
        IBdoDepot AddFromAssembly<T>(IBdoBaseLog log = null) where T : class;

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <param key="log">The log to append.</param>
        void LoadLazy(IBdoBaseLog log);

        /// <summary>
        /// The initialization function.
        /// </summary>
        Func<IBdoDepot, IBdoBaseLog, int> LazyLoadFunction { get; set; }
    }
}