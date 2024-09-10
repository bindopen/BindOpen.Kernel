using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Scoping.Tasks
{
    [TestFixture, Order(300)]
    public class IOTaskTests
    {
        private TaskFake _task = null;

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoTaskFaker.NewData();
        }

        // Xml

        [Test, Order(2)]
        public void SaveXmlTest()
        {
            if (_task == null)
            {
                IBdoMetaObject meta = BdoTaskFaker.NewMetaObject(_testData);
                _task = GlobalTestData.Scope.CreateTask<TaskFake>(meta);
            }

            var isSaved = _task.ToMeta(GlobalTestData.Scope).ToDto().SaveXml(BdoTaskFaker.XmlFilePath);

            Assert.That(isSaved, "Task saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlTest()
        {
            if (_task == null || !File.Exists(BdoTaskFaker.XmlFilePath))
            {
                SaveXmlTest();
            }

            var meta = XmlHelper.LoadXml<MetaObjectDto>(BdoTaskFaker.XmlFilePath).ToPoco();
            var task = GlobalTestData.Scope.CreateTask<TaskFake>(meta);

            Assert.That(task != null, "Task loading failed");

            BdoTaskFaker.AssertFake(task, _testData);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonTest()
        {
            if (_task == null)
            {
                IBdoMetaObject meta = BdoTaskFaker.NewMetaObject(_testData);
                _task = GlobalTestData.Scope.CreateTask<TaskFake>(meta);
            }

            var isSaved = _task.ToMeta(GlobalTestData.Scope).ToDto().SaveJson(BdoTaskFaker.JsonFilePath);

            Assert.That(isSaved, "Task saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonTest()
        {
            if (_task == null || !File.Exists(BdoTaskFaker.JsonFilePath))
            {
                SaveJsonTest();
            }

            var meta = JsonHelper.LoadJson<MetaObjectDto>(BdoTaskFaker.JsonFilePath).ToPoco();
            var task = GlobalTestData.Scope.CreateTask<TaskFake>(meta);

            Assert.That(task != null, "Task loading failed");

            BdoTaskFaker.AssertFake(task, _testData);
        }
    }
}
