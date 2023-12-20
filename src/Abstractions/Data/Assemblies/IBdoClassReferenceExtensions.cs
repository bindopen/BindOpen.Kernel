using BindOpen.Data.Assemblies;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoClassReferenceExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyFileName"></param>
        public static T WithClassName<T>(
            this T obj,
            string className)
            where T : IBdoClassReference
        {
            if (obj != null)
            {
                obj.ClassName = className;
            }
            return obj;
        }
    }
}
