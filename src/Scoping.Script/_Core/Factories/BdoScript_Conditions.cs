using BindOpen.Data;
using BindOpen.Data.Conditions;
using System;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        /// <summary>
        /// Creates the exp.
        /// </summary>
        /// <param key="word">The word of exp to consider.</param>
        /// <returns>Returns the created exp.</returns>
        public static IBdoCondition NewCondition<T>(Func<ITBdoScriptword<T>, IBdoScriptword> func)
        {
            var word = func?.Invoke(This<T>());

            return BdoData.NewCondition(word);
        }

        public static T WithCondition<T, TThis>(
            this T obj,
            Func<ITBdoScriptword<TThis>, IBdoScriptword> func)
            where T : IBdoConditional
        {
            var condition = NewCondition(func);

            return obj.WithCondition(condition);
        }

        public static ITBdoScriptword<bool> And(
            params object[] parameters)
            => Func<bool>("and", parameters);

        public static ITBdoScriptword<bool> Or(
            params object[] parameters)
            => Func<bool>("or", parameters);

        public static ITBdoScriptword<bool> Not(
            object obj)
            => Func<bool>("not", obj);

        public static ITBdoScriptword<bool> Xor(
            params object[] parameters)
            => Func<bool>("xor", parameters);

        public static ITBdoScriptword<bool> Eq(
            this object obj1,
            object obj2)
            => Func<bool>("eq", obj1, obj2);

        public static ITBdoScriptword<bool> Ne(
            this object obj1,
            object obj2)
            => Func<bool>("ne", obj1, obj2);

        public static ITBdoScriptword<bool> Gt(
            this object obj1,
            object obj2)
            => Func<bool>("gt", obj1, obj2);

        public static ITBdoScriptword<bool> Gte(
            this object obj1,
            object obj2)
            => Func<bool>("gte", obj1, obj2);

        public static ITBdoScriptword<bool> Lt(
            this object obj1,
            object obj2)
            => Func<bool>("lt", obj1, obj2);

        public static ITBdoScriptword<bool> Lte(
            this object obj1,
            object obj2)
            => Func<bool>("lte", obj1, obj2);
    }
}
