using BindOpen.Data.Meta;
using BindOpen.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping.Tasks;

[TestFixture, Order(300)]
public class BdoTaskTests
{
    private dynamic _testData;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _testData = BdoTaskFaker.NewData();
    }

    [Test, Order(1)]
    public void CreateTaskTest_FromMetaSet()
    {
        IBdoMetaObject meta = BdoTaskFaker.NewMetaObject(_testData);
        var connector = ScopingTestData.Scope.CreateTask<TaskFake>(meta);

        BdoTaskFaker.AssertFake(connector, _testData);
    }

    [Test, Order(2)]
    public void CreateTaskTest_FromConfig()
    {
        IBdoMetaObject meta = BdoTaskFaker.NewMetaObject(_testData);
        var connector = ScopingTestData.Scope.CreateTask(meta) as TaskFake;

        BdoTaskFaker.AssertFake(connector, _testData);
    }

    [Test, Order(3)]
    public void CreateTaskTest_FromObject()
    {
        var connector = new TaskFake
        {
            BoolValue = _testData.boolValue,
            EnumValue = _testData.enumValue,
            IntValue = _testData.intValue,
            StringValue = _testData.stringValue,
        };

        var config = connector.ToMeta(ScopingTestData.Scope, "testConfig");
        connector = ScopingTestData.Scope.CreateTask(config) as TaskFake;

        BdoTaskFaker.AssertFake(connector, _testData);
    }
}
