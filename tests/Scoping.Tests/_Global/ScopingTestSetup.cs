using NUnit.Framework;
using System.IO;

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

        // we delete the working folder

        if (Directory.Exists(DataTestData.WorkingFolder))
        {
            Directory.Delete(DataTestData.WorkingFolder, true);
        }
    }
}