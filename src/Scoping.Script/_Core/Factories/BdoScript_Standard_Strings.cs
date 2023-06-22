namespace BindOpen.System.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static TBdoScriptword<bool> _Text(
            object obj)
            => Func<bool>("text", obj);

        public static TBdoScriptword<bool> _Concat(
            params object[] parameters)
            => Func<bool>("concat", parameters);
    }
}
