using System.Collections.Generic;
using System.Threading.Tasks;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Helpers.Serialization;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Dto;
using BindOpen.Framework.Core.System.Processing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BindOpen.Framework.Tests.TestConsole
{
    /// <summary>
    /// This class represents the test console program.
    /// </summary>
    /// <remarks>This class like the whole project is temporary. It allows to implement tests before inserting them in Unit test project.</remarks>
    internal static class Test_It
    {
        public static async Task Start()
        {
            //IDataElement element1 = ElementFactory.CreateScalar("test1", 1, 2, 3, 4, 5);
            //IDataElement element2 = ElementFactory.CreateScalar("test1", "a", "b", "c", "d", "e");

            //// we test argument handling
            //TestArguments.Test();

            ILog log = new Log();
            ILog subLog = log.AddSubLog();
            subLog.Detail = new Core.Data.Elements.Sets.DataElementSet(
                ElementFactory.CreateScalar("toto", "yes"));
            subLog.Execution = new ProcessExecution()
            {
                State = ProcessExecutionState.Ended,
                Status = ProcessExecutionStatus.Processing,
                ProgressIndex = 0
            };
            subLog.AddError("test1", resultCode: "user.events");
            //log.Events.RemoveAll(p =>
            //    p.Kind != BindOpen.Framework.Core.System.Diagnostics.Events.EventKind.Error
            //    || p?.ResultCode?.StartsWith("user.") == false);
            string st = JsonConvert.SerializeObject(
                log.ToApiDto(),
                Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ContractResolver = new XmlContractResolver(),
                    Converters = new List<JsonConverter> {
                        new StringEnumConverter { CamelCaseText = true },
                        new JavaScriptDateTimeConverter()
                    },
                    NullValueHandling = NullValueHandling.Ignore
                });

            string st12 = XmlHelper.ToXml(log);

            string st13 = "";
            //var model = AppDomain.CurrentDomain.GetAssemblies().SelectMany(p => p.GetTypes()).Where(p => p.FullName.Contains("Queries_"));

            //DbField field = new DbField();
            //field.Name = "test";
            //field.Alias = "alias";

            //Program._AppHost = new TAppHost<TestSettings>()
            //    .Configure(options => options
            //        .DefineSettings<TestSettings>()
            //        .SetRuntimeFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\run")
            //        .SetModule(new AppModule("app.test"))
            //        .SetExtensions(
            //            new AppExtensionConfiguration()
            //            .AddFilter("BindOpen.Framework.Databases")
            //            .AddFilter("BindOpen.Framework.Databases.MSSqlServer")
            //        )
            //        //.SetLibraryFolder(AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\lib")
            //        .AddDefaultLogger()
            //        .SetLoggers(
            //            LoggerFactory.Create<SnapLogger>(null, LoggerMode.Auto, DataSourceKind.Console))
            //    )
            //    //.UseSettingsFile((AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\run\settings\").ToPath())
            //    .Start();

            //string script = @"$(application.folderPath) ..\..\meltingFlow.Store.Sky.Repo";
            //string resultScript = Program._AppHost.ScriptInterpreter.Interprete(script, null, Program._AppHost.Log);

            //string path1 = Program._AppHost.GetSettings<TestSettings>().TestFolderPath;

            //string path2 = Program._AppHost.Settings.Get<string>("test.folderPath");

            //string path3 = Program._AppHost.Interpreter.Interprete("$sqlField('myfield')");

            //string path4 = "";

            //ILog log = new Log();
            //log.AddMessage("test1");
            //string st1 = log[0];

            //var st = Program._AppHost.Settings.Get<String>("test.folderPath").GetEndedString(@"\");

            //Console.WriteLine(Program._AppHost.GetKnownPath(ApplicationPathKind.SettingsFile));

            //var dbQuery = Queries_Tenants.InsertOrganization("tenantA");
            //.Filter(
            //    "name='Tenant'"
            //    , Program._AppManager.Log
            //    , new ApiScriptFilteringDefinition(
            //        new ApiScriptClause("CreationDate", new DbField("CreationDate", "tenant"))
            //        , new ApiScriptClause("Name", new DbField("Name", "tenant"), DataOperator.Equal)
            //        , new ApiScriptClause("Tenant", null, DataOperator.Has,
            //            new ApiScriptFilteringDefinition(
            //                new ApiScriptClause("CreationDate", new DbField("CreationDate", "Tenant", "Iam", null), DataOperator.GreaterOrEqual))
            //)))
            //.Sort(
            //    "creationdate asc, id desc"
            //    , Program._AppManager.Log
            //    , new ApiScriptSortingDefinition(
            //        new ApiScriptField("CreationDate", new DbField("CreationDate", "tenant"))
            //        , new ApiScriptField("Id", new DbField("Name", "tenant"))
            //        , new ApiScriptField("LastModificationDate", new DbField("LastModificationDate", "tenant"))
            //        , new ApiScriptField("Name", new DbField("DisplayName", "tenant"))
            //        , new ApiScriptField("ProviderName", new DbField("Name", "provider"))
            //        , new ApiScriptField("Provider.CreationDate", new DbField("CreationDate", "provider"))
            //        , new ApiScriptField("Provider.Id", new DbField("Name", "provider"))
            //        , new ApiScriptField("Provider.LastModificationDate", new DbField("LastModificationDate", "provider"))
            //        , new ApiScriptField("Provider.Name", new DbField("DisplayName", "provider"))
            //));

            //Program._AppHost.Log.Append(new DbQueryBuilder_MSSqlServer(Program._AppHost.AppScope).BuildQuery(dbQuery, null, out string sqlQuery));

            //using (DatabaseConnection connection =
            //    Program._AppHost.ConnectionService?.Open<DatabaseConnection>("test.db", null, Program._AppHost.Log))
            //{
            //}

            //Console.WriteLine(sqlQuery);

            //using (DatabaseConnection connection =
            //    Program._AppManager.ConnectionService.Open<DatabaseConnection>("test.db", null, Program._AppManager.Log))
            //{
            //    if (connection != null)
            //    {
            //        Program._AppManager.Log.Append(
            //            new DbQueryBuilder_MSSqlServer(
            //                Program._AppManager.AppScope).BuildQuery(
            //                    Queries_Tenants.GetTenant("MonTenant", "test.db"), null, out string sql1));

            //        //Program._AppManager.Log.Append(
            //        //new DbQueryBuilder_MSSqlServer(
            //        //    Program._AppManager.AppScope).BuildQuery(
            //        //        (Queries_Tenants.GetTenants("test.db") as AdvancedDbDataQuery)
            //        //            .Sort(
            //        //                "creationdate asc, id desc",
            //        //                Program._AppManager.Log,
            //        //                new Dictionary<string, DbField>(StringComparer.OrdinalIgnoreCase)
            //        //                {
            //        //                    { "CreationDate", null },
            //        //                    { "Id", new DbField("Id") },
            //        //                    { "Name", new DbField("Name") },
            //        //                    { "ProviderName", new DbField("ProviderName") },
            //        //                })
            //        //            .Filter(
            //        //                "creationdate >= '20181202' and Id=1234",
            //        //                Program._AppManager.Log,
            //        //                new Dictionary<string, ApiSearchClause>(StringComparer.OrdinalIgnoreCase)
            //        //                {
            //        //                    { "CreationDate", null },
            //        //                    { "Id", new DbDataQueryScriptElement(
            //        //                        new DbField("Id"), DataOperator.Equal) },
            //        //                }),
            //        //        null, out string sql2));

            //        Program._AppManager.Log.Append(
            //            new DbQueryBuilder_MSSqlServer(
            //                Program._AppManager.AppScope).BuildQuery(
            //                    Queries_Tenants.DeleteTenant("MonTenant", "test.db"), null, out string sql3));

            //        Program._AppManager.Log.Append(
            //            new DbQueryBuilder_MSSqlServer(
            //                Program._AppManager.AppScope).BuildQuery(
            //                    Queries_Tenants.UpdateTenant("MonTenant", "test.db"), null, out string sql4));

            //        Program._AppManager.Log.Append(
            //            new DbQueryBuilder_MSSqlServer(
            //                Program._AppManager.AppScope).BuildQuery(
            //                    Queries_Tenants.InsertTenant("MonTenant", "test.db"), null, out string sql5));
            //    }
            //}

            //(new DataItemSet<Event>(
            //    new Event(EventKind.Error),
            //    new Event(EventKind.Exception))).SaveXml(@"c:\workarea\temp\test.xml", Program._AppManager.Log);


            //var value = Program._AppManager.Configuration?.LogsFolderPath;

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();
            //Logger logger = LoggerFactory.Create<SnapLogger>(null, LoggerMode.Auto, DataSourceKind.Memory, folderPath: @"g:\temp");
            //stopwatch.Stop();
            //stopwatch.Restart();
            //Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            //for (int i=0;i<700000; i++)
            //    logger.WriteEvent(new LogEvent(EventKind.Message, "Log event" + i));
            //stopwatch.Stop();
            //Console.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);

            ////AppSettings configuration = new AppSettings(Program._AppHost.AppScope)
            ////{
            ////    ApplicationInstanceId = "applicationInstanceId",
            ////    ExecutionLevel = ApplicationExecutionLevel.DEV,
            ////    ApplicationInstanceKind = ApplicationInstanceKind.InCloud,
            ////    IsUserTrackingEnabled=true
            ////};
            ////String st = configuration.ApplicationInstanceId;
            ////var b = configuration.IsUserTrackingEnabled;
            ////configuration.SaveXml(SetupVariables.WorkingFolder + "config.xml");

            ////configuration = AppSettings.Load<AppSettings>(SetupVariables.WorkingFolder + "config.xml");


            //CarrierTest carrierTest = new CarrierTest();
            //carrierTest.TestCreateCarrier();
            //carrierTest.TestSaveCarrier();
            //carrierTest.TestLoadCarrier();


            //String script = "$SqlTable('MYTABLE').SqlField('MYFIELD')='abc'";
            //string resultScript = "";
            //using (ScriptVariableSet scriptVariableSet = new ScriptVariableSet())
            //{
            //    scriptVariableSet.SetValue("database_kind", DatabaseConnectorKind.MSSqlServer);
            //    resultScript = SetupVariables.AppScope.ScriptInterpreter.Interprete(
            //        script, scriptVariableSet, Program._AppManager.Log);
            //}


            //using (DatabaseConnection connection =
            //    Program._AppManager.ConnectionService.Open<DatabaseConnection>(
            //        "platform.bdd", null, Program._AppManager.Log))
            //    if (connection != null)
            //        connection.ExecuteNonQuery("SELECT * FROM TABLE1", null, Program._AppManager.Log);
        }
    }
}
