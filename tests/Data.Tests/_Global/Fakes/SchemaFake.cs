using BindOpen.Data.Schema;

namespace BindOpen.Tests;

/// <summary>
/// Represents a fake schema used for testing schema-based validation and metadata definitions.
/// </summary>
/// <remarks>
/// This class inherits from <see cref="BdoSchema"/> to simulate custom data contract 
/// definitions within the BindOpen Kernel unit tests.
/// </remarks>
public class SchemaFake : BdoSchema
{
    // -------------------------------------------------------------
    // CONSTRUCTORS
    // -------------------------------------------------------------

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="SchemaFake"/> class.
    /// </summary>
    public SchemaFake()
    {
    }

    #endregion
}