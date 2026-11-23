using BindOpen.Data;
using BindOpen.Data.Objects;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

/// <summary>
/// Contains unit tests for script evaluation and parsing within the BindOpen scoping engine,
/// verifying the interpreter's ability to process literals, variables, complex entities,
/// chained function calls, and interpolated templates.
/// </summary>
/// <remarks>
/// These tests cover direct <see cref="IBdoScriptword"/> evaluation, <see cref="BdoExpressionKind.Script"/>
/// and <see cref="BdoExpressionKind.Auto"/> template rendering, variable set resolution, and AST extraction
/// via <c>FindNextWord</c>.
/// </remarks>
[TestFixture]
public class BdoScriptwordTests_Evaluation
{
    private readonly string _scriptA = "$eq('MYTABLE', $text('MYTABLE'))";

    private readonly IBdoScriptword _scriptwordA =
        BdoScript.Function("eq", "MYTABLE", BdoScript.Function("text", "mytable"));

    private readonly IBdoScriptword _scriptword1 =
        BdoScript.Function("eq", "MYTABLE", BdoScript.Function("text", "MYTABLE"));
    private readonly string _interpretedScript1 = "true";

    private readonly string _script2 = "$eq('MYTABLE', $text('MYTABLE'))";
    private readonly string _interpretedScript2 = "true";

    private readonly string _script3 = "$eq('abc', 'efg').subFun('ijk')";
    private readonly string _interpretedScript3 = "false-ijk";

    private readonly string _script4 = "$eq($('var1'), 'const')";
    private readonly string _interpretedScript4 = "true";

    private readonly string _script5 = "abc{{$eq($('var1'), 'const')}}defg";
    private readonly string _interpretedScript5 = "abctruedefg";

    private readonly IBdoScriptword _scriptword6 =
        BdoScript.Function("eq", "mypath", BdoScript.Function("text", new EntityFake("mypath")));
    private readonly string _interpretedScript6 = "true";

    private readonly string _script7 = "{{$concat($eq($(var1), 'const'), $eq($(var2), 5500), 'toto', 'titi')}}";
    private readonly string _scriptVarValue71 = "const";
    private readonly int _scriptVarValue72 = 5500;
    private readonly string _interpretedScript7 = "truetruetototiti";

    private readonly string _scriptVarValue81 = "const";
    private readonly int _scriptVarValue82 = 5500;
    private readonly string _script8 = "{{$(var1)}}-{{$(var2)}}";
    private readonly string _interpretedScript8 = "const-5500";

    /// <summary>
    /// Verifies the direct evaluation of a programmatic <see cref="IBdoScriptword"/> tree
    /// representing <c>$eq('MYTABLE', $text('MYTABLE'))</c>.
    /// </summary>
    [Test]
    public void InterpreteWord1Test()
    {
        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(_scriptword1)?.ToString();

        Assert.That(result, Is.EqualTo(_interpretedScript1).IgnoreCase, "Scriptword evaluation failed.");
    }

    /// <summary>
    /// Verifies the evaluation of a raw <see cref="BdoExpressionKind.Script"/> expression string
    /// against an empty variable set.
    /// </summary>
    [Test]
    public void InterpreteScript2Test()
    {
        var metaSet = BdoData.NewSet();
        var exp = BdoData.NewExp(_script2, BdoExpressionKind.Script);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(exp, metaSet)?.ToString();

        Assert.That(result, Is.EqualTo(_interpretedScript2).IgnoreCase, "Script string evaluation failed.");
    }

    /// <summary>
    /// Verifies the evaluation of a chained script function call (<c>$eq('abc', 'efg').subFun('ijk')</c>).
    /// </summary>
    [Test]
    public void InterpreteScript3Test()
    {
        var exp = BdoData.NewExp(_script3, BdoExpressionKind.Script);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<string>(exp);

        Assert.That(result, Is.EqualTo(_interpretedScript3).IgnoreCase, "Chained function evaluation failed.");
    }

    /// <summary>
    /// Verifies variable lookup and equality comparison within a <see cref="BdoExpressionKind.Script"/>
    /// expression (<c>$eq($('var1'), 'const')</c>).
    /// </summary>
    [Test]
    public void InterpreteScript4Test()
    {
        var metaSet = BdoData.NewSet(("var1", "const"));
        var exp = BdoData.NewExp(_script4, BdoExpressionKind.Script);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(exp, metaSet)?.ToString();

        Assert.That(result, Is.EqualTo(_interpretedScript4).IgnoreCase, "Variable resolution in script failed.");
    }

    /// <summary>
    /// Verifies that a <see cref="BdoExpressionKind.Auto"/> template string containing static text
    /// surrounding a <c>{{ ... }}</c> script block interpolates the evaluated result inline.
    /// </summary>
    [Test]
    public void InterpreteScript5Test()
    {
        var metaSet = BdoData.NewSet(("var1", "const"));
        var exp = BdoData.NewExp(_script5, BdoExpressionKind.Auto);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<string>(exp, metaSet);

        Assert.That(result, Is.EqualTo(_interpretedScript5).IgnoreCase, "Template auto-evaluation failed.");
    }

    /// <summary>
    /// Verifies the evaluation of a scriptword passing a complex CLR object (<see cref="EntityFake"/>)
    /// into <see cref="BdoScript.Function"/> (<c>$text</c>) for string conversion and comparison.
    /// </summary>
    [Test]
    public void InterpreteScript6Test()
    {
        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(_scriptword6)?.ToString();

        Assert.That(result, Is.EqualTo(_interpretedScript6).IgnoreCase, "Entity-based scriptword evaluation failed.");
    }

    /// <summary>
    /// Verifies multi-argument string concatenation (<c>$concat</c>) combining evaluated boolean comparisons
    /// and string literals inside a <c>{{ ... }}</c> script expression.
    /// </summary>
    [Test]
    public void InterpreteScript7Test()
    {
        var varSet = BdoData.NewSet(
            ("var1", _scriptVarValue71),
            ("var2", _scriptVarValue72));

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<string>(_script7.ToExpression(), varSet: varSet);

        Assert.That(result, Is.EqualTo(_interpretedScript7).IgnoreCase, "Complex concatenation evaluation failed.");
    }

    /// <summary>
    /// Verifies template interpolation containing multiple adjacent variable placeholders
    /// (<c>{{$(var1)}}-{{$(var2)}}</c>) resolved from an <see cref="IBdoMetaSet"/>.
    /// </summary>
    [Test]
    public void VariableSetTest()
    {
        var varSet = BdoData.NewSet(
            ("var1", _scriptVarValue81),
            ("var2", _scriptVarValue82));

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<string>(_script8.ToExpression(), varSet: varSet);

        Assert.That(result, Is.EqualTo(_interpretedScript8).IgnoreCase, "Multi-variable template evaluation failed.");
    }

    /// <summary>
    /// Verifies that <c>FindNextWord</c> parses a raw script string and reconstructs an
    /// <see cref="IBdoScriptword"/> matching the expected root function name and argument count.
    /// </summary>
    [Test]
    public void CreateWordFromScriptTest()
    {
        var interpreter = ScopingTestData.Scope.Interpreter;
        var scriptword = interpreter.FindNextWord(_scriptA);

        Assert.That(scriptword, Is.Not.Null, "Parser returned null instead of a scriptword instance.");

        Assert.Multiple(() =>
        {
            Assert.That(scriptword.Name, Is.EqualTo(_scriptwordA.Name).IgnoreCase, "Parser failed to find the correct root function name.");
            Assert.That(scriptword.Count, Is.EqualTo(_scriptwordA.Count), "Parser found an incorrect number of arguments.");
        });
    }
}