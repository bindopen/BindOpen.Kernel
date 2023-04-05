﻿using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Script;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Script
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BdoScriptInterpreterTests
    {
        private readonly IBdoScriptword _scriptword1 =
            BdoScript.Function("eq", "MYTABLE", BdoScript.Function("text", "mytable"));
        private readonly string _interpretedScript1 = "true";

        private readonly string _script2 = "$eq('MYTABLE', $text('MYTABLE'))";
        private readonly string _interpretedScript2 = "true";

        private readonly string _script3 = "$func1('abc', 'efg').func2('ijk')";
        private readonly string _interpretedScript3 = "false:ijk";

        private readonly string _script4 = "$eq($('var1'), 'const')";
        private readonly string _interpretedScript4 = "true";

        private readonly string _script5 = "abc{{$eq($('var1'), 'const')}}defg";
        private readonly string _interpretedScript5 = "abctruedefg";

        private readonly IBdoScriptword _scriptword6 =
            BdoScript.Function("eq", "mypath", BdoScript.Function("text", new EntityFake("mypath")));
        private readonly string _interpretedScript6 = "true";

        private readonly string _script7 = "{{concat($eq($(var1), 'const'), $eq($(var1), 'const'), 'toto', 'titi')}}";
        private readonly string _interpretedScript7 = "true-true-toto-titi";

        private readonly string _scriptVarValue81 = "const";
        private readonly int _scriptVarValue82 = 5500;
        private readonly string _script8 = "{{$(var1)}}-{{$(var2)}}";
        private readonly string _interpretedScript8 = "const-5500";

        [Test, Order(201)]
        public void InterpreteWord1Test()
        {
            var interpreter = ScopingTests.Scope.Interpreter;
            var resultScript = interpreter.Evaluate<bool?>(_scriptword1.ToReference())?.ToString();

            Assert.That(_interpretedScript1.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(202)]
        public void InterpreteScript2Test()
        {
            var varSet = BdoMeta.NewSet();
            var exp = BdoData.NewExp(_script2, BdoExpressionKind.Script);

            var interpreter = ScopingTests.Scope.Interpreter;
            var resultScript = interpreter.Evaluate<bool?>(exp, varSet)?.ToString();

            Assert.That(_interpretedScript2.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(203)]
        public void InterpreteScript3Test()
        {
            var varSet = BdoMeta.NewSet();
            var exp = BdoData.NewExp(_script3, BdoExpressionKind.Script);

            var interpreter = ScopingTests.Scope.Interpreter;
            var resultScript = interpreter.Evaluate<string>(exp, varSet);

            Assert.That(_interpretedScript3.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(204)]
        public void InterpreteScript4Test()
        {
            var varSet = BdoMeta.NewSet(
                ((string Name, object Value))("var1", "const"));

            var exp = BdoData.NewExp(_script4, BdoExpressionKind.Script);

            var interpreter = ScopingTests.Scope.Interpreter;
            var resultScript = interpreter.Evaluate<bool?>(exp, varSet)?.ToString();

            Assert.That(_interpretedScript4.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(205)]
        public void InterpreteScript5Test()
        {
            var varSet = BdoMeta.NewSet(
                ((string Name, object Value))("var1", "const"));

            var exp = BdoData.NewExp(_script5, BdoExpressionKind.Auto);

            var interpreter = ScopingTests.Scope.Interpreter;
            var resultScript = interpreter.Evaluate<string>(exp, varSet);

            Assert.That(_interpretedScript5.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(206)]
        public void InterpreteScript6Test()
        {
            var interpreter = ScopingTests.Scope.Interpreter;
            var resultScript = interpreter.Evaluate<bool?>(_scriptword6.ToReference())?.ToString();

            Assert.That(_interpretedScript6.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(207)]
        public void InterpreteScript7Test()
        {
            var interpreter = ScopingTests.Scope.Interpreter;
            var resultScript = interpreter.Evaluate<string>(_script7.ToExpression());

            Assert.That(_interpretedScript7.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(208)]
        public void VariableSetTest()
        {
            var varSet = BdoMeta.NewSet(
                ((string Name, object Value))("var1", _scriptVarValue81),
                ((string Name, object Value))("var2", _scriptVarValue82));

            var interpreter = ScopingTests.Scope.Interpreter;
            var resultScript = interpreter.Evaluate<string>(
                _script8.ToExpression(), varSet: varSet);

            Assert.That(_interpretedScript8.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(4)]
        public void CreateWordFromScriptTest()
        {
            var interpreter = ScopingTests.Scope.Interpreter;
            var scriptword = interpreter.FindNextWord(_scriptA);
            Assert.That(
                _scriptwordA.Name.Equals(scriptword.Name, StringComparison.OrdinalIgnoreCase)
                && _scriptwordA.Count == scriptword.Count
                && (_scriptwordA[1] as BdoScriptword)?.Name.Equals((scriptword[1] as BdoScriptword)?.Name, StringComparison.OrdinalIgnoreCase) == true
                && (_scriptwordA[1] as BdoScriptword)?.Count == (scriptword[1] as BdoScriptword)?.Count,
                "Bad script interpretation");
        }
    }
}