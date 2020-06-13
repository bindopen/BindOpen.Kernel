using BindOpen.Application.Scopes;
using BindOpen.Data.Common;
using BindOpen.Data.Elements;
using BindOpen.Data.Helpers.Serialization;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Diagnostics;
using BindOpen.Tests.Core.Fakers;
using Bogus;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Tests.Core.Extensions.Carriers
{
    [TestFixture, Order(300)]
    public class CarrierTests
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

        [Test, Order(1)]
        public void CreateCarrierNewObjectTest()
        {
            _carrier = new CarrierFake
            {
                Name = "test",
                BoolValue = _testData.boolValue,
                EnumValue = _testData.enumValue,
                IntValue = _testData.intValue,
                StringValue = _testData.stringValue,
            };

            Test(_carrier);
        }

        [Test, Order(1)]
        public void CreateCarrierFromScopeTest()
        {
            IBdoCarrierConfiguration config =
                GlobalVariables.Scope.CreateCarrierConfiguration("tests.core$testCarrier")
                .WithItems(
                    ElementFactory.CreateScalar("boolValue", _testData.boolValue),
                    ElementFactory.CreateScalar("enumValue", _testData.enumValue),
                    ElementFactory.CreateScalar("intValue", _testData.intValue),
                    ElementFactory.CreateScalar("stringValue", _testData.stringValue));

            _carrier = GlobalVariables.Scope.CreateCarrier<CarrierFake>(config, "connector");

            Test(_carrier);
        }

        [Test, Order(2)]
        public void SaveCarrierTest()
        {
            if (_carrier == null)
            {
                CreateCarrierNewObjectTest();
            }

            var log = new BdoLog();
            _carrier.SaveXml(_filePath, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Carrier saving failed. Result was '" + xml);
        }

        [Test, Order(3)]
        public void LoadConfigurationTest()
        {
            if (_carrier == null || !File.Exists(_filePath))
            {
                SaveCarrierTest();
            }

            var log = new BdoLog();
            BdoCarrierConfiguration configuration = XmlHelper.Load<BdoCarrierConfiguration>(_filePath, log: log);
            var field = GlobalVariables.Scope.CreateCarrier<CarrierFake>(configuration, null, log);

            string xml = "";
            if (log.HasErrorsOrExceptions())
            {
                xml = ". Result was '" + log.ToXml() + "'";
            }
            Assert.That(!log.HasErrorsOrExceptions(), "Carrier loading failed" + xml);

            Test(field);
        }
    }

}
