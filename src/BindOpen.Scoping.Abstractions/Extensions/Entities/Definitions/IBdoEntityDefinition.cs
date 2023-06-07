using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Assemblies;
using System;

namespace BindOpen.Scoping.Extensions.Entities
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
        IBdoDictionary ViewerDictionary { get; set; }
    }
}