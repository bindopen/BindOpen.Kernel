using BindOpen.Data;
using BindOpen.Data.Meta;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping.Entities;

/// <summary>
/// Contains unit tests for JSON serialization and deserialization of <see cref="IBdoEntity"/>
/// instances via <see cref="MetaObjectDto"/> conversion.
/// </summary>
/// <remarks>
/// These tests verify that entities created from metadata sets, configurations, and direct objects
/// by <see cref="BdoEntityTests"/> can be converted to <see cref="IBdoMetaObject"/> representations,
/// mapped to DTOs, persisted to JSON files on disk, and reloaded cleanly.
/// </remarks>
[TestFixture]
public class EntityJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "Entity{0}.json";
    private BdoEntityTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    /// <summary>
    /// Cleans up previous test output files and initializes the shared <see cref="BdoEntityTests"/>
    /// helper before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(string.Format(_filePath_json, 1));
        File.Delete(string.Format(_filePath_json, 2));
        File.Delete(string.Format(_filePath_json, 3));

        _dataTests = new BdoEntityTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    /// <summary>
    /// Verifies that the first test <see cref="IBdoEntity"/> instance (created from a metadata set)
    /// converts to a <see cref="MetaObjectDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson1Test()
    {
        _dataTests.CreateEntityTest_FromMetaSet();
        var entity = _dataTests._entity1;

        var filePath = string.Format(_filePath_json, 1);

        var dto = entity.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved1 = dto.SaveJson(filePath);
        Assert.That(_isSaved1, Is.True, "Entity saving failed.");
    }

    /// <summary>
    /// Verifies that the first saved JSON file loads back into a <see cref="MetaObjectDto"/>,
    /// converts to a metadata object via <c>ToPoco()</c>, and preserves valid entity state.
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
        Assert.That(meta, Is.Not.Null, "Loaded entity metadata cannot be null.");

        var entity = _dataTests._entity1;
        _dataTests.AssertFake(entity);
    }

    // Test 2

    /// <summary>
    /// Verifies that the second test <see cref="IBdoEntity"/> instance (created from a configuration)
    /// converts to a <see cref="MetaObjectDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson2Test()
    {
        _dataTests.CreateEntityTest_FromConfig();
        var entity = _dataTests._entity2;

        var filePath = string.Format(_filePath_json, 2);

        var dto = entity.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved2 = dto.SaveJson(filePath);
        Assert.That(_isSaved2, Is.True, "Entity saving failed.");
    }

    /// <summary>
    /// Verifies that the second saved JSON file loads back into a <see cref="MetaObjectDto"/>,
    /// converts to a metadata object via <c>ToPoco()</c>, and preserves valid entity state.
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
        Assert.That(meta, Is.Not.Null, "Loaded entity metadata cannot be null.");

        var entity = _dataTests._entity2 ?? _dataTests._entity1;
        _dataTests.AssertFake(entity);
    }

    // Test 3

    /// <summary>
    /// Verifies that the third test <see cref="IBdoEntity"/> instance (created from an object)
    /// converts to a <see cref="MetaObjectDto"/> and saves cleanly to a JSON file.
    /// </summary>
    [Test]
    public void SaveJson3Test()
    {
        _dataTests.CreateEntityTest_FromObject();
        var entity = _dataTests._entity3;

        var filePath = string.Format(_filePath_json, 3);

        var dto = entity.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved3 = dto.SaveJson(filePath);
        Assert.That(_isSaved3, Is.True, "Entity saving failed.");
    }

    /// <summary>
    /// Verifies that the third saved JSON file loads back into a <see cref="MetaObjectDto"/>,
    /// converts to a metadata object via <c>ToPoco()</c>, and preserves valid entity state.
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
        Assert.That(meta, Is.Not.Null, "Loaded entity metadata cannot be null.");

        var entity = _dataTests._entity3 ?? _dataTests._entity1;
        _dataTests.AssertFake(entity);
    }
}