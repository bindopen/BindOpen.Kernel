namespace BindOpen.Kernel.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static TBdoScriptword<bool> _And(
            params object[] parameters)
            => Func<bool>("and", parameters);

        public static TBdoScriptword<bool> _Or(
            params object[] parameters)
            => Func<bool>("or", parameters);

        public static TBdoScriptword<bool> _Not(
            object obj)
            => Func<bool>("not", obj);

        public static TBdoScriptword<bool> _Xor(
            params object[] parameters)
            => Func<bool>("xor", parameters);

        public static TBdoScriptword<bool> _Eq(
            this object obj1,
            object obj2)
            => Func<bool>("eq", obj1, obj2);

        public static TBdoScriptword<bool> _Ne(
            this object obj1,
            object obj2)
            => Func<bool>("ne", obj1, obj2);

        public static TBdoScriptword<bool> _Gt(
            this object obj1,
            object obj2)
            => Func<bool>("gt", obj1, obj2);

        public static TBdoScriptword<bool> _Gte(
            this object obj1,
            object obj2)
            => Func<bool>("gte", obj1, obj2);

        public static TBdoScriptword<bool> _Lt(
            this object obj1,
            object obj2)
            => Func<bool>("lt", obj1, obj2);

        public static TBdoScriptword<bool> _Lte(
            this object obj1,
            object obj2)
            => Func<bool>("lte", obj1, obj2);
    }
}
