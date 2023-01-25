using BindOpen.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConfiguration :
        IReferenced, ITBdoMetaSet<IBdoConfiguration>,
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