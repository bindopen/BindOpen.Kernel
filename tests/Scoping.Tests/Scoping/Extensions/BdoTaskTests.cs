using BindOpen.Data.Meta;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Tasks;

/// <summary>
/// This class provides unit tests for BdoTasks, verifying their lifecycle, 
/// metadata-driven instantiation, and bidirectional state synchronization.
/// </summary>
[TestFixture, Order(300)]
public class BdoTaskTests
{
    private dynamic _testData;

    /// <summary>
    /// Initializes the randomized test data using the task faker.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoTaskFaker.NewData();
    }

    /// <summary>
    /// Tests the creation of a task directly from a MetaSet using the typed Scope factory.
    /// </summary>
    [Test, Order(1)]
    public void CreateTaskTest_FromMetaSet()
    {
        // 1. Generate a meta-object containing task configuration
        IBdoMetaObject meta = BdoTaskFaker.NewMetaObject(_testData);

        // 2. Resolve the concrete task instance via the Scope
        var task = ScopingTestData.Scope.CreateTask<TaskFake>(meta);

        // 3. Verify task integrity using the faker's standardized assertion logic
        BdoTaskFaker.AssertFake(task, _testData);
    }

    /// <summary>
    /// Tests the creation of a task from a generic configuration using type casting.
    /// </summary>
    [Test, Order(2)]
    public void CreateTaskTest_FromConfig()
    {
        IBdoMetaObject meta = BdoTaskFaker.NewMetaObject(_testData);

        // Resolve using the non-generic CreateTask method
        var task = ScopingTestData.Scope.CreateTask(meta) as TaskFake;

        BdoTaskFaker.AssertFake(task, _testData);
    }

    /// <summary>
    /// Tests the conversion of a POCO task into metadata and its subsequent 
    /// reconstruction via the scoping engine.
    /// </summary>
    [Test, Order(3)]
    public void CreateTaskTest_FromObject()
    {
        // 1. Manually instantiate a POCO task
        var task = new TaskFake
        {
            BoolValue = _testData.boolValue,
            EnumValue = _testData.enumValue,
            IntValue = _testData.intValue,
            StringValue = _testData.stringValue,
        };

        // 2. Transform the POCO into a metadata object (ToMeta)
        var config = task.ToMeta(ScopingTestData.Scope, "testConfig");

        // 3. Re-instantiate the task from the generated metadata
        task = ScopingTestData.Scope.CreateTask(config) as TaskFake;

        BdoTaskFaker.AssertFake(task, _testData);
    }
}