using BindOpen.Extensions.Scripting;
using BindOpen.Data;
using BindOpen.Data.Items;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Extensions
{
    [TestFixture, Order(401)]
    public class BdoScriptInterpreterTests
    {
        private readonly string _script1 = "$isEqual('MYTABLE', $text('MYTABLE'))";

        private readonly BdoScriptword _scriptword1 =
            BdoScript.Function("isEqual", "MYTABLE", BdoScript.Function("text", "mytable"));

        private readonly string _string1 = "totomax";

        private readonly string _script2 = "$('workflow').input('input1')";

        private readonly BdoScriptword _scriptword2 =
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
            var resultScript = GlobalVariables.ScriptInterpreter.Evaluate<bool?>(exp)?.ToString();

            Assert.That(resultScript == null, "Bad script interpretation");
        }

        [Test, Order(2)]
        public void CreateVariableSetTest()
        {
            var varSet = BdoData.NewMetaSet(
                ("var1", "sample1"),
                ("var2", 4.55));

            Assert.That(varSet.Count == 2, "Bad script interpretation");
            Assert.That(varSet.GetItem<string>("var1") == "sample1", "Bad script interpretation");
            Assert.That(varSet.GetItem<double>("var2") == 4.55, "Bad script interpretation");
        }

        [Test, Order(3)]
        public void WordToStringTest()
        {
            var script = _scriptword1?.ToString();
            Assert.That(_script1.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");

            script = (string)_scriptword1;
            Assert.That(_script1.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(4)]
        public void CreateWordFromScriptTest()
        {
            var scriptword = BdoScript.CreateWord(_script1);
            Assert.That(
                _scriptword1.Name.Equals(scriptword.Name, StringComparison.OrdinalIgnoreCase)
                && _scriptword1.Parameters.Count == scriptword.Parameters.Count
                && (_scriptword1.Parameters[1] as BdoScriptword)?.Name.Equals((scriptword.Parameters[1] as BdoScriptword)?.Name, StringComparison.OrdinalIgnoreCase) == true
                && (_scriptword1.Parameters[1] as BdoScriptword)?.Parameters.Count == (scriptword.Parameters[1] as BdoScriptword)?.Parameters.Count,
                "Bad script interpretation");
        }

        [Test, Order(4)]
        public void CreateWordConcatenation()
        {
            var st = _string1 + _scriptword1;
            var script = _string1 + "{{" + _script1 + "}}";
            Assert.That(st.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");

            st = _scriptword1 + _string1;
            script = "{{" + _script1 + "}}" + _string1;
            Assert.That(st.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }

        [Test, Order(4)]
        public void CreateSubword()
        {
            var script = (string)_scriptword2;
            Assert.That(_script2.Equals(script, StringComparison.OrdinalIgnoreCase), "Bad script interpretation");
        }
    }
}
