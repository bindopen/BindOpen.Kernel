using BindOpen.Kernel.Logging;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoConstraint : IBdoObject, IIdentified, IReferenced, IBdoConditional, IBdoReferenced, IGrouped
    {
        /// <summary>
        /// The mode.
        /// </summary>
        BdoConstraintModes Mode { get; set; }

        /// <summary>
        /// The result criticality.
        /// </summary>
        EventKinds ResultEventKind { get; set; }

        /// <summary>
        /// The result title.
        /// </summary>
        string ResultTitle { get; set; }

        /// <summary>
        /// The result description.
        /// </summary>
        string ResultDescription { get; set; }

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