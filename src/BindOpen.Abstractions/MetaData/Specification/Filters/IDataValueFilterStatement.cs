using BindOpen.MetaData.Items;
using System.Collections.Generic;

namespace BindOpen.MetaData.Specification
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDataValueFilterStatement : IBdoItem
    {
        /// <summary>
        /// 
        /// </summary>
        List<IDataValueFilter> Filters { get; set; }
    }
}