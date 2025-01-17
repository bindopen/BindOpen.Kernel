﻿using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
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
            IBdoScope scope,
            string groupId,
            BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            return meta?.GetSchemaRule(scope, groupId, ruleKind, varSet, log)?.Value;
        }

        public static object GetSpecValue(
            this IBdoMetaData meta,
            string groupId,
            BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => meta.GetSpecValue(meta?.Scope, groupId, ruleKind, varSet, log);

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public static T GetSpecValue<T>(
            this IBdoMetaData meta,
            IBdoScope scope,
            string groupId,
            BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (meta != null)
            {
                return meta.GetSpecValue(scope, groupId, ruleKind, varSet, log).As<T>();
            }

            return default;
        }

        public static T GetSpecValue<T>(
            this IBdoMetaData meta,
            string groupId,
            BdoSchemaRuleKinds ruleKind = BdoSchemaRuleKinds.Requirement,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            => meta.GetSpecValue<T>(meta?.Scope, groupId, ruleKind, varSet, log);

        /// <summary>
        /// The requirement level of this instance.
        /// </summary>
        [BdoFunction("requirementLevel")]
        public static RequirementLevels GetRequirementLevel(
            this IBdoMetaData meta,
            [BdoScriptParameter] IBdoScope scope,
            [BdoScriptParameter] IBdoMetaSet varSet = null,
            [BdoScriptParameter] IBdoLog log = null)
        {
            var level = meta.GetSpecValue<RequirementLevels?>(
                scope, BdoMetaDataProperties.RequirementLevel, BdoSchemaRuleKinds.Requirement, varSet, log) ?? RequirementLevels.None;

            return level;
        }

        public static RequirementLevels GetRequirementLevel(
            this IBdoMetaData meta,
            [BdoScriptParameter] IBdoMetaSet varSet = null,
            [BdoScriptParameter] IBdoLog log = null)
            => meta.GetRequirementLevel(meta?.Scope, varSet, log);

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        [BdoFunction("itemRequirementLevel")]
        public static RequirementLevels GetItemRequirementLevel(
            this IBdoMetaData meta,
            [BdoScriptParameter] IBdoScope scope,
            [BdoScriptParameter] IBdoMetaSet varSet = null,
            [BdoScriptParameter] IBdoLog log = null)
        {
            if (meta?.Schema != null)
            {
                var level = meta.GetSpecValue<RequirementLevels?>(
                    scope, BdoMetaDataProperties.ItemRequirementLevel, BdoSchemaRuleKinds.Requirement, varSet, log) ?? RequirementLevels.None;

                if (level == RequirementLevels.None)
                {
                    if (meta.Schema.MaxDataItemNumber == 0)
                    {
                        return RequirementLevels.Forbidden;
                    }
                    else if (meta.Schema.MinDataItemNumber > 0)
                    {
                        return RequirementLevels.Required;
                    }
                    else if (meta.Schema.MinDataItemNumber == 0)
                    {
                        return RequirementLevels.Optional;
                    }
                }

                return level;
            }

            return RequirementLevels.None;
        }

        public static RequirementLevels GetItemRequirementLevel(
            this IBdoMetaData meta,
            [BdoScriptParameter] IBdoMetaSet varSet = null,
            [BdoScriptParameter] IBdoLog log = null)
            => meta.GetItemRequirementLevel(meta?.Scope, varSet, log);
    }
}
