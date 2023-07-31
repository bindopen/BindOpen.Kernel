using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Data.Meta.Reflection;
using BindOpen.System.Tests;
using NUnit.Framework;
using System.IO;

namespace BindOpen.System.Scoping
{
    [TestFixture, Order(300)]
    public class IOEntityTests
    {
        private EntityFake _entity = null;

        private readonly string _filePath = SystemData.WorkingFolder + "Entity.xml";

        private dynamic _testData;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _testData = BdoEntityFaker.Fake();
        }

        private void Test(EntityFake entity)
        {
            Assert.That(entity != null, "Field missing");
            if (entity != null)
            {
                Assert.That(entity.BoolValue == _testData.boolValue, "Bad carrier - Boolean value");
                Assert.That(entity.EnumValue.ToString() == _testData.enumValue.ToString(), "Bad carrier - Enumeration value");
                Assert.That(entity.IntValue == _testData.intValue, "Bad carrier - Integer value");
                Assert.That(entity.StringValue == _testData.stringValue, "Bad carrier - String value");
            }
        }

        // Xml

        [Test, Order(2)]
        public void SaveXmlEntityTest()
        {
            if (_entity == null)
            {
                _entity = BdoEntityTests.CreateEntity(_testData);
            }

            var isSaved = _entity.ToConfig().ToDto().SaveXml(_filePath);

            Assert.That(isSaved, "Entity saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlConfigurationTest()
        {
            if (_entity == null || !File.Exists(_filePath))
            {
                SaveXmlEntityTest();
            }

            var config = XmlHelper.LoadXml<ConfigurationDto>(_filePath).ToPoco();
            var entity = SystemData.Scope.CreateEntity<EntityFake>(config);

            Assert.That(entity != null, "Entity loading failed");

            Test(entity);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonEntityTest()
        {
            if (_entity == null)
            {
                _entity = BdoEntityTests.CreateEntity(_testData);
            }

            var isSaved = _entity.ToConfig().ToDto().SaveJson(_filePath);

            Assert.That(isSaved, "Entity saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonConfigurationTest()
        {
            if (_entity == null || !File.Exists(_filePath))
            {
                SaveJsonEntityTest();
            }

            var config = JsonHelper.LoadJson<ConfigurationDto>(_filePath).ToPoco();
            var entity = SystemData.Scope.CreateEntity<EntityFake>(config);

            Assert.That(entity != null, "Entity loading failed");

            Test(entity);
        }
    }

}
