using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Assemblies;

namespace BindOpen.Kernel.Scoping.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEntityDefinition : IBdoExtensionDefinition, IBdoRuntimeTyped
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ITBdoDictionary<string> ViewerDictionary { get; set; }
    }
}