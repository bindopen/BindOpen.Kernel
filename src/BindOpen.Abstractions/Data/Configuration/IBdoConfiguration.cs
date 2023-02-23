using BindOpen.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// This interface defines a configuration.
    /// </summary>
    public interface IBdoConfiguration :
        IBdoMetaSet,
        IBdoTitled, IBdoDescribed,
        IStorable
    {
        string DefinitionUniqueName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> UsedItemIds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoConfiguration Add(
            params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param key="items"></param>
        /// <returns></returns>
        new IBdoConfiguration With(
            params IBdoMetaData[] items);
    }
}