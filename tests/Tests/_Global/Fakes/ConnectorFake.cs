using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using BindOpen.Scoping.Connectors;

namespace BindOpen.Tests;

/// <summary>
/// This class represents a test connector.
/// </summary>
[BdoConnector(Name = "bindopen.tests$testConnector")]
[BdoConnector(CreationDate = "2023-02-25")]
public class ConnectorFake : BdoConnector, ITBdoConnector<ConnectionFake>
{
    public static string __DefinitionUniqueName = "bindopen.tests$testConnector";

    /// <summary>
    /// The host of this instance.
    /// </summary>
    [BdoProperty(Name = "host")]
    public string Host { get; set; }

    /// <summary>
    /// Indicates whether this instance enables SSL.
    /// </summary>
    [BdoProperty(Name = "isSslEnabled")]
    public bool? IsSslEnabled { get; set; }

    /// <summary>
    /// The port of this instance.
    /// </summary>
    [BdoProperty(Name = "port")]
    public ITBdoMetaScalar<int?> Port { get; set; }

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
    /// Creates a new connection.
    /// </summary>
    /// <param key="log">The log to consider.</param>
    public override ConnectionFake NewConnection(IBdoLog log = null)
    {
        return new ConnectionFake(this);
    }

    #endregion
}
