using BindOpen.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Data.Schema;

[TestFixture, Order(201)]
public class SchemaXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "Schema.xml";

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

    [Test, Order(5)]
    public void SaveXmlTest()
    {
        var isSaved = _schema.ToDto().SaveXml(_filePath_xml);
        Assert.That(isSaved, "Meta list saving failed. ");
    }

    [Test, Order(6)]
    public void LoadXmlTest()
    {
        if (_schema == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        var schema = XmlHelper.LoadXml<SchemaDto>(_filePath_xml).ToPoco();
        Equals(schema, _schema);
    }
}
