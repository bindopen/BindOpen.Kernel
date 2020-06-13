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
        private readonly string _script = "$ISEQUAL(\"MYTABLE\", $Text(MYTABLE))";
        private readonly string _interpretedScript = "true";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void InterpreteTest()
        {
            var log = new BdoLog();

            var scriptVariableSet = ScriptingFactory.CreateVariableSet();
            var resultScript = GlobalVariables.Scope.Interpreter.Interprete(_script, DataExpressionKind.Script, scriptVariableSet, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(_interpretedScript.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}
