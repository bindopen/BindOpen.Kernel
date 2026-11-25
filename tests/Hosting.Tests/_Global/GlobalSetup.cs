using NUnit.Framework;

namespace BindOpen.Hosting.Tests;

/// <summary>
/// Provides assembly-level setup initialization for the <c>BindOpen.Hosting.Tests</c> test suite.
/// </summary>
/// <remarks>
/// Ensures that global hosting dependencies and shared application host state defined in
/// <see cref="GlobalVariables"/> are initialized once prior to executing any test fixtures in the namespace.
/// </remarks>
[SetUpFixture]
public class GlobalSetUp
{
    /// <summary>
    /// Initializes global test variables and triggers lazy instantiation of <see cref="GlobalVariables.AppHost"/>
    /// before any tests in the assembly are executed.
    /// </summary>
    [OneTimeSetUp]
    public void Setup()
    {
        // Setup variables for the first time
        _ = GlobalVariables.AppHost;
    }
}