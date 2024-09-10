using BindOpen.Tests;
using BindOpen.Scoping;
using Bogus;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Data.Meta;

[TestFixture, Order(201)]
public class ConfigurationXmlTests
{
    private readonly string _filePath_xml = GlobalTestData.WorkingFolder + "Configuration.xml";

    private BdoConfiguration _config;

    private object _obj1 = null;
    private object _obj2 = null;
    private object _obj3 = null;
    private dynamic _testData;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _obj1 = ClassObjectFaker.Fake();
        _obj2 = ClassObjectFaker.Fake();
        _obj3 = ClassObjectFaker.Fake();

        var f = new Faker();
        _testData = new
        {
            arrayNumber1 = Enumerable.Range(0, 10).Select(p => f.Random.Double()).ToArray(),
            arrayString2 = Enumerable.Range(0, 10).Select(p => f.Random.Word()).ToArray(),
            arrayInteger3 = Enumerable.Range(0, 10).Select(p => f.Random.Int()).ToArray(),
            arrayArrayByte4 = Enumerable.Range(1, 2).Select(p => f.Random.Bytes(5000)).ToArray()
        };
    }

    private void Test(IBdoConfiguration config)
    {
        var obj1 = config.Child("sources");
        var obj2 = config.Child("connectors");
        var obj3 = config.Child("tasks");

        Assert.That(obj1?.Count == 3, "Bad obj element set - Count");
        Assert.That(obj2?.Count == 3 == true, "Bad obj element set - Count");
        Assert.That(obj3?.Count == 3 == true, "Bad obj element set - Count");
        Assert.That(config?.Count == 5, "Bad object element set - Count");
    }

    [Test, Order(1)]
    public void CreateTest()
    {
        var meta1 = BdoData.NewObject("object1", _obj1)
            .WithDataType(BdoExtensionKinds.Connector, "test$sample1");
        var meta2 = BdoData.NewObject("object2", _obj2)
            .WithDataType(BdoExtensionKinds.Entity, "test$sample2");
        var meta3 = BdoData.NewObject("object3", _obj3)
            .WithDataType(BdoExtensionKinds.Task, "test$sample3");

        _config = BdoData.NewConfig("config1",
            BdoData.NewScalar("float1", DataValueTypes.Number, _testData.arrayNumber1 as double[]),
            BdoData.NewScalar("text2", DataValueTypes.Text, _testData.arrayString2 as string[]),
            BdoData.NewScalar("integer3", DataValueTypes.Integer, _testData.arrayInteger3 as int[]),
            BdoData.NewScalar("byteArray4", DataValueTypes.Binary, _testData.arrayArrayByte4 as byte[][]),
            BdoData.NewScalar("float2", DataValueTypes.Number, (_testData.arrayNumber1 as double[])[0]),
            BdoData.NewScalar("float2", DataValueTypes.Number, (_testData.arrayNumber1 as double[])[1])
                .WithReference(BdoData.NewExp("$sampleExp()", BdoExpressionKind.Auto)))
            .WithChildren(
                BdoData.NewConfig("sources", meta1, meta2, meta3),
                BdoData.NewConfig("connectors", meta1, meta2, meta3),
                BdoData.NewConfig("tasks", meta1, meta2, meta3));

        Test(_config);
    }

    [Test, Order(5)]
    public void SaveXmlTest()
    {
        if (_config == null)
        {
            CreateTest();
        }

        var isSaved = _config.ToDto().SaveXml(_filePath_xml);
        Assert.That(isSaved, "Meta list saving failed. ");
    }

    [Test, Order(6)]
    public void LoadXmlTest()
    {
        if (_config == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        var config = XmlHelper.LoadXml<ConfigurationDto>(_filePath_xml).ToPoco();
        Equals(config, _config);
    }
}
