namespace BindOpen.Scoping
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class IBdoScopeExtensions
    {
        public static T WithScope<T>(
            this T scoped,
            IBdoScope scope)
            where T : IBdoScoped
        {
            if (scoped != null)
            {
                scoped.Scope = scope;
            }

            return scoped;
        }
    }
}
