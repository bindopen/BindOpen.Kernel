using BindOpen.Data;
using BindOpen.Kernel.Tests;
using NUnit.Framework;

namespace BindOpen.Scoping
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
            _scope = BdoScoping.NewScope();
            _scope.LoadExtensions(q => q.AddAssemblyFrom<GlobalSetUp>());
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

        [Test, Order(2)]
        public void CreateExtensionLoadOptions()
        {
            var options = new ExtensionLoadOptions();
            options.AddSource(DatasourceKind.Database, "<database>");
            options.AddSource(DatasourceKind.Repository, "<repository>");
            options.AddSource(DatasourceKind.Repository, "<repository>");

            Assert.That(
                options.Sources?.Count == 3, "Error with string set");

            if (_scope == null)
            {
                CreateScopeNewObjectTest();
            }

            _scope.LoadExtensions(
                q => q = options
                    .AddSource(DatasourceKind.Repository, @"\\lib\"));

            Assert.That(
                options.Sources?.Count == 4, "Error with string set");
        }
    }
}
