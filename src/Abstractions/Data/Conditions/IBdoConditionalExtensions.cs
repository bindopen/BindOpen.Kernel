namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class IBdoConditionalExtensions
    {
        public static T WithCondition<T>(
            this T obj,
            IBdoCondition condition)
            where T : IBdoConditional
        {
            if (obj != null)
            {
                obj.Condition = condition;
            }

            return obj;
        }
    }
}