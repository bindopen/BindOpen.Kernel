using BindOpen.Scopes;
using NUnit.Framework;

namespace BindOpen.Tests.Scopes
{
    [TestFixture, Order(300)]
    public class ScopeTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateScopeNewObjectTest()
        {
            var scope = BdoScoping.NewScope();
            scope.LoadExtensions(
                q => q.AddAssemblyFrom<GlobalSetUp>());
        }
    }

}
