using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Data.Meta.Reflection;
using BindOpen.Kernel.Tests;
using NUnit.Framework;

namespace BindOpen.Data.Meta
{
    [TestFixture, Order(100)]
    public class BdoSpecTests
    {
        private IBdoSpec _spec;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _spec = BdoSpecFaker.CreateSpec();
        }

        [Test, Order(2)]
        public void AggregateSpecTest()
        {
            Assert.That(_spec._Children?.Count == 3, "Aggregate specification error");
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
            Assert.That(spec._Children?.Count == 13, "Aggregate specification error");
        }

        [Test, Order(5)]
        public void SatisfiedSubTest()
        {
            var meta = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("title", "myTitle"),
                    BdoData.NewMeta("description", "myDescription"));

            var subSpec = meta.FindChildSpec(_spec, SystemData.Scope);
            Assert.That(subSpec == _spec._Children[1], "Bad spec condition");
        }

        [Test, Order(6)]
        public void DefaultSubTest()
        {
            var meta = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("title", "myTitle"));

            var subSpec = meta.FindChildSpec(_spec, SystemData.Scope);
            Assert.That(subSpec == _spec._Children[0], "Bad spec condition");
        }

        [Test, Order(7)]
        public void CheckWithSpecTest()
        {
            var validator = SystemData.Scope.CreateValidator();

            var meta1 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", true),
                    BdoData.NewMeta("title", "myTitle"));

            var ok1 = validator.Check(meta1);
            Assert.That(ok1, "Check rules - Error");

            var meta2 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", true),
                    BdoData.NewMeta("label", true),     // should be forbidden
                    BdoData.NewMeta("title", "myTitle"));

            var ok2 = validator.Check(meta2);
            Assert.That(!ok2, "Check rules - Error");
        }

        [Test, Order(8)]
        public void CheckWithoutSpecTest()
        {
            var validator = SystemData.Scope.CreateValidator();

            var meta1 = BdoData.NewNode("meta-test")
                .With(
                    BdoData.NewMeta("auto", true),  // should be ok
                    BdoData.NewMeta("label", true),
                    BdoData.NewMeta("title", "myTitle"));

            var ok1 = validator.Check(meta1);
            Assert.That(ok1, "Check rules - Error");

            var meta2 = BdoData.NewNode("meta-test")
                .With(
                    BdoData.NewMeta("auto", true)   // should raise a value type error
                        .WithSpec(BdoData.NewSpec().WithDataType(DataValueTypes.Integer)),
                    BdoData.NewMeta("label", true),
                    BdoData.NewMeta("title", "myTitle"));

            var ok2 = validator.Check(meta2);
            Assert.That(!ok2, "Check rules - Error");

            var meta3 = BdoData.NewNode("meta-test")
                .With(
                    BdoData.NewMeta("auto", "true")   // should be ok
                        .WithSpec(BdoData.NewSpec().WithDataType(DataValueTypes.Text)),
                    BdoData.NewMeta("label", true),
                    BdoData.NewMeta("title", "myTitle"));

            var ok3 = validator.Check(meta3);
            Assert.That(ok3, "Check rules - Error");
        }
    }
}
