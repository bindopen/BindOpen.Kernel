using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Runtime.Items
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
