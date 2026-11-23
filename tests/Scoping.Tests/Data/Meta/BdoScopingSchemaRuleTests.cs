using BindOpen.Data.Schema;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta;

/// <summary>
/// Contains unit tests for schema rule evaluation, specifically focusing on
/// <see cref="RequirementLevels"/> resolution and constraint validation within the BindOpen scoping engine.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoSchema"/> rules dynamically evaluate node and child item
/// requirement levels based on contextual data values, and enforce allowed-value constraints
/// during metadata validation.
/// </remarks>
[TestFixture]
public class BdoScopingSchemaRuleTests
{
    private IBdoSchema _schema;

    /// <summary>
    /// Initializes the shared <see cref="IBdoSchema"/> test instance from <see cref="BdoScopingSchemaFaker"/>
    /// before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initialize the shared schema from the faker
        _schema = BdoScopingSchemaFaker.CreateSchema();
    }

    /// <summary>
    /// Verifies that <c>GetRequirementLevel</c> dynamically resolves <see cref="RequirementLevels.None"/>,
    /// <see cref="RequirementLevels.Required"/>, and <see cref="RequirementLevels.Optional"/> based on
    /// the active schema rules and current metadata values.
    /// </summary>
    [Test]
    public void RequirementTest()
    {
        // 1. Meta with empty schema: Requirement should be None
        var meta0 = BdoData.NewSet("meta-test")
            .WithSchema(BdoData.NewSchema())
            .Add(BdoData.NewScalar("title", "A"));

        var requirementLevel0 = meta0.GetRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel0, Is.EqualTo(RequirementLevels.None), "Default requirement level failed.");

        // 2. Meta matching a schema rule where 'title' makes it 'Required'
        var meta1 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .Add(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("title", "This is my title"));

        var requirementLevel1 = meta1.GetRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel1, Is.EqualTo(RequirementLevels.Required), "Requirement level evaluation failed for 'Required' context.");

        // 3. Meta matching a schema rule where it remains 'Optional'
        var meta2 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .Add(("auto", true));

        var requirementLevel2 = meta2.GetRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel2, Is.EqualTo(RequirementLevels.Optional), "Requirement level evaluation failed for 'Optional' context.");
    }

    /// <summary>
    /// Verifies that the scope validator successfully validates a metadata set configured
    /// with inline <see cref="RequirementLevels.Optional"/> node and item requirements.
    /// </summary>
    [Test]
    public void RequirementCheckTest()
    {
        // Creating a meta node with inline schema requirements
        var meta0 = BdoData.NewSet("meta-test")
            .WithSchema(schema => schema.AddItemRequirement(RequirementLevels.Optional).WithName("toto"))
            .Add(
                BdoData.NewScalar("title", "A")
                    .WithSchema(schema => schema.AddRequirement(RequirementLevels.Optional)));

        var validator = ScopingTestData.Scope.CreateValidator();
        var valid = validator.Check(meta0);

        Assert.That(valid, Is.True, "Validation check failed for optional requirements.");
    }

    /// <summary>
    /// Verifies that <c>GetItemRequirementLevel</c> dynamically resolves child item requirement levels
    /// (<see cref="RequirementLevels.Required"/> vs. <see cref="RequirementLevels.Optional"/>) according
    /// to the parent set's schema rules and property values.
    /// </summary>
    [Test]
    public void ItemRequirementTest()
    {
        // 1. Basic schema: Item requirement should be Optional by default
        var meta0 = BdoData.NewSet("meta-test")
            .WithSchema(BdoData.NewSchema())
            .Add(BdoData.NewScalar("title", "A"));

        var requirementLevel0 = meta0.GetItemRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel0, Is.EqualTo(RequirementLevels.Optional), "Default item requirement failed.");

        // 2. Schema rule: Required item
        var meta1 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .Add(
                BdoData.NewScalar("auto", true),
                BdoData.NewScalar("title", "This is my title"));

        var requirementLevel1 = meta1.GetItemRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel1, Is.EqualTo(RequirementLevels.Required), "Item requirement evaluation failed for 'Required' state.");

        // 3. Schema rule: Optional item (auto is false)
        var meta2 = BdoData.NewSet("meta-test")
            .WithSchema(_schema)
            .Add(BdoData.NewScalar("auto", false));

        var requirementLevel2 = meta2.GetItemRequirementLevel(ScopingTestData.Scope);
        Assert.That(requirementLevel2, Is.EqualTo(RequirementLevels.Optional), "Item requirement evaluation failed for 'Optional' state.");
    }

    /// <summary>
    /// Verifies that <c>MustBeInList&lt;RequirementLevels&gt;()</c> rejects improperly qualified
    /// enumeration strings (such as <c>"RequirementLevels.Optional"</c>) during validation.
    /// </summary>
    [Test]
    public void InEnumTest()
    {
        // meta0 uses a string value that is technically the name of the Enum, 
        // but validator check might fail if it's not strictly formatted or handled.
        var meta0 = BdoData.NewSet("meta-test")
            .Add(
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