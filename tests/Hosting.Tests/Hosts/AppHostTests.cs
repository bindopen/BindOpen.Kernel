using BindOpen.Hosting.Tests;
using BindOpen.Logging;
using NUnit.Framework;

namespace BindOpen.Hosting;

/// <summary>
/// Contains unit tests for application host initialization and execution state management within BindOpen Hosting.
/// </summary>
/// <remarks>
/// These tests verify that both the global application host and newly instantiated host instances
/// initialize and transition to the expected <see cref="ProcessExecutionState"/>.
/// </remarks>
[TestFixture]
public class AppHostTests
{
    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Verifies that the global application host (<see cref="GlobalVariables.AppHost"/>)
    /// is initialized in the <see cref="ProcessExecutionState.Pending"/> state.
    /// </summary>
    [Test]
    public void TestAppHost()
    {
        Assert.That(GlobalVariables.AppHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host failed to load in the expected Pending state.");
    }

    /// <summary>
    /// Verifies that a host instantiated without explicit options via <see cref="BdoHosting.NewHost"/>
    /// starts cleanly and reaches the <see cref="ProcessExecutionState.Pending"/> state.
    /// </summary>
    [Test]
    public void TestAppHostWithNoOptions()
    {
        var bdoHost = BdoHosting.NewHost();
        bdoHost.Start();

        Assert.That(bdoHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host without options failed to reach the expected Pending state.");
    }
}