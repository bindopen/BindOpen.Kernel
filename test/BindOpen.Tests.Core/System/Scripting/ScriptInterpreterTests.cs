using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Core.System.Diagnostics
{
    [TestFixture, Order(401)]
    public class ScriptInterpreterTests
    {
        private readonly string _script = "$isEqual('MYTABLE', $text('MYTABLE'))";
        private readonly string _interpretedScript = "true";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void LoadInterpreterTest()
        {
            var interpreter = GlobalVariables.Scope.Interpreter;
            Assert.That(interpreter.GetDefinitions().Count > 0, "Bad interpreter loading");
        }

        [Test, Order(2)]
        public void InterpreteWordTest()
        {
            var log = new BdoLog();

            var scriptVariableSet = ScriptingFactory.CreateVariableSet();
            var scriptword = ScriptingFactory.Function("isEqual", "MYTABLE",
                ScriptingFactory.Function("text", "mytable"));
            var resultScript = GlobalVariables.Scope.Interpreter.Evaluate(scriptword.CreateExpression(), scriptVariableSet, log)?.ToString();

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(_interpretedScript.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(3)]
        public void InterpreteScriptTest()
        {
            var log = new BdoLog();

            var scriptVariableSet = ScriptingFactory.CreateVariableSet();
            var resultScript = GlobalVariables.Scope.Interpreter.Evaluate(_script, DataExpressionKind.Script, scriptVariableSet, log)?.ToString();

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(_interpretedScript.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}
