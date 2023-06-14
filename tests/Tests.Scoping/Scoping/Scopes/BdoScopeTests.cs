using BindOpen.System.Scoping;
using NUnit.Framework;

namespace BindOpen.System.Tests.Scoping.Scopes
{
    [TestFixture, Order(300)]
    public class BdoScopeTests
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateScopeNewObjectTest()
        {
            var scope = BdoScoping.NewScope()
                .LoadExtensions(
                    q => q.AddAssemblyFrom<GlobalSetUp>());
        }
    }

}
