using BindOpen.Data.Conditions;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoBaseSpecExtensions
    {
        // Requirement

        /// <summary>
        /// 
        /// </summary>
        /// <param key="level"></param>
        public static T WithRequirement<T>(
            this T spec,
            params (RequirementLevels Level, IBdoCondition Condition)[] rules)
            where T : IBdoBaseSpec
        {
            if (spec != null)
            {
                spec.RemoveOfGroup(BdoMetaDataProperties.RequirementLevel);

                foreach (var (Level, Condition) in rules)
                {
                    spec.AddRequirement(Level, Condition);
                }
            }

            return spec;
        }

        public static T AddRequirement<T>(
            this T spec,
            RequirementLevels level,
            IBdoCondition condition = null)
            where T : IBdoBaseSpec
        {
            if (spec != null)
            {
                BdoSpecRule rule = (BdoMetaDataProperties.RequirementLevel, level, condition);
                spec.Insert(rule);
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T AsOptional<T>(
            this T spec,
            IBdoCondition condition = null)
            where T : IBdoBaseSpec
        {
            spec?.AddRequirement(RequirementLevels.Optional, condition);

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T AsRequired<T>(
            this T spec,
            IBdoCondition condition = null)
            where T : IBdoBaseSpec
        {
            spec?.AddRequirement(RequirementLevels.Required, condition);

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T AsForbidden<T>(
            this T spec,
            IBdoCondition condition = null)
            where T : IBdoBaseSpec
        {
            spec?.AddRequirement(RequirementLevels.Forbidden, condition);

            return spec;
        }

        public static T WithItemRequirement<T>(
            this T spec,
            params (RequirementLevels Level, IBdoCondition Condition)[] rules)
            where T : IBdoBaseSpec
        {
            if (spec != null)
            {
                spec.RemoveOfGroup(BdoMetaDataProperties.ItemRequirementLevel);

                foreach (var (Level, Condition) in rules)
                {
                    spec.AddItemRequirement(Level, Condition);
                }
            }

            return spec;
        }

        public static T AddItemRequirement<T>(
            this T spec,
            RequirementLevels level,
            IBdoCondition condition = null)
            where T : IBdoBaseSpec
        {
            if (spec != null)
            {
                BdoSpecRule rule = (BdoMetaDataProperties.ItemRequirementLevel, level, condition);
                spec.Insert(rule);
            }

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T AsItemOptional<T>(
            this T spec,
            IBdoCondition condition = null)
            where T : IBdoBaseSpec
        {
            spec?.AddItemRequirement(RequirementLevels.Optional, condition);

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T AsItemRequired<T>(
            this T spec,
            IBdoCondition condition = null)
            where T : IBdoBaseSpec
        {
            spec?.AddItemRequirement(RequirementLevels.Required, condition);

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T AsItemForbidden<T>(
            this T spec,
            IBdoCondition condition = null)
            where T : IBdoBaseSpec
        {
            spec?.AddItemRequirement(RequirementLevels.Forbidden, condition);

            return spec;
        }
    }
}
