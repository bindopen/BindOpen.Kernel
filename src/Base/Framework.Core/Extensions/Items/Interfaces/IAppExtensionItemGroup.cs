using System.Collections.Generic;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.Extensions.Items
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppExtensionItemGroup : IDescribedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<AppExtensionItemGroup> SubGroups { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IAppExtensionItemGroup GetGroupWithName(string name);
    }
}