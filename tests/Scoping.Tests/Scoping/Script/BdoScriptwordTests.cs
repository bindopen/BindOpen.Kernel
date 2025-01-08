using BindOpen.Data;
using DeepEqual.Syntax;
using NUnit.Framework;
using System;

namespace BindOpen.Scoping.Script;

[TestFixture, Order(401)]
public class BdoScriptwordTests
{
    public readonly string _scriptA = "$eq('MYTABLE', $text('MYTABLE'))";

    public readonly BdoScriptword _scriptwordA =
        (BdoScriptword)BdoScript.Function("eq", "MYTABLE", BdoScript.Function("text", "mytable"));

    public readonly string _stringA = "totomax";

    public readonly string _scriptB = "$('workflow').input('input1').value('value1')";

    public readonly string _scriptC = "$eq('i,np)ut''', 'i,np)ut'''))";

    public readonly IBdoScriptword _scriptwordB =
        BdoScript.Variable("workflow")
        .Func("input", "input1")
        .Func("value", "value1");

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
    }

    public static void AssertEquals(
        IBdoScriptword word1,
        IBdoScriptword word2)
    {
        if ((word1 != null && word2 == null) || (word1 == null && word2 != null))
        {
            Assert.That(Equals(word1, word2), "Unmatched objects");
        }

        var deepEq = word1.WithDeepEqual(word2);
        deepEq.Assert();
    }

    [Test, Order(1)]
    public void EvaluateScriptNullTest()
    {
        BdoExpression exp = null;
        var resultScript = ScopingTestData.ScriptInterpreter.Evaluate<bool?>(exp)?.ToString();

        Assert.That(resultScript == null, "Bad script interpretation");
    }

    [Test, Order(2)]
    public void CreateVariableSetTest()
    {
        var metaSet = BdoData.NewSet(
            ("var1", "sample1"),
            ("var2", 4.55));

        Assert.That(metaSet.Count == 2, "Bad script interpretation");
        Assert.That(metaSet.GetData<string>("var1") == "sample1", "Bad script interpretation");
        Assert.That(metaSet.GetData<double>("var2") == 4.55, "Bad script interpretation");
    }

    [Test, Order(3)]
    public void WordToStringTest()
    {
        var script = _scriptwordA?.ToString();
        Assert.That(_scriptA.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }


    [Test, Order(4)]
    public void WordConcatenationTest()
    {
        var st = _stringA + _scriptwordA;
        var script = _stringA + "{{" + _scriptA + "}}";
        Assert.That(st.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");

        st = _scriptwordA + _stringA;
        script = "{{" + _scriptA + "}}" + _stringA;
        Assert.That(st.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }

    [Test, Order(4)]
    public void CompareSubwordStringTest()
    {
        var script = _scriptwordB.ToString();
        Assert.That(_scriptB.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }

    [Test, Order(5)]
    public void EvaluateExpressionTest()
    {
        var script = BdoData.NewExp(_scriptC, BdoExpressionKind.Script);
        var result = ScopingTestData.ScriptInterpreter.Evaluate<bool?>(script);

        Assert.That(result == true, "Bad script interpretation");
    }

    [Test, Order(6)]
    public void EvaluateStringTest()
    {
        var script = _scriptC;
        var result = ScopingTestData.ScriptInterpreter.Evaluate<bool?>(script);

        Assert.That(result == true, "Bad script interpretation");
    }
}
