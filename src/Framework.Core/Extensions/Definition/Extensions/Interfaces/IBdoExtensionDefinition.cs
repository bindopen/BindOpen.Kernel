using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dto;

namespace BindOpen.Framework.Core.Extensions.Definition.Extensions
{
    /// <summary>
    /// This interface defines the library definition.
    /// </summary>
    public interface IBdoExtensionDefinition : IDataItem, IReferenced
    {
        /// <summary>
        /// The item of this instance.
        /// </summary>
        IBdoExtensionDefinitionDto Dto { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary> 
        string UniqueId { get; }
    }
}