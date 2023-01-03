using BindOpen.Data.Specification;
using System.Collections.Generic;

namespace BindOpen.Data.Elements.Schema
{
    public interface ISchemaElementSpec : IBdoElementSpec
    {
        RequirementLevels FormatRequirementLevel { get; set; }
        List<SpecificationLevels> FormatSpecificationLevels { get; set; }
        IDataValueFilter FormatUniqueNameFilter { get; set; }
        RequirementLevels SchemaRequirementLevel { get; set; }
        List<SpecificationLevels> SchemaSpecificationLevels { get; set; }
        IDataValueFilter SchemuniqueNameFilter { get; set; }
    }
}