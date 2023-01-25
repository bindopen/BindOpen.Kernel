using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions;
using BindOpen.Extensions.Modeling;
using BindOpen.Tests.Extensions;
using Bogus;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.IO.Extensions
{
    [TestFixture, Order(300)]
    public class IOEntityTests
    {
        private EntityFake _carrier = null;

        private readonly string _filePath = GlobalVariables.WorkingFolder + "Entity.xml";

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

        private void Test(EntityFake field)
        {
            Assert.That(field != null, "Field missing");
            if (field != null)
            {
                Assert.That(field.BoolValue == _testData.boolValue, "Bad carrier - Boolean value");
                Assert.That(field.EnumValue.ToString() == _testData.enumValue.ToString(), "Bad carrier - Enumeration value");
                Assert.That(field.IntValue == _testData.intValue, "Bad carrier - Integer value");
                Assert.That(field.StringValue == _testData.stringValue, "Bad carrier - String value");
            }
        }

        // Xml

        [Test, Order(2)]
        public void SaveXmlEntityTest()
        {
            if (_carrier == null)
            {
                _carrier = BdoEntityTests.CreateEntity(_testData);
            }

            var isSaved = _carrier.ToDto().SaveXml(_filePath);

            Assert.That(isSaved, "Entity saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlConfigurationTest()
        {
            if (_carrier == null || !File.Exists(_filePath))
            {
                SaveXmlEntityTest();
            }

            var configuration = XmlHelper.LoadXml<BdoEntityConfigurationDto>(_filePath).ToPoco();
            var field = BdoExt.NewEntity<EntityFake>(configuration);

            Assert.That(field != null, "Entity loading failed");

            Test(field);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonEntityTest()
        {
            if (_carrier == null)
            {
                _carrier = BdoEntityTests.CreateEntity(_testData);
            }

            var isSaved = _carrier.ToDto().SaveJson(_filePath);

            Assert.That(isSaved, "Entity saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonConfigurationTest()
        {
            if (_carrier == null || !File.Exists(_filePath))
            {
                SaveJsonEntityTest();
            }

            var configuration = JsonHelper.LoadJson<BdoEntityConfigurationDto>(_filePath).ToPoco();
            var field = BdoExt.NewEntity<EntityFake>(configuration);

            Assert.That(field != null, "Entity loading failed");

            Test(field);
        }
    }

}
