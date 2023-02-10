using BindOpen.Data.Meta;
using System.Collections.Generic;

namespace BindOpen.Data.Configuration
{
    /// <summary>
    /// This interface defines a configuration.
    /// </summary>
    public interface IBdoConfiguration :
        IBdoMetaList,
        INamed, IReferenced,
        IGloballyTitled, IGloballyDescribed,
        IStorable
    {
        /// <summary>
        /// The unique ID of the definition.
        /// </summary>
        string DefinitionUniqueName { get; set; }

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
            params IBdoMetaData[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoConfiguration With(
            params IBdoMetaData[] items);
    }
}