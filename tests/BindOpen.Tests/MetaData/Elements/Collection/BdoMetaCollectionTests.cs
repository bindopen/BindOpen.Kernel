using BindOpen.Extensions.Modeling;
using BindOpen.MetaData;
using BindOpen.MetaData.Elements;
using Bogus;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Tests.MetaData
{
    [TestFixture, Order(201)]
    public class BdoMetaCollectionTests
    {
        private dynamic _testData;

        private IBdoMetaCollection _collectionElement = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _testData = new
            {
                collectionStringValues1 = Enumerable.Range(0, 10).Select(p => (Key: f.Random.Word(), Value: f.Random.Word())).ToArray(),
                collectionStringValues2 = Enumerable.Range(0, 10).Select(p => (Key: f.Random.Word(), Value: f.Random.Word())).ToArray(),
                collectionDoubleValues1 = Enumerable.Range(0, 10).Select(p => (Key: f.Random.Word(), Value: f.Random.Double())).ToArray(),
                collectionDoubleValues2 = Enumerable.Range(0, 10).Select(p => (Key: f.Random.Word(), Value: f.Random.Double())).ToArray(),
            };
        }

        private static void Test(IBdoMetaCollection collection)
        {
            var el1 = collection.Get("collection1");
            var el2 = collection.Get<IBdoMetaCollection>(1);

            var item2 = el2.Get<IBdoMetaCarrier>("collection2");

            Assert.That(collection?.Count == 2, "Bad collection element set - Count");
        }

        [Test, Order(1)]
        public void CreateCollectionElementSetTest()
        {
            (string Key, string Value)[] collectionStringValues = _testData.collectionStringValues1;
            (string Key, double Value)[] collectionDoubleValues = _testData.collectionDoubleValues1;

            var collectionElement1 = BdoMeta.NewCollection(
                "collection1",
                collectionStringValues.Select(p => BdoMeta.NewScalar(p.Key, p.Value)).ToArray(),
                collectionDoubleValues.Select(p => BdoMeta.NewScalar(p.Key, p.Value)).ToArray()
            );

            var collectionElement2 = BdoMeta.NewCollection(
                "collection2",
                collectionStringValues.Select(p => BdoMeta.NewScalar(p.Key, p.Value)).ToArray(),
                (((string Key, double Value)[])_testData.collectionDoubleValues2).Select(p => BdoMeta.NewScalar(p.Key, p.Value)).ToArray());
            collectionElement2
                .Add(
                    BdoMeta.NewCarrier("collection2", "tests.core$testCarrier")
                        .WithItems(
                            (new { path = "file2.txt" }).AsElementSet<BdoCarrierConfiguration>()));

            _collectionElement = BdoMeta.NewCollection(collectionElement1, collectionElement2);

            Test(_collectionElement);
        }
    }
}
