using BindOpen.MetaData.Elements;
using NUnit.Framework;

namespace BindOpen.Tests.MetaData
{
    [TestFixture, Order(200)]
    public class BdoMetaTests
    {
        private dynamic _testData;

        private IBdoMetaElementSet _metaCarrierSet = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        public void Test(IBdoMetaElementSet elemSet)
        {
            var metaCarrier1 = elemSet.Get<IBdoMetaCarrier>("carrier1");
            var metaCarrier2 = elemSet["carrier2"] as IBdoMetaCarrier;
            var metaCarrier3 = elemSet.Get<IBdoMetaCarrier>(2);
            var metaCarrier4 = elemSet.Get<IBdoMetaCarrier>("carrier4");

            Assert.That(elemSet?.Count == 4, "Bad carrier element set - Count");

            var path1 = metaCarrier1?.Item().GetItem<string>("path");
            Assert.That(
                path1 == _testData.path1
                , "Bad carrier element - Set1");

            Assert.That(
                metaCarrier2?.SubItem<string>("path") == _testData.path2
                , "Bad carrier element - Set2");

            Assert.That(
                metaCarrier3?.Item()?.GetItem<string>("path") == _testData.path3
                , "Bad carrier element - Set3");

            Assert.That(
                metaCarrier4?.SubItem<string>("path") == _testData.path4
                , "Bad carrier element - Set4");
        }

        [Test, Order(1)]
        public void CreateMetaScalarTest()
        {
            // Object => MetaCollection
        }
    }
}
