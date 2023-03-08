using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Meta;
using System;

namespace BindOpen.Extensions.Entities
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
        ITBdoSet<IBdoSpec> SpecDetail { get; set; }

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