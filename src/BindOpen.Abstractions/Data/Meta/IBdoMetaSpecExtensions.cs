using BindOpen.Data.Conditions;
using BindOpen.Data.Items;
using BindOpen.Data.Specification;
using System.Linq;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoMetaSpecExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modes"></param>
        public static T WithItemizationModes<T>(
            this T metaSpec,
            params DataItemizationMode[] modes)
            where T : IBdoMetaSpec
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
            where T : IBdoMetaSpec
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
            where T : IBdoMetaSpec
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
            where T : IBdoMetaSpec
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
            where T : IBdoMetaSpec
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
            params IBdoMetaSpec[] specs)
            where T : IBdoMetaSpec
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
            IDataConstraintStatement statement)
            where T : IBdoMetaSpec
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
            where T : IBdoMetaSpec
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
            where T : IBdoMetaSpec
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
            int number)
            where T : IBdoMetaSpec
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
            int number)
            where T : IBdoMetaSpec
        {
            if (metaSpec != null)
            {
                metaSpec.MinimumItemNumber = number;
            }
            return metaSpec;
        }
    }
}
