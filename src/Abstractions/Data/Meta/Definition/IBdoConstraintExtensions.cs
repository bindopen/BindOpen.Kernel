namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoConstraintExtensions
    {
        public static T WithResultCode<T>(
            this T obj,
            string resultCode)
            where T : IBdoConstraint
        {
            if (obj != null)
            {
                obj.ResultCode = resultCode;
            }

            return obj;
        }

        public static T WithValue<T>(
            this T obj,
            object value)
            where T : IBdoConstraint
        {
            if (obj != null)
            {
                obj.Value = value;
            }

            return obj;
        }

        public static T WithMode<T>(
            this T obj,
            BdoConstraintModes mode)
            where T : IBdoConstraint
        {
            if (obj != null)
            {
                obj.Mode = mode;
            }

            return obj;
        }
    }
}
