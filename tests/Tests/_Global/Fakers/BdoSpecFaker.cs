﻿using BindOpen.Data;
using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Script;

namespace BindOpen.Kernel.Tests
{
    public static class BdoSpecFaker
    {
        public static readonly string XmlFilePath = SystemData.WorkingFolder + "Spec.xml";

        public static IBdoNodeSpec CreateSpecWithReference()
        {
            var spec = BdoData.NewSpec<BdoSpec>()
                .WithReference(BdoData.NewRef(BdoScript.Eq(1, 0)));

            return spec;
        }

        public static IBdoNodeSpec CreateSpec()
        {
            var spec = BdoData.NewSpec<BdoSpec>()
                .WithCondition((BdoExpression)BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("title")._Value(), "myTitle"))
                .WithProperties(BdoData.NewSpec("stringValue", DataValueTypes.Text))
                .WithChildren(
                    BdoData.NewSpec<BdoSpec>("default"),
                    BdoData.NewSpec<BdoSpec>()
                        .WithCondition((BdoExpression)BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("description")._Value(), "myDescription"))
                )
                .WithRules(
                    BdoData.NewRequirement(BdoMetaDataProperties.Property("title"), "myTitle")
                )
                .AsRequired((BdoCondition)BdoScript.This<IBdoMetaData>()._Has("title"))
                .AsOptional()
                .WithItemRequirement((RequirementLevels.Required,
                    (BdoCondition)BdoScript.This<IBdoMetaData>()._Descendant("auto")._Value()));

            return spec;
        }
    }
}
