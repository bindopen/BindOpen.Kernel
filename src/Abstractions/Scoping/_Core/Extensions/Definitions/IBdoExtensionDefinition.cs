using BindOpen.Data;
using BindOpen.Data.Schema;

namespace BindOpen.Scoping
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionDefinition : IBdoDefinition, IGrouped
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoPackageDefinition PackageDefinition { get; set; }

        /// <summary>
        /// The unique ID of this instance.
        /// </summary> 
        string UniqueName { get; }

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