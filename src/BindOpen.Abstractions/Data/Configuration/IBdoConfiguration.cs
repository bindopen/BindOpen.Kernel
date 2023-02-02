using BindOpen.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConfiguration :
        IBdoMetaSet,
        INamed, IReferenced,
        IGloballyTitled, IGloballyDescribed,
        IStorable
    {
        /// <summary>
        /// 
        /// </summary>
        string DefinitionUniqueId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> UsedItemIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoConfiguration Add(
            params IBdoMetaItem[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoConfiguration WithItems(
            params IBdoMetaItem[] items);
    }
}