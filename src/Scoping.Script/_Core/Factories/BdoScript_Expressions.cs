using BindOpen.Data;
using System;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static BdoExpression NewExpression<T>(Func<ITBdoScriptword<T>, IBdoScriptword> func)
        {
            var word = func?.Invoke(This<T>());

            return new()
            {
                Kind = BdoExpressionKind.Word,
                Word = word
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public static BdoExpression NewExp<T>(Func<ITBdoScriptword<T>, IBdoScriptword> func)
            => NewExpression(func);
    }
}
