using BindOpen.Data;
using BindOpen.Data.Configuration;
using BindOpen.Data.Helpers;
using Bogus;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Tests.Data
{
    [TestFixture, Order(100)]
    public class BdoConfigurationTests
    {
        private readonly string _configName1 = "Main";
        private readonly string _configName20 = "Child1";
        private readonly string _configName21 = "Child2";

        private IBdoConfiguration _config1 = null;
        private IBdoConfiguration _config20 = null;
        private IBdoConfiguration _config21 = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();

            _config1 = BdoConfig.New(
                _configName1,
                BdoMeta.NewScalar("float1", DataValueTypes.Number, f.Random.Float()))
                .Using(_configName20, _configName21);

            _config20 = BdoConfig.New(
                _configName20,
                BdoMeta.NewScalar("text1", DataValueTypes.Text, f.Lorem.Words(10)),
                BdoMeta.NewScalar("integer1", DataValueTypes.Integer, Enumerable.Range(0, 10).Select(p => f.Random.Int(5000))),
                BdoMeta.NewScalar("byteArray1", DataValueTypes.ByteArray, Enumerable.Range(1, 2).Select(p => f.Random.Bytes(5000)).ToArray()));

            _config21 = BdoConfig.New(
                _configName21,
                BdoMeta.NewScalar("float2", DataValueTypes.Number, 1.1, 1.2, 1.3));
        }

        [Test, Order(1)]
        public void CreateConfigurationSet()
        {
            var bundle = BdoConfig.NewSet(
                _config1,
                _config20,
                _config21);

            //bundle.Update(_config1);
        }

        [Test, Order(2)]
        public void GetConfigurationItem1()
        {
            var text = _config20.GetData<string>("text1");

            Assert.That(!string.IsNullOrEmpty(text), "Error with config");
        }
    }
}
