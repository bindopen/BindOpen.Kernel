using BindOpen.Scoping.Script;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class BdoExpressionTests
{
    dynamic _valueSet;
    public IBdoExpression _exp = null;

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

    public static void AssertEquals(
        IBdoExpression exp1,
        IBdoExpression exp2)
    {
        if ((exp1 != null && exp2 == null) || (exp1 == null && exp2 != null))
        {
            Assert.That(Equals(exp1, exp2), "Unmatched objects");
        }

        var deepEq = exp1.WithDeepEqual(exp2);
        deepEq.Assert();
    }

    public void Test(IBdoExpression exp)
    {
        switch (exp.ExpressionKind)
        {
            case BdoExpressionKind.Literal:
                Assert.That(exp.Text == _valueSet.Literal);
                break;
            case BdoExpressionKind.Script:
                Assert.That(exp.Text == _valueSet.Script);
                break;
        }
    }

    [Test, Order(1)]
    public void Create1Test()
    {
        _exp = BdoData.NewExp(
            _valueSet.Literal as string,
            BdoExpressionKind.Literal);

        Test(_exp);
    }

    [Test, Order(2)]
    public void Create2Test()
    {
        _exp = BdoData.NewExpression(_valueSet.Script as string)
            .AsScript();

        Test(_exp);
    }

    [Test, Order(3)]
    public void Create3Test()
    {
        _exp = BdoScript.Func(_valueSet.ScriptwordName as string);

        Test(_exp);
    }
}
