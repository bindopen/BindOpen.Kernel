using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data;
using System.Collections.Generic;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionDictionary :
        IIdentified, INamed,
        IBdoTitled, IBdoDescribed,
        IDated
    {

        /// <summary>
        /// The groups.
        /// </summary>
        IList<IBdoExtensionGroup> Groups { get; set; }

        /// <summary>
        /// ID of library.
        /// </summary>
        string LibraryId { get; set; }

        /// <summary>
        /// Name of library.
        /// </summary>
        string LibraryName { get; set; }
    }
}