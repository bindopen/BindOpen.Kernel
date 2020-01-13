using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Extensions.Definition
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