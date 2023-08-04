using BindOpen.System.Data;
using BindOpen.System.Data.Assemblies;
using System;

namespace BindOpen.System.Scoping
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEntityDefinition : IBdoExtensionDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ITBdoDictionary<string> ViewerDictionary { get; set; }
    }
}