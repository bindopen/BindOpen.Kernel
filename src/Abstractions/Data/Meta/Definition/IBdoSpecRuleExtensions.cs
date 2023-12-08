namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoSpecRuleExtensions
    {
        public static T WithResultCode<T>(
            this T obj,
            string resultCode)
            where T : IBdoSpecRule
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
            where T : IBdoSpecRule
        {
            if (obj != null)
            {
                obj.Value = value;
            }

            return obj;
        }

        public static T WithMode<T>(
            this T obj,
            BdoSpecRuleKinds mode)
            where T : IBdoSpecRule
        {
            if (obj != null)
            {
                obj.Kind = mode;
            }

            return obj;
        }
    }
}
