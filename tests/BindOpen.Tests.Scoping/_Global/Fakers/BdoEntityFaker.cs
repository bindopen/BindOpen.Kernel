using BindOpen.Scoping.Data;
using Bogus;
using NUnit.Framework;
using System.Dynamic;

namespace BindOpen.Tests.Scoping
{
    public static class BdoEntityFaker
    {
        public static readonly string XmlFilePath = Tests.WorkingFolder + "Entity.xml";
        public static readonly string JsonFilePath = Tests.WorkingFolder + "Entity.json";

        public static dynamic Fake()
        {
            var f = new Faker();
            dynamic b = new ExpandoObject();
            b.boolValue = f.Random.Bool();
            b.intValue = f.Random.Int(800);
            b.enumValue = ActionPriorities.High;
            b.stringValue = f.Lorem.Word();
            return b;
        }

        public static void AssertFake(EntityFake entity, dynamic reference)
        {
            Assert.That(entity != null, "Entity missing");

            Assert.That(entity.BoolValue?.GetData<bool?>() == reference.boolValue, "Bad entity - Boolean value");
            Assert.That(entity.EnumValue.ToString() == reference.enumValue.ToString(), "Bad entity - Enumeration value");
            Assert.That(entity.IntValue == reference.intValue, "Bad entity - Integer value");
            Assert.That(entity.StringValue == reference.stringValue, "Bad entity - String value");
        }
    }
}
