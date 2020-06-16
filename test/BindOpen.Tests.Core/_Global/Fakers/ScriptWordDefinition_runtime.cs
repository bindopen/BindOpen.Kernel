using BindOpen.Data.Helpers.Objects;
using BindOpen.Extensions.Runtime;
using System;

namespace BindOpen.Tests.Core.Fakers
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
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Text(BdoScriptwordFunctionVariable variable)
        {
            return variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_IsEqual(BdoScriptwordFunctionVariable variable)
        {
            string value1 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string value2 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();

            return (value1.Equals(value2, StringComparison.OrdinalIgnoreCase) ? "true" : "false");
        }

        #endregion
    }
}