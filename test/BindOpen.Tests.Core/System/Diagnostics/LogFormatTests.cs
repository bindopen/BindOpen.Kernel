using BindOpen.System.Diagnostics;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Core.System.Diagnostics
{
    [TestFixture, Order(401)]
    public class LogFormatTests
    {
        private readonly string _filePath = GlobalVariables.WorkingFolder + "Log.xml";

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var log = new BdoLog();
            for (int i = 0; i < 2; i++)
            {
                log.AddError("Error" + i);
                log.AddException("Exception" + i);
                log.AddMessage("Message" + i);
                log.AddWarning("Warning" + i);
                log.AddSubLog(new BdoLog() { DisplayName = "Sub log" + i });
            }

            _testData = new
            {
                log
            };
        }

        [Test, Order(1)]
        public void LogToStringTest()
        {
            BdoLog log = _testData.log;
            var st = log.ToString<BdoSnapLoggerFormat>();
            var st_expected =
                "- Error0" + Environment.NewLine +
                "- Exception0" + Environment.NewLine +
                "- Message0" + Environment.NewLine +
                "- Warning0" + Environment.NewLine +
                "o Sub log0" + Environment.NewLine +
                "- Error1" + Environment.NewLine +
                "- Exception1" + Environment.NewLine +
                "- Message1" + Environment.NewLine +
                "- Warning1" + Environment.NewLine +
                "o Sub log1" + Environment.NewLine;

            Assert.That(st == st_expected, "Bad ToString function.");
        }

        [Test, Order(2)]
        public void LogEventToStringTest()
        {
            BdoLogEvent logEvent = _testData.log.Events[0];
            var st = logEvent.ToString<BdoSnapLoggerFormat>();
            var st_expected = "- Error0";

            Assert.That(st == st_expected, "Bad ToString function.");
        }
    }
}
