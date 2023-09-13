using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Data.Meta.Reflection;
using BindOpen.Kernel.Scoping;
using BindOpen.Kernel.Tests;
using NUnit.Framework;

namespace BindOpen.Kernel.Data.Meta
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
            var spec = BdoData.NewSpec<BdoSpec>()
                .WithProperties(BdoData.NewSpec("stringValue", DataValueTypes.Text));
            Assert.That(spec.As<BdoSpec>()._Children?.Count == 1, "Aggregate specification error");
        }

        [Test, Order(3)]
        public void CreateFromTypeTest()
        {
            var spec = BdoData.NewSpecFrom<EntityFake>("test1");
            Assert.That(spec.As<BdoSpec>()._Children?.Count == 13, "Aggregate specification error");
        }

        [Test, Order(4)]
        public void CreateFromTypeToSpecTest()
        {
            var spec = typeof(EntityFake).ToSpec<SpecFake>("test1");
            Assert.That(spec.As<BdoSpec>()._Children?.Count == 13, "Aggregate specification error");
        }
    }
}
