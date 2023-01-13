using System.Collections.Generic;

namespace BindOpen.MetaData.Elements.Schema
{
    public interface IBdoMetaSchemaZone
    {
        List<IBdoMetaSchema> SubElements { get; set; }

        void AddSubElement(IBdoMetaSchema aSchemaElement);
        void BuildTree();
        object Clone(IBdoMetaSchemaZone parent, params string[] areas);
        IBdoMetaSchema GetElementWithId(string id, IBdoMetaSchema parentSchemaElement = null);
    }
}