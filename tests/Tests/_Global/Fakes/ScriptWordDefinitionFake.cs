using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Scoping;
using BindOpen.Kernel.Scoping.Script;
using System;

namespace BindOpen.Kernel.Tests
{
    /// <summary>
    /// This class represents a script word definition fake.
    /// </summary>
    public static class ScriptWordDefinitionFake
    {
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
        [BdoFunction(Name = "concatTest")]
        public static object Concat(
            IBdoScriptDomain domain,
            params object[] values)
        {
            string parentValueText = domain?.Scriptword?.Parent?.GetData<string>();
            string valuesText = string.Join('-', values);

            return valuesText;
        }
    }
}