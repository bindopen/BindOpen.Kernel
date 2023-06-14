namespace BindOpen.System.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoReferenceCondition : IBdoCondition
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoReference Reference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="expression"></param>
        IBdoReferenceCondition WithReference(IBdoReference reference);
    }
}