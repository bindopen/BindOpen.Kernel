using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.System.Scoping
{
    [TestFixture, Order(300)]
    public class IOTaskTests
    {
        private TaskFake _task = null;

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoTaskFaker.Fake();
        }

        // Xml

        [Test, Order(2)]
        public void SaveXmlTaskTest()
        {
            if (_task == null)
            {
                IBdoConfiguration config = BdoTaskTests.CreateMetaTask(_testData);
                _task = SystemData.Scope.CreateTask<TaskFake>(config);
            }

            var isSaved = _task.ToMeta(SystemData.Scope).ToDto().SaveXml(BdoTaskFaker.XmlFilePath);

            Assert.That(isSaved, "Task saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlConfigurationTest()
        {
            if (_task == null || !File.Exists(BdoTaskFaker.XmlFilePath))
            {
                SaveXmlTaskTest();
            }

            var config = XmlHelper.LoadXml<ConfigurationDto>(BdoTaskFaker.XmlFilePath).ToPoco();
            var task = SystemData.Scope.CreateTask<TaskFake>(config);

            Assert.That(task != null, "Task loading failed");

            BdoTaskFaker.AssertFake(task, _testData);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonTaskTest()
        {
            if (_task == null)
            {
                IBdoConfiguration config = BdoTaskTests.CreateMetaTask(_testData);
                _task = SystemData.Scope.CreateTask<TaskFake>(config);
            }

            var isSaved = _task.ToMeta(SystemData.Scope).ToDto().SaveJson(BdoTaskFaker.JsonFilePath);

            Assert.That(isSaved, "Task saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonConfigurationTest()
        {
            if (_task == null || !File.Exists(BdoTaskFaker.JsonFilePath))
            {
                SaveJsonTaskTest();
            }

            var config = JsonHelper.LoadJson<ConfigurationDto>(BdoTaskFaker.JsonFilePath).ToPoco();
            var task = SystemData.Scope.CreateTask<TaskFake>(config);

            Assert.That(task != null, "Task loading failed");

            BdoTaskFaker.AssertFake(task, _testData);
        }
    }

}
