using NUnit.Framework;

namespace BindOpen.Data.Stores;

/// <summary>
/// Contains unit tests for datasource management within the BindOpen scoping engine,
/// focusing on the instantiation and metadata wrapping of <see cref="BdoDatasource"/> instances.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoScope.NewMetaWrapper{T}"/> accurately wraps a raw
/// <see cref="IBdoMetaSet"/> into a strongly-typed <see cref="BdoDatasource"/> while preserving
/// its identity metadata such as <see cref="BdoDatasource.Name"/> and <see cref="BdoDatasource.DataType"/>.
/// </remarks>
[TestFixture]
public class BdoScopingDatasourceTests
{
    /// <summary>
    /// Gets or sets the shared <see cref="BdoDatasourceTests"/> fixture instance providing pre-configured metadata nodes.
    /// </summary>
    public BdoDatasourceTests _dataTests;

    /// <summary>
    /// Initializes the shared <see cref="BdoDatasourceTests"/> helper and prepares the test metadata set
    /// before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new BdoDatasourceTests();
        _dataTests.OneTimeSetUp();
    }

    /// <summary>
    /// Verifies that <see cref="IBdoScope.NewMetaWrapper{T}"/> wraps a raw <see cref="IBdoMetaSet"/>
    /// into a <see cref="BdoDatasource"/> instance and preserves its <see cref="BdoDatasource.Name"/>
    /// and <see cref="BdoDatasource.DataType"/> properties.
    /// </summary>
    [Test]
    public void Create1Test()
    {
        // 1. Create a BdoDatasource wrapper from a raw MetaSet.
        // This verifies that the scoping engine can map a MetaSet to the concrete BdoDatasource class.
        var source = ScopingTestData.Scope.NewMetaWrapper<BdoDatasource>(_dataTests._metaSet)
            .WithName(_dataTests._metaSet.Name)
            .WithDataType(_dataTests._metaSet.DataType);

        // 2. Verify identity mapping
        Assert.Multiple(() =>
        {
            Assert.That(source.DataType, Is.EqualTo(_dataTests._metaSet.DataType),
                "The wrapped datasource failed to maintain the original DataType.");

            Assert.That(source.Name, Is.EqualTo(_dataTests._metaSet.Name),
                "The wrapped datasource failed to maintain the original Name.");
        });
    }
}