using System.Linq;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoAggregateSpecExtensions
    {
        public static T WithChildren<T>(this T log, params IBdoSpec[] children) where T : IBdoAggregateSpec
        {
            if (log != null)
            {
                log._Children = BdoData.NewSet(children?.Any() == true ? children : null);
            }

            return log;
        }

        public static T AddChildren<T>(this T log, params IBdoSpec[] children) where T : IBdoAggregateSpec
        {
            if (log != null)
            {
                log._Children ??= BdoData.NewSet<IBdoSpec>();
                foreach (var child in children)
                {
                    log._Children.Add(child);
                }
            }

            return log;
        }
    }
}
