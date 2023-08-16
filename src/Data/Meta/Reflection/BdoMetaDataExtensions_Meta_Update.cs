using BindOpen.System.Data.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BindOpen.System.Data.Meta.Reflection
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        public static void UpdateTrees(
            this IBdoMetaSet set,
            bool onlyMetaAttributes = false)
        {
            if (set != null)
            {
                foreach (var meta in set)
                {
                    meta?.UpdateTree(onlyMetaAttributes);
                }
            }
        }

        public static void UpdateTrees(
            this IBdoConfiguration config,
            bool onlyMetaAttributes = false)
        {
            ((IBdoMetaSet)config).UpdateTrees(onlyMetaAttributes);
            if (config?._Children != null)
            {
                foreach (var child in config._Children)
                {
                    child.UpdateTrees(onlyMetaAttributes);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static T UpdateTree<T>(
            this T meta,
            bool onlyMetaAttributes = false)
            where T : IBdoMetaData
        {
            List<IBdoMetaData> list;

            var obj = meta?.DataReference == null ? meta.GetData() : null;

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
                                IBdoSpec spec = BdoData.NewSpec();
                                var change = spec.UpdateFrom<BdoPropertyAttribute>(propInfo);

                                var propName = spec.Name ?? propInfo.Name;
                                spec.Name = null;
                                object propValue = propInfo.GetValue(obj);

                                IBdoMetaData subMeta = metaObject[propName];
                                if (subMeta != null)
                                {
                                    subMeta.WithData(propValue);
                                    subMeta.UpdateTree();
                                }
                                else
                                {
                                    subMeta = propValue.ToMeta(propInfo.PropertyType, propName, onlyMetaAttributes);
                                    if (change)
                                    {
                                        subMeta.WithSpec(spec);
                                    }
                                }

                                list.Add(subMeta);
                            }
                        }
                    }

                    metaObject.With(list?.ToArray());
                }
                else if (type.IsList() && meta is IBdoMetaComposite metaSet)
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
                            subMeta.UpdateTree();
                        }
                        else
                        {
                            subMeta = subObj.ToMeta(propName, onlyMetaAttributes);
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
