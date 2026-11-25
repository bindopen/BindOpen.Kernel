using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Entities;

/// <summary>
/// Contains unit tests for <see cref="IBdoEntity"/> implementations, verifying their instantiation
/// from metadata objects or CLR instances and the bidirectional integrity of property mapping within a scope.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoScope.CreateEntity"/> accurately hydrates strongly-typed entity
/// properties (including scalar metadata properties, enumerations, and primitives) and supports round-trip
/// serialization via <c>ToMeta</c>.
/// </remarks>
[TestFixture]
public class BdoEntityTests
{
    private dynamic _testData;

    /// <summary>
    /// Gets or sets the first test entity instance instantiated via generic factory creation.
    /// </summary>
    public EntityFake _entity1;

    /// <summary>
    /// Gets or sets the second test entity instance instantiated via non-generic factory creation.
    /// </summary>
    public EntityFake _entity2;

    /// <summary>
    /// Gets or sets the third test entity instance round-tripped from a CLR object through metadata conversion.
    /// </summary>
    public EntityFake _entity3;

    /// <summary>
    /// Initializes randomized entity property values using <see cref="BdoEntityFaker"/>
    /// before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoEntityFaker.NewData();
    }

    /// <summary>
    /// Validates that the specified <see cref="EntityFake"/> instance is non-null and that its
    /// <see cref="EntityFake.BoolValue"/>, <see cref="EntityFake.EnumValue"/>, <see cref="EntityFake.IntValue"/>,
    /// and <see cref="EntityFake.StringValue"/> properties match the initialized test dataset.
    /// </summary>
    /// <param name="entity">The entity instance to validate.</param>
    public void AssertFake(EntityFake entity)
    {
        Assert.That(entity, Is.Not.Null, "The entity instance is missing.");

        Assert.Multiple(() =>
        {
            // Verifying specific data types mapped from metadata
            Assert.That(entity.BoolValue?.GetData<bool?>(), Is.EqualTo(_testData.boolValue), "Boolean value mismatch.");
            Assert.That(entity.EnumValue.ToString(), Is.EqualTo(_testData.enumValue.ToString()), "Enumeration value mismatch.");
            Assert.That(entity.IntValue, Is.EqualTo(_testData.intValue), "Integer value mismatch.");
            Assert.That(entity.StringValue, Is.EqualTo(_testData.stringValue), "String value mismatch.");
        });
    }

    /// <summary>
    /// Verifies that a strongly-typed <see cref="EntityFake"/> can be instantiated directly from an
    /// <see cref="IBdoMetaObject"/> using <see cref="IBdoScope.CreateEntity{T}"/>.
    /// </summary>
    [Test]
    public void CreateEntityTest_FromMetaSet()
    {
        // 1. Generate meta-object from fake data
        IBdoMetaObject meta = BdoEntityFaker.NewMetaObject(_testData);

        // 2. Resolve the concrete entity via the Scope
        _entity1 = ScopingTestData.Scope.CreateEntity<EntityFake>(meta);

        AssertFake(_entity1);
    }

    /// <summary>
    /// Verifies that an entity instantiated via the non-generic <see cref="IBdoScope.CreateEntity"/>
    /// method resolves to <see cref="EntityFake"/> with all data properties populated.
    /// </summary>
    [Test]
    public void CreateEntityTest_FromConfig()
    {
        IBdoMetaObject meta = BdoEntityFaker.NewMetaObject(_testData);

        // Resolve using the generic CreateEntity method
        _entity2 = ScopingTestData.Scope.CreateEntity(meta) as EntityFake;

        AssertFake(_entity2);
    }

    /// <summary>
    /// Verifies that a populated <see cref="EntityFake"/> CLR instance can be converted to metadata
    /// via <c>ToMeta</c> and re-instantiated through <see cref="IBdoScope.CreateEntity"/> without data loss.
    /// </summary>
    [Test]
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