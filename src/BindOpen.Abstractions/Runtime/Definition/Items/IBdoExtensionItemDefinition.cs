using BindOpen.Data;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionItemDefinition :
        IIdentified, INamed,
        IGloballyTitled, IGloballyDescribed,
        IIndexed, IStorable, IReferenced
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