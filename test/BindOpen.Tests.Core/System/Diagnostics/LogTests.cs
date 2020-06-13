using BindOpen.Data.Helpers.Serialization;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.System.Diagnostics
{
    [TestFixture, Order(400)]
    public class LogTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "Log.xml";

        private IBdoLog _log = null;

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = new
            {
                itemNumber = 1000
            };
        }

        private void Test(IBdoLog log)
        {
            Assert.That(log.Errors.Count == _testData.itemNumber, "Bad insertion of errors ({0} expected; {1} found)", _testData.itemNumber, _log.Errors.Count);
            Assert.That(log.Exceptions.Count == _testData.itemNumber, "Bad insertion of exceptions ({0} expected; {1} found)", _testData.itemNumber, _log.Exceptions.Count);
            Assert.That(log.Messages.Count == _testData.itemNumber, "Bad insertion of messages ({0} expected; {1} found)", _testData.itemNumber, _log.Messages.Count);
            Assert.That(log.Warnings.Count == _testData.itemNumber, "Bad insertion of warnings ({0} expected; {1} found)", _testData.itemNumber, _log.Warnings.Count);
            Assert.That(log.SubLogs.Count == _testData.itemNumber, "Bad insertion of sub logs ({0} expected; {1} found)", _testData.itemNumber, _log.SubLogs.Count);
        }

        [Test, Order(1)]
        public void CreateEventsTest()
        {
            _log = new BdoLog();

            for (int i = 0; i < _testData.itemNumber; i++)
            {
                _log.AddError("Error" + i);
                _log.AddException("Exception" + i);
                _log.AddMessage("Message" + i);
                _log.AddWarning("Warning" + i);
                _log.AddSubLog(new BdoLog());
            }

            Test(_log);
        }

        [Test, Order(2)]
        public void SaveEventsTest()
        {
            if (_log == null)
            {
                CreateEventsTest();
            }

            var log = new BdoLog();
            _log?.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Log saving failed. Result was '" + xml);
        }

        [Test, Order(3)]
        public void LoadEventsTest()
        {
            if (_log == null || !File.Exists(_filePath))
            {
                CreateEventsTest();
            }

            BdoLog log = new BdoLog();
            _log = XmlHelper.Load<BdoLog>(_filePath, log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Error while loading log. Result was '" + xml);

            Test(_log);
        }

    }
}
