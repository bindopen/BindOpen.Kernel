namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This interface defines a configuration.
    /// </summary>
    public interface IBdoDefinition :
        ITBdoSet<IBdoSpec>,
        IBdoTitled, IBdoDescribed,
        INamed,
        IIndexed, IStorable, IBdoUsing
    {
    }
}