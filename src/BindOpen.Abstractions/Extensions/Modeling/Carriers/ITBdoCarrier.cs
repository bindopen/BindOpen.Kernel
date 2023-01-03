using System;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITBdoCarrier<T> : IBdoCarrier
        where T : IBdoCarrier
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="relativePath"></param>
        new T WithPath(string path = null, string relativePath = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        new T WithParentPath(string path);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        new T WithCreationDate(DateTime? date);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        new T WithFlag(string flag);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        new T AsReadonly(bool readOnly = false);

        /// <summary>
        /// 
        /// </summary>
        new T WithLastAccessDate(DateTime? date);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        new T WithLastWriteDate(DateTime? date);
    }
}