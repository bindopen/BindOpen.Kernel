using NUnit.Framework;

namespace BindOpen;

/// <summary>
/// This class set the global setup.
/// </summary>
[SetUpFixture]
public class ScopingTestSetup
{
    /// <summary>
    /// 
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Setup singleton variables for the first time

        var _ = ScopingTestData.Scope;
    }
}