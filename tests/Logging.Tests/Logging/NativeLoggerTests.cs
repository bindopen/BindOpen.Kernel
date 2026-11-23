using BindOpen.Logging.Tests;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Serilog;
using System.Linq;

namespace BindOpen.Logging.Loggers;

/// <summary>
/// Contains unit tests for external logging framework integration (Microsoft.Extensions.Logging and Serilog)
/// within BindOpen Logging.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="BdoLogging"/> can wrap an <see cref="ILoggerFactory"/> configured
/// with third-party providers such as Serilog, and accurately record and forward high-volume
/// hierarchical logs without losing events or child entries.
/// </remarks>
[TestFixture]
public class ExternalLoggerTests
{
    private readonly string _filePath_serilog = GlobalVariables.WorkingFolder + "Serilog.txt";

    private IBdoCompleteLog _log = null;

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
    /// Validates that the specified <see cref="IBdoCompleteLog"/> contains the expected number
    /// of errors, exceptions, messages, warnings, and child log entries.
    /// </summary>
    /// <param name="log">The complete log instance to validate.</param>
    private void Test(IBdoCompleteLog log)
    {
        int expectedCount = _testData.itemNumber;

        Assert.Multiple(() =>
        {
            Assert.That(log.Errors().Count(), Is.EqualTo(expectedCount), $"Bad insertion of errors ({expectedCount} expected; {log.Errors().Count()} found).");
            Assert.That(log.Exceptions().Count(), Is.EqualTo(expectedCount), $"Bad insertion of exceptions ({expectedCount} expected; {log.Exceptions().Count()} found).");
            Assert.That(log.Messages().Count(), Is.EqualTo(expectedCount), $"Bad insertion of messages ({expectedCount} expected; {log.Messages().Count()} found).");
            Assert.That(log.Warnings().Count(), Is.EqualTo(expectedCount), $"Bad insertion of warnings ({expectedCount} expected; {log.Warnings().Count()} found).");
            Assert.That(log.Children().Count(), Is.EqualTo(expectedCount), $"Bad insertion of sub-logs ({expectedCount} expected; {log.Children().Count()} found).");
        });
    }

    /// <summary>
    /// Verifies that a BindOpen logger backed by a Serilog-configured <see cref="ILoggerFactory"/>
    /// processes and retains 1,000 entries of each event category and child log level.
    /// </summary>
    [Test]
    public void CreateTest()
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(_filePath_serilog, rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var loggerFactory = new LoggerFactory();
        loggerFactory.AddSerilog(Log.Logger);

        var logger = BdoLogging.NewLogger(loggerFactory);
        _log = logger.NewRootLog();

        for (int i = 0; i < _testData.itemNumber; i++)
        {
            _log.AddError("Error" + i);
            _log.AddException("Exception" + i);
            _log.AddMessage("Message" + i);
            _log.AddWarning("Warning" + i);
            _log.AddChild(BdoLogging.NewLog());
        }

        _log.WithEvents(_log._Events?.ToArray());
        _log.WithChildren(_log._Children?.ToArray());

        logger.Log(_log);

        Test(_log);
    }
}