using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Stores;
using BindOpen.Hosting.Tests;
using BindOpen.Logging;
using NUnit.Framework;

namespace BindOpen.Hosting;

/// <summary>
/// Contains unit tests for datasource instantiation, registration, and depot resolution
/// within BindOpen Hosting.
/// </summary>
/// <remarks>
/// These tests verify that datasources can be instantiated from metadata wrappers as well as
/// registered into the host's depot store from .NET Core configurations and BindOpen XML settings files.
/// </remarks>
[TestFixture]
public class DatasourcesTests
{
    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Verifies that a <see cref="BdoDatasource"/> wrapper can be instantiated from an <see cref="IBdoMetaSet"/>
    /// and properly exposes its connection string and <see cref="DatasourceKind"/>.
    /// </summary>
    [Test]
    public void CreateDatasourceTest()
    {
        var detail = BdoData.NewSet()
            .Add(
                (IBdoDatasource.__ConnectionString_Token, "test_connectionString"),
                (IBdoDatasource.__ConnectionString_DatasourceKind, DatasourceKind.EmailServer));

        var datasource = GlobalVariables.AppHost.NewMetaWrapper<BdoDatasource>(detail);

        Assert.That(datasource.ConnectionString, Is.EqualTo("test_connectionString"), "The datasource ConnectionString was not mapped properly.");
        Assert.That(datasource.DatasourceKind, Is.EqualTo(DatasourceKind.EmailServer), "The datasource DatasourceKind was not mapped properly.");
    }

    /// <summary>
    /// Verifies that datasources defined in the .NET Core <see cref="GlobalVariables.NetCoreConfiguration"/>
    /// are registered into the host's datasource depot and retain their inferred types and connection strings.
    /// </summary>
    [Test]
    public void AddFromConnectionStringsTest()
    {
        var bdoHost = BdoHosting.NewHost(
            options => options
                .AddDepotStore(store => store
                    .RegisterDatasources(m => m
                        .AddFromConnectionStrings(GlobalVariables.NetCoreConfiguration))));
        bdoHost.Start();

        Assert.That(bdoHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host failed to reach the expected Pending state.");

        var datasourceDepot = bdoHost.GetDatasourceDepot();

        var datasourceA = datasourceDepot?["db.testA"];
        Assert.That(datasourceA?.Name, Is.EqualTo("db.testA"), "Datasource 'db.testA' was not loaded with the expected name.");
        Assert.That(datasourceA?.DatasourceKind, Is.EqualTo(DatasourceKind.Database), "Datasource 'db.testA' was not identified as a Database.");

        Assert.That(datasourceDepot?.Get("db.testA")?.ConnectionString, Is.Not.Null,
            "Datasource 'db.testA' connection string should not be null.");

        var datasourceB = datasourceDepot?["db.testB"];
        Assert.That(datasourceB?.Name, Is.EqualTo("db.testB"), "Datasource 'db.testB' was not loaded from .NET Core configuration.");
    }

    /// <summary>
    /// Verifies that datasources configured across multiple BindOpen XML settings files
    /// (<c>appconfig.xml</c> and <c>appconfig2.xml</c>) are registered and merged into the host's datasource depot.
    /// </summary>
    [Test]
    public void AddFromSettingsTest()
    {
        var bdoHost = BdoHosting.NewHost(
            options => options
                .AddConfigurationFile(@".\bdo\config\appconfig.xml")
                .AddConfigurationFile(@".\bdo\config\appconfig2.xml")
                .AddDepotStore(store => store
                    .RegisterDatasources(m => m
                        .AddFromSettings(options.Settings))));
        bdoHost.Start();

        Assert.That(bdoHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host failed to reach the expected Pending state.");

        var datasourceDepot = bdoHost.GetDatasourceDepot();

        var datasourceA = datasourceDepot?["db.testA"];
        Assert.That(datasourceA?.Name, Is.EqualTo("db.testA"), "Datasource 'db.testA' was not loaded with the expected name.");
        Assert.That(datasourceA?.DatasourceKind, Is.EqualTo(DatasourceKind.EmailServer), "Datasource 'db.testA' should have been overridden as EmailServer.");

        Assert.That(datasourceDepot?.Get("db.testA")?.ConnectionString, Is.Not.Null,
            "Datasource 'db.testA' connection string should not be null.");

        var datasourceB = datasourceDepot?["db.testB"];
        Assert.That(datasourceB?.Name, Is.EqualTo("db.testB"), "Datasource 'db.testB' was not loaded from settings.");

        var datasourceC = datasourceDepot?["db.testC"];
        Assert.That(datasourceC?.Name, Is.EqualTo("db.testC"), "Datasource 'db.testC' was not loaded from settings.");
    }
}