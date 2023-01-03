using BindOpen.Logging;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static partial class BdoScopeExtension
    {
        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="checkExtensionStore">Indicates whether the extension item definition store extistence is chekced.</param>
        /// <param name="checkDataContext">Indicates whether the data context extistence is chekced.</param>
        /// <param name="checkDataStore">Indicates whether the data store extistence is chekced.</param>
        /// <returns>The log of check log.</returns>
        public static bool Check(
            this IBdoScope scope,
            bool checkExtensionStore = false,
            bool checkDataContext = false,
            bool checkDataStore = false,
            IBdoLog log = null)
        {
            if (checkExtensionStore && scope?.ExtensionStore == null)
            {
                log?.AddError(title: "Application extension missing", description: "No extension item definition store specified.");
                return true;
            }
            if (checkDataContext && scope?.Context == null)
            {
                log?.AddError(title: "Data context missing", description: "No data context specified.");
                return true;
            }
            if (checkDataStore && scope?.DataStore == null)
            {
                log?.AddError(title: "Depot set missing", description: "No depot set specified.");
                return true;
            }

            return false;
        }
    }
}
