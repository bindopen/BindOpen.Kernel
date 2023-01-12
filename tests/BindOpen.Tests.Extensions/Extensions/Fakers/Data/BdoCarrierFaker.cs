using BindOpen.Meta;
using Bogus;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace BindOpen.Runtime.Tests.Extensions.Data
{
    public static class BdoCarrierFaker
    {
        public static readonly string XmlFilePath = GlobalVariables.WorkingFolder + "Carrier.xml";
        public static readonly string JsonFilePath = GlobalVariables.WorkingFolder + "Carrier.json";

        public static dynamic Fake()
        {
            var f = new Faker();
            return new
            {
                boolValue = f.Random.Bool(),
                intValue = f.Random.Int(800),
                enumValue = ActionPriorities.High,
                stringValue = f.Lorem.Word()
            };
        }

        public static void AssertFake(CarrierFake carrier, dynamic reference)
        {
            Assert.That(carrier != null, "Carrier missing");

            Assert.That(carrier.BoolValue == reference.boolValue, "Bad carrier - Boolean value");
            Assert.That(carrier.EnumValue.ToString() == reference.enumValue.ToString(), "Bad carrier - Enumeration value");
            Assert.That(carrier.IntValue == reference.intValue, "Bad carrier - Integer value");
            Assert.That(carrier.StringValue == reference.stringValue, "Bad carrier - String value");
        }
    }
}
