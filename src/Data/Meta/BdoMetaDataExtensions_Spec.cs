using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public static object GetSpecValue(
            this IBdoMetaData meta,
            string groupId,
            BdoSpecRuleKinds ruleKind = BdoSpecRuleKinds.Requirement,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            return meta?.GetSpecRule(groupId, ruleKind, scope, varSet, log)?.Value;
        }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public static T GetSpecValue<T>(
            this IBdoMetaData meta,
            string groupId,
            BdoSpecRuleKinds ruleKind = BdoSpecRuleKinds.Requirement,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (meta != null)
            {
                return meta.GetSpecValue(groupId, ruleKind, scope, varSet, log).As<T>();
            }

            return default;
        }

        /// <summary>
        /// The requirement level of this instance.
        /// </summary>
        [BdoFunction("requirementLevel")]
        public static RequirementLevels GetRequirementLevel(
            this IBdoMetaData meta,
            [BdoScriptParameter] IBdoScope scope = null,
            [BdoScriptParameter] IBdoMetaSet varSet = null,
            [BdoScriptParameter] IBdoLog log = null)
        {
            var level = meta.GetSpecValue<RequirementLevels?>(
                BdoMetaDataProperties.RequirementLevel, BdoSpecRuleKinds.Requirement, scope, varSet, log) ?? RequirementLevels.None;

            return level;
        }


        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        [BdoFunction("itemRequirementLevel")]
        public static RequirementLevels GetItemRequirementLevel(
            this IBdoMetaData meta,
            [BdoScriptParameter] IBdoScope scope = null,
            [BdoScriptParameter] IBdoMetaSet varSet = null,
            [BdoScriptParameter] IBdoLog log = null)
        {
            if (meta?.Spec != null)
            {
                var level = meta.GetSpecValue<RequirementLevels?>(
                    BdoMetaDataProperties.ItemRequirementLevel, BdoSpecRuleKinds.Requirement, scope, varSet, log) ?? RequirementLevels.None;

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
