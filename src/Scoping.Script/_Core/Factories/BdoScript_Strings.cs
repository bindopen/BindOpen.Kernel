namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This static class provides methods to create script elements.
    /// </summary>
    public static partial class BdoScript
    {
        public static ITBdoScriptword<bool> Text(
            object obj)
            => Func<bool>("text", obj);

        public static ITBdoScriptword<bool> Concat(
            params object[] parameters)
            => Func<bool>("concat", parameters);
    }
}
