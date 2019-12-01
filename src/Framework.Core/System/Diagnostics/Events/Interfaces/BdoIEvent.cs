using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.System.Diagnostics.Events
{
    /// <summary>
    /// 
    /// </summary>
    public interface BdoIEvent: IDescribedDataItem
    {
        /// <summary>
        /// 
        /// </summary>
        BdoEventCriticality Criticality { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Date { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataElementSet Detail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        EventKinds Kind { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DictionaryDataItem LongDescription { get; set; }
    }
}