using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Data.Elements
{
    [TestFixture]
    public class ScalarElementTest
    {
        [Test]
        public void TestCreateScalarElements()
        {
            ScalarElement element10 = new ScalarElement("text1", DataValueType.Text, "item1", "item2", "item3");
            ScalarElement element11 = new ScalarElement("integer1", DataValueType.Integer, 1,2,3);
            ScalarElement element12 = new ScalarElement("float1", DataValueType.Number, 1.1, 1.2, 1.3);

            // check insertion

            // check getWithName function

            // check deletion

            //Assert.That(log.Errors.Count == itemsNumber, "Bad insertion of errors ({0} expected; {1} found)", itemsNumber, log.Errors.Count);
            //Assert.That(log.Exceptions.Count == itemsNumber, "Bad insertion of exceptions ({0} expected; {1} found)", itemsNumber, log.Exceptions.Count);
            //Assert.That(log.Messages.Count == itemsNumber, "Bad insertion of messages ({0} expected; {1} found)", itemsNumber, log.Messages.Count);
            //Assert.That(log.Warnings.Count == itemsNumber, "Bad insertion of warnings ({0} expected; {1} found)", itemsNumber, log.Warnings.Count);
            //Assert.That(log.SubLogs.Count == itemsNumber, "Bad insertion of sub logs ({0} expected; {1} found)", itemsNumber, log.SubLogs.Count);
        }
    }
}
