using BindOpen.MetaData;
using System.Collections.Generic;

namespace BindOpen.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoExtensionItemGroup :
        ITIdentifiedPoco<IBdoExtensionItemGroup>,
        ITNamedPoco<IBdoExtensionItemGroup>,
        ITGloballyTitledPoco<IBdoExtensionItemGroup>,
        ITGloballyDescribedPoco<IBdoExtensionItemGroup>,
        ITStorablePoco<IBdoExtensionItemGroup>
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