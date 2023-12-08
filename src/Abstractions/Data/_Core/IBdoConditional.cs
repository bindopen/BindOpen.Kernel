using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Data
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

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        bool GetConditionValue(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}