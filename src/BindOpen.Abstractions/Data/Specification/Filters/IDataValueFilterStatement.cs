using BindOpen.Data.Items;
using System.Collections.Generic;

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