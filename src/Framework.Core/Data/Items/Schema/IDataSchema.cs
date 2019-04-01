using BindOpen.Framework.Core.Data.Elements.Schema;
using BindOpen.Framework.Core.Data.References;

namespace BindOpen.Framework.Core.Data.Items.Schema
{
    public interface IDataSchema : IDescribedDataItem
    {
        DataReference MetaSchemreference { get; set; }
        SchemaZoneElement RootZone { get; set; }

        SchemaElement GetElementWithId(string id, SchemaElement parentMetobject1 = null);
    }
}