using System.Xml.Linq;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Data.Connections;
using BindOpen.Framework.Databases.Extensions.Scriptwords;
using BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders;
using BindOpen.Framework.UnitTest.Setup;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.Extensions.Runtime
{
    [TestFixture, Order(12)]
    public class ConnectorTest
    {
        private readonly string _script = "$sqlTable('MYTABLE').sqlField('MYFIELD') ='abc'";
        private readonly string _interpretedScript = "[MYTABLE].[MYFIELD]='abc'";

        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public void TestEmpty()
        {
            XElement element = new XElement("name");
        }

        [Test, Order(2)]
        public void TestInterpreteDatabaseScript()
        {
            ILog log = new Log();

            string resultScript = "";

            using (ScriptVariableSet scriptVariableSet = new ScriptVariableSet())
            {
                scriptVariableSet.SetValue(ScriptVariableKey_Database.DbBuilder,
                    new DbQueryBuilder_MSSqlServer(SetupVariables.AppScope));
                resultScript = SetupVariables.AppScope.ScriptInterpreter.Interprete(this._script, scriptVariableSet, log);
            }

            Assert.That(this._interpretedScript.ToLower() == resultScript?.ToLower(), "Bad script interpretation. Result was '" + log.ToXml());
        }

        [Test, Order(3)]
        public void TestCreateOpenCloseConnection()
        {
            ILog log = new Log();

            using (DatabaseConnection connection =
                SetupVariables.AppScope.ConnectionService.Open<DatabaseConnection>("bdd1", null, log))
            {
                connection?.ExecuteNonQuery(this._script, null, log);
            }
        }
    }
}
