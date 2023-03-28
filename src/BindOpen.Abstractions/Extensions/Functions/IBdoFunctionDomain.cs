using BindOpen.Data.Meta;
using BindOpen.Scopes;
using BindOpen.Script;

namespace BindOpen.Extensions.Functions
{
    /// <summary>
    /// This class represents the script word domain.
    /// </summary>
    public interface IBdoFunctionDomain
    {
        /// <summary>
        /// The scope of this instance.
        /// </summary>
        IBdoScope Scope { get; }

        /// <summary>
        /// Sets the scope of this instance.
        /// </summary>
        IBdoFunctionDomain WithScope(IBdoScope scope);

        /// <summary>
        /// The variable element set of this instance.
        /// </summary>
        IBdoMetaSet ScriptVariableSet { get; }

        /// <summary>
        /// Sets the variable element set of this instance.
        /// </summary>
        IBdoFunctionDomain WithScriptVariableSet(IBdoMetaSet variableSet);

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        IBdoScriptword Scriptword { get; }

        /// <summary>
        /// Sets the script word of this instance.
        /// </summary>
        IBdoFunctionDomain WithScriptword(IBdoScriptword word);
    }
}
