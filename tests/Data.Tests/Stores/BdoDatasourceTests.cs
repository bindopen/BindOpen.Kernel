using BindOpen.Data.Meta;
using BindOpen.Scoping;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Data.Stores;

/// <summary>
/// Contains unit tests for <see cref="IBdoDatasource"/>, focusing on datasource initialization and metadata synchronization.
/// </summary>
/// <remarks>
/// These tests verify that datasource instances properly store connection settings,
/// extension data types, and automatically synchronize their internal detail metadata set.
/// </remarks>
[TestFixture]
public class BdoDatasourceTests
{
    /// <summary>
    /// Gets or sets the mock <see cref="IBdoMetaSet"/> representing connector configuration settings.
    /// </summary>
    public IBdoMetaSet _metaSet;

    /// <summary>
    /// Initializes a mock metadata object representing connector settings before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var f = new Faker();

        // Simulate a connector configuration (e.g., SMTP or Database)
        _metaSet = BdoMetaSetExtensions.Add(
            BdoData.NewObject("testA")
                .WithId()
                .WithDataType(BdoExtensionKinds.Connector, "bindopen.tests$testConnector"),
            BdoData.NewScalar("host", f.Random.Word()),
            BdoData.NewScalar("port", f.Random.Int()),
            BdoData.NewScalar("isSslEnabled", f.Random.Bool()));
    }

    /// <summary>
    /// Verifies the creation of a <see cref="BdoDatasource"/> via fluent builders and ensures
    /// that its internal <see cref="IBdoMetaSet"/> detail stays synchronized when properties are assigned or updated.
    /// </summary>
    /// <remarks>
    /// Ensures that setting a connection string or <see cref="DatasourceKind"/> automatically populates
    /// the internal <c>Detail</c> metadata set, and that calling <c>UpdateDetail(null)</c> safely preserves existing values.
    /// </remarks>
    [Test]
    public void Create2Test()
    {
        // 1. Initialize the datasource with fluent API
        var source = BdoData.New<BdoDatasource>()
            .WithName(_metaSet.Name)
            .WithDataType(_metaSet.DataType)
            .WithConnectionString("__connectionString__")
            .WithDatasourceKind(DatasourceKind.EmailServer);

        // 2. Test metadata update (passing null should not crash and preserve logic)
        source.UpdateDetail(null);

        // 3. Validations
        Assert.That(source.DataType, Is.EqualTo(_metaSet.DataType), "The datasource DataType does not match the source meta-node.");
        Assert.That(source.Name, Is.EqualTo(_metaSet.Name), "The datasource Name was not correctly assigned.");

        // Check if the internal Detail (metadata set) was automatically populated
        Assert.That(source.Detail?.GetData<string>("connectionString"), Is.EqualTo("__connectionString__"),
            "The connection string was not correctly mapped to the internal metadata detail.");

        Assert.That(source.Detail?.GetData<DatasourceKind?>("datasourceKind"), Is.EqualTo(DatasourceKind.EmailServer),
            "The datasource kind was not correctly mapped to the internal metadata detail.");
    }
}