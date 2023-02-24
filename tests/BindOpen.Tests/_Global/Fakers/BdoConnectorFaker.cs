using Bogus;
using NUnit.Framework;
using System.Dynamic;

namespace BindOpen.Tests
{
    public static class BdoConnectorFaker
    {
        public static readonly string XmlFilePath = Tests.WorkingFolder + "Connector.xml";
        public static readonly string JsonFilePath = Tests.WorkingFolder + "Connector.json";

        public static dynamic Fake()
        {
            var f = new Faker();
            dynamic b = new ExpandoObject();
            b.host = f.Internet.IpAddress().ToString();
            b.port = f.Random.Int(800);
            b.isSslEnabled = f.Random.Bool();
            return b;
        }

        public static void AssertFake(ConnectorFake connector, dynamic reference)
        {
            Assert.That(connector != null, "Connector missing");

            Assert.That(connector.Host == reference.host, "Bad connector");
            Assert.That(connector.Port == reference.port, "Bad connector");
            Assert.That(connector.IsSslEnabled == reference.isSslEnabled, "Bad connector");
        }
    }
}
