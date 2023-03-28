using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Script;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Script
{
    [TestFixture, Order(401)]
    public partial class BdoScriptInterpreterTests
    {
        private readonly string _scriptA = "$isEqual('MYTABLE', $text('MYTABLE'))";

        private readonly BdoScriptword _scriptwordA =
            BdoScript.Function("isEqual", "MYTABLE", BdoScript.Function("text", "mytable"));

        private readonly string _stringA = "totomax";

        private readonly string _scriptB = "$('workflow').input('input1')";

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
            var resultScript = Tests.ScriptInterpreter.Evaluate<bool?>(exp)?.ToString();

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
        public void CreateWordFromScriptTest()
        {
            var scriptword = BdoScript.NewWordFromScript(_scriptA);
            Assert.That(
                _scriptwordA.Name.Equals(scriptword.Name, StringComparison.OrdinalIgnoreCase)
                && _scriptwordA.Count == scriptword.Count
                && (_scriptwordA[1] as BdoScriptword)?.Name.Equals((scriptword[1] as BdoScriptword)?.Name, StringComparison.OrdinalIgnoreCase) == true
                && (_scriptwordA[1] as BdoScriptword)?.Count == (scriptword[1] as BdoScriptword)?.Count,
                "Bad script interpretation");
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
    }
}
