using BindOpen.Data.Items;
using System.Linq;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IDataSpecificationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        public static IDataSpecification WithDataValueType<T>(
            this T spec,
            DataValueTypes valueType)
            where T : IDataSpecification
        {
            if (spec != null)
            {
                spec.DataValueType = valueType;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        public static IDataSpecification WithAccessibilityLevel<T>(
            this T spec,
            AccessibilityLevels level)
            where T : IDataSpecification
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
        public static IDataSpecification WithInheritanceLevel<T>(
            this T spec,
            InheritanceLevels level)
            where T : IDataSpecification
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
        public static IDataSpecification WithRequirementLevel<T>(
            this T spec,
            RequirementLevels level)
            where T : IDataSpecification
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
        public static IDataSpecification AsOptional<T>(
            this T spec)
            where T : IDataSpecification
        {
            if (spec != null)
            {
                spec.WithRequirementLevel(RequirementLevels.Optional);
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IDataSpecification AsRequired<T>(
            this T spec)
            where T : IDataSpecification
        {
            if (spec != null)
            {
                spec.WithRequirementLevel(RequirementLevels.Required);
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IDataSpecification AsForbidden<T>(
            this T spec)
            where T : IDataSpecification
        {
            if (spec != null)
            {
                spec.WithRequirementLevel(RequirementLevels.Forbidden);
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="script"></param>
        public static IDataSpecification WithRequirementScript<T>(
            this T spec,
            IBdoExpression exp)
            where T : IDataSpecification
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
            where T : IDataSpecification
        {
            if (spec != null)
            {
                spec.SpecificationLevels = levels?.ToList();
            }

            return spec;
        }
    }
}
