using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.System.Diagnostics;
using BindOpen.System.Scripting;
using NUnit.Framework;
using System;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Scriptwords
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
            string expectedResult = "true";

            var log = new BdoLog();

            string result = "";

            var scriptVariableSet = new ScriptVariableSet();
            result = GlobalVariables.AppHost.Scope.Interpreter.Interprete(script, DataExpressionKind.Script, scriptVariableSet, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(expectedResult.Equals(result, StringComparison.OrdinalIgnoreCase), "Bad script interpretation. Result was '" + xml);
        }
    }
}
