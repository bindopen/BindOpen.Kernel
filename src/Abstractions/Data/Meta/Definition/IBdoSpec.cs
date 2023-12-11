using BindOpen.Kernel.Data;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System.Collections.Generic;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoSpec :
        IBdoObject, IReferenced, IBdoDataTyped, IBdoConditional, IGrouped,
        IIdentified, INamed, IIndexed, IBdoReferenced,
        IBdoTitled, IBdoDescribed, IBdoDetailed,
        ITBdoSet<IBdoSpecRule>,
        ITTreeNode<IBdoSpec>, ITGroup<IBdoSpec>,
        IUpdatable
    {
        /// <summary>
        /// 
        /// </summary>
        IList<string> Aliases { get; set; }

        /// <summary>
        /// 
        /// </summary>
        uint MinDataItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        uint? MaxDataItemNumber { get; set; }

        // Data

        /// <summary>
        /// The label of this instance.
        /// </summary>
        string Label { get; set; }

        // Levels

        /// <summary>
        /// 
        /// </summary>
        IList<DataMode> AvailableDataModes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        AccessibilityLevels AccessibilityLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        InheritanceLevels InheritanceLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object DefaultData { get; set; }

        IBdoSpecRule Get(
            string groupId,
            BdoSpecRuleKinds ruleKind = BdoSpecRuleKinds.Requirement,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

        object GetValue(
            string groupId,
            BdoSpecRuleKinds ruleKind = BdoSpecRuleKinds.Requirement,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);
    }
}