using NUnit.Framework;
using System.IO;

namespace BindOpen;

/// <summary>
/// This class set the global setup.
/// </summary>
[SetUpFixture]
public class GlobalDtosTestSetup
{
    /// <summary>
    /// 
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        try
        {
            // we create the working folder

            if (!Directory.Exists(DataTestData.WorkingFolder))
            {
                Directory.CreateDirectory(DataTestData.WorkingFolder);
            }
        }
        catch
        {
        }
    }
}