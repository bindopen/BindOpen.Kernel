using BindOpen.Data.Schema;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

[TestFixture, Order(100)]
public class BdoScopingSchemaRuleTests
{
    private IBdoSchema _schema;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _schema = BdoScopingSchemaFaker.CreateSchema();
    }

    [Test, Order(2)]
    public void RequirementTest()
    {
        var meta0 = BdoData.NewNode("meta-test")
            .WithSchema(BdoData.NewSchema())
            .With(
                BdoData.NewScalar("title", "A"));

        var requirementLevel0 = meta0.GetRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel0 == RequirementLevels.None, "Statement - Error");

        var meta1 = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("title", "This is my title"));

        var requirementLevel1 = meta1.GetRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel1 == RequirementLevels.Required, "Statement - Error");

        var meta2 = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(("auto", true));

        var requirementLevel2 = meta2.GetRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel2 == RequirementLevels.Optional, "Statement - Error");
    }

    [Test, Order(3)]
    public void RequirementCheckTest()
    {
        var meta0 = BdoData.NewNode("meta-test")
            .WithSchema(schema => schema.AddItemRequirement(RequirementLevels.Optional).WithName("toto"))
            .With(
                BdoData.NewScalar("title", "A")
                    .WithSchema(schema => schema.AddRequirement(RequirementLevels.Optional)));

        var validator = ScopingTestData.Scope.CreateValidator();
        var valid = validator.Check(meta0);
        Assert.That(valid, "Statement - Error");
    }

    [Test, Order(4)]
    public void ItemRequirementTest()
    {
        var meta0 = BdoData.NewNode("meta-test")
            .WithSchema(BdoData.NewSchema())
            .With(
                BdoData.NewScalar("title", "A"));

        var requirementLevel0 = meta0.GetItemRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel0 == RequirementLevels.Optional, "Statement - Error");

        var meta1 = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("title", "This is my title"));

        var requirementLevel1 = meta1.GetItemRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel1 == RequirementLevels.Required, "Statement - Error");

        var meta2 = BdoData.NewNode("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("auto", false));

        var requirementLevel2 = meta2.GetItemRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel2 == RequirementLevels.Optional, "Statement - Error");
    }

    [Test, Order(6)]
    public void InEnumTest()
    {
        var schema = BdoData.NewSchema();

        var meta0 = BdoData.NewNode("meta-test")
            .With(
                BdoData.NewScalar("level", "RequirementLevels.Optional")
                    .WithSchema(q => q.MustBeInList<RequirementLevels>())
            );

        var validator = ScopingTestData.Scope.CreateValidator();
        var isChecked = validator.Check(meta0);
        Assert.That(!isChecked, "Constraint validation failed");
    }
}
