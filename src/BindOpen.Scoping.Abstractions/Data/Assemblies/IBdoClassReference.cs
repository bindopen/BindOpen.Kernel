namespace BindOpen.Scoping.Data.Assemblies
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoClassReference : IBdoAssemblyReference
    {
        /// <summary>
        /// The library name of this instance.
        /// </summary>
        string ClassName { get; }

        bool IsCompatibleWith(IBdoClassReference reference);
    }
}