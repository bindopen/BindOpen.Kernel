using Bogus;
using System.Dynamic;

namespace BindOpen.Tests;

/// <summary>
/// Provides fake data generation for BindOpen expression tests.
/// </summary>
public static class BdoExpressionFaker
{
    private static readonly Faker _faker = new();

    /// <summary>
    /// Generates a dynamic object containing common script and literal values for testing.
    /// </summary>
    /// <returns>A dynamic <see cref="ExpandoObject"/> containing test data.</returns>
    /// <remarks>
    /// This dynamic object is typically used to test the framework's ability 
    /// to resolve values from non-static data sources.
    /// </remarks>
    public static dynamic GetValueSet()
    {
        dynamic obj = new ExpandoObject();

        // A standard BindOpen variable reference script
        obj.Script = "$(var1)";

        // A random word to simulate literal content
        obj.Literal = _faker.Random.Word();

        // A target scriptword name for function resolution tests
        obj.ScriptwordName = "func1";

        return obj;
    }
}