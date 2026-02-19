using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class provides factory methods to create complex, conditional schemas 
/// for testing BindOpen's metadata validation and scoping rules.
/// </summary>
public static class BdoScopingSchemaFaker
{
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "Spec.xml";

    /// <summary>
    /// Creates a highly conditional schema structure used to test 
    /// requirement levels, forbidden states, and child satisfaction.
    /// </summary>
    /// <returns>A configured IBdoSchema instance.</returns>
    public static IBdoSchema CreateSchema()
    {
        var schema = BdoData.NewSchema<BdoSchema>()
            // 1. Root Condition: This schema is "Satisfied" if title == 'myTitle'
            .WithCondition(
                BdoScript.NewCondition<IBdoMetaData>(q =>
                    BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("title")._Value(), "myTitle")))

            .WithChildren(
                // 2. Default child schema (fallback)
                BdoData.NewSchema<BdoSchema>("default"),

                // 3. Conditional child: Satisfied if description == 'myDescription'
                BdoData.NewSchema<BdoSchema>()
                    .WithCondition(BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("description")._Value(), "myDescription")),

                // 4. Forbidden Rule: The 'label' schema is forbidden if the parent has an 'auto' property
                BdoData.NewSchema<BdoSchema>("label")
                    .AsForbidden(BdoScript.This<IBdoMetaData>()._Parent()._Has("auto").ToCondition())
            )

            .WithRules(
                // 5. Validation Rules: Constraints on specific properties
                BdoData.NewRequirement(BdoMetaDataProperties.Property("title"), "myTitle")
            )

            // 6. Global Requirements:
            // Required if 'title' property exists; otherwise Optional (fallback).
            .AsRequired(BdoScript.This<IBdoMetaData>()._Has("title").ToCondition())
            .AsOptional()

            // 7. Item-Level Requirements:
            // Child items are Required only if the 'auto' property is True.
            .WithItemRequirement((RequirementLevels.Required,
                BdoScript.NewCondition<IBdoMetaData>(q => q._Descendant("auto")._Value())));

        return schema;
    }
}