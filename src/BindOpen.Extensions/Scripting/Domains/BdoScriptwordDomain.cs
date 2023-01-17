using BindOpen.MetaData.Elements;
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
        public IBdoMetaElementSet ScriptVariableSet { get; private set; }

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
            IBdoMetaElementSet varElementSet,
            IBdoScriptword scriptword)
        {
            Scope = scope;
            ScriptVariableSet = varElementSet;
            Scriptword = scriptword;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public IBdoScriptwordDomain WithScope(IBdoScope scope)
        {
            Scope = scope;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="variableSet"></param>
        /// <returns></returns>
        public IBdoScriptwordDomain WithScriptVariableSet(IBdoMetaElementSet variableSet)
        {
            ScriptVariableSet = variableSet;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public IBdoScriptwordDomain WithScriptword(
            IBdoScriptword word)
        {
            Scriptword = word;
            return this;
        }
    }
}
