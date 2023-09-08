using BindOpen.Kernel.Data;
using Bogus;
using System.Dynamic;

namespace BindOpen.Kernel.Tests
{
    public static class BdoFunctionFaker
    {
        public static readonly string XmlFilePath = SystemData.WorkingFolder + "Function.xml";
        public static readonly string JsonFilePath = SystemData.WorkingFolder + "Function.json";

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
    }
}
