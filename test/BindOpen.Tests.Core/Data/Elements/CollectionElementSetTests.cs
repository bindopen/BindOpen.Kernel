using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using Bogus;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Tests.Core.Data.Elements
{
    [TestFixture, Order(201)]
    public class CollectionElementSetTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "CollectionElementSet.xml";

        private dynamic _testData;

        private IDataElementSet _collectionElementSet = null;

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

        private void Test(IDataElementSet elementSet)
        {
            _ = elementSet.Get<ICarrierElement>("collection1");
            _ = elementSet.Get<ICarrierElement>(1);

            Assert.That(elementSet?.Count == 2, "Bad collection element set - Count");
        }

        [Test, Order(1)]
        public void CreateCollectionElementSetTest()
        {
            var collectionElement1 = ElementFactory.CreateCollection(
                "collection1",
                (((string Key, string Value)[])_testData.collectionStringValues1).Select(p => ElementFactory.CreateScalar(p.Key, p.Value)).ToArray()
            );
            collectionElement1.Add(
                (((string Key, double Value)[])_testData.collectionDoubleValues1).Select(p => ElementFactory.CreateScalar(p.Key, p.Value)).ToArray());

            var collectionElement2 = ElementFactory.CreateCollection(
                "collection2",
                (((string Key, string Value)[])_testData.collectionStringValues2).Select(p => ElementFactory.CreateScalar(p.Key, p.Value)).ToArray()
            );
            collectionElement2.Add(
                (((string Key, double Value)[])_testData.collectionDoubleValues2).Select(p => ElementFactory.CreateScalar(p.Key, p.Value)).ToArray());
            collectionElement2.Add(
                ElementFactory.CreateCarrier("collection2", "tests.core$testCarrier")
                    .WithItems(
                        ElementFactory.CreateSetFromObject<BdoCarrierConfiguration>(new { path = "file2.txt" })));

            _collectionElementSet = ElementFactory.CreateSet(collectionElement1, collectionElement2);

            Test(_collectionElementSet);
        }

        [Test, Order(3)]
        public void SaveDataElementSetTest()
        {
            if (_collectionElementSet == null)
            {
                CreateCollectionElementSetTest();
            }

            var log = new BdoLog();
            _collectionElementSet.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set saving failed. Result was '" + xml);
        }

        [Test, Order(4)]
        public void LoadDataElementSetTest()
        {
            var log = new BdoLog();

            if (_collectionElementSet == null || !File.Exists(_filePath))
            {
                SaveDataElementSetTest();
            }

            var elementSet = XmlHelper.Load<DataElementSet>(_filePath, log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set loading failed. Result was '" + xml);

            Test(elementSet);
        }
    }
}
