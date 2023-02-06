using BindOpen.Extensions.Scripting;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IBdoScriptwordExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public static T WithDefinition<T>(
            this T obj,
            IBdoScriptwordDefinition def)
            where T : IBdoScriptword
        {
            if (obj != null)
            {
                obj.Definition = def;
            }

            return obj;
        }
    }
}