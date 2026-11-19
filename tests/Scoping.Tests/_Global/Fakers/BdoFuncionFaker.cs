using BindOpen.Data;
using Bogus;
using System.Dynamic;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class provides factory methods to generate randomized data for testing 
/// BindOpen script functions and expressions.
/// </summary>
public static class BdoFunctionFaker
{
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "Function.xml";
    public static readonly string JsonFilePath = DataTestData.WorkingFolder + "Function.json";

    /// <summary>
    /// Generates a randomized set of data used as arguments for script functions.
    /// </summary>
    /// <returns>A dynamic ExpandoObject containing mixed-type values.</returns>
    public static dynamic Fake()
    {
        var f = new Faker();
        dynamic b = new ExpandoObject();

        // Standardizing randomized values to test type-coercion in scripts
        b.boolValue = f.Random.Bool();
        b.intValue = f.Random.Int(1, 800);
        b.enumValue = AccessibilityLevels.Private;
        b.stringValue = f.Lorem.Word();

        return b;
    }
}