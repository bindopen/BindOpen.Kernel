using BindOpen.Abstractions.Meta.Configuration;
using BindOpen.Meta;
using BindOpen.Meta.Elements;
using Bogus;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Runtime.Tests.MetaData.Configuration
{
    [TestFixture, Order(100)]
    public class ConfigurationTests
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

            _config1 = BdoMeta.NewConfiguration(
                _configName1,
                BdoMeta.NewScalar("float1", DataValueTypes.Number, 10))
                .Using(_configName20, _configName21);

            _config20 = BdoMeta.NewConfiguration(
                _configName20,
                BdoMeta.NewScalar("text1", DataValueTypes.Text, f.Lorem.Words(10)),
                BdoMeta.NewScalar("integer1", DataValueTypes.Integer, Enumerable.Range(0, 10).Select(p => f.Random.Int(5000))),
                BdoMeta.NewScalar("byteArray1", DataValueTypes.ByteArray, Enumerable.Range(0, 100).Select(p => f.PickRandom<byte>())));

            _config21 = BdoMeta.NewConfiguration(
                _configName21,
                BdoMeta.NewScalar("float2", DataValueTypes.Number, 1.1, 1.2, 1.3));
        }

        [Test, Order(1)]
        public void CreateConfigurationBundle()
        {
            var bundle = BdoMeta.NewConfigurationBundle(
                _config1,
                _config20,
                _config21);

            bundle.Update(_config1);
        }
    }
}
