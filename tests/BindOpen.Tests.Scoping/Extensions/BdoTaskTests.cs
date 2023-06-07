using BindOpen.Scoping.Data;
using BindOpen.Scoping.Data.Meta;
using BindOpen.Scoping.Extensions;
using BindOpen.Scoping.Extensions.Tasks;
using NUnit.Framework;

namespace BindOpen.Tests.Scoping.Extensions
{
    [TestFixture, Order(300)]
    public class BdoTaskTests
    {
        private readonly string _filePath = Tests.WorkingFolder + "Task.xml";

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
                BdoMeta.NewConfig()
                .WithDefinition("bindopen.tests.kernel$taskFake")
                .WithProperties(
                    BdoMeta.NewScalar("boolValue", data.boolValue as bool?),
                    BdoMeta.NewScalar("intValue", data.intValue as int?))
                .WithInputs(
                    BdoMeta.NewScalar("enumValue", data.enumValue as ActionPriorities?))
                .WithOutputs(
                    BdoMeta.NewScalar("stringValue", data.stringValue as string));

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
            var config = ScopingTests.Scope.CreateConfigFrom(task, "testConfig");
            var task2 = ScopingTests.Scope.CreateTask<TaskFake>(config);

            BdoTaskFaker.AssertFake(task2, _testData);
        }

        [Test, Order(3)]
        public void CreateTaskFromScopeTest()
        {
            IBdoConfiguration config = CreateTaskConfig(_testData);
            var task = ScopingTests.Scope.CreateTask<TaskFake>(config);

            BdoTaskFaker.AssertFake(task, _testData);
        }
    }

}
