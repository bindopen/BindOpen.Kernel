using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping.Connectors;

namespace BindOpen.Scoping.Tests;

/// <summary>
/// This class represents a fake connector used for testing metadata-to-object mapping
/// and connection instantiation within the BindOpen scoping framework.
/// </summary>
[BdoConnector(Name = "scoping.tests$testConnector")]
[BdoConnector(CreationDate = "2023-02-25")]
public class ConnectorFake : BdoConnector, ITBdoConnector<ConnectionFake>
{
    /// <summary>
    /// The unique name used by the scoping engine to identify this extension.
    /// </summary>
    public static string __DefinitionUniqueName = "scoping.tests$testConnector";

    // ------------------------------------------
    // PROPERTIES
    // ------------------------------------------

    #region Properties

    /// <summary>
    /// Gets or sets the host address. Mapped from the "host" metadata property.
    /// </summary>
    [BdoProperty(Name = "host")]
    public string Host { get; set; }

    /// <summary>
    /// Gets or sets whether SSL is enabled. Mapped from the "isSslEnabled" metadata property.
    /// </summary>
    [BdoProperty(Name = "isSslEnabled")]
    public bool? IsSslEnabled { get; set; }

    /// <summary>
    /// Gets or sets the port. Mapped as a MetaScalar to handle complex data states 
    /// (e.g., scripts or requirements) within the port value.
    /// </summary>
    [BdoProperty(Name = "port")]
    public ITBdoMetaScalar<int?> Port { get; set; }

    #endregion

    // ------------------------------------------
    // CONSTRUCTORS
    // ------------------------------------------

    #region Constructors

    /// <summary>
    /// Instantiates a new instance of the ConnectorFake class.
    /// </summary>
    public ConnectorFake() : base()
    {
    }

    #endregion

    // ------------------------------------------
    // MUTATORS
    // ------------------------------------------

    #region Mutators

    /// <summary>
    /// Creates a new active connection based on this connector's configuration.
    /// </summary>
    /// <param name="log">The log to consider for tracking instantiation.</param>
    /// <returns>A new instance of ConnectionFake.</returns>
    public override ConnectionFake NewConnection(IBdoLog log = null)
    {
        // We pass 'this' to the connection so it can access Host, Port, and SSL settings.
        return new ConnectionFake(this);
    }

    #endregion
}