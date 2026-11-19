using BindOpen.Logging;

namespace BindOpen.Scoping;

/// <summary>
/// Defines a loader responsible for discovering and registering extensions within the store.
/// </summary>
/// <remarks>
/// The store loader handles the heavy lifting of assembly scanning and metadata extraction 
/// during the application initialization phase.
/// </remarks>
public interface IBdoExtensionStoreLoader
{
    /// <summary>
    /// Executes the discovery process to load all available extension packages and definitions.
    /// </summary>
    /// <param name="log">The log to use for tracking discovery progress and capturing any loading errors.</param>
    /// <returns>Returns <c>true</c> if the loading process completed successfully; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// This method typically utilizes the <see cref="IExtensionLoadOptions"/> to determine 
    /// which folders or assemblies to scan.
    /// </remarks>
    bool LoadAll(IBdoLog log = null);
}