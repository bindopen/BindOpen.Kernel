using BindOpen.Application.Scopes;
using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Extensions.Runtime;
using BindOpen.System.Scripting;
using System;
using System.Collections;

namespace BindOpen.Tests.Core.Extensions.Connectors
{
    /// <summary>
    /// This class represents a script word definition.
    /// </summary>
    [BdoScriptwordDefinition]
    public static class ScriptwordDefinition_runtime
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Evaluates the script word $TEXT.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_Text(
            IBdoScope scope,
            IScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            return "\"" + value1 + "\"";
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <param name="scope">The scope to consider.</param>
        /// <param name="scriptVariableSet">The set of variables that can be used for interpretation.</param>
        /// <param name="scriptWord">The script word to evaluate.</param>
        /// <param name="parameters">The parameters to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static string Fun_IsEqual(
            IBdoScope scope,
            IScriptVariableSet scriptVariableSet,
            IBdoScriptword scriptWord,
            params object[] parameters)
        {
            String value1 = parameters.GetStringAtIndex(0);
            String value2 = parameters.GetStringAtIndex(1);

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "true" : "false");
        }

        #endregion
    }
}