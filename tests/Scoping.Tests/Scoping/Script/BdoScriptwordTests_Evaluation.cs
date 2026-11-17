using BindOpen.Data;
using BindOpen.Data.Objects.Interfaces;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Script;

/// <summary>
/// This class provides unit tests for script evaluation, verifying the interpreter's 
/// ability to process literals, variables, and nested functions within templates.
/// </summary>
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
    /// Tests the interpretation of a direct scriptword object.
    /// </summary>
    [Test, Order(201)]
    public void InterpreteWord1Test()
    {
        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(_scriptword1)?.ToString();

        Assert.That(result, Is.EqualTo(_interpretedScript1).IgnoreCase, "Scriptword evaluation failed.");
    }

    /// <summary>
    /// Tests the interpretation of a raw script string using a script expression.
    /// </summary>
    [Test, Order(202)]
    public void InterpreteScript2Test()
    {
        var metaSet = BdoData.NewSet();
        var exp = BdoData.NewExp(_script2, BdoExpressionKind.Script);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(exp, metaSet)?.ToString();

        Assert.That(result, Is.EqualTo(_interpretedScript2).IgnoreCase, "Script string evaluation failed.");
    }

    /// <summary>
    /// Tests the interpretation of a chained function call.
    /// </summary>
    [Test, Order(203)]
    public void InterpreteScript3Test()
    {
        var exp = BdoData.NewExp(_script3, BdoExpressionKind.Script);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<string>(exp);

        Assert.That(result, Is.EqualTo(_interpretedScript3).IgnoreCase, "Chained function evaluation failed.");
    }

    /// <summary>
    /// Tests variable resolution within a script.
    /// </summary>
    [Test, Order(204)]
    public void InterpreteScript4Test()
    {
        var metaSet = BdoData.NewSet(("var1", "const"));
        var exp = BdoData.NewExp(_script4, BdoExpressionKind.Script);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(exp, metaSet)?.ToString();

        Assert.That(result, Is.EqualTo(_interpretedScript4).IgnoreCase, "Variable resolution in script failed.");
    }

    /// <summary>
    /// Tests an 'Auto' expression (template) containing static text and dynamic script zones.
    /// </summary>
    [Test, Order(205)]
    public void InterpreteScript5Test()
    {
        var metaSet = BdoData.NewSet(("var1", "const"));
        var exp = BdoData.NewExp(_script5, BdoExpressionKind.Auto);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<string>(exp, metaSet);

        Assert.That(result, Is.EqualTo(_interpretedScript5).IgnoreCase, "Template auto-evaluation failed.");
    }

    /// <summary>
    /// Tests the interpretation of a scriptword containing a complex entity object.
    /// </summary>
    [Test, Order(206)]
    public void InterpreteScript6Test()
    {
        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(_scriptword6)?.ToString();

        Assert.That(result, Is.EqualTo(_interpretedScript6).IgnoreCase, "Entity-based scriptword evaluation failed.");
    }

    /// <summary>
    /// Tests complex concatenation within a dynamic script zone.
    /// </summary>
    [Test, Order(207)]
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
    /// Tests simple template evaluation with multiple variable injections.
    /// </summary>
    [Test, Order(208)]
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
    /// Tests the parser's ability to find and recreate a scriptword structure from a raw string.
    /// </summary>
    [Test, Order(4)]
    public void CreateWordFromScriptTest()
    {
        var interpreter = ScopingTestData.Scope.Interpreter;
        var scriptword = interpreter.FindNextWord(_scriptA);

        Assert.That(scriptword, Is.Not.Null);
        Assert.That(scriptword.Name, Is.EqualTo(_scriptwordA.Name).IgnoreCase, "Parser failed to find the correct root function name.");
        Assert.That(scriptword.Count, Is.EqualTo(_scriptwordA.Count), "Parser found an incorrect number of arguments.");
    }
}