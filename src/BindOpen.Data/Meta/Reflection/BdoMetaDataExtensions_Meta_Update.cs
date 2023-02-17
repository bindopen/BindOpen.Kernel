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
                        && type.IsAssignableFrom(typeof(IBdoHandledItem)))
                    {
                        foreach (var propInfo in type.GetProperties())
                        {
                            var bdoAttribute = propInfo.GetCustomAttribute(typeof(BdoDataAttribute)) as BdoDataAttribute;
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
                                    if (subMeta.ValueMode == DataValueMode.Value)
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
                                        else if (subMeta is IBdoMetaList subMetaList)
                                        {
                                            subMetaList.UpdateTree();
                                        }
                                    }
                                }
                                else
                                {
                                    subMeta = propValue.ToMetaData(onlyMetaAttributes, propName);
                                }

                                list.Add(subMeta);
                            }
                        }
                    }

                    metaObject.With(list?.ToArray());
                }
                else if (type.IsList() && meta is IBdoMetaObject metaList)
                {
                    list = new();

                    var objList = obj.ToObjectList();
                    foreach (var subObj in objList)
                    {
                        var subMeta = subObj.ToMetaData(onlyMetaAttributes);

                        list.Add(subMeta);
                    }

                    metaList.With(list?.ToArray());
                }
            }

            return meta;
        }
    }
}
