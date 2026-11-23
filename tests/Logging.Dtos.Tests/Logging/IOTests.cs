using BindOpen.Data;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace BindOpen.Logging.Dtos.Tests;

/// <summary>
/// Contains unit tests for creation, XML serialization/deserialization, and JSON
/// serialization/deserialization of <see cref="IBdoCompleteLog"/> instances via <see cref="LogDto"/>.
/// </summary>
/// <remarks>
/// These tests verify that large, hierarchical log structures containing details, errors,
/// exceptions, messages, warnings, and nested child logs can be converted to DTOs, persisted to
/// XML and JSON files on disk, and reloaded back into domain objects without losing entries or
/// structural integrity.
/// </remarks>
[TestFixture]
public class IOTests
{
    private readonly string _filePath_xml = GlobalVariables.WorkingFolder + "Log.xml";
    private readonly string _filePath_json = GlobalVariables.WorkingFolder + "Log.json";

    private IBdoCompleteLog _log = null;

    private dynamic _testData;

    /// <summary>
    /// Cleans up any leftover XML and JSON log files and initializes test parameters
    /// before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(_filePath_xml);
        File.Delete(_filePath_json);

        _testData = new
        {
            itemNumber = 1000
        };
    }

    /// <summary>
    /// Asserts that the specified <see cref="IBdoCompleteLog"/> instance contains the expected
    /// number of errors, exceptions, messages, warnings, and child sub-logs.
    /// </summary>
    /// <param name="log">The log instance to validate.</param>
    private void Test(IBdoCompleteLog log)
    {
        Assert.That(log, Is.Not.Null, "Log instance cannot be null.");
        Assert.That(log.Errors(false).Count(), Is.EqualTo((int)_testData.itemNumber), string.Format("Bad insertion of errors ({0} expected; {1} found)", _testData.itemNumber, log.Errors(false).Count()));
        Assert.That(log.Exceptions().Count(), Is.EqualTo((int)_testData.itemNumber), string.Format("Bad insertion of exceptions ({0} expected; {1} found)", _testData.itemNumber, log.Exceptions().Count()));
        Assert.That(log.Messages().Count(), Is.EqualTo((int)_testData.itemNumber), string.Format("Bad insertion of messages ({0} expected; {1} found)", _testData.itemNumber, log.Messages().Count()));
        Assert.That(log.Warnings().Count(), Is.EqualTo((int)_testData.itemNumber), string.Format("Bad insertion of warnings ({0} expected; {1} found)", _testData.itemNumber, log.Warnings().Count()));
        Assert.That(log.Children().Count(), Is.EqualTo((int)_testData.itemNumber), string.Format("Bad insertion of sub logs ({0} expected; {1} found)", _testData.itemNumber, log.Children().Count()));
    }

    /// <summary>
    /// Verifies that a new <see cref="IBdoCompleteLog"/> can be populated with metadata details,
    /// log entries across all severity levels, and nested child logs.
    /// </summary>
    [Test]
    public void CreateTest()
    {
        _log = BdoLogging.NewLog()
            .WithDetail(
                ("string", DataValueTypes.Text, "stringValue"),
                ("int", DataValueTypes.Integer, 1500)
            );

        for (int i = 0; i < _testData.itemNumber; i++)
        {
            _log.AddError("Error" + i);
            _log.AddException("Exception" + i);
            _log.AddMessage("Message" + i);
            _log.AddWarning("Warning" + i);
            _log.AddChild(BdoLogging.NewLog()
                .WithTitle("Child" + i)
                .AddError("Child_Error" + i + "_1")
            );
        }

        Test(_log);
    }

    // Xml

    /// <summary>
    /// Verifies that the populated <see cref="IBdoCompleteLog"/> converts to a <see cref="LogDto"/>
    /// and saves cleanly to an XML file without recording operation errors.
    /// </summary>
    [Test]
    public void SaveXmlTest()
    {
        if (_log == null)
        {
            CreateTest();
        }

        var log = BdoLogging.NewLog();
        _log.ToDto()?.SaveXml(_filePath_xml, log);

        string xml = string.Empty;
        if (log.HasErrorOrException())
        {
            xml = ". Result was '" + log.ToDto().ToXml() + "'";
        }
        Assert.That(log.HasErrorOrException(), Is.False, "Log saving failed" + xml);
    }

    /// <summary>
    /// Verifies that the saved XML file loads back into a <see cref="LogDto"/>, converts to a POCO
    /// via <c>ToPoco()</c>, and preserves all expected log entries and child logs.
    /// </summary>
    [Test]
    public void LoadXmlTest()
    {
        if (_log == null || !File.Exists(_filePath_xml))
        {
            SaveXmlTest();
        }

        BdoLog log = BdoLogging.NewLog();
        _log = XmlHelper.LoadXml<LogDto>(_filePath_xml, log: log).ToPoco();

        string xml = string.Empty;
        if (log.HasErrorOrException())
        {
            xml = ". Result was '" + log.ToDto().ToXml() + "'";
        }
        Assert.That(log.HasErrorOrException(), Is.False, "Error while loading log" + xml);
        Assert.That(_log.HasErrorOrException(), Is.True, "Loaded log is expected to contain recorded errors and exceptions.");

        Test(_log);
    }

    // Json

    /// <summary>
    /// Verifies that the populated <see cref="IBdoCompleteLog"/> converts to a <see cref="LogDto"/>
    /// and saves cleanly to a JSON file without recording operation errors.
    /// </summary>
    [Test]
    public void SaveJsonTest()
    {
        if (_log == null)
        {
            CreateTest();
        }

        var log = BdoLogging.NewLog();
        _log.ToDto()?.SaveJson(_filePath_json, log);

        string json = string.Empty;
        if (log.HasErrorOrException())
        {
            json = ". Result was '" + log.ToDto().ToJson() + "'";
        }
        Assert.That(log.HasErrorOrException(), Is.False, "Log saving failed" + json);
    }

    /// <summary>
    /// Verifies that the saved JSON file loads back into a <see cref="LogDto"/>, converts to a POCO
    /// via <c>ToPoco()</c>, and preserves all expected log entries and child logs.
    /// </summary>
    [Test]
    public void LoadJsonTest()
    {
        if (_log == null || !File.Exists(_filePath_json))
        {
            SaveJsonTest();
        }

        BdoLog log = BdoLogging.NewLog();
        _log = JsonHelper.LoadJson<LogDto>(_filePath_json, log: log).ToPoco();

        string json = string.Empty;
        if (log.HasErrorOrException())
        {
            json = ". Result was '" + log.ToDto().ToJson() + "'";
        }
        Assert.That(log.HasErrorOrException(), Is.False, "Error while loading log" + json);
        Assert.That(_log.HasErrorOrException(), Is.True, "Loaded log is expected to contain recorded errors and exceptions.");

        Test(_log);
    }
}