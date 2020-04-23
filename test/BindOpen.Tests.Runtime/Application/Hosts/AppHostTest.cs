using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Stores;
using NUnit.Framework;

namespace BindOpen.Tests.Core.Application.Hosts
{
    [TestFixture]
    public class AppHostTest
    {
        [OneTimeSetUp]
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
                        .RegisterDatasources(m => m
                            .AddFromConfiguration(options)
                            .AddFromNetCoreConfiguration(GlobalVariables.NetCoreConfiguration)))
                    .AddDefaultConsoleLogger()
                    .AddDefaultFileLogger());

            Assert.That(appHost.IsLoaded, "Application host not load failed");

            var datasourceA = appHost.GetDatasourceDepot()?["db.testA"];
            Assert.That(datasourceA?.Name == "db.testA", "Bad data source loading");

            Assert.That(appHost.GetDatasourceDepot()?.GetConnectionString("db.testA") != StringHelper.__NoneString,
                "Bad data source loading");

            Assert.That(appHost.GetDatasourceDepot()?.GetConnectionString("db.testA", "database.mssqlserver$client") != StringHelper.__NoneString,
                "Bad data source loading");

            var datasourceB = appHost.GetDatasourceDepot()?["db.testB"];
            Assert.That(datasourceB?.Name == "db.testB", "Bad data source loading from .NET Core configuration");

            Assert.That(appHost.GetDatasourceDepot()?.GetConnectionString("db.testB") != StringHelper.__NoneString,
                "Bad data source loading from .NET Core configuration");

            Assert.That(appHost.GetDatasourceDepot().Get()?.Name == "db.testA", "Bad data source loading");

            Assert.That(appHost.GetDatasourceDepot().GetConnectionString() != StringHelper.__NoneString, "Bad data source loading");
        }
    }
}
