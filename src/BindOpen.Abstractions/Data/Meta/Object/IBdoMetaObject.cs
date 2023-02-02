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
        ITBdoMetaItem<IBdoMetaObject, IBdoMetaObjectSpec, object>,
        IBdoMetaSet
    {
        /// <summary>
        /// 
        /// </summary>
        IBdoMetaSet PropertySet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithProperties(
            params IBdoMetaData[] metas);

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoMetaObject Add(
            params IBdoMetaItem[] items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        new IBdoMetaObject WithItems(
            params IBdoMetaItem[] items);
    }
}