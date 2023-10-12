using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Data.Stores;
using BindOpen.Kernel.Scoping;
using BindOpen.Kernel.Tests;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Kernel.Data
{
    [TestFixture, Order(210)]
    public class BdoDatasourceTests
    {
        public IBdoMetaNode _metaNode;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();

            _metaNode =
                BdoData.NewObject("testA")
                .WithId()
                .WithDataType(BdoExtensionKinds.Connector, "bindopen.kernel.tests$testConnector")
                .With(
                    BdoData.NewScalar("host", f.Random.Word()),
                    BdoData.NewScalar("port", f.Random.Int()),
                    BdoData.NewScalar("isSslEnabled", f.Random.Bool()));
        }

        [Test, Order(1)]
        public void Create1Test()
        {
            var source = SystemData.Scope.NewMetaWrapper<BdoDatasource>(_metaNode)
                .WithName(_metaNode.Name)
                .WithDataType(_metaNode.DataType);

            Assert.That(source.DataType == _metaNode.DataType, "Bad data type");
            Assert.That(source.Name == _metaNode.Name, "Bad data type");
        }
    }
}
