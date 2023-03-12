using BindOpen.Data;
using BindOpen.Script;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Tests.Data
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

        public void Test(IBdoReference exp)
        {
            switch (exp.Kind)
            {
                case BdoExpressionKind.Literal:
                    Assert.That(exp.Text == _valueSet.Literal);
                    break;
                case BdoExpressionKind.Script:
                    Assert.That(exp.Text == _valueSet.Script);
                    break;
                    //case BdoExpressionKind.Word:
                    //    Assert.That(exp.Word?.Name.Equals(_valueSet.ScriptwordName));
                    //    break;
            }
        }

        [Test, Order(1)]
        public void Create1Test()
        {
            _reference = BdoData.NewReference(
                _valueSet.Literal as string,
                BdoExpressionKind.Literal);

            Test(_reference);
        }

        [Test, Order(2)]
        public void Create2Test()
        {
            _reference = BdoData.NewReference(_valueSet.Script as string)
                .AsScript();

            Test(_reference);
        }

        [Test, Order(3)]
        public void Create3Test()
        {
            _reference = BdoData.NewReference(
                BdoScript.Func(_valueSet.ScriptwordName as string));

            Test(_reference);
        }
    }
}
