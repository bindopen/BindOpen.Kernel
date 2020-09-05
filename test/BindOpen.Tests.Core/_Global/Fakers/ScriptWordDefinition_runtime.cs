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
        /// Evaluates the script word $Constant.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Var_Constant()
        {
            return "const";
        }

        /// <summary>
        /// Evaluates the script word $TEXT.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Text(BdoScriptwordFunctionScope variable)
        {
            return variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_IsEqual(BdoScriptwordFunctionScope variable)
        {
            string value1 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string value2 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();

            return value1.Equals(value2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Evaluates the script word $Func1.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Func1(BdoScriptwordFunctionScope variable)
        {
            string value1 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string value2 = variable?.Scriptword?.Parameters?.GetObjectAtIndex(1)?.ToString();

            return value1.Equals(value2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Evaluates the script word $Func2.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Func2(BdoScriptwordFunctionScope variable)
        {
            string value = variable?.Scriptword?.Parameters?.GetObjectAtIndex(0)?.ToString();
            string parentValue = variable?.Scriptword?.Parent?.Item?.ToString();

            return parentValue + ":" + value;
        }

        /// <summary>
        /// Evaluates the script word $Func3.
        /// </summary>
        /// <param name="variable">The script word function variable to consider.</param>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Func3(BdoScriptwordFunctionScope variable)
        {
            return string.Join('-', variable?.Scriptword?.Parameters);
        }

        #endregion
    }
}