using BindOpen.System.Data.Meta;
using BindOpen.System.Data;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping
{
    [TestFixture, Order(300)]
    public class BdoTaskTests
    {
        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoTaskFaker.Fake();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="data"></param>
        /// <returns></returns>
        public static IBdoMetaObject CreateMetaTask(dynamic data)
        {
            var meta = BdoData.NewObject()
                .WithDataType(BdoExtensionKinds.Task, "bindopen.system.tests$taskFake")
                .WithProperties(
                    ("boolValue", data.boolValue as bool?),
                    ("intValue", data.intValue as int?))
                .WithInputs(
                    ("enumValue", data.enumValue as ActionPriorities?))
                .WithOutputs(
                    ("stringValue", data.stringValue as string));

            return meta;
        }

        [Test, Order(1)]
        public void CreateTaskTest_FromMetaSet()
        {
            IBdoMetaObject meta = CreateMetaTask(_testData);
            var connector = SystemData.Scope.CreateTask<TaskFake>(meta);

            BdoTaskFaker.AssertFake(connector, _testData);
        }

        [Test, Order(2)]
        public void CreateTaskTest_FromConfig()
        {
            IBdoMetaObject meta = CreateMetaTask(_testData);
            var connector = SystemData.Scope.CreateTask(meta) as TaskFake;

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

            var config = connector.ToMeta(SystemData.Scope, "testConfig");
            connector = SystemData.Scope.CreateTask(config) as TaskFake;

            BdoTaskFaker.AssertFake(connector, _testData);
        }
    }

}
