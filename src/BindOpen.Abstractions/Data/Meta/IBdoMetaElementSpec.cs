using BindOpen.Data.Conditions;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaDataSpec : IDataSpecification
    {
        /// <summary>
        /// 
        /// </summary>
        ICondition Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        IBdoMetaDataSpec WithCondition(ICondition condition);

        /// <summary>
        /// 
        /// </summary>
        List<string> Aliases { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliases"></param>
        IBdoMetaDataSpec WithAliases(params string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        List<IBdoMetaDataSpec> SubSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specs"></param>
        IBdoMetaDataSpec WithSubSpecs(params IBdoMetaDataSpec[] specs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoMetaDataSpec GetSubSpec(string name);

        /// <summary>
        /// 
        /// </summary>
        List<DataItemizationMode> AvailableItemizationModes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modes"></param>
        IBdoMetaDataSpec WithAvailableItemizationModes(params DataItemizationMode[] modes);

        /// <summary>
        /// 
        /// </summary>
        IDataConstraintStatement ConstraintStatement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        IBdoMetaDataSpec WithConstraintStatement(IDataConstraintStatement statement);

        /// <summary>
        /// 
        /// </summary>
        bool IsAllocatable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isAllocatable"></param>
        IBdoMetaDataSpec AsAllocatable(bool isAllocatable = true);

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
        IBdoMetaDataSpec WithItemScript(string script);

        /// <summary>
        /// 
        /// </summary>
        List<SpecificationLevels> ItemSpecificationLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levels"></param>
        IBdoMetaDataSpec WithItemSpecificationLevels(params SpecificationLevels[] levels);

        /// <summary>
        /// 
        /// </summary>
        int MaximumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        IBdoMetaDataSpec WithMaximumItemNumber(int number);

        /// <summary>
        /// 
        /// </summary>
        int MinimumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        IBdoMetaDataSpec WithMinimumItemNumber(int number);

        /// <summary>
        /// 
        /// </summary>
        object DefaultItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        IBdoMetaDataSpec WithDefaultItem(object item);
    }
}