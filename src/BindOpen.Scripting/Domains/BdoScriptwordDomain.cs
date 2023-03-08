using BindOpen.Data.Meta;
using BindOpen.Extensions.Functions;
using BindOpen.Scoping.Scopes;

namespace BindOpen.Scripting
{
    /// <summary>
    /// This class represents the script word area.
    /// </summary>
    public class BdoScriptwordDomain : IBdoFunctionDomain
    {
        /// <summary>
        /// The scope of this instance.
        /// </summary>
        public IBdoScope Scope { get; private set; }

        /// <summary>
        /// The variable element set of this instance.
        /// </summary>
        public IBdoMetaSet ScriptVariableSet { get; private set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        public IBdoScriptword Scriptword { get; private set; }

        /// <summary>
        /// Initializes a new instance of the BdoScriptwordArea class.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        /// <param key="varSet">The variable element set to consider.</param>
        /// <param key="scriptword">The script word to consider.</param>
        public BdoScriptwordDomain(
            IBdoScope scope,
            IBdoMetaSet varSet,
            IBdoScriptword scriptword)
        {
            Scope = scope;
            ScriptVariableSet = varSet;
            Scriptword = scriptword;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="scope"></param>
        /// <returns></returns>
        public IBdoFunctionDomain WithScope(IBdoScope scope)
        {
            Scope = scope;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="variableSet"></param>
        /// <returns></returns>
        public IBdoFunctionDomain WithScriptVariableSet(IBdoMetaSet variableSet)
        {
            ScriptVariableSet = variableSet;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="word"></param>
        /// <returns></returns>
        public IBdoFunctionDomain WithScriptword(
            IBdoScriptword word)
        {
            Scriptword = word;
            return this;
        }
    }
}
