namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoCompositeSpecExtensions
    {
        public static T WithChildren<T>(this T log, params IBdoSpec[] children) where T : IBdoCompositeSpec
        {
            if (log != null)
            {
                log._Children = children;
            }

            return log;
        }
    }
}
