using BindOpen.System.Data.Conditions;
using BindOpen.System.Scoping.Script;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Data.Meta
{
    [TestFixture, Order(100)]
    public class ConditionalStatementTests
    {
        private IBdoSpec _spec = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _spec = BdoData.NewSpec<BdoSpec>()
                .WithCondition((BdoExpression)BdoScript._Eq(BdoScript._This<IBdoMetaData>()._Descendant("title")._Value(), "myTitle"))
                .AsRequired((BdoCondition)BdoScript._This<IBdoMetaData>()._Has("title"))
                .AsForbidden()
                .WithItemRequirement((RequirementLevels.Required,
                    (BdoCondition)BdoScript._This<IBdoMetaData>()._Descendant("auto")._Value()));
        }

        [Test, Order(1)]
        public void ConditionTest()
        {
            var meta0 = BdoData.NewMetaNode("meta-test")
                .WithSpec(BdoData.NewSpec())
                .With(
                    BdoData.NewMeta("title", "A"));

            var existence0 = meta0.WhatCondition(SystemData.Scope);
            Assert.That(existence0, "Statement - Error");

            var meta1 = BdoData.NewMetaNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("title", "myTitle"));

            var existence1 = meta1.WhatCondition(SystemData.Scope);
            Assert.That(existence1, "Statement - Error");

            var meta2 = BdoData.NewMetaNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("title", "A"));

            var existence2 = meta2.WhatCondition(SystemData.Scope);
            Assert.That(!existence2, "Statement - Error");
        }

        [Test, Order(2)]
        public void RequirementTest()
        {
            var meta0 = BdoData.NewMetaNode("meta-test")
                .WithSpec(BdoData.NewSpec())
                .With(
                    BdoData.NewMeta("title", "A"));

            var requirementLvel0 = meta0.WhatRequirement(SystemData.Scope);
            Assert.That(requirementLvel0 == RequirementLevels.None, "Statement - Error");

            var meta1 = BdoData.NewMetaNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", true),
                    BdoData.NewMeta("title", "This is my title"));

            var requirementLvel1 = meta1.WhatRequirement(SystemData.Scope);
            Assert.That(requirementLvel1 == RequirementLevels.Required, "Statement - Error");

            var meta2 = BdoData.NewMetaNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", true));

            var requirementLvel2 = meta2.WhatRequirement(SystemData.Scope);
            Assert.That(requirementLvel2 == RequirementLevels.Forbidden, "Statement - Error");
        }

        [Test, Order(3)]
        public void ItemRequirementTest()
        {
            var meta0 = BdoData.NewMetaNode("meta-test")
                .WithSpec(BdoData.NewSpec())
                .With(
                    BdoData.NewMeta("title", "A"));

            var requirementLvel0 = meta0.WhatItemRequirement(SystemData.Scope);
            Assert.That(requirementLvel0 == RequirementLevels.Optional, "Statement - Error");

            var meta1 = BdoData.NewMetaNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", true),
                    BdoData.NewMeta("title", "This is my title"));

            var requirementLvel1 = meta1.WhatItemRequirement(SystemData.Scope);
            Assert.That(requirementLvel1 == RequirementLevels.Required, "Statement - Error");

            var meta2 = BdoData.NewMetaNode("meta-test")
                .WithSpec(_spec)
                .With(
                    BdoData.NewMeta("auto", false));

            var requirementLvel2 = meta2.WhatItemRequirement(SystemData.Scope);
            Assert.That(requirementLvel2 == RequirementLevels.Optional, "Statement - Error");

        }
    }
}
