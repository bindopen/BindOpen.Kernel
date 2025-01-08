using NUnit.Framework;
using System.IO;

namespace BindOpen;

/// <summary>
/// This class set the global setup.
/// </summary>
[SetUpFixture]
public class DataTestSetup
{
    /// <summary>
    /// 
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        // we delete the working folder

        if (Directory.Exists(DataTestData.WorkingFolder))
        {
            Directory.Delete(DataTestData.WorkingFolder, true);
        }
    }
}