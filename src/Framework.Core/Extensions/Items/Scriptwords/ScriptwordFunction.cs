using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Items.Scriptwords
{

    // --------------------------------------------------
    // DELEGATES
    // --------------------------------------------------

    #region Delegates

    /// <summary>
    /// This delegate represents a script word function.
    /// </summary>
    public delegate string ScriptwordFunction(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptword scriptWord,
            params object[] parameters);

    #endregion

}
