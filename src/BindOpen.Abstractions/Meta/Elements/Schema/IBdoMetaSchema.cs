using System.Collections.Generic;

namespace BindOpen.Meta.Elements.Schema
{
    public interface IBdoMetaSchema : IBdoMetaElement
    {
        string EntityUniqueName { get; set; }
        string ImageFileName { get; set; }
        IBdoMetaSchemaZone ParentZone { get; set; }
        IBdoMetaSchemaSpec Specification { get; set; }

        void AddElement(IBdoMetaSchemaZone parentZoneElement, IBdoMetaSchemaZone parent, params string[] areas);
        IBdoMetaSchema CreateElement(IBdoMetaSchemaZone parentZoneElement);
        IBdoMetaSchemaZone CreateSchemaZoneElement(IBdoMetaSchemaZone parentZoneElement);
        bool DeleteElement(IBdoMetaSchema element);
        void DeleteElements(List<IBdoMetaSchema> elements);
        bool IsDescendantOf(IBdoMetaSchemaZone parentZoneElement);
        bool MoveElement(IBdoMetaSchema aElement, IBdoMetaSchemaZone parentZoneElement);
        void MoveElements(List<IBdoMetaSchema> elements, IBdoMetaSchemaZone parentZoneElement);
        new IBdoMetaSchemaSpec NewSpecification();
    }
}