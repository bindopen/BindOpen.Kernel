using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.Data.Elements
{
    [TestFixture]
    public class ScalarElementSetTest
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "ScalarElementSet.xml";

        private DataElementSet _scalarElementSetA = null;

        [SetUp]
        public void Setup()
        {
            _scalarElementSetA = new[]
            {
                ElementFactory.CreateScalar("float1", DataValueType.Number, 10),
                ElementFactory.CreateScalar("text1", DataValueType.Text, "item1", "item2", "item3"),
                ElementFactory.CreateScalar("integer1", DataValueType.Integer, 1, 2, 3),
                ElementFactory.CreateScalar("float2", DataValueType.Number, 1.1, 1.2, 1.3)
            };
        }

        [Test]
        public void TestCreateElementSet()
        {
            Assert.That(
                ((string)_scalarElementSetA["text1"]?[0] == "item1")
                && ((string)_scalarElementSetA["text1"]?[1] == "item2")
                && ((string)_scalarElementSetA["text1"]?[2] == "item3"), "Bad text scalar element creation");
            Assert.That(
                ((int)_scalarElementSetA["integer1"]?[0] == 1)
                && ((int)_scalarElementSetA["integer1"]?[1] == 2)
                && ((int)_scalarElementSetA["integer1"]?[2] == 3), "Bad integer scalar element creation");
            Assert.That(
                ((double)_scalarElementSetA["float2"]?[0] == 1.1)
                && ((double)_scalarElementSetA["float2"]?[1] == 1.2)
                && ((double)_scalarElementSetA["float2"]?[2] == 1.3), "Bad float scalar element creation");

            Assert.That(
                _scalarElementSetA.Count == 4, "Bad scalar element set creation");
        }

        [Test]
        public void TestUpdateCheckRepair()
        {
            var log = new BdoLog();

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
            var log = new BdoLog();

            _scalarElementSetA.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set saving failed. Result was '" + xml);
        }

        [Test]
        public void TestLoadDataElementSet()
        {
            var log = new BdoLog();

            if (_scalarElementSetA == null || !File.Exists(_filePath))
                TestSaveDataElementSet();

            var elementSet = XmlHelper.Load<DataElementSet>(_filePath, null, null, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Element set loading failed. Result was '" + xml);

            TestCreateElementSet();
        }
    }
}
