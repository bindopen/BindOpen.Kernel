using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionDefinition : IBdoDefinition
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
        /// Name of the group of this instance.
        /// </summary>
        string GroupId { get; set; }

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