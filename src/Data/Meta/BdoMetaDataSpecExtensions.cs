using BindOpen.System.Data.Conditions;
using BindOpen.System.Logging;
using BindOpen.System.Scoping;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoMetaDataSpecExtensions
    {

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public static bool WhatCondition<T>(
            this T meta,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : IBdoMetaData
        {
            if (meta?.Spec?.Condition != null)
            {
                var localVarSet = BdoData.NewMetaSet(varSet?.ToArray());
                localVarSet.Add("$this", meta);

                var b = scope.Evaluate(meta.Spec.Condition, localVarSet, log);

                return b;
            }

            return true;
        }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public static RequirementLevels WhatRequirement<T>(
            this T meta,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : IBdoMetaData
        {
            if (meta?.Spec != null)
            {
                var localVarSet = BdoData.NewMetaSet(varSet?.ToArray());
                localVarSet.Add("$this", meta);

                var level = meta.Spec.RequirementStatement.GetItem(scope, localVarSet, log);

                return level;
            }

            return RequirementLevels.None;
        }

        /// <summary>
        /// The item requirement level of this instance.
        /// </summary>
        public static RequirementLevels WhatItemRequirement<T>(
            this T meta,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
            where T : IBdoMetaData
        {
            if (meta?.Spec != null)
            {
                var localVarSet = BdoData.NewMetaSet(varSet?.ToArray());
                localVarSet.Add("$this", meta);

                var level = meta.Spec.ItemRequirementStatement.GetItem(scope, localVarSet, log);

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
