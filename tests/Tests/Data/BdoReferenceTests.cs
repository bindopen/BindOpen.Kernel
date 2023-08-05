using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Script;
using BindOpen.System.Tests;
using Bogus;
using NUnit.Framework;

namespace BindOpen.System.Data
{
    [TestFixture, Order(210)]
    public class BdoReferenceTests
    {
        dynamic _valueSet;
        private IBdoReference _reference = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _valueSet = new
            {
                Script = "$(var1)",
                Literal = f.Random.Word(),
                ScriptwordName = "func1"
            };
        }

        public void Test(IBdoReference reference)
        {
            //switch (reference.Kind)
            //{
            //    case BdoReferenceKind.Expression:
            //        Assert.That(reference.Text == _valueSet.Literal);
            //        break;
            //    case BdoReferenceKind.Identifier:
            //        Assert.That(reference.Text == _valueSet.Script);
            //        break;
            //    case BdoReferenceKind.MetaData:
            //        Assert.That(reference.Text == _valueSet.Script);
            //        break;
            //    case BdoReferenceKind.Word:
            //        Assert.That(reference.Text == _valueSet.Script);
            //        break;
            //}
        }

        [Test, Order(1)]
        public void Create1Test()
        {
            _reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Literal as string, BdoExpressionKind.Literal));

            Test(_reference);
        }

        [Test, Order(2)]
        public void Create2Test()
        {
            _reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Script as string, BdoExpressionKind.Script));

            Test(_reference);
        }

        [Test, Order(3)]
        public void Create3Test()
        {
            _reference = BdoData.NewReference(
                BdoScript.Func(_valueSet.ScriptwordName as string));

            Test(_reference);
        }

        [Test, Order(4)]
        public void NewReferenceTest()
        {
            var set = BdoData.NewMetaComposite(
                    BdoData.NewMeta().WithDataReference(BdoData.NewReference(BdoScript.Func("eq", 1, 1))),
                    BdoData.NewMeta().WithDataReference(BdoScript.Func("eq", 1, 1))
                );

            var value1 = set[0].GetData<bool?>(SystemData.Scope);
            var value2 = set[1].GetData<bool?>(SystemData.Scope);

            Assert.That(value1 == true && value2 == true, "Error ");
        }
    }
}
