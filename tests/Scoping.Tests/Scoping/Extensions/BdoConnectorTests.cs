using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Scoping.Tests;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Scoping.Connectors;

/// <summary>
/// This class provides unit tests for BdoConnectors, verifying their instantiation 
/// from various metadata sources and their ability to execute Pull/Push operations.
/// </summary>
[TestFixture, Order(301)]
public class BdoConnectorTests
{
    private dynamic _testData;

    public ConnectorFake _connector1;
    public ConnectorFake _connector2;
    public ConnectorFake _connector3;

    /// <summary>
    /// Initializes the test data using the connector faker.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoConnectorFaker.NewData();
    }

    /// <summary>
    /// Standardized assertion logic to verify the properties of a fake connector.
    /// </summary>
    /// <param name="connector">The connector instance to validate.</param>
    public void AssertFake(ConnectorFake connector)
    {
        Assert.That(connector, Is.Not.Null, "The connector instance is missing.");

        Assert.That(connector.Host, Is.EqualTo(_testData.host), "Connector Host mismatch.");

        // Verifying the Port which is stored as a Meta Scalar
        Assert.That(connector.Port?.GetData<int?>(), Is.EqualTo(_testData.port), "Connector Port mismatch.");

        Assert.That(connector.IsSslEnabled ?? false, Is.EqualTo(_testData.isSslEnabled), "Connector SSL status mismatch.");
    }

    /// <summary>
    /// Tests the creation of a connector directly from a MetaSet using a typed factory method.
    /// </summary>
    [Test, Order(1)]
    public void CreateConnectorTest_FromMetaSet()
    {
        IBdoMetaObject meta = BdoConnectorFaker.NewMetaObject(_testData);
        _connector1 = ScopingTestData.Scope.CreateConnector<ConnectorFake>(meta);

        AssertFake(_connector1);
    }

    /// <summary>
    /// Tests the creation of a connector from a generic meta-object using type casting.
    /// </summary>
    [Test, Order(2)]
    public void CreateConnectorTest_FromConfig()
    {
        IBdoMetaObject meta = BdoConnectorFaker.NewMetaObject(_testData);
        _connector2 = ScopingTestData.Scope.CreateConnector(meta) as ConnectorFake;

        AssertFake(_connector2);
    }

    /// <summary>
    /// Tests the conversion of a POCO connector to metadata and back into a scoped connector.
    /// </summary>
    [Test, Order(3)]
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
    /// Tests the operational workflow of a connector, specifically its ability 
    /// to open a connection and perform Pull (Read) and Push (Write) operations.
    /// </summary>
    [Test, Order(4)]
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
            var paramSet = BdoData.NewNode(
                BdoData.NewObject(nameof(BdoSchema.GroupId)));

            var entities = conn.Pull(paramSet, log);

            // 2. Push the retrieved entities back
            conn.Push(null, entities?.ToArray());
        });
    }
}