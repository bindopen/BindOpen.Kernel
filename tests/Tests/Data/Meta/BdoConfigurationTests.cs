using BindOpen.Data.Meta;
using Bogus;
using DeepEqual.Syntax;
using NUnit.Framework;
using System.Linq;

namespace BindOpen.Data.Meta
{
    [TestFixture, Order(100)]
    public class BdoConfigurationTests
    {
        private readonly string _configName10 = "Child1";
        private readonly string _configName10a = "Child1a";
        private readonly string _configName20 = "Child2";

        private IBdoMetaSet _config10 = null;
        private IBdoConfiguration _config10a = null;

        private IBdoConfiguration _config20 = null;
        private IBdoConfiguration _config20a = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var f = new Faker();

            _config10a = BdoData.NewConfig(
                _configName10a,
                BdoData.NewScalar("float1", DataValueTypes.Number, f.Random.Float()))
                .Using(_configName10, _configName20);

            _config10 = BdoData.NewConfig(_configName10)
                .WithTitle("myConfiguration")
                .WithDescription(("en", "Sample of description"))
                .WithChildren(_config10a)
                .With(
                    BdoData.NewScalar("float1", DataValueTypes.Number, f.Random.Float()),
                    BdoData.NewScalar("text1", f.Lorem.Words(10)),
                    BdoData.NewScalar("integer1", DataValueTypes.Integer, Enumerable.Range(0, 10).Select(p => f.Random.Int(5000))),
                    BdoData.NewScalar("byteArray1", DataValueTypes.Binary, Enumerable.Range(1, 2).Select(p => f.Random.Bytes(5000)).ToArray()),
                    BdoData.NewNode(
                        "$node1",
                        BdoData.NewScalar("textB1", DataValueTypes.Text, f.Lorem.Words(10)),
                        BdoData.NewScalar("textB2", DataValueTypes.Integer, f.Random.Int(5000)))
                );

            _config20a = BdoData.NewConfig(
                _configName10a,
                BdoData.NewScalar("float1", DataValueTypes.Number, f.Random.Float()),
                BdoData.NewScalar("text1", f.Lorem.Words(10)))
                .Using(_configName10, _configName20);

            _config20 = BdoData.NewConfig(
                _configName20,
                BdoData.NewScalar("float2", DataValueTypes.Number, 1.1, 1.2, 1.3),
                BdoData.NewScalar("float1", DataValueTypes.Number, f.Random.Float()),
                BdoData.NewNode(
                    "$node1",
                    BdoData.NewScalar("textB1", DataValueTypes.Text, f.Lorem.Words(10)),
                    BdoData.NewScalar("coolB1", DataValueTypes.Boolean, f.Random.Bool())))
                .WithChildren(_config20a);
        }

        [Test, Order(1)]
        public void GetConfigurationItem1()
        {
            var text = _config10.GetData<string>("text1");

            Assert.That(!string.IsNullOrEmpty(text), "Error with config");
        }

        [Test, Order(2)]
        public void DescendantTest()
        {
            // Null

            var meta = _config10.Descendant<IBdoMetaData>();

            Assert.That(meta == null, "Error with config");

            // String

            meta = _config10.Descendant<IBdoMetaData>("^" + _configName10a, "float1");

            Assert.That(meta?.GetData() == _config10a["float1"]?.GetData(), "Error with config");

            // Indexed

            meta = _config10.Descendant<IBdoMetaData>("^:0", "float1");

            Assert.That(meta?.GetData() == _config10a["float1"]?.GetData(), "Error with config");

            // Not existing

            var subConfig = _config10.Descendant<IBdoConfiguration>("^" + _configName10a + "x", "float1");

            Assert.That(subConfig == null, "Error with config");
        }

        [Test, Order(3)]
        public void CloneTest()
        {
            var meta = _config10.Clone<IBdoConfiguration>();

            meta?.WithDeepEqual(_config10)
               .Assert();
        }

        [Test, Order(4)]
        public void UpdateTest()
        {
            var meta = _config10.Clone<IBdoConfiguration>();
            meta.Update(_config20);

            Assert.That(meta.Descendant<IBdoMetaScalar>("float1")?.GetData() == _config20["float1"]?.GetData(), "Error with config");
            Assert.That(meta.Descendant<IBdoMetaNode>("$node1")?.Count == 3, "Error with config");
            Assert.That(meta.Descendant<IBdoConfiguration>("^" + _configName10a)?.Count == 2, "Error with config");
            Assert.That(meta.Descendant<IBdoMetaScalar>("^" + _configName10a, "float1")?.GetData() == _config20a["float1"]?.GetData(), "Error with config");
        }
    }
}
