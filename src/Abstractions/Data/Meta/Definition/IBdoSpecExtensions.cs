//using BindOpen.Data.Helpers;
//using BindOpen.Data.Meta;
//using BindOpen.Logging;
//using BindOpen.Scoping;

//namespace BindOpen.Data
//{
//    /// <summary>
//    /// This class represents a data element set.
//    /// </summary>
//    public static partial class IBdoSpecExtensions
//    {
//        public static T With<T>(this T spec, params IBdoSpecRule[] rules) where T : IBdoSpec
//        {
//            spec?.With<T, IBdoSpecRule>(rules);

//            return spec;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param key="aliases"></param>
//        public static T WithAvailableDataModes<T>(
//            this T spec,
//            params DataMode[] modes)
//            where T : IBdoSpec
//        {
//            if (spec != null)
//            {
//                spec.AvailableDataModes = modes;
//            }
//            return spec;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param key="aliases"></param>
//        public static T WithAliases<T>(
//            this T spec,
//            params string[] aliases)
//            where T : IBdoSpec
//        {
//            if (spec != null)
//            {
//                spec.Aliases = aliases;
//            }
//            return spec;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param key="item"></param>
//        public static T WithDefaultData<T>(
//            this T spec,
//            object item)
//            where T : IBdoSpec
//        {
//            if (spec != null)
//            {
//                if (item is IBdoReference reference)
//                {
//                    spec.WithReference(reference);
//                }
//                else
//                {
//                    spec.DefaultData = item;
//                }
//            }
//            return spec;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param key="number"></param>
//        public static T WithMaxDataItemNumber<T>(
//            this T spec,
//            uint? number = null)
//            where T : IBdoSpec
//        {
//            if (spec != null)
//            {
//                spec.MaxDataItemNumber = number;
//            }
//            return spec;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param key="number"></param>
//        public static T WithMinDataItemNumber<T>(
//            this T spec,
//            uint number)
//            where T : IBdoSpec
//        {
//            if (spec != null)
//            {
//                spec.MinDataItemNumber = number;
//            }
//            return spec;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param key="level"></param>
//        public static T WithAccessibilityLevel<T>(
//            this T spec,
//            AccessibilityLevels level)
//            where T : IBdoSpec
//        {
//            if (spec != null)
//            {
//                spec.AccessibilityLevel = level;
//            }

//            return spec;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param key="level"></param>
//        public static T WithInheritanceLevel<T>(
//            this T spec,
//            InheritanceLevels level)
//            where T : IBdoSpec
//        {
//            if (spec != null)
//            {
//                spec.InheritanceLevel = level;
//            }

//            return spec;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        public static T WithLabel<T>(
//            this T spec,
//            string label)
//            where T : IBdoSpec
//        {
//            if (spec != null)
//            {
//                spec.Label = label;
//            }

//            return spec;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        public static T WithLabel<T>(
//            this T spec,
//            LabelFormats label)
//            where T : IBdoSpec
//        {
//            if (spec != null)
//            {
//                spec.Label = label.GetScript();
//            }

//            return spec;
//        }

//        public static T GetValue<T>(
//            this IBdoSpec spec,
//            string groupId,
//            BdoSpecRuleKinds mode,
//            IBdoScope scope = null,
//            IBdoMetaSet varSet = null,
//            IBdoLog log = null)
//        {
//            if (spec != null)
//            {
//                return spec.GetValue(groupId, mode, scope, varSet, log).As<T>();
//            }

//            return default;
//        }

//    }
//}
