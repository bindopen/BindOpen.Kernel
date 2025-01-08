using NUnit.Framework;

namespace BindOpen.Data.Stores;

[TestFixture, Order(210)]
public class BdoScopingDatasourceTests
{
    public BdoDatasourceTests _dataTests;


    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(1)]
    public void Create1Test()
    {
        var source = ScopingTestData.Scope.NewMetaWrapper<BdoDatasource>(_dataTests._metaNode)
            .WithName(_dataTests._metaNode.Name)
            .WithDataType(_dataTests._metaNode.DataType);

        Assert.That(source.DataType == _dataTests._metaNode.DataType, "Bad data type");
        Assert.That(source.Name == _dataTests._metaNode.Name, "Bad data type");
    }
}
