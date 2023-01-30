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
        ITBdoMetaData<IBdoMetaObject, IBdoMetaObjectSpec, object>
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoMetaSet SubSet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithSubSet(IBdoMetaSet set);

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithSubSet(params IBdoMetaData[] metas);

        /// <summary>
        /// 
        /// </summary>
        IBdoClassReference ClassReference { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithClassReference(IBdoClassReference reference);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        new IBdoMetaObject WithItems(
            params object[] objs);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IBdoMetaObject UpdateTree(
            IBdoScope scope = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Type GetItemType(
            IBdoScope scope = null,
            IBdoLog log = null);
    }
}