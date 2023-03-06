using BindOpen.Runtime;
using BindOpen.Runtime.Scopes;
using NUnit.Framework;

namespace BindOpen.Tests.Runtime
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
            var scope = BdoRuntime.NewScope();
            scope.LoadExtensions(
                q => q.AddAssemblyFrom<GlobalSetUp>());
        }
    }

}
