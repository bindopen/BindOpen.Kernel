using BindOpen.Framework.Core.System.Diagnostics;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.System.Diagnostics
{
    [TestFixture, Order(21)]
    public class LogTest
    {
        [Test, Order(1)]
        public void TestEvents()
        {
            ILog log = new Log();

            int itemsNumber = 180;
            for (int i = 0; i < itemsNumber; i++)
                log.AddError("Error" + i);
            for (int i = 0; i < itemsNumber; i++)
                log.AddException("Exception" + i);
            for (int i = 0; i < itemsNumber; i++)
                log.AddMessage("Message" + i);
            for (int i = 0; i < itemsNumber; i++)
                log.AddWarning("Warning" + i);
            for (int i = 0; i < itemsNumber; i++)
                log.AddSubLog(new Log());
            log.SaveXml(@"G:\temp\log.xml");

            //Assert.That(log.Errors.Count == itemsNumber, "Bad insertion of errors ({0} expected; {1} found)", itemsNumber, log.Errors.Count);
            //Assert.That(log.Exceptions.Count == itemsNumber, "Bad insertion of exceptions ({0} expected; {1} found)", itemsNumber, log.Exceptions.Count);
            //Assert.That(log.Messages.Count == itemsNumber, "Bad insertion of messages ({0} expected; {1} found)", itemsNumber, log.Messages.Count);
            //Assert.That(log.Warnings.Count == itemsNumber, "Bad insertion of warnings ({0} expected; {1} found)", itemsNumber, log.Warnings.Count);
            //Assert.That(log.SubLogs.Count == itemsNumber, "Bad insertion of sub logs ({0} expected; {1} found)", itemsNumber, log.SubLogs.Count);
        }
    }
}
