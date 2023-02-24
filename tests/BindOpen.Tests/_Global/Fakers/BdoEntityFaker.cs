using BindOpen.Data;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Tests
{
    public static class BdoEntityFaker
    {
        public static readonly string XmlFilePath = Tests.WorkingFolder + "Entity.xml";
        public static readonly string JsonFilePath = Tests.WorkingFolder + "Entity.json";

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

        public static void AssertFake(EntityFake entity, dynamic reference)
        {
            Assert.That(entity != null, "Entity missing");

            Assert.That(entity.BoolValue == reference.boolValue, "Bad entity - Boolean value");
            Assert.That(entity.EnumValue.ToString() == reference.enumValue.ToString(), "Bad entity - Enumeration value");
            Assert.That(entity.IntValue == reference.intValue, "Bad entity - Integer value");
            Assert.That(entity.StringValue == reference.stringValue, "Bad entity - String value");
        }
    }
}
