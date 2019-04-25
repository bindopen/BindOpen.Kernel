using System.IO;
using BindOpen.Framework.Core.Data.Elements.Carrier;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.Extensions.Items.Carriers;
using BindOpen.Framework.Core.Extensions.Items.Factories;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Standard.Extensions.Carriers;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Data.Elements
{
    [TestFixture]
    public class CarrierElementSetTest
    {
        private readonly string _filePath = SetupVariables.WorkingFolder + "CarrierElementSet.xml";

        private ICarrierElement _carrierElement1 = null;
        private ICarrierElement _carrierElement2 = null;
        private ICarrierElement _carrierElement3 = null;
        private ICarrierElement _carrierElement4 = null;

        private DataElementSet _carrierElementSetA = null;

        [SetUp]
        public void Setup()
        {
            Log log = new Log();

            _carrierElement1 = ElementFactory.CreateCarrier(
                "carrier1", "standard$file",
                new CarrierConfiguration(
                    ElementFactory.CreateScalar("path", "file1.txt")));

            _carrierElement2 = ElementFactory.CreateCarrier(
                "carrier2", "standard$file",
                ElementFactory.CreateSet<CarrierConfiguration>(new { path = "file2.txt" }));

            _carrierElement3 = new RepositoryFile("file3.txt", "myfolder")?.AsElement();

            _carrierElement4 = SetupVariables.AppScope.CreateCarrier(
                new CarrierConfiguration(
                    "standard$file",
                    ElementFactory.CreateElementArray(new { path = "file4.txt" })),
                "carrier4", log)?.AsElement();

            _carrierElementSetA = new DataElementSet(_carrierElement1, _carrierElement2, _carrierElement3, _carrierElement4);
        }

        [Test]
        public void TestCreateCarrierElementSet()
        {
            Assert.That(
                ((string)_carrierElement1.First?["path"] == "file1.txt")
                && ((string)_carrierElement2.First?["path"] == "file2.txt")
                && ((string)_carrierElement3.First?["path"] == "file3.txt")
                && ((string)_carrierElement4.First?["path"] == "file4.txt")
                , "Bad carrier element creation");

            Assert.That(
                (string)(_carrierElementSetA[0] as CarrierElement)?.First?["path"] == "file1.txt"
                , "Bad carrier element set indexation");

            Assert.That(
                _carrierElementSetA.Count == 4, "Bad carrier element set creation");
        }

        [Test]
        public void TestUpdateCheckRepair()
        {
            ILog log = new Log();

            //test update
            //log = _scalarElementSetB.Update(_scalarElementSetA);

            //Assert.That(log.Errors.Count == itemsNumber, "Bad insertion of errors ({0} expected; {1} found)", itemsNumber, log.Errors.Count);
            //Assert.That(log.Exceptions.Count == itemsNumber, "Bad insertion of exceptions ({0} expected; {1} found)", itemsNumber, log.Exceptions.Count);
            //Assert.That(log.Messages.Count == itemsNumber, "Bad insertion of messages ({0} expected; {1} found)", itemsNumber, log.Messages.Count);
            //Assert.That(log.Warnings.Count == itemsNumber, "Bad insertion of warnings ({0} expected; {1} found)", itemsNumber, log.Warnings.Count);
            //Assert.That(log.SubLogs.Count == itemsNumber, "Bad insertion of sub logs ({0} expected; {1} found)", itemsNumber, log.SubLogs.Count);
        }

        [Test]
        public void TestSaveDataElementSet()
        {
            ILog log = new Log();

            _carrierElementSetA.SaveXml(_filePath, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Element set saving failed. Result was '" + log.ToXml());
        }

        [Test]
        public void TestLoadDataElementSet()
        {
            ILog log = new Log();

            if (_carrierElementSetA == null || !File.Exists(_filePath))
                TestSaveDataElementSet();

            var elementSet = XmlHelper.Load<DataElementSet>(_filePath, null, null, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Element set saving failed. Result was '" + log.ToXml());
        }
    }
}
