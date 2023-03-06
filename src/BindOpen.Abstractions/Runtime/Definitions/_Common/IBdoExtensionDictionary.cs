using BindOpen.Data;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionDictionary :
        IIdentified, INamed,
        IBdoTitled, IBdoDescribed,
        IStorable
    {

        /// <summary>
        /// The groups.
        /// </summary>
        List<IBdoExtensionGroup> Groups { get; set; }

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