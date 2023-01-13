using BindOpen.MetaData.Specification;
using System.Collections.Generic;

namespace BindOpen.MetaData.Elements.Schema
{
    public interface IBdoMetaSchemaSpec : IBdoMetaElementSpec
    {
        RequirementLevels FormatRequirementLevel { get; set; }
        List<SpecificationLevels> FormatSpecificationLevels { get; set; }
        IDataValueFilter FormatUniqueNameFilter { get; set; }
        RequirementLevels SchemaRequirementLevel { get; set; }
        List<SpecificationLevels> SchemaSpecificationLevels { get; set; }
        IDataValueFilter SchemuniqueNameFilter { get; set; }
    }
}