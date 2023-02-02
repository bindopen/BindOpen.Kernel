using BindOpen.Data.Conditions;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaSpec : IDataSpecification
    {
        /// <summary>
        /// 
        /// </summary>
        ICondition Condition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<string> Aliases { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<IBdoMetaSpec> SubSpecs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IBdoMetaSpec GetSubSpec(string name);

        /// <summary>
        /// 
        /// </summary>
        List<DataItemizationMode> ItemizationModes { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IDataConstraintStatement ConstraintStatement { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsAllocatable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        RequirementLevels ItemRequirementLevel { get; }

        /// <summary>
        /// 
        /// </summary>
        IBdoExpression ItemExpression { get; set; }

        /// <summary>
        /// 
        /// </summary>
        List<SpecificationLevels> ItemSpecificationLevels { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int MaximumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        int MinimumItemNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        object DefaultItem { get; set; }
    }
}