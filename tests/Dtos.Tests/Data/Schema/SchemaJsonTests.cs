using BindOpen.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Schema;

[TestFixture, Order(201)]
public class SchemaJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "Schema.json";

    private IBdoSchema _schema;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _schema = BdoSchemaFaker.CreateSpecWithReference();
    }

    public static bool Equals(
        IBdoSchema spec1,
        IBdoSchema spec2)
    {
        var b = spec1 != null && spec2 != null
            && spec1.IsDeepEqual(spec2);
        return b;
    }

    [Test, Order(7)]
    public void SaveJsonTest()
    {
        var isSaved = _schema.ToDto().SaveJson(_filePath_json);
        Assert.That(isSaved, "Meta list saving failed. ");
    }

    [Test, Order(8)]
    public void LoadJsonTest()
    {
        if (_schema == null || !File.Exists(_filePath_json))
        {
            SaveJsonTest();
        }

        var schema = JsonHelper.LoadJson<SchemaDto>(_filePath_json).ToPoco();
        Equals(schema, _schema);
    }
}
