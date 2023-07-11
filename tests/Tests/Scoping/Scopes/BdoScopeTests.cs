using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Tests;
using NUnit.Framework;

namespace BindOpen.System.Scoping
{
    [TestFixture, Order(300)]
    public class BdoScopeTests
    {
        IBdoScope _scope;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [Test, Order(1)]
        public void CreateScopeNewObjectTest()
        {
            _scope = BdoScoping.NewScope()
                .LoadExtensions(
                    q => q.AddAssemblyFrom<GlobalSetUp>());
        }

        [Test, Order(2)]
        public void CreateScopeDataStore()
        {
            if (_scope == null)
            {
                CreateScopeNewObjectTest();
            }

            _scope.DataStore.Add(("$host", this));
            _scope.DataStore.Add("$host", this);

            Assert.That(
                _scope.DataStore.Count == 1, "Error with string set");
        }
    }
}
