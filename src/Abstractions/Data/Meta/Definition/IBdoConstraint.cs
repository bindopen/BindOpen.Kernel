namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConstraint : IBdoObject, IIdentified, IReferenced, IBdoConditional, IGrouped
    {
        /// <summary>
        /// The mode.
        /// </summary>
        BdoConstraintModes Mode { get; set; }

        /// <summary>
        /// The result code.
        /// </summary>
        string ResultCode { get; set; }

        /// <summary>
        /// The value.
        /// </summary>
        object Value { get; set; }
    }
}