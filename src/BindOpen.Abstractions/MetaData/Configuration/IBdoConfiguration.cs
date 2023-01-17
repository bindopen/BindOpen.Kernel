using BindOpen.MetaData.Elements;
using System.Collections.Generic;

namespace BindOpen.MetaData.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConfiguration :
        IReferenced, IBdoMetaElementSet,
        ITNamedPoco<IBdoConfiguration>,
        ITGloballyDescribedPoco<IBdoConfiguration>,
        ITStorablePoco<IBdoConfiguration>
    {
        /// <summary>
        /// 
        /// </summary>
        List<string> UsedItemIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        IBdoConfiguration Using(params string[] ids);
    }
}