using BindOpen.System.Scoping.Script;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Functions;
using System;

namespace BindOpen.System.Tests.Scoping
{
    /// <summary>
    /// This class represents a script word definition fake.
    /// </summary>
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
        [BdoFunction(
            Name = "text",
            Description = "Returns the test constant.",
            CreationDate = "2016-09-14")]
        public static string Var_Text(object st)
        {
            return st?.ToString();
        }

        /// <summary>
        /// The boolean value of this instance.
        /// </summary>
        [BdoFunction(Name = "eq")]
        public static bool GetTest(
            [BdoParameter(Title = "String value")]
            object obj1,
            [BdoParameter(Title = "Integer value")]
            object obj2)
            => obj1?.ToString().Equals(obj2?.ToString(), StringComparison.OrdinalIgnoreCase) == true;

        /// <summary>
        /// Evaluates the script word $TEXT.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(
            Name = "Func1",
            Description = "Test function 1.",
            CreationDate = "2022-06-24")]
        public static object Fun_Func1(IBdoScriptDomain domain)
        {
            return domain?.Scriptword.GetData<string>(0);
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(
            Name = "subFun",
            Description = "Test function 2.",
            CreationDate = "2022-06-24")]
        public static object Fun_Func2a(
            this object obj1,
            string st,
            IBdoScriptDomain domain)
        {
            return Concat(domain, obj1, st);
        }

        /// <summary>
        /// Evaluates the script word $ISEQUAL.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(
            Name = "Func2",
            Description = "Test function 2.",
            CreationDate = "2022-06-24")]
        public static object Fun_Func2b(IBdoScriptDomain domain)
        {
            string value1 = domain?.Scriptword.GetData<string>(0);
            string value2 = domain?.Scriptword.GetData<string>(1);

            return value1.Equals(value2, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Evaluates the script word $Func1.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(
            Name = "Func3",
            CreationDate = "2022-06-24")]
        public static object Fun_Func3(
            [BdoParameter(Name = "Value1")] object value1,
            [BdoParameter(Name = "Value2")] object value2)
        {
            return value1?.ToString() == value2?.ToString();
        }

        /// <summary>
        /// Evaluates the script word $Func2.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction]
        public static object Fun_Func4(IBdoScriptDomain domain)
        {
            string value = domain?.Scriptword.GetData<string>(0);
            string parentValue = domain?.Scriptword?.Parent.GetData<string>();

            return parentValue + ":" + value;
        }

        /// <summary>
        /// Evaluates the script word $Func5.
        /// </summary>
        /// <returns>The interpreted string value.</returns>
        [BdoFunction(Name = "concat")]
        public static object Concat(
            IBdoScriptDomain domain,
            params object[] values)
        {
            string parentValueText = domain?.Scriptword?.Parent?.GetData<string>();
            string valuesText = string.Join('-', values);

            return valuesText;
        }

        #endregion
    }
}