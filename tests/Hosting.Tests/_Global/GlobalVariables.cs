using BindOpen.Data.Helpers;
using BindOpen.Hosting.Settings;
using Microsoft.Extensions.Configuration;
using System;

namespace BindOpen.Hosting.Tests;

/// <summary>
/// Provides centralized, lazily initialized global test state, configuration, and host instances
/// for the <c>BindOpen.Hosting.Tests</c> assembly.
/// </summary>
/// <remarks>
/// Exposes shared accessors for the temporary test working directory, the active <see cref="IBdoHost"/>
/// instance, and the underlying .NET <see cref="IConfiguration"/> loaded from <c>appsettings.json</c>.
/// </remarks>
public static class GlobalVariables
{
    private static string _workingFolder = null;
    private static IBdoHost _bdoHost = null;
    private static IConfiguration _netCoreConfiguration;

    /// <summary>
    /// Gets the normalized path to the temporary working folder (<c>bdo\temp\</c>) used for test artifacts.
    /// </summary>
    /// <value>
    /// A platform-normalized directory path resolved from the host's root folder, or falling back
    /// if the host has not been initialized.
    /// </value>
    public static string WorkingFolder
    {
        get
        {
            string workingFolder = _workingFolder;
            if (workingFolder == null)
            {
                _workingFolder = workingFolder =
                    ((_bdoHost?.GetKnownPath(BdoHostPathKind.RootFolder) ?? AppDomain.CurrentDomain.BaseDirectory)
                    .EndingWith(@"\") + @"bdo\temp\").ToPath();
            }

            return workingFolder;
        }
    }

    /// <summary>
    /// Gets the shared <see cref="IBdoHost"/> instance, initializing and starting it on first access.
    /// </summary>
    /// <value>
    /// The started <see cref="IBdoHost"/> configured to throw exceptions upon initialization failure.
    /// </value>
    public static IBdoHost AppHost
    {
        get
        {
            _bdoHost ??= BdoHosting.NewHost(
                options => options
                    .ThrowExceptionOnInitFailure());

            _bdoHost?.Start();

            return _bdoHost;
        }
    }

    /// <summary>
    /// Gets the shared <see cref="IConfiguration"/> built from <c>bdo\config\appsettings.json</c>
    /// relative to the host root directory.
    /// </summary>
    /// <value>
    /// The lazily constructed <see cref="IConfiguration"/> instance with optional file loading and reload-on-change enabled.
    /// </value>
    public static IConfiguration NetCoreConfiguration
    {
        get
        {
            if (_netCoreConfiguration != null)
            {
                return _netCoreConfiguration;
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppHost?.GetKnownPath(BdoHostPathKind.RootFolder))
                .AddJsonFile(@"bdo\config\appsettings.json".ToPath(), optional: true, reloadOnChange: true);

            return _netCoreConfiguration = builder.Build();
        }
    }
}