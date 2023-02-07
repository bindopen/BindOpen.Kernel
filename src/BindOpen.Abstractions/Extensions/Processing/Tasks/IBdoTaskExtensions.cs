using BindOpen.Extensions.Processing;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IBdoTaskExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public static T WithDefinition<T>(
            this T obj,
            IBdoTaskDefinition def)
            where T : IBdoTask
        {
            if (obj != null)
            {
                obj.Definition = def;
            }

            return obj;
        }
    }
}