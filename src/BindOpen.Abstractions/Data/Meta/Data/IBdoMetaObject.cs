using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoMetaObject :
        ITBdoMetaData<IBdoMetaObject, IBdoObjectSpec, object>,
        IBdoMetaSet
    {
        new void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        IBdoMetaObject WithData(object obj);

        /// <summary>
        /// 
        /// </summary>
        IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithClassReference(
            IBdoClassReference reference);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Type GetClassType(
            IBdoScope scope = null,
            IBdoLog log = null);
    }
}