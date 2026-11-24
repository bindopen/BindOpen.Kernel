using BindOpen.Data.Helpers;
using BindOpen.Logging;
using NUnit.Framework;
using System.IO;

namespace BindOpen.Hosting.Settings;

/// <summary>
/// Contains unit tests for host path resolution, including root and library folder configurations
/// within BindOpen Hosting.
/// </summary>
/// <remarks>
/// These tests verify that <see cref="BdoHostPathKind"/> locations (such as the root folder and library folder)
/// are accurately resolved across conditional overrides, relative paths, and default application paths.
/// </remarks>
[TestFixture]
public class PathTests
{
    /// <summary>
    /// Performs one-time setup before any tests in this fixture are executed.
    /// </summary>
    [OneTimeSetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Verifies that conditional <c>AddRootFolder</c> predicates override the base root folder
    /// only when their condition evaluates to <see langword="true"/>, falling back to the default root folder otherwise.
    /// </summary>
    [Test]
    public void RootFolderTest()
    {
        var bdoHost = BdoHosting.NewHost(
            options => options
                .SetRootFolder("rootFolder")
                .AddRootFolder(s => false, "rootFolderA")
                .AddRootFolder(s => true, "rootFolderB")
                .WithLibraryFolder(@".\bdo\lib"));
        bdoHost.Start();

        Assert.That(bdoHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host failed to reach the expected Pending state.");

        var rootFolderPath = bdoHost.GetKnownPath(BdoHostPathKind.RootFolder);

        Assert.That(rootFolderPath, Is.EqualTo(@"rootFolderB\".ToPath()), "Root folder path did not resolve to the matching conditional folder.");

        bdoHost = BdoHosting.NewHost(
            options => options
                .SetRootFolder("rootFolder")
                .AddRootFolder(s => false, "rootFolderA")
                .WithLibraryFolder(@".\bdo\lib"));
        bdoHost.Start();

        Assert.That(bdoHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host failed to reach the expected Pending state.");

        rootFolderPath = bdoHost.GetKnownPath(BdoHostPathKind.RootFolder).ToPath();

        Assert.That(rootFolderPath, Is.EqualTo(@"rootFolder\".ToPath()), "Root folder path did not fall back to the base root folder.");
    }

    /// <summary>
    /// Verifies that relative root paths resolve against <see cref="FileHelper.GetAppRootFolderPath"/>
    /// and that omitting <c>SetRootFolder</c> defaults directly to the application's root directory.
    /// </summary>
    [Test]
    public void DefaultRootFolderTest()
    {
        var bdoHost = BdoHosting.NewHost(
            options => options
                .SetRootFolder(@".\default")
                .AddRootFolder(s => false, "rootFolderA")
                .WithLibraryFolder(@".\bdo\lib"));
        bdoHost.Start();

        Assert.That(bdoHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host failed to reach the expected Pending state.");

        var rootFolderPath = bdoHost.GetKnownPath(BdoHostPathKind.RootFolder);

        var path = Path.Combine(FileHelper.GetAppRootFolderPath(), @"default\").ToPath();

        Assert.That(rootFolderPath, Is.EqualTo(path), "Relative root folder path did not resolve against the application root directory.");

        // No root folder set initially
        bdoHost = BdoHosting.NewHost(
            options => options
                .AddRootFolder(s => false, "rootFolderA")
                .WithLibraryFolder(@".\bdo\lib"));
        bdoHost.Start();

        Assert.That(bdoHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host failed to reach the expected Pending state.");

        rootFolderPath = bdoHost.GetKnownPath(BdoHostPathKind.RootFolder);

        path = (FileHelper.GetAppRootFolderPath().EndingWith(@"\")).ToPath();

        Assert.That(rootFolderPath, Is.EqualTo(path), "Unset root folder did not default to the application root directory.");
    }

    /// <summary>
    /// Verifies that the library folder (<see cref="BdoHostPathKind.LibraryFolder"/>) resolves
    /// relative to the host's configured root directory.
    /// </summary>
    [Test]
    public void LibraryFolderTest()
    {
        var bdoHost = BdoHosting.NewHost(
            options => options
                .SetRootFolder("./")
                .WithLibraryFolder(@".\bdo\lib"));
        bdoHost.Start();

        Assert.That(bdoHost.State, Is.EqualTo(ProcessExecutionState.Pending), "Application host failed to reach the expected Pending state.");

        var libraryPath = bdoHost.GetKnownPath(BdoHostPathKind.LibraryFolder);

        var path = Path.Combine(bdoHost.GetKnownPath(BdoHostPathKind.RootFolder), @"bdo\lib\").ToPath();

        Assert.That(libraryPath, Is.EqualTo(path), "Library folder path did not resolve properly relative to the root folder.");
    }
}