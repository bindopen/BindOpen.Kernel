using NUnit.Framework;
using System.IO;

namespace BindOpen;

/// <summary>
/// Provides global NUnit setup initialization for the <see cref="BindOpen"/> DTO test assembly,
/// preparing required filesystem directories before any test fixtures execute.
/// </summary>
/// <remarks>
/// As an NUnit <see cref="SetUpFixtureAttribute"/> at the root namespace level, this class runs once
/// prior to all tests in the assembly to ensure <see cref="DataTestData.WorkingFolder"/> exists.
/// </remarks>
[SetUpFixture]
public class GlobalDtosTestSetup
{
    /// <summary>
    /// Ensures that the working directory specified by <see cref="DataTestData.WorkingFolder"/>
    /// is created on disk before any tests in the assembly are executed.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        try
        {
            // Create the working folder if it does not already exist
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