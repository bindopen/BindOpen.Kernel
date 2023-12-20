using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping;
using Bogus;
using NUnit.Framework;
using System.Dynamic;

namespace BindOpen.Kernel.Tests
{
    public static class BdoEntityFaker
    {
        public static readonly string XmlFilePath = SystemData.WorkingFolder + "Entity.xml";
        public static readonly string JsonFilePath = SystemData.WorkingFolder + "Entity.json";

        public static dynamic NewData()
        {
            var f = new Faker();
            dynamic b = new ExpandoObject();
            b.boolValue = f.Random.Bool();
            b.intValue = f.Random.Int(800);
            b.enumValue = AccessibilityLevels.Private;
            b.stringValue = f.Lorem.Word();
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

            var meta =
                BdoData.NewObject()
                .WithDataType(BdoExtensionKinds.Entity, "bindopen.kernel.tests$testEntity")
                .With(
                    BdoData.NewScalar("boolValue", data.boolValue as bool?),
                    BdoData.NewScalar("enumValue", data.enumValue as AccessibilityLevels?),
                    BdoData.NewScalar("intValue", data.intValue as int?),
                    BdoData.NewScalar("stringValue", data.stringValue as string));

            return meta;
        }

        public static void AssertFake(EntityFake entity, dynamic reference = null)
        {
            Assert.That(entity != null, "Entity missing");

            Assert.That(entity.BoolValue?.GetData<bool?>() == reference.boolValue, "Bad entity - Boolean value");
            Assert.That(entity.EnumValue.ToString() == reference.enumValue.ToString(), "Bad entity - Enumeration value");
            Assert.That(entity.IntValue == reference.intValue, "Bad entity - Integer value");
            Assert.That(entity.StringValue == reference.stringValue, "Bad entity - String value");
        }
    }
}
