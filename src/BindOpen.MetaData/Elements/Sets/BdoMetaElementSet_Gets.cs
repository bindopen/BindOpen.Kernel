using BindOpen.Logging;
using BindOpen.MetaData.Items;
using BindOpen.Runtime.Scopes;
using System.Collections.Generic;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public partial class BdoMetaElementSet : TBdoItemSet<IBdoMetaElement>, IBdoMetaElementSet
    {
        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaCarrier GetCarrier(
            string key = null)
            => Get<IBdoMetaCarrier>(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaCarrier GetCarrier(
            int index)
            => Get<IBdoMetaCarrier>(index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaCollection GetCollection(
            string key = null)
            => Get<IBdoMetaCollection>(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaCollection GetCollection(
            int index)
            => Get<IBdoMetaCollection>(index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaObject GetObject(
            string key = null)
            => Get<IBdoMetaObject>(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaObject GetObject(
            int index)
            => Get<IBdoMetaObject>(index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaScalar GetScalar(
            string key = null)
            => Get<IBdoMetaScalar>(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaScalar GetScalar(
            int index)
            => Get<IBdoMetaScalar>(index);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="key">The key to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaSource GetSource(
            string key = null)
            => Get<IBdoMetaSource>(key);

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="index">The index to consider.</param>
        /// <returns>Returns the item of this instance.</returns>
        public IBdoMetaSource GetSource(
            int index)
            => Get<IBdoMetaSource>(index);

        //

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="elementSet"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public List<object> GetItems(
            string key,
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            return this[key]
                ?.Items(scope, varElementSet, log);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Q"></typeparam>
        /// <param name="elementSet"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public List<Q> GetItems<Q>(
            string key,
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            return this[key]
                ?.Items<Q>(scope, varElementSet, log);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public object GetItem(
            string key,
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            return this[key]
                ?.Item(scope, varElementSet, log);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementSet"></param>
        /// <param name="scope"></param>
        /// <param name="varElementSet"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public Q GetItem<Q>(
            string key,
            IBdoScope scope = null,
            IBdoMetaElementSet varElementSet = null,
            IBdoLog log = null)
        {
            var el = this[key];
            if (el == default)
                return default;

            return el.Item<Q>(scope, varElementSet, log);
        }
    }
}

