using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.System.Scoping.Script
{
    [TestFixture, Order(401)]
    public partial class BdoScriptInterpreterTests
    {
        private readonly string _scriptA = "$eq('MYTABLE', $text('MYTABLE'))";

        private readonly BdoScriptword _scriptwordA =
            BdoScript.Function("eq", "MYTABLE", BdoScript.Function("text", "mytable"));

        private readonly string _stringA = "totomax";

        private readonly string _scriptB = "$('workflow').input('input1')";

        private readonly string _scriptC = "$eq('i,np)ut''', 'i,np)ut'''))";

        private readonly BdoScriptword _scriptwordB =
            BdoScript.Variable("workflow")
            .Func("input", "input1");

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void InterpreteScriptNullTest()
        {
            BdoExpression exp = null;
            var resultScript = SystemData.ScriptInterpreter.Evaluate<bool?>(exp)?.ToString();

            Assert.That(resultScript == null, "Bad script interpretation");
        }

        [Test, Order(2)]
        public void CreateVariableSetTest()
        {
            var varSet = BdoMeta.NewSet(
                ("var1", "sample1"),
                ("var2", 4.55));

            Assert.That(varSet.Count == 2, "Bad script interpretation");
            Assert.That(varSet.GetData<string>("var1") == "sample1", "Bad script interpretation");
            Assert.That(varSet.GetData<double>("var2") == 4.55, "Bad script interpretation");
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
        public void CreateWordConcatenation()
        {
            var st = _stringA + _scriptwordA;
            var script = _stringA + "{{" + _scriptA + "}}";
            Assert.That(st.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");

            st = _scriptwordA + _stringA;
            script = "{{" + _scriptA + "}}" + _stringA;
            Assert.That(st.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(4)]
        public void CreateSubword()
        {
            var script = (string)_scriptwordB;
            Assert.That(_scriptB.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(5)]
        public void Create5()
        {
            var script = BdoData.NewExp(_scriptC, BdoExpressionKind.Script);
            var result = SystemData.ScriptInterpreter.Evaluate<bool?>(script);

            Assert.That(result == true, "Bad script interpretation");
        }
    }
}
