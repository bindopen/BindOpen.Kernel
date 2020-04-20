using BindOpen.Application.Scopes;
using BindOpen.System.Scripting;

namespace BindOpen.Extensions.Runtime
{
    // --------------------------------------------------
    // DELEGATES
    // --------------------------------------------------

    #region Delegates

    /// <summary>
    /// This delegate represents a script word function.
    /// </summary>
    public delegate string BdoScriptwordFunction(
            IBdoScope scope,
            IScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters);

    #endregion

}
