using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
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