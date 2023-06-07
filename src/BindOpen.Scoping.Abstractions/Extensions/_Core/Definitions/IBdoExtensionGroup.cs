using BindOpen.Scoping.Data;
using System.Collections.Generic;

namespace BindOpen.Scoping.Extensions
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
        IList<IBdoExtensionGroup> SubGroups { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="name"></param>
        /// <returns></returns>
        IBdoExtensionGroup GetGroupWithName(string name);
    }
}