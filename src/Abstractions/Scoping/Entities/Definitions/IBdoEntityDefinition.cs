using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;

namespace BindOpen.System.Scoping
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