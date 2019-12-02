using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Extensions.Scriptwords;
using BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders;
using NUnit.Framework;

namespace BindOpen.Framework.Tests.UnitTest.System.Diagnostics
{
    [TestFixture, Order(12)]
    public class ScriptInterpreterTest
    {
        private readonly string _script = "$sqlTable('MYTABLE').sqlField('MYFIELD') ='abc'";
        private readonly string _interpretedScript = "[MYTABLE].[MYFIELD]='abc'";

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void TestInterprete_Databases()
        {
            IBdoLog log = new BdoLog();

            string resultScript = "";

            using (ScriptVariableSet scriptVariableSet = new ScriptVariableSet())
            {
                scriptVariableSet.SetValue(ScriptVariableKey_Database.DbBuilder, new DbQueryBuilder_MSSqlServer(SetupVariables.AppHost.Scope));
                resultScript = SetupVariables.AppHost.Scope.Interpreter.Interprete(this._script, scriptVariableSet, log);
            }

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = log.ToXml();
            }
            Assert.That(this._interpretedScript.ToLower() == resultScript?.ToLower(), "Bad script interpretation. Result was '" + xml);
        }
    }
}
