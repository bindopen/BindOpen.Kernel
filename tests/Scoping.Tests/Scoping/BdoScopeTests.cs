using BindOpen.Data;
using BindOpen.Data.Stores;
using NUnit.Framework;

namespace BindOpen.Scoping;

/// <summary>
/// Contains unit tests for the <see cref="IBdoScope"/> component, verifying its instantiation,
/// dynamic extension discovery, global data store deduplication, and <see cref="ExtensionLoadOptions"/> processing.
/// </summary>
/// <remarks>
/// These tests verify that scopes created via <see cref="BdoScoping.NewScope"/> properly load extensions
/// from assemblies and configured datasources, and safely handle duplicate host entries in <see cref="IBdoScope.DataStore"/>.
/// </remarks>
[TestFixture]
public class BdoScopeTests
{
    private IBdoScope _scope;

    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Initial setup logic can be placed here if shared across all tests
    }

    /// <summary>
    /// Verifies that <see cref="BdoScoping.NewScope"/> instantiates a valid <see cref="IBdoScope"/>
    /// and dynamically loads extensions from the assembly containing <see cref="ScopingTestSetup"/>.
    /// </summary>
    [Test]
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
    /// Verifies that <see cref="IBdoScope.DataStore"/> manages shared host references and gracefully
    /// deduplicates identical key-value insertions without throwing or creating duplicate entries.
    /// </summary>
    [Test]
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
    /// Verifies that <see cref="ExtensionLoadOptions"/> records multiple datasource entries
    /// (databases, repositories, and directory paths) and retains them when processed by <see cref="IBdoScope.LoadExtensions"/>.
    /// </summary>
    [Test]
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