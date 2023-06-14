using BindOpen.System.Scoping.Script;
using BindOpen.System.Data.Helpers;
using System;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This static class provides methods to create data elems.
    /// </summary>
    public static partial class BdoMeta
    {
        // New

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData New(
            object data)
        {
            return New(null, data);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData New(
            string name,
            object data)
        {
            if (data == null) return NewObject(name);

            var type = data.GetType();
            var meta = New(name, type, DataValueTypes.Any, data);

            return meta;
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData New(
            string name,
            DataValueTypes valueType,
            object data = null)
        {
            return New(name, null, valueType, data);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        public static IBdoMetaData New(
            string name,
            Type type,
            object data = null)
        {
            return New(name, type, DataValueTypes.Any, data);
        }

        /// <summary>
        /// Creates a data meta with specified items.
        /// </summary>
        /// <param key="name">The name to consider.</param>
        /// <param key="items">The items to consider.</param>
        private static IBdoMetaData New(
            string name,
            Type type,
            DataValueTypes valueType,
            object data = null)
        {
            type ??= data?.GetType();

            if (type != null)
            {
                valueType = type.GetValueType();
            }
            else if (valueType == DataValueTypes.Any || valueType == DataValueTypes.None)
            {
                valueType = data.GetValueType();
            }

            if (valueType.IsScalar() && type?.IsScalar() == true)
            {
                var metaScalar = NewScalar(name, valueType, data);
                return metaScalar;
            }
            else if (valueType == DataValueTypes.Scriptword)
            {
                var word = data as IBdoScriptword;
                return New(name, null)
                    .WithDataReference(word);
            }
            else if (valueType == DataValueTypes.MetaData)
            {
                var meta = data as IBdoMetaData;
                return New(name, null)
                    .WithDataReference(meta);
                //Type metaType;
                //Type itemType = null;

                //if (type.IsGenericType)
                //{
                //    metaType = type.GetGenericTypeDefinition();
                //    itemType = type.GenericTypeArguments.GetAt(0);
                //}
                //else
                //{
                //    metaType = type;
                //}

                //IBdoMetaData metaValue = null;
                //var meta = data as BdoMetaData;

                //if (typeof(ITBdoMetaScalar<>).IsAssignableFrom(metaType)
                //    && itemType?.IsScalar() == true)
                //{
                //    var scalar = meta as IBdoMetaScalar;
                //    metaValue = (typeof(TBdoMetaScalar<>).MakeGenericType(itemType).CreateInstance() as BdoMetaScalar)
                //        .WithName(name)
                //        .WithData(scalar?.GetData());
                //}
                //else if (typeof(IBdoMetaScalar).IsAssignableFrom(metaType))
                //{
                //    var scalar = meta as IBdoMetaScalar;
                //    metaValue = NewScalar(name, scalar?.DataValueType)
                //        .WithData(scalar?.GetData());
                //}
                //else if (typeof(IBdoConfiguration).IsAssignableFrom(metaType))
                //{
                //    var config = meta as IBdoConfiguration;
                //    metaValue = BdoConfig.New(name)
                //        .With(config.Items?.ToArray());
                //}
                //else if (typeof(IBdoMetaSet).IsAssignableFrom(metaType))
                //{
                //    var set = meta as IBdoMetaSet;
                //    metaValue = NewSet(name)
                //        .With(set?.Items?.ToArray());
                //}
                //else if (typeof(ITBdoMetaObject<>).IsAssignableFrom(metaType)
                //    && itemType != null)
                //{
                //    var obj = meta as IBdoMetaObject;
                //    metaValue = (typeof(TBdoMetaObject<>).MakeGenericType(itemType).CreateInstance() as BdoMetaObject)
                //        .WithName(name)
                //        .WithData(obj?.GetData());
                //}
                //else if (typeof(IBdoMetaObject).IsAssignableFrom(metaType))
                //{
                //    var obj = meta as IBdoMetaObject;
                //    metaValue = NewObject(name)
                //        .WithData(obj.GetData());
                //}

                //if (metaValue != null && meta != null)
                //{
                //    metaValue
                //        .WithSpecs(meta.Specs?.ToArray())
                //        .WithDataMode(meta.DataMode)
                //        .WithDataReference(meta.Reference);
                //}

                //return metaValue;
            }
            else if (type.IsList())
            {
                var objList = data.ToObjectArray();

                var metaSet = NewSet(name);
                if (objList != null)
                {
                    foreach (var obj in objList)
                    {
                        metaSet.InsertData(obj);
                    }
                }
                return metaSet;
            }
            else
            {
                var metaObj = NewObject(name, data);
                return metaObj;
            }
        }
    }
}
