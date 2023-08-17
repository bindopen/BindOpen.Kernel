using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Data
{
    [TestFixture, Order(100)]
    public class BdoMetaWrapperTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateTest()
        {
            var obj = SystemData.Scope.NewMetaWrapper<MetaWrapperFake>(
                ("test1", "ABC"));

            var value1 = obj.Test1;
            Assert.That(value1 == "ABC", "Bad dynamic object");

            obj.SetProperty(q => q.Test1, "123");
            obj.SetProperty(q => q.Test2, 1234);

            value1 = obj.GetData<string>("test1");
            Assert.That(value1 == obj.Test1, "Bad dynamic object");

            var value2 = obj.GetData<int?>("test2");
            Assert.That(value2 == obj.Test2, "Bad dynamic object");

            obj.Test2 = 150;
            obj.UpdateDetail();
            Assert.That(obj?.Detail?.GetData<int?>("test2") == 150, "Bad dynamic object");

            obj.Detail["test1"]?.SetData("TEST1");
            obj.UpdateProperties();
            Assert.That(obj?.Test1 == "TEST1", "Bad dynamic object");

        }
    }
}
