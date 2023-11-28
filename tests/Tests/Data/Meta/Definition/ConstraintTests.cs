using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Scoping.Script;
using BindOpen.Kernel.Tests;
using NUnit.Framework;

namespace BindOpen.Kernel.Data.Meta
{
    [TestFixture, Order(100)]
    public class ConstraintTests
    {
        private IBdoSpec _spec = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _spec = BdoData.NewSpec<BdoSpec>()
                .WithCondition((BdoExpression)BdoScript.Eq(BdoScript.This<IBdoMetaData>()._Descendant("title").Value(), "myTitle"))
                .AsRequired((BdoCondition)BdoScript.This<IBdoMetaData>()._Has("title"))
                .AsForbidden()
                .WithItemRequirement((RequirementLevels.Required,
                    (BdoCondition)BdoScript.This<IBdoMetaData>()._Descendant("auto").Value()));
        }

        [Test, Order(1)]
        public void ConditionTest()
        {
            var meta0 = BdoData.NewNode("meta-test")
                .WithSpec(BdoData.NewSpec())
                .With(
                    BdoData.NewMeta("title", "A"));

            var existence0 = meta0.GetConditionValue(SystemData.Scope);
            Assert.That(existence0, "Statement - Error");

            var meta1 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("title", "myTitle"));

            var existence1 = meta1.GetConditionValue(SystemData.Scope);
            Assert.That(existence1, "Statement - Error");

            var meta2 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("title", "A"));

            var existence2 = meta2.GetConditionValue(SystemData.Scope);
            Assert.That(!existence2, "Statement - Error");
        }

        [Test, Order(2)]
        public void RequirementTest()
        {
            var meta0 = BdoData.NewNode("meta-test")
                .WithSpec(BdoData.NewSpec())
                .With(
                    BdoData.NewMeta("title", "A"));

            var requirementLevel0 = meta0.GetRequirement(SystemData.Scope);
            Assert.That(requirementLevel0 == RequirementLevels.None, "Statement - Error");

            var meta1 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", true),
                    BdoData.NewMeta("title", "This is my title"));

            var requirementLevel1 = meta1.GetRequirement(SystemData.Scope);
            Assert.That(requirementLevel1 == RequirementLevels.Required, "Statement - Error");

            var meta2 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(("auto", true));

            var requirementLevel2 = meta2.GetRequirement(SystemData.Scope);
            Assert.That(requirementLevel2 == RequirementLevels.Forbidden, "Statement - Error");
        }

        [Test, Order(3)]
        public void ItemRequirementTest()
        {
            var meta0 = BdoData.NewNode("meta-test")
                .WithSpec(BdoData.NewSpec())
                .With(
                    BdoData.NewMeta("title", "A"));

            var requirementLevel0 = meta0.GetItemRequirement(SystemData.Scope);
            Assert.That(requirementLevel0 == RequirementLevels.Optional, "Statement - Error");

            var meta1 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", true),
                    BdoData.NewMeta("title", "This is my title"));

            var requirementLevel1 = meta1.GetItemRequirement(SystemData.Scope);
            Assert.That(requirementLevel1 == RequirementLevels.Required, "Statement - Error");

            var meta2 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", false));

            var requirementLevel2 = meta2.GetItemRequirement(SystemData.Scope);
            Assert.That(requirementLevel2 == RequirementLevels.Optional, "Statement - Error");
        }

        [Test, Order(4)]
        public void CheckConstraintsTest()
        {
            var validator = SystemData.Scope.CreateValidator();

            var meta1 = BdoData.NewNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", true),
                    BdoData.NewMeta("title", "This is my title"));

            var ok = validator.Check(meta1);
            Assert.That(ok, "Check constraints - Error");
        }
    }
}
