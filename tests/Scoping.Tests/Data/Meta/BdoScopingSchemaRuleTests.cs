using BindOpen.Data.Schema;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// This class provides unit tests for schema rules, specifically focusing on 
/// requirement levels and constraint validation within the BindOpen scoping engine.
/// </summary>
[TestFixture, Order(100)]
public class BdoScopingSchemaRuleTests
{
    private IBdoSchema _schema;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initialize the shared schema from the faker
        _schema = BdoScopingSchemaFaker.CreateSchema();
    }

    /// <summary>
    /// Tests the retrieval of requirement levels based on the current data context and schema.
    /// </summary>
    [Test, Order(2)]
    public void RequirementTest()
    {
        // 1. Meta with empty schema: Requirement should be None
        var meta0 = BdoData.NewSet("meta-test")
            .WithSchema(BdoData.NewSchema())
            .With(BdoData.NewScalar("title", "A"));

        var requirementLevel0 = meta0.GetRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel0, Is.EqualTo(RequirementLevels.None), "Default requirement level failed.");

        // 2. Meta matching a schema rule where 'title' makes it 'Required'
        var meta1 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("title", "This is my title"));

        var requirementLevel1 = meta1.GetRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel1, Is.EqualTo(RequirementLevels.Required), "Requirement level evaluation failed for 'Required' context.");

        // 3. Meta matching a schema rule where it remains 'Optional'
        var meta2 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .With(("auto", true));

        var requirementLevel2 = meta2.GetRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel2, Is.EqualTo(RequirementLevels.Optional), "Requirement level evaluation failed for 'Optional' context.");
    }

    /// <summary>
    /// Tests the validator's ability to check a meta-node against requirement levels.
    /// </summary>
    [Test, Order(3)]
    public void RequirementCheckTest()
    {
        // Creating a meta node with inline schema requirements
        var meta0 = BdoData.NewSet("meta-test")
            .WithSchema(schema => schema.AddItemRequirement(RequirementLevels.Optional).WithName("toto"))
            .With(
                BdoData.NewScalar("title", "A")
                    .WithSchema(schema => schema.AddRequirement(RequirementLevels.Optional)));

        var validator = ScopingTestData.Scope.CreateValidator();
        var valid = validator.Check(meta0);

        Assert.That(valid, Is.True, "Validation check failed for optional requirements.");
    }

    /// <summary>
    /// Tests the retrieval of requirement levels specifically for child items.
    /// </summary>
    [Test, Order(4)]
    public void ItemRequirementTest()
    {
        // 1. Basic schema: Item requirement should be Optional by default
        var meta0 = BdoData.NewSet("meta-test")
            .WithSchema(BdoData.NewSchema())
            .With(BdoData.NewScalar("title", "A"));

        var requirementLevel0 = meta0.GetItemRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel0, Is.EqualTo(RequirementLevels.Optional), "Default item requirement failed.");

        // 2. Schema rule: Required item
        var meta1 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .With(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("title", "This is my title"));

        var requirementLevel1 = meta1.GetItemRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel1, Is.EqualTo(RequirementLevels.Required), "Item requirement evaluation failed for 'Required' state.");

        // 3. Schema rule: Optional item (auto is false)
        var meta2 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .With(BdoData.NewScalar("auto", false));

        var requirementLevel2 = meta2.GetItemRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel2, Is.EqualTo(RequirementLevels.Optional), "Item requirement evaluation failed for 'Optional' state.");
    }

    /// <summary>
    /// Tests the Enum constraint validation (MustBeInList).
    /// </summary>
    [Test, Order(6)]
    public void InEnumTest()
    {
        // meta0 uses a string value that is technically the name of the Enum, 
        // but validator check might fail if it's not strictly formatted or handled.
        var meta0 = BdoData.NewSet("meta-test")
            .With(
                BdoData.NewScalar("level", "RequirementLevels.Optional")
                    .WithSchema(q => q.MustBeInList<RequirementLevels>())
            );

        var validator = ScopingTestData.Scope.CreateValidator();
        var isChecked = validator.Check(meta0);

        // Asserting false: depending on implementation, a string "EnumName.Value" 
        // might fail if the validator expects just "Optional" or the numeric value.
        Assert.That(isChecked, Is.False, "Constraint validation should have rejected the invalid enum string format.");
    }
}