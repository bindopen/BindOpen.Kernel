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
        private readonly string _script1 = "$isEqual('MYTABLE', $text('MYTABLE'))";
        private readonly string _interpretedScript1 = "true";
        private readonly string _script2 = "$func1('abc', 'efg').func2('ijk')";
        private readonly string _interpretedScript2 = "false:ijk";
        private readonly string _script3 = "$isEqual($(constant), 'const')";
        private readonly string _interpretedScript3 = "true";

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
            var resultScript = GlobalVariables.Scope.Interpreter.Evaluate<bool?>(scriptword, scriptVariableSet, log)?.ToString();

            string xml = string.Empty;
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(_interpretedScript1.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(3)]
        public void InterpreteScript1Test()
        {
            var log = new BdoLog();

            var scriptVariableSet = ScriptingFactory.CreateVariableSet();
            var resultScript = GlobalVariables.Scope.Interpreter.Evaluate<bool?>(_script1, DataExpressionKind.Script, scriptVariableSet, log)?.ToString();

            string xml = string.Empty;
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(_interpretedScript1.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(4)]
        public void InterpreteScript2Test()
        {
            var log = new BdoLog();

            var scriptVariableSet = ScriptingFactory.CreateVariableSet();
            var resultScript = GlobalVariables.Scope.Interpreter.Evaluate<string>(_script2, DataExpressionKind.Script, scriptVariableSet, log);

            string xml = string.Empty;
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(_interpretedScript2.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }

        [Test, Order(5)]
        public void InterpreteScript3Test()
        {
            var log = new BdoLog();

            var scriptVariableSet = ScriptingFactory.CreateVariableSet();
            var resultScript = GlobalVariables.Scope.Interpreter.Evaluate<bool?>(_script3, DataExpressionKind.Script, scriptVariableSet, log)?.ToString();

            string xml = string.Empty;
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(_interpretedScript3.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation" + xml);
        }
    }
}
