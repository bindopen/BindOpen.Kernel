using BindOpen.System.Data.Meta;
using BindOpen.System.Logging;

namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// This class represents the script word area.
    /// </summary>
    public class BdoScriptDomain : IBdoScriptDomain
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; set; }

        /// <summary>
        /// The variable element set of this instance.
        /// </summary>
        public IBdoMetaSet VariableSet { get; set; }

        /// <summary>
        /// The log of this instance.
        /// </summary>
        public IBdoLog Log { get; set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        public IBdoScriptword Scriptword { get; set; }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BdoScriptwordArea class.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="scriptword">The script word to consider.</param>
        public BdoScriptDomain(
            IBdoScope scope,
            IBdoMetaSet varSet,
            IBdoScriptword scriptword,
            IBdoLog log = null)
        {
            Scope = scope;
            VariableSet = varSet;
            Scriptword = scriptword;
            Log = log;
        }

        #endregion
    }
}
