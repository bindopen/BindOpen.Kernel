using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Items;

namespace BindOpen.System.Diagnostics.Events
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoEvent : INamedDataItem, IDisplayNamed, IDescribed
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
        string LongDescription { get; set; }
    }
}