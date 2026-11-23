using BindOpen.Data;
using BindOpen.Logging.Tests;
using NUnit.Framework;

namespace BindOpen.Logging.Loggers;

/// <summary>
/// Contains unit tests for basic logger implementations (<see cref="BdoTraceLogger"/> and <see cref="BdoConsoleLogger"/>)
/// and root log lifecycle management within BindOpen Logging.
/// </summary>
/// <remarks>
/// These tests verify root log singleton consistency per logger instance and validate
/// high-volume hierarchical log population and output rendering.
/// </remarks>
[TestFixture]
public class BasicLoggerTests
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
    /// Populates the specified <see cref="IBdoLog"/> with a structured hierarchy of errors,
    /// exceptions, messages, warnings, and nested child log entries.
    /// </summary>
    /// <param name="log">The target log instance to populate.</param>
    private void PopulateLog(IBdoLog log)
    {
        if (log != null)
        {
            for (int i = 0; i < _testData.itemNumber; i++)
            {
                log.AddError("Error" + i);
                log.AddException("Exception" + i);
                log.AddMessage("Message" + i);
                log.AddWarning("Warning" + i);
                log.InsertChild(q => q.WithTitle("Child" + i))
                    .AddError("Error" + i + "-1");
            }

            var subLog = log?.InsertChild(BdoEventKinds.Message, "Initializing Child...");
            var childLog = subLog?.InsertChild(BdoEventKinds.Message, "Loading Child configuration...");
        }
    }

    /// <summary>
    /// Verifies that consecutive calls to <see cref="IBdoLogger.NewRootLog"/> on the same logger
    /// return the same root log instance identified by matching <see cref="IBdoLog.Identifier"/> values.
    /// </summary>
    [Test]
    public void RootLogTest()
    {
        var logger = BdoLogging.NewLogger<BdoTraceLogger>();

        var log = logger.NewRootLog();

        var newLog = logger.NewRootLog();

        Assert.That(log?.Identifier, Is.EqualTo(newLog?.Identifier), "Root log instances returned by the same logger must share the same identifier.");
    }

    /// <summary>
    /// Verifies that <see cref="BdoTraceLogger"/> successfully processes and writes a large
    /// multi-level hierarchy of log entries without errors.
    /// </summary>
    [Test]
    public void TraceLoggerTest()
    {
        var logger = BdoLogging.NewLogger<BdoTraceLogger>();

        var log = logger.NewRootLog().AddChild();
        PopulateLog(log);

        logger.Log(log);
    }

    /// <summary>
    /// Verifies that <see cref="BdoConsoleLogger"/> successfully processes and renders a large
    /// multi-level hierarchy of log entries to the console output without errors.
    /// </summary>
    [Test]
    public void ConsoleLoggerTest()
    {
        var logger = BdoLogging.NewLogger<BdoConsoleLogger>();

        var log = logger.NewRootLog();
        PopulateLog(log);

        logger.Log(log);
    }
}