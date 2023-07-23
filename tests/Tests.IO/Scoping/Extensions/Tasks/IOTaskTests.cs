using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using BindOpen.System.Tests;
using Bogus;
using NUnit.Framework;
using System.IO;

namespace BindOpen.System.Scoping.Tasks
{
    [TestFixture, Order(300)]
    public class IOTaskTests
    {
        private TaskFake _task = null;

        private readonly string _filePath = SystemData.WorkingFolder + "Task.xml";

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();
            _testData = new
            {
                boolValue = f.Random.Bool(),
                intValue = f.Random.Int(800),
                enumValue = ActionPriorities.High,
                stringValue = f.Lorem.Word()
            };
        }

        private void Test(TaskFake task)
        {
            Assert.That(task != null, "Field missing");
            if (task != null)
            {
                Assert.That(task.BoolValue == _testData.boolValue, "Bad carrier - Boolean value");
                Assert.That(task.EnumValue.ToString() == _testData.enumValue.ToString(), "Bad carrier - Enumeration value");
                Assert.That(task.IntValue == _testData.intValue, "Bad carrier - Integer value");
                Assert.That(task.StringValue == _testData.stringValue, "Bad carrier - String value");
            }
        }

        // Xml

        [Test, Order(2)]
        public void SaveXmlTaskTest()
        {
            _task ??= BdoTaskTests.CreateTask(_testData);

            var isSaved = _task.ToMetaData().ToDto().SaveXml(_filePath);

            Assert.That(isSaved, "Task saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlConfigurationTest()
        {
            if (_task == null || !File.Exists(_filePath))
            {
                SaveXmlTaskTest();
            }

            var config = XmlHelper.LoadXml<ConfigurationDto>(_filePath).ToPoco();
            var task = SystemData.Scope.CreateTask<TaskFake>(config);

            Assert.That(task != null, "Task loading failed");

            Test(task);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonTaskTest()
        {
            _task ??= BdoTaskTests.CreateTask(_testData);

            var isSaved = _task.ToMetaData().ToDto().SaveJson(_filePath);

            Assert.That(isSaved, "Task saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonConfigurationTest()
        {
            if (_task == null || !File.Exists(_filePath))
            {
                SaveJsonTaskTest();
            }

            var config = JsonHelper.LoadJson<ConfigurationDto>(_filePath).ToPoco();
            var task = SystemData.Scope.CreateTask<TaskFake>(config);

            Assert.That(task != null, "Task loading failed");

            Test(task);
        }
    }

}
