using BindOpen.Kernel.Data.Conditions;
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
        ITBdoSet<IBdoConstraint>,
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
        /// 
        /// </summary>
        bool IsStatic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsAllocatable { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param key="item"></param>
        /// <returns></returns>
        bool IsCompatibleWithData(object item);

        IBdoConstraint Get(string reference, BdoConstraintModes mode = BdoConstraintModes.Requirement, IBdoScope scope = null, IBdoMetaSet varSet = null, IBdoLog log = null);

        object GetValue(string reference, BdoConstraintModes mode = BdoConstraintModes.Requirement, IBdoScope scope = null, IBdoMetaSet varSet = null, IBdoLog log = null);

        T GetValue<T>(string reference, BdoConstraintModes mode = BdoConstraintModes.Requirement, IBdoScope scope = null, IBdoMetaSet varSet = null, IBdoLog log = null);

        void RemoveOfReference(string reference, bool isRecursive = false);
    }
}