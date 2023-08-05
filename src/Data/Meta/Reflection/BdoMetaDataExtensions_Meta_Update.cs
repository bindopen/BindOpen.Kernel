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

            var obj = meta?.DataMode == DataMode.Value ?
                meta.GetData() : null;

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

                                var propName = spec?.Name ?? propInfo.Name;
                                object propValue = propInfo.GetValue(obj);

                                IBdoMetaData subMeta = metaObject[propName];
                                if (subMeta != null)
                                {
                                    if (subMeta.DataMode == DataMode.Value)
                                    {
                                        subMeta.WithData(propValue);
                                        subMeta.UpdateTree();
                                    }
                                }
                                else
                                {
                                    subMeta = propValue.ToMetaData(propInfo.PropertyType, propName, onlyMetaAttributes);
                                    if (change)
                                    {
                                        subMeta.WithSpecs(spec);
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
                    foreach (var subObj in objList)
                    {
                        var subMeta = subObj.ToMetaData(null, onlyMetaAttributes);

                        list.Add(subMeta);
                    }

                    metaSet.With(list?.ToArray());
                }
            }

            return meta;
        }
    }
}
