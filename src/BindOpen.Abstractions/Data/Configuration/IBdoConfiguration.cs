using BindOpen.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConfiguration :
        IReferenced, IBdoMetaSet,
        INamed, IGloballyDescribed, IStorable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoConfiguration Add(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        new IBdoConfiguration WithItems(params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        List<string> UsedItemIds { get; set; }
    }
}