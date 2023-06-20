using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;
using BindOpen.System.Tests;
using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.System.Data
{
    [TestFixture, Order(201)]
    public class BdoMetaDataExpressionTests
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
        public void DataReferenceTest()
        {
            var meta1 = BdoData.NewMetaObject()
                .WithDataReference(
                    BdoScript.Var("workflow").Func("input", "input1"));
        }

        [Test, Order(2)]
        public void LabelTest()
        {
            var meta1 = BdoData.NewMetaScalar("toto", 23);
            meta1.WithLabel(LabelFormats.NameColonValue);
            Assert.That(meta1.GetOrAddSpec().Label == "{{$(this).(name)}}:{{$(this).value()}}", "Bad meta data label");

            var label = meta1.GetLabel(SystemData.Scope);
            Assert.That(label == "toto:23", "Bad meta data label");
        }
    }
}
