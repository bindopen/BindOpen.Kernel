using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a data element set.
    /// </summary>
    public static partial class BdoMetaItemExtensions
    {
        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData ToMetaData(
            this object obj,
            string name = null)
        {
            var objList = obj.ToObjectList();
            var meta = BdoMeta.New(name, objList?.ToArray());
            meta?.UpdateMetaTree();

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T UpdateMetaTree<T>(
            this T meta,
            IBdoScope scope = null,
            IBdoLog log = null)
            where T : IBdoMetaData
        {
            if (meta is IBdoMetaObject metaObj)
            {
                var obj = metaObj?.GetData();
                metaObj.PropertySet = obj.ToMetaSet(
                    metaObj.GetClassType(scope, log));
            }
            else if (meta is IBdoMetaSet metaSet)
            {
                foreach (var subMeta in metaSet)
                {
                    subMeta?.UpdateMetaTree(scope, log);
                }
            }

            return meta;
        }

        /// <summary>
        /// Creates a data element set from a dynamic object.
        /// </summary>
        /// <param name="obj">The objet to consider.</param>
        public static IBdoMetaData[] ToMetaArray(
            this object obj,
            Type type = null,
            bool onlyMetaAttributes = true)
            => obj.ToMetaSet(type, onlyMetaAttributes)?.ToArray();
    }
}
