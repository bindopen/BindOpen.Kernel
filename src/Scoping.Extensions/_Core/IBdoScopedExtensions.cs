namespace BindOpen.Scoping
{
    /// <summary>
    /// This static class provides methods to create extension items.
    /// </summary>
    public static class IBdoScopedExtensions
    {
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
