using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.IO;
using BindOpen.Kernel.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Kernel.Scoping.Entities
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
        public void SaveXmlTest()
        {
            if (_entity == null)
            {
                IBdoMetaObject meta = BdoEntityTests.CreateMetaObject(_testData);
                _entity = SystemData.Scope.CreateEntity<EntityFake>(meta);
            }

            var isSaved = _entity.ToMeta(SystemData.Scope).ToDto().SaveXml(BdoEntityFaker.XmlFilePath);

            Assert.That(isSaved, "Entity saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlTest()
        {
            if (_entity == null || !File.Exists(BdoEntityFaker.XmlFilePath))
            {
                SaveXmlTest();
            }

            var meta = XmlHelper.LoadXml<MetaObjectDto>(BdoEntityFaker.XmlFilePath).ToPoco();
            var entity = SystemData.Scope.CreateEntity(meta) as EntityFake;

            Assert.That(entity != null, "Entity loading failed");

            BdoEntityFaker.AssertFake(entity, _testData);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonTest()
        {
            if (_entity == null)
            {
                IBdoMetaObject meta = BdoEntityTests.CreateMetaObject(_testData);
                _entity = SystemData.Scope.CreateEntity<EntityFake>(meta);
            }

            var isSaved = _entity.ToMeta(SystemData.Scope).ToDto().SaveJson(BdoEntityFaker.JsonFilePath);

            Assert.That(isSaved, "Entity saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonTest()
        {
            if (_entity == null || !File.Exists(BdoEntityFaker.JsonFilePath))
            {
                SaveJsonTest();
            }

            var meta = JsonHelper.LoadJson<MetaObjectDto>(BdoEntityFaker.JsonFilePath).ToPoco();
            var entity = SystemData.Scope.CreateEntity(meta) as EntityFake;

            Assert.That(entity != null, "Entity loading failed");

            BdoEntityFaker.AssertFake(entity, _testData);
        }
    }

}
