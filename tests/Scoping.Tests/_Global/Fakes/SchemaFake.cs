using BindOpen.Data.Schema;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class represents a fake option schema used for testing metadata 
/// specifications and validation rules within the BindOpen scoping engine.
/// </summary>
public class SchemaFake : BdoSchema
{
    // -------------------------------------------------------------
    // CONSTRUCTORS
    // -------------------------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the SchemaFake class.
    /// </summary>
    public SchemaFake() : base()
    {
        // Initializing with the base constructor ensures that 
        // internal collections (Requirements, Constraints) are ready.
    }

    #endregion
}