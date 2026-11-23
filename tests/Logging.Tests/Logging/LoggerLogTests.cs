using BindOpen.Logging.Tests;
using NUnit.Framework;

namespace BindOpen.Logging.Loggers;

/// <summary>
/// Contains unit tests for log execution state transitions and lifecycle management
/// within BindOpen Logging.
/// </summary>
/// <remarks>
/// These tests verify that root logs created by loggers properly track execution metadata
/// and update their <see cref="ProcessExecutionStatus"/> when lifecycle methods are invoked.
/// </remarks>
[TestFixture]
public class LoggerLogTests
{
    private readonly string _filePath_serilog = GlobalVariables.WorkingFolder + "Debug.txt";

    private dynamic _testData;

    /// <summary>
    /// Initializes test parameters before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = new
        {
            itemNumber = 1000
        };
    }

    /// <summary>
    /// Verifies that calling <c>WithExecutionAsStarted()</c> on a root log transitions
    /// its <see cref="ProcessExecutionStatus"/> to <see cref="ProcessExecutionStatus.Processing"/>.
    /// </summary>
    [Test]
    public void RootLogTest()
    {
        var logger = BdoLogging.NewLogger<BdoTraceLogger>();

        var log = logger.NewRootLog();

        log.WithExecutionAsStarted();

        Assert.That(log?.Execution?.Status, Is.EqualTo(ProcessExecutionStatus.Processing), "Root log execution status should transition to Processing when started.");
    }
}