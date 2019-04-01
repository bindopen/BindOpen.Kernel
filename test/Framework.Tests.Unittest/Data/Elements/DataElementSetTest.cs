using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Data.Elements
{
    [TestFixture]
    public class DataElementSetTest
    {

        private ScalarElement _Element10 = null;
        private ScalarElement _Element11 = null;
        private ScalarElement _Element12 = null;
        //private DataElementSet _ElementSetA = null;
        //private DataElementSet _ElementSetB = null;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCreateDataElementSet()
        {
            this._Element10 = new ScalarElement("text1", DataValueType.Text, "item1", "item2", "item3");
            this._Element11 = new ScalarElement("integer1", DataValueType.Integer, 1, 2, 3);
            this._Element12 = new ScalarElement("float1", DataValueType.Number, 1.1, 1.2, 1.3);

            //this._ElementSetA = new DataElementSet(this._Element10, this._Element11, this._Element12);
            //this._ElementSetB = new DataElementSet(this._Element10, this._Element11);

            //Assert.That(log.Errors.Count == itemsNumber, "Bad insertion of errors ({0} expected; {1} found)", itemsNumber, log.Errors.Count);
        }

        [Test]
        public void TestUpdateCheckRepair()
        {
            ILog log = new Log();

            // test update
            //log = this._ElementSetB.Update(this._ElementSetA);

            //Assert.That(log.Errors.Count == itemsNumber, "Bad insertion of errors ({0} expected; {1} found)", itemsNumber, log.Errors.Count);
            //Assert.That(log.Exceptions.Count == itemsNumber, "Bad insertion of exceptions ({0} expected; {1} found)", itemsNumber, log.Exceptions.Count);
            //Assert.That(log.Messages.Count == itemsNumber, "Bad insertion of messages ({0} expected; {1} found)", itemsNumber, log.Messages.Count);
            //Assert.That(log.Warnings.Count == itemsNumber, "Bad insertion of warnings ({0} expected; {1} found)", itemsNumber, log.Warnings.Count);
            //Assert.That(log.SubLogs.Count == itemsNumber, "Bad insertion of sub logs ({0} expected; {1} found)", itemsNumber, log.SubLogs.Count);
        }
    }
}
