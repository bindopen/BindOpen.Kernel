namespace BindOpen.Kernel.Scoping
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

        public static T CreateScoped<T>(
            this IBdoScope scope)
            where T : IBdoScoped, new()
        {
            T scoped = default;

            if (scope != null)
            {
                scoped = new()
                {
                    Scope = scope
                };
            }

            return scoped;
        }
    }
}
