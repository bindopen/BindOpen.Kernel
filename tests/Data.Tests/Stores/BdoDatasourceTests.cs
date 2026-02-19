using BindOpen.Data.Meta;
using BindOpen.Scoping;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Data.Stores;

/// <summary>
/// Contains unit tests for <see cref="IBdoDatasource"/>, focusing on datasource initialization and metadata updates.
/// </summary>
/// <remarks>
/// These tests verify that datasource objects correctly store connection information, 
/// extension types, and that their internal metadata details are properly synchronized.
/// </remarks>
[TestFixture, Order(210)]
public class BdoDatasourceTests
{
    /// <summary>
    /// The meta node to consider.
    /// </summary>
    public IBdoMetaNode _metaNode;

    /// <summary>
    /// Initializes a mock metadata node representing connector settings.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var f = new Faker();

        // We simulate a connector configuration (e.g., SMTP or Database)
        _metaNode = BdoData.NewObject("testA")
            .WithId()
            .WithDataType(BdoExtensionKinds.Connector, "bindopen.tests$testConnector")
            .With(
                BdoData.NewScalar("host", f.Random.Word()),
                BdoData.NewScalar("port", f.Random.Int()),
                BdoData.NewScalar("isSslEnabled", f.Random.Bool()));
    }

    /// <summary>
    /// Tests the creation of a datasource and the automatic synchronization of its detail metadata.
    /// </summary>
    /// <remarks>
    /// This test ensures that when a connection string or a datasource kind is set, 
    /// the internal <see cref="IBdoMetaSet"/> (Detail) reflects these values.
    /// </remarks>
    [Test, Order(2)]
    public void Create2Test()
    {
        // 1. Initialize the datasource with fluent API
        var source = BdoData.New<BdoDatasource>()
            .WithName(_metaNode.Name)
            .WithDataType(_metaNode.DataType)
            .WithConnectionString("__connectionString__")
            .WithDatasourceKind(DatasourceKind.EmailServer);

        // 2. Test metadata update (passing null should not crash and preserve logic)
        source.UpdateDetail(null);

        // 3. Validations
        Assert.That(source.DataType, Is.EqualTo(_metaNode.DataType), "The datasource DataType does not match the source meta-node.");
        Assert.That(source.Name, Is.EqualTo(_metaNode.Name), "The datasource Name was not correctly assigned.");

        // Check if the internal Detail (metadata set) was automatically populated
        Assert.That(source.Detail?.GetData<string>("connectionString"), Is.EqualTo("__connectionString__"),
            "The connection string was not correctly mapped to the internal metadata detail.");

        Assert.That(source.Detail?.GetData<DatasourceKind?>("datasourceKind"), Is.EqualTo(DatasourceKind.EmailServer),
            "The datasource kind was not correctly mapped to the internal metadata detail.");
    }
}