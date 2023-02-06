using BindOpen.Data;
using BindOpen.Extensions;
using BindOpen.Extensions.Scripting;
using System;

namespace BindOpen.Tests.Runtime
{
    /// <summary>
    /// This class represents a script word definition fake.
    /// </summary>
    [BdoScriptwordDefinition]
    public static class ScriptWordDefinitionFake
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        /// <summary>
        /// Evaluates the script word $Constant.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(
            Name = "CONSTANT",
            Kind = ScriptItemKinds.Variable,
            Description = "Returns the test constant.",
            CreationDate = "2016-09-14")]
        public static object Var_Constant()
        {
            return "const";
        }

        /// <summary>
        /// Evaluates the script word $TEXT.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(
            Name = "Func1",
            Kind = ScriptItemKinds.Function,
            Description = "Test function 1.",
            CreationDate = "2022-06-24")]
        public static object Fun_Func1(IBdoScriptwordDomain domain)
        {
            return domain?.Scriptword?.Parameters?.Get(0, q => q?.ToString());
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(
            Name = "Func2",
            Kind = ScriptItemKinds.Function,
            Description = "Test function 2.",
            CreationDate = "2022-06-24",
            Parameter1Name = "value1", Parameter1ValueType = DataValueTypes.Text,
            Parameter2Name = "value2", Parameter2ValueType = DataValueTypes.Text)]
        public static object Fun_Func2(IBdoScriptwordDomain domain)
        {
            string value1 = domain?.Scriptword?.Parameters.GetString(0);
            string value2 = domain?.Scriptword?.Parameters.GetString(1);

            return value1.Equals(value2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Evaluates the script word $Func1.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword(
            Name = "Func3",
            Kind = ScriptItemKinds.Function,
            Description = "Test function 3.",
            CreationDate = "2022-06-24",
            Parameter1Name = "value1", Parameter1ValueType = DataValueTypes.Object,
            Parameter2Name = "value2", Parameter2ValueType = DataValueTypes.Object)]
        public static object Fun_Func3(object value1, object value2)
        {
            return value1?.ToString() == value2?.ToString();
        }

        /// <summary>
        /// Evaluates the script word $Func2.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoScriptword]
        public static object Fun_Func4(IBdoScriptwordDomain domain)
        {
            string value = domain?.Scriptword?.Parameters?.Get(0, q => q.ToString());
            string parentValue = domain?.Scriptword?.Parent?.Item?.ToString();

            return parentValue + ":" + value;
        }

        /// <summary>
        /// Evaluates the script word $Func5.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        public static object Fun_Func5(
            IBdoScriptwordDomain domain,
            object value1,
            params string[] values)
        {
            string parentValueText = domain?.Scriptword?.Parent?.Item?.ToString();
            string valueText = value1?.ToString();
            string valuesText = string.Join('-', values);

            return parentValueText + ":" + valueText + ":" + valuesText;
        }

        #endregion
    }
}