using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Items.Dictionary;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.System.Diagnostics.Events
{
    public interface IEvent: IDescribedDataItem
    {
        string CreationDate { get; set; }
        EventCriticality Criticality { get; set; }
        string Date { get; set; }
        DataElementSet Detail { get; set; }
        EventKind Kind { get; set; }
        DictionaryDataItem LongDescription { get; set; }
    }
}