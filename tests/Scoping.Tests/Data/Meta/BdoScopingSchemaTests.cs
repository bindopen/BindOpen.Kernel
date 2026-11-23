using BindOpen.Data.Helpers;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for schema management within the BindOpen scoping engine,
/// including reflection-based schema generation, sub-schema resolution, and metadata node validation.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoSchema"/> definitions can be generated from CLR types,
/// matched dynamically against <see cref="IBdoMetaSet"/> instances, and enforced by the scope validator
/// across structural, data-type, and nullability constraints.
/// </remarks>
[TestFixture]
public class BdoScopingSchemaTests
{
    private IBdoSchema _schema;

    /// <summary>
    /// Initializes the shared <see cref="IBdoSchema"/> test instance from <see cref="BdoScopingSchemaFaker"/>
    /// before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _schema = BdoScopingSchemaFaker.CreateSchema();
    }

    /// <summary>
    /// Verifies that the aggregated test schema contains the expected number of child specifications.
    /// </summary>
    [Test]
    public void AggregateSpecTest()
    {
        Assert.That(_schema._Children?.Count, Is.EqualTo(3), "Aggregate schema child count mismatch.");
    }

    /// <summary>
    /// Verifies schema generation directly from a CLR type using <see cref="BdoData.NewSchemaFrom{T}"/>
    /// and confirms all reflected properties of <see cref="EntityFake"/> are represented.
    /// </summary>
    [Test]
    public void CreateFromTypeTest()
    {
        var schema = BdoData.NewSchemaFrom<EntityFake>("test1");

        // Verifies if the reflection accurately identified all properties of EntityFake
        Assert.That(schema.As<BdoSchema>()._Children?.Count, Is.EqualTo(13), "Schema generation from generic type failed.");
    }

    /// <summary>
    /// Verifies specification generation from a <see cref="System.Type"/> instance using the <c>ToSpec</c> reflection extension.
    /// </summary>
    [Test]
    public void CreateFromTypeToSpecTest()
    {
        var schema = typeof(EntityFake).ToSpec<SchemaFake>("test1");
        Assert.That(schema._Children?.Count, Is.EqualTo(13), "Schema generation from Type extension failed.");
    }

    /// <summary>
    /// Verifies that <c>FindChildSpec</c> resolves the matching specialized sub-schema
    /// when a metadata set satisfies multiple property criteria.
    /// </summary>
    [Test]
    public void SatisfiedSubTest()
    {
        var meta = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .Add(
                BdoData.NewScalar("title", "myTitle"),
                BdoData.NewScalar("description", "myDescription"));

        // Finds the child spec satisfied by the metadata content
        var subSpec = meta.FindChildSpec(_schema, ScopingTestData.Scope);
        Assert.That(subSpec, Is.EqualTo(_schema._Children[1]), "Incorrect schema satisfaction mapping.");
    }

    /// <summary>
    /// Verifies that <c>FindChildSpec</c> falls back to the default child specification
    /// when only baseline metadata criteria are present.
    /// </summary>
    [Test]
    public void DefaultSubTest()
    {
        var meta = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .Add(
                BdoData.NewScalar("title", "myTitle"));

        var subSpec = meta.FindChildSpec(_schema, ScopingTestData.Scope);
        Assert.That(subSpec, Is.EqualTo(_schema._Children[0]), "Failed to fall back to default schema rule.");
    }

    /// <summary>
    /// Verifies full metadata node validation against a schema, ensuring valid sets pass
    /// and sets containing forbidden scalar elements are rejected.
    /// </summary>
    [Test]
    public void CheckWithSchemaTest()
    {
        var validator = ScopingTestData.Scope.CreateValidator();

        // 1. Valid data node
        var meta1 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .Add(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("title", "myTitle"));

        Assert.That(validator.Check(meta1), Is.True, "Validation failed for a valid meta node.");

        // 2. Node with forbidden 'label' scalar
        var meta2 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .Add(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("label", true),
                BdoData.NewScalar("title", "myTitle2"));

        Assert.That(validator.Check(meta2), Is.False, "Validation should have failed for a node containing forbidden elements.");
    }

    /// <summary>
    /// Verifies validation behavior when no parent schema is attached, ensuring unconstrained nodes pass
    /// while inline element schemas still enforce <see cref="DataValueTypes"/> mismatches.
    /// </summary>
    [Test]
    public void CheckWithoutSpecTest()
    {
        // 1. Node without schema should pass basic checks
        var meta1 = BdoData.NewSet("meta-test")
            .Add(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("label", true),
                BdoData.NewScalar("title", "myTitle"));

        Assert.That(ScopingTestData.Scope.Check(meta1), Is.True, "Standard node check failed.");

        // 2. Conflict: Boolean data provided for an Integer schema
        var validator = ScopingTestData.Scope.CreateValidator();

        var meta2 = BdoData.NewSet("meta-test")
            .Add(
                BdoData.NewScalar("auto", true)
                    .WithSchema(BdoData.NewSchema().WithDataType(DataValueTypes.Integer)),
                BdoData.NewScalar("label", true),
                BdoData.NewScalar("title", "myTitle"));

        Assert.That(validator.Check(meta2), Is.False, "Validation failed to detect DataValueType mismatch (Boolean vs Integer).");
    }

    /// <summary>
    /// Verifies value type constraint enforcement, including explicit <see langword="null"/> value schemas
    /// and strict <see cref="DataValueTypes.Integer"/> parsing checks.
    /// </summary>
    [Test]
    public void CheckValueTypeTest()
    {
        var validator = ScopingTestData.Scope.CreateValidator();

        // 1. Basic null value check
        var meta1 = BdoData.NewScalar("name", null).AsNullValue();
        Assert.That(validator.Check(meta1), Is.True, "Null value check failed.");

        // 2. Strict null schema check
        meta1.WithSchema(q => q.AsNullValue());
        Assert.That(validator.Check(meta1), Is.True, "Strict null schema validation failed.");

        // 3. String value provided where Null was expected
        meta1.WithData("maria");
        Assert.That(validator.Check(meta1), Is.False, "Validation should fail when data is provided for a null-only schema.");

        // 4. Mismatch check: Text data provided for an Integer schema
        var meta4 = BdoData.NewScalar("name")
            .WithSchema(q => q.AsInteger())
            .WithData("ABC");
        Assert.That(validator.Check(meta4), Is.False, "Validation failed to detect string data for integer schema.");
    }
}