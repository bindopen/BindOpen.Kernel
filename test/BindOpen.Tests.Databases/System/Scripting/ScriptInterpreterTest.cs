using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.System.Diagnostics
{
    [TestFixture, Order(12)]
    public class ScriptInterpreterTest
    {
        private readonly string _script = "$ISEQUAL(\"MYTABLE\", $Text(MYTABLE))";
        private readonly string _expectedResult = "true";

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void TestInterprete_Databases()
        {
            var log = new BdoLog();

            string result = "";

            var scriptVariableSet = new ScriptVariableSet();
            result = GlobalVariables.AppHost.Scope.Interpreter.Interprete(_script, DataExpressionKind.Script, scriptVariableSet, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(_expectedResult.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}
