using BindOpen.Data.Assemblies;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoMetaObject<TItem> :
        ITBdoMetaData<TItem, IBdoObjectSpec>,
        IBdoMetaSet
    {
        new void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <param key="objs"></param>
        ITBdoMetaObject<TItem> WithData(TItem obj);

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