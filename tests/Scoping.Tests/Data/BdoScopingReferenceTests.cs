using BindOpen.Scoping.Script;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class BdoScopingReferenceTests
{
    public IBdoReference _reference = null;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var dataTests = new BdoReferenceTests();
        dataTests.OneTimeSetUp();
    }

    [Test, Order(3)]
    public void Create3Test()
    {
        var valueSet = BdoExpressionFaker.GetValueSet();
        _reference = BdoData.NewReference(BdoScript.Func(valueSet.ScriptwordName as string));

        Assert.That(_reference?.ToString() == $"${valueSet.ScriptwordName}()", "");
    }

    [Test, Order(4)]
    public void NewReferenceTest()
    {
        var set = BdoData.NewNode(
                BdoData.NewMeta().WithReference(BdoData.NewReference(BdoScript.Func("eq", 1, 1))),
                BdoData.NewMeta().WithReference(BdoScript.Func("eq", 1, 1))
            );

        var value1 = set[0].GetData<bool?>(ScopingTestData.Scope);
        var value2 = set[1].GetData<bool?>(ScopingTestData.Scope);

        Assert.That(value1 == true && value2 == true, "Error ");
    }

    [Test, Order(5)]
    public void NewReferenceFromScriptwordTest()
    {
        _reference = BdoScript.Eq(1, 0).ToReference();

        Assert.That(_reference?.ToString() == $"$eq('1', '0')", "");
    }
}
