using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using BindOpen.Scoping.Tests;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// This class provides unit tests for scoping-related metadata operations, 
/// including references, labels, and conditional evaluations.
/// </summary>
[TestFixture, Order(201)]
public class BdoScopingMetaDataTests
{
    private object _obj;
    private IBdoSchema _schema;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initialize test data and schema from fakers
        _obj = ClassObjectFaker.Fake();
        _schema = BdoScopingSchemaFaker.CreateSchema();
    }

    /// <summary>
    /// Tests the ability to resolve data through a script reference within a meta-object.
    /// </summary>
    [Test, Order(1)]
    public void ReferenceTest()
    {
        var entity = BdoData.New<EntityFake>();
        var varSet = BdoData.NewSet(("entity", entity));

        var meta1 = BdoData.NewObject()
            .WithReference(BdoScript.Var("entity"));

        // Retrieve data using the scope and variable set
        var value1 = meta1.GetData(ScopingTestData.Scope, varSet);

        Assert.That(value1, Is.EqualTo(entity), "The meta-data reference resolution failed.");
    }

    /// <summary>
    /// Tests the automatic label generation using predefined formats and script-based templates.
    /// </summary>
    [Test, Order(2)]
    public void LabelTest()
    {
        var meta1 = BdoData.NewScalar("toto", 23);

        // Setting label format (Name:Value template)
        meta1.WithLabel(LabelFormats.NameColonValue);

        // Verify that the specification has been automatically populated with the correct script
        Assert.That(meta1.GetOrAddSpec().Label, Is.EqualTo("{{$(this).(name)}}:{{$(this).value()}}"), "The label template generation is incorrect.");

        // Evaluate the label within the test scope
        var label = meta1.GetLabel(ScopingTestData.Scope);
        Assert.That(label, Is.EqualTo("toto:23"), "The evaluated label string is incorrect.");
    }

    /// <summary>
    /// Tests the conditional evaluation of metadata nodes based on their schema and data content.
    /// </summary>
    [Test, Order(3)]
    public void ConditionTest()
    {
        // 1. Basic test with an empty schema (should be valid/existent by default)
        var meta0 = BdoData.NewSet("meta-test")
            .WithCondition(
                BdoScript.NewCondition<IBdoMetaData>(q =>
                    BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("title")._Value(), "myTitle")))
            .With(BdoData.NewScalar("title", "A"));

        var existence0 = meta0.GetConditionValue(ScopingTestData.Scope);
        Assert.That(existence0, Is.False, "The basic condition check failed.");

        // 2. Test with full schema and valid data
        var meta1 = BdoData.NewSet("meta-test")
            .WithCondition(
                BdoScript.NewCondition<IBdoMetaData>(q =>
                    BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("title")._Value(), "myTitle")))
            .With(BdoData.NewScalar("title", "myTitle"));

        var existence1 = meta1.GetConditionValue(ScopingTestData.Scope);
        Assert.That(existence1, Is.True, "The schema-based existence check failed for valid data.");

        // 3. Test with full schema but invalid data (Condition should fail)
        // Note: This assumes _schema has a rule stating 'title' cannot be 'A'
        var meta2 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .With(BdoData.NewScalar("title", "A"));

        var existence2 = meta2.GetConditionValue(ScopingTestData.Scope);
        Assert.That(existence2, Is.False, "The condition check should have failed for 'title=A'.");
    }
}