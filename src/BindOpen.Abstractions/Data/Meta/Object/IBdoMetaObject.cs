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
        ITBdoMetaData<IBdoMetaObject, IBdoMetaObjectSpec, object>,
        IBdoMetaSet
    {
        new void Clear();

        IBdoMetaSet PropertySet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objs"></param>
        IBdoMetaObject WithData(object obj);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="varSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        Q GetData<Q>(
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null);

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