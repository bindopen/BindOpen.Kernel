using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Tests;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Scoping.Connectors;

/// <summary>
/// Contains unit tests for <see cref="IBdoConnector"/> implementations, verifying their instantiation
/// from metadata objects or CLR instances and their execution of connection-scoped Pull and Push operations.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoScope.CreateConnector"/> properly hydrates connector properties
/// (including scalar metadata properties) and that <c>UsingConnection</c> safely manages connection lifetimes
/// during data retrieval and persistence workflows.
/// </remarks>
[TestFixture]
public class BdoConnectorTests
{
    private dynamic _testData;

    /// <summary>
    /// Gets or sets the first test connector instance instantiated via generic factory creation.
    /// </summary>
    public ConnectorFake _connector1;

    /// <summary>
    /// Gets or sets the second test connector instance instantiated via non-generic factory creation.
    /// </summary>
    public ConnectorFake _connector2;

    /// <summary>
    /// Gets or sets the third test connector instance round-tripped from a CLR object through metadata conversion.
    /// </summary>
    public ConnectorFake _connector3;

    /// <summary>
    /// Initializes randomized connector configuration values using <see cref="BdoConnectorFaker"/>
    /// before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoConnectorFaker.NewData();
    }

    /// <summary>
    /// Validates that the specified <see cref="ConnectorFake"/> instance is non-null and that its
    /// <see cref="ConnectorFake.Host"/>, <see cref="ConnectorFake.Port"/>, and <see cref="ConnectorFake.IsSslEnabled"/>
    /// properties match the initialized test dataset.
    /// </summary>
    /// <param name="connector">The connector instance to validate.</param>
    public void AssertFake(ConnectorFake connector)
    {
        Assert.That(connector, Is.Not.Null, "The connector instance is missing.");

        Assert.Multiple(() =>
        {
            Assert.That(connector.Host, Is.EqualTo(_testData.host), "Connector Host mismatch.");

            // Verifying the Port which is stored as a Meta Scalar
            Assert.That(connector.Port?.GetData<int?>(), Is.EqualTo(_testData.port), "Connector Port mismatch.");

            Assert.That(connector.IsSslEnabled ?? false, Is.EqualTo(_testData.isSslEnabled), "Connector SSL status mismatch.");
        });
    }

    /// <summary>
    /// Verifies that a strongly-typed <see cref="ConnectorFake"/> can be instantiated directly from an
    /// <see cref="IBdoMetaObject"/> using <see cref="IBdoScope.CreateConnector{T}"/>.
    /// </summary>
    [Test]
    public void CreateConnectorTest_FromMetaSet()
    {
        IBdoMetaObject meta = BdoConnectorFaker.NewMetaObject(_testData);
        _connector1 = ScopingTestData.Scope.CreateConnector<ConnectorFake>(meta);

        AssertFake(_connector1);
    }

    /// <summary>
    /// Verifies that a connector instantiated via the non-generic <see cref="IBdoScope.CreateConnector"/>
    /// method resolves to <see cref="ConnectorFake"/> with all configuration properties populated.
    /// </summary>
    [Test]
    public void CreateConnectorTest_FromConfig()
    {
        IBdoMetaObject meta = BdoConnectorFaker.NewMetaObject(_testData);
        _connector2 = ScopingTestData.Scope.CreateConnector(meta) as ConnectorFake;

        AssertFake(_connector2);
    }

    /// <summary>
    /// Verifies that a populated <see cref="ConnectorFake"/> CLR instance can be serialized to metadata
    /// via <c>ToMeta</c> and re-instantiated through <see cref="IBdoScope.CreateConnector"/> without data loss.
    /// </summary>
    [Test]
    public void CreateConnectorTest_FromObject()
    {
        var connector = new ConnectorFake
        {
            ConnectionString = _testData.connectionString,
            Host = _testData.host,
            IsSslEnabled = _testData.isSslEnabled,
            Port = BdoData.NewScalar<int?>(_testData.port as int?)
        };

        // Convert existing object to metadata and re-instantiate via the Scope
        var meta = connector.ToMeta(ScopingTestData.Scope);
        _connector3 = ScopingTestData.Scope.CreateConnector(meta) as ConnectorFake;

        AssertFake(_connector3);
    }

    /// <summary>
    /// Verifies the operational lifecycle of a connector using <c>UsingConnection</c> to open a connection,
    /// execute a <c>Pull</c> query with a parameter set, and <c>Push</c> the retrieved entities back.
    /// </summary>
    [Test]
    public void CreateConnectorTest_Pull_Push()
    {
        var connector = new ConnectorFake
        {
            ConnectionString = _testData.connectionString,
            Host = _testData.host,
            IsSslEnabled = _testData.isSslEnabled,
            Port = BdoData.NewScalar<int?>(_testData.port as int?)
        };

        // UsingConnection ensures the connection is opened and properly disposed
        connector.UsingConnection((conn, log) =>
        {
            // 1. Pull data using a parameter set
            var paramSet = BdoData.NewSet(
                BdoData.NewObject(nameof(BdoSchema.GroupId)));

            var entities = conn.Pull(paramSet, log);

            // 2. Push the retrieved entities back
            conn.Push(null, entities?.ToArray());
        });
    }
}