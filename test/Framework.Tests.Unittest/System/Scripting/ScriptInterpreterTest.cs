using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using NUnit.Framework;
using System;

namespace BindOpen.Framework.Tests.UnitTest.System.Diagnostics
{
    [TestFixture, Order(12)]
    public class ScriptInterpreterTest
    {
        private readonly string _script = "$ISEQUAL(\"MYTABLE\", $Text(MYTABLE))";
        private readonly string _interpretedScript = "true";

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void TestInterprete_Databases()
        {
            IBdoLog log = new BdoLog();

            string resultScript = "";

            var scriptVariableSet = new ScriptVariableSet();
            resultScript = SetupVariables.AppHost.Scope.Interpreter.Interprete(_script, scriptVariableSet, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(_interpretedScript.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}
