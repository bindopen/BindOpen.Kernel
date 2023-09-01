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
                .WithChildren(
                    BdoData.NewSpec("title"),
                    BdoData.NewSpec("auto"))
                .WithCondition((BdoExpression)BdoScript._This<IBdoSpec>()._Has("auto"))
                .AsRequired((BdoCondition)BdoScript._This<IBdoSpec>()._Has("title"))
                .WithItemRequirement((RequirementLevels.Optional, (BdoCondition)BdoScript._Eq(BdoScript._Parent<IBdoSpec>()._Descendant("auto")._Value(), true)));
        }

        [Test, Order(1)]
        public void ConditionTest()
        {
            var varSet = BdoData.NewMetaSet(("$this", _spec));
            var requirementLvel = _spec.RequirementStatement?.GetItem(SystemData.Scope, varSet);
            Assert.That(requirementLvel == RequirementLevels.Required, "Statement - Error");
        }

        [Test, Order(2)]
        public void RequirementTest()
        {

        }

        [Test, Order(3)]
        public void ItemRequirementTest()
        {

        }
    }
}
