using BindOpen.Kernel.Data;
using System.Collections.Generic;

namespace BindOpen.Kernel.Scoping
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionGroup :
        IIdentified, INamed,
        IBdoTitled, IBdoDescribed,
        IDated
    {
        /// <summary>
        /// 
        /// </summary>
        IList<IBdoExtensionGroup> SubGroups { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        IBdoExtensionGroup GetGroupWithName(string name);
    }
}