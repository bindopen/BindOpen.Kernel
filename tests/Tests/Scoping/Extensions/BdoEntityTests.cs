using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Entities;

[TestFixture, Order(300)]
public class BdoEntityTests
{
    private dynamic _testData;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoEntityFaker.NewData();
    }

    [Test, Order(1)]
    public void CreateEntityTest_FromMetaSet()
    {
        IBdoMetaObject meta = BdoEntityFaker.NewMetaObject(_testData);
        var entity = GlobalTestData.Scope.CreateEntity<EntityFake>(meta);

        BdoEntityFaker.AssertFake(entity, _testData);
    }

    [Test, Order(2)]
    public void CreateEntityTest_FromConfig()
    {
        IBdoMetaObject meta = BdoEntityFaker.NewMetaObject(_testData);
        var entity = GlobalTestData.Scope.CreateEntity(meta) as EntityFake;

        BdoEntityFaker.AssertFake(entity, _testData);
    }

    [Test, Order(3)]
    public void CreateEntityTest_FromObject()
    {
        var entity = new EntityFake
        {
            BoolValue = BdoData.NewScalar<bool?>(_testData.boolValue as bool?),
            EnumValue = (AccessibilityLevels)_testData.enumValue,
            IntValue = (int)_testData.intValue,
            StringValue = _testData.stringValue as string
        };

        var config = entity.ToMeta(GlobalTestData.Scope);
        entity = GlobalTestData.Scope.CreateEntity(config) as EntityFake;

        BdoEntityFaker.AssertFake(entity, _testData);
    }
}
