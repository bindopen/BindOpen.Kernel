using BindOpen.Kernel.Data.Meta;

namespace BindOpen.Kernel.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoSpecExtensions
    {
        public static T With<T>(this T spec, params IBdoConstraint[] constraints) where T : IBdoSpec
        {
            spec?.With<T, IBdoConstraint>(constraints);

            return spec;
        }

        public static T WithParent<T>(this T log, IBdoSpec parent) where T : IBdoSpec
        {
            if (log != null)
            {
                log.Parent = parent;
            }

            return log;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="aliases"></param>
        public static T WithAvailableDataModes<T>(
            this T spec,
            params DataMode[] modes)
            where T : IBdoSpec
        {
            if (spec != null)
            {
                spec.AvailableDataModes = modes;
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
                if (item is IBdoReference reference)
                {
                    spec.WithReference(reference);
                }
                else
                {
                    spec.DefaultData = item;
                }
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
