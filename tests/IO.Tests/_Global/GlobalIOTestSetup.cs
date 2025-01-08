using NUnit.Framework;
using System.IO;

namespace BindOpen;

/// <summary>
/// This class set the global setup.
/// </summary>
[SetUpFixture]
public class GlobalIOTestSetup
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

            // we delete any existing database file

            File.Delete(GlobalIOTestData.GetDbFilePath());
        }
        catch
        {
        }

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Database.EnsureCreated();
    }
}