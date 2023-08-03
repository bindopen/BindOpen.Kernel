using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping
{
    [TestFixture, Order(300)]
    public class BdoTaskTests
    {
        private readonly string _filePath = SystemData.WorkingFolder + "Task.xml";

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
        public static IBdoConfiguration CreateTaskConfig(dynamic data)
        {
            var config =
                BdoData.NewConfig()
                .WithDefinition("bindopen.system.tests$taskFake")
                .WithProperties(
                    BdoData.NewMetaScalar("boolValue", data.boolValue as bool?),
                    BdoData.NewMetaScalar("intValue", data.intValue as int?))
                .WithInputs(
                    BdoData.NewMetaScalar("enumValue", data.enumValue as ActionPriorities?))
                .WithOutputs(
                    BdoData.NewMetaScalar("stringValue", data.stringValue as string));

            return config;
        }

        [Test, Order(1)]
        public void CreateTaskTest_FromMetaSet()
        {
            IBdoConfiguration config = CreateTaskConfig(_testData);
            var connector = SystemData.Scope.CreateTask<TaskFake>(config);

            BdoTaskFaker.AssertFake(connector, _testData);
        }

        [Test, Order(2)]
        public void CreateTaskTest_FromConfig()
        {
            IBdoConfiguration config = CreateTaskConfig(_testData);
            var connector = SystemData.Scope.CreateTask(config) as TaskFake;

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

            var config = connector.ToConfig(SystemData.Scope, "testConfig");
            connector = SystemData.Scope.CreateTask(config) as TaskFake;

            BdoTaskFaker.AssertFake(connector, _testData);
        }
    }

}
