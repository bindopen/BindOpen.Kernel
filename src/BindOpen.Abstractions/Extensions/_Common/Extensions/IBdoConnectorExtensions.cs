using BindOpen.Extensions.Connecting;
using BindOpen.Runtime.Definition;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This interface represents an named data item.
    /// </summary>
    public static class IBdoConnectorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public static T WithDefinition<T>(
            this T obj,
            IBdoConnectorDefinition def)
            where T : IBdoConnector
        {
            if (obj != null)
            {
                obj.Definition = def;
            }

            return obj;
        }
    }
}