using BindOpen.Data;
using BindOpen.Kernel.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Scoping.Script
{
    [TestFixture, Order(401)]
    public class BdoScriptTests
    {
        private readonly string _scriptA = "$eq('MYTABLE', $text('MYTABLE'))";

        private readonly BdoScriptword _scriptwordA =
            BdoScript.Function("eq", "MYTABLE", BdoScript.Function("text", "mytable"));

        private readonly string _stringA = "totomax";

        private readonly string _scriptB = "$('workflow').input('input1').value('value1')";

        private readonly string _scriptC = "$eq('i,np)ut''', 'i,np)ut'''))";

        private readonly BdoScriptword _scriptwordB =
            BdoScript.Variable("workflow")
            .Func("input", "input1")
            .Func("value", "value1");

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void EvaluateScriptNullTest()
        {
            BdoExpression exp = null;
            var resultScript = SystemData.ScriptInterpreter.Evaluate<bool?>(exp)?.ToString();

            Assert.That(resultScript == null, "Bad script interpretation");
        }

        [Test, Order(2)]
        public void CreateVariableSetTest()
        {
            var metaSet = BdoData.NewSet(
                ((string Name, object Value))("var1", "sample1"),
                ((string Name, object Value))("var2", 4.55));

            Assert.That(metaSet.Count == 2, "Bad script interpretation");
            Assert.That(metaSet.GetData<string>("var1") == "sample1", "Bad script interpretation");
            Assert.That(metaSet.GetData<double>("var2") == 4.55, "Bad script interpretation");
        }

        [Test, Order(3)]
        public void WordToStringTest()
        {
            var script = _scriptwordA?.ToString();
            Assert.That(_scriptA.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");

            script = (string)_scriptwordA;
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
            var script = (string)_scriptwordB;
            Assert.That(_scriptB.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(5)]
        public void EvaluateExpressionTest()
        {
            var script = BdoData.NewExp(_scriptC, BdoExpressionKind.Script);
            var result = SystemData.ScriptInterpreter.Evaluate<bool?>(script);

            Assert.That(result == true, "Bad script interpretation");
        }

        [Test, Order(6)]
        public void EvaluateStringTest()
        {
            var script = _scriptC;
            var result = SystemData.ScriptInterpreter.Evaluate<bool?>(script);

            Assert.That(result == true, "Bad script interpretation");
        }
    }
}
