using System;

namespace BindOpen.MetaData
{
    /// <summary>
    /// This interface defines a storable data item.
    /// </summary>
    public interface ITStorablePoco<T> : IStorable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        T WithCreationDate(DateTime? date);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        T WithLastModificationDate(DateTime? date);
    }
}
