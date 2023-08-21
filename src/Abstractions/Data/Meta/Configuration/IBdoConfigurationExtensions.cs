namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents an accessibility level extension.
    /// </summary>
    public static class IBdoConfigurationExtensions
    {
        public static T WithParent<T>(this T log, IBdoConfiguration parent) where T : IBdoConfiguration
        {
            if (log != null)
            {
                log.Parent = parent;
            }

            return log;
        }
    }
}
