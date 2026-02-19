using BindOpen.Data;
using BindOpen.Data.Meta;
using Bogus;
using System.Dynamic;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class provides factory methods to generate randomized data and meta-objects
/// for testing BdoEntities.
/// </summary>
public static class BdoEntityFaker
{
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "Entity.xml";
    public static readonly string JsonFilePath = DataTestData.WorkingFolder + "Entity.json";

    /// <summary>
    /// Generates a randomized set of entity data using the Bogus library.
    /// </summary>
    /// <returns>A dynamic ExpandoObject containing entity properties.</returns>
    public static dynamic NewData()
    {
        var f = new Faker();
        dynamic b = new ExpandoObject();

        b.boolValue = f.Random.Bool();
        b.intValue = f.Random.Int(1, 800);
        b.enumValue = AccessibilityLevels.Private;
        b.stringValue = f.Lorem.Word();

        return b;
    }

    /// <summary>
    /// Creates a new BdoMetaObject initialized with entity data, including nested nodes.
    /// </summary>
    /// <param name="data">The dynamic data source. If null, fresh data is generated.</param>
    /// <returns>An IBdoMetaObject configured for a test entity.</returns>
    public static IBdoMetaObject NewMetaObject(dynamic data = null)
    {
        data ??= NewData();

        // We define the meta-object as an Entity extension
        var meta = BdoData.NewObject()
            .WithDataType(BdoExtensionKinds.Entity, "scoping.tests$testEntity")
            .With(
                BdoData.NewScalar("boolValue", data.boolValue as bool?),
                BdoData.NewScalar("enumValue", data.enumValue as AccessibilityLevels?),
                BdoData.NewScalar("intValue", data.intValue as int?),
                BdoData.NewScalar("stringValue", data.stringValue as string),

                // Demonstration of a nested MetaNode for grouped properties
                BdoData.NewNode("inputs",
                    BdoData.NewScalar("bool1", data.boolValue as bool?),
                    BdoData.NewScalar("int1", (int)data.intValue))
            );

        return meta;
    }
}