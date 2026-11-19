using NUnit.Framework;

namespace BindOpen;

/// <summary>
/// This class manages the global setup and teardown for the entire test assembly.
/// </summary>
/// <remarks>
/// Being decorated with <see cref="SetUpFixtureAttribute"/>, this class ensures that 
/// the initialization logic runs once before any tests in the namespace are executed.
/// </remarks>
[SetUpFixture]
public class DataTestSetup
{
    /// <summary>
    /// Performs one-time initialization for the test assembly.
    /// </summary>
    /// <remarks>
    /// This is the ideal place to initialize global resources, such as the 
    /// <see cref="DataTestData.WorkingFolder"/> or shared framework configurations.
    /// </remarks>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Global initialization logic goes here
    }
}