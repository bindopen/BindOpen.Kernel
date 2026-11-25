using BindOpen.Data.Meta;
using BindOpen.Scoping.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Tasks;

/// <summary>
/// Contains unit tests for <see cref="IBdoTask"/> implementations, verifying their lifecycle,
/// metadata-driven instantiation, and bidirectional state synchronization within a scope.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="IBdoScope.CreateTask"/> properly hydrates strongly-typed task
/// instances from <see cref="IBdoMetaObject"/> configurations and supports round-trip serialization
/// from plain CLR task objects via <c>ToMeta</c>.
/// </remarks>
[TestFixture]
public class BdoTaskTests
{
    private dynamic _testData;

    /// <summary>
    /// Initializes randomized task property values using <see cref="BdoTaskFaker"/>
    /// before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoTaskFaker.NewData();
    }

    /// <summary>
    /// Verifies that a strongly-typed <see cref="TaskFake"/> can be instantiated directly from an
    /// <see cref="IBdoMetaObject"/> using <see cref="IBdoScope.CreateTask{T}"/>.
    /// </summary>
    [Test]
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
    /// Verifies that a task instantiated via the non-generic <see cref="IBdoScope.CreateTask"/>
    /// method resolves to <see cref="TaskFake"/> with all configuration properties populated.
    /// </summary>
    [Test]
    public void CreateTaskTest_FromConfig()
    {
        IBdoMetaObject meta = BdoTaskFaker.NewMetaObject(_testData);

        // Resolve using the non-generic CreateTask method
        var task = ScopingTestData.Scope.CreateTask(meta) as TaskFake;

        BdoTaskFaker.AssertFake(task, _testData);
    }

    /// <summary>
    /// Verifies that a populated <see cref="TaskFake"/> CLR instance can be serialized to metadata
    /// via <c>ToMeta</c> and re-instantiated through <see cref="IBdoScope.CreateTask"/> without data loss.
    /// </summary>
    [Test]
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