using BindOpen.Meta.Conditions;
using BindOpen.Meta.Items;
using BindOpen.Meta.Specification;
using System.Collections.Generic;

namespace BindOpen.Meta.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaElementSpec : IDataSpecification
    {
        /// <summary>
        /// 
        /// </summary>
        ICondition Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        IBdoMetaElementSpec WithCondition(ICondition condition);

        /// <summary>
        /// 
        /// </summary>
        List<string> Aliases { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliases"></param>
        IBdoMetaElementSpec WithAliases(params string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        List<IBdoMetaElementSpec> SubSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specs"></param>
        IBdoMetaElementSpec WithSubSpecs(params IBdoMetaElementSpec[] specs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoMetaElementSpec GetSubSpec(string name);

        /// <summary>
        /// 
        /// </summary>
        List<DataItemizationMode> AvailableItemizationModes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modes"></param>
        IBdoMetaElementSpec WithAvailableItemizationModes(params DataItemizationMode[] modes);

        /// <summary>
        /// 
        /// </summary>
        IDataConstraintStatement ConstraintStatement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        IBdoMetaElementSpec WithConstraintStatement(IDataConstraintStatement statement);

        /// <summary>
        /// 
        /// </summary>
        bool IsAllocatable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isAllocatable"></param>
        IBdoMetaElementSpec AsAllocatable(bool isAllocatable = true);

        /// <summary>
        /// 
        /// </summary>
        bool IsValueList { get; }

        /// <summary>
        /// 
        /// </summary>
        RequirementLevels ItemRequirementLevel { get; }

        /// <summary>
        /// 
        /// </summary>
        string ItemScript { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        IBdoMetaElementSpec WithItemScript(string script);

        /// <summary>
        /// 
        /// </summary>
        List<SpecificationLevels> ItemSpecificationLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levels"></param>
        IBdoMetaElementSpec WithItemSpecificationLevels(params SpecificationLevels[] levels);

        /// <summary>
        /// 
        /// </summary>
        int MaximumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        IBdoMetaElementSpec WithMaximumItemNumber(int number);

        /// <summary>
        /// 
        /// </summary>
        int MinimumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        IBdoMetaElementSpec WithMinimumItemNumber(int number);

        /// <summary>
        /// 
        /// </summary>
        object DefaultItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        IBdoMetaElementSpec WithDefaultItem(object item);
    }
}