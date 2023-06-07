using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Assemblies;
using BindOpen.Scoping.Data.Meta;
using System;

namespace BindOpen.Scoping.Extensions.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoTaskDefinition : IBdoExtensionDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ITBdoSet<IBdoSpec> OutputSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Type RuntimeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        float MaximumIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsExecutable { get; set; }
    }
}