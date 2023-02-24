using BindOpen.Data.Meta;
using BindOpen.Data.Meta;
using BindOpen.Data.Helpers;
using BindOpen.Data.Items;
using System.Collections.Generic;
using System.Reflection;

namespace BindOpen.Data.Meta.Reflection
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

            var obj = meta?.GetData();

            if (obj != null)
            {
                var type = obj.GetType();

                if (meta is IBdoMetaObject metaObject)
                {
                    list = new();

                    if (!type.IsScalar() && !type.IsList()
                        && !type.IsAssignableFrom(typeof(IBdoNotMetableItem)))
                    {
                        foreach (var propInfo in type.GetProperties())
                        {
                            var bdoAttribute = propInfo.GetCustomAttribute(typeof(BdoPropertyAttribute)) as BdoPropertyAttribute;
                            if (bdoAttribute != null || !onlyMetaAttributes)
                            {
                                string propName = propInfo.Name;
                                object propValue = propInfo.GetValue(obj);

                                if (!string.IsNullOrEmpty(bdoAttribute?.Name))
                                {
                                    propName = bdoAttribute.Name;
                                }

                                IBdoMetaData subMeta;
                                if (metaObject?.Has(propName) == true)
                                {
                                    subMeta = metaObject[propName];
                                    if (subMeta.DataMode == DataMode.Value)
                                    {
                                        if (subMeta is IBdoMetaScalar subMetaScalar)
                                        {
                                            subMetaScalar.WithData(propValue)
                                                .UpdateTree();
                                        }
                                        else if (subMeta is IBdoMetaObject subMetaObject)
                                        {
                                            subMetaObject.WithData(propValue)
                                                .UpdateTree();
                                        }
                                        else if (subMeta is IBdoMetaSet subMetaSet)
                                        {
                                            subMetaSet.UpdateTree();
                                        }
                                    }
                                }
                                else
                                {
                                    subMeta = propValue.ToMetaData(propName, onlyMetaAttributes, propInfo.PropertyType);
                                }

                                list.Add(subMeta);
                            }
                        }
                    }

                    metaObject.With(list?.ToArray());
                }
                else if (type.IsList() && meta is IBdoMetaSet metaSet)
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
