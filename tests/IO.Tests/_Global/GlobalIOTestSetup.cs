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
        File.Delete(GlobalIOTestData.GetDbFilePath());

        using var dbContext = GlobalIOTestData.CreateDbContext();
        dbContext.Database.EnsureCreated();
    }
}