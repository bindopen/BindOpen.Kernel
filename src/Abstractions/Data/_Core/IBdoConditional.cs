using BindOpen.System.Data.Conditions;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This interface represents a conditional object.
    /// </summary>
    public interface IBdoConditional
    {
        /// <summary>
        /// The condition of this object.
        /// </summary>
        IBdoCondition Condition { get; set; }
    }
}