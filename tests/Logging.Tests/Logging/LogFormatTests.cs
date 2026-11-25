using BindOpen.Data;
using BindOpen.Logging.Loggers;
using BindOpen.Logging.Tests;
using NUnit.Framework;
using System;

namespace BindOpen.Logging;

/// <summary>
/// Contains unit tests for log and log event formatting using specialized logger formatters
/// such as <see cref="BdoSnapLoggerFormater"/>.
/// </summary>
/// <remarks>
/// These tests verify that hierarchical <see cref="BdoLog"/> structures and individual log events
/// render their textual representations accurately according to the configured formatter.
/// </remarks>
[TestFixture]
public class LogFormatTests
{
    private readonly string _filePath = GlobalVariables.WorkingFolder + "Log.xml";

    private dynamic _testData;

    /// <summary>
    /// Initializes a structured <see cref="BdoLog"/> containing errors, exceptions, messages,
    /// warnings, and nested sub-logs before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var log = BdoLogging.NewLog();
        for (int i = 0; i < 2; i++)
        {
            log
                .AddError("Error" + i)
                .AddException("Exception" + i)
                .AddMessage("Message" + i)
                .AddWarning("Warning" + i)
                .AddChild(
                    BdoLogging.NewLog().WithTitle("Sub log" + i)
                );
        }

        _testData = new
        {
            log
        };
    }

    /// <summary>
    /// Verifies that <c>ToString&lt;BdoSnapLoggerFormater&gt;()</c> formats a complete multi-item
    /// <see cref="BdoLog"/> hierarchy into the expected indented string output.
    /// </summary>
    [Test]
    public void LogToStringTest()
    {
        BdoLog log = _testData.log;
        var st = log.ToString<BdoSnapLoggerFormater>();
        var st_expected =
            " - Error: Error0" + Environment.NewLine +
            " - Exception: Exception0" + Environment.NewLine +
            " - Message: Message0" + Environment.NewLine +
            " - Warning: Warning0" + Environment.NewLine +
            " o Sub log0" + Environment.NewLine +
            " - Error: Error1" + Environment.NewLine +
            " - Exception: Exception1" + Environment.NewLine +
            " - Message: Message1" + Environment.NewLine +
            " - Warning: Warning1" + Environment.NewLine +
            " o Sub log1" + Environment.NewLine;

        Assert.That(st, Is.EqualTo(st_expected), "The formatted string output for the log hierarchy did not match the expected result.");
    }

    /// <summary>
    /// Verifies that <c>ToString&lt;BdoSnapLoggerFormater&gt;()</c> formats an individual log event
    /// entry into its expected single-line string representation.
    /// </summary>
    [Test]
    public void LogEventToStringTest()
    {
        BdoLog log = _testData.log;
        var logEvent = log._Events?[0];
        var st = logEvent.ToString<BdoSnapLoggerFormater>();
        var st_expected = "- Error: Error0" + Environment.NewLine;

        Assert.That(st, Is.EqualTo(st_expected), "The formatted string output for the individual log event did not match the expected result.");
    }
}