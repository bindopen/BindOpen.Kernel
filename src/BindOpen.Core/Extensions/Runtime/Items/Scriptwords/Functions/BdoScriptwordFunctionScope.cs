using BindOpen.Application.Scopes;
using BindOpen.System.Scripting;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents the script word function scope.
    /// </summary>
    public class BdoScriptwordFunctionScope
    {
        /// <summary>
        /// The scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; set; }

        /// <summary>
        /// The script variable set of this instance.
        /// </summary>
        public IScriptVariableSet ScriptVariableSet { get; set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        public IBdoScriptword Scriptword { get; set; }

        /// <summary>
        /// Initializes a new instance of the BdoScriptwordFunctionScope class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="scriptword">The script word to consider.</param>
        public BdoScriptwordFunctionScope(
            IBdoScope scope,
            IScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptword)
        {
            Scope = scope;
            ScriptVariableSet = scriptVariableSet;
            Scriptword = scriptword;
        }
    }
}
