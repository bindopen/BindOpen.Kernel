using BindOpen.Data.Elements;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents the script word area.
    /// </summary>
    public class BdoScriptwordDomain : IBdoScriptwordDomain
    {
        /// <summary>
        /// The scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; private set; }

        /// <summary>
        /// The variable element set of this instance.
        /// </summary>
        public IBdoElementSet ScriptVariableSet { get; private set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        public IBdoScriptword Scriptword { get; private set; }

        /// <summary>
        /// Initializes a new instance of the BdoScriptwordArea class.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="varElementSet">The variable element set to consider.</param>
        /// <param name="scriptword">The script word to consider.</param>
        public BdoScriptwordDomain(
            IBdoScope scope,
            IBdoElementSet varElementSet,
            IBdoScriptword scriptword)
        {
            Scope = scope;
            ScriptVariableSet = varElementSet;
            Scriptword = scriptword;
        }

        public IBdoScriptwordDomain WithScope(IBdoScope scope)
        {
            Scope = scope;
            return this;
        }

        public IBdoScriptwordDomain WithScriptVariableSet(IBdoElementSet variableSet)
        {
            ScriptVariableSet = variableSet;
            return this;
        }

        public IBdoScriptwordDomain WithScriptword(IBdoScriptword word)
        {
            Scriptword = word;
            return this;
        }
    }
}
