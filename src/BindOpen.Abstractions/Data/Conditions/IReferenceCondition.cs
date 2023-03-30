namespace BindOpen.Data.Conditions
{
    /// <summary>
    /// 
    /// </summary>
    public interface IReferenceCondition : ICondition
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoReference Reference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="expression"></param>
        IReferenceCondition WithReference(IBdoReference reference);
    }
}