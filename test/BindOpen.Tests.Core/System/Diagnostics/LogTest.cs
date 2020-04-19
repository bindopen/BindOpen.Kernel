using BindOpen.Data.Helpers.Serialization;
using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.System.Diagnostics
{
    [TestFixture, Order(21)]
    public class LogTest
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "Log.xml";

        private const int _itemsNumber = 180;

        private IBdoLog _log = null;

        public void CreateEvents()
        {
            _log = new BdoLog();

            for (int i = 0; i < _itemsNumber; i++)
                _log.AddError("Error" + i);
            for (int i = 0; i < _itemsNumber; i++)
                _log.AddException("Exception" + i);
            for (int i = 0; i < _itemsNumber; i++)
                _log.AddMessage("Message" + i);
            for (int i = 0; i < _itemsNumber; i++)
                _log.AddWarning("Warning" + i);
            for (int i = 0; i < _itemsNumber; i++)
                _log.AddSubLog(new BdoLog());
        }

        [Test, Order(1)]
        public void TestCreateEvents()
        {
            if (_log == null)
            {
                CreateEvents();
            }

            Assert.That(_log.Errors.Count == _itemsNumber, "Bad insertion of errors ({0} expected; {1} found)", _itemsNumber, _log.Errors.Count);
            Assert.That(_log.Exceptions.Count == _itemsNumber, "Bad insertion of exceptions ({0} expected; {1} found)", _itemsNumber, _log.Exceptions.Count);
            Assert.That(_log.Messages.Count == _itemsNumber, "Bad insertion of messages ({0} expected; {1} found)", _itemsNumber, _log.Messages.Count);
            Assert.That(_log.Warnings.Count == _itemsNumber, "Bad insertion of warnings ({0} expected; {1} found)", _itemsNumber, _log.Warnings.Count);
            Assert.That(_log.SubLogs.Count == _itemsNumber, "Bad insertion of sub logs ({0} expected; {1} found)", _itemsNumber, _log.SubLogs.Count);
        }

        [Test, Order(2)]
        public void TestSaveEvents()
        {
            if (_log == null)
            {
                CreateEvents();
            }

            _log?.SaveXml(_filePath);
        }

        [Test, Order(3)]
        public void TestLoadEvents()
        {
            if (!File.Exists(_filePath))
                TestSaveEvents();

            BdoLog log = new BdoLog();
            _log = XmlHelper.Load<BdoLog>(_filePath, log: log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Error while loading log. Result was '" + xml);

            TestCreateEvents();
        }

    }
}
