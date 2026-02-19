using BindOpen.Data;
using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This class provides unit tests for BdoScriptword operations, ensuring correct
/// string representation, concatenation, and interpretation of script logic.
/// </summary>
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
        // Initial setup logic if required
    }

    /// <summary>
    /// Standardized deep equality assertion for scriptword objects.
    /// </summary>
    public static void AssertEquals(IBdoScriptword word1, IBdoScriptword word2)
    {
        if ((word1 != null && word2 == null) || (word1 == null && word2 != null))
        {
            Assert.That(Equals(word1, word2), Is.True, "Unmatched scriptword objects.");
        }

        // Using DeepEqual library to compare complex object graphs
        word1.WithDeepEqual(word2).Assert();
    }

    /// <summary>
    /// Tests that evaluating a null expression returns a null result.
    /// </summary>
    [Test, Order(1)]
    public void EvaluateScriptNullTest()
    {
        BdoExpression exp = null;
        var resultScript = ScopingTestData.ScriptInterpreter.Evaluate<bool?>(exp)?.ToString();

        Assert.That(resultScript, Is.Null, "Evaluating a null expression should return a null result.");
    }

    /// <summary>
    /// Tests the creation of a metadata set used for variable resolution within scripts.
    /// </summary>
    [Test, Order(2)]
    public void CreateVariableSetTest()
    {
        var metaSet = BdoData.NewSet(
            ("var1", "sample1"),
            ("var2", 4.55));

        Assert.That(metaSet.Count, Is.EqualTo(2), "Variable set count mismatch.");
        Assert.That(metaSet.GetData<string>("var1"), Is.EqualTo("sample1"), "Variable 'var1' resolution failed.");
        Assert.That(metaSet.GetData<double>("var2"), Is.EqualTo(4.55), "Variable 'var2' resolution failed.");
    }

    /// <summary>
    /// Tests the string representation of a scriptword function.
    /// </summary>
    [Test, Order(3)]
    public void WordToStringTest()
    {
        var script = _scriptwordA?.ToString();
        Assert.That(script, Is.EqualTo(_scriptA).IgnoreCase, "Scriptword to string conversion failed.");
    }

    /// <summary>
    /// Tests the automatic wrapping of scriptwords when concatenated with plain strings.
    /// BindOpen scripts are wrapped in {{ }} when treated as templates.
    /// </summary>
    [Test, Order(4)]
    public void WordConcatenationTest()
    {
        var st = _stringA + _scriptwordA;
        var script = _stringA + "{{" + _scriptA + "}}";
        Assert.That(st, Is.EqualTo(script).IgnoreCase, "Scriptword prefix concatenation failed.");

        st = _scriptwordA + _stringA;
        script = "{{" + _scriptA + "}}" + _stringA;
        Assert.That(st, Is.EqualTo(script).IgnoreCase, "Scriptword suffix concatenation failed.");
    }

    /// <summary>
    /// Tests the string representation of a chained variable and function calls.
    /// </summary>
    [Test, Order(4)]
    public void CompareSubwordStringTest()
    {
        var script = _scriptwordB.ToString();
        Assert.That(script, Is.EqualTo(_scriptB).IgnoreCase, "Chained scriptword string representation mismatch.");
    }

    /// <summary>
    /// Tests the evaluation of a script expression containing escaped characters and delimiters.
    /// </summary>
    [Test, Order(5)]
    public void EvaluateExpressionTest()
    {
        var script = BdoData.NewExp(_scriptC, BdoExpressionKind.Script);
        var result = ScopingTestData.ScriptInterpreter.Evaluate<bool?>(script);

        Assert.That(result, Is.True, "Failed to evaluate complex script expression.");
    }

    /// <summary>
    /// Tests the evaluation of a raw string as a script.
    /// </summary>
    [Test, Order(6)]
    public void EvaluateStringTest()
    {
        var result = ScopingTestData.ScriptInterpreter.Evaluate<bool?>(_scriptC);

        Assert.That(result, Is.True, "Failed to interpret raw script string.");
    }
}