using BindOpen.Data.Schema;
using BindOpen.Scoping.Script;
using BindOpen.Scoping.Tests;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for scoping-related metadata operations,
/// including script reference resolution, dynamic label generation, and conditional evaluations.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoMetaData"/> elements properly interact with an
/// <see cref="IBdoScope"/> to resolve variable references, format labels from templates,
/// and evaluate existence conditions against schemas and metadata values.
/// </remarks>
[TestFixture]
public class BdoScopingMetaDataTests
{
    private object _obj;
    private IBdoSchema _schema;

    /// <summary>
    /// Initializes mock object and schema instances before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initialize test data and schema from fakers
        _obj = ClassObjectFaker.Fake();
        _schema = BdoScopingSchemaFaker.CreateSchema();
    }

    /// <summary>
    /// Verifies that an <see cref="IBdoMetaObject"/> configured with a <see cref="BdoScript.Var"/>
    /// reference resolves its underlying data from the provided variable set within a scope.
    /// </summary>
    [Test]
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
    /// Verifies automatic label script generation using <see cref="LabelFormats.NameColonValue"/>
    /// and subsequent evaluation of the formatted label string within a scope.
    /// </summary>
    [Test]
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
    /// Verifies conditional existence evaluation (<c>GetConditionValue</c>) on metadata sets
    /// using both inline <see cref="BdoScript"/> conditions and <see cref="IBdoSchema"/> rules.
    /// </summary>
    [Test]
    public void ConditionTest()
    {
        // 1. Basic test with a non-matching condition (should evaluate to false)
        var meta0 = BdoData.NewSet("meta-test")
            .WithCondition(
                BdoScript.NewCondition<IBdoMetaData>(q =>
                    BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("title")._Value(), "myTitle")))
            .Add(BdoData.NewScalar("title", "A"));

        var existence0 = meta0.GetConditionValue(ScopingTestData.Scope);
        Assert.That(existence0, Is.False, "The basic condition check failed.");

        // 2. Test with matching condition and valid data
        var meta1 = BdoData.NewSet("meta-test")
            .WithCondition(
                BdoScript.NewCondition<IBdoMetaData>(q =>
                    BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("title")._Value(), "myTitle")))
            .Add(BdoData.NewScalar("title", "myTitle"));

        var existence1 = meta1.GetConditionValue(ScopingTestData.Scope);
        Assert.That(existence1, Is.True, "The schema-based existence check failed for valid data.");

        // 3. Test with full schema but invalid data (Condition should fail)
        // Note: This assumes _schema has a rule stating 'title' cannot be 'A'
        var meta2 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .Add(BdoData.NewScalar("title", "A"));

        var existence2 = meta2.GetConditionValue(ScopingTestData.Scope);
        Assert.That(existence2, Is.False, "The condition check should have failed for 'title=A'.");
    }
}