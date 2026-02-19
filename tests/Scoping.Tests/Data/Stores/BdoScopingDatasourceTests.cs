using NUnit.Framework;

namespace BindOpen.Data.Stores;

/// <summary>
/// This class provides unit tests for data source management within the scoping engine,
/// focusing on the creation and metadata wrapping of datasource instances.
/// </summary>
[TestFixture, Order(210)]
public class BdoScopingDatasourceTests
{
    /// <summary>
    /// Shared data test instance providing pre-configured metadata nodes.
    /// </summary>
    public BdoDatasourceTests _dataTests;

    /// <summary>
    /// Initializes the test environment by preparing shared data sources.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoDatasourceTests();
        _dataTests.OneTimeSetUp();
    }

    /// <summary>
    /// Tests the creation of a BdoDatasource using the scope's MetaWrapper factory.
    /// </summary>
    [Test, Order(1)]
    public void Create1Test()
    {
        // 1. Create a BdoDatasource wrapper from a raw MetaNode.
        // This verifies that the scoping engine can map a MetaNode to the concrete BdoDatasource class.
        var source = ScopingTestData.Scope.NewMetaWrapper<BdoDatasource>(_dataTests._metaNode)
            .WithName(_dataTests._metaNode.Name)
            .WithDataType(_dataTests._metaNode.DataType);

        // 2. Verify identity mapping
        Assert.That(source.DataType, Is.EqualTo(_dataTests._metaNode.DataType),
            "The wrapped datasource failed to maintain the original DataType.");

        Assert.That(source.Name, Is.EqualTo(_dataTests._metaNode.Name),
            "The wrapped datasource failed to maintain the original Name.");
    }
}