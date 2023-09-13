using BindOpen.Kernel.Data;
using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;

namespace BindOpen.Kernel.Data.Meta
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
                var localVarSet = BdoData.NewSet(varSet?.ToArray());
                localVarSet.Add(BdoData.__VarName_This, meta);

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
                var localVarSet = BdoData.NewSet(varSet?.ToArray());
                localVarSet.Add(BdoData.__VarName_This, meta);

                var level = meta.Spec.RequirementStatement?.GetItem(scope, localVarSet, log) ?? RequirementLevels.None;

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
                var localVarSet = BdoData.NewSet(varSet?.ToArray());
                localVarSet.Add(BdoData.__VarName_This, meta);

                var level = meta.Spec.ItemRequirementStatement?.GetItem(scope, localVarSet, log) ?? RequirementLevels.None;

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
