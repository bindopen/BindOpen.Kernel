﻿using BindOpen.Logging;
using BindOpen.Scoping;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoBaseSpec :
        IBdoObject, IReferenced, IBdoDataTyped, IBdoConditional, IGrouped,
        IIdentified, INamed, IIndexed, IBdoReferenced,
        IBdoTitled, IBdoDescribed, IBdoDetailed,
        ITBdoSet<IBdoSpecRule>,
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