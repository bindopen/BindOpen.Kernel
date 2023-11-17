using BindOpen.Kernel.Data.Helpers;
using BindOpen.Kernel.Data.Meta;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public static T GetConstraintValue<T>(
            this IBdoMetaData meta,
            string groupId,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (meta!=null)
            {
                return meta.GetConstraintValue(groupId, scope, varSet, log).As<T>();
            }

            return default;
        }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public static RequirementLevels GetRequirement<T>(
            this T meta,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : IBdoMetaData
        {
            var level = meta.GetConstraintValue<RequirementLevels?>(
                BdoMetaConstraintGroupIds.Requirement, scope, varSet, log) ?? RequirementLevels.None;

            return level;
        }


        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public static RequirementLevels GetItemRequirement(
            this IBdoMetaData meta,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (meta?.Spec != null)
            {
                var level = meta.GetConstraintValue<RequirementLevels?>(
                    BdoMetaConstraintGroupIds.ItemRequirement, scope, varSet, log) ?? RequirementLevels.None;

                if (level == RequirementLevels.None)
                {
                    if (meta.Spec.MaxDataItemNumber == 0)
                    {
                        return RequirementLevels.Forbidden;
                    }
                    else if (meta.Spec.MinDataItemNumber > 0)
                    {
                        return RequirementLevels.Required;
                    }
                    else if (meta.Spec.MinDataItemNumber == 0)
                    {
                        return RequirementLevels.Optional;
                    }
                }

                return level;
            }

            return RequirementLevels.None;
        }
    }
}
