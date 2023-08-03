using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.System.Scoping
{
    [TestFixture, Order(300)]
    public class IOEntityTests
    {
        private EntityFake _entity = null;

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoEntityFaker.Fake();
        }

        // Xml

        [Test, Order(2)]
        public void SaveXmlEntityTest()
        {
            if (_entity == null)
            {
                IBdoConfiguration config = BdoEntityTests.CreateEntityConfig(_testData);
                _entity = SystemData.Scope.CreateEntity<EntityFake>(config);
            }

            var isSaved = _entity.ToConfig(SystemData.Scope).ToDto().SaveXml(BdoEntityFaker.XmlFilePath);

            Assert.That(isSaved, "Entity saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlConfigurationTest()
        {
            if (_entity == null || !File.Exists(BdoEntityFaker.XmlFilePath))
            {
                SaveXmlEntityTest();
            }

            var config = XmlHelper.LoadXml<ConfigurationDto>(BdoEntityFaker.XmlFilePath).ToPoco();
            var entity = SystemData.Scope.CreateEntity<EntityFake>(config);

            Assert.That(entity != null, "Entity loading failed");

            BdoEntityFaker.AssertFake(entity, _testData);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonEntityTest()
        {
            if (_entity == null)
            {
                IBdoConfiguration config = BdoEntityTests.CreateEntityConfig(_testData);
                _entity = SystemData.Scope.CreateEntity<EntityFake>(config);
            }

            var isSaved = _entity.ToConfig(SystemData.Scope).ToDto().SaveJson(BdoEntityFaker.JsonFilePath);

            Assert.That(isSaved, "Entity saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonConfigurationTest()
        {
            if (_entity == null || !File.Exists(BdoEntityFaker.JsonFilePath))
            {
                SaveJsonEntityTest();
            }

            var config = JsonHelper.LoadJson<ConfigurationDto>(BdoEntityFaker.JsonFilePath).ToPoco();
            var entity = SystemData.Scope.CreateEntity<EntityFake>(config);

            Assert.That(entity != null, "Entity loading failed");

            BdoEntityFaker.AssertFake(entity, _testData);
        }
    }

}
