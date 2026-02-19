using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Entities;

/// <summary>
/// This class provides unit tests for BdoEntities, verifying their instantiation 
/// from various metadata sources and the integrity of data mapping via the Scope.
/// </summary>
[TestFixture, Order(300)]
public class BdoEntityTests
{
    private dynamic _testData;

    public EntityFake _entity1;
    public EntityFake _entity2;
    public EntityFake _entity3;

    /// <summary>
    /// Initializes the randomized test data using the entity faker.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoEntityFaker.NewData();
    }

    /// <summary>
    /// Standardized assertion logic to verify the properties of a fake entity.
    /// </summary>
    /// <param name="entity">The entity instance to validate.</param>
    public void AssertFake(EntityFake entity)
    {
        Assert.That(entity, Is.Not.Null, "The entity instance is missing.");

        // Verifying specific data types mapped from metadata
        Assert.That(entity.BoolValue?.GetData<bool?>(), Is.EqualTo(_testData.boolValue), "Boolean value mismatch.");
        Assert.That(entity.EnumValue.ToString(), Is.EqualTo(_testData.enumValue.ToString()), "Enumeration value mismatch.");
        Assert.That(entity.IntValue, Is.EqualTo(_testData.intValue), "Integer value mismatch.");
        Assert.That(entity.StringValue, Is.EqualTo(_testData.stringValue), "String value mismatch.");
    }

    /// <summary>
    /// Tests the creation of an entity directly from a meta-object using a typed factory method.
    /// </summary>
    [Test, Order(1)]
    public void CreateEntityTest_FromMetaSet()
    {
        // 1. Generate meta-object from fake data
        IBdoMetaObject meta = BdoEntityFaker.NewMetaObject(_testData);

        // 2. Resolve the concrete entity via the Scope
        _entity1 = ScopingTestData.Scope.CreateEntity<EntityFake>(meta);

        AssertFake(_entity1);
    }

    /// <summary>
    /// Tests the creation of an entity from a generic meta-object using type casting.
    /// </summary>
    [Test, Order(2)]
    public void CreateEntityTest_FromConfig()
    {
        IBdoMetaObject meta = BdoEntityFaker.NewMetaObject(_testData);

        // Resolve using the generic CreateEntity method
        _entity2 = ScopingTestData.Scope.CreateEntity(meta) as EntityFake;

        AssertFake(_entity2);
    }

    /// <summary>
    /// Tests the conversion of a POCO entity to metadata and its subsequent 
    /// re-instantiation via the scoping engine.
    /// </summary>
    [Test, Order(3)]
    public void CreateEntityTest_FromObject()
    {
        // 1. Manually create a POCO instance
        var entity = new EntityFake
        {
            BoolValue = BdoData.NewScalar<bool?>(_testData.boolValue as bool?),
            EnumValue = (AccessibilityLevels)_testData.enumValue,
            IntValue = (int)_testData.intValue,
            StringValue = _testData.stringValue as string
        };

        // 2. Extract metadata from the POCO using the Scope's reflection extensions
        var config = entity.ToMeta(ScopingTestData.Scope);

        // 3. Re-create the entity from that metadata to verify bidirectional mapping
        _entity3 = ScopingTestData.Scope.CreateEntity(config) as EntityFake;

        AssertFake(_entity3);
    }
}