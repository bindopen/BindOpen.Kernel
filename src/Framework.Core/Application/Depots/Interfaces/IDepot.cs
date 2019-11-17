using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Application.Depots
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDepot
    {
        /// <summary>
        /// Add the items from all the assemblies.
        /// </summary>
        ILog AddFromAllAssemblies();

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        ILog AddFromAssembly(string assemblyName);

        /// <summary>
        /// Add the items from the assembly of the specified type.
        /// </summary>
        ILog AddFromAssembly<T>() where T : class;
    }
}