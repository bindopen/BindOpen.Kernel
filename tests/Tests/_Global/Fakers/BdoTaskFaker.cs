using BindOpen.Kernel.Data;
using Bogus;
using NUnit.Framework;

namespace BindOpen.Kernel.Tests
{
    public static class BdoTaskFaker
    {
        public static readonly string XmlFilePath = SystemData.WorkingFolder + "Task.xml";
        public static readonly string JsonFilePath = SystemData.WorkingFolder + "Task.json";

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

        public static void AssertFake(TaskFake task, dynamic reference)
        {
            Assert.That(task != null, "Task missing");

            Assert.That(task.BoolValue == reference.boolValue, "Bad task - Boolean value");
            Assert.That(task.EnumValue.ToString() == reference.enumValue.ToString(), "Bad task - Enumeration value");
            Assert.That(task.IntValue == reference.intValue, "Bad task - Integer value");
            Assert.That(task.StringValue == reference.stringValue, "Bad task - String value");
        }
    }
}
