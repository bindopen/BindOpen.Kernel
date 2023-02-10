using BindOpen.Data.Meta;
using BindOpen.Extensions.Scripting;
using BindOpen.Logging;
using BindOpen.Runtime.Scopes;
using System;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a meta data.
    /// </summary>
    public static partial class BdoMetaDataExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            string text,
            BdoExpressionKind kind = BdoExpressionKind.Auto)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.DataReference = BdoData.NewExp(text, kind);
            }

            return meta;
        }


        /// <summary>
        /// 
        /// </summary>
        public static T WithDataReference<T>(
            this T meta,
            IBdoScriptword word)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.DataReference = BdoData.NewExp(word);
            }

            return meta;
        }


        /// <summary>
        /// Creates a meta data of the specified object.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="items">The items to consider.</param>
        public static IBdoMetaData ToMetaData(
            this object obj,
            string name = null,
            IBdoScope scope = null,
            IBdoLog log = null)
        {
            var meta = BdoMeta.New(name, obj);
            if (meta is IBdoMetaObject metaObj)
            {
                metaObj.With(
                    obj.ToMetaArray(
                        metaObj.GetClassType(scope, log)));
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
            => obj.ToMetaList(type, onlyMetaAttributes)?.ToArray();
    }
}
