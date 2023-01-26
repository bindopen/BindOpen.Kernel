﻿using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This interface represents a named data.
    /// </summary>
    public static class IDetailedExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="detail"></param>
        public static T WithDetail<T>(
            this T detailed,
            params IBdoMetaData[] elms)
            where T : IDetailed
        {
            if (detailed != null)
            {
                detailed.Detail = BdoMeta.NewSet(elms);
            }
            return detailed;
        }
    }
}
