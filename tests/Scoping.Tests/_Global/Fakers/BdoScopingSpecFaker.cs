using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;

namespace BindOpen.Tests;

public static class BdoScopingSpecFaker
{
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "Spec.xml";

    public static IBdoSpec CreateSpec()
    {
        var spec = BdoData.NewSpec<BdoSpec>()
            .WithCondition(
                BdoScript.NewCondition<IBdoMetaData>(q => BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("title")._Value(), "myTitle")))
            .WithChildren(
                BdoData.NewSpec<BdoSpec>("default"),
                BdoData.NewSpec<BdoSpec>()
                    .WithCondition(BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("description")._Value(), "myDescription")),
                BdoData.NewSpec<BdoSpec>("label")
                    .AsForbidden(BdoScript.This<IBdoMetaData>()._Parent()._Has("auto").ToCondition())
            )
            .WithRules(
                BdoData.NewRequirement(BdoMetaDataProperties.Property("title"), "myTitle")
            )
            .AsRequired(BdoScript.This<IBdoMetaData>()._Has("title").ToCondition())
            .AsOptional()
            .WithItemRequirement((RequirementLevels.Required,
                BdoScript.NewCondition<IBdoMetaData>(q => q._Descendant("auto")._Value())));

        return spec;
    }
}
