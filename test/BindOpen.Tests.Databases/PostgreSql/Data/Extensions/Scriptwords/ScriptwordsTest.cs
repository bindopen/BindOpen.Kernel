using BindOpen.Data.Helpers.Serialization;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Core.Extensions.Scriptwords
{
    [TestFixture, Order(12)]
    public class ScriptwordsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void TestInterprete_Fun_SqlCount()
        {
            string script = "$ISEQUAL(\"MYTABLE\", $Text(MYTABLE))";
            string interpretedScript = "true";

            var log = new BdoLog();

            string resultScript = "";

            var scriptVariableSet = new ScriptVariableSet();
            resultScript = GlobalVariables.AppHost.Scope.Interpreter.Interprete(script, scriptVariableSet, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(interpretedScript.Equals(resultScript, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}
