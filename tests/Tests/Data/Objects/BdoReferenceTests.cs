using BindOpen.Scoping.Script;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class BdoReferenceTests
{
    dynamic _valueSet;
    public IBdoReference _reference = null;

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
        IBdoReference ref1,
        IBdoReference ref2)
    {
        if ((ref1 != null && ref2 == null) || (ref1 == null && ref2 != null))
        {
            Assert.That(Equals(ref1, ref2), "Unmatched objects");
        }

        var deepEq = ref1.WithDeepEqual(ref2);
        deepEq.Assert();
    }

    [Test, Order(1)]
    public void Create1Test()
    {
        _reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Literal as string, BdoExpressionKind.Literal));

        Assert.That(_reference?.ToString() == (string)_valueSet.Literal, "");
    }

    [Test, Order(2)]
    public void Create2Test()
    {
        _reference = BdoData.NewReference(BdoData.NewExp(_valueSet.Script as string, BdoExpressionKind.Script));

        Assert.That(_reference?.ToString() == (string)_valueSet.Script, "");
    }

    [Test, Order(3)]
    public void Create3Test()
    {
        _reference = BdoData.NewReference(BdoScript.Func(_valueSet.ScriptwordName as string));

        Assert.That(_reference?.ToString() == $"${_valueSet.ScriptwordName}()", "");
    }

    [Test, Order(4)]
    public void NewReferenceTest()
    {
        var set = BdoData.NewNode(
                BdoData.NewMeta().WithReference(BdoData.NewReference(BdoScript.Func("eq", 1, 1))),
                BdoData.NewMeta().WithReference(BdoScript.Func("eq", 1, 1))
            );

        var value1 = set[0].GetData<bool?>(GlobalTestData.Scope);
        var value2 = set[1].GetData<bool?>(GlobalTestData.Scope);

        Assert.That(value1 == true && value2 == true, "Error ");
    }

    [Test, Order(5)]
    public void NewReferenceFromScriptwordTest()
    {
        _reference = BdoScript.Eq(1, 0).ToReference();

        Assert.That(_reference?.ToString() == $"$eq('1', '0')", "");
    }
}
