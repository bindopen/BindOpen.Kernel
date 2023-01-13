using Bogus;
using NUnit.Framework;

namespace BindOpen.Tests.Extensions
{
    public static class BdoConnectorFaker
    {
        public static readonly string XmlFilePath = GlobalVariables.WorkingFolder + "Connector.xml";
        public static readonly string JsonFilePath = GlobalVariables.WorkingFolder + "Connector.json";

        public static dynamic Fake()
        {
            var f = new Faker();
            return new
            {
                host = f.Internet.IpAddress().ToString(),
                port = f.Random.Int(800),
                isSslEnabled = f.Random.Bool()
            };
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
