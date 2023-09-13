using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using Bogus;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Kernel.Data
{
    [TestFixture, Order(100)]
    public class BdoConfigurationTests
    {
        private readonly string _configName1 = "Main";
        private readonly string _configName20 = "Child1";
        private readonly string _configName21 = "Child2";

        private IBdoConfiguration _config1 = null;
        private IBdoMetaSet _config20 = null;
        private IBdoConfiguration _config21 = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();

            _config1 = BdoData.NewConfig(
                _configName1,
                BdoData.NewScalar("float1", DataValueTypes.Number, f.Random.Float()))
                .Using(_configName20, _configName21);

            _config20 = BdoData.NewConfig(_configName20)
                .WithTitle("myConfiguration")
                .WithDescription(("en", "Sample of description"))
                .WithChildren(_config1)
                .With(
                    BdoData.NewScalar("text1", f.Lorem.Words(10)),
                    BdoData.NewScalar("integer1", DataValueTypes.Integer, Enumerable.Range(0, 10).Select(p => f.Random.Int(5000))),
                    BdoData.NewScalar("byteArray1", DataValueTypes.Binary, Enumerable.Range(1, 2).Select(p => f.Random.Bytes(5000)).ToArray()),
                    BdoData.NewNode(
                        BdoData.NewScalar("textB1", DataValueTypes.Text, f.Lorem.Words(10)),
                        BdoData.NewScalar("textB2", DataValueTypes.Integer, f.Random.Int(5000)))
                );

            _config21 = BdoData.NewConfig(
                _configName21,
                BdoData.NewScalar("float2", DataValueTypes.Number, 1.1, 1.2, 1.3));
        }

        [Test, Order(1)]
        public void GetConfigurationItem1()
        {
            var text = _config20.GetData<string>("text1");

            Assert.That(!string.IsNullOrEmpty(text), "Error with config");
        }

        [Test, Order(1)]
        public void DescendantTest()
        {
            // Null

            var meta = _config20.Descendant<IBdoMetaData>();

            Assert.That(meta == null, "Error with config");

            // String

            meta = _config20.Descendant<IBdoMetaData>("^" + _configName1, "float1");

            Assert.That(meta?.GetData() == _config1["float1"]?.GetData(), "Error with config");

            // Indexed

            meta = _config20.Descendant<IBdoMetaData>("^:0", "float1");

            Assert.That(meta?.GetData() == _config1["float1"]?.GetData(), "Error with config");

            // Not existing

            var subConfig = _config20.Descendant<IBdoConfiguration>("^" + _configName1 + "x", "float1");

            Assert.That(subConfig == null, "Error with config");
        }
    }
}
