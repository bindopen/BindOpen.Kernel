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
        public static BdoCondition NewCondition<T>(Func<TBdoScriptword<T>, IBdoScriptword> func)
        {
            var word = func?.Invoke(This<T>());

            return BdoData.NewCondition(BdoData.NewExp(word));
        }

        public static T WithCondition<T, TThis>(
            this T obj,
            Func<TBdoScriptword<TThis>, IBdoScriptword> func)
            where T : IBdoConditional
        {
            var condition = NewCondition(func);

            return obj.WithCondition(condition);
        }

        public static TBdoScriptword<bool> And(
            params object[] parameters)
            => Func<bool>("and", parameters);

        public static TBdoScriptword<bool> Or(
            params object[] parameters)
            => Func<bool>("or", parameters);

        public static TBdoScriptword<bool> Not(
            object obj)
            => Func<bool>("not", obj);

        public static TBdoScriptword<bool> Xor(
            params object[] parameters)
            => Func<bool>("xor", parameters);

        public static TBdoScriptword<bool> Eq(
            this object obj1,
            object obj2)
            => Func<bool>("eq", obj1, obj2);

        public static TBdoScriptword<bool> Ne(
            this object obj1,
            object obj2)
            => Func<bool>("ne", obj1, obj2);

        public static TBdoScriptword<bool> Gt(
            this object obj1,
            object obj2)
            => Func<bool>("gt", obj1, obj2);

        public static TBdoScriptword<bool> Gte(
            this object obj1,
            object obj2)
            => Func<bool>("gte", obj1, obj2);

        public static TBdoScriptword<bool> Lt(
            this object obj1,
            object obj2)
            => Func<bool>("lt", obj1, obj2);

        public static TBdoScriptword<bool> Lte(
            this object obj1,
            object obj2)
            => Func<bool>("lte", obj1, obj2);
    }
}
