using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Data
{
    [TestFixture, Order(100)]
    public class BdoDynamicObjectTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void GetSpecTest()
        {
            var obj = new BdoDynamicObject(SystemData.Scope, null);
            obj.SetProperty(q => q.Test1, "123");
            obj.SetProperty(q => q.Test2, 1234);


            var value1 = obj.GetData("test1");
            Assert.That(value1 == obj.Test1, "Bad dynamic object");

            var value2 = obj.GetData("test2");
            Assert.That(value2 as int? == obj.Test2, "Bad dynamic object");
        }
    }
}
