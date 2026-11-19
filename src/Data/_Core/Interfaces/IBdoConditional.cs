using BindOpen.Data.Conditions;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface defines an object that can be conditional.
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