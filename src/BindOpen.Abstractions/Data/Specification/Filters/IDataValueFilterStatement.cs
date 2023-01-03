using System.Collections.Generic;
using BindOpen.Data.Items;

namespace BindOpen.Data.Specification
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