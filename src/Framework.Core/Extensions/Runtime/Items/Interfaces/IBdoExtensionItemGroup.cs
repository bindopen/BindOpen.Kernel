using System.Collections.Generic;
using BindOpen.Framework.Data.Items;

namespace BindOpen.Framework.Extensions.Runtime
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