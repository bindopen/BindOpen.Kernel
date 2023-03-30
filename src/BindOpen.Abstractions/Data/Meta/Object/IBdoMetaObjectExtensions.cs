using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using BindOpen.Scopes;
using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class IBdoMetaObjectExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithClassReference<T>(
            this T meta,
            IBdoClassReference reference)
            where T : IBdoMetaObject
        {
            if (meta != null)
            {
                meta.ClassReference = reference;
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        public static Type GetClassType<T>(
            this T meta,
            IBdoScope scope = null,
            IBdoLog log = null)
            where T : IBdoMetaObject
        {
            var type = scope?.CreateType(meta?.ClassReference, log);
            return type;
        }
    }
}
