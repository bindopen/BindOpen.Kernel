using BindOpen.Scoping.Script;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class BdoReferenceTests
{
    dynamic _valueSet;
    public IBdoReference _ref = null;

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

    [Test, Order(1)]
    public void Create1Test()
    {
        _ref = BdoData.NewReference(BdoData.NewExp(_valueSet.Literal as string, BdoExpressionKind.Literal));

        Assert.That(_ref?.ToString() == (string)_valueSet.Literal, "");
    }

    [Test, Order(2)]
    public void Create2Test()
    {
        _ref = BdoData.NewReference(BdoData.NewExp(_valueSet.Script as string, BdoExpressionKind.Script));

        Assert.That(_ref?.ToString() == (string)_valueSet.Script, "");
    }

    [Test, Order(3)]
    public void Create3Test()
    {
        _ref = BdoData.NewReference(BdoScript.Func(_valueSet.ScriptwordName as string));

        Assert.That(_ref?.ToString() == $"${_valueSet.ScriptwordName}()", "");
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
        _ref = BdoScript.Eq(1, 0).ToReference();

        Assert.That(_ref?.ToString() == $"$eq('1', '0')", "");
    }
}
