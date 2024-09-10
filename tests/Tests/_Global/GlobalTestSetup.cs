using NUnit.Framework;
using System.IO;

namespace BindOpen;

/// <summary>
/// This class set the global setup.
/// </summary>
[SetUpFixture]
public class GlobalTestSetup
{
    /// <summary>
    /// 
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // Setup singleton variables for the first time

        var _ = GlobalTestData.Scope;

        // we delete the working folder

        if (Directory.Exists(GlobalTestData.WorkingFolder))
        {
            Directory.Delete(GlobalTestData.WorkingFolder, true);
        }
    }
}