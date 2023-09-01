using BindOpen.System.Data.Conditions;
using BindOpen.System.Scoping.Script;
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
                .WithCondition((BdoExpression)BdoScript._This<IBdoSpec>()._Parent()._Has("auto"))
                .AsRequired((BdoCondition)BdoScript._This<IBdoSpec>()._Parent()._Has("file"))
                .WithItemRequirement((RequirementLevels.Optional, (BdoCondition)BdoScript._Eq(BdoScript._Parent<IBdoSpec>()._Descendant("auto")._Value(), true)));
        }

        [Test, Order(1)]
        public void ConditionTest()
        {

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
