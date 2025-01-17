﻿using BindOpen.Data;
using BindOpen.Scoping.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Scoping.Script;

/// <summary>
/// 
/// </summary>
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

    [Test, Order(201)]
    public void InterpreteWord1Test()
    {
        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(_scriptword1)?.ToString();

        Assert.That(_interpretedScript1.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }

    [Test, Order(202)]
    public void InterpreteScript2Test()
    {
        var metaSet = BdoData.NewSet();
        var exp = BdoData.NewExp(_script2, BdoExpressionKind.Script);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(exp, metaSet)?.ToString();

        Assert.That(_interpretedScript2.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }

    [Test, Order(203)]
    public void InterpreteScript3Test()
    {
        var metaSet = BdoData.NewSet();
        var exp = BdoData.NewExp(_script3, BdoExpressionKind.Script);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<string>(exp, metaSet);

        Assert.That(_interpretedScript3.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }

    [Test, Order(204)]
    public void InterpreteScript4Test()
    {
        var metaSet = BdoData.NewSet(
            ("var1", "const"));

        var exp = BdoData.NewExp(_script4, BdoExpressionKind.Script);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(exp, metaSet)?.ToString();

        Assert.That(_interpretedScript4.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }

    [Test, Order(205)]
    public void InterpreteScript5Test()
    {
        var metaSet = BdoData.NewSet(
            ("var1", "const"));

        var exp = BdoData.NewExp(_script5, BdoExpressionKind.Auto);

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<string>(exp, metaSet);

        Assert.That(_interpretedScript5.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }

    [Test, Order(206)]
    public void InterpreteScript6Test()
    {
        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<bool?>(_scriptword6)?.ToString();

        Assert.That(_interpretedScript6.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }

    [Test, Order(207)]
    public void InterpreteScript7Test()
    {
        var varSet = BdoData.NewSet(
            ("var1", _scriptVarValue71),
            ("var2", _scriptVarValue72));

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<string>(_script7.ToExpression(), varSet: varSet);

        Assert.That(_interpretedScript7.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }

    [Test, Order(208)]
    public void VariableSetTest()
    {
        var varSet = BdoData.NewSet(
            ("var1", _scriptVarValue81),
            ("var2", _scriptVarValue82));

        var interpreter = ScopingTestData.Scope.Interpreter;
        var result = interpreter.Evaluate<string>(
            _script8.ToExpression(), varSet: varSet);

        Assert.That(_interpretedScript8.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
    }

    [Test, Order(4)]
    public void CreateWordFromScriptTest()
    {
        var interpreter = ScopingTestData.Scope.Interpreter;
        var scriptword = interpreter.FindNextWord(_scriptA);
        Assert.That(
            _scriptwordA.Name.Equals(scriptword.Name, StringComparison.OrdinalIgnoreCase)
            && _scriptwordA.Count == scriptword.Count
            && (_scriptwordA[1] as BdoScriptword)?.Name.Equals((scriptword[1] as BdoScriptword)?.Name, StringComparison.OrdinalIgnoreCase) == true
            && (_scriptwordA[1] as BdoScriptword)?.Count == (scriptword[1] as BdoScriptword)?.Count,
            "Bad script interpretation");
    }
}
