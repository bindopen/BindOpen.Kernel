using BindOpen.Data.Conditions;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using System.Collections.Generic;

namespace BindOpen.Data.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoElementSpec : IDataSpecification
    {
        /// <summary>
        /// 
        /// </summary>
        ICondition Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        IBdoElementSpec WithCondition(ICondition condition);

        /// <summary>
        /// 
        /// </summary>
        List<string> Aliases { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliases"></param>
        IBdoElementSpec WithAliases(params string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        List<IBdoElementSpec> SubSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specs"></param>
        IBdoElementSpec WithSubSpecs(params IBdoElementSpec[] specs);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoElementSpec GetSubSpec(string name);

        /// <summary>
        /// 
        /// </summary>
        List<DataItemizationMode> AvailableItemizationModes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modes"></param>
        IBdoElementSpec WithAvailableItemizationModes(params DataItemizationMode[] modes);

        /// <summary>
        /// 
        /// </summary>
        IDataConstraintStatement ConstraintStatement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        IBdoElementSpec WithConstraintStatement(IDataConstraintStatement statement);

        /// <summary>
        /// 
        /// </summary>
        bool IsAllocatable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isAllocatable"></param>
        IBdoElementSpec AsAllocatable(bool isAllocatable = true);

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
        /// <param name="level"></param>
        IBdoElementSpec WithItemScript(string script);

        /// <summary>
        /// 
        /// </summary>
        List<SpecificationLevels> ItemSpecificationLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levels"></param>
        IBdoElementSpec WithItemSpecificationLevels(params SpecificationLevels[] levels);

        /// <summary>
        /// 
        /// </summary>
        int MaximumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        IBdoElementSpec WithMaximumItemNumber(int number);

        /// <summary>
        /// 
        /// </summary>
        int MinimumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        IBdoElementSpec WithMinimumItemNumber(int number);

        /// <summary>
        /// 
        /// </summary>
        object DefaultItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        IBdoElementSpec WithDefaultItem(object item);
    }
}