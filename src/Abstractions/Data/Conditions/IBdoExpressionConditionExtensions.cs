namespace BindOpen.Kernel.Data.Conditions
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class IBdoExpressionConditionExtensions
    {
        public static T WithExpression<T>(
            this T obj,
            IBdoExpression exp)
            where T : IBdoExpressionCondition
        {
            if (obj != null)
            {
                obj.Expression = exp;
            }

            return obj;
        }
    }
}