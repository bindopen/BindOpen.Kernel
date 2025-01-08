using BindOpen.Scoping.Script;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Data;

[TestFixture, Order(210)]
public class BdoScopingExpressionTests
{
    IBdoExpression _exp;
    BdoExpressionTests _dataTests;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _dataTests = new();
        _dataTests.OneTimeSetUp();
    }

    [Test, Order(3)]
    public void Create3Test()
    {
        var valueSet = BdoExpressionFaker.GetValueSet();
        _exp = BdoScript.Func(valueSet.ScriptwordName as string);

        _dataTests.Test(_exp);
    }
}
