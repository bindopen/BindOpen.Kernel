using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Data
{
    [TestFixture, Order(100)]
    public class BdoSpecTests
    {
        private IBdoSpec _spec = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _spec = BdoSpecFaker.CreateSpec();
        }

        [Test, Order(1)]
        public void GetSpecTest()
        {
            var text = _spec.DataType;
        }

        [Test, Order(2)]
        public void AggregateSpecTest()
        {
            var spec = BdoData.NewSpec<BdoAggregateSpec>()
                .WithProperties(BdoData.NewSpec("stringValue", DataValueTypes.Text));
        }

        [Test, Order(3)]
        public void ToSpecTest()
        {
            var spec = BdoData.NewSpecFrom<EntityFake>("test1");
        }
    }
}
