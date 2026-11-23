using BindOpen.Hosting.Tests;
using BindOpen.Logging;
using NUnit.Framework;

namespace BindOpen.Hosting;

/// <summary>
/// Contains unit tests for host configuration loading, strongly-typed settings casting,
/// and multi-file XML configuration merging within BindOpen Hosting.
/// </summary>
/// <remarks>
/// These tests verify that host options properly apply programmatic settings, instantiate
/// custom settings types, and merge properties from single or multiple external configuration files.
/// </remarks>
[TestFixture]
public class ConfigurationTests
{
    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Verifies that programmatic host settings configured via <c>SetSettings</c>
    /// are applied and preserved once the host starts.
    /// </summary>
    [Test]
    public void DefaultConfigurationTest()
    {
        var bdoHost = BdoHosting.NewHost(
            options => options
                .SetSettings(q => q.Kernel.ApplicationInstanceName = "host-test"));
        bdoHost.Start();

        Assert.That(bdoHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host failed to reach the expected Pending state.");
        Assert.That(bdoHost.Options?.Settings?.Kernel?.ApplicationInstanceName, Is.EqualTo("host-test"), "The configured ApplicationInstanceName was not applied.");
    }

    /// <summary>
    /// Verifies that configuring the host with a custom strongly-typed settings class
    /// (<see cref="TestSettings"/>) properly instantiates and exposes that settings type.
    /// </summary>
    [Test]
    public void CastingConfigTest()
    {
        var bdoHost = BdoHosting.NewHost(
            options => options
                .SetRootFolder(@".\")
                .SetSettings<TestSettings>());
        bdoHost.Start();

        Assert.That(bdoHost.Options.Settings, Is.InstanceOf<TestSettings>(), "Host settings were not instantiated as TestSettings.");
    }

    /// <summary>
    /// Verifies that the host loads and resolves strongly-typed <see cref="TestSettings"/>
    /// from the default kernel configuration file without initialization errors.
    /// </summary>
    [Test]
    public void KernelConfigFileTest()
    {
        var bdoHost = BdoHosting.NewHost(
            options => options
                .SetRootFolder(@".\")
                .SetSettings<TestSettings>()
                .AddConfigurationFile());
        bdoHost.Start();

        var settings = bdoHost.Options.GetSettings<TestSettings>();

        Assert.That(settings, Is.Not.Null, "TestSettings could not be retrieved from host options.");
        //Assert.That(settings.Kernel.ApplicationInstanceName, Is.EqualTo("test.console"), "Application instance name mismatch.");
    }

    /// <summary>
    /// Verifies that loading multiple sequential configuration files (<c>appconfig.xml</c> and <c>appconfig2.xml</c>)
    /// merges scalar properties and aggregates collection entries into <see cref="TestSettings"/>.
    /// </summary>
    [Test]
    public void SeveralFilesTest()
    {
        var bdoHost = BdoHosting.NewHost(
            options => options
                .SetRootFolder(@".\bdo\config")
                .SetSettings<TestSettings>()
                .AddConfigurationFile(@".\appconfig.xml", true)
                .AddConfigurationFile(@".\appconfig2.xml", true)
                .ThrowExceptionOnInitFailure());
        bdoHost.Start();

        var settings = bdoHost.Options.GetSettings<TestSettings>();

        Assert.That(settings.FolderPath, Is.EqualTo("_folderPath"), "FolderPath was not properly loaded from the configuration files.");
        Assert.That(settings.NewsUris, Has.Count.EqualTo(2), "NewsUris should contain 2 merged entries across the configuration files.");
    }
}