using System.Collections.Generic;

namespace BindOpen.Data.Elements.Schema
{
    public interface ISchemaZoneElement
    {
        List<ISchemaElement> SubElements { get; set; }

        void AddSubElement(ISchemaElement aSchemaElement);
        void BuildTree();
        object Clone(ISchemaZoneElement parent, params string[] areas);
        ISchemaElement GetElementWithId(string id, ISchemaElement parentSchemaElement = null);
    }
}