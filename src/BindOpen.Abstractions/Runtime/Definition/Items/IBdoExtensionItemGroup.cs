using BindOpen.Data;
using System.Collections.Generic;

namespace BindOpen.Runtime.Definition
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionItemGroup :
        IIdentified, INamed,
        IGloballyTitled, IGloballyDescribed,
        IStorable
    {
        /// <summary>
        /// 
        /// </summary>
        List<IBdoExtensionItemGroup> SubGroups { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoExtensionItemGroup GetGroupWithName(string name);
    }
}