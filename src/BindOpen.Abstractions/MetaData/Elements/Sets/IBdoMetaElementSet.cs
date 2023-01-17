using BindOpen.MetaData.Items;
using System.Collections.Generic;

namespace BindOpen.MetaData.Elements
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface IBdoMetaElementSet : ITBdoItemSet<IBdoMetaElement>
    {
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
        IBdoMetaElement Get(string name, string specId = null);
    }
}