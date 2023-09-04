namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoDefinitionExtensions
    {
        public static T WithChildren<T>(this T log, params IBdoDefinition[] children) where T : IBdoDefinition
        {
            if (log != null)
            {
                log._Children = BdoData.NewItemSet(children);
            }

            return log;
        }
        public static T AddChildren<T>(this T log, params IBdoDefinition[] children) where T : IBdoDefinition
        {
            if (log != null)
            {
                log._Children ??= BdoData.NewItemSet<IBdoDefinition>();
                foreach (var child in children)
                {
                    log._Children.Add(child);
                }
            }

            return log;
        }
    }
}