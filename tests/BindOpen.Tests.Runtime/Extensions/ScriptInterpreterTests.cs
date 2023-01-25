using BindOpen.Extensions.Scripting;
using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Tests.Extensions;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture, Order(401)]
    public class ScriptInterpreterTests
    {
        private readonly IBdoScriptword _scriptword1 =
            BdoScript.Function("isEqual", "MYTABLE", BdoScript.Function("text", "mytable"));
        private readonly string _interpretedScript1 = "true";

        private readonly string _script2 = "$eq('MYTABLE', $text('MYTABLE'))";
        private readonly string _interpretedScript2 = "true";

        private readonly string _script3 = "$func1('abc', 'efg').func2('ijk')";
        private readonly string _interpretedScript3 = "false:ijk";

        private readonly string _script4 = "$eq($(var1), 'const')";
        private readonly string _interpretedScript4 = "true";

        private readonly string _script5 = "abc{{$eq($(var1), 'const')}}defg";
        private readonly string _interpretedScript5 = "abctruedefg";

        private readonly IBdoScriptword _scriptword6 =
            BdoScript.Function("isEqual", "mypath", BdoScript.Function("text", new EntityFake("mypath")));
        private readonly string _interpretedScript6 = "true";

        private readonly string _script7 = "{{$func3($eq($(var1), 'const'), $eq($(var1), 'const'), 'toto', 'titi')}}";
        private readonly string _interpretedScript7 = "true-true-toto-titi";

        private readonly string _scriptVarValue81 = "const";
        private readonly int _scriptVarValue82 = 5500;
        private readonly string _script8 = "{{$(var1)}}-{{$(var2)}}";
        private readonly string _interpretedScript8 = "const-5500";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(201)]
        public void InterpreteWord1Test()
        {
            var interpreter = GlobalVariables.Scope.NewScriptInterpreter();
            var resultScript = interpreter.Evaluate<bool?>(_scriptword1)?.ToString();

            Assert.That(_interpretedScript1.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(202)]
        public void InterpreteScript2Test()
        {
            var varSet = BdoData.NewMetaSet();

            var interpreter = GlobalVariables.Scope.NewScriptInterpreter();
            var resultScript = interpreter.Evaluate<bool?>(_script2, BdoExpressionKind.Script, varSet)?.ToString();

            Assert.That(_interpretedScript2.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(203)]
        public void InterpreteScript3Test()
        {
            var varSet = BdoData.NewMetaSet();

            var interpreter = GlobalVariables.Scope.NewScriptInterpreter();
            var resultScript = interpreter.Evaluate<string>(_script3, BdoExpressionKind.Script, varSet);

            Assert.That(_interpretedScript3.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(204)]
        public void InterpreteScript4Test()
        {
            var interpreter = GlobalVariables.Scope.NewScriptInterpreter();
            var resultScript = interpreter.Evaluate<bool?>(_script4, BdoExpressionKind.Script)?.ToString();

            Assert.That(_interpretedScript4.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(205)]
        public void InterpreteScript5Test()
        {
            var interpreter = GlobalVariables.Scope.NewScriptInterpreter();
            var resultScript = interpreter.Evaluate<string>(_script5, BdoExpressionKind.Auto);

            Assert.That(_interpretedScript5.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(206)]
        public void InterpreteScript6Test()
        {
            var interpreter = GlobalVariables.Scope.NewScriptInterpreter();
            var resultScript = interpreter.Evaluate<bool?>(_scriptword6.AsExpression())?.ToString();

            Assert.That(_interpretedScript6.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(207)]
        public void InterpreteScript7Test()
        {
            var interpreter = GlobalVariables.Scope.NewScriptInterpreter();
            var resultScript = interpreter.Evaluate<string>(_script7);

            Assert.That(_interpretedScript7.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(208)]
        public void VarElementSetTest()
        {
            var varSet = BdoData.NewMetaSet(
                ((string Name, object Value))("value1", _scriptVarValue81), ((string Name, object Value))("value2", _scriptVarValue82));

            var interpreter = GlobalVariables.Scope.NewScriptInterpreter();
            var resultScript = interpreter.Evaluate<string>(
                _script8, varSet: varSet);

            Assert.That(_interpretedScript8.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

    }
}
