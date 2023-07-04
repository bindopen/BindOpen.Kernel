namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This interface defines a configuration.
    /// </summary>
    public interface IBdoConfiguration :
        IBdoMetaObject,
        IBdoTitled, IBdoDescribed,
        IDated, IBdoDefinable, IBdoUsing
    {
    }
}