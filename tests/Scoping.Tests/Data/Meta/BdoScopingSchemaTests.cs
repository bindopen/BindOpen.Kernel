using BindOpen.Data.Helpers;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// This class provides unit tests for schema management, including schema generation 
/// from reflection and validation of metadata nodes.
/// </summary>
[TestFixture, Order(100)]
public class BdoScopingSchemaTests
{
    private IBdoSchema _schema;

    /// <summary>
    /// Sets up the shared schema used across various validation tests.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _schema = BdoScopingSchemaFaker.CreateSchema();
    }

    /// <summary>
    /// Verifies that the aggregated schema contains the expected number of child rules.
    /// </summary>
    [Test, Order(2)]
    public void AggregateSpecTest()
    {
        Assert.That(_schema._Children?.Count == 3, "Aggregate schema child count mismatch.");
    }

    /// <summary>
    /// Tests the creation of a schema directly from a POCO type using the generic factory.
    /// </summary>
    [Test, Order(3)]
    public void CreateFromTypeTest()
    {
        var schema = BdoData.NewSchemaFrom<EntityFake>("test1");
        // Verifies if the reflection accurately identified all properties of EntityFake
        Assert.That(schema.As<BdoSchema>()._Children?.Count == 13, "Schema generation from generic type failed.");
    }

    /// <summary>
    /// Tests the creation of a specification (schema) using the ToSpec reflection extension.
    /// </summary>
    [Test, Order(4)]
    public void CreateFromTypeToSpecTest()
    {
        var schema = typeof(EntityFake).ToSpec<SchemaFake>("test1");
        Assert.That(schema._Children?.Count == 13, "Schema generation from Type extension failed.");
    }

    /// <summary>
    /// Verifies that the system can find the correct sub-schema when multiple metadata criteria are met.
    /// </summary>
    [Test, Order(5)]
    public void SatisfiedSubTest()
    {
        var meta = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("title", "myTitle"),
                BdoData.NewScalar("description", "myDescription"));

        // Finds the child spec satisfied by the metadata content
        var subSpec = meta.FindChildSpec(_schema, ScopingTestData.Scope);
        Assert.That(subSpec == _schema._Children[1], "Incorrect schema satisfaction mapping.");
    }

    /// <summary>
    /// Verifies that the system falls back to the default sub-schema when only basic criteria are met.
    /// </summary>
    [Test, Order(6)]
    public void DefaultSubTest()
    {
        var meta = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("title", "myTitle"));

        var subSpec = meta.FindChildSpec(_schema, ScopingTestData.Scope);
        Assert.That(subSpec == _schema._Children[0], "Failed to fall back to default schema rule.");
    }

    /// <summary>
    /// Tests full metadata node validation against a schema, ensuring forbidden elements are rejected.
    /// </summary>
    [Test, Order(7)]
    public void CheckWithSchemaTest()
    {
        var validator = ScopingTestData.Scope.CreateValidator();

        // 1. Valid data node
        var meta1 = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("title", "myTitle"));

        Assert.That(validator.Check(meta1), "Validation failed for a valid meta node.");

        // 2. Node with forbidden 'label' scalar
        var meta2 = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("label", true),
                BdoData.NewScalar("title", "myTitle"));

        Assert.That(!validator.Check(meta2), "Validation should have failed for a node containing forbidden elements.");
    }

    /// <summary>
    /// Tests validation behavior when a global schema is missing but inline specifications are provided.
    /// </summary>
    [Test, Order(8)]
    public void CheckWithoutSpecTest()
    {
        var validator = ScopingTestData.Scope.CreateValidator();

        // 1. Node without schema should pass basic checks
        var meta1 = BdoData.NewNode("meta-test")
            .With(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("label", true),
                BdoData.NewScalar("title", "myTitle"));

        Assert.That(meta1.Check(ScopingTestData.Scope), "Standard node check failed.");

        // 2. Conflict: Boolean data provided for an Integer schema
        var meta2 = BdoData.NewNode("meta-test")
            .With(
                BdoData.NewScalar("auto", true)
                    .WithSchema(BdoData.NewSchema().WithDataType(DataValueTypes.Integer)),
                BdoData.NewScalar("label", true),
                BdoData.NewScalar("title", "myTitle"));

        Assert.That(!validator.Check(meta2), "Validation failed to detect DataValueType mismatch (Boolean vs Integer).");
    }

    /// <summary>
    /// Tests specific value type constraints, including nullability and type casting checks.
    /// </summary>
    [Test, Order(9)]
    public void CheckValueTypeTest()
    {
        var validator = ScopingTestData.Scope.CreateValidator();

        // 1. Basic null value check
        var meta1 = BdoData.NewScalar("name").AsNullValue();
        Assert.That(validator.Check(meta1), "Null value check failed.");

        // 2. Strict null schema check
        meta1.WithSchema(q => q.AsNullValue());
        Assert.That(validator.Check(meta1), "Strict null schema validation failed.");

        // 3. String value provided where Null was expected
        meta1.WithData("maria");
        Assert.That(!validator.Check(meta1), "Validation should fail when data is provided for a null-only schema.");

        // 4. Mismatch check: Text data provided for an Integer schema
        var meta4 = BdoData.NewScalar("name")
            .WithSchema(q => q.AsInteger())
            .WithData("ABC");
        Assert.That(!validator.Check(meta4), "Validation failed to detect string data for integer schema.");
    }
}