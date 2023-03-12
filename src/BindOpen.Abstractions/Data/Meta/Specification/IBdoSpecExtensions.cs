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
        /// <param key="modes"></param>
        public static T WithValueModes<T>(
            this T spec,
            params DataMode[] modes)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.ValueModes = modes?.ToList();
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="script"></param>
        public static T WithDataReference<T>(
            this T spec,
            IBdoExpression exp)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.DataReference = exp;
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="levels"></param>
        public static T WithItemSpecificationLevels<T>(
            this T spec,
            params SpecificationLevels[] levels)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.DataSpecificationLevels = levels?.ToList();
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="aliases"></param>
        public static T WithAliases<T>(
            this T spec,
            params string[] aliases)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.Aliases = aliases?.ToList();
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="specs"></param>
        public static T WithSubSpecs<T>(
            this T spec,
            params IBdoSpec[] specs)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.SubSpecs = specs?.ToList();
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isAllocatable"></param>
        public static T AsAllocatable<T>(
            this T spec,
            bool isAllocatable = true)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.IsAllocatable = isAllocatable;
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="item"></param>
        public static T WithDefaultItem<T>(
            this T spec,
            object item)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.DefaultData = item;
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="number"></param>
        public static T WithMaximumItemNumber<T>(
            this T spec,
            uint? number = null)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.MaximumItemNumber = number;
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="number"></param>
        public static T WithMinimumItemNumber<T>(
            this T spec,
            uint number)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.MinimumItemNumber = number;
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="number"></param>
        public static T WithDataValueType<T>(
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
        /// <param key="level"></param>
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
        /// <param key="level"></param>
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
        /// <param key="level"></param>
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
        /// <param key="script"></param>
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
        /// <param key="levels"></param>
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
