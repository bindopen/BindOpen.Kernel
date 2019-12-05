using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Runtime.Application.Services;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This interface defines a bot scope.
    /// </summary>
    public interface IBdoHostScope : IBdoScope
    {
        /// <summary>
        /// The connection service.
        /// </summary>
        IBdoConnectionService ConnectionService { get; set; }

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="isExtensionChecked">Indicates whether the BindOpen extension is chekced.</param>
        /// <param name="isInterpreterChecked">Indicates whether the script interpreter is chekced.</param>
        /// <param name="isConnectionServiceChecked">Indicates whether the connection service is chekced.</param>
        /// <param name="isContextChecked">Indicates whether the data context is chekced.</param>
        /// <param name="isDatasourceDepotChecked">Indicates whether the data module manager is chekced.</param>
        /// <returns>The log of check log.</returns>
        IBdoLog Check(
            bool isExtensionChecked = false,
            bool isInterpreterChecked = false,
            bool isConnectionServiceChecked = false,
            bool isContextChecked = false,
            bool isDatasourceDepotChecked = false)
;
    }
}