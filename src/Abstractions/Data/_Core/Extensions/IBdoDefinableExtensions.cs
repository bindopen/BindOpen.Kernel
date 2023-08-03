using BindOpen.System.Scoping;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class IBdoDefinableExtensions
    {
        public static T WithDefinition<T>(
            this T obj,
            string definitionUniqueName)
            where T : IBdoDefinable
        {
            if (obj != null)
            {
                obj.DefinitionUniqueName = definitionUniqueName;
            }
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T WithDefinition<T>(
            this T obj,
            BdoExtensionKind definitionExtensionKind,
            string definitionUniqueName = null)
            where T : IBdoDefinable
        {
            if (obj != null)
            {
                obj.DefinitionExtensionKind = definitionExtensionKind;

                if (definitionUniqueName != null)
                {
                    obj.WithDefinition(definitionUniqueName);
                }
            }
            return obj;
        }
    }
}