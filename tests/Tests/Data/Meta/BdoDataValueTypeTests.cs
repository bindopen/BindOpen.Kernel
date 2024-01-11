using BindOpen.Kernel.Tests;
using NUnit.Framework;

namespace BindOpen.Data
{
    [TestFixture, Order(210)]
    public class BdoDataValueTypeTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void ToStringTest()
        {
            var dataType = BdoData.NewDataType(DataValueTypes.Text);
            var st = dataType.ToString();
            Assert.That(st == nameof(DataValueTypes.Text), "Bad assembly reference");

            dataType = BdoData.NewDataType<ClassFake>();
            st = dataType.ToString();
            Assert.That(st.StartsWith("Object, BindOpen.Kernel.Tests.ClassFake"), "Bad assembly reference");
        }
    }
}
