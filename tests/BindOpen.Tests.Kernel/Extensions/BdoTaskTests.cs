using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Tasks;
using NUnit.Framework;

namespace BindOpen.Tests.Extensions
{
    [TestFixture, Order(300)]
    public class BdoTaskTests
    {
        private TaskFake _task = null;

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
        public static IBdoTask CreateTask(dynamic data)
        {
            BdoConfig.New()
                .With(BdoMeta.NewScalar());

            var config =
                BdoConfig.New()
                .WithDefinition("bindopen.tests.kernel$taskFake")
                .With(
                    BdoMeta.NewScalar("boolValue", data.boolValue as bool?),
                    BdoMeta.NewScalar("enumValue", data.enumValue as ActionPriorities?)
                        .AsInput(),
                    BdoMeta.NewScalar("intValue", data.intValue as int?),
                    BdoMeta.NewScalar("stringValue", data.stringValue as string)
                        .AsOutput()
                );

            return ScopingTests.Scope.CreateTask<TaskFake>(config);
        }

        [Test, Order(1)]
        public void CreateTaskNewObjectTest()
        {
            _task = new TaskFake
            {
                BoolValue = _testData.boolValue,
                EnumValue = _testData.enumValue,
                IntValue = _testData.intValue,
                StringValue = _testData.stringValue,
            };

            BdoTaskFaker.AssertFake(_task, _testData);
        }


        [Test, Order(2)]
        public void CreateTaskFromScopeTest()
        {
            _task = CreateTask(_testData);

            BdoTaskFaker.AssertFake(_task, _testData);
        }
    }

}
