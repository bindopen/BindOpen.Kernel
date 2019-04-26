﻿using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Databases.Extensions.Scriptwords;
using BindOpen.Framework.Databases.MSSqlServer.Data.Queries.Builders;
using BindOpen.Framework.UnitTest;
using NUnit.Framework;

namespace BindOpen.Framework.UnitTest.System.Diagnostics
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
    }
}
