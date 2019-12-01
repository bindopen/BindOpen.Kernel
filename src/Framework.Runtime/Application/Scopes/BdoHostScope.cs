using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Runtime.Application.Services;
using System;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This class represents a BDO host scope.
    /// </summary>
    public class BdoHostScope : BdoScope, IBdoHostScope
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The connection service of this instance.
        /// </summary>
        public IBdoConnectionService ConnectionService { get; set; } = null;

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoHostScope class.
        /// </summary>
        public BdoHostScope() : this(null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the BdoHostScope class.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        public BdoHostScope(AppDomain appDomain) : base(appDomain)
        {
            ConnectionService = new BdoConnectionService(this);
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Check the specified item.
        /// </summary>
        /// <param name="isExtensionChecked">Indicates whether the BindOpen extension is chekced.</param>
        /// <param name="isInterpreterChecked">Indicates whether the script interpreter is chekced.</param>
        /// <param name="isConnectionServiceChecked">Indicates whether the connection service is chekced.</param>
        /// <param name="isContextChecked">Indicates whether the data context is chekced.</param>
        /// <param name="isDatasourceDepotChecked">Indicates whether the data module manager is chekced.</param>
        /// <returns>The log of check log.</returns>
        public IBdoLog Check(
            bool isExtensionChecked = false,
            bool isInterpreterChecked = false,
            bool isConnectionServiceChecked = false,
            bool isContextChecked = false,
            bool isDatasourceDepotChecked = false)
        {
            IBdoLog log = Check(
                isExtensionChecked,
                isInterpreterChecked,
                isConnectionServiceChecked,
                isDatasourceDepotChecked);

            if (isConnectionServiceChecked || ConnectionService == null)
            {
                log.AddError(title: "Connection manager missing", description: "No connection service specified.");
            }

            return log;
        }

        #endregion
    }
}