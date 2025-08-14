using BindOpen.Data;
using BindOpen.Data.Meta;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping.Connectors;

[TestFixture, Order(210)]
public class ConnectorJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "Connector{0}.json";
    private BdoConnectorTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        _dataTests = new BdoConnectorTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    [Test, Order(10)]
    public void SaveJson1Test()
    {
        _dataTests.CreateConnectorTest_FromMetaSet();
        var connector = _dataTests._connector1;

        var filePath = string.Format(_filePath_json, 1);

        var dto = connector.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved1 = dto.SaveJson(filePath);
        Assert.That(_isSaved1, "Connector saving failed");
    }

    [Test, Order(11)]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var meta = JsonHelper.LoadJson<MetaObjectDto>(filePath).ToPoco();
        var connector = _dataTests._connector1;

        _dataTests.AssertFake(connector);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveJson2Test()
    {
        _dataTests.CreateConnectorTest_FromConfig();
        var connector = _dataTests._connector2;

        var filePath = string.Format(_filePath_json, 2);

        var dto = connector.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved2 = dto.SaveJson(filePath);
        Assert.That(_isSaved2, "Connector saving failed");
    }

    [Test, Order(21)]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var meta = JsonHelper.LoadJson<MetaObjectDto>(filePath).ToPoco();
        var connector = _dataTests._connector1;

        _dataTests.AssertFake(connector);
    }

    // Test 3

    [Test, Order(30)]
    public void SaveJson3Test()
    {
        _dataTests.CreateConnectorTest_FromObject();
        var connector = _dataTests._connector3;

        var filePath = string.Format(_filePath_json, 3);

        var dto = connector.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved3 = dto.SaveJson(filePath);
        Assert.That(_isSaved3, "Connector saving failed");
    }

    [Test, Order(31)]
    public void LoadJson3Test()
    {
        if (!_isSaved3)
        {
            SaveJson3Test();
        }

        var filePath = string.Format(_filePath_json, 3);

        var meta = JsonHelper.LoadJson<MetaObjectDto>(filePath).ToPoco();
        var connector = _dataTests._connector1;

        _dataTests.AssertFake(connector);
    }
}
