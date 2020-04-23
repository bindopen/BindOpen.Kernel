using BindOpen.Data.Common;
using BindOpen.Data.Items;

namespace BindOpen.Extensions.Definition
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