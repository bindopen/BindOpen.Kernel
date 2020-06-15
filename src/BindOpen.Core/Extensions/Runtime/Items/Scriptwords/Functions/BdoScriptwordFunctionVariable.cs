using BindOpen.Application.Scopes;
using BindOpen.System.Scripting;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// This class represents the script word function variable.
    /// </summary>
    public class BdoScriptwordFunctionVariable
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
        /// Initializes a new instance of the BdoScriptwordFunctionVariable class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to consider.</param>
        /// <param name="scriptword">The script word to consider.</param>
        public BdoScriptwordFunctionVariable(
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
