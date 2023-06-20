using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping.Tasks
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

        public static IBdoTask CreateTask(dynamic data)
        {
            var task = new TaskFake
            {
                BoolValue = data.boolValue,
                EnumValue = data.enumValue,
                IntValue = data.intValue,
                StringValue = data.stringValue,
            };

            return task;
        }

        [Test, Order(1)]
        public void CreateTaskToConfig()
        {
            IBdoTask task = CreateTask(_testData);
            var config = SystemData.Scope.CreateConfigFrom(task, "testConfig");
            var task2 = SystemData.Scope.CreateTask<TaskFake>(config);

            BdoTaskFaker.AssertFake(task2, _testData);
        }

        [Test, Order(3)]
        public void CreateTaskFromScopeTest()
        {
            IBdoConfiguration config = CreateTaskConfig(_testData);
            var task = SystemData.Scope.CreateTask<TaskFake>(config);

            BdoTaskFaker.AssertFake(task, _testData);
        }
    }

}
