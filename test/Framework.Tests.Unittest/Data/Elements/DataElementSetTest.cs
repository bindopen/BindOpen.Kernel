using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Elements.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Data.Elements
{
    [TestFixture]
    public class DataElementSetTest
    {
        private IScalarElement _element10 = null;
        private IScalarElement _element11 = null;
        private IScalarElement _element12 = null;
        //private DataElementSet _elementSetA = null;
        //private DataElementSet _elementSetB = null;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCreateDataElementSet()
        {
            this._element10 = ElementFactory.CreateScalar("text1", DataValueType.Text, "item1", "item2", "item3");
            this._element11 = ElementFactory.CreateScalar("integer1", DataValueType.Integer, 1, 2, 3);
            this._element12 = ElementFactory.CreateScalar("float1", DataValueType.Number, 1.1, 1.2, 1.3);

            //this._elementSetA = new DataElementSet(this._element10, this._element11, this._element12);
            //this._elementSetB = new DataElementSet(this._element10, this._element11);

            //Assert.That(log.Errors.Count == itemsNumber, "Bad insertion of errors ({0} expected; {1} found)", itemsNumber, log.Errors.Count);
        }

        [Test]
        public void TestUpdateCheckRepair()
        {
            ILog log = new Log();

            // test update
            //log = this._elementSetB.Update(this._elementSetA);

            //Assert.That(log.Errors.Count == itemsNumber, "Bad insertion of errors ({0} expected; {1} found)", itemsNumber, log.Errors.Count);
            //Assert.That(log.Exceptions.Count == itemsNumber, "Bad insertion of exceptions ({0} expected; {1} found)", itemsNumber, log.Exceptions.Count);
            //Assert.That(log.Messages.Count == itemsNumber, "Bad insertion of messages ({0} expected; {1} found)", itemsNumber, log.Messages.Count);
            //Assert.That(log.Warnings.Count == itemsNumber, "Bad insertion of warnings ({0} expected; {1} found)", itemsNumber, log.Warnings.Count);
            //Assert.That(log.SubLogs.Count == itemsNumber, "Bad insertion of sub logs ({0} expected; {1} found)", itemsNumber, log.SubLogs.Count);
        }
    }
}
