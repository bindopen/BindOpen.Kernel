using BindOpen.Data;
using BindOpen.Data.Meta;
using Bogus;
using System.Dynamic;

namespace BindOpen.Scoping.Tests;

public static class BdoEntityFaker
{
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "Entity.xml";
    public static readonly string JsonFilePath = DataTestData.WorkingFolder + "Entity.json";

    public static dynamic NewData()
    {
        var f = new Faker();
        dynamic b = new ExpandoObject();
        b.boolValue = f.Random.Bool();
        b.intValue = f.Random.Int(800);
        b.enumValue = AccessibilityLevels.Private;
        b.stringValue = f.Lorem.Word();
        return b;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param key="data"></param>
    /// <returns></returns>
    public static IBdoMetaObject NewMetaObject(dynamic data = null)
    {
        data ??= NewData();

        var meta =
            BdoData.NewObject()
            .WithDataType(BdoExtensionKinds.Entity, "scoping.tests$testEntity")
            .With(
                BdoData.NewScalar("boolValue", data.boolValue as bool?),
                BdoData.NewScalar("enumValue", data.enumValue as AccessibilityLevels?),
                BdoData.NewScalar("intValue", data.intValue as int?),
                BdoData.NewScalar("stringValue", data.stringValue as string),
                BdoData.NewNode("inputs",
                    ("bool1", data.boolValue as bool?),
                    ("int1", (int)data.intValue)));

        return meta;
    }
}
