using BindOpen.Script;

namespace BindOpen.Data.Meta
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
                meta.WithDataMode(DataMode.Reference);
                meta.Reference = BdoData.NewRef(text, kind);
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
                meta.WithDataMode(DataMode.Reference);
                meta.Reference = BdoData.NewRef(word);
            }

            return meta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="modes"></param>
        public static T WithSpecs<T>(
            this T meta,
            params IBdoSpec[] specs)
            where T : IBdoMetaData
        {
            if (meta != null)
            {
                meta.Specs = BdoMeta.NewSpecSet(specs);
            }
            return meta;
        }
    }
}
