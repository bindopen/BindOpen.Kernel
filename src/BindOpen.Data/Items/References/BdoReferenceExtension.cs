using BindOpen.Data.Items;
using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public static class BdoReferenceExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reference"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T WithSource<T>(
            this T reference,
            IBdoMetaData data)
            where T : IBdoReference
        {
            if (reference != null)
            {
                reference.Source = data;
            }
            return reference;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reference"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T WithDataHandlerUniqueName<T>(
            this T reference,
            string uniqueName)
            where T : IBdoReference
        {
            if (reference != null)
            {
                reference.DataHandlerUniqueName = uniqueName;
            }
            return reference;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        public static T WithPathDetail<T>(
            this T reference,
            params IBdoMetaData[] elms)
            where T : IBdoReference
        {
            if (reference != null)
            {
                reference.PathDetail = BdoMeta.NewList(elms);
            }
            return reference;
        }
    }
}