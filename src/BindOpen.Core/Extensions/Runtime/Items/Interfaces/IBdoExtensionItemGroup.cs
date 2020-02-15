using System.Collections.Generic;
using BindOpen.Data.Items;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionItemGroup : IDescribedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<BdoExtensionItemGroup> SubGroups { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoExtensionItemGroup GetGroupWithName(string name);
    }
}