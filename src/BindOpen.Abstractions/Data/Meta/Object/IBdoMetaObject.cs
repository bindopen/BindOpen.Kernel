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
        IBdoMetaSet PropertyMetaSet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithMetaProperties(IBdoMetaSet set);

        /// <summary>
        /// 
        /// </summary>
        IBdoMetaObject WithMetaProperties(params IBdoMetaData[] metas);

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
        /// <returns></returns>
        Type GetClassType(
            IBdoScope scope = null,
            IBdoLog log = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        new IBdoMetaObject WithItems(
            params object[] objs);
    }
}