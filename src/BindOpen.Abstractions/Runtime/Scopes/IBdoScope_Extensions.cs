using BindOpen.Data.Configuration;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Connecting;
using BindOpen.Extensions.Modeling;
using BindOpen.Extensions.Processing;
using BindOpen.Logging;
using System;

namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This interface defines an application 
    /// </summary>
    public partial interface IBdoScope : IDisposable
    {
        // Entities ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created entity.</returns>
        IBdoEntity CreateEntity(
            IBdoConfiguration config,
            IBdoMetaList varSet = null,
            IBdoLog log = null);

        // Connectors ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created connector.</returns>
        IBdoConnector CreateConnector(
            IBdoConfiguration config,
            IBdoMetaList varSet = null,
            IBdoLog log = null);

        // Tasks ------------------------------------------------

        /// <summary>
        /// Creates the instance of the specified definition.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="config">The config to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <param name="varSet">The variable element set to use.</param>
        /// <returns>Returns the created task.</returns>
        IBdoTask CreateTask(
            IBdoConfiguration config = null,
            IBdoMetaList varSet = null,
            IBdoLog log = null);

        // Data handlers ---------------------------------------

        /// <summary>
        /// Handles the specified data object.
        /// </summary>
        /// <param name="handlerUniqueName"></param>
        /// <param name="obj"></param>
        /// <param name="pathDetail"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object GetData(
            string handlerUniqueName,
            object obj,
            IBdoMetaList pathDetail,
            IBdoMetaList varSet,
            IBdoLog log);

        /// <summary>
        /// Handles the specified data object.
        /// </summary>
        /// <param name="handlerUniqueName"></param>
        /// <param name="obj"></param>
        /// <param name="pathDetail"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        object PostData(
            string handlerUniqueName,
            object obj,
            IBdoMetaList pathDetail,
            IBdoMetaList varSet,
            IBdoLog log);
    }
}

