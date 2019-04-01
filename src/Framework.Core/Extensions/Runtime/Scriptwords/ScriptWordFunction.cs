using System;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Extensions.Configuration.Scriptwords;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Extensions.Runtime.Scriptwords
{

    // --------------------------------------------------
    // DELEGATES
    // --------------------------------------------------

    #region Delegates

    /// <summary>
    /// This delegate represents a script word function.
    /// </summary>
    public delegate String ScriptWordFunction(
            IAppScope appScope,
            IScriptVariableSet scriptVariableSet,
            IScriptWord scriptWord,
            params object[] parameters);

    #endregion

}
