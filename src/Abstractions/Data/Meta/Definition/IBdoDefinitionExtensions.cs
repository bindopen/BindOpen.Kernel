using System.Collections.Generic;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoDefinitionExtensions
    {
        public static T WithChildren<T>(this T log, params IBdoDefinition[] children) where T : IBdoDefinition
        {
            if (log != null)
            {
                log._Children = children;
            }

            return log;
        }
        public static T AddChildren<T>(this T log, params IBdoDefinition[] children) where T : IBdoDefinition
        {
            if (log != null)
            {
                log._Children ??= new List<IBdoDefinition>();
                foreach (var child in children)
                {
                    log._Children.Add(child);
                }
            }

            return log;
        }

        public static T WithParent<T>(this T log, IBdoDefinition parent) where T : IBdoDefinition
        {
            if (log != null)
            {
                log.Parent = parent;
            }

            return log;
        }
    }
}