using BindOpen.Data;
using BindOpen.Data.Schema;

namespace BindOpen.Tests;

/// <summary>
/// Provides factory methods to create fake <see cref="IBdoSchema"/> instances for unit testing.
/// </summary>
public static class BdoSchemaFaker
{
    /// <summary>
    /// The local file path used for XML schema serialization tests.
    /// </summary>
    public static readonly string XmlFilePath = DataTestData.WorkingFolder + "Spec.xml";

    /// <summary>
    /// Creates a new schema instance configured with a script-based reference.
    /// </summary>
    /// <returns>Returns an initialized <see cref="IBdoSchema"/> object.</returns>
    /// <remarks>
    /// The schema is initialized with an expression reference ($eq(1, 0)) to test 
    /// conditional schema resolution and validation logic.
    /// </remarks>
    public static IBdoSchema CreateSpecWithReference()
    {
        // Using the fluent BdoData factory to create a schema with an expression reference
        var schema = BdoData.NewSchema<BdoSchema>()
            .WithReference(BdoData.NewExp("$eq(1, 0)"));

        return schema;
    }
}