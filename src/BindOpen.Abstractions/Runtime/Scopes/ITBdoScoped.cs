namespace BindOpen.Runtime.Scopes
{
    /// <summary>
    /// This interfaces represents a scoped service.
    /// </summary>
    public interface ITBdoScoped<T> : IBdoScoped where T : IBdoScoped
    {
        /// <summary>
        /// Sets the specified scope.
        /// </summary>
        /// <param key="scope">The scope to consider.</param>
        T WithScope(IBdoScope scope)
        {
            Scope = scope;
            return (T)this;
        }

    }
}