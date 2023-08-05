namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This interface defines a configuration.
    /// </summary>
    public interface IBdoConfiguration :
        IBdoMetaSet,
        IBdoTitled, IBdoDescribed,
        IDated, IBdoUsing
    {
    }
}