using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Script;
using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Tests.Data
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
            var meta1 = BdoMeta.NewObject()
                .WithDataReference(
                    BdoScript.Var("workflow").Func("input", "input1"));
        }

        [Test, Order(2)]
        public void LabelTest()
        {
            var meta1 = BdoMeta.NewScalar("toto", 23);
            meta1.WithLabel(LabelFormats.NameColonValue);
            Assert.That(meta1.GetOrAddSpec().Label == "{{$(this).prop('name')}}:{{$(this).value()}}", "Bad meta data label");

            var label = meta1.GetLabel(ScopingTests.Scope);
            Assert.That(label == "toto:23", "Bad meta data label");
        }
    }
}
