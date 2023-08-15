using BindOpen.System.Scoping;
using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Data
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoDataTypedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithValueType<T>(
            this T dataType,
            DataValueTypes valueType)
            where T : IBdoDataType
        {
            if (dataType != null)
            {
                dataType.ValueType = valueType;
            }

            return dataType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="detail"></param>
        public static T WithDefinition<T>(
            this T obj,
            BdoExtensionKinds definitionExtensionKind,
            string definitionUniqueName = null)
            where T : IBdoDataType
        {
            if (obj != null)
            {
                obj.ValueType = definitionExtensionKind.GetValueType();

                if (definitionUniqueName != null)
                {
                    obj.WithDefinition(definitionUniqueName);
                }
            }
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyFileName"></param>
        public static bool IsScope(this IBdoDataType dataType)
        {
            return dataType?.IsCompatibleWith(typeof(IBdoScope)) ?? false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyFileName"></param>
        public static bool IsScriptDomain(this IBdoDataType dataType)
        {
            return dataType?.IsCompatibleWith(typeof(IBdoScriptDomain)) ?? false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="assemblyFileName"></param>
        public static bool IsScriptword(this IBdoDataType dataType)
        {
            return dataType?.IsCompatibleWith(typeof(IBdoScriptword)) ?? false;
        }
    }
}
