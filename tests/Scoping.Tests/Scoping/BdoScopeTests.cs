using BindOpen.Data;
using BindOpen.Data.Stores;
using NUnit.Framework;

namespace BindOpen.Scoping;

/// <summary>
/// This class provides unit tests for the BdoScope component, verifying its ability 
/// to load extensions, manage global data stores, and handle configuration options.
/// </summary>
[TestFixture, Order(300)]
public class BdoScopeTests
{
    private IBdoScope _scope;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initial setup logic can be placed here if shared across all tests
    }

    /// <summary>
    /// Tests the basic instantiation of a scope and the dynamic loading of extensions 
    /// from an assembly.
    /// </summary>
    [Test, Order(1)]
    public void CreateScopeNewObjectTest()
    {
        // 1. Create a fresh scope instance
        _scope = BdoScoping.NewScope();

        // 2. Load extensions using reflection on the setup assembly
        // This populates the scope's internal ExtensionStore
        _scope.LoadExtensions(q => q.AddAssemblyFrom<ScopingTestSetup>());

        Assert.That(_scope, Is.Not.Null, "Scope failed to initialize.");
    }

    /// <summary>
    /// Tests the Scope's global DataStore, ensuring it manages shared objects 
    /// correctly and prevents duplicate key issues for identical references.
    /// </summary>
    [Test, Order(2)]
    public void CreateScopeDataStore()
    {
        // Lazy initialization of scope if tests are run out of order
        if (_scope == null)
        {
            CreateScopeNewObjectTest();
        }

        // 1. Add host references to the DataStore. 
        // The store should handle duplicates gracefully if the key and object match.
        _scope.DataStore.Add(("$host", this));
        _scope.DataStore.Add("$host", this);

        Assert.That(_scope.DataStore.Count, Is.EqualTo(1), "DataStore failed to handle duplicate host entries correctly.");
    }

    /// <summary>
    /// Tests the configuration of ExtensionLoadOptions and verifies that the scope 
    /// can process multiple sources (Databases, Repositories, Paths).
    /// </summary>
    [Test, Order(3)]
    public void CreateExtensionLoadOptions()
    {
        // 1. Configure loading options with various sources
        var options = new ExtensionLoadOptions()
            .AddSource(DatasourceKind.Database, "<database>")
            .AddSource(DatasourceKind.Repository, "<repository>")
            .AddSource(DatasourceKind.Repository, "<repository>")
            .AddSource(DatasourceKind.Repository, @"\\lib\");

        Assert.That(options.Sources?.Count, Is.EqualTo(4), "Extension load options failed to record all sources.");

        // 2. Ensure scope is ready
        if (_scope == null)
        {
            CreateScopeNewObjectTest();
        }

        // 3. Trigger extension loading using the defined options
        _scope.LoadExtensions(options);

        Assert.That(options.Sources?.Count, Is.EqualTo(4), "Sources were lost during the extension loading process.");
    }
}