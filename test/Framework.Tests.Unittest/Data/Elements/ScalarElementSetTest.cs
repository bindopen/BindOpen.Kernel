using System.IO;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.UnitTest;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Data.Elements
{
    [TestFixture]
    public class ScalarElementSetTest
    {
        private readonly string _filePath = SetupVariables.WorkingFolder + "ScalarElementSet.xml";

        private DataElementSet _scalarElementSetA = null;

        [SetUp]
        public void Setup()
        {
            _scalarElementSetA = new DataElementSet(
                ElementFactory.CreateScalar("float1", DataValueType.Number, 10),
                ElementFactory.CreateScalar("text1", DataValueType.Text, "item1", "item2", "item3"),
                ElementFactory.CreateScalar("integer1", DataValueType.Integer, 1, 2, 3),
                ElementFactory.CreateScalar("float2", DataValueType.Number, 1.1, 1.2, 1.3));
        }

        [Test]
        public void TestCreateElementSet()
        {
            Assert.That(
                ((string)_scalarElementSetA["text1"]?[0] == "item1")
                && ((string)_scalarElementSetA["text1"]?[1] == "item2")
                && ((string)_scalarElementSetA["text1"]?[2]=="item3"), "Bad text scalar element creation");
            Assert.That(
                ((int)_scalarElementSetA["integer1"]?[0] == 1)
                && ((int)_scalarElementSetA["integer1"]?[1] == 2)
                && ((int)_scalarElementSetA["integer1"]?[2] == 3), "Bad integer scalar element creation");
            Assert.That(
                ((double)_scalarElementSetA["float2"]?[0] == 1.1)
                && ((double)_scalarElementSetA["float2"]?[1] == 1.2)
                && ((double)_scalarElementSetA["float2"]?[2] == 1.3), "Bad float scalar element creation");

            Assert.That(
                _scalarElementSetA.Count==4, "Bad scalar element set creation");
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

            _scalarElementSetA.SaveXml(_filePath, log);

            Assert.That(!log.HasErrorsOrExceptions(), "Element set saving failed. Result was '" + log.ToXml());
        }

        [Test]
        public void TestLoadDataElementSet()
        {
            ILog log = new Log();

            if (_scalarElementSetA == null || !File.Exists(_filePath))
                TestSaveDataElementSet();

            var elementSet = XmlHelper.Load<DataElementSet>(_filePath, log);

            TestCreateElementSet();
        }
    }
}
