using Bogus;
using System.Dynamic;

namespace BindOpen.Tests;

/// <summary>
/// Provides fake data generation for BindOpen reference and scriptword tests.
/// </summary>
public static class BdoReferenceFaker
{
    private static readonly Faker _faker = new();

    /// <summary>
    /// Generates a dynamic object containing sample script references and literal values.
    /// </summary>
    /// <returns>Returns a dynamic <see cref="ExpandoObject"/> containing test data.</returns>
    /// <remarks>
    /// This dynamic object is used to verify that the BindOpen kernel can correctly 
    /// resolve values and scripts from dynamic or untyped data sources.
    /// </remarks>
    public static dynamic GetValueSet()
    {
        dynamic obj = new ExpandoObject();

        // A standard BindOpen script syntax for variable referencing
        obj.Script = "$(var1)";

        // A randomized word to simulate dynamic literal content
        obj.Literal = _faker.Random.Word();

        // A placeholder name for a scriptword function
        obj.ScriptwordName = "func1";

        return obj;
    }
}