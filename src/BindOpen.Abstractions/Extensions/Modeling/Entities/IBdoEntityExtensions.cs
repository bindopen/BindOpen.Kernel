using BindOpen.Extensions.Modeling;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IBdoEntityExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public static T WithDefinition<T>(
            this T obj,
            IBdoEntityDefinition def)
            where T : IBdoEntity
        {
            if (obj != null)
            {
                obj.Definition = def;
            }

            return obj;
        }
    }
}