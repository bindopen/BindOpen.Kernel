using BindOpen.Data.Meta;
using BindOpen.Runtime.Scopes;

namespace BindOpen.Extensions.Scripting
{
    /// <summary>
    /// This class represents the script word domain.
    /// </summary>
    public interface IBdoScriptwordDomain
    {
        /// <summary>
        /// The scope of this instance.
        /// </summary>
        IBdoScope Scope { get; }

        /// <summary>
        /// Sets the scope of this instance.
        /// </summary>
        IBdoScriptwordDomain WithScope(IBdoScope scope);

        /// <summary>
        /// The variable element set of this instance.
        /// </summary>
        IBdoMetaSet ScriptVariableSet { get; }

        /// <summary>
        /// Sets the variable element set of this instance.
        /// </summary>
        IBdoScriptwordDomain WithScriptVariableSet(IBdoMetaSet variableSet);

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        IBdoScriptword Scriptword { get; }

        /// <summary>
        /// Sets the script word of this instance.
        /// </summary>
        IBdoScriptwordDomain WithScriptword(IBdoScriptword word);
    }
}
