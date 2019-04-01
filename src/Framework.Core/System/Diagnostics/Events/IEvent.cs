using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Dictionary;

namespace BindOpen.Framework.Core.System.Diagnostics.Events
{
    public interface IEvent: IDescribedDataItem
    {
        string CreationDate { get; set; }
        EventCriticality Criticality { get; set; }
        string Date { get; set; }
        IDataElementSet Detail { get; set; }
        EventKind Kind { get; set; }
        IDictionaryDataItem LongDescription { get; set; }
    }
}