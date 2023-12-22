namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoConfigurationExtensions
    {
        public static T WithChildren<T>(this T parent, params IBdoConfiguration[] children) where T : IBdoConfiguration
        {
            return parent.WithChildren<T, IBdoConfiguration>(children);
        }

        public static T AddChildren<T>(this T parent, params IBdoConfiguration[] children) where T : IBdoConfiguration
        {
            return parent.AddChildren<T, IBdoConfiguration>(children);
        }
    }
}