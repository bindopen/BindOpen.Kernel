using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.System.Data.Meta
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
                spec.ValueModes = modes;
            }
            return spec;
        }

        public static T WithGroupId<T>(
            this T spec,
            string groupId)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.GroupId = groupId;
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
                spec.Aliases = aliases;
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
        /// <param key="isAllocatable"></param>
        public static T AsStatic<T>(
            this T spec,
            bool isStatic = true)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.IsStatic = isStatic;
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="item"></param>
        public static T WithDefaultData<T>(
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
        public static T WithMaxDataItemNumber<T>(
            this T spec,
            uint? number = null)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.MaxDataItemNumber = number;
            }
            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="number"></param>
        public static T WithMinDataItemNumber<T>(
            this T spec,
            uint number)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.MinDataItemNumber = number;
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
        /// <returns></returns>
        public static T TAsOptional<T>(
            this T spec)
            where T : IBdoSpec
        {
            spec?.WithRequirement(RequirementLevels.Optional);

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
            spec?.WithRequirement(RequirementLevels.Required);

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
            spec?.WithRequirement(RequirementLevels.Forbidden);

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="level"></param>
        public static T WithDataRequirement<T>(
            this T spec,
            RequirementLevels level)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.DataRequirement = level;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="script"></param>
        public static T WithDataRequirementExp<T>(
            this T spec,
            string exp)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.DataRequirementExp = exp;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="level"></param>
        public static T WithRequirement<T>(
            this T spec,
            RequirementLevels level)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.Requirement = level;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="script"></param>
        public static T WithRequirementExp<T>(
            this T spec,
            string exp)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.RequirementExp = exp;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="levels"></param>
        public static T WithSpecLevels<T>(
            this T spec,
            params SpecificationLevels[] levels)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.SpecLevels = levels;
            }

            return spec;
        }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public static RequirementLevels WhatDataRequirement<T>(
            this T spec)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                if (!string.IsNullOrEmpty(spec.DataRequirementExp))
                {
                    return RequirementLevels.Custom;
                }
                else if (spec.MaxDataItemNumber == 0)
                {
                    return RequirementLevels.Forbidden;
                }
                else if (spec.MinDataItemNumber > 0)
                {
                    return RequirementLevels.Required;
                }
                else if (spec.MinDataItemNumber == 0)
                {
                    return RequirementLevels.Optional;
                }
            }

            return RequirementLevels.None;
        }

        /// <summary>
        /// 
        /// </summary>
        public static IEnumerable<IBdoSpec> GetSpecs(
            this IBdoSpec spec,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (spec != null)
            {
                return spec.SubSpecs?.Where(
                    q => q?.Condition.Evaluate(scope, varSet, log) == true);
            }

            return Enumerable.Empty<IBdoSpec>();
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithLabel<T>(
            this T spec,
            string label)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.Label = label;
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        public static T WithLabel<T>(
            this T spec,
            LabelFormats label)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.Label = label.GetScript();
            }

            return spec;
        }

    }
}
