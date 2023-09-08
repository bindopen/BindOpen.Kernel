using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Assemblies;
using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Scoping.Tasks
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoTaskDefinition : IBdoExtensionDefinition, IBdoRuntimeTyped
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
        float MaximumIndex { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsExecutable { get; set; }
    }
}