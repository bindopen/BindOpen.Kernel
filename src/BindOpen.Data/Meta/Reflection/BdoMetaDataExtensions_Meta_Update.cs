using BindOpen.Data.Helpers;
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
                        && !type.IsAssignableFrom(typeof(IBdoItemNotMetable)))
                    {
                        foreach (var propInfo in type.GetProperties())
                        {
                            var hasMetaAttribute = propInfo.GetCustomAttributes(typeof(BdoPropertyAttribute)).Any();
                            if (hasMetaAttribute || !onlyMetaAttributes)
                            {
                                var spec = BdoMeta.NewSpec();
                                spec.UpdateFrom<BdoPropertyAttribute>(propInfo);

                                var propName = spec.Name;
                                object propValue = propInfo.GetValue(obj);

                                IBdoMetaData subMeta = metaObject[propName];
                                if (subMeta != null)
                                {
                                    if (subMeta.DataMode == DataMode.Value)
                                    {
                                        if (subMeta is IBdoMetaScalar subMetaScalar)
                                        {
                                            subMetaScalar.WithData(propValue);
                                        }
                                        else if (subMeta is IBdoMetaObject subMetaObject)
                                        {
                                            subMetaObject.WithData(propValue);
                                        }

                                        subMeta.UpdateTree();
                                    }
                                }
                                else
                                {
                                    subMeta = ToMetaData(propInfo.PropertyType, propValue, propName, onlyMetaAttributes);
                                    subMeta.WithGroupId(spec.GroupId);
                                    if (spec.ValueType == DataValueTypes.Any)
                                    {
                                        subMeta.WithDataValueType(spec.ValueType);
                                    }
                                    subMeta.WithSpecs(spec);
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
