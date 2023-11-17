using BindOpen.Kernel.Data.Conditions;
using BindOpen.Kernel.Logging;
using BindOpen.Kernel.Scoping;
using System.Linq;

namespace BindOpen.Kernel.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoSpecExtensions
    {
        public static T WithChildren<T>(this T log, params IBdoSpec[] children) where T : IBdoSpec
        {
            if (log != null)
            {
                log._Children = BdoData.NewItemSet(children?.Any() == true ? children : null);
            }

            return log;
        }

        public static T AddChildren<T>(this T log, params IBdoSpec[] children) where T : IBdoSpec
        {
            if (log != null)
            {
                log._Children ??= BdoData.NewItemSet<IBdoSpec>();
                foreach (var child in children)
                {
                    log._Children.Add(child);
                }
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isAllocatable"></param>
        /// <returns></returns>
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
        /// <returns></returns>
        public static bool IsCompatibleWith(
            this ITBdoSet<IBdoSpec> specs,
            ITBdoSet<IBdoMetaData> metas,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            if (specs == null || metas == null) return false;

            var b = true;

            var iObj = 0;
            var iSpec = 0;
            while (iSpec < specs.Count && iObj < metas.Count)
            {
                var spec = specs[iSpec];

                var obj = metas[iObj].GetData(scope, varSet, log);
                b &= spec?.IsCompatibleWithData(obj) ?? true;

                iSpec++;

                iObj++;
            }

            return b;
        }

        public static IBdoSpec ToSpec(
            this IBdoMetaData meta,
            string name = null,
            bool onlyMetaAttributes = true)
            => ToSpec<BdoSpec>(meta, name, onlyMetaAttributes);

        public static T ToSpec<T>(
            this IBdoMetaData meta,
            string name = null,
            bool onlyMetaAttributes = true)
            where T : class, IBdoSpec, new()
        {
            T spec = null;

            if (meta != null)
            {
                var metaComposite = meta as IBdoMetaNode;
                spec = BdoData.NewSpec<T>();

                if (spec != null)
                {
                    spec.Update(meta.Spec);
                    spec.Name = name;
                    spec.Name ??= meta.Name;
                    spec.DataType = meta.DataType;

                    if (metaComposite != null)
                    {
                        foreach (var subMeta in metaComposite)
                        {
                            var subSpec = subMeta.ToSpec<T>(null, onlyMetaAttributes);
                            spec.AddChildren(subSpec);
                        }
                    }
                }
            }

            return spec;
        }

        // Requirement

        /// <summary>
        /// 
        /// </summary>
        /// <param key="level"></param>
        public static T WithRequirement<T>(
            this T spec,
            params (RequirementLevels Level, IBdoCondition Condition)[] constraints)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.RemoveOfGroup(BdoMetaConstraintGroupIds.Requirement);

                foreach (var (Level, Condition) in constraints)
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
            where T : IBdoSpec
        {
            if (spec != null)
            {
                BdoConstraint constraint = (BdoMetaConstraintGroupIds.Requirement, level, condition);
                spec.Insert(constraint);
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
            where T : IBdoSpec
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
            where T : IBdoSpec
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
            where T : IBdoSpec
        {
            spec?.AddRequirement(RequirementLevels.Forbidden, condition);

            return spec;
        }

        public static T WithItemRequirement<T>(
            this T spec,
            params (RequirementLevels Level, IBdoCondition Condition)[] constraints)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.RemoveOfGroup(BdoMetaConstraintGroupIds.ItemRequirement);

                foreach (var (Level, Condition) in constraints)
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
            where T : IBdoSpec
        {
            if (spec != null)
            {
                BdoConstraint constraint = (BdoMetaConstraintGroupIds.ItemRequirement, level, condition);
                spec.Insert(constraint);
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
            where T : IBdoSpec
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
            where T : IBdoSpec
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
            where T : IBdoSpec
        {
            spec?.AddItemRequirement(RequirementLevels.Forbidden, condition);

            return spec;
        }
    }
}
