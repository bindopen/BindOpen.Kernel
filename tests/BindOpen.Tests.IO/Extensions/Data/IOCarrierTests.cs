using BindOpen.Extensions;
using BindOpen.Extensions.Modeling;
using BindOpen.Data;
using BindOpen.Data.Elements;
using BindOpen.Runtime.Tests;
using BindOpen.Runtime.Tests.Extensions;
using BindOpen.Runtime.Tests.Extensions.Data;
using Bogus;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Runtime.IO.Tests.Extensions.Data
{
    [TestFixture, Order(300)]
    public class IOCarrierTests
    {
        private CarrierFake _carrier = null;

        private readonly string _filePath = GlobalVariables.WorkingFolder + "Carrier.xml";

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

        private void Test(CarrierFake field)
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
        public void SaveXmlCarrierTest()
        {
            if (_carrier == null)
            {
                _carrier = BdoCarrierTests.CreateCarrier(_testData);
            }

            var isSaved = _carrier.ToDto().SaveXml(_filePath);

            Assert.That(isSaved, "Carrier saving failed");
        }

        [Test, Order(3)]
        public void LoadXmlConfigurationTest()
        {
            if (_carrier == null || !File.Exists(_filePath))
            {
                SaveXmlCarrierTest();
            }

            var configuration = XmlHelper.LoadXml<BdoCarrierConfigurationDto>(_filePath).ToPoco();
            var field = BdoExtensions.NewCarrier<CarrierFake>(configuration);

            Assert.That(field != null, "Carrier loading failed");

            Test(field);
        }

        // Json

        [Test, Order(4)]
        public void SaveJsonCarrierTest()
        {
            if (_carrier == null)
            {
                _carrier = BdoCarrierTests.CreateCarrier(_testData);
            }

            var isSaved = _carrier.ToDto().SaveJson(_filePath);

            Assert.That(isSaved, "Carrier saving failed");
        }

        [Test, Order(5)]
        public void LoadJsonConfigurationTest()
        {
            if (_carrier == null || !File.Exists(_filePath))
            {
                SaveJsonCarrierTest();
            }

            var configuration = JsonHelper.LoadJson<BdoCarrierConfigurationDto>(_filePath).ToPoco();
            var field = BdoExtensions.NewCarrier<CarrierFake>(configuration);

            Assert.That(field != null, "Carrier loading failed");

            Test(field);
        }
    }

}
