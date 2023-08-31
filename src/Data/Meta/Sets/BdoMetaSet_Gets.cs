using BindOpen.System.Data.Meta;
using BindOpen.System.Logging;
using BindOpen.System.Scoping;
using System.Collections.Generic;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public partial class BdoMetaSet
    {
        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaObject Object(
            string key = null)
            => Get<IBdoMetaObject>(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaObject Object(
            int index)
            => Get<IBdoMetaObject>(index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaScalar Scalar(
            string key = null)
            => Get<IBdoMetaScalar>(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaScalar Scalar(
            int index)
            => Get<IBdoMetaScalar>(index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaNode Composite(
            string key = null)
            => Get<IBdoMetaNode>(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param key="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaNode Composite(
            int index)
            => Get<IBdoMetaNode>(index);

        // Data

        /// <summary>
        /// 
        /// </summary>
        /// <param key="elementSet"></param>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        public IList<object> GetDataList(
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var meta = this[key];
            if (meta is IBdoMetaObject metaObject)
                return metaObject.GetDataList(scope, varSet, log);
            else if (meta is IBdoMetaNode metaComposite)
                return metaComposite.GetDataList(scope, varSet, log);
            else if (meta is IBdoMetaScalar metaScalar)
                return metaScalar.GetDataList(scope, varSet, log);

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param key="elementSet"></param>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        public IList<Q> GetDataList<Q>(
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var meta = this[key];
            if (meta is IBdoMetaObject metaObject)
                return metaObject.GetDataList<Q>(scope, varSet, log);
            else if (meta is IBdoMetaNode metaComposite)
                return metaComposite.GetDataList<Q>(scope, varSet, log);
            else if (meta is IBdoMetaScalar metaScalar)
                return metaScalar.GetDataList<Q>(scope, varSet, log);

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="element"></param>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        public object GetData(
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var meta = this[key];
            if (meta is IBdoMetaObject metaObject)
                return metaObject.GetData(scope, varSet, log);
            else if (meta is IBdoMetaNode metaSet)
                return metaSet.GetData(scope, varSet, log);
            else if (meta is IBdoMetaScalar metaScalar)
                return metaScalar.GetData(scope, varSet, log);

            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="elementSet"></param>
        /// <param key="scope"></param>
        /// <param key="varSet"></param>
        /// <param key="log"></param>
        /// <returns></returns>
        public Q GetData<Q>(
            string key,
            IBdoScope scope = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var meta = this[key];
            if (meta is IBdoMetaObject metaObject)
                return metaObject.GetData<Q>(scope, varSet, log);
            else if (meta is IBdoMetaNode metaComposite)
                return metaComposite.GetData<Q>(scope, varSet, log);
            else if (meta is IBdoMetaScalar metaScalar)
                return metaScalar.GetData<Q>(scope, varSet, log);

            return default;
        }
    }
}

