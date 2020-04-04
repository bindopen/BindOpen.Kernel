using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Stores;
using NUnit.Framework;

namespace BindOpen.Tests.Core.Application.Hosts
{
    [TestFixture]
    public class AppHostTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestAppHost()
        {
            Assert.That(GlobalVariables.AppHost.IsLoaded, "Application host not load failed");
        }

        [Test]
        public void TestAppHostWithNoOptions()
        {
            var appHost = BdoHostFactory.CreateBindOpenDefaultHost(
                options => options
                    .AddDefaultConsoleLogger()
                    .AddDefaultFileLogger());

            Assert.That(appHost.IsLoaded, "Application host not load failed");

            Assert.That(appHost.Options.HostSettings.ApplicationInstanceName == "test.console", "Application host settings not loaded correctly");

            Assert.That(appHost.Options.HostSettings.LogsExpirationDayNumber == 1, "Application host settings not loaded correctly");

            Assert.That(appHost.Options.AppSettings.Get<string>("test.folderPath") != null, "Application host settings not loaded correctly");
        }

        [Test]
        public void TestAppHostWithDatasources()
        {
            var appHost = BdoHostFactory.CreateBindOpenDefaultHost(
                options => options
                    .AddDataStore(store => store
                        .RegisterDatasources(m => m.AddFromConfiguration(options)))
                    .AddDefaultConsoleLogger()
                    .AddDefaultFileLogger());

            Assert.That(appHost.IsLoaded, "Application host not load failed");

            var datasource = appHost.GetDatasourceDepot()?["db.test"];
            Assert.That(datasource?.Name == "db.test", "Bad data source loading");

            Assert.That(appHost.GetDatasourceDepot()?.GetConnectionString("db.test") != StringHelper.__NoneString,
                "Bad data source loading");

            Assert.That(appHost.GetDatasourceDepot()?.GetConnectionString("db.test", "database.mssqlserver$client") != StringHelper.__NoneString,
                "Bad data source loading");
        }
    }
}
