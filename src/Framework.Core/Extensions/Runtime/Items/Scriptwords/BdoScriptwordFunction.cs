using BindOpen.Framework.Application.Scopes;
using BindOpen.Framework.System.Scripting;

namespace BindOpen.Framework.Extensions.Runtime
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
            IBdoScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters);

    #endregion

}
