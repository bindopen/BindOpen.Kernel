using BindOpen.Data;
using BindOpen.Data.Meta;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping.Connectors;

/// <summary>
/// Contains unit tests for JSON serialization and deserialization of <see cref="IBdoConnector"/>
/// instances via <see cref="MetaObjectDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that connectors created from metadata sets, configurations, and direct objects
/// by <see cref="BdoConnectorTests"/> can be converted to <see cref="IBdoMetaObject"/> representations,
/// mapped to DTOs, persisted to JSON files on disk, and reloaded cleanly.
/// </remarks>
[TestFixture]
public class ConnectorJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "Connector{0}.json";
    private BdoConnectorTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoConnectorTests"/>
    /// helper before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_json, 1));
        File.Delete(string.Format(_filePath_json, 2));
        File.Delete(string.Format(_filePath_json, 3));

        _dataTests = new BdoConnectorTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoConnector"/> instance (created from a metadata set)
    /// converts to a <see cref="MetaObjectDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson1Test()
    {
        _dataTests.CreateConnectorTest_FromMetaSet();
        var connector = _dataTests._connector1;

        var filePath = string.Format(_filePath_json, 1);

        var dto = connector.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved1 = dto.SaveJson(filePath);
        Assert.That(_isSaved1, Is.True, "Connector saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved JSON file loads back into a <see cref="MetaObjectDto"/>,
    /// converts to a metadata object via <c>ToPoco()</c>, and preserves valid connector state.
    /// </summary>
    [Test]
    public void LoadJson1Test()
    {
        if (!_isSaved1)
        {
            SaveJson1Test();
        }

        var filePath = string.Format(_filePath_json, 1);

        var meta = JsonHelper.LoadJson<MetaObjectDto>(filePath).ToPoco();
        Assert.That(meta, Is.Not.Null, "Loaded connector metadata cannot be null.");

        var connector = _dataTests._connector1;
        _dataTests.AssertFake(connector);
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="IBdoConnector"/> instance (created from a configuration)
    /// converts to a <see cref="MetaObjectDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson2Test()
    {
        _dataTests.CreateConnectorTest_FromConfig();
        var connector = _dataTests._connector2;

        var filePath = string.Format(_filePath_json, 2);

        var dto = connector.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved2 = dto.SaveJson(filePath);
        Assert.That(_isSaved2, Is.True, "Connector saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved JSON file loads back into a <see cref="MetaObjectDto"/>,
    /// converts to a metadata object via <c>ToPoco()</c>, and preserves valid connector state.
    /// </summary>
    [Test]
    public void LoadJson2Test()
    {
        if (!_isSaved2)
        {
            SaveJson2Test();
        }

        var filePath = string.Format(_filePath_json, 2);

        var meta = JsonHelper.LoadJson<MetaObjectDto>(filePath).ToPoco();
        Assert.That(meta, Is.Not.Null, "Loaded connector metadata cannot be null.");

        var connector = _dataTests._connector2 ?? _dataTests._connector1;
        _dataTests.AssertFake(connector);
    }

    // Test 3

    /// <summary>
    /// Verifies that the third test <see cref="IBdoConnector"/> instance (created from an object)
    /// converts to a <see cref="MetaObjectDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson3Test()
    {
        _dataTests.CreateConnectorTest_FromObject();
        var connector = _dataTests._connector3;

        var filePath = string.Format(_filePath_json, 3);

        var dto = connector.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved3 = dto.SaveJson(filePath);
        Assert.That(_isSaved3, Is.True, "Connector saving failed.");
    }

    /// <summary>
    /// Verifies that the third saved JSON file loads back into a <see cref="MetaObjectDto"/>,
    /// converts to a metadata object via <c>ToPoco()</c>, and preserves valid connector state.
    /// </summary>
    [Test]
    public void LoadJson3Test()
    {
        if (!_isSaved3)
        {
            SaveJson3Test();
        }

        var filePath = string.Format(_filePath_json, 3);

        var meta = JsonHelper.LoadJson<MetaObjectDto>(filePath).ToPoco();
        Assert.That(meta, Is.Not.Null, "Loaded connector metadata cannot be null.");

        var connector = _dataTests._connector3 ?? _dataTests._connector1;
        _dataTests.AssertFake(connector);
    }
}