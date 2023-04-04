using BindOpen.Scopes;
using BindOpen.Data.Meta;

namespace BindOpen.Script
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IBdoScriptDomainExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithScope<T>(
            this T function,
            IBdoScope scope)
            where T : IBdoScriptDomain
        {
            if (function != null)
            {
                function.Scope = scope;
            }

            return function;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithVariableSet<T>(
            this T function,
            IBdoMetaSet variableSet)
            where T : IBdoScriptDomain
        {
            if (function != null)
            {
                function.VariableSet = variableSet;
            }

            return function;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithScriptword<T>(
            this T function,
            IBdoScriptword word)
            where T : IBdoScriptDomain
        {
            if (function != null)
            {
                function.Scriptword = word;
            }

            return function;
        }
    }
}