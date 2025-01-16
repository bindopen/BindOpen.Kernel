using BindOpen.Data.Helpers;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

[TestFixture, Order(100)]
public class BdoScopingSchemaTests
{
    private IBdoSchema _schema;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _schema = BdoScopingSchemaFaker.CreateSchema();
    }

    [Test, Order(2)]
    public void AggregateSpecTest()
    {
        Assert.That(_schema._Children?.Count == 3, "Aggregate schema error");
    }

    [Test, Order(3)]
    public void CreateFromTypeTest()
    {
        var schema = BdoData.NewSchemaFrom<EntityFake>("test1");
        Assert.That(schema.As<BdoSchema>()._Children?.Count == 13, "Aggregate schema error");
    }

    [Test, Order(4)]
    public void CreateFromTypeToSpecTest()
    {
        var schema = typeof(EntityFake).ToSpec<SchemaFake>("test1");
        Assert.That(schema._Children?.Count == 13, "Aggregate schema error");
    }

    [Test, Order(5)]
    public void SatisfiedSubTest()
    {
        var meta = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("title", "myTitle"),
                BdoData.NewScalar("description", "myDescription"));

        var subSpec = meta.FindChildSpec(_schema, ScopingTestData.Scope);
        Assert.That(subSpec == _schema._Children[1], "Bad schema condition");
    }

    [Test, Order(6)]
    public void DefaultSubTest()
    {
        var meta = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("title", "myTitle"));

        var subSpec = meta.FindChildSpec(_schema, ScopingTestData.Scope);
        Assert.That(subSpec == _schema._Children[0], "Bad schema condition");
    }

    [Test, Order(7)]
    public void CheckWithSchemaTest()
    {
        var validator = ScopingTestData.Scope.CreateValidator();

        var meta1 = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("title", "myTitle"));

        var ok1 = validator.Check(meta1);
        Assert.That(ok1, "Check rules - Error");

        var meta2 = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("label", true),     // should be forbidden
                BdoData.NewScalar("title", "myTitle"));

        var ok2 = validator.Check(meta2);
        Assert.That(!ok2, "Check rules - Error");
    }

    [Test, Order(8)]
    public void CheckWithoutSpecTest()
    {
        var validator = ScopingTestData.Scope.CreateValidator();

        var meta1 = BdoData.NewNode("meta-test")
            .With(
                BdoData.NewScalar("auto", true),  // should be ok
                BdoData.NewScalar("label", true),
                BdoData.NewScalar("title", "myTitle"));

        var ok1 = meta1.Check(ScopingTestData.Scope);
        Assert.That(ok1, "Check rules - Error");

        var meta2 = BdoData.NewNode("meta-test")
            .With(
                BdoData.NewScalar("auto", true)   // should raise a value type error
                    .WithSchema(BdoData.NewSchema().WithDataType(DataValueTypes.Integer)),
                BdoData.NewScalar("label", true),
                BdoData.NewScalar("title", "myTitle"));

        var ok2 = validator.Check(meta2);
        Assert.That(!ok2, "Check rules - Error");

        var meta3 = BdoData.NewNode("meta-test")
            .With(
                BdoData.NewScalar("auto", "true")   // should be ok
                    .WithSchema(BdoData.NewSchema().WithDataType(DataValueTypes.Text)),
                BdoData.NewScalar("label", true),
                BdoData.NewScalar("title", "myTitle"));

        var ok3 = validator.Check(meta3);
        Assert.That(ok3, "Check rules - Error");
    }

    [Test, Order(8)]
    public void CheckValueTypeTest()
    {
        var validator = ScopingTestData.Scope.CreateValidator();

        var meta1 = BdoData.NewScalar("name")
            .AsNullValue();
        Assert.That(validator.Check(meta1), "Check rules - Error");

        meta1 = BdoData.NewScalar("name")
            .WithSchema(q => q.AsNullValue())
            .AsNullValue();
        Assert.That(validator.Check(meta1), "Check rules - Error");

        meta1.WithData("maria");
        Assert.That(!validator.Check(meta1), "Check rules - Error");

        var meta2 = BdoData.NewScalar("name")
            .WithSchema(q => q.AsNullValue());
        Assert.That(validator.Check(meta2), "Check rules - Error");

        meta2.WithData("maria");
        Assert.That(!validator.Check(meta2), "Check rules - Error");

        var meta4 = BdoData.NewScalar("name")
            .WithSchema(q => q.AsInteger())
            .WithData("ABC");
        Assert.That(!validator.Check(meta4), "Check rules - Error");
    }
}
