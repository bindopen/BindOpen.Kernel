using BindOpen.System.Data.Conditions;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This static class provides methods to handle conditions.
    /// </summary>
    public static class IBdoBasicConditionExtensions
    {
        public static T WithArgument1<T>(
            this T obj,
            object argument1)
            where T : IBdoBasicCondition
        {
            if (obj != null)
            {
                obj.Argument1 = argument1;
            }

            return obj;
        }

        public static T WithArgument2<T>(
            this T obj,
            object argument1)
            where T : IBdoBasicCondition
        {
            if (obj != null)
            {
                obj.Argument2 = argument1;
            }

            return obj;
        }

        public static T WithOperator<T>(
            this T obj,
            DataOperators op)
            where T : IBdoBasicCondition
        {
            if (obj != null)
            {
                obj.Operator = op;
            }

            return obj;
        }
    }
}