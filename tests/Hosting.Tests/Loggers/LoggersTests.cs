using BindOpen.Logging;
using BindOpen.Logging.Loggers;
using NUnit.Framework;

namespace BindOpen.Hosting.Loggers;

/// <summary>
/// Contains unit tests for logger configuration and registration within BindOpen Hosting.
/// </summary>
/// <remarks>
/// These tests verify that host instances can be configured with custom or built-in
/// <see cref="IBdoLogger"/> implementations and start without initialization errors.
/// </remarks>
[TestFixture]
public class LoggersTests
{
    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Verifies that a host configured with a <see cref="BdoTraceLogger"/> via <c>SetLogger</c>
    /// starts properly and reaches the <see cref="ProcessExecutionState.Pending"/> state.
    /// </summary>
    [Test]
    public void DefaultConfigurationTest()
    {
        var bdoHost = BdoHosting.NewHost(
            options => options
                .SetLogger(q => BdoLogging.NewLogger<BdoTraceLogger>()));
        bdoHost.Start();

        Assert.That(bdoHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host failed to reach the expected Pending state.");
    }
}