using BindOpen.Data;
using BindOpen.Data.Meta;
using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Tests.Data
{
    [TestFixture, Order(201)]
    public class BdoMetaObjectTests
    {
        private object _obj = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _obj = ClassObjectFaker.Fake();
        }

        private void Test(IBdoMetaData meta)
        {
            var obj = meta?.Item();
            Assert.That(obj?.IsDeepEqual(_obj) == true, "Bad obj element set - Count");
        }

        [Test, Order(1)]
        public void ToMetaTest()
        {
            var meta = _obj.ToMetaData();
            Test(meta);
        }

        [Test, Order(1)]
        public void NewTest()
        {
            var meta = BdoMeta.NewObject(_obj);
            Test(meta);
        }
    }
}
