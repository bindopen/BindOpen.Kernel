namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoSpecifiedExtensions
    {
        public static T WithSpec<T>(
            this T obj,
            IBdoNodeSpec spec)
            where T : IBdoSpecified
        {
            if (obj != null)
            {
                obj.Spec = spec;
            }

            return obj;
        }
    }
}
