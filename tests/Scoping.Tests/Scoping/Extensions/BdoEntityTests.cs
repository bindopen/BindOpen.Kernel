using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Entities;

[TestFixture, Order(300)]
public class BdoEntityTests
{
    private dynamic _testData;

    public EntityFake _entity1;
    public EntityFake _entity2;
    public EntityFake _entity3;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoEntityFaker.NewData();
    }

    public void AssertFake(EntityFake entity)
    {
        Assert.That(entity != null, "Entity missing");

        Assert.That(entity.BoolValue?.GetData<bool?>() == _testData.boolValue, "Bad entity - Boolean value");
        Assert.That(entity.EnumValue.ToString() == _testData.enumValue.ToString(), "Bad entity - Enumeration value");
        Assert.That(entity.IntValue == _testData.intValue, "Bad entity - Integer value");
        Assert.That(entity.StringValue == _testData.stringValue, "Bad entity - String value");
    }

    [Test, Order(1)]
    public void CreateEntityTest_FromMetaSet()
    {
        IBdoMetaObject meta = BdoEntityFaker.NewMetaObject(_testData);
        _entity1 = ScopingTestData.Scope.CreateEntity<EntityFake>(meta);

        AssertFake(_entity1);
    }

    [Test, Order(2)]
    public void CreateEntityTest_FromConfig()
    {
        IBdoMetaObject meta = BdoEntityFaker.NewMetaObject(_testData);
        _entity2 = ScopingTestData.Scope.CreateEntity(meta) as EntityFake;

        AssertFake(_entity2);
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

        var config = entity.ToMeta(ScopingTestData.Scope);
        _entity3 = ScopingTestData.Scope.CreateEntity(config) as EntityFake;

        AssertFake(_entity3);
    }
}
