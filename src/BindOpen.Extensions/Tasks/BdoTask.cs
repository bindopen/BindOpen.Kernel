using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scopes;
using System.Threading;
using System.Threading.Tasks;

namespace BindOpen.Extensions.Tasks
{
    /// <summary>
    /// This class represents an task.
    /// </summary>
    public abstract class BdoTask : BdoExtension, IBdoTask
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoTask class.
        /// </summary>
        protected BdoTask() : base()
        {
        }

        #endregion

        //------------------------------------------
        // EXECUTION
        //-----------------------------------------

        #region Execution

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param key="log">The log to consider.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use for execution.</param>
        /// <param key="runtimeMode">The runtime mode to consider.</param>
        /// <returns>Returns the output value of the execution.</returns>
        public virtual Task<bool> ExecuteAsync(
            CancellationToken toekn,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            RuntimeModes runtimeMode = RuntimeModes.Normal,
            IBdoLog log = null)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param key="log">The log to consider.</param>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to use for execution.</param>
        /// <param key="runtimeMode">The runtime mode to consider.</param>
        /// <returns>Returns the output value of the execution.</returns>
        public virtual bool Execute(
            CancellationToken toekn,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            RuntimeModes runtimeMode = RuntimeModes.Normal,
            IBdoLog log = null)
        {
            return true;
        }

        #endregion
    }
}