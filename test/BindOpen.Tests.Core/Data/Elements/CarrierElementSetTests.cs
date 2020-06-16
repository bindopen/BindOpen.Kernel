using BindOpen.Application.Scopes;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Extensions;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Core.Fakers;
using Bogus;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.Data.Elements
{
    [TestFixture, Order(200)]
    public class CarrierElementSetTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "CarrierElementSet.xml";

        private dynamic _testData;

        private IDataElementSet _carrierElementSet = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _testData = new
            {
                path1 = f.Random.Word() + "_1.txt",
                path2 = f.Random.Word() + "_2.txt",
                path3 = f.Random.Word() + "_3.txt",
                folderPath3 = f.Random.Word() + "_3.txt",
                path4 = f.Random.Word() + "_4.txt"
            };
        }

        private void Test(IDataElementSet elementSet)
        {
            var carrierElement1 = elementSet.Get<ICarrierElement>("carrier1");
            var carrierElement2 = elementSet.Get<ICarrierElement>("carrier2");
            var carrierElement3 = elementSet.Get<ICarrierElement>(2);
            var carrierElement4 = elementSet.Get<ICarrierElement>("carrier4");

            Assert.That(elementSet?.Count == 4, "Bad carrier element set - Count");

            Assert.That(
                carrierElement1?.Item()?.GetValue<string>("path") == _testData.path1
                , "Bad carrier element - Set1");

            Assert.That(
                carrierElement2?.Item()?.GetValue<string>("path") == _testData.path2
                , "Bad carrier element - Set2");

            Assert.That(
                carrierElement3?.Item()?.GetValue<string>("path") == _testData.path3
                , "Bad carrier element - Set3");

            Assert.That(
                carrierElement4?.Item()?.GetValue<string>("path") == _testData.path4
                , "Bad carrier element - Set4");
        }

        [Test, Order(1)]
        public void CreateCarrierElementSetTest()
        {
            var log = new BdoLog();

            var carrierElement1 = ElementFactory.CreateCarrier(
                "carrier1", "tests.core$testCarrier",
                BdoExtensionFactory.CreateCarrierConfiguration(
                    "tests.core$testCarrier",
                    ElementFactory.CreateScalar("path", _testData.path1)));

            var carrierElement2 = ElementFactory.CreateCarrier("carrier2", "tests.core$testCarrier")
                .WithConfiguration(ElementFactory.CreateSetFromObject<BdoCarrierConfiguration>(new { path = _testData.path2 }));

            var carrierElement3 = new CarrierFake(_testData.path3, _testData.folderPath3)?.AsElement();

            var carrierElement4 = GlobalVariables.Scope.CreateCarrier(
                BdoExtensionFactory.CreateCarrierConfiguration("tests.core$testCarrier")
                    .WithItems(ElementFactory.CreateSetFromObject(new { path = _testData.path4 })?.ToArray()),
                "carrier4", log)?.AsElement();

            _carrierElementSet = ElementFactory.CreateSet(
                carrierElement1, carrierElement2, carrierElement3, carrierElement4);

            Test(_carrierElementSet);
        }

        [Test, Order(2)]
        public void SaveDataElementSetTest()
        {
            if (_carrierElementSet == null)
            {
                CreateCarrierElementSetTest();
            }

            var log = new BdoLog();
            _carrierElementSet.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set saving failed" + xml);

            Test(_carrierElementSet);
        }

        [Test, Order(3)]
        public void LoadDataElementSetTest()
        {
            if (_carrierElementSet == null || !File.Exists(_filePath))
            {
                SaveDataElementSetTest();
            }

            var log = new BdoLog();
            var elementSet = XmlHelper.Load<DataElementSet>(_filePath, log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set loading failed" + xml);

            Test(elementSet);
        }
    }
}
