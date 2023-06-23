namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static ITBdoScriptword<bool> _And(
            params object[] parameters)
            => Func<bool>("and", parameters);

        public static ITBdoScriptword<bool> _Or(
            params object[] parameters)
            => Func<bool>("or", parameters);

        public static ITBdoScriptword<bool> _Not(
            object obj)
            => Func<bool>("not", obj);

        public static ITBdoScriptword<bool> _Xor(
            params object[] parameters)
            => Func<bool>("xor", parameters);

        public static ITBdoScriptword<bool> _Eq(
            object obj1,
            object obj2)
            => Func<bool>("eq", obj1, obj2);

        public static ITBdoScriptword<bool> _Ne(
            object obj1,
            object obj2)
            => Func<bool>("ne", obj1, obj2);

        public static ITBdoScriptword<bool> _Gt(
            object obj1,
            object obj2)
            => Func<bool>("gt", obj1, obj2);

        public static ITBdoScriptword<bool> _Gte(
            object obj1,
            object obj2)
            => Func<bool>("gte", obj1, obj2);

        public static ITBdoScriptword<bool> _Lt(
            object obj1,
            object obj2)
            => Func<bool>("lt", obj1, obj2);

        public static ITBdoScriptword<bool> _Lte(
            object obj1,
            object obj2)
            => Func<bool>("lte", obj1, obj2);
    }
}
