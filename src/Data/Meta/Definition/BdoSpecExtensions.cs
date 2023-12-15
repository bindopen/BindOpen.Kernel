using BindOpen.Kernel.Data.Conditions;
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

        public static IBdoSpec ToSpec(
            this IBdoMetaData meta,
            string name = null,
            bool onlyMetaAttributes = true)
            => ToSpec<BdoSpec>(meta, name, onlyMetaAttributes);

        public static T ToSpec<T>(
            this IBdoMetaData meta,
            string name = null,
            bool onlyMetaAttributes = true)
            where T : IBdoSpec, new()
        {
            T spec = default;

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
            params (RequirementLevels Level, IBdoCondition Condition)[] rules)
            where T : IBdoSpec
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
            where T : IBdoSpec
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
            params (RequirementLevels Level, IBdoCondition Condition)[] rules)
            where T : IBdoSpec
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
            where T : IBdoSpec
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

        // Flag

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isAllocatable"></param>
        /// <returns></returns>
        public static T AsFlag<T>(
            this T spec,
            string flagName,
            bool isFlag = true)
            where T : IBdoSpec
        {
            spec?.GetOrNewDetail().Add(flagName, isFlag);

            return spec;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="isAllocatable"></param>
        /// <returns></returns>
        public static bool IsFlag(this IBdoSpec spec, string flagName)
        {
            return spec?.Detail?.GetData<bool?>(flagName) ?? false;
        }
    }
}
