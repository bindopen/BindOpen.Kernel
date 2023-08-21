namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoBasicCondition : IBdoCondition
    {
        /// <summary>
        /// 
        /// </summary>
        object Argument1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object Argument2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataOperators Operator { get; set; }
    }
}