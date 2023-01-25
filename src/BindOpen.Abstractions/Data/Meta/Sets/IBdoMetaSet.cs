using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface IBdoMetaSet : ITBdoItemSet<IBdoMetaData>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        IBdoMetaSet FromObject(object obj);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<string> GetSpecIds();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="specId"></param>
        /// <returns></returns>
        IBdoMetaData Get(string name, string specId = null);
    }
}