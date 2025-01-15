using BindOpen.Data.Helpers;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

[TestFixture, Order(100)]
public class BdoScopingSpecTests
{
    private IBdoSpec _spec;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _spec = BdoScopingSpecFaker.CreateSpec();
    }

    [Test, Order(2)]
    public void AggregateSpecTest()
    {
        Assert.That(_spec._Children?.Count == 3, "Aggregate specification error");
    }

    [Test, Order(3)]
    public void CreateFromTypeTest()
    {
        var spec = BdoData.NewSpecFrom<EntityFake>("test1");
        Assert.That(spec.As<BdoSpec>()._Children?.Count == 13, "Aggregate specification error");
    }

    [Test, Order(4)]
    public void CreateFromTypeToSpecTest()
    {
        var spec = typeof(EntityFake).ToSpec<SpecFake>("test1");
        Assert.That(spec._Children?.Count == 13, "Aggregate specification error");
    }

    [Test, Order(5)]
    public void SatisfiedSubTest()
    {
        var meta = BdoData.NewNode("meta-test")
            .WithSpec(_spec)
            .With(
                BdoData.NewScalar("title", "myTitle"),
                BdoData.NewScalar("description", "myDescription"));

        var subSpec = meta.FindChildSpec(_spec, ScopingTestData.Scope);
        Assert.That(subSpec == _spec._Children[1], "Bad spec condition");
    }

    [Test, Order(6)]
    public void DefaultSubTest()
    {
        var meta = BdoData.NewNode("meta-test")
            .WithSpec(_spec)
            .With(
                BdoData.NewScalar("title", "myTitle"));

        var subSpec = meta.FindChildSpec(_spec, ScopingTestData.Scope);
        Assert.That(subSpec == _spec._Children[0], "Bad spec condition");
    }

    [Test, Order(7)]
    public void CheckWithSpecTest()
    {
        var validator = ScopingTestData.Scope.CreateValidator();

        var meta1 = BdoData.NewNode("meta-test")
            .WithSpec(_spec)
            .With(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("title", "myTitle"));

        var ok1 = validator.Check(meta1);
        Assert.That(ok1, "Check rules - Error");

        var meta2 = BdoData.NewNode("meta-test")
            .WithSpec(_spec)
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
                    .WithSpec(BdoData.NewSpec().WithDataType(DataValueTypes.Integer)),
                BdoData.NewScalar("label", true),
                BdoData.NewScalar("title", "myTitle"));

        var ok2 = validator.Check(meta2);
        Assert.That(!ok2, "Check rules - Error");

        var meta3 = BdoData.NewNode("meta-test")
            .With(
                BdoData.NewScalar("auto", "true")   // should be ok
                    .WithSpec(BdoData.NewSpec().WithDataType(DataValueTypes.Text)),
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
            .WithSpec(q => q.AsNullValue())
            .AsNullValue();
        Assert.That(validator.Check(meta1), "Check rules - Error");

        meta1.WithData("maria");
        Assert.That(!validator.Check(meta1), "Check rules - Error");

        var meta2 = BdoData.NewScalar("name")
            .WithSpec(q => q.AsNullValue());
        Assert.That(validator.Check(meta2), "Check rules - Error");

        meta2.WithData("maria");
        Assert.That(!validator.Check(meta2), "Check rules - Error");

        var meta4 = BdoData.NewScalar("name")
            .WithSpec(q => q.AsInteger())
            .WithData("ABC");
        Assert.That(!validator.Check(meta4), "Check rules - Error");
    }
}
