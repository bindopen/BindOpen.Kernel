using BindOpen.Data.Meta;
using BindOpen.Data;
using BindOpen.Scoping;
using Bogus;
using NUnit.Framework;
using System.Dynamic;

namespace BindOpen.Kernel.Tests
{
    public static class BdoConnectorFaker
    {
        public static readonly string XmlFilePath = SystemData.WorkingFolder + "Connector.xml";
        public static readonly string JsonFilePath = SystemData.WorkingFolder + "Connector.json";

        public static dynamic NewData()
        {
            var f = new Faker();
            dynamic b = new ExpandoObject();
            b.connecString = f.Random.Word();
            b.host = f.Internet.IpAddress().ToString();
            b.port = f.Random.Int(800);
            b.isSslEnabled = f.Random.Bool();
            return b;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="data"></param>
        /// <returns></returns>
        public static IBdoMetaObject NewMetaObject(dynamic data = null)
        {
            data ??= NewData();

            var config =
                BdoData.NewObject()
                .WithDataType(BdoExtensionKinds.Connector, "bindopen.kernel.tests$testConnector")
                .With(
                    BdoData.NewScalar("host", data.host as string),
                    BdoData.NewScalar("port", data.port as int?),
                    BdoData.NewScalar("isSslEnabled", data.isSslEnabled as bool?));

            return config;
        }

        public static void AssertFake(ConnectorFake connector, dynamic reference)
        {
            Assert.That(connector != null, "Connector missing");

            Assert.That(connector.Host == reference.host, "Bad connector");
            Assert.That(connector.Port?.GetData<int?>() == reference.port, "Bad connector");
            Assert.That((connector.IsSslEnabled ?? false) == reference.isSslEnabled, "Bad connector");
        }
    }
}
