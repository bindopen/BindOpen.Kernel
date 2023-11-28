namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
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
