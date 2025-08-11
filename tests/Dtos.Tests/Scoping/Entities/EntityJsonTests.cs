using BindOpen.Data;
using BindOpen.Data.Meta;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping.Entities;

[TestFixture, Order(210)]
public class EntityJsonTests
{
    private readonly string _filePath_json = DataTestData.WorkingFolder + "Entity{0}.json";
    private BdoEntityTests _dataTests;
    private bool _isSaved1 = false;
    private bool _isSaved2 = false;
    private bool _isSaved3 = false;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_json);

        _dataTests = new BdoEntityTests();
        _dataTests.OneTimeSetUp();
    }

    // Test 1

    [Test, Order(10)]
    public void SaveJson1Test()
    {
        _dataTests.CreateEntityTest_FromMetaSet();
        var entity = _dataTests._entity1;

        var filePath = string.Format(_filePath_json, 1);

        var dto = entity.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved1 = dto.SaveJson(filePath);
        Assert.That(_isSaved1, "Entity saving failed");
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
        var entity = _dataTests._entity1;

        _dataTests.AssertFake(entity);
    }

    // Test 2

    [Test, Order(20)]
    public void SaveJson2Test()
    {
        _dataTests.CreateEntityTest_FromConfig();
        var entity = _dataTests._entity2;

        var filePath = string.Format(_filePath_json, 2);

        var dto = entity.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved2 = dto.SaveJson(filePath);
        Assert.That(_isSaved2, "Entity saving failed");
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
        var entity = _dataTests._entity1;

        _dataTests.AssertFake(entity);
    }

    // Test 3

    [Test, Order(30)]
    public void SaveJson3Test()
    {
        _dataTests.CreateEntityTest_FromObject();
        var entity = _dataTests._entity3;

        var filePath = string.Format(_filePath_json, 3);

        var dto = entity.ToMeta(ScopingTestData.Scope).ToDto();
        _isSaved3 = dto.SaveJson(filePath);
        Assert.That(_isSaved3, "Entity saving failed");
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
        var entity = _dataTests._entity1;

        _dataTests.AssertFake(entity);
    }
}
