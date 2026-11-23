using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Objects;
using DeepEqual.Syntax;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Contains unit tests for <see cref="BdoScriptword"/> operations, verifying string serialization,
/// template concatenation, deep structural equality, and expression interpretation within the BindOpen scoping engine.
/// </summary>
/// <remarks>
/// These tests verify that nested and chained <see cref="IBdoScriptword"/> trees render valid BindOpen script syntax,
/// automatically wrap themselves in <c>{{ }}</c> delimiters when concatenated with plain strings, and evaluate
/// accurately when processed by <see cref="ScopingTestData.ScriptInterpreter"/>.
/// </remarks>
[TestFixture]
public class BdoScriptwordTests
{
    /// <summary>
    /// Gets the expected string representation for a nested <c>$eq</c> and <c>$text</c> script expression.
    /// </summary>
    public readonly string _scriptA = "$eq('MYTABLE', $text('MYTABLE'))";

    /// <summary>
    /// Gets the programmatic <see cref="BdoScriptword"/> tree corresponding to <see cref="_scriptA"/>.
    /// </summary>
    public readonly BdoScriptword _scriptwordA =
        (BdoScriptword)BdoScript.Function("eq", "MYTABLE", BdoScript.Function("text", "MYTABLE"));

    /// <summary>
    /// Gets a sample literal string used to test prefix and suffix concatenation with scriptwords.
    /// </summary>
    public readonly string _stringA = "totomax";

    /// <summary>
    /// Gets the expected string representation for a chained variable and function call expression.
    /// </summary>
    public readonly string _scriptB = "$('workflow').input('input1').value('value1')";

    /// <summary>
    /// Gets a complex script expression containing escaped quotes, commas, and parentheses.
    /// </summary>
    public readonly string _scriptC = "$eq('i,np)ut''', 'i,np)ut'''))";

    /// <summary>
    /// Gets the programmatic chained <see cref="IBdoScriptword"/> instance corresponding to <see cref="_scriptB"/>.
    /// </summary>
    public readonly IBdoScriptword _scriptwordB =
        BdoScript.Variable("workflow")
            .Func("input", "input1")
            .Func("value", "value1");

    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initial setup logic if required
    }

    /// <summary>
    /// Performs a deep structural equality assertion between two <see cref="IBdoScriptword"/> instances,
    /// ignoring circular <c>Parent</c> navigation references.
    /// </summary>
    /// <param name="word1">The first scriptword instance to compare.</param>
    /// <param name="word2">The second scriptword instance to compare.</param>
    public static void AssertEquals(IBdoScriptword word1, IBdoScriptword word2)
    {
        if (word1 == null || word2 == null)
        {
            Assert.That(word1, Is.EqualTo(word2), "One of the scriptwords is null while the other is not.");
            return;
        }

        word1.WithDeepEqual(word2)
             .IgnoreProperty<IBdoMetaData>(w => w.Parent)
             .IgnoreProperty<IBdoScriptword>(w => w.Parent)
             .IgnoreProperty<BdoScriptword>(w => w.Parent)
             .Assert();
    }

    /// <summary>
    /// Verifies that evaluating a <see langword="null"/> <see cref="BdoExpression"/> via
    /// <see cref="ScopingTestData.ScriptInterpreter"/> safely returns <see langword="null"/>.
    /// </summary>
    [Test]
    public void EvaluateScriptNullTest()
    {
        BdoExpression exp = null;
        var resultScript = ScopingTestData.ScriptInterpreter.Evaluate<bool?>(exp)?.ToString();

        Assert.That(resultScript, Is.Null, "Evaluating a null expression should return a null result.");
    }

    /// <summary>
    /// Verifies that an <see cref="IBdoMetaSet"/> created from key-value tuples properly stores
    /// and resolves typed script variables.
    /// </summary>
    [Test]
    public void CreateVariableSetTest()
    {
        var metaSet = BdoData.NewSet(
            ("var1", "sample1"),
            ("var2", 4.55));

        Assert.Multiple(() =>
        {
            Assert.That(metaSet.Count, Is.EqualTo(2), "Variable set count mismatch.");
            Assert.That(metaSet.GetData<string>("var1"), Is.EqualTo("sample1"), "Variable 'var1' resolution failed.");
            Assert.That(metaSet.GetData<double>("var2"), Is.EqualTo(4.55), "Variable 'var2' resolution failed.");
        });
    }

    /// <summary>
    /// Verifies that calling <c>ToString()</c> on a nested <see cref="BdoScriptword"/> function
    /// produces the expected canonical script string.
    /// </summary>
    [Test]
    public void WordToStringTest()
    {
        var script = _scriptwordA?.ToString();
        Assert.That(script, Is.EqualTo(_scriptA).IgnoreCase, "Scriptword to string conversion failed.");
    }

    /// <summary>
    /// Verifies that concatenating a <see cref="BdoScriptword"/> before or after a plain string
    /// automatically wraps the scriptword in <c>{{ }}</c> template delimiters.
    /// </summary>
    [Test]
    public void WordConcatenationTest()
    {
        Assert.Multiple(() =>
        {
            var st = _stringA + _scriptwordA;
            var script = _stringA + "{{" + _scriptA + "}}";
            Assert.That(st, Is.EqualTo(script).IgnoreCase, "Scriptword prefix concatenation failed.");

            st = _scriptwordA + _stringA;
            script = "{{" + _scriptA + "}}" + _stringA;
            Assert.That(st, Is.EqualTo(script).IgnoreCase, "Scriptword suffix concatenation failed.");
        });
    }

    /// <summary>
    /// Verifies that a chained variable and function call <see cref="IBdoScriptword"/> serializes
    /// to the expected dot-notation script string.
    /// </summary>
    [Test]
    public void CompareSubwordStringTest()
    {
        var script = _scriptwordB.ToString();

        Assert.That(script, Is.EqualTo(_scriptB).IgnoreCase, "Chained scriptword string representation mismatch.");
    }

    /// <summary>
    /// Verifies that <see cref="ScopingTestData.ScriptInterpreter"/> accurately parses and evaluates
    /// a <see cref="BdoExpression"/> of kind <see cref="BdoExpressionKind.Script"/> containing escaped quotes and delimiters.
    /// </summary>
    [Test]
    public void EvaluateExpressionTest()
    {
        var script = BdoData.NewExp(_scriptC, BdoExpressionKind.Script);
        var result = ScopingTestData.ScriptInterpreter.Evaluate<bool?>(script);

        Assert.That(result, Is.True, "Failed to evaluate complex script expression.");
    }

    /// <summary>
    /// Verifies that <see cref="ScopingTestData.ScriptInterpreter"/> accurately parses and evaluates
    /// a raw script string containing escaped quotes and delimiters directly.
    /// </summary>
    [Test]
    public void EvaluateStringTest()
    {
        var result = ScopingTestData.ScriptInterpreter.Evaluate<bool?>(_scriptC);

        Assert.That(result, Is.True, "Failed to interpret raw script string.");
    }
}