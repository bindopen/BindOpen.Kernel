using BindOpen.Data.Helpers;
using BindOpen.Data.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BindOpen.Data.Meta.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        public static void UpdateTrees(
            this IBdoMetaSet set,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
        {
            if (set != null)
            {
                foreach (var meta in set)
                {
                    meta?.UpdateTree(onlyMetaAttributes, includeNullValues);
                }
            }
        }

        public static void UpdateTrees(
            this IBdoConfiguration config,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
        {
            ((IBdoMetaSet)config).UpdateTrees(onlyMetaAttributes, includeNullValues);
            if (config?._Children != null)
            {
                foreach (var child in config._Children)
                {
                    child.UpdateTrees(onlyMetaAttributes, includeNullValues);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T UpdateTree<T>(
            this T meta,
            bool onlyMetaAttributes = false,
            bool includeNullValues = true)
            where T : IBdoMetaData
        {
            List<IBdoMetaData> list;

            var obj = meta?.Reference == null ? meta.GetData() : null;

            if (obj != null)
            {
                var type = obj.GetType();

                if (meta is IBdoMetaObject metaObject)
                {
                    list = new();

                    if (!type.IsScalar() && !type.IsList()
                        && !type.IsAssignableFrom(typeof(IBdoObjectNotMetable)))
                    {
                        foreach (var propInfo in type.GetProperties())
                        {
                            var hasMetaAttribute = propInfo.GetCustomAttributes(typeof(BdoPropertyAttribute)).Any();
                            if (hasMetaAttribute || !onlyMetaAttributes)
                            {
                                IBdoSchema schema = BdoData.NewSchema();
                                var change = schema.UpdateFrom<BdoPropertyAttribute>(propInfo);

                                var propName = schema.Name ?? propInfo.Name;
                                schema.Name = null;
                                object propValue = propInfo.GetValue(obj);

                                IBdoMetaData subMeta = metaObject[propName];
                                if (subMeta != null)
                                {
                                    subMeta.WithData(propValue);
                                    subMeta.UpdateTree(onlyMetaAttributes, includeNullValues);
                                }
                                else
                                {
                                    subMeta = propValue.ToMeta(propInfo.PropertyType, propName, onlyMetaAttributes, includeNullValues);
                                    if (change)
                                    {
                                        subMeta.WithSchema(schema);
                                    }
                                }

                                list.Add(subMeta);
                            }
                        }
                    }

                    metaObject.With(list?.ToArray());
                }
                else if (type.IsList() && meta is IBdoMetaNode metaSet)
                {
                    list = new();

                    var objList = obj.ToObjectList();

                    int i = 0;
                    foreach (var subObj in objList)
                    {
                        var propName = metaSet?[i]?.Name;

                        IBdoMetaData subMeta = metaSet?[i];
                        if (subMeta != null)
                        {
                            subMeta.WithData(subObj);
                            subMeta.UpdateTree(onlyMetaAttributes, includeNullValues);
                        }
                        else
                        {
                            subMeta = subObj.ToMeta(propName, onlyMetaAttributes, includeNullValues);
                        }

                        list.Add(subMeta);
                        i++;
                    }

                    metaSet.With(list?.ToArray());
                }
            }

            return meta;
        }
    }
}
