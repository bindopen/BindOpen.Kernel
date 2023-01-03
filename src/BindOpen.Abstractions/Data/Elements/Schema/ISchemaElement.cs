using System.Collections.Generic;

namespace BindOpen.Data.Elements.Schema
{
    public interface ISchemaElement : IBdoElement
    {
        string EntityUniqueName { get; set; }
        string ImageFileName { get; set; }
        ISchemaZoneElement ParentZone { get; set; }
        ISchemaElementSpec Specification { get; set; }

        void AddElement(ISchemaZoneElement parentZoneElement, ISchemaZoneElement parent, params string[] areas);
        ISchemaElement CreateElement(ISchemaZoneElement parentZoneElement);
        ISchemaZoneElement CreateSchemaZoneElement(ISchemaZoneElement parentZoneElement);
        bool DeleteElement(ISchemaElement element);
        void DeleteElements(List<ISchemaElement> elements);
        bool IsDescendantOf(ISchemaZoneElement parentZoneElement);
        bool MoveElement(ISchemaElement aElement, ISchemaZoneElement parentZoneElement);
        void MoveElements(List<ISchemaElement> elements, ISchemaZoneElement parentZoneElement);
        new ISchemaElementSpec NewSpecification();
    }
}