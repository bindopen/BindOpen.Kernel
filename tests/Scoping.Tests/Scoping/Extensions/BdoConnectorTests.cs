using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Tests;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Scoping.Connectors;

[TestFixture, Order(301)]
public class BdoConnectorTests
{
    private dynamic _testData;

    public ConnectorFake _connector1;
    public ConnectorFake _connector2;
    public ConnectorFake _connector3;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoConnectorFaker.NewData();
    }

    public void AssertFake(ConnectorFake connector)
    {
        Assert.That(connector != null, "Connector missing");

        Assert.That(connector.Host == _testData.host, "Bad connector");
        Assert.That(connector.Port?.GetData<int?>() == _testData.port, "Bad connector");
        Assert.That((connector.IsSslEnabled ?? false) == _testData.isSslEnabled, "Bad connector");
    }

    [Test, Order(1)]
    public void CreateConnectorTest_FromMetaSet()
    {
        IBdoMetaObject meta = BdoConnectorFaker.NewMetaObject(_testData);
        _connector1 = ScopingTestData.Scope.CreateConnector<ConnectorFake>(meta);

        AssertFake(_connector1);
    }

    [Test, Order(2)]
    public void CreateConnectorTest_FromConfig()
    {
        IBdoMetaObject meta = BdoConnectorFaker.NewMetaObject(_testData);
        _connector2 = ScopingTestData.Scope.CreateConnector(meta) as ConnectorFake;

        AssertFake(_connector2);
    }

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

        var meta = connector.ToMeta(ScopingTestData.Scope);
        _connector3 = ScopingTestData.Scope.CreateConnector(meta) as ConnectorFake;

        AssertFake(_connector3);
    }

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

        connector.UsingConnection((conn, log) =>
        {
            var paramSet = BdoData.NewSet(
                BdoData.NewObject(nameof(BdoSpec.GroupId)));
            var entities = conn.Pull(paramSet, log);

            conn.Push(null, entities?.ToArray());
        });
    }
}
