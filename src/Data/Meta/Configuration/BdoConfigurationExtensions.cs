namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoConfigurationExtensions
    {
        public static T WithChildren<T>(this T log, params IBdoConfiguration[] children) where T : IBdoConfiguration
        {
            if (log != null)
            {
                log._Children = BdoData.NewSet(children);
            }

            return log;
        }

        public static T AddChildren<T>(this T log, params IBdoConfiguration[] children) where T : IBdoConfiguration
        {
            if (log != null)
            {
                log._Children ??= BdoData.NewSet<IBdoConfiguration>();
                foreach (var child in children)
                {
                    log._Children.Add(child);
                }
            }

            return log;
        }

    }
}