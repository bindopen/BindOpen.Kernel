using BindOpen.Kernel.Tests;
using NUnit.Framework;

namespace BindOpen.Kernel.Data.Meta
{
    [TestFixture, Order(100)]
    public class BdoSpecRuleTests
    {
        private IBdoSpec _spec;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _spec = BdoSpecFaker.CreateSpec();
        }

        [Test, Order(2)]
        public void RequirementTest()
        {
            var meta0 = BdoData.NewNode("meta-test")
                .WithSpec(BdoData.NewSpec())
                .With(
                    BdoData.NewMeta("title", "A"));

            var requirementLevel0 = meta0.GetRequirementLevel(SystemData.Scope);
            Assert.That(requirementLevel0 == RequirementLevels.None, "Statement - Error");

            var meta1 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", true),
                    BdoData.NewMeta("title", "This is my title"));

            var requirementLevel1 = meta1.GetRequirementLevel(SystemData.Scope);
            Assert.That(requirementLevel1 == RequirementLevels.Required, "Statement - Error");

            var meta2 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(("auto", true));

            var requirementLevel2 = meta2.GetRequirementLevel(SystemData.Scope);
            Assert.That(requirementLevel2 == RequirementLevels.Forbidden, "Statement - Error");
        }

        [Test, Order(3)]
        public void ItemRequirementTest()
        {
            var meta0 = BdoData.NewNode("meta-test")
                .WithSpec(BdoData.NewSpec())
                .With(
                    BdoData.NewMeta("title", "A"));

            var requirementLevel0 = meta0.GetItemRequirementLevel(SystemData.Scope);
            Assert.That(requirementLevel0 == RequirementLevels.Optional, "Statement - Error");

            var meta1 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", true),
                    BdoData.NewMeta("title", "This is my title"));

            var requirementLevel1 = meta1.GetItemRequirementLevel(SystemData.Scope);
            Assert.That(requirementLevel1 == RequirementLevels.Required, "Statement - Error");

            var meta2 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", false));

            var requirementLevel2 = meta2.GetItemRequirementLevel(SystemData.Scope);
            Assert.That(requirementLevel2 == RequirementLevels.Optional, "Statement - Error");
        }
    }
}
