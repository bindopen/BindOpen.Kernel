using BindOpen.Data;
using BindOpen.Data.Meta;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping.Connectors;

[TestFixture, Order(210)]
public class ConnectorXmlTests
{
    private readonly string _filePath_xml = DataTestData.WorkingFolder + "Connector{0}.xml";
    private BdoConnectorTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);

        _dataTests = new BdoConnectorTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    [Test, Order(10)]
    public void SaveXml1Test()
    {
        _dataTests.CreateConnectorTest_FromMetaSet();
        var connector = _dataTests._connector1;

        var filePath = string.Format(_filePath_xml, 1);

        var dto = connector.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved1 = dto.SaveXml(filePath);
        Assert.That(_isSaved1, "Connector saving failed");
    }

    [Test, Order(11)]
    public void LoadXml1Test()
    {
        if (!_isSaved1)
        {
            SaveXml1Test();
        }

        var filePath = string.Format(_filePath_xml, 1);

        var meta = XmlHelper.LoadXml<MetaObjectDto>(filePath).ToPoco();
        var connector = _dataTests._connector1;

        _dataTests.AssertFake(connector);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveXml2Test()
    {
        _dataTests.CreateConnectorTest_FromConfig();
        var connector = _dataTests._connector2;

        var filePath = string.Format(_filePath_xml, 2);

        var dto = connector.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved2 = dto.SaveXml(filePath);
        Assert.That(_isSaved2, "Connector saving failed");
    }

    [Test, Order(21)]
    public void LoadXml2Test()
    {
        if (!_isSaved2)
        {
            SaveXml2Test();
        }

        var filePath = string.Format(_filePath_xml, 2);

        var meta = XmlHelper.LoadXml<MetaObjectDto>(filePath).ToPoco();
        var connector = _dataTests._connector1;

        _dataTests.AssertFake(connector);
    }

    // Test 3

    [Test, Order(30)]
    public void SaveXml3Test()
    {
        _dataTests.CreateConnectorTest_FromObject();
        var connector = _dataTests._connector3;

        var filePath = string.Format(_filePath_xml, 3);

        var dto = connector.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved3 = dto.SaveXml(filePath);
        Assert.That(_isSaved3, "Connector saving failed");
    }

    [Test, Order(31)]
    public void LoadXml3Test()
    {
        if (!_isSaved3)
        {
            SaveXml3Test();
        }

        var filePath = string.Format(_filePath_xml, 3);

        var meta = XmlHelper.LoadXml<MetaObjectDto>(filePath).ToPoco();
        var connector = _dataTests._connector1;

        _dataTests.AssertFake(connector);
    }
}
