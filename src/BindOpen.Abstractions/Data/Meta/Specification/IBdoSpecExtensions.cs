using BindOpen.Data.Conditions;
using BindOpen.Data.Configuration;
using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoSpecExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modes"></param>
        public static T WithItemizationModes<T>(
            this T metaSpec,
            params DataItemizationMode[] modes)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.ItemizationModes = modes?.ToList();
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        public static T WithItemExpression<T>(
            this T metaSpec,
            IBdoExpression exp)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.ItemExpression = exp;
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levels"></param>
        public static T WithItemSpecificationLevels<T>(
            this T metaSpec,
            params SpecificationLevels[] levels)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.ItemSpecificationLevels = levels?.ToList();
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        public static T WithCondition<T>(
            this T metaSpec,
            ICondition condition)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.Condition = condition;
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aliases"></param>
        public static T WithAliases<T>(
            this T metaSpec,
            params string[] aliases)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.Aliases = aliases?.ToList();
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specs"></param>
        public static T WithSubSpecs<T>(
            this T metaSpec,
            params IBdoSpec[] specs)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.SubSpecs = specs?.ToList();
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statement"></param>
        public static T WithConstraintStatement<T>(
            this T metaSpec,
            IBdoConfigurationSet statement)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.ConstraintStatement = statement;
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isAllocatable"></param>
        public static T AsAllocatable<T>(
            this T metaSpec,
            bool isAllocatable = true)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.IsAllocatable = isAllocatable;
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public static T WithDefaultItem<T>(
            this T metaSpec,
            object item)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.DefaultItem = item;
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        public static T WithMaximumItemNumber<T>(
            this T metaSpec,
            uint? number = null)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.MaximumItemNumber = number;
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        public static T WithMinimumItemNumber<T>(
            this T metaSpec,
            uint number)
            where T : IBdoSpec
        {
            if (metaSpec != null)
            {
                metaSpec.MinimumItemNumber = number;
            }
            return metaSpec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        public static T WithValueType<T>(
            this T spec,
            DataValueTypes valueType)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.ValueType = valueType;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public static T WithAccessibilityLevel<T>(
            this T spec,
            AccessibilityLevels level)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.AccessibilityLevel = level;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public static T WithInheritanceLevel<T>(
            this T spec,
            InheritanceLevels level)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.InheritanceLevel = level;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public static T WithRequirementLevel<T>(
            this T spec,
            RequirementLevels level)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.RequirementLevel = level;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T TAsOptional<T>(
            this T spec)
            where T : IBdoSpec
        {
            spec?.WithRequirementLevel(RequirementLevels.Optional);

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T AsRequired<T>(
            this T spec)
            where T : IBdoSpec
        {
            spec?.WithRequirementLevel(RequirementLevels.Required);

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T AsForbidden<T>(
            this T spec)
            where T : IBdoSpec
        {
            spec?.WithRequirementLevel(RequirementLevels.Forbidden);

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        public static T WithRequirementScript<T>(
            this T spec,
            IBdoExpression exp)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.RequirementExpression = exp;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="levels"></param>
        public static T WithSpecificationLevels<T>(
            this T spec,
            params SpecificationLevels[] levels)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.SpecificationLevels = levels?.ToList();
            }

            return spec;
        }
    }
}
