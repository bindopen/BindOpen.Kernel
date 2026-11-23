using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Scoping.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping.Tasks;

/// <summary>
/// Contains unit tests for XML and JSON serialization and deserialization of <see cref="TaskFake"/>
/// instances via <see cref="MetaObjectDto"/> conversion and scope-based instantiation.
/// </summary>
/// <remarks>
/// These tests verify that task instances initialized from faked metadata via <see cref="BdoTaskFaker"/>
/// can be converted to <see cref="IBdoMetaObject"/> representations, mapped to DTOs, persisted to XML
/// and JSON files on disk, and re-instantiated through <see cref="IBdoScope.CreateTask{T}"/> without
/// losing configuration or runtime property state.
/// </remarks>
[TestFixture]
public class IOTaskTests
{
    private TaskFake _task = null;

    private dynamic _testData;

    /// <summary>
    /// Cleans up previous XML and JSON test output files and generates faked task test data
    /// using <see cref="BdoTaskFaker"/> before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        File.Delete(BdoTaskFaker.XmlFilePath);
        File.Delete(BdoTaskFaker.JsonFilePath);

        _testData = BdoTaskFaker.NewData();
    }

    // Xml

    /// <summary>
    /// Verifies that a <see cref="TaskFake"/> instance converts to a <see cref="MetaObjectDto"/>
    /// and saves cleanly to an XML file on disk.
    /// </summary>
    [Test]
    public void SaveXmlTest()
    {
        if (_task == null)
        {
            IBdoMetaObject meta = BdoTaskFaker.NewMetaObject(_testData);
            _task = ScopingTestData.Scope.CreateTask<TaskFake>(meta);
        }

        var isSaved = _task.ToMeta(ScopingTestData.Scope).ToDto().SaveXml(BdoTaskFaker.XmlFilePath);

        Assert.That(isSaved, Is.True, "Task XML saving failed.");
    }

    /// <summary>
    /// Verifies that the saved XML file loads back into a <see cref="MetaObjectDto"/>, converts to a
    /// metadata object via <c>ToPoco()</c>, and creates a valid <see cref="TaskFake"/> instance.
    /// </summary>
    [Test]
    public void LoadXmlTest()
    {
        if (_task == null || !File.Exists(BdoTaskFaker.XmlFilePath))
        {
            SaveXmlTest();
        }

        var meta = XmlHelper.LoadXml<MetaObjectDto>(BdoTaskFaker.XmlFilePath).ToPoco();
        var task = ScopingTestData.Scope.CreateTask<TaskFake>(meta);

        Assert.That(task, Is.Not.Null, "Task loading from XML failed.");

        BdoTaskFaker.AssertFake(task, _testData);
    }

    // Json

    /// <summary>
    /// Verifies that a <see cref="TaskFake"/> instance converts to a <see cref="MetaObjectDto"/>
    /// and saves cleanly to a JSON file on disk.
    /// </summary>
    [Test]
    public void SaveJsonTest()
    {
        if (_task == null)
        {
            IBdoMetaObject meta = BdoTaskFaker.NewMetaObject(_testData);
            _task = ScopingTestData.Scope.CreateTask<TaskFake>(meta);
        }

        var isSaved = _task.ToMeta(ScopingTestData.Scope).ToDto().SaveJson(BdoTaskFaker.JsonFilePath);

        Assert.That(isSaved, Is.True, "Task JSON saving failed.");
    }

    /// <summary>
    /// Verifies that the saved JSON file loads back into a <see cref="MetaObjectDto"/>, converts to a
    /// metadata object via <c>ToPoco()</c>, and creates a valid <see cref="TaskFake"/> instance.
    /// </summary>
    [Test]
    public void LoadJsonTest()
    {
        if (_task == null || !File.Exists(BdoTaskFaker.JsonFilePath))
        {
            SaveJsonTest();
        }

        var meta = JsonHelper.LoadJson<MetaObjectDto>(BdoTaskFaker.JsonFilePath).ToPoco();
        var task = ScopingTestData.Scope.CreateTask<TaskFake>(meta);

        Assert.That(task, Is.Not.Null, "Task loading from JSON failed.");

        BdoTaskFaker.AssertFake(task, _testData);
    }
}