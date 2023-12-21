namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoDefinitionExtensions
    {
        public static T WithChildren<T>(this T parent, params IBdoDefinition[] children)
            where T : IBdoDefinition
        {
            return parent.WithChildren<T, IBdoDefinition>(children);
        }

        public static T AddChildren<T>(this T parent, params IBdoDefinition[] children) where T : IBdoDefinition
        {
            return parent.AddChildren<T, IBdoDefinition>(children);
        }
    }
}