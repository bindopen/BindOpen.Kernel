using BindOpen.Data;
using System.Collections.Generic;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConfiguration :
        IReferenced, IBdoElementSet,
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