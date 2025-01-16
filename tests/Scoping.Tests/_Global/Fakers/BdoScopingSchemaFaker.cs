using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;

namespace BindOpen.Scoping.Tests;

public static class BdoScopingSchemaFaker
{
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "Spec.xml";

    public static IBdoSchema CreateSchema()
    {
        var schema = BdoData.NewSchema<BdoSchema>()
            .WithCondition(
                BdoScript.NewCondition<IBdoMetaData>(q => BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("title")._Value(), "myTitle")))
            .WithChildren(
                BdoData.NewSchema<BdoSchema>("default"),
                BdoData.NewSchema<BdoSchema>()
                    .WithCondition(BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("description")._Value(), "myDescription")),
                BdoData.NewSchema<BdoSchema>("label")
                    .AsForbidden(BdoScript.This<IBdoMetaData>()._Parent()._Has("auto").ToCondition())
            )
            .WithRules(
                BdoData.NewRequirement(BdoMetaDataProperties.Property("title"), "myTitle")
            )
            .AsRequired(BdoScript.This<IBdoMetaData>()._Has("title").ToCondition())
            .AsOptional()
            .WithItemRequirement((RequirementLevels.Required,
                BdoScript.NewCondition<IBdoMetaData>(q => q._Descendant("auto")._Value())));

        return schema;
    }
}
