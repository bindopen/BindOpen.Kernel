using NUnit.Framework;

namespace BindOpen;

/// <summary>
/// This class handles the global setup for all tests within the BindOpen namespace.
/// It ensures that shared resources and scoping engines are initialized before 
/// any specific test fixture is executed.
/// </summary>
[SetUpFixture]
public class ScopingTestSetup
{
    /// <summary>
    /// Executes once for the entire test assembly. 
    /// Triggers the static initialization of the global Scope and Extension Store.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // By accessing ScopingTestData.Scope, we trigger the lazy-loading 
        // of assemblies and the registration of extensions (Functions, Tasks, etc.)
        // as defined in the ScopingTestData singleton.
        var _ = ScopingTestData.Scope;
    }
}