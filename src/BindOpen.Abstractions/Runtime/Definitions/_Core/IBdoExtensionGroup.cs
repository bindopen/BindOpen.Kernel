using BindOpen.Data;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definitions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionGroup :
        IIdentified, INamed,
        IBdoTitled, IBdoDescribed,
        IStorable
    {
        /// <summary>
        /// 
        /// </summary>
        List<IBdoExtensionGroup> SubGroups { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        IBdoExtensionGroup GetGroupWithName(string name);
    }
}