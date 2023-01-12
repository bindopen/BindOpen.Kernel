using BindOpen.Meta;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionItemDefinition :
        ITIdentifiedPoco<IBdoExtensionItemDefinition>,
        ITNamedPoco<IBdoExtensionItemDefinition>,
        ITGloballyTitledPoco<IBdoExtensionItemDefinition>,
        ITGloballyDescribedPoco<IBdoExtensionItemDefinition>,
        ITIndexedPoco<IBdoExtensionItemDefinition>,
        ITStorablePoco<IBdoExtensionItemDefinition>,
        IReferenced
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoExtensionDefinition ExtensionDefinition { get; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary> 
        string UniqueId { get; }

        /// <summary>
        /// 
        /// </summary>
        string ImageUri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsEditable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsIndexed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string LibraryId { get; set; }
    }
}