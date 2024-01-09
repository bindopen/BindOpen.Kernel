using BindOpen.Kernel.Tests;
using BindOpen.Scoping.Script;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Data
{
    [TestFixture, Order(210)]
    public class BdoReferenceTests
    {
        dynamic _valueSet;

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
        }

        [Test, Order(1)]
        public void Create1Test()
        {
            var reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Literal as string, BdoExpressionKind.Literal));

            Test(reference);
        }

        [Test, Order(2)]
        public void Create2Test()
        {
            var reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Script as string, BdoExpressionKind.Script));

            Test(reference);
        }

        [Test, Order(3)]
        public void Create3Test()
        {
            var reference = BdoData.NewReference(
                BdoScript.Func(_valueSet.ScriptwordName as string));

            Test(reference);
        }

        [Test, Order(4)]
        public void NewReferenceTest()
        {
            var set = BdoData.NewNode(
                    BdoData.NewMeta().WithReference(BdoData.NewReference(BdoScript.Func("eq", 1, 1))),
                    BdoData.NewMeta().WithReference(BdoScript.Func("eq", 1, 1))
                );

            var value1 = set[0].GetData<bool?>(SystemData.Scope);
            var value2 = set[1].GetData<bool?>(SystemData.Scope);

            Assert.That(value1 == true && value2 == true, "Error ");
        }

        [Test, Order(5)]
        public void NewReferenceFromScriptwordTest()
        {
            var reference = BdoScript.Eq(1, 0).ToReference();

            Test(reference);
        }
    }
}
