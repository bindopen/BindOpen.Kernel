using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using BindOpen.System.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.System.Data
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
            var obj = meta?.GetData();
            Assert.That(obj?.IsDeepEqual(_obj) == true, "Bad obj element set - Count");
        }

        [Test, Order(1)]
        public void ToMetaTest()
        {
            var meta = _obj.ToMetaData();
            Test(meta);
        }

        [Test, Order(2)]
        public void NewTest()
        {
            var meta1 = BdoMeta.NewObject(_obj);
            Test(meta1);

            var meta2 = BdoMeta.NewObject()
                .WithData(_obj);
            Test(meta2);
        }
    }
}
