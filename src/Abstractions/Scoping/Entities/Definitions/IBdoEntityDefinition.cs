using BindOpen.Data;
using BindOpen.Data.Assemblies;

namespace BindOpen.Scoping.Entities
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