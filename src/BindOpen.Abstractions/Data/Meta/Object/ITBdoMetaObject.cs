using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using BindOpen.Scopes.Scopes;
using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoMetaObject<TItem> :
        ITBdoMetaData<TItem>,
        IBdoMetaSet
    {
        new void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        ITBdoMetaObject<TItem> WithData(object obj);

        /// <summary>
        /// 
        /// </summary>
        IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ITBdoMetaObject<TItem> WithClassReference(
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