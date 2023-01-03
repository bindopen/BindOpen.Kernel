using BindOpen.Extensions.Scripting;
using BindOpen.Data.Items;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Runtime.Tests.MetaData.Items
{
    [TestFixture, Order(210)]
    public class BdoExpressionTests
    {
        dynamic _valueSet;
        private BdoExpression _expression = null;


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

        public void Test(IBdoExpression expression)
        {
            switch (expression.Kind)
            {
                case BdoExpressionKind.Literal:
                    Assert.That(expression.Text == _valueSet.Literal);
                    break;
                case BdoExpressionKind.Script:
                    Assert.That(expression.Text == _valueSet.Script);
                    break;
                case BdoExpressionKind.Word:
                    Assert.That(expression.Word?.Name.Equals(_valueSet.ScriptwordName));
                    break;
            }
        }

        [Test, Order(1)]
        public void Create1Test()
        {
            _expression = BdoItems.NewExpression(
                BdoExpressionKind.Literal,
                _valueSet.Literal as string);

            Test(_expression);
        }

        [Test, Order(2)]
        public void Create2Test()
        {
            _expression = BdoItems.NewExpression(
                BdoExpressionKind.Script,
                _valueSet.Script as string);

            Test(_expression);
        }

        [Test, Order(3)]
        public void Create3Test()
        {
            _expression = BdoItems.NewExpression(
                BdoScript.Function(_valueSet.ScriptwordName as string));

            Test(_expression);
        }
    }
}
